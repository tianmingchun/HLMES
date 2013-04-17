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

    public class CheckOutByOrderFrm : Form
    {
        private ArrayList arrindex = new ArrayList();
        private Button btnCheck;
        private Button btnExit;
        private Button btnQuery;
        private Button btnQueryByDate;
        private Button btnSubmit;
        private Button btnUpdateQty;
        private IContainer components;
        private Label label1;
        private Label label2;
        private HeadLabel lblSingleID;
        private ListView lsvTasks;
        private const int RET_KEYSTROKE = 0;
        private MiddleService service = Global.MiddleService;
        private CheckTask t;
        private HeadLabel TasksTitile;
        private CheckInstruction tempInstruction;
        private TextBox txtOrderid;
        private TextBox txtQty;

        public CheckOutByOrderFrm()
        {
            this.InitializeComponent();
        }

        private bool AddTotalDetail()
        {
            ArrayList keys = this.t.InsList.Keys as ArrayList;
            for (int i = 0; i < keys.Count; i++)
            {
                if (decimal.Parse(this.lsvTasks.Items[Convert.ToInt32(this.arrindex[i])].SubItems[3].Text) <= decimal.Parse(this.lsvTasks.Items[Convert.ToInt32(this.arrindex[i])].SubItems[2].Text))
                {
                    CheckInstruction instruction = this.t.InsList[keys[i]] as CheckInstruction;
                    this.tempInstruction = instruction;
                    CheckInColDetail result = new CheckInColDetail();
                    result.AssociateInstruction = this.tempInstruction;
                    result.BatchNumber = DateTime.Now.ToString("yyyyMMdd");
                    result.SingletonNo = this.lblSingleID.Text;
                    result.Location = Management.GetSingleton().WarehouseNo + "01";
                    result.CollectedQuantity = decimal.Parse(this.lsvTasks.Items[Convert.ToInt32(this.arrindex[i])].SubItems[3].Text);
                    result.CollectedTime = DateTime.Now;
                    result.Unit = this.lsvTasks.Items[Convert.ToInt32(this.arrindex[i])].SubItems[6].Text;
                    try
                    {
                        this.tempInstruction.Result.AddColDetail(result);
                    }
                    catch (ApplicationException exception)
                    {
                        MessageShow.Alert("Error", exception.Message);
                        return false;
                    }
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
            if (this.txtOrderid.Text == "")
            {
                MessageShow.Alert("error", "请输入发料单号");
                this.txtOrderid.Focus();
            }
            else
            {
                this.lsvTasks.Items.Clear();
                Cursor.Current = Cursors.WaitCursor;
                DataSet set = new DataSet();
                string text = this.txtOrderid.Text;
                if (text.Length < 12)
                {
                    text = text.PadLeft(12, '0');
                }
                try
                {
                    set = this.service.dsGetTaskByOrderID(Management.GetSingleton().WarehouseNo, text, EnumTaskType.CheckOut);
                }
                catch (ApplicationException exception)
                {
                    Cursor.Current = Cursors.Default;
                    MessageShow.Alert("错误", exception.Message);
                    this.txtOrderid.Focus();
                    this.txtOrderid.SelectAll();
                    return;
                }
                catch (Exception exception2)
                {
                    Cursor.Current = Cursors.Default;
                    MessageShow.Alert("错误", exception2.Message);
                    this.txtOrderid.Focus();
                    this.txtOrderid.SelectAll();
                    return;
                }
                Cursor.Current = Cursors.Default;
                DataTable table = set.Tables[0];
                if (table.Rows.Count != 0)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        decimal num2;
                        decimal num3;
                        DataRow row = table.Rows[i];
                        if (row["tiofiniqty"].ToString() == "")
                        {
                            num2 = decimal.Parse(row["TioTaskQty"].ToString()) - 0M;
                        }
                        else
                        {
                            num2 = decimal.Parse(row["TioTaskQty"].ToString()) - decimal.Parse(row["tiofiniqty"].ToString());
                        }
                        if (row["tiofiniqty"].ToString() == "")
                        {
                            num3 = 0M;
                        }
                        else
                        {
                            num3 = decimal.Parse(row["tiofiniqty"].ToString());
                        }
                        ListViewItem item = new ListViewItem(new string[] { "", table.Rows[i]["GDSID"].ToString(), table.Rows[i]["STORQTY"].ToString(), num2.ToString(), num2.ToString(), num3.ToString(), table.Rows[i]["TIOUNIT"].ToString(), table.Rows[i]["TIOSNO"].ToString(), table.Rows[i]["MATCANOVER"].ToString() });
                        this.lsvTasks.Items.Add(item);
                    }
                }
                else
                {
                    MessageShow.Alert("error", "该发料单没有任务明细");
                    this.txtOrderid.Focus();
                    this.txtOrderid.SelectAll();
                    this.lsvTasks.Items.Clear();
                }
            }
        }

        private void btnQueryByDate_Click(object sender, EventArgs e)
        {
            if (new QueryOrderByDate().ShowDialog() == DialogResult.OK)
            {
                this.txtOrderid.Text = Order.GlobalVals.strOrder;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.lsvTasks.Items.Count != 0)
            {
                this.t = new CheckTask(this.txtOrderid.Text);
                string str = string.Empty;
                for (int i = 0; i < this.lsvTasks.Items.Count; i++)
                {
                    if (this.lsvTasks.Items[i].Checked)
                    {
                        this.arrindex.Add(i);
                        CheckInstruction ins = new CheckInstruction(this.lsvTasks.Items[i].SubItems[7].Text.ToString(), decimal.Parse(this.lsvTasks.Items[i].SubItems[3].Text.ToString()));
                        ins.GoodsID = this.lsvTasks.Items[i].SubItems[1].Text.ToString();
                        ins.Finishqty = decimal.Parse(this.lsvTasks.Items[i].SubItems[5].Text.ToString());
                        this.t.AddIns(ins);
                    }
                }
                if (this.arrindex.Count != 0)
                {
                    if (this.AddTotalDetail())
                    {
                        try
                        {
                            Global.MiddleService.SubmitOutRecord(this.txtOrderid.Text, Global.CurrUser.ToString(), this.t, ref str);
                        }
                        catch (ApplicationException exception)
                        {
                            Cursor.Current = Cursors.Default;
                            MessageShow.Alert("Error", exception.Message);
                            this.arrindex.Clear();
                            this.btnQuery_Click(sender, e);
                            return;
                        }
                        catch (Exception exception2)
                        {
                            Cursor.Current = Cursors.Default;
                            MessageShow.Alert("UnKnowError", exception2.Message);
                            this.arrindex.Clear();
                            this.btnQuery_Click(sender, e);
                            return;
                        }
                        Cursor.Current = Cursors.Default;
                        MessageShow.Alert("提示", str);
                        this.arrindex.Clear();
                        this.btnQuery_Click(sender, e);
                    }
                    this.arrindex.Clear();
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
                    MessageShow.Alert("error", "请输入实际发料数");
                }
                else if (this.CheckQuantity(this.txtQty.Text))
                {
                    if ((this.lsvTasks.Items[this.lsvTasks.SelectedIndices[0]].SubItems[8].Text.ToString() == "0") && (decimal.Parse(this.txtQty.Text) > decimal.Parse(this.lsvTasks.Items[this.lsvTasks.SelectedIndices[0]].SubItems[4].Text)))
                    {
                        MessageShow.Alert("error", "发料数不能大于任务数");
                        this.txtQty.Focus();
                        this.txtQty.SelectAll();
                    }
                    else if (decimal.Parse(this.txtQty.Text) > decimal.Parse(this.lsvTasks.Items[this.lsvTasks.SelectedIndices[0]].SubItems[2].Text))
                    {
                        MessageShow.Alert("error", "发料数不能库存数");
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

        private void CheckOutByOrderFrm_Activated(object sender, EventArgs e)
        {
            OEM_Catchwell.SetBeamHold(false);
            this.txtOrderid.Focus();
            this.txtOrderid.SelectAll();
        }

        private void CheckOutByOrderFrm_Deactivate(object sender, EventArgs e)
        {
            OEM_Catchwell.SetBeamHold(true);
        }

        private void CheckOutByOrderFrm_KeyDown(object sender, KeyEventArgs e)
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

        private void CheckOutByOrderFrm_Load(object sender, EventArgs e)
        {
            this.txtOrderid.Focus();
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
            this.lsvTasks = new ListView();
            this.btnExit = new Button();
            this.btnSubmit = new Button();
            this.btnQuery = new Button();
            this.txtOrderid = new TextBox();
            this.label1 = new Label();
            this.btnUpdateQty = new Button();
            this.txtQty = new TextBox();
            this.label2 = new Label();
            this.btnCheck = new Button();
            this.lblSingleID = new HeadLabel();
            this.TasksTitile = new HeadLabel();
            this.btnQueryByDate = new Button();
            base.SuspendLayout();
            this.lsvTasks.CheckBoxes = true;
            this.lsvTasks.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.lsvTasks.FullRowSelect = true;
            this.lsvTasks.Location = new Point(3, 0x70);
            this.lsvTasks.Name = "lsvTasks";
            this.lsvTasks.Size = new Size(0xed, 0xaf);
            this.lsvTasks.TabIndex = 0x44;
            this.lsvTasks.View = View.Details;
            this.btnExit.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnExit.Location = new Point(0xab, 0x52);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new Size(0x3f, 0x18);
            this.btnExit.TabIndex = 0x43;
            this.btnExit.Text = "返回";
            this.btnExit.Click += new EventHandler(this.btnExit_Click);
            this.btnSubmit.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnSubmit.Location = new Point(0x55, 0x52);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new Size(0x3f, 0x18);
            this.btnSubmit.TabIndex = 0x42;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.Click += new EventHandler(this.btnSubmit_Click);
            this.btnQuery.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnQuery.Location = new Point(5, 0x52);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new Size(0x3f, 0x18);
            this.btnQuery.TabIndex = 0x41;
            this.btnQuery.Text = "检索";
            this.btnQuery.Click += new EventHandler(this.btnQuery_Click);
            this.txtOrderid.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.txtOrderid.Location = new Point(0x41, 0x1d);
            this.txtOrderid.Name = "txtOrderid";
            this.txtOrderid.Size = new Size(100, 0x15);
            this.txtOrderid.TabIndex = 0x40;
            this.txtOrderid.GotFocus += new EventHandler(this.txtOrderid_GotFocus);
            this.txtOrderid.KeyDown += new KeyEventHandler(this.txtOrderid_KeyDown);
            this.label1.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label1.Location = new Point(3, 0x1f);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x41, 20);
            this.label1.Text = "生产订单:";
            this.btnUpdateQty.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnUpdateQty.Location = new Point(0xab, 0x35);
            this.btnUpdateQty.Name = "btnUpdateQty";
            this.btnUpdateQty.Size = new Size(0x3d, 0x17);
            this.btnUpdateQty.TabIndex = 0x48;
            this.btnUpdateQty.Text = "确定";
            this.btnUpdateQty.Click += new EventHandler(this.btnUpdateQty_Click);
            this.txtQty.Location = new Point(0x41, 0x35);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new Size(100, 0x17);
            this.txtQty.TabIndex = 0x47;
            this.txtQty.KeyDown += new KeyEventHandler(this.txtQty_KeyDown);
            this.label2.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label2.Location = new Point(15, 0x38);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 20);
            this.label2.Text = "发料数:";
            this.btnCheck.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnCheck.Location = new Point(5, 0x70);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new Size(0x19, 0x12);
            this.btnCheck.TabIndex = 0x63;
            this.btnCheck.Text = "选";
            this.btnCheck.Click += new EventHandler(this.btnCheck_Click);
            this.lblSingleID.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.lblSingleID.Location = new Point(150, 3);
            this.lblSingleID.Name = "lblSingleID";
            this.lblSingleID.Size = new Size(0x4c, 10);
            this.lblSingleID.TabIndex = 0x60;
            this.lblSingleID.Visible = false;
            this.TasksTitile.BackColor = SystemColors.HotTrack;
            this.TasksTitile.Font = new Font("Arial", 14f, FontStyle.Regular);
            this.TasksTitile.ForeColor = SystemColors.ActiveCaptionText;
            this.TasksTitile.Location = new Point(0, 0);
            this.TasksTitile.Name = "TasksTitile";
            this.TasksTitile.Size = new Size(0xed, 0x18);
            this.TasksTitile.TabIndex = 0x31;
            this.TasksTitile.Text = "按订单发料";
            this.btnQueryByDate.Font = new Font("Tahoma", 11f, FontStyle.Regular);
            this.btnQueryByDate.Location = new Point(0xac, 0x1d);
            this.btnQueryByDate.Name = "btnQueryByDate";
            this.btnQueryByDate.Size = new Size(40, 20);
            this.btnQueryByDate.TabIndex = 0x66;
            this.btnQueryByDate.Text = "…";
            this.btnQueryByDate.Click += new EventHandler(this.btnQueryByDate_Click);
            base.AutoScaleDimensions = new SizeF(96f, 96f);
            //base.AutoScaleMode = AutoScaleMode.Dpi;
            this.AutoScroll = true;
            base.ClientSize = new Size(240, 290);
            base.ControlBox = false;
            base.Controls.Add(this.btnQueryByDate);
            base.Controls.Add(this.btnCheck);
            base.Controls.Add(this.lblSingleID);
            base.Controls.Add(this.btnUpdateQty);
            base.Controls.Add(this.txtQty);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.lsvTasks);
            base.Controls.Add(this.btnExit);
            base.Controls.Add(this.btnSubmit);
            base.Controls.Add(this.btnQuery);
            base.Controls.Add(this.txtOrderid);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.TasksTitile);
            //base.FormBorderStyle = FormBorderStyle.None;
            base.KeyPreview = true;
            base.Name = "CheckOutByOrderFrm";
            this.Text = "按订单发料";
            base.Deactivate += new EventHandler(this.CheckOutByOrderFrm_Deactivate);
            base.Load += new EventHandler(this.CheckOutByOrderFrm_Load);
            base.Activated += new EventHandler(this.CheckOutByOrderFrm_Activated);
            base.KeyDown += new KeyEventHandler(this.CheckOutByOrderFrm_KeyDown);
            base.ResumeLayout(false);
        }

        private void InitializeLsvDetail()
        {
            this.lsvTasks.Columns.Clear();
            this.lsvTasks.Columns.Add(" ", 0x1b, HorizontalAlignment.Center);
            this.lsvTasks.Columns.Add("物料", 100, HorizontalAlignment.Center);
            this.lsvTasks.Columns.Add("库存", 50, HorizontalAlignment.Center);
            this.lsvTasks.Columns.Add("采集数", 50, HorizontalAlignment.Center);
            this.lsvTasks.Columns.Add("任务数", 50, HorizontalAlignment.Center);
            this.lsvTasks.Columns.Add("已发料", 50, HorizontalAlignment.Center);
            this.lsvTasks.Columns.Add("单位", 40, HorizontalAlignment.Center);
            this.lsvTasks.Columns.Add("操作号", 0, HorizontalAlignment.Center);
            this.lsvTasks.Columns.Add("是否超发", 0, HorizontalAlignment.Center);
        }

        private void txtOrderid_GotFocus(object sender, EventArgs e)
        {
            OEM_Catchwell.SetOpMode(0);
        }

        private void txtOrderid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                this.btnQuery_Click(null, null);
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

