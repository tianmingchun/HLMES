namespace DLLImport
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ModemInformation
    {
        public char[] Manufacture;
        public char[] Model;
        public char[] Revision;
        public char[] IMEI;
        public char[] IMSI;
    }
}

