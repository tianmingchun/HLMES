namespace HL.UI
{
    using BizLayer;
    using Entity;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using HL.Framework;
    using HL.Controls;

    public class PartConfigFrm : Form
    {
        private Button BtnConfig;
        private Button btnExit;
        private IContainer components;
        private Label label1;
        private Management management;
        private const int RET_KEYSTROKE = 0;
        private MiddleService service = Global.MiddleService;
        private HeadLabel TasksTitile;
        private TextBox txtPart;

        public PartConfigFrm()
        {
            this.InitializeComponent();
        }

        private void BtnConfig_Click(object sender, EventArgs e)
        {
            bool flag = false;
            string msgError = string.Empty;
            if (this.txtPart.Text == string.Empty)
            {
                MessageShow.Alert("error", "事业部代码不能为空");
                this.txtPart.Focus();
                this.txtPart.SelectAll();
            }
            else
            {
                try
                {
                    flag = this.service.GetWorkshoppriview(Global.CurrUser.Name, this.txtPart.Text, ref msgError);
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
                if (flag)
                {
                    try
                    {
                        this.management.SaveConfig(this.txtPart.Text, EnumSaveContent.Part);
                        base.Close();
                        return;
                    }
                    catch (Exception exception3)
                    {
                        MessageShow.Alert("error", exception3.ToString());
                        return;
                    }
                }
                MessageShow.Alert("error", "此用户无" + this.txtPart.Text + "事业部权限");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            base.Close();
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
            this.btnExit = new Button();
            this.BtnConfig = new Button();
            this.txtPart = new TextBox();
            this.label1 = new Label();
            this.TasksTitile = new HeadLabel();
            base.SuspendLayout();
            this.btnExit.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnExit.Location = new Point(130, 0xa7);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new Size(80, 30);
            this.btnExit.TabIndex = 0x10;
            this.btnExit.Text = "取消";
            this.btnExit.Click += new EventHandler(this.btnExit_Click);
            this.BtnConfig.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.BtnConfig.Location = new Point(0x1a, 0xa7);
            this.BtnConfig.Name = "BtnConfig";
            this.BtnConfig.Size = new Size(80, 30);
            this.BtnConfig.TabIndex = 15;
            this.BtnConfig.Text = "确定";
            this.BtnConfig.Click += new EventHandler(this.BtnConfig_Click);
            this.txtPart.Location = new Point(0x1a, 0x5c);
            this.txtPart.Name = "txtPart";
            this.txtPart.Size = new Size(0xb8, 0x17);
            this.txtPart.TabIndex = 14;
            this.txtPart.KeyDown += new KeyEventHandler(this.txtPart_KeyDown);
            this.label1.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label1.Location = new Point(0x1a, 0x45);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x4e, 20);
            this.label1.Text = "事业部：";
            this.TasksTitile.BackColor = SystemColors.HotTrack;
            this.TasksTitile.Font = new Font("Arial", 14f, FontStyle.Regular);
            this.TasksTitile.ForeColor = SystemColors.ActiveCaptionText;
            this.TasksTitile.Location = new Point(0, 1);
            this.TasksTitile.Name = "TasksTitile";
            this.TasksTitile.Size = new Size(240, 0x18);
            this.TasksTitile.TabIndex = 13;
            this.TasksTitile.Text = "事业部设置";
            base.AutoScaleDimensions = new SizeF(96f, 96f);
            //base.AutoScaleMode = AutoScaleMode.Dpi;
            this.AutoScroll = true;
            base.ClientSize = new Size(240, 290);
            base.ControlBox = false;
            base.Controls.Add(this.btnExit);
            base.Controls.Add(this.BtnConfig);
            base.Controls.Add(this.txtPart);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.TasksTitile);
            //base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "PartConfigFrm";
            this.Text = "部分设置";
            base.Load += new EventHandler(this.PartConfigFrm_Load);
            base.ResumeLayout(false);
        }

        private void PartConfigFrm_Load(object sender, EventArgs e)
        {
            this.txtPart.Focus();
            this.management = Management.GetSingleton();
            this.txtPart.Text = this.management.PartNo;
            this.txtPart.Focus();
            this.txtPart.SelectAll();
        }

        private void txtPart_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                this.BtnConfig_Click(null, null);
            }
        }
    }
}

