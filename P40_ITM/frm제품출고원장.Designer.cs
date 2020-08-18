namespace 스마트팩토리.P40_ITM
{
    partial class frm제품출고원장
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm제품출고원장));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.conTextBox1 = new 스마트팩토리.Controls.conTextBox();
            this.btnSrch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.end_date = new System.Windows.Forms.DateTimePicker();
            this.btnClose = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.start_date = new System.Windows.Forms.DateTimePicker();
            this.itemOutGrid = new System.Windows.Forms.DataGridView();
            this.OUTPUT_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OUTPUT_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SEQ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CUST_NM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEM_NM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEM_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SPEC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OUTPUT_AMT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRICE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TOTAL_MONEY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LOT_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CHK = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.itemOutGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.PaleTurquoise;
            this.panel2.Controls.Add(this.conTextBox1);
            this.panel2.Controls.Add(this.btnSrch);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.end_date);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.start_date);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1360, 33);
            this.panel2.TabIndex = 14;
            // 
            // conTextBox1
            // 
            this.conTextBox1._AutoTab = true;
            this.conTextBox1._BorderColor = System.Drawing.Color.LightSteelBlue;
            this.conTextBox1._FocusedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.conTextBox1._WaterMarkColor = System.Drawing.Color.Gray;
            this.conTextBox1._WaterMarkText = "";
            this.conTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.conTextBox1.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.conTextBox1.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.conTextBox1.Location = new System.Drawing.Point(928, 6);
            this.conTextBox1.MaxLength = 20;
            this.conTextBox1.Name = "conTextBox1";
            this.conTextBox1.Size = new System.Drawing.Size(171, 22);
            this.conTextBox1.TabIndex = 380;
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
            this.btnSrch.Location = new System.Drawing.Point(1104, 2);
            this.btnSrch.Name = "btnSrch";
            this.btnSrch.Size = new System.Drawing.Size(33, 30);
            this.btnSrch.TabIndex = 381;
            this.btnSrch.Tag = "검색";
            this.btnSrch.UseVisualStyleBackColor = false;
            this.btnSrch.Click += new System.EventHandler(this.btnSrch_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(351, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 23);
            this.label2.TabIndex = 350;
            this.label2.Text = "조회일자";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(813, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 23);
            this.label3.TabIndex = 382;
            this.label3.Text = "제품 or 거래처";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label1.Location = new System.Drawing.Point(10, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 22);
            this.label1.TabIndex = 93;
            this.label1.Text = "제품출고원장";
            // 
            // end_date
            // 
            this.end_date.Checked = false;
            this.end_date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.end_date.Location = new System.Drawing.Point(654, 6);
            this.end_date.Name = "end_date";
            this.end_date.Size = new System.Drawing.Size(156, 21);
            this.end_date.TabIndex = 348;
            this.end_date.Value = new System.DateTime(2018, 10, 8, 0, 0, 0, 0);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.DodgerBlue;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClose.Location = new System.Drawing.Point(1292, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(65, 29);
            this.btnClose.TabIndex = 10;
            this.btnClose.Tag = "종료";
            this.btnClose.Text = "닫기";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label7.Location = new System.Drawing.Point(608, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 23);
            this.label7.TabIndex = 346;
            this.label7.Text = "~";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // start_date
            // 
            this.start_date.Checked = false;
            this.start_date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.start_date.Location = new System.Drawing.Point(444, 6);
            this.start_date.Name = "start_date";
            this.start_date.Size = new System.Drawing.Size(154, 21);
            this.start_date.TabIndex = 347;
            this.start_date.Value = new System.DateTime(2018, 10, 8, 0, 0, 0, 0);
            // 
            // itemOutGrid
            // 
            this.itemOutGrid.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.itemOutGrid.AllowUserToAddRows = false;
            this.itemOutGrid.AllowUserToDeleteRows = false;
            this.itemOutGrid.AllowUserToOrderColumns = true;
            this.itemOutGrid.AllowUserToResizeColumns = false;
            this.itemOutGrid.AllowUserToResizeRows = false;
            this.itemOutGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.itemOutGrid.BackgroundColor = System.Drawing.Color.White;
            this.itemOutGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.PowderBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.itemOutGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.itemOutGrid.ColumnHeadersHeight = 35;
            this.itemOutGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OUTPUT_DATE,
            this.OUTPUT_CD,
            this.SEQ,
            this.CUST_NM,
            this.ITEM_NM,
            this.ITEM_CD,
            this.SPEC,
            this.OUTPUT_AMT,
            this.PRICE,
            this.TOTAL_MONEY,
            this.LOT_NO,
            this.CHK});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("굴림", 11F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.itemOutGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.itemOutGrid.EnableHeadersVisualStyles = false;
            this.itemOutGrid.GridColor = System.Drawing.Color.PowderBlue;
            this.itemOutGrid.Location = new System.Drawing.Point(0, 34);
            this.itemOutGrid.Name = "itemOutGrid";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.itemOutGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.itemOutGrid.RowHeadersVisible = false;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("굴림", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.LightCyan;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.itemOutGrid.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.itemOutGrid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("굴림", 12F);
            this.itemOutGrid.RowTemplate.Height = 30;
            this.itemOutGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.itemOutGrid.Size = new System.Drawing.Size(1357, 677);
            this.itemOutGrid.TabIndex = 379;
            // 
            // OUTPUT_DATE
            // 
            this.OUTPUT_DATE.HeaderText = "출고일자";
            this.OUTPUT_DATE.Name = "OUTPUT_DATE";
            this.OUTPUT_DATE.ReadOnly = true;
            this.OUTPUT_DATE.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.OUTPUT_DATE.Width = 150;
            // 
            // OUTPUT_CD
            // 
            this.OUTPUT_CD.HeaderText = "출고코드";
            this.OUTPUT_CD.Name = "OUTPUT_CD";
            this.OUTPUT_CD.Visible = false;
            // 
            // SEQ
            // 
            this.SEQ.HeaderText = "SEQ";
            this.SEQ.Name = "SEQ";
            this.SEQ.Visible = false;
            // 
            // CUST_NM
            // 
            this.CUST_NM.HeaderText = "납품처명";
            this.CUST_NM.Name = "CUST_NM";
            this.CUST_NM.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CUST_NM.Width = 220;
            // 
            // ITEM_NM
            // 
            this.ITEM_NM.HeaderText = "제품명";
            this.ITEM_NM.Name = "ITEM_NM";
            this.ITEM_NM.ReadOnly = true;
            this.ITEM_NM.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ITEM_NM.Width = 300;
            // 
            // ITEM_CD
            // 
            this.ITEM_CD.HeaderText = "제품코드";
            this.ITEM_CD.Name = "ITEM_CD";
            this.ITEM_CD.Visible = false;
            // 
            // SPEC
            // 
            this.SPEC.HeaderText = "규격";
            this.SPEC.Name = "SPEC";
            this.SPEC.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SPEC.Width = 200;
            // 
            // OUTPUT_AMT
            // 
            this.OUTPUT_AMT.HeaderText = "수량";
            this.OUTPUT_AMT.Name = "OUTPUT_AMT";
            // 
            // PRICE
            // 
            this.PRICE.HeaderText = "단가";
            this.PRICE.Name = "PRICE";
            this.PRICE.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // TOTAL_MONEY
            // 
            this.TOTAL_MONEY.HeaderText = "금액";
            this.TOTAL_MONEY.Name = "TOTAL_MONEY";
            // 
            // LOT_NO
            // 
            this.LOT_NO.HeaderText = "LOTNO";
            this.LOT_NO.Name = "LOT_NO";
            this.LOT_NO.Width = 120;
            // 
            // CHK
            // 
            this.CHK.HeaderText = "확정";
            this.CHK.Name = "CHK";
            this.CHK.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CHK.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.CHK.Width = 60;
            // 
            // frm제품출고원장
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1360, 714);
            this.Controls.Add(this.itemOutGrid);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frm제품출고원장";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frm제품출고원장";
            this.Load += new System.EventHandler(this.frm제품출고원장_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.itemOutGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private Controls.conTextBox conTextBox1;
        private System.Windows.Forms.Button btnSrch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker end_date;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker start_date;
        private System.Windows.Forms.DataGridView itemOutGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn OUTPUT_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn OUTPUT_CD;
        private System.Windows.Forms.DataGridViewTextBoxColumn SEQ;
        private System.Windows.Forms.DataGridViewTextBoxColumn CUST_NM;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEM_NM;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEM_CD;
        private System.Windows.Forms.DataGridViewTextBoxColumn SPEC;
        private System.Windows.Forms.DataGridViewTextBoxColumn OUTPUT_AMT;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRICE;
        private System.Windows.Forms.DataGridViewTextBoxColumn TOTAL_MONEY;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOT_NO;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CHK;
    }
}