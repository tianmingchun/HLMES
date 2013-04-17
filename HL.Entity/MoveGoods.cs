namespace Entity
{
    using System;

    public class MoveGoods
    {
        private string fromwarehouse;
        private string goodsID;
        private string outtype;
        private decimal qty;
        private string toorder;
        private string towarehouse;

        public string Fromwarehouse
        {
            get
            {
                return this.fromwarehouse;
            }
            set
            {
                this.fromwarehouse = value;
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

        public string Outtype
        {
            get
            {
                return this.outtype;
            }
            set
            {
                this.outtype = value;
            }
        }

        public decimal Qty
        {
            get
            {
                return this.qty;
            }
            set
            {
                this.qty = value;
            }
        }

        public string Toorder
        {
            get
            {
                return this.toorder;
            }
            set
            {
                this.toorder = value;
            }
        }

        public string Towarehouse
        {
            get
            {
                return this.towarehouse;
            }
            set
            {
                this.towarehouse = value;
            }
        }
    }
}

