namespace HL.UI
{
    using BizLayer;
    using Entity;
    using Printlib;
    using SharpExCS;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using System.Threading;
    using System.Windows.Forms;
    using HL.Framework;
    using HL.Controls;

    public class MultiCheckInFrm : Form
    {
        private int a;
        private Button btnBack;
        private Button btnChangeLocation;
        private Button btnDetail;
        private Button btnSubmit;
        private Button btnTotalSubmit;
        private bool busy;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private IContainer components;
        private Step currStep = Step.GoodsID;
        private string Date = string.Empty;
        private CheckInColDetail detail;
        private ColumnHeader headerFreightNumber;
        private ColumnHeader headerRequired;
        private ColumnHeader headerTaskNumber;
        private int index = -1;
        private Label label1;
        private Label label2;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private HeadLabel lblBatchID;
        private Label Lblcount;
        private HeadLabel lblGoodsID;
        private Label LblGoodsname;
        private HeadLabel lblLocation;
        private Label lblMsg;
        private Label lblProduceNo;
        private HeadLabel lblQuantity;
        private Label lblRowno;
        private HeadLabel lblSingleID;
        private Label lbltisno;
        private Label Lblunit;
        private Label lblWarehouse;
        private ListView lsvTaskDetails;
        private Management management = Management.GetSingleton();
        private string orderID = string.Empty;
        private string proID = string.Empty;
        private RegulationCheck regulation = RegulationCheck.GetInstance();
        private const int RET_KEYSTROKE = 0;
        private string strresult = string.Empty;
        private string supplyID = string.Empty;
        private string supplyName = string.Empty;
        private HeadLabel TasksTitile;
        private TextBox tbxBarcode;
        private CheckInstruction tempInstruction;
        private string userName = string.Empty;

        public MultiCheckInFrm(string orderid, string proid, string supplyid, string supplyname, string date)
        {
            this.InitializeComponent();
            this.orderID = orderid;
            this.supplyName = supplyname;
            this.supplyID = supplyid;
            this.Date = date;
            this.proID = proid;
        }

        private bool AddNewDetail(decimal quantity)
        {
            this.detail = new CheckInColDetail();
            this.detail.AssociateInstruction = this.tempInstruction;
            this.detail.BatchNumber = this.lblBatchID.Text;
            if (this.management.NeedProduceBatchNo)
            {
                this.detail.ProduceBatchNo = this.lblProduceNo.Text;
            }
            else
            {
                this.detail.ProduceBatchNo = "";
            }
            this.detail.Location = this.lblLocation.Text;
            this.detail.SingletonNo = this.lblSingleID.Text;
            this.detail.GoodsID = this.lblGoodsID.Text;
            this.detail.CollectedQuantity = quantity;
            this.detail.CollectedTime = DateTime.Now;
            this.detail.Unit = this.Lblunit.Text;
            this.detail.Rowno = this.lblRowno.Text;
            this.detail.Tiosno = this.lbltisno.Text;
            try
            {
                this.tempInstruction.Result.AddColDetail(this.detail);
            }
            catch (ApplicationException exception)
            {
                MessageShow.Alert("Error", exception.Message);
                this.tbxBarcode.Text = "";
                return false;
            }
            this.a++;
            this.Lblcount.Text = this.a.ToString() + "笔";
            return true;
        }

        private bool AddTotalDetail()
        {
            ArrayList keys = Global.CurrTask.InsList.Keys as ArrayList;
            for (int i = 0; i < keys.Count; i++)
            {
                CheckInstruction instruction = Global.CurrTask.InsList[keys[i]] as CheckInstruction;
                this.tempInstruction = instruction;
                this.detail = new CheckInColDetail();
                this.detail.AssociateInstruction = this.tempInstruction;
                this.detail.BatchNumber = this.supplyID + this.Date;
                this.detail.SingletonNo = this.lblSingleID.Text;
                this.detail.Location = this.lblLocation.Text;
                this.detail.GoodsID = this.lsvTaskDetails.Items[i].SubItems[2].Text;
                this.detail.Unit = this.lsvTaskDetails.Items[i].SubItems[5].Text;
                this.detail.Rowno = this.lsvTaskDetails.Items[i].SubItems[7].Text;
                this.detail.CollectedQuantity = decimal.Parse(this.lsvTaskDetails.Items[i].SubItems[3].Text.Split(new char[] { '/' })[1]) - decimal.Parse(this.lsvTaskDetails.Items[i].SubItems[3].Text.Split(new char[] { '/' })[0]);
                this.detail.CollectedTime = DateTime.Now;
                this.detail.Tiosno = this.lbltisno.Text;
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

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (!this.busy && (this.CheckResultIsEmpty() || (MessageBox.Show("已采集的数据还未提交，确定要退出吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) != DialogResult.No)))
            {
                base.Close();
            }
        }

        private void btnChangeLocation_Click(object sender, EventArgs e)
        {
            if (!this.busy && (MessageBox.Show("你确定要复位吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK))
            {
                this.ClearAllControls();
                this.currStep = Step.GoodsID;
                this.tbxBarcode.Text = "";
                this.tbxBarcode.Focus();
                this.lblMsg.Text = "请扫描物料条码";
            }
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            if (!this.busy)
            {
                new CollectionDetailFrm().ShowDialog();
                this.UpdateListView();
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string str = string.Empty;
            DataSet listPda = new DataSet();
            if (!this.busy)
            {
                if (this.CheckResultIsEmpty())
                {
                    MessageShow.Alert("Warning", "该任务还没有任何采集数据，无法提交！");
                }
                else if (!this.isOpenPort())
                {
                    if (MessageBox.Show("打开蓝牙打印机失败，现在要连接打印机吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        new ConfigMain().btnConfigPrinter_Click(null, null);
                    }
                }
                else
                {
                    this.busy = true;
                    Cursor.Current = Cursors.WaitCursor;
                    WaitingFrm frm = new WaitingFrm();
                    frm.Show("提交", "正在提交采集数据，请稍候...");
                    try
                    {
                        this.strresult = Global.MiddleService.SubmitInRecord(this.orderID, this.proID, this.supplyID, Management.GetSingleton().WarehouseNo, Global.CurrUser.ToString(), Global.CurrTask, ref listPda, ref str);
                        if (this.strresult != "")
                        {
                            this.print(EnumSubmitType.PartSubmit, listPda);
                        }
                    }
                    catch (ApplicationException exception)
                    {
                        Cursor.Current = Cursors.Default;
                        frm.Close();
                        MessageShow.Alert("Error", exception.Message);
                        this.busy = false;
                        return;
                    }
                    catch (Exception exception2)
                    {
                        Cursor.Current = Cursors.Default;
                        frm.Close();
                        MessageShow.Alert("UnKnowError", exception2.Message);
                        this.busy = false;
                        return;
                    }
                    Cursor.Current = Cursors.Default;
                    frm.Close();
                    MessageShow.Alert("提示", str);
                    this.busy = false;
                    base.Close();
                    base.DialogResult = DialogResult.OK;
                    new CheckInMain().btnCheckIn_Click(null, null);
                }
            }
        }

        private void btnTotalSubmit_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("确定要提交整个采购订单吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.No) && this.AddTotalDetail())
            {
                if (!this.isOpenPort())
                {
                    if (MessageBox.Show("打开蓝牙打印机失败，现在要连接打印机吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        new ConfigMain().btnConfigPrinter_Click(null, null);
                    }
                }
                else
                {
                    this.submit();
                }
            }
        }

        private int CheckBatchID(string barcode)
        {
            if (barcode.Substring(0, 1) == "$")
            {
                return -1;
            }
            return 1;
        }

        private int CheckGoodsID(string barcode)
        {
            this.index = -1;
            bool flag = false;
            ArrayList keys = Global.CurrTask.InsList.Keys as ArrayList;
            for (int i = 0; i < keys.Count; i++)
            {
                this.index++;
                CheckInstruction instruction = Global.CurrTask.InsList[keys[i]] as CheckInstruction;
                if (instruction.GoodsID == barcode.Substring(1))
                {
                    flag = true;
                    if (instruction.RequiredQuantity != instruction.Result.TotalQuantity)
                    {
                        this.tempInstruction = instruction;
                        return 1;
                    }
                    if (this.index == (Global.CurrTask.InsList.Count - 1))
                    {
                        return 0;
                    }
                }
            }
            if (flag)
            {
                return 0;
            }
            return -1;
        }

        private bool CheckResultIsEmpty()
        {
            for (int i = 0; i < this.lsvTaskDetails.Items.Count; i++)
            {
                if (this.lsvTaskDetails.Items[i].SubItems[3].Text.Split(new char[] { '/' })[0] != "0")
                {
                    return false;
                }
            }
            return true;
        }

        private bool CheckSingleID(string barcode)
        {
            if (barcode.Substring(0, 1) == "$")
            {
                return false;
            }
            return true;
        }

        private bool CheckTaskCompleted()
        {
            for (int i = 0; i < this.lsvTaskDetails.Items.Count; i++)
            {
                string text = this.lsvTaskDetails.Items[i].SubItems[3].Text;
                decimal num2 = decimal.Parse(text.Split(new char[] { '/' })[1]);
                decimal num3 = decimal.Parse(text.Split(new char[] { '/' })[0]);
                if (num2 == num3)
                {
                    return true;
                }
            }
            return false;
        }

        private void ClearAllControls()
        {
            this.lblBatchID.Text = "";
            this.lblGoodsID.Text = "";
            this.lblQuantity.Text = "";
            this.lblSingleID.Text = "";
            this.LblGoodsname.Text = "";
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
            this.tbxBarcode = new TextBox();
            this.btnSubmit = new Button();
            this.btnBack = new Button();
            this.label6 = new Label();
            this.lsvTaskDetails = new ListView();
            this.headerTaskNumber = new ColumnHeader();
            this.columnHeader1 = new ColumnHeader();
            this.headerFreightNumber = new ColumnHeader();
            this.headerRequired = new ColumnHeader();
            this.columnHeader4 = new ColumnHeader();
            this.columnHeader2 = new ColumnHeader();
            this.columnHeader3 = new ColumnHeader();
            this.columnHeader5 = new ColumnHeader();
            this.btnDetail = new Button();
            this.lblMsg = new Label();
            this.btnChangeLocation = new Button();
            this.label2 = new Label();
            this.label1 = new Label();
            this.label5 = new Label();
            this.lblProduceNo = new Label();
            this.lblWarehouse = new Label();
            this.label7 = new Label();
            this.LblGoodsname = new Label();
            this.btnTotalSubmit = new Button();
            this.lblSingleID = new HeadLabel();
            this.lblQuantity = new HeadLabel();
            this.lblBatchID = new HeadLabel();
            this.lblGoodsID = new HeadLabel();
            this.lblLocation = new HeadLabel();
            this.TasksTitile = new HeadLabel();
            this.label8 = new Label();
            this.Lblcount = new Label();
            this.Lblunit = new Label();
            this.lblRowno = new Label();
            this.columnHeader6 = new ColumnHeader();
            this.lbltisno = new Label();
            base.SuspendLayout();
            this.tbxBarcode.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Regular);
            this.tbxBarcode.Location = new Point(0x84, 0x1f);
            this.tbxBarcode.Name = "tbxBarcode";
            this.tbxBarcode.Size = new Size(0x6c, 0x15);
            this.tbxBarcode.TabIndex = 0;
            this.tbxBarcode.GotFocus += new EventHandler(this.tbxBarcode_GotFocus);
            this.tbxBarcode.KeyDown += new KeyEventHandler(this.tbxBarcode_KeyDown);
            this.btnSubmit.BackColor = Color.FromArgb(0xc0, 0xc0, 0xc0);
            this.btnSubmit.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Regular);
            this.btnSubmit.ForeColor = Color.Black;
            this.btnSubmit.Location = new Point(0x52, 0x7b);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new Size(0x36, 0x17);
            this.btnSubmit.TabIndex = 7;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.Click += new EventHandler(this.btnSubmit_Click);
            this.btnBack.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Regular);
            this.btnBack.Location = new Point(0x92, 0x7b);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new Size(0x36, 0x17);
            this.btnBack.TabIndex = 8;
            this.btnBack.Text = "返回";
            this.btnBack.Click += new EventHandler(this.btnBack_Click);
            this.label6.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Regular);
            this.label6.Location = new Point(0, 0x95);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x4a, 0x10);
            this.label6.Text = "任务明细：";
            this.lsvTaskDetails.Columns.Add(this.headerTaskNumber);
            this.lsvTaskDetails.Columns.Add(this.columnHeader1);
            this.lsvTaskDetails.Columns.Add(this.headerFreightNumber);
            this.lsvTaskDetails.Columns.Add(this.headerRequired);
            this.lsvTaskDetails.Columns.Add(this.columnHeader4);
            this.lsvTaskDetails.Columns.Add(this.columnHeader2);
            this.lsvTaskDetails.Columns.Add(this.columnHeader3);
            this.lsvTaskDetails.Columns.Add(this.columnHeader5);
            this.lsvTaskDetails.Columns.Add(this.columnHeader6);
            this.lsvTaskDetails.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Regular);
            this.lsvTaskDetails.FullRowSelect = true;
            this.lsvTaskDetails.Location = new Point(3, 0xa8);
            this.lsvTaskDetails.Name = "lsvTaskDetails";
            this.lsvTaskDetails.Size = new Size(0xea, 0x79);
            this.lsvTaskDetails.TabIndex = 9;
            this.lsvTaskDetails.View = View.Details;
            this.lsvTaskDetails.KeyDown += new KeyEventHandler(this.lsvTaskDetails_KeyDown);
            this.headerTaskNumber.Text = "任务明细号";
            this.headerTaskNumber.Width = 0;
            this.columnHeader1.Text = "库位";
            this.columnHeader1.Width = 0;
            this.headerFreightNumber.Text = "物料";
            this.headerFreightNumber.Width = 110;
            this.headerRequired.Text = "数量";
            this.headerRequired.Width = 70;
            this.columnHeader4.Text = "已收";
            this.columnHeader4.Width = 40;
            this.columnHeader2.Text = "单位";
            this.columnHeader2.Width = 40;
            this.columnHeader3.Text = "物料名称";
            this.columnHeader3.Width = 0;
            this.columnHeader5.Text = "行号";
            this.columnHeader5.Width = 0;
            this.btnDetail.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Regular);
            this.btnDetail.Location = new Point(0x12, 0x7b);
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new Size(0x36, 0x17);
            this.btnDetail.TabIndex = 6;
            this.btnDetail.Text = "明细";
            this.btnDetail.Click += new EventHandler(this.btnDetail_Click);
            this.lblMsg.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Regular);
            this.lblMsg.Location = new Point(0, 0x20);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new Size(0x74, 20);
            this.lblMsg.Text = "请扫描";
            this.btnChangeLocation.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Regular);
            this.btnChangeLocation.Location = new Point(0xb0, 0x38);
            this.btnChangeLocation.Name = "btnChangeLocation";
            this.btnChangeLocation.Size = new Size(0x3d, 20);
            this.btnChangeLocation.TabIndex = 0x48;
            this.btnChangeLocation.Text = "复位";
            this.btnChangeLocation.Click += new EventHandler(this.btnChangeLocation_Click);
            this.label2.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Regular);
            this.label2.Location = new Point(2, 0x53);
            this.label2.Name = "label2";
            this.label2.Size = new Size(50, 0x11);
            this.label2.Text = "物料：";
            this.label1.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Regular);
            this.label1.Location = new Point(0, 0x3b);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x48, 0x11);
            this.label1.Text = "当前库房：";
            this.label5.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Regular);
            this.label5.Location = new Point(0xa5, 0x55);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x22, 0x12);
            this.label5.Text = "数量：";
            this.lblProduceNo.Location = new Point(0x4e, 0x95);
            this.lblProduceNo.Name = "lblProduceNo";
            this.lblProduceNo.Size = new Size(0x2e, 0x10);
            this.lblProduceNo.Visible = false;
            this.lblWarehouse.Location = new Point(0x4b, 0x38);
            this.lblWarehouse.Name = "lblWarehouse";
            this.lblWarehouse.Size = new Size(0x40, 20);
            this.label7.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Regular);
            this.label7.Location = new Point(0, 0x68);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x4b, 14);
            this.label7.Text = "物料名称：";
            this.LblGoodsname.Location = new Point(60, 0x68);
            this.LblGoodsname.Name = "LblGoodsname";
            this.LblGoodsname.Size = new Size(0x8a, 0x10);
            this.btnTotalSubmit.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Regular);
            this.btnTotalSubmit.Location = new Point(210, 0x7b);
            this.btnTotalSubmit.Name = "btnTotalSubmit";
            this.btnTotalSubmit.Size = new Size(0x18, 0x17);
            this.btnTotalSubmit.TabIndex = 0x69;
            this.btnTotalSubmit.Text = "批";
            this.btnTotalSubmit.Click += new EventHandler(this.btnTotalSubmit_Click);
            this.lblSingleID.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Regular);
            this.lblSingleID.Location = new Point(0x9a, 0x95);
            this.lblSingleID.Name = "lblSingleID";
            this.lblSingleID.Size = new Size(0x4c, 13);
            this.lblSingleID.TabIndex = 0x61;
            this.lblQuantity.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Regular);
            this.lblQuantity.Location = new Point(0xcd, 0x55);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new Size(0x20, 0x12);
            this.lblQuantity.TabIndex = 0x49;
            this.lblQuantity.DoubleClick += new EventHandler(this.lblQuantity_DoubleClick);
            this.lblBatchID.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Regular);
            this.lblBatchID.Location = new Point(60, 0x95);
            this.lblBatchID.Name = "lblBatchID";
            this.lblBatchID.Size = new Size(0x7c, 12);
            this.lblBatchID.TabIndex = 0x4a;
            this.lblBatchID.Visible = false;
            this.lblBatchID.DoubleClick += new EventHandler(this.lblBatchID_DoubleClick);
            this.lblGoodsID.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Regular);
            this.lblGoodsID.Location = new Point(0x25, 0x54);
            this.lblGoodsID.Name = "lblGoodsID";
            this.lblGoodsID.Size = new Size(0x73, 0x12);
            this.lblGoodsID.TabIndex = 0x4c;
            this.lblGoodsID.DoubleClick += new EventHandler(this.lblGoodsID_DoubleClick);
            this.lblLocation.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Regular);
            this.lblLocation.Location = new Point(160, 60);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new Size(0x4d, 0x10);
            this.lblLocation.TabIndex = 0x4d;
            this.lblLocation.Visible = false;
            this.lblLocation.DoubleClick += new EventHandler(this.lblLocation_DoubleClick);
            this.TasksTitile.BackColor = SystemColors.HotTrack;
            this.TasksTitile.Font = new System.Drawing.Font("Arial", 14f, FontStyle.Regular);
            this.TasksTitile.ForeColor = SystemColors.ActiveCaptionText;
            this.TasksTitile.Location = new Point(0, 1);
            this.TasksTitile.Name = "TasksTitile";
            this.TasksTitile.Size = new Size(240, 0x18);
            this.TasksTitile.TabIndex = 0x39;
            this.TasksTitile.Text = "收货采集";
            this.label8.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Regular);
            this.label8.Location = new Point(0x91, 1);
            this.label8.Name = "label8";
            this.label8.Size = new Size(60, 20);
            this.label8.Text = "已采集：";
            this.Lblcount.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Regular);
            this.Lblcount.ForeColor = Color.Red;
            this.Lblcount.Location = new Point(0xc4, 1);
            this.Lblcount.Name = "Lblcount";
            this.Lblcount.Size = new Size(0x25, 20);
            this.Lblunit.Location = new Point(200, 0x62);
            this.Lblunit.Name = "Lblunit";
            this.Lblunit.Size = new Size(0x21, 20);
            this.Lblunit.Visible = false;
            this.lblRowno.Location = new Point(210, 100);
            this.lblRowno.Name = "lblRowno";
            this.lblRowno.Size = new Size(20, 20);
            this.lblRowno.Visible = false;
            this.columnHeader6.Text = "操作号";
            this.columnHeader6.Width = 0;
            this.lbltisno.Location = new Point(60, 0x95);
            this.lbltisno.Name = "lbltisno";
            this.lbltisno.Size = new Size(100, 0x10);
            this.lbltisno.Visible = false;
            base.AutoScaleDimensions = new SizeF(96f, 96f);
            //base.AutoScaleMode = AutoScaleMode.Dpi;
            this.AutoScroll = true;
            base.ClientSize = new Size(240, 290);
            base.ControlBox = false;
            base.Controls.Add(this.lbltisno);
            base.Controls.Add(this.lblRowno);
            base.Controls.Add(this.Lblunit);
            base.Controls.Add(this.Lblcount);
            base.Controls.Add(this.label8);
            base.Controls.Add(this.btnTotalSubmit);
            base.Controls.Add(this.LblGoodsname);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.lblWarehouse);
            base.Controls.Add(this.lblSingleID);
            base.Controls.Add(this.lblProduceNo);
            base.Controls.Add(this.btnChangeLocation);
            base.Controls.Add(this.lblQuantity);
            base.Controls.Add(this.lblBatchID);
            base.Controls.Add(this.lblGoodsID);
            base.Controls.Add(this.lblLocation);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.TasksTitile);
            base.Controls.Add(this.lblMsg);
            base.Controls.Add(this.btnDetail);
            base.Controls.Add(this.lsvTaskDetails);
            base.Controls.Add(this.btnBack);
            base.Controls.Add(this.btnSubmit);
            base.Controls.Add(this.tbxBarcode);
            base.Controls.Add(this.label6);
            //base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "MultiCheckInFrm";
            this.Text = "MultiCheckInFrm";
            base.Deactivate += new EventHandler(this.MultiCheckInFrm_Deactivate);
            base.Load += new EventHandler(this.MultiCheckInAndOutFrm_Load);
            base.Activated += new EventHandler(this.MultiCheckInFrm_Activated);
            base.ResumeLayout(false);
        }

        private bool isOpenPort()
        {
            IniFile file = new IniFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName) + "/PrintTest.ini");
            string port = file.ReadValue("Print", "PrintPort", "COM7");
            file.ReadValue("Print", "PrintModel", "MPK1230E");
            string s = file.ReadValue("Print", "PrintBaud", "115200");
            MPK1280 mpk = new MPK1280();
            mpk.portbaudrate = int.Parse(s);
            if (!mpk.OpenPort(port))
            {
                return false;
            }
            return true;
        }

        private void lblBatchID_DoubleClick(object sender, EventArgs e)
        {
            if (this.lblBatchID.Text.Length > 14)
            {
                new DetailFrm(this.lblBatchID.Text).ShowDialog();
            }
        }

        private void lblGoodsID_DoubleClick(object sender, EventArgs e)
        {
            if (this.lblGoodsID.Text.Length > 10)
            {
                new DetailFrm(this.lblGoodsID.Text).ShowDialog();
            }
        }

        private void lblLocation_DoubleClick(object sender, EventArgs e)
        {
            if (this.lblLocation.Text.Length > 13)
            {
                new DetailFrm(this.lblLocation.Text).ShowDialog();
            }
        }

        private void lblQuantity_DoubleClick(object sender, EventArgs e)
        {
            if (this.lblQuantity.Text.Length > 4)
            {
                new DetailFrm(this.lblQuantity.Text).ShowDialog();
            }
        }

        private void lsvTaskDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyValue == 13) && (this.currStep == Step.GoodsID))
            {
                this.ClearAllControls();
                string barcode = "$" + this.lsvTaskDetails.Items[this.lsvTaskDetails.SelectedIndices[0]].SubItems[2].Text;
                string text = this.lsvTaskDetails.Items[this.lsvTaskDetails.SelectedIndices[0]].SubItems[6].Text;
                string str3 = this.lsvTaskDetails.Items[this.lsvTaskDetails.SelectedIndices[0]].SubItems[5].Text;
                string str4 = this.lsvTaskDetails.Items[this.lsvTaskDetails.SelectedIndices[0]].SubItems[7].Text;
                string text1 = this.lsvTaskDetails.Items[this.lsvTaskDetails.SelectedIndices[0]].SubItems[8].Text;
                switch (this.CheckGoodsID(barcode))
                {
                    case 1:
                        this.lblGoodsID.Text = barcode.Substring(1);
                        this.lblBatchID.Text = this.supplyID + this.Date;
                        this.LblGoodsname.Text = text;
                        this.Lblunit.Text = str3;
                        this.lblRowno.Text = str4;
                        this.lblMsg.Text = "请输入产品数量";
                        this.currStep = Step.Quantity;
                        this.tbxBarcode.Text = "";
                        this.tbxBarcode.Focus();
                        return;

                    case 0:
                        MessageShow.Alert("Error", "该物料对应的任务项已完成！");
                        this.tbxBarcode.Focus();
                        this.tbxBarcode.SelectAll();
                        return;

                    case -1:
                        MessageShow.Alert("Error", "任务中不包含指定的物料！");
                        this.tbxBarcode.Focus();
                        this.tbxBarcode.SelectAll();
                        break;
                }
            }
        }

        private void MPK1280Print(string port, string baud, EnumSubmitType submittype, DataSet PrintRecord)
        {
            string str = string.Empty;
            string str2 = string.Empty;
            string str3 = string.Empty;
            string str4 = string.Empty;
            string str5 = string.Empty;
            MPK1280 mpk = new MPK1280();
            mpk.portbaudrate = int.Parse(baud);
            if (!mpk.OpenPort(port))
            {
                MessageBox.Show("打开蓝牙打印机串口失败");
            }
            else
            {
                mpk.PrinterWake();
                mpk.PrinterReset();
                mpk.Feed(100);
                mpk.SetSnapMode(1);
                mpk.FontZoom(2, 1);
                mpk.FontBold(1);
                mpk.PrintStrLine("安徽合力收货小票");
                mpk.FontBold(0);
                mpk.FontZoom(1, 1);
                mpk.SetSnapMode(0);
                mpk.Feed(20);
                mpk.PrintStrLine("凭证:" + this.strresult + "  次数:1");
                mpk.PrintStrLine("采购订单：" + this.orderID);
                mpk.FontZoom(1, 1);
                mpk.SetSnapMode(0);
                mpk.Feed(20);
                mpk.PrintStrLine("供应商：" + this.supplyName);
                mpk.PrintStr("工厂：" + Management.GetSingleton().WarehouseNo.Substring(0, 4) + "  库房：" + Management.GetSingleton().WarehouseNo);
                mpk.PrintStrLine("收货人：" + this.userName);
                mpk.PrintStrLine("日期：" + DateTime.Now.ToString("yyyy-MM-dd"));
                mpk.PrintStr("------------------------");
                new MPK1230().FontCompress23(1);
                if (submittype == EnumSubmitType.PartSubmit)
                {
                    for (int i = 0; i < PrintRecord.Tables[0].Rows.Count; i++)
                    {
                        str = PrintRecord.Tables[0].Rows[i]["FINISHQTY"].ToString();
                        string str6 = PrintRecord.Tables[0].Rows[i]["UNIT"].ToString();
                        str2 = PrintRecord.Tables[0].Rows[i]["MATCODE"].ToString();
                        str3 = PrintRecord.Tables[0].Rows[i]["MATNAME"].ToString();
                        str4 = str2.PadRight(0x12, ' ');
                        if (str3.Length < 0x10)
                        {
                            str5 = str3;
                        }
                        else
                        {
                            str5 = str3.Substring(0, 0x10);
                        }
                        mpk.PrintStrLine(str5);
                        mpk.FontUnderLine(2);
                        mpk.PrintStrLine(str4 + str.ToString() + str6);
                        mpk.FontUnderLine(0);
                    }
                }
                else
                {
                    for (int j = 0; j < PrintRecord.Tables[0].Rows.Count; j++)
                    {
                        str = PrintRecord.Tables[0].Rows[j]["FINISHQTY"].ToString();
                        string str7 = PrintRecord.Tables[0].Rows[j]["UNIT"].ToString();
                        str2 = PrintRecord.Tables[0].Rows[j]["MATCODE"].ToString();
                        str3 = PrintRecord.Tables[0].Rows[j]["MATNAME"].ToString();
                        str4 = str2.PadRight(0x12, ' ');
                        if (str3.Length < 0x10)
                        {
                            str5 = str3;
                        }
                        else
                        {
                            str5 = str3.Substring(0, 0x10);
                        }
                        mpk.PrintStrLine(str5);
                        mpk.FontUnderLine(2);
                        mpk.PrintStrLine(str4 + str.ToString() + str7);
                        mpk.FontUnderLine(0);
                    }
                }
                mpk.SetSnapMode(1);
                mpk.PrintStrLine("------End-----");
                mpk.PrintStrLine("          ");
                mpk.PrintCR();
                mpk.Feed(100);
                Thread.Sleep(0x3e8);
                mpk.ClosePort();
            }
        }

        private void MultiCheckInAndOutFrm_Load(object sender, EventArgs e)
        {
            DataSet set = new DataSet();
            DataTable table = new DataTable();
            Cursor.Current = Cursors.WaitCursor;
            WaitingFrm frm = new WaitingFrm();
            frm.Show("读取数据", "正在读取记录，请稍候...");
            frm.Close();
            Cursor.Current = Cursors.Default;
            this.lblWarehouse.Text = Management.GetSingleton().WarehouseNo;
            this.lblLocation.Text = this.lblWarehouse.Text + "01";
            this.UpdateListView();
            this.lblMsg.Text = this.lblMsg.Text + "物料条码";
            try
            {
                table = Global.MiddleService.GetLoginName(Global.CurrUser.ToString()).Tables[0];
                if (table.Rows.Count != 0)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        this.userName = table.Rows[i]["log_name"].ToString();
                    }
                }
            }
            catch (ApplicationException exception)
            {
                Cursor.Current = Cursors.Default;
                MessageShow.Alert("Error", exception.Message);
            }
            catch (Exception exception2)
            {
                Cursor.Current = Cursors.Default;
                MessageShow.Alert("UnKnowError", exception2.Message);
            }
        }

        private void MultiCheckInFrm_Activated(object sender, EventArgs e)
        {
            OEM_Catchwell.SetBeamHold(false);
            this.tbxBarcode.Focus();
            this.tbxBarcode.SelectAll();
        }

        private void MultiCheckInFrm_Deactivate(object sender, EventArgs e)
        {
            OEM_Catchwell.SetBeamHold(true);
        }

        private void Performing(object sender, string barcode)
        {
            switch (this.currStep)
            {
                case Step.Location:
                    if (!this.management.CheckCommonLocation(barcode))
                    {
                        MessageShow.Alert("Error", "错误的库位条码！");
                        this.tbxBarcode.Focus();
                        this.tbxBarcode.SelectAll();
                        return;
                    }
                    this.lblLocation.Text = barcode;
                    this.tbxBarcode.Text = "";
                    this.tbxBarcode.Focus();
                    this.lblMsg.Text = "请采集货号条码";
                    this.currStep = Step.GoodsID;
                    return;

                case Step.GoodsID:
                {
                    this.ClearAllControls();
                    int num = this.CheckGoodsID(barcode);
                    if (num != 1)
                    {
                        if (num == 0)
                        {
                            MessageShow.Alert("Error", "该物料对应的任务项已完成！");
                            this.tbxBarcode.Focus();
                            this.tbxBarcode.SelectAll();
                            return;
                        }
                        if (num == -1)
                        {
                            MessageShow.Alert("Error", "任务中不包含指定的物料！");
                            this.tbxBarcode.Focus();
                            this.tbxBarcode.SelectAll();
                        }
                        return;
                    }
                    this.lblGoodsID.Text = barcode.Substring(1);
                    for (int i = 0; i < this.lsvTaskDetails.Items.Count; i++)
                    {
                        if (this.lsvTaskDetails.Items[i].SubItems[2].Text == barcode.Substring(1))
                        {
                            this.LblGoodsname.Text = this.lsvTaskDetails.Items[i].SubItems[6].Text;
                            this.Lblunit.Text = this.lsvTaskDetails.Items[i].SubItems[5].Text;
                            this.lblRowno.Text = this.lsvTaskDetails.Items[i].SubItems[7].Text;
                            this.lbltisno.Text = this.lsvTaskDetails.Items[i].SubItems[8].Text;
                            break;
                        }
                    }
                    break;
                }
                case Step.Quantity:
                    if (!this.management.CheckQuantity(barcode))
                    {
                        MessageShow.Alert("Error", "请输入合法的数量！");
                        this.tbxBarcode.Focus();
                        this.tbxBarcode.SelectAll();
                        return;
                    }
                    if ((this.tempInstruction.RequiredQuantity - this.tempInstruction.Result.TotalQuantity) >= decimal.Parse(barcode))
                    {
                        if (this.AddNewDetail(decimal.Parse(barcode)))
                        {
                            this.lblQuantity.Text = barcode;
                            this.UpdateSubItem();
                            this.tbxBarcode.Text = "";
                            this.lblMsg.Text = "请采集物料条码";
                            this.currStep = Step.GoodsID;
                        }
                        return;
                    }
                    MessageShow.Alert("Error", "指定的数量超出范围！");
                    this.tbxBarcode.Focus();
                    this.tbxBarcode.SelectAll();
                    return;

                default:
                    MessageShow.Alert("Error", "错误的条码");
                    return;
            }
            this.tbxBarcode.Text = "";
            this.tbxBarcode.Focus();
            this.lblBatchID.Text = this.supplyID + this.Date;
            this.lblMsg.Text = "请输入产品数量";
            this.currStep = Step.Quantity;
        }

        private void print(EnumSubmitType submitType, DataSet PrintRecord)
        {
            IniFile file = new IniFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName) + "/PrintTest.ini");
            string port = file.ReadValue("Print", "PrintPort", "COM7");
            file.ReadValue("Print", "PrintModel", "MPK1230E");
            string baud = file.ReadValue("Print", "PrintBaud", "115200");
            switch (submitType)
            {
                case EnumSubmitType.PartSubmit:
                    this.MPK1280Print(port, baud, EnumSubmitType.PartSubmit, PrintRecord);
                    return;

                case EnumSubmitType.TotalSubmit:
                    this.MPK1280Print(port, baud, EnumSubmitType.TotalSubmit, PrintRecord);
                    return;
            }
        }

        private void submit()
        {
            string str = string.Empty;
            DataSet listPda = new DataSet();
            if (!this.busy)
            {
                this.busy = true;
                Cursor.Current = Cursors.WaitCursor;
                WaitingFrm frm = new WaitingFrm();
                frm.Show("提交", "正在提交采集数据，请稍候...");
                try
                {
                    this.strresult = Global.MiddleService.SubmitInRecord(this.orderID, this.proID, this.supplyID, Management.GetSingleton().WarehouseNo, Global.CurrUser.ToString(), Global.CurrTask, ref listPda, ref str);
                    if (this.strresult != "")
                    {
                        this.print(EnumSubmitType.TotalSubmit, listPda);
                    }
                }
                catch (ApplicationException exception)
                {
                    Cursor.Current = Cursors.Default;
                    frm.Close();
                    MessageShow.Alert("Error", exception.Message);
                    this.busy = false;
                    return;
                }
                catch (Exception exception2)
                {
                    Cursor.Current = Cursors.Default;
                    frm.Close();
                    MessageShow.Alert("UnKnowError", exception2.Message);
                    this.busy = false;
                    return;
                }
                Cursor.Current = Cursors.Default;
                frm.Close();
                MessageShow.Alert("提示", str);
                this.busy = false;
                base.Close();
                base.DialogResult = DialogResult.OK;
                new CheckInMain().btnCheckIn_Click(null, null);
            }
        }

        private void tbxBarcode_GotFocus(object sender, EventArgs e)
        {
            OEM_Catchwell.SetOpMode(0);
        }

        private void tbxBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                string barcode = this.tbxBarcode.Text.Trim();
                if (barcode != string.Empty)
                {
                    this.Performing(sender, barcode);
                }
            }
        }

        private void UpdateListView()
        {
            this.a = 0;
            this.lsvTaskDetails.Items.Clear();
            ArrayList keys = Global.CurrTask.InsList.Keys as ArrayList;
            for (int i = 0; i < keys.Count; i++)
            {
                CheckInstruction instruction = Global.CurrTask.InsList[keys[i]] as CheckInstruction;
                ListViewItem item = new ListViewItem();
                item.SubItems.Add(instruction.IndicateLocation);
                item.SubItems.Add(instruction.GoodsID);
                item.SubItems.Add(instruction.Result.TotalQuantity.ToString() + '/' + instruction.RequiredQuantity.ToString());
                item.SubItems.Add(instruction.Finishqty.ToString());
                item.SubItems.Add(instruction.Unit);
                item.SubItems.Add(instruction.Goodsname);
                item.SubItems.Add(instruction.Rowno);
                item.SubItems.Add(instruction.Tiosno);
                this.lsvTaskDetails.Items.Add(item);
                this.a += instruction.Result.ColDetails.Count;
            }
            this.Lblcount.Text = this.a.ToString() + "笔";
            this.tbxBarcode.Focus();
            this.tbxBarcode.SelectAll();
        }

        private void UpdateSubItem()
        {
            string text = this.lsvTaskDetails.Items[this.index].SubItems[3].Text;
            this.lsvTaskDetails.Items[this.index].SubItems[3].Text = ((decimal.Parse(text.Split(new char[] { '/' })[0]) + this.detail.CollectedQuantity)).ToString() + '/' + decimal.Parse(text.Split(new char[] { '/' })[1]).ToString();
            this.lsvTaskDetails.Items[this.index].Selected = true;
            this.lsvTaskDetails.EnsureVisible(this.index);
        }

        private enum Step
        {
            Location,
            GoodsID,
            BatchID,
            SingleID,
            Quantity,
            ProduceNo
        }
    }
}

