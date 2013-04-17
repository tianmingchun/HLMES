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

    public class ReprintListFrm : Form
    {
        private Button btnDetail;
        private Button btnQuery;
        private Button button3;
        private CheckBox checkBox1;
        private IContainer components;
        private DateTimePicker dateTimePicker;
        private Label label1;
        private Label label2;
        private Label label3;
        private ListView lsvTasks;
        private MiddleService service = Global.MiddleService;
        private HeadLabel TasksTitile;
        private TextBox txtOrder;
        private TextBox txtProof;

        public ReprintListFrm()
        {
            this.InitializeComponent();
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            if ((this.lsvTasks.SelectedIndices != null) && (this.lsvTasks.SelectedIndices.Count != 0))
            {
                string text = this.lsvTasks.Items[this.lsvTasks.SelectedIndices[0]].SubItems[1].Text;
                string orderid = this.lsvTasks.Items[this.lsvTasks.SelectedIndices[0]].SubItems[0].Text;
                string printno = this.lsvTasks.Items[this.lsvTasks.SelectedIndices[0]].SubItems[2].Text;
                string printdate = this.lsvTasks.Items[this.lsvTasks.SelectedIndices[0]].SubItems[3].Text;
                string supplyname = this.lsvTasks.Items[this.lsvTasks.SelectedIndices[0]].SubItems[4].Text;
                string printloc = this.lsvTasks.Items[this.lsvTasks.SelectedIndices[0]].SubItems[5].Text;
                string userid = this.lsvTasks.Items[this.lsvTasks.SelectedIndices[0]].SubItems[6].Text;
                string pof = this.lsvTasks.Items[this.lsvTasks.SelectedIndices[0]].SubItems[7].Text;
                Cursor.Current = Cursors.WaitCursor;
                DataSet dsprint = new DataSet();
                try
                {
                    dsprint = this.service.GetRECORD(pof);
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
                if (new PrintDetailFrm(text, orderid, printno, printdate, supplyname, printloc, userid, pof, dsprint).ShowDialog() == DialogResult.OK)
                {
                    this.btnQuery_Click(sender, e);
                }
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (((this.txtOrder.Text == string.Empty) && (this.txtProof.Text == string.Empty)) && !this.checkBox1.Checked)
            {
                MessageShow.Alert("error", "请输入查询条件");
                this.txtOrder.Focus();
            }
            else
            {
                this.lsvTasks.Items.Clear();
                Cursor.Current = Cursors.WaitCursor;
                DataSet set = new DataSet();
                try
                {
                    if (this.checkBox1.Checked)
                    {
                        set = this.service.GetGetCPO(this.txtProof.Text, this.txtOrder.Text, this.dateTimePicker.Value.ToString("yyyyMMdd"));
                    }
                    else
                    {
                        set = this.service.GetGetCPO(this.txtProof.Text, this.txtOrder.Text, "");
                    }
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
                DataTable table = set.Tables[0];
                if (table.Rows.Count != 0)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        ListViewItem item = new ListViewItem(new string[] { table.Rows[i]["POFPOID"].ToString(), table.Rows[i]["POFERPID"].ToString(), table.Rows[i]["POFPRTNUM"].ToString(), table.Rows[i]["POFDATE"].ToString(), table.Rows[i]["CLN_NAME"].ToString(), table.Rows[i]["POFSTUID"].ToString(), table.Rows[i]["LOG_NAME"].ToString(), table.Rows[i]["POFID"].ToString() });
                        this.lsvTasks.Items.Add(item);
                    }
                }
                else
                {
                    MessageShow.Alert("error", "不存在出货记录");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                this.dateTimePicker.Enabled = true;
            }
            else
            {
                this.dateTimePicker.Enabled = false;
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
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.dateTimePicker = new DateTimePicker();
            this.lsvTasks = new ListView();
            this.txtOrder = new TextBox();
            this.txtProof = new TextBox();
            this.btnQuery = new Button();
            this.btnDetail = new Button();
            this.button3 = new Button();
            this.checkBox1 = new CheckBox();
            base.SuspendLayout();
            this.TasksTitile.BackColor = SystemColors.HotTrack;
            this.TasksTitile.Font = new Font("Arial", 14f, FontStyle.Regular);
            this.TasksTitile.ForeColor = SystemColors.ActiveCaptionText;
            this.TasksTitile.Location = new Point(0, 0);
            this.TasksTitile.Name = "TasksTitile";
            this.TasksTitile.Size = new Size(240, 0x18);
            this.TasksTitile.TabIndex = 0x3a;
            this.TasksTitile.Text = "补打";
            this.label1.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label1.Location = new Point(4, 0x22);
            this.label1.Name = "label1";
            this.label1.Size = new Size(100, 20);
            this.label1.Text = "采购订单号：";
            this.label2.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label2.Location = new Point(4, 60);
            this.label2.Name = "label2";
            this.label2.Size = new Size(100, 20);
            this.label2.Text = "收货凭证号：";
            this.label3.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label3.Location = new Point(13, 0x55);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x45, 20);
            this.label3.Text = "收货日期:";
            this.dateTimePicker.Enabled = false;
            this.dateTimePicker.Format = DateTimePickerFormat.Short;
            this.dateTimePicker.Location = new Point(0x4e, 0x55);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new Size(0x6b, 0x18);
            this.dateTimePicker.TabIndex = 0x3b;
            this.lsvTasks.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.lsvTasks.FullRowSelect = true;
            this.lsvTasks.Location = new Point(2, 160);
            this.lsvTasks.Name = "lsvTasks";
            this.lsvTasks.Size = new Size(0xeb, 0x7f);
            this.lsvTasks.TabIndex = 60;
            this.lsvTasks.View = View.Details;
            this.txtOrder.Location = new Point(0x4e, 0x1f);
            this.txtOrder.Name = "txtOrder";
            this.txtOrder.Size = new Size(0x8e, 0x17);
            this.txtOrder.TabIndex = 0x3d;
            this.txtProof.Location = new Point(0x4e, 0x39);
            this.txtProof.Name = "txtProof";
            this.txtProof.Size = new Size(0x8e, 0x17);
            this.txtProof.TabIndex = 0x3e;
            this.btnQuery.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnQuery.Location = new Point(4, 0x7d);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new Size(0x45, 0x1d);
            this.btnQuery.TabIndex = 0x42;
            this.btnQuery.Text = "检索";
            this.btnQuery.Click += new EventHandler(this.btnQuery_Click);
            this.btnDetail.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnDetail.Location = new Point(0x56, 0x7d);
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new Size(0x45, 0x1d);
            this.btnDetail.TabIndex = 0x43;
            this.btnDetail.Text = "收货记录";
            this.btnDetail.Click += new EventHandler(this.btnDetail_Click);
            this.button3.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.button3.Location = new Point(0xa8, 0x7d);
            this.button3.Name = "button3";
            this.button3.Size = new Size(0x45, 0x1d);
            this.button3.TabIndex = 0x44;
            this.button3.Text = "返回";
            this.button3.Click += new EventHandler(this.button3_Click);
            this.checkBox1.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.checkBox1.Location = new Point(0xbf, 0x56);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new Size(0x1d, 20);
            this.checkBox1.TabIndex = 0x48;
            this.checkBox1.Click += new EventHandler(this.checkBox1_Click);
            base.AutoScaleDimensions = new SizeF(96f, 96f);
            //base.AutoScaleMode = AutoScaleMode.Dpi;
            this.AutoScroll = true;
            base.ClientSize = new Size(240, 290);
            base.ControlBox = false;
            base.Controls.Add(this.checkBox1);
            base.Controls.Add(this.button3);
            base.Controls.Add(this.btnDetail);
            base.Controls.Add(this.btnQuery);
            base.Controls.Add(this.txtProof);
            base.Controls.Add(this.txtOrder);
            base.Controls.Add(this.lsvTasks);
            base.Controls.Add(this.dateTimePicker);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.TasksTitile);
            //base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "ReprintListFrm";
            this.Text = "补打";
            base.Load += new EventHandler(this.ReprintListFrm_Load);
            base.ResumeLayout(false);
        }

        private void InitializeLsvDetail()
        {
            this.lsvTasks.Columns.Clear();
            this.lsvTasks.Columns.Add("采购订单", 100, HorizontalAlignment.Center);
            this.lsvTasks.Columns.Add("收货凭证号", 100, HorizontalAlignment.Center);
            this.lsvTasks.Columns.Add("打印次数", 60, HorizontalAlignment.Center);
            this.lsvTasks.Columns.Add("收货日期", -2, HorizontalAlignment.Center);
            this.lsvTasks.Columns.Add("供应商", -2, HorizontalAlignment.Center);
            this.lsvTasks.Columns.Add("库房", -2, HorizontalAlignment.Center);
            this.lsvTasks.Columns.Add("收货人", -2, HorizontalAlignment.Center);
            this.lsvTasks.Columns.Add("序列号", 0, HorizontalAlignment.Center);
        }

        private void ReprintListFrm_Load(object sender, EventArgs e)
        {
            this.txtOrder.Focus();
            this.InitializeLsvDetail();
        }
    }
}

