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

    public class QueryOrderByDate : Form
    {
        private Button btnConfirm;
        private Button btnExit;
        private Button btnQuery;
        private IContainer components;
        private DateTimePicker dateTimeBegin;
        private DateTimePicker dateTimeEnd;
        private Label label1;
        private Label label2;
        private ListView lsvTasks;
        private HeadLabel TasksTitile;

        public QueryOrderByDate()
        {
            this.InitializeComponent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if ((this.lsvTasks.SelectedIndices != null) && (this.lsvTasks.SelectedIndices.Count != 0))
            {
                Order.GlobalVals.strOrder = this.lsvTasks.Items[this.lsvTasks.SelectedIndices[0]].SubItems[0].Text;
                base.Close();
                base.DialogResult = DialogResult.OK;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.lsvTasks.Items.Clear();
            Cursor.Current = Cursors.WaitCursor;
            DataSet set = new DataSet();
            DateTime begintime = Convert.ToDateTime(this.dateTimeBegin.Value.ToString("yyyy-MM-dd"));
            DateTime endtime = Convert.ToDateTime(this.dateTimeEnd.Value.ToString("yyyy-MM-dd"));
            try
            {
                set = Global.MiddleService.GetGetWTOExtTime(Management.GetSingleton().WarehouseNo, begintime, endtime);
            }
            catch (ApplicationException exception)
            {
                Cursor.Current = Cursors.Default;
                MessageShow.Alert("错误", exception.Message);
                return;
            }
            catch (Exception exception2)
            {
                Cursor.Current = Cursors.Default;
                MessageShow.Alert("错误", exception2.Message);
                return;
            }
            Cursor.Current = Cursors.Default;
            if (set.Tables.Count >= 1)
            {
                DataTable table = set.Tables[0];
                if (table.Rows.Count == 0)
                {
                    MessageShow.Alert("error", "查无订单");
                    this.lsvTasks.Items.Clear();
                }
                else
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        DataRow row1 = table.Rows[i];
                        ListViewItem item = new ListViewItem(new string[] { table.Rows[i]["PROPOID"].ToString(), table.Rows[i]["PROASKDATE"].ToString() });
                        this.lsvTasks.Items.Add(item);
                    }
                }
            }
            else
            {
                MessageShow.Alert("error", "查无订单");
                this.lsvTasks.Items.Clear();
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
            this.label1 = new Label();
            this.label2 = new Label();
            this.btnQuery = new Button();
            this.btnConfirm = new Button();
            this.btnExit = new Button();
            this.lsvTasks = new ListView();
            this.TasksTitile = new HeadLabel();
            this.dateTimeBegin = new DateTimePicker();
            this.dateTimeEnd = new DateTimePicker();
            base.SuspendLayout();
            this.label1.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label1.Location = new Point(9, 0x1f);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x53, 20);
            this.label1.Text = "开始日期:";
            this.label2.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label2.Location = new Point(9, 0x44);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x53, 20);
            this.label2.Text = "结束日期:";
            this.btnQuery.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnQuery.Location = new Point(4, 0x68);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new Size(0x3b, 0x19);
            this.btnQuery.TabIndex = 0x37;
            this.btnQuery.Text = "检索";
            this.btnQuery.Click += new EventHandler(this.btnQuery_Click);
            this.btnConfirm.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnConfirm.Location = new Point(0x56, 0x68);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new Size(0x3b, 0x19);
            this.btnConfirm.TabIndex = 0x38;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.Click += new EventHandler(this.btnConfirm_Click);
            this.btnExit.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnExit.Location = new Point(0xa6, 0x68);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new Size(0x3b, 0x19);
            this.btnExit.TabIndex = 0x39;
            this.btnExit.Text = "返回";
            this.btnExit.Click += new EventHandler(this.btnExit_Click);
            this.lsvTasks.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.lsvTasks.FullRowSelect = true;
            this.lsvTasks.Location = new Point(0, 0x87);
            this.lsvTasks.Name = "lsvTasks";
            this.lsvTasks.Size = new Size(0xed, 0x98);
            this.lsvTasks.TabIndex = 0x45;
            this.lsvTasks.View = View.Details;
            this.TasksTitile.BackColor = SystemColors.HotTrack;
            this.TasksTitile.Font = new Font("Arial", 14f, FontStyle.Regular);
            this.TasksTitile.ForeColor = SystemColors.ActiveCaptionText;
            this.TasksTitile.Location = new Point(0, 0);
            this.TasksTitile.Name = "TasksTitile";
            this.TasksTitile.Size = new Size(0xed, 0x18);
            this.TasksTitile.TabIndex = 50;
            this.TasksTitile.Text = "订单查询";
            this.dateTimeBegin.Format = DateTimePickerFormat.Custom;
            this.dateTimeBegin.Location = new Point(0x56, 0x1a);
            this.dateTimeBegin.Name = "dateTimeBegin";
            this.dateTimeBegin.Size = new Size(0x8b, 0x18);
            this.dateTimeBegin.TabIndex = 0x48;
            this.dateTimeEnd.Format = DateTimePickerFormat.Custom;
            this.dateTimeEnd.Location = new Point(0x56, 0x44);
            this.dateTimeEnd.Name = "dateTimeEnd";
            this.dateTimeEnd.Size = new Size(0x8b, 0x18);
            this.dateTimeEnd.TabIndex = 0x49;
            base.AutoScaleDimensions = new SizeF(96f, 96f);
            //base.AutoScaleMode = AutoScaleMode.Dpi;
            this.AutoScroll = true;
            base.ClientSize = new Size(240, 290);
            base.ControlBox = false;
            base.Controls.Add(this.dateTimeEnd);
            base.Controls.Add(this.dateTimeBegin);
            base.Controls.Add(this.lsvTasks);
            base.Controls.Add(this.btnExit);
            base.Controls.Add(this.btnConfirm);
            base.Controls.Add(this.btnQuery);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.TasksTitile);
            //base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "QueryOrderByDate";
            this.Text = "按日期查询时间";
            base.Load += new EventHandler(this.QueryOrderByDate_Load);
            base.ResumeLayout(false);
        }

        private void InitializeLsvDetail()
        {
            this.lsvTasks.Columns.Clear();
            this.lsvTasks.Columns.Add("订单号", 100, HorizontalAlignment.Center);
            this.lsvTasks.Columns.Add("日期", 140, HorizontalAlignment.Center);
        }

        private void QueryOrderByDate_Load(object sender, EventArgs e)
        {
            this.InitializeLsvDetail();
            this.dateTimeEnd.Value = Convert.ToDateTime(DateTime.Now.AddDays(2.0).ToString("yyyy-MM-dd"));
        }
    }
}

