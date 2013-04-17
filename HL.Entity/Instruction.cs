namespace Entity
{
    using System;

    public class Instruction
    {
        private Task associateTask;
        private string id;
        private InstructionResult result = new InstructionResult();

        public Instruction(string insID)
        {
            if (string.IsNullOrEmpty(insID))
            {
                throw new ArgumentNullException("任务明细号不能够为空！");
            }
            this.id = insID;
            this.result.AssociateInstruction = this;
        }

        public Task AssociateTask
        {
            get
            {
                return this.associateTask;
            }
            set
            {
                this.associateTask = value;
            }
        }

        public string ID
        {
            get
            {
                return this.id;
            }
        }

        public InstructionResult Result
        {
            get
            {
                return this.result;
            }
        }
    }
}

