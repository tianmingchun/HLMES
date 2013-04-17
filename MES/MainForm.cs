using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms; 
using System.Reflection;
using System.IO; 
using System.Threading; 
using System.Resources;
using HL.Framework;
using HL.Controls;
using HL.DAL;
using HL.Framework.Utils;
using MES.Properties;

namespace MES
{
    public partial class MainForm : ChildForm, IFramework
    {      
        #region Constructor
        public MainForm()
        {
            InitializeComponent();
        }
        #endregion

        #region IFramework 成员
        /// <summary>
        /// 主窗体对象
        /// </summary>
        Form IFramework.MainForm
        {
            get { return this; }
        }
        #endregion

        #region OnLoad
        private DataTable BuildMenus()
        {
            DataTable dtMenus = new DataTable();
            dtMenus.Columns.Add("MenuID");
            dtMenus.Columns.Add("PID");
            dtMenus.Columns.Add("MenuText");
            dtMenus.Columns.Add("Icon");
            dtMenus.Columns.Add("Assembly");
            dtMenus.Columns.Add("ClassName");
            dtMenus.Columns.Add("Background");

            dtMenus.Rows.Add(CreateMenuRow(dtMenus.NewRow(), "1", "0", "外部采购订单收货", "blank.png", "HL.UI.dll", "HL.UI.CheckIn.OuterOrderForm", ""));
            dtMenus.Rows.Add(CreateMenuRow(dtMenus.NewRow(), "2", "0", "工厂采购订单收货", "blank.png", "HL.UI.dll", "HL.UI.CheckIn.FactoryOrderForm", ""));

            dtMenus.Rows.Add(CreateMenuRow(dtMenus.NewRow(), "3", "0", "工厂采购订单发货", "blank.png", "HL.UI.dll", "HL.UI.CheckOut.FactoryOrderForm", ""));

          
            //FactoryOrderForm

            //dtMenus.Rows.Add(CreateMenuRow(dtMenus.NewRow(), "20", "0", "下载数据", "blank.png", "HL.UI.dll", "HL.UI.Download.DownloadDataForm", ""));
            //dtMenus.Rows.Add(CreateMenuRow(dtMenus.NewRow(), "1", "0", "收货采集", "blank.png", "", "", "background_main.jpg"));
            //dtMenus.Rows.Add(CreateMenuRow(dtMenus.NewRow(), "101", "1", "收货采集", "blank.png", "HL.UI.dll", "HL.UI.CheckInCommand", ""));
            //dtMenus.Rows.Add(CreateMenuRow(dtMenus.NewRow(), "102", "1", "按订单收货", "blank.png", "HL.UI.dll", "HL.UI.CheckInByOrderFrm", ""));
            //dtMenus.Rows.Add(CreateMenuRow(dtMenus.NewRow(), "103", "1", "返回", "blank.png", "", "", ""));

            //dtMenus.Rows.Add(CreateMenuRow(dtMenus.NewRow(), "2", "0", "发料采集", "blank.png", "", "", "background_main.jpg"));
            //dtMenus.Rows.Add(CreateMenuRow(dtMenus.NewRow(), "201", "2", "发料采集", "blank.png", "HL.UI.dll", "HL.UI.CheckOutCommand", ""));
            //dtMenus.Rows.Add(CreateMenuRow(dtMenus.NewRow(), "202", "2", "按物料发料", "blank.png", "HL.UI.dll", "HL.UI.CheckOutByMaterialFrm", ""));
            //dtMenus.Rows.Add(CreateMenuRow(dtMenus.NewRow(), "203", "2", "按订单发料", "blank.png", "HL.UI.dll", "HL.UI.CheckOutByOrderFrm", ""));
            //dtMenus.Rows.Add(CreateMenuRow(dtMenus.NewRow(), "204", "2", "特殊发料", "blank.png", "HL.UI.dll", "HL.UI.MoveOutByGoods", ""));
            //dtMenus.Rows.Add(CreateMenuRow(dtMenus.NewRow(), "205", "2", "返回", "blank.png", "", "", ""));


            //dtMenus.Rows.Add(CreateMenuRow(dtMenus.NewRow(), "3", "0", "检验采集", "blank.png", "", "", "background_main.jpg"));
            //dtMenus.Rows.Add(CreateMenuRow(dtMenus.NewRow(), "301", "3", "单次检验", "blank.png", "HL.UI.dll", "HL.UI.CheckOrderCommand", ""));
            //dtMenus.Rows.Add(CreateMenuRow(dtMenus.NewRow(), "302", "3", "批量检验", "blank.png", "HL.UI.dll", "HL.UI.InspectGoodsFrm", ""));
            //dtMenus.Rows.Add(CreateMenuRow(dtMenus.NewRow(), "303", "3", "返回", "blank.png", "", "", ""));

            dtMenus.Rows.Add(CreateMenuRow(dtMenus.NewRow(), "4", "0", "系统配置", "blank.png", "HL.UI.dll", "HL.UI.ConfigMain", "background_main.jpg"));
            dtMenus.Rows.Add(CreateMenuRow(dtMenus.NewRow(), "401", "4", "设置事业部", "blank.png", "HL.UI.dll", "HL.UI.PartConfigFrm", ""));
            dtMenus.Rows.Add(CreateMenuRow(dtMenus.NewRow(), "402", "4", "设置仓库", "blank.png", "HL.UI.dll", "HL.UI.ConfigFrm", ""));
            //dtMenus.Rows.Add(CreateMenuRow(dtMenus.NewRow(), "403", "4", "设置打印机", "blank.png", "HL.UI.dll", "HL.UI.ConfigPrinterCommand", ""));
            //dtMenus.Rows.Add(CreateMenuRow(dtMenus.NewRow(), "404", "4", "补打小票", "blank.png", "HL.UI.dll", "HL.UI.ReprintListFrm", ""));
            dtMenus.Rows.Add(CreateMenuRow(dtMenus.NewRow(), "405", "4", "返回", "blank.png", "", "", ""));


            //dtMenus.Rows.Add(CreateMenuRow(dtMenus.NewRow(), "5", "0", "更新程序", "blank.png", "", "", ""));
            dtMenus.Rows.Add(CreateMenuRow(dtMenus.NewRow(), "6", "0", "退出系统", "blank.png", "", "", ""));

            return dtMenus;
        }
        private DataRow CreateMenuRow(DataRow row, string menuId, string pid, string menuText, string icon, string assembly, string className, string background)
        {
            row["MenuID"] = menuId;
            row["PID"] = pid;
            row["MenuText"] = menuText;
            row["Icon"] = icon;
            row["Assembly"] = assembly;
            row["ClassName"] = className;
            row["Background"] = background;
            return row;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.LineNum = 3;
            this.Framework = this;
            //测试用菜单，需要从服务器获取用户菜单项
            Global.ModuleMenus = BuildMenus();         
            //背景图片
            this.BackgroundImage = Resources.background_main;
            this.PanelDock = DockStyle.Fill;
            List<AccessButton> buttons = new List<AccessButton>();
            DataRow[] findRows = Global.ModuleMenus.Select("pid='0'");
            for (int i = 0; i < findRows.Length; i++)
            {
                Image image = GetResourceImage(DbValue.GetString(findRows[i]["icon"]));
                AccessButton button = new AccessButton(DbValue.GetString(findRows[i]["MenuText"]), image, ModuleClick);
                //AccessButton button = new AccessButton(image, ModuleClick);
                button.Tag = DbValue.GetString(findRows[i]["MenuID"]);
                buttons.Add(button);
            }
            
            this.CreateLayout(buttons);
            this.MainPanel.Focus();
        }      
        #endregion

      

        #region Menu
      

        private void UpdateClick(object sender, EventArgs e)
        {
           
        }

        private void HelpClick(object sender, EventArgs e)
        {
            MessageBox.Show("帮助不存在。", "提示");
        }


        private void SyncClick(object sender, EventArgs e)
        {
           
        }

        private void OptionClick(object sender, EventArgs e)
        {
             
        }      

        private void ExitClick(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion      
    

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == System.Windows.Forms.Keys.Up))
            {
                // Up
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Down))
            {
                // Down
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Left))
            {
                // Left
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Right))
            {
                // Right
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Enter))
            {
                // Enter
            }

        }
    }
}