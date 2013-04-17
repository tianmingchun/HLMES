namespace HL.Framework
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class ShellExecute
    {
        public const uint BAUD_075 = 1;
        public const uint BAUD_110 = 2;
        public const uint BAUD_115200 = 0x20000;
        public const uint BAUD_1200 = 0x40;
        public const uint BAUD_128K = 0x10000;
        public const uint BAUD_134_5 = 4;
        public const uint BAUD_14400 = 0x1000;
        public const uint BAUD_150 = 8;
        public const uint BAUD_1800 = 0x80;
        public const uint BAUD_19200 = 0x2000;
        public const uint BAUD_2400 = 0x100;
        public const uint BAUD_300 = 0x10;
        public const uint BAUD_38400 = 0x4000;
        public const uint BAUD_4800 = 0x200;
        public const uint BAUD_56K = 0x8000;
        public const uint BAUD_57600 = 0x40000;
        public const uint BAUD_600 = 0x20;
        public const uint BAUD_7200 = 0x400;
        public const uint BAUD_9600 = 0x800;
        public const uint BAUD_USER = 0x10000000;
        public const uint CBR_110 = 110;
        public const uint CBR_115200 = 0x1c200;
        public const uint CBR_1200 = 0x4b0;
        public const uint CBR_128000 = 0x1f400;
        public const uint CBR_14400 = 0x3840;
        public const uint CBR_19200 = 0x4b00;
        public const uint CBR_2400 = 0x960;
        public const uint CBR_256000 = 0x3e800;
        public const uint CBR_300 = 300;
        public const uint CBR_38400 = 0x9600;
        public const uint CBR_4800 = 0x12c0;
        public const uint CBR_56000 = 0xdac0;
        public const uint CBR_57600 = 0xe100;
        public const uint CBR_600 = 600;
        public const uint CBR_9600 = 0x2580;
        public const uint CE_BREAK = 0x10;
        public const uint CE_DNS = 0x800;
        public const uint CE_FRAME = 8;
        public const uint CE_IOE = 0x400;
        public const uint CE_MODE = 0x8000;
        public const uint CE_OOP = 0x1000;
        public const uint CE_OVERRUN = 2;
        public const uint CE_PTO = 0x200;
        public const uint CE_RXOVER = 1;
        public const uint CE_RXPARITY = 4;
        public const uint CE_TXFULL = 0x100;
        public const uint CLRBREAK = 9;
        public const uint CLRDTR = 6;
        public const uint CLRIR = 11;
        public const uint CLRRTS = 4;
        public const uint CREATE_ALWAYS = 2;
        public const uint CREATE_NEW = 1;
        public const byte DATABITS_16 = 0x10;
        public const byte DATABITS_16X = 0x20;
        public const byte DATABITS_5 = 1;
        public const byte DATABITS_6 = 2;
        public const byte DATABITS_7 = 4;
        public const byte DATABITS_8 = 8;
        public const uint DTR_CONTROL_DISABLE = 0;
        public const uint DTR_CONTROL_ENABLE = 0x10;
        public const uint DTR_CONTROL_HANDSHAKE = 0x20;
        public const uint EV_BREAK = 0x40;
        public const uint EV_CTS = 8;
        public const uint EV_DSR = 0x10;
        public const uint EV_ERR = 0x80;
        public const uint EV_EVENT1 = 0x800;
        public const uint EV_EVENT2 = 0x1000;
        public const uint EV_PERR = 0x200;
        public const uint EV_POWER = 0x2000;
        public const uint EV_RING = 0x100;
        public const uint EV_RLSD = 0x20;
        public const uint EV_RX80FULL = 0x400;
        public const uint EV_RXCHAR = 1;
        public const uint EV_RXFLAG = 2;
        public const uint EV_TXEMPTY = 4;
        public const byte EVENPARITY = 2;
        public const uint fAbortOnError = 0x4000;
        public const uint fBinary = 1;
        public const uint fCtsHold = 1;
        public const uint fDsrHold = 2;
        public const uint fDsrSensitivity = 0x40;
        public const uint fDtrControl = 0x30;
        public const uint fDummy = 0xffff8000;
        public const uint fEof = 0x20;
        public const uint fErrorChar = 0x400;
        public const uint FILE_ACTION_ADDED = 1;
        public const uint FILE_ACTION_MODIFIED = 3;
        public const uint FILE_ACTION_REMOVED = 2;
        public const uint FILE_ACTION_RENAMED_NEW_NAME = 5;
        public const uint FILE_ACTION_RENAMED_OLD_NAME = 4;
        public const uint FILE_ATTRIBUTE_ARCHIVE = 0x20;
        public const uint FILE_ATTRIBUTE_COMPRESSED = 0x800;
        public const uint FILE_ATTRIBUTE_DIRECTORY = 0x10;
        public const uint FILE_ATTRIBUTE_ENCRYPTED = 0x40;
        public const uint FILE_ATTRIBUTE_HIDDEN = 2;
        public const uint FILE_ATTRIBUTE_INROM = 0x40;
        public const uint FILE_ATTRIBUTE_NORMAL = 0x80;
        public const uint FILE_ATTRIBUTE_NOT_CONTENT_INDEXED = 0x2000;
        public const uint FILE_ATTRIBUTE_OFFLINE = 0x1000;
        public const uint FILE_ATTRIBUTE_READONLY = 1;
        public const uint FILE_ATTRIBUTE_REPARSE_POINT = 0x400;
        public const uint FILE_ATTRIBUTE_ROMMODULE = 0x2000;
        public const uint FILE_ATTRIBUTE_ROMSTATICREF = 0x1000;
        public const uint FILE_ATTRIBUTE_SPARSE_FILE = 0x200;
        public const uint FILE_ATTRIBUTE_SYSTEM = 4;
        public const uint FILE_ATTRIBUTE_TEMPORARY = 0x100;
        public const uint FILE_CASE_PRESERVED_NAMES = 2;
        public const uint FILE_CASE_SENSITIVE_SEARCH = 1;
        public const uint FILE_FILE_COMPRESSION = 0x10;
        public const uint FILE_FLAG_BACKUP_SEMANTICS = 0x2000000;
        public const uint FILE_FLAG_DELETE_ON_CLOSE = 0x4000000;
        public const uint FILE_FLAG_NO_BUFFERING = 0x20000000;
        public const uint FILE_FLAG_OVERLAPPED = 0x40000000;
        public const uint FILE_FLAG_POSIX_SEMANTICS = 0x1000000;
        public const uint FILE_FLAG_RANDOM_ACCESS = 0x10000000;
        public const uint FILE_FLAG_SEQUENTIAL_SCAN = 0x8000000;
        public const uint FILE_FLAG_WRITE_THROUGH = 0x80000000;
        public const uint FILE_NOTIFY_CHANGE_ATTRIBUTES = 4;
        public const uint FILE_NOTIFY_CHANGE_CREATION = 0x40;
        public const uint FILE_NOTIFY_CHANGE_DIR_NAME = 2;
        public const uint FILE_NOTIFY_CHANGE_FILE_NAME = 1;
        public const uint FILE_NOTIFY_CHANGE_LAST_ACCESS = 0x20;
        public const uint FILE_NOTIFY_CHANGE_LAST_WRITE = 0x10;
        public const uint FILE_NOTIFY_CHANGE_SECURITY = 0x100;
        public const uint FILE_NOTIFY_CHANGE_SIZE = 8;
        public const uint FILE_PERSISTENT_ACLS = 8;
        public const uint FILE_SHARE_DELETE = 4;
        public const uint FILE_SHARE_READ = 1;
        public const uint FILE_SHARE_WRITE = 2;
        public const uint FILE_SUPPORTS_ENCRYPTION = 0x20000;
        public const uint FILE_SUPPORTS_OBJECT_IDS = 0x10000;
        public const uint FILE_SUPPORTS_REMOTE_STORAGE = 0x100;
        public const uint FILE_SUPPORTS_REPARSE_POINTS = 0x80;
        public const uint FILE_SUPPORTS_SPARSE_FILES = 0x40;
        public const uint FILE_UNICODE_ON_DISK = 4;
        public const uint FILE_VOLUME_IS_COMPRESSED = 0x8000;
        public const uint FILE_VOLUME_QUOTAS = 0x20;
        public const uint fInX = 0x200;
        public const uint fNull = 0x800;
        public const uint fOutX = 0x100;
        public const uint fOutxCtsFlow = 4;
        public const uint fOutxDsrFlow = 8;
        public const uint fParity = 2;
        public const uint fReserved = 0xffffff80;
        public const uint fRlsdHold = 4;
        public const uint fRtsControl = 0x3000;
        public const uint fTXContinueOnXoff = 0x80;
        public const uint fTxim = 0x40;
        public const uint fXoffHold = 8;
        public const uint fXoffSent = 0x10;
        public const uint GENERIC_ALL = 0x10000000;
        public const uint GENERIC_EXECUTE = 0x20000000;
        public const uint GENERIC_READ = 0x80000000;
        public const uint GENERIC_WRITE = 0x40000000;
        public const int INVALID_HANDLE_VALUE = -1;
        public const int MAILSLOT_NO_MESSAGE = -1;
        public const int MAILSLOT_WAIT_FOREVER = -1;
        public const byte MARKPARITY = 3;
        public const uint MODULE_ATTR_NODEBUG = 0x400;
        public const uint MODULE_ATTR_NOT_TRUSTED = 0x200;
        public const uint MS_CTS_ON = 0x10;
        public const uint MS_DSR_ON = 0x20;
        public const uint MS_RING_ON = 0x40;
        public const uint MS_RLSD_ON = 0x80;
        public const byte NOPARITY = 0;
        public const byte ODDPARITY = 1;
        public const byte ONE5STOPBITS = 1;
        public const byte ONESTOPBIT = 0;
        public const uint OPEN_ALWAYS = 4;
        public const uint OPEN_EXISTING = 3;
        public const uint OPEN_FOR_LOADER = 6;
        public const uint PARITY_EVEN = 0x400;
        public const uint PARITY_MARK = 0x800;
        public const uint PARITY_NONE = 0x100;
        public const uint PARITY_ODD = 0x200;
        public const uint PARITY_SPACE = 0x1000;
        public const uint PCF_16BITMODE = 0x200;
        public const uint PCF_DTRDSR = 1;
        public const uint PCF_INTTIMEOUTS = 0x80;
        public const uint PCF_PARITY_CHECK = 8;
        public const uint PCF_RLSD = 4;
        public const uint PCF_RTSCTS = 2;
        public const uint PCF_SETXCHAR = 0x20;
        public const uint PCF_SPECIALCHARS = 0x100;
        public const uint PCF_TOTALTIMEOUTS = 0x40;
        public const uint PCF_XONXOFF = 0x10;
        public const uint PST_FAX = 0x21;
        public const uint PST_LAT = 0x101;
        public const uint PST_MODEM = 6;
        public const uint PST_NETWORK_BRIDGE = 0x100;
        public const uint PST_PARALLELPORT = 2;
        public const uint PST_RS232 = 1;
        public const uint PST_RS422 = 3;
        public const uint PST_RS423 = 4;
        public const uint PST_RS449 = 5;
        public const uint PST_SCANNER = 0x22;
        public const uint PST_TCPIP_TELNET = 0x102;
        public const uint PST_UNSPECIFIED = 0;
        public const uint PST_X25 = 0x103;
        public const uint RTS_CONTROL_DISABLE = 0;
        public const uint RTS_CONTROL_ENABLE = 0x1000;
        public const uint RTS_CONTROL_HANDSHAKE = 0x2000;
        public const uint RTS_CONTROL_TOGGLE = 0x3000;
        public const uint SETBREAK = 8;
        public const uint SETDTR = 5;
        public const uint SETIR = 10;
        public const uint SETRTS = 3;
        public const uint SETXOFF = 1;
        public const uint SETXON = 2;
        public const byte SP_BAUD = 2;
        public const byte SP_DATABITS = 4;
        public const byte SP_HANDSHAKING = 0x10;
        public const byte SP_PARITY = 1;
        public const byte SP_PARITY_CHECK = 0x20;
        public const byte SP_RLSD = 0x40;
        public const byte SP_STOPBITS = 8;
        public const byte SPACEPARITY = 4;
        public const uint STOPBITS_10 = 1;
        public const uint STOPBITS_15 = 2;
        public const uint STOPBITS_20 = 4;
        public const uint TRUNCATE_EXISTING = 5;
        public const byte TWOSTOPBITS = 2;

        [DllImport("coredll")]
        public static extern bool CloseHandle(IntPtr hObject);
        [DllImport("coredll")]
        public static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);
        public static void ExeFile(string sFilePath)
        {
            int size = (sFilePath.Length * 2) + 2;
            IntPtr destination = LocalAlloc(0x40, size);
            Marshal.Copy(Encoding.Unicode.GetBytes(sFilePath), 0, destination, size - 2);
            SHELLEXECUTEEX ex = new SHELLEXECUTEEX();
            ex.cbSize = 60;
            ex.dwHotKey = 0;
            ex.fMask = 0;
            ex.hIcon = IntPtr.Zero;
            ex.hInstApp = IntPtr.Zero;
            ex.hProcess = IntPtr.Zero;
            ex.lpClass = IntPtr.Zero;
            ex.lpDirectory = IntPtr.Zero;
            ex.lpIDList = IntPtr.Zero;
            ex.lpParameters = IntPtr.Zero;
            ex.lpVerb = IntPtr.Zero;
            ex.hwnd = IntPtr.Zero;
            ex.hkeyClass = IntPtr.Zero;
            ex.nShow = 1;
            ex.lpFile = destination;
            ShellExecuteEx(ex);
            LocalFree(destination);
        }

        public static string FILETIMEtoDataTime(FILETIME time)
        {
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(FILETIME)));
            IntPtr lpSystemTime = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SystemTime)));
            Marshal.StructureToPtr(time, ptr, false);
            FileTimeToSystemTime(ptr, lpSystemTime);
            SystemTime time2 = (SystemTime) Marshal.PtrToStructure(lpSystemTime, typeof(SystemTime));
            return (time2.wYear.ToString() + "." + time2.wMonth.ToString() + "." + time2.wDay.ToString() + "." + time2.wHour.ToString() + "." + time2.wMinute.ToString() + "." + time2.wSecond.ToString());
        }

        [DllImport("coredll")]
        private static extern int FileTimeToSystemTime(IntPtr lpFileTime, IntPtr lpSystemTime);
        [DllImport("coredll")]
        private static extern IntPtr LocalAlloc(int flags, int size);
        [DllImport("coredll")]
        private static extern void LocalFree(IntPtr ptr);
        [DllImport("coredll")]
        public static extern int SetFileTime(IntPtr hFile, ref FILETIME sysCreateTime, ref FILETIME sysAccessTime, ref FILETIME sysModifyTime);
        [DllImport("coredll")]
        public static extern bool SetLocalTime(ref SystemTime sysTime);
        [DllImport("coredll")]
        private static extern int ShellExecuteEx(SHELLEXECUTEEX ex);
        public static FILETIME SystemTimetoFILETIME(SystemTime time)
        {
            IntPtr lpFileTime = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(FILETIME)));
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SystemTime)));
            Marshal.StructureToPtr(time, ptr, false);
            SystemTimeToFileTime(ptr, lpFileTime);
            return (FILETIME) Marshal.PtrToStructure(lpFileTime, typeof(FILETIME));
        }

        [DllImport("coredll")]
        private static extern int SystemTimeToFileTime(IntPtr lpSystemTime, IntPtr lpFileTime);

        [StructLayout(LayoutKind.Sequential)]
        public struct FILETIME
        {
            private int dwLowDateTime;
            private int dwHighDateTime;
        }

        private class SHELLEXECUTEEX
        {
            public uint cbSize;
            public uint dwHotKey;
            public uint fMask;
            public IntPtr hIcon;
            public IntPtr hInstApp;
            public IntPtr hkeyClass;
            public IntPtr hProcess;
            public IntPtr hwnd;
            public IntPtr lpClass;
            public IntPtr lpDirectory;
            public IntPtr lpFile;
            public IntPtr lpIDList;
            public IntPtr lpParameters;
            public IntPtr lpVerb;
            public int nShow;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SystemTime
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMiliseconds;
        }
    }
}

