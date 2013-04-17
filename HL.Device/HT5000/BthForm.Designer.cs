namespace HL.Device.HT5000
{
    partial class BthForm
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pin = new System.Windows.Forms.TextBox();
            this.bthDisconn = new System.Windows.Forms.Button();
            this.bthConn = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.Add(this.columnHeader1);
            this.listView1.Columns.Add(this.columnHeader2);
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(3, 3);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(234, 116);
            this.listView1.TabIndex = 4;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 72;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Address";
            this.columnHeader2.Width = 124;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(74, 125);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(72, 20);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "Search";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(17, 167);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 20);
            this.label2.Text = "Pin:";
            // 
            // pin
            // 
            this.pin.Location = new System.Drawing.Point(53, 165);
            this.pin.Name = "pin";
            this.pin.Size = new System.Drawing.Size(132, 23);
            this.pin.TabIndex = 9;
            // 
            // bthDisconn
            // 
            this.bthDisconn.Enabled = false;
            this.bthDisconn.Location = new System.Drawing.Point(105, 202);
            this.bthDisconn.Name = "bthDisconn";
            this.bthDisconn.Size = new System.Drawing.Size(80, 20);
            this.bthDisconn.TabIndex = 7;
            this.bthDisconn.Text = "Disconnect";
            // 
            // bthConn
            // 
            this.bthConn.Location = new System.Drawing.Point(15, 202);
            this.bthConn.Name = "bthConn";
            this.bthConn.Size = new System.Drawing.Size(72, 20);
            this.bthConn.TabIndex = 8;
            this.bthConn.Text = "Connect";
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular);
            this.btnClose.Location = new System.Drawing.Point(86, 244);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(72, 20);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "返回";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // BthForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.ControlBox = false;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pin);
            this.Controls.Add(this.bthDisconn);
            this.Controls.Add(this.bthConn);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.listView1);
            this.Name = "BthForm";
            this.Text = "蓝牙管理器";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox pin;
        private System.Windows.Forms.Button bthDisconn;
        private System.Windows.Forms.Button bthConn;
        private System.Windows.Forms.Button btnClose;
    }
}