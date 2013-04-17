using System;

using System.Collections.Generic;
using System.Text;
using System.Data;
using HL.Device;
using Entity;
using System.Net;
using HL.BLL.sap300.checkInPurOrderSummit;
using HL.BLL.SAP300_SetFactoryOrderCheckOut;

namespace HL.Framework
{
    /// <summary>
    /// 服务是否联机，用以判断是联机访问，
    /// 还是本地访问。
    /// </summary>
    public enum ServiceOnline
    {
        Online,
        Offline
    }

    /// <summary>
    /// 服务调用接口。
    /// 调用服务前判断网络情况，如果SAP服务可以访问，调用RemoteService，
    /// 否则调用SQLite本地的LocalService。
    /// </summary>
    public class ServiceCaller
    {
        #region 检查网络服务是否可用
        /// <summary>
        /// 检查网络服务是否可用
        /// </summary>
        /// <returns></returns>
        public static bool CheckNetworkService()
        {
            return DeviceCaller.CheckNetworkStat() && Global.RemoteService.TryConnetion();
        }
        #endregion

        #region 外部采购订单收货

        #region 获得外部采购订单收货记录
        /// <summary>
        /// 获得外部采购订单收货记录，同时返回联机状态。
        /// </summary>
        /// <param name="warehouse"></param>
        /// <param name="taskorderid"></param>
        /// <param name="so"></param>
        /// <returns></returns>
        public static DataSet GetOuterOrderCheckIn(string warehouse, string taskorderid,out ServiceOnline so)
        {
            if (CheckNetworkService())
            {
                so = ServiceOnline.Online;
                return Global.RemoteService.GetOuterOrderCheckIn(warehouse, taskorderid);
            }
            else
            {
                so = ServiceOnline.Offline;
                return Global.LocalService.GetOuterOrderCheckInLocal(warehouse, taskorderid);
            }
        }
      
        /// <summary>
        /// 根据物料号和日期查询采购订单，同时返回联机状态。
        /// </summary>
        /// <param name="warehouse"></param>
        /// <param name="taskorderid"></param>
        /// <param name="so"></param>
        /// <returns></returns>
        public static DataSet GetOuterOrderCheckIn(string warehouse, string materialNo, DateTime dtBegin, DateTime dtEnd, out ServiceOnline so)
        {
            if (CheckNetworkService())
            {
                so = ServiceOnline.Online;
                return Global.RemoteService.GetOuterOrderCheckIn(warehouse, materialNo, dtBegin, dtEnd);
            }
            else
            {
                so = ServiceOnline.Offline;
                return Global.LocalService.GetOuterOrderCheckInLocal(warehouse, materialNo, dtBegin, dtEnd);
            }
        }
        #endregion

        #region 外部采购订单收货提交
        /// <summary>
        /// 外部采购订单收货提交。
        /// 1.先保存本地SQLite
        /// 2.判断网络情况，查询SAP服务是否可用，若网络可用，设置状态：已同步
        /// 3.提交SAP，成功或失败，设置状态。
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public static string SetOuterOrderCheckIn(ZMES_Task task, out string strMsg, out ServiceOnline so)
        {
            Global.LocalService.SetOuterOrderCheckInLocal(task);
            if (CheckNetworkService())
            {
                so = ServiceOnline.Online;
                string retval;
                bool success = SetOuterOrderCheckInSAP(task, out retval);
                if (success)
                {
                    //联网状态下，收货成功，需要修改本地数据。
                    Global.LocalService.UpdateOuterOrderCheckInLocal(task);
                    strMsg = "收货成功！" + retval;
                    return retval;
                }
                else
                {
                    strMsg = "收货失败！" + retval;
                    return string.Empty;
                }                
            }
            else
            {
                Global.LocalService.UpdateOuterOrderCheckInLocal(task);
                so = ServiceOnline.Offline;
                strMsg = "未联网，已保存本地。";
                return null;
            }
        }
        /// <summary>
        /// 采购订单收货提交SAP。
        /// </summary>
        /// <param name="task"></param>
        /// <param name="retval">收货成功retval=凭证号,收货失败retval=失败原因</param>
        /// <returns>返回是否收货成功</returns>
        private static bool SetOuterOrderCheckInSAP(ZMES_Task task, out string retval)
        {
            //是否收货成功。
            //收货成功retval=凭证号
            //收货失败retval=失败原因
            bool success = false;
            //设置同步状态=Y
            SetOuterOrderCheckInStatus(task, "Syn_state", "Y");
            retval = "S";
            try
            {
                ZMES_BIAOZHUNCAIGOUSHOUHUOResponse result300 = Global.RemoteService.SetOuterOrderCheckIn(task);
                if (result300.RETVAL == "S")
                {
                    //收货成功，设置状态
                    SetOuterOrderCheckInStatus(task, "Del_state", "S", "Del_Remark", result300.RETMSG);
                    success = true;
                    retval = result300.RETMSG;
                }
                else
                {
                    //收货失败，设置状态
                    SetOuterOrderCheckInStatus(task, "Del_state", "E", "Del_Remark", result300.RETMSG);
                    retval = result300.RETMSG;
                }
            }
            catch (Exception ex)
            {
                //收货失败，设置status=3
                SetOuterOrderCheckInStatus(task, "Del_state", "E", "Del_Remark", ex.Message);
                retval = ex.Message;
            }
            return success;

        }
        private static void SetOuterOrderCheckInStatus(ZMES_Task task, string fieldName, string fieldValue)
        {
            string commandText = string.Format("update ZMES_Delivery set {0}='{1}' where Delivery_ID='{2}'", fieldName, fieldValue, task.Task_ID);
            Global.LocalService.DataAccess.ExecuteNonQuery(commandText);
        }

        private static void SetOuterOrderCheckInStatus(ZMES_Task task, string fieldName, string fieldValue, string fieldName2, string fieldValue2)
        {
            string commandText = string.Format("update ZMES_Delivery set {0}='{1}',{2}='{3}' where Delivery_ID='{4}'", fieldName, fieldValue, fieldName2, fieldValue2, task.Task_ID);
            Global.LocalService.DataAccess.ExecuteNonQuery(commandText);
        }

      
        #endregion

        #region 外部采购订单收货提交（历史）
        /// <summary>
        /// 采购订单收货提交（历史）
        /// 1.未提交的记录
        /// 2.提交失败的记录
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="loctionid"></param>
        /// <returns></returns>
        public static string SetOuterOrderCheckInHistory(ZMES_Task task, out string strMsg)
        {
            if (CheckNetworkService())
            {
                string retval;
                bool success = SetOuterOrderCheckInSAP(task, out retval);
                if (success)
                {
                    strMsg = "收货成功！" + retval;
                    return retval;
                }
                else
                {
                    strMsg = "收货失败！" + retval;
                    return string.Empty;
                }
            }
            else
            {
                strMsg = "未联网，无法提交数据。";
                return null;
            }
        }
        #endregion

        #endregion

        #region 工厂间采购订单收货

        #region 获得工厂间采购订单收货记录
        /// <summary>
        /// 获得工厂间采购订单收货记录，同时返回联机状态。
        /// </summary>
        /// <param name="warehouse"></param>
        /// <param name="taskorderid"></param>
        /// <param name="so"></param>
        /// <returns></returns>
        public static DataSet GetFactoryOrderCheckIn(string warehouse, string taskorderid, out ServiceOnline so)
        {
            if (CheckNetworkService())
            {
                so = ServiceOnline.Online;
                return Global.RemoteService.GetFactoryOrderCheckIn(warehouse, taskorderid);
            }
            else
            {
                so = ServiceOnline.Offline;
                return Global.LocalService.GetFactoryOrderCheckInLocal(warehouse, taskorderid);
            }
        }

        /// <summary>
        /// 根据物料号和日期查询采购订单，同时返回联机状态。
        /// </summary>
        /// <param name="warehouse"></param>
        /// <param name="taskorderid"></param>
        /// <param name="so"></param>
        /// <returns></returns>
        public static DataSet GetFactoryOrderCheckIn(string warehouse, string materialNo, DateTime dtBegin, DateTime dtEnd, out ServiceOnline so)
        {
            if (CheckNetworkService())
            {
                so = ServiceOnline.Online;
                return Global.RemoteService.GetFactoryOrderCheckIn(warehouse, materialNo, dtBegin, dtEnd);
            }
            else
            {
                so = ServiceOnline.Offline;
                return Global.LocalService.GetFactoryOrderCheckInLocal(warehouse, materialNo, dtBegin, dtEnd);
            }
        }
        #endregion

        #region 外部采购订单收货提交
        /// <summary>
        /// 外部采购订单收货提交。
        /// 1.先保存本地SQLite
        /// 2.判断网络情况，查询SAP服务是否可用，若网络可用，设置状态：已同步
        /// 3.提交SAP，成功或失败，设置状态。
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public static string SetFactoryOrderCheckIn(MES_ReceiveRecordTask task, out string strMsg, out ServiceOnline so)
        {
            Global.LocalService.SetFactoryOrderCheckInLocal(task);
            if (CheckNetworkService())
            {
                so = ServiceOnline.Online;
                string retval;
                bool success = SetFactoryOrderCheckInSAP(task, out retval);
                if (success)
                {
                    //联网状态下，收货成功，需要修改本地数据。
                    Global.LocalService.UpdateFactoryOrderCheckInLocal(task);
                    strMsg = "收货成功！" + retval;
                    return retval;
                }
                else
                {
                    strMsg = "收货失败！" + retval;
                    return string.Empty;
                }
            }
            else
            {
                //未联网状态下，保存成功，需要修改本地数据。
                Global.LocalService.UpdateFactoryOrderCheckInLocal(task);
                so = ServiceOnline.Offline;
                strMsg = "未联网，已保存本地。";
                return null;
            }
        }
        /// <summary>
        /// 采购订单收货提交SAP。
        /// </summary>
        /// <param name="task"></param>
        /// <param name="retval">收货成功retval=凭证号,收货失败retval=失败原因</param>
        /// <returns>返回是否收货成功</returns>
        private static bool SetFactoryOrderCheckInSAP(MES_ReceiveRecordTask task, out string retval)
        {
            //是否收货成功。
            //收货成功retval=凭证号
            //收货失败retval=失败原因
            bool success = false;
            //设置同步状态=Y
            SetFactoryOrderCheckInStatus(task, "Syn_state", "Y");
            retval = "S";
            try
            {
                HL.BLL.SAP300_SetFactoryOrderCheckIn.ZMES_FACTORYCAIGOUSHOUHUOResponse result300 = Global.RemoteService.SetFactoryOrderCheckIn(task);
                if (result300.RETVAL == "S")
                {
                    //收货成功，设置状态
                    SetFactoryOrderCheckInStatus(task, "Del_state", "S", "Del_Remark", result300.RETMSG);
                    success = true;
                    retval = result300.RETMSG;
                }
                else
                {
                    //收货失败，设置状态
                    SetFactoryOrderCheckInStatus(task, "Del_state", "E", "Del_Remark", result300.RETMSG);
                    retval = result300.RETMSG;
                }
            }
            catch (Exception ex)
            {
                //收货失败，设置status=3
                SetFactoryOrderCheckInStatus(task, "Del_state", "E", "Del_Remark", ex.Message);
                retval = ex.Message;
            }
            return success;

        }
        private static void SetFactoryOrderCheckInStatus(MES_ReceiveRecordTask task, string fieldName, string fieldValue)
        {
            string commandText = string.Format("update MES_ReceiveRecord set {0}='{1}' where Delivery_ID='{2}'", fieldName, fieldValue, task.Task_ID);
            Global.LocalService.DataAccess.ExecuteNonQuery(commandText);
        }

        private static void SetFactoryOrderCheckInStatus(MES_ReceiveRecordTask task, string fieldName, string fieldValue, string fieldName2, string fieldValue2)
        {
            string commandText = string.Format("update MES_ReceiveRecord set {0}='{1}',{2}='{3}' where Delivery_ID='{4}'", fieldName, fieldValue, fieldName2, fieldValue2, task.Task_ID);
            Global.LocalService.DataAccess.ExecuteNonQuery(commandText);
        }


        #endregion

        #region 外部采购订单收货提交（历史）
        /// <summary>
        /// 采购订单收货提交（历史）
        /// 1.未提交的记录
        /// 2.提交失败的记录
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="loctionid"></param>
        /// <returns></returns>
        public static string SetFactoryOrderCheckInHistory(MES_ReceiveRecordTask task, out string strMsg)
        {
            if (CheckNetworkService())
            {
                string retval;
                bool success = SetFactoryOrderCheckInSAP(task, out retval);
                if (success)
                {
                    strMsg = "收货成功！" + retval;
                    return retval;
                }
                else
                {
                    strMsg = "收货失败！" + retval;
                    return string.Empty;
                }
            }
            else
            {
                strMsg = "未联网，无法提交数据。";
                return null;
            }
        }
        #endregion

        #endregion

        #region 工厂间采购订单发货

        #region 获得工厂间采购订单发货记录
        /// <summary>
        /// 获得工厂间采购订单发货记录，同时返回联机状态。
        /// </summary>
        /// <param name="warehouse"></param>
        /// <param name="taskorderid"></param>
        /// <param name="so"></param>
        /// <returns></returns>
        public static DataSet GetFactoryOrderCheckOut(string warehouse, string taskorderid, out ServiceOnline so)
        {
            DataSet ds;
            if (CheckNetworkService())
            {
                so = ServiceOnline.Online;
                ds =  Global.RemoteService.GetFactoryOrderCheckOut(warehouse, taskorderid);
            }
            else
            {
                so = ServiceOnline.Offline;
               ds= Global.LocalService.GetFactoryOrderCheckOutLocal(warehouse, taskorderid);
            }
            //调用方法，把DataSet增加一列：KuCunNum，
            ds.Tables[0].Columns.Add("KuCunNum");
            if (CheckNetworkService())
            {
                Global.RemoteService.QueryMaterialKuCun(ds);
            }
            return ds;
        }

        /// <summary>
        /// 根据物料号和日期查询采购订单，同时返回联机状态。
        /// </summary>
        /// <param name="warehouse"></param>
        /// <param name="taskorderid"></param>
        /// <param name="so"></param>
        /// <returns></returns>
        public static DataSet GetFactoryOrderCheckOut(string warehouse, string materialNo, DateTime dtBegin, DateTime dtEnd, out ServiceOnline so)
        {
            if (CheckNetworkService())
            {
                so = ServiceOnline.Online;
                return Global.RemoteService.GetFactoryOrderCheckOut(warehouse, materialNo, dtBegin, dtEnd);
            }
            else
            {
                so = ServiceOnline.Offline;
                return Global.LocalService.GetFactoryOrderCheckOutLocal(warehouse, materialNo, dtBegin, dtEnd);
            }
        }
        #endregion

        #region 外部采购订单收货提交
        /// <summary>
        /// 外部采购订单收货提交。
        /// 1.先保存本地SQLite
        /// 2.判断网络情况，查询SAP服务是否可用，若网络可用，设置状态：已同步
        /// 3.提交SAP，成功或失败，设置状态。
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public static string SetFactoryOrderCheckOut(MES_SendRecordTask task, out string strMsg, out ServiceOnline so)
        {
            Global.LocalService.SetFactoryOrderCheckOutLocal(task);
            if (CheckNetworkService())
            {
                so = ServiceOnline.Online;
                string retval;
                bool success = SetFactoryOrderCheckOutSAP(task, out retval);
                if (success)
                {
                    //联网状态下，收货成功，需要修改本地数据。
                    Global.LocalService.UpdateFactoryOrderCheckOutLocal(task);
                    strMsg = "发货成功！" + retval;
                    return retval;
                }
                else
                {
                    strMsg = "发货失败！" + retval;
                    return string.Empty;
                }
            }
            else
            {  //未联网状态下，保存成功，需要修改本地数据。
                Global.LocalService.UpdateFactoryOrderCheckOutLocal(task);
                so = ServiceOnline.Offline;
                strMsg = "未联网，已保存本地。";
                return null;
            }
        }
        /// <summary>
        /// 采购订单收货提交SAP。
        /// </summary>
        /// <param name="task"></param>
        /// <param name="retval">收货成功retval=凭证号,收货失败retval=失败原因</param>
        /// <returns>返回是否收货成功</returns>
        private static bool SetFactoryOrderCheckOutSAP(MES_SendRecordTask task, out string retval)
        {
            //是否收货成功。
            //收货成功retval=凭证号
            //收货失败retval=失败原因
            bool success = false;
            //设置同步状态=Y
            SetFactoryOrderCheckOutStatus(task, "Syn_state", "Y");
            retval = "S";
            try
            {
                ZMES_FACTORYCAIGOUFAHUOResponse result300 = Global.RemoteService.SetFactoryOrderCheckOut(task);
                if (result300.RETVAL == "S")
                {
                    //收货成功，设置状态
                    SetFactoryOrderCheckOutStatus(task, "Del_state", "S", "Del_Remark", result300.RETMSG);
                    success = true;
                    retval = result300.RETMSG;
                }
                else
                {
                    //收货失败，设置状态
                    SetFactoryOrderCheckOutStatus(task, "Del_state", "E", "Del_Remark", result300.RETMSG);
                    retval = result300.RETMSG;
                }
            }
            catch (Exception ex)
            {
                //收货失败，设置status=3
                SetFactoryOrderCheckOutStatus(task, "Del_state", "E", "Del_Remark", ex.Message);
                retval = ex.Message;
            }
            return success;

        }
        private static void SetFactoryOrderCheckOutStatus(MES_SendRecordTask task, string fieldName, string fieldValue)
        {
            string commandText = string.Format("update MES_SendRecord set {0}='{1}' where Delivery_ID='{2}'", fieldName, fieldValue, task.Task_ID);
            Global.LocalService.DataAccess.ExecuteNonQuery(commandText);
        }

        private static void SetFactoryOrderCheckOutStatus(MES_SendRecordTask task, string fieldName, string fieldValue, string fieldName2, string fieldValue2)
        {
            string commandText = string.Format("update MES_SendRecord set {0}='{1}',{2}='{3}' where Delivery_ID='{4}'", fieldName, fieldValue, fieldName2, fieldValue2, task.Task_ID);
            Global.LocalService.DataAccess.ExecuteNonQuery(commandText);
        }


        #endregion

        #region 外部采购订单收货提交（历史）
        /// <summary>
        /// 采购订单收货提交（历史）
        /// 1.未提交的记录
        /// 2.提交失败的记录
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="loctionid"></param>
        /// <returns></returns>
        public static string SetFactoryOrderCheckOutHistory(MES_SendRecordTask task, out string strMsg)
        {
            if (CheckNetworkService())
            {
                string retval;
                bool success = SetFactoryOrderCheckOutSAP(task, out retval);
                if (success)
                {
                    strMsg = "收货成功！" + retval;
                    return retval;
                }
                else
                {
                    strMsg = "收货失败！" + retval;
                    return string.Empty;
                }
            }
            else
            {
                strMsg = "未联网，无法提交数据。";
                return null;
            }
        }
        #endregion

        #endregion
    }
}
