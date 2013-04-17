namespace BizLayer.ITF_PDA
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [DesignerCategory("code"), XmlType(Namespace="http://tempuri.org/"), DebuggerStepThrough]
    public class U_IIO
    {
        private decimal id_DefectQtyField;
        private decimal id_InspQtyField;
        private decimal id_ReachQtyField;
        private decimal id_ResultQty_NGField;
        private decimal id_ResultQty_OKField;
        private DateTime idt_InspDateField;
        private int ii_InspResultField;
        private U_IIO_DT[] iL_IIODTField;
        private string is_ColDescField;
        private string is_DescField;
        private string is_FactoryField;
        private string is_InspNoField;
        private string is_InspUserField;
        private string is_MatcodeField;
        private string is_MatLotField;
        private string is_MoveRsnField;
        private string is_PONoField;
        private string is_WareHouseField;
        private string is_WareSiteField;

        public decimal Id_DefectQty
        {
            get
            {
                return this.id_DefectQtyField;
            }
            set
            {
                this.id_DefectQtyField = value;
            }
        }

        public decimal Id_InspQty
        {
            get
            {
                return this.id_InspQtyField;
            }
            set
            {
                this.id_InspQtyField = value;
            }
        }

        public decimal Id_ReachQty
        {
            get
            {
                return this.id_ReachQtyField;
            }
            set
            {
                this.id_ReachQtyField = value;
            }
        }

        public decimal Id_ResultQty_NG
        {
            get
            {
                return this.id_ResultQty_NGField;
            }
            set
            {
                this.id_ResultQty_NGField = value;
            }
        }

        public decimal Id_ResultQty_OK
        {
            get
            {
                return this.id_ResultQty_OKField;
            }
            set
            {
                this.id_ResultQty_OKField = value;
            }
        }

        public DateTime Idt_InspDate
        {
            get
            {
                return this.idt_InspDateField;
            }
            set
            {
                this.idt_InspDateField = value;
            }
        }

        public int Ii_InspResult
        {
            get
            {
                return this.ii_InspResultField;
            }
            set
            {
                this.ii_InspResultField = value;
            }
        }

        public U_IIO_DT[] IL_IIODT
        {
            get
            {
                return this.iL_IIODTField;
            }
            set
            {
                this.iL_IIODTField = value;
            }
        }

        public string Is_ColDesc
        {
            get
            {
                return this.is_ColDescField;
            }
            set
            {
                this.is_ColDescField = value;
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

        public string Is_InspNo
        {
            get
            {
                return this.is_InspNoField;
            }
            set
            {
                this.is_InspNoField = value;
            }
        }

        public string Is_InspUser
        {
            get
            {
                return this.is_InspUserField;
            }
            set
            {
                this.is_InspUserField = value;
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

        public string Is_MatLot
        {
            get
            {
                return this.is_MatLotField;
            }
            set
            {
                this.is_MatLotField = value;
            }
        }

        public string Is_MoveRsn
        {
            get
            {
                return this.is_MoveRsnField;
            }
            set
            {
                this.is_MoveRsnField = value;
            }
        }

        public string Is_PONo
        {
            get
            {
                return this.is_PONoField;
            }
            set
            {
                this.is_PONoField = value;
            }
        }

        public string Is_WareHouse
        {
            get
            {
                return this.is_WareHouseField;
            }
            set
            {
                this.is_WareHouseField = value;
            }
        }

        public string Is_WareSite
        {
            get
            {
                return this.is_WareSiteField;
            }
            set
            {
                this.is_WareSiteField = value;
            }
        }
    }
}

