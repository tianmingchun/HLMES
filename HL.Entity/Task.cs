namespace Entity
{
    using System;
    using System.Collections;

    public class Task
    {
        private string caigouyuan;
        private string caigouzu;
        private string chejian;
        private string choujianqty;
        private string daohuoqty;
        private string date;
        private string huohao;
        private string id;
        private SortedHashTable insList = new SortedHashTable();
        private string inspectno;
        private string kuwei;
        private string ordernum;
        private string pici;
        private ArrayList singleids = new ArrayList();
        private string supplierNum;
        private EnumTaskType t;
        private string warehouseNum;
        private string zuzhi;

        public Task(string taskid)
        {
            if (string.IsNullOrEmpty(taskid))
            {
                throw new ArgumentNullException("任务号不能够为空！");
            }
            this.id = taskid;
        }

        public void AddIns(Instruction ins)
        {
            ins.AssociateTask = this;
            this.insList.Add(ins.ID, ins);
        }

        public Instruction GetInstruction(int index)
        {
            object obj2 = ((ArrayList) this.insList.Keys)[index];
            return (this.insList[obj2] as Instruction);
        }

        public string Caigouyuan
        {
            get
            {
                return this.caigouyuan;
            }
            set
            {
                this.caigouyuan = value;
            }
        }

        public string Caigouzu
        {
            get
            {
                return this.caigouzu;
            }
            set
            {
                this.caigouzu = value;
            }
        }

        public string Chejian
        {
            get
            {
                return this.chejian;
            }
            set
            {
                this.chejian = value;
            }
        }

        public string Choujianqty
        {
            get
            {
                return this.choujianqty;
            }
            set
            {
                this.choujianqty = value;
            }
        }

        public string Daohuoqty
        {
            get
            {
                return this.daohuoqty;
            }
            set
            {
                this.daohuoqty = value;
            }
        }

        public string Date
        {
            get
            {
                return this.date;
            }
            set
            {
                this.date = value;
            }
        }

        public string Huohao
        {
            get
            {
                return this.huohao;
            }
            set
            {
                this.huohao = value;
            }
        }

        public string ID
        {
            get
            {
                return this.id;
            }
        }

        public SortedHashTable InsList
        {
            get
            {
                return this.insList;
            }
        }

        public string Inspectno
        {
            get
            {
                return this.inspectno;
            }
            set
            {
                this.inspectno = value;
            }
        }

        public string Kuwei
        {
            get
            {
                return this.kuwei;
            }
            set
            {
                this.kuwei = value;
            }
        }

        public string Ordernum
        {
            get
            {
                return this.ordernum;
            }
            set
            {
                this.ordernum = value;
            }
        }

        public string Pici
        {
            get
            {
                return this.pici;
            }
            set
            {
                this.pici = value;
            }
        }

        public ArrayList SingleIDs
        {
            get
            {
                return this.singleids;
            }
            set
            {
                this.singleids = value;
            }
        }

        public string SupplierNum
        {
            get
            {
                return this.supplierNum;
            }
            set
            {
                this.supplierNum = value;
            }
        }

        public virtual EnumTaskType TaskType
        {
            get
            {
                return this.t;
            }
            set
            {
                this.t = value;
            }
        }

        public decimal TotalQuantity
        {
            get
            {
                decimal num = 0M;
                foreach (DictionaryEntry entry in this.insList)
                {
                    num += (entry.Value as Instruction).Result.TotalQuantity;
                }
                return num;
            }
        }

        public string WarehouseNum
        {
            get
            {
                return this.warehouseNum;
            }
            set
            {
                this.warehouseNum = value;
            }
        }

        public string Zuzhi
        {
            get
            {
                return this.zuzhi;
            }
            set
            {
                this.zuzhi = value;
            }
        }
    }
}

