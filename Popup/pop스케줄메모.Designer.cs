namespace 스마트팩토리.Popup
{
    partial class pop스케줄메모
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
            this.lbl_Date = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_Title = new System.Windows.Forms.TextBox();
            this.btm_Exit = new System.Windows.Forms.Button();
            this.txt_Contents = new System.Windows.Forms.TextBox();
            this.lbl_Nothing = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_Date
            // 
            this.lbl_Date.AutoSize = true;
            this.lbl_Date.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Date.Location = new System.Drawing.Point(12, 10);
            this.lbl_Date.Name = "lbl_Date";
            this.lbl_Date.Size = new System.Drawing.Size(44, 25);
            this.lbl_Date.TabIndex = 0;
            this.lbl_Date.Text = "날짜";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "TITLE";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "CONTENTS";
            // 
            // txt_Title
            // 
            this.txt_Title.BackColor = System.Drawing.Color.Moccasin;
            this.txt_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Title.Location = new System.Drawing.Point(77, 52);
            this.txt_Title.MaxLength = 3;
            this.txt_Title.Name = "txt_Title";
            this.txt_Title.Size = new System.Drawing.Size(202, 21);
            this.txt_Title.TabIndex = 1;
            this.txt_Title.Click += new System.EventHandler(this.txt_Title_Click);
            // 
            // btm_Exit
            // 
            this.btm_Exit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btm_Exit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btm_Exit.Location = new System.Drawing.Point(203, 364);
            this.btm_Exit.Name = "btm_Exit";
            this.btm_Exit.Size = new System.Drawing.Size(75, 23);
            this.btm_Exit.TabIndex = 3;
            this.btm_Exit.Text = "저장";
            this.btm_Exit.UseVisualStyleBackColor = true;
            this.btm_Exit.Click += new System.EventHandler(this.btm_Exit_Click);
            // 
            // txt_Contents
            // 
            this.txt_Contents.BackColor = System.Drawing.Color.Moccasin;
            this.txt_Contents.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Contents.Location = new System.Drawing.Point(12, 117);
            this.txt_Contents.Multiline = true;
            this.txt_Contents.Name = "txt_Contents";
            this.txt_Contents.Size = new System.Drawing.Size(266, 241);
            this.txt_Contents.TabIndex = 2;
            this.txt_Contents.Click += new System.EventHandler(this.txt_Contents_Click);
            // 
            // lbl_Nothing
            // 
            this.lbl_Nothing.AutoSize = true;
            this.lbl_Nothing.Location = new System.Drawing.Point(84, 227);
            this.lbl_Nothing.Name = "lbl_Nothing";
            this.lbl_Nothing.Size = new System.Drawing.Size(125, 12);
            this.lbl_Nothing.TabIndex = 7;
            this.lbl_Nothing.Text = "아직 일정이 없습니다.";
            this.lbl_Nothing.Visible = false;
            // 
            // pop스케줄메모
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Moccasin;
            this.ClientSize = new System.Drawing.Size(291, 389);
            this.Controls.Add(this.lbl_Nothing);
            this.Controls.Add(this.txt_Contents);
            this.Controls.Add(this.btm_Exit);
            this.Controls.Add(this.txt_Title);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl_Date);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "pop스케줄메모";
            this.Load += new System.EventHandler(this.pop스케쥴메모_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Date;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_Title;
        private System.Windows.Forms.Button btm_Exit;
        private System.Windows.Forms.TextBox txt_Contents;
        private System.Windows.Forms.Label lbl_Nothing;
    }
}