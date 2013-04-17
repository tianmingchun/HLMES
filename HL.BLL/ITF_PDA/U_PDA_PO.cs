namespace BizLayer.ITF_PDA
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [DesignerCategory("code"), XmlType(Namespace="http://tempuri.org/"), DebuggerStepThrough]
    public class U_PDA_PO
    {
        private U_PDA_PO_DT[] iL_poDTField;
        private string is_ClientIDField;
        private string is_GDSIDField;
        private string is_poDescField;
        private string is_ProIDField;
        private string is_PROPOIDField;
        private string is_PROSTUIDField;
        private string is_SapProofField;
        private string is_TIOPOLIDField;
        private string is_TIOSNOField;
        private int is_TORNOField;
        private string is_USERField;

        public U_PDA_PO_DT[] IL_poDT
        {
            get
            {
                return this.iL_poDTField;
            }
            set
            {
                this.iL_poDTField = value;
            }
        }

        public string Is_ClientID
        {
            get
            {
                return this.is_ClientIDField;
            }
            set
            {
                this.is_ClientIDField = value;
            }
        }

        public string Is_GDSID
        {
            get
            {
                return this.is_GDSIDField;
            }
            set
            {
                this.is_GDSIDField = value;
            }
        }

        public string Is_poDesc
        {
            get
            {
                return this.is_poDescField;
            }
            set
            {
                this.is_poDescField = value;
            }
        }

        public string Is_ProID
        {
            get
            {
                return this.is_ProIDField;
            }
            set
            {
                this.is_ProIDField = value;
            }
        }

        public string Is_PROPOID
        {
            get
            {
                return this.is_PROPOIDField;
            }
            set
            {
                this.is_PROPOIDField = value;
            }
        }

        public string Is_PROSTUID
        {
            get
            {
                return this.is_PROSTUIDField;
            }
            set
            {
                this.is_PROSTUIDField = value;
            }
        }

        public string Is_SapProof
        {
            get
            {
                return this.is_SapProofField;
            }
            set
            {
                this.is_SapProofField = value;
            }
        }

        public string Is_TIOPOLID
        {
            get
            {
                return this.is_TIOPOLIDField;
            }
            set
            {
                this.is_TIOPOLIDField = value;
            }
        }

        public string Is_TIOSNO
        {
            get
            {
                return this.is_TIOSNOField;
            }
            set
            {
                this.is_TIOSNOField = value;
            }
        }

        public int Is_TORNO
        {
            get
            {
                return this.is_TORNOField;
            }
            set
            {
                this.is_TORNOField = value;
            }
        }

        public string Is_USER
        {
            get
            {
                return this.is_USERField;
            }
            set
            {
                this.is_USERField = value;
            }
        }
    }
}

