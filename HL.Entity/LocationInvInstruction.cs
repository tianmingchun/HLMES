namespace Entity
{
    using System;
    using System.Runtime.CompilerServices;

    public class LocationInvInstruction : Instruction
    {
        private string location;

        public event EventHandler ItemChanged;

        public LocationInvInstruction(string id, string location) : base(id)
        {
            this.location = location;
        }

        private void value_TotalQuantityChanged(object sender, EventArgs e)
        {
            this.ItemChanged(this, EventArgs.Empty);
        }

        public string Location
        {
            get
            {
                return this.location;
            }
        }
    }
}

