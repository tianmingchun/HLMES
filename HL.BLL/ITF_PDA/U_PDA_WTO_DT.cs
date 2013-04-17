namespace BizLayer.ITF_PDA
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [DesignerCategory("code"), XmlType(Namespace="http://tempuri.org/"), DebuggerStepThrough]
    public class U_PDA_WTO_DT
    {
        private DateTime iLdt_TORFDATEField;
        private decimal iLi_TORFINIQTYField;
        private int iLi_TORNOField;
        private decimal iLi_TORPLANQTYField;
        private string iLs_TORBACTHNOField;
        private string iLs_TORCUSTIDField;
        private string iLs_TORDESCRIPTIONField;
        private string iLs_TORSITEField;

        public DateTime ILdt_TORFDATE
        {
            get
            {
                return this.iLdt_TORFDATEField;
            }
            set
            {
                this.iLdt_TORFDATEField = value;
            }
        }

        public decimal ILi_TORFINIQTY
        {
            get
            {
                return this.iLi_TORFINIQTYField;
            }
            set
            {
                this.iLi_TORFINIQTYField = value;
            }
        }

        public int ILi_TORNO
        {
            get
            {
                return this.iLi_TORNOField;
            }
            set
            {
                this.iLi_TORNOField = value;
            }
        }

        public decimal ILi_TORPLANQTY
        {
            get
            {
                return this.iLi_TORPLANQTYField;
            }
            set
            {
                this.iLi_TORPLANQTYField = value;
            }
        }

        public string ILs_TORBACTHNO
        {
            get
            {
                return this.iLs_TORBACTHNOField;
            }
            set
            {
                this.iLs_TORBACTHNOField = value;
            }
        }

        public string ILs_TORCUSTID
        {
            get
            {
                return this.iLs_TORCUSTIDField;
            }
            set
            {
                this.iLs_TORCUSTIDField = value;
            }
        }

        public string ILs_TORDESCRIPTION
        {
            get
            {
                return this.iLs_TORDESCRIPTIONField;
            }
            set
            {
                this.iLs_TORDESCRIPTIONField = value;
            }
        }

        public string ILs_TORSITE
        {
            get
            {
                return this.iLs_TORSITEField;
            }
            set
            {
                this.iLs_TORSITEField = value;
            }
        }
    }
}

