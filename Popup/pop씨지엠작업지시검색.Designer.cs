namespace 스마트팩토리.Popup
{
    partial class pop씨지엠작업지시검색
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pop씨지엠작업지시검색));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSrch = new System.Windows.Forms.Button();
            this.end_date = new System.Windows.Forms.DateTimePicker();
            this.start_date = new System.Windows.Forms.DateTimePicker();
            this.btnExit = new System.Windows.Forms.Button();
            this.GridRecord = new System.Windows.Forms.DataGridView();
            this.W_INST_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.W_INST_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LOT_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RAW_MAT_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RAW_MAT_NM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INST_AMT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DELIVERY_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INST_NOTICE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COMPLETE_YN = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.panel2.Size = new System.Drawing.Size(1254, 33);
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
            this.btnExit.Location = new System.Drawing.Point(1177, 3);
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
            this.GridRecord.AllowUserToOrderColumns = true;
            this.GridRecord.AllowUserToResizeRows = false;
            this.GridRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GridRecord.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.GridRecord.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridRecord.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.GridRecord.ColumnHeadersHeight = 40;
            this.GridRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.GridRecord.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.W_INST_DATE,
            this.W_INST_CD,
            this.LOT_NO,
            this.RAW_MAT_CD,
            this.RAW_MAT_NM,
            this.INST_AMT,
            this.DELIVERY_DATE,
            this.INST_NOTICE,
            this.COMPLETE_YN});
            this.GridRecord.EnableHeadersVisualStyles = false;
            this.GridRecord.GridColor = System.Drawing.Color.PowderBlue;
            this.GridRecord.Location = new System.Drawing.Point(0, 32);
            this.GridRecord.Name = "GridRecord";
            this.GridRecord.ReadOnly = true;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridRecord.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.GridRecord.RowHeadersVisible = false;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.LightCyan;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.GridRecord.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.GridRecord.RowTemplate.Height = 23;
            this.GridRecord.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridRecord.Size = new System.Drawing.Size(1254, 378);
            this.GridRecord.TabIndex = 112;
            this.GridRecord.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridRecord_CellDoubleClick);
            // 
            // W_INST_DATE
            // 
            this.W_INST_DATE.HeaderText = "지시일자";
            this.W_INST_DATE.Name = "W_INST_DATE";
            this.W_INST_DATE.ReadOnly = true;
            this.W_INST_DATE.Width = 130;
            // 
            // W_INST_CD
            // 
            this.W_INST_CD.HeaderText = "지시번호";
            this.W_INST_CD.Name = "W_INST_CD";
            this.W_INST_CD.ReadOnly = true;
            // 
            // LOT_NO
            // 
            this.LOT_NO.HeaderText = "LOT번호";
            this.LOT_NO.Name = "LOT_NO";
            this.LOT_NO.ReadOnly = true;
            // 
            // RAW_MAT_CD
            // 
            this.RAW_MAT_CD.HeaderText = "원재료코드";
            this.RAW_MAT_CD.Name = "RAW_MAT_CD";
            this.RAW_MAT_CD.ReadOnly = true;
            this.RAW_MAT_CD.Visible = false;
            // 
            // RAW_MAT_NM
            // 
            this.RAW_MAT_NM.HeaderText = "원재료";
            this.RAW_MAT_NM.Name = "RAW_MAT_NM";
            this.RAW_MAT_NM.ReadOnly = true;
            this.RAW_MAT_NM.Width = 250;
            // 
            // INST_AMT
            // 
            this.INST_AMT.HeaderText = "지시량";
            this.INST_AMT.Name = "INST_AMT";
            this.INST_AMT.ReadOnly = true;
            // 
            // DELIVERY_DATE
            // 
            this.DELIVERY_DATE.HeaderText = "완료요구일";
            this.DELIVERY_DATE.Name = "DELIVERY_DATE";
            this.DELIVERY_DATE.ReadOnly = true;
            this.DELIVERY_DATE.Width = 130;
            // 
            // INST_NOTICE
            // 
            this.INST_NOTICE.HeaderText = "지시사항";
            this.INST_NOTICE.Name = "INST_NOTICE";
            this.INST_NOTICE.ReadOnly = true;
            this.INST_NOTICE.Width = 300;
            // 
            // COMPLETE_YN
            // 
            this.COMPLETE_YN.HeaderText = "진행상태";
            this.COMPLETE_YN.Name = "COMPLETE_YN";
            this.COMPLETE_YN.ReadOnly = true;
            this.COMPLETE_YN.Width = 120;
            // 
            // lblStatus
            // 
            this.lblStatus._BorderColor = System.Drawing.Color.LightGray;
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblStatus.Location = new System.Drawing.Point(1092, 416);
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
            this.cmbPage.Location = new System.Drawing.Point(946, 418);
            this.cmbPage.Name = "cmbPage";
            this.cmbPage.Size = new System.Drawing.Size(77, 20);
            this.cmbPage.TabIndex = 302;
            // 
            // lblPage
            // 
            this.lblPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPage.AutoSize = true;
            this.lblPage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lblPage.Location = new System.Drawing.Point(871, 423);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(69, 12);
            this.lblPage.TabIndex = 307;
            this.lblPage.Text = "페이지 이동";
            // 
            // btnLast
            // 
            this.btnLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnLast.Location = new System.Drawing.Point(1221, 416);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(28, 22);
            this.btnLast.TabIndex = 306;
            this.btnLast.Text = ">|";
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnNext.Location = new System.Drawing.Point(1192, 416);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(29, 22);
            this.btnNext.TabIndex = 305;
            this.btnNext.Text = ">";
            // 
            // btnPrevious
            // 
            this.btnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnPrevious.Location = new System.Drawing.Point(1057, 416);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(29, 22);
            this.btnPrevious.TabIndex = 304;
            this.btnPrevious.Text = "<";
            // 
            // btnFirst
            // 
            this.btnFirst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnFirst.Location = new System.Drawing.Point(1029, 416);
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
            this.label1.Size = new System.Drawing.Size(134, 29);
            this.label1.TabIndex = 348;
            this.label1.Text = "작업지시 검색";
            // 
            // pop씨지엠작업지시검색
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 442);
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
            this.Name = "pop씨지엠작업지시검색";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.pop씨지엠작업지시검색_Load);
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
        private System.Windows.Forms.DataGridViewTextBoxColumn W_INST_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn W_INST_CD;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOT_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn RAW_MAT_CD;
        private System.Windows.Forms.DataGridViewTextBoxColumn RAW_MAT_NM;
        private System.Windows.Forms.DataGridViewTextBoxColumn INST_AMT;
        private System.Windows.Forms.DataGridViewTextBoxColumn DELIVERY_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn INST_NOTICE;
        private System.Windows.Forms.DataGridViewTextBoxColumn COMPLETE_YN;
        private System.Windows.Forms.Label label1;
    }
}