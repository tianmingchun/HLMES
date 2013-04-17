namespace BizLayer.ITF_PDA
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [DebuggerStepThrough, XmlType(Namespace="http://tempuri.org/"), DesignerCategory("code")]
    public class U_PDA_WTO
    {
        private int ii_TIOISBACKField;
        private U_PDA_WTO_DT[] iL_wtoDTField;
        private string is_GDSIDField;
        private string is_poDescField;
        private string is_PROPOIDField;
        private string is_PROSTUIDField;
        private string is_TIOPOLIDField;
        private string is_TIOREVSTAField;
        private string is_TIOSNOField;
        private int li_SUnionField;

        public int Ii_TIOISBACK
        {
            get
            {
                return this.ii_TIOISBACKField;
            }
            set
            {
                this.ii_TIOISBACKField = value;
            }
        }

        public U_PDA_WTO_DT[] IL_wtoDT
        {
            get
            {
                return this.iL_wtoDTField;
            }
            set
            {
                this.iL_wtoDTField = value;
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

        public string Is_TIOREVSTA
        {
            get
            {
                return this.is_TIOREVSTAField;
            }
            set
            {
                this.is_TIOREVSTAField = value;
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

        public int li_SUnion
        {
            get
            {
                return this.li_SUnionField;
            }
            set
            {
                this.li_SUnionField = value;
            }
        }
    }
}

