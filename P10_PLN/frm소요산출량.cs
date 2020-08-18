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

namespace 스마트팩토리.P10_PLN
{
    public partial class frm소요산출량 : Form
    {
        private int cust_max_num = 0;
        private DataGridView chk_planGrid = new DataGridView();

        private string saveStr = "";
        public frm소요산출량()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm소요산출량_Load(object sender, EventArgs e)
        {
            start_date.Text = DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd");

            planGrid.Columns["CHK"].ReadOnly = false;

            grdCellSetting();
        }

        #region button logic

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (rs_order_grid.Rows.Count > 0)
                {
                    int chk = 0;
                    //체크박스 재 정의 
                    for (int i = 0; i < rs_order_grid.Rows.Count; i++)
                    {
                        if ((bool)rs_order_grid.Rows[i].Cells[0].Value == true)
                        {
                            chk++;
                        }
                    }

                    //if (chk < rs_order_grid.Rows.Count)
                    //{
                    //    //재 정의
                    //    for (int i = 0; i < rs_order_grid.Rows.Count; i++)
                    //    {
                    //        if ((bool)rs_order_grid.Rows[i].Cells[0].Value == true)
                    //        {
                    //            if (i == 0)
                    //            {
                    //                cust_max_num = int.Parse(rs_order_grid.Rows[i].Cells["CUST_NUM"].Value.ToString());
                    //            }
                    //            else
                    //            {
                    //                if (cust_max_num < int.Parse(rs_order_grid.Rows[i].Cells["CUST_NUM"].Value.ToString()))
                    //                {
                    //                    cust_max_num = int.Parse(rs_order_grid.Rows[i].Cells["CUST_NUM"].Value.ToString());
                    //                }
                    //            }
                    //        }
                    //    }
                    //}

                    if (chk < rs_order_grid.Rows.Count) 
                    {
                        StringBuilder sb2 = new StringBuilder();
                        sb2.AppendLine(saveStr);
                        if (chk > 0)  // 하나 이상 체크될 경우
                        {
                            for (int i = 0; i < rs_order_grid.Rows.Count; i++)
                            {
                                if ((bool)rs_order_grid.Rows[i].Cells[0].Value == false)
                                {
                                    sb2.AppendLine(" and  C.RAW_MAT_CD != '" + rs_order_grid.Rows[i].Cells["RAW_MAT_CD"].Value.ToString() + "' ");
                                }
                            }
                            soyoLogic(sb2);
                        }
                    }

                    if (cust_max_num > 1)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine(cust_max_num + "개의 거래처가 있어 발주서가 나눠서 저장됩니다.");
                        sb.AppendLine(" 저장하시겠습니까?");
                        DialogResult msgOk = MessageBox.Show(sb.ToString(), "저장여부", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (msgOk == DialogResult.No)
                        {
                            return;
                        }
                    }

                    wnDm wDm = new wnDm();
                    int rsNum = wDm.insertSoyo(
                        rs_order_grid
                        , chk_planGrid
                        , cust_max_num);

                    if (rsNum == 0)
                    {
                        plan_list(planGrid, "where A.PLAN_DATE >= '" + start_date.Text.ToString() + "' and  A.PLAN_DATE <= '" + end_date.Text.ToString() + "' and ORDER_YN = 'N'");
                        chk_planGrid.Rows.Clear();
                        rs_order_grid.Rows.Clear();
                        itemPlanGrid.Rows.Clear();
                        MessageBox.Show("성공적으로 등록하였습니다.");
                    }
                    else if (rsNum == 1)
                        MessageBox.Show("저장에 실패하였습니다");
                    else if (rsNum == 2)
                        MessageBox.Show("SQL COMMAND 에러");
                    else
                        MessageBox.Show("Exception 에러1");
                }
                else
                {
                    MessageBox.Show("데이터가 없습니다.");
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("시스템 오류: " + ex.ToString());
            }
        }

        private void btn_rs_srch_Click(object sender, EventArgs e)
        {
            planDetail2();
        }
        #endregion button logic

        private void btnSrch_Click(object sender, EventArgs e)
        {
            if (rs_order_grid.Rows.Count > 0)
            {
                DialogResult msgOk = MessageBox.Show("재검색시 기존 데이터 조회는 없어집니다. \n 그래도 검색 진행하시겠습니까?", "확인여부", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (msgOk == DialogResult.No)
                {
                    return;
                }
            }

            plan_list(planGrid, "where A.PLAN_DATE >= '" + start_date.Text.ToString() + "' and  A.PLAN_DATE <= '" + end_date.Text.ToString() + "' and ORDER_YN = 'N'");
        }

        private void plan_list(DataGridView dgv, string condition)
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_Plan_List(condition);

                rs_order_grid.Rows.Clear();
                itemPlanGrid.Rows.Clear();

                lbl_plan_cnt.Text = dt.Rows.Count.ToString();

                if (dt != null && dt.Rows.Count > 0)
                {
                    dgv.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dgv.Rows[i].Cells[0].Value = (i + 1).ToString();
                        dgv.Rows[i].Cells["PLAN_DATE"].Value = dt.Rows[i]["PLAN_DATE"].ToString();
                        dgv.Rows[i].Cells["PLAN_CD"].Value = dt.Rows[i]["PLAN_CD"].ToString();
                        dgv.Rows[i].Cells["CUST_CD"].Value = dt.Rows[i]["CUST_CD"].ToString();
                        dgv.Rows[i].Cells["CUST_NM"].Value = dt.Rows[i]["CUST_NM"].ToString();
                        dgv.Rows[i].Cells["ITEM_CNT"].Value = dt.Rows[i]["ITEM_CNT"].ToString();
                        dgv.Rows[i].Cells["CHK"].Value = false;
                    }
                }
                else
                {
                    planGrid.Rows.Clear();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("시스템 오류" + e.ToString());
            }
        }

        private void planDetail2()
        {
            cust_max_num = 0;
            wnDm wDm = new wnDm();
            DataTable dt = null;

            if (planGrid.Rows.Count > 0)   //row 없이 버튼 누를때 체크 확인
            {
                bool chk = false;
                for (int i = 0; i < planGrid.Rows.Count; i++) //chk 검사
                {
                    if ((bool)planGrid.Rows[i].Cells["CHK"].Value == true)
                    {
                        chk = true;
                        break;
                    }
                }
                chk_planGrid.Rows.Clear();
                if (chk)
                {
                    bool chk2 = false;
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(" where 1=1 ");
                    for (int i = 0; i < planGrid.Rows.Count; i++)
                    {
                        if ((bool)planGrid.Rows[i].Cells["CHK"].Value == true)
                        {
                            chk_planGrid.Rows.Add();
                            chk_planGrid.Rows[chk_planGrid.Rows.Count - 1].Cells["PLAN_DATE"].Value = planGrid.Rows[i].Cells["PLAN_DATE"].Value;
                            chk_planGrid.Rows[chk_planGrid.Rows.Count - 1].Cells["PLAN_CD"].Value = planGrid.Rows[i].Cells["PLAN_CD"].Value;
                            chk_planGrid.Rows[chk_planGrid.Rows.Count - 1].Cells["CHK"].Value = planGrid.Rows[i].Cells["CHK"].Value;
                            
                            if (chk2 == false) //처음 체크일 경우
                            {
                                sb.AppendLine(" and (A.PLAN_DATE = '" + planGrid.Rows[i].Cells["PLAN_DATE"].Value + "' and A.PLAN_CD = '" + planGrid.Rows[i].Cells["PLAN_CD"].Value + "')");
                            }
                            else 
                            {
                                sb.AppendLine(" or (A.PLAN_DATE = '" + planGrid.Rows[i].Cells["PLAN_DATE"].Value + "' and A.PLAN_CD = '" + planGrid.Rows[i].Cells["PLAN_CD"].Value + "')");
                            }
                            chk2 = true;
                        }
                    }
                    dt = wDm.fn_Plan_Detail_List(sb.ToString());

                    itemPlanGrid.Rows.Clear();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            itemPlanGrid.Rows.Add();
                            itemPlanGrid.Rows[i].Cells[0].Value = (i + 1).ToString();
                            itemPlanGrid.Rows[i].Cells["P_PLAN_DATE"].Value = dt.Rows[i]["PLAN_DATE"].ToString();
                            itemPlanGrid.Rows[i].Cells["P_PLAN_CD"].Value = dt.Rows[i]["PLAN_CD"].ToString();
                            itemPlanGrid.Rows[i].Cells["P_SEQ"].Value = dt.Rows[i]["SEQ"].ToString();
                            itemPlanGrid.Rows[i].Cells["ITEM_CD"].Value = dt.Rows[i]["ITEM_CD"].ToString();
                            itemPlanGrid.Rows[i].Cells["ITEM_NM"].Value = dt.Rows[i]["ITEM_NM"].ToString();
                            itemPlanGrid.Rows[i].Cells["P_UNIT_CD"].Value = dt.Rows[i]["UNIT_CD"].ToString();
                            itemPlanGrid.Rows[i].Cells["P_UNIT_NM"].Value = dt.Rows[i]["UNIT_NM"].ToString();
                            itemPlanGrid.Rows[i].Cells["P_TOTAL_AMT"].Value = (decimal.Parse(dt.Rows[i]["TOTAL_AMT"].ToString())).ToString("#,0.######");
                            itemPlanGrid.Rows[i].Cells["P_PRICE"].Value = (decimal.Parse(dt.Rows[i]["PRICE"].ToString())).ToString("#,0.######");
                            itemPlanGrid.Rows[i].Cells["P_TOTAL_MONEY"].Value = (decimal.Parse(dt.Rows[i]["TOTAL_MONEY"].ToString())).ToString("#,0.######");
                        }
                    }

                    soyoLogic(sb); //소요산출량
                }
                else
                {
                    itemPlanGrid.Rows.Clear();
                    rs_order_grid.Rows.Clear();
                }
                //MessageBox.Show(cust_max_num.ToString());
            }
            else 
            {
                itemPlanGrid.Rows.Clear();
            }
        }

        private void grdCellSetting()
        {
            chk_planGrid.AllowUserToAddRows = false;

            chk_planGrid.Columns.Add("PLAN_DATE", "PLAN_DATE");
            chk_planGrid.Columns.Add("PLAN_CD", "PLAN_CD");
            chk_planGrid.Columns.Add("CHK", "CHK");

            for (int i = 0; i < planGrid.ColumnCount; i++)
            {
                planGrid.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            for (int i = 0; i < rs_order_grid.ColumnCount; i++)
            {
                rs_order_grid.Columns[i].ReadOnly = true;
                //rs_order_grid.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            rs_order_grid.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            rs_order_grid.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            rs_order_grid.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            rs_order_grid.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            rs_order_grid.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            rs_order_grid.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            rs_order_grid.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            rs_order_grid.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


            rs_order_grid.Columns[0].ReadOnly = false; //맨 앞 체크박스
            rs_order_grid.Columns["RS_AMT"].ReadOnly = false;
        }

        private void resetSetting() 
        {
            cust_max_num = 0;
            rs_order_grid.Rows.Clear();
            itemPlanGrid.Rows.Clear();

            plan_list(planGrid, "where A.PLAN_DATE >= '" + start_date.Text.ToString() + "' and  A.PLAN_DATE <= '" + end_date.Text.ToString() + "' and ORDER_YN = 'N'");
        }

        private void soyoLogic(StringBuilder sb) 
        {
            wnDm wDm = new wnDm();
            DataTable dt = new DataTable();
            dt = wDm.fn_Soyo_Result_List(sb.ToString());

            rs_order_grid.Rows.Clear();
            saveStr = sb.ToString(); //저장 (체크박스 해제 사용하기 위한 용도)

            lbl_order_cnt.Text = dt.Rows.Count.ToString();
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rs_order_grid.Rows.Add();
                    rs_order_grid.Rows[i].Cells[0].Value = false;
                    rs_order_grid.Rows[i].Cells[1].Value = (i + 1).ToString();
                    rs_order_grid.Rows[i].Cells["RAW_MAT_CD"].Value = dt.Rows[i]["RAW_MAT_CD"].ToString();
                    rs_order_grid.Rows[i].Cells["RAW_MAT_NM"].Value = dt.Rows[i]["RAW_MAT_NM"].ToString();
                    rs_order_grid.Rows[i].Cells["SPEC"].Value = dt.Rows[i]["SPEC"].ToString();
                    rs_order_grid.Rows[i].Cells["TOTAL_AMT"].Value = (decimal.Parse(dt.Rows[i]["TOTAL_AMT"].ToString())).ToString("#,0.######");
                    rs_order_grid.Rows[i].Cells["BAL_STOCK"].Value = (decimal.Parse(dt.Rows[i]["BAL_STOCK"].ToString())).ToString("#,0.######");
                    rs_order_grid.Rows[i].Cells["RS_AMT"].Value = (decimal.Parse(dt.Rows[i]["RS_AMT"].ToString())).ToString("#,0.######");
                    rs_order_grid.Rows[i].Cells["PRICE"].Value = (decimal.Parse(dt.Rows[i]["INPUT_PRICE"].ToString())).ToString("#,0.######");
                    rs_order_grid.Rows[i].Cells["TOTAL_MONEY"].Value = (decimal.Parse(dt.Rows[i]["TOTAL_MONEY"].ToString())).ToString("#,0.######");
                    rs_order_grid.Rows[i].Cells["UNIT_CD"].Value = dt.Rows[i]["INPUT_UNIT"].ToString();
                    rs_order_grid.Rows[i].Cells["PUR_CUST_CD"].Value = dt.Rows[i]["CUST_CD"].ToString();
                    rs_order_grid.Rows[i].Cells["PUR_CUST_NM"].Value = dt.Rows[i]["CUST_NM"].ToString();
                    rs_order_grid.Rows[i].Cells["CUST_NUM"].Value = dt.Rows[i]["CUST_NUM"].ToString(); // 총 row에서 거래처가 N개일 경우 번호를 1~N번까지 매김 
                    //rs_order_grid.Rows[i].Cells["CUST_GUBUN"].Value = dt.Rows[i]["CUST_GUBUN"].ToString();
                    //rs_order_grid.Rows[i].Cells["CUST_GUBUN_NM"].Value = dt.Rows[i]["CUST_GUBUN_NM"].ToString();

                    rs_order_grid.Rows[i].Cells["REAL_AMT"].Value = (decimal.Parse(dt.Rows[i]["REAL_AMT"].ToString())).ToString("#,0.######");

                    string rs_amt = ((string)rs_order_grid.Rows[i].Cells["RS_AMT"].Value).Replace(",", "");
                    double d_rs_amt = double.Parse(rs_amt);

                    if (d_rs_amt > 0)
                    {
                        rs_order_grid.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    }
                    else if (d_rs_amt == 0)
                    {
                        rs_order_grid.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                    }
                    else
                    {
                        rs_order_grid.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    }

                    if (i == 0)
                    {
                        cust_max_num = int.Parse(dt.Rows[i]["CUST_NUM"].ToString());
                    }
                    else
                    {
                        if (cust_max_num < int.Parse(dt.Rows[i]["CUST_NUM"].ToString()))
                        {
                            cust_max_num = int.Parse(dt.Rows[i]["CUST_NUM"].ToString());
                        }
                    }
                }
            }
        }

        private void rs_order_grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("where 1=1 ");
                sb.AppendLine(" and A.RAW_MAT_CD = '" + rs_order_grid.Rows[e.RowIndex].Cells["RAW_MAT_CD"].Value.ToString() + "' ");
                sb.AppendLine(" and F.COMPLETE_YN = 'N' ");
                dt = wDm.fn_Work_Rm_Detail_List(sb.ToString());

                txt_raw_mat_nm.Text = rs_order_grid.Rows[e.RowIndex].Cells["RAW_MAT_NM"].Value.ToString();
                workRmGrid.Rows.Clear();
                if (dt != null && dt.Rows.Count > 0) 
                {
                    for (int i = 0; i < dt.Rows.Count; i++) 
                    {
                        workRmGrid.Rows.Add();
                        workRmGrid.Rows[i].Cells[0].Value = (i + 1);
                        workRmGrid.Rows[i].Cells["WORK_INST_DATE"].Value = dt.Rows[i]["W_INST_DATE"].ToString();
                        workRmGrid.Rows[i].Cells["WORK_INST_CD"].Value = dt.Rows[i]["W_INST_CD"].ToString();
                        workRmGrid.Rows[i].Cells[3].Value = dt.Rows[i]["CUST_NM"].ToString();
                        workRmGrid.Rows[i].Cells["INPUT_UNIT_NM"].Value = dt.Rows[i]["INPUT_UNIT_NM"].ToString();
                        workRmGrid.Rows[i].Cells["OUTPUT_UNIT_NM"].Value = dt.Rows[i]["OUTPUT_UNIT_NM"].ToString();
                        workRmGrid.Rows[i].Cells[6].Value = (decimal.Parse(dt.Rows[i]["TOTAL_SOYO_AMT"].ToString())).ToString("#,0.######");
                        workRmGrid.Rows[i].Cells["LOT_NO"].Value = dt.Rows[i]["LOT_NO"].ToString();
                    }
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("시스템 오류" + ex.ToString());
            }
        }
    }
}
