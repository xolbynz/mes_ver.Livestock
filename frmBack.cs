using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 스마트팩토리.CLS;
using 스마트팩토리.Popup;


namespace 스마트팩토리
{
    public partial class frmBack : Form
    {
        private IfrmInterface parentFrm = null;
        string today = DateTime.Today.ToString("yyyy-MM-dd");
        Timer timer;
        Timer timer2;

        public string scheduleDate;
        public frmBack(IfrmInterface pFrm)
        {
            InitializeComponent();
            parentFrm = pFrm;
        }

        private void frmBack_Load(object sender, EventArgs e)
        {
            notice_ExpertDate(sender, e);
            string today = DateTime.Today.AddMonths(0).ToString("yyyy-MM-dd");
            Srch_by_LotNo("L" + today.Replace("-", "").Substring(2));

            ComInfo.gridHeaderSet(MadeGrid);
          
            updateList(sender, e);
            notice_ExpertDate(sender, e);
            timer = new Timer();
            timer.Enabled = true;
            timer.Interval = 1000;
            timer.Tick += new EventHandler(twin);

            timer.Start();

            timer2 = new Timer();
            timer2.Enabled = true;
            timer2.Interval = 60000;
            timer2.Tick += new EventHandler(notice_ExpertDate);
            timer2.Tick += new EventHandler(updateList);

            timer2.Start();

        }

        public void updateList(object sender, EventArgs e)
        {
            string today = DateTime.Today.AddMonths(0).ToString("yyyy-MM-dd");
            Srch_by_LotNo(today);
        }

        public void Srch_by_LotNo(string today)
        {
            try
            {
                // 생산 & 지시정보 + 지시 원료육
                wnDm wDm = new wnDm();
                DataTable dt = null;
                DataTable dt2 = null;

                dt = wDm.fn_WorkDay_toip_todaylist(today);
                dt2 = wDm.fn_WorkDay_Made_todaylist(today);
                if (dt != null && dt2 != null && dt.Rows.Count > 0 && dt2.Rows.Count > 0)
                {
                    textBox1.SendToBack();
                    textBox2.SendToBack();
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                    ToipGrid.Rows.Clear();
                    MadeGrid.Rows.Clear();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ToipGrid.Rows.Add();
                        ToipGrid.Rows[i].Cells["TO_LABEL_NM"].Value = dt.Rows[i]["LABEL_NM"].ToString();
                        ToipGrid.Rows[i].Cells["TO_UNIT_NM"].Value = dt.Rows[i]["UNIT_NM"].ToString();
                        ToipGrid.Rows[i].Cells["TO_COUNTRY_NM"].Value = dt.Rows[i]["COUNTRY_NM"].ToString();
                        ToipGrid.Rows[i].Cells["TO_CUST_NM"].Value = dt.Rows[i]["CUST_NM"].ToString();
                        ToipGrid.Rows[i].Cells["TO_TOTAL_AMT"].Value = decimal.Parse(dt.Rows[i]["TOTAL_AMT"].ToString()).ToString("#,0.######");
                        ToipGrid.Rows[i].Cells["TO_LOT_NO"].Value = dt.Rows[i]["LOT_NO"].ToString();
                    }

                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        MadeGrid.Rows.Add();
                        MadeGrid.Rows[i].Cells["MD_LABEL_NM"].Value = dt2.Rows[i]["LABEL_NM"].ToString();
                        MadeGrid.Rows[i].Cells["MD_UNIT_NM"].Value = dt2.Rows[i]["UNIT_NM"].ToString();
                        MadeGrid.Rows[i].Cells["MD_SPEC"].Value = dt2.Rows[i]["SPEC"].ToString();
                        MadeGrid.Rows[i].Cells["MD_TOTAL_AMT"].Value = decimal.Parse(dt2.Rows[i]["TOTAL_AMT"].ToString()).ToString("#,0.######");
                        MadeGrid.Rows[i].Cells["MD_LOT_NO"].Value = dt2.Rows[i]["LOT_NO"].ToString();
                        MadeGrid.Rows[i].Cells["MD_FROZEN_GUBUN"].Value = dt2.Rows[i]["FROZEN_GUBUN"].ToString();
                    }
                }
                else
                {
                    textBox1.BringToFront();
                    textBox2.BringToFront();
                    textBox1.Visible = true;
                    textBox2.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("검색중 오류가 발생했습니다");
                Console.WriteLine(ex);
                return;
            }
        }

        private void notice_ExpertDate(object obj, EventArgs e)
        {
            wnDm wdm = new wnDm();
            DataTable dt = new DataTable();
            dt = wdm.notice_Exprtdate();

            if (dt != null && dt.Rows.Count > 0)
            {
                ExprtDate_GridView.RowCount = dt.Rows.Count;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["NO"] = (i + 1); //숫자의 경우  문자면 .string : 계산된 값을 READ 한 테이블로 다시 전달한다 - 출력물 사용
                    ExprtDate_GridView.Rows[i].Cells["NO"].Value = dt.Rows[i]["NO"].ToString();
                    ExprtDate_GridView.Rows[i].Cells["INPUT_DATE"].Value = dt.Rows[i]["INPUT_DATE"].ToString();
                    ExprtDate_GridView.Rows[i].Cells["RAW_MAT_NM"].Value = dt.Rows[i]["RAW_MAT_NM"].ToString();
                    ExprtDate_GridView.Rows[i].Cells["TOTAL_AMT"].Value = decimal.Parse(dt.Rows[i]["TOTAL_AMT"].ToString()).ToString("#,0.######");
                    ExprtDate_GridView.Rows[i].Cells["UNIT_NM"].Value = dt.Rows[i]["UNIT_NM"].ToString();
                    ExprtDate_GridView.Rows[i].Cells["CUST_NM"].Value = dt.Rows[i]["CUST_NM"].ToString();
                    ExprtDate_GridView.Rows[i].Cells["FROZEN_GUBUN"].Value = dt.Rows[i]["FROZEN_GUBUN"].ToString();
                    ExprtDate_GridView.Rows[i].Cells["EXPRT_DATE"].Value = dt.Rows[i]["EXPRT_DATE"].ToString();
                    if (DateTime.Parse(dt.Rows[i]["EXPRT_DATE"].ToString()) < DateTime.Parse((DateTime.Today.AddMonths(0).ToString("yyyy-MM-dd"))))
                    {
                        ExprtDate_GridView.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        ExprtDate_GridView.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                    }
                }
            }
        }

        private void twin(object sender, EventArgs e)
        {
            if (lbl_ExpertDate.ForeColor == Color.Black)
            {
                lbl_ExpertDate.ForeColor = Color.Red;
            }
            else
            {
                lbl_ExpertDate.ForeColor = Color.Black;
            }
        }

        private void frmBack_Resize(object sender, EventArgs e)
        {
            panCenter.Left = this.ClientSize.Width / 2 - panCenter.Width / 2;
            panCenter.Top = this.ClientSize.Height / 2 - panCenter.Height / 2;

            w_home1.Left = this.ClientSize.Width / 2 - panCenter.Width / 2;
            w_home1.Top = this.ClientSize.Height / 2 - panCenter.Height / 2;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SendKeys.Send("{LEFT}");
            timer1.Enabled = false;
        }

        private void Bookmark1_Click(object sender, EventArgs e)
        {
            string sFrmName = ((Button)sender).Tag.ToString();
            string sMnuName = ((Button)sender).Text.ToString();
            parentFrm.sub_Form(sFrmName, sMnuName);
        }

        private void Bookmark2_Click(object sender, EventArgs e)
        {
            string sFrmName = ((Button)sender).Tag.ToString();
            string sMnuName = ((Button)sender).Text.ToString();
            parentFrm.sub_Form(sFrmName, sMnuName);
        }

        private void Bookmark3_Click(object sender, EventArgs e)
        {
            string sFrmName = ((Button)sender).Tag.ToString();
            string sMnuName = ((Button)sender).Text.ToString();
            parentFrm.sub_Form(sFrmName, sMnuName);
        }

        private void Bookmark4_Click(object sender, EventArgs e)
        {
            string sFrmName = ((Button)sender).Tag.ToString();
            string sMnuName = ((Button)sender).Text.ToString();
            parentFrm.sub_Form(sFrmName, sMnuName);
        }

        private void Bookmark5_Click(object sender, EventArgs e)
        {
            string sFrmName = ((Button)sender).Tag.ToString();
            string sMnuName = ((Button)sender).Text.ToString();
            parentFrm.sub_Form(sFrmName, sMnuName);
        }

        private void Bookmark6_Click(object sender, EventArgs e)
        {
            string sFrmName = ((Button)sender).Tag.ToString();
            string sMnuName = ((Button)sender).Text.ToString();
            parentFrm.sub_Form(sFrmName, sMnuName);
        }

        private void panel7_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://320.co.kr");

        }

        private void MadeGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            string sellotno = MadeGrid.Rows[e.RowIndex].Cells["MD_LOT_NO"].Value.ToString();
            //for (int i = 0; i < ToipGrid.Rows.Count; i++)
            //{
            //    if (ToipGrid.Rows[i].Cells["TO_LOT_NO"].Value.ToString().Equals(sellotno))
            //    {
            //        ToipGrid.Rows[i].Cells["TO_LOT_NO"].Selected = true;
            //        ToipGrid.Focus();
            //        tabControl1.SelectedIndex = 0;
            //    }
            //}

            string sFrmName = "P50_QUA.frm씨지엠공정추적";
            string sMnuName = "제품 공정추적";
            parentFrm.sub_Form(sFrmName, sMnuName, sellotno);
        }
        private void ToipGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            string sellotno = MadeGrid.Rows[e.RowIndex].Cells["MD_LOT_NO"].Value.ToString();

            string sFrmName = "P20_ORD.frm원자재투입현황";
            string sMnuName = "원자재 투입현황";
            parentFrm.sub_Form(sFrmName, sMnuName, sellotno);
        }
      

      


    }
}
