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

namespace 스마트팩토리.P10_ORD
{
    public partial class frm할인마감 : Form
    {
        private wnGConstant wConst = new wnGConstant();
        private bool bEditText = false;

        public frm할인마감()
        {
            InitializeComponent();
        }

        private void frm할인마감_Load(object sender, EventArgs e)
        {
            panBody.Top = 39;
            panBody.Left = this.ClientSize.Width / 2 - panBody.Width / 2;

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
                btnSave.Enabled = true;
                return;
            }

            lblSaving.Left = panBody.Width / 2 - lblSaving.Width / 2;
            lblSaving.Top = panBody.Height / 2 - lblSaving.Height / 2;
            lblSaving.Text = "Processing ...";
            lblSaving.Visible = true;
            lblSaving.BringToFront();
            Application.DoEvents();

            bool bResult = false;

            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_PRODUCT_STOCK_Magam(dtp마감일자1.Text, dtp마감일자2.Text, txtS거래처코드.Text);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        progBar.Maximum = dt.Rows.Count;

                        for (int kk = 0; kk < dt.Rows.Count; kk++)
                        {
                            progBar.Value = kk + 1;
                            Application.DoEvents();

                            //거래처별 할인마감 처리
                            bResult = this.savePost(dt.Rows[kk]["CUST_CODE1"].ToString().Trim(), dt.Rows[kk]["CUST_NAME1"].ToString().Trim());

                            if (bResult == false)
                            {
                                break;
                            }
                        }

                        if (bResult == true)
                        {
                            MessageBox.Show("할인마감을 완료했습니다.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("할인마감 대상 거래처가 해당 기간내에 없습니다.");
                    }
                }
                else
                {
                    MessageBox.Show("할인마감 대상 검색중에 오류가 발생했습니다.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("검색중에 오류가 발생했습니다.");
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
            }

            lblSaving.Visible = false;
            btnSave.Enabled = true;
        }

        private bool validate_InputBox()
        {
            bool bRet = true;

            try
            {
                if (chkAll.Checked == false)
                {
                    if (this.txtS거래처코드.Text.Trim() == "")
                    {
                        MessageBox.Show("[ 거래처 ]를 입력하세요.");
                        return false;
                    }
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

        private bool savePost(string sCust, string sCustName)
        {
            bool bRet = false;

            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                sb.AppendLine(" exec dbo.proc_할인마감 @Day1, @Day2, @SDay, @CustCode, @UserCode ");

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@Day1", dtp마감일자1.Text);
                sCommand.Parameters.AddWithValue("@Day2", dtp마감일자2.Text);
                sCommand.Parameters.AddWithValue("@SDay", dtp수금일자.Text);
                sCommand.Parameters.AddWithValue("@CustCode", sCust);
                sCommand.Parameters.AddWithValue("@UserCode", Common.p_strUserCode);

                int qResult = wAdo.SqlCommandEtc(sCommand, "MaGam PRODUCT_STOCK to PRODUCT_COLLECT");
                if (qResult > 0) bRet = true;
                else bRet = false;

                if (bRet == true)
                {
                }
                else
                {
                    MessageBox.Show("거래처 [ " + sCustName + " ]의 할인마감 중에 오류가 발생했습니다.");
                }
            }
            catch (Exception ex)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
                MessageBox.Show("데이터베이스에 문제가 발생했습니다.");
            }
            return bRet;
        }

        private void frm할인마감_SizeChanged(object sender, EventArgs e)
        {
            panBody.Left = this.ClientSize.Width / 2 - panBody.Width / 2;
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

        private void chkAll_Click(object sender, EventArgs e)
        {
            txtS거래처명.Enabled = true;
            btnS거래처.Enabled = true;

            if (chkAll.Checked == true)
            {
                txtS거래처코드.Text = "";
                txtS거래처명.Text = "";
                txtS거래처명.Enabled = false;
                btnS거래처.Enabled = false;
            }
        }

    }
}
