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

namespace 스마트팩토리.P20_ORD
{
    public partial class frm지육입고및공정 : Form
    {

        private wnGConstant wConst = new wnGConstant();
        private ComInfo comInfo = new ComInfo();


        public frm지육입고및공정()
        {
            InitializeComponent();
        }

        

        

        private void frm지육입고및공정_Load(object sender, EventArgs e)
        {
            lbl_input_gbn.Text = "";
            input_list(tdInputGrid, "where convert(varchar(10), A.INTIME, 120) = convert(varchar(10), getDate(), 120) ");
            init_ComboBox();

            ComInfo.gridHeaderSet(tdInputGrid);
            ComInfo.gridHeaderSet(inputGrid);
            ComInfo.gridHeaderSet(sourceRawGrid);

            
        }


        private void init_ComboBox()
        {
            ComInfo comInfo = new ComInfo();
            string sqlQuery = "";

            cmb_meatkind.ValueMember = "코드";
            cmb_meatkind.DisplayMember = "명칭";
            sqlQuery = comInfo.queryMeatKind(); 
            wConst.ComboBox_Read_Blank(cmb_meatkind, sqlQuery);

            cmb_grade_gubun.ValueMember = "코드";
            cmb_grade_gubun.DisplayMember = "명칭";
            sqlQuery = comInfo.queryGradeGubun(); 
            wConst.ComboBox_Read_NoBlank(cmb_grade_gubun, sqlQuery);

            cmb_fgubun.ValueMember = "코드";
            cmb_fgubun.DisplayMember = "명칭";
            sqlQuery = comInfo.queryFrozenGubun();
            wConst.ComboBox_Read_NoBlank(cmb_fgubun, sqlQuery);

            cmb_corigin.ValueMember = "코드";
            cmb_corigin.DisplayMember = "명칭";
            sqlQuery = comInfo.queryOriginGubun();
            wConst.ComboBox_Read_NoBlank(cmb_corigin, sqlQuery);

        }

        private void init_MeatGrid(DataGridView meatRawGrid)
        {
            meatRawGrid.Rows.Clear();
            meatRawGrid.Rows.Add();
            meatRawGrid.Rows.Add();
            meatRawGrid.Rows.Add();
            meatRawGrid.Rows[0].Cells[1].Value = "1등급 이상";
            meatRawGrid.Rows[1].Cells[1].Value = "2등급";
            meatRawGrid.Rows[2].Cells[1].Value = "등 외 등급";

            for (int i = 0; i < 3; i++)
            {
                meatRawGrid.Rows[i].Cells[2].Value = 0;
                meatRawGrid.Rows[i].Cells[3].Value = 0;
                meatRawGrid.Rows[i].Cells[4].Value = 0;
                meatRawGrid.Rows[i].Cells[5].Value = "";
                meatRawGrid.Rows[i].Cells[6].Value = false;


            }



        }

        private void resetSetting()
        {
            lbl_input_gbn.Text = "";
            btnDelete.Enabled = false;
            chk_input_yn.Enabled = true;
            btnSave.Enabled = true;
            
           

            txt_input_date.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txt_input_date.Enabled = true;
            cmb_meatkind.Text = "";
            cmb_corigin.Text = "국산";
            cmb_fgubun.Text = "냉동";
            cmb_grade_gubun.Text = "1등급";
            txt_union_cd.Text = "";
            txt_mf_date.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txt_input_cd.Text = "";
            txt_cust_cd.Text = "";
            txt_cust_nm.Text = "";
            txt_meat_amount.Text = "";
            txt_meat_price.Text = "";
            txt_meat_weight.Text = "";
            
            chk_input_yn.Checked = false;
            txt_comment.Text = "";

        }

        private void Meat_Input()
        {
            try
            {

                if (cmb_meatkind.SelectedValue == null || cmb_meatkind.SelectedValue.Equals(""))
                {
                    MessageBox.Show("지육을 선택하여 주십시오.");
                    return;
                }
                if (txt_meat_amount.Text == null || txt_meat_amount.Text.ToString().Equals("") || int.Parse(txt_meat_amount.Text.ToString()) < 1)
                {
                    MessageBox.Show("입고 수량을 1두 이상 입력하여 주십시오.");
                    return;
                }
                if (txt_meat_weight.Text == null || txt_meat_weight.Text.ToString().Equals("") || double.Parse(txt_meat_weight.Text.ToString()) < 0)
                {
                    MessageBox.Show("총 중량을 입력하여 주십시오.");
                    return;
                }
                if (txt_meat_weight.Text == null || txt_meat_weight.Text.ToString().Equals("") || double.Parse(txt_meat_weight.Text.ToString()) < 0)
                {
                    MessageBox.Show("총 중량을 입력하여 주십시오.");
                    return;
                }
                if (txt_meat_price.Text == null || txt_meat_price.Text.ToString().Equals("") || int.Parse(txt_meat_price.Text.ToString()) < 0)
                {
                    MessageBox.Show("매입가를 입력하여 주십시오.");
                    return;
                }
                if (txt_comment.Text == null)
                {
                    txt_comment.Text = "";
                }
                if (txt_cust_cd.Text == null || txt_cust_cd.Text.ToString().Equals(""))
                {
                    MessageBox.Show("구매처를 입력해 주십시오.");
                    return;
                }

                if (txt_slauhouse_cd.Text == null || txt_slauhouse_cd.Text.ToString().Equals(""))
                {
                    MessageBox.Show("도축장을 입력해 주십시오.");
                    return;
                }

                if (chk_input_yn.Checked == true)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("※ 공정 완료 ※");
                    sb.AppendLine("이미 공정이 완료되어 수정할 수 없습니다.");
                    
                    ComInfo comInfo = new ComInfo();
                    MessageBox.Show(sb.ToString());
                }

                Console.WriteLine("combo : " + cmb_meatkind.SelectedValue);
                Console.WriteLine("cmb_text: "+ cmb_meatkind.GetItemText(cmb_meatkind.SelectedItem).ToString());
                Console.WriteLine("amount : " + txt_meat_amount.Text.ToString());
                Console.WriteLine("weight : " + txt_meat_weight.Text.ToString());
                Console.WriteLine("price : " + txt_meat_price.Text.ToString());
                Console.WriteLine("date : " + txt_input_date.Text.ToString());
                Console.WriteLine("comment : " + txt_comment.Text.ToString());
                Console.WriteLine("cust : " + txt_cust_nm.Text.ToString());
                Console.WriteLine("chk : " + chk_input_yn.Checked.ToString());


                wnDm wDm = new wnDm();
                int rsNum = 1;
                if (lbl_input_gbn.Text.Equals(""))
                {
                    rsNum = wDm.insert_Meat_Input(
                                      cmb_meatkind.SelectedValue.ToString()
                                    , cmb_meatkind.GetItemText(cmb_meatkind.SelectedItem).ToString()
                                    , txt_meat_amount.Text.ToString()
                                    , txt_meat_weight.Text.ToString()
                                    , txt_meat_price.Text.ToString().Replace(",","")
                                    , txt_input_date.Text.ToString()
                                    , txt_comment.Text.ToString()
                                    , txt_cust_cd.Text.ToString()
                                    , chk_input_yn.Checked.ToString().Equals("True") ? "1" : "0"
                                    , txt_mf_date.Text.ToString()
                                    , txt_union_cd.Text.ToString()
                                    , cmb_grade_gubun.SelectedValue.ToString()
                                    , cmb_fgubun.SelectedValue.ToString()
                                    , txt_slauhouse_cd.Text.ToString()
                                    , sourceRawGrid
                                    );
                }
                else
                {//2019-10-31 이재원 도입기업의 요구에 따라 테이블 구조를 수정하였음
                    // 그에 따른 로직 변경
                    rsNum = wDm.Update_Meat_Input(
                                      cmb_meatkind.SelectedValue.ToString()
                                    , cmb_meatkind.GetItemText(cmb_meatkind.SelectedItem).ToString()
                                    , txt_meat_amount.Text.ToString()
                                    , txt_meat_weight.Text.ToString()
                                    , txt_meat_price.Text.ToString().Replace(",","")
                                    , txt_input_date.Text.ToString()
                                    , txt_input_cd.Text.ToString()
                                    , txt_comment.Text.ToString()
                                    , txt_cust_cd.Text.ToString()
                                    , chk_input_yn.Checked.ToString().Equals("True") ? "1" : "0"
                                    , txt_mf_date.Text.ToString()
                                    , txt_union_cd.Text.ToString()
                                    , cmb_grade_gubun.SelectedValue.ToString()
                                    , cmb_fgubun.SelectedValue.ToString()
                                    , txt_slauhouse_cd.Text.ToString()
                                    , sourceRawGrid
                                    );
                }

                if (rsNum == 0)
                {
                    if(lbl_input_gbn.Text.Equals("")){
                        resetSetting();
                        input_list(tdInputGrid, "where convert(varchar(10), A.INTIME, 120) = convert(varchar(10), getDate(), 120) ");
                        MessageBox.Show("성공적으로 등록하였습니다.");
                    }
                    else{
                        resetSetting();
                        input_list(tdInputGrid, "where convert(varchar(10), A.INTIME, 120) = convert(varchar(10), getDate(), 120) ");
                        input_list(inputGrid, "where A.INPUT_DATE >= '" + start_date.Text.ToString() + "' and  A.INPUT_DATE <= '" + end_date.Text.ToString() + "'");
                        MessageBox.Show("성공적으로 수정하였습니다.");
                    }
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




        private void input_list(DataGridView dgv, string condition)
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_Meat_Input_List(condition);

                if (dt != null && dt.Rows.Count > 0)
                {
                    dgv.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //dgv.Rows[i].Cells["INPUT_DATE"].Value = dt.Rows[i]["INPUT_DATE"].ToString();
                        //dgv.Rows[i].Cells["INPUT_CD"].Value = dt.Rows[i]["INPUT_CD"].ToString();
                        //dgv.Rows[i].Cells["CUST_CD"].Value = dt.Rows[i]["CUST_CD"].ToString();
                        //dgv.Rows[i].Cells["CUST_NM"].Value = dt.Rows[i]["CUST_NM"].ToString();
                        //dgv.Rows[i].Cells["RAW_MAT_CNT"].Value = dt.Rows[i]["RAW_MAT_CNT"].ToString();
                        //dgv.Rows[i].Cells["COMPLETE_YN"].Value = dt.Rows[i]["COMPLETE_YN"].ToString();

                        dgv.Rows[i].Cells[0].Value = dt.Rows[i]["INPUT_DATE"].ToString();
                        dgv.Rows[i].Cells[1].Value = dt.Rows[i]["INPUT_CD"].ToString();
                        dgv.Rows[i].Cells[2].Value = dt.Rows[i]["CUST_NM"].ToString();
                        dgv.Rows[i].Cells[3].Value = (int)double.Parse(dt.Rows[i]["RAW_MAT_WEIGHT"].ToString());
                        dgv.Rows[i].Cells[4].Value = dt.Rows[i]["OUTPUT_YN"].ToString().Equals("1") ? "Y" : "N";
                        dgv.Rows[i].Cells[5].Value = dt.Rows[i]["CUST_CD"].ToString();
                        dgv.Rows[i].Cells[6].Value = dt.Rows[i]["COMMENT"].ToString();
                        dgv.Rows[i].Cells[7].Value = dt.Rows[i]["RAW_MAT_NM"].ToString();
                        dgv.Rows[i].Cells[8].Value = dt.Rows[i]["RAW_MAT_AMOUNT"].ToString();
                        dgv.Rows[i].Cells[10].Value = dt.Rows[i]["INPUT_PRICE"].ToString();
                        dgv.Rows[i].Cells[11].Value = dt.Rows[i]["MF_DATE"].ToString();
                        dgv.Rows[i].Cells[12].Value = dt.Rows[i]["SLAUHOUSE_CD"].ToString();
                        dgv.Rows[i].Cells[13].Value = dt.Rows[i]["FROZEN_GUBUN"].ToString();
                        dgv.Rows[i].Cells[14].Value = dt.Rows[i]["EXPRT_DATE"].ToString();
                        dgv.Rows[i].Cells[15].Value = dt.Rows[i]["GRADE"].ToString();
                        dgv.Rows[i].Cells[16].Value = dt.Rows[i]["UNION_CD"].ToString();
                        dgv.Rows[i].Cells[17].Value = dt.Rows[i]["SLAUHOUSE_NM"].ToString();

                    }
                }
                else
                {
                    dgv.Rows.Clear();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("시스템 오류" + e.ToString());
            }
        }
                
            
        



        private void meat_Source_print(String condition)
        {
            try
            {

                wnDm wDm = new wnDm();
                DataTable dt = null;
                sourceRawGrid.Rows.Clear();

                dt = wDm.fn_Raw_Meat_List(condition);
                if (dt != null && dt.Rows.Count > 0)
                {
                    
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            
                            sourceRawGrid.Rows.Add();
                            sourceRawGrid.Rows[i].Cells["RAW_NM"].Value = dt.Rows[i]["RAW_MAT_NM"].ToString();
                            sourceRawGrid.Rows[i].Cells["RAW_CD"].Value = dt.Rows[i]["RAW_MAT_CD"].ToString();
                            sourceRawGrid.Rows[i].Cells["Chk_YN"].Value = dt.Rows[i]["CHECK_GUBUN"].ToString() == "1" ? "검사" : "생략";
                            for (int j = 1; j <= 3; j++)
                            {
                                sourceRawGrid.Rows[i].Cells["g"+j+"weight"].Value = 0;
                                sourceRawGrid.Rows[i].Cells["g" + j + "price"].Value = 0;
                            }
                        }
                }
                else
                {
                    sourceRawGrid.Rows.Clear();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("시스템 에러: " + e.Message.ToString());
            }
        }


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                input_list(tdInputGrid, "where convert(varchar(10), A.INTIME, 120) = convert(varchar(10), getDate(), 120) ");
            }
            else
            {
                start_date.Text = DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd");
                input_list(inputGrid, "where A.INPUT_DATE >= '" + start_date.Text.ToString() + "' and  A.INPUT_DATE <= '" + end_date.Text.ToString() + "'");
            }
        }


        private void cmb_meatkind_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmbTemp = (ComboBox)sender;
            if (cmbTemp.SelectedValue == null || cmbTemp.SelectedValue.Equals(""))
            {
                //meatRawGrid.Rows.Clear();
                sourceRawGrid.Rows.Clear();
                return;
            }

            string condition = "where RAW_SOURCE_CD = '" + cmbTemp.SelectedValue + "' ";
            meat_Source_print(condition);
            //init_MeatGrid(meatRawGrid);

        }

        private void btnCustSrch_Click(object sender, EventArgs e)
        {
            Popup.pop거래처검색 frm = new Popup.pop거래처검색();

            frm.sCustGbn = "2";
            frm.sCustNm = txt_cust_nm.Text.ToString();
            frm.ShowDialog();

            if (frm.sCode != "")
            {
                txt_cust_cd.Text = frm.sCode.Trim();
                txt_cust_nm.Text = frm.sName.Trim();
                //old_cust_nm = frm.sCode.Trim();
                //ni_detail();
                //in_grid_detail();
            }
            

            frm.Dispose();
            frm = null;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Meat_Input();
        }
        
        
        private void btnSrch_Click(object sender, EventArgs e)
        {
            input_list(inputGrid, "where A.INPUT_DATE >= '" + start_date.Text.ToString() + "' and  A.INPUT_DATE <= '" + end_date.Text.ToString() + "'");
        }


        private void inputGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ComInfo.grdHeaderNoAction(e))
            {
                input_detail(inputGrid, e);
            }
        }

        private void tdInputGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ComInfo.grdHeaderNoAction(e))
            {
                input_detail(tdInputGrid, e);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            resetSetting();
        }

        private void input_detail(DataGridView dgv, DataGridViewCellEventArgs e)
        {
            btnDelete.Enabled = true;
            txt_input_date.Enabled = false;
            lbl_input_gbn.Text = "1";

            

            txt_input_date.Text = dgv.Rows[e.RowIndex].Cells[0].Value.ToString();
            txt_input_cd.Text = dgv.Rows[e.RowIndex].Cells[1].Value.ToString();
            txt_cust_cd.Text = dgv.Rows[e.RowIndex].Cells[5].Value.ToString();
            txt_cust_nm.Text = dgv.Rows[e.RowIndex].Cells[2].Value.ToString();
            txt_meat_weight.Text = dgv.Rows[e.RowIndex].Cells[3].Value.ToString();
            txt_comment.Text = dgv.Rows[e.RowIndex].Cells[6].Value.ToString();
            cmb_meatkind.Text = dgv.Rows[e.RowIndex].Cells[7].Value.ToString();
            txt_meat_amount.Text = dgv.Rows[e.RowIndex].Cells[8].Value.ToString();
            txt_meat_price.Text = (decimal.Parse(dgv.Rows[e.RowIndex].Cells[10].Value.ToString())).ToString("0.######"); ;
            

            txt_mf_date.Text = dgv.Rows[e.RowIndex].Cells[11].Value.ToString();
            txt_slauhouse_cd.Text = dgv.Rows[e.RowIndex].Cells[12].Value.ToString();
            cmb_fgubun.Text = dgv.Rows[e.RowIndex].Cells[13].Value.ToString();
            cmb_grade_gubun.Text = dgv.Rows[e.RowIndex].Cells[15].Value.ToString();
            txt_union_cd.Text = dgv.Rows[e.RowIndex].Cells[16].Value.ToString();
            txt_slauhouse_nm.Text = dgv.Rows[e.RowIndex].Cells[17].Value.ToString();
            




            if (dgv.Rows[e.RowIndex].Cells[4].Value.ToString().Equals("Y"))
            {
                chk_input_yn.Checked = true;
                
                btnSave.Enabled = false;
                btnDelete.Enabled = false;
            }
            else
            {
                chk_input_yn.Checked = false;
                btnDelete.Enabled = true;
                btnSave.Enabled = true;
                
            }
            //2019-10-31 이재원 메소드 명칭을 수정하였음
            inputDetail_SourceGrid();
            
            //in_grid_detail();
        }


        


        private void inputDetail_SourceGrid()
        {
            wnDm wDm = new wnDm();
            DataTable dt = null;

            dt = wDm.fn_Meat_Detail_List("where A.INPUT_DATE = '" + txt_input_date.Text.ToString() + "' and A.INPUT_CD = '" + txt_input_cd.Text.ToString() + "'  ");



            if (dt != null && dt.Rows.Count > 0)
            {
                int rowNum = 0;
                int dtRowNum = 0;


                for (int i = 0; i < sourceRawGrid.Rows.Count; i++)
                {
                    sourceRawGrid.Rows[i].Cells["Source_weight"].Value = (decimal.Parse(dt.Rows[dtRowNum]["RAW_MAT_WEIGHT"].ToString())).ToString("0.######");
                    sourceRawGrid.Rows[i].Cells["Source_Price"].Value = (decimal.Parse(dt.Rows[dtRowNum++]["INPUT_PRICE"].ToString())).ToString("#,0.######");
                }
            }
        }


        private void sourceRawGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < sourceRawGrid.RowCount; i++)
            {

                if (sourceRawGrid.Rows[i].Cells["Source_Price"].Value == null || sourceRawGrid.Rows[i].Cells["Source_Price"].Value.ToString().Equals(""))
                    {
                        sourceRawGrid.Rows[i].Cells["Source_Price"].Value = 0;
                    }
                    if (sourceRawGrid.Rows[i].Cells["Source_Weight"].Value == null || sourceRawGrid.Rows[i].Cells["Source_Weight"].Value.ToString().Equals(""))
                    {
                        sourceRawGrid.Rows[i].Cells["Source_Weight"].Value = 0;
                    }
                
                
                
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            try
            {
                int rsNum = 2;
                wnDm wdm = new wnDm();
                rsNum = wdm.Delete_Meat_Input(txt_input_date.Text, txt_input_cd.Text);
            
            if (rsNum == 0)
                {
                    
                    resetSetting();
                    input_list(tdInputGrid, "where convert(varchar(10), A.INTIME, 120) = convert(varchar(10), getDate(), 120) ");
                    input_list(inputGrid, "where A.INPUT_DATE >= '" + start_date.Text.ToString() + "' and  A.INPUT_DATE <= '" + end_date.Text.ToString() + "'");
                    MessageBox.Show("성공적으로 삭제하였습니다.");
                   
                }
                else if (rsNum == 1)
                    MessageBox.Show("삭제에 실패하였습니다");
                else
                    MessageBox.Show("Exception 에러");
            }
                
            catch (Exception e2)
            {
                MessageBox.Show("시스템 에러: " + e2.Message.ToString());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSlauHouseSrch_Click(object sender, EventArgs e)
        {
            Popup.pop도축장검색 frm = new Popup.pop도축장검색();


            frm.sSlauHouseNm = txt_slauhouse_nm.Text.ToString();
            frm.ShowDialog();

            if (frm.sCode != "")
            {
                txt_slauhouse_cd.Text = frm.sCode.Trim();
                txt_slauhouse_nm.Text = frm.sName.Trim();
            }

            frm.Dispose();
            frm = null;
        }

        private void txt_cust_nm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Popup.pop거래처검색 frm = new Popup.pop거래처검색();

                frm.sCustGbn = "2";
                frm.sCustNm = txt_cust_nm.Text.ToString();
                frm.ShowDialog();

                if (frm.sCode != "")
                {
                    txt_cust_cd.Text = frm.sCode.Trim();
                    txt_cust_nm.Text = frm.sName.Trim();
                    //old_cust_nm = frm.sCode.Trim();
                    //ni_detail();
                    //in_grid_detail();
                }


                frm.Dispose();
                frm = null;
            }
        }

        private void txt_slauhouse_nm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Popup.pop도축장검색 frm = new Popup.pop도축장검색();


                frm.sSlauHouseNm = txt_slauhouse_nm.Text.ToString();
                frm.ShowDialog();

                if (frm.sCode != "")
                {
                    txt_slauhouse_cd.Text = frm.sCode.Trim();
                    txt_slauhouse_nm.Text = frm.sName.Trim();
                }

                frm.Dispose();
                frm = null;
            }
        }



    }
}
