namespace 스마트팩토리.P40_ITM
{
    partial class frm제품출하바코드
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm제품출하바코드));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataItemGrid = new System.Windows.Forms.DataGridView();
            this.ITEM_NM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BASIC_STOCK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PACK_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.conTextBox3 = new 스마트팩토리.Controls.conTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.conTextBox2 = new 스마트팩토리.Controls.conTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.cmb_type = new 스마트팩토리.Controls.conComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.conTextBox1 = new 스마트팩토리.Controls.conTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataItemGrid)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.PaleTurquoise;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1554, 41);
            this.panel2.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label1.Location = new System.Drawing.Point(11, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 27);
            this.label1.TabIndex = 93;
            this.label1.Text = "제품출하바코드";
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
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.dataItemGrid);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(0, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1554, 851);
            this.panel1.TabIndex = 17;
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataItemGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataItemGrid.ColumnHeadersHeight = 40;
            this.dataItemGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ITEM_NM,
            this.BASIC_STOCK,
            this.PACK_DATE,
            this.Column2,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column1,
            this.Column3});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("굴림", 11F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataItemGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataItemGrid.EnableHeadersVisualStyles = false;
            this.dataItemGrid.GridColor = System.Drawing.Color.DodgerBlue;
            this.dataItemGrid.Location = new System.Drawing.Point(3, 129);
            this.dataItemGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataItemGrid.Name = "dataItemGrid";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataItemGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataItemGrid.RowHeadersVisible = false;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("굴림", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.LightCyan;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.dataItemGrid.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataItemGrid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("굴림", 15F);
            this.dataItemGrid.RowTemplate.Height = 40;
            this.dataItemGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataItemGrid.Size = new System.Drawing.Size(1548, 722);
            this.dataItemGrid.TabIndex = 384;
            // 
            // ITEM_NM
            // 
            this.ITEM_NM.Frozen = true;
            this.ITEM_NM.HeaderText = "식별번호";
            this.ITEM_NM.Name = "ITEM_NM";
            this.ITEM_NM.ReadOnly = true;
            this.ITEM_NM.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ITEM_NM.Width = 170;
            // 
            // BASIC_STOCK
            // 
            this.BASIC_STOCK.Frozen = true;
            this.BASIC_STOCK.HeaderText = "제품코드";
            this.BASIC_STOCK.Name = "BASIC_STOCK";
            this.BASIC_STOCK.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.BASIC_STOCK.Width = 140;
            // 
            // PACK_DATE
            // 
            this.PACK_DATE.HeaderText = "제품명";
            this.PACK_DATE.Name = "PACK_DATE";
            this.PACK_DATE.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PACK_DATE.Width = 255;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "규격";
            this.Column2.Name = "Column2";
            this.Column2.Width = 170;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "박스수량";
            this.Column4.Name = "Column4";
            this.Column4.Width = 120;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "출고수량";
            this.Column5.Name = "Column5";
            this.Column5.Width = 120;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "단가";
            this.Column6.Name = "Column6";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "금액";
            this.Column1.Name = "Column1";
            this.Column1.Width = 120;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "상대식별";
            this.Column3.Name = "Column3";
            this.Column3.Width = 150;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.conTextBox3);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.conTextBox2);
            this.groupBox2.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox2.Location = new System.Drawing.Point(1121, 8);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(421, 113);
            this.groupBox2.TabIndex = 355;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "업체바코드";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.AliceBlue;
            this.label6.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(44, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(127, 29);
            this.label6.TabIndex = 361;
            this.label6.Text = "출고합계";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // conTextBox3
            // 
            this.conTextBox3._AutoTab = true;
            this.conTextBox3._BorderColor = System.Drawing.Color.LightSteelBlue;
            this.conTextBox3._FocusedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.conTextBox3._WaterMarkColor = System.Drawing.Color.Gray;
            this.conTextBox3._WaterMarkText = "";
            this.conTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.conTextBox3.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.conTextBox3.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.conTextBox3.Location = new System.Drawing.Point(172, 66);
            this.conTextBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.conTextBox3.MaxLength = 20;
            this.conTextBox3.Name = "conTextBox3";
            this.conTextBox3.Size = new System.Drawing.Size(209, 28);
            this.conTextBox3.TabIndex = 362;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.RoyalBlue;
            this.label5.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(44, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 29);
            this.label5.TabIndex = 359;
            this.label5.Text = "제품코드";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // conTextBox2
            // 
            this.conTextBox2._AutoTab = true;
            this.conTextBox2._BorderColor = System.Drawing.Color.LightSteelBlue;
            this.conTextBox2._FocusedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.conTextBox2._WaterMarkColor = System.Drawing.Color.Gray;
            this.conTextBox2._WaterMarkText = "";
            this.conTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.conTextBox2.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.conTextBox2.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.conTextBox2.Location = new System.Drawing.Point(172, 27);
            this.conTextBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.conTextBox2.MaxLength = 20;
            this.conTextBox2.Name = "conTextBox2";
            this.conTextBox2.Size = new System.Drawing.Size(209, 28);
            this.conTextBox2.TabIndex = 360;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.cmb_type);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.conTextBox1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox1.Location = new System.Drawing.Point(16, 8);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(802, 113);
            this.groupBox1.TabIndex = 354;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "업체바코드";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(471, 65);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(238, 31);
            this.button1.TabIndex = 358;
            this.button1.Text = "식별표 재발행";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // cmb_type
            // 
            this.cmb_type._BorderColor = System.Drawing.Color.Transparent;
            this.cmb_type._FocusedBackColor = System.Drawing.Color.White;
            this.cmb_type.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cmb_type.FormattingEnabled = true;
            this.cmb_type.Location = new System.Drawing.Point(150, 72);
            this.cmb_type.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmb_type.Name = "cmb_type";
            this.cmb_type.Size = new System.Drawing.Size(231, 25);
            this.cmb_type.TabIndex = 355;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.PowderBlue;
            this.label3.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(22, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 29);
            this.label3.TabIndex = 341;
            this.label3.Text = "제품코드";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.conTextBox1.Location = new System.Drawing.Point(150, 25);
            this.conTextBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.conTextBox1.MaxLength = 20;
            this.conTextBox1.Name = "conTextBox1";
            this.conTextBox1.Size = new System.Drawing.Size(231, 28);
            this.conTextBox1.TabIndex = 342;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.PowderBlue;
            this.label4.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(406, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 29);
            this.label4.TabIndex = 343;
            this.label4.Text = "출고일자";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Checked = false;
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(535, 25);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(244, 27);
            this.dateTimePicker1.TabIndex = 344;
            this.dateTimePicker1.Value = new System.DateTime(2018, 10, 8, 0, 0, 0, 0);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.PowderBlue;
            this.label2.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(22, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 29);
            this.label2.TabIndex = 345;
            this.label2.Text = "납품처";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frm제품출하바코드
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1554, 893);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "frm제품출하바코드";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frm제품출하바코드";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataItemGrid)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private Controls.conTextBox conTextBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label2;
        private Controls.conComboBox cmb_type;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
        private Controls.conTextBox conTextBox2;
        private System.Windows.Forms.Label label6;
        private Controls.conTextBox conTextBox3;
        private System.Windows.Forms.DataGridView dataItemGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEM_NM;
        private System.Windows.Forms.DataGridViewTextBoxColumn BASIC_STOCK;
        private System.Windows.Forms.DataGridViewTextBoxColumn PACK_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}