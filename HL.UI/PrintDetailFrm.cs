namespace HL.UI
{
    using BizLayer;
    using Entity;
    using Printlib;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using System.Threading;
    using System.Windows.Forms;
    using HL.Framework;
    using HL.Controls;

    public class PrintDetailFrm : Form
    {
        private Button btnExit;
        private Button btnPrint;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private IContainer components;
        private DataSet DSprint = new DataSet();
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label lblDate;
        private Label lblOrderno;
        private Label lblQty;
        private Label LblSAPNo;
        private Label lblSupplyname;
        private Label lblUserid;
        private Label lblWarehouse;
        private ListView lsvTasks;
        private string orderID = string.Empty;
        private string printDate = string.Empty;
        private string printLoc = string.Empty;
        private string printNo = string.Empty;
        private int printTimes;
        private string proofID = string.Empty;
        private MiddleService service = Global.MiddleService;
        private string supplyName = string.Empty;
        private HeadLabel TasksTitile;
        private string userID = string.Empty;
        private string xuleiHao = string.Empty;

        public PrintDetailFrm(string proofid, string orderid, string printno, string printdate, string supplyname, string printloc, string userid, string xuleihao, DataSet dsprint)
        {
            this.InitializeComponent();
            this.proofID = proofid;
            this.orderID = orderid;
            this.printNo = printno;
            this.printDate = printdate;
            this.supplyName = supplyname;
            this.printLoc = printloc;
            this.userID = userid;
            this.xuleiHao = xuleihao;
            this.DSprint = dsprint;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (!this.isOpenPort())
            {
                if (MessageBox.Show("打开蓝牙打印机失败，现在要连接打印机吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    new ConfigMain().btnConfigPrinter_Click(null, null);
                }
            }
            else if (this.print())
            {
                this.UpdateTimes();
                base.Close();
                base.DialogResult = DialogResult.OK;
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
            this.label3 = new Label();
            this.label4 = new Label();
            this.label5 = new Label();
            this.label6 = new Label();
            this.TasksTitile = new HeadLabel();
            this.LblSAPNo = new Label();
            this.lblQty = new Label();
            this.lblOrderno = new Label();
            this.lblSupplyname = new Label();
            this.lblWarehouse = new Label();
            this.lblUserid = new Label();
            this.lsvTasks = new ListView();
            this.columnHeader1 = new ColumnHeader();
            this.columnHeader3 = new ColumnHeader();
            this.columnHeader4 = new ColumnHeader();
            this.columnHeader2 = new ColumnHeader();
            this.btnPrint = new Button();
            this.btnExit = new Button();
            this.label7 = new Label();
            this.lblDate = new Label();
            base.SuspendLayout();
            this.label1.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Regular);
            this.label1.Location = new Point(4, 0x1f);
            this.label1.Name = "label1";
            this.label1.Size = new Size(70, 20);
            this.label1.Text = "SAP凭证：";
            this.label2.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Regular);
            this.label2.Location = new Point(0xa4, 0x1b);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x2d, 20);
            this.label2.Text = "次数：";
            this.label3.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Regular);
            this.label3.Location = new Point(4, 0x36);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x54, 20);
            this.label3.Text = "采购订单：";
            this.label4.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Regular);
            this.label4.Location = new Point(4, 0x4d);
            this.label4.Name = "label4";
            this.label4.Size = new Size(70, 20);
            this.label4.Text = "供应商：";
            this.label5.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Regular);
            this.label5.Location = new Point(4, 100);
            this.label5.Name = "label5";
            this.label5.Size = new Size(70, 20);
            this.label5.Text = "库房:";
            this.label6.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Regular);
            this.label6.Location = new Point(4, 0x7b);
            this.label6.Name = "label6";
            this.label6.Size = new Size(70, 20);
            this.label6.Text = "收货人：";
            this.TasksTitile.BackColor = SystemColors.HotTrack;
            this.TasksTitile.Font = new System.Drawing.Font("Arial", 14f, FontStyle.Regular);
            this.TasksTitile.ForeColor = SystemColors.ActiveCaptionText;
            this.TasksTitile.Location = new Point(0, 0);
            this.TasksTitile.Name = "TasksTitile";
            this.TasksTitile.Size = new Size(240, 0x18);
            this.TasksTitile.TabIndex = 0x3b;
            this.TasksTitile.Text = "收货记录";
            this.LblSAPNo.Location = new Point(0x40, 0x1b);
            this.LblSAPNo.Name = "LblSAPNo";
            this.LblSAPNo.Size = new Size(0x53, 20);
            this.lblQty.Location = new Point(0xc9, 0x1b);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new Size(0x27, 20);
            this.lblOrderno.Location = new Point(0x40, 0x33);
            this.lblOrderno.Name = "lblOrderno";
            this.lblOrderno.Size = new Size(100, 20);
            this.lblSupplyname.Location = new Point(0x33, 0x4d);
            this.lblSupplyname.Name = "lblSupplyname";
            this.lblSupplyname.Size = new Size(0xba, 20);
            this.lblWarehouse.Location = new Point(40, 100);
            this.lblWarehouse.Name = "lblWarehouse";
            this.lblWarehouse.Size = new Size(0x42, 20);
            this.lblUserid.Location = new Point(0x40, 0x7b);
            this.lblUserid.Name = "lblUserid";
            this.lblUserid.Size = new Size(100, 20);
            this.lsvTasks.Columns.Add(this.columnHeader1);
            this.lsvTasks.Columns.Add(this.columnHeader3);
            this.lsvTasks.Columns.Add(this.columnHeader4);
            this.lsvTasks.Columns.Add(this.columnHeader2);
            this.lsvTasks.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Regular);
            this.lsvTasks.FullRowSelect = true;
            this.lsvTasks.Location = new Point(0, 0xb5);
            this.lsvTasks.Name = "lsvTasks";
            this.lsvTasks.Size = new Size(0xed, 0x6a);
            this.lsvTasks.TabIndex = 80;
            this.lsvTasks.View = View.Details;
            this.columnHeader1.Text = "物料";
            this.columnHeader1.Width = 130;
            this.columnHeader3.Text = "数量";
            this.columnHeader3.Width = 50;
            this.columnHeader4.Text = "单位";
            this.columnHeader4.Width = 50;
            this.columnHeader2.Text = "物料名称";
            this.columnHeader2.Width = 0;
            this.btnPrint.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Regular);
            this.btnPrint.Location = new Point(0x10, 0x92);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new Size(0x48, 0x1d);
            this.btnPrint.TabIndex = 0x51;
            this.btnPrint.Text = "打印";
            this.btnPrint.Click += new EventHandler(this.btnPrint_Click);
            this.btnExit.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Regular);
            this.btnExit.Location = new Point(0x89, 0x92);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new Size(0x48, 0x1d);
            this.btnExit.TabIndex = 0x52;
            this.btnExit.Text = "返回";
            this.btnExit.Click += new EventHandler(this.btnExit_Click);
            this.label7.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Regular);
            this.label7.Location = new Point(0x71, 100);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x33, 20);
            this.label7.Text = "日期：";
            this.lblDate.Location = new Point(0x98, 100);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new Size(0x55, 20);
            base.AutoScaleDimensions = new SizeF(96f, 96f);
            //base.AutoScaleMode = AutoScaleMode.Dpi;
            this.AutoScroll = true;
            base.ClientSize = new Size(240, 290);
            base.ControlBox = false;
            base.Controls.Add(this.lblDate);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.btnExit);
            base.Controls.Add(this.btnPrint);
            base.Controls.Add(this.lsvTasks);
            base.Controls.Add(this.lblUserid);
            base.Controls.Add(this.lblWarehouse);
            base.Controls.Add(this.lblSupplyname);
            base.Controls.Add(this.lblOrderno);
            base.Controls.Add(this.lblQty);
            base.Controls.Add(this.LblSAPNo);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.TasksTitile);
            //base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "PrintDetailFrm";
            this.Text = "收货记录";
            base.Load += new EventHandler(this.PrintDetailFrm_Load);
            base.ResumeLayout(false);
        }

        private void InitializePrint()
        {
            this.LblSAPNo.Text = this.proofID;
            this.lblOrderno.Text = this.orderID;
            this.lblQty.Text = this.printNo;
            this.lblSupplyname.Text = this.supplyName;
            this.lblWarehouse.Text = this.printLoc;
            this.lblUserid.Text = this.userID;
            this.lblDate.Text = this.printDate;
            DataTable table = this.DSprint.Tables[0];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem(new string[] { table.Rows[i]["GDSID"].ToString(), table.Rows[i]["TIRFINIQTY"].ToString(), table.Rows[i]["TIRUNIT"].ToString(), table.Rows[i]["MATNAME"].ToString() });
                this.lsvTasks.Items.Add(item);
            }
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

        private bool MPK1280Print(string port, string baud)
        {
            this.printTimes = Convert.ToInt32(this.lblQty.Text.ToString()) + 1;
            MPK1280 mpk = new MPK1280();
            mpk.portbaudrate = int.Parse(baud);
            if (!mpk.OpenPort(port))
            {
                MessageBox.Show("打开蓝牙打印机串口失败");
                return false;
            }
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
            mpk.PrintStrLine("凭证:" + this.LblSAPNo.Text + "  次数:" + this.printTimes.ToString());
            mpk.PrintStrLine("采购订单:" + this.lblOrderno.Text);
            mpk.FontZoom(1, 1);
            mpk.SetSnapMode(0);
            mpk.Feed(20);
            mpk.PrintStrLine("供应商：" + this.lblSupplyname.Text);
            mpk.PrintStr("工厂：" + this.lblWarehouse.Text.Substring(0, 4) + "  库房：" + this.lblWarehouse.Text);
            mpk.PrintStrLine("收货人：" + this.lblUserid.Text);
            mpk.PrintStrLine("日期：" + this.lblDate.Text.ToString());
            mpk.PrintStr("------------------------");
            new MPK1230().FontCompress23(1);
            for (int i = 0; i < this.lsvTasks.Items.Count; i++)
            {
                string text = this.lsvTasks.Items[i].SubItems[0].Text;
                string str2 = this.lsvTasks.Items[i].SubItems[1].Text;
                string str3 = this.lsvTasks.Items[i].SubItems[2].Text;
                string str = this.lsvTasks.Items[i].SubItems[3].Text;
                if (str.Length < 0x10)
                {
                    mpk.PrintStrLine(str);
                    mpk.FontUnderLine(2);
                    mpk.PrintStrLine(text.PadRight(0x12, ' ') + str2.ToString() + str3);
                    mpk.FontUnderLine(0);
                }
                else
                {
                    mpk.PrintStrLine(str.Substring(0, 0x10));
                    mpk.FontUnderLine(2);
                    mpk.PrintStrLine(text.PadRight(0x12, ' ') + str2.ToString() + str3);
                    mpk.FontUnderLine(0);
                }
            }
            mpk.SetSnapMode(1);
            mpk.PrintStrLine("------End-----");
            mpk.PrintStrLine("             ");
            mpk.PrintCR();
            mpk.Feed(100);
            Thread.Sleep(0x3e8);
            mpk.ClosePort();
            return true;
        }

        private bool print()
        {
            IniFile file = new IniFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName) + "/PrintTest.ini");
            string port = file.ReadValue("Print", "PrintPort", "COM7");
            file.ReadValue("Print", "PrintModel", "MPK1230E");
            string baud = file.ReadValue("Print", "PrintBaud", "115200");
            return this.MPK1280Print(port, baud);
        }

        private void PrintDetailFrm_Load(object sender, EventArgs e)
        {
            this.InitializePrint();
        }

        private void UpdateTimes()
        {
            string msgError = string.Empty;
            Cursor.Current = Cursors.WaitCursor;
            int num = -1;
            try
            {
                num = this.service.UpdatePrint(this.printTimes, this.xuleiHao, ref msgError);
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
            if (num != 0)
            {
                MessageShow.Alert("提示", msgError);
            }
        }
    }
}

