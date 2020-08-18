namespace 스마트팩토리.P90_SYS
{
    partial class frm그룹사용자관리
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm그룹사용자관리));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.spCont = new System.Windows.Forms.SplitContainer();
            this.lblSearch = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.GridRecord = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtSrch = new 스마트팩토리.Controls.conTextBox();
            this.butSearch = new System.Windows.Forms.Button();
            this.lblBody = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.grdSetUser = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.lblGrpCode = new 스마트팩토리.Controls.conLabel();
            this.grdGetUser = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.btnDn2 = new System.Windows.Forms.Button();
            this.lblGrpName = new 스마트팩토리.Controls.conLabel();
            this.btnUp2 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.spCont.Panel1.SuspendLayout();
            this.spCont.Panel2.SuspendLayout();
            this.spCont.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridRecord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSetUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdGetUser)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.PaleTurquoise;
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1360, 33);
            this.panel2.TabIndex = 2;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label1.Location = new System.Drawing.Point(10, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 22);
            this.label1.TabIndex = 93;
            this.label1.Text = "그룹사용자관리";
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
            // spCont
            // 
            this.spCont.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.spCont.IsSplitterFixed = true;
            this.spCont.Location = new System.Drawing.Point(0, 39);
            this.spCont.Name = "spCont";
            // 
            // spCont.Panel1
            // 
            this.spCont.Panel1.BackColor = System.Drawing.Color.Lavender;
            this.spCont.Panel1.Controls.Add(this.lblSearch);
            this.spCont.Panel1.Controls.Add(this.label2);
            this.spCont.Panel1.Controls.Add(this.GridRecord);
            this.spCont.Panel1.Controls.Add(this.txtSrch);
            this.spCont.Panel1.Controls.Add(this.butSearch);
            // 
            // spCont.Panel2
            // 
            this.spCont.Panel2.Controls.Add(this.lblBody);
            this.spCont.Panel2.Controls.Add(this.label8);
            this.spCont.Panel2.Controls.Add(this.grdSetUser);
            this.spCont.Panel2.Controls.Add(this.lblGrpCode);
            this.spCont.Panel2.Controls.Add(this.grdGetUser);
            this.spCont.Panel2.Controls.Add(this.label5);
            this.spCont.Panel2.Controls.Add(this.btnDn2);
            this.spCont.Panel2.Controls.Add(this.lblGrpName);
            this.spCont.Panel2.Controls.Add(this.btnUp2);
            this.spCont.Panel2.Controls.Add(this.label9);
            this.spCont.Size = new System.Drawing.Size(1341, 547);
            this.spCont.SplitterDistance = 264;
            this.spCont.SplitterWidth = 1;
            this.spCont.TabIndex = 3;
            // 
            // lblSearch
            // 
            this.lblSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSearch.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblSearch.ForeColor = System.Drawing.Color.Green;
            this.lblSearch.Location = new System.Drawing.Point(39, 190);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(193, 57);
            this.lblSearch.TabIndex = 272;
            this.lblSearch.Text = "Searching ...";
            this.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSearch.Visible = false;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(12, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 22);
            this.label2.TabIndex = 78;
            this.label2.Text = "그룹명";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GridRecord
            // 
            this.GridRecord.AllowUserToAddRows = false;
            this.GridRecord.AllowUserToDeleteRows = false;
            this.GridRecord.AllowUserToOrderColumns = true;
            this.GridRecord.AllowUserToResizeColumns = false;
            this.GridRecord.AllowUserToResizeRows = false;
            this.GridRecord.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.GridRecord.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.GridRecord.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridRecord.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GridRecord.ColumnHeadersHeight = 30;
            this.GridRecord.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.Column4});
            this.GridRecord.EnableHeadersVisualStyles = false;
            this.GridRecord.GridColor = System.Drawing.Color.PowderBlue;
            this.GridRecord.Location = new System.Drawing.Point(12, 41);
            this.GridRecord.Name = "GridRecord";
            this.GridRecord.ReadOnly = true;
            this.GridRecord.RowHeadersVisible = false;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightCyan;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.GridRecord.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.GridRecord.RowTemplate.Height = 23;
            this.GridRecord.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridRecord.Size = new System.Drawing.Size(244, 503);
            this.GridRecord.TabIndex = 11;
            this.GridRecord.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridRecord_CellDoubleClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn1.HeaderText = "그룹명";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 215;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "code";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Visible = false;
            // 
            // txtSrch
            // 
            this.txtSrch._AutoTab = true;
            this.txtSrch._BorderColor = System.Drawing.Color.LightSteelBlue;
            this.txtSrch._FocusedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtSrch._WaterMarkColor = System.Drawing.Color.Gray;
            this.txtSrch._WaterMarkText = "";
            this.txtSrch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSrch.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtSrch.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txtSrch.Location = new System.Drawing.Point(79, 13);
            this.txtSrch.MaxLength = 50;
            this.txtSrch.Name = "txtSrch";
            this.txtSrch.Size = new System.Drawing.Size(138, 22);
            this.txtSrch.TabIndex = 0;
            // 
            // butSearch
            // 
            this.butSearch.BackColor = System.Drawing.Color.Transparent;
            this.butSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.butSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butSearch.FlatAppearance.BorderSize = 0;
            this.butSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.SkyBlue;
            this.butSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butSearch.Image = ((System.Drawing.Image)(resources.GetObject("butSearch.Image")));
            this.butSearch.Location = new System.Drawing.Point(223, 6);
            this.butSearch.Name = "butSearch";
            this.butSearch.Size = new System.Drawing.Size(33, 33);
            this.butSearch.TabIndex = 10;
            this.butSearch.Tag = "검색";
            this.butSearch.UseVisualStyleBackColor = false;
            this.butSearch.Click += new System.EventHandler(this.butSearch_Click);
            // 
            // lblBody
            // 
            this.lblBody.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblBody.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBody.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblBody.ForeColor = System.Drawing.Color.Green;
            this.lblBody.Location = new System.Drawing.Point(430, 190);
            this.lblBody.Name = "lblBody";
            this.lblBody.Size = new System.Drawing.Size(313, 57);
            this.lblBody.TabIndex = 273;
            this.lblBody.Text = "Loading ...";
            this.lblBody.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblBody.Visible = false;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.PowderBlue;
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label8.Location = new System.Drawing.Point(13, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 20);
            this.label8.TabIndex = 122;
            this.label8.Text = "소속 사용자";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grdSetUser
            // 
            this.grdSetUser.AllowUserToAddRows = false;
            this.grdSetUser.AllowUserToDeleteRows = false;
            this.grdSetUser.AllowUserToOrderColumns = true;
            this.grdSetUser.AllowUserToResizeColumns = false;
            this.grdSetUser.AllowUserToResizeRows = false;
            this.grdSetUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grdSetUser.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.grdSetUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.PowderBlue;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdSetUser.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.grdSetUser.ColumnHeadersHeight = 30;
            this.grdSetUser.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn7,
            this.Column1,
            this.Column3,
            this.Column7,
            this.Column8,
            this.Column6});
            this.grdSetUser.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.grdSetUser.EnableHeadersVisualStyles = false;
            this.grdSetUser.GridColor = System.Drawing.Color.PowderBlue;
            this.grdSetUser.Location = new System.Drawing.Point(15, 66);
            this.grdSetUser.Name = "grdSetUser";
            this.grdSetUser.RowHeadersVisible = false;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.LightCyan;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.grdSetUser.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.grdSetUser.RowTemplate.Height = 23;
            this.grdSetUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.grdSetUser.Size = new System.Drawing.Size(527, 477);
            this.grdSetUser.TabIndex = 1;
            this.grdSetUser.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grdSetUser_ColumnHeaderMouseClick);
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTextBoxColumn7.HeaderText = "코드";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "아이디";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "사용자명";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 150;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "쓰기";
            this.Column7.Name = "Column7";
            this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column7.Width = 50;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "출력";
            this.Column8.Name = "Column8";
            this.Column8.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column8.Width = 50;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "[ ]";
            this.Column6.Name = "Column6";
            this.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column6.Width = 50;
            // 
            // lblGrpCode
            // 
            this.lblGrpCode._BorderColor = System.Drawing.Color.LightGray;
            this.lblGrpCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblGrpCode.Location = new System.Drawing.Point(136, 13);
            this.lblGrpCode.Name = "lblGrpCode";
            this.lblGrpCode.Size = new System.Drawing.Size(27, 22);
            this.lblGrpCode.TabIndex = 117;
            this.lblGrpCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblGrpCode.Visible = false;
            // 
            // grdGetUser
            // 
            this.grdGetUser.AllowUserToAddRows = false;
            this.grdGetUser.AllowUserToDeleteRows = false;
            this.grdGetUser.AllowUserToOrderColumns = true;
            this.grdGetUser.AllowUserToResizeColumns = false;
            this.grdGetUser.AllowUserToResizeRows = false;
            this.grdGetUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grdGetUser.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.grdGetUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdGetUser.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.grdGetUser.ColumnHeadersHeight = 30;
            this.grdGetUser.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn10,
            this.Column2,
            this.Column5,
            this.Column9,
            this.Column10,
            this.dataGridViewCheckBoxColumn1});
            this.grdGetUser.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.grdGetUser.EnableHeadersVisualStyles = false;
            this.grdGetUser.GridColor = System.Drawing.Color.PowderBlue;
            this.grdGetUser.Location = new System.Drawing.Point(626, 66);
            this.grdGetUser.Name = "grdGetUser";
            this.grdGetUser.RowHeadersVisible = false;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.LightCyan;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            this.grdGetUser.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.grdGetUser.RowTemplate.Height = 23;
            this.grdGetUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.grdGetUser.Size = new System.Drawing.Size(428, 477);
            this.grdGetUser.TabIndex = 2;
            this.grdGetUser.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grdGetUser_ColumnHeaderMouseClick);
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.HeaderText = "코드";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "아이디";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "사용자명";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 150;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "쓰기";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Visible = false;
            this.Column9.Width = 50;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "출력";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Visible = false;
            this.Column10.Width = 50;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.HeaderText = "[ ]";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewCheckBoxColumn1.Width = 50;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.PowderBlue;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(13, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 22);
            this.label5.TabIndex = 81;
            this.label5.Text = "그룹명";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDn2
            // 
            this.btnDn2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnDn2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDn2.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnDn2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnDn2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDn2.ForeColor = System.Drawing.Color.DodgerBlue;
            this.btnDn2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDn2.Location = new System.Drawing.Point(548, 298);
            this.btnDn2.Name = "btnDn2";
            this.btnDn2.Size = new System.Drawing.Size(72, 29);
            this.btnDn2.TabIndex = 4;
            this.btnDn2.Tag = "저장";
            this.btnDn2.Text = "해제 ▷";
            this.btnDn2.UseVisualStyleBackColor = false;
            this.btnDn2.Click += new System.EventHandler(this.btnDn2_Click);
            // 
            // lblGrpName
            // 
            this.lblGrpName._BorderColor = System.Drawing.Color.LightGray;
            this.lblGrpName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblGrpName.Location = new System.Drawing.Point(114, 13);
            this.lblGrpName.Name = "lblGrpName";
            this.lblGrpName.Size = new System.Drawing.Size(329, 22);
            this.lblGrpName.TabIndex = 0;
            this.lblGrpName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnUp2
            // 
            this.btnUp2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnUp2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUp2.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnUp2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnUp2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUp2.ForeColor = System.Drawing.Color.DodgerBlue;
            this.btnUp2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnUp2.Location = new System.Drawing.Point(548, 263);
            this.btnUp2.Name = "btnUp2";
            this.btnUp2.Size = new System.Drawing.Size(72, 29);
            this.btnUp2.TabIndex = 3;
            this.btnUp2.Tag = "저장";
            this.btnUp2.Text = "◁ 적용";
            this.btnUp2.UseVisualStyleBackColor = false;
            this.btnUp2.Click += new System.EventHandler(this.btnUp2_Click);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label9.Location = new System.Drawing.Point(624, 45);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 20);
            this.label9.TabIndex = 120;
            this.label9.Text = "미소속 사용자";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frm그룹사용자관리
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1360, 598);
            this.Controls.Add(this.spCont);
            this.Controls.Add(this.panel2);
            this.KeyPreview = true;
            this.Name = "frm그룹사용자관리";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frm그룹사용자관리";
            this.Load += new System.EventHandler(this.frm그룹사용자관리_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.spCont.Panel1.ResumeLayout(false);
            this.spCont.Panel2.ResumeLayout(false);
            this.spCont.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridRecord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSetUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdGetUser)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.SplitContainer spCont;
        private System.Windows.Forms.DataGridView GridRecord;
        private 스마트팩토리.Controls.conTextBox txtSrch;
        private System.Windows.Forms.Button butSearch;
        private System.Windows.Forms.Label label2;
        private 스마트팩토리.Controls.conLabel lblGrpCode;
        private 스마트팩토리.Controls.conLabel lblGrpName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnUp2;
        private System.Windows.Forms.DataGridView grdGetUser;
        private System.Windows.Forms.DataGridView grdSetUser;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnDn2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Label lblBody;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column7;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column8;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
    }
}