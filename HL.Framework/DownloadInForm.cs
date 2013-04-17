using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using BizLayer;

namespace HL.Framework
{
    public partial class DownloadInForm : Form
    {
        private ProgressBar _currentProgressBar;
        private Label _currentLabel;
        private Button _currentButton;
        private long _typeCode;
        public TaskType TaskType = TaskType.OuterOrderCheckIn;
        public DownloadInForm()
        {
            InitializeComponent();
        }

        private void btnSyncBase_Click(object sender, EventArgs e)
        {
            this._currentProgressBar = this.progressSyncBase;
            this._currentLabel = this.lblDownData;
            this._currentButton = this.btnSyncBase;
            this.sbError = new StringBuilder();
            this.btnBack.Enabled = false;
            this.btnSyncBase.Enabled = false;
            new Thread(new ThreadStart(this.DoSyncBase)).Start();
        }

        private void DoSyncBase()
        {
            ShowProgress method = new ShowProgress(this.ShowResult);
            DateTime dtBegin, dtEnd;
            //if (this.rbtnRQ.Checked)
            //{
            //    dtBegin = this.dateTimeBegin.Value;
            //    dtEnd = this.dateTimeEnd.Value;
            //}
            //else
            //{
            //    //dtBegin=本地SQLite库查询最后下载日期
            dtBegin = new DateTime(1900, 1, 1);
            dtEnd = new DateTime(2100, 1, 1);
            //}
            if(TaskType == TaskType.OuterOrderCheckIn)
            DownloadService.DownOuterOrderCheckIn(Management.GetSingleton().WarehouseNo, dtBegin, dtEnd, this, method);
            else  if(TaskType == TaskType.FactoryOrderCheckIn)
                DownloadService.DownFactoryOrderCheckIn(Management.GetSingleton().WarehouseNo, dtBegin, dtEnd, this, method);
            else if (TaskType == TaskType.FactoryOrderCheckOut)
                DownloadService.DownFactoryOrderCheckOut(Management.GetSingleton().WarehouseNo, dtBegin, dtEnd, this, method);

        }

        StringBuilder sbError = new StringBuilder();
        private void ShowResult(int progress, string data)
        {
            if (progress == -1)//总体异常出现，任务结束。
            {
                this.btnBack.Enabled = true;
                this._currentLabel.Text = data;
                this._currentButton.Enabled = true;
            }
            else if (progress == -2)//局部更新错误，记录错误信息。
            {
                sbError.Append(data + "\r\n");
            }
            else if (progress > 0)
            {
                this._currentProgressBar.Value = progress;
                this._currentLabel.Text = data;
            }
            if (progress == -3)//任务完成
            {
                this._currentProgressBar.Value = 100;
                this.btnBack.Enabled = true;
                this._currentButton.Enabled = true;
                if (sbError.Length > 0)
                    this._currentLabel.Text += "\r\n但存在以下错误:\r\n" + sbError.ToString();
                else
                    this._currentLabel.Text = data;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbtnRQ_CheckedChanged(object sender, EventArgs e)
        {
            this.pnlDate.Enabled = this.rbtnRQ.Checked;
        }
    }
}