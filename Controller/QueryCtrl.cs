using 스마트팩토리.Model.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 스마트팩토리.Controller
{
    class QueryCtrl
    {
        QueryInsert qInsert = new QueryInsert();
        QuerySelect qSelect = new QuerySelect();
        QueryUpdate qUpdate = new QueryUpdate();
        QueryDelete qDelete = new QueryDelete();

        #region select 
        public DataTable fn_TopMenu_List()
        {
            //탑 메뉴 Controller
            return qSelect.sTopMenuList();
        }

        public DataTable fn_SubMenu_List(string uCD, string sID)
        {
            //서브 메뉴 Controller
            return qSelect.sSumMenuList(uCD, sID);
        }

        public DataTable fn_Plan_List(string condition)
        {
            //생산계획 정보 가져오기 
            return qSelect.sPlanDetailList(condition);
        }

        public DataTable fn_Plan_Raw_List(string condition)
        {
            //생산계획 정보(원재료) 가져오기 
            return qSelect.sPlanRawList(condition);
        }

        public DataTable fn_Plan_Subject_List(string condition)
        {
            //생산계획 정보 (조치사항) 가져오기 
            return qSelect.sPlanSubjectList(condition);
        }

        public DataTable fn_Soyo_Result_List(string condition)
        {
            //소요결과 정보 가져오기
            return qSelect.sSoyoRsList(condition);
        }

        public int insertSoyo(
                  DataGridView dgv
                , DataGridView chk_dgv
                , int cust_max_num){
            //소요량 저장
                return qInsert.insertSoyo(dgv,chk_dgv,cust_max_num);
        }

        public int deletePlan(string plan_date, string plan_cd, string jumun_date, string jumun_cd, string jumun_seq) 
        {

            return qDelete.deletePlan(plan_date, plan_cd, jumun_date, jumun_cd, jumun_seq);
        }
        #endregion

        public int workLogic(
              string work_date
            , string work_cd
            , string item_cd
            , string target_amt
            , string rs_amt
            , string plan_date
            , string plan_cd
            , string all_input_amt
            , string all_loss
            , string all_rs_input_amt
            , DataGridView rawGrid
            , DataGridView flowGrid
            , int gubun)
        {
            if (gubun == 1)  //insert
            {
                return qInsert.insertWorkResult(work_date, work_cd, item_cd,target_amt, rs_amt, plan_date, plan_cd, all_input_amt,all_loss,all_rs_input_amt,rawGrid,flowGrid,1);
            }
            return 0;
        }

        public int workUpdateLogic(
             string work_date
           , string work_cd
           , string item_cd
           , double rs_amt
           , string plan_date
           , string plan_cd
           , double all_input_amt
           , double all_loss
           , double all_rs_input_amt
           , DataGridView rawGrid
           , DataGridView flowGrid
           , int gubun)
        {
            if (gubun != 1)  //insert
            {
                return qUpdate.UpdateWorkResult(work_date, work_cd, item_cd, rs_amt, plan_date, plan_cd, all_input_amt, all_loss, all_rs_input_amt,rawGrid,flowGrid,2);
            }
            return 0;
        }

        public DataTable fn_WorkList(string condition)
        {
            return qSelect.SelectWorkList(condition);
        }


        //작업공정등록화면 Main 뿌리기 
        public DataTable fn_workMain_List(string condition)
        {
            return qSelect.SelectWorkMainList(condition);
        }

        //작업공정등록화면 공정그리드 뿌리기 
        public DataTable fn_WorkRsFlowList(string condition)
        {
            return qSelect.SelectWorkFlowList(condition);
        }
        //작업공정등록화면 원자재그리드 뿌리기 
        public DataTable fn_WorkRsRawList(string condition)
        {
            return qSelect.SelectWorkRawList(condition);
        }

        internal int deleteWork(string txt_work_date, string lbl_work_cd)// string w_jumun_date, string w_jumun_cd, string w_jumun_seq
        {
            return qDelete.deleteWork(txt_work_date, lbl_work_cd);
        }

        public int updateWorkState(string plan_date, string plan_cd,string state_cd) 
        {
            return qUpdate.updateWorkState(plan_date, plan_cd, state_cd);
        }

        public int insert_ItemInput(string work_date, string work_cd, string item_cd, string item_rs_amt)
        {
            return qInsert.insertWorkRsState(work_date, work_cd, item_cd, item_rs_amt);
        }

        public DataTable fn_SalesList(string condition)
        {
            return qSelect.SelectSalesList(condition);
        }

        public DataTable fn_Sales_Detail_List(string condition)
        {
            return qSelect.SelectSales_Detail_List(condition);
        }

        public DataTable fn_reportCust_List(string condition)
        {
            return qSelect.SelectReport_Cust_List(condition);
        }

        public DataTable fn_reportItem_List(string condition)
        {
            return qSelect.SelectReport_Item_List(condition);
        }

        //출고지시 제품 소요
        public DataTable fn_item_soyo(string condition) 
        {
            return qSelect.SelectItemSoyo(condition);
        }

        //출고 승인 업데이트 
        public int update_item_Sign_Yn(string out_date, string out_cd, string up_prog_cd) 
        {
            return qUpdate.updateOutSignYn(out_date, out_cd, up_prog_cd);
        }

        //부서 트리뷰 목록 불러오기 

        public DataTable fn_tvDept_List(string condition) 
        {
            return qSelect.tvDeptList(condition);
        }

        public DataTable fn_tvDept_MaxLvCnt()
        {
            return qSelect.tvDeptMaxLvCnt();
        }


    }
}
