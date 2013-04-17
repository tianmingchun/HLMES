namespace Entity
{
    using System;
    using System.Collections;

    public class SortedHashTable : Hashtable
    {
        private ArrayList list = new ArrayList();

        public override void Add(object key, object value)
        {
            base.Add(key, value);
            this.list.Add(key);
        }

        public override void Clear()
        {
            base.Clear();
            this.list.Clear();
        }

        public override void Remove(object key)
        {
            base.Remove(key);
            this.list.Remove(key);
        }

        public override ICollection Keys
        {
            get
            {
                return this.list;
            }
        }
    }
}

