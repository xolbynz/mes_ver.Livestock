namespace 스마트팩토리.P40_ITM
{
    partial class frm제품출고이력현황
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm제품출고이력현황));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSrch = new System.Windows.Forms.Button();
            this.end_date = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.start_date = new System.Windows.Forms.DateTimePicker();
            this.itemOutGrid = new System.Windows.Forms.DataGridView();
            this.OUTPUT_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OUTPUT_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SEQ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEM_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEM_NM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SPEC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OUTPUT_AMT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LOT_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CUST_NM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CHK = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.itemOutGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.PaleTurquoise;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.btnSrch);
            this.panel2.Controls.Add(this.end_date);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.start_date);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1360, 33);
            this.panel2.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(435, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 23);
            this.label2.TabIndex = 345;
            this.label2.Text = "출고일자";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.btnSrch.Location = new System.Drawing.Point(899, 1);
            this.btnSrch.Name = "btnSrch";
            this.btnSrch.Size = new System.Drawing.Size(33, 30);
            this.btnSrch.TabIndex = 344;
            this.btnSrch.Tag = "검색";
            this.btnSrch.UseVisualStyleBackColor = false;
            this.btnSrch.Click += new System.EventHandler(this.btnSrch_Click);
            // 
            // end_date
            // 
            this.end_date.Checked = false;
            this.end_date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.end_date.Location = new System.Drawing.Point(738, 6);
            this.end_date.Name = "end_date";
            this.end_date.Size = new System.Drawing.Size(156, 21);
            this.end_date.TabIndex = 343;
            this.end_date.Value = new System.DateTime(2018, 10, 8, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label1.Location = new System.Drawing.Point(10, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(202, 22);
            this.label1.TabIndex = 93;
            this.label1.Text = "제품출고 이력현황";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label7.Location = new System.Drawing.Point(692, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 23);
            this.label7.TabIndex = 341;
            this.label7.Text = "~";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            // start_date
            // 
            this.start_date.Checked = false;
            this.start_date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.start_date.Location = new System.Drawing.Point(528, 6);
            this.start_date.Name = "start_date";
            this.start_date.Size = new System.Drawing.Size(154, 21);
            this.start_date.TabIndex = 342;
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
            this.ITEM_CD,
            this.ITEM_NM,
            this.SPEC,
            this.OUTPUT_AMT,
            this.LOT_NO,
            this.CUST_NM,
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
            this.itemOutGrid.Location = new System.Drawing.Point(0, 33);
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
            this.itemOutGrid.RowHeadersWidth = 30;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("굴림", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.LightCyan;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.itemOutGrid.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.itemOutGrid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("굴림", 12F);
            this.itemOutGrid.RowTemplate.Height = 30;
            this.itemOutGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.itemOutGrid.Size = new System.Drawing.Size(1360, 686);
            this.itemOutGrid.TabIndex = 377;
            // 
            // OUTPUT_DATE
            // 
            this.OUTPUT_DATE.HeaderText = "출고일자";
            this.OUTPUT_DATE.Name = "OUTPUT_DATE";
            this.OUTPUT_DATE.ReadOnly = true;
            this.OUTPUT_DATE.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.OUTPUT_DATE.Width = 140;
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
            // ITEM_CD
            // 
            this.ITEM_CD.HeaderText = "제품코드";
            this.ITEM_CD.Name = "ITEM_CD";
            this.ITEM_CD.Width = 120;
            // 
            // ITEM_NM
            // 
            this.ITEM_NM.HeaderText = "제품명";
            this.ITEM_NM.Name = "ITEM_NM";
            this.ITEM_NM.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ITEM_NM.Width = 330;
            // 
            // SPEC
            // 
            this.SPEC.HeaderText = "규격";
            this.SPEC.Name = "SPEC";
            this.SPEC.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SPEC.Width = 200;
            // 
            // OUTPUT_AMT
            // 
            this.OUTPUT_AMT.HeaderText = "수량";
            this.OUTPUT_AMT.Name = "OUTPUT_AMT";
            this.OUTPUT_AMT.Width = 120;
            // 
            // LOT_NO
            // 
            this.LOT_NO.HeaderText = "LOTNO";
            this.LOT_NO.Name = "LOT_NO";
            this.LOT_NO.Width = 150;
            // 
            // CUST_NM
            // 
            this.CUST_NM.HeaderText = "납품처";
            this.CUST_NM.Name = "CUST_NM";
            this.CUST_NM.Width = 230;
            // 
            // CHK
            // 
            this.CHK.HeaderText = "확인";
            this.CHK.Name = "CHK";
            this.CHK.Width = 60;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "출고일자";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.Width = 140;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "출고코드";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Visible = false;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "SEQ";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Visible = false;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "제품코드";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 120;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "제품명";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn5.Width = 330;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "규격";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn6.Width = 200;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "수량";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Width = 150;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "LOTNO";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Width = 150;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "납품처";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.Width = 300;
            // 
            // frm제품출고이력현황
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1360, 714);
            this.Controls.Add(this.itemOutGrid);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frm제품출고이력현황";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frm제품출고현황";
            this.Load += new System.EventHandler(this.frm제품출고이력현황_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.itemOutGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSrch;
        private System.Windows.Forms.DateTimePicker end_date;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DateTimePicker start_date;
        private System.Windows.Forms.DataGridView itemOutGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn OUTPUT_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn OUTPUT_CD;
        private System.Windows.Forms.DataGridViewTextBoxColumn SEQ;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEM_CD;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEM_NM;
        private System.Windows.Forms.DataGridViewTextBoxColumn SPEC;
        private System.Windows.Forms.DataGridViewTextBoxColumn OUTPUT_AMT;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOT_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CUST_NM;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CHK;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
    }
}