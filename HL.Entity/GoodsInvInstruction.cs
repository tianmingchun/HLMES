namespace Entity
{
    using System;

    public class GoodsInvInstruction : Instruction
    {
        private string anyRegex;
        private string batchNoReg;
        private string goodsID;
        private bool isBatch;
        private bool isSingle;
        private string singleNoReg;

        public GoodsInvInstruction(string id, bool isBatchM, bool isSingleM) : base(id)
        {
            this.anyRegex = "^*$";
            this.isBatch = isBatchM;
            this.isSingle = isSingleM;
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

        public bool IsBatch
        {
            get
            {
                return this.isBatch;
            }
        }

        public bool IsSingle
        {
            get
            {
                return this.isSingle;
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
    }
}

