namespace 스마트팩토리
{
    partial class frmBackRoot
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
            this.panCenter = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnTile01 = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panCenter.SuspendLayout();
            this.SuspendLayout();
            // 
            // panCenter
            // 
            this.panCenter.Controls.Add(this.button2);
            this.panCenter.Controls.Add(this.button1);
            this.panCenter.Controls.Add(this.btnTile01);
            this.panCenter.Location = new System.Drawing.Point(6, 5);
            this.panCenter.Name = "panCenter";
            this.panCenter.Size = new System.Drawing.Size(1145, 603);
            this.panCenter.TabIndex = 99;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.LightSeaGreen;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.LightSeaGreen;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(141, 7);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(130, 130);
            this.button2.TabIndex = 136;
            this.button2.Tag = "Z1관리자.frm엑셀변환";
            this.button2.Text = "엑셀 변환";
            this.button2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.menuButton_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LightSeaGreen;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.LightSeaGreen;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(5, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 130);
            this.button1.TabIndex = 131;
            this.button1.Tag = "Z1관리자.frm업체등록";
            this.button1.Text = "업체 등록";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.menuButton_Click);
            // 
            // btnTile01
            // 
            this.btnTile01.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btnTile01.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnTile01.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTile01.FlatAppearance.BorderColor = System.Drawing.Color.LightSeaGreen;
            this.btnTile01.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTile01.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnTile01.ForeColor = System.Drawing.Color.White;
            this.btnTile01.Location = new System.Drawing.Point(437, 7);
            this.btnTile01.Name = "btnTile01";
            this.btnTile01.Size = new System.Drawing.Size(130, 130);
            this.btnTile01.TabIndex = 130;
            this.btnTile01.Tag = "Z1관리자.frm데이터베이스";
            this.btnTile01.Text = "DB 관리";
            this.btnTile01.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnTile01.UseVisualStyleBackColor = false;
            this.btnTile01.Click += new System.EventHandler(this.menuButton_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "사업자번호";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "상호명";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 190;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "대표자";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 90;
            // 
            // frmBackRoot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1163, 620);
            this.Controls.Add(this.panCenter);
            this.Name = "frmBackRoot";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmBackAdmin";
            this.Load += new System.EventHandler(this.frmBackRoot_Load);
            this.Resize += new System.EventHandler(this.frmBackRoot_Resize);
            this.panCenter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panCenter;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnTile01;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.Button button2;
    }
}