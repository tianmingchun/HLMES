namespace HL.UI
{
    using BizLayer;
    using Entity;
    using SharpExCS;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using HL.Framework;
    using HL.Controls;

    public class MoveOutByGoods : Form
    {
        private Button btnExit;
        private Button btnSubmit;
        private IContainer components;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label lblFromWarehouse;
        private Label LblQty;
        private RadioButton rabLocation;
        private RadioButton rabOrder;
        private DialogResult ree;
        private const int RET_KEYSTROKE = 0;
        private decimal storyQty;
        private HeadLabel TasksTitile;
        private TextBox txtGosid;
        private TextBox txtQty;
        private TextBox txtToOrder;
        private TextBox txtToWarehoues;

        public MoveOutByGoods()
        {
            this.InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.txtGosid.Text == string.Empty)
            {
                MessageShow.Alert("Error", "物料不能为空");
            }
            else if (this.txtQty.Text == string.Empty)
            {
                MessageShow.Alert("Error", "数量不能为空");
            }
            else if (this.rabLocation.Checked && (this.txtToWarehoues.Text == string.Empty))
            {
                MessageShow.Alert("Error", "发往库房不能为空");
            }
            else if (this.rabOrder.Checked && (this.txtToOrder.Text == string.Empty))
            {
                MessageShow.Alert("Error", "发往订单不能为空");
            }
            else
            {
                if (this.LblQty.Text == string.Empty)
                {
                    this.GetMaterialStockQty();
                }
                MoveGoods movegoods = new MoveGoods();
                movegoods.GoodsID = this.txtGosid.Text.Substring(1);
                movegoods.Qty = decimal.Parse(this.txtQty.Text.ToString());
                movegoods.Fromwarehouse = this.lblFromWarehouse.Text;
                movegoods.Towarehouse = this.txtToWarehoues.Text;
                movegoods.Toorder = this.txtToOrder.Text;
                if (this.rabLocation.Checked)
                {
                    movegoods.Outtype = "1";
                }
                else if (this.rabOrder.Checked)
                {
                    movegoods.Outtype = "2";
                }
                string msgExrror = string.Empty;
                bool flag = false;
                if (this.Verify())
                {
                    try
                    {
                        flag = Global.MiddleService.SetStorTrans(movegoods, ref msgExrror);
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
                    MessageShow.Alert("提示", msgExrror);
                }
                if (flag)
                {
                    if (this.rabLocation.Checked)
                    {
                        this.label5.Text = "业务执行成功，已向" + this.txtToWarehoues.Text + "仓库发" + this.txtGosid.Text.Substring(1) + "物料" + this.txtQty.Text + "数量";
                    }
                    else
                    {
                        this.label5.Text = "业务执行成功，已向" + this.txtToOrder.Text + "订单发" + this.txtGosid.Text.Substring(1) + "物料" + this.txtQty.Text + "数量";
                    }
                    this.txtQty.Text = "";
                    this.txtToWarehoues.Text = "";
                    this.txtToOrder.Text = "";
                    this.txtGosid.Focus();
                    this.GetMaterialStockQty();
                }
            }
        }

        public bool CheckQuantity(string count)
        {
            Regex regex = new Regex(@"^-?\d+(\.\d{1,3})?$");
            if (!regex.IsMatch(count))
            {
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

        private void GetMaterialStockQty()
        {
            string msgExrror = string.Empty;
            this.storyQty = Global.MiddleService.GetInventoryQty(Management.GetSingleton().WarehouseNo.Substring(0, 4).ToString(), this.txtGosid.Text.Substring(1).ToString(), Management.GetSingleton().WarehouseNo, ref msgExrror);
            this.LblQty.Text = this.storyQty.ToString();
            this.txtQty.Focus();
        }

        private void InitializeComponent()
        {
            this.LblQty = new Label();
            this.label3 = new Label();
            this.txtGosid = new TextBox();
            this.label1 = new Label();
            this.label2 = new Label();
            this.label4 = new Label();
            this.txtQty = new TextBox();
            this.lblFromWarehouse = new Label();
            this.txtToWarehoues = new TextBox();
            this.btnSubmit = new Button();
            this.btnExit = new Button();
            this.TasksTitile = new HeadLabel();
            this.rabLocation = new RadioButton();
            this.rabOrder = new RadioButton();
            this.txtToOrder = new TextBox();
            this.label5 = new Label();
            base.SuspendLayout();
            this.LblQty.ForeColor = Color.Red;
            this.LblQty.Location = new Point(0xbc, 0x4c);
            this.LblQty.Name = "LblQty";
            this.LblQty.Size = new Size(0x2f, 20);
            this.label3.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label3.Location = new Point(150, 0x4c);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x2e, 20);
            this.label3.Text = "库存：";
            this.txtGosid.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.txtGosid.Location = new Point(0x2c, 30);
            this.txtGosid.Name = "txtGosid";
            this.txtGosid.Size = new Size(100, 0x15);
            this.txtGosid.TabIndex = 0x36;
            this.txtGosid.GotFocus += new EventHandler(this.txtGosid_GotFocus);
            this.txtGosid.KeyDown += new KeyEventHandler(this.txtGosid_KeyDown);
            this.label1.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label1.Location = new Point(3, 0x21);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x34, 20);
            this.label1.Text = "物料：";
            this.label2.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label2.Location = new Point(3, 0x73);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x42, 20);
            this.label2.Text = "发出库房:";
            this.label4.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label4.Location = new Point(3, 0x4c);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x34, 20);
            this.label4.Text = "数量：";
            this.txtQty.Location = new Point(0x2c, 0x49);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new Size(100, 0x17);
            this.txtQty.TabIndex = 0x3b;
            this.txtQty.KeyDown += new KeyEventHandler(this.txtQty_KeyDown);
            this.lblFromWarehouse.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.lblFromWarehouse.Location = new Point(0x42, 0x73);
            this.lblFromWarehouse.Name = "lblFromWarehouse";
            this.lblFromWarehouse.Size = new Size(0x4e, 20);
            this.txtToWarehoues.Enabled = false;
            this.txtToWarehoues.Location = new Point(0x5f, 0x87);
            this.txtToWarehoues.Name = "txtToWarehoues";
            this.txtToWarehoues.Size = new Size(100, 0x17);
            this.txtToWarehoues.TabIndex = 0x40;
            this.btnSubmit.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnSubmit.Location = new Point(0x11, 0xc4);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new Size(0x48, 0x1c);
            this.btnSubmit.TabIndex = 0x41;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.Click += new EventHandler(this.btnSubmit_Click);
            this.btnExit.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.btnExit.Location = new Point(0x7c, 0xc4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new Size(0x48, 0x1c);
            this.btnExit.TabIndex = 0x42;
            this.btnExit.Text = "取消";
            this.btnExit.Click += new EventHandler(this.btnExit_Click);
            this.TasksTitile.BackColor = SystemColors.HotTrack;
            this.TasksTitile.Font = new Font("Arial", 14f, FontStyle.Regular);
            this.TasksTitile.ForeColor = SystemColors.ActiveCaptionText;
            this.TasksTitile.Location = new Point(0, 0);
            this.TasksTitile.Name = "TasksTitile";
            this.TasksTitile.Size = new Size(0xed, 0x18);
            this.TasksTitile.TabIndex = 0x38;
            this.TasksTitile.Text = "特殊发料";
            this.rabLocation.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.rabLocation.Location = new Point(4, 0x8a);
            this.rabLocation.Name = "rabLocation";
            this.rabLocation.Size = new Size(0x55, 20);
            this.rabLocation.TabIndex = 0x4a;
            this.rabLocation.Text = "发往库房";
            this.rabLocation.Click += new EventHandler(this.rabLocation_Click);
            this.rabOrder.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.rabOrder.Location = new Point(3, 0xa7);
            this.rabOrder.Name = "rabOrder";
            this.rabOrder.Size = new Size(0x55, 20);
            this.rabOrder.TabIndex = 0x4b;
            this.rabOrder.Text = "发往订单";
            this.rabOrder.Click += new EventHandler(this.rabOrder_Click);
            this.txtToOrder.Enabled = false;
            this.txtToOrder.Location = new Point(0x5e, 0xa4);
            this.txtToOrder.Name = "txtToOrder";
            this.txtToOrder.Size = new Size(100, 0x17);
            this.txtToOrder.TabIndex = 0x4c;
            this.label5.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            this.label5.ForeColor = Color.Red;
            this.label5.Location = new Point(4, 0xe3);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0xe7, 0x36);
            base.AutoScaleDimensions = new SizeF(96f, 96f);
            //base.AutoScaleMode = AutoScaleMode.Dpi;
            this.AutoScroll = true;
            base.ClientSize = new Size(240, 290);
            base.ControlBox = false;
            base.Controls.Add(this.label5);
            base.Controls.Add(this.txtToOrder);
            base.Controls.Add(this.rabOrder);
            base.Controls.Add(this.rabLocation);
            base.Controls.Add(this.btnExit);
            base.Controls.Add(this.btnSubmit);
            base.Controls.Add(this.txtToWarehoues);
            base.Controls.Add(this.lblFromWarehouse);
            base.Controls.Add(this.txtQty);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.TasksTitile);
            base.Controls.Add(this.LblQty);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.txtGosid);
            base.Controls.Add(this.label1);
            //base.FormBorderStyle = FormBorderStyle.None;
            base.KeyPreview = true;
            base.Name = "MoveOutByGoods";
            this.Text = "按物料发料";
            base.Deactivate += new EventHandler(this.MoveOutByGoods_Deactivate);
            base.Load += new EventHandler(this.MoveOutByGoods_Load);
            base.Activated += new EventHandler(this.MoveOutByGoods_Activated);
            base.KeyDown += new KeyEventHandler(this.MoveOutByGoods_KeyDown);
            base.ResumeLayout(false);
        }

        private void MoveOutByGoods_Activated(object sender, EventArgs e)
        {
            OEM_Catchwell.SetBeamHold(false);
            this.txtGosid.Focus();
            this.txtGosid.SelectAll();
        }

        private void MoveOutByGoods_Deactivate(object sender, EventArgs e)
        {
            OEM_Catchwell.SetBeamHold(true);
        }

        private void MoveOutByGoods_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 0x84)
            {
                this.ree = new ConfigFrm().ShowDialog();
            }
            if (this.ree == DialogResult.OK)
            {
                this.lblFromWarehouse.Text = Management.GetSingleton().WarehouseNo;
            }
        }

        private void MoveOutByGoods_Load(object sender, EventArgs e)
        {
            this.lblFromWarehouse.Text = Management.GetSingleton().WarehouseNo;
            this.rabLocation.Checked = true;
            this.txtToWarehoues.Enabled = true;
        }

        private void rabLocation_Click(object sender, EventArgs e)
        {
            if (this.rabLocation.Checked)
            {
                this.txtToWarehoues.Enabled = true;
                this.txtToWarehoues.Focus();
                this.txtToOrder.Enabled = false;
                this.txtToOrder.Text = "";
            }
        }

        private void rabOrder_Click(object sender, EventArgs e)
        {
            if (this.rabOrder.Checked)
            {
                this.txtToOrder.Enabled = true;
                this.txtToOrder.Focus();
                this.txtToWarehoues.Enabled = false;
                this.txtToWarehoues.Text = "";
            }
        }

        private void txtGosid_GotFocus(object sender, EventArgs e)
        {
            OEM_Catchwell.SetOpMode(0);
        }

        private void txtGosid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                this.GetMaterialStockQty();
            }
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                this.txtToWarehoues.Focus();
            }
        }

        private bool Verify()
        {
            int num = -1;
            string msgError = string.Empty;
            if (this.CheckQuantity(this.txtQty.Text))
            {
                if (decimal.Parse(this.txtQty.Text.ToString()) > decimal.Parse(this.LblQty.Text.ToString()))
                {
                    MessageShow.Alert("Error", "指定的数量超出库存");
                    this.txtQty.Focus();
                    this.txtQty.SelectAll();
                    return false;
                }
            }
            else
            {
                MessageShow.Alert("Error", "指定的数量不合法");
                this.txtQty.Focus();
                this.txtQty.SelectAll();
                return false;
            }
            try
            {
                num = Global.MiddleService.CheckStuid(this.txtToWarehoues.Text, ref msgError);
            }
            catch (ApplicationException exception)
            {
                Cursor.Current = Cursors.Default;
                MessageShow.Alert("错误", exception.Message);
                return false;
            }
            catch (Exception exception2)
            {
                Cursor.Current = Cursors.Default;
                MessageShow.Alert("错误", exception2.Message);
                return false;
            }
            if (num != 0)
            {
                MessageShow.Alert("Error", "指定的发往仓库代码不合法");
                this.txtToWarehoues.Focus();
                this.txtToWarehoues.SelectAll();
                return false;
            }
            return true;
        }
    }
}

