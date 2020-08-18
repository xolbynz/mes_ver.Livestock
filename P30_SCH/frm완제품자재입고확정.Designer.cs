namespace 스마트팩토리.P30_SCH
{
    partial class frm완제품자재입고확정
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm완제품자재입고확정));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.conTextBox1 = new 스마트팩토리.Controls.conTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dataItemGrid = new System.Windows.Forms.DataGridView();
            this.ITEM_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEM_NM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BASIC_STOCK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PACK_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataItemGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.PaleTurquoise;
            this.panel2.Controls.Add(this.conTextBox1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.btnSearch);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1554, 41);
            this.panel2.TabIndex = 19;
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
            this.conTextBox1.Location = new System.Drawing.Point(493, 7);
            this.conTextBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.conTextBox1.MaxLength = 20;
            this.conTextBox1.Name = "conTextBox1";
            this.conTextBox1.Size = new System.Drawing.Size(312, 27);
            this.conTextBox1.TabIndex = 375;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(387, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 29);
            this.label2.TabIndex = 379;
            this.label2.Text = "제품명";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.SkyBlue;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(811, 2);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(38, 37);
            this.btnSearch.TabIndex = 378;
            this.btnSearch.Tag = "검색";
            this.btnSearch.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label1.Location = new System.Drawing.Point(11, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(274, 27);
            this.label1.TabIndex = 93;
            this.label1.Text = "완제품 자재입고등록";
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
            this.btnClose.Location = new System.Drawing.Point(1477, 4);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(74, 36);
            this.btnClose.TabIndex = 10;
            this.btnClose.Tag = "종료";
            this.btnClose.Text = "닫기";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.DodgerBlue;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSave.Location = new System.Drawing.Point(1335, 4);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(134, 36);
            this.btnSave.TabIndex = 0;
            this.btnSave.Tag = "저장";
            this.btnSave.Text = "완제품입고";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // dataItemGrid
            // 
            this.dataItemGrid.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dataItemGrid.AllowUserToAddRows = false;
            this.dataItemGrid.AllowUserToDeleteRows = false;
            this.dataItemGrid.AllowUserToOrderColumns = true;
            this.dataItemGrid.AllowUserToResizeColumns = false;
            this.dataItemGrid.AllowUserToResizeRows = false;
            this.dataItemGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataItemGrid.BackgroundColor = System.Drawing.Color.White;
            this.dataItemGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataItemGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataItemGrid.ColumnHeadersHeight = 40;
            this.dataItemGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ITEM_CD,
            this.ITEM_NM,
            this.BASIC_STOCK,
            this.PACK_DATE,
            this.Column2,
            this.Column1,
            this.Column3,
            this.Column4});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("굴림", 11F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataItemGrid.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataItemGrid.EnableHeadersVisualStyles = false;
            this.dataItemGrid.GridColor = System.Drawing.Color.DodgerBlue;
            this.dataItemGrid.Location = new System.Drawing.Point(0, 39);
            this.dataItemGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataItemGrid.Name = "dataItemGrid";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataItemGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataItemGrid.RowHeadersVisible = false;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("굴림", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.LightCyan;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            this.dataItemGrid.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dataItemGrid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("굴림", 15F);
            this.dataItemGrid.RowTemplate.Height = 40;
            this.dataItemGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataItemGrid.Size = new System.Drawing.Size(1548, 853);
            this.dataItemGrid.TabIndex = 377;
            // 
            // ITEM_CD
            // 
            this.ITEM_CD.HeaderText = "LOTNO";
            this.ITEM_CD.Name = "ITEM_CD";
            this.ITEM_CD.ReadOnly = true;
            this.ITEM_CD.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ITEM_CD.Width = 120;
            // 
            // ITEM_NM
            // 
            this.ITEM_NM.HeaderText = "처리일자";
            this.ITEM_NM.Name = "ITEM_NM";
            this.ITEM_NM.ReadOnly = true;
            this.ITEM_NM.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // BASIC_STOCK
            // 
            this.BASIC_STOCK.HeaderText = "제품명";
            this.BASIC_STOCK.Name = "BASIC_STOCK";
            this.BASIC_STOCK.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.BASIC_STOCK.Width = 300;
            // 
            // PACK_DATE
            // 
            this.PACK_DATE.HeaderText = "규격";
            this.PACK_DATE.Name = "PACK_DATE";
            this.PACK_DATE.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PACK_DATE.Width = 170;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "수량";
            this.Column2.Name = "Column2";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "원자재명";
            this.Column1.Name = "Column1";
            this.Column1.Width = 300;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "원자재규격";
            this.Column3.Name = "Column3";
            this.Column3.Width = 170;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "확인";
            this.Column4.Name = "Column4";
            this.Column4.Width = 60;
            // 
            // frm완제품자재입고확정
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1554, 893);
            this.Controls.Add(this.dataItemGrid);
            this.Controls.Add(this.panel2);
            this.Name = "frm완제품자재입고확정";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frm완제품자재입고확정";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataItemGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private Controls.conTextBox conTextBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dataItemGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEM_CD;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEM_NM;
        private System.Windows.Forms.DataGridViewTextBoxColumn BASIC_STOCK;
        private System.Windows.Forms.DataGridViewTextBoxColumn PACK_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column4;
    }
}