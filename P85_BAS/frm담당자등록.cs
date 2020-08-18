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

namespace 스마트팩토리.P85_BAS
{
    public partial class frm담당자등록 : Form
    {
        private wnGConstant wConst = new wnGConstant();
        private bool bData = false;

        public frm담당자등록()
        {
            InitializeComponent();
        }

        private void frm담당자등록_Load(object sender, EventArgs e)
        {
            spCont.Dock = DockStyle.Fill;

            lblSearch.BringToFront();
            lblBody.BringToFront();

            this.init_ComboBox();
            this.init_InputBox(true);
            this.bindData(this.makeSearchCondition());
        }

        private void init_ComboBox()
        {
            string sqlQuery = "";

            cmb부서지역.ValueMember = "코드";
            cmb부서지역.DisplayMember = "명칭";
            sqlQuery = " select a.AreaCode as 코드, b.AreaName as 명칭 ";
            //sqlQuery = " select isnull(a.AreaCode, '') as 코드, b.AreaName as 명칭 ";
            sqlQuery += " from DEPTCODE a ";
            sqlQuery += "     left outer join T_Area b on b.AreaCode = a.AreaCode ";
            sqlQuery += " order by a.DEPT_CODE asc ";
            wConst.ComboBox_Read_NoBlank(cmb부서지역, sqlQuery);

            cmb부서.ValueMember = "코드";
            cmb부서.DisplayMember = "명칭";
            sqlQuery = " select DEPT_CODE as 코드, CODE_DESC as 명칭 from DEPTCODE ";
            sqlQuery += " order by DEPT_CODE asc ";
            wConst.ComboBox_Read_NoBlank(cmb부서, sqlQuery);

        }

        private void init_InputBox(bool bNew)
        {
            wConst.Form_Clear(spCont.Panel1.Controls);

            cmb부서.SelectedIndex = 0;
            dtp입사일자.Text = DateTime.Now.ToString("yyyy-MM-dd");
            dtp퇴사일자.Text = "1900-01-01";
            dtp퇴사일자.Enabled = false;

            if (bNew == true)
            {
                bData = false;
                btnDelete.Enabled = false;
                txtCode.Enabled = false;
            }
            else
            {
                bData = true;
                btnDelete.Enabled = true;
                txtCode.Enabled = false;
            }
        }

        private string makeSearchCondition()
        {
            StringBuilder sb = new StringBuilder();

            switch (this.txtSrch.Text)
            {
                case "":
                    sb.Append("");
                    break;
                default:
                    sb.Append(" and a.CODE_DESC like '%" + txtSrch.Text + "%' ");
                    break;
            }

            return sb.ToString();
        }

        private void bindData(string condition)
        {
            lblSearch.Left = this.ClientSize.Width / 2 - lblSearch.Width / 2;
            lblSearch.Top = this.ClientSize.Height / 2 - lblSearch.Height / 2;
            lblSearch.Visible = true;
            Application.DoEvents();

            this.GridRecord.DataSource = null;
            this.GridRecord.RowCount = 0;

            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_MANCODE_List(condition);

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.GridRecord.RowCount = dt.Rows.Count;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.GridRecord.Rows[i].Cells[0].Value = dt.Rows[i]["MAN_CODE"].ToString();
                        this.GridRecord.Rows[i].Cells[1].Value = dt.Rows[i]["CODE_DESC"].ToString();
                        this.GridRecord.Rows[i].Cells[2].Value = dt.Rows[i]["직급"].ToString();
                        this.GridRecord.Rows[i].Cells[3].Value = dt.Rows[i]["DEPT_NM"].ToString();
                        this.GridRecord.Rows[i].Cells[4].Value = (dt.Rows[i]["MAN_OUTCHK"].ToString() == "1" ? "퇴사" : "");
                        this.GridRecord.Rows[i].Cells[5].Value = dt.Rows[i]["입사일자"].ToString();
                        this.GridRecord.Rows[i].Cells[6].Value = dt.Rows[i]["퇴사일자"].ToString();
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
            //int iKeyCol = GridRecord.ColumnCount - 1;
            int iKeyCol = 0;
            string strValue = "" + (string)GridRecord.Rows[iRow].Cells[iKeyCol].Value;

            getDetailPost(strValue);
            txtName.Focus();
        }

        private void getDetailPost(string sKey)
        {
            init_InputBox(false);

            lblBody.Left = this.ClientSize.Width / 2 - lblBody.Width / 2;
            lblBody.Top = this.ClientSize.Height / 2 - lblBody.Height / 2;
            lblBody.Visible = true;
            lblBody.Text = "Loading ...";
            Application.DoEvents();

            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_MANCODE_Detail(sKey);

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.txtCode.Text = dt.Rows[0]["MAN_CODE"].ToString();
                    this.txtName.Text = dt.Rows[0]["CODE_DESC"].ToString();
                    this.txt직급.Text = dt.Rows[0]["직급"].ToString();
                    cmb부서.SelectedValue = dt.Rows[0]["DEPT_CODE"].ToString();
                    if (dt.Rows[0]["입사일자"].ToString() == "")
                    {
                        dtp입사일자.Text = "1900-01-01";
                    }
                    else
                    {
                        dtp입사일자.Text = dt.Rows[0]["입사일자"].ToString();
                    }
                    if (dt.Rows[0]["MAN_OUTCHK"].ToString() == "1")
                    {
                        chk퇴사구분.Checked = true;
                        dtp퇴사일자.Enabled = true;
                        if (dt.Rows[0]["퇴사일자"].ToString() == "")
                        {
                            dtp퇴사일자.Text = "1900-01-01";
                        }
                        else
                        {
                            dtp퇴사일자.Text = dt.Rows[0]["퇴사일자"].ToString();
                        }
                    }
                    else
                    {
                        chk퇴사구분.Checked = false;
                        dtp퇴사일자.Text = "1900-01-01";
                    }
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
            txtCode.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            cmb부서지역.SelectedIndex = cmb부서.SelectedIndex;

            if (validate_InputBox() == false)
            {
                btnSave.Enabled = true;
                return;
            }

            //if (bData == false)
            //{
            //    if (get_Dup_Check("" + cmb부서지역.SelectedValue.ToString() + txtCode.Text, "" + cmb부서지역.SelectedValue.ToString() + txtCode.Text) == true)
            //    {
            //        MessageBox.Show("이미 존재하는 [ 코드 ]입니다." + " [ " + this.txtCode.Text + " ]");
            //        return;
            //    }
            //}

            lblBody.Left = this.ClientSize.Width / 2 - lblBody.Width / 2;
            lblBody.Top = this.ClientSize.Height / 2 - lblBody.Height / 2;
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

            lblBody.Left = this.ClientSize.Width / 2 - lblBody.Width / 2;
            lblBody.Top = this.ClientSize.Height / 2 - lblBody.Height / 2;
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
                //if (this.txtCode.Text.Trim() == "")
                //{
                //    MessageBox.Show("[ 코드 ]를 입력하세요.");
                //    return false;
                //}

                if (this.txtName.Text.Trim() == "")
                {
                    MessageBox.Show("[ 담당자명 ]을 입력하세요.");
                    return false;
                }

                if ("" + cmb부서지역.SelectedValue.ToString() == "")
                {
                    MessageBox.Show("'영업소 등록'에서, [ 지역구분 ]을 입력하세요.");
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

        private bool insertPost()
        {
            bool bRet = false;

            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                sb.AppendLine(" select isnull(max(convert(int, MAN_CODE)), " + cmb부서지역.SelectedValue.ToString() + "000) + 1 ");
                sb.AppendLine(" from MANCODE where MAN_CODE like '" + cmb부서지역.SelectedValue.ToString() + "%' ");
                string sRetVal = wConst.maxValue_Check(sb.ToString());
                if (sRetVal == "")
                {
                    MessageBox.Show("데이터 검색 중 오류 발생!!!");
                    return false;
                }

                if (int.Parse("" + cmb부서지역.SelectedValue.ToString() + "999") < int.Parse(sRetVal))
                {
                    MessageBox.Show("부서별로 규정된 범위를 초과하는 [ 코드 ]입니다." + " [ " + sRetVal + " ]");
                    return false;
                }

                sb = new StringBuilder();

                sb.AppendLine(" insert into MANCODE ( ");
                sb.AppendLine("     MAN_CODE ");
                sb.AppendLine("     , CODE_DESC ");
                sb.AppendLine("     , 직급 ");
                sb.AppendLine("     , DEPT_CODE ");
                sb.AppendLine("     , 입사일자 ");
                sb.AppendLine("     , MAN_OUTCHK ");
                sb.AppendLine("     , 퇴사일자 ");
                sb.AppendLine(" ) values ( ");
                sb.AppendLine("     @MAN_CODE ");
                sb.AppendLine("     , @CODE_DESC ");
                sb.AppendLine("     , @직급 ");
                sb.AppendLine("     , @DEPT_CODE ");
                sb.AppendLine("     , @입사일자 ");
                sb.AppendLine("     , @MAN_OUTCHK ");
                sb.AppendLine("     , @퇴사일자 ");
                sb.AppendLine(" ) ");

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@MAN_CODE", sRetVal);
                sCommand.Parameters.AddWithValue("@CODE_DESC", txtName.Text);
                sCommand.Parameters.AddWithValue("@직급", txt직급.Text);
                sCommand.Parameters.AddWithValue("@DEPT_CODE", "" + cmb부서.SelectedValue.ToString());
                sCommand.Parameters.AddWithValue("@입사일자", (dtp입사일자.Text == "1900-01-01" ? "" : dtp입사일자.Text));
                sCommand.Parameters.AddWithValue("@MAN_OUTCHK", (chk퇴사구분.Checked == true ? "1" : "0"));
                sCommand.Parameters.AddWithValue("@퇴사일자", (dtp퇴사일자.Text == "1900-01-01" ? "" : dtp퇴사일자.Text));

                int qResult = wAdo.SqlCommandEtc(sCommand, "Insert MANCODE");
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

                sb.AppendLine(" update MANCODE set ");
                sb.AppendLine("     CODE_DESC = @CODE_DESC ");
                sb.AppendLine("     , 직급 = @직급 ");
                sb.AppendLine("     , DEPT_CODE = @DEPT_CODE ");
                sb.AppendLine("     , 입사일자 = @입사일자 ");
                sb.AppendLine("     , MAN_OUTCHK = @MAN_OUTCHK ");
                sb.AppendLine("     , 퇴사일자 = @퇴사일자 ");
                sb.AppendLine(" where 1=1 ");
                sb.AppendLine("     and MAN_CODE = @p1 ");

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@p1", txtCode.Text);
                sCommand.Parameters.AddWithValue("@CODE_DESC", txtName.Text);
                sCommand.Parameters.AddWithValue("@직급", txt직급.Text);
                sCommand.Parameters.AddWithValue("@DEPT_CODE", "" + cmb부서.SelectedValue.ToString());
                sCommand.Parameters.AddWithValue("@입사일자", (dtp입사일자.Text == "1900-01-01" ? "" : dtp입사일자.Text));
                sCommand.Parameters.AddWithValue("@MAN_OUTCHK", (chk퇴사구분.Checked == true ? "1" : "0"));
                sCommand.Parameters.AddWithValue("@퇴사일자", (dtp퇴사일자.Text == "1900-01-01" ? "" : dtp퇴사일자.Text));

                int qResult = wAdo.SqlCommandEtc(sCommand, "Update MANCODE");
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

                sb.AppendLine(" delete from MANCODE ");
                sb.AppendLine(" where MAN_CODE = @p1  ");

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@p1", this.txtCode.Text);

                int qResult = wAdo.SqlCommandEtc(sCommand, "Delete MANCODE");
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

        private bool get_Dup_Check(string sNew, string sOld)
        {
            try
            {
                if (sNew != sOld)
                {
                    wnDm wDm = new wnDm();
                    DataTable dt = null;
                    dt = wDm.fn_MANCODE_Detail(sNew);

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

        private void chk퇴사구분_Click(object sender, EventArgs e)
        {
            if (chk퇴사구분.Checked == true)
            {
                dtp퇴사일자.Enabled = true;
            }
            else
            {
                dtp퇴사일자.Enabled = false;
                dtp퇴사일자.Text = "1900-01-01";
            }
        }

        private void cmb부서_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmb부서지역.SelectedIndex = cmb부서.SelectedIndex;
        }

    }
}
