namespace HL.UI
{
    using BizLayer;
    using Entity;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using HL.Framework;

    public class CheckWaitingFrm : Form
    {
        private Button btnCancel;
        private Button btnRetry;
        private IContainer components;
        public Label lblMsg;
        private MiddleService service = Global.MiddleService;
        private EnumTaskType taskType;
        private Timer timer1;

        public CheckWaitingFrm(EnumTaskType taskType)
        {
            this.taskType = taskType;
            this.InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
        }

        private void btnRetry_Click(object sender, EventArgs e)
        {
            this.EnableButtons(false);
        }

        private void CheckWaitingFrm_Load(object sender, EventArgs e)
        {
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

        private void EnableButtons(bool p)
        {
            this.btnCancel.Enabled = p;
            this.btnRetry.Enabled = p;
        }

        private void GetTaskMsg(int status)
        {
            switch (this.taskType)
            {
                case EnumTaskType.CheckIn:
                    if (status != 1)
                    {
                        if (status != 2)
                        {
                            break;
                        }
                        this.lblMsg.Text = "获取入库任务失败！";
                        return;
                    }
                    this.lblMsg.Text = "正在更新入库任务，请稍候...";
                    return;

                case EnumTaskType.CheckOut:
                    if (status != 1)
                    {
                        if (status == 2)
                        {
                            this.lblMsg.Text = "获取出库任务失败！";
                        }
                        break;
                    }
                    this.lblMsg.Text = "正在更新出库任务，请稍候...";
                    return;

                default:
                    return;
            }
        }

        private void InitializeComponent()
        {
            this.btnCancel = new Button();
            this.btnRetry = new Button();
            this.lblMsg = new Label();
            this.timer1 = new Timer();
            base.SuspendLayout();
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new Point(100, 0x74);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x48, 0x1f);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.btnRetry.Enabled = false;
            this.btnRetry.Location = new Point(0x16, 0x74);
            this.btnRetry.Name = "btnRetry";
            this.btnRetry.Size = new Size(0x48, 0x1f);
            this.btnRetry.TabIndex = 6;
            this.btnRetry.Text = "重试";
            this.btnRetry.Click += new EventHandler(this.btnRetry_Click);
            this.lblMsg.Location = new Point(9, 0x1b);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new Size(0xb5, 20);
            this.timer1.Interval = 500;
            this.timer1.Tick += new EventHandler(this.timer1_Tick);
            base.AutoScaleDimensions = new SizeF(96f, 96f);
            //base.AutoScaleMode = AutoScaleMode.Dpi;
            this.AutoScroll = true;
            base.ClientSize = new Size(0xc6, 0xaf);
            base.ControlBox = false;
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnRetry);
            base.Controls.Add(this.lblMsg);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "CheckWaitingFrm";
            this.Text = "任务下载";
            base.Load += new EventHandler(this.CheckWaitingFrm_Load);
            base.ResumeLayout(false);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
        }
    }
}

