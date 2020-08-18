namespace 스마트팩토리.Popup
{
    partial class pop유통기한등록
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pop유통기한등록));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.GridRecord = new System.Windows.Forms.DataGridView();
            this.EXPRT_COUNT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EXPRT_GUBUN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_exprt_count = new 스마트팩토리.Controls.conTextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tmFocus = new System.Windows.Forms.Timer(this.components);
            this.tmClose = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_exprt_gubun = new 스마트팩토리.Controls.conComboBox();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridRecord)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.PaleTurquoise;
            this.panel2.Controls.Add(this.btnExit);
            this.panel2.Controls.Add(this.lblTitle);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(544, 33);
            this.panel2.TabIndex = 100;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.ForeColor = System.Drawing.Color.DodgerBlue;
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnExit.Location = new System.Drawing.Point(467, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(65, 29);
            this.btnExit.TabIndex = 2;
            this.btnExit.Tag = "종료";
            this.btnExit.Text = "닫기";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("굴림", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTitle.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblTitle.Location = new System.Drawing.Point(11, 5);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(156, 22);
            this.lblTitle.TabIndex = 95;
            this.lblTitle.Text = "유통기한 등록";
            // 
            // GridRecord
            // 
            this.GridRecord.AllowUserToAddRows = false;
            this.GridRecord.AllowUserToDeleteRows = false;
            this.GridRecord.AllowUserToOrderColumns = true;
            this.GridRecord.AllowUserToResizeRows = false;
            this.GridRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GridRecord.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.GridRecord.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridRecord.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GridRecord.ColumnHeadersHeight = 30;
            this.GridRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.GridRecord.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EXPRT_COUNT,
            this.EXPRT_GUBUN});
            this.GridRecord.EnableHeadersVisualStyles = false;
            this.GridRecord.GridColor = System.Drawing.Color.PowderBlue;
            this.GridRecord.Location = new System.Drawing.Point(12, 109);
            this.GridRecord.Name = "GridRecord";
            this.GridRecord.ReadOnly = true;
            this.GridRecord.RowHeadersVisible = false;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightCyan;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.GridRecord.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.GridRecord.RowTemplate.Height = 23;
            this.GridRecord.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridRecord.Size = new System.Drawing.Size(520, 545);
            this.GridRecord.TabIndex = 11;
            // 
            // EXPRT_COUNT
            // 
            this.EXPRT_COUNT.Frozen = true;
            this.EXPRT_COUNT.HeaderText = "유통일수,개월수";
            this.EXPRT_COUNT.Name = "EXPRT_COUNT";
            this.EXPRT_COUNT.ReadOnly = true;
            this.EXPRT_COUNT.Width = 273;
            // 
            // EXPRT_GUBUN
            // 
            this.EXPRT_GUBUN.Frozen = true;
            this.EXPRT_GUBUN.HeaderText = "일/개월";
            this.EXPRT_GUBUN.Name = "EXPRT_GUBUN";
            this.EXPRT_GUBUN.ReadOnly = true;
            this.EXPRT_GUBUN.Width = 272;
            // 
            // txt_exprt_count
            // 
            this.txt_exprt_count._AutoTab = true;
            this.txt_exprt_count._BorderColor = System.Drawing.Color.LightSkyBlue;
            this.txt_exprt_count._FocusedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txt_exprt_count._WaterMarkColor = System.Drawing.Color.Gray;
            this.txt_exprt_count._WaterMarkText = "";
            this.txt_exprt_count.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_exprt_count.Font = new System.Drawing.Font("굴림", 10F);
            this.txt_exprt_count.Location = new System.Drawing.Point(122, 44);
            this.txt_exprt_count.Name = "txt_exprt_count";
            this.txt_exprt_count.Size = new System.Drawing.Size(122, 22);
            this.txt_exprt_count.TabIndex = 0;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.Gainsboro;
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label15.Location = new System.Drawing.Point(12, 44);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(104, 22);
            this.label15.TabIndex = 106;
            this.label15.Text = "유통일수,개월수";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tmClose
            // 
            this.tmClose.Tick += new System.EventHandler(this.tmClose_Tick);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Gainsboro;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(13, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 22);
            this.label1.TabIndex = 108;
            this.label1.Text = "일/개월";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmb_exprt_gubun
            // 
            this.cmb_exprt_gubun._BorderColor = System.Drawing.Color.Transparent;
            this.cmb_exprt_gubun._FocusedBackColor = System.Drawing.Color.White;
            this.cmb_exprt_gubun.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cmb_exprt_gubun.FormattingEnabled = true;
            this.cmb_exprt_gubun.Location = new System.Drawing.Point(122, 77);
            this.cmb_exprt_gubun.Name = "cmb_exprt_gubun";
            this.cmb_exprt_gubun.Size = new System.Drawing.Size(122, 20);
            this.cmb_exprt_gubun.TabIndex = 382;
            // 
            // pop유통기한등록
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(544, 666);
            this.ControlBox = false;
            this.Controls.Add(this.cmb_exprt_gubun);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.GridRecord);
            this.Controls.Add(this.txt_exprt_count);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Name = "pop유통기한등록";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "유통기한 등록";
            this.Load += new System.EventHandler(this.pop유통기한등록_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridRecord)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView GridRecord;
        public Controls.conTextBox txt_exprt_count;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Timer tmFocus;
        private System.Windows.Forms.Timer tmClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn EXPRT_COUNT;
        private System.Windows.Forms.DataGridViewTextBoxColumn EXPRT_GUBUN;
        private Controls.conComboBox cmb_exprt_gubun;
    }
}