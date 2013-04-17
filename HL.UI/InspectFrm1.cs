namespace HL.UI
{
    using BizLayer;
    using Entity;
    using SharpExCS;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using HL.Framework;
    using HL.Controls;

    public class InspectFrm1 : Form
    {
        private Button btnBack;
        private Button btnCollect;
        private IContainer components;
        private string insGodis = "";
        private string insgodsname = "";
        private string insLocation = "";
        private string inspectNo = "";
        private DataSet InspectTasks = new DataSet();
        private string insPici = "";
        private decimal insSelectqty;
        private decimal insTotalqty;
        private Label label1;
        private Label label2;
        private ListView lsvTasks;
        private string path = "";
        private const int RET_KEYSTROKE = 0;
        private MiddleService service = Global.MiddleService;
        private string taskID = "";
        private HeadLabel TasksTitile;
        private TextBox txtGosid;
        private TextBox txtInspectNo;

        public InspectFrm1(DataSet Instasks)
        {
            this.InitializeComponent();
            this.InspectTasks = Instasks;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnCollect_Click(object sender, EventArgs e)
        {
            if (this.txtInspectNo.Text != "")
            {
                this.inspectNo = this.txtInspectNo.Text;
                ArrayList list = new ArrayList();
                for (int i = 0; i < this.lsvTasks.Items.Count; i++)
                {
                    list.Add(this.lsvTasks.Items[i].SubItems[0].Text);
                }
                if (this.txtInspectNo.Text != "")
                {
                    if (!list.Contains(this.inspectNo))
                    {
                        MessageShow.Alert("error", "检验单号不合法，请重新扫描");
                        this.txtInspectNo.Focus();
                        this.txtInspectNo.SelectAll();
                        return;
                    }
                    for (int j = 0; j < this.lsvTasks.Items.Count; j++)
                    {
                        if (this.inspectNo == this.lsvTasks.Items[j].SubItems[0].Text)
                        {
                            this.taskID = this.lsvTasks.Items[j].SubItems[0].Text;
                            this.insGodis = this.lsvTasks.Items[j].SubItems[1].Text;
                            this.insTotalqty = decimal.Parse(this.lsvTasks.Items[j].SubItems[2].Text);
                            this.insSelectqty = decimal.Parse(this.lsvTasks.Items[j].SubItems[3].Text);
                            this.insLocation = this.lsvTasks.Items[j].SubItems[4].Text;
                            this.insPici = this.lsvTasks.Items[j].SubItems[5].Text;
                            this.insgodsname = this.lsvTasks.Items[j].SubItems[6].Text;
                        }
                    }
                }
            }
            else
            {
                if (((this.lsvTasks.Items.Count == 0) || (this.lsvTasks.SelectedIndices == null)) || (this.lsvTasks.SelectedIndices.Count == 0))
                {
                    return;
                }
                this.taskID = this.lsvTasks.Items[this.lsvTasks.SelectedIndices[0]].SubItems[0].Text;
                this.inspectNo = this.lsvTasks.Items[this.lsvTasks.SelectedIndices[0]].SubItems[0].Text;
                this.insGodis = this.lsvTasks.Items[this.lsvTasks.SelectedIndices[0]].SubItems[1].Text;
                this.insTotalqty = decimal.Parse(this.lsvTasks.Items[this.lsvTasks.SelectedIndices[0]].SubItems[2].Text);
                this.insSelectqty = decimal.Parse(this.lsvTasks.Items[this.lsvTasks.SelectedIndices[0]].SubItems[3].Text);
                this.insLocation = this.lsvTasks.Items[this.lsvTasks.SelectedIndices[0]].SubItems[4].Text;
                this.insPici = this.lsvTasks.Items[this.lsvTasks.SelectedIndices[0]].SubItems[5].Text;
                this.insgodsname = this.lsvTasks.Items[this.lsvTasks.SelectedIndices[0]].SubItems[6].Text;
            }
            if (new InspectFrm2(this.inspectNo, this.insLocation, this.insGodis, this.insPici, this.insTotalqty, this.insSelectqty, this.insgodsname).ShowDialog() == DialogResult.OK)
            {
                base.Close();
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
            this.txtInspectNo = new TextBox();
            this.txtGosid = new TextBox();
            this.btnCollect = new Button();
            this.btnBack = new Button();
            this.lsvTasks = new ListView();
            base.SuspendLayout();
            this.TasksTitile.BackColor = SystemColors.HotTrack;
            this.TasksTitile.Font = new Font("Arial", 14f, FontStyle.Regular);
            this.TasksTitile.ForeColor = SystemColors.ActiveCaptionText;
            this.TasksTitile.Location = new Point(-3, 3);
            this.TasksTitile.Name = "TasksTitile";
            this.TasksTitile.Size = new Size(240, 0x18);
            this.TasksTitile.TabIndex = 0x2f;
            this.TasksTitile.Text = "任务列表";
            this.label1.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label1.Location = new Point(12, 60);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x4f, 20);
            this.label1.Text = "检验单号：";
            this.label2.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label2.Location = new Point(12, 0x5f);
            this.label2.Name = "label2";
            this.label2.Size = new Size(70, 20);
            this.label2.Text = "货号：";
            this.txtInspectNo.Location = new Point(0x62, 60);
            this.txtInspectNo.Name = "txtInspectNo";
            this.txtInspectNo.Size = new Size(0x7e, 0x17);
            this.txtInspectNo.TabIndex = 50;
            this.txtInspectNo.GotFocus += new EventHandler(this.txtInspectNo_GotFocus);
            this.txtInspectNo.KeyDown += new KeyEventHandler(this.txtInspectNo_KeyDown);
            this.txtGosid.Location = new Point(0x62, 0x5b);
            this.txtGosid.Name = "txtGosid";
            this.txtGosid.Size = new Size(0x7e, 0x17);
            this.txtGosid.TabIndex = 0x33;
            this.txtGosid.GotFocus += new EventHandler(this.txtGosid_GotFocus);
            this.txtGosid.KeyDown += new KeyEventHandler(this.txtGosid_KeyDown);
            this.btnCollect.ForeColor = Color.FromArgb(0, 0, 0);
            this.btnCollect.Location = new Point(0x13, 0x81);
            this.btnCollect.Name = "btnCollect";
            this.btnCollect.Size = new Size(0x48, 0x23);
            this.btnCollect.TabIndex = 0x34;
            this.btnCollect.Text = "检验";
            this.btnCollect.Click += new EventHandler(this.btnCollect_Click);
            this.btnBack.Location = new Point(140, 0x81);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new Size(0x48, 0x23);
            this.btnBack.TabIndex = 0x35;
            this.btnBack.Text = "返回";
            this.btnBack.Click += new EventHandler(this.btnBack_Click);
            this.lsvTasks.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.lsvTasks.FullRowSelect = true;
            this.lsvTasks.Location = new Point(1, 170);
            this.lsvTasks.Name = "lsvTasks";
            this.lsvTasks.Size = new Size(0xed, 0x7a);
            this.lsvTasks.TabIndex = 0x36;
            this.lsvTasks.View = View.Details;
            base.AutoScaleDimensions = new SizeF(96f, 96f);
            //base.AutoScaleMode = AutoScaleMode.Dpi;
            this.AutoScroll = true;
            base.ClientSize = new Size(0xee, 0x127);
            base.ControlBox = false;
            base.Controls.Add(this.lsvTasks);
            base.Controls.Add(this.btnBack);
            base.Controls.Add(this.btnCollect);
            base.Controls.Add(this.txtGosid);
            base.Controls.Add(this.txtInspectNo);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.TasksTitile);
            //base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "InspectFrm1";
            this.Text = " ";
            base.Deactivate += new EventHandler(this.InspectFrm1_Deactivate);
            base.Load += new EventHandler(this.InspectFrm1_Load);
            base.Activated += new EventHandler(this.InspectFrm1_Activated);
            base.ResumeLayout(false);
        }

        private void InitializeLsvDetail()
        {
            this.lsvTasks.Columns.Clear();
            this.lsvTasks.Columns.Add("检验单", -2, HorizontalAlignment.Center);
            this.lsvTasks.Columns.Add("货号", -2, HorizontalAlignment.Center);
            this.lsvTasks.Columns.Add("到货数量", -2, HorizontalAlignment.Center);
            this.lsvTasks.Columns.Add("抽检数量", -2, HorizontalAlignment.Center);
            this.lsvTasks.Columns.Add("库位", -2, HorizontalAlignment.Center);
            this.lsvTasks.Columns.Add("批次", -2, HorizontalAlignment.Center);
            this.lsvTasks.Columns.Add("物料名称", 0, HorizontalAlignment.Center);
        }

        private void InsertListview()
        {
            DataTable table = this.InspectTasks.Tables[0];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem(new string[] { table.Rows[i]["ICNO"].ToString(), table.Rows[i]["Icmatcode"].ToString(), table.Rows[i]["ICNumber"].ToString(), table.Rows[i]["ICCheckNum"].ToString(), table.Rows[i]["ICSITE"].ToString(), table.Rows[i]["ICMATSN"].ToString(), table.Rows[i]["MATNAME"].ToString() });
                this.lsvTasks.Items.Add(item);
            }
        }

        private void InspectFrm1_Activated(object sender, EventArgs e)
        {
            OEM_Catchwell.SetBeamHold(false);
        }

        private void InspectFrm1_Deactivate(object sender, EventArgs e)
        {
            OEM_Catchwell.SetBeamHold(true);
        }

        private void InspectFrm1_Load(object sender, EventArgs e)
        {
            this.txtInspectNo.Focus();
            this.InitializeLsvDetail();
            this.InsertListview();
        }

        private void txtGosid_GotFocus(object sender, EventArgs e)
        {
            OEM_Catchwell.SetOpMode(0);
        }

        private void txtGosid_KeyDown(object sender, KeyEventArgs e)
        {
            if ((this.txtGosid.Text != "") && (e.KeyCode == Keys.Return))
            {
                this.lsvTasks.Items.Clear();
                Cursor.Current = Cursors.WaitCursor;
                DataSet goodsInspectTasks = new DataSet();
                try
                {
                    goodsInspectTasks = this.service.GetGoodsInspectTasks(this.txtGosid.Text.Substring(1));
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
                DataTable table = goodsInspectTasks.Tables[0];
                if (table.Rows.Count != 0)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        ListViewItem item = new ListViewItem(new string[] { table.Rows[i]["ICNO"].ToString(), table.Rows[i]["Icmatcode"].ToString(), table.Rows[i]["ICNumber"].ToString(), table.Rows[i]["ICCheckNum"].ToString(), table.Rows[i]["ICSITE"].ToString(), table.Rows[i]["ICMATSN"].ToString(), table.Rows[i]["MATNAME"].ToString() });
                        this.lsvTasks.Items.Add(item);
                    }
                }
                else
                {
                    MessageShow.Alert("error", "当前该货物不存在检验单");
                }
            }
        }

        private void txtInspectNo_GotFocus(object sender, EventArgs e)
        {
            OEM_Catchwell.SetOpMode(0);
        }

        private void txtInspectNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.lsvTasks.Items.Count != 0)
            {
                ArrayList list = new ArrayList();
                for (int i = 0; i < this.lsvTasks.Items.Count; i++)
                {
                    list.Add(this.lsvTasks.Items[i].SubItems[0].Text);
                }
                if ((e.KeyValue == 13) && (this.txtInspectNo.Text != ""))
                {
                    if (list.Contains(this.txtInspectNo.Text))
                    {
                        this.lsvTasks.Items[list.IndexOf(this.txtInspectNo.Text)].Selected = true;
                        this.lsvTasks.EnsureVisible(list.IndexOf(this.txtInspectNo.Text));
                        this.txtInspectNo.Focus();
                        this.txtInspectNo.SelectAll();
                        this.btnCollect_Click(null, null);
                    }
                    else
                    {
                        MessageShow.Alert("error", "检验单号不合法，请重新扫描");
                        this.txtInspectNo.Focus();
                        this.txtInspectNo.SelectAll();
                    }
                }
            }
        }

        private void UpdataTaskStatue()
        {
            string text = this.lsvTasks.Items[this.lsvTasks.SelectedIndices[0]].SubItems[0].Text;
            this.lsvTasks.Items.RemoveAt(this.lsvTasks.SelectedIndices[0]);
        }
    }
}

