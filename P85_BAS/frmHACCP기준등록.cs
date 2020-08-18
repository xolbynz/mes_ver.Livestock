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
    public partial class frmHACCP기준등록 : Form
    {

        DataTable adoPrt = null;
        wnAdo wAdo = new wnAdo();
        
        private wnGConstant wConst = new wnGConstant();       

        public string strCondition = "";
       
        private int nPageSize = int.Parse(Common.p_PageSize);

        public frmHACCP기준등록()
        {
            InitializeComponent();
        }

        private void chk_manager_yn_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void dataChkGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            lbl_input_gbn.Text = "1";

            cmb_cd.SelectedValue = dataChkGrid.Rows[e.RowIndex].Cells[5].Value.ToString();
            txt_cd.Text = dataChkGrid.Rows[e.RowIndex].Cells[2].Value.ToString();
            txt_nm.Text = dataChkGrid.Rows[e.RowIndex].Cells[3].Value.ToString();
            if (dataChkGrid.Rows[e.RowIndex].Cells[4].Value.ToString() == "Y")
            {
                chk_yn.Checked = true;
            }
            else
            {
                chk_yn.Checked = false;
            }
      
            
            //txt_nm.Text == dgv.Rows[e.RowIndex].Cells[1].Value.ToString() || txt_cd.Text == dgv.Rows[e.RowIndex].Cells[2].Value.ToString())
            //{
                
            //}  
            
        }       

        private void resetSetting()
        {
            lbl_input_gbn.Text = "";
            cmb_cd.SelectedValue = "";
            txt_nm.Text = "";
            txt_cd.Text = "";
            dataChkGrid.Rows.Clear();
        }
        private void HACCP_logic()
        {
            try
            {
                //if (cmb_cd.SelectedValue == null) cmb_cd.SelectedValue = "";
                string haccp_yn;

                if (cmb_cd.SelectedValue == null)
                {
                    MessageBox.Show("공정코드를 선택하시기 바랍니다.");
                    return;
                }
                if (txt_nm.Text.ToString().Equals(""))
                {
                    MessageBox.Show("검사항목코드를 입력하시기 바랍니다.");
                    return;
                }
                if (txt_cd.Text.ToString().Equals(""))
                {
                    MessageBox.Show("검사항목을 입력하시기 바랍니다.");
                    return;
                }
                if (chk_yn.Checked == true)
                {
                    haccp_yn = "Y";
                }
                else
                {
                    haccp_yn = "N";
                }


                if (lbl_input_gbn.Text != "1")
                {
                    wnDm wDm = new wnDm();

                    int rsNum = wDm.haccp_flow(
                        cmb_cd.SelectedValue.ToString()
                        , txt_nm.Text.ToString()
                        , txt_cd.Text.ToString()
                        , haccp_yn
                        );

                    if (rsNum == 0)
                    {
                        resetSetting();
                        MessageBox.Show("성공적으로 등록하였습니다.");
                    }
                    else if (rsNum == 1)
                        MessageBox.Show("저장에 실패하였습니다");
                    else if (rsNum == 2)
                        MessageBox.Show("SQL COMMAND 에러");                    
                    else
                        MessageBox.Show("Exception 에러");
                }
                else
                {

                    wnDm wDm = new wnDm();

                    int rsNum = wDm.haccp_update(
                        cmb_cd.SelectedValue.ToString()
                        , txt_nm.Text.ToString()
                        , txt_cd.Text.ToString()
                        , haccp_yn
                        );

                    if (rsNum == 0)
                    {
                        dataChkGrid.Rows.Clear(); //기존 삭제 데이터할 제품구성 리스트 초기화
                                                
                        MessageBox.Show("성공적으로 수정하였습니다.");
                    }
                    else if (rsNum == 1)
                        MessageBox.Show("저장에 실패하였습니다");
                    else
                        MessageBox.Show("Exception 에러");
                }
            }



            catch (Exception e)
            {
                MessageBox.Show("시스템 에러: " + e.Message.ToString());
            }
        }
       
        private void HACCP_list()
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("where 1=1 ");
                
                if (!txt_srch.Text.ToString().Equals(""))
                {
                    sb.AppendLine("and CHK_NM like '%" + txt_srch.Text.ToString() + "%' ");
                }
                else if (cmb_cd2.SelectedValue != "전체" && cmb_cd2.SelectedValue != "")
                {
                    sb.AppendLine("and FLOW_CD = '" + cmb_cd2.SelectedValue + "' ");
                }

                dt = wDm.haccp_Grid_List(sb.ToString());

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.dataChkGrid.RowCount = dt.Rows.Count;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dataChkGrid.Rows[i].Cells[0].Value = dt.Rows[i]["CHK_ORD"].ToString();
                        ComInfo comInfo = new ComInfo();
                        string sqlQuery = "";

                        //cmb.ValueMember = "코드";
                        //cmb.DisplayMember = "명칭";
                        //sqlQuery = comInfo.queryFlow();
                        //wConst.ComboBox_Read_Blank(cmb, sqlQuery);

                        dataChkGrid.Rows[i].Cells[1].Value = dt.Rows[i]["FLOW_NM"].ToString();
                        dataChkGrid.Rows[i].Cells[2].Value = dt.Rows[i]["CHK_CD"].ToString();
                        dataChkGrid.Rows[i].Cells[3].Value = dt.Rows[i]["CHK_NM"].ToString();
                        dataChkGrid.Rows[i].Cells[5].Value = dt.Rows[i]["FLOW_CD"].ToString();
                        if (dt.Rows[i]["USE_YN"].ToString().Equals("Y"))
                        {
                            dataChkGrid.Rows[i].Cells[4].Value = "Y";
                        }
                        else
                        {
                            dataChkGrid.Rows[i].Cells[4].Value = "N";
                        }
                    }
                }
                else
                {
                    dataChkGrid.Rows.Clear();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("시스템 에러: " + e.Message.ToString());
            }        
        }       

        private void init_ComboBox()
        {
            ComInfo comInfo = new ComInfo();
            string sqlQuery = "";

            cmb_cd.ValueMember = "코드";
            cmb_cd.DisplayMember = "명칭";
            sqlQuery = comInfo.queryFlow();
            wConst.ComboBox_Read_Blank(cmb_cd, sqlQuery);

            cmb_cd2.ValueMember = "코드";
            cmb_cd2.DisplayMember = "명칭";
            sqlQuery = comInfo.queryFlow();
            wConst.ComboBox_Read_ALL(cmb_cd2, sqlQuery);                
           
        }

        private void chk_yn_CheckedChanged(object sender, EventArgs e)
        {
            wnDm wDm = new wnDm();
            StringBuilder sb = new StringBuilder();

            if (chk_yn.Checked == true)
            {
                sb.AppendLine("Y");
            }
            else
            {
                sb.AppendLine("N");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            HACCP_logic();
        }

        private void frmHACCP기준등록_Load(object sender, EventArgs e)
        {
            ComInfo.gridHeaderSet(dataChkGrid);

            HACCP_list();
            init_ComboBox();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            resetSetting();
        }       
        private void btnSearch_Click(object sender, EventArgs e)
        {
            HACCP_list();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }               
    }
}
