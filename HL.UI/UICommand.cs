using System;
using System.Collections.Generic;
using System.Text;
using HL.Framework;
using BizLayer;
using Entity;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;

namespace HL.UI
{
    #region 单次检验
    /// <summary>
    /// 单次检验
    /// </summary>
    public class CheckOrderCommand : ICommand
    {
        private MiddleService service = Global.MiddleService;
        public void Execute()
        {
            string taskMsg = this.GetTaskMsg(EnumTaskType.CheckInspect);
            DataSet instasks = new DataSet();
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                instasks = this.service.GetInspectTasks();
            }
            catch (ApplicationException exception)
            {
                Cursor.Current = Cursors.Default;
                MessageShow.Alert("错误", "下载" + taskMsg + "任务失败！" + exception.Message);
                return;
            }
            catch (Exception exception2)
            {
                Cursor.Current = Cursors.Default;
                MessageShow.Alert("错误", "下载" + taskMsg + "任务失败！" + exception2.Message);
                return;
            }
            Cursor.Current = Cursors.Default;
            if (instasks.Tables[0].Rows.Count != 0)
            {
                InspectFrm1 frm = new InspectFrm1(instasks);
                frm.ShowDialog();
                frm.Dispose();
            }
            else
            {
                MessageShow.Alert("Warning", "当前没有任何" + taskMsg + "任务！");
            }
        }

        private string GetTaskMsg(EnumTaskType taskType)
        {
            switch (taskType)
            {
                case EnumTaskType.CheckIn:
                    return "入库";

                case EnumTaskType.CheckOut:
                    return "出库";

                case EnumTaskType.CheckInspect:
                    return "检验";
            }
            return "";
        }

    }
    #endregion

    #region 设置打印机
    /// <summary>
    /// 设置打印机
    /// </summary>
    public class ConfigPrinterCommand : ICommand
    {
        public void Execute()
        {
            IntPtr hWnd = Global.FindWindow(null, "BluetoothSPPUI");
            if (hWnd.ToInt32() <= 0)
            {
                string fileName = @"\Storage\BTPrinter\SPPUICN.EXE";
                try
                {
                    Process.Start(fileName, "");
                }
                catch (Exception)
                {
                    MessageBox.Show("无法启动蓝牙配置程序", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                try
                {
                    if (Global.IsWindowVisible(hWnd))
                    {
                        Global.SetForegroundWindow(hWnd);
                    }
                    else
                    {
                        Global.SendMessage(hWnd, 1, IntPtr.Zero, IntPtr.Zero);
                    }
                }
                catch
                {
                    MessageBox.Show("打开错误");
                }
            }
        }
    }
    #endregion

    #region 发料采集
    /// <summary>
    /// 发料采集
    /// </summary>
    public class CheckOutCommand : ICommand
    {
        private MiddleService service = Global.MiddleService;
        public void Execute()
        {
            DataSet task = new DataSet();
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                task = this.service.GetOutTasks(Management.GetSingleton().WarehouseNo);
            }
            catch (ApplicationException exception)
            {
                Cursor.Current = Cursors.Default;
                MessageShow.Alert("错误", "下载发料任务失败！" + exception.Message);
                return;
            }
            catch (Exception exception2)
            {
                Cursor.Current = Cursors.Default;
                MessageShow.Alert("错误", "下载发料任务失败！" + exception2.Message);
                return;
            }
            Cursor.Current = Cursors.Default;
            if (task.Tables.Count >= 1)
            {
                if (task.Tables[0].Rows.Count == 0)
                {
                    MessageShow.Alert("Warning", "当前没有任何发料任务！");
                }
                else
                {
                    CheckInTasksFrm frm = new CheckInTasksFrm(task, EnumTaskType.CheckOut);
                    frm.ShowDialog();
                    frm.Dispose();
                }
            }
            else
            {
                MessageShow.Alert("Warning", "当前没有任何发料任务！");
            }
        }
    }
    #endregion

    #region 收货采集
    /// <summary>
    /// 收货采集
    /// </summary>
    public class CheckInCommand : ICommand
    {
        private MiddleService service = Global.MiddleService;
        public void Execute()
        {
            DataSet task = new DataSet();
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                task = this.service.GetInTasks(Management.GetSingleton().WarehouseNo);
            }
            catch (ApplicationException exception)
            {
                Cursor.Current = Cursors.Default;
                MessageShow.Alert("错误", "下载收货任务失败！" + exception.Message);
                return;
            }
            catch (Exception exception2)
            {
                Cursor.Current = Cursors.Default;
                MessageShow.Alert("错误", "下载收货任务失败！" + exception2.Message);
                return;
            }
            Cursor.Current = Cursors.Default;
            if (task.Tables.Count >= 1)
            {
                if (task.Tables[0].Rows.Count == 0)
                {
                    MessageShow.Alert("Warning", "当前没有任何收货任务！");
                }
                else
                {
                    CheckInTasksFrm frm = new CheckInTasksFrm(task, EnumTaskType.CheckIn);
                    frm.ShowDialog();
                    frm.Dispose();
                }
            }
            else
            {
                MessageShow.Alert("Warning", "当前没有任何收货任务！");
            }
        }
    }
    #endregion
}
