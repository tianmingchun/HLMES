namespace BizLayer.ITF_PDA
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [XmlType(Namespace="http://tempuri.org/"), DebuggerStepThrough, DesignerCategory("code")]
    public class U_WTO_MOVE
    {
        private decimal id_QtyField;
        private int ii_srtypeField;
        private string is_DescField;
        private string is_FactoryField;
        private string is_MatcodeField;
        private string is_PropoidField;
        private string is_RgekzField;
        private string is_StorFromField;
        private string is_StorToField;
        private string is_TransPersionField;

        public decimal Id_Qty
        {
            get
            {
                return this.id_QtyField;
            }
            set
            {
                this.id_QtyField = value;
            }
        }

        public int Ii_srtype
        {
            get
            {
                return this.ii_srtypeField;
            }
            set
            {
                this.ii_srtypeField = value;
            }
        }

        public string Is_Desc
        {
            get
            {
                return this.is_DescField;
            }
            set
            {
                this.is_DescField = value;
            }
        }

        public string Is_Factory
        {
            get
            {
                return this.is_FactoryField;
            }
            set
            {
                this.is_FactoryField = value;
            }
        }

        public string Is_Matcode
        {
            get
            {
                return this.is_MatcodeField;
            }
            set
            {
                this.is_MatcodeField = value;
            }
        }

        public string Is_Propoid
        {
            get
            {
                return this.is_PropoidField;
            }
            set
            {
                this.is_PropoidField = value;
            }
        }

        public string Is_Rgekz
        {
            get
            {
                return this.is_RgekzField;
            }
            set
            {
                this.is_RgekzField = value;
            }
        }

        public string Is_StorFrom
        {
            get
            {
                return this.is_StorFromField;
            }
            set
            {
                this.is_StorFromField = value;
            }
        }

        public string Is_StorTo
        {
            get
            {
                return this.is_StorToField;
            }
            set
            {
                this.is_StorToField = value;
            }
        }

        public string Is_TransPersion
        {
            get
            {
                return this.is_TransPersionField;
            }
            set
            {
                this.is_TransPersionField = value;
            }
        }
    }
}

