namespace 스마트팩토리
{
    partial class frmMain
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.panTopBorder = new System.Windows.Forms.Panel();
            this.lblCompName = new System.Windows.Forms.Label();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolMenu = new System.Windows.Forms.ToolStrip();
            this.toolStripDrop1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.butSetting = new System.Windows.Forms.Button();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lbl업체명 = new System.Windows.Forms.Label();
            this.panBackRight = new System.Windows.Forms.Panel();
            this.panBackBottom = new System.Windows.Forms.Panel();
            this.panBackLeft = new System.Windows.Forms.Panel();
            this.tmLogin = new System.Windows.Forms.Timer(this.components);
            this.panMenu = new System.Windows.Forms.Panel();
            this.miniToolStrip = new System.Windows.Forms.ToolStrip();
            this.panBackTop = new System.Windows.Forms.Panel();
            this.tmSelected = new System.Windows.Forms.Timer(this.components);
            this.tmDelFile = new System.Windows.Forms.Timer(this.components);
            this.butExit = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.picMain = new System.Windows.Forms.PictureBox();
            this.panTopBorder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.toolMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMain)).BeginInit();
            this.SuspendLayout();
            // 
            // panTopBorder
            // 
            this.panTopBorder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panTopBorder.Controls.Add(this.lblCompName);
            this.panTopBorder.Controls.Add(this.picLogo);
            this.panTopBorder.Controls.Add(this.pictureBox1);
            this.panTopBorder.Controls.Add(this.toolMenu);
            this.panTopBorder.Controls.Add(this.butSetting);
            this.panTopBorder.Controls.Add(this.lblUserName);
            this.panTopBorder.Dock = System.Windows.Forms.DockStyle.Top;
            this.panTopBorder.Location = new System.Drawing.Point(0, 0);
            this.panTopBorder.Name = "panTopBorder";
            this.panTopBorder.Size = new System.Drawing.Size(1364, 49);
            this.panTopBorder.TabIndex = 1;
            // 
            // lblCompName
            // 
            this.lblCompName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCompName.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblCompName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblCompName.Location = new System.Drawing.Point(1222, 18);
            this.lblCompName.Name = "lblCompName";
            this.lblCompName.Size = new System.Drawing.Size(60, 18);
            this.lblCompName.TabIndex = 48;
            this.lblCompName.Text = "회사명";
            this.lblCompName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // picLogo
            // 
            this.picLogo.BackColor = System.Drawing.Color.White;
            this.picLogo.BackgroundImage = global::스마트팩토리.Properties.Resources.czmlogo2;
            this.picLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picLogo.Location = new System.Drawing.Point(0, 0);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(86, 51);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogo.TabIndex = 44;
            this.picLogo.TabStop = false;
            this.picLogo.Click += new System.EventHandler(this.picMain_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pictureBox1.Location = new System.Drawing.Point(0, 40);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1364, 9);
            this.pictureBox1.TabIndex = 47;
            this.pictureBox1.TabStop = false;
            // 
            // toolMenu
            // 
            this.toolMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.toolMenu.Dock = System.Windows.Forms.DockStyle.None;
            this.toolMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolMenu.ImageScalingSize = new System.Drawing.Size(16, 30);
            this.toolMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDrop1});
            this.toolMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolMenu.Location = new System.Drawing.Point(100, 8);
            this.toolMenu.Name = "toolMenu";
            this.toolMenu.Padding = new System.Windows.Forms.Padding(0);
            this.toolMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolMenu.ShowItemToolTips = false;
            this.toolMenu.Size = new System.Drawing.Size(57, 25);
            this.toolMenu.Stretch = true;
            this.toolMenu.TabIndex = 41;
            // 
            // toolStripDrop1
            // 
            this.toolStripDrop1.AutoSize = false;
            this.toolStripDrop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.toolStripDrop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStripDrop1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDrop1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.toolStripDrop1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.toolStripDrop1.ForeColor = System.Drawing.Color.Black;
            this.toolStripDrop1.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripDrop1.Name = "toolStripDrop1";
            this.toolStripDrop1.ShowDropDownArrow = false;
            this.toolStripDrop1.Size = new System.Drawing.Size(55, 25);
            this.toolStripDrop1.Text = " 열린창 ";
            this.toolStripDrop1.MouseEnter += new System.EventHandler(this.mainMenu_MouseEnter);
            this.toolStripDrop1.MouseLeave += new System.EventHandler(this.mainMenu_MouseLeave);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(126, 22);
            this.toolStripMenuItem1.Tag = "P1매출관리.frm매출등록";
            this.toolStripMenuItem1.Text = "매출 등록";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.mainMemu_Click);
            // 
            // butSetting
            // 
            this.butSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butSetting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.butSetting.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butSetting.FlatAppearance.BorderSize = 0;
            this.butSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butSetting.Image = ((System.Drawing.Image)(resources.GetObject("butSetting.Image")));
            this.butSetting.Location = new System.Drawing.Point(1261, 8);
            this.butSetting.Name = "butSetting";
            this.butSetting.Size = new System.Drawing.Size(35, 35);
            this.butSetting.TabIndex = 42;
            this.butSetting.Tag = "frmSetting";
            this.butSetting.UseVisualStyleBackColor = true;
            this.butSetting.Visible = false;
            this.butSetting.Click += new System.EventHandler(this.butSetting_Click);
            // 
            // lblUserName
            // 
            this.lblUserName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUserName.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblUserName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblUserName.Location = new System.Drawing.Point(1258, 18);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(94, 18);
            this.lblUserName.TabIndex = 45;
            this.lblUserName.Text = "아무게";
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbl업체명
            // 
            this.lbl업체명.AutoSize = true;
            this.lbl업체명.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl업체명.Font = new System.Drawing.Font("굴림", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl업체명.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lbl업체명.Location = new System.Drawing.Point(321, 131);
            this.lbl업체명.Name = "lbl업체명";
            this.lbl업체명.Size = new System.Drawing.Size(88, 19);
            this.lbl업체명.TabIndex = 46;
            this.lbl업체명.Text = "lbl업체명";
            this.lbl업체명.Visible = false;
            this.lbl업체명.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl업체명_MouseClick);
            // 
            // panBackRight
            // 
            this.panBackRight.BackColor = System.Drawing.Color.White;
            this.panBackRight.Location = new System.Drawing.Point(148, 108);
            this.panBackRight.Name = "panBackRight";
            this.panBackRight.Size = new System.Drawing.Size(27, 42);
            this.panBackRight.TabIndex = 22;
            // 
            // panBackBottom
            // 
            this.panBackBottom.BackColor = System.Drawing.Color.White;
            this.panBackBottom.Location = new System.Drawing.Point(50, 156);
            this.panBackBottom.Name = "panBackBottom";
            this.panBackBottom.Size = new System.Drawing.Size(125, 16);
            this.panBackBottom.TabIndex = 23;
            // 
            // panBackLeft
            // 
            this.panBackLeft.BackColor = System.Drawing.Color.White;
            this.panBackLeft.Location = new System.Drawing.Point(50, 108);
            this.panBackLeft.Name = "panBackLeft";
            this.panBackLeft.Size = new System.Drawing.Size(23, 42);
            this.panBackLeft.TabIndex = 24;
            // 
            // tmLogin
            // 
            this.tmLogin.Tick += new System.EventHandler(this.tmLogin_Tick);
            // 
            // panMenu
            // 
            this.panMenu.BackColor = System.Drawing.Color.LightBlue;
            this.panMenu.Location = new System.Drawing.Point(106, 329);
            this.panMenu.Name = "panMenu";
            this.panMenu.Size = new System.Drawing.Size(984, 43);
            this.panMenu.TabIndex = 26;
            this.panMenu.Visible = false;
            // 
            // miniToolStrip
            // 
            this.miniToolStrip.AutoSize = false;
            this.miniToolStrip.BackColor = System.Drawing.SystemColors.HotTrack;
            this.miniToolStrip.CanOverflow = false;
            this.miniToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.miniToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.miniToolStrip.ImageScalingSize = new System.Drawing.Size(16, 30);
            this.miniToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.miniToolStrip.Location = new System.Drawing.Point(813, 0);
            this.miniToolStrip.Name = "miniToolStrip";
            this.miniToolStrip.Padding = new System.Windows.Forms.Padding(0);
            this.miniToolStrip.ShowItemToolTips = false;
            this.miniToolStrip.Size = new System.Drawing.Size(813, 35);
            this.miniToolStrip.Stretch = true;
            this.miniToolStrip.TabIndex = 37;
            // 
            // panBackTop
            // 
            this.panBackTop.BackColor = System.Drawing.Color.White;
            this.panBackTop.Location = new System.Drawing.Point(50, 83);
            this.panBackTop.Name = "panBackTop";
            this.panBackTop.Size = new System.Drawing.Size(125, 19);
            this.panBackTop.TabIndex = 24;
            // 
            // tmSelected
            // 
            this.tmSelected.Tick += new System.EventHandler(this.tmSelected_Tick);
            // 
            // tmDelFile
            // 
            this.tmDelFile.Interval = 1000;
            this.tmDelFile.Tick += new System.EventHandler(this.tmDelFile_Tick);
            // 
            // butExit
            // 
            this.butExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butExit.BackColor = System.Drawing.Color.IndianRed;
            this.butExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butExit.FlatAppearance.BorderSize = 0;
            this.butExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butExit.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.butExit.ForeColor = System.Drawing.Color.White;
            this.butExit.Image = ((System.Drawing.Image)(resources.GetObject("butExit.Image")));
            this.butExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butExit.Location = new System.Drawing.Point(1257, 74);
            this.butExit.Name = "butExit";
            this.butExit.Size = new System.Drawing.Size(36, 37);
            this.butExit.TabIndex = 5;
            this.butExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.butExit.UseVisualStyleBackColor = false;
            this.butExit.Visible = false;
            this.butExit.Click += new System.EventHandler(this.butExit_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(1225, 127);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(42, 45);
            this.button2.TabIndex = 41;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            // 
            // picMain
            // 
            this.picMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picMain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picMain.Image = ((System.Drawing.Image)(resources.GetObject("picMain.Image")));
            this.picMain.Location = new System.Drawing.Point(387, 242);
            this.picMain.Name = "picMain";
            this.picMain.Size = new System.Drawing.Size(69, 43);
            this.picMain.TabIndex = 39;
            this.picMain.TabStop = false;
            this.picMain.Visible = false;
            this.picMain.Click += new System.EventHandler(this.picMain_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1364, 618);
            this.Controls.Add(this.panMenu);
            this.Controls.Add(this.panBackRight);
            this.Controls.Add(this.panBackBottom);
            this.Controls.Add(this.lbl업체명);
            this.Controls.Add(this.panBackLeft);
            this.Controls.Add(this.butExit);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.picMain);
            this.Controls.Add(this.panTopBorder);
            this.Controls.Add(this.panBackTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "스마트 팩토리 (1.0.0.1)";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.panTopBorder.ResumeLayout(false);
            this.panTopBorder.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.toolMenu.ResumeLayout(false);
            this.toolMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panTopBorder;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox picMain;
        private System.Windows.Forms.Button butExit;
        internal System.Windows.Forms.Panel panBackRight;
        internal System.Windows.Forms.Panel panBackBottom;
        internal System.Windows.Forms.Panel panBackLeft;
        private System.Windows.Forms.Timer tmLogin;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Panel panMenu;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDrop1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Button butSetting;
        private System.Windows.Forms.ToolStrip miniToolStrip;
        private System.Windows.Forms.Label lblUserName;
        internal System.Windows.Forms.Panel panBackTop;
        private System.Windows.Forms.Timer tmSelected;
        private System.Windows.Forms.Label lbl업체명;
        private System.Windows.Forms.ToolStrip toolMenu;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer tmDelFile;
        private System.Windows.Forms.ToolStripDropDownButton tsOpenWin;
        private System.Windows.Forms.Label lblCompName;
    }
}

