namespace HL.UI
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class DetailFrm : Form
    {
        private IContainer components;
        public Label HintLabel;
        private Timer timer1;

        public DetailFrm(string msg)
        {
            this.InitializeComponent();
            this.HintLabel.Text = msg;
        }

        private void DetailFrm_KeyDown(object sender, KeyEventArgs e)
        {
            base.DialogResult = DialogResult.OK;
        }

        private void DetailFrm_Load(object sender, EventArgs e)
        {
            base.Location = new Point(120 - (base.Width / 2), 160 - (base.Height / 2));
            this.timer1.Enabled = true;
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
            this.HintLabel = new Label();
            this.timer1 = new Timer();
            base.SuspendLayout();
            this.HintLabel.Font = new Font("宋体", 10f, FontStyle.Regular);
            this.HintLabel.Location = new Point(0, 15);
            this.HintLabel.Name = "HintLabel";
            this.HintLabel.Size = new Size(0xd5, 0x1f);
            this.HintLabel.TextAlign = ContentAlignment.TopCenter;
            this.timer1.Interval = 0x7d0;
            this.timer1.Tick += new EventHandler(this.timer1_Tick);
            base.AutoScaleMode = AutoScaleMode.Inherit;
            base.ClientSize = new Size(0xd5, 0x3d);
            base.Controls.Add(this.HintLabel);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "DetailFrm";
            this.Text = "明细";
            base.KeyDown += new KeyEventHandler(this.DetailFrm_KeyDown);
            base.Load += new EventHandler(this.DetailFrm_Load);
            base.ResumeLayout(false);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.OK;
        }
    }
}

