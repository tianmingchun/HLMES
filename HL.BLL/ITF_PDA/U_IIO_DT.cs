namespace BizLayer.ITF_PDA
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [DesignerCategory("code"), XmlType(Namespace="http://tempuri.org/"), DebuggerStepThrough]
    public class U_IIO_DT
    {
        private decimal iLd_LevelDownField;
        private decimal iLd_LevelStandField;
        private decimal iLd_LevelUpField;
        private int iLi_InspTypeField;
        private string iLs_InspToolField;
        private string iLs_ItemDescField;
        private string iLs_ItemNameField;
        private string iLs_ItemNoField;

        public decimal ILd_LevelDown
        {
            get
            {
                return this.iLd_LevelDownField;
            }
            set
            {
                this.iLd_LevelDownField = value;
            }
        }

        public decimal ILd_LevelStand
        {
            get
            {
                return this.iLd_LevelStandField;
            }
            set
            {
                this.iLd_LevelStandField = value;
            }
        }

        public decimal ILd_LevelUp
        {
            get
            {
                return this.iLd_LevelUpField;
            }
            set
            {
                this.iLd_LevelUpField = value;
            }
        }

        public int ILi_InspType
        {
            get
            {
                return this.iLi_InspTypeField;
            }
            set
            {
                this.iLi_InspTypeField = value;
            }
        }

        public string ILs_InspTool
        {
            get
            {
                return this.iLs_InspToolField;
            }
            set
            {
                this.iLs_InspToolField = value;
            }
        }

        public string ILs_ItemDesc
        {
            get
            {
                return this.iLs_ItemDescField;
            }
            set
            {
                this.iLs_ItemDescField = value;
            }
        }

        public string ILs_ItemName
        {
            get
            {
                return this.iLs_ItemNameField;
            }
            set
            {
                this.iLs_ItemNameField = value;
            }
        }

        public string ILs_ItemNo
        {
            get
            {
                return this.iLs_ItemNoField;
            }
            set
            {
                this.iLs_ItemNoField = value;
            }
        }
    }
}

