using System;

using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Net;
using System.Reflection;
using HL.DAL;
using Entity;
using HL.BLL.sap300.checkInPurOrderSummit;
using BizLayer;

namespace HL.BLL
{
    /// <summary>
    /// 访问远程服务（SAP）接口的包装类。
    /// </summary>
    public class RemoteService
    {
        #region Credentials     
        /// <summary>
        /// 开发时SAP服务地址
        /// </summary>
        public string DevServerUrl = "http://r3dev01.heli.com:8000";
        #endregion

        #region 测试服务是否可连接
        public bool TryConnetion()
        {
            bool canConnect = true;

            return canConnect;
        }
        #endregion

        #region 外部采购订单收货

        #region 获得外部采购订单收货记录
        public DataSet GetOuterOrderCheckIn(string warehouse, string taskorderid)
        {
            DataSet ds = new DataSet();
            HL.BLL.sap.checkInPurOrder.ZMES_BATFETCHPURORDER obj300 = new HL.BLL.sap.checkInPurOrder.ZMES_BATFETCHPURORDER();
            obj300.Url = obj300.Url.Replace(DevServerUrl, Management.GetSingleton().DefaultBaseUrl);
            obj300.Credentials = new NetworkCredential(Management.GetSingleton().ServiceUserName, Management.GetSingleton().ServicePassword);

            HL.BLL.sap.checkInPurOrder.ZMES_BATFETCHPURORDER1 input300 = new HL.BLL.sap.checkInPurOrder.ZMES_BATFETCHPURORDER1();
            HL.BLL.sap.checkInPurOrder.ZMES_PURORDER objrow300 = new HL.BLL.sap.checkInPurOrder.ZMES_PURORDER();
            HL.BLL.sap.checkInPurOrder.ZMES_PURORDER[] purOrderRows = new HL.BLL.sap.checkInPurOrder.ZMES_PURORDER[1];
            input300.TB_EKPO = purOrderRows;
            input300.USERNAME = "tmc";
            input300.EBELN = taskorderid;
            input300.TB_LGORT = new HL.BLL.sap.checkInPurOrder.ZMES_LGORT[1];
            input300.TB_LGORT[0] = new HL.BLL.sap.checkInPurOrder.ZMES_LGORT();
            input300.TB_LGORT[0].LGORT = warehouse;
            HL.BLL.sap.checkInPurOrder.ZMES_BATFETCHPURORDERResponse result300 = obj300.CallZMES_BATFETCHPURORDER(input300);
            purOrderRows = result300.TB_EKPO;

            ds.Tables.Add("ZMES_EKKO");
            for (int i = 0; i < purOrderRows.Length; i++)
            {
                HL.BLL.sap.checkInPurOrder.ZMES_PURORDER purOrderRow = (HL.BLL.sap.checkInPurOrder.ZMES_PURORDER)purOrderRows.GetValue(i);
                Type t = purOrderRow.GetType();
                if (ds.Tables[0].Columns.Count == 0)
                {
                    foreach (PropertyInfo pi in t.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                    {
                        ds.Tables[0].Columns.Add(pi.Name);
                    }
                }
                DataRow row = ds.Tables[0].NewRow();
                foreach (PropertyInfo pi in t.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {

                    object o = t.GetProperty(pi.Name).GetValue(purOrderRow, null);
                    if (o == null || o == DBNull.Value)
                    {
                        row[pi.Name] = null;
                    }
                    else if (o.GetType().ToString() == "System.DateTime")
                    {
                        row[pi.Name] = Convert.ToDateTime(o).ToString("yyyy-MM-dd hh:mm:ss");
                    }
                    else
                    {
                        row[pi.Name] = DbValue.FormatSqlText(o.ToString());
                    }
                }
                ds.Tables[0].Rows.Add(row);
            }
            return ds;
        }

         /// <summary>
        /// 根据物料号和日期查询采购订单
        /// </summary>
        /// <param name="materialNo"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public DataSet GetOuterOrderCheckIn(string warehouse, string materialNo, DateTime dtBegin, DateTime dtEnd)
        {
           throw new Exception("SAP服务不可用。");
        }
        #endregion

        #region 采购订单收货提交
        public ZMES_BIAOZHUNCAIGOUSHOUHUOResponse SetOuterOrderCheckIn(ZMES_Task task)
        {
            HL.BLL.sap300.checkInPurOrderSummit.ZMES_BIAOZHUNCAIGOUSHOUHUO obj300 = new HL.BLL.sap300.checkInPurOrderSummit.ZMES_BIAOZHUNCAIGOUSHOUHUO();
            obj300.Url = obj300.Url.Replace(DevServerUrl, Management.GetSingleton().DefaultBaseUrl);
            obj300.Credentials = new NetworkCredential(Management.GetSingleton().ServiceUserName, Management.GetSingleton().ServicePassword);

            HL.BLL.sap300.checkInPurOrderSummit.ZMES_BIAOZHUNCAIGOUSHOUHUO1 input300 = new HL.BLL.sap300.checkInPurOrderSummit.ZMES_BIAOZHUNCAIGOUSHOUHUO1();
            HL.BLL.sap300.checkInPurOrderSummit.ZMES_DELIVERY objrow300 = new HL.BLL.sap300.checkInPurOrderSummit.ZMES_DELIVERY();
            HL.BLL.sap300.checkInPurOrderSummit.ZMES_DELIVERY[] objtab300 = new HL.BLL.sap300.checkInPurOrderSummit.ZMES_DELIVERY[task.Items.Count];
            int index = 0;
            foreach (KeyValuePair<int, ZMES_Delivery> kvp in task.Items)
            {
                ZMES_DELIVERY item = new ZMES_DELIVERY();
                item.BSART = kvp.Value.BSART;
                item.BUKRS = kvp.Value.BUKRS;
                item.DEL_DATE = kvp.Value.Del_Date;
                item.DEL_TIME = kvp.Value.Del_Time;
                item.DELIVERY_ID = kvp.Value.Delivery_ID;
                item.EBELN = kvp.Value.EBELN;
                item.EBELP = kvp.Value.EBELP;
                item.EINDT = kvp.Value.EINDT;
                item.EKGRP = kvp.Value.EKGRP;
                item.FWERKS = kvp.Value.FWerks;
                item.LGOBE = kvp.Value.LGOBE;
                item.LIFNR = kvp.Value.LIFNR;
                item.LIFNRNAME = kvp.Value.BSART;
                item.MAKTX = kvp.Value.MAKTX;
                item.MATNR = kvp.Value.MATNR;
                item.MEINS = kvp.Value.MEINS;
                item.MENGE = kvp.Value.MENGE;
                item.SWERKS = kvp.Value.SWerks;
                item.USERNAME = kvp.Value.UserName;
                item.WEMNG = kvp.Value.WEMNG;
                objtab300[index++] = item;
            }
            input300.TB_DELIVERY = objtab300;
            HL.BLL.sap300.checkInPurOrderSummit.ZMES_BIAOZHUNCAIGOUSHOUHUOResponse result300 = obj300.CallZMES_BIAOZHUNCAIGOUSHOUHUO(input300);
            return result300;
        }
        #endregion

        #region 删除SAP采购订单未收货记录
        /// <summary>
        /// 删除SAP采购订单未收货记录。
        /// 收货提交SAP时，如果收货不成功。用户在PDA端删除收货记录时，
        /// 需要提交SAP同步删除此收货记录。
        /// SAP可能会存在记录不存在的情况，比如提交的瞬间因网络原因，
        /// 本地已保存收货记录，SAP未收到数据的情况。
        /// </summary>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public bool DeleteOuterOrderCheckIn(string deliveryId, out string strMsg)
        {
            strMsg = "SAP服务不可用。";
            return false;
        }
        #endregion      

        #endregion

        #region 工厂间采购订单收货

        #region 获得外部采购订单收货记录
        public DataSet GetFactoryOrderCheckIn(string warehouse, string taskorderid)
        {
            DataSet ds = new DataSet();


            HL.BLL.SAP300_GetFactoryOrderCheckIn.ZMES_PURCHASE_SINGLE_DATA obj300 = new HL.BLL.SAP300_GetFactoryOrderCheckIn.ZMES_PURCHASE_SINGLE_DATA();
            obj300.Url = obj300.Url.Replace(DevServerUrl, Management.GetSingleton().DefaultBaseUrl);
            obj300.Credentials = new NetworkCredential(Management.GetSingleton().ServiceUserName, Management.GetSingleton().ServicePassword);

            HL.BLL.SAP300_GetFactoryOrderCheckIn.ZMES_PURCHASE_SINGLE_DATA1 input300 = new HL.BLL.SAP300_GetFactoryOrderCheckIn.ZMES_PURCHASE_SINGLE_DATA1();


            HL.BLL.SAP300_GetFactoryOrderCheckIn.ZMES_PURCHASE_UB objrow300 = new HL.BLL.SAP300_GetFactoryOrderCheckIn.ZMES_PURCHASE_UB();
            HL.BLL.SAP300_GetFactoryOrderCheckIn.ZMES_PURCHASE_UB[] purOrderRows = new HL.BLL.SAP300_GetFactoryOrderCheckIn.ZMES_PURCHASE_UB[1];
            input300.TB_EKPO = purOrderRows;
            input300.USERNAME = "a";
            input300.EBELN = taskorderid;
            
            HL.BLL.SAP300_GetFactoryOrderCheckIn.ZMES_PURCHASE_SINGLE_DATAResponse result300 = obj300.CallZMES_PURCHASE_SINGLE_DATA(input300);
            purOrderRows = result300.TB_EKPO;


            ds.Tables.Add("MES_Receive");
            for (int i = 0; i < purOrderRows.Length; i++)
            {
                HL.BLL.SAP300_GetFactoryOrderCheckIn.ZMES_PURCHASE_UB purOrderRow = (HL.BLL.SAP300_GetFactoryOrderCheckIn.ZMES_PURCHASE_UB)purOrderRows.GetValue(i);
                Type t = purOrderRow.GetType();
                if (ds.Tables[0].Columns.Count == 0)
                {
                    foreach (PropertyInfo pi in t.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                    {
                        ds.Tables[0].Columns.Add(pi.Name);
                    }
                }
                DataRow row = ds.Tables[0].NewRow();
                foreach (PropertyInfo pi in t.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {

                    object o = t.GetProperty(pi.Name).GetValue(purOrderRow, null);
                    if (o == null || o == DBNull.Value)
                    {
                        row[pi.Name] = null;
                    }
                    else if (o.GetType().ToString() == "System.DateTime")
                    {
                        row[pi.Name] = Convert.ToDateTime(o).ToString("yyyy-MM-dd hh:mm:ss");
                    }
                    else
                    {
                        row[pi.Name] = DbValue.FormatSqlText(o.ToString());
                    }
                }
                ds.Tables[0].Rows.Add(row);
            }
            return ds;
        }

        /// <summary>
        /// 根据物料号和日期查询采购订单
        /// </summary>
        /// <param name="materialNo"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public DataSet GetFactoryOrderCheckIn(string warehouse, string materialNo, DateTime dtBegin, DateTime dtEnd)
        {
            throw new Exception("SAP服务不可用。");
        }
        #endregion

        #region 采购订单收货提交
        public SAP300_SetFactoryOrderCheckIn.ZMES_FACTORYCAIGOUSHOUHUOResponse SetFactoryOrderCheckIn(MES_ReceiveRecordTask task)
        {

            SAP300_SetFactoryOrderCheckIn.ZMES_FACTORYCAIGOUSHOUHUO obj300 = new SAP300_SetFactoryOrderCheckIn.ZMES_FACTORYCAIGOUSHOUHUO();
            obj300.Url = obj300.Url.Replace(DevServerUrl, Management.GetSingleton().DefaultBaseUrl);
            obj300.Credentials = new NetworkCredential(Management.GetSingleton().ServiceUserName, Management.GetSingleton().ServicePassword);

            SAP300_SetFactoryOrderCheckIn.ZMES_FACTORYCAIGOUSHOUHUO1 input300 = new SAP300_SetFactoryOrderCheckIn.ZMES_FACTORYCAIGOUSHOUHUO1();
            SAP300_SetFactoryOrderCheckIn.ZMES_RECEIPT objrow300 = new SAP300_SetFactoryOrderCheckIn.ZMES_RECEIPT();
            SAP300_SetFactoryOrderCheckIn.ZMES_RECEIPT[] objtab300 = new SAP300_SetFactoryOrderCheckIn.ZMES_RECEIPT[task.Items.Count];
            int index = 0;
            foreach (KeyValuePair<int, MES_ReceiveRecord> kvp in task.Items)
            {
                SAP300_SetFactoryOrderCheckIn.ZMES_RECEIPT item = new SAP300_SetFactoryOrderCheckIn.ZMES_RECEIPT();
                item.BSART = kvp.Value.BSART;
                item.BUKRS = kvp.Value.BUKRS;
                item.DEL_DATE = kvp.Value.Del_Date;
                item.DEL_TIME = kvp.Value.Del_Time;
                item.DELIVERY_ID = kvp.Value.Delivery_ID;
                item.EBELN = kvp.Value.EBELN;
                item.EBELP = kvp.Value.EBELP;
                item.EINDT = kvp.Value.EINDT;
                item.EKGRP = kvp.Value.EKGRP;
                item.FWERKS = kvp.Value.FWerks;
                item.FLGOBE = kvp.Value.FLGOBE;
                item.SLGOBE = kvp.Value.SLGOBE;
                item.MAKTX = kvp.Value.MAKTX;
                item.MATNR = kvp.Value.MATNR;
                item.MEINS = kvp.Value.MEINS;
                item.MENGE = kvp.Value.MENGE;
                item.SWERKS = kvp.Value.SWerks;
                item.USERNAME = kvp.Value.UserName;
                item.WEMNG = kvp.Value.WEMNG;
                objtab300[index++] = item;
            }
            input300.TB_RECEIPT = objtab300;
            SAP300_SetFactoryOrderCheckIn.ZMES_FACTORYCAIGOUSHOUHUOResponse result300 = obj300.CallZMES_FACTORYCAIGOUSHOUHUO(input300);
            return result300;
        }
        #endregion

        #region 删除SAP采购订单未收货记录
        /// <summary>
        /// 删除SAP采购订单未收货记录。
        /// 收货提交SAP时，如果收货不成功。用户在PDA端删除收货记录时，
        /// 需要提交SAP同步删除此收货记录。
        /// SAP可能会存在记录不存在的情况，比如提交的瞬间因网络原因，
        /// 本地已保存收货记录，SAP未收到数据的情况。
        /// </summary>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public bool DeleteFactoryOrderCheckIn(string deliveryId, out string strMsg)
        {
            strMsg = "SAP服务不可用。";
            return false;
        }
        #endregion

        #endregion

        #region 工厂间采购订单发货

        #region 获得外部采购订单收货记录
        public DataSet GetFactoryOrderCheckOut(string warehouse, string taskorderid)
        {
            DataSet ds = new DataSet();
            SAP300_GetFactoryOrderCheckOut.ZMES_PURCHASE_FETCH_DATA obj300 = new SAP300_GetFactoryOrderCheckOut.ZMES_PURCHASE_FETCH_DATA();
            obj300.Url = obj300.Url.Replace(DevServerUrl, Management.GetSingleton().DefaultBaseUrl);
            obj300.Credentials = new NetworkCredential(Management.GetSingleton().ServiceUserName, Management.GetSingleton().ServicePassword);
            SAP300_GetFactoryOrderCheckOut.ZMES_PURCHASE_FETCH_DATA1 input300 = new SAP300_GetFactoryOrderCheckOut.ZMES_PURCHASE_FETCH_DATA1();
            SAP300_GetFactoryOrderCheckOut.ZMES_PURCHASE_UB objrow300 = new SAP300_GetFactoryOrderCheckOut.ZMES_PURCHASE_UB();
            SAP300_GetFactoryOrderCheckOut.ZMES_PURCHASE_UB[] purOrderRows = new SAP300_GetFactoryOrderCheckOut.ZMES_PURCHASE_UB[1];
            input300.TB_EKPO = purOrderRows;
            input300.USERNAME = "tmc";
            input300.EBELN = taskorderid;

            SAP300_GetFactoryOrderCheckOut.ZMES_PURCHASE_FETCH_DATAResponse result300 = obj300.CallZMES_PURCHASE_FETCH_DATA(input300);
            purOrderRows = result300.TB_EKPO;

            ds.Tables.Add("MES_Send");
            for (int i = 0; i < purOrderRows.Length; i++)
            {
                SAP300_GetFactoryOrderCheckOut.ZMES_PURCHASE_UB purOrderRow = (SAP300_GetFactoryOrderCheckOut.ZMES_PURCHASE_UB)purOrderRows.GetValue(i);
                Type t = purOrderRow.GetType();
                if (ds.Tables[0].Columns.Count == 0)
                {
                    foreach (PropertyInfo pi in t.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                    {
                        ds.Tables[0].Columns.Add(pi.Name);
                    }
                }
                DataRow row = ds.Tables[0].NewRow();
                foreach (PropertyInfo pi in t.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {

                    object o = t.GetProperty(pi.Name).GetValue(purOrderRow, null);
                    if (o == null || o == DBNull.Value)
                    {
                        row[pi.Name] = null;
                    }
                    else if (o.GetType().ToString() == "System.DateTime")
                    {
                        row[pi.Name] = Convert.ToDateTime(o).ToString("yyyy-MM-dd hh:mm:ss");
                    }
                    else
                    {
                        row[pi.Name] = DbValue.FormatSqlText(o.ToString());
                    }
                }
                ds.Tables[0].Rows.Add(row);
            }
            return ds;
        }

        /// <summary>
        /// 根据物料号和日期查询采购订单
        /// </summary>
        /// <param name="materialNo"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public DataSet GetFactoryOrderCheckOut(string warehouse, string materialNo, DateTime dtBegin, DateTime dtEnd)
        {
            throw new Exception("SAP服务不可用。");
        }
        #endregion

        #region 采购订单收货提交
        public SAP300_SetFactoryOrderCheckOut.ZMES_FACTORYCAIGOUFAHUOResponse SetFactoryOrderCheckOut(MES_SendRecordTask task)
        {

            SAP300_SetFactoryOrderCheckOut.zmes_factorycaigoufahuo obj300 = new SAP300_SetFactoryOrderCheckOut.zmes_factorycaigoufahuo();
            obj300.Url = obj300.Url.Replace(DevServerUrl, Management.GetSingleton().DefaultBaseUrl);
            obj300.Credentials = new NetworkCredential(Management.GetSingleton().ServiceUserName, Management.GetSingleton().ServicePassword);

            SAP300_SetFactoryOrderCheckOut.ZMES_FACTORYCAIGOUFAHUO input300 = new SAP300_SetFactoryOrderCheckOut.ZMES_FACTORYCAIGOUFAHUO();

            SAP300_SetFactoryOrderCheckOut.ZMES_SENDRECORD objrow300 = new SAP300_SetFactoryOrderCheckOut.ZMES_SENDRECORD();
            SAP300_SetFactoryOrderCheckOut.ZMES_SENDRECORD[] objtab300 = new SAP300_SetFactoryOrderCheckOut.ZMES_SENDRECORD[task.Items.Count];
            int index = 0;
            foreach (KeyValuePair<int, MES_SendRecord> kvp in task.Items)
            {
                SAP300_SetFactoryOrderCheckOut.ZMES_SENDRECORD item = new SAP300_SetFactoryOrderCheckOut.ZMES_SENDRECORD();
                item.BSART = kvp.Value.BSART;
                item.BUKRS = kvp.Value.BUKRS;
                item.DEL_DATE = kvp.Value.Del_Date;
                item.DEL_TIME = kvp.Value.Del_Time;
                item.DELIVERY_ID = kvp.Value.Delivery_ID;
                item.EBELN = kvp.Value.EBELN;
                item.EBELP = kvp.Value.EBELP;
                item.EINDT = kvp.Value.EINDT;
                item.EKGRP = kvp.Value.EKGRP;
                item.FWERKS = kvp.Value.FWerks;
                item.FLGOBE = kvp.Value.FLGOBE;
                item.SLGOBE = kvp.Value.SLGOBE;              
                item.MAKTX = kvp.Value.MAKTX;
                item.MATNR = kvp.Value.MATNR;
                item.MEINS = kvp.Value.MEINS;
                item.MENGE = kvp.Value.MENGE;
                item.SWERKS = kvp.Value.SWerks;
                item.USERNAME = kvp.Value.UserName;
                item.WAMNG = kvp.Value.WAMNG;
                objtab300[index++] = item;
            }
            input300.TB_SENDRECORD = objtab300;
            SAP300_SetFactoryOrderCheckOut.ZMES_FACTORYCAIGOUFAHUOResponse result300 = obj300.ZMES_FACTORYCAIGOUFAHUO(input300);
            return result300;
        }
        #endregion

        #region 删除SAP采购订单未收货记录
        /// <summary>
        /// 删除SAP采购订单未收货记录。
        /// 收货提交SAP时，如果收货不成功。用户在PDA端删除收货记录时，
        /// 需要提交SAP同步删除此收货记录。
        /// SAP可能会存在记录不存在的情况，比如提交的瞬间因网络原因，
        /// 本地已保存收货记录，SAP未收到数据的情况。
        /// </summary>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public bool DeleteFactoryOrderCheckOut(string deliveryId, out string strMsg)
        {
            strMsg = "SAP服务不可用。";
            return false;
        }
        #endregion

        #endregion

        #region 查询物料库存数量
        /// <summary>
        /// 查询物料库存数量
        /// </summary>      
        /// <returns></returns>
        public DataSet QueryMaterialKuCun(DataSet dsOrder)
        {
          
            //SAP300_QueryMaterialKuCun.ZMATERIAL_AVAILABILITY obj300 = new SAP300_QueryMaterialKuCun.ZMATERIAL_AVAILABILITY();
            //obj300.Url = obj300.Url.Replace(DevServerUrl, Management.GetSingleton().DefaultBaseUrl);
            //obj300.Credentials = new NetworkCredential(Management.GetSingleton().ServiceUserName, Management.GetSingleton().ServicePassword);

            //SAP300_QueryMaterialKuCun.ZMATERIAL_AVAILABILITY1 input300 = new SAP300_QueryMaterialKuCun.ZMATERIAL_AVAILABILITY1();
            //SAP300_QueryMaterialKuCun.ZAVAILABILITY1 objrow300 = new SAP300_QueryMaterialKuCun.ZAVAILABILITY1();
            //SAP300_QueryMaterialKuCun.ZAVAILABILITY1[] objtab300 = new SAP300_QueryMaterialKuCun.ZAVAILABILITY1[dsOrder.Tables[0].Rows.Count];
            //for (int i = 0; i < dsOrder.Tables[0].Rows.Count; i++)
            //{

            //}
            //input300.ZKCSLB = objtab300;           
            //SAP300_QueryMaterialKuCun.ZMATERIAL_AVAILABILITYResponse result300 = obj300.CallZMATERIAL_AVAILABILITY(input300);
            //objtab300 = result300.ZKCSLB;


            return dsOrder;            
        }       
        #endregion

    }
}
