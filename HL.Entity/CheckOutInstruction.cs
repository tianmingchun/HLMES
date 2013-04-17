namespace Entity
{
    using System;

    public class CheckOutInstruction : CheckInstruction
    {
        private string batchid;

        public CheckOutInstruction(string id, decimal required) : base(id, required)
        {
        }

        public string BatchID
        {
            get
            {
                return this.batchid;
            }
            set
            {
                this.batchid = value;
            }
        }
    }
}

