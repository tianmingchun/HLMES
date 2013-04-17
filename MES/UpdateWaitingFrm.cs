namespace MES
{
    using BizLayer;
    using Entity;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using HL.Framework;

    public class UpdateWaitingFrm : Form
    {
        private Button btnCancel;
        private Button btnRetry;
        private IContainer components;
        public Label lblMsg;
        public Label lblMsgPlus;
        private string password;
        private Timer timer1;
        private string userName;

        public UpdateWaitingFrm(string userID, string pwd)
        {
            this.password = pwd;
            this.userName = userID;
            this.InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
        }

        private void btnRetry_Click(object sender, EventArgs e)
        {
            this.EnableButtons(false);
            this.timer1_Tick(sender, e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void EnableButtons(bool enabled)
        {
            this.btnCancel.Enabled = enabled;
            this.btnRetry.Enabled = enabled;
        }

        private void InitializeComponent()
        {
            this.lblMsg = new Label();
            this.lblMsgPlus = new Label();
            this.btnRetry = new Button();
            this.btnCancel = new Button();
            this.timer1 = new Timer();
            base.SuspendLayout();
            this.lblMsg.Location = new Point(9, 0x18);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new Size(0xb5, 20);
            this.lblMsgPlus.Location = new Point(9, 0x3a);
            this.lblMsgPlus.Name = "lblMsgPlus";
            this.lblMsgPlus.Size = new Size(0xb5, 20);
            this.btnRetry.Enabled = false;
            this.btnRetry.Location = new Point(0x16, 0x7a);
            this.btnRetry.Name = "btnRetry";
            this.btnRetry.Size = new Size(0x48, 0x1f);
            this.btnRetry.TabIndex = 2;
            this.btnRetry.Text = "重试";
            this.btnRetry.Click += new EventHandler(this.btnRetry_Click);
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new Point(100, 0x7a);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x48, 0x1f);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.timer1.Interval = 500;
            this.timer1.Tick += new EventHandler(this.timer1_Tick);
            //base.AutoScaleMode = AutoScaleMode.Inherit;
            base.ClientSize = new Size(0xc6, 0xaf);
            base.ControlBox = false;
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnRetry);
            base.Controls.Add(this.lblMsgPlus);
            base.Controls.Add(this.lblMsg);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "UpdateWaitingFrm";
            this.Text = "登录";
            base.Load += new EventHandler(this.WaitingFrm_Load);
            base.ResumeLayout(false);
        }

        private bool Login()
        {
            this.lblMsg.Text = "正在登录，请稍候...";
            this.lblMsgPlus.Text = "";
            Application.DoEvents();
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                Global.MiddleService.Login(this.userName, this.password);
            }
            catch (ApplicationException)
            {
                Cursor.Current = Cursors.Default;
                MessageShow.Alert("Error", "登录失败，请重新登录！");
                this.lblMsg.Text = "登录失败！";
                this.EnableButtons(true);
                return false;
            }
            catch (Exception)
            {
                Cursor.Current = Cursors.Default;
                MessageShow.Alert("Error", "登录失败，请重新登录！");
                this.lblMsg.Text = "登录失败！";
                this.EnableButtons(true);
                return false;
            }
            Cursor.Current = Cursors.Default;
            Global.CurrUser = new User(this.userName, this.password);
            this.lblMsg.Text = "登录成功！";
            Application.DoEvents();
            return true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
            if (this.Login())
            {
                try
                {
                    this.lblMsg.Text = "正在获取全局规则信息，请稍候...";
                    Application.DoEvents();
                    Cursor.Current = Cursors.WaitCursor;
                    Management.GetSingleton().LoadGloableRegulation();
                    Cursor.Current = Cursors.Default;
                    this.lblMsg.Text = "获取全局规则信息成功！";
                    Application.DoEvents();
                    base.DialogResult = DialogResult.OK;
                }
                catch (Exception exception)
                {
                    Cursor.Current = Cursors.Default;
                    this.lblMsg.Text = "获取全局规则信息失败！";
                    Application.DoEvents();
                    MessageShow.Alert("Error", exception.Message);
                    base.DialogResult = DialogResult.Cancel;
                }
            }
        }

        private void WaitingFrm_Load(object sender, EventArgs e)
        {
            this.timer1.Enabled = true;
        }
    }
}

