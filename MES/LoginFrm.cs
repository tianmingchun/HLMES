namespace MES
{
    using BizLayer;
    using Entity;
    using Microsoft.WindowsCE.Forms;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Net;
    using System.Windows.Forms;
    using HL.Framework;

    public class LoginFrm : Form
    {
        private Button btnAdvan;
        private Button btnLogin;
        private Button btnLogout;
        private Button btnModify;
        private Button btnTest;
        private IContainer components;
        private InputPanel inputPanel1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Management management;
        private MiddleService service;
        private TabControl tabControl1;
        private TextBox tbAgain;
        private TextBox tbNewPwd;
        private TextBox tbPwd;
        private TextBox tbUrl;
        private TextBox tbUserID;
        private TabPage tpChangePwd;
        private TabPage tpConfig;

        public LoginFrm()
        {
            this.InitializeComponent();
            this.service = new MiddleService();
        }

        private void btnAdvan_Click(object sender, EventArgs e)
        {
            if (this.tabControl1.Visible)
            {
                this.tabControl1.Hide();
                this.btnAdvan.Text = "高级 >>";
                this.btnLogin.Text = "登录";
            }
            else
            {
                this.tabControl1.Show();
                this.btnAdvan.Text = "标准 <<";
                this.btnLogin.Text = "确定";
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userID = this.tbUserID.Text.Trim();
            string pwd = this.tbPwd.Text.Trim();
            DataSet set = new DataSet();
            if (userID == "")
            {
                MessageShow.Alert("错误", "用户名不能为空！");
            }
            else
            {
                this.tbPwd.Text = "";
                UpdateWaitingFrm frm = new UpdateWaitingFrm(userID, pwd);
                frm.Location = new Point(120 - (frm.Width / 2), 160 - (frm.Height / 2));
                DialogResult result = frm.ShowDialog();
                frm.Dispose();
                if (result != DialogResult.Cancel)
                {
                    try
                    {
                        Global.CurrUserpriview = new UserPriview(Global.MiddleService.GetPDAPriview(userID));
                    }
                    catch (ApplicationException exception)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageShow.Alert("Error", "获取用户权限失败" + exception.Message.ToString());
                        return;
                    }
                    catch (Exception exception2)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageShow.Alert("Error", "获取用户权限失败" + exception2.Message.ToString());
                        return;
                    }
                    MainForm frm2 = new MainForm();
                    frm2.ShowDialog();
                    frm2.Dispose();
                }
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确定要退出采集程序吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                this.management.SaveLocationCfg();
                base.Close();
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (this.tbUserID.Text == string.Empty)
            {
                MessageShow.Alert("Error", "请输入用户名！");
            }
            else if (this.tbNewPwd.Text == string.Empty)
            {
                MessageShow.Alert("Error", "新密码不能为空！");
            }
            else if (!this.VerifyNewPassword(this.tbNewPwd.Text, this.tbAgain.Text))
            {
                MessageShow.Alert("Error", "两次输入的密码不一致！");
            }
            else
            {
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    Global.MiddleService.ChangePassword(this.tbUserID.Text.Trim(), this.tbPwd.Text.Trim(), this.tbNewPwd.Text.Trim());
                }
                catch (ApplicationException exception)
                {
                    Cursor.Current = Cursors.Default;
                    if (exception.InnerException is WebException)
                    {
                        MessageShow.Alert("Error", "无法连接到服务器，修改密码必须在联机情况下进行！");
                    }
                    else
                    {
                        MessageShow.Alert("Error", exception.Message);
                    }
                    return;
                }
                catch (Exception exception2)
                {
                    Cursor.Current = Cursors.Default;
                    MessageShow.Alert("未知错误", exception2.Message);
                    return;
                }
                Cursor.Current = Cursors.Default;
                MessageShow.Alert("修改成功", "修改密码成功！");
                this.tabControl1.Hide();
                this.btnAdvan.Text = "高级 >>";
                this.btnLogin.Text = "登录";
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            this.ConnectionTest();
        }

        private void ConnectionTest()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                Global.MiddleService.ConnectionTest(this.tbUrl.Text);
            }
            catch (ApplicationException exception)
            {
                Cursor.Current = Cursors.Default;
                MessageShow.Alert("Error", exception.Message);
                return;
            }
            this.management.SaveConfig(this.tbUrl.Text, EnumSaveContent.Url);
            Cursor.Current = Cursors.Default;
            Global.MiddleService.ServiceUrl = this.tbUrl.Text;
            MessageShow.Alert("Success", "测试连接成功，地址已保存");
            this.tabControl1.Hide();
            this.btnAdvan.Text = "高级 >>";
            this.btnLogin.Text = "登录";
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpChangePwd = new System.Windows.Forms.TabPage();
            this.btnModify = new System.Windows.Forms.Button();
            this.tbAgain = new System.Windows.Forms.TextBox();
            this.tbNewPwd = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tpConfig = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.btnTest = new System.Windows.Forms.Button();
            this.tbUrl = new System.Windows.Forms.TextBox();
            this.btnAdvan = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.tbPwd = new System.Windows.Forms.TextBox();
            this.tbUserID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel();
            this.tabControl1.SuspendLayout();
            this.tpChangePwd.SuspendLayout();
            this.tpConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpChangePwd);
            this.tabControl1.Controls.Add(this.tpConfig);
            this.tabControl1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.tabControl1.Location = new System.Drawing.Point(6, 152);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(224, 130);
            this.tabControl1.TabIndex = 15;
            this.tabControl1.Visible = false;
            // 
            // tpChangePwd
            // 
            this.tpChangePwd.Controls.Add(this.btnModify);
            this.tpChangePwd.Controls.Add(this.tbAgain);
            this.tpChangePwd.Controls.Add(this.tbNewPwd);
            this.tpChangePwd.Controls.Add(this.label4);
            this.tpChangePwd.Controls.Add(this.label3);
            this.tpChangePwd.Location = new System.Drawing.Point(4, 23);
            this.tpChangePwd.Name = "tpChangePwd";
            this.tpChangePwd.Size = new System.Drawing.Size(216, 103);
            this.tpChangePwd.Text = "修改密码";
            // 
            // btnModify
            // 
            this.btnModify.Location = new System.Drawing.Point(132, 70);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(72, 30);
            this.btnModify.TabIndex = 6;
            this.btnModify.Text = "修改";
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // tbAgain
            // 
            this.tbAgain.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.tbAgain.Location = new System.Drawing.Point(90, 43);
            this.tbAgain.MaxLength = 8;
            this.tbAgain.Name = "tbAgain";
            this.tbAgain.PasswordChar = '*';
            this.tbAgain.Size = new System.Drawing.Size(100, 21);
            this.tbAgain.TabIndex = 3;
            this.tbAgain.GotFocus += new System.EventHandler(this.tb_FocusChanged);
            this.tbAgain.LostFocus += new System.EventHandler(this.tb_FocusChanged);
            // 
            // tbNewPwd
            // 
            this.tbNewPwd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.tbNewPwd.Location = new System.Drawing.Point(90, 16);
            this.tbNewPwd.MaxLength = 8;
            this.tbNewPwd.Name = "tbNewPwd";
            this.tbNewPwd.PasswordChar = '*';
            this.tbNewPwd.Size = new System.Drawing.Size(100, 21);
            this.tbNewPwd.TabIndex = 2;
            this.tbNewPwd.GotFocus += new System.EventHandler(this.tb_FocusChanged);
            this.tbNewPwd.LostFocus += new System.EventHandler(this.tb_FocusChanged);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.label4.Location = new System.Drawing.Point(2, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 20);
            this.label4.Text = "再输一遍:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 20);
            this.label3.Text = "新密码:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tpConfig
            // 
            this.tpConfig.Controls.Add(this.label6);
            this.tpConfig.Controls.Add(this.btnTest);
            this.tpConfig.Controls.Add(this.tbUrl);
            this.tpConfig.Location = new System.Drawing.Point(4, 23);
            this.tpConfig.Name = "tpConfig";
            this.tpConfig.Size = new System.Drawing.Size(216, 103);
            this.tpConfig.Text = "设置服务器";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.label6.Location = new System.Drawing.Point(3, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(139, 20);
            this.label6.Text = "服务器地址(URL)：";
            // 
            // btnTest
            // 
            this.btnTest.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.btnTest.Location = new System.Drawing.Point(118, 62);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(72, 32);
            this.btnTest.TabIndex = 1;
            this.btnTest.Text = "连接";
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // tbUrl
            // 
            this.tbUrl.Location = new System.Drawing.Point(3, 33);
            this.tbUrl.Name = "tbUrl";
            this.tbUrl.Size = new System.Drawing.Size(210, 23);
            this.tbUrl.TabIndex = 0;
            this.tbUrl.Text = "http://";
            this.tbUrl.GotFocus += new System.EventHandler(this.tb_FocusChanged);
            this.tbUrl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbUrl_KeyDown);
            this.tbUrl.LostFocus += new System.EventHandler(this.tb_FocusChanged);
            // 
            // btnAdvan
            // 
            this.btnAdvan.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.btnAdvan.Location = new System.Drawing.Point(160, 104);
            this.btnAdvan.Name = "btnAdvan";
            this.btnAdvan.Size = new System.Drawing.Size(72, 32);
            this.btnAdvan.TabIndex = 14;
            this.btnAdvan.Text = "高级>>";
            this.btnAdvan.Click += new System.EventHandler(this.btnAdvan_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.btnLogout.Location = new System.Drawing.Point(82, 104);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(72, 32);
            this.btnLogout.TabIndex = 13;
            this.btnLogout.Text = "退出";
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.btnLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnLogin.Location = new System.Drawing.Point(5, 104);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(72, 32);
            this.btnLogin.TabIndex = 12;
            this.btnLogin.Text = "登录";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // tbPwd
            // 
            this.tbPwd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.tbPwd.Location = new System.Drawing.Point(100, 72);
            this.tbPwd.MaxLength = 8;
            this.tbPwd.Name = "tbPwd";
            this.tbPwd.PasswordChar = '*';
            this.tbPwd.Size = new System.Drawing.Size(100, 21);
            this.tbPwd.TabIndex = 11;
            this.tbPwd.GotFocus += new System.EventHandler(this.tb_FocusChanged);
            this.tbPwd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_EnterKeyDown);
            this.tbPwd.LostFocus += new System.EventHandler(this.tb_FocusChanged);
            // 
            // tbUserID
            // 
            this.tbUserID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.tbUserID.Location = new System.Drawing.Point(100, 45);
            this.tbUserID.Name = "tbUserID";
            this.tbUserID.Size = new System.Drawing.Size(100, 21);
            this.tbUserID.TabIndex = 10;
            this.tbUserID.GotFocus += new System.EventHandler(this.tb_FocusChanged);
            this.tbUserID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_EnterKeyDown);
            this.tbUserID.LostFocus += new System.EventHandler(this.tb_FocusChanged);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(39, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 20);
            this.label2.Text = "密码:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(36, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 22);
            this.label1.Text = "用户名:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular);
            this.label5.Location = new System.Drawing.Point(7, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(225, 28);
            this.label5.Text = "仓库无线采集系统";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // LoginFrm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.ControlBox = false;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnAdvan);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.tbPwd);
            this.Controls.Add(this.tbUserID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginFrm";
            this.Text = "登录";
            this.Load += new System.EventHandler(this.LoginFrm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tpChangePwd.ResumeLayout(false);
            this.tpConfig.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void InitializeForm()
        {
            this.tbPwd.Text = "";
            this.tbNewPwd.Text = "";
            this.tbAgain.Text = "";
        }

        private void LoginFrm_Load(object sender, EventArgs e)
        {
            try
            {
                Global.MiddleService = new MiddleService();
                this.management = Management.GetSingleton();
                this.tbUrl.Text = this.management.DefaultBaseUrl;
                Global.BarcodeType = EnumBarcodeType.Separate;
            }
            catch (ApplicationException exception)
            {
                MessageShow.Alert("配置错误", exception.Message);
                base.Close();
                return;
            }
            catch
            {
                MessageShow.Alert("配置错误", "配置文件错误，请更新正确的配置文件！");
                base.Close();
                return;
            }
            if (!Directory.Exists(@"\Storage\AutoInstall"))
            {
                Directory.CreateDirectory(@"\Storage\AutoInstall");
            }
            string path = @"\Storage\AutoInstall\bilin WMSWireless.cab";
            if (!System.IO.File.Exists(path))
            {
                try
                {
                    string uriString = this.management.DefaultBaseUrl + "/bilin WMSWireless.cab";
                    FileInfo localFile = new FileInfo(path);
                    new HttpFileTrans(new Uri(uriString), localFile).Download();
                    IntPtr hFile = ShellExecute.CreateFile(@"\Storage\AutoInstall\bilin WMSWireless.cab", 0xc0000000, 3, IntPtr.Zero, 3, 0x80, IntPtr.Zero);
                    if (hFile.ToInt32() == -1)
                    {
                        hFile = IntPtr.Zero;
                    }
                    else
                    {
                        DateTime time = Convert.ToDateTime(this.service.GetVersion());
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
                    MessageBox.Show("下载最新版本失败。", "版本更新");
                }
            }
        }

        private void tb_EnterKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                TextBox box = sender as TextBox;
                if (box.Text != "")
                {
                    if (box == this.tbUserID)
                    {
                        this.tbPwd.Focus();
                    }
                    else if (box == this.tbPwd)
                    {
                        if (this.tbUserID.Text == "")
                        {
                            this.tbUserID.Focus();
                        }
                        else
                        {
                            this.btnLogin_Click(this.btnLogin, EventArgs.Empty);
                        }
                    }
                }
            }
        }

        private void tb_FocusChanged(object sender, EventArgs e)
        {
            this.inputPanel1.Enabled = ((Control) sender).Focused;
        }

        private void tbUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                this.ConnectionTest();
            }
        }

        private bool VerifyNewPassword(string pwd1, string pwd2)
        {
            return (pwd1 == pwd2);
        }
    }
}

