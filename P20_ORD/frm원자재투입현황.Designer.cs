namespace 스마트팩토리.P20_ORD
{
    partial class frm원자재투입현황
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm원자재투입현황));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rawOutGrid = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OUTPUT_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INPUT_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INPUT_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RAW_MAT_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RAW_MAT_NM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chugjong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CLASS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GRADE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COUNTRY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SPEC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TOTAL_AMT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LOT_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LOT_SUB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BAR_NUM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmb_cd_srch = new 스마트팩토리.Controls.conComboBox();
            this.txt_srch = new 스마트팩토리.Controls.conTextBox();
            this.btn출력 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSrch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.end_date = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.start_date = new System.Windows.Forms.DateTimePicker();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rawOutGrid)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.rawOutGrid);
            this.panel1.Location = new System.Drawing.Point(2, 34);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1358, 707);
            this.panel1.TabIndex = 14;
            // 
            // rawOutGrid
            // 
            this.rawOutGrid.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.rawOutGrid.AllowUserToAddRows = false;
            this.rawOutGrid.AllowUserToDeleteRows = false;
            this.rawOutGrid.AllowUserToOrderColumns = true;
            this.rawOutGrid.AllowUserToResizeColumns = false;
            this.rawOutGrid.AllowUserToResizeRows = false;
            this.rawOutGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rawOutGrid.BackgroundColor = System.Drawing.Color.White;
            this.rawOutGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.CadetBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.rawOutGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.rawOutGrid.ColumnHeadersHeight = 35;
            this.rawOutGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.OUTPUT_DATE,
            this.INPUT_DATE,
            this.INPUT_CD,
            this.RAW_MAT_CD,
            this.RAW_MAT_NM,
            this.chugjong,
            this.CLASS,
            this.GRADE,
            this.COUNTRY,
            this.TYPE,
            this.SPEC,
            this.TOTAL_AMT,
            this.unit,
            this.LOT_NO,
            this.LOT_SUB,
            this.BAR_NUM});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("굴림", 11F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.rawOutGrid.DefaultCellStyle = dataGridViewCellStyle9;
            this.rawOutGrid.EnableHeadersVisualStyles = false;
            this.rawOutGrid.GridColor = System.Drawing.Color.PowderBlue;
            this.rawOutGrid.Location = new System.Drawing.Point(-2, 0);
            this.rawOutGrid.Name = "rawOutGrid";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.rawOutGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.rawOutGrid.RowHeadersVisible = false;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.LightCyan;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
            this.rawOutGrid.RowsDefaultCellStyle = dataGridViewCellStyle11;
            this.rawOutGrid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("굴림", 12F);
            this.rawOutGrid.RowTemplate.Height = 30;
            this.rawOutGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.rawOutGrid.Size = new System.Drawing.Size(1357, 678);
            this.rawOutGrid.TabIndex = 374;
            // 
            // Column1
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column1.HeaderText = "No.";
            this.Column1.Name = "Column1";
            this.Column1.Width = 50;
            // 
            // OUTPUT_DATE
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.OUTPUT_DATE.DefaultCellStyle = dataGridViewCellStyle3;
            this.OUTPUT_DATE.HeaderText = "출고일자";
            this.OUTPUT_DATE.Name = "OUTPUT_DATE";
            this.OUTPUT_DATE.ReadOnly = true;
            this.OUTPUT_DATE.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // INPUT_DATE
            // 
            this.INPUT_DATE.HeaderText = "입고일자";
            this.INPUT_DATE.Name = "INPUT_DATE";
            this.INPUT_DATE.ReadOnly = true;
            // 
            // INPUT_CD
            // 
            this.INPUT_CD.HeaderText = "입고번호";
            this.INPUT_CD.Name = "INPUT_CD";
            this.INPUT_CD.ReadOnly = true;
            this.INPUT_CD.Width = 80;
            // 
            // RAW_MAT_CD
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.RAW_MAT_CD.DefaultCellStyle = dataGridViewCellStyle4;
            this.RAW_MAT_CD.HeaderText = "원자재코드";
            this.RAW_MAT_CD.Name = "RAW_MAT_CD";
            this.RAW_MAT_CD.ReadOnly = true;
            this.RAW_MAT_CD.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.RAW_MAT_CD.Visible = false;
            // 
            // RAW_MAT_NM
            // 
            this.RAW_MAT_NM.HeaderText = "원자재";
            this.RAW_MAT_NM.Name = "RAW_MAT_NM";
            this.RAW_MAT_NM.ReadOnly = true;
            this.RAW_MAT_NM.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // chugjong
            // 
            this.chugjong.HeaderText = "축종";
            this.chugjong.Name = "chugjong";
            this.chugjong.Width = 80;
            // 
            // CLASS
            // 
            this.CLASS.HeaderText = "부위";
            this.CLASS.Name = "CLASS";
            this.CLASS.Width = 80;
            // 
            // GRADE
            // 
            this.GRADE.HeaderText = "등급";
            this.GRADE.Name = "GRADE";
            this.GRADE.Width = 80;
            // 
            // COUNTRY
            // 
            this.COUNTRY.HeaderText = "원산지";
            this.COUNTRY.Name = "COUNTRY";
            this.COUNTRY.Width = 80;
            // 
            // TYPE
            // 
            this.TYPE.HeaderText = "유형";
            this.TYPE.Name = "TYPE";
            // 
            // SPEC
            // 
            this.SPEC.HeaderText = "규격";
            this.SPEC.Name = "SPEC";
            this.SPEC.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SPEC.Visible = false;
            this.SPEC.Width = 240;
            // 
            // TOTAL_AMT
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.TOTAL_AMT.DefaultCellStyle = dataGridViewCellStyle5;
            this.TOTAL_AMT.HeaderText = "투입량";
            this.TOTAL_AMT.Name = "TOTAL_AMT";
            this.TOTAL_AMT.Width = 105;
            // 
            // unit
            // 
            this.unit.HeaderText = "단위";
            this.unit.Name = "unit";
            // 
            // LOT_NO
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.LOT_NO.DefaultCellStyle = dataGridViewCellStyle6;
            this.LOT_NO.HeaderText = "LOTNO";
            this.LOT_NO.Name = "LOT_NO";
            this.LOT_NO.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.LOT_NO.Width = 140;
            // 
            // LOT_SUB
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.LOT_SUB.DefaultCellStyle = dataGridViewCellStyle7;
            this.LOT_SUB.HeaderText = "LOTSUB";
            this.LOT_SUB.Name = "LOT_SUB";
            this.LOT_SUB.Visible = false;
            // 
            // BAR_NUM
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.BAR_NUM.DefaultCellStyle = dataGridViewCellStyle8;
            this.BAR_NUM.HeaderText = "바코드";
            this.BAR_NUM.Name = "BAR_NUM";
            this.BAR_NUM.Width = 140;
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
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.Controls.Add(this.cmb_cd_srch);
            this.panel2.Controls.Add(this.txt_srch);
            this.panel2.Controls.Add(this.btn출력);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.btnSrch);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.end_date);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.start_date);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1360, 33);
            this.panel2.TabIndex = 13;
            // 
            // cmb_cd_srch
            // 
            this.cmb_cd_srch._BorderColor = System.Drawing.Color.Transparent;
            this.cmb_cd_srch._FocusedBackColor = System.Drawing.Color.White;
            this.cmb_cd_srch.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cmb_cd_srch.FormattingEnabled = true;
            this.cmb_cd_srch.Location = new System.Drawing.Point(688, 6);
            this.cmb_cd_srch.Name = "cmb_cd_srch";
            this.cmb_cd_srch.Size = new System.Drawing.Size(128, 20);
            this.cmb_cd_srch.TabIndex = 395;
            // 
            // txt_srch
            // 
            this.txt_srch._AutoTab = true;
            this.txt_srch._BorderColor = System.Drawing.Color.LightSteelBlue;
            this.txt_srch._FocusedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txt_srch._WaterMarkColor = System.Drawing.Color.Gray;
            this.txt_srch._WaterMarkText = "";
            this.txt_srch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_srch.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_srch.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txt_srch.Location = new System.Drawing.Point(822, 5);
            this.txt_srch.MaxLength = 20;
            this.txt_srch.Name = "txt_srch";
            this.txt_srch.Size = new System.Drawing.Size(171, 22);
            this.txt_srch.TabIndex = 394;
            this.txt_srch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_srch_KeyDown);
            // 
            // btn출력
            // 
            this.btn출력.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn출력.BackColor = System.Drawing.Color.Transparent;
            this.btn출력.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn출력.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn출력.FlatAppearance.BorderColor = System.Drawing.Color.PaleTurquoise;
            this.btn출력.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btn출력.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn출력.ForeColor = System.Drawing.Color.DodgerBlue;
            this.btn출력.Image = ((System.Drawing.Image)(resources.GetObject("btn출력.Image")));
            this.btn출력.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn출력.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn출력.Location = new System.Drawing.Point(1146, 2);
            this.btn출력.Name = "btn출력";
            this.btn출력.Size = new System.Drawing.Size(69, 29);
            this.btn출력.TabIndex = 383;
            this.btn출력.Tag = "출력";
            this.btn출력.Text = "출력";
            this.btn출력.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn출력.UseVisualStyleBackColor = false;
            this.btn출력.Click += new System.EventHandler(this.btn출력_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(302, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 23);
            this.label2.TabIndex = 379;
            this.label2.Text = "조회일자";
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
            this.btnSrch.Location = new System.Drawing.Point(1001, 2);
            this.btnSrch.Name = "btnSrch";
            this.btnSrch.Size = new System.Drawing.Size(33, 30);
            this.btnSrch.TabIndex = 378;
            this.btnSrch.TabStop = false;
            this.btnSrch.Tag = "검색";
            this.btnSrch.UseVisualStyleBackColor = false;
            this.btnSrch.Click += new System.EventHandler(this.btnSrch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.CadetBlue;
            this.label1.Location = new System.Drawing.Point(10, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 29);
            this.label1.TabIndex = 93;
            this.label1.Text = "원자재 투입현황";
            // 
            // end_date
            // 
            this.end_date.Checked = false;
            this.end_date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.end_date.Location = new System.Drawing.Point(551, 6);
            this.end_date.Name = "end_date";
            this.end_date.Size = new System.Drawing.Size(100, 21);
            this.end_date.TabIndex = 377;
            this.end_date.Value = new System.DateTime(2018, 10, 8, 0, 0, 0, 0);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label7.Location = new System.Drawing.Point(505, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 23);
            this.label7.TabIndex = 375;
            this.label7.Text = "~";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.btnSave.Location = new System.Drawing.Point(1221, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(65, 29);
            this.btnSave.TabIndex = 0;
            this.btnSave.Tag = "저장";
            this.btnSave.Text = "저장";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // start_date
            // 
            this.start_date.Checked = false;
            this.start_date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.start_date.Location = new System.Drawing.Point(395, 6);
            this.start_date.Name = "start_date";
            this.start_date.Size = new System.Drawing.Size(100, 21);
            this.start_date.TabIndex = 376;
            this.start_date.Value = new System.DateTime(2018, 10, 8, 0, 0, 0, 0);
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridViewTextBoxColumn1.HeaderText = "제품코드";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.Width = 200;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle13;
            this.dataGridViewTextBoxColumn2.HeaderText = "제품명";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn2.Width = 500;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle14;
            this.dataGridViewTextBoxColumn3.HeaderText = "규격";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn3.Width = 250;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "기초재고량";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn4.Width = 150;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "포장일자";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn5.Width = 150;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle15;
            this.dataGridViewTextBoxColumn6.HeaderText = "LOTSUB";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn6.Width = 150;
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle16;
            this.dataGridViewTextBoxColumn7.HeaderText = "바코드";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Width = 200;
            // 
            // dataGridViewTextBoxColumn8
            // 
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn8.DefaultCellStyle = dataGridViewCellStyle17;
            this.dataGridViewTextBoxColumn8.HeaderText = "투입량";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Width = 120;
            // 
            // dataGridViewTextBoxColumn9
            // 
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn9.DefaultCellStyle = dataGridViewCellStyle18;
            this.dataGridViewTextBoxColumn9.HeaderText = "투입량";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.Width = 105;
            // 
            // frm원자재투입현황
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1360, 714);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frm원자재투입현황";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frm원자재투입현황";
            this.Load += new System.EventHandler(this.frm원자재투입현황_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rawOutGrid)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView rawOutGrid;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSrch;
        private System.Windows.Forms.DateTimePicker end_date;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker start_date;
        private System.Windows.Forms.Button btn출력;
        private Controls.conTextBox txt_srch;
        private Controls.conComboBox cmb_cd_srch;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn OUTPUT_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn INPUT_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn INPUT_CD;
        private System.Windows.Forms.DataGridViewTextBoxColumn RAW_MAT_CD;
        private System.Windows.Forms.DataGridViewTextBoxColumn RAW_MAT_NM;
        private System.Windows.Forms.DataGridViewTextBoxColumn chugjong;
        private System.Windows.Forms.DataGridViewTextBoxColumn CLASS;
        private System.Windows.Forms.DataGridViewTextBoxColumn GRADE;
        private System.Windows.Forms.DataGridViewTextBoxColumn COUNTRY;
        private System.Windows.Forms.DataGridViewTextBoxColumn TYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn SPEC;
        private System.Windows.Forms.DataGridViewTextBoxColumn TOTAL_AMT;
        private System.Windows.Forms.DataGridViewTextBoxColumn unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOT_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOT_SUB;
        private System.Windows.Forms.DataGridViewTextBoxColumn BAR_NUM;

    }
}