namespace 스마트팩토리.Popup
{
    partial class pop씨지엠주문서검색
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pop씨지엠주문서검색));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSrch = new System.Windows.Forms.Button();
            this.end_date = new System.Windows.Forms.DateTimePicker();
            this.start_date = new System.Windows.Forms.DateTimePicker();
            this.btnExit = new System.Windows.Forms.Button();
            this.GridRecord = new System.Windows.Forms.DataGridView();
            this.PLAN_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PLAN_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DELIVERY_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CUST_NM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PLAN_PRODUCTS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COMMENT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WORK_YN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CUST_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TAX_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TAX_NM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblStatus = new 스마트팩토리.Controls.conLabel();
            this.cmbPage = new 스마트팩토리.Controls.conComboBox();
            this.lblPage = new System.Windows.Forms.Label();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridRecord)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btnSrch);
            this.panel2.Controls.Add(this.end_date);
            this.panel2.Controls.Add(this.start_date);
            this.panel2.Controls.Add(this.btnExit);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1103, 33);
            this.panel2.TabIndex = 101;
            // 
            // btnSrch
            // 
            this.btnSrch.BackColor = System.Drawing.Color.Transparent;
            this.btnSrch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSrch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSrch.FlatAppearance.BorderSize = 0;
            this.btnSrch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.SkyBlue;
            this.btnSrch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSrch.Image = ((System.Drawing.Image)(resources.GetObject("btnSrch.Image")));
            this.btnSrch.Location = new System.Drawing.Point(539, 2);
            this.btnSrch.Name = "btnSrch";
            this.btnSrch.Size = new System.Drawing.Size(33, 30);
            this.btnSrch.TabIndex = 345;
            this.btnSrch.Tag = "검색";
            this.btnSrch.UseVisualStyleBackColor = false;
            this.btnSrch.Click += new System.EventHandler(this.btnSrch_Click);
            // 
            // end_date
            // 
            this.end_date.Checked = false;
            this.end_date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.end_date.Location = new System.Drawing.Point(378, 6);
            this.end_date.Name = "end_date";
            this.end_date.Size = new System.Drawing.Size(156, 21);
            this.end_date.TabIndex = 347;
            this.end_date.Value = new System.DateTime(2018, 10, 8, 0, 0, 0, 0);
            // 
            // start_date
            // 
            this.start_date.Checked = false;
            this.start_date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.start_date.Location = new System.Drawing.Point(219, 6);
            this.start_date.Name = "start_date";
            this.start_date.Size = new System.Drawing.Size(154, 21);
            this.start_date.TabIndex = 346;
            this.start_date.Value = new System.DateTime(2018, 10, 8, 0, 0, 0, 0);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.ForeColor = System.Drawing.Color.DodgerBlue;
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnExit.Location = new System.Drawing.Point(1026, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(65, 29);
            this.btnExit.TabIndex = 2;
            this.btnExit.Tag = "종료";
            this.btnExit.Text = "닫기";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // GridRecord
            // 
            this.GridRecord.AllowUserToAddRows = false;
            this.GridRecord.AllowUserToDeleteRows = false;
            this.GridRecord.AllowUserToResizeColumns = false;
            this.GridRecord.AllowUserToResizeRows = false;
            this.GridRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GridRecord.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.GridRecord.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridRecord.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GridRecord.ColumnHeadersHeight = 40;
            this.GridRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.GridRecord.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PLAN_DATE,
            this.PLAN_CD,
            this.DELIVERY_DATE,
            this.CUST_NM,
            this.PLAN_PRODUCTS,
            this.COMMENT,
            this.WORK_YN,
            this.CUST_CD,
            this.TAX_CD,
            this.TAX_NM});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridRecord.DefaultCellStyle = dataGridViewCellStyle2;
            this.GridRecord.EnableHeadersVisualStyles = false;
            this.GridRecord.GridColor = System.Drawing.Color.PowderBlue;
            this.GridRecord.Location = new System.Drawing.Point(0, 32);
            this.GridRecord.Name = "GridRecord";
            this.GridRecord.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridRecord.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.GridRecord.RowHeadersVisible = false;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.LightCyan;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.GridRecord.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.GridRecord.RowTemplate.Height = 23;
            this.GridRecord.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridRecord.Size = new System.Drawing.Size(1103, 378);
            this.GridRecord.TabIndex = 112;
            this.GridRecord.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridRecord_CellDoubleClick);
            // 
            // PLAN_DATE
            // 
            this.PLAN_DATE.HeaderText = "주문일자";
            this.PLAN_DATE.Name = "PLAN_DATE";
            this.PLAN_DATE.ReadOnly = true;
            this.PLAN_DATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PLAN_DATE.Width = 130;
            // 
            // PLAN_CD
            // 
            this.PLAN_CD.HeaderText = "주문번호";
            this.PLAN_CD.Name = "PLAN_CD";
            this.PLAN_CD.ReadOnly = true;
            this.PLAN_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DELIVERY_DATE
            // 
            this.DELIVERY_DATE.HeaderText = "납품요구일";
            this.DELIVERY_DATE.Name = "DELIVERY_DATE";
            this.DELIVERY_DATE.ReadOnly = true;
            this.DELIVERY_DATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DELIVERY_DATE.Width = 130;
            // 
            // CUST_NM
            // 
            this.CUST_NM.HeaderText = "거래처";
            this.CUST_NM.Name = "CUST_NM";
            this.CUST_NM.ReadOnly = true;
            this.CUST_NM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CUST_NM.Width = 200;
            // 
            // PLAN_PRODUCTS
            // 
            this.PLAN_PRODUCTS.HeaderText = "주문 제품 수";
            this.PLAN_PRODUCTS.Name = "PLAN_PRODUCTS";
            this.PLAN_PRODUCTS.ReadOnly = true;
            this.PLAN_PRODUCTS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PLAN_PRODUCTS.Width = 140;
            // 
            // COMMENT
            // 
            this.COMMENT.HeaderText = "지시사항";
            this.COMMENT.Name = "COMMENT";
            this.COMMENT.ReadOnly = true;
            this.COMMENT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.COMMENT.Width = 250;
            // 
            // WORK_YN
            // 
            this.WORK_YN.HeaderText = "진행상태";
            this.WORK_YN.Name = "WORK_YN";
            this.WORK_YN.ReadOnly = true;
            this.WORK_YN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.WORK_YN.Width = 120;
            // 
            // CUST_CD
            // 
            this.CUST_CD.HeaderText = "거래처코드";
            this.CUST_CD.Name = "CUST_CD";
            this.CUST_CD.ReadOnly = true;
            this.CUST_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CUST_CD.Visible = false;
            // 
            // TAX_CD
            // 
            this.TAX_CD.HeaderText = "부가세코드";
            this.TAX_CD.Name = "TAX_CD";
            this.TAX_CD.ReadOnly = true;
            this.TAX_CD.Visible = false;
            // 
            // TAX_NM
            // 
            this.TAX_NM.HeaderText = "부가세명칭";
            this.TAX_NM.Name = "TAX_NM";
            this.TAX_NM.ReadOnly = true;
            this.TAX_NM.Visible = false;
            // 
            // lblStatus
            // 
            this.lblStatus._BorderColor = System.Drawing.Color.LightGray;
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblStatus.Location = new System.Drawing.Point(941, 416);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(94, 22);
            this.lblStatus.TabIndex = 308;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbPage
            // 
            this.cmbPage._BorderColor = System.Drawing.Color.LightSteelBlue;
            this.cmbPage._FocusedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbPage.FormattingEnabled = true;
            this.cmbPage.Location = new System.Drawing.Point(795, 418);
            this.cmbPage.Name = "cmbPage";
            this.cmbPage.Size = new System.Drawing.Size(77, 20);
            this.cmbPage.TabIndex = 302;
            // 
            // lblPage
            // 
            this.lblPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPage.AutoSize = true;
            this.lblPage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lblPage.Location = new System.Drawing.Point(720, 423);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(69, 12);
            this.lblPage.TabIndex = 307;
            this.lblPage.Text = "페이지 이동";
            // 
            // btnLast
            // 
            this.btnLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnLast.Location = new System.Drawing.Point(1070, 416);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(28, 22);
            this.btnLast.TabIndex = 306;
            this.btnLast.Text = ">|";
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnNext.Location = new System.Drawing.Point(1041, 416);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(29, 22);
            this.btnNext.TabIndex = 305;
            this.btnNext.Text = ">";
            // 
            // btnPrevious
            // 
            this.btnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnPrevious.Location = new System.Drawing.Point(906, 416);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(29, 22);
            this.btnPrevious.TabIndex = 304;
            this.btnPrevious.Text = "<";
            // 
            // btnFirst
            // 
            this.btnFirst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnFirst.Location = new System.Drawing.Point(878, 416);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(28, 22);
            this.btnFirst.TabIndex = 303;
            this.btnFirst.Text = "|<";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.CadetBlue;
            this.label1.Location = new System.Drawing.Point(12, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 29);
            this.label1.TabIndex = 348;
            this.label1.Text = "주문서 검색";
            // 
            // pop씨지엠주문서검색
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1103, 442);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.cmbPage);
            this.Controls.Add(this.lblPage);
            this.Controls.Add(this.btnLast);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnFirst);
            this.Controls.Add(this.GridRecord);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "pop씨지엠주문서검색";
            this.ShowIcon = false;
            this.Text = "주문서 검색";
            this.Load += new System.EventHandler(this.pop씨지엠주문서검색_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridRecord)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSrch;
        private System.Windows.Forms.DateTimePicker end_date;
        private System.Windows.Forms.DateTimePicker start_date;
        private System.Windows.Forms.DataGridView GridRecord;
        private Controls.conLabel lblStatus;
        private Controls.conComboBox cmbPage;
        private System.Windows.Forms.Label lblPage;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.DataGridViewTextBoxColumn PLAN_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn PLAN_CD;
        private System.Windows.Forms.DataGridViewTextBoxColumn DELIVERY_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CUST_NM;
        private System.Windows.Forms.DataGridViewTextBoxColumn PLAN_PRODUCTS;
        private System.Windows.Forms.DataGridViewTextBoxColumn COMMENT;
        private System.Windows.Forms.DataGridViewTextBoxColumn WORK_YN;
        private System.Windows.Forms.DataGridViewTextBoxColumn CUST_CD;
        private System.Windows.Forms.DataGridViewTextBoxColumn TAX_CD;
        private System.Windows.Forms.DataGridViewTextBoxColumn TAX_NM;
        private System.Windows.Forms.Label label1;
    }
}