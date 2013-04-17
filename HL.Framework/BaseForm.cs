using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HL.Framework
{
    /// <summary>
    /// 窗体基类
    /// </summary>
    public partial class BaseForm : Form
    {
        #region Vars
        public IFramework Framework;
        #endregion

        #region Propertys
        private bool _defineCursor;
        protected bool DefineCursor
        {
            get
            {
                return this._defineCursor;
            }
            set
            {
                this._defineCursor = value;
            }
        }
        #endregion

        #region Constructor
        public BaseForm()
        {
            if (Cursor.Current == Cursors.Default)
            {
                this._defineCursor = true;
                Cursor.Current = Cursors.WaitCursor;
            }
            InitializeComponent();

        }
        #endregion

        #region OnLoad
        private void BaseForm_Load(object sender, EventArgs e)
        {
            if (this._defineCursor)
            {
                Cursor.Current = Cursors.Default;
            }
        }
        #endregion      

        #region Progress Status
        public void ShowStatus(string text)
        {
            this.txtTs.Text = "\r\n" + "   " + text + "...";
            this.txtTs.Visible = true;
            this.txtTs.BringToFront();
            this.txtTs.Refresh();
        }
      
        public void HiddenStatus()
        {
            this.txtTs.Visible = false;
        }
        #endregion
    }
}