namespace HL.UI
{
    using Entity;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using HL.Framework;
    using HL.Controls;

    public class CollectionDetailFrm : Form
    {
        private Button btnBack;
        private Button btnDelAll;
        private Button btnDelete;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader5;
        private IContainer components;
        private ColumnHeader headerNo;
        private Label label1;
        private ListView listView1;
        private ColumnHeader TaskDetailNo;
        private HeadLabel TasksTitile;

        public CollectionDetailFrm()
        {
            this.InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnDelAll_Click(object sender, EventArgs e)
        {
            if (this.listView1.Items.Count == 0)
            {
                MessageShow.Alert("提示", "当前还没有任何采集数据！");
            }
            else if (MessageBox.Show("你确定要删除全部采集数据吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) != DialogResult.Cancel)
            {
                foreach (DictionaryEntry entry in Global.CurrTask.InsList)
                {
                    Instruction instruction = entry.Value as Instruction;
                    instruction.Result.ColDetails.Clear();
                }
                Global.CurrTask.SingleIDs.Clear();
                this.listView1.Items.Clear();
                MessageShow.Alert("Success", "采集数据删除成功！");
                base.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (((this.listView1.SelectedIndices != null) && (this.listView1.SelectedIndices.Count != 0)) && (MessageBox.Show("你确定要删除该条数据吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) != DialogResult.Cancel))
            {
                int index = this.listView1.SelectedIndices[0];
                string text = this.listView1.Items[index].SubItems[0].Text;
                Instruction instruction = Global.CurrTask.InsList[text] as Instruction;
                int num2 = Convert.ToInt32(this.listView1.Items[index].SubItems[5].Text);
                instruction.Result.RemoveColDetail(num2);
                this.listView1.Items.RemoveAt(index);
                Application.DoEvents();
                MessageShow.Alert("Success", "删除成功！");
                if (this.listView1.Items.Count == 0)
                {
                    base.Close();
                }
            }
        }

        private void CollectionDetailFrm_Load(object sender, EventArgs e)
        {
            ArrayList keys = Global.CurrTask.InsList.Keys as ArrayList;
            for (int i = 0; i < keys.Count; i++)
            {
                Instruction instruction = Global.CurrTask.InsList[keys[i]] as Instruction;
                if (instruction.Result == null)
                {
                    return;
                }
                foreach (DictionaryEntry entry in instruction.Result.ColDetails)
                {
                    ColDetail detail = entry.Value as ColDetail;
                    this.listView1.Items.Add(new ListViewItem(new string[] { instruction.ID, detail.Location, detail.GoodsID, detail.BatchNumber, detail.CollectedQuantity.ToString(), entry.Key.ToString() }));
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
            this.label1 = new Label();
            this.listView1 = new ListView();
            this.TaskDetailNo = new ColumnHeader();
            this.columnHeader1 = new ColumnHeader();
            this.columnHeader2 = new ColumnHeader();
            this.columnHeader3 = new ColumnHeader();
            this.columnHeader5 = new ColumnHeader();
            this.headerNo = new ColumnHeader();
            this.btnDelete = new Button();
            this.btnDelAll = new Button();
            this.btnBack = new Button();
            this.TasksTitile = new HeadLabel();
            base.SuspendLayout();
            this.label1.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label1.Location = new Point(5, 0x21);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x52, 20);
            this.label1.Text = "已采集数据:";
            this.listView1.Columns.Add(this.TaskDetailNo);
            this.listView1.Columns.Add(this.columnHeader1);
            this.listView1.Columns.Add(this.columnHeader2);
            this.listView1.Columns.Add(this.columnHeader3);
            this.listView1.Columns.Add(this.columnHeader5);
            this.listView1.Columns.Add(this.headerNo);
            this.listView1.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new Point(3, 0x33);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0xe9, 0xd5);
            this.listView1.TabIndex = 1;
            this.listView1.View = View.Details;
            this.TaskDetailNo.Text = "任务明细号";
            this.TaskDetailNo.Width = 0;
            this.columnHeader1.Text = "库位";
            this.columnHeader1.Width = 50;
            this.columnHeader2.Text = "物料";
            this.columnHeader2.Width = 60;
            this.columnHeader3.Text = "批次/单件";
            this.columnHeader3.Width = 80;
            this.columnHeader5.Text = "数量";
            this.columnHeader5.Width = 40;
            this.headerNo.Text = "记录索引号";
            this.headerNo.Width = 0;
            this.btnDelete.Font = new Font("Arial", 9f, FontStyle.Regular);
            this.btnDelete.Location = new Point(7, 270);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(70, 30);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.btnDelAll.Font = new Font("Arial", 9f, FontStyle.Regular);
            this.btnDelAll.Location = new Point(0x55, 270);
            this.btnDelAll.Name = "btnDelAll";
            this.btnDelAll.Size = new Size(70, 30);
            this.btnDelAll.TabIndex = 2;
            this.btnDelAll.Text = "全部删除";
            this.btnDelAll.Click += new EventHandler(this.btnDelAll_Click);
            this.btnBack.Font = new Font("Arial", 9f, FontStyle.Regular);
            this.btnBack.Location = new Point(0xa2, 270);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new Size(70, 30);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "返回";
            this.btnBack.Click += new EventHandler(this.btnBack_Click);
            this.TasksTitile.BackColor = SystemColors.HotTrack;
            this.TasksTitile.Font = new Font("Arial", 14f, FontStyle.Regular);
            this.TasksTitile.ForeColor = SystemColors.ActiveCaptionText;
            this.TasksTitile.Location = new Point(0, 0);
            this.TasksTitile.Name = "TasksTitile";
            this.TasksTitile.Size = new Size(240, 0x18);
            this.TasksTitile.TabIndex = 6;
            this.TasksTitile.Text = "采集明细";
            base.AutoScaleDimensions = new SizeF(96f, 96f);
            base.AutoScaleMode = AutoScaleMode.Dpi;
            this.AutoScroll = true;
            base.ClientSize = new Size(240, 0x131);
            base.ControlBox = false;
            base.Controls.Add(this.TasksTitile);
            base.Controls.Add(this.btnBack);
            base.Controls.Add(this.btnDelAll);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "CollectionDetailFrm";
            this.Text = "CollectionDetailFrm";
            base.Load += new EventHandler(this.CollectionDetailFrm_Load);
            base.ResumeLayout(false);
        }
    }
}

