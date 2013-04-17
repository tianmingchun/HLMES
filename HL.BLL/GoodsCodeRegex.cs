namespace BizLayer
{
    using System;

    public class GoodsCodeRegex
    {
        private ElementRegex[] elements;
        private string id;
        private string regex;

        public GoodsCodeRegex(string id)
        {
            this.id = id;
        }

        public string[] GetAllElements(string barcode)
        {
            if ((this.elements == null) || (this.elements.Length == 0))
            {
                return null;
            }
            int end = 0;
            for (int i = 0; i < this.elements.Length; i++)
            {
                if (this.elements[i].End > end)
                {
                    end = this.elements[i].End;
                }
            }
            if (barcode.Length < end)
            {
                throw new ApplicationException("条码的长度非法！");
            }
            string[] strArray = new string[3];
            for (int j = 0; j < this.elements.Length; j++)
            {
                if (this.elements[j].Name.ToLower() == "goods")
                {
                    if (strArray[0] == null)
                    {
                        strArray[0] = barcode.Substring(this.elements[j].Start - 1, (this.elements[j].End - this.elements[j].Start) + 1);
                    }
                }
                else if ((this.elements[j].Name.ToLower() == "batch") && (strArray[1] == null))
                {
                    strArray[1] = barcode.Substring(this.elements[j].Start - 1, (this.elements[j].End - this.elements[j].Start) + 1);
                }
            }
            for (int k = 0; k < 3; k++)
            {
                strArray[k] = (strArray[k] == null) ? "" : strArray[k];
            }
            return strArray;
        }

        public ElementRegex[] Elements
        {
            get
            {
                return this.elements;
            }
            set
            {
                this.elements = value;
            }
        }

        public string ID
        {
            get
            {
                return this.id;
            }
        }

        public string Regex
        {
            get
            {
                return this.regex;
            }
            set
            {
                this.regex = value;
            }
        }
    }
}

