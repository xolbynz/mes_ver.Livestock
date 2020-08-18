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
    public partial class frm진료과목등록 : Form
    {
        private wnGConstant wConst = new wnGConstant();
        private bool bData = false;

        public frm진료과목등록()
        {
            InitializeComponent();
        }

        private void frm진료과목등록_Load(object sender, EventArgs e)
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
            
        }

        private void init_InputBox(bool bNew)
        {
            wConst.Form_Clear(spCont.Panel1.Controls);

            if (bNew == true)
            {
                bData = false;
                btnDelete.Enabled = false;
                txtCode.Enabled = true;
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
                dt = wDm.fn_MAJORCODE_List(condition);

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.GridRecord.RowCount = dt.Rows.Count;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.GridRecord.Rows[i].Cells[0].Value = dt.Rows[i]["MAJOR_CODE"].ToString();
                        this.GridRecord.Rows[i].Cells[1].Value = dt.Rows[i]["CODE_DESC"].ToString();
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
                dt = wDm.fn_MAJORCODE_Detail(sKey);

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.txtCode.Text = dt.Rows[0]["MAJOR_CODE"].ToString();
                    this.txtName.Text = dt.Rows[0]["CODE_DESC"].ToString();
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
                    MessageBox.Show("[ 진료과목 ]을 입력하세요.");
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

                sb.AppendLine(" insert into MAJORCODE ( ");
                sb.AppendLine("     MAJOR_CODE ");
                sb.AppendLine("     , CODE_DESC ");
                sb.AppendLine(" ) values ( ");
                sb.AppendLine("     @MAJOR_CODE ");
                sb.AppendLine("     , @CODE_DESC ");
                sb.AppendLine(" ) ");

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@MAJOR_CODE", txtCode.Text);
                sCommand.Parameters.AddWithValue("@CODE_DESC", txtName.Text);

                int qResult = wAdo.SqlCommandEtc(sCommand, "Insert MAJORCODE");
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

                sb.AppendLine(" update MAJORCODE set ");
                sb.AppendLine("     CODE_DESC = @CODE_DESC ");
                sb.AppendLine(" where 1=1 ");
                sb.AppendLine("     and MAJOR_CODE = @p1 ");

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@p1", txtCode.Text);
                sCommand.Parameters.AddWithValue("@CODE_DESC", txtName.Text);

                int qResult = wAdo.SqlCommandEtc(sCommand, "Update MAJORCODE");
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

                sb.AppendLine(" delete from MAJORCODE ");
                sb.AppendLine(" where MAJOR_CODE = @p1  ");

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@p1", this.txtCode.Text);

                int qResult = wAdo.SqlCommandEtc(sCommand, "Delete MAJORCODE");
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
                    dt = wDm.fn_MAJORCODE_Detail(sNew);

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
