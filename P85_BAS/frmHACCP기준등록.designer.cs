namespace 스마트팩토리.P85_BAS
{
    partial class frmHACCP기준등록
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHACCP기준등록));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.lbl_input_gbn = new 스마트팩토리.Controls.conTextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmb_cd = new 스마트팩토리.Controls.conComboBox();
            this.txt_nm = new System.Windows.Forms.TextBox();
            this.txt_cd = new System.Windows.Forms.TextBox();
            this.chk_yn = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmb_cd2 = new 스마트팩토리.Controls.conComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txt_srch = new 스마트팩토리.Controls.conTextBox();
            this.dataChkGrid = new System.Windows.Forms.DataGridView();
            this.FLOW_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CHK_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CHK_ORD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CHK_NM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FLOW_NM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FLOW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataChkGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.Controls.Add(this.label18);
            this.panel2.Controls.Add(this.lbl_input_gbn);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.btnNew);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1360, 714);
            this.panel2.TabIndex = 10;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label18.Font = new System.Drawing.Font("굴림", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label18.Location = new System.Drawing.Point(13, 7);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(199, 24);
            this.label18.TabIndex = 386;
            this.label18.Text = "HACCP 기준등록";
            // 
            // lbl_input_gbn
            // 
            this.lbl_input_gbn._AutoTab = true;
            this.lbl_input_gbn._BorderColor = System.Drawing.Color.LightSteelBlue;
            this.lbl_input_gbn._FocusedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lbl_input_gbn._WaterMarkColor = System.Drawing.Color.Gray;
            this.lbl_input_gbn._WaterMarkText = "";
            this.lbl_input_gbn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lbl_input_gbn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_input_gbn.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_input_gbn.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.lbl_input_gbn.Location = new System.Drawing.Point(691, 9);
            this.lbl_input_gbn.MaxLength = 6;
            this.lbl_input_gbn.Name = "lbl_input_gbn";
            this.lbl_input_gbn.Size = new System.Drawing.Size(166, 22);
            this.lbl_input_gbn.TabIndex = 385;
            this.lbl_input_gbn.Visible = false;
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
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.Transparent;
            this.btnNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnNew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNew.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnNew.FlatAppearance.BorderSize = 0;
            this.btnNew.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.ForeColor = System.Drawing.Color.DodgerBlue;
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnNew.Location = new System.Drawing.Point(877, 3);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(65, 29);
            this.btnNew.TabIndex = 20;
            this.btnNew.Tag = "추가";
            this.btnNew.Text = "신규";
            this.btnNew.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
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
            this.btnSave.Location = new System.Drawing.Point(948, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(65, 29);
            this.btnSave.TabIndex = 0;
            this.btnSave.Tag = "저장";
            this.btnSave.Text = "저장";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.cmb_cd);
            this.panel1.Controls.Add(this.txt_nm);
            this.panel1.Controls.Add(this.txt_cd);
            this.panel1.Controls.Add(this.chk_yn);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cmb_cd2);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.txt_srch);
            this.panel1.Controls.Add(this.dataChkGrid);
            this.panel1.Location = new System.Drawing.Point(0, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1360, 678);
            this.panel1.TabIndex = 10;
            // 
            // cmb_cd
            // 
            this.cmb_cd._BorderColor = System.Drawing.Color.Transparent;
            this.cmb_cd._FocusedBackColor = System.Drawing.Color.White;
            this.cmb_cd.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cmb_cd.FormattingEnabled = true;
            this.cmb_cd.Location = new System.Drawing.Point(175, 40);
            this.cmb_cd.Name = "cmb_cd";
            this.cmb_cd.Size = new System.Drawing.Size(116, 20);
            this.cmb_cd.TabIndex = 376;
            // 
            // txt_nm
            // 
            this.txt_nm.Location = new System.Drawing.Point(175, 142);
            this.txt_nm.Name = "txt_nm";
            this.txt_nm.Size = new System.Drawing.Size(403, 21);
            this.txt_nm.TabIndex = 375;
            // 
            // txt_cd
            // 
            this.txt_cd.Location = new System.Drawing.Point(175, 90);
            this.txt_cd.Name = "txt_cd";
            this.txt_cd.Size = new System.Drawing.Size(116, 21);
            this.txt_cd.TabIndex = 374;
            // 
            // chk_yn
            // 
            this.chk_yn.AutoSize = true;
            this.chk_yn.Location = new System.Drawing.Point(175, 192);
            this.chk_yn.Name = "chk_yn";
            this.chk_yn.Size = new System.Drawing.Size(48, 16);
            this.chk_yn.TabIndex = 372;
            this.chk_yn.Text = "사용";
            this.chk_yn.UseVisualStyleBackColor = true;
            this.chk_yn.CheckedChanged += new System.EventHandler(this.chk_yn_CheckedChanged);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.LightGray;
            this.label5.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(35, 187);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 23);
            this.label5.TabIndex = 371;
            this.label5.Text = "사용여부";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.LightGray;
            this.label4.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(35, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 23);
            this.label4.TabIndex = 370;
            this.label4.Text = "검사항목";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.LightGray;
            this.label3.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(35, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 23);
            this.label3.TabIndex = 369;
            this.label3.Text = "검사항목코드";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.LightGray;
            this.label2.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(35, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 23);
            this.label2.TabIndex = 368;
            this.label2.Text = "공정코드선택";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmb_cd2
            // 
            this.cmb_cd2._BorderColor = System.Drawing.Color.Transparent;
            this.cmb_cd2._FocusedBackColor = System.Drawing.Color.White;
            this.cmb_cd2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cmb_cd2.FormattingEnabled = true;
            this.cmb_cd2.Location = new System.Drawing.Point(883, 23);
            this.cmb_cd2.Name = "cmb_cd2";
            this.cmb_cd2.Size = new System.Drawing.Size(116, 20);
            this.cmb_cd2.TabIndex = 367;
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
            this.btnSearch.Location = new System.Drawing.Point(1168, 16);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(33, 33);
            this.btnSearch.TabIndex = 366;
            this.btnSearch.Tag = "검색";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
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
            this.txt_srch.Location = new System.Drawing.Point(1005, 21);
            this.txt_srch.MaxLength = 20;
            this.txt_srch.Name = "txt_srch";
            this.txt_srch.Size = new System.Drawing.Size(157, 24);
            this.txt_srch.TabIndex = 365;
            // 
            // dataChkGrid
            // 
            this.dataChkGrid.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dataChkGrid.AllowUserToAddRows = false;
            this.dataChkGrid.AllowUserToDeleteRows = false;
            this.dataChkGrid.AllowUserToOrderColumns = true;
            this.dataChkGrid.AllowUserToResizeColumns = false;
            this.dataChkGrid.AllowUserToResizeRows = false;
            this.dataChkGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataChkGrid.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataChkGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("굴림", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataChkGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataChkGrid.ColumnHeadersHeight = 30;
            this.dataChkGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FLOW_CD,
            this.CHK_CD,
            this.CHK_ORD,
            this.CHK_NM,
            this.FLOW_NM,
            this.FLOW});
            this.dataChkGrid.EnableHeadersVisualStyles = false;
            this.dataChkGrid.GridColor = System.Drawing.Color.PowderBlue;
            this.dataChkGrid.Location = new System.Drawing.Point(691, 61);
            this.dataChkGrid.Name = "dataChkGrid";
            this.dataChkGrid.ReadOnly = true;
            this.dataChkGrid.RowHeadersVisible = false;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightCyan;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.dataChkGrid.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataChkGrid.RowTemplate.Height = 23;
            this.dataChkGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataChkGrid.Size = new System.Drawing.Size(643, 581);
            this.dataChkGrid.TabIndex = 364;
            this.dataChkGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataChkGrid_CellDoubleClick);
            // 
            // FLOW_CD
            // 
            this.FLOW_CD.HeaderText = "No";
            this.FLOW_CD.Name = "FLOW_CD";
            this.FLOW_CD.ReadOnly = true;
            this.FLOW_CD.Width = 70;
            // 
            // CHK_CD
            // 
            this.CHK_CD.HeaderText = "공정명";
            this.CHK_CD.Name = "CHK_CD";
            this.CHK_CD.ReadOnly = true;
            this.CHK_CD.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CHK_CD.Width = 160;
            // 
            // CHK_ORD
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.CHK_ORD.DefaultCellStyle = dataGridViewCellStyle2;
            this.CHK_ORD.HeaderText = "기준코드";
            this.CHK_ORD.Name = "CHK_ORD";
            this.CHK_ORD.ReadOnly = true;
            this.CHK_ORD.Width = 160;
            // 
            // CHK_NM
            // 
            this.CHK_NM.HeaderText = "기준명";
            this.CHK_NM.Name = "CHK_NM";
            this.CHK_NM.ReadOnly = true;
            this.CHK_NM.Width = 250;
            // 
            // FLOW_NM
            // 
            this.FLOW_NM.HeaderText = "Column1";
            this.FLOW_NM.Name = "FLOW_NM";
            this.FLOW_NM.ReadOnly = true;
            this.FLOW_NM.Visible = false;
            // 
            // FLOW
            // 
            this.FLOW.HeaderText = "CHK";
            this.FLOW.Name = "FLOW";
            this.FLOW.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "No";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 80;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn2.HeaderText = "공정명";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTextBoxColumn3.HeaderText = "기준코드";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "기준명";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 200;
            // 
            // frmHACCP기준등록
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1360, 714);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "frmHACCP기준등록";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmHACCP기준등록";
            this.Load += new System.EventHandler(this.frmHACCP기준등록_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataChkGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txt_nm;
        private System.Windows.Forms.TextBox txt_cd;
        private System.Windows.Forms.CheckBox chk_yn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private Controls.conComboBox cmb_cd2;
        private System.Windows.Forms.Button btnSearch;
        private Controls.conTextBox txt_srch;
        private System.Windows.Forms.DataGridView dataChkGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private Controls.conComboBox cmb_cd;
        private Controls.conTextBox lbl_input_gbn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FLOW_CD;
        private System.Windows.Forms.DataGridViewTextBoxColumn CHK_CD;
        private System.Windows.Forms.DataGridViewTextBoxColumn CHK_ORD;
        private System.Windows.Forms.DataGridViewTextBoxColumn CHK_NM;
        private System.Windows.Forms.DataGridViewTextBoxColumn FLOW_NM;
        private System.Windows.Forms.DataGridViewTextBoxColumn FLOW;
        private System.Windows.Forms.Label label18;
    }
}