namespace HL.UI
{
    using BizLayer;
    using Entity;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using HL.Framework;
    using HL.Controls;

    public class CheckInMain : Form
    {
        private Button btnCheckIn;
        private Button btnCheckInOrder;
        private Button btnExit;
        private IContainer components;
        private MiddleService service = Global.MiddleService;
        private HeadLabel TasksTitile;

        public CheckInMain()
        {
            this.InitializeComponent();
        }

        public void btnCheckIn_Click(object sender, EventArgs e)
        {
            DataSet task = new DataSet();
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                task = this.service.GetInTasks(Management.GetSingleton().WarehouseNo);
            }
            catch (ApplicationException exception)
            {
                Cursor.Current = Cursors.Default;
                MessageShow.Alert("错误", "下载收货任务失败！" + exception.Message);
                return;
            }
            catch (Exception exception2)
            {
                Cursor.Current = Cursors.Default;
                MessageShow.Alert("错误", "下载收货任务失败！" + exception2.Message);
                return;
            }
            Cursor.Current = Cursors.Default;
            if (task.Tables.Count >= 1)
            {
                if (task.Tables[0].Rows.Count == 0)
                {
                    MessageShow.Alert("Warning", "当前没有任何收货任务！");
                }
                else
                {
                    CheckInTasksFrm frm = new CheckInTasksFrm(task, EnumTaskType.CheckIn);
                    frm.ShowDialog();
                    frm.Dispose();
                }
            }
            else
            {
                MessageShow.Alert("Warning", "当前没有任何收货任务！");
            }
        }

        private void btnCheckInOrder_Click(object sender, EventArgs e)
        {
            CheckInByOrderFrm frm = new CheckInByOrderFrm();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void CheckInMain_Load(object sender, EventArgs e)
        {
            string str = string.Empty;
            if (Global.CurrUserpriview.Priview.Tables.Count >= 1)
            {
                DataTable table = Global.CurrUserpriview.Priview.Tables[0];
                if (table.Rows.Count != 0)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        if ((table.Rows[i][0].ToString() == "CheckInMain") && (table.Rows[i][2].ToString() == "0"))
                        {
                            str = table.Rows[i][1].ToString();
                            if (this.btnCheckIn.Name == str)
                            {
                                this.btnCheckIn.Enabled = false;
                            }
                            else if (this.btnCheckInOrder.Name == str)
                            {
                                this.btnCheckInOrder.Enabled = false;
                            }
                        }
                    }
                }
            }
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
            this.TasksTitile = new HeadLabel();
            this.btnExit = new Button();
            this.btnCheckInOrder = new Button();
            this.btnCheckIn = new Button();
            base.SuspendLayout();
            this.TasksTitile.BackColor = SystemColors.HotTrack;
            this.TasksTitile.Font = new Font("Arial", 14f, FontStyle.Regular);
            this.TasksTitile.ForeColor = SystemColors.ActiveCaptionText;
            this.TasksTitile.Location = new Point(0, 0);
            this.TasksTitile.Name = "TasksTitile";
            this.TasksTitile.Size = new Size(0xed, 0x18);
            this.TasksTitile.TabIndex = 50;
            this.TasksTitile.Text = "菜单";
            this.btnExit.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnExit.Location = new Point(0x37, 0xcb);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new Size(0x83, 0x1c);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "返回";
            this.btnExit.Click += new EventHandler(this.btnExit_Click);
            this.btnCheckInOrder.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnCheckInOrder.Location = new Point(0x37, 0x83);
            this.btnCheckInOrder.Name = "btnCheckInOrder";
            this.btnCheckInOrder.Size = new Size(0x83, 0x1c);
            this.btnCheckInOrder.TabIndex = 1;
            this.btnCheckInOrder.Text = "按订单收货";
            this.btnCheckInOrder.Click += new EventHandler(this.btnCheckInOrder_Click);
            this.btnCheckIn.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnCheckIn.Location = new Point(0x37, 0x3b);
            this.btnCheckIn.Name = "btnCheckIn";
            this.btnCheckIn.Size = new Size(0x83, 0x1c);
            this.btnCheckIn.TabIndex = 0;
            this.btnCheckIn.Text = "收货采集";
            this.btnCheckIn.Click += new EventHandler(this.btnCheckIn_Click);
            base.AutoScaleDimensions = new SizeF(96f, 96f);
            //base.AutoScaleMode = AutoScaleMode.Dpi;
            this.AutoScroll = true;
            base.ClientSize = new Size(240, 320);
            base.ControlBox = false;
            base.Controls.Add(this.btnExit);
            base.Controls.Add(this.btnCheckInOrder);
            base.Controls.Add(this.btnCheckIn);
            base.Controls.Add(this.TasksTitile);
            //base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "CheckInMain";
            this.Text = "收货菜单";
            base.Load += new EventHandler(this.CheckInMain_Load);
            base.ResumeLayout(false);
        }
    }
}

