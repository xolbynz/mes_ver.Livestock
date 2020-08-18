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
    public partial class frm그룹사용자관리 : Form
    {
        private wnGConstant wConst = new wnGConstant();
        private bool bData = false;
        private bool bHeadCheck_User = false;
        private bool bHeadCheck_User2 = false;
        private bool bHeadCheck_FW = false;
        private bool bHeadCheck_FP = false;

        public frm그룹사용자관리()
        {
            InitializeComponent();
        }

        private void frm그룹사용자관리_Load(object sender, EventArgs e)
        {
            spCont.Dock = DockStyle.Fill;

            lblSearch.BringToFront();
            lblBody.BringToFront();

            this.init_InputBox(true);
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
            lblGrpCode.Text = "";
            lblGrpName.Text = "";

            this.grdSetUser.DataSource = null;
            this.grdSetUser.RowCount = 0;

            this.grdGetUser.DataSource = null;
            this.grdGetUser.RowCount = 0;

            if (bNew == true)
            {
                bData = false;
                //btnDelete.Enabled = false;
            }
            else
            {
                bData = true;
                //btnDelete.Enabled = true;
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
            bindData_UnCheck_User();
            bindData_GroupUser(strValue);
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
                    this.lblGrpCode.Text = dt.Rows[0]["GrpID"].ToString();
                    this.lblGrpName.Text = dt.Rows[0]["GrpName"].ToString();
                }
            }
            catch (Exception ex)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
            }

            lblBody.Visible = false;
        }

        private void bindData_UnCheck_User()
        {
            lblBody.Visible = true;
            lblBody.Text = "Loading ...";
            Application.DoEvents();

            this.grdGetUser.Columns[5].HeaderText = "[ ]";
            bHeadCheck_User = false;

            this.grdGetUser.DataSource = null;
            this.grdGetUser.RowCount = 0;

            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_User_UnCheckList();

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.grdGetUser.RowCount = dt.Rows.Count;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        // 미소속 사용자 목록
                        this.grdGetUser.Rows[i].Cells[0].Value = dt.Rows[i]["USER_CODE"].ToString();
                        this.grdGetUser.Rows[i].Cells[1].Value = dt.Rows[i]["USER_ID"].ToString();
                        this.grdGetUser.Rows[i].Cells[2].Value = dt.Rows[i]["USER_NAME"].ToString();
                        if (dt.Rows[i]["FlgWrite"].ToString() == "Y")
                        {
                            this.grdGetUser.Rows[i].Cells[3].Value = true;
                        }
                        else
                        {
                            this.grdGetUser.Rows[i].Cells[3].Value = false;
                        }
                        if (dt.Rows[i]["FlgPrint"].ToString() == "Y")
                        {
                            this.grdGetUser.Rows[i].Cells[4].Value = true;
                        }
                        else
                        {
                            this.grdGetUser.Rows[i].Cells[4].Value = false;
                        }
                        this.grdGetUser.Rows[i].Cells[5].Value = false;
                    }
                }
            }
            catch (Exception ex)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
            }

            lblBody.Visible = false;
        }

        private void bindData_GroupUser(string sID)
        {
            lblBody.Visible = true;
            lblBody.Text = "Loading ...";
            Application.DoEvents();

            this.grdSetUser.Columns[5].HeaderText = "[ ]";
            bHeadCheck_User2 = false;
            bHeadCheck_FW = false;
            bHeadCheck_FP = false;

            this.grdSetUser.DataSource = null;
            this.grdSetUser.RowCount = 0;

            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_GroupUser_List(sID);

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.grdSetUser.RowCount = dt.Rows.Count;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.grdSetUser.Rows[i].Cells[0].Value = dt.Rows[i]["USER_CODE"].ToString();
                        this.grdSetUser.Rows[i].Cells[1].Value = dt.Rows[i]["USER_ID"].ToString();
                        this.grdSetUser.Rows[i].Cells[2].Value = dt.Rows[i]["USER_NAME"].ToString();
                        if (dt.Rows[i]["FlgWrite"].ToString() == "Y")
                        {
                            this.grdSetUser.Rows[i].Cells[3].Value = true;
                        }
                        else
                        {
                            this.grdSetUser.Rows[i].Cells[3].Value = false;
                        }
                        if (dt.Rows[i]["FlgPrint"].ToString() == "Y")
                        {
                            this.grdSetUser.Rows[i].Cells[4].Value = true;
                        }
                        else
                        {
                            this.grdSetUser.Rows[i].Cells[4].Value = false;
                        }
                        this.grdSetUser.Rows[i].Cells[5].Value = false;
                    }
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

        private void btnUp2_Click(object sender, EventArgs e)
        {
            if (lblGrpCode.Text == "") return;

            for (int kk = grdGetUser.Rows.Count - 1; kk >= 0; kk--)
            {
                if ((bool)grdGetUser.Rows[kk].Cells[5].Value == true)
                {
                    grdGetUser.Rows[kk].Cells[5].Value = false;

                    grdSetUser.RowCount += 1;

                    int nNewRow = grdSetUser.Rows.Count - 1;
                    for (int cc = 0; cc < grdGetUser.Columns.Count; cc++)
                    {
                        grdSetUser.Rows[nNewRow].Cells[cc].Value = grdGetUser.Rows[kk].Cells[cc].Value;
                    }
                    grdGetUser.Rows.RemoveAt(kk);
                }
            }
            bHeadCheck_User = false;
            this.grdGetUser.Columns[5].HeaderText = "[ ]";
        }

        private void btnDn2_Click(object sender, EventArgs e)
        {
            if (lblGrpCode.Text == "") return;

            for (int kk = grdSetUser.Rows.Count - 1; kk >= 0; kk--)
            {
                if ((bool)grdSetUser.Rows[kk].Cells[5].Value == true)
                {
                    grdSetUser.Rows[kk].Cells[5].Value = false;

                    grdGetUser.RowCount += 1;

                    int nNewRow = grdGetUser.Rows.Count - 1;
                    for (int cc = 0; cc < grdSetUser.Columns.Count; cc++)
                    {
                        grdGetUser.Rows[nNewRow].Cells[cc].Value = grdSetUser.Rows[kk].Cells[cc].Value;
                    }
                    grdSetUser.Rows.RemoveAt(kk);
                }
            }
            bHeadCheck_User2 = false;
            this.grdSetUser.Columns[5].HeaderText = "[ ]";
        }

        private void grdGetUser_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // 헤더 컬럼 클릭시
            if (e.ColumnIndex != 5) return;

            if (bHeadCheck_User == false)
            {
                grdGetUser.Columns[5].HeaderText = "[v]";
                bHeadCheck_User = true;
                wConst.select_Check(grdGetUser, 5, bHeadCheck_User);
            }
            else
            {
                grdGetUser.Columns[5].HeaderText = "[ ]";
                bHeadCheck_User = false;
                wConst.select_Check(grdGetUser, 5, bHeadCheck_User);
            }
            grdGetUser.RefreshEdit();
            grdGetUser.Refresh();
        }

        private void grdSetUser_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // 헤더 컬럼 클릭시
            if (e.ColumnIndex == 3)
            {
                if (bHeadCheck_FW == false)
                {
                    //grdSetUser.Columns[2].HeaderText = "[v]";
                    bHeadCheck_FW = true;
                    wConst.select_Check(grdSetUser, 3, bHeadCheck_FW);
                }
                else
                {
                    //grdSetUser.Columns[2].HeaderText = "[ ]";
                    bHeadCheck_FW = false;
                    wConst.select_Check(grdSetUser, 3, bHeadCheck_FW);
                }
            }
            if (e.ColumnIndex == 4)
            {
                if (bHeadCheck_FP == false)
                {
                    //grdSetUser.Columns[3].HeaderText = "[v]";
                    bHeadCheck_FP = true;
                    wConst.select_Check(grdSetUser, 4, bHeadCheck_FP);
                }
                else
                {
                    //grdSetUser.Columns[3].HeaderText = "[ ]";
                    bHeadCheck_FP = false;
                    wConst.select_Check(grdSetUser, 4, bHeadCheck_FP);
                }
            }
            if (e.ColumnIndex == 5)
            {
                if (bHeadCheck_User2 == false)
                {
                    grdSetUser.Columns[5].HeaderText = "[v]";
                    bHeadCheck_User2 = true;
                    wConst.select_Check(grdSetUser, 5, bHeadCheck_User2);
                }
                else
                {
                    grdSetUser.Columns[5].HeaderText = "[ ]";
                    bHeadCheck_User2 = false;
                    wConst.select_Check(grdSetUser, 5, bHeadCheck_User2);
                }
            }
            grdSetUser.RefreshEdit();
            grdSetUser.Refresh();
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
            bResult = this.insertPost();

            lblBody.Visible = false;

            if (bResult == true)
            {
                this.init_InputBox(true);
                //this.bindData(this.makeSearchCondition());
            }
            btnSave.Enabled = true;
        }

        private bool validate_InputBox()
        {
            bool bRet = true;

            try
            {
                //if (this.txtGrpName.Text.Trim() == "")
                //{
                //    MessageBox.Show("[ 그룹명 ]을 입력하세요.");
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

                sb.AppendLine(" delete from T_GroupUser ");
                sb.AppendLine(" where GrpID = @GrpID  ");

                for (int kk = 0; kk < grdSetUser.Rows.Count; kk++)
                {
                    if ("" + (string)grdSetUser.Rows[kk].Cells[0].Value != "")
                    {
                        sb.AppendLine(" insert into T_GroupUser ( ");
                        sb.AppendLine("     GrpID ");
                        sb.AppendLine("     , USER_CODE ");
                        sb.AppendLine(" ) values ( ");
                        sb.AppendLine("     @GrpID ");
                        sb.AppendLine("     , '" + (string)grdSetUser.Rows[kk].Cells[0].Value + "' ");
                        sb.AppendLine(" ) ");

                        string strWrite = "N";
                        string strPrint = "N";
                        if (grdSetUser.Rows[kk].Cells[3].Value != null)
                        {
                            if ((bool)grdSetUser.Rows[kk].Cells[3].Value == true)
                            {
                                strWrite = "Y";
                            }
                        }
                        if (grdSetUser.Rows[kk].Cells[4].Value != null)
                        {
                            if ((bool)grdSetUser.Rows[kk].Cells[4].Value == true)
                            {
                                strPrint = "Y";
                            }
                        }

                        sb.AppendLine(" update USER_PASSWD set ");
                        sb.AppendLine("     FlgWrite = '" + strWrite + "' ");
                        sb.AppendLine("     , FlgPrint = '" + strPrint + "' ");
                        sb.AppendLine(" where 1=1 ");
                        sb.AppendLine("     and USER_CODE = '" + (string)grdSetUser.Rows[kk].Cells[0].Value + "' ");
                    }
                }

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@GrpID", lblGrpCode.Text);

                int qResult = wAdo.SqlCommandEtc(sCommand, "Insert GroupUser");
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
