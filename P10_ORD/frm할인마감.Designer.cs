namespace 스마트팩토리.P10_ORD
{
    partial class frm할인마감
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm할인마감));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.dtp마감일자2 = new System.Windows.Forms.DateTimePicker();
            this.dtp마감일자1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtp수금일자 = new System.Windows.Forms.DateTimePicker();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnS거래처 = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.progBar = new System.Windows.Forms.ProgressBar();
            this.panBody = new System.Windows.Forms.Panel();
            this.lblSaving = new System.Windows.Forms.Label();
            this.txtS거래처코드 = new 스마트팩토리.Controls.conTextBox();
            this.txtS거래처명 = new 스마트팩토리.Controls.conTextBox();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2.SuspendLayout();
            this.panBody.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.PaleTurquoise;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1360, 33);
            this.panel2.TabIndex = 4;
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
            this.label1.Text = "할인마감";
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
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(219, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 12);
            this.label7.TabIndex = 299;
            this.label7.Text = "~";
            // 
            // dtp마감일자2
            // 
            this.dtp마감일자2.Checked = false;
            this.dtp마감일자2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp마감일자2.Location = new System.Drawing.Point(239, 13);
            this.dtp마감일자2.Name = "dtp마감일자2";
            this.dtp마감일자2.Size = new System.Drawing.Size(100, 21);
            this.dtp마감일자2.TabIndex = 297;
            // 
            // dtp마감일자1
            // 
            this.dtp마감일자1.Checked = false;
            this.dtp마감일자1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp마감일자1.Location = new System.Drawing.Point(113, 13);
            this.dtp마감일자1.Name = "dtp마감일자1";
            this.dtp마감일자1.Size = new System.Drawing.Size(100, 21);
            this.dtp마감일자1.TabIndex = 296;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Silver;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(12, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 22);
            this.label2.TabIndex = 298;
            this.label2.Text = "마감일자";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Silver;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(12, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 22);
            this.label3.TabIndex = 298;
            this.label3.Text = "수금일자";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtp수금일자
            // 
            this.dtp수금일자.Checked = false;
            this.dtp수금일자.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp수금일자.Location = new System.Drawing.Point(113, 42);
            this.dtp수금일자.Name = "dtp수금일자";
            this.dtp수금일자.Size = new System.Drawing.Size(100, 21);
            this.dtp수금일자.TabIndex = 296;
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(239, 45);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(88, 16);
            this.chkAll.TabIndex = 300;
            this.chkAll.Text = "전체 거래처";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.Click += new System.EventHandler(this.chkAll_Click);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Silver;
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label8.Location = new System.Drawing.Point(12, 79);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 22);
            this.label8.TabIndex = 318;
            this.label8.Text = "거래처";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnS거래처
            // 
            this.btnS거래처.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnS거래처.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnS거래처.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnS거래처.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnS거래처.Font = new System.Drawing.Font("굴림", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnS거래처.ForeColor = System.Drawing.Color.Black;
            this.btnS거래처.Location = new System.Drawing.Point(394, 79);
            this.btnS거래처.Name = "btnS거래처";
            this.btnS거래처.Size = new System.Drawing.Size(17, 22);
            this.btnS거래처.TabIndex = 317;
            this.btnS거래처.TabStop = false;
            this.btnS거래처.Tag = "";
            this.btnS거래처.Text = "▼";
            this.btnS거래처.UseVisualStyleBackColor = false;
            this.btnS거래처.Click += new System.EventHandler(this.btnS거래처_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.Color.Blue;
            this.label20.Location = new System.Drawing.Point(12, 130);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(399, 12);
            this.label20.TabIndex = 320;
            this.label20.Text = "※ 이 작업은 매출자료 중 할인금액을 수금(72)으로 마감하는 작업입니다.";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.DodgerBlue;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSave.Location = new System.Drawing.Point(180, 167);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(65, 29);
            this.btnSave.TabIndex = 321;
            this.btnSave.Tag = "저장";
            this.btnSave.Text = "마감";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // progBar
            // 
            this.progBar.Location = new System.Drawing.Point(14, 145);
            this.progBar.Name = "progBar";
            this.progBar.Size = new System.Drawing.Size(397, 16);
            this.progBar.TabIndex = 322;
            // 
            // panBody
            // 
            this.panBody.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panBody.Controls.Add(this.lblSaving);
            this.panBody.Controls.Add(this.label2);
            this.panBody.Controls.Add(this.progBar);
            this.panBody.Controls.Add(this.label3);
            this.panBody.Controls.Add(this.btnSave);
            this.panBody.Controls.Add(this.dtp마감일자1);
            this.panBody.Controls.Add(this.label20);
            this.panBody.Controls.Add(this.dtp수금일자);
            this.panBody.Controls.Add(this.dtp마감일자2);
            this.panBody.Controls.Add(this.label8);
            this.panBody.Controls.Add(this.label7);
            this.panBody.Controls.Add(this.txtS거래처코드);
            this.panBody.Controls.Add(this.chkAll);
            this.panBody.Controls.Add(this.btnS거래처);
            this.panBody.Controls.Add(this.txtS거래처명);
            this.panBody.Location = new System.Drawing.Point(452, 39);
            this.panBody.Name = "panBody";
            this.panBody.Size = new System.Drawing.Size(425, 515);
            this.panBody.TabIndex = 323;
            // 
            // lblSaving
            // 
            this.lblSaving.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblSaving.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSaving.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblSaving.ForeColor = System.Drawing.Color.Green;
            this.lblSaving.Location = new System.Drawing.Point(68, 238);
            this.lblSaving.Name = "lblSaving";
            this.lblSaving.Size = new System.Drawing.Size(313, 57);
            this.lblSaving.TabIndex = 323;
            this.lblSaving.Text = "Processing ...";
            this.lblSaving.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSaving.Visible = false;
            // 
            // txtS거래처코드
            // 
            this.txtS거래처코드._AutoTab = false;
            this.txtS거래처코드._BorderColor = System.Drawing.Color.Gainsboro;
            this.txtS거래처코드._FocusedBackColor = System.Drawing.Color.WhiteSmoke;
            this.txtS거래처코드._WaterMarkColor = System.Drawing.Color.Gray;
            this.txtS거래처코드._WaterMarkText = "";
            this.txtS거래처코드.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtS거래처코드.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtS거래처코드.ForeColor = System.Drawing.Color.Gray;
            this.txtS거래처코드.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txtS거래처코드.Location = new System.Drawing.Point(113, 79);
            this.txtS거래처코드.MaxLength = 50;
            this.txtS거래처코드.Name = "txtS거래처코드";
            this.txtS거래처코드.ReadOnly = true;
            this.txtS거래처코드.Size = new System.Drawing.Size(54, 22);
            this.txtS거래처코드.TabIndex = 315;
            this.txtS거래처코드.TabStop = false;
            // 
            // txtS거래처명
            // 
            this.txtS거래처명._AutoTab = false;
            this.txtS거래처명._BorderColor = System.Drawing.Color.LightSteelBlue;
            this.txtS거래처명._FocusedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtS거래처명._WaterMarkColor = System.Drawing.Color.Gray;
            this.txtS거래처명._WaterMarkText = "";
            this.txtS거래처명.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtS거래처명.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtS거래처명.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txtS거래처명.Location = new System.Drawing.Point(168, 79);
            this.txtS거래처명.MaxLength = 50;
            this.txtS거래처명.Name = "txtS거래처명";
            this.txtS거래처명.Size = new System.Drawing.Size(228, 22);
            this.txtS거래처명.TabIndex = 316;
            this.txtS거래처명.TextChanged += new System.EventHandler(this.txtS거래처_TextChanged);
            this.txtS거래처명.Enter += new System.EventHandler(this.txtS거래처_Enter);
            this.txtS거래처명.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtS거래처_KeyDown);
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "영업소명";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 200;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTextBoxColumn3.HeaderText = "지역구분";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn4.HeaderText = "구분";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn5.HeaderText = "조회순서";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 80;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "소계";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 200;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "부서계";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 200;
            // 
            // dataGridViewTextBoxColumn8
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn8.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn8.HeaderText = "사용어부";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Width = 80;
            // 
            // frm할인마감
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1360, 566);
            this.Controls.Add(this.panBody);
            this.Controls.Add(this.panel2);
            this.KeyPreview = true;
            this.Name = "frm할인마감";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frm할인마감";
            this.Load += new System.EventHandler(this.frm할인마감_Load);
            this.SizeChanged += new System.EventHandler(this.frm할인마감_SizeChanged);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panBody.ResumeLayout(false);
            this.panBody.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtp마감일자2;
        private System.Windows.Forms.DateTimePicker dtp마감일자1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtp수금일자;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.Label label8;
        private Controls.conTextBox txtS거래처코드;
        private System.Windows.Forms.Button btnS거래처;
        private Controls.conTextBox txtS거래처명;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ProgressBar progBar;
        private System.Windows.Forms.Panel panBody;
        private System.Windows.Forms.Label lblSaving;
    }
}