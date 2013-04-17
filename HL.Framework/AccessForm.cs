using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HL.Controls;
using System.IO;

namespace HL.Framework
{   
    /// <summary>
    /// 界面布局控件。
    /// </summary>
    public partial class AccessForm : BaseForm
    {
        private string _backgroundImageFile;
        private Panel _pnlMain;
        public AccessForm()
        {
            this.LineNum = 3;
            InitializeComponent();
            InitializeLayout();
        }   
      
        /// <summary>
        /// 背景图片。
        /// </summary>
        public Image BackgroundImage
        {
            get
            {
                return this.picBackground.Image;
            }
            set
            {
                this.picBackground.Image = value;
            }
        }

        /// <summary>
        /// 背景图片文件。
        /// </summary>
        public string BackgroundImageFile
        {
            get
            {
                return this._backgroundImageFile;
            }
            set
            {
                this._backgroundImageFile = value;
                if (File.Exists(this._backgroundImageFile))
                    this.picBackground.Image = new Bitmap(this._backgroundImageFile);
            }
        }
        /// <summary>
        /// 背景颜色。
        /// </summary>
        public Color BackgroundColor
        {
            get
            {
                if (this._pnlMain != null)
                    return this._pnlMain.BackColor;
                return Color.White;
            }
            set
            {
                if (this._pnlMain != null)
                    this._pnlMain.BackColor = value;
            }
        }

        /// <summary>
        /// 面板容器的停靠方式。
        /// </summary>
        public DockStyle PanelDock
        {
            get
            {
                if (this._pnlMain != null)
                    return this._pnlMain.Dock;
                else
                    return DockStyle.Fill;
            }
            set
            {
                if (this._pnlMain != null)
                this._pnlMain.Dock = value;
            }
        }

        public Panel MainPanel
        {
            get
            {
                return _pnlMain;
            }
        }
       
        /// <summary>
        /// 每行显示的图标数量
        /// </summary>
        public int LineNum { get; set; }    

        /// <summary>
        /// 创建布局。
        /// </summary>
        /// <param name="buttons">按钮列表</param>
        protected void CreateLayout(List<AccessButton> buttons)
        {
            this.SuspendLayout();
            _pnlMain = new System.Windows.Forms.Panel();
            _pnlMain.AutoScroll = true;
            _pnlMain.BackColor = System.Drawing.Color.Transparent;
            _pnlMain.Location = new System.Drawing.Point(15, 29);
            //_pnlMain.Location = new System.Drawing.Point(5, 19);
            _pnlMain.Name = "pnlMain";
            _pnlMain.Size = new System.Drawing.Size(229, 245);

            _pnlMain.Controls.Clear();
            _pnlMain.Controls.Add(picBackground);
            picBackground.Dock = DockStyle.Fill;
            int xinit = 40;// 30;
            int x = xinit;
            int y = 40;// 14;
            int xstep = 100;
            int ystep = 74;
            //每行只能显示2或3
            if (this.LineNum == 3)
            {
                xinit = 20;// 10;
                x = xinit;
                y = 40;// 14;
                xstep = 70;
                ystep = 74;
            }
            for (int i = 0; i < buttons.Count; i++)
            {
                ImageButton btn = new ImageButton();

                if (buttons[i].Image != null)
                {
                    btn.Image = (Image)buttons[i].Image.Clone();
                }
                else if (!string.IsNullOrEmpty(buttons[i].ImageFile))
                {
                    if (File.Exists(buttons[i].ImageFile))
                        btn.Image = new Bitmap(buttons[i].ImageFile);
                }
                btn.Tag = buttons[i].Tag;
                btn.Assembly = buttons[i].Assembly;
                btn.ClassName = buttons[i].ClassName;
                btn.Name = buttons[i].Name;
                btn.Top = y + (i / LineNum) * ystep;
                btn.Left = x + (i % LineNum) * xstep;

                btn.Width = 55;              
                if (buttons[i].Text.Length > 4)
                    btn.Height = 66;
                else
                    btn.Height = 55;
                btn.Text = buttons[i].Text;
                btn.Click += new EventHandler(buttons[i].Click);
                _pnlMain.Controls.Add(btn);
                btn.BringToFront();
            }
            _pnlMain.Refresh();
            this.Controls.Add(_pnlMain);
            _pnlMain.Dock = DockStyle.Fill;
            _pnlMain.BringToFront();
            _pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #region InitializeLayout
        /// <summary>
        /// 界面布局初始化。
        /// </summary>
        protected virtual void InitializeLayout()
        {
        }
        #endregion
      

        private void menuExit_Click(object sender, EventArgs e)
        {
            MenuExitClick();
        }

        protected virtual void MenuExitClick()
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void AccessForm_Load(object sender, EventArgs e)
        {
        }
    }

    /// <summary>
    /// AccessForm窗体使用的按钮。
    /// </summary>
    public class AccessButton
    {
        /// <summary>
        /// 按钮名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 显示文本
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public Image Image { get; set; }
        /// <summary>
        /// 图片文件
        /// </summary>
        public string ImageFile { get; set; }
        /// <summary>
        /// 按钮宽度
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 按钮高度
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// 程序集文件
        /// </summary>
        public string Assembly { get; set; }
        /// <summary>
        /// 类名称
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 包含有关控件的数据
        /// </summary>
        public object Tag { get; set; }
        /// <summary>
        /// 按钮点击事件
        /// </summary>
        public EventHandler<EventArgs> Click;
        /// <summary>
        /// 鼠标按下事件
        /// </summary>
        public MouseEventHandler MouseMove;


        public AccessButton(string text, string imageFile, EventHandler<EventArgs> click)
        {
            this.Text = text;
            this.ImageFile = imageFile;
            this.Click += new EventHandler<EventArgs>(click);
        }

        public AccessButton(string text, Image image, EventHandler<EventArgs> click)
        {
            this.Text = text;
            this.Image = image;
            this.Click += new EventHandler<EventArgs>(click);
        }

        public AccessButton(string imageFile, EventHandler<EventArgs> click)
        {
            this.ImageFile = imageFile;
            this.Click += new EventHandler<EventArgs>(click);
        }

        public AccessButton(Image image, EventHandler<EventArgs> click)
        {
            this.Image = image;
            this.Click += new EventHandler<EventArgs>(click);
        }
        public AccessButton(Image image, EventHandler<EventArgs> click, MouseEventHandler mousemove)
        {
            this.Image = image;
            this.MouseMove += new MouseEventHandler(mousemove);
            this.Click += new EventHandler<EventArgs>(click);            
        }
    }
}