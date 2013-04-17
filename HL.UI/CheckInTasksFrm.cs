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

    public class CheckInTasksFrm : Form
    {
        private Button btnBack;
        private Button btnCollect;
        private IContainer components;
        private Label label1;
        private Label label2;
        private Label lblOrderNum;
        private ListView lsvTasks;
        private string path = "";
        private DialogResult ree;
        private const int RET_KEYSTROKE = 0;
        private MiddleService service = Global.MiddleService;
        private DataSet Task = new DataSet();
        private HeadLabel TasksTitile;
        private TextBox txtOrderNum;

        public CheckInTasksFrm(DataSet task, EnumTaskType tasktype)
        {
            this.InitializeComponent();
            this.Task = task;
            Global.CurrTaskType = tasktype;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnCollect_Click(object sender, EventArgs e)
        {
            string item = "";
            string supplyname = "";
            string supplyid = "";
            string date = "";
            string proofid = "";
            DialogResult none = DialogResult.None;
            if (this.txtOrderNum.Text != "")
            {
                if (this.lsvTasks.Items.Count == 0)
                {
                    return;
                }
                item = this.txtOrderNum.Text.Trim();
                if ((Global.CurrTaskType == EnumTaskType.CheckOut) && (item.Length < 12))
                {
                    item = item.PadLeft(12, '0');
                }
                ArrayList list = new ArrayList();
                for (int i = 0; i < this.lsvTasks.Items.Count; i++)
                {
                    list.Add(this.lsvTasks.Items[i].SubItems[0].Text);
                }
                if (!list.Contains(item))
                {
                    MessageShow.Alert("error", "该订单没有任务明细");
                    this.txtOrderNum.Focus();
                    this.txtOrderNum.SelectAll();
                    return;
                }
                for (int j = 0; j < this.lsvTasks.Items.Count; j++)
                {
                    if (item == this.lsvTasks.Items[j].SubItems[0].Text)
                    {
                        if (Global.CurrTaskType == EnumTaskType.CheckIn)
                        {
                            supplyid = this.lsvTasks.Items[j].SubItems[2].Text;
                            date = Convert.ToDateTime(this.lsvTasks.Items[j].SubItems[1].Text).ToString("yyyyMMdd");
                            proofid = this.lsvTasks.Items[j].SubItems[3].Text;
                        }
                        else
                        {
                            proofid = this.lsvTasks.Items[j].SubItems[3].Text;
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
                item = this.lsvTasks.Items[this.lsvTasks.SelectedIndices[0]].SubItems[0].Text;
                if (Global.CurrTaskType == EnumTaskType.CheckIn)
                {
                    supplyid = this.lsvTasks.Items[this.lsvTasks.SelectedIndices[0]].SubItems[2].Text;
                    date = Convert.ToDateTime(this.lsvTasks.Items[this.lsvTasks.SelectedIndices[0]].SubItems[1].Text).ToString("yyyyMMdd");
                    proofid = this.lsvTasks.Items[this.lsvTasks.SelectedIndices[0]].SubItems[3].Text;
                }
                else
                {
                    proofid = this.lsvTasks.Items[this.lsvTasks.SelectedIndices[0]].SubItems[3].Text;
                }
            }
            CheckTask task = new CheckTask(item);
            task.TaskType = Global.CurrTaskType;
            DataSet set = new DataSet();
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                switch (task.TaskType)
                {
                    case EnumTaskType.CheckIn:
                        set = this.service.dsGetTaskByID(proofid, EnumTaskType.CheckIn);
                        goto Label_041E;

                    case EnumTaskType.CheckOut:
                        set = this.service.dsGetTaskByID(proofid, EnumTaskType.CheckOut);
                        goto Label_041E;
                }
            }
            catch (ApplicationException exception)
            {
                Cursor.Current = Cursors.Default;
                this.txtOrderNum.Focus();
                this.txtOrderNum.SelectAll();
                MessageShow.Alert("错误", exception.Message);
                return;
            }
            catch (Exception exception2)
            {
                Cursor.Current = Cursors.Default;
                this.txtOrderNum.Focus();
                this.txtOrderNum.SelectAll();
                MessageShow.Alert("错误", exception2.Message);
                return;
            }
        Label_041E:
            Cursor.Current = Cursors.Default;
            if (set.Tables.Count >= 1)
            {
                DataTable table = set.Tables[0];
                if (set.Tables[0].Rows.Count == 0)
                {
                    MessageShow.Alert("error", "该订单没有任务明细");
                    this.txtOrderNum.Focus();
                    this.txtOrderNum.SelectAll();
                }
                else
                {
                    if (Global.CurrTaskType == EnumTaskType.CheckIn)
                    {
                        supplyname = table.Rows[0]["CLN_NAME"].ToString();
                    }
                    for (int k = 0; k < table.Rows.Count; k++)
                    {
                        decimal num4;
                        decimal num5;
                        DataRow row = table.Rows[k];
                        bool flag = false;
                        int num6 = 0;
                        if (row["tiofiniqty"].ToString() == "")
                        {
                            num4 = decimal.Parse(row["TioTaskQty"].ToString()) - 0M;
                        }
                        else
                        {
                            num4 = decimal.Parse(row["TioTaskQty"].ToString()) - decimal.Parse(row["tiofiniqty"].ToString());
                        }
                        if (row["tiofiniqty"].ToString() == "")
                        {
                            num5 = 0M;
                        }
                        else
                        {
                            num5 = decimal.Parse(row["tiofiniqty"].ToString());
                        }
                        if (Global.CurrTaskType == EnumTaskType.CheckOut)
                        {
                            if (row["MATCANOVER"].ToString() == "")
                            {
                                num6 = 0;
                            }
                            else
                            {
                                num6 = Convert.ToInt32(row["MATCANOVER"].ToString());
                            }
                            switch (num6)
                            {
                                case 0:
                                    flag = false;
                                    break;

                                case 1:
                                    flag = true;
                                    goto Label_05F4;
                            }
                        }
                    Label_05F4:
                        switch (task.TaskType)
                        {
                            case EnumTaskType.CheckIn:
                            {
                                CheckInstruction ins = new CheckInstruction(row["TIOSNO"].ToString(), num4);
                                ins.GoodsID = row["GDSID"].ToString();
                                ins.Unit = row["TIOUNIT"].ToString();
                                ins.Goodsname = row["MATNAME"].ToString();
                                ins.Finishqty = num5;
                                ins.Rowno = row["TIOPOLID"].ToString();
                                ins.Tiosno = row["TIOSNO"].ToString();
                                task.AddIns(ins);
                                break;
                            }
                            case EnumTaskType.CheckOut:
                            {
                                CheckOutInstruction instruction2 = new CheckOutInstruction(row["TIOSNO"].ToString(), num4);
                                instruction2.IsOver = flag;
                                instruction2.GoodsID = row["GDSID"].ToString();
                                instruction2.Unit = row["TIOUNIT"].ToString();
                                instruction2.Goodsname = row["MATNAME"].ToString();
                                instruction2.Finishqty = num5;
                                instruction2.Rowno = row["TIOPOLID"].ToString();
                                instruction2.Tiosno = row["TIOSNO"].ToString();
                                task.AddIns(instruction2);
                                break;
                            }
                            default:
                                throw new ApplicationException("未知的任务类型");
                        }
                    }
                    Global.CurrTask = task;
                    if (Global.BarcodeType == EnumBarcodeType.Separate)
                    {
                        if (task.TaskType == EnumTaskType.CheckIn)
                        {
                            none = new MultiCheckInFrm(item, proofid, supplyid, supplyname, date).ShowDialog();
                        }
                        else if (task.TaskType == EnumTaskType.CheckOut)
                        {
                            none = new MultiCheckOutFrm(item).ShowDialog();
                        }
                    }
                    if (none == DialogResult.OK)
                    {
                        base.Close();
                    }
                    Global.CurrTask = null;
                }
            }
            else
            {
                MessageShow.Alert("error", "该订单没有任务明细");
                this.txtOrderNum.Focus();
                this.txtOrderNum.SelectAll();
            }
        }

        private void CheckInTasksFrm_Activated(object sender, EventArgs e)
        {
            OEM_Catchwell.SetBeamHold(false);
            this.txtOrderNum.Focus();
            this.txtOrderNum.SelectAll();
        }

        private void CheckInTasksFrm_Deactivate(object sender, EventArgs e)
        {
            OEM_Catchwell.SetBeamHold(true);
        }

        private void CheckInTasksFrm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 0x84)
            {
                this.ree = new ConfigFrm().ShowDialog();
            }
            if (this.ree == DialogResult.OK)
            {
                base.Close();
                if (Global.CurrTaskType == EnumTaskType.CheckIn)
                {
                    new CheckInMain().btnCheckIn_Click(null, null);
                }
                else
                {
                    new CheckOutMain().btnCheckOut_Click(null, null);
                }
            }
        }

        private void CheckInTasksFrm_Load(object sender, EventArgs e)
        {
            this.txtOrderNum.Focus();
            this.label1.Text = this.RenameTag1();
            this.label2.Text = this.RenameTag2();
            this.InitializeLsvDetail();
            this.InsertListview();
        }

        private void ClearTxtorder()
        {
            if ((this.lsvTasks.SelectedIndices != null) || (this.lsvTasks.SelectedIndices.Count > 0))
            {
                this.txtOrderNum.Text = "";
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
            this.btnCollect = new Button();
            this.btnBack = new Button();
            this.lsvTasks = new ListView();
            this.label1 = new Label();
            this.txtOrderNum = new TextBox();
            this.label2 = new Label();
            this.lblOrderNum = new Label();
            this.TasksTitile = new HeadLabel();
            base.SuspendLayout();
            this.btnCollect.Location = new Point(30, 130);
            this.btnCollect.Name = "btnCollect";
            this.btnCollect.Size = new Size(0x48, 0x23);
            this.btnCollect.TabIndex = 3;
            this.btnCollect.Text = "采集";
            this.btnCollect.Click += new EventHandler(this.btnCollect_Click);
            this.btnBack.Location = new Point(0x87, 130);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new Size(0x48, 0x23);
            this.btnBack.TabIndex = 3;
            this.btnBack.Text = "返回";
            this.btnBack.Click += new EventHandler(this.btnBack_Click);
            this.lsvTasks.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.lsvTasks.FullRowSelect = true;
            this.lsvTasks.Location = new Point(0, 180);
            this.lsvTasks.Name = "lsvTasks";
            this.lsvTasks.Size = new Size(0xed, 0x6a);
            this.lsvTasks.TabIndex = 8;
            this.lsvTasks.View = View.Details;
            this.label1.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label1.Location = new Point(0x12, 40);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x83, 20);
            this.label1.Text = "请扫描采购订单号:";
            this.txtOrderNum.Location = new Point(0x12, 0x40);
            this.txtOrderNum.Name = "txtOrderNum";
            this.txtOrderNum.Size = new Size(0xbd, 0x17);
            this.txtOrderNum.TabIndex = 10;
            this.txtOrderNum.GotFocus += new EventHandler(this.txtOrderNum_GotFocus);
            this.txtOrderNum.KeyDown += new KeyEventHandler(this.txtOrderNum_KeyDown);
            this.label2.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label2.Location = new Point(0x12, 0x6b);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x60, 20);
            this.label2.Text = "采购订单号：";
            this.lblOrderNum.Location = new Point(0x5e, 0x6b);
            this.lblOrderNum.Name = "lblOrderNum";
            this.lblOrderNum.Size = new Size(0x71, 20);
            this.TasksTitile.BackColor = SystemColors.HotTrack;
            this.TasksTitile.Font = new Font("Arial", 14f, FontStyle.Regular);
            this.TasksTitile.ForeColor = SystemColors.ActiveCaptionText;
            this.TasksTitile.Location = new Point(0, 0);
            this.TasksTitile.Name = "TasksTitile";
            this.TasksTitile.Size = new Size(240, 0x18);
            this.TasksTitile.TabIndex = 5;
            this.TasksTitile.Text = "任务列表";
            base.AutoScaleDimensions = new SizeF(96f, 96f);
            //base.AutoScaleMode = AutoScaleMode.Dpi;
            this.AutoScroll = true;
            base.ClientSize = new Size(240, 290);
            base.ControlBox = false;
            base.Controls.Add(this.lblOrderNum);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.txtOrderNum);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.lsvTasks);
            base.Controls.Add(this.TasksTitile);
            base.Controls.Add(this.btnBack);
            base.Controls.Add(this.btnCollect);
            //base.FormBorderStyle = FormBorderStyle.None;
            base.KeyPreview = true;
            base.Name = "CheckInTasksFrm";
            this.Text = "CheckInTasksFrm";
            base.Deactivate += new EventHandler(this.CheckInTasksFrm_Deactivate);
            base.Load += new EventHandler(this.CheckInTasksFrm_Load);
            base.Activated += new EventHandler(this.CheckInTasksFrm_Activated);
            base.KeyDown += new KeyEventHandler(this.CheckInTasksFrm_KeyDown);
            base.ResumeLayout(false);
        }

        private void InitializeLsvDetail()
        {
            this.lsvTasks.Columns.Clear();
            if (Global.CurrTaskType == EnumTaskType.CheckIn)
            {
                this.lsvTasks.Columns.Add("采购订单号", 90, HorizontalAlignment.Center);
                this.lsvTasks.Columns.Add("日期", 100, HorizontalAlignment.Center);
                this.lsvTasks.Columns.Add("供应商", 90, HorizontalAlignment.Center);
                this.lsvTasks.Columns.Add("凭证号", 0, HorizontalAlignment.Center);
            }
            else if (Global.CurrTaskType == EnumTaskType.CheckOut)
            {
                this.lsvTasks.Columns.Add("订单号", 110, HorizontalAlignment.Center);
                this.lsvTasks.Columns.Add("需求日期", 100, HorizontalAlignment.Center);
                this.lsvTasks.Columns.Add("合并料单", 70, HorizontalAlignment.Center);
                this.lsvTasks.Columns.Add("凭证号", 0, HorizontalAlignment.Center);
            }
        }

        private void InsertListview()
        {
            this.lsvTasks.Items.Clear();
            DataTable table = this.Task.Tables[0];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (Global.CurrTaskType == EnumTaskType.CheckIn)
                {
                    ListViewItem item = new ListViewItem(new string[] { table.Rows[i]["PROPOID"].ToString(), table.Rows[i]["Prodate"].ToString(), table.Rows[i]["Proclients"].ToString(), table.Rows[i]["PROID"].ToString() });
                    this.lsvTasks.Items.Add(item);
                }
                else if (Global.CurrTaskType == EnumTaskType.CheckOut)
                {
                    ListViewItem item2 = new ListViewItem(new string[] { table.Rows[i]["PROPOID"].ToString(), table.Rows[i]["PROASKDATE"].ToString(), table.Rows[i]["PROISUNION"].ToString(), table.Rows[i]["PROID"].ToString() });
                    this.lsvTasks.Items.Add(item2);
                }
            }
        }

        private string RenameTag1()
        {
            switch (Global.CurrTaskType)
            {
                case EnumTaskType.CheckIn:
                    return "请扫描采购订单号：";

                case EnumTaskType.CheckOut:
                    return "请扫描发料单号";
            }
            return "";
        }

        private string RenameTag2()
        {
            switch (Global.CurrTaskType)
            {
                case EnumTaskType.CheckIn:
                    return "采购订单号";

                case EnumTaskType.CheckOut:
                    return "发料单号";
            }
            return "";
        }

        private void txtOrderNum_GotFocus(object sender, EventArgs e)
        {
            OEM_Catchwell.SetOpMode(0);
        }

        private void txtOrderNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (((e.KeyCode == Keys.Return) && (this.txtOrderNum.Text != "")) && (this.lsvTasks.Items.Count != 0))
            {
                this.lblOrderNum.Text = this.txtOrderNum.Text;
                this.btnCollect_Click(null, null);
            }
        }
    }
}

