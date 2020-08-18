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
    public partial class frm원산지등록 : Form
    {
        private wnGConstant wConst = new wnGConstant();
        private bool bData = false;

        public frm원산지등록()
        {
            InitializeComponent();
        }

        private void frm원산지등록_Load(object sender, EventArgs e)
        {
            init_ComboBox();
            item_list();
        }

        private void init_ComboBox()
        {
            ComInfo comInfo = new ComInfo();
            string sqlQuery = "";

            cmb_used_srch.ValueMember = "코드";
            cmb_used_srch.DisplayMember = "명칭";
            txt_used_yn.ValueMember = "코드";
            txt_used_yn.DisplayMember = "명칭";
            sqlQuery = comInfo.queryUsedYn(); //사용여부검색
            wConst.ComboBox_Read_ALL(cmb_used_srch, sqlQuery);
            wConst.ComboBox_Read_NoBlank(txt_used_yn, sqlQuery);
        }

        private void resetSetting()
        {
            lbl_item_gbn.Text = "";
            txt_country_nm.Text = "";
            txt_country_cd.Text = "";
            txt_used_yn.Text = "사용";
            txt_country_cmt.Text = "";

        }



        private void item_insert()
        {
            try
            {

                if (txt_country_cd.Text.ToString().Equals(""))
                {
                    MessageBox.Show("원산지코드를 입력하시기 바랍니다.");
                    return;
                }
                if (txt_country_nm.Text.ToString().Equals(""))
                {
                    MessageBox.Show("원산지명을 입력하시기 바랍니다.");
                    return;
                }
                if (txt_country_cmt.Text == null)
                {
                    txt_country_cmt.Text = "";
                }

                wnDm wDm = new wnDm();
                int rsNum = 1;
                if (lbl_item_gbn.Text.Equals(""))
                {
                    rsNum = wDm.insertCountryCode(
                                      txt_country_cd.Text
                                    , txt_country_nm.Text
                                    , txt_country_cmt.Text
                                    , txt_used_yn.SelectedValue.ToString()
                                    );
                }
                else
                {
                    rsNum = wDm.UpdateCountryCode(
                                      txt_country_cd.Text
                                    , txt_country_nm.Text
                                    , txt_country_cmt.Text
                                    , txt_used_yn.SelectedValue.ToString()
                                    );
                }

                if (rsNum == 0)
                {
                    

                    if (lbl_item_gbn.Text.Equals(""))
                    {
                        MessageBox.Show("성공적으로 등록하였습니다.");
                    }
                    else
                    {
                        MessageBox.Show("성공적으로 수정하였습니다.");
                    }
                    resetSetting();
                    item_list();
                }
                else if (rsNum == 1)
                    MessageBox.Show("저장에 실패하였습니다");
                else if (rsNum == 2)
                    MessageBox.Show("SQL COMMAND 에러");
                else if (rsNum == 3)
                    MessageBox.Show("기존 코드가 있으니 \n 다른 코드로 입력 바랍니다.");
                else
                    MessageBox.Show("Exception 에러");
            }



            catch (Exception e)
            {
                MessageBox.Show("시스템 에러: " + e.Message.ToString());
            }
        }

        private void item_Delete()
        {
            try
            {

                wnDm wDm = new wnDm();

                int rsNum = wDm.DeleteCountryCode(
                                      txt_country_cd.Text
                                    );        

               
                if (rsNum == 0)
                {
                    resetSetting();
                    item_list();
                    MessageBox.Show("성공적으로 삭제하였습니다.");
 
                }
                else if (rsNum == 1)
                    MessageBox.Show("삭제에 실패하였습니다");
                else if (rsNum == 2)
                    MessageBox.Show("SQL COMMAND 에러");
                else if (rsNum == 3)
                    MessageBox.Show("기존 코드가 있으니 \n 다른 코드로 입력 바랍니다.");
                else
                    MessageBox.Show("Exception 에러");
            }



            catch (Exception e)
            {
                MessageBox.Show("시스템 에러: " + e.Message.ToString());
            }
        }


        private void item_list()
        {
            try
            {

                wnDm wDm = new wnDm();
                DataTable dt = null;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("where 1=1  ");


                if (!txt_srch.Text.ToString().Equals(""))
                {
                    sb.AppendLine("and COUNTRY_NM like '%" + txt_srch.Text.ToString() + "%' ");
                }

                if (cmb_used_srch.SelectedIndex == 1)
                {
                    sb.AppendLine(" and USED_CD = 1 ");
                }
                else if (cmb_used_srch.SelectedIndex == 2)
                {
                    sb.AppendLine(" and USED_CD = 2 ");
                }
                else if (cmb_used_srch.SelectedIndex == 3)
                {
                    sb.AppendLine(" and USED_CD = 3 ");
                }


                dt = wDm.fn_Country_List(sb.ToString());

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.dataCountryGrid.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        if (dt.Rows[i]["USED_CD"].ToString().Equals("2"))
                        {
                            dataCountryGrid.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                        }
                        else if (dt.Rows[i]["USED_CD"].ToString().Equals("3"))
                        {
                            dataCountryGrid.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                        else if (dt.Rows[i]["USED_CD"].ToString().Equals("1"))
                        {
                            dataCountryGrid.Rows[i].DefaultCellStyle.BackColor = Color.Empty;
                        }
                        dataCountryGrid.Rows[i].Cells[0].Value = dt.Rows[i]["COUNTRY_CD"].ToString();
                        dataCountryGrid.Rows[i].Cells[1].Value = dt.Rows[i]["COUNTRY_NM"].ToString();
                        dataCountryGrid.Rows[i].Cells[2].Value = dt.Rows[i]["COMMENT"].ToString();
                        

                    }
                }
                else
                {
                    dataCountryGrid.Rows.Clear();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("시스템 에러: " + e.Message.ToString());
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            item_list();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            item_insert();
        }
        private void dataCountryGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            country_detail_logic(dataCountryGrid, e);
        }
        private void country_detail_logic(DataGridView dg, DataGridViewCellEventArgs e)
        {
            btnDelete.Enabled = true;
            lbl_item_gbn.Text = "1";
            txt_country_cd.Enabled = false;

            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                string condition = "where country_cd = '" + dg.Rows[e.RowIndex].Cells[0].Value.ToString() + "'";
                dt = wDm.fn_Country_List(condition);
                
                if (dt != null && dt.Rows.Count > 0)
                {
                    
                    txt_country_cd.Text = dt.Rows[0]["COUNTRY_CD"].ToString();
                    txt_country_nm.Text = dt.Rows[0]["COUNTRY_NM"].ToString();
                    txt_country_cmt.Text = dt.Rows[0]["COMMENT"].ToString();
                    txt_used_yn.SelectedValue = dt.Rows[0]["USED_CD"].ToString();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("시스템 오류가 발생했습니다");
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            resetSetting();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            item_Delete();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        

    }
}
