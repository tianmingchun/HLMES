namespace HL.Framework
{
    partial class DownloadInForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblDownData = new System.Windows.Forms.Label();
            this.btnSyncBase = new System.Windows.Forms.Button();
            this.progressSyncBase = new System.Windows.Forms.ProgressBar();
            this.btnBack = new System.Windows.Forms.Button();
            this.dateTimeBegin = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimeEnd = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlDate = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbtnRQ = new System.Windows.Forms.RadioButton();
            this.rbtnZL = new System.Windows.Forms.RadioButton();
            this.pnlDate.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDownData
            // 
            this.lblDownData.BackColor = System.Drawing.Color.Silver;
            this.lblDownData.ForeColor = System.Drawing.Color.Blue;
            this.lblDownData.Location = new System.Drawing.Point(13, 167);
            this.lblDownData.Name = "lblDownData";
            this.lblDownData.Size = new System.Drawing.Size(215, 66);
            // 
            // btnSyncBase
            // 
            this.btnSyncBase.Location = new System.Drawing.Point(143, 110);
            this.btnSyncBase.Name = "btnSyncBase";
            this.btnSyncBase.Size = new System.Drawing.Size(85, 30);
            this.btnSyncBase.TabIndex = 13;
            this.btnSyncBase.Text = "下载";
            this.btnSyncBase.Click += new System.EventHandler(this.btnSyncBase_Click);
            // 
            // progressSyncBase
            // 
            this.progressSyncBase.Location = new System.Drawing.Point(13, 143);
            this.progressSyncBase.Name = "progressSyncBase";
            this.progressSyncBase.Size = new System.Drawing.Size(215, 20);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(72, 238);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(85, 30);
            this.btnBack.TabIndex = 17;
            this.btnBack.Text = "返回";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // dateTimeBegin
            // 
            this.dateTimeBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeBegin.Location = new System.Drawing.Point(86, 8);
            this.dateTimeBegin.Name = "dateTimeBegin";
            this.dateTimeBegin.Size = new System.Drawing.Size(139, 24);
            this.dateTimeBegin.TabIndex = 74;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(9, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 20);
            this.label1.Text = "开始日期:";
            // 
            // dateTimeEnd
            // 
            this.dateTimeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeEnd.Location = new System.Drawing.Point(86, 38);
            this.dateTimeEnd.Name = "dateTimeEnd";
            this.dateTimeEnd.Size = new System.Drawing.Size(139, 24);
            this.dateTimeEnd.TabIndex = 77;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(9, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 20);
            this.label2.Text = "结束日期:";
            // 
            // pnlDate
            // 
            this.pnlDate.Controls.Add(this.dateTimeBegin);
            this.pnlDate.Controls.Add(this.dateTimeEnd);
            this.pnlDate.Controls.Add(this.label1);
            this.pnlDate.Controls.Add(this.label2);
            this.pnlDate.Enabled = false;
            this.pnlDate.Location = new System.Drawing.Point(3, 39);
            this.pnlDate.Name = "pnlDate";
            this.pnlDate.Size = new System.Drawing.Size(234, 69);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rbtnRQ);
            this.panel2.Controls.Add(this.rbtnZL);
            this.panel2.Location = new System.Drawing.Point(3, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(234, 32);
            // 
            // rbtnRQ
            // 
            this.rbtnRQ.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.rbtnRQ.Location = new System.Drawing.Point(123, 8);
            this.rbtnRQ.Name = "rbtnRQ";
            this.rbtnRQ.Size = new System.Drawing.Size(106, 20);
            this.rbtnRQ.TabIndex = 1;
            this.rbtnRQ.TabStop = false;
            this.rbtnRQ.Text = "指定日期";
            this.rbtnRQ.CheckedChanged += new System.EventHandler(this.rbtnRQ_CheckedChanged);
            // 
            // rbtnZL
            // 
            this.rbtnZL.Checked = true;
            this.rbtnZL.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.rbtnZL.Location = new System.Drawing.Point(20, 8);
            this.rbtnZL.Name = "rbtnZL";
            this.rbtnZL.Size = new System.Drawing.Size(97, 20);
            this.rbtnZL.TabIndex = 0;
            this.rbtnZL.Text = "全部数据";
            // 
            // DownloadInForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlDate);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.lblDownData);
            this.Controls.Add(this.btnSyncBase);
            this.Controls.Add(this.progressSyncBase);
            this.Name = "DownloadInForm";
            this.Text = "收货数据下载";
            this.pnlDate.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblDownData;
        private System.Windows.Forms.Button btnSyncBase;
        private System.Windows.Forms.ProgressBar progressSyncBase;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.DateTimePicker dateTimeBegin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimeEnd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlDate;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rbtnRQ;
        private System.Windows.Forms.RadioButton rbtnZL;
    }
}