namespace BizLayer
{
    using System;
    using System.Runtime.CompilerServices;

    public interface IDataCollection
    {
        event PerformBarcode Perform;

        void Dispose();
        int GetKeyValue();
        void ScanBarcode(bool blocked);
    }
}

