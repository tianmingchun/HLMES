namespace BizLayer
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ElementRegex
    {
        public string Name;
        public int Start;
        public int End;
    }
}

