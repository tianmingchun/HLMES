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
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Windows.Forms;
    using HL.Framework;
    using HL.Controls;

    public class CheckInByOrderFrm : Form
    {
        private ArrayList arrindex = new ArrayList();
        private Button btnExit;
        private Button btnQuery;
        private Button btnSubmit;
        private Button btnUpdateQty;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader10;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader8;
        private ColumnHeader columnHeader9;
        private IContainer components;
        private Label label1;
        private Label label2;
        private Label lblDate;
        private Label lblProid;
        private Label lblSingleID;
        private Label lblSupplyID;
        private Label lblSupplyname;
        private ListView lsvTasks;
        private const int RET_KEYSTROKE = 0;
        private MiddleService service = Global.MiddleService;
        private string strresult = string.Empty;
        private CheckTask t;
        private HeadLabel TasksTitile;
        private CheckInstruction tempInstruction;
        private TextBox txtOrderID;
        private TextBox txtQty;
        private string userName = string.Empty;

        public CheckInByOrderFrm()
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
                CheckInColDetail result = new CheckInColDetail();
                result.AssociateInstruction = this.tempInstruction;
                result.BatchNumber = this.lblSupplyID.Text + this.lblDate.Text;
                result.SingletonNo = this.lblSingleID.Text;
                result.Location = Management.GetSingleton().WarehouseNo + "01";
                result.GoodsID = this.lsvTasks.Items[Convert.ToInt32(this.arrindex[i])].SubItems[1].Text;
                result.CollectedQuantity = decimal.Parse(this.lsvTasks.Items[Convert.ToInt32(this.arrindex[i])].SubItems[2].Text);
                result.CollectedTime = DateTime.Now;
                result.Unit = this.lsvTasks.Items[Convert.ToInt32(this.arrindex[i])].SubItems[5].Text;
                result.Rowno = this.lsvTasks.Items[Convert.ToInt32(this.arrindex[i])].SubItems[8].Text;
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
            return true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (this.txtOrderID.Text == "")
            {
                MessageShow.Alert("error", "请输入采购订单号");
                this.txtOrderID.Focus();
            }
            else
            {
                this.lsvTasks.Items.Clear();
                Cursor.Current = Cursors.WaitCursor;
                DataSet set = new DataSet();
                try
                {
                    set = this.service.dsGetTaskByOrderID(Management.GetSingleton().WarehouseNo, this.txtOrderID.Text, EnumTaskType.CheckIn);
                }
                catch (ApplicationException exception)
                {
                    Cursor.Current = Cursors.Default;
                    MessageShow.Alert("错误", exception.Message);
                    this.txtOrderID.Focus();
                    this.txtOrderID.SelectAll();
                    return;
                }
                catch (Exception exception2)
                {
                    Cursor.Current = Cursors.Default;
                    MessageShow.Alert("错误", exception2.Message);
                    this.txtOrderID.Focus();
                    this.txtOrderID.SelectAll();
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
                        this.lblSupplyname.Text = table.Rows[i]["CLN_NAME"].ToString();
                        this.lblSupplyID.Text = table.Rows[i]["proclients"].ToString();
                        this.lblDate.Text = Convert.ToDateTime(table.Rows[i]["prodate"]).ToString("yyyyMMdd");
                        this.lblProid.Text = table.Rows[i]["PROID"].ToString();
                        ListViewItem item = new ListViewItem(new string[] { "", table.Rows[i]["GDSID"].ToString(), num2.ToString(), num2.ToString(), num3.ToString(), table.Rows[i]["TIOUNIT"].ToString(), table.Rows[i]["MATNAME"].ToString(), table.Rows[i]["TIOSNO"].ToString(), table.Rows[i]["TIOPOLID"].ToString() });
                        this.lsvTasks.Items.Add(item);
                    }
                }
                else
                {
                    MessageShow.Alert("error", "该采购订单没有任务明细");
                    this.txtOrderID.Focus();
                    this.txtOrderID.SelectAll();
                    this.lsvTasks.Items.Clear();
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            this.t = new CheckTask(this.txtOrderID.Text);
            string str = string.Empty;
            DataSet listPda = new DataSet();
            for (int i = 0; i < this.lsvTasks.Items.Count; i++)
            {
                if (this.lsvTasks.Items[i].Checked)
                {
                    this.arrindex.Add(i);
                    CheckInstruction ins = new CheckInstruction(this.lsvTasks.Items[i].SubItems[7].Text.ToString(), decimal.Parse(this.lsvTasks.Items[i].SubItems[2].Text.ToString()));
                    ins.GoodsID = this.lsvTasks.Items[i].SubItems[1].Text.ToString();
                    ins.Finishqty = decimal.Parse(this.lsvTasks.Items[i].SubItems[4].Text.ToString());
                    this.t.AddIns(ins);
                }
            }
            if (this.arrindex.Count != 0)
            {
                if (this.AddTotalDetail())
                {
                    if (!this.isOpenPort())
                    {
                        if (MessageBox.Show("打开蓝牙打印机失败，现在要连接打印机吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            new ConfigMain().btnConfigPrinter_Click(null, null);
                        }
                        return;
                    }
                    try
                    {
                        this.strresult = Global.MiddleService.SubmitInRecord(this.txtOrderID.Text, this.lblProid.Text, this.lblSupplyID.Text, Management.GetSingleton().WarehouseNo, Global.CurrUser.ToString(), this.t, ref listPda, ref str);
                        if (this.strresult != "")
                        {
                            this.print(listPda);
                        }
                    }
                    catch (ApplicationException exception)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageShow.Alert("Error", exception.Message);
                        return;
                    }
                    catch (Exception exception2)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageShow.Alert("UnKnowError", exception2.Message);
                        return;
                    }
                    Cursor.Current = Cursors.Default;
                    MessageShow.Alert("提示", str);
                    this.btnQuery_Click(sender, e);
                }
                this.arrindex.Clear();
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
                    if (decimal.Parse(this.txtQty.Text) > decimal.Parse(this.lsvTasks.Items[this.lsvTasks.SelectedIndices[0]].SubItems[3].Text))
                    {
                        MessageShow.Alert("error", "发料数不能大于任务数");
                        this.txtQty.Focus();
                        this.txtQty.SelectAll();
                    }
                    else
                    {
                        this.lsvTasks.Items[this.lsvTasks.SelectedIndices[0]].SubItems[2].Text = this.txtQty.Text;
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

        private void CheckInByOrderFrm_Activated(object sender, EventArgs e)
        {
            OEM_Catchwell.SetBeamHold(false);
            this.txtOrderID.Focus();
            this.txtOrderID.SelectAll();
        }

        private void CheckInByOrderFrm_Deactivate(object sender, EventArgs e)
        {
            OEM_Catchwell.SetBeamHold(true);
        }

        private void CheckInByOrderFrm_KeyDown(object sender, KeyEventArgs e)
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

        private void CheckInByOrderFrm_Load(object sender, EventArgs e)
        {
            this.txtOrderID.Focus();
            DataSet set = new DataSet();
            DataTable table = new DataTable();
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

        public bool CheckQuantity(string count)
        {
            Regex regex = new Regex(@"^\d+(\.\d{1,3})?$");
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
            this.columnHeader1 = new ColumnHeader();
            this.columnHeader2 = new ColumnHeader();
            this.columnHeader3 = new ColumnHeader();
            this.columnHeader4 = new ColumnHeader();
            this.columnHeader8 = new ColumnHeader();
            this.columnHeader5 = new ColumnHeader();
            this.columnHeader6 = new ColumnHeader();
            this.columnHeader7 = new ColumnHeader();
            this.columnHeader9 = new ColumnHeader();
            this.columnHeader10 = new ColumnHeader();
            this.btnExit = new Button();
            this.btnSubmit = new Button();
            this.btnQuery = new Button();
            this.txtOrderID = new TextBox();
            this.label1 = new Label();
            this.btnUpdateQty = new Button();
            this.lblSupplyname = new Label();
            this.label2 = new Label();
            this.txtQty = new TextBox();
            this.lblSupplyID = new Label();
            this.lblDate = new Label();
            this.TasksTitile = new HeadLabel();
            this.lblSingleID = new Label();
            this.lblProid = new Label();
            base.SuspendLayout();
            this.lsvTasks.CheckBoxes = true;
            this.lsvTasks.Columns.Add(this.columnHeader1);
            this.lsvTasks.Columns.Add(this.columnHeader2);
            this.lsvTasks.Columns.Add(this.columnHeader3);
            this.lsvTasks.Columns.Add(this.columnHeader4);
            this.lsvTasks.Columns.Add(this.columnHeader8);
            this.lsvTasks.Columns.Add(this.columnHeader5);
            this.lsvTasks.Columns.Add(this.columnHeader6);
            this.lsvTasks.Columns.Add(this.columnHeader7);
            this.lsvTasks.Columns.Add(this.columnHeader9);
            this.lsvTasks.Columns.Add(this.columnHeader10);
            this.lsvTasks.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Regular);
            this.lsvTasks.FullRowSelect = true;
            this.lsvTasks.Location = new Point(0, 0x7b);
            this.lsvTasks.Name = "lsvTasks";
            this.lsvTasks.Size = new Size(0xed, 0xa4);
            this.lsvTasks.TabIndex = 0x44;
            this.lsvTasks.View = View.Details;
            this.columnHeader1.Text = " ";
            this.columnHeader1.Width = 0x19;
            this.columnHeader2.Text = "物料";
            this.columnHeader2.Width = 100;
            this.columnHeader3.Text = "收货数";
            this.columnHeader3.Width = 50;
            this.columnHeader4.Text = "任务数";
            this.columnHeader4.Width = 50;
            this.columnHeader8.Text = "已收数";
            this.columnHeader8.Width = 50;
            this.columnHeader5.Text = "单位";
            this.columnHeader5.Width = 40;
            this.columnHeader6.Text = "物料名称";
            this.columnHeader6.Width = 0;
            this.columnHeader7.Text = "操作号";
            this.columnHeader7.Width = 0;
            this.columnHeader9.Text = "行号";
            this.columnHeader9.Width = 0;
            this.columnHeader10.Text = "凭证号";
            this.columnHeader10.Width = 0;
            this.btnExit.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Regular);
            this.btnExit.Location = new Point(0xa7, 0x57);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new Size(0x48, 30);
            this.btnExit.TabIndex = 0x43;
            this.btnExit.Text = "返回";
            this.btnExit.Click += new EventHandler(this.btnExit_Click);
            this.btnSubmit.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Regular);
            this.btnSubmit.Location = new Point(0x55, 0x57);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new Size(0x48, 30);
            this.btnSubmit.TabIndex = 0x42;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.Click += new EventHandler(this.btnSubmit_Click);
            this.btnQuery.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Regular);
            this.btnQuery.Location = new Point(3, 0x57);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new Size(0x48, 30);
            this.btnQuery.TabIndex = 0x41;
            this.btnQuery.Text = "检索";
            this.btnQuery.Click += new EventHandler(this.btnQuery_Click);
            this.txtOrderID.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Regular);
            this.txtOrderID.Location = new Point(0x4b, 30);
            this.txtOrderID.Name = "txtOrderID";
            this.txtOrderID.Size = new Size(0x7a, 0x15);
            this.txtOrderID.TabIndex = 0x40;
            this.txtOrderID.GotFocus += new EventHandler(this.txtOrderID_GotFocus);
            this.txtOrderID.KeyDown += new KeyEventHandler(this.txtOrderID_KeyDown);
            this.label1.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Regular);
            this.label1.Location = new Point(8, 0x1f);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x3d, 20);
            this.label1.Text = "采购订单";
            this.btnUpdateQty.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Regular);
            this.btnUpdateQty.Location = new Point(0xa5, 0x3a);
            this.btnUpdateQty.Name = "btnUpdateQty";
            this.btnUpdateQty.Size = new Size(0x3d, 0x17);
            this.btnUpdateQty.TabIndex = 70;
            this.btnUpdateQty.Text = "确定";
            this.btnUpdateQty.Click += new EventHandler(this.btnUpdateQty_Click);
            this.lblSupplyname.Location = new Point(0xcb, 0x23);
            this.lblSupplyname.Name = "lblSupplyname";
            this.lblSupplyname.Size = new Size(0x22, 20);
            this.lblSupplyname.Visible = false;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Regular);
            this.label2.Location = new Point(8, 0x3a);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x43, 0x12);
            this.label2.Text = "到货数：";
            this.txtQty.Location = new Point(0x4b, 0x3a);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new Size(0x49, 0x17);
            this.txtQty.TabIndex = 0x49;
            this.txtQty.KeyDown += new KeyEventHandler(this.txtQty_KeyDown);
            this.lblSupplyID.Location = new Point(0xcb, 0x1b);
            this.lblSupplyID.Name = "lblSupplyID";
            this.lblSupplyID.Size = new Size(0x22, 20);
            this.lblSupplyID.Visible = false;
            this.lblDate.Location = new Point(0xcb, 0x1f);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new Size(0x17, 20);
            this.lblDate.Visible = false;
            this.TasksTitile.BackColor = SystemColors.HotTrack;
            this.TasksTitile.Font = new System.Drawing.Font("Arial", 14f, FontStyle.Regular);
            this.TasksTitile.ForeColor = SystemColors.ActiveCaptionText;
            this.TasksTitile.Location = new Point(0, 0);
            this.TasksTitile.Name = "TasksTitile";
            this.TasksTitile.Size = new Size(0xed, 0x18);
            this.TasksTitile.TabIndex = 0x33;
            this.TasksTitile.Text = "按订单收货";
            this.lblSingleID.Location = new Point(0x89, 10);
            this.lblSingleID.Name = "lblSingleID";
            this.lblSingleID.Size = new Size(0x38, 10);
            this.lblSingleID.Visible = false;
            this.lblProid.Location = new Point(0xb6, 0);
            this.lblProid.Name = "lblProid";
            this.lblProid.Size = new Size(0x2c, 20);
            this.lblProid.Visible = false;
            base.AutoScaleDimensions = new SizeF(96f, 96f);
            //base.AutoScaleMode = AutoScaleMode.Dpi;
            this.AutoScroll = true;
            base.ClientSize = new Size(240, 320);
            base.ControlBox = false;
            base.Controls.Add(this.lblProid);
            base.Controls.Add(this.lblSingleID);
            base.Controls.Add(this.lblDate);
            base.Controls.Add(this.lblSupplyID);
            base.Controls.Add(this.txtQty);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.lblSupplyname);
            base.Controls.Add(this.btnUpdateQty);
            base.Controls.Add(this.lsvTasks);
            base.Controls.Add(this.btnExit);
            base.Controls.Add(this.btnSubmit);
            base.Controls.Add(this.btnQuery);
            base.Controls.Add(this.txtOrderID);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.TasksTitile);
            //base.FormBorderStyle = FormBorderStyle.None;
            base.KeyPreview = true;
            base.Name = "CheckInByOrderFrm";
            this.Text = "按订单收货";
            base.Deactivate += new EventHandler(this.CheckInByOrderFrm_Deactivate);
            base.Load += new EventHandler(this.CheckInByOrderFrm_Load);
            base.Activated += new EventHandler(this.CheckInByOrderFrm_Activated);
            base.KeyDown += new KeyEventHandler(this.CheckInByOrderFrm_KeyDown);
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

        private void MPK1280Print(string port, string baud, DataSet PrintRecord)
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
                mpk.PrintStrLine("采购订单：" + this.txtOrderID.Text);
                mpk.FontZoom(1, 1);
                mpk.SetSnapMode(0);
                mpk.Feed(20);
                mpk.PrintStrLine("供应商：" + this.lblSupplyname.Text);
                mpk.PrintStr("工厂：" + Management.GetSingleton().WarehouseNo.Substring(0, 4) + "  库房：" + Management.GetSingleton().WarehouseNo);
                mpk.PrintStrLine("收货人：" + this.userName);
                mpk.PrintStrLine("日期：" + DateTime.Now.ToString("yyyy-MM-dd"));
                mpk.PrintStr("------------------------");
                new MPK1230().FontCompress23(1);
                for (int i = 0; i < PrintRecord.Tables[0].Rows.Count; i++)
                {
                    str = PrintRecord.Tables[0].Rows[i]["FINISHQTY"].ToString();
                    str5 = PrintRecord.Tables[0].Rows[i]["UNIT"].ToString();
                    str2 = PrintRecord.Tables[0].Rows[i]["MATCODE"].ToString();
                    str3 = PrintRecord.Tables[0].Rows[i]["MATNAME"].ToString();
                    if (str3.Length < 0x10)
                    {
                        str4 = str3;
                    }
                    else
                    {
                        str4 = str3.Substring(0, 0x10);
                    }
                    mpk.PrintStrLine(str4);
                    mpk.FontUnderLine(2);
                    mpk.PrintStrLine(str2.PadRight(0x12, ' ') + str + str5);
                    mpk.FontUnderLine(0);
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

        private void print(DataSet PrintRecord)
        {
            IniFile file = new IniFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName) + "/PrintTest.ini");
            string port = file.ReadValue("Print", "PrintPort", "COM7");
            file.ReadValue("Print", "PrintModel", "MPK1230E");
            string baud = file.ReadValue("Print", "PrintBaud", "115200");
            this.MPK1280Print(port, baud, PrintRecord);
        }

        private void txtOrderID_GotFocus(object sender, EventArgs e)
        {
            OEM_Catchwell.SetOpMode(0);
        }

        private void txtOrderID_KeyDown(object sender, KeyEventArgs e)
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
                this.btnUpdateQty_Click(sender, e);
            }
        }
    }
}

