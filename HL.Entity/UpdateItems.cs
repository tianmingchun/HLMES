namespace Entity
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class UpdateItems
    {
        private ArrayList items = new ArrayList();

        public void AddItems(UpdateItem item)
        {
            this.items.Add(item);
        }

        public int Count
        {
            get
            {
                return this.items.Count;
            }
        }

        public UpdateItem this[int index]
        {
            get
            {
                return (UpdateItem) this.items[index];
            }
        }

        public ArrayList Items
        {
            get
            {
                return this.items;
            }
        }
    }
}

