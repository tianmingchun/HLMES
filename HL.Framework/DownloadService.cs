using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Net;
using System.Diagnostics;
using HL.DAL;
using System.Reflection;
using BizLayer;
namespace HL.Framework
{
    /// <summary>
    /// 定义一个委托，以便线程之间传递数据。
    /// </summary>
    /// <param name="data"></param>
    public delegate void ShowProgress(int progress, string data);
    /// <summary>
    /// 调用SAP服务接口，下载任务到移动端。
    /// </summary>
    public static class DownloadService
    {
        #region 外部采购订单收货记录下载
        /// <summary>
        /// 外部采购订单收货记录下载。
        /// 应该只根据当前用户的账户获取全部采购订单收货记录。
        /// 如果获取的记录集太大，应该根据日期再筛选，否则无需传入日期，直接按
        /// 账户一次性下载即可。
        /// </summary>
        /// <returns></returns>
        public static void DownOuterOrderCheckIn(string warehouse, DateTime begindate, DateTime enddate, Form form, ShowProgress method)
        {
            try
            {
                form.Invoke(method, new object[] { 1, "正在连接服务器..." });
               

                DateTime beginTime = System.DateTime.Now;

                HL.BLL.sap.checkInPurOrder.ZMES_BATFETCHPURORDER obj300 = new HL.BLL.sap.checkInPurOrder.ZMES_BATFETCHPURORDER();
                obj300.Url = obj300.Url.Replace(Global.RemoteService.DevServerUrl, Management.GetSingleton().DefaultBaseUrl);
                obj300.Credentials = new NetworkCredential(Management.GetSingleton().ServiceUserName, Management.GetSingleton().ServicePassword);

                HL.BLL.sap.checkInPurOrder.ZMES_BATFETCHPURORDER1 input300 = new HL.BLL.sap.checkInPurOrder.ZMES_BATFETCHPURORDER1();
                HL.BLL.sap.checkInPurOrder.ZMES_PURORDER objrow300 = new HL.BLL.sap.checkInPurOrder.ZMES_PURORDER();
                HL.BLL.sap.checkInPurOrder.ZMES_PURORDER[] objtab300 = new HL.BLL.sap.checkInPurOrder.ZMES_PURORDER[1];
                input300.TB_EKPO = objtab300;
                input300.USERNAME = "a";
                form.Invoke(method, new object[] { 1, "正在下载数据..." });
               
                HL.BLL.sap.checkInPurOrder.ZMES_BATFETCHPURORDERResponse result300 = obj300.CallZMES_BATFETCHPURORDER(input300);
                objtab300 = result300.TB_EKPO;

                form.Invoke(method, new object[] { 1, "已下载数据，记录数：" + objtab300.Length + "\r\n正在准备更新..." });

                UpdateOuterOrderCheckIn(objtab300, objtab300.Length, form, method);

                DateTime endTime = System.DateTime.Now;
                double totalSeconds = endTime.Subtract(beginTime).TotalSeconds;
                form.Invoke(method, new object[] { -3, "同步数据完成，记录数：" + objtab300.Length + "，耗时：" + totalSeconds + " 秒。" });
               
            }
            catch (Exception ex)
            {
                form.Invoke(method, new object[] { -1, ex.Message });
            }
        }

        /// <summary>
        /// 从远程下载的数据，更新本地数据库。
        /// </summary>
        private static void UpdateOuterOrderCheckIn(HL.BLL.sap.checkInPurOrder.ZMES_PURORDER[] purOrderRows, int recordCount, Form form, ShowProgress method)
        {
            string tableName = "ZMES_EKKO";
            try
            {
                Global.LocalService.DataAccess.BeginTransaction();
                Global.LocalService.DataAccess.ExecuteNonQuery("delete from " + tableName);
                int recordIndex = 1;
                for (int i = 0; i < purOrderRows.Length; i++)
                {
                    HL.BLL.sap.checkInPurOrder.ZMES_PURORDER purOrderRow = (HL.BLL.sap.checkInPurOrder.ZMES_PURORDER)purOrderRows.GetValue(i);

                    Type t = purOrderRow.GetType();

                    if (!string.IsNullOrEmpty(tableName))
                    {
                        StringBuilder commandText = new StringBuilder("insert into " + tableName + "(");
                        int j = 0;
                        foreach (PropertyInfo pi in t.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                        {
                            if (j > 0)
                                commandText.Append(",");
                            commandText.Append(pi.Name);
                            j++;
                        }
                        commandText.Append(") values (");

                        j = 0;
                        StringBuilder commandValues = new StringBuilder();
                        foreach (PropertyInfo pi in t.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                        {
                            if (j > 0)
                                commandValues.Append(",");
                            object o = t.GetProperty(pi.Name).GetValue(purOrderRow, null);
                            if (o == null || o == DBNull.Value)
                            {
                                commandValues.Append("null");
                            }
                            else if (o.GetType().ToString() == "System.DateTime")
                            {
                                commandValues.Append("'" + Convert.ToDateTime(o).ToString("yyyy-MM-dd hh:mm:ss") + "'");
                            }
                            else
                            {
                                commandValues.Append("'" + DbValue.FormatSqlText(o.ToString()) + "'");
                            }
                            j++;
                        }
                        Global.LocalService.DataAccess.ExecuteNonQuery(commandText.ToString() + commandValues.ToString() + ")");
                        form.Invoke(method, new object[] { 1, "正在更新数据:\r\n" + tableName + "\r\n" + recordIndex + "/" + recordCount });
                        recordIndex++;
                    }
                }

                Global.LocalService.DataAccess.Commit();
            }
            catch (Exception ex)
            {
                Global.LocalService.DataAccess.RollBack();
                throw ex;
            }
        }     
        #endregion      
 
        #region 工厂间采购订单收货记录下载
        /// <summary>
        /// 外部采购订单收货记录下载。
        /// 应该只根据当前用户的账户获取全部采购订单收货记录。
        /// 如果获取的记录集太大，应该根据日期再筛选，否则无需传入日期，直接按
        /// 账户一次性下载即可。
        /// </summary>
        /// <returns></returns>
        public static void DownFactoryOrderCheckIn(string warehouse, DateTime begindate, DateTime enddate, Form form, ShowProgress method)
        {
            try
            {
                form.Invoke(method, new object[] { 1, "正在连接服务器..." });


                DateTime beginTime = System.DateTime.Now;

                HL.BLL.SAP300_GetFactoryOrderCheckIn.ZMES_PURCHASE_SINGLE_DATA obj300 = new HL.BLL.SAP300_GetFactoryOrderCheckIn.ZMES_PURCHASE_SINGLE_DATA();
                obj300.Url = obj300.Url.Replace(Global.RemoteService.DevServerUrl, Management.GetSingleton().DefaultBaseUrl);
                obj300.Credentials = new NetworkCredential(Management.GetSingleton().ServiceUserName, Management.GetSingleton().ServicePassword);

                HL.BLL.SAP300_GetFactoryOrderCheckIn.ZMES_PURCHASE_SINGLE_DATA1 input300 = new HL.BLL.SAP300_GetFactoryOrderCheckIn.ZMES_PURCHASE_SINGLE_DATA1();

              
                HL.BLL.SAP300_GetFactoryOrderCheckIn.ZMES_PURCHASE_UB objrow300 = new HL.BLL.SAP300_GetFactoryOrderCheckIn.ZMES_PURCHASE_UB();
                HL.BLL.SAP300_GetFactoryOrderCheckIn.ZMES_PURCHASE_UB[] objtab300 = new HL.BLL.SAP300_GetFactoryOrderCheckIn.ZMES_PURCHASE_UB[1];
                input300.TB_EKPO = objtab300;
                input300.USERNAME = "a";
                form.Invoke(method, new object[] { 1, "正在下载数据..." });

                HL.BLL.SAP300_GetFactoryOrderCheckIn.ZMES_PURCHASE_SINGLE_DATAResponse result300 = obj300.CallZMES_PURCHASE_SINGLE_DATA(input300);
                objtab300 = result300.TB_EKPO;

                form.Invoke(method, new object[] { 1, "已下载数据，记录数：" + objtab300.Length + "\r\n正在准备更新..." });

                UpdateFactoryOrderCheckIn(objtab300, objtab300.Length, form, method);

                DateTime endTime = System.DateTime.Now;
                double totalSeconds = endTime.Subtract(beginTime).TotalSeconds;
                form.Invoke(method, new object[] { -3, "同步数据完成，记录数：" + objtab300.Length + "，耗时：" + totalSeconds + " 秒。" });

            }
            catch (Exception ex)
            {
                form.Invoke(method, new object[] { -1, ex.Message });
            }
        }

        /// <summary>
        /// 从远程下载的数据，更新本地数据库。
        /// </summary>
        private static void UpdateFactoryOrderCheckIn(HL.BLL.SAP300_GetFactoryOrderCheckIn.ZMES_PURCHASE_UB[] purOrderRows, int recordCount, Form form, ShowProgress method)
        {
            string tableName = "MES_Receive";
            try
            {
                Global.LocalService.DataAccess.BeginTransaction();
                Global.LocalService.DataAccess.ExecuteNonQuery("delete from " + tableName);
                int recordIndex = 1;
                for (int i = 0; i < purOrderRows.Length; i++)
                {
                    HL.BLL.SAP300_GetFactoryOrderCheckIn.ZMES_PURCHASE_UB purOrderRow = (HL.BLL.SAP300_GetFactoryOrderCheckIn.ZMES_PURCHASE_UB)purOrderRows.GetValue(i);

                    Type t = purOrderRow.GetType();

                    if (!string.IsNullOrEmpty(tableName))
                    {
                        StringBuilder commandText = new StringBuilder("insert into " + tableName + "(");
                        int j = 0;
                        foreach (PropertyInfo pi in t.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                        {
                            if (j > 0)
                                commandText.Append(",");
                            commandText.Append(pi.Name);
                            j++;
                        }
                        commandText.Append(") values (");

                        j = 0;
                        StringBuilder commandValues = new StringBuilder();
                        foreach (PropertyInfo pi in t.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                        {
                            if (j > 0)
                                commandValues.Append(",");
                            object o = t.GetProperty(pi.Name).GetValue(purOrderRow, null);
                            if (o == null || o == DBNull.Value)
                            {
                                commandValues.Append("null");
                            }
                            else if (o.GetType().ToString() == "System.DateTime")
                            {
                                commandValues.Append("'" + Convert.ToDateTime(o).ToString("yyyy-MM-dd hh:mm:ss") + "'");
                            }
                            else
                            {
                                commandValues.Append("'" + DbValue.FormatSqlText(o.ToString()) + "'");
                            }
                            j++;
                        }
                        Global.LocalService.DataAccess.ExecuteNonQuery(commandText.ToString() + commandValues.ToString() + ")");
                        form.Invoke(method, new object[] { 1, "正在更新数据:\r\n" + tableName + "\r\n" + recordIndex + "/" + recordCount });
                        recordIndex++;
                    }
                }

                Global.LocalService.DataAccess.Commit();
            }
            catch (Exception ex)
            {
                Global.LocalService.DataAccess.RollBack();
                throw ex;
            }
        }
        #endregion      
 
        #region 工厂间采购订单发货记录下载
        /// <summary>
        /// 工厂间采购订单发货记录下载。
        /// 应该只根据当前用户的账户获取全部采购订单收货记录。
        /// 如果获取的记录集太大，应该根据日期再筛选，否则无需传入日期，直接按
        /// 账户一次性下载即可。
        /// </summary>
        /// <returns></returns>
        public static void DownFactoryOrderCheckOut(string warehouse, DateTime begindate, DateTime enddate, Form form, ShowProgress method)
        {
            try
            {
                form.Invoke(method, new object[] { 1, "正在连接服务器..." });


                DateTime beginTime = System.DateTime.Now;

                HL.BLL.SAP300_GetFactoryOrderCheckOut.ZMES_PURCHASE_FETCH_DATA obj300 = new HL.BLL.SAP300_GetFactoryOrderCheckOut.ZMES_PURCHASE_FETCH_DATA();
                obj300.Url = obj300.Url.Replace(Global.RemoteService.DevServerUrl, Management.GetSingleton().DefaultBaseUrl);
                obj300.Credentials = new NetworkCredential(Management.GetSingleton().ServiceUserName, Management.GetSingleton().ServicePassword);

                HL.BLL.SAP300_GetFactoryOrderCheckOut.ZMES_PURCHASE_FETCH_DATA1 input300 = new HL.BLL.SAP300_GetFactoryOrderCheckOut.ZMES_PURCHASE_FETCH_DATA1();
                HL.BLL.SAP300_GetFactoryOrderCheckOut.ZMES_PURCHASE_UB objrow300 = new HL.BLL.SAP300_GetFactoryOrderCheckOut.ZMES_PURCHASE_UB();
                HL.BLL.SAP300_GetFactoryOrderCheckOut.ZMES_PURCHASE_UB[] objtab300 = new HL.BLL.SAP300_GetFactoryOrderCheckOut.ZMES_PURCHASE_UB[1];
                input300.TB_EKPO = objtab300;
                input300.USERNAME = "a";
                form.Invoke(method, new object[] { 1, "正在下载数据..." });

                HL.BLL.SAP300_GetFactoryOrderCheckOut.ZMES_PURCHASE_FETCH_DATAResponse result300 = obj300.CallZMES_PURCHASE_FETCH_DATA(input300);
                objtab300 = result300.TB_EKPO;

                form.Invoke(method, new object[] { 1, "已下载数据，记录数：" + objtab300.Length + "\r\n正在准备更新..." });

                UpdateFactoryOrderCheckOut(objtab300, objtab300.Length, form, method);

                DateTime endTime = System.DateTime.Now;
                double totalSeconds = endTime.Subtract(beginTime).TotalSeconds;
                form.Invoke(method, new object[] { -3, "同步数据完成，记录数："+objtab300.Length+"，耗时：" + totalSeconds + " 秒。" });

            }
            catch (Exception ex)
            {
                form.Invoke(method, new object[] { -1, ex.Message });
            }
        }

        /// <summary>
        /// 从远程下载的数据，更新本地数据库。
        /// </summary>
        private static void UpdateFactoryOrderCheckOut(HL.BLL.SAP300_GetFactoryOrderCheckOut.ZMES_PURCHASE_UB[] purOrderRows, int recordCount, Form form, ShowProgress method)
        {
            string tableName = "MES_Send";
            try
            {
                Global.LocalService.DataAccess.BeginTransaction();
                Global.LocalService.DataAccess.ExecuteNonQuery("delete from " + tableName);
                int recordIndex = 1;
                for (int i = 0; i < purOrderRows.Length; i++)
                {
                    HL.BLL.SAP300_GetFactoryOrderCheckOut.ZMES_PURCHASE_UB purOrderRow = (HL.BLL.SAP300_GetFactoryOrderCheckOut.ZMES_PURCHASE_UB)purOrderRows.GetValue(i);

                    Type t = purOrderRow.GetType();

                    if (!string.IsNullOrEmpty(tableName))
                    {
                        StringBuilder commandText = new StringBuilder("insert into " + tableName + "(");
                        int j = 0;
                        foreach (PropertyInfo pi in t.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                        {
                            if (j > 0)
                                commandText.Append(",");
                            commandText.Append(pi.Name);
                            j++;
                        }
                        commandText.Append(") values (");

                        j = 0;
                        StringBuilder commandValues = new StringBuilder();
                        foreach (PropertyInfo pi in t.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                        {
                            if (j > 0)
                                commandValues.Append(",");
                            object o = t.GetProperty(pi.Name).GetValue(purOrderRow, null);
                            if (o == null || o == DBNull.Value)
                            {
                                commandValues.Append("null");
                            }
                            else if (o.GetType().ToString() == "System.DateTime")
                            {
                                commandValues.Append("'" + Convert.ToDateTime(o).ToString("yyyy-MM-dd hh:mm:ss") + "'");
                            }
                            else
                            {
                                commandValues.Append("'" + DbValue.FormatSqlText(o.ToString()) + "'");
                            }
                            j++;
                        }
                        Global.LocalService.DataAccess.ExecuteNonQuery(commandText.ToString() + commandValues.ToString() + ")");
                        form.Invoke(method, new object[] { 1, "正在更新数据:\r\n" + tableName + "\r\n" + recordIndex + "/" + recordCount });
                        recordIndex++;
                    }
                }

                Global.LocalService.DataAccess.Commit();
            }
            catch (Exception ex)
            {
                Global.LocalService.DataAccess.RollBack();
                throw ex;
            }
        }
        #endregion       
    }
}
