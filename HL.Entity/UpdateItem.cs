namespace Entity
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct UpdateItem
    {
        public EnumUpdateItem Item;
        public int Quantity;
        public UpdateItem(EnumUpdateItem updateItem, int count)
        {
            this.Quantity = count;
            this.Item = updateItem;
        }
    }
}

