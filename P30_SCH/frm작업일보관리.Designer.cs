namespace 스마트팩토리.P30_SCH
{
    partial class frm작업일보관리
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm작업일보관리));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSrch = new System.Windows.Forms.Button();
            this.txtcustSrch = new 스마트팩토리.Controls.conTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btncustSrch = new System.Windows.Forms.Button();
            this.txtitemSrch = new 스마트팩토리.Controls.conTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnitemSrch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.dTP1 = new System.Windows.Forms.DateTimePicker();
            this.workGrid = new System.Windows.Forms.DataGridView();
            this.LOT_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.W_INST_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEM_NM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SPEC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CUST_NM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INST_AMT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INPUT_YN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.W_STEP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CHECK_YN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_item_cd = new 스마트팩토리.Controls.conTextBox();
            this.txt_item_nm = new 스마트팩토리.Controls.conTextBox();
            this.txt_char_amt = new 스마트팩토리.Controls.conTextBox();
            this.txt_pack_amt = new 스마트팩토리.Controls.conTextBox();
            this.txt_cust_cd = new 스마트팩토리.Controls.conTextBox();
            this.txt_cust_nm = new 스마트팩토리.Controls.conTextBox();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.workGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.PaleTurquoise;
            this.panel2.Controls.Add(this.btnSrch);
            this.panel2.Controls.Add(this.txtcustSrch);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.btncustSrch);
            this.panel2.Controls.Add(this.txtitemSrch);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.btnitemSrch);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.dTP1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1360, 33);
            this.panel2.TabIndex = 18;
            // 
            // btnSrch
            // 
            this.btnSrch.Location = new System.Drawing.Point(1015, 5);
            this.btnSrch.Name = "btnSrch";
            this.btnSrch.Size = new System.Drawing.Size(75, 23);
            this.btnSrch.TabIndex = 404;
            this.btnSrch.Text = "검색";
            this.btnSrch.UseVisualStyleBackColor = true;
            this.btnSrch.Click += new System.EventHandler(this.btnSrch_Click);
            // 
            // txtcustSrch
            // 
            this.txtcustSrch._AutoTab = true;
            this.txtcustSrch._BorderColor = System.Drawing.Color.LightSteelBlue;
            this.txtcustSrch._FocusedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtcustSrch._WaterMarkColor = System.Drawing.Color.Gray;
            this.txtcustSrch._WaterMarkText = "";
            this.txtcustSrch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtcustSrch.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtcustSrch.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txtcustSrch.Location = new System.Drawing.Point(737, 5);
            this.txtcustSrch.MaxLength = 20;
            this.txtcustSrch.Name = "txtcustSrch";
            this.txtcustSrch.Size = new System.Drawing.Size(115, 22);
            this.txtcustSrch.TabIndex = 403;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(858, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 23);
            this.label4.TabIndex = 402;
            this.label4.Text = "구매처검색";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(597, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 23);
            this.label3.TabIndex = 401;
            this.label3.Text = "제품검색";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btncustSrch
            // 
            this.btncustSrch.BackColor = System.Drawing.Color.Transparent;
            this.btncustSrch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btncustSrch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btncustSrch.FlatAppearance.BorderSize = 0;
            this.btncustSrch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.SkyBlue;
            this.btncustSrch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncustSrch.Image = ((System.Drawing.Image)(resources.GetObject("btncustSrch.Image")));
            this.btncustSrch.Location = new System.Drawing.Point(942, 2);
            this.btncustSrch.Name = "btncustSrch";
            this.btncustSrch.Size = new System.Drawing.Size(33, 30);
            this.btncustSrch.TabIndex = 399;
            this.btncustSrch.Tag = "검색";
            this.btncustSrch.UseVisualStyleBackColor = false;
            this.btncustSrch.Click += new System.EventHandler(this.btncustSrch_Click);
            // 
            // txtitemSrch
            // 
            this.txtitemSrch._AutoTab = true;
            this.txtitemSrch._BorderColor = System.Drawing.Color.LightSteelBlue;
            this.txtitemSrch._FocusedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtitemSrch._WaterMarkColor = System.Drawing.Color.Gray;
            this.txtitemSrch._WaterMarkText = "";
            this.txtitemSrch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtitemSrch.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtitemSrch.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txtitemSrch.Location = new System.Drawing.Point(476, 4);
            this.txtitemSrch.MaxLength = 20;
            this.txtitemSrch.Name = "txtitemSrch";
            this.txtitemSrch.Size = new System.Drawing.Size(115, 22);
            this.txtitemSrch.TabIndex = 398;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(131, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 23);
            this.label2.TabIndex = 379;
            this.label2.Text = "날짜조회";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnitemSrch
            // 
            this.btnitemSrch.BackColor = System.Drawing.Color.Transparent;
            this.btnitemSrch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnitemSrch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnitemSrch.FlatAppearance.BorderSize = 0;
            this.btnitemSrch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.SkyBlue;
            this.btnitemSrch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnitemSrch.Image = ((System.Drawing.Image)(resources.GetObject("btnitemSrch.Image")));
            this.btnitemSrch.Location = new System.Drawing.Point(681, 1);
            this.btnitemSrch.Name = "btnitemSrch";
            this.btnitemSrch.Size = new System.Drawing.Size(33, 30);
            this.btnitemSrch.TabIndex = 378;
            this.btnitemSrch.Tag = "검색";
            this.btnitemSrch.UseVisualStyleBackColor = false;
            this.btnitemSrch.Click += new System.EventHandler(this.btnitemSrch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label1.Location = new System.Drawing.Point(10, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 22);
            this.label1.TabIndex = 93;
            this.label1.Text = "작업일보";
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
            this.btnClose.Location = new System.Drawing.Point(1292, 1);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(65, 29);
            this.btnClose.TabIndex = 10;
            this.btnClose.Tag = "종료";
            this.btnClose.Text = "닫기";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dTP1
            // 
            this.dTP1.Checked = false;
            this.dTP1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dTP1.Location = new System.Drawing.Point(215, 5);
            this.dTP1.Name = "dTP1";
            this.dTP1.Size = new System.Drawing.Size(100, 21);
            this.dTP1.TabIndex = 376;
            this.dTP1.Value = new System.DateTime(2018, 10, 8, 0, 0, 0, 0);
            // 
            // workGrid
            // 
            this.workGrid.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.workGrid.AllowUserToAddRows = false;
            this.workGrid.AllowUserToDeleteRows = false;
            this.workGrid.AllowUserToOrderColumns = true;
            this.workGrid.AllowUserToResizeColumns = false;
            this.workGrid.AllowUserToResizeRows = false;
            this.workGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.workGrid.BackgroundColor = System.Drawing.Color.White;
            this.workGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.PowderBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.workGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.workGrid.ColumnHeadersHeight = 40;
            this.workGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LOT_NO,
            this.W_INST_DATE,
            this.ITEM_NM,
            this.SPEC,
            this.CUST_NM,
            this.INST_AMT,
            this.INPUT_YN,
            this.W_STEP,
            this.CHECK_YN});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("굴림", 11F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.workGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.workGrid.EnableHeadersVisualStyles = false;
            this.workGrid.GridColor = System.Drawing.Color.PowderBlue;
            this.workGrid.Location = new System.Drawing.Point(0, 34);
            this.workGrid.Name = "workGrid";
            this.workGrid.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.workGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.workGrid.RowHeadersVisible = false;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.LightCyan;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.workGrid.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.workGrid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("굴림", 10F);
            this.workGrid.RowTemplate.Height = 30;
            this.workGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.workGrid.Size = new System.Drawing.Size(1357, 677);
            this.workGrid.TabIndex = 379;
            // 
            // LOT_NO
            // 
            this.LOT_NO.HeaderText = "LOT_NO";
            this.LOT_NO.Name = "LOT_NO";
            this.LOT_NO.ReadOnly = true;
            this.LOT_NO.Width = 200;
            // 
            // W_INST_DATE
            // 
            this.W_INST_DATE.HeaderText = "작업일자";
            this.W_INST_DATE.Name = "W_INST_DATE";
            this.W_INST_DATE.ReadOnly = true;
            this.W_INST_DATE.Width = 120;
            // 
            // ITEM_NM
            // 
            this.ITEM_NM.HeaderText = "제품명";
            this.ITEM_NM.Name = "ITEM_NM";
            this.ITEM_NM.ReadOnly = true;
            this.ITEM_NM.Width = 220;
            // 
            // SPEC
            // 
            this.SPEC.HeaderText = "규격";
            this.SPEC.Name = "SPEC";
            this.SPEC.ReadOnly = true;
            this.SPEC.Width = 220;
            // 
            // CUST_NM
            // 
            this.CUST_NM.HeaderText = "납품처";
            this.CUST_NM.Name = "CUST_NM";
            this.CUST_NM.ReadOnly = true;
            // 
            // INST_AMT
            // 
            this.INST_AMT.HeaderText = "수량";
            this.INST_AMT.Name = "INST_AMT";
            this.INST_AMT.ReadOnly = true;
            this.INST_AMT.Width = 80;
            // 
            // INPUT_YN
            // 
            this.INPUT_YN.HeaderText = "투입여부";
            this.INPUT_YN.Name = "INPUT_YN";
            this.INPUT_YN.ReadOnly = true;
            this.INPUT_YN.Width = 150;
            // 
            // W_STEP
            // 
            this.W_STEP.HeaderText = "공정현황";
            this.W_STEP.Name = "W_STEP";
            this.W_STEP.ReadOnly = true;
            // 
            // CHECK_YN
            // 
            this.CHECK_YN.HeaderText = "공정여부";
            this.CHECK_YN.Name = "CHECK_YN";
            this.CHECK_YN.ReadOnly = true;
            this.CHECK_YN.Width = 150;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "LOT_NO";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 200;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "작업일자";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 120;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "제품명";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 220;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "규격";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 220;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "납품처";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "수량";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 80;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "투입여부";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Width = 150;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "공정현황";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "공정여부";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.Width = 150;
            // 
            // txt_item_cd
            // 
            this.txt_item_cd._AutoTab = true;
            this.txt_item_cd._BorderColor = System.Drawing.Color.LightSteelBlue;
            this.txt_item_cd._FocusedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txt_item_cd._WaterMarkColor = System.Drawing.Color.Gray;
            this.txt_item_cd._WaterMarkText = "";
            this.txt_item_cd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_item_cd.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_item_cd.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txt_item_cd.Location = new System.Drawing.Point(48, 117);
            this.txt_item_cd.MaxLength = 20;
            this.txt_item_cd.Name = "txt_item_cd";
            this.txt_item_cd.Size = new System.Drawing.Size(115, 22);
            this.txt_item_cd.TabIndex = 405;
            this.txt_item_cd.Visible = false;
            // 
            // txt_item_nm
            // 
            this.txt_item_nm._AutoTab = true;
            this.txt_item_nm._BorderColor = System.Drawing.Color.LightSteelBlue;
            this.txt_item_nm._FocusedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txt_item_nm._WaterMarkColor = System.Drawing.Color.Gray;
            this.txt_item_nm._WaterMarkText = "";
            this.txt_item_nm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_item_nm.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_item_nm.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txt_item_nm.Location = new System.Drawing.Point(48, 154);
            this.txt_item_nm.MaxLength = 20;
            this.txt_item_nm.Name = "txt_item_nm";
            this.txt_item_nm.Size = new System.Drawing.Size(115, 22);
            this.txt_item_nm.TabIndex = 406;
            this.txt_item_nm.Visible = false;
            // 
            // txt_char_amt
            // 
            this.txt_char_amt._AutoTab = true;
            this.txt_char_amt._BorderColor = System.Drawing.Color.LightSteelBlue;
            this.txt_char_amt._FocusedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txt_char_amt._WaterMarkColor = System.Drawing.Color.Gray;
            this.txt_char_amt._WaterMarkText = "";
            this.txt_char_amt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_char_amt.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_char_amt.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txt_char_amt.Location = new System.Drawing.Point(48, 182);
            this.txt_char_amt.MaxLength = 20;
            this.txt_char_amt.Name = "txt_char_amt";
            this.txt_char_amt.Size = new System.Drawing.Size(115, 22);
            this.txt_char_amt.TabIndex = 407;
            this.txt_char_amt.Visible = false;
            // 
            // txt_pack_amt
            // 
            this.txt_pack_amt._AutoTab = true;
            this.txt_pack_amt._BorderColor = System.Drawing.Color.LightSteelBlue;
            this.txt_pack_amt._FocusedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txt_pack_amt._WaterMarkColor = System.Drawing.Color.Gray;
            this.txt_pack_amt._WaterMarkText = "";
            this.txt_pack_amt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_pack_amt.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_pack_amt.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txt_pack_amt.Location = new System.Drawing.Point(315, 117);
            this.txt_pack_amt.MaxLength = 20;
            this.txt_pack_amt.Name = "txt_pack_amt";
            this.txt_pack_amt.Size = new System.Drawing.Size(115, 22);
            this.txt_pack_amt.TabIndex = 405;
            this.txt_pack_amt.Visible = false;
            // 
            // txt_cust_cd
            // 
            this.txt_cust_cd._AutoTab = true;
            this.txt_cust_cd._BorderColor = System.Drawing.Color.LightSteelBlue;
            this.txt_cust_cd._FocusedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txt_cust_cd._WaterMarkColor = System.Drawing.Color.Gray;
            this.txt_cust_cd._WaterMarkText = "";
            this.txt_cust_cd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_cust_cd.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_cust_cd.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txt_cust_cd.Location = new System.Drawing.Point(315, 145);
            this.txt_cust_cd.MaxLength = 20;
            this.txt_cust_cd.Name = "txt_cust_cd";
            this.txt_cust_cd.Size = new System.Drawing.Size(115, 22);
            this.txt_cust_cd.TabIndex = 408;
            this.txt_cust_cd.Visible = false;
            // 
            // txt_cust_nm
            // 
            this.txt_cust_nm._AutoTab = true;
            this.txt_cust_nm._BorderColor = System.Drawing.Color.LightSteelBlue;
            this.txt_cust_nm._FocusedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txt_cust_nm._WaterMarkColor = System.Drawing.Color.Gray;
            this.txt_cust_nm._WaterMarkText = "";
            this.txt_cust_nm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_cust_nm.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_cust_nm.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txt_cust_nm.Location = new System.Drawing.Point(315, 173);
            this.txt_cust_nm.MaxLength = 20;
            this.txt_cust_nm.Name = "txt_cust_nm";
            this.txt_cust_nm.Size = new System.Drawing.Size(115, 22);
            this.txt_cust_nm.TabIndex = 409;
            this.txt_cust_nm.Visible = false;
            // 
            // frm작업일보관리
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1360, 726);
            this.Controls.Add(this.txt_cust_nm);
            this.Controls.Add(this.txt_cust_cd);
            this.Controls.Add(this.txt_pack_amt);
            this.Controls.Add(this.txt_char_amt);
            this.Controls.Add(this.txt_item_nm);
            this.Controls.Add(this.txt_item_cd);
            this.Controls.Add(this.workGrid);
            this.Controls.Add(this.panel2);
            this.Name = "frm작업일보관리";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frm작업일보관리";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.workGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnitemSrch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DateTimePicker dTP1;
        private System.Windows.Forms.DataGridView workGrid;
        private Controls.conTextBox txtcustSrch;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btncustSrch;
        private Controls.conTextBox txtitemSrch;
        private System.Windows.Forms.Button btnSrch;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOT_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn W_INST_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEM_NM;
        private System.Windows.Forms.DataGridViewTextBoxColumn SPEC;
        private System.Windows.Forms.DataGridViewTextBoxColumn CUST_NM;
        private System.Windows.Forms.DataGridViewTextBoxColumn INST_AMT;
        private System.Windows.Forms.DataGridViewTextBoxColumn INPUT_YN;
        private System.Windows.Forms.DataGridViewTextBoxColumn W_STEP;
        private System.Windows.Forms.DataGridViewTextBoxColumn CHECK_YN;
        private Controls.conTextBox txt_item_cd;
        private Controls.conTextBox txt_item_nm;
        private Controls.conTextBox txt_char_amt;
        private Controls.conTextBox txt_pack_amt;
        private Controls.conTextBox txt_cust_cd;
        private Controls.conTextBox txt_cust_nm;
    }
}