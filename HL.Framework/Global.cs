using System;
using System.Collections.Generic;
using System.Text;
using Entity;
using System.IO;
using System.Runtime.InteropServices;
using System.Data;
using HL.DAL;
using HL.BLL;

namespace HL.Framework
{
    public static class Global
    {      
        public static EnumBarcodeType BarcodeType;
        public static Task CurrTask;
        public static EnumTaskType CurrTaskType;
        public static User CurrUser = new User("tmc", "1");
        public static UserPriview CurrUserpriview;       
        public static bool IsRegulationUpdated;      
        public static BizLayer.MiddleService MiddleService;
        public static DataTable ModuleMenus;
        /// <summary>
        /// 移动端SQLite数据服务
        /// </summary>
        public static LocalService LocalService;
        /// <summary>
        /// 服务端SAP数据服务
        /// </summary>
        public static RemoteService RemoteService;

        [DllImport("coredll")]
        public static extern int CreateMutex(IntPtr lpMutexAttributes, bool InitialOwner, string MutexName);
        [DllImport("CoreDll")]
        public static extern IntPtr FindWindow(string className, string WindowsName);
        [DllImport("coredll")]
        public static extern int GetLastError();
        [DllImport("CoreDll")]
        public static extern bool IsWindowVisible(IntPtr hWnd);
        [DllImport("CoreDll")]
        public static extern int SendMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam);
        [DllImport("CoreDll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("CoreDll")]
        public static extern int ShowWindowEx(IntPtr hWnd, int nCmdShow);
    }
}
