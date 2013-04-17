using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;

namespace HL.Framework.Utils
{
    public static class MachineControl
    {      
        private static Int32 METHOD_BUFFERED = 0;
        private static Int32 FILE_ANY_ACCESS = 0;
        private static Int32 FILE_DEVICE_HAL = 0x00000101;
        private const Int32 ERROR_NOT_SUPPORTED = 0x32;
        private const Int32 ERROR_INSUFFICIENT_BUFFER = 0x7A;
        private static Int32 IOCTL_HAL_GET_DEVICEID = ((FILE_DEVICE_HAL) << 16) | ((FILE_ANY_ACCESS) << 14) | ((21) << 2) | (METHOD_BUFFERED);


        [DllImport("coredll.dll", SetLastError = true)]
        private static extern bool KernelIoControl(Int32 dwIoControlCode, IntPtr lpInBuf, Int32 nInBufSize, byte[] lpOutBuf, Int32 nOutBufSize, ref Int32 lpBytesReturned);


        public static string GetDeviceID()
        {
            // Initialize the output buffer to the size of a Win32 DEVICE_ID structure
            byte[] outbuff = new byte[20];
            Int32 dwOutBytes;
            bool done = false;

            Int32 nBuffSize = outbuff.Length;

            // Set DEVICEID.dwSize to size of buffer.  Some platforms look at
            // this field rather than the nOutBufSize param of KernelIoControl
            // when determining if the buffer is large enough.
            //
            BitConverter.GetBytes(nBuffSize).CopyTo(outbuff, 0);
            dwOutBytes = 0;


            // Loop until the device ID is retrieved or an error occurs
            while (!done)
            {
                if (KernelIoControl(IOCTL_HAL_GET_DEVICEID, IntPtr.Zero, 0, outbuff, nBuffSize, ref dwOutBytes))
                {
                    done = true;
                }
                else
                {
                    int error = Marshal.GetLastWin32Error();
                    switch (error)
                    {
                        case ERROR_NOT_SUPPORTED:
                            throw new NotSupportedException("IOCTL_HAL_GET_DEVICEID is not supported on this device", new Win32Exception(error));

                        case ERROR_INSUFFICIENT_BUFFER:
                            // The buffer wasn't big enough for the data.  The
                            // required size is in the first 4 bytes of the output
                            // buffer (DEVICE_ID.dwSize).
                            nBuffSize = BitConverter.ToInt32(outbuff, 0);
                            outbuff = new byte[nBuffSize];

                            // Set DEVICEID.dwSize to size of buffer.  Some
                            // platforms look at this field rather than the
                            // nOutBufSize param of KernelIoControl when
                            // determining if the buffer is large enough.
                            //
                            BitConverter.GetBytes(nBuffSize).CopyTo(outbuff, 0);
                            break;

                        default:
                            throw new Win32Exception(error, "Unexpected error");
                    }
                }
            }

            Int32 dwPresetIDOffset = BitConverter.ToInt32(outbuff, 0x4);    // DEVICE_ID.dwPresetIDOffset
            Int32 dwPresetIDSize = BitConverter.ToInt32(outbuff, 0x8);      // DEVICE_ID.dwPresetSize
            Int32 dwPlatformIDOffset = BitConverter.ToInt32(outbuff, 0xc);  // DEVICE_ID.dwPlatformIDOffset
            Int32 dwPlatformIDSize = BitConverter.ToInt32(outbuff, 0x10);   // DEVICE_ID.dwPlatformIDBytes
            StringBuilder sb = new StringBuilder();

            for (int i = dwPresetIDOffset; i < dwPresetIDOffset + dwPresetIDSize; i++)
            {
                sb.Append(String.Format("{0:X2}", outbuff[i]));
            }

            sb.Append("-");
            for (int i = dwPlatformIDOffset; i < dwPlatformIDOffset + dwPlatformIDSize; i++)
            {
                sb.Append(String.Format("{0:X2}", outbuff[i]));

            }
            return sb.ToString();

        }
     
     

        #region 简单的加密
        public static string SimpleEncode(string input)
        {
            StringBuilder sb = new StringBuilder();
            char[] chars = input.ToCharArray();        
            int len = chars.Length;
            //首先swap一下
            for (int i = 0; i < len / 2; i++)
            {
                char tmp = chars[i];
                chars[i] = chars[len - i - 1];
                chars[len - i - 1] = tmp;
            }
            //偶序位字母加１，奇序位字母减１，除了-
            for (int i = 0; i < len; i++)
            {
                char cap = chars[i];
                if (cap == '-')
                    continue;
                if (i % 2 == 0) //偶
                {
                    if ((cap >= '0') && (cap <= '8') || (cap >= 'A') && (cap <= 'Y'))
                        chars[i]++;
                }
                else
                {
                    if ((cap >= '1') && (cap <= '9') || (cap >= 'B') && (cap <= 'Z'))
                        chars[i]--;

                }
            }
         
            int sum = 0;
            //取1/4长度的串，把它们的值加起来
            for (int i = 0; i < len / 4; i++)
            {
                if ((chars[i] >= '0') && (chars[i] <= '9'))
                    sum += Int32.Parse(chars[i].ToString());
            }
            //最后的串为：6位的1/4长度的串和加上减去1/5的串
            sb.Append(String.Format("{0:X6}", sum));
            for (int i = 0; i < (len - len / 5); i++)
            {
                sb.Append(String.Format("{0:X2}", chars[i]));
            }
            return sb.ToString();

        }
        #endregion

        

    }
}
