namespace Entity
{
    using System;
    using System.Data;

    public class UserPriview
    {
        private DataSet priview;

        public UserPriview(DataSet priview)
        {
            this.Priview = priview;
        }

        public DataSet Priview
        {
            get
            {
                return this.priview;
            }
            set
            {
                this.priview = value;
            }
        }
    }
}

