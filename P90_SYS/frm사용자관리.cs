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

namespace 스마트팩토리.P90_SYS
{
    public partial class frm사용자관리 : Form
    {
        private wnGConstant wConst = new wnGConstant();
        private bool bData = false;

        public frm사용자관리()
        {
            InitializeComponent();
        }

        private void frm사용자관리_Load(object sender, EventArgs e)
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

            cmb검색조건.ValueMember = "코드";
            cmb검색조건.DisplayMember = "명칭";
            sqlQuery = " select '0' as 코드, '전체' as 명칭 ";
            sqlQuery += " union all ";
            sqlQuery += " select '1' as 코드, '부서별' as 명칭 ";
            sqlQuery += " union all ";
            sqlQuery += " select '2' as 코드, '담당자별' as 명칭 ";
            wConst.ComboBox_Read_NoBlank(cmb검색조건, sqlQuery);

            cmb담당자.ValueMember = "코드";
            cmb담당자.DisplayMember = "명칭";
            sqlQuery = " select MAN_CODE as 코드, CODE_DESC as 명칭 from MANCODE where 1=1 ";
            wConst.ComboBox_Read_NoBlank(cmb담당자, sqlQuery);

            cmb부서.ValueMember = "코드";
            cmb부서.DisplayMember = "명칭";
            sqlQuery = " select DEPT_CODE as 코드, CODE_DESC as 명칭 from DEPTCODE where 1=1 ";
            wConst.ComboBox_Read_NoBlank(cmb부서, sqlQuery);

        }

        private void init_InputBox(bool bNew)
        {
            wConst.Form_Clear(spCont.Panel1.Controls);

            cmb검색조건.SelectedIndex = 0;
            cmb담당자.SelectedIndex = 0;
            cmb부서.SelectedIndex = 0;
            rb서버2.Checked = true;

            if (bNew == true)
            {
                bData = false;
                btnDelete.Enabled = false;
                txtCode.Enabled = true;
                txt아이디.Enabled = true;
            }
            else
            {
                bData = true;
                btnDelete.Enabled = true;
                txtCode.Enabled = false;
                txt아이디.Enabled = false;
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
                    sb.Append(" and a.USER_NAME like '%" + txtSrch.Text + "%' ");
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
                dt = wDm.fn_User_List(condition);

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.GridRecord.RowCount = dt.Rows.Count;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.GridRecord.Rows[i].Cells[0].Value = dt.Rows[i]["USER_CODE"].ToString();
                        this.GridRecord.Rows[i].Cells[1].Value = dt.Rows[i]["USER_NAME"].ToString();
                        this.GridRecord.Rows[i].Cells[2].Value = dt.Rows[i]["USER_RESNUM"].ToString();
                        this.GridRecord.Rows[i].Cells[3].Value = dt.Rows[i]["USER_SELECT_NM"].ToString();
                        this.GridRecord.Rows[i].Cells[4].Value = dt.Rows[i]["USER_MAN_NM"].ToString();
                        this.GridRecord.Rows[i].Cells[5].Value = dt.Rows[i]["USER_ID"].ToString();
                        this.GridRecord.Rows[i].Cells[6].Value = dt.Rows[i]["USER_PASS"].ToString();
                        this.GridRecord.Rows[i].Cells[7].Value = dt.Rows[i]["USER_DEPT_NM"].ToString();
                        this.GridRecord.Rows[i].Cells[8].Value = (dt.Rows[i]["USER_SERVER1"].ToString() == "1" ? "o" : "");
                        this.GridRecord.Rows[i].Cells[9].Value = (dt.Rows[i]["USER_SERVER2"].ToString() == "1" ? "o" : "");
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
                dt = wDm.fn_User_Detail(sKey);

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.txtCode.Text = dt.Rows[0]["USER_CODE"].ToString();
                    this.txtName.Text = dt.Rows[0]["USER_NAME"].ToString();
                    this.txt주민번호.Text = dt.Rows[0]["USER_RESNUM"].ToString();
                    this.cmb검색조건.SelectedValue = dt.Rows[0]["USER_SELECT"].ToString();
                    this.cmb담당자.SelectedValue = dt.Rows[0]["USER_MAN"].ToString();
                    this.txt아이디.Text = dt.Rows[0]["USER_ID"].ToString();
                    this.txt비밀번호.Text = dt.Rows[0]["USER_PASS"].ToString();
                    this.cmb부서.SelectedValue = dt.Rows[0]["USER_DEPT"].ToString();
                    if (dt.Rows[0]["USER_SERVER1"].ToString() == "1")
                    {
                        this.rb서버1.Checked = true;
                    }
                    if (dt.Rows[0]["USER_SERVER2"].ToString() == "1")
                    {
                        this.rb서버2.Checked = true;
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
            if (validate_InputBox() == false)
            {
                btnSave.Enabled = true;
                return;
            }

            if (bData == false)
            {
                if (get_Dup_Check(txtCode.Text, txtCode.Text) == true)
                {
                    MessageBox.Show("이미 존재하는 [ 코드 ]입니다." + " [ " + this.txtCode.Text + " ]");
                    btnSave.Enabled = true;
                    return;
                }
                if (get_Dup_Check_UserID(txt아이디.Text, txt아이디.Text) == true)
                {
                    MessageBox.Show("이미 존재하는 [ 사용자 ID ]입니다." + " [ " + this.txt아이디.Text + " ]");
                    btnSave.Enabled = true;
                    return;
                }
            }

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
                if (this.txtCode.Text.Trim() == "")
                {
                    MessageBox.Show("[ 코드 ]를 입력하세요.");
                    return false;
                }

                if (this.txtName.Text.Trim() == "")
                {
                    MessageBox.Show("[ 사용자명 ]을 입력하세요.");
                    return false;
                }

                if (this.txt아이디.Text.Trim() == "")
                {
                    MessageBox.Show("[ 사용자 ID ]을 입력하세요.");
                    return false;
                }

                if (this.txt비밀번호.Text.Trim() == "")
                {
                    MessageBox.Show("[ 비밀번호 ]을 입력하세요.");
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

                sb.AppendLine(" insert into USER_PASSWD ( ");
                sb.AppendLine("     USER_CODE ");
                sb.AppendLine("     , USER_NAME ");
                sb.AppendLine("     , USER_RESNUM ");
                sb.AppendLine("     , USER_ID ");
                sb.AppendLine("     , USER_PASS ");
                sb.AppendLine("     , USER_DEPT ");
                sb.AppendLine("     , USER_MAN ");
                sb.AppendLine("     , USER_SELECT ");
                sb.AppendLine("     , USER_SERVER1 ");
                sb.AppendLine("     , USER_SERVER2 ");
                sb.AppendLine("     , USER_SERVER3 ");
                sb.AppendLine(" ) values ( ");
                sb.AppendLine("     @USER_CODE ");
                sb.AppendLine("     , @USER_NAME ");
                sb.AppendLine("     , @USER_RESNUM ");
                sb.AppendLine("     , @USER_ID ");
                sb.AppendLine("     , @USER_PASS ");
                sb.AppendLine("     , @USER_DEPT ");
                sb.AppendLine("     , @USER_MAN ");
                sb.AppendLine("     , @USER_SELECT ");
                sb.AppendLine("     , @USER_SERVER1 ");
                sb.AppendLine("     , @USER_SERVER2 ");
                sb.AppendLine("     , @USER_SERVER3 ");
                sb.AppendLine(" ) ");

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@USER_CODE", txtCode.Text);
                sCommand.Parameters.AddWithValue("@USER_NAME", txtName.Text);
                sCommand.Parameters.AddWithValue("@USER_RESNUM", txt주민번호.Text);
                sCommand.Parameters.AddWithValue("@USER_ID", txt아이디.Text);
                sCommand.Parameters.AddWithValue("@USER_PASS", txt비밀번호.Text);
                sCommand.Parameters.AddWithValue("@USER_DEPT", "" + cmb부서.SelectedValue.ToString());
                sCommand.Parameters.AddWithValue("@USER_MAN", "" + cmb담당자.SelectedValue.ToString());
                sCommand.Parameters.AddWithValue("@USER_SELECT", "" + cmb검색조건.SelectedValue.ToString());
                sCommand.Parameters.AddWithValue("@USER_SERVER1", (rb서버1.Checked == true ? "1" : "0"));
                sCommand.Parameters.AddWithValue("@USER_SERVER2", (rb서버2.Checked == true ? "1" : "0"));
                sCommand.Parameters.AddWithValue("@USER_SERVER3", "0");

                int qResult = wAdo.SqlCommandEtc(sCommand, "Insert USER_PASSWD");
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

                sb.AppendLine(" update USER_PASSWD set ");
                sb.AppendLine("     USER_NAME = @USER_NAME ");
                sb.AppendLine("     , USER_RESNUM = @USER_RESNUM ");
                sb.AppendLine("     , USER_PASS = @USER_PASS ");
                sb.AppendLine("     , USER_DEPT = @USER_DEPT ");
                sb.AppendLine("     , USER_MAN = @USER_MAN ");
                sb.AppendLine("     , USER_SELECT = @USER_SELECT ");
                sb.AppendLine("     , USER_SERVER1 = @USER_SERVER1 ");
                sb.AppendLine("     , USER_SERVER2 = @USER_SERVER2 ");
                sb.AppendLine("     , USER_SERVER3 = @USER_SERVER3 ");
                sb.AppendLine(" where 1=1 ");
                sb.AppendLine("     and USER_CODE = @p1 ");

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@p1", txtCode.Text);
                sCommand.Parameters.AddWithValue("@USER_NAME", txtName.Text);
                sCommand.Parameters.AddWithValue("@USER_RESNUM", txt주민번호.Text);
                sCommand.Parameters.AddWithValue("@USER_PASS", txt비밀번호.Text);
                sCommand.Parameters.AddWithValue("@USER_DEPT", "" + cmb부서.SelectedValue.ToString());
                sCommand.Parameters.AddWithValue("@USER_MAN", "" + cmb담당자.SelectedValue.ToString());
                sCommand.Parameters.AddWithValue("@USER_SELECT", "" + cmb검색조건.SelectedValue.ToString());
                sCommand.Parameters.AddWithValue("@USER_SERVER1", (rb서버1.Checked == true ? "1" : "0"));
                sCommand.Parameters.AddWithValue("@USER_SERVER2", (rb서버2.Checked == true ? "1" : "0"));
                sCommand.Parameters.AddWithValue("@USER_SERVER3", "0");

                int qResult = wAdo.SqlCommandEtc(sCommand, "Update USER_PASSWD");
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

                sb.AppendLine(" delete from USER_PASSWD ");
                sb.AppendLine(" where USER_CODE = @p1  ");

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@p1", this.txtCode.Text);

                int qResult = wAdo.SqlCommandEtc(sCommand, "Delete USER_PASSWD");
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
                    dt = wDm.fn_User_Detail(sNew);

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

        private bool get_Dup_Check_UserID(string sNew, string sOld)
        {
            try
            {
                if (sNew != sOld)
                {
                    wnDm wDm = new wnDm();
                    DataTable dt = null;
                    dt = wDm.fn_UserID_Detail(sNew);

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

    }
}
