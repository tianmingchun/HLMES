namespace HL.UI
{
    using Entity;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;
    using HL.Framework;
    using HL.Controls;

    public class ConfigMain : BaseForm
    {
        private Button btnConfigPrinter;
        private Button btnConfigWarehous;
        private Button btnExit;
        private Button btnPart;
        private Button btnReprint;
        private IContainer components;
        private HeadLabel TasksTitile;

        public ConfigMain()
        {
            this.InitializeComponent();
        }

        public void btnConfigPrinter_Click(object sender, EventArgs e)
        {
            IntPtr hWnd = Global.FindWindow(null, "BluetoothSPPUI");
            if (hWnd.ToInt32() <= 0)
            {
                string fileName = @"\Storage\BTPrinter\SPPUICN.EXE";
                try
                {
                    Process.Start(fileName, "");
                }
                catch (Exception)
                {
                    MessageBox.Show("无法启动蓝牙配置程序", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                try
                {
                    if (Global.IsWindowVisible(hWnd))
                    {
                        Global.SetForegroundWindow(hWnd);
                    }
                    else
                    {
                        Global.SendMessage(hWnd, 1, IntPtr.Zero, IntPtr.Zero);
                    }
                }
                catch
                {
                    MessageBox.Show("打开错误");
                }
            }
        }

        private void btnConfigWarehous_Click(object sender, EventArgs e)
        {
            ConfigFrm frm = new ConfigFrm();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnPart_Click(object sender, EventArgs e)
        {
            PartConfigFrm frm = new PartConfigFrm();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void btnReprint_Click(object sender, EventArgs e)
        {
            ReprintListFrm frm = new ReprintListFrm();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void ConfigMain_Load(object sender, EventArgs e)
        {
            string str = string.Empty;
            if (Global.CurrUserpriview != null && Global.CurrUserpriview.Priview.Tables.Count >= 1)
            {
                DataTable table = Global.CurrUserpriview.Priview.Tables[0];
                if (table.Rows.Count != 0)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        if ((table.Rows[i][0].ToString() == "ConfigMain") && (table.Rows[i][2].ToString() == "0"))
                        {
                            str = table.Rows[i][1].ToString();
                            if (this.btnPart.Name == str)
                            {
                                this.btnPart.Enabled = false;
                            }
                            else if (this.btnConfigWarehous.Name == str)
                            {
                                this.btnConfigWarehous.Enabled = false;
                            }
                            else if (this.btnConfigPrinter.Name == str)
                            {
                                this.btnConfigPrinter.Enabled = false;
                            }
                            else if (this.btnReprint.Name == str)
                            {
                                this.btnReprint.Enabled = false;
                            }
                        }
                    }
                }
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
            this.btnConfigWarehous = new Button();
            this.btnConfigPrinter = new Button();
            this.btnReprint = new Button();
            this.TasksTitile = new HeadLabel();
            this.btnExit = new Button();
            this.btnPart = new Button();
            base.SuspendLayout();
            this.btnConfigWarehous.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnConfigWarehous.Location = new Point(0x35, 0x57);
            this.btnConfigWarehous.Name = "btnConfigWarehous";
            this.btnConfigWarehous.Size = new Size(0x83, 0x1c);
            this.btnConfigWarehous.TabIndex = 1;
            this.btnConfigWarehous.Text = "设置仓库";
            this.btnConfigWarehous.Click += new EventHandler(this.btnConfigWarehous_Click);
            this.btnConfigPrinter.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnConfigPrinter.Location = new Point(0x35, 0x86);
            this.btnConfigPrinter.Name = "btnConfigPrinter";
            this.btnConfigPrinter.Size = new Size(0x83, 0x1c);
            this.btnConfigPrinter.TabIndex = 2;
            this.btnConfigPrinter.Text = "设置打印机";
            this.btnConfigPrinter.Click += new EventHandler(this.btnConfigPrinter_Click);
            this.btnReprint.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnReprint.Location = new Point(0x35, 0xb6);
            this.btnReprint.Name = "btnReprint";
            this.btnReprint.Size = new Size(0x83, 0x1c);
            this.btnReprint.TabIndex = 3;
            this.btnReprint.Text = "补打小票";
            this.btnReprint.Click += new EventHandler(this.btnReprint_Click);
            this.TasksTitile.BackColor = SystemColors.HotTrack;
            this.TasksTitile.Font = new Font("Arial", 14f, FontStyle.Regular);
            this.TasksTitile.ForeColor = SystemColors.ActiveCaptionText;
            this.TasksTitile.Location = new Point(0, 3);
            this.TasksTitile.Name = "TasksTitile";
            this.TasksTitile.Size = new Size(240, 0x18);
            this.TasksTitile.TabIndex = 8;
            this.TasksTitile.Text = "菜单";
            this.btnExit.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnExit.Location = new Point(0x35, 0xe3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new Size(0x83, 0x1c);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "返回";
            this.btnExit.Click += new EventHandler(this.btnExit_Click);
            this.btnPart.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnPart.Location = new Point(0x35, 0x2c);
            this.btnPart.Name = "btnPart";
            this.btnPart.Size = new Size(0x83, 0x1c);
            this.btnPart.TabIndex = 0;
            this.btnPart.Text = "设置事业部";
            this.btnPart.Click += new EventHandler(this.btnPart_Click);
            base.AutoScaleDimensions = new SizeF(96f, 96f);
            //base.AutoScaleMode = AutoScaleMode.Dpi;
            this.AutoScroll = true;
            base.ClientSize = new Size(240, 290);
            base.ControlBox = false;
            base.Controls.Add(this.btnPart);
            base.Controls.Add(this.btnExit);
            base.Controls.Add(this.TasksTitile);
            base.Controls.Add(this.btnReprint);
            base.Controls.Add(this.btnConfigPrinter);
            base.Controls.Add(this.btnConfigWarehous);
            //base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "ConfigMain";
            this.Text = "系统配置";
            base.Load += new EventHandler(this.ConfigMain_Load);
            base.ResumeLayout(false);
        }
    }
}

