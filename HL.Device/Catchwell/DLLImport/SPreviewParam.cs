namespace DLLImport
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct SPreviewParam
    {
        public int Width;
        public int Height;
        public int PreviewX;
        public int PreviewY;
    }
}

