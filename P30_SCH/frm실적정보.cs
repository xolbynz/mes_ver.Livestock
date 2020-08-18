using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 스마트팩토리.P30_SCH
{
    public partial class frm실적정보 : Form
    {
        Popup.frmPrint readyPrt = new Popup.frmPrint();

        uc원장조회 uc = new uc원장조회();
        //uc원장조회2 uc2 = new uc원장조회2();
        //uc원장조회3 uc3 = new uc원장조회3();
        //uc원장조회4 uc4 = new uc원장조회4();
        //uc원장조회5 uc5 = new uc원장조회5();

        public frm실적정보()
        {
            InitializeComponent();

            set_Panels();
        }

        private void set_Panels()
        {
            panData1.Visible = true;

            panData1.Top = tBtn1.Top + tBtn1.Height;
            panData1.Left = tBtn1.Left;
            panData1.Width = this.Width - 40;
            panData1.Height = this.Height - panData1.Top - 50;

            panData1.Controls.Add(uc);
            uc.Top = 6;
            uc.Left = 3;
            uc.Width = panData1.Width - 6;
            uc.Height = panData1.Height - 12;
            uc.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;

            panData2.Top = panData1.Top;
            panData2.Left = panData1.Left;
            panData2.Width = panData1.Width;
            panData2.Height = panData1.Height;

            //panData2.Controls.Add(uc2);
            //uc2.Top = 6;
            //uc2.Left = 3;
            //uc2.Width = panData2.Width - 6;
            //uc2.Height = panData2.Height - 12;
            //uc2.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;

            panData3.Top = panData1.Top;
            panData3.Left = panData1.Left;
            panData3.Width = panData1.Width;
            panData3.Height = panData1.Height;

            //panData3.Controls.Add(uc3);
            //uc3.Top = 6;
            //uc3.Left = 3;
            //uc3.Width = panData3.Width - 6;
            //uc3.Height = panData3.Height - 12;
            //uc3.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;

            panData4.Top = panData1.Top;
            panData4.Left = panData1.Left;
            panData4.Width = panData1.Width;
            panData4.Height = panData1.Height;

            //panData4.Controls.Add(uc4);
            //uc4.Top = 6;
            //uc4.Left = 3;
            //uc4.Width = panData4.Width - 6;
            //uc4.Height = panData4.Height - 12;
            //uc4.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;

            panData5.Top = panData1.Top;
            panData5.Left = panData1.Left;
            panData5.Width = panData1.Width;
            panData5.Height = panData1.Height;

            //panData5.Controls.Add(uc5);
            //uc5.Top = 6;
            //uc5.Left = 3;
            //uc5.Width = panData5.Width - 6;
            //uc5.Height = panData5.Height - 12;
            //uc5.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;

            panData6.Top = panData1.Top;
            panData6.Left = panData1.Left;
            panData6.Width = panData1.Width;
            panData6.Height = panData1.Height;

            //panData6.Controls.Add(uc6);
            //uc6.Top = 6;
            //uc6.Left = 3;
            //uc6.Width = panData6.Width - 6;
            //uc6.Height = panData6.Height - 12;
            //uc6.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;

            panData7.Top = panData1.Top;
            panData7.Left = panData1.Left;
            panData7.Width = panData1.Width;
            panData7.Height = panData1.Height;

            //panData7.Controls.Add(uc7);
            //uc7.Top = 6;
            //uc7.Left = 3;
            //uc7.Width = panData7.Width - 6;
            //uc7.Height = panData7.Height - 12;
            //uc7.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;

            panData8.Top = panData1.Top;
            panData8.Left = panData1.Left;
            panData8.Width = panData1.Width;
            panData8.Height = panData1.Height;

            //panData8.Controls.Add(uc8);
            //uc8.Top = 6;
            //uc8.Left = 3;
            //uc8.Width = panData8.Width - 6;
            //uc8.Height = panData8.Height - 12;
            //uc8.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;

            panData9.Top = panData1.Top;
            panData9.Left = panData1.Left;
            panData9.Width = panData1.Width;
            panData9.Height = panData1.Height;

            //panData9.Controls.Add(uc9);
            //uc9.Top = 6;
            //uc9.Left = 3;
            //uc9.Width = panData9.Width - 6;
            //uc9.Height = panData9.Height - 12;
            //uc9.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;

            panData10.Top = panData1.Top;
            panData10.Left = panData1.Left;
            panData10.Width = panData1.Width;
            panData10.Height = panData1.Height;

            //panData10.Controls.Add(uc10);
            //uc10.Top = 6;
            //uc10.Left = 3;
            //uc10.Width = panData10.Width - 6;
            //uc10.Height = panData10.Height - 12;
            //uc10.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;

        }

        private void frm_실적정보_Load(object sender, EventArgs e)
        {
            dtpDay1.Text = DateTime.Now.ToString("yyyy-MM-01");
            dtpDay2.Text = DateTime.Now.ToString("yyyy-MM-dd");

            tBtn_Click(tBtn1, null);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (panData1.Visible == true)
            {
                uc.frmPrt = readyPrt;
                uc.strDay1 = dtpDay1.Text;
                uc.strDay2 = dtpDay2.Text;
                uc.strCust = txtS거래처코드.Text;
                uc.strCondition = "";
                uc.strCondition += "기간: " + dtpDay1.Text + " ~ " + dtpDay2.Text;
                uc.strCondition += "   거래처: " + (txtS거래처코드.Text == "" ? "(전체)" : txtS거래처명.Text);
                uc.bindData();
            }
            //if (panData2.Visible == true)
            //{
            //    uc2.bindData();
            //}
            //if (panData3.Visible == true)
            //{
            //    uc3.bindData();
            //}
            //if (panData4.Visible == true)
            //{
            //    uc4.bindData();
            //}
            //if (panData5.Visible == true)
            //{
            //    uc5.bindData();
            //}
            //if (panData6.Visible == true)
            //{
            //    uc6.bindData();
            //}
            //if (panData7.Visible == true)
            //{
            //    uc7.bindData();
            //}
            //if (panData8.Visible == true)
            //{
            //    uc8.bindData();
            //}
            //if (panData9.Visible == true)
            //{
            //    uc9.bindData();
            //}
            //if (panData10.Visible == true)
            //{
            //    uc10.bindData();
            //}
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn출력_Click(object sender, EventArgs e)
        {
            btn출력.Enabled = false;

            if (panData1.Visible == true)
            {
                uc.call_Print();
            }
            //if (panData2.Visible == true)
            //{
            //    uc2.call_Print();
            //}
            //if (panData3.Visible == true)
            //{
            //    uc3.call_Print();
            //}
            //if (panData4.Visible == true)
            //{
            //    uc4.call_Print();
            //}
            //if (panData5.Visible == true)
            //{
            //    uc5.call_Print();
            //}
            //if (panData6.Visible == true)
            //{
            //    uc6.call_Print();
            //}
            //if (panData7.Visible == true)
            //{
            //    uc7.call_Print();
            //}
            //if (panData8.Visible == true)
            //{
            //    uc8.call_Print();
            //}
            //if (panData9.Visible == true)
            //{
            //    uc9.call_Print();
            //}
            //if (panData10.Visible == true)
            //{
            //    uc10.call_Print();
            //}

            btn출력.Enabled = true;
        }

        private void tBtn_Click(object sender, EventArgs e)
        {
            string sTabName = ((Button)sender).Tag.ToString();

            tBtn1.Visible = true;
            tBtn2.Visible = true;
            tBtn3.Visible = true;
            tBtn4.Visible = true;
            tBtn5.Visible = true;
            tBtn6.Visible = true;
            tBtn7.Visible = true;
            tBtn8.Visible = true;
            tBtn9.Visible = true;
            tBtn10.Visible = true;

            tBtn1.BackColor = Color.LightGray;
            tBtn2.BackColor = Color.LightGray;
            tBtn3.BackColor = Color.LightGray;
            tBtn4.BackColor = Color.LightGray;
            tBtn5.BackColor = Color.LightGray;
            tBtn6.BackColor = Color.LightGray;
            tBtn7.BackColor = Color.LightGray;
            tBtn8.BackColor = Color.LightGray;
            tBtn9.BackColor = Color.LightGray;
            tBtn10.BackColor = Color.LightGray;

            tBtn1.FlatAppearance.BorderColor = Color.LightGray;
            tBtn2.FlatAppearance.BorderColor = Color.LightGray;
            tBtn3.FlatAppearance.BorderColor = Color.LightGray;
            tBtn4.FlatAppearance.BorderColor = Color.LightGray;
            tBtn5.FlatAppearance.BorderColor = Color.LightGray;
            tBtn6.FlatAppearance.BorderColor = Color.LightGray;
            tBtn7.FlatAppearance.BorderColor = Color.LightGray;
            tBtn8.FlatAppearance.BorderColor = Color.LightGray;
            tBtn9.FlatAppearance.BorderColor = Color.LightGray;
            tBtn10.FlatAppearance.BorderColor = Color.LightGray;

            tBtn1.ForeColor = Color.DimGray;
            tBtn2.ForeColor = Color.DimGray;
            tBtn3.ForeColor = Color.DimGray;
            tBtn4.ForeColor = Color.DimGray;
            tBtn5.ForeColor = Color.DimGray;
            tBtn6.ForeColor = Color.DimGray;
            tBtn7.ForeColor = Color.DimGray;
            tBtn8.ForeColor = Color.DimGray;
            tBtn9.ForeColor = Color.DimGray;
            tBtn10.ForeColor = Color.DimGray;

            panData1.Visible = false;
            panData2.Visible = false;
            panData3.Visible = false;
            panData4.Visible = false;
            panData5.Visible = false;
            panData6.Visible = false;
            panData7.Visible = false;
            panData8.Visible = false;
            panData9.Visible = false;
            panData10.Visible = false;

            switch (sTabName)
            {
                case "1":
                    tBtn1.BackColor = Color.PowderBlue;
                    tBtn1.ForeColor = Color.Black;
                    tBtn1.FlatAppearance.BorderColor = Color.PowderBlue;
                    panData1.Visible = true;
                    break;
                case "2":
                    tBtn2.BackColor = Color.PowderBlue;
                    tBtn2.ForeColor = Color.Black;
                    tBtn2.FlatAppearance.BorderColor = Color.PowderBlue;
                    panData2.Visible = true;
                    break;
                case "3":
                    tBtn3.BackColor = Color.PowderBlue;
                    tBtn3.ForeColor = Color.Black;
                    tBtn3.FlatAppearance.BorderColor = Color.PowderBlue;
                    panData3.Visible = true;
                    break;
                case "4":
                    tBtn4.BackColor = Color.PowderBlue;
                    tBtn4.ForeColor = Color.Black;
                    tBtn4.FlatAppearance.BorderColor = Color.PowderBlue;
                    panData4.Visible = true;
                    break;
                case "5":
                    tBtn5.BackColor = Color.PowderBlue;
                    tBtn5.ForeColor = Color.Black;
                    tBtn5.FlatAppearance.BorderColor = Color.PowderBlue;
                    panData5.Visible = true;
                    break;
                case "6":
                    tBtn6.BackColor = Color.PowderBlue;
                    tBtn6.ForeColor = Color.Black;
                    tBtn6.FlatAppearance.BorderColor = Color.PowderBlue;
                    panData6.Visible = true;
                    break;
                case "7":
                    tBtn7.BackColor = Color.PowderBlue;
                    tBtn7.ForeColor = Color.Black;
                    tBtn7.FlatAppearance.BorderColor = Color.PowderBlue;
                    panData7.Visible = true;
                    break;
                case "8":
                    tBtn8.BackColor = Color.PowderBlue;
                    tBtn8.ForeColor = Color.Black;
                    tBtn8.FlatAppearance.BorderColor = Color.PowderBlue;
                    panData8.Visible = true;
                    break;
                case "9":
                    tBtn9.BackColor = Color.PowderBlue;
                    tBtn9.ForeColor = Color.Black;
                    tBtn9.FlatAppearance.BorderColor = Color.PowderBlue;
                    panData9.Visible = true;
                    break;
                case "10":
                    tBtn10.BackColor = Color.PowderBlue;
                    tBtn10.ForeColor = Color.Black;
                    tBtn10.FlatAppearance.BorderColor = Color.PowderBlue;
                    panData10.Visible = true;
                    break;
                default:
                    tBtn1.BackColor = Color.PowderBlue;
                    tBtn1.ForeColor = Color.Black;
                    tBtn1.FlatAppearance.BorderColor = Color.PowderBlue;
                    panData1.Visible = true;
                    break;
            }
        }

        private void frm실적정보_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (readyPrt != null)
            {
                readyPrt.Close();
            }
        }

    }
}
