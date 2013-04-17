using System;

using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;

namespace Entity
{
    /// <summary>
    /// PDA采购收货任务对象
    /// </summary>
    public class ZMES_Task
    {
        private int _index;
        private string _taskId;
        private Dictionary<int, ZMES_Delivery> _items = new Dictionary<int, ZMES_Delivery>();

        public virtual void AddItem(ZMES_Delivery item)
        {
            this._index++;
            this._items.Add(this._index, item);
        }

        public void RemoveItem(int index)
        {
            this._items.Remove(index);
        }

        public Dictionary<int, ZMES_Delivery> Items
        {
            get
            {
                return this._items;
            }
        }

        public void Clear()
        {
            this._items.Clear();
        }

        public string Task_ID
        {
            get { return _taskId; }
            set { this._taskId = value; }
        }
    }
}
