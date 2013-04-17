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

    public class CheckOutMain : Form
    {
        private Button btnCheckOut;
        private Button btnExit;
        private Button btnGoodsCheckOut;
        private Button btnMoveGoods;
        private Button btnOrderCheckOut;
        private IContainer components;
        private MiddleService service = Global.MiddleService;
        private HeadLabel TasksTitile;

        public CheckOutMain()
        {
            this.InitializeComponent();
        }

        public void btnCheckOut_Click(object sender, EventArgs e)
        {
            DataSet task = new DataSet();
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                task = this.service.GetOutTasks(Management.GetSingleton().WarehouseNo);
            }
            catch (ApplicationException exception)
            {
                Cursor.Current = Cursors.Default;
                MessageShow.Alert("错误", "下载发料任务失败！" + exception.Message);
                return;
            }
            catch (Exception exception2)
            {
                Cursor.Current = Cursors.Default;
                MessageShow.Alert("错误", "下载发料任务失败！" + exception2.Message);
                return;
            }
            Cursor.Current = Cursors.Default;
            if (task.Tables.Count >= 1)
            {
                if (task.Tables[0].Rows.Count == 0)
                {
                    MessageShow.Alert("Warning", "当前没有任何发料任务！");
                }
                else
                {
                    CheckInTasksFrm frm = new CheckInTasksFrm(task, EnumTaskType.CheckOut);
                    frm.ShowDialog();
                    frm.Dispose();
                }
            }
            else
            {
                MessageShow.Alert("Warning", "当前没有任何发料任务！");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnGoodsCheckOut_Click(object sender, EventArgs e)
        {
            CheckOutByMaterialFrm frm = new CheckOutByMaterialFrm();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void btnMoveGoods_Click(object sender, EventArgs e)
        {
            MoveOutByGoods goods = new MoveOutByGoods();
            goods.ShowDialog();
            goods.Dispose();
        }

        private void btnOrderCheckOut_Click(object sender, EventArgs e)
        {
            CheckOutByOrderFrm frm = new CheckOutByOrderFrm();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void CheckOutMain_Load(object sender, EventArgs e)
        {
            string str = string.Empty;
            if (Global.CurrUserpriview.Priview.Tables.Count >= 1)
            {
                DataTable table = Global.CurrUserpriview.Priview.Tables[0];
                if (table.Rows.Count != 0)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        if ((table.Rows[i][0].ToString() == "CheckOutMain") && (table.Rows[i][2].ToString() == "0"))
                        {
                            str = table.Rows[i][1].ToString();
                            if (this.btnCheckOut.Name == str)
                            {
                                this.btnCheckOut.Enabled = false;
                            }
                            else if (this.btnGoodsCheckOut.Name == str)
                            {
                                this.btnGoodsCheckOut.Enabled = false;
                            }
                            else if (this.btnOrderCheckOut.Name == str)
                            {
                                this.btnOrderCheckOut.Enabled = false;
                            }
                            else if (this.btnMoveGoods.Name == str)
                            {
                                this.btnMoveGoods.Enabled = false;
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
            this.btnCheckOut = new Button();
            this.btnGoodsCheckOut = new Button();
            this.btnExit = new Button();
            this.btnOrderCheckOut = new Button();
            this.btnMoveGoods = new Button();
            base.SuspendLayout();
            this.TasksTitile.BackColor = SystemColors.HotTrack;
            this.TasksTitile.Font = new Font("Arial", 14f, FontStyle.Regular);
            this.TasksTitile.ForeColor = SystemColors.ActiveCaptionText;
            this.TasksTitile.Location = new Point(-4, 0);
            this.TasksTitile.Name = "TasksTitile";
            this.TasksTitile.Size = new Size(240, 0x18);
            this.TasksTitile.TabIndex = 8;
            this.TasksTitile.Text = "菜单";
            this.btnCheckOut.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnCheckOut.Location = new Point(50, 0x2d);
            this.btnCheckOut.Name = "btnCheckOut";
            this.btnCheckOut.Size = new Size(0x83, 0x1c);
            this.btnCheckOut.TabIndex = 0;
            this.btnCheckOut.Text = "发料采集";
            this.btnCheckOut.Click += new EventHandler(this.btnCheckOut_Click);
            this.btnGoodsCheckOut.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnGoodsCheckOut.Location = new Point(50, 0x57);
            this.btnGoodsCheckOut.Name = "btnGoodsCheckOut";
            this.btnGoodsCheckOut.Size = new Size(0x83, 0x1c);
            this.btnGoodsCheckOut.TabIndex = 1;
            this.btnGoodsCheckOut.Text = "按物料发料";
            this.btnGoodsCheckOut.Click += new EventHandler(this.btnGoodsCheckOut_Click);
            this.btnExit.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnExit.Location = new Point(50, 0xd5);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new Size(0x83, 0x1c);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "返回";
            this.btnExit.Click += new EventHandler(this.btnExit_Click);
            this.btnOrderCheckOut.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnOrderCheckOut.Location = new Point(50, 0x81);
            this.btnOrderCheckOut.Name = "btnOrderCheckOut";
            this.btnOrderCheckOut.Size = new Size(0x83, 0x1c);
            this.btnOrderCheckOut.TabIndex = 2;
            this.btnOrderCheckOut.Text = "按订单发料";
            this.btnOrderCheckOut.Click += new EventHandler(this.btnOrderCheckOut_Click);
            this.btnMoveGoods.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnMoveGoods.Location = new Point(50, 0xab);
            this.btnMoveGoods.Name = "btnMoveGoods";
            this.btnMoveGoods.Size = new Size(0x83, 0x1c);
            this.btnMoveGoods.TabIndex = 3;
            this.btnMoveGoods.Text = "特殊发料";
            this.btnMoveGoods.Click += new EventHandler(this.btnMoveGoods_Click);
            base.AutoScaleDimensions = new SizeF(96f, 96f);
            //base.AutoScaleMode = AutoScaleMode.Dpi;
            this.AutoScroll = true;
            base.ClientSize = new Size(240, 320);
            base.ControlBox = false;
            base.Controls.Add(this.btnMoveGoods);
            base.Controls.Add(this.btnOrderCheckOut);
            base.Controls.Add(this.btnExit);
            base.Controls.Add(this.btnGoodsCheckOut);
            base.Controls.Add(this.btnCheckOut);
            base.Controls.Add(this.TasksTitile);
            //base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "CheckOutMain";
            base.Load += new EventHandler(this.CheckOutMain_Load);
            base.ResumeLayout(false);
        }
    }
}

