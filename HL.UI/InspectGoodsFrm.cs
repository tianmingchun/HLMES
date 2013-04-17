namespace HL.UI
{
    using BizLayer;
    using Entity;
    using SharpExCS;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using HL.Framework;
    using HL.Controls;

    public class InspectGoodsFrm : Form
    {
        private Button btnBack;
        private Button btnInspect;
        private Button btnQuery;
        private IContainer components;
        private Label label2;
        private ListView lsvTasks;
        private const int RET_KEYSTROKE = 0;
        private MiddleService service = Global.MiddleService;
        private HeadLabel TasksTitile;
        private TextBox txtGosid;

        public InspectGoodsFrm()
        {
            this.InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnCollect_Click(object sender, EventArgs e)
        {
            string str = string.Empty;
            string text = string.Empty;
            string str3 = "";
            string str4 = "";
            int num = 100;
            string str5 = "";
            string str6 = "";
            string str7 = string.Empty;
            string str8 = string.Empty;
            ArrayList list = new ArrayList();
            for (int i = 0; i < this.lsvTasks.Items.Count; i++)
            {
                if (this.lsvTasks.Items[i].Checked)
                {
                    list.Add(i);
                    text = this.lsvTasks.Items[i].SubItems[1].Text;
                    str3 = this.lsvTasks.Items[i].SubItems[3].Text;
                    str4 = "0";
                    str5 = str3;
                    str6 = "0";
                    string[] arrTask = new string[] { text, "", "", "", str3, "", str4, num.ToString(), str5, str6, str7, str8 };
                    Cursor.Current = Cursors.WaitCursor;
                    try
                    {
                        Global.MiddleService.SubmitInspectRecord(Global.CurrUser.ToString(), arrTask, ref str);
                    }
                    catch (ApplicationException exception)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageShow.Alert("Error", exception.Message);
                        return;
                    }
                    catch (Exception exception2)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageShow.Alert("UnKnowError", exception2.Message);
                        return;
                    }
                    Cursor.Current = Cursors.Default;
                }
            }
            for (int j = list.Count - 1; j >= 0; j--)
            {
                this.lsvTasks.Items.RemoveAt(Convert.ToInt32(list[j]));
            }
            MessageShow.Alert("提示", str);
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.lsvTasks.Items.Clear();
            Cursor.Current = Cursors.WaitCursor;
            DataSet goodsInspectTasks = new DataSet();
            try
            {
                if (this.txtGosid.Text != "")
                {
                    goodsInspectTasks = this.service.GetGoodsInspectTasks(this.txtGosid.Text.Substring(1));
                }
                else
                {
                    goodsInspectTasks = this.service.GetInspectTasks();
                }
            }
            catch (ApplicationException exception)
            {
                Cursor.Current = Cursors.Default;
                MessageShow.Alert("错误", exception.Message);
                this.txtGosid.Focus();
                this.txtGosid.SelectAll();
                return;
            }
            catch (Exception exception2)
            {
                Cursor.Current = Cursors.Default;
                MessageShow.Alert("错误", exception2.Message);
                this.txtGosid.Focus();
                this.txtGosid.SelectAll();
                return;
            }
            Cursor.Current = Cursors.Default;
            DataTable table = goodsInspectTasks.Tables[0];
            if (table.Rows.Count != 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    ListViewItem item = new ListViewItem(new string[] { "", table.Rows[i]["ICNO"].ToString(), table.Rows[i]["Icmatcode"].ToString(), table.Rows[i]["ICNumber"].ToString(), table.Rows[i]["ICCheckNum"].ToString(), table.Rows[i]["ICSITE"].ToString(), table.Rows[i]["ICMATSN"].ToString() });
                    this.lsvTasks.Items.Add(item);
                }
            }
            else
            {
                MessageShow.Alert("error", "当前该货物不存在检验单");
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
            this.TasksTitile = new HeadLabel();
            this.lsvTasks = new ListView();
            this.btnBack = new Button();
            this.btnInspect = new Button();
            this.txtGosid = new TextBox();
            this.label2 = new Label();
            this.btnQuery = new Button();
            base.SuspendLayout();
            this.TasksTitile.BackColor = SystemColors.HotTrack;
            this.TasksTitile.Font = new Font("Arial", 14f, FontStyle.Regular);
            this.TasksTitile.ForeColor = SystemColors.ActiveCaptionText;
            this.TasksTitile.Location = new Point(-4, 3);
            this.TasksTitile.Name = "TasksTitile";
            this.TasksTitile.Size = new Size(240, 0x18);
            this.TasksTitile.TabIndex = 0x30;
            this.TasksTitile.Text = "任务列表";
            this.lsvTasks.CheckBoxes = true;
            this.lsvTasks.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.lsvTasks.FullRowSelect = true;
            this.lsvTasks.Location = new Point(3, 0x72);
            this.lsvTasks.Name = "lsvTasks";
            this.lsvTasks.Size = new Size(0xe9, 0xb2);
            this.lsvTasks.TabIndex = 0x3d;
            this.lsvTasks.View = View.Details;
            this.btnBack.Location = new Point(0xab, 0x49);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new Size(0x37, 0x23);
            this.btnBack.TabIndex = 60;
            this.btnBack.Text = "返回";
            this.btnBack.Click += new EventHandler(this.btnBack_Click);
            this.btnInspect.ForeColor = Color.FromArgb(0, 0, 0);
            this.btnInspect.Location = new Point(0x5b, 0x49);
            this.btnInspect.Name = "btnInspect";
            this.btnInspect.Size = new Size(0x48, 0x23);
            this.btnInspect.TabIndex = 0x3b;
            this.btnInspect.Text = "检合格";
            this.btnInspect.Click += new EventHandler(this.btnCollect_Click);
            this.txtGosid.Location = new Point(0x59, 0x21);
            this.txtGosid.Name = "txtGosid";
            this.txtGosid.Size = new Size(0x7e, 0x17);
            this.txtGosid.TabIndex = 0x3a;
            this.txtGosid.GotFocus += new EventHandler(this.txtGosid_GotFocus);
            this.txtGosid.KeyDown += new KeyEventHandler(this.txtGosid_KeyDown);
            this.label2.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label2.Location = new Point(13, 0x24);
            this.label2.Name = "label2";
            this.label2.Size = new Size(70, 20);
            this.label2.Text = "货号：";
            this.btnQuery.ForeColor = Color.FromArgb(0, 0, 0);
            this.btnQuery.Location = new Point(11, 0x49);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new Size(0x48, 0x23);
            this.btnQuery.TabIndex = 0x3f;
            this.btnQuery.Text = "检索";
            this.btnQuery.Click += new EventHandler(this.btnQuery_Click);
            base.AutoScaleDimensions = new SizeF(96f, 96f);
            //base.AutoScaleMode = AutoScaleMode.Dpi;
            this.AutoScroll = true;
            base.ClientSize = new Size(0xee, 0x127);
            base.ControlBox = false;
            base.Controls.Add(this.btnQuery);
            base.Controls.Add(this.lsvTasks);
            base.Controls.Add(this.btnBack);
            base.Controls.Add(this.btnInspect);
            base.Controls.Add(this.txtGosid);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.TasksTitile);
            //base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "InspectGoodsFrm";
            this.Text = "按货物检验";
            base.Deactivate += new EventHandler(this.InspectGoodsFrm_Deactivate);
            base.Load += new EventHandler(this.InspectGoodsFrm_Load);
            base.Activated += new EventHandler(this.InspectGoodsFrm_Activated);
            base.ResumeLayout(false);
        }

        private void InitializeLsvDetail()
        {
            this.lsvTasks.Columns.Clear();
            this.lsvTasks.Columns.Add(" ", 0x19, HorizontalAlignment.Center);
            this.lsvTasks.Columns.Add("检验单", 100, HorizontalAlignment.Center);
            this.lsvTasks.Columns.Add("货号", 100, HorizontalAlignment.Center);
            this.lsvTasks.Columns.Add("到数", 0x2d, HorizontalAlignment.Center);
            this.lsvTasks.Columns.Add("抽数", 0x2d, HorizontalAlignment.Center);
            this.lsvTasks.Columns.Add("库位", 40, HorizontalAlignment.Center);
            this.lsvTasks.Columns.Add("批次", 40, HorizontalAlignment.Center);
        }

        private void InspectGoodsFrm_Activated(object sender, EventArgs e)
        {
            OEM_Catchwell.SetBeamHold(false);
            this.txtGosid.Focus();
            this.txtGosid.SelectAll();
        }

        private void InspectGoodsFrm_Deactivate(object sender, EventArgs e)
        {
            OEM_Catchwell.SetBeamHold(true);
        }

        private void InspectGoodsFrm_Load(object sender, EventArgs e)
        {
            this.txtGosid.Focus();
            this.InitializeLsvDetail();
        }

        private void txtGosid_GotFocus(object sender, EventArgs e)
        {
            OEM_Catchwell.SetOpMode(0);
        }

        private void txtGosid_KeyDown(object sender, KeyEventArgs e)
        {
            if ((this.txtGosid.Text.Trim() != string.Empty) && (e.KeyCode == Keys.Return))
            {
                this.lsvTasks.Items.Clear();
                Cursor.Current = Cursors.WaitCursor;
                DataSet goodsInspectTasks = new DataSet();
                try
                {
                    goodsInspectTasks = this.service.GetGoodsInspectTasks(this.txtGosid.Text.Substring(1));
                }
                catch (ApplicationException exception)
                {
                    Cursor.Current = Cursors.Default;
                    MessageShow.Alert("错误", exception.Message);
                    this.txtGosid.Focus();
                    this.txtGosid.SelectAll();
                    return;
                }
                catch (Exception exception2)
                {
                    Cursor.Current = Cursors.Default;
                    MessageShow.Alert("错误", exception2.Message);
                    this.txtGosid.Focus();
                    this.txtGosid.SelectAll();
                    return;
                }
                Cursor.Current = Cursors.Default;
                DataTable table = goodsInspectTasks.Tables[0];
                if (table.Rows.Count != 0)
                {
                    this.txtGosid.Focus();
                    this.txtGosid.SelectAll();
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        ListViewItem item = new ListViewItem(new string[] { "", table.Rows[i]["ICNO"].ToString(), table.Rows[i]["Icmatcode"].ToString(), table.Rows[i]["ICNumber"].ToString(), table.Rows[i]["ICCheckNum"].ToString(), table.Rows[i]["ICSITE"].ToString(), table.Rows[i]["ICMATSN"].ToString() });
                        this.lsvTasks.Items.Add(item);
                    }
                }
                else
                {
                    MessageShow.Alert("error", "当前该货物不存在检验单");
                    this.txtGosid.Focus();
                    this.txtGosid.SelectAll();
                }
            }
        }
    }
}

