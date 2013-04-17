namespace HL.UI
{
    using BizLayer;
    using Entity;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using HL.Framework;
    using HL.Controls;

    public class MenuFrm : Form
    {
        private Button btnBack;
        private Button btnCheckIn;
        private Button btnCheckOut;
        private Button btnConfig;
        private Button btnInspect;
        private Button btnUpdate;
        private IContainer components;
        private Label label2;
        private string pdtFilePath;
        private string serverFileDateTime;
        private MiddleService service = Global.MiddleService;
        private HeadLabel TasksTitile;
        private bool uploaded;
        private WaitingFrm w = new WaitingFrm();

        public MenuFrm()
        {
            this.InitializeComponent();
            this.pdtFilePath = @"\Storage\AutoInstall\bilin HL.UI.cab";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnBack_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 0x84)
            {
                ConfigFrm frm = new ConfigFrm();
                frm.ShowDialog();
                frm.Dispose();
            }
        }

        private void btnBlueConnect_Click(object sender, EventArgs e)
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

        public void btnCheckIn_Click(object sender, EventArgs e)
        {
            CheckInMain main = new CheckInMain();
            main.ShowDialog();
            main.Dispose();
        }

        public void btnCheckOut_Click(object sender, EventArgs e)
        {
            CheckOutMain main = new CheckOutMain();
            main.ShowDialog();
            main.Dispose();
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            ConfigMain main = new ConfigMain();
            main.ShowDialog();
            main.Dispose();
        }

        private void btnInspect_Click(object sender, EventArgs e)
        {
            InspectMain main = new InspectMain();
            main.ShowDialog();
            main.Dispose();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.checkVersion())
            {
                MessageBox.Show("当前已是最新版本，不需要更新。", "版本更新", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            }
            else if (new InstallForm().ShowDialog() == DialogResult.Abort)
            {
                try
                {
                    IntPtr hFile = ShellExecute.CreateFile(this.pdtFilePath, 0xc0000000, 3, IntPtr.Zero, 3, 0x80, IntPtr.Zero);
                    if (hFile.ToInt32() == -1)
                    {
                        hFile = IntPtr.Zero;
                    }
                    else
                    {
                        DateTime time = Convert.ToDateTime(this.serverFileDateTime);
                        ShellExecute.SystemTime time2 = new ShellExecute.SystemTime();
                        time2.wYear = (ushort) time.ToUniversalTime().Year;
                        time2.wMonth = (ushort) time.ToUniversalTime().Month;
                        time2.wDay = (ushort) time.ToUniversalTime().Day;
                        time2.wHour = (ushort) time.ToUniversalTime().Hour;
                        time2.wMinute = (ushort) time.ToUniversalTime().Minute;
                        time2.wSecond = (ushort) time.ToUniversalTime().Second;
                        time2.wMiliseconds = 1;
                        time2.wDayOfWeek = (ushort) time.ToUniversalTime().DayOfWeek;
                        ShellExecute.FILETIME sysCreateTime = ShellExecute.SystemTimetoFILETIME(time2);
                        ShellExecute.SetFileTime(hFile, ref sysCreateTime, ref sysCreateTime, ref sysCreateTime);
                        ShellExecute.CloseHandle(hFile);
                    }
                }
                catch
                {
                }
                ShellExecute.ExeFile(this.pdtFilePath);
                base.Close();
                Application.Exit();
            }
            else
            {
                GC.Collect();
            }
        }

        private bool checkVersion()
        {
            try
            {
                this.serverFileDateTime = this.service.GetVersion();
                if (this.serverFileDateTime == "")
                {
                    return true;
                }
                if (File.Exists(this.pdtFilePath))
                {
                    if (File.GetLastWriteTime(this.pdtFilePath).ToString("yyyy-MM-dd HH:mm:ss").Substring(0, 0x10) != this.serverFileDateTime.Substring(0, 0x10))
                    {
                        return (MessageBox.Show("发现新版本，版本号为：" + this.serverFileDateTime + "\n是否进行版本更新？", "合力无线采集系统", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes);
                    }
                    return true;
                }
                return (MessageBox.Show("需要下载新版本，版本号为：" + this.serverFileDateTime + "\n是否需要版本更新？", "合力无线采集系统", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes);
            }
            catch
            {
                return true;
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
            this.label2 = new Label();
            this.btnCheckIn = new Button();
            this.btnCheckOut = new Button();
            this.btnInspect = new Button();
            this.btnBack = new Button();
            this.btnConfig = new Button();
            this.btnUpdate = new Button();
            this.TasksTitile = new HeadLabel();
            base.SuspendLayout();
            this.label2.Font = new Font("Tahoma", 14f, FontStyle.Regular);
            this.label2.Location = new Point(0x4b, 0x22);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x5e, 0x1c);
            this.label2.Text = "功能选择";
            this.btnCheckIn.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnCheckIn.Location = new Point(0x38, 0x41);
            this.btnCheckIn.Name = "btnCheckIn";
            this.btnCheckIn.Size = new Size(0x7d, 0x1a);
            this.btnCheckIn.TabIndex = 0;
            this.btnCheckIn.Text = "收货采集";
            this.btnCheckIn.Click += new EventHandler(this.btnCheckIn_Click);
            this.btnCheckOut.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnCheckOut.Location = new Point(0x38, 0x65);
            this.btnCheckOut.Name = "btnCheckOut";
            this.btnCheckOut.Size = new Size(0x7d, 0x1a);
            this.btnCheckOut.TabIndex = 1;
            this.btnCheckOut.Text = "发料采集";
            this.btnCheckOut.Click += new EventHandler(this.btnCheckOut_Click);
            this.btnInspect.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnInspect.Location = new Point(0x38, 0x89);
            this.btnInspect.Name = "btnInspect";
            this.btnInspect.Size = new Size(0x7d, 0x1a);
            this.btnInspect.TabIndex = 2;
            this.btnInspect.Text = "检验采集";
            this.btnInspect.Click += new EventHandler(this.btnInspect_Click);
            this.btnBack.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnBack.Location = new Point(0x38, 0xf5);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new Size(0x7d, 0x1a);
            this.btnBack.TabIndex = 5;
            this.btnBack.Text = "返回";
            this.btnBack.Click += new EventHandler(this.btnBack_Click);
            this.btnBack.KeyDown += new KeyEventHandler(this.btnBack_KeyDown);
            this.btnConfig.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnConfig.Location = new Point(0x38, 0xad);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new Size(0x7d, 0x1a);
            this.btnConfig.TabIndex = 3;
            this.btnConfig.Text = "系统配置";
            this.btnConfig.Click += new EventHandler(this.btnConfig_Click);
            this.btnUpdate.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnUpdate.Location = new Point(0x38, 0xd1);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new Size(0x7d, 0x1a);
            this.btnUpdate.TabIndex = 4;
            this.btnUpdate.Text = "更新程序";
            this.btnUpdate.Click += new EventHandler(this.btnUpdate_Click);
            this.TasksTitile.BackColor = SystemColors.HotTrack;
            this.TasksTitile.Font = new Font("Arial", 14f, FontStyle.Regular);
            this.TasksTitile.ForeColor = SystemColors.ActiveCaptionText;
            this.TasksTitile.Location = new Point(0, 0);
            this.TasksTitile.Name = "TasksTitile";
            this.TasksTitile.Size = new Size(240, 0x18);
            this.TasksTitile.TabIndex = 6;
            this.TasksTitile.Text = "菜单";
            base.AutoScaleDimensions = new SizeF(96f, 96f);
            //base.AutoScaleMode = AutoScaleMode.Dpi;
            this.AutoScroll = true;
            base.ClientSize = new Size(240, 320);
            base.ControlBox = false;
            base.Controls.Add(this.btnUpdate);
            base.Controls.Add(this.btnConfig);
            base.Controls.Add(this.TasksTitile);
            base.Controls.Add(this.btnBack);
            base.Controls.Add(this.btnInspect);
            base.Controls.Add(this.btnCheckOut);
            base.Controls.Add(this.btnCheckIn);
            base.Controls.Add(this.label2);
            //base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "MenuFrm";
            this.Text = "MenuFrm";
            base.Load += new EventHandler(this.MenuFrm_Load);
            base.ResumeLayout(false);
        }

        private void MenuFrm_Load(object sender, EventArgs e)
        {
            if (!this.checkVersion() && (new InstallForm().ShowDialog() == DialogResult.Abort))
            {
                try
                {
                    IntPtr hFile = ShellExecute.CreateFile(this.pdtFilePath, 0xc0000000, 3, IntPtr.Zero, 3, 0x80, IntPtr.Zero);
                    if (hFile.ToInt32() == -1)
                    {
                        hFile = IntPtr.Zero;
                    }
                    else
                    {
                        DateTime time = Convert.ToDateTime(this.serverFileDateTime);
                        ShellExecute.SystemTime time2 = new ShellExecute.SystemTime();
                        time2.wYear = (ushort) time.ToUniversalTime().Year;
                        time2.wMonth = (ushort) time.ToUniversalTime().Month;
                        time2.wDay = (ushort) time.ToUniversalTime().Day;
                        time2.wHour = (ushort) time.ToUniversalTime().Hour;
                        time2.wMinute = (ushort) time.ToUniversalTime().Minute;
                        time2.wSecond = (ushort) time.ToUniversalTime().Second;
                        time2.wMiliseconds = 1;
                        time2.wDayOfWeek = (ushort) time.ToUniversalTime().DayOfWeek;
                        ShellExecute.FILETIME sysCreateTime = ShellExecute.SystemTimetoFILETIME(time2);
                        ShellExecute.SetFileTime(hFile, ref sysCreateTime, ref sysCreateTime, ref sysCreateTime);
                        ShellExecute.CloseHandle(hFile);
                    }
                }
                catch
                {
                }
                ShellExecute.ExeFile(this.pdtFilePath);
                base.Close();
                Application.Exit();
            }
            else
            {
                string str = string.Empty;
                if (Global.CurrUserpriview.Priview.Tables.Count >= 1)
                {
                    DataTable table = Global.CurrUserpriview.Priview.Tables[0];
                    if (table.Rows.Count != 0)
                    {
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            if ((table.Rows[i][0].ToString() == "MenuFrm") && (table.Rows[i][2].ToString() == "0"))
                            {
                                str = table.Rows[i][1].ToString();
                                if (this.btnCheckIn.Name == str)
                                {
                                    this.btnCheckIn.Enabled = false;
                                }
                                else if (this.btnCheckOut.Name == str)
                                {
                                    this.btnCheckOut.Enabled = false;
                                }
                                else if (this.btnInspect.Name == str)
                                {
                                    this.btnInspect.Enabled = false;
                                }
                                else if (this.btnConfig.Name == str)
                                {
                                    this.btnConfig.Enabled = false;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

