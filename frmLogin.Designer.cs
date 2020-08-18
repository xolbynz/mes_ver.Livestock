namespace 스마트팩토리
{
    partial class frmLogin
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
            this.components = new System.ComponentModel.Container();
            this.panMain = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label12 = new System.Windows.Forms.Label();
            this.lblToday = new System.Windows.Forms.Label();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Label6 = new System.Windows.Forms.Label();
            this.tmFocus = new System.Windows.Forms.Timer(this.components);
            this.txtPW = new 스마트팩토리.Controls.conTextBox();
            this.txtID = new 스마트팩토리.Controls.conTextBox();
            this.txtCompID = new 스마트팩토리.Controls.conMaskedTextBox();
            this.panMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panMain
            // 
            this.panMain.BackColor = System.Drawing.Color.Gray;
            this.panMain.Controls.Add(this.panel1);
            this.panMain.Controls.Add(this.Panel2);
            this.panMain.Location = new System.Drawing.Point(0, 0);
            this.panMain.Name = "panMain";
            this.panMain.Size = new System.Drawing.Size(256, 200);
            this.panMain.TabIndex = 46;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.txtPW);
            this.panel1.Controls.Add(this.txtID);
            this.panel1.Controls.Add(this.txtCompID);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.Label3);
            this.panel1.Controls.Add(this.Label12);
            this.panel1.Controls.Add(this.lblToday);
            this.panel1.Location = new System.Drawing.Point(3, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 165);
            this.panel1.TabIndex = 47;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.FlatAppearance.BorderColor = System.Drawing.Color.LightSeaGreen;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnOK.Location = new System.Drawing.Point(36, 109);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(89, 24);
            this.btnOK.TabIndex = 3;
            this.btnOK.Tag = "";
            this.btnOK.Text = "로그인";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(36, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 22);
            this.label1.TabIndex = 41;
            this.label1.Text = "사업자번호";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnClose.Location = new System.Drawing.Point(135, 109);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(89, 24);
            this.btnClose.TabIndex = 4;
            this.btnClose.Tag = "";
            this.btnClose.Text = "취소";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Label3.ForeColor = System.Drawing.Color.Black;
            this.Label3.Location = new System.Drawing.Point(36, 81);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(80, 22);
            this.Label3.TabIndex = 40;
            this.Label3.Text = "암호";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label12
            // 
            this.Label12.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Label12.ForeColor = System.Drawing.Color.Black;
            this.Label12.Location = new System.Drawing.Point(36, 54);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(80, 22);
            this.Label12.TabIndex = 41;
            this.Label12.Text = "아이디";
            this.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblToday
            // 
            this.lblToday.BackColor = System.Drawing.SystemColors.Window;
            this.lblToday.ForeColor = System.Drawing.Color.Blue;
            this.lblToday.Location = new System.Drawing.Point(36, 2);
            this.lblToday.Name = "lblToday";
            this.lblToday.Size = new System.Drawing.Size(188, 19);
            this.lblToday.TabIndex = 39;
            this.lblToday.Text = "2006-01-01";
            this.lblToday.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // Panel2
            // 
            this.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.Panel2.Controls.Add(this.Label6);
            this.Panel2.Location = new System.Drawing.Point(3, 3);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(250, 28);
            this.Panel2.TabIndex = 46;
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.Label6.Font = new System.Drawing.Font("굴림체", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label6.ForeColor = System.Drawing.Color.White;
            this.Label6.Location = new System.Drawing.Point(8, 4);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(226, 19);
            this.Label6.TabIndex = 29;
            this.Label6.Text = "■ 로그인 ■";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // tmFocus
            // 
            this.tmFocus.Tick += new System.EventHandler(this.tmFocus_Tick);
            // 
            // txtPW
            // 
            this.txtPW._AutoTab = true;
            this.txtPW._BorderColor = System.Drawing.Color.LightSkyBlue;
            this.txtPW._FocusedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtPW._WaterMarkColor = System.Drawing.Color.Gray;
            this.txtPW._WaterMarkText = "";
            this.txtPW.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPW.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtPW.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.txtPW.Location = new System.Drawing.Point(122, 81);
            this.txtPW.MaxLength = 20;
            this.txtPW.Name = "txtPW";
            this.txtPW.PasswordChar = '*';
            this.txtPW.Size = new System.Drawing.Size(102, 22);
            this.txtPW.TabIndex = 2;
            this.txtPW.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPW_KeyDown_1);
            // 
            // txtID
            // 
            this.txtID._AutoTab = true;
            this.txtID._BorderColor = System.Drawing.Color.LightSkyBlue;
            this.txtID._FocusedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtID._WaterMarkColor = System.Drawing.Color.Gray;
            this.txtID._WaterMarkText = "";
            this.txtID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtID.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtID.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.txtID.Location = new System.Drawing.Point(122, 54);
            this.txtID.MaxLength = 20;
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(102, 22);
            this.txtID.TabIndex = 0;
            // 
            // txtCompID
            // 
            this.txtCompID._BorderColor = System.Drawing.Color.LightSkyBlue;
            this.txtCompID._FocusedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtCompID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCompID.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtCompID.Location = new System.Drawing.Point(122, 24);
            this.txtCompID.Mask = "000-00-00000";
            this.txtCompID.Name = "txtCompID";
            this.txtCompID.ResetOnSpace = false;
            this.txtCompID.Size = new System.Drawing.Size(102, 22);
            this.txtCompID.TabIndex = 5;
            this.txtCompID.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 271);
            this.Controls.Add(this.panMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmLogin";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "로그인";
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.panMain.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.Panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panMain;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.Label lblToday;
        internal System.Windows.Forms.Panel Panel2;
        internal System.Windows.Forms.Label Label6;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOK;
        internal System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer tmFocus;
        private Controls.conTextBox txtID;
        private Controls.conMaskedTextBox txtCompID;
        private Controls.conTextBox txtPW;
    }
}