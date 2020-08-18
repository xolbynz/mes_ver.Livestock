using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 스마트팩토리.CLS;

namespace 스마트팩토리.Popup
{
    public partial class pop거래처검색영업관리 : Form
    {
        public string sCode = "";
        public string sName = "";
        public string sCountry = "";
        public string sCustGbn = "";
        public string sCustNm = "";
        public string sBalance = "";
        private int intPageSize = 0;
        private int intTotalRecords = 0;
        private int intPageCount = 0;
        private int nPageSize = int.Parse(Common.p_PageSize);
        private int intCurrentPage = 1;
        public string start_date = "";
        public string end_date = "";
        public string str매출원장 = "";
        public string str매출등록 = "";



        public pop거래처검색영업관리()
        {
            InitializeComponent();
        }

        private void pop거래처검색영업관리_Load(object sender, EventArgs e)
        {
            cust_logic();
        }
        private void cust_logic()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("where 1=1 ");
            if (!sCustGbn.Equals("") && !sCustNm.Equals(""))
            {
                if (start_date.Equals(""))
                {
                    sb.AppendLine("and CUST_GUBUN = '" + sCustGbn + "' and CUST_NM LIKE '%" + sCustNm + "%' ");
                }
                else if (str매출등록.Equals("2"))
                {
                    sb.AppendLine("and CUST_GUBUN = '" + sCustGbn + "' and CUST_NM LIKE '%" + sCustNm + "%' ");
                    sb.AppendLine(" and B.SALES_DATE >= '" + start_date + "' and B.SALES_DATE <= '" + str매출원장 + "' ");
                }
            }
            else if (!sCustGbn.Equals(""))
            {
                sb.AppendLine("and CUST_GUBUN = '" + sCustGbn + "' ");
            }
            else if (!sCustNm.Equals(""))
            {
                sb.AppendLine("and CUST_NM LIKE '%" + sCustNm + "%' ");
            }
            else if (str매출원장.Equals("3"))
            {
                sb.AppendLine(" AND CUST_NM LIKE '%" + sCustNm + "%'");

            }

            sb.AppendLine(" and USED_CD = '1' ");
            bindData(sb.ToString());
        }

        private void bindData(string condition)
        {
            this.GridRecord.DataSource = null;
            this.GridRecord.RowCount = 0;

            wnDm wDm = new wnDm();
            DataTable dt = null;
            if( start_date.Equals("") )
            {
                dt = wDm.fn_Cust_List(condition);
            }
            else if (str매출등록.Equals("2")) 
            {
                dt = wDm.fn_Cust_Sales_List(condition);
            }

            else if ((str매출원장.Equals("3")))
            {
                dt = wDm.fn_Cust_rsSales_List(condition);
            }

            // For Page view.
            intPageSize = nPageSize;
            intTotalRecords = getCount(dt);
            intPageCount = intTotalRecords / intPageSize;

            // Adjust page count if the last page contains partial page.
            if (intTotalRecords % intPageSize > 0)
                intPageCount++;

            intCurrentPage = 0;

            cmbPage.Items.Clear();
            if (intTotalRecords == 0)
            {
                cmbPage.Items.Add("1");
            }
            else
            {
                for (int kk = 0; kk < intPageCount; kk++)
                {
                    cmbPage.Items.Add((kk + 1).ToString());
                }
            }
            cmbPage.SelectedIndex = 0;

            loadPage(dt);
        }
        private int getCount(DataTable dt)
        {
            int intCount = 0;

            if (dt != null)
            {
                intCount = dt.Rows.Count;
            }

            return intCount;
        }

        private void loadPage(DataTable dt)
        {
            this.lblStatus.Text = "- / -";

            try
            {
                int intSkip = 0;
                intSkip = (intCurrentPage * intPageSize);

                GridRecord.Columns[0].DefaultCellStyle.ForeColor = Color.ForestGreen;
                GridRecord.Columns[1].DefaultCellStyle.ForeColor = Color.ForestGreen;
                GridRecord.Columns[2].DefaultCellStyle.ForeColor = Color.ForestGreen;

                GridRecord.Columns[0].Frozen = true;
                GridRecord.Columns[1].Frozen = true;
                GridRecord.Columns[2].Frozen = true;

                GridRecord.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                GridRecord.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                GridRecord.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                GridRecord.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                GridRecord.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                GridRecord.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                GridRecord.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                this.lblStatus.Text = (intCurrentPage + 1).ToString() + " / " + intPageCount.ToString();

                if (dt.Rows.Count > 0)
                {
                    GridRecord.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        if (dt.Rows[i]["USED_CD"].ToString().Equals("2"))
                        {
                            GridRecord.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                        }
                        else if (dt.Rows[i]["USED_CD"].ToString().Equals("3"))
                        {
                            GridRecord.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                        else if (dt.Rows[i]["USED_CD"].ToString().Equals("1"))
                        {
                            GridRecord.Rows[i].DefaultCellStyle.BackColor = Color.Empty;
                        }

                        GridRecord.Rows[i].Cells[0].Value = dt.Rows[i]["CUST_CD"].ToString();
                        GridRecord.Rows[i].Cells[1].Value = dt.Rows[i]["CUST_GUBUN_NM"].ToString();
                        GridRecord.Rows[i].Cells[2].Value = dt.Rows[i]["CUST_NM"].ToString();
                        GridRecord.Rows[i].Cells[3].Value = dt.Rows[i]["SAUP_NO"].ToString();
                        GridRecord.Rows[i].Cells[4].Value = dt.Rows[i]["UPTAE"].ToString();
                        GridRecord.Rows[i].Cells[5].Value = dt.Rows[i]["JONGMOK"].ToString();
                        GridRecord.Rows[i].Cells[6].Value = dt.Rows[i]["CUST_MANAGER"].ToString();
                        GridRecord.Rows[i].Cells["BALANCE"].Value = decimal.Parse(dt.Rows[i]["BALANCE"].ToString()).ToString("#,0.######");
                        
                    }
                }
                else
                {
                    GridRecord.Rows.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("검색중에 오류가 발생했습니다.");
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
                Console.WriteLine(ex + "\n" + ex.ToString());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GridRecord_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            sCode = GridRecord.Rows[e.RowIndex].Cells[0].Value.ToString();
            sName = GridRecord.Rows[e.RowIndex].Cells[2].Value.ToString();
            sBalance = GridRecord.Rows[e.RowIndex].Cells["BALANCE"].Value.ToString();

            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            sCustNm = txtSrch.Text.ToString();
            cust_logic();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            //
        }


        


    }

}
