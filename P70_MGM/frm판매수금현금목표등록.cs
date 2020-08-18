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
    public partial class frm판매수금현금목표등록 : Form
    {
        private wnGConstant wConst = new wnGConstant();
        private bool bData = false;
        private bool bEditText = false;

        public frm판매수금현금목표등록()
        {
            InitializeComponent();
        }

        private void frm판매수금현금목표등록_Load(object sender, EventArgs e)
        {
            spCont.Dock = DockStyle.Fill;

            lblSearch.BringToFront();
            lblBody.BringToFront();

            this.init_ComboBox();
            this.init_InputBox(true);

            cmbS연도.SelectedIndex = 0;
            cmbS구분.SelectedIndex = 0;

            this.bindData(this.makeSearchCondition());
        }

        private void init_ComboBox()
        {
            string sqlQuery = "";

            cmb연도.ValueMember = "코드";
            cmb연도.DisplayMember = "명칭";
            sqlQuery = " select '" + DateTime.Now.ToString("yyyy") + "' as 코드, '" + DateTime.Now.ToString("yyyy") + "년' as 명칭 ";
            for (int kk = int.Parse(DateTime.Now.ToString("yyyy")) - 1; kk >= 2000; kk--)
            {
                sqlQuery += " union all ";
                sqlQuery += " select '" + kk.ToString() + "' as 코드, '" + kk.ToString() + "년' as 명칭 ";
            }
            wConst.ComboBox_Read_NoBlank(cmb연도, sqlQuery);

            cmbS연도.ValueMember = "코드";
            cmbS연도.DisplayMember = "명칭";
            wConst.ComboBox_Read_NoBlank(cmbS연도, sqlQuery);

            cmb구분.ValueMember = "코드";
            cmb구분.DisplayMember = "명칭";
            sqlQuery = " select '1' as 코드, '판매' as 명칭 ";
            sqlQuery += " union all ";
            sqlQuery += " select '2' as 코드, '수금' as 명칭 ";
            sqlQuery += " union all ";
            sqlQuery += " select '3' as 코드, '현금' as 명칭 ";
            wConst.ComboBox_Read_NoBlank(cmb구분, sqlQuery);

            cmbS구분.ValueMember = "코드";
            cmbS구분.DisplayMember = "명칭";
            wConst.ComboBox_Read_ALL(cmbS구분, sqlQuery);
        }

        private void init_InputBox(bool bNew)
        {
            wConst.Form_Clear(spCont.Panel1.Controls);
            cmb연도.SelectedIndex = 0;
            cmb구분.SelectedIndex = 0;
            
            if (bNew == true)
            {
                bData = false;
                btnDelete.Enabled = false;
                cmb연도.Enabled = true;
                cmb구분.Enabled = true;
                txt담당자명.Enabled = true;
                btn담당자.Enabled = true;
            }
            else
            {
                bData = true;
                btnDelete.Enabled = true;
                cmb연도.Enabled = false;
                cmb구분.Enabled = false;
                txt담당자명.Enabled = false;
                btn담당자.Enabled = false;
            }
        }

        private string makeSearchCondition()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" and a.GOAL_YEAR = '" + cmb연도.SelectedValue.ToString() + "' ");

            switch ("" + cmbS구분.SelectedValue.ToString())
            {
                case "":
                    sb.Append("");
                    break;
                default:
                    sb.Append(" and a.GOAL_CODE = '" + cmbS구분.SelectedValue.ToString() + "' ");
                    break;
            }
            switch (this.txtS담당자코드.Text)
            {
                case "":
                    sb.Append("");
                    break;
                default:
                    sb.Append(" and a.MAN_CODE = '" + txtS담당자코드.Text + "' ");
                    break;
            }

            return sb.ToString();
        }

        private void bindData(string condition)
        {
            lblSearch.Left = spCont.Panel2.ClientSize.Width / 2 - lblSearch.Width / 2;
            lblSearch.Top = spCont.Panel2.ClientSize.Height / 2 - lblSearch.Height / 2;
            lblSearch.Visible = true;
            Application.DoEvents();

            this.GridRecord.DataSource = null;
            this.GridRecord.RowCount = 0;

            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_STOCK_GOAL_List(condition);

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.GridRecord.RowCount = dt.Rows.Count;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.GridRecord.Rows[i].Cells[0].Value = dt.Rows[i]["GOAL_YEAR"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[1].Value = dt.Rows[i]["GOAL_CODE"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[2].Value = dt.Rows[i]["MAN_CODE"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[3].Value = dt.Rows[i]["GOAL_YEAR"].ToString().Trim() + "년";
                        this.GridRecord.Rows[i].Cells[4].Value = dt.Rows[i]["담당자명"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[5].Value = dt.Rows[i]["구분명"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[6].Value = decimal.Parse(dt.Rows[i]["M01"].ToString().Trim()).ToString("#,0");
                        this.GridRecord.Rows[i].Cells[7].Value = decimal.Parse(dt.Rows[i]["M02"].ToString().Trim()).ToString("#,0");
                        this.GridRecord.Rows[i].Cells[8].Value = decimal.Parse(dt.Rows[i]["M03"].ToString().Trim()).ToString("#,0");
                        this.GridRecord.Rows[i].Cells[9].Value = decimal.Parse(dt.Rows[i]["M04"].ToString().Trim()).ToString("#,0");
                        this.GridRecord.Rows[i].Cells[10].Value = decimal.Parse(dt.Rows[i]["M05"].ToString().Trim()).ToString("#,0");
                        this.GridRecord.Rows[i].Cells[11].Value = decimal.Parse(dt.Rows[i]["M06"].ToString().Trim()).ToString("#,0");
                        this.GridRecord.Rows[i].Cells[12].Value = decimal.Parse(dt.Rows[i]["M07"].ToString().Trim()).ToString("#,0");
                        this.GridRecord.Rows[i].Cells[13].Value = decimal.Parse(dt.Rows[i]["M08"].ToString().Trim()).ToString("#,0");
                        this.GridRecord.Rows[i].Cells[14].Value = decimal.Parse(dt.Rows[i]["M09"].ToString().Trim()).ToString("#,0");
                        this.GridRecord.Rows[i].Cells[15].Value = decimal.Parse(dt.Rows[i]["M10"].ToString().Trim()).ToString("#,0");
                        this.GridRecord.Rows[i].Cells[16].Value = decimal.Parse(dt.Rows[i]["M11"].ToString().Trim()).ToString("#,0");
                        this.GridRecord.Rows[i].Cells[17].Value = decimal.Parse(dt.Rows[i]["M12"].ToString().Trim()).ToString("#,0");
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

        private void GridRecord_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (GridRecord.CurrentCell == null) return;
            if (GridRecord.CurrentCell.RowIndex < 0) return;
            if (GridRecord.CurrentCell.ColumnIndex < 0) return;

            int iRow = GridRecord.CurrentCell.RowIndex;
            string strValue = "" + (string)GridRecord.Rows[iRow].Cells[0].Value;
            string strValue2 = "" + (string)GridRecord.Rows[iRow].Cells[1].Value;
            string strValue3 = "" + (string)GridRecord.Rows[iRow].Cells[2].Value;

            getDetailPost(strValue, strValue2, strValue3);
            txt1.Focus();
        }

        private void getDetailPost(string sKey, string sKey2, string sKey3)
        {
            init_InputBox(false);

            lblBody.Left = spCont.Panel1.Width / 2 - lblBody.Width / 2;
            lblBody.Top = spCont.Panel1.Height / 2 - lblBody.Height / 2;
            lblBody.Visible = true;
            lblBody.Text = "Loading ...";
            Application.DoEvents();

            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_STOCK_GOAL_Detail(sKey, sKey2, sKey3);

                if (dt != null && dt.Rows.Count > 0)
                {
                    cmb연도.SelectedValue = dt.Rows[0]["GOAL_YEAR"].ToString();
                    cmb구분.SelectedValue = dt.Rows[0]["GOAL_CODE"].ToString();
                    this.txt담당자코드old.Text = dt.Rows[0]["MAN_CODE"].ToString();
                    this.txt담당자코드.Text = dt.Rows[0]["MAN_CODE"].ToString();
                    this.txt담당자명.Text = dt.Rows[0]["담당자명"].ToString();
                    this.txt1.Text = decimal.Parse(dt.Rows[0]["M01"].ToString().Trim()).ToString("#,0");
                    this.txt2.Text = decimal.Parse(dt.Rows[0]["M02"].ToString().Trim()).ToString("#,0");
                    this.txt3.Text = decimal.Parse(dt.Rows[0]["M03"].ToString().Trim()).ToString("#,0");
                    this.txt4.Text = decimal.Parse(dt.Rows[0]["M04"].ToString().Trim()).ToString("#,0");
                    this.txt5.Text = decimal.Parse(dt.Rows[0]["M05"].ToString().Trim()).ToString("#,0");
                    this.txt6.Text = decimal.Parse(dt.Rows[0]["M06"].ToString().Trim()).ToString("#,0");
                    this.txt7.Text = decimal.Parse(dt.Rows[0]["M07"].ToString().Trim()).ToString("#,0");
                    this.txt8.Text = decimal.Parse(dt.Rows[0]["M08"].ToString().Trim()).ToString("#,0");
                    this.txt9.Text = decimal.Parse(dt.Rows[0]["M09"].ToString().Trim()).ToString("#,0");
                    this.txt10.Text = decimal.Parse(dt.Rows[0]["M10"].ToString().Trim()).ToString("#,0");
                    this.txt11.Text = decimal.Parse(dt.Rows[0]["M11"].ToString().Trim()).ToString("#,0");
                    this.txt12.Text = decimal.Parse(dt.Rows[0]["M12"].ToString().Trim()).ToString("#,0");
                }
                else
                {
                    MessageBox.Show("존재하지 않는 자료입니다.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("검색중에 오류가 발생했습니다.");
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
            }

            lblBody.Visible = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //this.init_InputBox(true);
            this.bindData(this.makeSearchCondition());
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            this.init_InputBox(true);
            cmb연도.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            if (validate_InputBox() == false)
            {
                btnSave.Enabled = true;
                return;
            }

            if (bData == false)
            {
                if (get_Dup_Check("" + cmb연도.SelectedValue.ToString(), "" + cmb구분.SelectedValue.ToString(), txt담당자코드.Text, "", "", "") == true)
                {
                    MessageBox.Show("이미 존재하는 [ 목표 ]입니다.");
                    btnSave.Enabled = true;
                    return;
                }
            }

            lblBody.Left = spCont.Panel1.Width / 2 - lblBody.Width / 2;
            lblBody.Top = spCont.Panel1.Height / 2 - lblBody.Height / 2;
            lblBody.Visible = true;
            lblBody.Text = "Saving ...";
            Application.DoEvents();

            bool bResult = false;

            if (bData == false)
            {
                bResult = this.insertPost();
            }
            else
            {
                bResult = this.updatePost();
            }

            lblBody.Visible = false;

            if (bResult == true)
            {
                this.init_InputBox(true);
                this.bindData(this.makeSearchCondition());
            }
            btnSave.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult msgOk = MessageBox.Show("자료를 삭제하시겠습니까?", "삭제여부", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (msgOk == DialogResult.Cancel)
            {
                return;
            }

            lblBody.Left = spCont.Panel1.Width / 2 - lblBody.Width / 2;
            lblBody.Top = spCont.Panel1.Height / 2 - lblBody.Height / 2;
            lblBody.Visible = true;
            lblBody.Text = "Deleting ...";
            Application.DoEvents();

            bool bResult = this.deletePost();

            lblBody.Visible = false;

            if (bResult == true)
            {
                this.init_InputBox(true);
                this.bindData(this.makeSearchCondition());
            }
        }

        private bool validate_InputBox()
        {
            bool bRet = true;

            try
            {
                if (this.txt담당자코드.Text.Trim() == "")
                {
                    MessageBox.Show("[ 담당자 ]를 입력하세요.");
                    return false;
                }

                //if (this.txtName.Text.Trim() == "")
                //{
                //    MessageBox.Show("[ 단위명 ]을 입력하세요.");
                //    return false;
                //}

            }
            catch (Exception ex)
            {
                bRet = false;
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
                MessageBox.Show("입력 데이터 확인 중에 오류가 있습니다.");
            }
            return bRet;
        }

        private bool insertPost()
        {
            bool bRet = false;

            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                sb.AppendLine(" insert into STOCK_GOAL ( ");
                sb.AppendLine("     GOAL_YEAR ");
                sb.AppendLine("     , GOAL_CODE ");
                sb.AppendLine("     , MAN_CODE ");
                sb.AppendLine("     , M01 ");
                sb.AppendLine("     , M02 ");
                sb.AppendLine("     , M03 ");
                sb.AppendLine("     , M04 ");
                sb.AppendLine("     , M05 ");
                sb.AppendLine("     , M06 ");
                sb.AppendLine("     , M07 ");
                sb.AppendLine("     , M08 ");
                sb.AppendLine("     , M09 ");
                sb.AppendLine("     , M10 ");
                sb.AppendLine("     , M11 ");
                sb.AppendLine("     , M12 ");
                sb.AppendLine(" ) values ( ");
                sb.AppendLine("     @GOAL_YEAR ");
                sb.AppendLine("     , @GOAL_CODE ");
                sb.AppendLine("     , @MAN_CODE ");
                sb.AppendLine("     , @M01 ");
                sb.AppendLine("     , @M02 ");
                sb.AppendLine("     , @M03 ");
                sb.AppendLine("     , @M04 ");
                sb.AppendLine("     , @M05 ");
                sb.AppendLine("     , @M06 ");
                sb.AppendLine("     , @M07 ");
                sb.AppendLine("     , @M08 ");
                sb.AppendLine("     , @M09 ");
                sb.AppendLine("     , @M10 ");
                sb.AppendLine("     , @M11 ");
                sb.AppendLine("     , @M12 ");
                sb.AppendLine(" ) ");

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@GOAL_YEAR", "" + cmb연도.SelectedValue.ToString());
                sCommand.Parameters.AddWithValue("@GOAL_CODE", "" + cmb구분.SelectedValue.ToString());
                sCommand.Parameters.AddWithValue("@MAN_CODE", txt담당자코드.Text);
                sCommand.Parameters.AddWithValue("@M01", txt1.Text.Replace(",", ""));
                sCommand.Parameters.AddWithValue("@M02", txt2.Text.Replace(",", ""));
                sCommand.Parameters.AddWithValue("@M03", txt3.Text.Replace(",", ""));
                sCommand.Parameters.AddWithValue("@M04", txt4.Text.Replace(",", ""));
                sCommand.Parameters.AddWithValue("@M05", txt5.Text.Replace(",", ""));
                sCommand.Parameters.AddWithValue("@M06", txt6.Text.Replace(",", ""));
                sCommand.Parameters.AddWithValue("@M07", txt7.Text.Replace(",", ""));
                sCommand.Parameters.AddWithValue("@M08", txt8.Text.Replace(",", ""));
                sCommand.Parameters.AddWithValue("@M09", txt9.Text.Replace(",", ""));
                sCommand.Parameters.AddWithValue("@M10", txt10.Text.Replace(",", ""));
                sCommand.Parameters.AddWithValue("@M11", txt11.Text.Replace(",", ""));
                sCommand.Parameters.AddWithValue("@M12", txt12.Text.Replace(",", ""));

                int qResult = wAdo.SqlCommandEtc(sCommand, "Insert STOCK_GOAL");
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

        private bool updatePost()
        {
            bool bRet = false;

            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                sb.AppendLine(" update STOCK_GOAL set ");
                sb.AppendLine("     M01 = @M01 ");
                sb.AppendLine("     , M02 = @M02 ");
                sb.AppendLine("     , M03 = @M03 ");
                sb.AppendLine("     , M04 = @M04 ");
                sb.AppendLine("     , M05 = @M05 ");
                sb.AppendLine("     , M06 = @M06 ");
                sb.AppendLine("     , M07 = @M07 ");
                sb.AppendLine("     , M08 = @M08 ");
                sb.AppendLine("     , M09 = @M09 ");
                sb.AppendLine("     , M10 = @M10 ");
                sb.AppendLine("     , M11 = @M11 ");
                sb.AppendLine("     , M12 = @M12 ");
                sb.AppendLine(" where 1=1 ");
                sb.AppendLine("     and GOAL_YEAR = @p1 ");
                sb.AppendLine("     and GOAL_CODE = @p2 ");
                sb.AppendLine("     and MAN_CODE = @p3 ");

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@p1", "" + cmb연도.SelectedValue.ToString());
                sCommand.Parameters.AddWithValue("@p2", "" + cmb구분.SelectedValue.ToString());
                sCommand.Parameters.AddWithValue("@p3", txt담당자코드old.Text);
                sCommand.Parameters.AddWithValue("@M01", txt1.Text.Replace(",", ""));
                sCommand.Parameters.AddWithValue("@M02", txt2.Text.Replace(",", ""));
                sCommand.Parameters.AddWithValue("@M03", txt3.Text.Replace(",", ""));
                sCommand.Parameters.AddWithValue("@M04", txt4.Text.Replace(",", ""));
                sCommand.Parameters.AddWithValue("@M05", txt5.Text.Replace(",", ""));
                sCommand.Parameters.AddWithValue("@M06", txt6.Text.Replace(",", ""));
                sCommand.Parameters.AddWithValue("@M07", txt7.Text.Replace(",", ""));
                sCommand.Parameters.AddWithValue("@M08", txt8.Text.Replace(",", ""));
                sCommand.Parameters.AddWithValue("@M09", txt9.Text.Replace(",", ""));
                sCommand.Parameters.AddWithValue("@M10", txt10.Text.Replace(",", ""));
                sCommand.Parameters.AddWithValue("@M11", txt11.Text.Replace(",", ""));
                sCommand.Parameters.AddWithValue("@M12", txt12.Text.Replace(",", ""));

                int qResult = wAdo.SqlCommandEtc(sCommand, "Update STOCK_GOAL");
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

        private bool deletePost()
        {
            bool bRet = false;

            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                sb.AppendLine(" delete from STOCK_GOAL ");
                sb.AppendLine(" where 1=1 ");
                sb.AppendLine("     and GOAL_YEAR = @p1 ");
                sb.AppendLine("     and GOAL_CODE = @p2 ");
                sb.AppendLine("     and MAN_CODE = @p3 ");

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@p1", "" + cmb연도.SelectedValue.ToString());
                sCommand.Parameters.AddWithValue("@p2", "" + cmb구분.SelectedValue.ToString());
                sCommand.Parameters.AddWithValue("@p3", txt담당자코드old.Text);

                int qResult = wAdo.SqlCommandEtc(sCommand, "Delete STOCK_GOAL");
                if (qResult > 0) bRet = true;
                else bRet = false;

                if (bRet == true)
                {
                }
                else
                {
                    MessageBox.Show("삭제 중에 오류가 발생했습니다.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("데이터베이스에 문제가 발생했습니다.");
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
            }
            return bRet;
        }

        private bool get_Dup_Check(string sYear, string sGubun, string sMan, string oYear, string oGubun, string oMan)
        {
            try
            {
                if (sYear + "_" + sGubun + "_" + sMan != oYear + "_" + oGubun + "_" + oMan)
                {
                    wnDm wDm = new wnDm();
                    DataTable dt = null;
                    dt = wDm.fn_STOCK_GOAL_Detail(sYear, sGubun, sMan);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
                return true;
            }
        }

        private void LastTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                btnSave.Focus();
            }
        }

        private void get_Man_Info(string sCode)
        {
            try
            {
                //wnDm wDm = new wnDm();
                //DataTable dt = null;
                //dt = wDm.fn_User_Detail(sCode);

                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    txt부서코드.Text = dt.Rows[0]["USER_DEPT"].ToString().Trim();
                //    txt부서명.Text = dt.Rows[0]["CODE_DESC"].ToString().Trim();
                //}
            }
            catch (Exception ex)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
            }
        }

        #region 입력 담당자 ##############################################################################################################

        private void btn담당자_Click(object sender, EventArgs e)
        {
            bEditText = false;

            if (txt담당자명.Text == "")
            {
                txt담당자코드.Text = "";
                txt담당자코드old.Text = "";
            }
            wConst.call_popRef_Man("", txt담당자코드, txt담당자명, "0");
            if (txt담당자코드.Text != "")
            {
                if (txt담당자코드old.Text != txt담당자코드.Text)
                {
                    get_Man_Info(txt담당자코드.Text);
                    txt담당자코드old.Text = txt담당자코드.Text;
                }

            }

            bEditText = true;
            SendKeys.Send("{TAB}");
        }

        private void txt담당자명_Enter(object sender, EventArgs e)
        {
            bEditText = true;
        }

        private void txt담당자명_TextChanged(object sender, EventArgs e)
        {
            if (bEditText == false) return;

            txt담당자코드.Text = "";
        }

        private void txt담당자명_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;

                bEditText = false;

                bool bPopSrch = true;

                if (txt담당자코드.Text != "")
                {
                    bPopSrch = false;
                }

                if (bPopSrch == true)
                {
                    if (txt담당자명.Text == "")
                    {
                        txt담당자코드.Text = "";
                        txt담당자코드old.Text = "";
                    }
                    wConst.call_popRef_Man(txt담당자명.Text, txt담당자코드, txt담당자명, "0");
                    if (txt담당자코드old.Text != txt담당자코드.Text)
                    {
                        get_Man_Info(txt담당자코드.Text);
                        txt담당자코드old.Text = txt담당자코드.Text;
                    }

                }

                if (txt담당자코드.Text == "")
                {
                    init_DataText_Man();
                }
                SendKeys.Send("{TAB}");
                bEditText = true;
            }
        }

        private void init_DataText_Man()
        {
            txt담당자코드.Text = "";
            txt담당자명.Text = "";
        }

        #endregion 입력 담당자 ##############################################################################################################

        #region 검색 담당자 ##############################################################################################################

        private void btnS담당자_Click(object sender, EventArgs e)
        {
            bEditText = false;

            if (txtS담당자명.Text == "")
            {
                txtS담당자코드.Text = "";
            }
            wConst.call_popRef_Man("", txtS담당자코드, txtS담당자명, "0");

            bEditText = true;
            SendKeys.Send("{TAB}");
        }

        private void txtS담당자명_Enter(object sender, EventArgs e)
        {
            bEditText = true;
        }

        private void txtS담당자명_TextChanged(object sender, EventArgs e)
        {
            if (bEditText == false) return;

            txtS담당자코드.Text = "";
        }

        private void txtS담당자명_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;

                bEditText = false;

                bool bPopSrch = true;

                if (txtS담당자코드.Text != "")
                {
                    bPopSrch = false;
                }

                if (bPopSrch == true)
                {
                    if (txtS담당자명.Text == "")
                    {
                        txtS담당자코드.Text = "";
                    }
                    wConst.call_popRef_Man(txtS담당자명.Text, txtS담당자코드, txtS담당자명, "0");
                    
                }

                if (txtS담당자코드.Text == "")
                {
                    init_SrchText_Man();
                }
                SendKeys.Send("{TAB}");
                bEditText = true;
            }
        }

        private void init_SrchText_Man()
        {
            txtS담당자코드.Text = "";
            txtS담당자명.Text = "";
        }

        #endregion 검색 담당자 ##############################################################################################################

    }
}
