namespace HL.UI
{
    using BizLayer;
    using Entity;
    using SharpExCS;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using HL.Framework;
    using HL.Controls;

    public class ConfigFrm : Form
    {
        private Button BtnConfig;
        private Button button2;
        private IContainer components;
        private Label label1;
        private Management management;
        private string partno = string.Empty;
        private const int RET_KEYSTROKE = 0;
        private MiddleService service = Global.MiddleService;
        private string stuid = string.Empty;
        private HeadLabel TasksTitile;
        private TextBox txtWarehouse;

        public ConfigFrm()
        {
            this.InitializeComponent();
        }

        private void BtnConfig_Click(object sender, EventArgs e)
        {
            bool flag = false;
            string msgError = string.Empty;
            this.partno = Management.GetSingleton().PartNo;
            if (this.txtWarehouse.Text == string.Empty)
            {
                MessageShow.Alert("error", "仓库号不能为空");
                this.txtWarehouse.Focus();
                this.txtWarehouse.SelectAll();
            }
            else
            {
                if (this.txtWarehouse.Text.Length == 4)
                {
                    this.stuid = this.partno + this.txtWarehouse.Text;
                }
                else if (this.txtWarehouse.Text.Length == 8)
                {
                    this.stuid = this.txtWarehouse.Text;
                }
                else
                {
                    MessageShow.Alert("error", "仓库代码不合法");
                    return;
                }
                try
                {
                    flag = this.service.GetWarehouspriview(Global.CurrUser.Name, this.stuid, ref msgError);
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
                        this.management.SaveConfig(this.stuid, EnumSaveContent.Warehouse);
                        base.Close();
                        base.DialogResult = DialogResult.OK;
                        return;
                    }
                    catch (Exception exception3)
                    {
                        MessageShow.Alert("error", exception3.ToString());
                        return;
                    }
                }
                MessageShow.Alert("error", "此用户无" + this.txtWarehouse.Text + "仓库权限");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            base.Close();
            base.DialogResult = DialogResult.OK;
        }

        private void ConfigFrm_Activated(object sender, EventArgs e)
        {
            OEM_Catchwell.SetBeamHold(false);
            this.txtWarehouse.Focus();
            this.txtWarehouse.SelectAll();
        }

        private void ConfigFrm_Deactivate(object sender, EventArgs e)
        {
            OEM_Catchwell.SetBeamHold(true);
        }

        private void ConfigFrm_Load(object sender, EventArgs e)
        {
            this.txtWarehouse.Focus();
            this.management = Management.GetSingleton();
            this.txtWarehouse.Text = this.management.WarehouseNo;
            this.txtWarehouse.Focus();
            this.txtWarehouse.SelectAll();
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
            this.txtWarehouse = new TextBox();
            this.BtnConfig = new Button();
            this.button2 = new Button();
            this.TasksTitile = new HeadLabel();
            base.SuspendLayout();
            this.label1.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label1.Location = new Point(0x16, 0x44);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x4e, 20);
            this.label1.Text = "仓库号：";
            this.txtWarehouse.Location = new Point(0x16, 0x5b);
            this.txtWarehouse.Name = "txtWarehouse";
            this.txtWarehouse.Size = new Size(0xb8, 0x17);
            this.txtWarehouse.TabIndex = 9;
            this.txtWarehouse.GotFocus += new EventHandler(this.txtWarehouse_GotFocus);
            this.txtWarehouse.KeyDown += new KeyEventHandler(this.txtWarehouse_KeyDown);
            this.BtnConfig.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.BtnConfig.Location = new Point(0x16, 0xa6);
            this.BtnConfig.Name = "BtnConfig";
            this.BtnConfig.Size = new Size(80, 30);
            this.BtnConfig.TabIndex = 10;
            this.BtnConfig.Text = "确定";
            this.BtnConfig.Click += new EventHandler(this.BtnConfig_Click);
            this.button2.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.button2.Location = new Point(0x7e, 0xa6);
            this.button2.Name = "button2";
            this.button2.Size = new Size(80, 30);
            this.button2.TabIndex = 11;
            this.button2.Text = "取消";
            this.button2.Click += new EventHandler(this.button2_Click);
            this.TasksTitile.BackColor = SystemColors.HotTrack;
            this.TasksTitile.Font = new Font("Arial", 14f, FontStyle.Regular);
            this.TasksTitile.ForeColor = SystemColors.ActiveCaptionText;
            this.TasksTitile.Location = new Point(-4, 0);
            this.TasksTitile.Name = "TasksTitile";
            this.TasksTitile.Size = new Size(240, 0x18);
            this.TasksTitile.TabIndex = 7;
            this.TasksTitile.Text = "仓库设置";
            base.AutoScaleDimensions = new SizeF(96f, 96f);
            //base.AutoScaleMode = AutoScaleMode.Dpi;
            this.AutoScroll = true;
            base.ClientSize = new Size(0xec, 270);
            base.ControlBox = false;
            base.Controls.Add(this.button2);
            base.Controls.Add(this.BtnConfig);
            base.Controls.Add(this.txtWarehouse);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.TasksTitile);
            //base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "ConfigFrm";
            this.Text = "设置";
            base.Deactivate += new EventHandler(this.ConfigFrm_Deactivate);
            base.Load += new EventHandler(this.ConfigFrm_Load);
            base.Activated += new EventHandler(this.ConfigFrm_Activated);
            base.ResumeLayout(false);
        }

        private void txtWarehouse_GotFocus(object sender, EventArgs e)
        {
            OEM_Catchwell.SetOpMode(0);
        }

        private void txtWarehouse_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                this.BtnConfig_Click(null, null);
            }
        }
    }
}

