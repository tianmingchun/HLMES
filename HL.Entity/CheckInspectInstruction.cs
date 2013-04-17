namespace Entity
{
    using System;

    public class CheckInspectInstruction : Instruction
    {
        private string ispBatchid;
        private decimal ispdefqty;
        private string ispdescribe;
        private string ispGosid;
        private string ispicino;
        private string ispLocation;
        private string ispname;
        private decimal ispSelqty;
        private decimal ispstd;
        private decimal ispstddown;
        private decimal ispstdup;
        private string isptools;
        private decimal ispTotalqty;
        private int isptype;

        public CheckInspectInstruction(string id) : base(id)
        {
        }

        public string IspBatchid
        {
            get
            {
                return this.ispBatchid;
            }
            set
            {
                this.ispBatchid = value;
            }
        }

        public decimal Ispdefqty
        {
            get
            {
                return this.ispdefqty;
            }
            set
            {
                this.ispdefqty = value;
            }
        }

        public string Ispdescribe
        {
            get
            {
                return this.ispdescribe;
            }
            set
            {
                this.ispdescribe = value;
            }
        }

        public string IspGosid
        {
            get
            {
                return this.ispGosid;
            }
            set
            {
                this.ispGosid = value;
            }
        }

        public string Ispicino
        {
            get
            {
                return this.ispicino;
            }
            set
            {
                this.ispicino = value;
            }
        }

        public string IspLocation
        {
            get
            {
                return this.ispLocation;
            }
            set
            {
                this.ispLocation = value;
            }
        }

        public string Ispname
        {
            get
            {
                return this.ispname;
            }
            set
            {
                this.ispname = value;
            }
        }

        public decimal IspSelqty
        {
            get
            {
                return this.ispSelqty;
            }
            set
            {
                this.ispSelqty = value;
            }
        }

        public decimal Ispstd
        {
            get
            {
                return this.ispstd;
            }
            set
            {
                this.ispstd = value;
            }
        }

        public decimal Ispstddown
        {
            get
            {
                return this.ispstddown;
            }
            set
            {
                this.ispstddown = value;
            }
        }

        public decimal Ispstdup
        {
            get
            {
                return this.ispstdup;
            }
            set
            {
                this.ispstdup = value;
            }
        }

        public string Isptools
        {
            get
            {
                return this.isptools;
            }
            set
            {
                this.isptools = value;
            }
        }

        public decimal IspTotalqty
        {
            get
            {
                return this.ispTotalqty;
            }
            set
            {
                this.ispTotalqty = value;
            }
        }

        public int Isptype
        {
            get
            {
                return this.isptype;
            }
            set
            {
                this.isptype = value;
            }
        }
    }
}

