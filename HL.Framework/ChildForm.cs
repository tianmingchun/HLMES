using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HL.Framework.Properties;
using System.IO;
using HL.DAL;
using System.Reflection;
using HL.Controls;

namespace HL.Framework
{
    public partial class ChildForm : AccessForm
    {
        #region Vars
        private Form UIForm;
        #endregion

        #region Construct
        public ChildForm()
        {
            InitializeComponent();
        }
        #endregion     
  
        #region Override
        /// <summary>
        /// 界面布局初始化。
        /// </summary>
        protected override void InitializeLayout()
        {

        }


        #endregion

        #region ModuleClick
        public void LoadChildMenus(IFramework framework, DataRow[] childRow,string background)
        {
            _childForm.LineNum = 3;
            _childForm.Framework = framework;           
            //背景图片
            _childForm.BackgroundImage = GetResourceImage(background);
            _childForm.FormBorderStyle = FormBorderStyle.None;
            _childForm.PanelDock = DockStyle.Fill;
            List<AccessButton> buttons = new List<AccessButton>();
            for (int i = 0; i < childRow.Length; i++)
            {
                Image image = GetResourceImage(DbValue.GetString(childRow[i]["icon"]));
                AccessButton button = new AccessButton(DbValue.GetString(childRow[i]["MenuText"]), image, ModuleClick);
                //AccessButton button = new AccessButton(image, ModuleClick);
                button.Tag = DbValue.GetString(childRow[i]["MenuID"]);
                buttons.Add(button);
                if (button.Text == "返回")
                {
                    button.Click += delegate(object sender, EventArgs e)
                    {
                        _childForm.Close();
                    };
                }
            }
            _childForm.CreateLayout(buttons);
            _childForm.MainPanel.Focus();
            _childForm.ShowDialog();
            _childForm.Dispose();
            _childForm = null;
        }

      

        public void ModuleClick(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                ImageButton button = (ImageButton)sender;
                DataRow[] rows = Global.ModuleMenus.Select("MenuID='" + button.Tag.ToString()+"'");
                ShowModule(rows[0]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private ChildForm _childForm;
        public void ShowModule(DataRow row)
        {
            string menuId = DbValue.GetString(row["MenuID"]);
            string menuText = DbValue.GetString(row["MenuText"]);   
            //判断是否存在下级菜单，如果有下级菜单，则载入下级菜单窗口
            DataRow[] childRow = Global.ModuleMenus.Select("PID='" + menuId + "'");
            if (childRow.Length > 0)
            {
                _childForm = new ChildForm();
                LoadChildMenus(this.Framework, childRow, DbValue.GetString(row["Background"]));
            }
            else
            {
                if (menuText == "退出系统" && _childForm == null)
                {
                    if (MessageBox.Show("你确定要退出采集程序吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        Application.Exit();
                    }
                }
                else
                {
                    string assemblyFile = DbValue.GetString(row["Assembly"]);
                    string className = DbValue.GetString(row["ClassName"]);
                    if (!string.IsNullOrEmpty(assemblyFile) && !string.IsNullOrEmpty(className))
                    {
                        string path = Path.Combine(Global.LocalService.ApplicationPath, assemblyFile);
                        Assembly assebly = Assembly.LoadFrom(path);
                        Type type = assebly.GetType(className, false);
                        object instance = Activator.CreateInstance(type);
                        UIForm = instance as Form;
                        if (UIForm == null)
                        {
                            ICommand command = instance as ICommand;
                            if (command != null)
                            {
                                command.Execute();
                            }
                            else
                            {
                                Cursor.Current = Cursors.Default;
                                MessageBox.Show("无效对象。");
                                return;
                            }
                        }
                        else
                        {
                            UIForm.Text = menuText;                          
                            UIForm.Load += new EventHandler(_frameForm_Load);
                            UIForm.Closed += new EventHandler(_frameForm_Closed);
                            UIForm.ShowDialog();
                            UIForm.Dispose();
                        }
                    }                    
                }
            }
        }

        void _frameForm_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Default;
        }

        void _frameForm_Closed(object sender, EventArgs e)
        {

        }
        #endregion


        #region Utils
        public Image GetResourceImage(string icon)
        {
            if (string.IsNullOrEmpty(icon))
                return Resources.blank;
            icon = Path.GetFileNameWithoutExtension(icon).ToLower();
            object obj = Resources.ResourceManager.GetObject(icon, Resources.resourceCulture);
            if (obj == null)
                return Resources.blank;
            return ((System.Drawing.Bitmap)(obj));
        }
        #endregion   

    }
}