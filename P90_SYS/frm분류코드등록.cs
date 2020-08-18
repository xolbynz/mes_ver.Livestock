using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 스마트팩토리.P90_SYS
{
    public partial class frm분류코드등록 : Form
    {
        public frm분류코드등록()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
          //  code_logic();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSrch_Click(object sender, EventArgs e)
        {
           // code_list();
        }

        //private void code_list()
        //{
        //    try
        //    {
        //        wnDm wDm = new wnDm();
        //        DataTable dt = null;

        //        StringBuilder sb = new StringBuilder();
        //        sb.AppendLine("where 1=1 ");

        //        //if (!txtSrch.Text.ToString().Equals(""))
        //        //{
        //        //    sb.AppendLine("and CHK_NM like '%" + txt_srch.Text.ToString() + "%' ");
        //        //}
        //        //else if (cmb_cd2.SelectedValue != "전체" && cmb_cd2.SelectedValue != "")
        //        //{
        //        //    sb.AppendLine("and FLOW_CD = '" + cmb_cd2.SelectedValue + "' ");
        //        //}

        //        dt = wDm.haccp_Grid_List(sb.ToString());

        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            this.GridCode.RowCount = dt.Rows.Count;

        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                GridCode.Rows[i].Cells[0].Value = dt.Rows[i]["CHK_ORD"].ToString();
        //                GridCode.Rows[i].Cells[1].Value = dt.Rows[i]["FLOW_CD"].ToString();
        //                GridCode.Rows[i].Cells[2].Value = dt.Rows[i]["CHK_CD"].ToString();
        //                GridCode.Rows[i].Cells[3].Value = dt.Rows[i]["CHK_NM"].ToString();
        //                if (dt.Rows[i]["USE_YN"].ToString().Equals("Y"))
        //                {
        //                    dataChkGrid.Rows[i].Cells[4].Value = "Y";
        //                }
        //                else
        //                {
        //                    dataChkGrid.Rows[i].Cells[4].Value = "N";
        //                }
        //            }
        //        }
        //        else
        //        {
        //            GridCode.Rows.Clear();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show("시스템 에러: " + e.Message.ToString());
        //    }
        //}       

        //private void code_logic()
        //{
        //    try
        //    {
        //        if (txtCode.Text.ToString().Equals(""))
        //        {
        //            MessageBox.Show("코드를 입력하시기 바랍니다.");
        //            return;
        //        }
        //        if (txtName.Text.ToString().Equals(""))
        //        {
        //            MessageBox.Show("명칭을 입력하시기 바랍니다.");
        //            return;
        //        }

        //        if (GridCode.Rows.Count > 0)
        //        {
        //            int cnt = 0;
        //            int grid_cnt = GridCode.Rows.Count;
        //            for (int i = 0; i < grid_cnt; i++)
        //            {
        //                string txt_item_cd = (string)GridCode.Rows[i - cnt].Cells["ITEM_CD"].Value;

        //                if (txt_item_cd == "" || txt_item_cd == null)  //마지막 행에 원부재료코드가 없을 경우 제거
        //                {
        //                    GridCode.Rows.RemoveAt(i - cnt);
        //                    cnt++;
        //                }
        //            }
        //        }

        //        string order_yn = comInfo.resultYn(chk_order_yn);
        //        if (lbl_plan_gbn.Text != "1")
        //        {
        //            string plan_num = txt_plan_date.Text.ToString().Replace("-", "");
        //            plan_num = plan_num.Substring(2).ToString();

        //            wnDm wDm = new wnDm();
        //            int rsNum = wDm.insertPlan(
        //                txt_plan_date.Text.ToString(),
        //                txt_cust_cd.Text.ToString(),
        //                req_date.Text.ToString(),
        //                order_yn,
        //                plan_num,
        //                txt_comment.Text.ToString(),
        //                itemPlanGrid,
        //                itemHalfGrid);

        //            if (rsNum == 0)
        //            {
        //                //wnProcCon wDmProc = new wnProcCon();
        //                //int rsNum2 = wDmProc.prod_plan_group(txt_plan_date.Text.ToString(), txt_plan_cd.Text.ToString(), Common.p_strStaffNo);

        //                //if (rsNum2 == -9)
        //                //{
        //                //    wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
        //                //}

        //                resetSetting();
        //                plan_list(planGrid, "where A.PLAN_DATE >= '" + start_date.Text.ToString() + "' and  A.PLAN_DATE <= '" + end_date.Text.ToString() + "'");

        //                MessageBox.Show("성공적으로 등록하였습니다.");
        //            }
        //            else if (rsNum == 1)
        //                MessageBox.Show("저장에 실패하였습니다");
        //            else if (rsNum == 2)
        //                MessageBox.Show("SQL COMMAND 에러");
        //            else if (rsNum == 3)
        //                MessageBox.Show("기존 코드가 있으니 \n 다른 코드로 입력 바랍니다.");
        //            else
        //                MessageBox.Show("Exception 에러");
        //        }
        //        else
        //        {
        //            wnDm wDm = new wnDm();
        //            int rsNum = wDm.updatePlan(
        //                txt_plan_date.Text.ToString(),
        //                txt_plan_cd.Text.ToString(),
        //                txt_cust_cd.Text.ToString(),
        //                req_date.Text.ToString(),
        //                order_yn,
        //                txt_comment.Text.ToString(),
        //                itemPlanGrid,
        //                itemHalfGrid,
        //                del_planGrid,
        //                del_HalfGrid);

        //            if (rsNum == 0)
        //            {
        //                wnProcCon wDmProc = new wnProcCon();
        //                int rsNum2 = wDmProc.prod_plan_group(txt_plan_date.Text.ToString(), txt_plan_cd.Text.ToString(), Common.p_strStaffNo);

        //                if (rsNum2 == -9)
        //                {
        //                    wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
        //                }

        //                del_planGrid.Rows.Clear();
        //                del_HalfGrid.Rows.Clear();
        //                plan_list(planGrid, "where A.PLAN_DATE >= '" + start_date.Text.ToString() + "' and  A.PLAN_DATE <= '" + end_date.Text.ToString() + "'");
        //                planDetail2();

        //                MessageBox.Show("성공적으로 수정하였습니다.");
        //            }
        //            else if (rsNum == 1)
        //                MessageBox.Show("저장에 실패하였습니다");
        //            else
        //                MessageBox.Show("Exception 에러");
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show("시스템 오류" + e.ToString());
        //    }
        }
    }

