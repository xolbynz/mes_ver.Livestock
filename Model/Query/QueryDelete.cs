using 스마트팩토리.CLS;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 스마트팩토리.Model.Query
{
    class QueryDelete
    {
        public int deletePlan(string plan_date, string plan_cd, string jumun_date, string jumun_cd, string jumun_seq) 
        {
            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("delete from F_PLAN ");
                sb.AppendLine("    where PLAN_DATE = @PLAN_DATE ");
                sb.AppendLine("    and PLAN_CD = @PLAN_CD ");


                sb.AppendLine("delete from F_PLAN_RAW ");
                sb.AppendLine("    where PLAN_DATE = @PLAN_DATE ");
                sb.AppendLine("    and PLAN_CD = @PLAN_CD ");

                sb.AppendLine("delete from F_PLAN_FAC ");
                sb.AppendLine("    where PLAN_DATE = @PLAN_DATE ");
                sb.AppendLine("    and PLAN_CD = @PLAN_CD ");

                sb.AppendLine("delete from F_PLAN_SUBJECT ");
                sb.AppendLine("    where PLAN_DATE = @PLAN_DATE ");
                sb.AppendLine("    and PLAN_CD = @PLAN_CD ");

                sb.AppendLine("update F_JUMUN_DETAIL ");
                sb.AppendLine("    set PLAN_YN = 'N' ");
                sb.AppendLine("    where JUMUN_DATE = @JUMUN_DATE ");
                sb.AppendLine("         and JUMUN_CD = @JUMUN_CD ");
                sb.AppendLine("         and SEQ = @JUMUN_SEQ ");

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@PLAN_DATE", plan_date);
                sCommand.Parameters.AddWithValue("@PLAN_CD", plan_cd);
                sCommand.Parameters.AddWithValue("@JUMUN_DATE", jumun_date);
                sCommand.Parameters.AddWithValue("@JUMUN_CD", jumun_cd);
                sCommand.Parameters.AddWithValue("@JUMUN_SEQ", jumun_seq);

                int qResult = wAdo.SqlCommandEtc(sCommand, "delete_PLAN_TB");
                if (qResult > 0)
                {
                    return 0;  // 0 true, 1 false
                }
                else return 1;
            }
            catch (Exception e) 
            {
                wnLog.writeLog(wnLog.LOG_ERROR, e.Message + " - " + e.ToString());
                return 9;
            }
        }

        public int deleteWork(string txt_work_date, string lbl_work_cd)
        {
            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("delete from F_WORK_RESULT ");
                sb.AppendLine("    where WORK_DATE = @WORK_DATE ");
                sb.AppendLine("    and WORK_CD = @WORK_CD ");


                sb.AppendLine("delete from F_WORK_RESULT_FLOW ");
                sb.AppendLine("    where WORK_DATE = @WORK_DATE ");
                sb.AppendLine("    and WORK_CD = @WORK_CD ");

                sb.AppendLine("delete from F_WORK_RESULT_RAW ");
                sb.AppendLine("    where WORK_DATE = @WORK_DATE ");
                sb.AppendLine("    and WORK_CD = @WORK_CD ");


                //sb.AppendLine("update F_PLAN ");
                //sb.AppendLine("    set ORDER_YN = 'N' ");
                //sb.AppendLine("    where JUMUN_DATE = @JUMUN_DATE ");
                //sb.AppendLine("         and JUMUN_CD = @JUMUN_CD ");
                //sb.AppendLine("         and SEQ = @JUMUN_SEQ ");

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@WORK_DATE", txt_work_date);
                sCommand.Parameters.AddWithValue("@WORK_CD", lbl_work_cd);
                //sCommand.Parameters.AddWithValue("@JUMUN_DATE", w_jumun_date);
                //sCommand.Parameters.AddWithValue("@JUMUN_CD", w_jumun_cd);
                //sCommand.Parameters.AddWithValue("@JUMUN_SEQ", w_jumun_seq);

                int qResult = wAdo.SqlCommandEtc(sCommand, "delete_PLAN_TB");
                if (qResult > 0)
                {
                    return 0;  // 0 true, 1 false
                }
                else return 1;
            }
            catch (Exception e)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, e.Message + " - " + e.ToString());
                return 9;
            }
        }
    }
}
