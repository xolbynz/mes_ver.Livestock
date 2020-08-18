namespace 스마트팩토리
{
    partial class frmCheck
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblToday = new System.Windows.Forms.Label();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Label6 = new System.Windows.Forms.Label();
            this.tmSec = new System.Windows.Forms.Timer(this.components);
            this.tmStart = new System.Windows.Forms.Timer(this.components);
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
            this.panMain.Size = new System.Drawing.Size(278, 107);
            this.panMain.TabIndex = 47;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblToday);
            this.panel1.Location = new System.Drawing.Point(3, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(272, 72);
            this.panel1.TabIndex = 47;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Window;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(9, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(253, 19);
            this.label1.TabIndex = 40;
            this.label1.Text = "데이터베이스 연결 확인 중...";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblToday
            // 
            this.lblToday.BackColor = System.Drawing.SystemColors.Window;
            this.lblToday.Location = new System.Drawing.Point(9, 53);
            this.lblToday.Name = "lblToday";
            this.lblToday.Size = new System.Drawing.Size(253, 19);
            this.lblToday.TabIndex = 39;
            this.lblToday.Text = "0";
            this.lblToday.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblToday.Visible = false;
            // 
            // Panel2
            // 
            this.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.Panel2.Controls.Add(this.Label6);
            this.Panel2.Location = new System.Drawing.Point(3, 3);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(272, 28);
            this.Panel2.TabIndex = 46;
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.Label6.Font = new System.Drawing.Font("굴림체", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label6.ForeColor = System.Drawing.Color.White;
            this.Label6.Location = new System.Drawing.Point(8, 4);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(256, 19);
            this.Label6.TabIndex = 29;
            this.Label6.Text = "■ 환경 체크 ■";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // tmSec
            // 
            this.tmSec.Interval = 1000;
            this.tmSec.Tick += new System.EventHandler(this.tmSec_Tick);
            // 
            // tmStart
            // 
            this.tmStart.Tick += new System.EventHandler(this.tmStart_Tick);
            // 
            // frmCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 259);
            this.Controls.Add(this.panMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmCheck";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmCheck";
            this.Load += new System.EventHandler(this.frmCheck_Load);
            this.panMain.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.Panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panMain;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Label lblToday;
        internal System.Windows.Forms.Panel Panel2;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer tmSec;
        private System.Windows.Forms.Timer tmStart;
    }
}