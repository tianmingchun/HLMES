namespace Entity
{
    using System;

    public class CheckOutTask : CheckTask
    {
        private bool forceBatch;
        private bool forceSite;

        public CheckOutTask(string id, bool isForceSite, bool isForceBatch) : base(id)
        {
            this.forceSite = isForceSite;
            this.forceBatch = isForceBatch;
        }

        public bool ForceBatch
        {
            get
            {
                return this.forceBatch;
            }
        }

        public bool ForceSite
        {
            get
            {
                return this.forceSite;
            }
        }
    }
}

