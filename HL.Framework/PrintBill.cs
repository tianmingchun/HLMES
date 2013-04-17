using System;

using System.Collections.Generic;
using System.Text;
using HL.Framework;
using System.Reflection;
using System.IO;
using Printlib;
using System.Windows.Forms;
using System.Threading;
using System.Data;
using BizLayer;

namespace HL.Framework
{
    /// <summary>
    /// 打印小票类
    /// </summary>
    public class PrintBill
    {
        #region 打开端口
        /// <summary>
        /// 打开端口
        /// </summary>
        /// <returns></returns>
        public static bool IsOpenPort()
        {
            try
            {
                IniFile file = new IniFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName) + "/PrintTest.ini");
                string port = file.ReadValue("Print", "PrintPort", "COM7");
                file.ReadValue("Print", "PrintModel", "MPK1230E");
                string s = file.ReadValue("Print", "PrintBaud", "115200");
                MPK1280 mpk = new MPK1280();
                mpk.portbaudrate = int.Parse(s);
                if (!mpk.OpenPort(port))
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
            }
            return false;
        }
        #endregion

        #region 打印外部采购订单收货小票
        /// <summary>
        /// 打印外部采购订单收货小票
        /// </summary>
        /// <param name="bookNum"></param>
        /// <param name="orderId"></param>
        /// <param name="supplyName"></param>
        /// <param name="PrintRecord"></param>
        public static void PrintOuterOrderCheckIn(string bookNum, string orderId, string supplyName, DataSet PrintRecord)
        {
            IniFile file = new IniFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName) + "/PrintTest.ini");
            string port = file.ReadValue("Print", "PrintPort", "COM7");
            file.ReadValue("Print", "PrintModel", "MPK1230E");
            string baud = file.ReadValue("Print", "PrintBaud", "115200");
            MPK1280Print_OuterOrderCheckIn(bookNum, orderId, supplyName, port, baud, PrintRecord);
        }

        /// <summary>
        /// 打印外部采购订单收货小票
        /// </summary>
        /// <param name="bookNum"></param>
        /// <param name="orderId"></param>
        /// <param name="supplyName"></param>
        /// <param name="port"></param>
        /// <param name="baud"></param>
        /// <param name="PrintRecord"></param>
        private static void MPK1280Print_OuterOrderCheckIn(string bookNum, string orderId, string supplyName, string port, string baud, DataSet PrintRecord)
        {
            string str = string.Empty;
            string str2 = string.Empty;
            string str3 = string.Empty;
            string str4 = string.Empty;
            string str5 = string.Empty;
            MPK1280 mpk = new MPK1280();
            mpk.portbaudrate = int.Parse(baud);
            if (!mpk.OpenPort(port))
            {
                MessageBox.Show("打开蓝牙打印机串口失败");
            }
            else
            {
                mpk.PrinterWake();
                mpk.PrinterReset();
                mpk.Feed(100);
                mpk.SetSnapMode(1);
                mpk.FontZoom(2, 1);
                mpk.FontBold(1);
                mpk.PrintStrLine("安徽合力收货小票");
                mpk.FontBold(0);
                mpk.FontZoom(1, 1);
                mpk.SetSnapMode(0);
                mpk.Feed(20);
                mpk.PrintStrLine("凭证:" + bookNum + "  次数:1");
                mpk.PrintStrLine("采购订单：" + orderId);
                mpk.FontZoom(1, 1);
                mpk.SetSnapMode(0);
                mpk.Feed(20);
                mpk.PrintStrLine("供应商：" + supplyName);
                mpk.PrintStr("工厂：" + Management.GetSingleton().WarehouseNo.Substring(0, 4) + "  库房：" + Management.GetSingleton().WarehouseNo);
                mpk.PrintStrLine("收货人：" + Global.CurrUser.Name);
                mpk.PrintStrLine("日期：" + DateTime.Now.ToString("yyyy-MM-dd"));
                mpk.PrintStr("------------------------");
                new MPK1230().FontCompress23(1);
                for (int i = 0; i < PrintRecord.Tables[0].Rows.Count; i++)
                {
                    str = PrintRecord.Tables[0].Rows[i]["FINISHQTY"].ToString();
                    str5 = PrintRecord.Tables[0].Rows[i]["UNIT"].ToString();
                    str2 = PrintRecord.Tables[0].Rows[i]["MATCODE"].ToString();
                    str3 = PrintRecord.Tables[0].Rows[i]["MATNAME"].ToString();
                    if (str3.Length < 0x10)
                    {
                        str4 = str3;
                    }
                    else
                    {
                        str4 = str3.Substring(0, 0x10);
                    }
                    mpk.PrintStrLine(str4);
                    mpk.FontUnderLine(2);
                    mpk.PrintStrLine(str2.PadRight(0x12, ' ') + str + str5);
                    mpk.FontUnderLine(0);
                }
                mpk.SetSnapMode(1);
                mpk.PrintStrLine("------End-----");
                mpk.PrintStrLine("          ");
                mpk.PrintCR();
                mpk.Feed(100);
                Thread.Sleep(0x3e8);
                mpk.ClosePort();
            }
        }

       
        #endregion
    }
}
