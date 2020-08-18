using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 스마트팩토리.CLS;

namespace 스마트팩토리.P85_BAS
{
    public partial class frm거래처등록 : Form
    {
        private wnGConstant wConst = new wnGConstant();
        private ComInfo comInfo = new ComInfo();

        public frm거래처등록()
        {
            InitializeComponent();
        }

        private void frm거래처등록_Load(object sender, EventArgs e)
        {
            ComInfo.gridHeaderSet(dataCustGrid);

            init_ComboBox();
            cust_list();
            resetSetting();
        }

        #region common 
        private void btnNew_Click(object sender, EventArgs e)
        {
            resetSetting();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            cust_logic();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            cust_del();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void init_ComboBox()
        {
            string sqlQuery = "";
            //불량유형 시작
            cmb_cust_gbn.Items.Add("전체 검색");
            cmb_cust_gbn.Items.Add("매출처");
            cmb_cust_gbn.Items.Add("구매처");
            cmb_cust_gbn.SelectedIndex = 0;

            cmb_used.ValueMember = "코드";
            cmb_used.DisplayMember = "명칭";
            sqlQuery = comInfo.queryCustUsedYn();
            wConst.ComboBox_Read_NoBlank(cmb_used, sqlQuery);

            cmb_used_srch.ValueMember = "코드";
            cmb_used_srch.DisplayMember = "명칭";
            sqlQuery = comInfo.queryCustUsedYnAll();
            wConst.ComboBox_Read_NoBlank(cmb_used_srch, sqlQuery);

            cmb_manager.ValueMember = "코드";
            cmb_manager.DisplayMember = "명칭";
            sqlQuery = comInfo.queryStaff();
            wConst.ComboBox_Read_Blank(cmb_manager, sqlQuery);

            cmb_tax_nm.ValueMember = "코드";
            cmb_tax_nm.DisplayMember = "명칭";
            sqlQuery = comInfo.queryTax();
            wConst.ComboBox_Read_Blank(cmb_tax_nm, sqlQuery);
            
        }

        private void resetSetting()
        {
            lbl_cust_gbn.Text = "";
            btnDelete.Enabled = false;
            txt_cust_cd.Text = "";
            txt_cust_cd.Enabled = true;
            txt_cust_nm.Text = "";
            txt_owner.Text = "";
            txt_saup_no.Text = "";
            txt_uptae.Text = "";
            txt_jong.Text = "";
            txt_deal_type.Text = "";
            txt_post_no.Text = "";
            txt_addr.Text = "";
            txt_cust_manager.Text = "";
            txt_email.Text = "";
            txt_comp_phone.Text = "";
            txt_phone.Text = "";
            txt_fax.Text = "";
            cmb_manager.SelectedIndex = 0;
            cmb_used.SelectedIndex = 0;
            txt_comment.Text = "";
            cmb_tax_nm.SelectedIndex = 0;
            txt_balance.Text = "0";
            //cmb_raw_stor.SelectedIndex = 0;
            //cmb_cust.SelectedIndex = 0;
        }

        #endregion common

        #region cust

        private void cust_logic()  // 2019.12.09 문세진 거래처 등록에 부가세, 현 잔고 추가
        {
            try
            {
                if (cmb_manager.SelectedValue == null) cmb_manager.SelectedValue = "";
                if (cmb_used.SelectedValue == null) cmb_used.SelectedValue = "";
                
                if (txt_cust_cd.Text.ToString().Equals("")) 
                {
                    MessageBox.Show("거래처코드를 입력하시기 바랍니다.");
                    return;
                }
                if (txt_cust_nm.Text.ToString().Equals("")) 
                {
                    MessageBox.Show("거래처명을 입력하시기 바랍니다.");
                    return;
                }

                if (cmb_manager.SelectedIndex ==0 || cmb_manager == null) 
                {
                    MessageBox.Show("담당자를 선택하시기 바랍니다.");
                    return;
                }

                if (cmb_tax_nm.SelectedIndex == 0 || cmb_tax_nm == null)
                {
                    MessageBox.Show("부가세를 선택하시기 바랍니다.");
                    return;
                }

                if (txt_balance.Text.ToString().Equals(""))
                {
                    MessageBox.Show("현잔고를 입력하시기 바랍니다.");
                }
                string cust_gbn = ""; // 매출 1 , 매입 2
                if (radio_pur.Checked == true)
                {
                    cust_gbn = radio_pur.TabIndex.ToString();
                }
                else {
                    cust_gbn = radio_sales.TabIndex.ToString();
                }

                if (lbl_cust_gbn.Text != "1")
                {
                    wnDm wDm = new wnDm();
                    int rsNum = wDm.insertCust(
                                      txt_cust_cd.Text.ToString()
                                    , cust_gbn
                                    , txt_cust_nm.Text.ToString()
                                    , txt_owner.Text.ToString()
                                    , txt_saup_no.Text.ToString()
                                    , txt_uptae.Text.ToString()
                                    , txt_jong.Text.ToString()
                                    , txt_deal_type.Text.ToString()
                                    , txt_post_no.Text.ToString()
                                    , txt_addr.Text.ToString()
                                    , txt_cust_manager.Text.ToString()
                                    , txt_email.Text.ToString()
                                    , txt_comp_phone.Text.ToString()
                                    , txt_phone.Text.ToString()
                                    , txt_fax.Text.ToString()
                                    , txt_start_date.Text.ToString()
                                    , cmb_manager.SelectedValue.ToString()
                                    , cmb_used.SelectedValue.ToString()
                                    , txt_comment.Text.ToString()
                                    , cmb_tax_nm.SelectedValue.ToString()
                                    , txt_balance.Text.ToString());

                    if (rsNum == 0)
                    {
                        resetSetting();
                        cust_list();
                        MessageBox.Show("성공적으로 등록하였습니다.");
                    }
                    else if (rsNum == 1)
                        MessageBox.Show("저장에 실패하였습니다");
                    else if (rsNum == 2)
                        MessageBox.Show("SQL COMMAND 에러");
                    else if (rsNum == 3)
                        MessageBox.Show("기존 코드가 있으니 \n 다른 코드로 입력 바랍니다.");
                    else
                        MessageBox.Show("Exception 에러1");
                }
                else 
                {
                    wnDm wDm = new wnDm();
                    int rsNum = wDm.updateCust(
                                      txt_cust_cd.Text.ToString()
                                    , cust_gbn
                                    , txt_cust_nm.Text.ToString()
                                    , txt_owner.Text.ToString()
                                    , txt_saup_no.Text.ToString()
                                    , txt_uptae.Text.ToString()
                                    , txt_jong.Text.ToString()
                                    , txt_deal_type.Text.ToString()
                                    , txt_post_no.Text.ToString()
                                    , txt_addr.Text.ToString()
                                    , txt_cust_manager.Text.ToString()
                                    , txt_email.Text.ToString()
                                    , txt_comp_phone.Text.ToString()
                                    , txt_phone.Text.ToString()
                                    , txt_fax.Text.ToString()
                                    , txt_start_date.Text.ToString()
                                    , cmb_manager.SelectedValue.ToString()
                                    , cmb_used.SelectedValue.ToString()
                                    , txt_comment.Text.ToString()
                                    , cmb_tax_nm.SelectedValue.ToString()
                                    );

                    Console.WriteLine(cmb_tax_nm.SelectedValue.ToString());

                    if (rsNum == 0)
                    {
                        cust_list();
                        MessageBox.Show("성공적으로 수정하였습니다.");
                    }
                    else if (rsNum == 1)
                        MessageBox.Show("저장에 실패하였습니다");
                    else
                        MessageBox.Show("Exception 에러1");
                }
            }
            catch (Exception e) 
            {
                MessageBox.Show("시스템 오류");
            }
        }

        private void cust_list()
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("where 1=1 ");

                if (cmb_used_srch.SelectedIndex > 0)
                {
                    sb.AppendLine("and USED_CD = '" + cmb_used_srch.SelectedValue.ToString() + "' ");
                }

                if (cmb_cust_gbn.SelectedIndex > 0)
                {
                    sb.AppendLine("and CUST_GUBUN = '" + cmb_cust_gbn.SelectedIndex.ToString() + "' ");
                }

                if (txt_srch.Text.ToString().Equals(""))
                {
                    dt = wDm.fn_Cust_List(sb.ToString());
                }
                else
                {
                    sb.AppendLine("and CUST_NM like '%" + txt_srch.Text.ToString() + "%' ");
                    dt = wDm.fn_Cust_List(sb.ToString());
                }

                cust_list_rs(dt);
            }
            catch (Exception e)
            {
                MessageBox.Show("시스템 오류: " + e.ToString());
            }
        }

        private void cust_list_rs(DataTable dt)
        {
            lbl_cnt.Text = dt.Rows.Count.ToString();

            if (dt != null && dt.Rows.Count > 0)
            {
                this.dataCustGrid.RowCount = dt.Rows.Count;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    
                    if(dt.Rows[i]["USED_NM"].ToString().Equals("중지")){
                        dataCustGrid.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    }else if(dt.Rows[i]["USED_NM"].ToString().Equals("종료")){
                        dataCustGrid.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    }
                    else if (dt.Rows[i]["USED_NM"].ToString().Equals("사용"))
                    {
                        dataCustGrid.Rows[i].DefaultCellStyle.BackColor = Color.Empty;
                    }

                    dataCustGrid.Rows[i].Cells[0].Value = dt.Rows[i]["CUST_GUBUN_NM"].ToString();
                    dataCustGrid.Rows[i].Cells[1].Value = dt.Rows[i]["CUST_CD"].ToString();
                    dataCustGrid.Rows[i].Cells[2].Value = dt.Rows[i]["CUST_NM"].ToString();
                    dataCustGrid.Rows[i].Cells[3].Value = dt.Rows[i]["JONGMOK"].ToString();
                    dataCustGrid.Rows[i].Cells[4].Value = dt.Rows[i]["USED_NM"].ToString();


                }
            }
            else
            {
                dataCustGrid.Rows.Clear();
            }
        }


        #endregion cust

        private void dataCustGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            btnDelete.Enabled = true;
            lbl_cust_gbn.Text = "1";
            txt_cust_cd.Enabled = false;

            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                //2019-10-31 이재원
                //우측 리스트에서 구매처, 매출처가 동일한 코드일때 더블클릭시 매출처만 지정되는 오류 수정
                string condition = "where cust_cd = '" + dataCustGrid.Rows[e.RowIndex].Cells[1].Value.ToString()
                    + "' and cust_gubun = '" + (dataCustGrid.Rows[e.RowIndex].Cells[0].Value.ToString().Equals("매출처")? "1":"2") + "'       ";

                dt = wDm.fn_Cust_List(condition);

                if (dt != null && dt.Rows.Count > 0)
                {

                    if (dt.Rows[0]["CUST_GUBUN"].ToString().Equals("1"))
                    { //매출
                        radio_sales.Checked = true;
                        radio_pur.Checked = false;
                    }
                    else 
                    { //매입
                        radio_sales.Checked = false;
                        radio_pur.Checked = true;
                    }
                    txt_cust_cd.Text = dt.Rows[0]["CUST_CD"].ToString();
                    txt_cust_nm.Text = dt.Rows[0]["CUST_NM"].ToString();
                    txt_owner.Text = dt.Rows[0]["OWNER"].ToString();
                    txt_saup_no.Text = dt.Rows[0]["SAUP_NO"].ToString();
                    txt_uptae.Text = dt.Rows[0]["UPTAE"].ToString();
                    txt_jong.Text = dt.Rows[0]["JONGMOK"].ToString();
                    txt_deal_type.Text = dt.Rows[0]["DEAL_TYPE"].ToString();
                    txt_post_no.Text = dt.Rows[0]["POST_NO"].ToString();
                    txt_addr.Text = dt.Rows[0]["ADDR"].ToString();
                    txt_cust_manager.Text = dt.Rows[0]["CUST_MANAGER"].ToString();
                    txt_email.Text = dt.Rows[0]["CUST_EMAIL"].ToString();
                    txt_comp_phone.Text = dt.Rows[0]["CUST_COMP_PHONE"].ToString();
                    txt_phone.Text = dt.Rows[0]["CUST_PHONE"].ToString();
                    cmb_manager.SelectedValue = dt.Rows[0]["STAFF_CD"].ToString();
                    cmb_used.SelectedValue = int.Parse(dt.Rows[0]["USED_CD"].ToString());
                    txt_comment.Text = dt.Rows[0]["COMMENT"].ToString();

                    cmb_tax_nm.SelectedValue = dt.Rows[0]["TAX_CD"].ToString();

                    if (dt.Rows[0]["BALANCE"] != null && !dt.Rows[0]["BALANCE"].ToString().Equals(""))
                        txt_balance.Text = (decimal.Parse(dt.Rows[0]["BALANCE"].ToString())).ToString("#,0.######");
                    else
                        txt_balance.Text = "0";
                }
                else
                {
                    dataCustGrid.Rows.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("시스템 오류: " + ex.ToString());
            }
        }

        private void cust_del()
        {
            string cust_gbn = ""; // 매출 1 , 매입 2
            if (radio_pur.Checked == true)
            {
                cust_gbn = radio_pur.TabIndex.ToString();
            }
            else
            {
                cust_gbn = radio_sales.TabIndex.ToString();

            }
            wnDm wDm = new wnDm();
            int rsNum = wDm.deleteCust(txt_cust_cd.Text.ToString(), cust_gbn);
            if (rsNum == 0)
            {
                resetSetting();

                cust_list();
                MessageBox.Show("성공적으로 삭제하였습니다.");
            }
            else if (rsNum == 1)
            {
                MessageBox.Show("삭제에 실패하였습니다.");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            cust_list();
        }

        private void btn_Change_balance_Click(object sender, EventArgs e)
        {
            if (lbl_cust_gbn.Text.Equals("1"))
            {
                Popup.pop거래처잔고수정 msg = new Popup.pop거래처잔고수정();
                msg.sCust_nm = txt_cust_nm.Text;
                msg.sCust_cd = txt_cust_cd.Text;
                msg.sUptae = txt_uptae.Text;
                msg.sJongmock = txt_jong.Text;
                msg.sDamdang = txt_cust_manager.Text;
                msg.sSaupNo = txt_saup_no.Text;
                msg.sOwner = txt_owner.Text;
                msg.sPhone = txt_phone.Text;
                msg.sFax = txt_fax.Text;
                msg.sOpenDay = txt_start_date.Text;
                msg.sAddr = txt_addr.Text;
                if(cmb_tax_nm.SelectedValue.ToString().Equals("1"))
                {
                    msg.sTax_nm = "면세";
                }
                else
                {
                    msg.sTax_nm = "과세";
                }
                msg.sBalance = txt_balance.Text;

                msg.StartPosition = FormStartPosition.CenterParent;
                msg.ShowDialog();
            }
            else
            {
                MessageBox.Show("거래처를 일단 등록 후 수정해 주십시오.");
            }
        }
    }
}
