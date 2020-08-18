namespace 스마트팩토리.P90_SYS
{
    partial class frm공지사항
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm공지사항));
            this.noticeGrid = new System.Windows.Forms.DataGridView();
            this.SEQ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CONTENT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INSTAFF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TITLE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INTIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbl = new 스마트팩토리.Controls.conTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lbl_flow_cd = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dTP1 = new System.Windows.Forms.DateTimePicker();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnCustSrch2 = new System.Windows.Forms.Button();
            this.txtSrch = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.noticeGrid)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // noticeGrid
            // 
            this.noticeGrid.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.noticeGrid.AllowUserToAddRows = false;
            this.noticeGrid.AllowUserToDeleteRows = false;
            this.noticeGrid.AllowUserToOrderColumns = true;
            this.noticeGrid.AllowUserToResizeColumns = false;
            this.noticeGrid.AllowUserToResizeRows = false;
            this.noticeGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.noticeGrid.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.noticeGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("굴림", 11F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.noticeGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.noticeGrid.ColumnHeadersHeight = 40;
            this.noticeGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SEQ,
            this.CONTENT,
            this.INSTAFF,
            this.TITLE,
            this.INTIME});
            this.noticeGrid.EnableHeadersVisualStyles = false;
            this.noticeGrid.GridColor = System.Drawing.Color.PowderBlue;
            this.noticeGrid.Location = new System.Drawing.Point(8, 45);
            this.noticeGrid.Name = "noticeGrid";
            this.noticeGrid.ReadOnly = true;
            this.noticeGrid.RowHeadersVisible = false;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightCyan;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.noticeGrid.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.noticeGrid.RowTemplate.Height = 23;
            this.noticeGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.noticeGrid.Size = new System.Drawing.Size(472, 658);
            this.noticeGrid.TabIndex = 333;
            this.noticeGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.noticeGrid_CellDoubleClick);
            // 
            // SEQ
            // 
            this.SEQ.HeaderText = "No";
            this.SEQ.Name = "SEQ";
            this.SEQ.ReadOnly = true;
            this.SEQ.Width = 40;
            // 
            // CONTENT
            // 
            this.CONTENT.HeaderText = "CONTENT";
            this.CONTENT.Name = "CONTENT";
            this.CONTENT.ReadOnly = true;
            this.CONTENT.Visible = false;
            // 
            // INSTAFF
            // 
            this.INSTAFF.HeaderText = "INSTAFF";
            this.INSTAFF.Name = "INSTAFF";
            this.INSTAFF.ReadOnly = true;
            this.INSTAFF.Visible = false;
            // 
            // TITLE
            // 
            this.TITLE.HeaderText = "제목";
            this.TITLE.Name = "TITLE";
            this.TITLE.ReadOnly = true;
            this.TITLE.Width = 270;
            // 
            // INTIME
            // 
            this.INTIME.HeaderText = "작성일";
            this.INTIME.Name = "INTIME";
            this.INTIME.ReadOnly = true;
            this.INTIME.Width = 120;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.PaleTurquoise;
            this.panel2.Controls.Add(this.lbl);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.btnNew);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1360, 33);
            this.panel2.TabIndex = 334;
            // 
            // lbl
            // 
            this.lbl._AutoTab = true;
            this.lbl._BorderColor = System.Drawing.Color.LightSteelBlue;
            this.lbl._FocusedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lbl._WaterMarkColor = System.Drawing.Color.Gray;
            this.lbl._WaterMarkText = "";
            this.lbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.lbl.Location = new System.Drawing.Point(663, 5);
            this.lbl.MaxLength = 6;
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(166, 22);
            this.lbl.TabIndex = 383;
            this.lbl.Visible = false;
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
            this.label1.Text = "공지사항";
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
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.Enabled = false;
            this.btnDelete.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ForeColor = System.Drawing.Color.DodgerBlue;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDelete.Location = new System.Drawing.Point(1019, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(65, 29);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Tag = "삭제";
            this.btnDelete.Text = "삭제";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lbl_flow_cd
            // 
            this.lbl_flow_cd.BackColor = System.Drawing.Color.PowderBlue;
            this.lbl_flow_cd.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_flow_cd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbl_flow_cd.Location = new System.Drawing.Point(57, 55);
            this.lbl_flow_cd.Name = "lbl_flow_cd";
            this.lbl_flow_cd.Size = new System.Drawing.Size(100, 22);
            this.lbl_flow_cd.TabIndex = 340;
            this.lbl_flow_cd.Text = "번호";
            this.lbl_flow_cd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.dTP1);
            this.panel1.Controls.Add(this.textBox5);
            this.panel1.Controls.Add(this.textBox4);
            this.panel1.Controls.Add(this.textBox3);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lbl_flow_cd);
            this.panel1.Location = new System.Drawing.Point(12, 39);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(838, 706);
            this.panel1.TabIndex = 341;
            // 
            // dTP1
            // 
            this.dTP1.Location = new System.Drawing.Point(163, 496);
            this.dTP1.Name = "dTP1";
            this.dTP1.Size = new System.Drawing.Size(169, 21);
            this.dTP1.TabIndex = 350;
            this.dTP1.Visible = false;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(163, 530);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(100, 21);
            this.textBox5.TabIndex = 349;
            this.textBox5.Visible = false;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(339, 530);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 21);
            this.textBox4.TabIndex = 348;
            this.textBox4.Visible = false;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(163, 124);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(621, 351);
            this.textBox3.TabIndex = 347;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(163, 88);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(621, 21);
            this.textBox2.TabIndex = 346;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(163, 55);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 345;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.PowderBlue;
            this.label5.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(57, 530);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 22);
            this.label5.TabIndex = 344;
            this.label5.Text = "작성자";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label5.Visible = false;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.PowderBlue;
            this.label4.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(57, 495);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 22);
            this.label4.TabIndex = 343;
            this.label4.Text = "작성일";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label4.Visible = false;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.PowderBlue;
            this.label3.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(57, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 22);
            this.label3.TabIndex = 342;
            this.label3.Text = "내용";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.PowderBlue;
            this.label2.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(57, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 22);
            this.label2.TabIndex = 341;
            this.label2.Text = "제목";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel3.Controls.Add(this.btnCustSrch2);
            this.panel3.Controls.Add(this.txtSrch);
            this.panel3.Controls.Add(this.noticeGrid);
            this.panel3.Location = new System.Drawing.Point(856, 39);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(492, 706);
            this.panel3.TabIndex = 342;
            // 
            // btnCustSrch2
            // 
            this.btnCustSrch2.BackColor = System.Drawing.Color.Transparent;
            this.btnCustSrch2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnCustSrch2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCustSrch2.FlatAppearance.BorderSize = 0;
            this.btnCustSrch2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.SkyBlue;
            this.btnCustSrch2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCustSrch2.Image = ((System.Drawing.Image)(resources.GetObject("btnCustSrch2.Image")));
            this.btnCustSrch2.Location = new System.Drawing.Point(305, 6);
            this.btnCustSrch2.Name = "btnCustSrch2";
            this.btnCustSrch2.Size = new System.Drawing.Size(33, 33);
            this.btnCustSrch2.TabIndex = 393;
            this.btnCustSrch2.Tag = "검색";
            this.btnCustSrch2.UseVisualStyleBackColor = false;
            this.btnCustSrch2.Click += new System.EventHandler(this.btnCustSrch2_Click);
            // 
            // txtSrch
            // 
            this.txtSrch.Location = new System.Drawing.Point(60, 12);
            this.txtSrch.Name = "txtSrch";
            this.txtSrch.Size = new System.Drawing.Size(239, 21);
            this.txtSrch.TabIndex = 350;
            // 
            // frm공지사항
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1360, 757);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "frm공지사항";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frm공지사항";
            this.Load += new System.EventHandler(this.frm공지사항_Load);
            ((System.ComponentModel.ISupportInitialize)(this.noticeGrid)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView noticeGrid;
        private System.Windows.Forms.Panel panel2;
        private Controls.conTextBox lbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lbl_flow_cd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSrch;
        private System.Windows.Forms.Button btnCustSrch2;
        private System.Windows.Forms.DateTimePicker dTP1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SEQ;
        private System.Windows.Forms.DataGridViewTextBoxColumn CONTENT;
        private System.Windows.Forms.DataGridViewTextBoxColumn INSTAFF;
        private System.Windows.Forms.DataGridViewTextBoxColumn TITLE;
        private System.Windows.Forms.DataGridViewTextBoxColumn INTIME;
    }
}