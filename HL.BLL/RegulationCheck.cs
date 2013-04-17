namespace BizLayer
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;

    public class RegulationCheck
    {
        private string[] batchNoRegex;
        private static RegulationCheck check;
        private HybridDictionary goodsCode = new HybridDictionary();
        private string[] goodsIDRegex;
        private string[] locationRegex;

        private RegulationCheck()
        {
        }

        public bool Check(string content, DetailFields field)
        {
            if (!string.IsNullOrEmpty(content))
            {
                string[] regexExpress = this.GetRegexExpress(field);
                if ((regexExpress == null) || (regexExpress.Length == 0))
                {
                    return true;
                }
                for (int i = 0; i < regexExpress.Length; i++)
                {
                    if (Management.GetSingleton().RegexCheck(regexExpress[i], content))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private string[] GetGoodsCodeRegexs()
        {
            ArrayList list = new ArrayList();
            foreach (DictionaryEntry entry in this.goodsCode)
            {
                GoodsCodeRegex regex = entry.Value as GoodsCodeRegex;
                list.Add(regex.Regex);
            }
            return (list.ToArray(typeof(string)) as string[]);
        }

        public static RegulationCheck GetInstance()
        {
            object obj2 = new object();
            if (check == null)
            {
                lock (obj2)
                {
                    if (check == null)
                    {
                        check = new RegulationCheck();
                    }
                }
            }
            return check;
        }

        private string[] GetRegexExpress(DetailFields field)
        {
            switch (field)
            {
                case DetailFields.Location:
                    return this.locationRegex;

                case DetailFields.GoodsID:
                    return this.goodsIDRegex;

                case DetailFields.BatchNo:
                    return this.batchNoRegex;

                case DetailFields.GoodsCode:
                    return this.GetGoodsCodeRegexs();
            }
            return null;
        }

        public string[] BatchNoRegex
        {
            get
            {
                return this.batchNoRegex;
            }
            set
            {
                this.batchNoRegex = value;
            }
        }

        public HybridDictionary GoodsCode
        {
            get
            {
                return this.goodsCode;
            }
            set
            {
                this.goodsCode = value;
            }
        }

        public string[] GoodsIDRegex
        {
            get
            {
                return this.goodsIDRegex;
            }
            set
            {
                this.goodsIDRegex = value;
            }
        }

        public string[] LocationRegex
        {
            get
            {
                return this.locationRegex;
            }
            set
            {
                this.locationRegex = value;
            }
        }

        public enum DetailFields
        {
            BatchNo = 3,
            GoodsCode = 4,
            GoodsID = 2,
            Location = 1,
            None = -1
        }
    }
}

