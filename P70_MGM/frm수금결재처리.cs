using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 스마트팩토리.CLS;

namespace 스마트팩토리.P70_MGM
{
    public partial class frm수금결재처리 : Form
    {
        private wnGConstant wConst = new wnGConstant();
        private bool bEditText = false;
        private int nCheckCol = 10;

        public frm수금결재처리()
        {
            InitializeComponent();
        }

        private void GridRecord_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView grd = (DataGridView)sender;

            // 헤더 nCheckCol 번째 컬럼 클릭시
            if (e.ColumnIndex != nCheckCol) return;

            if (bHeadCheck == false)
            {
                grd.Columns[nCheckCol].HeaderText = "결재 [v]";
                bHeadCheck = true;
                select_Check(grd, bHeadCheck);
            }
            else
            {
                grd.Columns[nCheckCol].HeaderText = "결재 [ ]";
                bHeadCheck = false;
                select_Check(grd, bHeadCheck);
            }
            grd.RefreshEdit();
            grd.Refresh();
        }

        private bool bHeadCheck = false;
        private void select_Check(DataGridView grd, bool bCheck)
        {
            for (int kk = 0; kk < grd.Rows.Count; kk++)
            {
                if (bCheck == true)
                {
                    grd.Rows[kk].Cells[nCheckCol].Value = true;
                }
                else
                {
                    grd.Rows[kk].Cells[nCheckCol].Value = false;
                }
            }
        }

        private void frm수금결재처리_Load(object sender, EventArgs e)
        {
            GridRecord.Columns[nCheckCol].HeaderCell.Style.BackColor = Color.PaleGreen;

            init_ComboBox();

            this.init_InputBox();
            btnSearch_Click(this, null);
        }

        public void init_InputBox()
        {
            dtpDay1.Text = DateTime.Now.ToString("yyyy-MM-dd");
            dtpDay2.Text = DateTime.Now.ToString("yyyy-MM-dd");

            txtS거래처코드.Text = "";
            txtS거래처명.Text = "";
            cmbS구분.SelectedIndex = 0;
        }

        private void init_ComboBox()
        {
            string sqlQuery = "";

            wConst.set_Combo수금구분(true, cmbS구분);

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.bindData(this.makeSearchCondition());

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            if (validate_InputBox() == false)
            {
                // 필수 입력 체크.
                btnSave.Enabled = true;
                return;
            }

            lblSearch.Left = this.ClientSize.Width / 2 - lblSearch.Width / 2;
            lblSearch.Top = this.ClientSize.Height / 2 - lblSearch.Height / 2;
            lblSearch.Text = "Saving...";
            lblSearch.Visible = true;
            lblSearch.BringToFront();
            Application.DoEvents();

            bool bResult = false;

            progBar.Maximum = GridRecord.Rows.Count;

            for (int kk = 0; kk < GridRecord.Rows.Count; kk++)
            {
                if ((bool)GridRecord.Rows[kk].Cells[nCheckCol].Value == true)
                {
                    progBar.Value = kk + 1;
                    Application.DoEvents();

                    bResult = this.savePost(kk);
                    if (bResult == false) break;
                }
            }

            if (bResult == true)
            {
                MessageBox.Show("결재 완료되었습니다.");
            }

            btnSearch_Click(this, null);

            progBar.Value = 0;
            lblSearch.Visible = false;
            btnSave.Enabled = true;
        }

        private string makeSearchCondition()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" and a.COLLECT_DATE >= '" + dtpDay1.Text + "' ");
            sb.Append(" and a.COLLECT_DATE <= '" + dtpDay2.Text + "' ");

            //미결 건만.
            sb.Append(" and isnull(a.COLLECT_LAST, '0') <> '1' ");

            switch (this.txtS거래처코드.Text)
            {
                case "":
                    sb.Append("");
                    break;
                default:
                    sb.Append(" and a.CUST_CODE1 = '" + txtS거래처코드.Text + "' ");
                    break;
            }

            switch ("" + this.cmbS구분.SelectedValue.ToString())
            {
                case "":
                    sb.Append("");
                    break;
                default:
                    sb.Append(" and a.COLLECT_KIND = '" + cmbS구분.SelectedValue.ToString() + "' ");
                    break;
            }

            return sb.ToString();
        }

        private void bindData(string condition)
        {
            lblSearch.Left = this.ClientSize.Width / 2 - lblSearch.Width / 2;
            lblSearch.Top = this.ClientSize.Height / 2 - lblSearch.Height / 2;
            lblSearch.Text = "Searching...";
            lblSearch.Visible = true;
            Application.DoEvents();

            this.GridRecord.DataSource = null;
            this.GridRecord.RowCount = 0;

            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_PRODUCT_COLLECT_DEPT_List(condition);

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.GridRecord.RowCount = dt.Rows.Count;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.GridRecord.Rows[i].Cells[0].Value = (i + 1).ToString();
                        this.GridRecord.Rows[i].Cells[1].Value = dt.Rows[i]["COLLECT_DATE"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[2].Value = dt.Rows[i]["COLLECT_NUM"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[3].Value = wConst.get_수금구분_명칭(dt.Rows[i]["COLLECT_KIND"].ToString().Trim());
                        this.GridRecord.Rows[i].Cells[4].Value = dt.Rows[i]["CUST_CODE1"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[5].Value = dt.Rows[i]["CUST_NAME1"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[6].Value = dt.Rows[i]["REP_NAME1"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[7].Value = dt.Rows[i]["COMP_NUM1"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[8].Value = dt.Rows[i]["MAN_NAME1"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[9].Value = decimal.Parse(dt.Rows[i]["COLLECT_AMOUNT"].ToString().Trim()).ToString("#,0");
                        
                        if (dt.Rows[i]["COLLECT_LAST"].ToString().Trim() == "1")
                        {
                            this.GridRecord.Rows[i].Cells[10].Value = true;
                        }
                        else
                        {
                            this.GridRecord.Rows[i].Cells[10].Value = false;
                        }

                        this.GridRecord.Rows[i].Cells[11].Value = dt.Rows[i]["TAX_DATE"].ToString().Trim();

                        if (dt.Rows[i]["KIND_AB"].ToString().Trim() == "1")
                        {
                            this.GridRecord.Rows[i].Cells[12].Value = "B수금";
                        }
                        else
                        {
                            this.GridRecord.Rows[i].Cells[12].Value = "A수금";
                        }

                        this.GridRecord.Rows[i].Cells[13].Value = dt.Rows[i]["COLLECT_KIND"].ToString().Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("검색중에 오류가 발생했습니다.");
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
            }

            lblSearch.Visible = false;
        }

        #region 검색 거래처 ##############################################################################################################

        private void btnS거래처_Click(object sender, EventArgs e)
        {
            bEditText = false;

            wConst.call_popRef_Cust("", txtS거래처코드, txtS거래처명, "");
            //if (txtS거래처코드.Text != "")
            //{
            //    get_Srch_Cust_Info(txtS거래처코드.Text, txtS거래처명);
            //}

            bEditText = true;
            SendKeys.Send("{TAB}");
        }

        private void txtS거래처_Enter(object sender, EventArgs e)
        {
            bEditText = true;
        }

        private void txtS거래처_TextChanged(object sender, EventArgs e)
        {
            if (bEditText == false) return;

            txtS거래처코드.Text = "";
        }

        private void txtS거래처_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;

                bEditText = false;

                bool bPopSrch = true;

                if (txtS거래처코드.Text != "")
                {
                    bPopSrch = false;
                }

                if (bPopSrch == true)
                {
                    wConst.call_popRef_Cust(txtS거래처명.Text, txtS거래처코드, txtS거래처명, "");
                    //get_Srch_Cust_Info(txtS거래처코드.Text, txtS거래처명);
                }

                if (txtS거래처코드.Text == "")
                {
                    init_SearchText_Cust();
                }
                SendKeys.Send("{TAB}");
                bEditText = true;
            }
        }

        private void init_SearchText_Cust()
        {
            txtS거래처코드.Text = "";
            txtS거래처명.Text = "";
        }

        //private void get_Srch_Cust_Info(string sID, TextBox txt_Name)
        //{
        //    try
        //    {
        //        wnDm wDm = new wnDm();
        //        DataTable dt = null;
        //        dt = wDm.fn_CUSTOMER_Detail(sID);

        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            txt_Name.Text = dt.Rows[0]["CUST_NAME"].ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
        //    }
        //}

        #endregion 검색 거래처 ##############################################################################################################

        private bool validate_InputBox()
        {
            bool bRet = true;

            try
            {
                //if (txt거래처코드.Text == "")
                //{
                //    MessageBox.Show("[ 거래처 ] 를 입력하세요.");
                //    return false;
                //}

                int nChkCnt = 0;
                for (int kk = 0; kk < GridRecord.Rows.Count; kk++)
                {
                    if ((bool)GridRecord.Rows[kk].Cells[nCheckCol].Value == true)
                    {
                        nChkCnt += 1;
                        break;
                    }
                }
                if (nChkCnt == 0)
                {
                    MessageBox.Show("결재할 대상이 없습니다.");
                    return false;
                }

            }
            catch (Exception ex)
            {
                bRet = false;
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
                MessageBox.Show("입력 데이터 확인 중에 오류가 있습니다.");
            }
            return bRet;
        }

        private bool savePost(int nRow)
        {
            bool bRet = false;

            try
            {
                wnAdo wAdo = new wnAdo();

                StringBuilder sb = new StringBuilder();

                string s일자 = "" + (string)GridRecord.Rows[nRow].Cells[1].Value;
                string s번호 = "" + (string)GridRecord.Rows[nRow].Cells[2].Value;
                string s구분코드 = "" + (string)GridRecord.Rows[nRow].Cells[13].Value;

                sb.AppendLine(" declare @maxKey nvarchar(4) ");

                //일반용 전표번호
                sb.AppendLine(" select @maxKey = right('000' + convert(nvarchar(4), isnull(max(convert(int, COLLECT_NUM)), 0) + 1), 4) ");
                sb.AppendLine(" from PRODUCT_COLLECT ");
                sb.AppendLine(" where COLLECT_DATE = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ");
                sb.AppendLine("     and COLLECT_NUM < '7000' ");

                sb.AppendLine(" if ('" + s구분코드 + "' = '90') ");
                sb.AppendLine(" begin ");
                sb.AppendLine("     select @maxKey = right('000' + convert(nvarchar(4), isnull(max(convert(int, COLLECT_NUM)), 9000) + 1), 4) ");
                sb.AppendLine("     from PRODUCT_COLLECT ");
                sb.AppendLine("     where COLLECT_DATE = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ");
                sb.AppendLine("         and COLLECT_NUM > '9000' ");
                sb.AppendLine(" end ");

                sb.AppendLine(" if ('" + s구분코드 + "' = '72') ");
                sb.AppendLine(" begin ");
                sb.AppendLine("     select @maxKey = right('000' + convert(nvarchar(4), isnull(max(convert(int, COLLECT_NUM)), 7000) + 1), 4) ");
                sb.AppendLine("     from PRODUCT_COLLECT ");
                sb.AppendLine("     where COLLECT_DATE = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ");
                sb.AppendLine("         and COLLECT_NUM > '7000' ");
                sb.AppendLine("         and COLLECT_NUM < '9000' ");
                sb.AppendLine(" end ");

                sb.AppendLine(" insert into PRODUCT_COLLECT ( ");
                sb.AppendLine("     COLLECT_DATE ");
                sb.AppendLine("     , COLLECT_NUM ");
                sb.AppendLine("     , COLLECT_KIND ");
                sb.AppendLine("     , CUST_CODE1 ");
                sb.AppendLine("     , MAN_CODE1 ");
                sb.AppendLine("     , COLLECT_AMOUNT ");
                sb.AppendLine("     , BILL_NUMBER ");
                sb.AppendLine("     , BILL_TYPE ");
                sb.AppendLine("     , HANDLE_DATE ");
                sb.AppendLine("     , ISSUE_DATE ");
                sb.AppendLine("     , SETTLE_DATE ");
                sb.AppendLine("     , SETTLE_CUST ");
                sb.AppendLine("     , SETTLE_CUST1 ");
                sb.AppendLine("     , COLLECT_NAME1 ");
                sb.AppendLine("     , COLLECT_NAME2 ");
                sb.AppendLine("     , MEMO ");
                sb.AppendLine("     , USER_CODE ");
                sb.AppendLine("     , PRODUCT_KIND ");
                sb.AppendLine("     , CUST_NAME1 ");
                sb.AppendLine("     , MAN_NAME1 ");
                sb.AppendLine("     , DEPT_NAME1 ");
                sb.AppendLine("     , REP_NAME1 ");
                sb.AppendLine("     , COMP_NUM1 ");
                sb.AppendLine("     , KIND_AB ");
                sb.AppendLine("     , DEAL_TYPE ");
                sb.AppendLine("     , DEPT_CODE1 ");
                sb.AppendLine("     , 미송구분 ");
                sb.AppendLine(" ) select ");
                sb.AppendLine("     COLLECT_DATE ");
                sb.AppendLine("     , @maxKey ");
                sb.AppendLine("     , COLLECT_KIND ");
                sb.AppendLine("     , CUST_CODE1 ");
                sb.AppendLine("     , MAN_CODE1 ");
                sb.AppendLine("     , COLLECT_AMOUNT ");
                sb.AppendLine("     , BILL_NUMBER ");
                sb.AppendLine("     , BILL_TYPE ");
                sb.AppendLine("     , HANDLE_DATE ");
                sb.AppendLine("     , ISSUE_DATE ");
                sb.AppendLine("     , SETTLE_DATE ");
                sb.AppendLine("     , SETTLE_CUST ");
                sb.AppendLine("     , SETTLE_CUST1 ");
                sb.AppendLine("     , COLLECT_NAME1 ");
                sb.AppendLine("     , COLLECT_NAME2 ");
                sb.AppendLine("     , MEMO ");
                sb.AppendLine("     , USER_CODE ");
                sb.AppendLine("     , PRODUCT_KIND ");
                sb.AppendLine("     , CUST_NAME1 ");
                sb.AppendLine("     , MAN_NAME1 ");
                sb.AppendLine("     , DEPT_NAME1 ");
                sb.AppendLine("     , REP_NAME1 ");
                sb.AppendLine("     , COMP_NUM1 ");
                sb.AppendLine("     , KIND_AB ");
                sb.AppendLine("     , DEAL_TYPE ");
                sb.AppendLine("     , DEPT_CODE1 ");
                sb.AppendLine("     , 미송구분 ");
                sb.AppendLine(" from PRODUCT_COLLECT_DEPT ");
                sb.AppendLine(" where 1=1 ");
                sb.AppendLine("     and COLLECT_DATE = '" + s일자 + "' ");
                sb.AppendLine("     and COLLECT_NUM = '" + s번호 + "' ");

                sb.AppendLine(" update PRODUCT_COLLECT_DEPT set ");
                sb.AppendLine("     COLLECT_LAST = '1' ");
                sb.AppendLine("     , TAX_DATE = convert(nvarchar(10), getdate(), 120) ");
                sb.AppendLine(" where 1=1 ");
                sb.AppendLine("     and COLLECT_DATE = '" + s일자 + "' ");
                sb.AppendLine("     and COLLECT_NUM = '" + s번호 + "' ");

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                //sCommand.Parameters.AddWithValue("@COLLECT_DATE", dtp일자.Text);
                //sCommand.Parameters.AddWithValue("@COLLECT_KIND", "" + cmb구분.SelectedValue.ToString());

                int qResult = wAdo.SqlCommandEtc(sCommand, "save-PRODUCT_COLLECT");
                if (qResult > 0) bRet = true;
                else bRet = false;

                if (bRet == true)
                {
                }
                else
                {
                    MessageBox.Show("저장 중에 오류가 발생했습니다.");
                }
            }
            catch (Exception ex)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
                MessageBox.Show("데이터베이스에 문제가 발생했습니다.");
            }
            return bRet;
        }

    }
}
