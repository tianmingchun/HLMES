namespace HL.UI
{
    using BizLayer;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using HL.Framework;

    public class InstallForm : Form
    {
        private Button button1;
        private Button button2;
        private IContainer components;
        private Label label1;
        private Management management;
        private ProgressBar pbDownload;
        private MiddleService service = Global.MiddleService;

        public InstallForm()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("现在退出采集系统，进行版本更新吗？", "版本更新", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                this.management = Management.GetSingleton();
                string uriString = this.management.DefaultBaseUrl + "/" + this.service.GetCabPath();
                string fileName = @"\Storage\AutoInstall\" + this.service.GetCabPath();
                try
                {
                    FileInfo localFile = new FileInfo(fileName);
                    HttpFileTrans trans = new HttpFileTrans(new Uri(uriString), localFile);
                    trans.Transing += new EventHandler(this.transing);
                    this.label1.Visible = true;
                    this.pbDownload.Visible = true;
                    trans.Download();
                    this.label1.Visible = false;
                    this.pbDownload.Visible = false;
                }
                catch
                {
                    MessageBox.Show("更新失败。", "版本更新");
                    return;
                }
                MessageBox.Show("下载完成，将退出系统并进行安装。请在安装完成后重新启动。", "版本更新", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                try
                {
                    base.DialogResult = DialogResult.Abort;
                }
                catch
                {
                    base.DialogResult = DialogResult.Cancel;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
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
            this.button1 = new Button();
            this.button2 = new Button();
            this.label1 = new Label();
            this.pbDownload = new ProgressBar();
            base.SuspendLayout();
            this.button1.Location = new Point(0x23, 0x5e);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x48, 30);
            this.button1.TabIndex = 0;
            this.button1.Text = "版本更新";
            this.button1.Click += new EventHandler(this.button1_Click);
            this.button2.Location = new Point(0x86, 0x5e);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x48, 30);
            this.button2.TabIndex = 1;
            this.button2.Text = "退出";
            this.button2.Click += new EventHandler(this.button2_Click);
            this.label1.Location = new Point(0x23, 0x9a);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x71, 20);
            this.label1.Text = "正在下载。。。";
            this.pbDownload.Location = new Point(0x23, 0xb1);
            this.pbDownload.Name = "pbDownload";
            this.pbDownload.Size = new Size(0xab, 20);
            base.AutoScaleDimensions = new SizeF(96f, 96f);
            //base.AutoScaleMode = AutoScaleMode.Dpi;
            this.AutoScroll = true;
            base.ClientSize = new Size(240, 320);
            base.ControlBox = false;
            base.Controls.Add(this.pbDownload);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.button1);
            //base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "InstallForm";
            this.Text = "InstallForm";
            base.ResumeLayout(false);
        }

        private void transing(object sender, EventArgs e)
        {
            this.pbDownload.Value = (int) sender;
            this.Refresh();
        }
    }
}

