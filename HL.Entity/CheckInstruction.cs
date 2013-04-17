namespace Entity
{
    using System;

    public class CheckInstruction : Instruction
    {
        private string anyRegex;
        private string batchNoReg;
        private decimal finishqty;
        private string goodsid;
        private string goodsname;
        private bool isOver;
        private string location;
        private decimal requiredQuantity;
        private string rowno;
        private string singleNoReg;
        private string tiosno;
        private string unit;

        public CheckInstruction(string id, decimal required) : base(id)
        {
            this.anyRegex = "^*$";
            this.requiredQuantity = required;
        }

        public string BatchNoReg
        {
            get
            {
                return this.batchNoReg;
            }
            set
            {
                if (value == string.Empty)
                {
                    value = this.anyRegex;
                }
                this.batchNoReg = value;
            }
        }

        public decimal Finishqty
        {
            get
            {
                return this.finishqty;
            }
            set
            {
                this.finishqty = value;
            }
        }

        public string GoodsID
        {
            get
            {
                return this.goodsid;
            }
            set
            {
                this.goodsid = value;
            }
        }

        public string Goodsname
        {
            get
            {
                return this.goodsname;
            }
            set
            {
                this.goodsname = value;
            }
        }

        public string IndicateLocation
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

        public bool IsOver
        {
            get
            {
                return this.isOver;
            }
            set
            {
                this.isOver = value;
            }
        }

        public decimal RequiredQuantity
        {
            get
            {
                return this.requiredQuantity;
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

        public string SingleNoReg
        {
            get
            {
                return this.singleNoReg;
            }
            set
            {
                if (value == string.Empty)
                {
                    value = this.anyRegex;
                }
                this.singleNoReg = value;
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

