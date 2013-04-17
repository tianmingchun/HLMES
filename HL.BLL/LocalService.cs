using System;
using System.Collections.Generic;
using System.Text;
using Entity;
using HL.DAL;
using System.IO;
using System.Collections;
using BizLayer.ITF_PDA;
using System.Net;
using BizLayer;
using System.Data;

namespace HL.BLL
{
    /// <summary>
    /// 移动端SQLite数据服务。
    /// (1)提交的数据首先存储到本地SQLite数据库中，
    /// (2)网络正常时，将本地SQLite库中待提交数据，再次提交到SAP中。
    /// </summary>
    public class LocalService
    {
        #region Vars
        public string ApplicationPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
        /// <summary>
        /// 数据库文件名称mobile_mes.db3。
        /// </summary>
        private string _databaseFile = "mobile_mes.db3";
        /// <summary>
        /// 数据库操作对象。
        /// </summary>
        private DataAccess _dataAccess;
        private MiddleService _middleService = new MiddleService();
        #endregion

        #region Construct
        /// <summary>
        /// 移动端SQLite数据服务，
        /// SQLite数据库名称为：mobile_mes.db3，
        /// 且必须放在移动客户端应用程序根目录下。
        /// 系统登录显示进度时，初始化此类！
        /// </summary>
        public LocalService()
        {
            if (_dataAccess == null)
            {
                string databasePath = Path.Combine(ApplicationPath, _databaseFile);
                _dataAccess = DataAccessFactory.CreateDataAccess("SQLite", "Data Source=" + databasePath);
            }
        }
        #endregion

        #region DataAccess
        public DataAccess DataAccess
        {
            get
            {
                return _dataAccess;
            }
        }
        #endregion

        #region 外部采购订单收货

        #region 数据插入本地SQLite库
        /// <summary>
        ///提交SAP之前先调用此方法，把数据插入本地SQLite库。
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="proid"></param>
        /// <param name="supplyid"></param>
        /// <param name="loctionid"></param>
        /// <param name="userid"></param>
        /// <param name="t"></param>
        public void SetOuterOrderCheckInLocal(ZMES_Task task)
        {
            foreach (KeyValuePair<int, ZMES_Delivery> kvp in task.Items)
            {
                ZMES_Delivery item = kvp.Value as ZMES_Delivery;
                string commandText = string.Format(@"insert into ZMES_Delivery(Delivery_ID,username,EBELN,EBELP,WEMNG,MATNR,MAKTX,MENGE,
                                            MEINS,FWerks,LGOBE,EINDT,BUKRS,BSART,EKGRP,LIFNR,
                                            LIFNRName,SWerks,Del_DATE,Del_Time)
                                            values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',
                                            '{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}',
                                            '{17}','{18}','{19}')",
                       item.Delivery_ID, item.UserName, item.EBELN, item.EBELP, item.WEMNG, item.MATNR, item.MAKTX, item.MENGE,
                       item.MEINS, item.FWerks, item.LGOBE, item.EINDT, item.BUKRS, item.BSART, item.EKGRP, item.LIFNR,
                       item.LIFNRName, item.SWerks, item.Del_Date, item.Del_Time);
                _dataAccess.ExecuteNonQuery(commandText);
                ////根据订单号和行号，更新本地收货记录表
                //commandText = string.Format("update ZMES_EKKO set OBMNG=OBMNG-{0},WEMNG=WEMNG+{0} where EBELN='{1}' and EBELP='{2}'",
                //    item.WEMNG,item.EBELN,item.EBELP);
                //_dataAccess.ExecuteNonQuery(commandText);
            }
        }

        /// <summary>
        /// 获得存储在本地SQLite的任务记录。
        /// 1.未提交的记录
        /// 2.提交失败的记录
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="loctionid"></param>
        /// <returns></returns>
        public ZMES_Task GetOuterOrderHistoryTask(string orderid, string loctionid)
        {
            string str = string.Empty;
            //考虑同一个订单，收货多次对应多个凭证的情况，ZMES_Delivery表每次收货提交会新产生一个Task_ID号，
            //同一个Task_ID发送单独发送一次
            string commandText = string.Format("select * from ZMES_Delivery where EBELN='{0}' and JWerks='{1}'",
                orderid, loctionid);
            DataTable dtDelivery = _dataAccess.ExecuteDataTable(commandText);

            ZMES_Task task = new ZMES_Task();

            for (int i = 0; i < dtDelivery.Rows.Count; i++)
            {
                ZMES_Delivery item = new ZMES_Delivery();
                DataRow row = dtDelivery.Rows[i];
                item.Delivery_ID = DbValue.GetString(row["Delivery_ID"]);
                item.Del_Date = DbValue.GetString(row["Del_Date"]);
                item.Del_Time = DbValue.GetString(row["Del_Time"]);
                item.WEMNG = DbValue.GetDecimal(row["BATCH"]);
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
                task.Task_ID = DbValue.GetString(row["Delivery_ID"]);
            }
            return task;
        }

        /// <summary>
        /// SAP联网收货成功后，更新本地SQLite库数据。
        /// 对应本地存储的非联网收货记录，再发送SAP时，不需要调用此方法。
        /// 因为存储本地的时候，就已经更新本地记录了。
        /// 根据订单号和行号号更新。
        /// 如果本地库没有对应数据，不做操作。
        /// 否则更新字段：
        /// OBMNG：未清数量-当前收货数量
        /// WEMNG：收货数量+当前收货数量
        /// </summary>
        /// <param name="task"></param>
        public void UpdateOuterOrderCheckInLocal(ZMES_Task task)
        {
            foreach (KeyValuePair<int, ZMES_Delivery> kvp in task.Items)
            {
                ZMES_Delivery item = kvp.Value;
                string commandText = "update ZMES_EKKO set OBMNG=OBMNG-" + item.WEMNG + " and WEMNG=WEMNG+" + item.WEMNG + " where EBELN='" + item.EBELN + "' and EBELP='" + item.EBELP + "'";
                _dataAccess.ExecuteNonQuery(commandText);
            }
        }       
    
        #endregion

        #region 获得采购订单收货记录
        /// <summary>
        /// 获得采购订单收货记录
        /// </summary>
        /// <param name="warehouse"></param>
        /// <param name="taskorderid"></param>
        /// <returns></returns>
        public DataSet GetOuterOrderCheckInLocal(string warehouse, string taskorderid)
        {
            //and LGOBE='{1}'
            //未清数量>0
            string commandText = string.Format("select * from ZMES_EKKO where EBELN='{0}'  and OBMNG>0 order by EINDT",
                taskorderid);
            return _dataAccess.ExecuteDataset(commandText);
        }

        /// <summary>
        /// 根据物料号和日期查询采购订单
        /// </summary>
        /// <param name="materialNo"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public DataSet GetOuterOrderCheckInLocal(string warehouse, string materialNo, DateTime dtBegin, DateTime dtEnd)
        {
            // and LGOBE='" + warehouse + "'";
            //未清数量>0
            string commandText = "select distinct EBELN,EINDT from ZMES_EKKO where obmng>0";
            if (!string.IsNullOrEmpty(materialNo))
            {
                commandText += " and MATNR='"+materialNo+"'";
            }
            commandText += " and EINDT>='" + dtBegin.ToString("yyyy-MM-dd") + "'";
            commandText += " and EINDT<='" + dtEnd.ToString("yyyy-MM-dd") + "' order by EINDT";
            return _dataAccess.ExecuteDataset(commandText);
        }
       
        #endregion

        #region 当日采购订单提交记录
        /// <summary>
        /// 当日采购订单提交记录。
        /// </summary>
        /// <returns></returns>
        public DataSet GetOuterOrderCheckInDelivery()
        {
            string commandText = string.Format("select * from ZMES_Delivery where Del_DATE='{0}' order by Del_Time,Delivery_ID",
                System.DateTime.Now.ToString("yyyy-MM-dd"));
            return _dataAccess.ExecuteDataset(commandText);
        }
        #endregion

        #region SAP未收货记录
        /// <summary>
        /// SAP未收货记录。
        /// </summary>
        /// <returns></returns>
        public DataSet GetOuterOrderCheckInFailure()
        {
            string commandText = "select * from ZMES_Delivery where (Del_State is null or Del_State='E') order by Del_Date,Del_Time,Delivery_ID";
            return _dataAccess.ExecuteDataset(commandText);
        }
        /// <summary>
        /// 查询SAP未收货记录条数。
        /// </summary>
        /// <returns></returns>
        public int GetOuterOrderCheckInFailureCount()
        {
            string commandText = "select count(*) from ZMES_Delivery where (Del_State is null or Del_State='E') order by Del_Date,Delivery_ID";
            return Convert.ToInt32(_dataAccess.ExecuteScalar(commandText));
        }
        #endregion

        #region 删除SAP采购订单未收货记录
        /// <summary>
        /// 删除SAP采购订单未收货记录
        /// </summary>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public bool DeleteOuterOrderCheckIn(string deliveryId, out string strMsg)
        {
            string commandText = "delete from ZMES_Delivery where Delivery_ID='" + deliveryId + "'";
            _dataAccess.ExecuteNonQuery(commandText);
            strMsg = string.Empty;
            return true;
        }
        #endregion      

        #endregion       

        #region 工厂间采购订单收货

        #region 数据插入本地SQLite库
        /// <summary>
        ///提交SAP之前先调用此方法，把数据插入本地SQLite库。
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="proid"></param>
        /// <param name="supplyid"></param>
        /// <param name="loctionid"></param>
        /// <param name="userid"></param>
        /// <param name="t"></param>
        public void SetFactoryOrderCheckInLocal(MES_ReceiveRecordTask task)
        {
            foreach (KeyValuePair<int, MES_ReceiveRecord> kvp in task.Items)
            {
                MES_ReceiveRecord item = kvp.Value as MES_ReceiveRecord;
                string commandText = string.Format(@"insert into MES_ReceiveRecord(Delivery_ID,username,EBELN,EBELP,WEMNG,MATNR,MAKTX,MENGE,
                                            MEINS,FWerks,FLGOBE,EINDT,BUKRS,BSART,EKGRP,SLGOBE,
                                            SWerks,Del_DATE,Del_Time)
                                            values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',
                                            '{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}',
                                            '{17}','{18}')",
                       item.Delivery_ID, item.UserName, item.EBELN, item.EBELP, item.WEMNG, item.MATNR, item.MAKTX, item.MENGE,
                       item.MEINS, item.FWerks, item.FLGOBE, item.EINDT, item.BUKRS, item.BSART, item.EKGRP, item.SLGOBE,
                       item.SWerks, item.Del_Date, item.Del_Time);
                _dataAccess.ExecuteNonQuery(commandText);
                ////根据订单号和行号，更新本地收货记录表
                //commandText = string.Format("update ZMES_EKKO set OBMNG=OBMNG-{0},WEMNG=WEMNG+{0} where EBELN='{1}' and EBELP='{2}'",
                //    item.WEMNG, item.EBELN, item.EBELP);
                //_dataAccess.ExecuteNonQuery(commandText);
            }
        }

        /// <summary>
        /// 获得存储在本地SQLite的任务记录。
        /// 1.未提交的记录
        /// 2.提交失败的记录
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="loctionid"></param>
        /// <returns></returns>
        public MES_ReceiveRecordTask GetFactoryOrderCheckInHistoryTask(string orderid, string loctionid)
        {
            string str = string.Empty;
            //考虑同一个订单，收货多次对应多个凭证的情况，ZMES_Delivery表每次收货提交会新产生一个Task_ID号，
            //同一个Task_ID发送单独发送一次
            string commandText = string.Format("select * from MES_ReceiveRecord where EBELN='{0}' and JWerks='{1}'",
                orderid, loctionid);
            DataTable dtDelivery = _dataAccess.ExecuteDataTable(commandText);

            MES_ReceiveRecordTask task = new MES_ReceiveRecordTask();

            for (int i = 0; i < dtDelivery.Rows.Count; i++)
            {
                MES_ReceiveRecord item = new MES_ReceiveRecord();
                DataRow row = dtDelivery.Rows[i];
                item.Delivery_ID = DbValue.GetString(row["Delivery_ID"]);
                item.Del_Date = DbValue.GetString(row["Del_Date"]);
                item.Del_Time = DbValue.GetString(row["Del_Time"]);
                item.WEMNG = DbValue.GetDecimal(row["BATCH"]);
                item.BSART = DbValue.GetString(row["BSART"]);
                item.BUKRS = DbValue.GetString(row["BUKRS"]);
                item.EBELN = DbValue.GetString(row["EBELN"]);
                item.EBELP = DbValue.GetString(row["EBELP"]);
                item.EINDT = DbValue.GetString(row["EINDT"]);
                item.EKGRP = DbValue.GetString(row["EKGRP"]);
                item.SWerks = DbValue.GetString(row["SWerks"]);
                item.FLGOBE = DbValue.GetString(row["FLGOBE"]);
                item.SLGOBE = DbValue.GetString(row["SLGOBE"]);
               
                item.MAKTX = DbValue.GetString(row["MAKTX"]);
                item.MATNR = DbValue.GetString(row["MATNR"]);
                item.MEINS = DbValue.GetString(row["MEINS"]);
                item.UserName = DbValue.GetString(row["UserName"]);
                item.FWerks = DbValue.GetString(row["FWerks"]);
                item.MENGE = DbValue.GetDecimal(row["MENGE"]);
                task.AddItem(item);
                task.Task_ID = DbValue.GetString(row["Delivery_ID"]);
            }
            return task;
        }

        /// <summary>
        /// SAP联网收货成功后，更新本地SQLite库数据。
        /// 对应本地存储的非联网收货记录，再发送SAP时，不需要调用此方法。
        /// 因为存储本地的时候，就已经更新本地记录了。
        /// 根据订单号和行号号更新。
        /// 如果本地库没有对应数据，不做操作。
        /// 否则更新字段：
        /// OBMNG：未清数量-当前收货数量
        /// WEMNG：收货数量+当前收货数量
        /// </summary>
        /// <param name="task"></param>
        public void UpdateFactoryOrderCheckInLocal(MES_ReceiveRecordTask task)
        {
            foreach (KeyValuePair<int, MES_ReceiveRecord> kvp in task.Items)
            {
                MES_ReceiveRecord item = kvp.Value;
                string commandText = "update MES_ReceiveRecord set OBMNG=OBMNG-" + item.WEMNG + " and WEMNG=WEMNG+" + item.WEMNG + " where EBELN='" + item.EBELN + "' and EBELP='" + item.EBELP + "'";
                _dataAccess.ExecuteNonQuery(commandText);
            }
        }

        #endregion

        #region 获得采购订单收货记录
        /// <summary>
        /// 获得采购订单收货记录
        /// </summary>
        /// <param name="warehouse"></param>
        /// <param name="taskorderid"></param>
        /// <returns></returns>
        public DataSet GetFactoryOrderCheckInLocal(string warehouse, string taskorderid)
        {
            //and LGOBE='{1}'
            //未清数量>0
            string commandText = string.Format("select * from MES_ReceiveRecord where EBELN='{0}'  and OBMNG>0 order by EINDT",
                taskorderid);
            return _dataAccess.ExecuteDataset(commandText);
        }

        /// <summary>
        /// 根据物料号和日期查询采购订单
        /// </summary>
        /// <param name="materialNo"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public DataSet GetFactoryOrderCheckInLocal(string warehouse, string materialNo, DateTime dtBegin, DateTime dtEnd)
        {
            // and LGOBE='" + warehouse + "'";
            //未清数量>0
            string commandText = "select distinct EBELN,EINDT from MES_ReceiveRecord where obmng>0";
            if (!string.IsNullOrEmpty(materialNo))
            {
                commandText += " and MATNR='" + materialNo + "'";
            }
            commandText += " and EINDT>='" + dtBegin.ToString("yyyy-MM-dd") + "'";
            commandText += " and EINDT<='" + dtEnd.ToString("yyyy-MM-dd") + "' order by EINDT";
            return _dataAccess.ExecuteDataset(commandText);
        }

        #endregion

        #region 当日采购订单提交记录
        /// <summary>
        /// 当日采购订单提交记录。
        /// </summary>
        /// <returns></returns>
        public DataSet GetFactoryOrderCheckInDelivery()
        {
            string commandText = string.Format("select * from MES_ReceiveRecord where Del_DATE='{0}' order by Del_Time,Delivery_ID",
                System.DateTime.Now.ToString("yyyy-MM-dd"));
            return _dataAccess.ExecuteDataset(commandText);
        }
        #endregion

        #region SAP未收货记录
        /// <summary>
        /// SAP未收货记录。
        /// </summary>
        /// <returns></returns>
        public DataSet GetFactoryOrderCheckInFailure()
        {
            string commandText = "select * from MES_ReceiveRecord where (Del_State is null or Del_State='E') order by Del_Date,Del_Time,Delivery_ID";
            return _dataAccess.ExecuteDataset(commandText);
        }
        /// <summary>
        /// 查询SAP未收货记录条数。
        /// </summary>
        /// <returns></returns>
        public int GetFactoryOrderCheckInFailureCount()
        {
            string commandText = "select count(*) from MES_ReceiveRecord where (Del_State is null or Del_State='E') order by Del_Date,Delivery_ID";
            return Convert.ToInt32(_dataAccess.ExecuteScalar(commandText));
        }
        #endregion

        #region 删除SAP采购订单未收货记录
        /// <summary>
        /// 删除SAP采购订单未收货记录
        /// </summary>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public bool DeleteFactoryOrderCheckIn(string deliveryId, out string strMsg)
        {
            string commandText = "delete from MES_ReceiveRecord where Delivery_ID='" + deliveryId + "'";
            _dataAccess.ExecuteNonQuery(commandText);
            strMsg = string.Empty;
            return true;
        }
        #endregion

        #endregion       

        #region 工厂间采购订单发货

        #region 数据插入本地SQLite库
        /// <summary>
        ///提交SAP之前先调用此方法，把数据插入本地SQLite库。
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="proid"></param>
        /// <param name="supplyid"></param>
        /// <param name="loctionid"></param>
        /// <param name="userid"></param>
        /// <param name="t"></param>
        public void SetFactoryOrderCheckOutLocal(MES_SendRecordTask task)
        {
            foreach (KeyValuePair<int, MES_SendRecord> kvp in task.Items)
            {
                MES_SendRecord item = kvp.Value as MES_SendRecord;
                string commandText = string.Format(@"insert into MES_SendRecord(Delivery_ID,username,EBELN,EBELP,WAMNG,MATNR,MAKTX,MENGE,
                                            MEINS,FWerks,FLGOBE,EINDT,BUKRS,BSART,EKGRP,SLGOBE,
                                            SWerks,Del_DATE,Del_Time)
                                            values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',
                                            '{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}',
                                            '{17}','{18}')",
                       item.Delivery_ID, item.UserName, item.EBELN, item.EBELP, item.WAMNG, item.MATNR, item.MAKTX, item.MENGE,
                       item.MEINS, item.FWerks, item.FLGOBE, item.EINDT, item.BUKRS, item.BSART, item.EKGRP, item.SLGOBE,
                        item.SWerks, item.Del_Date, item.Del_Time);
                _dataAccess.ExecuteNonQuery(commandText);
                ////根据订单号和行号，更新本地收货记录表
                //commandText = string.Format("update MES_SendRecord set OBMNG=OBMNG-{0},WAMNG=WAMNG+{0} where EBELN='{1}' and EBELP='{2}'",
                //    item.WEMNG, item.EBELN, item.EBELP);
                //_dataAccess.ExecuteNonQuery(commandText);
            }
        }

        /// <summary>
        /// 获得存储在本地SQLite的任务记录。
        /// 1.未提交的记录
        /// 2.提交失败的记录
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="loctionid"></param>
        /// <returns></returns>
        public MES_SendRecordTask GetFactoryOrderCheckOutHistoryTask(string orderid, string loctionid)
        {
            string str = string.Empty;
            //考虑同一个订单，收货多次对应多个凭证的情况，ZMES_Delivery表每次收货提交会新产生一个Task_ID号，
            //同一个Task_ID发送单独发送一次
            string commandText = string.Format("select * from MES_SendRecord where EBELN='{0}' and JWerks='{1}'",
                orderid, loctionid);
            DataTable dtDelivery = _dataAccess.ExecuteDataTable(commandText);

            MES_SendRecordTask task = new MES_SendRecordTask();

            for (int i = 0; i < dtDelivery.Rows.Count; i++)
            {
                MES_SendRecord item = new MES_SendRecord();
                DataRow row = dtDelivery.Rows[i];
                item.Delivery_ID = DbValue.GetString(row["Delivery_ID"]);
                item.Del_Date = DbValue.GetString(row["Del_Date"]);
                item.Del_Time = DbValue.GetString(row["Del_Time"]);
                item.WAMNG = DbValue.GetDecimal(row["WAMNG"]);
                item.BSART = DbValue.GetString(row["BSART"]);
                item.BUKRS = DbValue.GetString(row["BUKRS"]);
                item.EBELN = DbValue.GetString(row["EBELN"]);
                item.EBELP = DbValue.GetString(row["EBELP"]);
                item.EINDT = DbValue.GetString(row["EINDT"]);
                item.EKGRP = DbValue.GetString(row["EKGRP"]);
                item.SWerks = DbValue.GetString(row["SWerks"]);
                item.FLGOBE = DbValue.GetString(row["FLGOBE"]);
                item.SLGOBE = DbValue.GetString(row["SLGOBE"]);                
                item.MAKTX = DbValue.GetString(row["MAKTX"]);
                item.MATNR = DbValue.GetString(row["MATNR"]);
                item.MEINS = DbValue.GetString(row["MEINS"]);
                item.UserName = DbValue.GetString(row["UserName"]);
                item.FWerks = DbValue.GetString(row["FWerks"]);
                item.MENGE = DbValue.GetDecimal(row["MENGE"]);
                task.AddItem(item);
                task.Task_ID = DbValue.GetString(row["Delivery_ID"]);
            }
            return task;
        }

        /// <summary>
        /// SAP联网收货成功后，更新本地SQLite库数据。
        /// 对应本地存储的非联网收货记录，再发送SAP时，不需要调用此方法。
        /// 因为存储本地的时候，就已经更新本地记录了。
        /// 根据订单号和行号号更新。
        /// 如果本地库没有对应数据，不做操作。
        /// 否则更新字段：
        /// OBMNG：未清数量-当前收货数量
        /// WEMNG：收货数量+当前收货数量
        /// </summary>
        /// <param name="task"></param>
        public void UpdateFactoryOrderCheckOutLocal(MES_SendRecordTask task)
        {
            foreach (KeyValuePair<int, MES_SendRecord> kvp in task.Items)
            {
                MES_SendRecord item = kvp.Value;
                string commandText = "update MES_Send set OBMNG=OBMNG-" + item.WAMNG + " and WAMNG=WAMNG+" + item.WAMNG + " where EBELN='" + item.EBELN + "' and EBELP='" + item.EBELP + "'";
                _dataAccess.ExecuteNonQuery(commandText);
            }
        }

        #endregion

        #region 获得采购订单收货记录
        /// <summary>
        /// 获得采购订单收货记录
        /// </summary>
        /// <param name="warehouse"></param>
        /// <param name="taskorderid"></param>
        /// <returns></returns>
        public DataSet GetFactoryOrderCheckOutLocal(string warehouse, string taskorderid)
        {
            //and LGOBE='{1}'
            //未清数量>0
            string commandText = string.Format("select * from MES_Send where EBELN='{0}'  and OBMNG>0 order by EINDT",
                taskorderid);
            return _dataAccess.ExecuteDataset(commandText);
        }

        /// <summary>
        /// 根据物料号和日期查询采购订单
        /// </summary>
        /// <param name="materialNo"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public DataSet GetFactoryOrderCheckOutLocal(string warehouse, string materialNo, DateTime dtBegin, DateTime dtEnd)
        {
            // and LGOBE='" + warehouse + "'";
            //未清数量>0
            string commandText = "select distinct EBELN,EINDT from MES_Send where obmng>0";
            if (!string.IsNullOrEmpty(materialNo))
            {
                commandText += " and MATNR='" + materialNo + "'";
            }
            commandText += " and EINDT>='" + dtBegin.ToString("yyyy-MM-dd") + "'";
            commandText += " and EINDT<='" + dtEnd.ToString("yyyy-MM-dd") + "' order by EINDT";
            return _dataAccess.ExecuteDataset(commandText);
        }

        #endregion

        #region 当日采购订单提交记录
        /// <summary>
        /// 当日采购订单提交记录。
        /// </summary>
        /// <returns></returns>
        public DataSet GetFactoryOrderCheckOutDelivery()
        {
            string commandText = string.Format("select * from MES_SendRecord where Del_DATE='{0}' order by Del_Time,Delivery_ID",
                System.DateTime.Now.ToString("yyyy-MM-dd"));
            return _dataAccess.ExecuteDataset(commandText);
        }
        #endregion

        #region SAP未收货记录
        /// <summary>
        /// SAP未收货记录。
        /// </summary>
        /// <returns></returns>
        public DataSet GetFactoryOrderCheckOutFailure()
        {
            string commandText = "select * from MES_SendRecord where (Del_State is null or Del_State='E') order by Del_Date,Del_Time,Delivery_ID";
            return _dataAccess.ExecuteDataset(commandText);
        }
        /// <summary>
        /// 查询SAP未收货记录条数。
        /// </summary>
        /// <returns></returns>
        public int GetFactoryOrderCheckOutFailureCount()
        {
            string commandText = "select count(*) from MES_SendRecord where (Del_State is null or Del_State='E') order by Del_Date,Delivery_ID";
            return Convert.ToInt32(_dataAccess.ExecuteScalar(commandText));
        }
        #endregion

        #region 删除SAP采购订单未收货记录
        /// <summary>
        /// 删除SAP采购订单未收货记录
        /// </summary>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public bool DeleteFactoryOrderCheckOut(string deliveryId, out string strMsg)
        {
            string commandText = "delete from MES_SendRecord where Delivery_ID='" + deliveryId + "'";
            _dataAccess.ExecuteNonQuery(commandText);
            strMsg = string.Empty;
            return true;
        }
        #endregion

        #endregion       

    }
}
