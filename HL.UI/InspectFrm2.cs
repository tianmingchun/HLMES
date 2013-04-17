namespace HL.UI
{
    using BizLayer;
    using Entity;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using HL.Framework;
    using HL.Controls;

    public class InspectFrm2 : Form
    {
        private Button btnDetail;
        private Button btnExit;
        private Button btnSubmit;
        private ComboBox cmBoxReason;
        private IContainer components;
        private string insGodis = "";
        private string insGodsName = "";
        private string insLocation = "";
        private string insPici = "";
        private decimal insSelectqty;
        private string insSpectNo = "";
        private decimal insTotalqty;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Management management = Management.GetSingleton();
        private RadioButton raBhege;
        private RadioButton raBpart;
        private RadioButton raBrangbu;
        private RadioButton raBWhole;
        private MiddleService service = Global.MiddleService;
        private HeadLabel TasksTitile;
        private TextBox txtbad;
        private TextBox txtBuliang;
        private TextBox txtChoujian;
        private TextBox txtDanhao;
        private TextBox txtDaohuo;
        private TextBox txtDescrible;
        private TextBox txtgood;
        private TextBox txtGoodname;
        private TextBox txtHuohao;
        private TextBox txtKunwei;
        private TextBox txtPichi;

        public InspectFrm2(string insspectNo, string inslocation, string insgodis, string inspici, decimal instotalqty, decimal insselectqty, string insgoodname)
        {
            this.InitializeComponent();
            this.insSpectNo = insspectNo;
            this.insLocation = inslocation;
            this.insGodis = insgodis;
            this.insPici = inspici;
            this.insTotalqty = instotalqty;
            this.insSelectqty = insselectqty;
            this.insGodsName = insgoodname;
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            DataSet dsInspectitem = new DataSet();
            try
            {
                dsInspectitem = this.service.GetInspectItem(this.txtDanhao.Text);
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
            InspectFrm3 frm = new InspectFrm3(dsInspectitem);
            frm.ShowDialog();
            frm.Dispose();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string str = string.Empty;
            string str3 = string.Empty;
            if (this.confirm())
            {
                string str2;
                if (this.raBhege.Checked)
                {
                    str2 = "100";
                }
                else if (this.raBrangbu.Checked)
                {
                    str2 = "200";
                }
                else if (this.raBpart.Checked)
                {
                    str2 = "300";
                }
                else
                {
                    str2 = "400";
                }
                if (this.cmBoxReason.Text == "")
                {
                    str3 = "";
                }
                else
                {
                    str3 = this.cmBoxReason.SelectedValue.ToString();
                }
                string[] arrTask = new string[] { this.txtDanhao.Text, this.txtKunwei.Text, this.txtHuohao.Text, this.txtPichi.Text, this.txtDaohuo.Text, this.txtChoujian.Text, this.txtBuliang.Text, str2.ToString(), this.txtgood.Text, this.txtbad.Text, this.txtDescrible.Text, str3 };
                Cursor.Current = Cursors.WaitCursor;
                WaitingFrm frm = new WaitingFrm();
                frm.Show("提交", "正在提交采集数据，请稍候...");
                try
                {
                    Global.MiddleService.SubmitInspectRecord(Global.CurrUser.ToString(), arrTask, ref str);
                }
                catch (ApplicationException exception)
                {
                    Cursor.Current = Cursors.Default;
                    frm.Close();
                    MessageShow.Alert("Error", exception.Message);
                    return;
                }
                catch (Exception exception2)
                {
                    Cursor.Current = Cursors.Default;
                    frm.Close();
                    MessageShow.Alert("UnKnowError", exception2.Message);
                    return;
                }
                Cursor.Current = Cursors.Default;
                frm.Close();
                MessageShow.Alert("Success", str);
                base.DialogResult = DialogResult.OK;
                new InspectMain().btnCheckorder_Click(null, null);
            }
        }

        private bool confirm()
        {
            if (this.txtBuliang.Text == string.Empty)
            {
                MessageShow.Alert("error", "检验不良数不能为空");
                this.txtBuliang.Focus();
                return false;
            }
            if (this.txtChoujian.Text == "")
            {
                MessageShow.Alert("error", "抽检数不能为空");
                return false;
            }
            if (decimal.Parse(this.txtChoujian.Text) > decimal.Parse(this.txtDaohuo.Text))
            {
                MessageShow.Alert("error", "抽检数不能大于到货数");
                this.txtChoujian.Focus();
                this.txtChoujian.SelectAll();
                return false;
            }
            if ((this.txtBuliang.Text != "") && (decimal.Parse(this.txtBuliang.Text) > decimal.Parse(this.txtChoujian.Text)))
            {
                MessageShow.Alert("error", "检良数不能大于抽检数");
                this.txtBuliang.Focus();
                this.txtBuliang.SelectAll();
                return false;
            }
            if ((!this.management.CheckQuantity(this.txtChoujian.Text) || !this.management.CheckQuantity(this.txtBuliang.Text)) || (!this.management.CheckQuantity(this.txtbad.Text) || !this.management.CheckQuantity(this.txtgood.Text)))
            {
                MessageShow.Alert("error", "输入的数量不合法,请检查");
                return false;
            }
            if ((!this.raBhege.Checked && !this.raBpart.Checked) && (!this.raBrangbu.Checked && !this.raBWhole.Checked))
            {
                MessageShow.Alert("error", "请选择检验结果");
                return false;
            }
            if ((this.raBpart.Checked || this.raBWhole.Checked) && (this.cmBoxReason.Text == ""))
            {
                MessageShow.Alert("error", "请选择移动原因");
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
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.label5 = new Label();
            this.label6 = new Label();
            this.label7 = new Label();
            this.label8 = new Label();
            this.btnDetail = new Button();
            this.btnSubmit = new Button();
            this.btnExit = new Button();
            this.txtDanhao = new TextBox();
            this.txtKunwei = new TextBox();
            this.txtHuohao = new TextBox();
            this.txtPichi = new TextBox();
            this.txtDaohuo = new TextBox();
            this.txtChoujian = new TextBox();
            this.txtBuliang = new TextBox();
            this.raBhege = new RadioButton();
            this.raBrangbu = new RadioButton();
            this.raBWhole = new RadioButton();
            this.raBpart = new RadioButton();
            this.label9 = new Label();
            this.label10 = new Label();
            this.txtgood = new TextBox();
            this.txtbad = new TextBox();
            this.label11 = new Label();
            this.txtDescrible = new TextBox();
            this.label12 = new Label();
            this.cmBoxReason = new ComboBox();
            this.TasksTitile = new HeadLabel();
            this.txtGoodname = new TextBox();
            base.SuspendLayout();
            this.label1.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label1.Location = new Point(0, 30);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x4b, 20);
            this.label1.Text = "单号:";
            this.label2.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label2.Location = new Point(0x8d, 0x20);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x34, 20);
            this.label2.Text = "库位：";
            this.label3.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label3.Location = new Point(0, 0x3a);
            this.label3.Name = "label3";
            this.label3.Size = new Size(50, 20);
            this.label3.Text = "货号：";
            this.label4.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label4.Location = new Point(0, 0x54);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x3f, 20);
            this.label4.Text = "批次号：";
            this.label5.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label5.Location = new Point(0x83, 0x53);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x3f, 20);
            this.label5.Text = "到货数：";
            this.label6.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label6.Location = new Point(0, 0x6b);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x3f, 20);
            this.label6.Text = "抽检数：";
            this.label7.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label7.Location = new Point(0x83, 0x6a);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x3f, 20);
            this.label7.Text = "检不良：";
            this.label8.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label8.Location = new Point(0, 130);
            this.label8.Name = "label8";
            this.label8.Size = new Size(90, 20);
            this.label8.Text = "检验结果：";
            this.btnDetail.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnDetail.Location = new Point(3, 0x106);
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new Size(0x4b, 30);
            this.btnDetail.TabIndex = 0x16;
            this.btnDetail.Text = "检验科目";
            this.btnDetail.Click += new EventHandler(this.btnDetail_Click);
            this.btnSubmit.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnSubmit.Location = new Point(0x51, 0x106);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new Size(0x4b, 30);
            this.btnSubmit.TabIndex = 0x17;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.Click += new EventHandler(this.btnSubmit_Click);
            this.btnExit.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnExit.Location = new Point(160, 0x106);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new Size(0x4b, 30);
            this.btnExit.TabIndex = 0x18;
            this.btnExit.Text = "返回";
            this.btnExit.Click += new EventHandler(this.btnExit_Click);
            this.txtDanhao.BackColor = SystemColors.InactiveBorder;
            this.txtDanhao.Enabled = false;
            this.txtDanhao.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.txtDanhao.Location = new Point(0x22, 0x1c);
            this.txtDanhao.Name = "txtDanhao";
            this.txtDanhao.Size = new Size(0x65, 0x15);
            this.txtDanhao.TabIndex = 0x19;
            this.txtKunwei.BackColor = SystemColors.InactiveBorder;
            this.txtKunwei.Enabled = false;
            this.txtKunwei.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.txtKunwei.Location = new Point(0xae, 30);
            this.txtKunwei.Name = "txtKunwei";
            this.txtKunwei.Size = new Size(0x39, 0x15);
            this.txtKunwei.TabIndex = 0x1a;
            this.txtHuohao.BackColor = SystemColors.InactiveBorder;
            this.txtHuohao.Enabled = false;
            this.txtHuohao.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.txtHuohao.Location = new Point(0x22, 0x37);
            this.txtHuohao.Name = "txtHuohao";
            this.txtHuohao.Size = new Size(0x4e, 0x15);
            this.txtHuohao.TabIndex = 0x1b;
            this.txtPichi.BackColor = SystemColors.InactiveBorder;
            this.txtPichi.Enabled = false;
            this.txtPichi.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.txtPichi.Location = new Point(0x38, 0x52);
            this.txtPichi.Name = "txtPichi";
            this.txtPichi.Size = new Size(0x45, 0x15);
            this.txtPichi.TabIndex = 0x1c;
            this.txtDaohuo.BackColor = SystemColors.InactiveBorder;
            this.txtDaohuo.Enabled = false;
            this.txtDaohuo.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.txtDaohuo.Location = new Point(0xb5, 0x52);
            this.txtDaohuo.Name = "txtDaohuo";
            this.txtDaohuo.Size = new Size(50, 0x15);
            this.txtDaohuo.TabIndex = 0x1d;
            this.txtChoujian.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.txtChoujian.Location = new Point(0x38, 0x6a);
            this.txtChoujian.Name = "txtChoujian";
            this.txtChoujian.Size = new Size(0x45, 0x15);
            this.txtChoujian.TabIndex = 30;
            this.txtChoujian.KeyDown += new KeyEventHandler(this.txtChoujian_KeyDown);
            this.txtBuliang.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.txtBuliang.Location = new Point(0xb5, 0x69);
            this.txtBuliang.Name = "txtBuliang";
            this.txtBuliang.Size = new Size(50, 0x15);
            this.txtBuliang.TabIndex = 0x1f;
            this.txtBuliang.TextChanged += new EventHandler(this.txtBuliang_TextChanged);
            this.raBhege.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.raBhege.Location = new Point(0x51, 130);
            this.raBhege.Name = "raBhege";
            this.raBhege.Size = new Size(0x56, 20);
            this.raBhege.TabIndex = 0x20;
            this.raBhege.TabStop = false;
            this.raBhege.Text = "合格接收";
            this.raBhege.Click += new EventHandler(this.raBhege_Click);
            this.raBrangbu.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.raBrangbu.Location = new Point(160, 130);
            this.raBrangbu.Name = "raBrangbu";
            this.raBrangbu.Size = new Size(0x4b, 20);
            this.raBrangbu.TabIndex = 0x21;
            this.raBrangbu.TabStop = false;
            this.raBrangbu.Text = "让步接收";
            this.raBrangbu.Click += new EventHandler(this.raBrangbu_Click);
            this.raBWhole.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.raBWhole.Location = new Point(0x9f, 0x9c);
            this.raBWhole.Name = "raBWhole";
            this.raBWhole.Size = new Size(0x4c, 20);
            this.raBWhole.TabIndex = 0x2b;
            this.raBWhole.TabStop = false;
            this.raBWhole.Text = "全部退货";
            this.raBWhole.Click += new EventHandler(this.raBWhole_Click);
            this.raBpart.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.raBpart.Location = new Point(0x51, 0x9c);
            this.raBpart.Name = "raBpart";
            this.raBpart.Size = new Size(0x56, 20);
            this.raBpart.TabIndex = 0x2a;
            this.raBpart.TabStop = false;
            this.raBpart.Text = "部分退货";
            this.raBpart.Click += new EventHandler(this.raBpart_Click);
            this.label9.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label9.Location = new Point(4, 0xb7);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x3b, 20);
            this.label9.Text = "判合格：";
            this.label10.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label10.Location = new Point(0x75, 0xb5);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x4c, 20);
            this.label10.Text = "判不良：";
            this.txtgood.Location = new Point(0x38, 0xb2);
            this.txtgood.Name = "txtgood";
            this.txtgood.Size = new Size(0x2f, 0x17);
            this.txtgood.TabIndex = 0x36;
            this.txtbad.Location = new Point(0xb5, 180);
            this.txtbad.Name = "txtbad";
            this.txtbad.Size = new Size(0x39, 0x17);
            this.txtbad.TabIndex = 0x37;
            this.label11.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label11.Location = new Point(4, 0xeb);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x38, 20);
            this.label11.Text = "描述：";
            this.txtDescrible.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.txtDescrible.Location = new Point(0x2c, 0xe9);
            this.txtDescrible.Name = "txtDescrible";
            this.txtDescrible.Size = new Size(0xbb, 0x15);
            this.txtDescrible.TabIndex = 0x39;
            this.label12.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label12.Location = new Point(3, 0xcf);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x48, 20);
            this.label12.Text = "移动原因：";
            this.cmBoxReason.Items.Add("");
            this.cmBoxReason.Location = new Point(0x43, 0xcc);
            this.cmBoxReason.Name = "cmBoxReason";
            this.cmBoxReason.Size = new Size(0x7f, 0x17);
            this.cmBoxReason.TabIndex = 70;
            this.TasksTitile.BackColor = SystemColors.HotTrack;
            this.TasksTitile.Font = new Font("Arial", 14f, FontStyle.Regular);
            this.TasksTitile.ForeColor = SystemColors.ActiveCaptionText;
            this.TasksTitile.Location = new Point(-3, 3);
            this.TasksTitile.Name = "TasksTitile";
            this.TasksTitile.Size = new Size(240, 0x18);
            this.TasksTitile.TabIndex = 6;
            this.TasksTitile.Text = "检验采集";
            this.txtGoodname.BackColor = SystemColors.InactiveBorder;
            this.txtGoodname.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.txtGoodname.Location = new Point(0x71, 0x37);
            this.txtGoodname.Name = "txtGoodname";
            this.txtGoodname.Size = new Size(0x76, 0x15);
            this.txtGoodname.TabIndex = 0x53;
            base.AutoScaleDimensions = new SizeF(96f, 96f);
            //base.AutoScaleMode = AutoScaleMode.Dpi;
            this.AutoScroll = true;
            base.ClientSize = new Size(0xee, 0x127);
            base.ControlBox = false;
            base.Controls.Add(this.txtGoodname);
            base.Controls.Add(this.cmBoxReason);
            base.Controls.Add(this.label12);
            base.Controls.Add(this.txtDescrible);
            base.Controls.Add(this.label11);
            base.Controls.Add(this.txtbad);
            base.Controls.Add(this.txtgood);
            base.Controls.Add(this.label10);
            base.Controls.Add(this.label9);
            base.Controls.Add(this.raBWhole);
            base.Controls.Add(this.raBpart);
            base.Controls.Add(this.raBrangbu);
            base.Controls.Add(this.raBhege);
            base.Controls.Add(this.txtBuliang);
            base.Controls.Add(this.txtChoujian);
            base.Controls.Add(this.txtDaohuo);
            base.Controls.Add(this.txtPichi);
            base.Controls.Add(this.txtHuohao);
            base.Controls.Add(this.txtKunwei);
            base.Controls.Add(this.txtDanhao);
            base.Controls.Add(this.btnExit);
            base.Controls.Add(this.btnSubmit);
            base.Controls.Add(this.btnDetail);
            base.Controls.Add(this.label8);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.TasksTitile);
            //base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "InspectFrm2";
            this.Text = "InspectFrm2";
            base.Load += new EventHandler(this.InspectFrm2_Load);
            base.ResumeLayout(false);
        }

        private void InspectFrm2_Load(object sender, EventArgs e)
        {
            this.txtBuliang.Focus();
            this.txtDanhao.Text = this.insSpectNo;
            this.txtKunwei.Text = this.insLocation;
            this.txtHuohao.Text = this.insGodis;
            this.txtPichi.Text = this.insPici;
            this.txtDaohuo.Text = this.insTotalqty.ToString();
            this.txtChoujian.Text = this.insSelectqty.ToString();
            this.txtGoodname.Text = this.insGodsName;
            this.txtBuliang.Text = "0";
            Cursor.Current = Cursors.Default;
            this.MoveReason();
        }

        private void MoveReason()
        {
            DataSet set = new DataSet();
            DataTable table = this.service.GetMoveReason().Tables[0];
            this.cmBoxReason.DataSource = table;
            this.cmBoxReason.DisplayMember = "IDNAME";
            this.cmBoxReason.ValueMember = "DATDCODE";
            this.cmBoxReason.SelectedIndex = -1;
        }

        private void raBhege_Click(object sender, EventArgs e)
        {
            if (this.txtBuliang.Text == string.Empty)
            {
                MessageShow.Alert("error", "检验不良数不能为空");
                this.txtBuliang.Focus();
                this.raBhege.Checked = false;
            }
            else
            {
                this.txtgood.Text = this.txtDaohuo.Text;
                this.txtbad.Text = "0";
            }
        }

        private void raBpart_Click(object sender, EventArgs e)
        {
            if (this.txtBuliang.Text == string.Empty)
            {
                MessageShow.Alert("error", "检验不良数不能为空");
                this.txtBuliang.Focus();
                this.raBpart.Checked = false;
            }
            else
            {
                this.txtgood.Text = Convert.ToString((decimal) (decimal.Parse(this.txtDaohuo.Text) - decimal.Parse(this.txtBuliang.Text)));
                this.txtbad.Text = this.txtBuliang.Text;
            }
        }

        private void raBrangbu_Click(object sender, EventArgs e)
        {
            if (this.txtBuliang.Text == string.Empty)
            {
                MessageShow.Alert("error", "检验不良数不能为空");
                this.txtBuliang.Focus();
                this.raBrangbu.Checked = false;
            }
            else
            {
                this.txtgood.Text = this.txtDaohuo.Text;
                this.txtbad.Text = "0";
            }
        }

        private void raBWhole_Click(object sender, EventArgs e)
        {
            if (this.txtBuliang.Text == string.Empty)
            {
                MessageShow.Alert("error", "检验不良数不能为空");
                this.txtBuliang.Focus();
                this.raBWhole.Checked = false;
            }
            else
            {
                this.txtgood.Text = "0";
                this.txtbad.Text = this.txtDaohuo.Text;
            }
        }

        private void txtBuliang_TextChanged(object sender, EventArgs e)
        {
            if (this.raBhege.Checked)
            {
                this.raBhege_Click(null, null);
            }
            else if (this.raBrangbu.Checked)
            {
                this.raBrangbu_Click(null, null);
            }
            else if (this.raBpart.Checked)
            {
                this.raBpart_Click(null, null);
            }
            else if (this.raBWhole.Checked)
            {
                this.raBWhole_Click(null, null);
            }
        }

        private void txtChoujian_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                this.txtBuliang.Focus();
            }
        }
    }
}

