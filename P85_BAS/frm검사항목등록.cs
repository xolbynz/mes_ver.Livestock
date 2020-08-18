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

namespace 스마트팩토리.P85_BAS
{
    public partial class frm검사항목등록 : Form
    {
        public frm검사항목등록()
        {
            InitializeComponent();
        }

        private void frm검사항목등록_Load(object sender, EventArgs e)
        {
            init_ComboBox();
            chk_list();
        }

        #region button logic 

        private void btnNew_Click(object sender, EventArgs e)
        {
            resetSetting();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            chk_logic();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            chk_del();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion button logic


        #region logic 

        private void init_ComboBox()
        {
            ComInfo comInfo = new ComInfo();

            cmb_chk_gbn.Items.Add("전체 검색");
            cmb_chk_gbn.Items.Add("공정");
            cmb_chk_gbn.Items.Add("제품");
            cmb_chk_gbn.SelectedIndex = 0;
        }

        private void chk_logic() 
        {
            try
            {
                if (txt_chk_cd.Text.ToString().Equals("")) 
                {
                    MessageBox.Show("검사항목코드를 입력하시기 바랍니다.");
                    return;
                }
                if (txt_chk_nm.Text.ToString().Equals(""))
                {
                    MessageBox.Show("검사항목코드를 입력하시기 바랍니다.");
                    return;
                }

                if (txt_chk_ord.Text.ToString().Equals("")) 
                {
                    MessageBox.Show("검사순서를 입력하시기 바랍니다.");
                    return;
                }

                string chk_gbn = ""; // 공정 1 , 제품 2
                if (radio_flow.Checked == true)
                {
                    chk_gbn = radio_flow.TabIndex.ToString();
                }
                else if (radio_item.Checked == true)
                {
                    chk_gbn = radio_item.TabIndex.ToString();

                }
                else
                {
                    chk_gbn = radio_raw.TabIndex.ToString();
                }


                if (lbl_chk_gbn.Text != "1")
                {
                    wnDm wDm = new wnDm();
                    int rsNum = wDm.insertChk(
                                      txt_chk_cd.Text.ToString()
                                    , chk_gbn
                                    , txt_chk_nm.Text.ToString()
                                    , txt_chk_ord.Text.ToString()
                                    , txt_comment.Text.ToString());

                    if (rsNum == 0)
                    {
                        resetSetting();
                        chk_list();
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
                    int rsNum = wDm.updateChk(
                                      txt_chk_cd.Text.ToString()
                                    , chk_gbn
                                    , txt_chk_nm.Text.ToString()
                                    , txt_chk_ord.Text.ToString()
                                    , txt_comment.Text.ToString());

                    if (rsNum == 0)
                    {
                        chk_list();
                        MessageBox.Show("성공적으로 수정하였습니다.");
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
            }
            catch (Exception e) 
            {
                MessageBox.Show("시스템 오류: " + e.ToString());
            }
        }

        private void resetSetting()
        {
            radio_flow.Enabled = true;
            radio_item.Enabled = true;
            radio_raw.Enabled = true;

            lbl_chk_gbn.Text = "";
            btnDelete.Enabled = false;
            txt_chk_cd.Text = "";
            txt_chk_cd.Enabled = true;
            txt_chk_ord.Text = "";
            txt_chk_nm.Text = "";
            txt_comment.Text = "";
        }

        private void chk_list()
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("where 1=1 ");
                if (cmb_chk_gbn.SelectedIndex > 0) 
                {
                    sb.AppendLine("and A.CHK_GUBUN = '" + cmb_chk_gbn.SelectedIndex.ToString() + "' ");
                }
                if (!txt_srch.Text.ToString().Equals(""))
                {
                    sb.AppendLine("and CHK_NM like '%" + txt_srch.Text.ToString() + "%' ");
                }

                dt = wDm.fn_Chk_List(sb.ToString());

                lbl_cnt.Text = dt.Rows.Count.ToString();

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.dataChkGrid.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dataChkGrid.Rows[i].Cells["CHK_CD"].Value = dt.Rows[i]["CHK_CD"].ToString();
                        dataChkGrid.Rows[i].Cells["CHK_GUBUN_NM"].Value = dt.Rows[i]["CHK_GUBUN_NM"].ToString();
                        dataChkGrid.Rows[i].Cells["CHK_NM"].Value = dt.Rows[i]["CHK_NM"].ToString();
                        dataChkGrid.Rows[i].Cells["CHK_ORD"].Value = dt.Rows[i]["CHK_ORD"].ToString();
                        dataChkGrid.Rows[i].Cells["CHK_GUBUN"].Value = dt.Rows[i]["CHK_GUBUN"].ToString();
                        dataChkGrid.Rows[i].Cells["COMMENT"].Value = dt.Rows[i]["COMMENT"].ToString();
                    }
                }
                else
                {
                    dataChkGrid.Rows.Clear();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("시스템 오류: " + e.ToString());
            }
        }
        #endregion logic

        private void dataChkGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = dataChkGrid;
            int idx = e.RowIndex;
            btnDelete.Enabled = true;
            lbl_chk_gbn.Text = "1";
            txt_chk_cd.Enabled = false;

            radio_flow.Enabled = false;
            radio_item.Enabled = false;
            radio_raw.Enabled = false;
            try
            {
                if (dgv.Rows[idx].Cells["CHK_GUBUN"].Value.ToString().Equals("1"))
                { //공정
                    radio_flow.Checked = true;
                    radio_item.Checked = false;
                    radio_raw.Checked = false;
                }
                else if (dgv.Rows[idx].Cells["CHK_GUBUN"].Value.ToString().Equals("2"))
                { //제품
                    radio_flow.Checked = false;
                    radio_item.Checked = true;
                    radio_raw.Checked = false;
                }
                else
                { //수입검사
                    radio_flow.Checked = false;
                    radio_item.Checked = false;
                    radio_raw.Checked = true;
                }

                txt_chk_cd.Text = dgv.Rows[idx].Cells["CHK_CD"].Value.ToString();
                txt_chk_ord.Text = dgv.Rows[idx].Cells["CHK_ORD"].Value.ToString();
                txt_chk_nm.Text = dgv.Rows[idx].Cells["CHK_NM"].Value.ToString();
                txt_comment.Text = dgv.Rows[idx].Cells["COMMENT"].Value.ToString();
            }
            catch (Exception ex) 
            {
                MessageBox.Show("시스템 오류: " + ex.ToString());
            }
        }

        private void chk_del()
        {
            string chk_gbn = ""; // 공정검사 1 , 제품검사 2 , 원자재수입검사 3
            if (radio_flow.Checked == true)
            {
                chk_gbn = radio_flow.TabIndex.ToString();
            }
            else if (radio_item.Checked == true)
            {
                chk_gbn = radio_item.TabIndex.ToString();

            }
            else 
            {
                chk_gbn = radio_raw.TabIndex.ToString();
            }

            wnDm wDm = new wnDm();
            int rsNum = wDm.deleteChk(txt_chk_cd.Text.ToString(), chk_gbn);
            if (rsNum == 0)
            {
                resetSetting();

                chk_list();
                MessageBox.Show("성공적으로 삭제하였습니다.");
            }
            else if (rsNum == 1)
            {
                MessageBox.Show("삭제에 실패하였습니다.");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            chk_list();
        }

        private void txt_chk_ord_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComInfo.onlyNum(sender, e);
        }
    }
}
