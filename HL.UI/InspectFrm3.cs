namespace HL.UI
{
    using Entity;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using HL.Controls;

    public class InspectFrm3 : Form
    {
        private Button button1;
        private IContainer components;
        private DataSet dsItem = new DataSet();
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private ListView lvsItem;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private HeadLabel TasksTitile;
        private TextBox txtDescible;
        private TextBox txtDown;
        private TextBox txtName;
        private TextBox txtStd;
        private TextBox txtTools;
        private TextBox txtUp;

        public InspectFrm3(DataSet dsInspectitem)
        {
            this.InitializeComponent();
            this.dsItem = dsInspectitem;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void ClearTxtbox()
        {
            this.txtName.Text = "";
            this.txtTools.Text = "";
            this.txtStd.Text = "";
            this.txtUp.Text = "";
            this.txtDown.Text = "";
            this.txtDescible.Text = "";
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
            this.TasksTitile = new HeadLabel();
            this.lvsItem = new ListView();
            this.button1 = new Button();
            this.txtDescible = new TextBox();
            this.txtDown = new TextBox();
            this.txtUp = new TextBox();
            this.txtStd = new TextBox();
            this.txtTools = new TextBox();
            this.txtName = new TextBox();
            this.radioButton2 = new RadioButton();
            this.radioButton1 = new RadioButton();
            this.label6 = new Label();
            this.label5 = new Label();
            this.label4 = new Label();
            this.label3 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            base.SuspendLayout();
            this.TasksTitile.BackColor = SystemColors.HotTrack;
            this.TasksTitile.Font = new Font("Arial", 14f, FontStyle.Regular);
            this.TasksTitile.ForeColor = SystemColors.ActiveCaptionText;
            this.TasksTitile.Location = new Point(-3, 3);
            this.TasksTitile.Name = "TasksTitile";
            this.TasksTitile.Size = new Size(240, 0x18);
            this.TasksTitile.TabIndex = 7;
            this.TasksTitile.Text = "检验科目";
            this.lvsItem.FullRowSelect = true;
            this.lvsItem.Location = new Point(0, 0xb1);
            this.lvsItem.Name = "lvsItem";
            this.lvsItem.Size = new Size(0xeb, 0x55);
            this.lvsItem.TabIndex = 8;
            this.lvsItem.View = View.Details;
            this.lvsItem.SelectedIndexChanged += new EventHandler(this.lvsItem_SelectedIndexChanged);
            this.button1.Location = new Point(0x4b, 0x10c);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x48, 0x1b);
            this.button1.TabIndex = 9;
            this.button1.Text = "返回";
            this.button1.Click += new EventHandler(this.button1_Click);
            this.txtDescible.BackColor = SystemColors.InactiveBorder;
            this.txtDescible.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.txtDescible.Location = new Point(0x57, 0x9a);
            this.txtDescible.Name = "txtDescible";
            this.txtDescible.Size = new Size(0x84, 0x15);
            this.txtDescible.TabIndex = 0x24;
            this.txtDown.BackColor = SystemColors.InactiveBorder;
            this.txtDown.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.txtDown.Location = new Point(0xa3, 0x81);
            this.txtDown.Name = "txtDown";
            this.txtDown.Size = new Size(0x38, 0x15);
            this.txtDown.TabIndex = 0x23;
            this.txtUp.BackColor = SystemColors.InactiveBorder;
            this.txtUp.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.txtUp.Location = new Point(0x37, 130);
            this.txtUp.Name = "txtUp";
            this.txtUp.Size = new Size(0x3a, 0x15);
            this.txtUp.TabIndex = 0x22;
            this.txtStd.BackColor = SystemColors.InactiveBorder;
            this.txtStd.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.txtStd.Location = new Point(0x57, 0x6b);
            this.txtStd.Name = "txtStd";
            this.txtStd.Size = new Size(0x84, 0x15);
            this.txtStd.TabIndex = 0x21;
            this.txtTools.BackColor = SystemColors.InactiveBorder;
            this.txtTools.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.txtTools.Location = new Point(0x57, 60);
            this.txtTools.Name = "txtTools";
            this.txtTools.Size = new Size(0x84, 0x15);
            this.txtTools.TabIndex = 0x20;
            this.txtName.BackColor = SystemColors.InactiveBorder;
            this.txtName.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.txtName.Location = new Point(0x57, 0x21);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(0x84, 0x15);
            this.txtName.TabIndex = 0x1f;
            this.radioButton2.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.radioButton2.Location = new Point(0x87, 0x57);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new Size(0x48, 20);
            this.radioButton2.TabIndex = 30;
            this.radioButton2.TabStop = false;
            this.radioButton2.Text = "定量";
            this.radioButton1.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.radioButton1.Location = new Point(0x29, 0x57);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new Size(0x48, 20);
            this.radioButton1.TabIndex = 0x1d;
            this.radioButton1.TabStop = false;
            this.radioButton1.Text = "定性";
            this.label6.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label6.Location = new Point(4, 0x97);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x4d, 20);
            this.label6.Text = "检验描述：";
            this.label5.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label5.Location = new Point(110, 0x83);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x3b, 20);
            this.label5.Text = "下偏差：";
            this.label4.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label4.Location = new Point(0, 0x83);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x3b, 20);
            this.label4.Text = "上偏差：";
            this.label3.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label3.Location = new Point(4, 110);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x4d, 20);
            this.label3.Text = "标准值：";
            this.label2.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label2.Location = new Point(0, 60);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x4e, 20);
            this.label2.Text = "检验工具：";
            this.label1.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label1.Location = new Point(4, 30);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x4d, 20);
            this.label1.Text = "检验科目：";
            base.AutoScaleDimensions = new SizeF(96f, 96f);
            //base.AutoScaleMode = AutoScaleMode.Dpi;
            this.AutoScroll = true;
            base.ClientSize = new Size(0xee, 0x127);
            base.ControlBox = false;
            base.Controls.Add(this.txtDescible);
            base.Controls.Add(this.txtDown);
            base.Controls.Add(this.txtUp);
            base.Controls.Add(this.txtStd);
            base.Controls.Add(this.txtTools);
            base.Controls.Add(this.txtName);
            base.Controls.Add(this.radioButton2);
            base.Controls.Add(this.radioButton1);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.lvsItem);
            base.Controls.Add(this.TasksTitile);
            //base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "InspectFrm3";
            this.Text = "检验科目";
            base.Load += new EventHandler(this.InspectFrm3_Load);
            base.ResumeLayout(false);
        }

        private void InitializeLsvDetail()
        {
            this.lvsItem.Columns.Clear();
            this.lvsItem.Columns.Add("检验单号", -2, HorizontalAlignment.Center);
            this.lvsItem.Columns.Add("检验名称", -2, HorizontalAlignment.Center);
            this.lvsItem.Columns.Add("检验工具", -2, HorizontalAlignment.Center);
            this.lvsItem.Columns.Add("定性/定量", -2, HorizontalAlignment.Center);
            this.lvsItem.Columns.Add("标准值", -2, HorizontalAlignment.Center);
            this.lvsItem.Columns.Add("上偏差", -2, HorizontalAlignment.Center);
            this.lvsItem.Columns.Add("下偏差", -2, HorizontalAlignment.Center);
            this.lvsItem.Columns.Add("描述", -2, HorizontalAlignment.Center);
        }

        private void InsertListview()
        {
            DataTable table = this.dsItem.Tables[0];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem(new string[] { table.Rows[i]["ICNO"].ToString(), table.Rows[i]["ICINAME"].ToString(), table.Rows[i]["ICITOOLS"].ToString(), table.Rows[i]["ICITYPE"].ToString(), table.Rows[i]["ICISTD"].ToString(), table.Rows[i]["ICISTDUP"].ToString(), table.Rows[i]["ICISTDDOWN"].ToString(), table.Rows[i]["ICIDESCRIBE"].ToString() });
                this.lvsItem.Items.Add(item);
            }
        }

        private void InspectFrm3_Load(object sender, EventArgs e)
        {
            this.InitializeLsvDetail();
            this.InsertListview();
        }

        private void lvsItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ClearTxtbox();
            if (((this.lvsItem.Items.Count != 0) && (this.lvsItem.SelectedIndices != null)) && (this.lvsItem.SelectedIndices.Count != 0))
            {
                this.txtName.Text = this.lvsItem.Items[this.lvsItem.SelectedIndices[0]].SubItems[1].Text;
                this.txtTools.Text = this.lvsItem.Items[this.lvsItem.SelectedIndices[0]].SubItems[2].Text;
                if (this.lvsItem.Items[this.lvsItem.SelectedIndices[0]].SubItems[3].Text.ToString() == "1")
                {
                    this.radioButton1.Checked = true;
                    this.radioButton2.Checked = false;
                }
                else
                {
                    this.radioButton2.Checked = true;
                    this.radioButton1.Checked = false;
                }
                this.txtStd.Text = this.lvsItem.Items[this.lvsItem.SelectedIndices[0]].SubItems[4].Text;
                this.txtUp.Text = this.lvsItem.Items[this.lvsItem.SelectedIndices[0]].SubItems[5].Text;
                this.txtDown.Text = this.lvsItem.Items[this.lvsItem.SelectedIndices[0]].SubItems[6].Text;
                this.txtDescible.Text = this.lvsItem.Items[this.lvsItem.SelectedIndices[0]].SubItems[7].Text;
            }
        }
    }
}

