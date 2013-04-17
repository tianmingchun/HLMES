using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HL.Framework;
using HL.DAL;
using Printlib;
using BizLayer;
using System.Reflection;
using System.IO;
using Entity;
using System.Threading;

namespace HL.UI.CheckIn
{
    public partial class ProductOrderHistoryForm : Form
    {
        public ProductOrderHistoryForm()
        {
            InitializeComponent();
        }

        #region 返回
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 批量
        private void btnBatch_Click(object sender, EventArgs e)
        {
            btnSelectAll_Click(null, null);
            btnSubmit_Click(null, null);
        }
        #endregion

        #region 提交
       /// <summary>
        /// 需要按照收货批次分组。
        /// 不同批次的依次发送，分别获得凭证号，并分开打印。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            DataSet listPda = new DataSet();
            //按照Delivery_ID分组，每一组调用一次SAP提交接口，生成一个凭证号。
            Dictionary<string, ZMES_Task> group = new Dictionary<string, ZMES_Task>();
            int rowCount = 0;
            for (int i = 0; i < this.lsvTasks.Items.Count; i++)
            {
                if (this.lsvTasks.Items[i].Checked)
                {
                    DataRow row = (DataRow)this.lsvTasks.Items[i].Tag;
                    string deliveryId = DbValue.GetString(row["Delivery_ID"]);
                    ZMES_Task task;
                    if (!group.ContainsKey(deliveryId))
                    {
                        task = new ZMES_Task();
                        group.Add(deliveryId, task);
                    }
                    else
                    {
                        task = group[deliveryId];
                    }
                    task.Task_ID = deliveryId;
                    ZMES_Delivery item = new ZMES_Delivery();
                    item.Delivery_ID = deliveryId;
                    item.Del_Date = System.DateTime.Now.ToString("yyyy-MM-dd");
                    item.Del_Time = System.DateTime.Now.ToString("HH:mm:ss");
                    item.WEMNG = DbValue.GetDecimal(row["WEMNG"]);
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
                    item.UserName = DbValue.GetString(row["UserName"]);
                    item.FWerks = DbValue.GetString(row["FWerks"]);
                    item.MENGE = DbValue.GetDecimal(row["MENGE"]);
                    task.AddItem(item);
                    rowCount++;
                }
            }


            if (group.Count > 0)
            {
                int successCount = 0;
                string strMsg = string.Empty;
                string strErr = string.Empty;
                foreach (KeyValuePair<string, ZMES_Task> kvp in group)
                {
                    string bookNum = string.Empty;
                    try
                    {
                        bookNum = ServiceCaller.SetOuterOrderCheckInHistory(kvp.Value, out strMsg);
                        if (!string.IsNullOrEmpty(bookNum))
                        {
                            successCount++;
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
                                    break;
                                }
                            }
                            if (openPort)
                                PrintBill.PrintOuterOrderCheckIn(bookNum, kvp.Value.Items[0].EBELN, kvp.Value.Items[0].LIFNRName, listPda);
                        }
                        else
                        {
                            strErr = strMsg;
                        }
                    }
                    catch (Exception ex)
                    {
                        Cursor.Current = Cursors.Default;
                        //strErr = ex.Message;
                        //return;
                    }
                }
                Cursor.Current = Cursors.Default;
                string message = "您提交：" + group.Count + "笔"+ + rowCount + "行。其中成功：" + successCount + "笔，失败：" + (group.Count - successCount) + "笔。";
                message += strErr;
                MessageShow.Alert("提示", message);
                if (successCount > 0)
                {
                    LoadTodayDelivery();
                    LoadDeliveryFailure();
                }
                group.Clear();
            }
            else
            {
                MessageShow.Alert("error", "请先勾选要提交的物料");
            }
        }
        #endregion 

        #region 窗体事件

        public void SetSelectUnSummitPage()
        {
            this.tabControl1.SelectedIndex = 1;
        }
        private void PurchaseOrderHistoryForm_Load(object sender, EventArgs e)
        {
            LoadTodayDelivery();
            LoadDeliveryFailure();
        }
     
        /// <summary>
        /// 载入当日提交记录
        /// </summary>
        private void LoadTodayDelivery()
        {
            string lastId = string.Empty;
            Color lastColor = Color.White;
            this.LsvTodayDelivery.Items.Clear();
            DataSet set = Global.LocalService.GetOuterOrderCheckInDelivery();
            if (set.Tables.Count > 0 && set.Tables[0].Rows.Count != 0)
            {
                DataTable table = set.Tables[0];
                List<string> group = new List<string>();
                bool useColor = false;
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataRow row = table.Rows[i];
                    string deliveryId = DbValue.GetString(row["Delivery_ID"]);
                    if (!group.Contains(deliveryId))
                        group.Add(deliveryId);
                    string status = GetRowStatus(row);
                    ListViewItem item = new ListViewItem(new string[] { table.Rows[i]["EBELN"].ToString(), table.Rows[i]["MATNR"].ToString(), table.Rows[i]["WEMNG"].ToString(), table.Rows[i]["EBELP"].ToString(), table.Rows[i]["Del_Time"].ToString(), status });
                    item.Tag = row;
                    if (i > 0 && lastId != deliveryId)
                    {
                        useColor = !useColor;
                        if (useColor)
                            item.BackColor = AlternateBackColor;
                        else
                            item.BackColor = Color.White;
                    }
                    else
                    {
                        item.BackColor = lastColor;
                    }
                    lastId = deliveryId;
                    lastColor = item.BackColor;
                    this.LsvTodayDelivery.Items.Add(item);
                }
                this.lblTodayCount.Text = group.Count + "笔，" + set.Tables[0].Rows.Count + "行";
            }
            else
            {
                this.lblTodayCount.Text = "0笔，0行";
            }
        }

        private string GetRowStatus(DataRow row)
        {
            string status = "";
            string syn_State = DbValue.GetString(row["Syn_State"]);
            if (string.IsNullOrEmpty(syn_State))
            {
                status = "未同步";
            }
            else
            {
                string del_Remark = DbValue.GetString(row["Del_Remark"]);
                string del_State = DbValue.GetString(row["Del_State"]);
                if (del_State == "S")
                {
                    status = "收货成功，凭证号：" + del_Remark;
                }
                else if (del_State == "E")
                {
                    status = "收货失败，" + del_Remark;
                }
                else
                {
                    status = "已同步";
                }
            }
            return status;
        }
        public static Color AlternateBackColor = Color.Pink;
        /// <summary>
        /// SAP未收货记录
        /// </summary>
        private void LoadDeliveryFailure()
        {
            this.lsvTasks.Items.Clear();
            DataSet set = Global.LocalService.GetOuterOrderCheckInFailure();
            string lastId = string.Empty;
            Color lastColor = Color.White;
            if (set.Tables.Count > 0 && set.Tables[0].Rows.Count != 0)
            {
                List<string> group = new List<string>();
                DataTable table = set.Tables[0];
                bool useColor = false;
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataRow row = table.Rows[i];
                    string deliveryId = DbValue.GetString(row["Delivery_ID"]);
                    if (!group.Contains(deliveryId))
                        group.Add(deliveryId);
                    //订单数量
                    decimal orderNum = DbValue.GetDecimal(row["MENGE"]);
                    //收货数量
                    decimal weiqingNum = DbValue.GetDecimal(row["WEMNG"]);
                    string status = GetRowStatus(row);
                    ListViewItem item = new ListViewItem(new string[] { "", table.Rows[i]["EBELN"].ToString(), table.Rows[i]["MATNR"].ToString(), weiqingNum.ToString(), orderNum.ToString(), table.Rows[i]["EBELP"].ToString(), table.Rows[i]["MEINS"].ToString(), status });
                    item.Tag = row;
                    if (i > 0 && lastId != deliveryId)
                    {
                        useColor = !useColor;
                        if (useColor)
                            item.BackColor = AlternateBackColor;
                        else
                            item.BackColor = Color.White;
                    }
                    else
                    {
                        item.BackColor = lastColor;
                    }
                    lastId = deliveryId;
                    lastColor = item.BackColor;
                    this.lsvTasks.Items.Add(item);
                }
                this.lblTaskCount.Text = group.Count + "笔，" + set.Tables[0].Rows.Count + "行";
            }
            else
            {
                this.lblTaskCount.Text = "0笔，0行";
            }
        }
        #endregion

        #region 控件键盘事件

        private void LsvTodayDelivery_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.LsvTodayDelivery.SelectedIndices.Count > 0)
            {
                DataRow row = (DataRow)this.LsvTodayDelivery.Items[this.LsvTodayDelivery.SelectedIndices[0]].Tag;
                this.lblMaterialName.Text = DbValue.GetString(row["MAKTX"]);
            }
        }


        private void lsvTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lsvTasks.SelectedIndices.Count > 0)
            {
                DataRow row = (DataRow)this.lsvTasks.Items[this.lsvTasks.SelectedIndices[0]].Tag;
                this.lblMaterialName2.Text = DbValue.GetString(row["MAKTX"]);
            }
        }
        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            _handing = true;
            Cursor.Current = Cursors.WaitCursor;
            for (int i = 0; i < this.lsvTasks.Items.Count; i++)
            {
                this.lsvTasks.Items[i].Checked = true;
            }
            Cursor.Current = Cursors.Default;
            _handing = false;
        }

        private void btnSelectNotAll_Click(object sender, EventArgs e)
        {
            _handing = true;
            Cursor.Current = Cursors.WaitCursor;
            for (int i = 0; i < this.lsvTasks.Items.Count; i++)
            {
                this.lsvTasks.Items[i].Checked = false;
            }
            Cursor.Current = Cursors.Default;
            _handing = false;
        }

        private bool _handing;
        private void lsvTasks_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (!_handing)
            {
                _handing = true;
                Cursor.Current = Cursors.WaitCursor;
                bool check = e.NewValue == CheckState.Checked;
                DataRow selectedRow = (DataRow)this.lsvTasks.Items[e.Index].Tag;
                string deliveryId = DbValue.GetString(selectedRow["Delivery_ID"]);
                for (int i = 0; i < this.lsvTasks.Items.Count; i++)
                {
                    DataRow row = (DataRow)this.lsvTasks.Items[i].Tag;
                    if (DbValue.GetString(row["Delivery_ID"]) == deliveryId)
                    {
                        this.lsvTasks.Items[i].Checked = check;
                    }
                }
                Cursor.Current = Cursors.Default;
                _handing = false;
            }
        }
        #endregion    

        #region 删除
        /// <summary>
        /// 未同步的直接删除本地。
        /// 已同步不成功的，需要联网删除。
        /// 只能按Delivery_ID整批次删除，不能按行项目删除。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int successCount = 0;
            int failureCount = 0;
            string strMsg = string.Empty;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                List<string> group = new List<string>();
                for (int i = 0; i < this.lsvTasks.Items.Count; i++)
                {
                    if (this.lsvTasks.Items[i].Checked)
                    {
                        DataRow row = (DataRow)this.lsvTasks.Items[i].Tag;
                        string deliveryId = DbValue.GetString(row["Delivery_ID"]);
                        //同一分组，只调用一次服务。
                        if (!group.Contains(deliveryId))
                        {
                            group.Add(deliveryId);
                        }
                        else
                        {
                            continue;
                        }
                        string syn_State = DbValue.GetString(row["Syn_State"]);
                        if (string.IsNullOrEmpty(syn_State))//未同步直接删除本地
                        {
                            if (Global.LocalService.DeleteOuterOrderCheckIn(deliveryId, out strMsg))
                            {
                                successCount++;
                            }
                            else
                            {
                                failureCount++;
                            }
                        }
                        else//调用SAP服务删除
                        {
                            //先删除SAP，再删除本地，如果未联机则不允许删除。
                            if (ServiceCaller.CheckNetworkService())
                            {
                                if (Global.RemoteService.DeleteOuterOrderCheckIn(deliveryId, out strMsg))
                                {
                                    successCount++;
                                }
                                else
                                {
                                    failureCount++;
                                }
                            }
                            else
                            {
                                failureCount++;
                                strMsg = "未联网，无法删除数据。";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageShow.Alert("error", ex.Message);
                return;
            }
            if (successCount > 0)
            {
                this.LoadDeliveryFailure();
            }
            Cursor.Current = Cursors.Default;
            if (failureCount > 0)
                MessageShow.Alert("提示", "成功：" + successCount + "批，失败：" + failureCount + "批。" + strMsg);
            else
                MessageShow.Alert("提示", "成功删除：" + successCount + "批。");
        }
        #endregion


    }
}