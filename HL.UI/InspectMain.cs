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

    public class InspectMain : BaseForm
    {
        private Button btnCheckGoods;
        private Button btnCheckorder;
        private Button button2;
        private IContainer components;
        private MiddleService service = Global.MiddleService;
        private HeadLabel TasksTitile;
        private bool uploaded;
        private WaitingFrm w = new WaitingFrm();

        public InspectMain()
        {
            this.InitializeComponent();
        }

        private void btnCheckGoods_Click(object sender, EventArgs e)
        {
            InspectGoodsFrm frm = new InspectGoodsFrm();
            frm.ShowDialog();
            frm.Dispose();
        }

        public void btnCheckorder_Click(object sender, EventArgs e)
        {
            string taskMsg = this.GetTaskMsg(EnumTaskType.CheckInspect);
            DataSet instasks = new DataSet();
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                instasks = this.service.GetInspectTasks();
            }
            catch (ApplicationException exception)
            {
                Cursor.Current = Cursors.Default;
                MessageShow.Alert("错误", "下载" + taskMsg + "任务失败！" + exception.Message);
                return;
            }
            catch (Exception exception2)
            {
                Cursor.Current = Cursors.Default;
                MessageShow.Alert("错误", "下载" + taskMsg + "任务失败！" + exception2.Message);
                return;
            }
            Cursor.Current = Cursors.Default;
            if (instasks.Tables[0].Rows.Count != 0)
            {
                InspectFrm1 frm = new InspectFrm1(instasks);
                frm.ShowDialog();
                frm.Dispose();
            }
            else
            {
                MessageShow.Alert("Warning", "当前没有任何" + taskMsg + "任务！");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private string GetTaskMsg(EnumTaskType taskType)
        {
            switch (taskType)
            {
                case EnumTaskType.CheckIn:
                    return "入库";

                case EnumTaskType.CheckOut:
                    return "出库";

                case EnumTaskType.CheckInspect:
                    return "检验";
            }
            return "";
        }

        private void InitializeComponent()
        {
            this.btnCheckorder = new Button();
            this.btnCheckGoods = new Button();
            this.button2 = new Button();
            this.TasksTitile = new HeadLabel();
            base.SuspendLayout();
            this.btnCheckorder.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnCheckorder.Location = new Point(0x34, 0x3b);
            this.btnCheckorder.Name = "btnCheckorder";
            this.btnCheckorder.Size = new Size(0x83, 0x1c);
            this.btnCheckorder.TabIndex = 0;
            this.btnCheckorder.Text = "单次检验";
            this.btnCheckorder.Click += new EventHandler(this.btnCheckorder_Click);
            this.btnCheckGoods.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnCheckGoods.Location = new Point(0x35, 0x79);
            this.btnCheckGoods.Name = "btnCheckGoods";
            this.btnCheckGoods.Size = new Size(0x83, 0x1c);
            this.btnCheckGoods.TabIndex = 1;
            this.btnCheckGoods.Text = "批量检验";
            this.btnCheckGoods.Click += new EventHandler(this.btnCheckGoods_Click);
            this.button2.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.button2.Location = new Point(0x35, 0xb9);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x83, 0x1c);
            this.button2.TabIndex = 2;
            this.button2.Text = "返回";
            this.button2.Click += new EventHandler(this.button2_Click);
            this.TasksTitile.BackColor = SystemColors.HotTrack;
            this.TasksTitile.Font = new Font("Arial", 14f, FontStyle.Regular);
            this.TasksTitile.ForeColor = SystemColors.ActiveCaptionText;
            this.TasksTitile.Location = new Point(-4, 0);
            this.TasksTitile.Name = "TasksTitile";
            this.TasksTitile.Size = new Size(240, 0x18);
            this.TasksTitile.TabIndex = 7;
            this.TasksTitile.Text = "菜单";
            base.AutoScaleDimensions = new SizeF(96f, 96f);
            //base.AutoScaleMode = AutoScaleMode.Dpi;
            this.AutoScroll = true;
            base.ClientSize = new Size(240, 320);
            base.ControlBox = false;
            base.Controls.Add(this.button2);
            base.Controls.Add(this.btnCheckGoods);
            base.Controls.Add(this.btnCheckorder);
            base.Controls.Add(this.TasksTitile);
            //base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "InspectMain";
            this.Text = "检验";
            base.Load += new EventHandler(this.InspectMain_Load);
            base.ResumeLayout(false);
        }

        private void InspectMain_Load(object sender, EventArgs e)
        {
            string str = string.Empty;
            if (Global.CurrUserpriview.Priview.Tables.Count >= 1)
            {
                DataTable table = Global.CurrUserpriview.Priview.Tables[0];
                if (table.Rows.Count != 0)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        if ((table.Rows[i][0].ToString() == "InspectMain") && (table.Rows[i][2].ToString() == "0"))
                        {
                            str = table.Rows[i][1].ToString();
                            if (this.btnCheckorder.Name == str)
                            {
                                this.btnCheckorder.Enabled = false;
                            }
                            else if (this.btnCheckGoods.Name == str)
                            {
                                this.btnCheckGoods.Enabled = false;
                            }
                        }
                    }
                }
            }
        }
    }
}

