using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HL.Framework;
using System.Text.RegularExpressions;
using System.Reflection;
using System.IO;
using System.Threading;
using Printlib;
using BizLayer;
using HL.DAL;
using System.Collections;
using Entity;
using HL.Device;
using HL.UI.Common;

namespace HL.UI.CheckIn
{
    public partial class ProductOrderForm : Form
    {
        #region Vars
        private string strresult;
        private string userName;
        #endregion

        #region Construct
        public ProductOrderForm()
        {
            InitializeComponent();
        }
        #endregion

        #region 修改到货数量
        private void btnUpdateQty_Click(object sender, EventArgs e)
        {
            if ((this.lsvTasks.SelectedIndices != null) && (this.lsvTasks.SelectedIndices.Count != 0))
            {
                if (!this.lsvTasks.Items[this.lsvTasks.SelectedIndices[0]].Checked)
                {
                    MessageShow.Alert("error", "请先勾选要修改数量的物料");
                }
                else if (this.txtQty.Text == "")
                {
                    MessageShow.Alert("error", "请输入实际到货数");
                }
                else if (this.CheckQuantity(this.txtQty.Text))
                {
                    if (decimal.Parse(this.txtQty.Text) > decimal.Parse(this.lsvTasks.Items[this.lsvTasks.SelectedIndices[0]].SubItems[3].Text))
                    {
                        MessageShow.Alert("error", "收货数不能大于任务数");
                        this.txtQty.Focus();
                        this.txtQty.SelectAll();
                    }
                    else
                    {
                        this.lsvTasks.Items[this.lsvTasks.SelectedIndices[0]].SubItems[2].Text = this.txtQty.Text;
                        this.txtQty.Text = "";
                    }
                }
                else
                {
                    MessageShow.Alert("error", "输入的数量不合法");
                    this.txtQty.Focus();
                    this.txtQty.SelectAll();
                }
            }
        }

        public bool CheckQuantity(string count)
        {
            Regex regex = new Regex(@"^\d+(\.\d{1,3})?$");
            if (!regex.IsMatch(count))
            {
                return false;
            }
            return true;
        }

        #endregion

        #region 窗体事件
        private void OutsideOrderForm_Load(object sender, EventArgs e)
        {
            this.txtOrderID.Focus();
        }

        private void OutsideOrderForm_Deactivate(object sender, EventArgs e)
        {

        }

        private void OutsideOrderForm_Activated(object sender, EventArgs e)
        {
            this.txtOrderID.Focus();
            this.txtOrderID.SelectAll();
        }      

        private void OutsideOrderForm_KeyDown(object sender, KeyEventArgs e)
        {
            DialogResult none = DialogResult.None;
            if (e.KeyValue == 0x84)
            {
                none = new ConfigFrm().ShowDialog();
            }
            if (none == DialogResult.OK)
            {
                this.btnQuery_Click(null, null);
            }
        }

        private void PurchaseOrderForm_Closing(object sender, CancelEventArgs e)
        {
            int failureCount = Global.LocalService.GetOuterOrderCheckInFailureCount();
            if (failureCount > 0)
            {
                if (MessageBox.Show("存在SAP未收货的记录，现在要提交SAP吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    e.Cancel = true;
                    using (OuterOrderHistoryForm dialog = new OuterOrderHistoryForm())
                    {
                        dialog.SetSelectUnSummitPage();
                        dialog.ShowDialog();
                    }
                }
            }
        }
        #endregion

        #region 返回
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 提交
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            DataSet listPda = new DataSet();
            ZMES_Task task = new ZMES_Task();
            task.Task_ID = Guid.NewGuid().ToString();
            for (int i = 0; i < this.lsvTasks.Items.Count; i++)
            {
                if (this.lsvTasks.Items[i].Checked)
                {
                    ZMES_Delivery item = new ZMES_Delivery();
                    item.Delivery_ID = task.Task_ID;
                    item.Del_Date = System.DateTime.Now.ToString("yyyy-MM-dd");
                    item.Del_Time = System.DateTime.Now.ToString("HH:mm:ss");
                    item.WEMNG = decimal.Parse(this.lsvTasks.Items[i].SubItems[2].Text.ToString());
                    DataRow row = (DataRow)this.lsvTasks.Items[i].Tag;
                    item.BSART = DbValue.GetString(row["BSART"]);
                    item.BUKRS = DbValue.GetString(row["BUKRS"]);
                    item.EBELN = DbValue.GetString(row["EBELN"]);
                    item.EBELP = DbValue.GetString(row["EBELP"]);
                    item.EINDT = DbValue.GetString(row["EINDT"]);
                    item.EKGRP = DbValue.GetString(row["EKGRP"]);
                    item.SWerks = DbValue.GetString(row["SWerks"]);
                    item.LGOBE = DbValue.GetString(row["LGOBE"]);
                    item.LIFNR = DbValue.GetString(row["LIFNR"]);
                    item.LIFNRName = DbValue.GetString(row["LIFNRName"]);
                    item.MAKTX = DbValue.GetString(row["MAKTX"]);
                    item.MATNR = DbValue.GetString(row["MATNR"]);
                    item.MEINS = DbValue.GetString(row["MEINS"]);
                    item.UserName = Global.CurrUser.Name;
                    item.FWerks = DbValue.GetString(row["FWerks"]);
                    item.MENGE = DbValue.GetDecimal(row["MENGE"]);
                    task.AddItem(item);
                }
            }
            if (task.Items.Count > 0)
            {
                ServiceOnline so;
                string strMsg = "";
                string bookNum = string.Empty;
                try
                {
                    bookNum = ServiceCaller.SetOuterOrderCheckIn(task, out strMsg, out so);
                    Cursor.Current = Cursors.Default;
                    MessageShow.Alert("提示", strMsg);
                    this.DoQuery(false);
                    if (!string.IsNullOrEmpty(bookNum))
                    {
                        //打印之前询问用户，是否需要打印，待完善？？？？？？？？？、
                        //按照需求文档，外部采购订单需要打印，工厂间订单无需打印。
                        bool openPort = PrintBill.IsOpenPort();
                        while (!openPort)
                        {
                            if (MessageBox.Show("打开蓝牙打印机失败，现在要连接打印机吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                            {
                                new ConfigMain().btnConfigPrinter_Click(null, null);
                                openPort = PrintBill.IsOpenPort();
                            }
                            else
                            {
                                Cursor.Current = Cursors.Default;
                                return;
                            }
                        }
                        PrintBill.PrintOuterOrderCheckIn(bookNum, task.Items[0].EBELN, task.Items[0].LIFNRName, listPda);
                    }
                }

                catch (Exception ex)
                {
                    Cursor.Current = Cursors.Default;
                   // MessageShow.Alert("Error", ex.Message);
                  //  return;
                }
            }
            else
            {
                Cursor.Current = Cursors.Default;
                MessageShow.Alert("error", "请先勾选要提交的物料");
            }
        }
        #endregion

        #region 批量
        private void btnBatch_Click(object sender, EventArgs e)
        {
            btnSelectAll_Click(null, null);
            btnSubmit_Click(null, null);
        }
        #endregion

        #region 查询
        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (this.txtOrderID.Text == "")
            {
                MessageShow.Alert("error", "请输入采购订单号");
                this.txtOrderID.Focus();
            }
            else
            {
                DoQuery(true);
            }
        }

        private void DoQuery(bool showMsg)
        {
            this.lsvTasks.Items.Clear();
            Cursor.Current = Cursors.WaitCursor;
            DataSet set = new DataSet();
            ServiceOnline so;
            try
            {
                set = ServiceCaller.GetOuterOrderCheckIn(Management.GetSingleton().WarehouseNo, this.txtOrderID.Text, out so);
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                if (showMsg)
                    MessageShow.Alert("错误", ex.Message);
                this.txtOrderID.Focus();
                this.txtOrderID.SelectAll();
                return;
            }
            Cursor.Current = Cursors.Default;
            if (so == ServiceOnline.Offline)
            {
                this.lblMaterialName.Text = "未联网";
                this.lblMaterialName.ForeColor = Color.Red;
            }
            else
            {
                this.lblMaterialName.Text = "联网";
                this.lblMaterialName.ForeColor = Color.Green;
            }
            if (set.Tables.Count > 0 && set.Tables[0].Rows.Count != 0)
            {
                DataTable table = set.Tables[0];
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataRow row = table.Rows[i];
                    //订单数量
                    decimal orderNum = Convert.ToDecimal(row["MENGE"]);
                    //未清数量
                    decimal weiqingNum = Convert.ToDecimal(row["OBMNG"]);
                    //已收数量
                    decimal yjshouNum = Convert.ToDecimal(row["WEMNG"]);
                    //供应商名称
                    this.lblSupplyname.Text = DbValue.GetString(row["LIFNRName"]);
                    //供应商编码
                    this.lblSupplyID.Text = DbValue.GetString(row["LIFNR"]);
                    //交货日期
                    this.lblDate.Text = Convert.ToDateTime(table.Rows[i]["EINDT"]).ToString("yyyyMMdd");
                    //this.lblProid.Text = table.Rows[i]["PROID"].ToString();
                    ListViewItem item = new ListViewItem(new string[] { "", table.Rows[i]["MATNR"].ToString(), weiqingNum.ToString(), weiqingNum.ToString(), yjshouNum.ToString(), table.Rows[i]["MEINS"].ToString() });
                    item.Tag = row;
                    this.lsvTasks.Items.Add(item);
                }
            }
            else
            {
                if (showMsg)
                    MessageShow.Alert("error", "该采购订单没有任务明细");
                this.txtOrderID.Focus();
                this.txtOrderID.SelectAll();
                this.lsvTasks.Items.Clear();
            }
        }

        #endregion

        #region 控件键盘事件
        private void txtOrderID_GotFocus(object sender, EventArgs e)
        {

        }

        private void txtOrderID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                this.btnQuery_Click(sender, e);
            }
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                this.btnUpdateQty_Click(sender, e);
            }
        }

        private void lsvTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lsvTasks.SelectedIndices.Count > 0)
            {
                DataRow row = (DataRow)this.lsvTasks.Items[this.lsvTasks.SelectedIndices[0]].Tag;
                this.lblMaterialName.Text = DbValue.GetString(row["MAKTX"]);
            }
        }
        #endregion

        #region 全选、全不选
        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            for (int i = 0; i < this.lsvTasks.Items.Count; i++)
            {
                this.lsvTasks.Items[i].Checked = true;
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnSelectNotAll_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            for (int i = 0; i < this.lsvTasks.Items.Count; i++)
            {
                this.lsvTasks.Items[i].Checked = false;
            }
            Cursor.Current = Cursors.Default;
        }
        #endregion
      
        #region 跟踪

        private void btnHistory_Click(object sender, EventArgs e)
        {
            using (OuterOrderHistoryForm dialog = new OuterOrderHistoryForm())
            {
                dialog.ShowDialog();
            }
        }
        #endregion

        #region  选择订单号
        private void btnChooseOrderID_Click(object sender, EventArgs e)
        {
            using (QueryOrderForm dialog = new QueryOrderForm())
            {
                dialog.TaskType = HL.Framework.TaskType.ProductOrderCheckIn;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.txtOrderID.Text = dialog.SelectedOrderID;
                    btnQuery_Click(null, null);
                }
            }
        }
        #endregion

        #region 数据下载
        private void btnDownload_Click(object sender, EventArgs e)
        {
            using (DownloadInForm dialog = new DownloadInForm())
            {
                dialog.Text = "生产订单收货数据下载";
                dialog.TaskType = HL.Framework.TaskType.OuterOrderCheckIn;
                dialog.ShowDialog();
            }
        }
        #endregion
    }
}