using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace LC
{
    public enum DeviceSearchType
    {
        DeviceSearchByLegacyName,
        DeviceSearchByDeviceName,
        DeviceSearchByBusName,
        DeviceSearchByGuid,
        DeviceSearchByParent
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DEVMGR_DEVICE_INFORMATION
    {
        public uint dwSize;
        public IntPtr hDevice;
        public IntPtr hParentDevice;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 6)]
        public String szLegacyName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public String szDeviceKey;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public String szDeviceName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public String szBusName;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PORTEMUPortParams
    {
    	public int		channel;
	    public int		flocal;
	    public ulong 	device;
	    public int		imtu;
	    public int		iminmtu;
	    public int		imaxmtu;
	    public int		isendquota;
	    public int		irecvquota;
	    public Guid     uuidService;
	    public uint	    uiportflags;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SOCKADDR_BTH
    {
         public ushort  addressFamily;
         public ulong btAddr;
         public Guid serviceClassId;
         public uint port;
    }
 

    [StructLayout(LayoutKind.Sequential)]
    public struct SOCKET_ADDRESS
    {
        public IntPtr lpSockaddr;
        public int iSockaddrLength;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CSADDR_INFO
    {
        public SOCKET_ADDRESS LocalAddr;
        public SOCKET_ADDRESS RemoteAddr;
        public int iSocketType;
        public int iProtocol;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct WSAQUERYSET
    {
        public uint dwSize;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpszServiceInstanceName;
        public IntPtr lpServiceClassId;
        public IntPtr lpVersion;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpszComment;
        public uint dwNameSpace;
        public IntPtr lpNSProviderId;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpszContext;
        public uint dwNumberOfProtocols;
        public IntPtr lpafpProtocols;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpszQueryString;
        public uint dwNumberOfCsAddrs;
        public IntPtr lpcsaBuffer;
        public uint dwOutputFlags;
        public IntPtr lpBlob;
    } 


    public class NativeMethod
    {
        public static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);


        public const uint GENERIC_READ = 0x80000000;
        public const uint GENERIC_WRITE = 0x40000000;
        public const uint FILE_SHARE_READ = 0x00000001;
        public const uint FILE_SHARE_WRITE = 0x00000002;
        public const uint OPEN_EXISTING = 3;
        public const uint FILE_ATTRIBUTE_NORMAL = 0x80;

        public const uint IOCTL_BLUETOOTH_GET_PEER_DEVICE = 0x001b0064;

        public const int ERROR_SUCCESS = 0;

        public const int NS_BTH = 16;

        public const int WSA_E_NO_MORE = 10110;


        public const uint LUP_CONTAINERS = 0x0002;
        public const uint LUP_RES_SERVICE = 0x8000;
        public const uint LUP_RETURN_NAME = 0x0010;
        public const uint LUP_RETURN_ADDR = 0x0100;
        public const uint LUP_RETURN_BLOB = 0x0200;
        public const uint BTHNS_LUP_RESET_ITERATOR = 0x00010000;
        public const uint BTHNS_LUP_NO_ADVANCE = 0x00020000;
        public const uint BTHNS_ABORT_CURRENT_INQUIRY = 0xfffffffd;


        [DllImport("coredll.dll", SetLastError = true)]
        public static extern IntPtr FindFirstDevice(DeviceSearchType searchType, [MarshalAs(UnmanagedType.LPWStr)]string pvSearchParam, ref DEVMGR_DEVICE_INFORMATION pdi);

        [DllImport("coredll.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FindNextDevice(IntPtr h, ref DEVMGR_DEVICE_INFORMATION pdi);

        [DllImport("coredll.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FindClose(IntPtr handle);


        [DllImport("coredll.dll", SetLastError = true)]
        public static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);

        [DllImport("coredll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeviceIoControl(IntPtr hDevice, uint dwIoControlCode, IntPtr lpInBuffer, uint nInBufferSize, ref ulong lpOutBuffer, uint nOutBufferSize, out uint lpBytesReturned, IntPtr lpOverlapped);

        [DllImport("coredll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr hObject); 


        [DllImport("coredll", SetLastError = true)]
        internal static extern IntPtr RegisterDevice([MarshalAs(UnmanagedType.LPWStr)]string lpszType, uint dwIndex, [MarshalAs(UnmanagedType.LPWStr)]string lpszLib, ref PORTEMUPortParams portInfo);

        [DllImport("coredll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeregisterDevice(IntPtr hDevice);



        [DllImport("btdrt.dll", SetLastError = true)]
        public static extern int BthNsLookupServiceBegin(ref WSAQUERYSET pQuerySet, uint dwFlags, ref IntPtr lphLookup);

        [DllImport("btdrt.dll", SetLastError = true)]
        public static extern int BthNsLookupServiceNext(IntPtr hLookup, uint dwFlags, ref uint lpdwBufferLength, IntPtr pResults);

        [DllImport("btdrt.dll", SetLastError = true)]
        public static extern int BthNsLookupServiceEnd(IntPtr hLookup);

        [DllImport("btdrt.dll", SetLastError = true)]
        public static extern int BthPairRequest(ref ulong pba, int cPinLength, byte[] ppin);

        [DllImport("btdrt.dll", SetLastError = true)]
        public static extern int BthSetPIN(ref ulong pba, int cPinLength, byte[] ppin);

        [DllImport("btdrt.dll", SetLastError = true)]
        public static extern int BthRevokePIN(ref ulong pba);
       
        [DllImport("btdrt.dll", SetLastError = true)]
        public static extern int BthRevokeLinkKey(ref ulong pba);
            
    }
}
