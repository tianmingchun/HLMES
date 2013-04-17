namespace HL.UI.CheckOut
{
    partial class FactoryOrderHistoryForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblMaterialName = new System.Windows.Forms.Label();
            this.lblTodayCount = new System.Windows.Forms.Label();
            this.LsvTodayDelivery = new System.Windows.Forms.ListView();
            this.columnHeader15 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lblMaterialName2 = new System.Windows.Forms.Label();
            this.btnBatch = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.lblTaskCount = new System.Windows.Forms.Label();
            this.lsvTasks = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader13 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
            this.btnSelectNotAll = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(234, 236);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblMaterialName);
            this.tabPage1.Controls.Add(this.lblTodayCount);
            this.tabPage1.Controls.Add(this.LsvTodayDelivery);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(226, 207);
            this.tabPage1.Text = "当日提交记录";
            // 
            // lblMaterialName
            // 
            this.lblMaterialName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.lblMaterialName.Location = new System.Drawing.Point(80, 187);
            this.lblMaterialName.Name = "lblMaterialName";
            this.lblMaterialName.Size = new System.Drawing.Size(146, 21);
            // 
            // lblTodayCount
            // 
            this.lblTodayCount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.lblTodayCount.Location = new System.Drawing.Point(3, 187);
            this.lblTodayCount.Name = "lblTodayCount";
            this.lblTodayCount.Size = new System.Drawing.Size(68, 20);
            this.lblTodayCount.Text = "总数:";
            // 
            // LsvTodayDelivery
            // 
            this.LsvTodayDelivery.Columns.Add(this.columnHeader15);
            this.LsvTodayDelivery.Columns.Add(this.columnHeader2);
            this.LsvTodayDelivery.Columns.Add(this.columnHeader3);
            this.LsvTodayDelivery.Columns.Add(this.columnHeader12);
            this.LsvTodayDelivery.Columns.Add(this.columnHeader6);
            this.LsvTodayDelivery.Columns.Add(this.columnHeader5);
            this.LsvTodayDelivery.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.LsvTodayDelivery.FullRowSelect = true;
            this.LsvTodayDelivery.Location = new System.Drawing.Point(0, 3);
            this.LsvTodayDelivery.Name = "LsvTodayDelivery";
            this.LsvTodayDelivery.Size = new System.Drawing.Size(230, 181);
            this.LsvTodayDelivery.TabIndex = 84;
            this.LsvTodayDelivery.View = System.Windows.Forms.View.Details;
            this.LsvTodayDelivery.SelectedIndexChanged += new System.EventHandler(this.LsvTodayDelivery_SelectedIndexChanged);
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "订单号";
            this.columnHeader15.Width = 80;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "物料";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "发货数";
            this.columnHeader3.Width = 50;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "行项目号";
            this.columnHeader12.Width = 70;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "收货时间";
            this.columnHeader6.Width = 70;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "状态";
            this.columnHeader5.Width = 300;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnDelete);
            this.tabPage2.Controls.Add(this.lblMaterialName2);
            this.tabPage2.Controls.Add(this.btnBatch);
            this.tabPage2.Controls.Add(this.btnSubmit);
            this.tabPage2.Controls.Add(this.lblTaskCount);
            this.tabPage2.Controls.Add(this.lsvTasks);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(226, 207);
            this.tabPage2.Text = "SAP未收货记录";
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.btnDelete.Location = new System.Drawing.Point(3, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(55, 23);
            this.btnDelete.TabIndex = 84;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lblMaterialName2
            // 
            this.lblMaterialName2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.lblMaterialName2.Location = new System.Drawing.Point(77, 187);
            this.lblMaterialName2.Name = "lblMaterialName2";
            this.lblMaterialName2.Size = new System.Drawing.Size(146, 21);
            // 
            // btnBatch
            // 
            this.btnBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.btnBatch.Location = new System.Drawing.Point(173, 3);
            this.btnBatch.Name = "btnBatch";
            this.btnBatch.Size = new System.Drawing.Size(38, 23);
            this.btnBatch.TabIndex = 91;
            this.btnBatch.Text = "批";
            this.btnBatch.Click += new System.EventHandler(this.btnBatch_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.btnSubmit.Location = new System.Drawing.Point(101, 3);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(64, 23);
            this.btnSubmit.TabIndex = 90;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // lblTaskCount
            // 
            this.lblTaskCount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.lblTaskCount.Location = new System.Drawing.Point(3, 187);
            this.lblTaskCount.Name = "lblTaskCount";
            this.lblTaskCount.Size = new System.Drawing.Size(68, 20);
            this.lblTaskCount.Text = "总数:";
            // 
            // lsvTasks
            // 
            this.lsvTasks.CheckBoxes = true;
            this.lsvTasks.Columns.Add(this.columnHeader1);
            this.lsvTasks.Columns.Add(this.columnHeader9);
            this.lsvTasks.Columns.Add(this.columnHeader4);
            this.lsvTasks.Columns.Add(this.columnHeader7);
            this.lsvTasks.Columns.Add(this.columnHeader8);
            this.lsvTasks.Columns.Add(this.columnHeader13);
            this.lsvTasks.Columns.Add(this.columnHeader10);
            this.lsvTasks.Columns.Add(this.columnHeader11);
            this.lsvTasks.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.lsvTasks.FullRowSelect = true;
            this.lsvTasks.Location = new System.Drawing.Point(0, 30);
            this.lsvTasks.Name = "lsvTasks";
            this.lsvTasks.Size = new System.Drawing.Size(226, 154);
            this.lsvTasks.TabIndex = 84;
            this.lsvTasks.View = System.Windows.Forms.View.Details;
            this.lsvTasks.SelectedIndexChanged += new System.EventHandler(this.lsvTasks_SelectedIndexChanged);
            this.lsvTasks.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lsvTasks_ItemCheck);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = " ";
            this.columnHeader1.Width = 25;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "订单号";
            this.columnHeader9.Width = 85;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "物料";
            this.columnHeader4.Width = 100;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "发货数";
            this.columnHeader7.Width = 50;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "订单数";
            this.columnHeader8.Width = 50;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "行项目号";
            this.columnHeader13.Width = 70;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "单位";
            this.columnHeader10.Width = 40;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "状态";
            this.columnHeader11.Width = 300;
            // 
            // btnSelectNotAll
            // 
            this.btnSelectNotAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.btnSelectNotAll.Location = new System.Drawing.Point(50, 241);
            this.btnSelectNotAll.Name = "btnSelectNotAll";
            this.btnSelectNotAll.Size = new System.Drawing.Size(48, 23);
            this.btnSelectNotAll.TabIndex = 93;
            this.btnSelectNotAll.Text = "全不选";
            this.btnSelectNotAll.Click += new System.EventHandler(this.btnSelectNotAll_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.btnSelectAll.Location = new System.Drawing.Point(3, 241);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(41, 23);
            this.btnSelectAll.TabIndex = 92;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.btnExit.Location = new System.Drawing.Point(108, 241);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(55, 23);
            this.btnExit.TabIndex = 83;
            this.btnExit.Text = "返回";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // FactoryOrderHistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.ControlBox = false;
            this.Controls.Add(this.btnSelectNotAll);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.tabControl1);
            this.Name = "FactoryOrderHistoryForm";
            this.Text = "工厂间采购订单提交记录跟踪";
            this.Load += new System.EventHandler(this.PurchaseOrderHistoryForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ListView LsvTodayDelivery;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Label lblTodayCount;
        private System.Windows.Forms.Label lblTaskCount;
        private System.Windows.Forms.ListView lsvTasks;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.Button btnBatch;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnSelectNotAll;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.Label lblMaterialName;
        private System.Windows.Forms.Label lblMaterialName2;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
    }
}