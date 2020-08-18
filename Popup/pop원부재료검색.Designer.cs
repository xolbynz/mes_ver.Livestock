namespace 스마트팩토리.Popup
{
    partial class pop원부재료검색
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pop원부재료검색));
            this.GridRecord = new System.Windows.Forms.DataGridView();
            this.txtSrch = new 스마트팩토리.Controls.conTextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblStatus = new 스마트팩토리.Controls.conLabel();
            this.cmbPage = new 스마트팩토리.Controls.conComboBox();
            this.lblPage = new System.Windows.Forms.Label();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.RAW_MAT_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CHUGJONG_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CHUGJONG_NM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CLASS_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CLASS_NM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RAW_MAT_NM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LABEL_NM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SPEC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GRADE_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GRADE_NM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BOX_AMT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COUNTRY_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COUNTRY_NM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RAW_MAT_GUBUN_NM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RAW_MAT_GUBUN_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TYPE_NM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TYPE_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INPUT_UNIT_NM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OUTPUT_UNIT_NM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INPUT_PRICE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OUTPUT_PRICE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INPUT_UNIT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OUTPUT_UNIT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BAL_STOCK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CUST_NM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CUST_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CVR_RATIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VAT_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.GridRecord)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridRecord.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GridRecord.ColumnHeadersHeight = 40;
            this.GridRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.GridRecord.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RAW_MAT_CD,
            this.CHUGJONG_CD,
            this.CHUGJONG_NM,
            this.CLASS_CD,
            this.CLASS_NM,
            this.RAW_MAT_NM,
            this.LABEL_NM,
            this.SPEC,
            this.GRADE_CD,
            this.GRADE_NM,
            this.BOX_AMT,
            this.COUNTRY_CD,
            this.COUNTRY_NM,
            this.RAW_MAT_GUBUN_NM,
            this.RAW_MAT_GUBUN_CD,
            this.TYPE_NM,
            this.TYPE_CD,
            this.INPUT_UNIT_NM,
            this.OUTPUT_UNIT_NM,
            this.INPUT_PRICE,
            this.OUTPUT_PRICE,
            this.INPUT_UNIT,
            this.OUTPUT_UNIT,
            this.BAL_STOCK,
            this.CUST_NM,
            this.CUST_CD,
            this.CVR_RATIO,
            this.VAT_CD});
            this.GridRecord.EnableHeadersVisualStyles = false;
            this.GridRecord.GridColor = System.Drawing.Color.PowderBlue;
            this.GridRecord.Location = new System.Drawing.Point(12, 78);
            this.GridRecord.Name = "GridRecord";
            this.GridRecord.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridRecord.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.GridRecord.RowHeadersVisible = false;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightCyan;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.GridRecord.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.GridRecord.RowTemplate.Height = 23;
            this.GridRecord.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridRecord.Size = new System.Drawing.Size(1015, 324);
            this.GridRecord.TabIndex = 105;
            this.GridRecord.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridRecord_CellDoubleClick);
            this.GridRecord.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridRecord_KeyDown);
            // 
            // txtSrch
            // 
            this.txtSrch._AutoTab = true;
            this.txtSrch._BorderColor = System.Drawing.Color.LightSkyBlue;
            this.txtSrch._FocusedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtSrch._WaterMarkColor = System.Drawing.Color.Gray;
            this.txtSrch._WaterMarkText = "";
            this.txtSrch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSrch.Font = new System.Drawing.Font("굴림", 10F);
            this.txtSrch.Location = new System.Drawing.Point(100, 44);
            this.txtSrch.Name = "txtSrch";
            this.txtSrch.Size = new System.Drawing.Size(145, 22);
            this.txtSrch.TabIndex = 103;
            this.txtSrch.Leave += new System.EventHandler(this.txtSrch_Leave);
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.Gainsboro;
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label15.Location = new System.Drawing.Point(12, 44);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(82, 22);
            this.label15.TabIndex = 106;
            this.label15.Text = "검색어";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.btnSearch.Location = new System.Drawing.Point(251, 39);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(33, 33);
            this.btnSearch.TabIndex = 104;
            this.btnSearch.TabStop = false;
            this.btnSearch.Tag = "검색";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.Controls.Add(this.btnExit);
            this.panel2.Controls.Add(this.lblTitle);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1034, 33);
            this.panel2.TabIndex = 107;
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
            this.btnExit.Location = new System.Drawing.Point(957, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(65, 29);
            this.btnExit.TabIndex = 2;
            this.btnExit.TabStop = false;
            this.btnExit.Tag = "종료";
            this.btnExit.Text = "닫기";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.CadetBlue;
            this.lblTitle.Location = new System.Drawing.Point(12, 3);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(134, 29);
            this.lblTitle.TabIndex = 95;
            this.lblTitle.Text = "원부재료 검색";
            // 
            // lblStatus
            // 
            this.lblStatus._BorderColor = System.Drawing.Color.LightGray;
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblStatus.Location = new System.Drawing.Point(866, 411);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(94, 22);
            this.lblStatus.TabIndex = 294;
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
            this.cmbPage.Location = new System.Drawing.Point(720, 414);
            this.cmbPage.Name = "cmbPage";
            this.cmbPage.Size = new System.Drawing.Size(77, 20);
            this.cmbPage.TabIndex = 288;
            this.cmbPage.TabStop = false;
            // 
            // lblPage
            // 
            this.lblPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPage.AutoSize = true;
            this.lblPage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lblPage.Location = new System.Drawing.Point(645, 418);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(69, 12);
            this.lblPage.TabIndex = 293;
            this.lblPage.Text = "페이지 이동";
            // 
            // btnLast
            // 
            this.btnLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnLast.Location = new System.Drawing.Point(995, 411);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(28, 22);
            this.btnLast.TabIndex = 292;
            this.btnLast.TabStop = false;
            this.btnLast.Text = ">|";
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnNext.Location = new System.Drawing.Point(966, 411);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(29, 22);
            this.btnNext.TabIndex = 291;
            this.btnNext.TabStop = false;
            this.btnNext.Text = ">";
            // 
            // btnPrevious
            // 
            this.btnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnPrevious.Location = new System.Drawing.Point(831, 411);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(29, 22);
            this.btnPrevious.TabIndex = 290;
            this.btnPrevious.TabStop = false;
            this.btnPrevious.Text = "<";
            // 
            // btnFirst
            // 
            this.btnFirst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnFirst.Location = new System.Drawing.Point(803, 411);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(28, 22);
            this.btnFirst.TabIndex = 289;
            this.btnFirst.TabStop = false;
            this.btnFirst.Text = "|<";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Red;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(955, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 12);
            this.label2.TabIndex = 316;
            this.label2.Text = "종료된 재료";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Yellow;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(956, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 12);
            this.label1.TabIndex = 315;
            this.label1.Text = "중지된 재료";
            // 
            // RAW_MAT_CD
            // 
            this.RAW_MAT_CD.HeaderText = "원부재료코드";
            this.RAW_MAT_CD.Name = "RAW_MAT_CD";
            this.RAW_MAT_CD.ReadOnly = true;
            this.RAW_MAT_CD.Width = 120;
            // 
            // CHUGJONG_CD
            // 
            this.CHUGJONG_CD.HeaderText = "축종코드";
            this.CHUGJONG_CD.Name = "CHUGJONG_CD";
            this.CHUGJONG_CD.ReadOnly = true;
            this.CHUGJONG_CD.Visible = false;
            // 
            // CHUGJONG_NM
            // 
            this.CHUGJONG_NM.HeaderText = "축종";
            this.CHUGJONG_NM.Name = "CHUGJONG_NM";
            this.CHUGJONG_NM.ReadOnly = true;
            // 
            // CLASS_CD
            // 
            this.CLASS_CD.HeaderText = "분류코드";
            this.CLASS_CD.Name = "CLASS_CD";
            this.CLASS_CD.ReadOnly = true;
            this.CLASS_CD.Visible = false;
            // 
            // CLASS_NM
            // 
            this.CLASS_NM.HeaderText = "분류";
            this.CLASS_NM.Name = "CLASS_NM";
            this.CLASS_NM.ReadOnly = true;
            // 
            // RAW_MAT_NM
            // 
            this.RAW_MAT_NM.HeaderText = "상품명";
            this.RAW_MAT_NM.Name = "RAW_MAT_NM";
            this.RAW_MAT_NM.ReadOnly = true;
            this.RAW_MAT_NM.Width = 220;
            // 
            // LABEL_NM
            // 
            this.LABEL_NM.HeaderText = "라벨명";
            this.LABEL_NM.Name = "LABEL_NM";
            this.LABEL_NM.ReadOnly = true;
            // 
            // SPEC
            // 
            this.SPEC.HeaderText = "규격";
            this.SPEC.Name = "SPEC";
            this.SPEC.ReadOnly = true;
            this.SPEC.Visible = false;
            this.SPEC.Width = 120;
            // 
            // GRADE_CD
            // 
            this.GRADE_CD.HeaderText = "등급코드";
            this.GRADE_CD.Name = "GRADE_CD";
            this.GRADE_CD.ReadOnly = true;
            this.GRADE_CD.Visible = false;
            // 
            // GRADE_NM
            // 
            this.GRADE_NM.HeaderText = "등급";
            this.GRADE_NM.Name = "GRADE_NM";
            this.GRADE_NM.ReadOnly = true;
            this.GRADE_NM.Visible = false;
            // 
            // BOX_AMT
            // 
            this.BOX_AMT.HeaderText = "장입중량";
            this.BOX_AMT.Name = "BOX_AMT";
            this.BOX_AMT.ReadOnly = true;
            // 
            // COUNTRY_CD
            // 
            this.COUNTRY_CD.HeaderText = "원산지코드";
            this.COUNTRY_CD.Name = "COUNTRY_CD";
            this.COUNTRY_CD.ReadOnly = true;
            this.COUNTRY_CD.Visible = false;
            // 
            // COUNTRY_NM
            // 
            this.COUNTRY_NM.HeaderText = "원산지";
            this.COUNTRY_NM.Name = "COUNTRY_NM";
            this.COUNTRY_NM.ReadOnly = true;
            // 
            // RAW_MAT_GUBUN_NM
            // 
            this.RAW_MAT_GUBUN_NM.HeaderText = "원부재료구분명";
            this.RAW_MAT_GUBUN_NM.Name = "RAW_MAT_GUBUN_NM";
            this.RAW_MAT_GUBUN_NM.ReadOnly = true;
            this.RAW_MAT_GUBUN_NM.Width = 130;
            // 
            // RAW_MAT_GUBUN_CD
            // 
            this.RAW_MAT_GUBUN_CD.HeaderText = "원부재료구분코드";
            this.RAW_MAT_GUBUN_CD.Name = "RAW_MAT_GUBUN_CD";
            this.RAW_MAT_GUBUN_CD.ReadOnly = true;
            this.RAW_MAT_GUBUN_CD.Visible = false;
            // 
            // TYPE_NM
            // 
            this.TYPE_NM.HeaderText = "유형";
            this.TYPE_NM.Name = "TYPE_NM";
            this.TYPE_NM.ReadOnly = true;
            this.TYPE_NM.Width = 80;
            // 
            // TYPE_CD
            // 
            this.TYPE_CD.HeaderText = "유형코드";
            this.TYPE_CD.Name = "TYPE_CD";
            this.TYPE_CD.ReadOnly = true;
            // 
            // INPUT_UNIT_NM
            // 
            this.INPUT_UNIT_NM.HeaderText = "입고단위";
            this.INPUT_UNIT_NM.Name = "INPUT_UNIT_NM";
            this.INPUT_UNIT_NM.ReadOnly = true;
            this.INPUT_UNIT_NM.Width = 70;
            // 
            // OUTPUT_UNIT_NM
            // 
            this.OUTPUT_UNIT_NM.HeaderText = "출고단위";
            this.OUTPUT_UNIT_NM.Name = "OUTPUT_UNIT_NM";
            this.OUTPUT_UNIT_NM.ReadOnly = true;
            this.OUTPUT_UNIT_NM.Width = 70;
            // 
            // INPUT_PRICE
            // 
            this.INPUT_PRICE.HeaderText = "입고단가";
            this.INPUT_PRICE.Name = "INPUT_PRICE";
            this.INPUT_PRICE.ReadOnly = true;
            this.INPUT_PRICE.Width = 90;
            // 
            // OUTPUT_PRICE
            // 
            this.OUTPUT_PRICE.HeaderText = "출고단가";
            this.OUTPUT_PRICE.Name = "OUTPUT_PRICE";
            this.OUTPUT_PRICE.ReadOnly = true;
            this.OUTPUT_PRICE.Width = 90;
            // 
            // INPUT_UNIT
            // 
            this.INPUT_UNIT.HeaderText = "입고단위코드";
            this.INPUT_UNIT.Name = "INPUT_UNIT";
            this.INPUT_UNIT.ReadOnly = true;
            this.INPUT_UNIT.Visible = false;
            // 
            // OUTPUT_UNIT
            // 
            this.OUTPUT_UNIT.HeaderText = "출고단위코드";
            this.OUTPUT_UNIT.Name = "OUTPUT_UNIT";
            this.OUTPUT_UNIT.ReadOnly = true;
            this.OUTPUT_UNIT.Visible = false;
            // 
            // BAL_STOCK
            // 
            this.BAL_STOCK.HeaderText = "현재고";
            this.BAL_STOCK.Name = "BAL_STOCK";
            this.BAL_STOCK.ReadOnly = true;
            this.BAL_STOCK.Visible = false;
            // 
            // CUST_NM
            // 
            this.CUST_NM.HeaderText = "거래처명";
            this.CUST_NM.Name = "CUST_NM";
            this.CUST_NM.ReadOnly = true;
            this.CUST_NM.Visible = false;
            // 
            // CUST_CD
            // 
            this.CUST_CD.HeaderText = "거래처코드";
            this.CUST_CD.Name = "CUST_CD";
            this.CUST_CD.ReadOnly = true;
            this.CUST_CD.Visible = false;
            // 
            // CVR_RATIO
            // 
            this.CVR_RATIO.HeaderText = "환산비율";
            this.CVR_RATIO.Name = "CVR_RATIO";
            this.CVR_RATIO.ReadOnly = true;
            this.CVR_RATIO.Visible = false;
            // 
            // VAT_CD
            // 
            this.VAT_CD.HeaderText = "과세구분";
            this.VAT_CD.Name = "VAT_CD";
            this.VAT_CD.ReadOnly = true;
            this.VAT_CD.Visible = false;
            // 
            // pop원부재료검색
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 442);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.cmbPage);
            this.Controls.Add(this.lblPage);
            this.Controls.Add(this.btnLast);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnFirst);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.GridRecord);
            this.Controls.Add(this.txtSrch);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.btnSearch);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "pop원부재료검색";
            this.Text = "원부재료검색";
            this.Load += new System.EventHandler(this.pop원부재료검색_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GridRecord)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView GridRecord;
        public Controls.conTextBox txtSrch;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblTitle;
        private Controls.conLabel lblStatus;
        private Controls.conComboBox cmbPage;
        private System.Windows.Forms.Label lblPage;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn RAW_MAT_CD;
        private System.Windows.Forms.DataGridViewTextBoxColumn CHUGJONG_CD;
        private System.Windows.Forms.DataGridViewTextBoxColumn CHUGJONG_NM;
        private System.Windows.Forms.DataGridViewTextBoxColumn CLASS_CD;
        private System.Windows.Forms.DataGridViewTextBoxColumn CLASS_NM;
        private System.Windows.Forms.DataGridViewTextBoxColumn RAW_MAT_NM;
        private System.Windows.Forms.DataGridViewTextBoxColumn LABEL_NM;
        private System.Windows.Forms.DataGridViewTextBoxColumn SPEC;
        private System.Windows.Forms.DataGridViewTextBoxColumn GRADE_CD;
        private System.Windows.Forms.DataGridViewTextBoxColumn GRADE_NM;
        private System.Windows.Forms.DataGridViewTextBoxColumn BOX_AMT;
        private System.Windows.Forms.DataGridViewTextBoxColumn COUNTRY_CD;
        private System.Windows.Forms.DataGridViewTextBoxColumn COUNTRY_NM;
        private System.Windows.Forms.DataGridViewTextBoxColumn RAW_MAT_GUBUN_NM;
        private System.Windows.Forms.DataGridViewTextBoxColumn RAW_MAT_GUBUN_CD;
        private System.Windows.Forms.DataGridViewTextBoxColumn TYPE_NM;
        private System.Windows.Forms.DataGridViewTextBoxColumn TYPE_CD;
        private System.Windows.Forms.DataGridViewTextBoxColumn INPUT_UNIT_NM;
        private System.Windows.Forms.DataGridViewTextBoxColumn OUTPUT_UNIT_NM;
        private System.Windows.Forms.DataGridViewTextBoxColumn INPUT_PRICE;
        private System.Windows.Forms.DataGridViewTextBoxColumn OUTPUT_PRICE;
        private System.Windows.Forms.DataGridViewTextBoxColumn INPUT_UNIT;
        private System.Windows.Forms.DataGridViewTextBoxColumn OUTPUT_UNIT;
        private System.Windows.Forms.DataGridViewTextBoxColumn BAL_STOCK;
        private System.Windows.Forms.DataGridViewTextBoxColumn CUST_NM;
        private System.Windows.Forms.DataGridViewTextBoxColumn CUST_CD;
        private System.Windows.Forms.DataGridViewTextBoxColumn CVR_RATIO;
        private System.Windows.Forms.DataGridViewTextBoxColumn VAT_CD;
    }
}