namespace Entity
{
    using System;

    public class ColDetail
    {
        private string batchNo;
        private DateTime colTime;
        private string goodsID;
        private Instruction ins;
        private string location;
        private decimal quantity;
        private string rowno;
        private string singletonNo;
        private string tiosno;
        private string unit;

        public Instruction AssociateInstruction
        {
            get
            {
                return this.ins;
            }
            set
            {
                this.ins = value;
            }
        }

        public string BatchNumber
        {
            get
            {
                return this.batchNo;
            }
            set
            {
                this.batchNo = value;
            }
        }

        public decimal CollectedQuantity
        {
            get
            {
                return this.quantity;
            }
            set
            {
                this.quantity = value;
            }
        }

        public DateTime CollectedTime
        {
            get
            {
                return this.colTime;
            }
            set
            {
                this.colTime = value;
            }
        }

        public string GoodsID
        {
            get
            {
                return this.goodsID;
            }
            set
            {
                this.goodsID = value;
            }
        }

        public string Location
        {
            get
            {
                return this.location;
            }
            set
            {
                this.location = value;
            }
        }

        public string Rowno
        {
            get
            {
                return this.rowno;
            }
            set
            {
                this.rowno = value;
            }
        }

        public string SingletonNo
        {
            get
            {
                return this.singletonNo;
            }
            set
            {
                this.singletonNo = value;
            }
        }

        public string Tiosno
        {
            get
            {
                return this.tiosno;
            }
            set
            {
                this.tiosno = value;
            }
        }

        public string Unit
        {
            get
            {
                return this.unit;
            }
            set
            {
                this.unit = value;
            }
        }
    }
}

