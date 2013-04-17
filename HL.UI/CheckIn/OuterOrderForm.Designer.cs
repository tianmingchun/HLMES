namespace HL.UI.CheckIn
{
    partial class OuterOrderForm
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
            this.lblDate = new System.Windows.Forms.Label();
            this.lblSupplyID = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSupplyname = new System.Windows.Forms.Label();
            this.btnUpdateQty = new System.Windows.Forms.Button();
            this.lsvTasks = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.txtOrderID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBatch = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnSelectNotAll = new System.Windows.Forms.Button();
            this.lblMaterialName = new System.Windows.Forms.Label();
            this.lblProid = new System.Windows.Forms.Label();
            this.btnChooseOrderID = new System.Windows.Forms.Button();
            this.btnHistory = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblDate
            // 
            this.lblDate.Location = new System.Drawing.Point(202, 7);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(23, 20);
            this.lblDate.Visible = false;
            // 
            // lblSupplyID
            // 
            this.lblSupplyID.Location = new System.Drawing.Point(202, 3);
            this.lblSupplyID.Name = "lblSupplyID";
            this.lblSupplyID.Size = new System.Drawing.Size(34, 20);
            this.lblSupplyID.Visible = false;
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(74, 34);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(103, 23);
            this.txtQty.TabIndex = 85;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(5, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 18);
            this.label2.Text = "收货数：";
            // 
            // lblSupplyname
            // 
            this.lblSupplyname.Location = new System.Drawing.Point(202, 11);
            this.lblSupplyname.Name = "lblSupplyname";
            this.lblSupplyname.Size = new System.Drawing.Size(34, 20);
            this.lblSupplyname.Visible = false;
            // 
            // btnUpdateQty
            // 
            this.btnUpdateQty.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.btnUpdateQty.Location = new System.Drawing.Point(184, 34);
            this.btnUpdateQty.Name = "btnUpdateQty";
            this.btnUpdateQty.Size = new System.Drawing.Size(46, 23);
            this.btnUpdateQty.TabIndex = 84;
            this.btnUpdateQty.Text = "确定";
            this.btnUpdateQty.Click += new System.EventHandler(this.btnUpdateQty_Click);
            // 
            // lsvTasks
            // 
            this.lsvTasks.CheckBoxes = true;
            this.lsvTasks.Columns.Add(this.columnHeader1);
            this.lsvTasks.Columns.Add(this.columnHeader2);
            this.lsvTasks.Columns.Add(this.columnHeader3);
            this.lsvTasks.Columns.Add(this.columnHeader4);
            this.lsvTasks.Columns.Add(this.columnHeader8);
            this.lsvTasks.Columns.Add(this.columnHeader5);
            this.lsvTasks.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.lsvTasks.FullRowSelect = true;
            this.lsvTasks.Location = new System.Drawing.Point(1, 85);
            this.lsvTasks.Name = "lsvTasks";
            this.lsvTasks.Size = new System.Drawing.Size(237, 133);
            this.lsvTasks.TabIndex = 83;
            this.lsvTasks.View = System.Windows.Forms.View.Details;
            this.lsvTasks.SelectedIndexChanged += new System.EventHandler(this.lsvTasks_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = " ";
            this.columnHeader1.Width = 25;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "物料";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "收货数";
            this.columnHeader3.Width = 50;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "任务数";
            this.columnHeader4.Width = 50;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "已收数";
            this.columnHeader8.Width = 50;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "单位";
            this.columnHeader5.Width = 40;
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.btnExit.Location = new System.Drawing.Point(140, 60);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(55, 23);
            this.btnExit.TabIndex = 82;
            this.btnExit.Text = "返回";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.btnSubmit.Location = new System.Drawing.Point(71, 60);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(55, 23);
            this.btnSubmit.TabIndex = 81;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.btnQuery.Location = new System.Drawing.Point(2, 60);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(55, 23);
            this.btnQuery.TabIndex = 80;
            this.btnQuery.Text = "检索";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtOrderID
            // 
            this.txtOrderID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtOrderID.Location = new System.Drawing.Point(74, 6);
            this.txtOrderID.Name = "txtOrderID";
            this.txtOrderID.Size = new System.Drawing.Size(103, 21);
            this.txtOrderID.TabIndex = 79;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(5, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 20);
            this.label1.Text = "采购订单：";
            // 
            // btnBatch
            // 
            this.btnBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.btnBatch.Location = new System.Drawing.Point(205, 60);
            this.btnBatch.Name = "btnBatch";
            this.btnBatch.Size = new System.Drawing.Size(30, 23);
            this.btnBatch.TabIndex = 89;
            this.btnBatch.Text = "批";
            this.btnBatch.Click += new System.EventHandler(this.btnBatch_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.btnSelectAll.Location = new System.Drawing.Point(4, 219);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(48, 23);
            this.btnSelectAll.TabIndex = 90;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnSelectNotAll
            // 
            this.btnSelectNotAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.btnSelectNotAll.Location = new System.Drawing.Point(56, 219);
            this.btnSelectNotAll.Name = "btnSelectNotAll";
            this.btnSelectNotAll.Size = new System.Drawing.Size(48, 23);
            this.btnSelectNotAll.TabIndex = 91;
            this.btnSelectNotAll.Text = "全不选";
            this.btnSelectNotAll.Click += new System.EventHandler(this.btnSelectNotAll_Click);
            // 
            // lblMaterialName
            // 
            this.lblMaterialName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.lblMaterialName.Location = new System.Drawing.Point(2, 245);
            this.lblMaterialName.Name = "lblMaterialName";
            this.lblMaterialName.Size = new System.Drawing.Size(234, 36);
            // 
            // lblProid
            // 
            this.lblProid.Location = new System.Drawing.Point(197, 6);
            this.lblProid.Name = "lblProid";
            this.lblProid.Size = new System.Drawing.Size(37, 20);
            this.lblProid.Visible = false;
            // 
            // btnChooseOrderID
            // 
            this.btnChooseOrderID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.btnChooseOrderID.Location = new System.Drawing.Point(184, 5);
            this.btnChooseOrderID.Name = "btnChooseOrderID";
            this.btnChooseOrderID.Size = new System.Drawing.Size(46, 23);
            this.btnChooseOrderID.TabIndex = 97;
            this.btnChooseOrderID.Text = "...";
            this.btnChooseOrderID.Click += new System.EventHandler(this.btnChooseOrderID_Click);
            // 
            // btnHistory
            // 
            this.btnHistory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.btnHistory.Location = new System.Drawing.Point(112, 219);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(48, 23);
            this.btnHistory.TabIndex = 98;
            this.btnHistory.Text = "跟踪";
            this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.btnDownload.Location = new System.Drawing.Point(169, 219);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(64, 23);
            this.btnDownload.TabIndex = 106;
            this.btnDownload.Text = "数据下载";
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // OuterOrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.ControlBox = false;
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.btnHistory);
            this.Controls.Add(this.btnChooseOrderID);
            this.Controls.Add(this.lblProid);
            this.Controls.Add(this.lblMaterialName);
            this.Controls.Add(this.btnSelectNotAll);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.btnBatch);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblSupplyID);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblSupplyname);
            this.Controls.Add(this.btnUpdateQty);
            this.Controls.Add(this.lsvTasks);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.txtOrderID);
            this.Controls.Add(this.label1);
            this.Name = "OuterOrderForm";
            this.Text = "采购订单收货";
            this.Deactivate += new System.EventHandler(this.OutsideOrderForm_Deactivate);
            this.Load += new System.EventHandler(this.OutsideOrderForm_Load);
            this.Activated += new System.EventHandler(this.OutsideOrderForm_Activated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.PurchaseOrderForm_Closing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OutsideOrderForm_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblSupplyID;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSupplyname;
        private System.Windows.Forms.Button btnUpdateQty;
        private System.Windows.Forms.ListView lsvTasks;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.TextBox txtOrderID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBatch;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnSelectNotAll;
        private System.Windows.Forms.Label lblMaterialName;
        private System.Windows.Forms.Label lblProid;
        private System.Windows.Forms.Button btnChooseOrderID;
        private System.Windows.Forms.Button btnHistory;
        private System.Windows.Forms.Button btnDownload;
    }
}