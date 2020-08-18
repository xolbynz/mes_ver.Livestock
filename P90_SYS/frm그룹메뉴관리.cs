using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 스마트팩토리;
using 스마트팩토리.CLS;

namespace 스마트팩토리.P90_SYS
{
    public partial class frm그룹메뉴관리 : Form
    {
        private wnGConstant wConst = new wnGConstant();
        private bool bData = false;
        private bool bHeadCheck = false;

        public frm그룹메뉴관리()
        {
            InitializeComponent();
        }

        private void frm그룹메뉴관리_Load(object sender, EventArgs e)
        {
            spCont.Dock = DockStyle.Fill;

            lblSearch.BringToFront();
            lblBody.BringToFront();

            this.init_InputBox(true);
            bindData_AllMenu("-1");
            this.bindData(this.makeSearchCondition());
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
                    sb.Append(" and a.GrpName like '%" + txtSrch.Text + "%' ");
                    break;
            }

            return sb.ToString();
        }

        private void init_InputBox(bool bNew)
        {
            txtCode.Text = "";
            txtName.Text = "";

            this.grdSet.DataSource = null;
            this.grdSet.RowCount = 0;

            this.grdGet.DataSource = null;
            this.grdGet.RowCount = 0;

            if (bNew == true)
            {
                bData = false;
                btnDelete.Enabled = false;
            }
            else
            {
                bData = true;
                btnDelete.Enabled = true;
            }
        }

        private void bindData(string condition)
        {
            lblSearch.Visible = true;
            Application.DoEvents();

            this.GridRecord.DataSource = null;
            this.GridRecord.RowCount = 0;

            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_Group_List(condition);

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.GridRecord.RowCount = dt.Rows.Count;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.GridRecord.Rows[i].Cells[0].Value = dt.Rows[i]["GrpName"].ToString();
                        this.GridRecord.Rows[i].Cells[1].Value = dt.Rows[i]["GrpID"].ToString();
                    }
                }

                this.GridRecord.Focus();
            }
            catch (Exception ex)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
            }

            lblSearch.Visible = false;
        }

        private void GridRecord_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GridRecord.CurrentCell == null) return;
            if (GridRecord.CurrentCell.RowIndex < 0) return;
            if (GridRecord.CurrentCell.ColumnIndex < 0) return;

            int iRow = GridRecord.CurrentCell.RowIndex;
            int iKeyCol = GridRecord.ColumnCount - 1;
            string strValue = "" + (string)GridRecord.Rows[iRow].Cells[iKeyCol].Value;

            getDetailPost(strValue);
            bindData_AllMenu(strValue);
        }

        private void getDetailPost(string sKey)
        {
            init_InputBox(false);

            lblBody.Visible = true;
            lblBody.Text = "Loading ...";
            Application.DoEvents();

            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_Group_Detail(sKey);

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.txtCode.Text = dt.Rows[0]["GrpID"].ToString();
                    this.txtName.Text = dt.Rows[0]["GrpName"].ToString();
                }
            }
            catch (Exception ex)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
            }

            lblBody.Visible = false;
        }

        private void bindData_AllMenu(string sID)
        {
            lblBody.Visible = true;
            lblBody.Text = "Loading ...";
            Application.DoEvents();

            this.grdGet.Columns[1].HeaderText = "[ ]";
            bHeadCheck = false;

            this.grdSet.DataSource = null;
            this.grdSet.RowCount = 0;

            this.grdGet.DataSource = null;
            this.grdGet.RowCount = 0;

            int nRowCnt = 0;

            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_AllMenu_CheckList(sID);

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.grdSet.RowCount = dt.Rows.Count;
                    this.grdGet.RowCount = dt.Rows.Count;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string sBlank = "";
                        if (dt.Rows[i]["SubID"].ToString() != "")
                        {
                            sBlank = "     ";
                        }

                        // 전체 메뉴 목록
                        this.grdGet.Rows[i].Cells[0].Value = sBlank + dt.Rows[i]["MenuName"].ToString();
                        if (dt.Rows[i]["ChkYN"].ToString() == "Y")
                        {
                            this.grdGet.Rows[i].Cells[1].Value = true;
                        }
                        else
                        {
                            if (dt.Rows[i]["SubID"].ToString() == "")
                            {
                                this.grdGet.Rows[i].Cells[1].Value = false;
                                this.grdGet.Rows[i].Cells[1].Style.BackColor = Color.Gainsboro;
                                this.grdGet.Rows[i].Cells[1].ReadOnly = true;
                            }
                        }
                        this.grdGet.Rows[i].Cells[2].Value = dt.Rows[i]["TopID"].ToString();
                        this.grdGet.Rows[i].Cells[3].Value = dt.Rows[i]["SubID"].ToString();

                        // 그룹 메뉴 목록
                        if (int.Parse(dt.Rows[i]["SubCnt"].ToString()) > 0 || dt.Rows[i]["ChkYN"].ToString() == "Y")
                        {
                            nRowCnt += 1;

                            this.grdSet.Rows[nRowCnt - 1].Cells[0].Value = sBlank + dt.Rows[i]["MenuName"].ToString();
                            this.grdSet.Rows[nRowCnt - 1].Cells[1].Value = dt.Rows[i]["TopID"].ToString();
                            this.grdSet.Rows[nRowCnt - 1].Cells[2].Value = dt.Rows[i]["SubID"].ToString();
                        }
                    }

                    this.grdSet.RowCount = nRowCnt;
                }
            }
            catch (Exception ex)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
            }

            lblBody.Visible = false;
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            //this.init_InputBox(true);
            this.bindData(this.makeSearchCondition());
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool validate_InputBox()
        {
            bool bRet = true;

            try
            {
                if (this.txtName.Text.Trim() == "")
                {
                    MessageBox.Show("[ 그룹명 ]을 입력하세요.");
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

                sb.AppendLine(" select isnull(max(GrpID), 0) + 1 from T_Group where 1=1 ");
                string sRetVal = wConst.maxValue_Check(sb.ToString());
                if (sRetVal == "")
                {
                    MessageBox.Show("데이터 검색 중 오류 발생!!!");
                    return false;
                }
                sb = new StringBuilder();

                sb.AppendLine(" insert into T_Group ( ");
                sb.AppendLine("     GrpID ");
                sb.AppendLine("     , GrpName ");
                sb.AppendLine(" ) values ( ");
                sb.AppendLine("     @GrpID ");
                sb.AppendLine("     , @GrpName ");
                sb.AppendLine(" ) ");

                for (int kk = 0; kk < grdSet.Rows.Count; kk++)
                {
                    // 하위메뉴 저장
                    if ((string)grdSet.Rows[kk].Cells[2].Value != "")
                    {
                        sb.AppendLine(" insert into T_GroupSub ( ");
                        sb.AppendLine("     GrpID ");
                        sb.AppendLine("     , TopID ");
                        sb.AppendLine("     , SubID ");
                        sb.AppendLine(" ) values ( ");
                        sb.AppendLine("     @GrpID ");
                        sb.AppendLine("     , '" + (string)grdSet.Rows[kk].Cells[1].Value + "' ");
                        sb.AppendLine("     , '" + (string)grdSet.Rows[kk].Cells[2].Value + "' ");
                        sb.AppendLine(" ) ");
                    }
                }

                // 상위메뉴 저장
                sb.AppendLine(" insert into T_GroupTop ( GrpID, TopID )");
                sb.AppendLine(" select GrpID, TopID from T_GroupSub ");
                sb.AppendLine(" where GrpID = @GrpID ");
                sb.AppendLine(" group by GrpID, TopID ");

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@GrpID", sRetVal);
                sCommand.Parameters.AddWithValue("@GrpName", txtName.Text);

                int qResult = wAdo.SqlCommandEtc(sCommand, "Insert Group");
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

                sb.AppendLine(" update T_Group set ");
                sb.AppendLine("     GrpName = @GrpName ");
                sb.AppendLine(" where 1=1 ");
                sb.AppendLine("     and GrpID = @p1 ");

                sb.AppendLine(" delete from T_GroupSub ");
                sb.AppendLine(" where GrpID = @p1  ");

                sb.AppendLine(" delete from T_GroupTop ");
                sb.AppendLine(" where GrpID = @p1  ");

                for (int kk = 0; kk < grdSet.Rows.Count; kk++)
                {
                    // 하위메뉴 저장
                    if ((string)grdSet.Rows[kk].Cells[2].Value != "")
                    {
                        sb.AppendLine(" insert into T_GroupSub ( ");
                        sb.AppendLine("     GrpID ");
                        sb.AppendLine("     , TopID ");
                        sb.AppendLine("     , SubID ");
                        sb.AppendLine(" ) values ( ");
                        sb.AppendLine("     @p1 ");
                        sb.AppendLine("     , '" + (string)grdSet.Rows[kk].Cells[1].Value + "' ");
                        sb.AppendLine("     , '" + (string)grdSet.Rows[kk].Cells[2].Value + "' ");
                        sb.AppendLine(" ) ");
                    }
                }

                // 상위메뉴 저장
                sb.AppendLine(" insert into T_GroupTop ( GrpID, TopID )");
                sb.AppendLine(" select GrpID, TopID from T_GroupSub ");
                sb.AppendLine(" where GrpID = @p1 ");
                sb.AppendLine(" group by GrpID, TopID ");

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@p1", txtCode.Text);
                sCommand.Parameters.AddWithValue("@GrpName", txtName.Text);

                int qResult = wAdo.SqlCommandEtc(sCommand, "Update Group");
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

                sb.AppendLine(" delete from T_GroupUser ");
                sb.AppendLine(" where GrpID = @p1  ");

                sb.AppendLine(" delete from T_GroupSub ");
                sb.AppendLine(" where GrpID = @p1  ");

                sb.AppendLine(" delete from T_GroupTop ");
                sb.AppendLine(" where GrpID = @p1  ");

                sb.AppendLine(" delete from T_Group ");
                sb.AppendLine(" where GrpID = @p1  ");

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@p1", this.txtCode.Text);

                int qResult = wAdo.SqlCommandEtc(sCommand, "Delete Group");
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

        private void grdGet_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // 헤더 컬럼 클릭시
            if (e.ColumnIndex != 1) return;

            if (bHeadCheck == false)
            {
                grdGet.Columns[1].HeaderText = "[v]";
                bHeadCheck = true;
                wConst.select_Check(grdGet, 1, bHeadCheck);
            }
            else
            {
                grdGet.Columns[1].HeaderText = "[ ]";
                bHeadCheck = false;
                wConst.select_Check(grdGet, 1, bHeadCheck);
            }
            grdGet.RefreshEdit();
            grdGet.Refresh();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            this.grdSet.DataSource = null;
            this.grdSet.RowCount = 0;

            for (int kk = 0; kk < grdGet.Rows.Count; kk++)
            {
                if (grdGet.Rows[kk].Cells[1].Value != null)
                {
                    // 상위메뉴 : SubID = blank임
                    if ("" + (string)grdGet.Rows[kk].Cells[3].Value == "")
                    {
                        // 이전 row에 대한 SubID 체크하여 하위메뉴 없는 상위메뉴이면 삭제.
                        if (grdSet.Rows.Count > 0)
                        {
                            int nPrevRow = grdSet.Rows.Count - 1;

                            if ("" + (string)grdSet.Rows[nPrevRow].Cells[2].Value == "")
                            {
                                grdSet.RowCount -= 1;
                            }
                        }

                        // 상위메뉴 넣기
                        grdSet.RowCount += 1;

                        int nNewRow = grdSet.Rows.Count - 1;
                        grdSet.Rows[nNewRow].Cells[0].Value = grdGet.Rows[kk].Cells[0].Value;
                        grdSet.Rows[nNewRow].Cells[1].Value = grdGet.Rows[kk].Cells[2].Value;
                        grdSet.Rows[nNewRow].Cells[2].Value = grdGet.Rows[kk].Cells[3].Value;
                    }

                    // 하위메뉴 : SubID = 값 있음.
                    if ("" + (string)grdGet.Rows[kk].Cells[3].Value != "")
                    {
                        if ((bool)grdGet.Rows[kk].Cells[1].Value == true)
                        {
                            // 하위메뉴 넣기
                            grdSet.RowCount += 1;

                            int nNewRow = grdSet.Rows.Count - 1;
                            grdSet.Rows[nNewRow].Cells[0].Value = grdGet.Rows[kk].Cells[0].Value;
                            grdSet.Rows[nNewRow].Cells[1].Value = grdGet.Rows[kk].Cells[2].Value;
                            grdSet.Rows[nNewRow].Cells[2].Value = grdGet.Rows[kk].Cells[3].Value;
                        }
                    }
                }
            }

            // 마지막 row 체크
            // 이전 row에 대한 SubID 체크하여 하위메뉴 없는 상위메뉴이면 삭제.
            if (grdSet.Rows.Count > 0)
            {
                int nPrevRow = grdSet.Rows.Count - 1;

                if ("" + (string)grdSet.Rows[nPrevRow].Cells[2].Value == "")
                {
                    grdSet.RowCount -= 1;
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            this.init_InputBox(true);
            bindData_AllMenu("-1");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            if (validate_InputBox() == false)
            {
                btnSave.Enabled = true;
                return;
            }

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
                bindData_AllMenu("-1");
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

            lblBody.Visible = true;
            lblBody.Text = "Deleting ...";
            Application.DoEvents();

            bool bResult = this.deletePost();

            lblBody.Visible = false;

            if (bResult == true)
            {
                this.init_InputBox(true);
                bindData_AllMenu("-1");
                this.bindData(this.makeSearchCondition());
            }
        }

    }
}
