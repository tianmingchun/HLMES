using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections;
using System.Windows.Forms;


namespace LC
{

    public class DeviceInfo
    {
        public DeviceInfo(string name, string addr)
        {
            _name = name;
            _addr = addr;

        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _addr;
        public string Address
        {
            get { return _addr; }
            set { _addr = value; }
        }
    }

    public class InquiryResultEventArgs : EventArgs
    {
        DeviceInfo _info;

        public LC.DeviceInfo Info
        {
            get { return _info; }
            set { _info = value; }
        }

     
        public InquiryResultEventArgs(DeviceInfo info)
        {
            this._info = info;
        }
    }

    public class Bluetooth
    {
        public event EventHandler<InquiryResultEventArgs> InquiryResult;

        private System.IO.Ports.SerialPort port;
        public System.IO.Ports.SerialPort Port
        {
            get { return port; }
            set { port = value; }
        }

        private IntPtr comDevice;
        private string comPortName = "BSP8:";

        public Bluetooth()
        {
            port = new System.IO.Ports.SerialPort(comPortName);
        }

        public bool IsConnected
        {
            get { return port.IsOpen; }
        }
        /// <summary>
        /// 连接蓝牙设备
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="pin"></param>
        /// <returns></returns>
        public bool Connect(string addr)
        {
            comDevice = RegisterComPort(addr, comPortName);
            if (comDevice != IntPtr.Zero)
            {
                try
                {
                    if (!port.IsOpen)
                    {
                        port.Open();
                    }
                }
                catch(Exception ex)
                {
                    Disconnect();
                }
            }

            return port.IsOpen;
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public void Disconnect()
        {
            if (port.IsOpen)
            {
                port.Close();
            }
            UnregisterCOMPort(ref comDevice);
        }

        /// <summary>
        /// 搜索设备
        /// </summary>
        /// <returns></returns>
        public bool DiscoverDevice()
        {
            bool bRet = false;

            IntPtr hLookup = IntPtr.Zero;
            if (PerformInquiry(false, ref hLookup) == NativeMethod.ERROR_SUCCESS)
            {
                if (PerformInquiry(true, ref hLookup) == NativeMethod.ERROR_SUCCESS)
                {
                    bRet = true;
                }
            }

            return bRet;
        }


        private int PerformInquiry(bool fDoNames, ref IntPtr hLookup)
        {
       	    int iRet = NativeMethod.ERROR_SUCCESS;

	        WSAQUERYSET		wsaq = new WSAQUERYSET();
	        wsaq.dwSize      = (uint)Marshal.SizeOf(typeof(WSAQUERYSET));
	        wsaq.dwNameSpace = NativeMethod.NS_BTH;

	        if (!fDoNames)
            {
		        // perform initial device inquiry
		        iRet = NativeMethod.BthNsLookupServiceBegin (ref wsaq, NativeMethod.LUP_CONTAINERS, ref hLookup);
	        }
            else
            {
		        // reset iterator to front of list to find names
		        uint dwUnused = 0;
		        iRet = NativeMethod.BthNsLookupServiceNext (hLookup, NativeMethod.BTHNS_LUP_RESET_ITERATOR, ref dwUnused, IntPtr.Zero);
	        }
        	
	        if (iRet != NativeMethod.ERROR_SUCCESS)
            {
		        return iRet;
            }

	        while (iRet == NativeMethod.ERROR_SUCCESS)
            {
                uint dwSize = 1024;
                IntPtr pwsaResults = Marshal.AllocHGlobal((int)dwSize);

                wsaq = new WSAQUERYSET();
	            wsaq.dwSize      = (uint)Marshal.SizeOf(typeof(WSAQUERYSET));
	            wsaq.dwNameSpace = NativeMethod.NS_BTH;
                wsaq.lpBlob = IntPtr.Zero;

                Marshal.StructureToPtr(wsaq, pwsaResults, false);

                iRet = NativeMethod.BthNsLookupServiceNext(hLookup, (fDoNames ? NativeMethod.LUP_RETURN_NAME : 0) | NativeMethod.LUP_RETURN_ADDR, ref dwSize, pwsaResults);

                if (iRet == NativeMethod.ERROR_SUCCESS)
                {
                    wsaq = (WSAQUERYSET)Marshal.PtrToStructure(pwsaResults, typeof(WSAQUERYSET));
                    if (wsaq.dwNumberOfCsAddrs == 1)
                    {
                        CSADDR_INFO csaBuffer = (CSADDR_INFO)Marshal.PtrToStructure(wsaq.lpcsaBuffer, typeof(CSADDR_INFO));

                        ulong btAddr = ((SOCKADDR_BTH)Marshal.PtrToStructure(csaBuffer.RemoteAddr.lpSockaddr, typeof(SOCKADDR_BTH))).btAddr;

                        if (InquiryResult != null)
                        {
                            InquiryResult(this, new InquiryResultEventArgs(new DeviceInfo(fDoNames ? wsaq.lpszServiceInstanceName : null, btAddr.ToString("X12"))));
                        }
                    }

                }
                else	//BthNsLookupServiceNext returns SOCKET_ERROR and sets last error
                {
                    iRet = Marshal.GetLastWin32Error();
                }

                Marshal.FreeHGlobal(pwsaResults);
	        }

	        return ((iRet == NativeMethod.WSA_E_NO_MORE) ? NativeMethod.ERROR_SUCCESS : iRet);
        }


        /// <summary>
        /// 匹配设备
        /// </summary>
        /// <param name="mac"></param>
        /// <param name="pin"></param>
        /// <returns></returns>
        public bool Pair(string addr, string pin)
        {
            if (pin.Length < 1 | pin.Length > 16)
            {
                throw new ArgumentException("Pin must be between 1 and 16 characters long.");
            }

            ulong btAddr = ulong.Parse(addr, System.Globalization.NumberStyles.HexNumber);
            byte[] pinbytes = System.Text.Encoding.ASCII.GetBytes(pin);

            int result = NativeMethod.BthSetPIN(ref btAddr, pinbytes.Length, pinbytes);
            if (result != 0)
            {
                throw new Exception(String.Format("BthSetPIN error {0}", result));
            }

            return true;
        }

        public void UnPair(string addr)
        {
            ulong btAddr = ulong.Parse(addr, System.Globalization.NumberStyles.HexNumber);

            NativeMethod.BthRevokeLinkKey(ref btAddr);
            NativeMethod.BthRevokePIN(ref btAddr);
        }

    
        /// <summary>
        /// 检测虚拟端口
        /// </summary>
        /// <param name="comPort"></param>
        /// <returns></returns>
        private IntPtr FindComPort(string portName)
        {
            IntPtr hDevice = IntPtr.Zero;

            DEVMGR_DEVICE_INFORMATION ddi = new DEVMGR_DEVICE_INFORMATION();
            ddi.dwSize = (uint)Marshal.SizeOf(ddi);
            IntPtr findHandle = NativeMethod.FindFirstDevice(DeviceSearchType.DeviceSearchByLegacyName, portName, ref ddi);
            if (findHandle != NativeMethod.INVALID_HANDLE_VALUE)
            {
                hDevice = ddi.hDevice;
                NativeMethod.FindClose(findHandle);
            }

            return hDevice;
        }

        /// <summary>
        /// 注册虚拟COM口
        /// </summary>
        /// <param name="addr"></param>
        private IntPtr RegisterComPort(string addr, string portName)
        {
            //if (pin.Length < 1 | pin.Length > 16)
            //{
            //    throw new ArgumentException("Pin must be between 1 and 16 characters long.");
            //}

            ulong btAddr = ulong.Parse(addr, System.Globalization.NumberStyles.HexNumber);

            bool bRegistered = false;

            IntPtr hDevice = FindComPort(portName);
            if (hDevice != IntPtr.Zero)
            {
                IntPtr hFile = NativeMethod.CreateFile(portName, NativeMethod.GENERIC_READ | NativeMethod.GENERIC_WRITE, 0, IntPtr.Zero, NativeMethod.OPEN_EXISTING, NativeMethod.FILE_ATTRIBUTE_NORMAL, IntPtr.Zero);
                if (hFile != NativeMethod.INVALID_HANDLE_VALUE)
                {
                    ulong pba = 0;
                    uint bytesReturned;
                    if (NativeMethod.DeviceIoControl(hFile, NativeMethod.IOCTL_BLUETOOTH_GET_PEER_DEVICE, IntPtr.Zero, 0, ref pba, 8, out bytesReturned, IntPtr.Zero))
                    {
                        if (pba == btAddr)
                        {
                            bRegistered = true;
                        }
                    }
                    NativeMethod.CloseHandle(hFile);
                }
            }

            if (bRegistered)
            {
                return hDevice;
            }

            if (hDevice != IntPtr.Zero)
            {
                UnregisterCOMPort(ref hDevice);
            }

            PORTEMUPortParams portInfo = new PORTEMUPortParams();
            portInfo.device = btAddr;
            portInfo.channel = 1;

            hDevice = NativeMethod.RegisterDevice("BSP", 8, "btd.dll", ref portInfo);  
            if (hDevice == IntPtr.Zero)
            {
                int error = Marshal.GetLastWin32Error();
            }

            return hDevice;
        }

        /// <summary>
        /// 注销虚拟COM
        /// </summary>
        /// <param name="comm"></param>
        private void UnregisterCOMPort(ref IntPtr hComPort)
        {
            if (hComPort != IntPtr.Zero)
            {
                NativeMethod.DeregisterDevice(hComPort);
                hComPort = IntPtr.Zero;
            }
        }


    }

}
