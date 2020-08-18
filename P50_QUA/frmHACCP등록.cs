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

namespace 스마트팩토리.P50_QUA
{
    public partial class frmHACCP등록 : Form
    {

        private Popup.frmPrint readyPrt = new Popup.frmPrint();
        private Popup.frmPrint frmPrt;
        private DataTable adoPrt = new DataTable();
        private DataTable adoPrt2 = new DataTable();

        public frmHACCP등록()
        {
            InitializeComponent();        
        }   

        private void frmHACCP등록_Load(object sender, EventArgs e)
        {
            ComInfo.gridHeaderSet(ccpChkGrid);
            ComInfo.gridHeaderSet(dataCcpGrid);

            get_user_nm();
            getHaccpGrid();
            resetSetting();
            input_list(dataCcpGrid, "where A.INPUT_DATE >= '" + start_date.Text.ToString() + "' and  A.INPUT_DATE <= '" + end_date.Text.ToString() + "'");
        }

        private void get_user_nm()
        {
            try
            {
                wnDm wdm = new wnDm();

                txt_user_cd.Text = Common.p_strStaffNo;
                int rs = wdm.getStaffName(txt_user_cd.Text, txt_user_nm);

                if (rs == 1)
                {
                    MessageBox.Show("사용자 검색 에러가 발생했습니다");
                }
                else if (rs == 9)
                {
                    MessageBox.Show("SQL 에러가 발생했습니다");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("시스템 에러가 발생했습니다");
            }
        }

        private void getHaccpGrid()
        {
            try
            {
                wnDm wdm = new wnDm();

                DataTable dt = wdm.getHaccpGrid();

                ccpChkGrid.Rows.Clear();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ccpChkGrid.Rows.Add();
                        ccpChkGrid.Rows[i].Cells["FLOW_CD"].Value = dt.Rows[i]["FLOW_CD"].ToString();
                        ccpChkGrid.Rows[i].Cells["FLOW_NM"].Value = dt.Rows[i]["FLOW_NM"].ToString();
                        ccpChkGrid.Rows[i].Cells["CHK_CD"].Value = dt.Rows[i]["CHK_CD"].ToString();
                        ccpChkGrid.Rows[i].Cells["CHK_NM"].Value = dt.Rows[i]["CHK_NM"].ToString();
                        ccpChkGrid.Rows[i].Cells["CHK_ORD"].Value = dt.Rows[i]["CHK_ORD"].ToString();

                    }
                }
                else
                {
                    MessageBox.Show("검색값이 없습니다. HACCP기준 등록을 먼저 해주세요");
                    return;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("시스템 에러가 발생했습니다");
                MessageBox.Show(e + "");

            }
        }


        private void resetSetting()
        {

            btnDelete.Enabled = false;
            btnSave.Enabled = true;
            lbl_input_gubun.Text = "";
            txt_chk_date.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txt_comment.Text = "";
            txt_input_cd.Text = "";

            getHaccpGrid();


            DataGridViewCheckBoxCell chk  = null;
            for (int i = 0; i < ccpChkGrid.Rows.Count; i++)
            {
                chk = (DataGridViewCheckBoxCell)ccpChkGrid.Rows[i].Cells["YES"];
                chk.Selected = false;
                chk = (DataGridViewCheckBoxCell)ccpChkGrid.Rows[i].Cells["NO"];
                chk.Selected = false;

                ccpChkGrid.Rows[i].Cells["NO"].Value = false;
                ccpChkGrid.Rows[i].Cells["YES"].Value = false;

            }


        }

        private void input_detail(DataGridView dgv, DataGridViewCellEventArgs e)
        {
            btnDelete.Enabled = true;
            txt_chk_date.Enabled = false;
            lbl_input_gubun.Text = "1";

            


            txt_chk_date.Text = dgv.Rows[e.RowIndex].Cells[1].Value.ToString();
            txt_input_cd.Text = dgv.Rows[e.RowIndex].Cells[2].Value.ToString();
            
            txt_comment.Text = dgv.Rows[e.RowIndex].Cells[5].Value.ToString();
            txt_user_cd.Text = dgv.Rows[e.RowIndex].Cells[3].Value.ToString();
            txt_user_nm.Text = dgv.Rows[e.RowIndex].Cells[4].Value.ToString();

            inputDetail2();
            //inputDetail3();

            //in_grid_detail();
        }

        private void inputDetail2()
        {
            wnDm wDm = new wnDm();
            DataTable dt = null;

            dt = wDm.fn_Haccp_Detail_List("where A.INPUT_DATE = '" + txt_chk_date.Text.ToString() + "' and A.INPUT_CD = '" + txt_input_cd.Text.ToString() + "'  ");



            if (dt != null && dt.Rows.Count > 0)
            {
                ccpChkGrid.Rows.Clear();
                ccpChkGrid.RowCount = dt.Rows.Count;
                for (int i = 0; i < ccpChkGrid.Rows.Count; i++)
                {


                    ccpChkGrid.Rows[i].Cells["FLOW_CD"].Value = dt.Rows[i]["FLOW_CD"].ToString();
                    ccpChkGrid.Rows[i].Cells["FLOW_NM"].Value = dt.Rows[i]["FLOW_NM"].ToString();
                    ccpChkGrid.Rows[i].Cells["CHK_CD"].Value = dt.Rows[i]["CHK_CD"].ToString();
                    ccpChkGrid.Rows[i].Cells["CHK_NM"].Value = dt.Rows[i]["CHK_NM"].ToString();
                    ccpChkGrid.Rows[i].Cells["CHK_ORD"].Value = dt.Rows[i]["CHK_ORD"].ToString();



                    if (dt.Rows[i]["CHK_VALUE"].ToString().Equals("Y"))
                    {
                        ccpChkGrid.Rows[i].Cells["YES"].Value = true;
                        ccpChkGrid.Rows[i].Cells["NO"].Value = false;
                    }
                    else
                    {
                        ccpChkGrid.Rows[i].Cells["YES"].Value = false;
                        ccpChkGrid.Rows[i].Cells["NO"].Value = true;
                    }
                }
            }
        }

        private void Haccp_Input(DataGridView ccpChkGrid)
        {
            try
            {

                for (int i = 0; i < ccpChkGrid.Rows.Count; i++)
                {
                    if (ccpChkGrid.Rows[i].Cells["YES"].Value.ToString().Equals("False") && ccpChkGrid.Rows[i].Cells["NO"].Value.ToString().Equals("False"))
                    {
                        MessageBox.Show("모든 체크항목에 체크해주십시오.");
                        return;
                    }
                }



                wnDm wDm = new wnDm();
                int rsNum = 1;
                if (lbl_input_gubun.Text.Equals(""))
                {
                    rsNum = wDm.Insert_Haccp_Input(txt_chk_date.Text,txt_comment.Text,ccpChkGrid);
                }
                else
                {
                    rsNum = wDm.Update_Haccp_Input(
                                      txt_chk_date.Text
                                      , txt_input_cd.Text
                                      , txt_comment.Text
                                      , ccpChkGrid);
                }

                if (rsNum == 0)
                {
                    if (lbl_input_gubun.Text.Equals(""))
                    {
                        resetSetting();
                        input_list(dataCcpGrid, "where A.INPUT_DATE >= '" + start_date.Text.ToString() + "' and  A.INPUT_DATE <= '" + end_date.Text.ToString() + "'");
                        MessageBox.Show("성공적으로 등록하였습니다.");
                    }
                    else
                    {
                        resetSetting();
                        //input_list(tdInputGrid, "where convert(varchar(10), A.INTIME, 120) = convert(varchar(10), getDate(), 120) ");
                        input_list(dataCcpGrid, "where A.INPUT_DATE >= '" + start_date.Text.ToString() + "' and  A.INPUT_DATE <= '" + end_date.Text.ToString() + "'");
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
                dt = wDm.fn_Haccp_input_list(condition);

                if (dt != null && dt.Rows.Count > 0)
                {
                    dgv.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        
                        dgv.Rows[i].Cells[0].Value = (i + 1);
                        dgv.Rows[i].Cells[1].Value = dt.Rows[i]["INPUT_DATE"].ToString();
                        dgv.Rows[i].Cells[2].Value = dt.Rows[i]["INPUT_CD"].ToString();
                        dgv.Rows[i].Cells[3].Value = dt.Rows[i]["STAFF_CD"].ToString();
                        dgv.Rows[i].Cells[4].Value = dt.Rows[i]["STAFF_NM"].ToString();
                        dgv.Rows[i].Cells[5].Value = dt.Rows[i]["COMMENT"].ToString();
                        
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
                



        private void btnNew_Click(object sender, EventArgs e)
        {
            resetSetting();
        }

        private void ccpChkGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            if (ccpChkGrid != null && ccpChkGrid.Rows.Count > 0)
            {
                if (ccpChkGrid.Columns["YES"] != null || ccpChkGrid.Columns["NO"] != null)
                {
                    if (e.ColumnIndex == ccpChkGrid.Columns["YES"].Index && e.RowIndex != -1)
                    {
                      // MessageBox.Show(ccpChkGrid.Rows[e.RowIndex].Cells["YES"].Value.ToString());
                       if (ccpChkGrid.Rows[e.RowIndex].Cells["YES"].Value.ToString().Equals("True"))
                       {
                           //DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)ccpChkGrid.Rows[e.RowIndex].Cells["NO"];
                           //chk.Value = chk.FalseValue;
                           ccpChkGrid.Rows[e.RowIndex].Cells["NO"].Value = false;

                       }
                           
                        
                    }
                    else if (e.ColumnIndex == ccpChkGrid.Columns["NO"].Index && e.RowIndex != -1)
                    {
                        if (ccpChkGrid.Rows[e.RowIndex].Cells["NO"].Value.ToString().Equals("True"))
                        {
                            ccpChkGrid.Rows[e.RowIndex].Cells["YES"].Value = false;
                            //DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)ccpChkGrid.Rows[e.RowIndex].Cells["YES"];
                           // chk.Value = chk.FalseValue;

                        }
                            
                    }
                }
            }
        }

        private void ccpChkGrid_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.ColumnIndex == ccpChkGrid.Columns["YES"].Index && e.RowIndex != -1) || (e.ColumnIndex == ccpChkGrid.Columns["NO"].Index && e.RowIndex != -1))
            {
                ccpChkGrid.EndEdit();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Haccp_Input(ccpChkGrid);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            input_list(dataCcpGrid, "where A.INPUT_DATE >= '" + start_date.Text.ToString() + "' and  A.INPUT_DATE <= '" + end_date.Text.ToString() + "'");
        }


        private void dataCcpGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ComInfo.grdHeaderNoAction(e))
            {
                input_detail(dataCcpGrid, e);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            try
            {
                int rsNum = 2;
                wnDm wdm = new wnDm();
                rsNum = wdm.Delete_Haccp_Input(txt_chk_date.Text, txt_input_cd.Text);

                if (rsNum == 0)
                {

                    resetSetting();
                    input_list(dataCcpGrid, "where A.INPUT_DATE >= '" + start_date.Text.ToString() + "' and  A.INPUT_DATE <= '" + end_date.Text.ToString() + "'");
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


        private void btnPrint_Click(object sender, EventArgs e)
        {
            DialogResult msgOk = MessageBox.Show("HACCP점검표를 발행하시겠습니까?", "발행여부", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (msgOk == DialogResult.No)
            {
                return;
            }
            HACCP점검표출력();
        }



        public void HACCP점검표출력()
        {
            try
            {
                string sValue = "" + txt_chk_date.Text;
                string sValue2 = "" + txt_input_cd.Text;

                if (sValue2 == "")
                {
                    MessageBox.Show("출력할 자료가 없습니다."); 
                    return;
                }
                getOrderMain(sValue, sValue2);

                frmPrt = readyPrt;
                frmPrt.Show();
                frmPrt.BringToFront();
                frmPrt.prt_HACCP점검표(adoPrt, adoPrt2, sValue, sValue2, "HACCP점검표");

            }
            catch (Exception ex)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
            }
        }

        private void getOrderMain(string sKey, string sKey2)
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("where 1=1 ");
                sb.AppendLine(" and A.INPUT_DATE = '" + sKey + "' ");
                sb.AppendLine(" and A.INPUT_CD = '" + sKey2 + "' ");
                dt = wDm.fn_Haccp_input_list(sb.ToString());

                //-- 출력을 위한 테이블 --
                adoPrt = new DataTable();
                adoPrt = dt.Copy();
                //------------------------
                //wConst.get_PComp_Info();
                ////------------------------

                if (dt != null && dt.Rows.Count > 0)
                {
                    //bEditText = false;

                    for (int kk = 0; kk < dt.Rows.Count; kk++)
                    {
                        //dt.Rows[kk]["SEQ"] = (kk + 1); //숫자의 경우  문자면 .string : 계산된 값을 READ 한 테이블로 다시 전달한다 - 출력물 사용

                        adoPrt = dt.Copy();
                    }

                    DataTable dt2 = new DataTable();
                    dt2 = wDm.fn_Haccp_Detail_List(sb.ToString());

                    if (dt2 != null && dt2.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            adoPrt2 = dt2.Copy();
                        }
                    }
                }

                //-- 출력을 위한 테이블 --
                adoPrt = dt.Copy();
                //------------------------
            }
            catch (Exception ex)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
