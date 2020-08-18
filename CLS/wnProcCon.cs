using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 스마트팩토리.CLS
{
    class wnProcCon
    {
        public void prod_pln_work_yn(string plan_num, string plan_item)
        {
            wnDmProc wDmProc = new wnDmProc();
            int num = wDmProc.sp_plan_work_yn(plan_num, plan_item);

            if (num != 0)
            {
                System.Console.WriteLine("프로시저 에러" + num.ToString());
            }
        }

        public void prod_raw_out(string lot_no, string raw_mat_cd, double out_amt) 
        {
            wnDmProc wDmProc = new wnDmProc();
            int num = wDmProc.sp_raw_out(lot_no, raw_mat_cd, out_amt);

            if (num != 0)
            {
                System.Console.WriteLine("프로시저 에러" + num.ToString());
                return;
            }
        }

        public void prod_half_out(string lot_no, string half_item_cd, double out_amt) 
        {
            wnDmProc wDmProc = new wnDmProc();
            int num = wDmProc.sp_half_out(lot_no, half_item_cd, out_amt);

            if (num != 0)
            {
                System.Console.WriteLine("프로시저 에러" + num.ToString());
                return;
            }
        
        }
        public int prod_item_stock_upd(string item_cd) 
        {
            wnDmProc wDmProc = new wnDmProc();
            int num = wDmProc.sp_item_stock_upd(item_cd);

            if (num != 0)
            {
                //System.Console.WriteLine("프로시저 에러" + num.ToString());
                return -9;
            }
            return num;
        }

        // 생산계획 제품 중복 통합테이블 생성 (작업지시서 목적)
        public int prod_plan_group(string plan_date,string plan_cd, string staffCd)
        {
            wnDmProc wDmProc = new wnDmProc();
            int num = wDmProc.sp_plan_group(plan_date,plan_cd,staffCd);

            if (num != 0)
            {
                //System.Console.WriteLine("프로시저 에러" + num.ToString());
                return -9;
            }
            return num;
        }
    }
}
