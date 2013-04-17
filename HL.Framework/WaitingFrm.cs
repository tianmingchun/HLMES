namespace HL.Framework
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class WaitingFrm : Form
    {
        private IContainer components;
        public Label lblMsg;

        public WaitingFrm()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblMsg = new Label();
            base.SuspendLayout();
            this.lblMsg.Location = new Point(5, 14);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new Size(0xb9, 20);
            base.AutoScaleDimensions = new SizeF(96f, 96f);
            //base.AutoScaleMode = AutoScaleMode.Dpi;
            this.AutoScroll = true;
            base.ClientSize = new Size(0xc6, 0xaf);
            base.ControlBox = false;
            base.Controls.Add(this.lblMsg);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "WaitingFrm";
            base.Load += new EventHandler(this.WaitingFrm_Load);
            base.ResumeLayout(false);
        }

        public void Show(string title, string msg)
        {
            this.Text = title;
            this.lblMsg.Text = msg;
            base.Show();
            Application.DoEvents();
        }

        private void WaitingFrm_Load(object sender, EventArgs e)
        {
            base.Location = new Point(120 - (base.Width / 2), 160 - (base.Height / 2));
        }
    }
}

