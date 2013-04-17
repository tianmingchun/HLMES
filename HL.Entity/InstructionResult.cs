namespace Entity
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;

    public class InstructionResult
    {
        private int index;
        private Instruction ins;
        private HybridDictionary items = new HybridDictionary();

        public virtual void AddColDetail(ColDetail result)
        {
            if ((result.SingletonNo != string.Empty) && this.AssociateInstruction.AssociateTask.SingleIDs.Contains(result.SingletonNo))
            {
                throw new ApplicationException("欲添加的采集记录已存在！");
            }
            this.index++;
            this.items.Add(this.index, result);
            if (result.SingletonNo != "")
            {
                this.AssociateInstruction.AssociateTask.SingleIDs.Add(result.SingletonNo);
            }
        }

        public void RemoveColDetail(int index)
        {
            this.items.Remove(index);
        }

        public virtual Instruction AssociateInstruction
        {
            get
            {
                return this.ins;
            }
            set
            {
                this.ins = value;
            }
        }

        public HybridDictionary ColDetails
        {
            get
            {
                return this.items;
            }
        }

        public decimal TotalQuantity
        {
            get
            {
                decimal num = 0M;
                foreach (DictionaryEntry entry in this.items)
                {
                    num += (entry.Value as ColDetail).CollectedQuantity;
                }
                return num;
            }
            set
            {
                throw new NotSupportedException("不可设置明细结果集的总数量");
            }
        }
    }
}

