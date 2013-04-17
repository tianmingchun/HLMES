using BizLayer;
using Entity;
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Windows.Forms;
using HL.Framework;

namespace MES
{
    static class Program
    {
        public static LoginFrm LoginFrm;
        private const int ERROR_ALREADY_EXISTS = 0xb7;       
        [MTAThread]
        private static void Main()
        {
            if ((Global.CreateMutex(IntPtr.Zero, true, "MES") != 0) && (Global.GetLastError() != ERROR_ALREADY_EXISTS))
            {
                //初始化本地数据服务，应该放到登录窗口中
                Global.LocalService = new HL.BLL.LocalService();
                Global.RemoteService = new HL.BLL.RemoteService();
                MainForm mainForm = new MainForm();
                mainForm.ShowDialog();
                mainForm.Dispose();
                //LoginFrm = new LoginFrm();
                //LoginFrm.ShowDialog();
                //LoginFrm.Dispose();
            }
        }      
    }
}