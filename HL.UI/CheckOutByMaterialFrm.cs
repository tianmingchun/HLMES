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
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using HL.Framework;
    using HL.Controls;

    public class CheckOutByMaterialFrm : Form
    {
        private ArrayList arrindex = new ArrayList();
        private Button btnCheck;
        private Button btnExit;
        private Button btnQuery;
        private Button btnSubmit;
        private Button btnUpdateQty;
        private IContainer components;
        private CheckInColDetail detail;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label LblQty;
        private ListView lsvTasks;
        private const int RET_KEYSTROKE = 0;
        private MiddleService service = Global.MiddleService;
        private decimal storyQty;
        private CheckTask t;
        private HeadLabel TasksTitile;
        private CheckInstruction tempInstruction;
        private TextBox txtGosid;
        private TextBox txtQty;

        public CheckOutByMaterialFrm()
        {
            this.InitializeComponent();
        }

        private bool AddTotalDetail()
        {
            ArrayList keys = this.t.InsList.Keys as ArrayList;
            for (int i = 0; i < keys.Count; i++)
            {
                CheckInstruction instruction = this.t.InsList[keys[i]] as CheckInstruction;
                this.tempInstruction = instruction;
                this.detail = new CheckInColDetail();
                this.detail.AssociateInstruction = this.tempInstruction;
                this.detail.BatchNumber = DateTime.Now.ToString("yyyyMMdd");
                this.detail.Location = Management.GetSingleton().WarehouseNo + "01";
                this.detail.CollectedQuantity = decimal.Parse(this.lsvTasks.Items[Convert.ToInt32(this.arrindex[i])].SubItems[3].Text);
                this.detail.CollectedTime = DateTime.Now;
                try
                {
                    this.tempInstruction.Result.AddColDetail(this.detail);
                }
                catch (ApplicationException exception)
                {
                    MessageShow.Alert("Error", exception.Message);
                    return false;
                }
            }
            return true;
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (this.lsvTasks.Items.Count != 0)
            {
                if (this.btnCheck.Text == "选")
                {
                    for (int i = 0; i < this.lsvTasks.Items.Count; i++)
                    {
                        this.lsvTasks.Items[i].Checked = true;
                    }
                    this.btnCheck.Text = "不选";
                }
                else
                {
                    for (int j = 0; j < this.lsvTasks.Items.Count; j++)
                    {
                        this.lsvTasks.Items[j].Checked = false;
                    }
                    this.btnCheck.Text = "选";
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string msgExrror = string.Empty;
            if (this.txtGosid.Text == "")
            {
                MessageShow.Alert("error", "请输入物料号");
            }
            else
            {
                this.lsvTasks.Items.Clear();
                Cursor.Current = Cursors.WaitCursor;
                DataSet goodsCheckoutTasks = new DataSet();
                try
                {
                    goodsCheckoutTasks = this.service.GetGoodsCheckoutTasks(this.txtGosid.Text.Substring(1), Management.GetSingleton().WarehouseNo);
                    this.storyQty = Global.MiddleService.GetInventoryQty(Management.GetSingleton().WarehouseNo.Substring(0, 4).ToString(), this.txtGosid.Text.Substring(1).ToString(), Management.GetSingleton().WarehouseNo, ref msgExrror);
                }
                catch (ApplicationException exception)
                {
                    Cursor.Current = Cursors.Default;
                    MessageShow.Alert("错误", exception.Message);
                    this.txtGosid.Focus();
                    this.txtGosid.SelectAll();
                    return;
                }
                catch (Exception exception2)
                {
                    Cursor.Current = Cursors.Default;
                    MessageShow.Alert("错误", exception2.Message);
                    this.txtGosid.Focus();
                    this.txtGosid.SelectAll();
                    return;
                }
                Cursor.Current = Cursors.Default;
                DataTable table = goodsCheckoutTasks.Tables[0];
                if (table.Rows.Count != 0)
                {
                    this.LblQty.Text = this.storyQty.ToString();
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        ListViewItem item = new ListViewItem(new string[] { "", table.Rows[i]["PROPOID"].ToString(), table.Rows[i]["TIOPOLID"].ToString(), table.Rows[i]["PLANQTY"].ToString(), table.Rows[i]["PLANQTY"].ToString(), table.Rows[i]["TIOARVDATE"].ToString(), table.Rows[i]["TIOSNO"].ToString(), table.Rows[i]["MATCANOVER"].ToString() });
                        this.lsvTasks.Items.Add(item);
                    }
                    this.txtGosid.Focus();
                    this.txtGosid.SelectAll();
                }
                else
                {
                    MessageShow.Alert("error", "当前该货物不存在收料单");
                    this.txtGosid.Focus();
                    this.txtGosid.SelectAll();
                    this.lsvTasks.Items.Clear();
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.lsvTasks.Items.Count != 0)
            {
                decimal num = 0M;
                for (int i = 0; i < this.lsvTasks.Items.Count; i++)
                {
                    if (this.lsvTasks.Items[i].Checked)
                    {
                        num += decimal.Parse(this.lsvTasks.Items[i].SubItems[3].Text.ToString());
                    }
                }
                if (num > this.storyQty)
                {
                    MessageShow.Alert("Error", "指定的数量超出库存");
                    this.txtGosid.Focus();
                    this.txtGosid.SelectAll();
                }
                else
                {
                    this.insertInstruction();
                }
            }
        }

        private void btnUpdateQty_Click(object sender, EventArgs e)
        {
            if ((this.lsvTasks.SelectedIndices != null) && (this.lsvTasks.SelectedIndices.Count != 0))
            {
                if (!this.lsvTasks.Items[this.lsvTasks.SelectedIndices[0]].Checked)
                {
                    MessageShow.Alert("error", "请先勾选要修改数量的物料");
                }
                else if (this.txtQty.Text == "")
                {
                    MessageShow.Alert("error", "请输入实际到货数");
                }
                else if (this.CheckQuantity(this.txtQty.Text))
                {
                    if ((this.lsvTasks.Items[this.lsvTasks.SelectedIndices[0]].SubItems[7].Text.ToString() == "0") && (decimal.Parse(this.txtQty.Text.ToString()) > decimal.Parse(this.lsvTasks.Items[this.lsvTasks.SelectedIndices[0]].SubItems[4].Text.ToString())))
                    {
                        MessageShow.Alert("error", "发料数不能大于任务数");
                        this.txtQty.Focus();
                        this.txtQty.SelectAll();
                    }
                    else
                    {
                        this.lsvTasks.Items[this.lsvTasks.SelectedIndices[0]].SubItems[3].Text = this.txtQty.Text;
                        this.txtQty.Text = "";
                    }
                }
                else
                {
                    MessageShow.Alert("error", "输入的数量不合法");
                    this.txtQty.Focus();
                    this.txtQty.SelectAll();
                }
            }
        }

        private void CheckOutByMaterialFrm_Activated(object sender, EventArgs e)
        {
            OEM_Catchwell.SetBeamHold(false);
            this.txtGosid.Focus();
            this.txtGosid.SelectAll();
        }

        private void CheckOutByMaterialFrm_Deactivate(object sender, EventArgs e)
        {
            OEM_Catchwell.SetBeamHold(true);
        }

        private void CheckOutByMaterialFrm_KeyDown(object sender, KeyEventArgs e)
        {
            DialogResult none = DialogResult.None;
            if (e.KeyValue == 0x84)
            {
                none = new ConfigFrm().ShowDialog();
            }
            if (none == DialogResult.OK)
            {
                this.btnQuery_Click(null, null);
            }
        }

        private void CheckOutByMaterialFrm_Load(object sender, EventArgs e)
        {
            this.txtGosid.Focus();
            this.InitializeLsvDetail();
        }

        public bool CheckQuantity(string count)
        {
            Regex regex = new Regex(@"^-?\d+(\.\d{1,3})?$");
            if (!regex.IsMatch(count))
            {
                return false;
            }
            return true;
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
            this.txtGosid = new TextBox();
            this.btnQuery = new Button();
            this.btnSubmit = new Button();
            this.btnExit = new Button();
            this.lsvTasks = new ListView();
            this.label2 = new Label();
            this.txtQty = new TextBox();
            this.btnUpdateQty = new Button();
            this.label3 = new Label();
            this.LblQty = new Label();
            this.btnCheck = new Button();
            this.TasksTitile = new HeadLabel();
            base.SuspendLayout();
            this.label1.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label1.Location = new Point(14, 0x1a);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x34, 20);
            this.label1.Text = "物料:";
            this.txtGosid.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.txtGosid.Location = new Point(60, 0x19);
            this.txtGosid.Name = "txtGosid";
            this.txtGosid.Size = new Size(100, 0x15);
            this.txtGosid.TabIndex = 50;
            this.txtGosid.GotFocus += new EventHandler(this.txtGosid_GotFocus);
            this.txtGosid.KeyDown += new KeyEventHandler(this.txtGosid_KeyDown);
            this.btnQuery.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnQuery.Location = new Point(3, 0x51);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new Size(0x3f, 0x18);
            this.btnQuery.TabIndex = 0x33;
            this.btnQuery.Text = "检索";
            this.btnQuery.Click += new EventHandler(this.btnQuery_Click);
            this.btnSubmit.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnSubmit.Location = new Point(0x53, 0x51);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new Size(0x3f, 0x18);
            this.btnSubmit.TabIndex = 0x34;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.Click += new EventHandler(this.btnSubmit_Click);
            this.btnExit.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnExit.Location = new Point(0xa6, 0x51);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new Size(0x3f, 0x18);
            this.btnExit.TabIndex = 0x35;
            this.btnExit.Text = "返回";
            this.btnExit.Click += new EventHandler(this.btnExit_Click);
            this.lsvTasks.CheckBoxes = true;
            this.lsvTasks.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.lsvTasks.FullRowSelect = true;
            this.lsvTasks.Location = new Point(0, 0x6f);
            this.lsvTasks.Name = "lsvTasks";
            this.lsvTasks.Size = new Size(0xec, 0xb3);
            this.lsvTasks.TabIndex = 0x3e;
            this.lsvTasks.View = View.Details;
            this.label2.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label2.Location = new Point(2, 0x37);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 20);
            this.label2.Text = "发料数:";
            this.txtQty.Location = new Point(60, 0x34);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new Size(100, 0x17);
            this.txtQty.TabIndex = 0x41;
            this.txtQty.KeyDown += new KeyEventHandler(this.txtQty_KeyDown);
            this.btnUpdateQty.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnUpdateQty.Location = new Point(0xa6, 0x34);
            this.btnUpdateQty.Name = "btnUpdateQty";
            this.btnUpdateQty.Size = new Size(0x3d, 0x17);
            this.btnUpdateQty.TabIndex = 0x42;
            this.btnUpdateQty.Text = "确定";
            this.btnUpdateQty.Click += new EventHandler(this.btnUpdateQty_Click);
            this.label3.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label3.ForeColor = Color.FromArgb(0, 0, 0);
            this.label3.Location = new Point(0xa4, 0x1b);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x2e, 20);
            this.label3.Text = "库存：";
            this.LblQty.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.LblQty.ForeColor = Color.Red;
            this.LblQty.Location = new Point(0xcb, 0x1a);
            this.LblQty.Name = "LblQty";
            this.LblQty.Size = new Size(0x22, 20);
            this.btnCheck.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnCheck.Location = new Point(0, 0x6f);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new Size(0x19, 0x12);
            this.btnCheck.TabIndex = 0x45;
            this.btnCheck.Text = "选";
            this.btnCheck.Click += new EventHandler(this.btnCheck_Click);
            this.TasksTitile.BackColor = SystemColors.HotTrack;
            this.TasksTitile.Font = new Font("Arial", 14f, FontStyle.Regular);
            this.TasksTitile.ForeColor = SystemColors.ActiveCaptionText;
            this.TasksTitile.Location = new Point(0, 0);
            this.TasksTitile.Name = "TasksTitile";
            this.TasksTitile.Size = new Size(0xed, 0x18);
            this.TasksTitile.TabIndex = 0x30;
            this.TasksTitile.Text = "按物料发料";
            base.AutoScaleDimensions = new SizeF(96f, 96f);
            //base.AutoScaleMode = AutoScaleMode.Dpi;
            this.AutoScroll = true;
            base.ClientSize = new Size(240, 290);
            base.ControlBox = false;
            base.Controls.Add(this.btnCheck);
            base.Controls.Add(this.LblQty);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.btnUpdateQty);
            base.Controls.Add(this.txtQty);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.lsvTasks);
            base.Controls.Add(this.btnExit);
            base.Controls.Add(this.btnSubmit);
            base.Controls.Add(this.btnQuery);
            base.Controls.Add(this.txtGosid);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.TasksTitile);
            //base.FormBorderStyle = FormBorderStyle.None;
            base.KeyPreview = true;
            base.Name = "CheckOutByMaterialFrm";
            this.Text = "按物料发料";
            base.Deactivate += new EventHandler(this.CheckOutByMaterialFrm_Deactivate);
            base.Load += new EventHandler(this.CheckOutByMaterialFrm_Load);
            base.Activated += new EventHandler(this.CheckOutByMaterialFrm_Activated);
            base.KeyDown += new KeyEventHandler(this.CheckOutByMaterialFrm_KeyDown);
            base.ResumeLayout(false);
        }

        private void InitializeLsvDetail()
        {
            this.lsvTasks.Columns.Clear();
            this.lsvTasks.Columns.Add(" ", 0x19, HorizontalAlignment.Center);
            this.lsvTasks.Columns.Add("生产订单", 100, HorizontalAlignment.Center);
            this.lsvTasks.Columns.Add("行号", 40, HorizontalAlignment.Center);
            this.lsvTasks.Columns.Add("采数", 40, HorizontalAlignment.Center);
            this.lsvTasks.Columns.Add("任数", 40, HorizontalAlignment.Center);
            this.lsvTasks.Columns.Add("日期", 100, HorizontalAlignment.Center);
            this.lsvTasks.Columns.Add("操作号", 0, HorizontalAlignment.Center);
            this.lsvTasks.Columns.Add("是否超发", 0, HorizontalAlignment.Center);
        }

        private void insertInstruction()
        {
            string str = string.Empty;
            ArrayList list = new ArrayList();
            for (int i = 0; i < this.lsvTasks.Items.Count; i++)
            {
                if (this.lsvTasks.Items[i].Checked)
                {
                    list.Add(i);
                    this.t = new CheckTask(this.lsvTasks.Items[i].SubItems[1].Text.ToString());
                    CheckInstruction ins = new CheckInstruction(this.lsvTasks.Items[i].SubItems[6].Text.ToString(), decimal.Parse(this.lsvTasks.Items[i].SubItems[3].Text.ToString()));
                    ins.GoodsID = this.txtGosid.Text.Substring(1);
                    this.t.AddIns(ins);
                    ArrayList keys = this.t.InsList.Keys as ArrayList;
                    for (int j = 0; j < keys.Count; j++)
                    {
                        CheckInstruction instruction2 = this.t.InsList[keys[j]] as CheckInstruction;
                        this.tempInstruction = instruction2;
                        this.detail = new CheckInColDetail();
                        this.detail.AssociateInstruction = this.tempInstruction;
                        this.detail.BatchNumber = DateTime.Now.ToString("yyyyMMdd");
                        this.detail.Location = Management.GetSingleton().WarehouseNo + "01";
                        this.detail.CollectedQuantity = decimal.Parse(this.lsvTasks.Items[i].SubItems[3].Text);
                        this.detail.CollectedTime = DateTime.Now;
                        try
                        {
                            this.tempInstruction.Result.AddColDetail(this.detail);
                        }
                        catch (ApplicationException exception)
                        {
                            MessageShow.Alert("Error", exception.Message);
                            return;
                        }
                    }
                    try
                    {
                        Global.MiddleService.SubmitOutRecord(this.lsvTasks.Items[i].SubItems[1].Text, Global.CurrUser.ToString(), this.t, ref str);
                    }
                    catch (ApplicationException exception2)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageShow.Alert("Error", exception2.Message);
                        return;
                    }
                    catch (Exception exception3)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageShow.Alert("UnKnowError", exception3.Message);
                        return;
                    }
                    Cursor.Current = Cursors.Default;
                }
            }
            MessageShow.Alert("提示", str);
            this.btnQuery_Click(null, null);
            list.Clear();
        }

        private void txtGosid_GotFocus(object sender, EventArgs e)
        {
            OEM_Catchwell.SetOpMode(0);
        }

        private void txtGosid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                this.btnQuery_Click(sender, e);
            }
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                this.btnUpdateQty_Click(null, null);
            }
        }
    }
}

