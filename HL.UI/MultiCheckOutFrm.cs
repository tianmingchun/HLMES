namespace HL.UI
{
    using BizLayer;
    using Entity;
    using SharpExCS;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using HL.Framework;
    using HL.Controls;

    public class MultiCheckOutFrm : Form
    {
        private int a;
        private ArrayList batchMatching = new ArrayList();
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
        private IContainer components;
        private Step currStep = Step.GoodsID;
        private ColDetail detail;
        private ArrayList fullMatching = new ArrayList();
        private ColumnHeader headerCollected;
        private ColumnHeader headerFreightNumber;
        private ColumnHeader headerTaskNumber;
        private int index = -1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private HeadLabel lblBatchID;
        private Label Lblcount;
        private HeadLabel lblGoodsID;
        private Label LblGoodsname;
        private HeadLabel lblInventory;
        private HeadLabel lblLocation;
        private Label lblMsg;
        private HeadLabel lblQuantity;
        private HeadLabel lblSingleID;
        private Label lbltiosno;
        private Label lblWarehouse;
        private ArrayList locationMatching = new ArrayList();
        private ListView lsvTaskDetails;
        private Management management = Management.GetSingleton();
        private string orderID = string.Empty;
        private RegulationCheck regulation = RegulationCheck.GetInstance();
        private RegulationCheck Regulation = RegulationCheck.GetInstance();
        private const int RET_KEYSTROKE = 0;
        private HeadLabel TasksTitile;
        private TextBox tbxBarcode;
        private CheckInstruction tempInstruction;
        private ArrayList unMatching = new ArrayList();

        public MultiCheckOutFrm(string orderid)
        {
            this.InitializeComponent();
            this.orderID = orderid;
        }

        private bool AddNewDetail(decimal quantity)
        {
            this.detail = new CheckInColDetail();
            this.detail.AssociateInstruction = this.tempInstruction;
            this.detail.BatchNumber = this.lblBatchID.Text;
            this.detail.Location = this.lblLocation.Text;
            this.detail.SingletonNo = this.lblSingleID.Text;
            this.detail.GoodsID = this.lblGoodsID.Text;
            this.detail.CollectedQuantity = quantity;
            this.detail.Tiosno = this.lbltiosno.Text;
            this.detail.CollectedTime = DateTime.Now;
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
                this.detail.BatchNumber = DateTime.Now.ToString("yyyyMMdd");
                this.detail.SingletonNo = this.lblSingleID.Text;
                this.detail.Location = this.lblLocation.Text;
                this.detail.GoodsID = this.lsvTaskDetails.Items[i].SubItems[2].Text;
                this.detail.Unit = this.lsvTaskDetails.Items[i].SubItems[5].Text;
                this.detail.Tiosno = this.lsvTaskDetails.Items[i].SubItems[7].Text;
                this.detail.CollectedQuantity = decimal.Parse(this.lsvTaskDetails.Items[i].SubItems[3].Text.Split(new char[] { '/' })[1]) - decimal.Parse(this.lsvTaskDetails.Items[i].SubItems[3].Text.Split(new char[] { '/' })[0]);
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
            if (!this.busy)
            {
                if (this.CheckResultIsEmpty())
                {
                    MessageShow.Alert("Warning", "该任务还没有任何采集数据，无法提交！");
                }
                else
                {
                    this.busy = true;
                    Cursor.Current = Cursors.WaitCursor;
                    WaitingFrm frm = new WaitingFrm();
                    frm.Show("提交", "正在提交采集数据，请稍候...");
                    try
                    {
                        Global.MiddleService.SubmitOutRecord(this.orderID, Global.CurrUser.ToString(), Global.CurrTask, ref str);
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
                    MessageShow.Alert("Success", str);
                    this.busy = false;
                    base.Close();
                    base.DialogResult = DialogResult.OK;
                    new CheckOutMain().btnCheckOut_Click(null, null);
                }
            }
        }

        private void btnTotalSubmit_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("确定要提交整个生产订单吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.No) && this.AddTotalDetail())
            {
                this.submit();
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
                CheckOutInstruction instruction = Global.CurrTask.InsList[keys[i]] as CheckOutInstruction;
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

        public bool CheckQuantity(string count)
        {
            Regex regex = new Regex(@"^-?\d+(\.\d{1,3})?$");
            if (!regex.IsMatch(count))
            {
                return false;
            }
            return true;
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
            this.lbltiosno.Text = "";
        }

        private void ClearErrorBarcode()
        {
            this.ClearAllControls();
            this.tbxBarcode.Focus();
            this.tbxBarcode.SelectAll();
        }

        private void ClearInsIndexs()
        {
            this.fullMatching.Clear();
            this.locationMatching.Clear();
            this.batchMatching.Clear();
            this.unMatching.Clear();
        }

        private void dcCollection_Perform(string barcode)
        {
            if (barcode != string.Empty)
            {
                this.Performing(this.tbxBarcode, barcode);
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
            this.lblMsg = new Label();
            this.headerFreightNumber = new ColumnHeader();
            this.headerTaskNumber = new ColumnHeader();
            this.btnBack = new Button();
            this.btnSubmit = new Button();
            this.tbxBarcode = new TextBox();
            this.label6 = new Label();
            this.btnDetail = new Button();
            this.headerCollected = new ColumnHeader();
            this.lsvTaskDetails = new ListView();
            this.columnHeader1 = new ColumnHeader();
            this.columnHeader4 = new ColumnHeader();
            this.columnHeader2 = new ColumnHeader();
            this.columnHeader3 = new ColumnHeader();
            this.btnChangeLocation = new Button();
            this.label2 = new Label();
            this.label5 = new Label();
            this.label7 = new Label();
            this.LblGoodsname = new Label();
            this.Lblcount = new Label();
            this.label8 = new Label();
            this.lblWarehouse = new Label();
            this.label1 = new Label();
            this.btnTotalSubmit = new Button();
            this.label3 = new Label();
            this.lbltiosno = new Label();
            this.columnHeader5 = new ColumnHeader();
            this.lblInventory = new HeadLabel();
            this.lblLocation = new HeadLabel();
            this.lblQuantity = new HeadLabel();
            this.lblBatchID = new HeadLabel();
            this.lblSingleID = new HeadLabel();
            this.lblGoodsID = new HeadLabel();
            this.TasksTitile = new HeadLabel();
            base.SuspendLayout();
            this.lblMsg.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.lblMsg.Location = new Point(0, 0x20);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new Size(0x72, 20);
            this.lblMsg.Text = "请扫描";
            this.headerFreightNumber.Text = "物料";
            this.headerFreightNumber.Width = 0x73;
            this.headerTaskNumber.Text = "任务明细号";
            this.headerTaskNumber.Width = 0;
            this.btnBack.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnBack.Location = new Point(0x8d, 0x7c);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new Size(0x36, 0x17);
            this.btnBack.TabIndex = 0x49;
            this.btnBack.Text = "返回";
            this.btnBack.Click += new EventHandler(this.btnBack_Click);
            this.btnSubmit.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnSubmit.ForeColor = Color.Black;
            this.btnSubmit.Location = new Point(0x48, 0x7c);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new Size(0x36, 0x17);
            this.btnSubmit.TabIndex = 0x48;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.Click += new EventHandler(this.btnSubmit_Click);
            this.tbxBarcode.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.tbxBarcode.Location = new Point(0x7a, 0x20);
            this.tbxBarcode.Name = "tbxBarcode";
            this.tbxBarcode.Size = new Size(0x73, 0x15);
            this.tbxBarcode.TabIndex = 70;
            this.tbxBarcode.GotFocus += new EventHandler(this.tbxBarcode_GotFocus);
            this.tbxBarcode.KeyDown += new KeyEventHandler(this.tbxBarcode_KeyDown);
            this.label6.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label6.Location = new Point(-1, 0x95);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x4a, 20);
            this.label6.Text = "任务明细：";
            this.btnDetail.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnDetail.Location = new Point(3, 0x7c);
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new Size(0x36, 0x17);
            this.btnDetail.TabIndex = 0x47;
            this.btnDetail.Text = "明细";
            this.btnDetail.Click += new EventHandler(this.btnDetail_Click);
            this.headerCollected.Text = "数量";
            this.headerCollected.Width = 70;
            this.lsvTaskDetails.Columns.Add(this.headerTaskNumber);
            this.lsvTaskDetails.Columns.Add(this.columnHeader1);
            this.lsvTaskDetails.Columns.Add(this.headerFreightNumber);
            this.lsvTaskDetails.Columns.Add(this.headerCollected);
            this.lsvTaskDetails.Columns.Add(this.columnHeader4);
            this.lsvTaskDetails.Columns.Add(this.columnHeader2);
            this.lsvTaskDetails.Columns.Add(this.columnHeader3);
            this.lsvTaskDetails.Columns.Add(this.columnHeader5);
            this.lsvTaskDetails.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.lsvTaskDetails.FullRowSelect = true;
            this.lsvTaskDetails.Location = new Point(3, 0xa5);
            this.lsvTaskDetails.Name = "lsvTaskDetails";
            this.lsvTaskDetails.Size = new Size(0xea, 0x7a);
            this.lsvTaskDetails.TabIndex = 0x4a;
            this.lsvTaskDetails.View = View.Details;
            this.lsvTaskDetails.KeyDown += new KeyEventHandler(this.lsvTaskDetails_KeyDown);
            this.columnHeader1.Text = "库位";
            this.columnHeader1.Width = 0;
            this.columnHeader4.Text = "已发";
            this.columnHeader4.Width = 50;
            this.columnHeader2.Text = "单位";
            this.columnHeader2.Width = 0x2b;
            this.columnHeader3.Text = "物料名称";
            this.columnHeader3.Width = 0;
            this.btnChangeLocation.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnChangeLocation.Location = new Point(0xb6, 0x38);
            this.btnChangeLocation.Name = "btnChangeLocation";
            this.btnChangeLocation.Size = new Size(0x30, 20);
            this.btnChangeLocation.TabIndex = 0x5c;
            this.btnChangeLocation.Text = "复位";
            this.btnChangeLocation.Click += new EventHandler(this.btnChangeLocation_Click);
            this.label2.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label2.Location = new Point(3, 0x4f);
            this.label2.Name = "label2";
            this.label2.Size = new Size(50, 0x11);
            this.label2.Text = "物料：";
            this.label5.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label5.Location = new Point(0x9e, 0x4e);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x2e, 0x12);
            this.label5.Text = "数量：";
            this.label7.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label7.Location = new Point(0, 0x61);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x35, 20);
            this.label7.Text = "名称：";
            this.LblGoodsname.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.LblGoodsname.Location = new Point(0x1f, 0x60);
            this.LblGoodsname.Name = "LblGoodsname";
            this.LblGoodsname.Size = new Size(0x7f, 0x10);
            this.Lblcount.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.Lblcount.ForeColor = Color.Red;
            this.Lblcount.Location = new Point(0xc5, 2);
            this.Lblcount.Name = "Lblcount";
            this.Lblcount.Size = new Size(0x25, 20);
            this.label8.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label8.Location = new Point(0x92, 2);
            this.label8.Name = "label8";
            this.label8.Size = new Size(60, 20);
            this.label8.Text = "已采集：";
            this.lblWarehouse.Location = new Point(0x44, 0x38);
            this.lblWarehouse.Name = "lblWarehouse";
            this.lblWarehouse.Size = new Size(0x56, 20);
            this.label1.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label1.Location = new Point(0, 0x39);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x48, 0x11);
            this.label1.Text = "当前库房：";
            this.btnTotalSubmit.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnTotalSubmit.Location = new Point(210, 0x7c);
            this.btnTotalSubmit.Name = "btnTotalSubmit";
            this.btnTotalSubmit.Size = new Size(0x18, 0x17);
            this.btnTotalSubmit.TabIndex = 130;
            this.btnTotalSubmit.Text = "批";
            this.btnTotalSubmit.Click += new EventHandler(this.btnTotalSubmit_Click);
            this.label3.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label3.Location = new Point(0x99, 0x62);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x2c, 0x12);
            this.label3.Text = "库存：";
            this.lbltiosno.Location = new Point(0x40, 150);
            this.lbltiosno.Name = "lbltiosno";
            this.lbltiosno.Size = new Size(100, 11);
            this.lbltiosno.Visible = false;
            this.columnHeader5.Text = "操作号";
            this.columnHeader5.Width = 0;
            this.lblInventory.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.lblInventory.ForeColor = Color.Red;
            this.lblInventory.Location = new Point(0xb6, 0x63);
            this.lblInventory.Name = "lblInventory";
            this.lblInventory.Size = new Size(0x37, 0x12);
            this.lblInventory.TabIndex = 0x8e;
            this.lblLocation.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.lblLocation.Location = new Point(0x51, 0x95);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new Size(0x4d, 0x10);
            this.lblLocation.TabIndex = 120;
            this.lblLocation.Visible = false;
            this.lblQuantity.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.lblQuantity.Location = new Point(0xc4, 0x4d);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new Size(0x22, 0x12);
            this.lblQuantity.TabIndex = 0x5d;
            this.lblQuantity.DoubleClick += new EventHandler(this.lblQuantity_DoubleClick);
            this.lblBatchID.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.lblBatchID.Location = new Point(0x38, 0x95);
            this.lblBatchID.Name = "lblBatchID";
            this.lblBatchID.Size = new Size(0x75, 0x10);
            this.lblBatchID.TabIndex = 0x5e;
            this.lblBatchID.Visible = false;
            this.lblBatchID.DoubleClick += new EventHandler(this.lblBatchID_DoubleClick);
            this.lblSingleID.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.lblSingleID.Location = new Point(0x99, 0x95);
            this.lblSingleID.Name = "lblSingleID";
            this.lblSingleID.Size = new Size(0x4c, 10);
            this.lblSingleID.TabIndex = 0x5f;
            this.lblSingleID.DoubleClick += new EventHandler(this.lblSingleID_DoubleClick);
            this.lblGoodsID.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.lblGoodsID.Location = new Point(0x2a, 0x4d);
            this.lblGoodsID.Name = "lblGoodsID";
            this.lblGoodsID.Size = new Size(0x68, 0x12);
            this.lblGoodsID.TabIndex = 0x60;
            this.lblGoodsID.DoubleClick += new EventHandler(this.lblGoodsID_DoubleClick);
            this.TasksTitile.BackColor = SystemColors.HotTrack;
            this.TasksTitile.Font = new Font("Arial", 14f, FontStyle.Regular);
            this.TasksTitile.ForeColor = SystemColors.ActiveCaptionText;
            this.TasksTitile.Location = new Point(0, 2);
            this.TasksTitile.Name = "TasksTitile";
            this.TasksTitile.Size = new Size(240, 0x18);
            this.TasksTitile.TabIndex = 0x4d;
            this.TasksTitile.Text = "发料采集";
            base.AutoScaleDimensions = new SizeF(96f, 96f);
            //base.AutoScaleMode = AutoScaleMode.Dpi;
            this.AutoScroll = true;
            base.ClientSize = new Size(240, 290);
            base.ControlBox = false;
            base.Controls.Add(this.lbltiosno);
            base.Controls.Add(this.lblInventory);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.btnTotalSubmit);
            base.Controls.Add(this.lblWarehouse);
            base.Controls.Add(this.lblLocation);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.Lblcount);
            base.Controls.Add(this.label8);
            base.Controls.Add(this.LblGoodsname);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.btnChangeLocation);
            base.Controls.Add(this.lblQuantity);
            base.Controls.Add(this.lblBatchID);
            base.Controls.Add(this.lblSingleID);
            base.Controls.Add(this.lblGoodsID);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.TasksTitile);
            base.Controls.Add(this.lblMsg);
            base.Controls.Add(this.btnBack);
            base.Controls.Add(this.btnSubmit);
            base.Controls.Add(this.tbxBarcode);
            base.Controls.Add(this.btnDetail);
            base.Controls.Add(this.lsvTaskDetails);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.label5);
            //base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "MultiCheckOutFrm";
            base.Deactivate += new EventHandler(this.MultiCheckOutFrm_Deactivate);
            base.Load += new EventHandler(this.MultiCheckOutFrm_Load);
            base.Activated += new EventHandler(this.MultiCheckOutFrm_Activated);
            base.ResumeLayout(false);
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

        private void lblSingleID_DoubleClick(object sender, EventArgs e)
        {
            if (this.lblSingleID.Text.Length > 9)
            {
                new DetailFrm(this.lblSingleID.Text).ShowDialog();
            }
        }

        private void lsvTaskDetails_KeyDown(object sender, KeyEventArgs e)
        {
            string msgExrror = string.Empty;
            if ((e.KeyValue == 13) && (this.currStep == Step.GoodsID))
            {
                this.ClearAllControls();
                string barcode = "$" + this.lsvTaskDetails.Items[this.lsvTaskDetails.SelectedIndices[0]].SubItems[2].Text;
                string text = this.lsvTaskDetails.Items[this.lsvTaskDetails.SelectedIndices[0]].SubItems[6].Text;
                string str4 = this.lsvTaskDetails.Items[this.lsvTaskDetails.SelectedIndices[0]].SubItems[7].Text;
                switch (this.CheckGoodsID(barcode))
                {
                    case 1:
                        this.lblGoodsID.Text = barcode.Substring(1);
                        this.LblGoodsname.Text = text;
                        this.lbltiosno.Text = str4;
                        this.tbxBarcode.Text = "";
                        this.tbxBarcode.Focus();
                        this.lblBatchID.Text = DateTime.Now.ToString("yyyyMMdd");
                        this.lblInventory.Text = Convert.ToString(Global.MiddleService.GetInventoryQty(this.lblWarehouse.Text.Substring(0, 4).ToString(), barcode.Substring(1).ToString(), this.lblWarehouse.Text, ref msgExrror));
                        this.lblMsg.Text = "请输入产品数量";
                        this.currStep = Step.Quantity;
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

        private void MultiCheckOutFrm_Activated(object sender, EventArgs e)
        {
            OEM_Catchwell.SetBeamHold(false);
            this.tbxBarcode.Focus();
            this.tbxBarcode.SelectAll();
        }

        private void MultiCheckOutFrm_Deactivate(object sender, EventArgs e)
        {
            OEM_Catchwell.SetBeamHold(true);
        }

        private void MultiCheckOutFrm_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Cursor.Current = Cursors.Default;
            this.lblWarehouse.Text = Management.GetSingleton().WarehouseNo;
            this.lblLocation.Text = this.lblWarehouse.Text + "01";
            this.UpdateListView();
            this.lblMsg.Text = this.lblMsg.Text + "物料条码";
        }

        private void Performing(object sender, string barcode)
        {
            string msgExrror = string.Empty;
            switch (this.currStep)
            {
                case Step.Location:
                    this.lblLocation.Text = barcode;
                    this.tbxBarcode.Text = "";
                    this.tbxBarcode.Focus();
                    this.lblMsg.Text = "请采集货号条码";
                    this.currStep = Step.GoodsID;
                    return;

                case Step.GoodsID:
                {
                    this.ClearAllControls();
                    this.ClearInsIndexs();
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
                            this.lbltiosno.Text = this.lsvTaskDetails.Items[i].SubItems[7].Text;
                            break;
                        }
                    }
                    break;
                }
                case Step.Quantity:
                    if (!this.CheckQuantity(barcode))
                    {
                        MessageShow.Alert("Error", "请输入合法的数量！");
                        this.tbxBarcode.Focus();
                        this.tbxBarcode.SelectAll();
                        return;
                    }
                    if ((decimal.Parse(barcode.ToString()) + this.tempInstruction.Result.TotalQuantity) <= decimal.Parse(this.lblInventory.Text.ToString()))
                    {
                        if (!this.tempInstruction.IsOver && ((this.tempInstruction.RequiredQuantity - this.tempInstruction.Result.TotalQuantity) < decimal.Parse(barcode.ToString())))
                        {
                            MessageShow.Alert("Error", "指定的数量超出范围！");
                            this.tbxBarcode.Focus();
                            this.tbxBarcode.SelectAll();
                            return;
                        }
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
                    MessageShow.Alert("Error", "指定的数量超出库存");
                    this.tbxBarcode.Focus();
                    this.tbxBarcode.SelectAll();
                    return;

                default:
                    MessageShow.Alert("Error", "错误的条码");
                    return;
            }
            this.tbxBarcode.Text = "";
            this.tbxBarcode.Focus();
            this.lblBatchID.Text = DateTime.Now.ToString("yyyyMMdd");
            this.lblInventory.Text = Convert.ToString(Global.MiddleService.GetInventoryQty(this.lblWarehouse.Text.Substring(0, 4).ToString(), barcode.Substring(1).ToString(), this.lblWarehouse.Text, ref msgExrror));
            this.lblMsg.Text = "请输入产品数量";
            this.currStep = Step.Quantity;
        }

        private void submit()
        {
            string str = string.Empty;
            if (!this.busy)
            {
                this.busy = true;
                Cursor.Current = Cursors.WaitCursor;
                WaitingFrm frm = new WaitingFrm();
                frm.Show("提交", "正在提交采集数据，请稍候...");
                try
                {
                    Global.MiddleService.SubmitOutRecord(this.orderID, Global.CurrUser.ToString(), Global.CurrTask, ref str);
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
                new CheckOutMain().btnCheckOut_Click(null, null);
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
            this.lsvTaskDetails.Items.Clear();
            ArrayList keys = Global.CurrTask.InsList.Keys as ArrayList;
            for (int i = 0; i < keys.Count; i++)
            {
                CheckOutInstruction instruction = Global.CurrTask.InsList[keys[i]] as CheckOutInstruction;
                ListViewItem item = new ListViewItem();
                item.SubItems.Add(instruction.IndicateLocation);
                item.SubItems.Add(instruction.GoodsID);
                item.SubItems.Add(instruction.Result.TotalQuantity.ToString() + '/' + instruction.RequiredQuantity.ToString());
                item.SubItems.Add(instruction.Finishqty.ToString());
                item.SubItems.Add(instruction.Unit);
                item.SubItems.Add(instruction.Goodsname);
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
            Quantity
        }
    }
}

