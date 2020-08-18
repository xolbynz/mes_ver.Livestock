using 스마트팩토리.CLS;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 스마트팩토리.Model.Query
{
    class QueryUpdate
    {

        #region 작업일보등록 업데이트
        public int UpdateWorkResult(
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
            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                sb.AppendLine(" update F_WORK_RESULT set");
              
                //sb.AppendLine("     ,WORK_RS_NUM = @WORK_RS_NUM ");
                //sb.AppendLine("     ,ITEM_CD = @ITEM_CD ");
                sb.AppendLine("     RS_AMT = @RS_AMT ");
                sb.AppendLine("     ,ALL_INPUT_AMT = @ALL_INPUT_AMT ");
                sb.AppendLine("     ,ALL_LOSS = @ALL_LOSS ");
                sb.AppendLine("     ,ALL_RS_INPUT_AMT = @ALL_RS_INPUT_AMT ");
                sb.AppendLine("     ,ST_STATUS_YN = @ST_STATUS_YN ");
                sb.AppendLine("     ,UPSTAFF = '" + Common.p_strStaffNo + "' ");
                sb.AppendLine("     ,UPTIME = convert(varchar, getdate(), 120) ");
                sb.AppendLine("     ,PLAN_DATE = @PLAN_DATE ");
                sb.AppendLine("     ,PLAN_CD = @PLAN_CD ");

                sb.AppendLine("where WORK_DATE = '" + work_date + "' ");
                sb.AppendLine("and WORK_CD = '" + work_cd + "'  ");
                if (rawGrid.Rows.Count > 0)
                {
                    for (int i = 0; i < rawGrid.Rows.Count; i++)
                    {
                        string txt_seq = (string)rawGrid.Rows[i].Cells["R_SEQ"].Value;
                        if (txt_seq == "" || txt_seq == null)
                        {
                            //필요없는부분
                            //sb.AppendLine("declare @workRaw_seq" + i + " int ");
                            //sb.AppendLine("select @workRaw_seq" + i + " =ISNULL(MAX(SEQ),0)+1 from F_WORK_RESULT_RAW ");
                            //sb.AppendLine("where WORK_DATE = '" + work_date + "' ");
                            //sb.AppendLine("and WORK_CD = '" + work_cd + "' "); //주문코드가 들어가야함
                            //sb.AppendLine("insert into F_WORK_RESULT_RAW(");
                            //sb.AppendLine("     WORK_DATE ");
                            //sb.AppendLine("     ,WORK_CD ");
                            //sb.AppendLine("     ,SEQ ");
                            //sb.AppendLine("     ,RAW_MAT_CD ");
                            //sb.AppendLine("     ,RS_AMT ");
                            //sb.AppendLine("     ,PLAN_DATE ");
                            //sb.AppendLine("     ,PLAN_CD ");
                            //sb.AppendLine("     ,PLAN_SEQ");
                            //sb.AppendLine("     ,COMMENT");
                            //sb.AppendLine("     ,UPSTAFF");
                            //sb.AppendLine("     ,UPTIME");
                            //sb.AppendLine("  )values ( ");
                            //sb.AppendLine("     '" + work_date + "' ");
                            //sb.AppendLine("     ,'" + work_cd + "'"); //주문코드
                            //sb.AppendLine("     ,@workRaw_seq" + i + " ");
                            //sb.AppendLine("     ,'" + ((string)rawGrid.Rows[i].Cells["RAW_MAT_CD"].Value) + "'"); //제품코드
                            //sb.AppendLine("     ,'" + ((string)rawGrid.Rows[i].Cells["RS_AMT"].Value) + "' ");
                            //sb.AppendLine("     ,'" + plan_date +"' ");
                            //sb.AppendLine("     ,'" + plan_cd + "' ");
                            //sb.AppendLine("     ,'" + (string)rawGrid.Rows[i].Cells["PLAN_SEQ"].Value + "' ");
                            //sb.AppendLine("     ,'" + (string)rawGrid.Rows[i].Cells["COMMENT"].Value + "' ");
                            //sb.AppendLine("     ,'" + Common.p_strStaffNo + "' "); //UPSTAFF 다시 수정하기
                            //sb.AppendLine("     ,convert(varchar, getdate(), 120) "); //UPTIME 다시 수정하기
                            //sb.AppendLine("  )");
                        }
                        else
                        {
                            sb.AppendLine("update F_WORK_RESULT_RAW set");
                            sb.AppendLine("       RAW_MAT_CD =  '" + rawGrid.Rows[i].Cells["RAW_MAT_CD"].Value + "' ");
                         
                            sb.AppendLine("       ,RS_AMT =  '" + ((string)rawGrid.Rows[i].Cells["RS_AMT"].Value).Replace(",", "") + "' ");
                            sb.AppendLine("      ,PLAN_DATE =  '" + plan_date + "' ");
                            sb.AppendLine("      ,PLAN_CD =  '" + plan_cd + "' ");
                            sb.AppendLine("      ,PLAN_SEQ =  '" + rawGrid.Rows[i].Cells["PLAN_SEQ"].Value + "' ");
                            sb.AppendLine("      ,COMMENT =  '" + rawGrid.Rows[i].Cells["COMMENT"].Value + "' ");
                            sb.AppendLine("      ,UPSTAFF =  '" + Common.p_strStaffNo + "' ");
                            sb.AppendLine("      ,UPTIME =  convert(varchar, getdate(), 120) ");
                            sb.AppendLine(" where WORK_DATE = '" + work_date + "' ");
                            sb.AppendLine(" and WORK_CD = '" + work_cd + "' ");
                            sb.AppendLine(" and SEQ = '" + rawGrid.Rows[i].Cells["R_SEQ"].Value + "'");
                        }
                    }
                }


                if (flowGrid.Rows.Count > 0)
                {
                    for (int i = 0; i < flowGrid.Rows.Count; i++)
                    {
                        string txt_seq = (string)flowGrid.Rows[i].Cells["F_SEQ"].Value;
                        if (txt_seq == "" || txt_seq == null)
                        {
                           
                        }
                        else
                        {
                            sb.AppendLine("update F_WORK_RESULT_FLOW set");
                            sb.AppendLine("       FLOW_CD =  '" + flowGrid.Rows[i].Cells["FLOW_CD"].Value + "' ");
                            sb.AppendLine("       ,INPUT_AMT =  '" + double.Parse((flowGrid.Rows[i].Cells["INPUT_AMT"].Value.ToString()).Replace(",", "")) + "' ");
                            sb.AppendLine("      ,LOSS =  '" + double.Parse((flowGrid.Rows[i].Cells["LOSS"].Value.ToString()).Replace(",", "")) + "' ");
                            sb.AppendLine("      ,RS_INPUT_AMT =  '" + double.Parse((flowGrid.Rows[i].Cells["RS_INPUT_AMT"].Value.ToString()).Replace(",", "")) + "' ");
                           // sb.AppendLine("      ,COMMENT =  '" + flowGrid.Rows[i].Cells["COMMENT"].Value + "' ");
                            sb.AppendLine("      ,UPSTAFF =  '" + Common.p_strStaffNo + "' ");
                            sb.AppendLine("      ,UPTIME =  convert(varchar, getdate(), 120) ");
                            sb.AppendLine(" where WORK_DATE = '" + work_date + "' ");
                            sb.AppendLine(" and WORK_CD = '" + work_cd + "' ");
                            sb.AppendLine(" and SEQ = '" + flowGrid.Rows[i].Cells["F_SEQ"].Value + "'");
                        }
                    }
                }


             
                SqlCommand sCommand = new SqlCommand(sb.ToString());

                //sCommand.Parameters.AddWithValue("@WORK_RS_NUM", "");
                //sCommand.Parameters.AddWithValue("@ITEM_CD", item_cd);
                sCommand.Parameters.AddWithValue("@RS_AMT", rs_amt);
                sCommand.Parameters.AddWithValue("@ALL_INPUT_AMT", all_input_amt);
                sCommand.Parameters.AddWithValue("@ALL_LOSS", all_loss);
                sCommand.Parameters.AddWithValue("@ALL_RS_INPUT_AMT", all_rs_input_amt);
                sCommand.Parameters.AddWithValue("@ST_STATUS_YN", "Y");
                sCommand.Parameters.AddWithValue("@PLAN_DATE", plan_date);
                sCommand.Parameters.AddWithValue("@PLAN_CD", plan_cd);


                int qResult = wAdo.SqlCommandEtc(sCommand, "update_RAW_MAT_TB");
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
        #endregion 

        public int updateWorkState(string plan_date, string plan_cd, string state_cd)
        {
            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();
                sb = new StringBuilder();

                sb.AppendLine("update F_PLAN set");
                sb.AppendLine("      STATE_CD = '" + state_cd +"' "); // RAW_OUT_YN = 'Y', 
                sb.AppendLine("where PLAN_DATE = '" + plan_date + "' ");
                sb.AppendLine(" and PLAN_CD = '" + plan_cd + "' ");

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                int qResult = wAdo.SqlCommandEtc(sCommand, "update_PLAN_RAW_OUT");
                if (qResult > 0)
                {
                    return 0;  // 0 true, 1 false
                }
                else return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine("error" + e.ToString());
                return 9;
            }
        }

        public int updateOutSignYn(string out_date, string out_cd, string up_prog_cd) 
        {
            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();
                sb = new StringBuilder();

                sb.AppendLine("update F_ITEM_OUT set");
                sb.AppendLine("      PROG_CD = '" + up_prog_cd + "' "); // RAW_OUT_YN = 'Y', 
                if (up_prog_cd.ToString().Equals("2"))
                {
                    sb.AppendLine("      ,OUT_SIGN_YN = 'Y' ");
                }
                else if (up_prog_cd.ToString().Equals("3"))
                {
                    sb.AppendLine("      ,QUAL_SIGN_YN = 'Y' ");
                }
                else 
                {
                    sb.AppendLine("      ,ST_MG_SIGN_YN = 'Y' ");
                }
                sb.AppendLine("where OUT_DATE = '" + out_date + "' ");
                sb.AppendLine(" and OUT_CD = '" + out_cd + "' ");

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                int qResult = wAdo.SqlCommandEtc(sCommand, "update_PLAN_RAW_OUT");
                if (qResult > 0)
                {
                    return 0;  // 0 true, 1 false
                }
                else return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine("error" + e.ToString());
                return 9;
            }
        }

        //작업일보등록 확정버튼
        
    }
}
