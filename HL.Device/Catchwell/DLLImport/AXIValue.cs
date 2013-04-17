namespace DLLImport
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct AXIValue
    {
        public int axiAzimuth;
        public int axiPitch;
        public int axiRoll;
        public int axiAcc_x;
        public int axiAcc_y;
        public int axiAcc_z;
        public int axiMag_x;
        public int axiMag_y;
        public int axiMag_z;
    }
}

