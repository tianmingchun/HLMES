namespace HL.UI.Common
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

    public class QueryOrderForm : Form
    {
        private Button btnConfirm;
        private Button btnExit;
        private Button btnQuery;
        private IContainer components;
        private DateTimePicker dateTimeBegin;
        private DateTimePicker dateTimeEnd;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtMATNR;
        private Label lblOnline;
        private ListView lsvTasks;
        public TaskType TaskType = TaskType.OuterOrderCheckIn;

        public QueryOrderForm()
        {
            this.InitializeComponent();
        }

        public string SelectedOrderID
        {
            get
            {
                return this.lsvTasks.Items[this.lsvTasks.SelectedIndices[0]].SubItems[0].Text;
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (this.lsvTasks.Items.Count == 0)
            {
                MessageShow.Alert("提示", "请先查询订单。");
                return;
            }
            if ((this.lsvTasks.SelectedIndices != null) && (this.lsvTasks.SelectedIndices.Count != 0))
            {
                base.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageShow.Alert("提示", "请选择订单行。");
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
            ServiceOnline so;
            try
            {
                set = HL.Framework.ServiceCaller.GetOuterOrderCheckIn(Management.GetSingleton().WarehouseNo,this.txtMATNR.Text.Trim(), begintime, endtime,out so);
            }
            
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageShow.Alert("错误", ex.Message);
                return;
            }
            Cursor.Current = Cursors.Default;
            
            if (so == ServiceOnline.Offline)
            {
                this.lblOnline.Text = "未联网";
                this.lblOnline.ForeColor = Color.Red;
            }
            else
            {
                this.lblOnline.Text = "联网";
                this.lblOnline.ForeColor = Color.Green;
            }
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
                        ListViewItem item = new ListViewItem(new string[] { table.Rows[i]["EBELN"].ToString(), table.Rows[i]["EINDT"].ToString() });
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lsvTasks = new System.Windows.Forms.ListView();
            this.dateTimeBegin = new System.Windows.Forms.DateTimePicker();
            this.dateTimeEnd = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMATNR = new System.Windows.Forms.TextBox();
            this.lblOnline = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(9, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 20);
            this.label1.Text = "开始日期:";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(9, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 20);
            this.label2.Text = "结束日期:";
            // 
            // btnQuery
            // 
            this.btnQuery.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.btnQuery.Location = new System.Drawing.Point(4, 86);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(59, 25);
            this.btnQuery.TabIndex = 55;
            this.btnQuery.Text = "检索";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.btnConfirm.Location = new System.Drawing.Point(86, 86);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(59, 25);
            this.btnConfirm.TabIndex = 56;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.btnExit.Location = new System.Drawing.Point(166, 86);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(59, 25);
            this.btnExit.TabIndex = 57;
            this.btnExit.Text = "返回";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lsvTasks
            // 
            this.lsvTasks.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.lsvTasks.FullRowSelect = true;
            this.lsvTasks.Location = new System.Drawing.Point(0, 115);
            this.lsvTasks.Name = "lsvTasks";
            this.lsvTasks.Size = new System.Drawing.Size(237, 127);
            this.lsvTasks.TabIndex = 69;
            this.lsvTasks.View = System.Windows.Forms.View.Details;
            // 
            // dateTimeBegin
            // 
            this.dateTimeBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeBegin.Location = new System.Drawing.Point(86, 31);
            this.dateTimeBegin.Name = "dateTimeBegin";
            this.dateTimeBegin.Size = new System.Drawing.Size(139, 24);
            this.dateTimeBegin.TabIndex = 72;
            this.dateTimeBegin.ValueChanged += new System.EventHandler(this.dateTimeBegin_ValueChanged);
            // 
            // dateTimeEnd
            // 
            this.dateTimeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeEnd.Location = new System.Drawing.Point(86, 60);
            this.dateTimeEnd.Name = "dateTimeEnd";
            this.dateTimeEnd.Size = new System.Drawing.Size(139, 24);
            this.dateTimeEnd.TabIndex = 73;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(9, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 20);
            this.label3.Text = "物料号:";
            // 
            // txtMATNR
            // 
            this.txtMATNR.Location = new System.Drawing.Point(86, 4);
            this.txtMATNR.Name = "txtMATNR";
            this.txtMATNR.Size = new System.Drawing.Size(139, 23);
            this.txtMATNR.TabIndex = 78;
            // 
            // lblOnline
            // 
            this.lblOnline.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.lblOnline.Location = new System.Drawing.Point(9, 245);
            this.lblOnline.Name = "lblOnline";
            this.lblOnline.Size = new System.Drawing.Size(228, 26);
            // 
            // QueryOrderByForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 290);
            this.ControlBox = false;
            this.Controls.Add(this.lblOnline);
            this.Controls.Add(this.txtMATNR);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTimeEnd);
            this.Controls.Add(this.dateTimeBegin);
            this.Controls.Add(this.lsvTasks);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "QueryOrderByForm";
            this.Text = "采购订单查询";
            this.Load += new System.EventHandler(this.QueryOrderByDate_Load);
            this.ResumeLayout(false);

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

        private void dateTimeBegin_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}

