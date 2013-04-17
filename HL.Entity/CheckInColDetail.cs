namespace Entity
{
    using System;

    public class CheckInColDetail : ColDetail
    {
        private string produceBatchNo;

        public string ProduceBatchNo
        {
            get
            {
                return this.produceBatchNo;
            }
            set
            {
                this.produceBatchNo = value;
            }
        }
    }
}

