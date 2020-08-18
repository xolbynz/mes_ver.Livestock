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
    class QueryInsert
    {
        public int insertSoyo(
              DataGridView dgv
            , DataGridView chk_dgv
            , int cust_max_num)
        {
            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                //2019-07-29 유정훈
                //거래처를 GROUP으로 나눠 번호 순을 매김..  

                for (int i = 1; i <= cust_max_num; i++)
                {
                    string cust_cd = "";
                    for (int j = 0; j < dgv.Rows.Count; j++)
                    {
                        if (int.Parse(dgv.Rows[j].Cells["CUST_NUM"].Value.ToString()) == i)
                        {
                            cust_cd = dgv.Rows[j].Cells["PUR_CUST_CD"].Value.ToString(); //거래처 번호가 맞으면 cust_cd를 변수에 저장
                            break;
                        }
                    }

                    string order_date = DateTime.Now.ToString("yyyy-MM-dd");
                    sb.AppendLine("declare @seq" + i + " int ");
                    sb.AppendLine("select  @seq" + i + " =ISNULL(MAX(ORDER_CD),0)+1 from F_ORDER ");
                    sb.AppendLine("where ORDER_DATE = '" + order_date + "' ");

                    sb.AppendLine("insert into F_ORDER(");
                    sb.AppendLine("     ORDER_DATE");
                    sb.AppendLine("     ,ORDER_CD ");
                    sb.AppendLine("     ,CUST_CD ");
                    sb.AppendLine("     ,INPUT_REQ_DATE ");
                    sb.AppendLine("     ,COMPLETE_YN ");
                    sb.AppendLine("     ,STAFF_CD ");
                    sb.AppendLine("     ,COMMENT ");
                    sb.AppendLine("     ,INTIME ");
                    sb.AppendLine(" ) values ( ");
                    sb.AppendLine("      '" + order_date + "' ");
                    sb.AppendLine("     ,@seq" + i + " ");
                    sb.AppendLine("     ,'" + cust_cd + "' ");
                    sb.AppendLine("     ,convert(varchar, getdate(), 23) ");
                    sb.AppendLine("     ,'N' ");
                    sb.AppendLine("     ,'" + Common.p_strStaffNo + "' ");
                    sb.AppendLine("     ,'' ");
                    sb.AppendLine("     ,convert(varchar, getdate(), 120) ");
                    sb.AppendLine(" ) ");

                    for (int j = 0; j < dgv.Rows.Count; j++)
                    {
                        string rs_amt = ((string)dgv.Rows[j].Cells["RS_AMT"].Value).Replace(",", "");
                        double d_rs_amt = double.Parse(rs_amt);
                        if (cust_cd.ToString().Equals(dgv.Rows[j].Cells["PUR_CUST_CD"].Value.ToString()) && d_rs_amt >= 0)
                        {
                            sb.AppendLine("declare @order_seq" + j + " int ");
                            sb.AppendLine("select @order_seq" + j + " =ISNULL(MAX(SEQ),0)+1 from F_ORDER_DETAIL ");
                            sb.AppendLine("where ORDER_DATE = '" + order_date + "' ");
                            sb.AppendLine("and ORDER_CD =  @seq" + i + " ");

                            sb.AppendLine("insert into F_ORDER_DETAIL(");
                            sb.AppendLine("     ORDER_DATE ");
                            sb.AppendLine("     ,ORDER_CD ");
                            sb.AppendLine("     ,SEQ ");
                            sb.AppendLine("     ,RAW_MAT_CD ");
                            sb.AppendLine("     ,UNIT_CD ");
                            sb.AppendLine("     ,TOTAL_AMT ");
                            sb.AppendLine("     ,PRICE ");
                            sb.AppendLine("     ,TOTAL_MONEY ");
                            sb.AppendLine("     ,INSTAFF ");
                            sb.AppendLine("     ,INTIME ");
                            sb.AppendLine("  )values ( ");
                            sb.AppendLine("     '" + order_date + "' ");
                            sb.AppendLine("      ,@seq" + i + " ");
                            sb.AppendLine("     ,@order_seq" + j + " ");
                            sb.AppendLine("     ,'" + dgv.Rows[j].Cells["RAW_MAT_CD"].Value + "' ");
                            sb.AppendLine("     ,'" + dgv.Rows[j].Cells["INPUT_UNIT_CD"].Value + "' ");
                            sb.AppendLine("     ,'" + ((string)dgv.Rows[j].Cells["RS_AMT"].Value).Replace(",", "") + "' ");
                            sb.AppendLine("     ,'" + ((string)dgv.Rows[j].Cells["PRICE"].Value).Replace(",", "") + "' ");
                            sb.AppendLine("     ,'" + ((string)dgv.Rows[j].Cells["TOTAL_MONEY"].Value).Replace(",", "") + "' ");
                            sb.AppendLine("     ,'" + Common.p_strStaffNo + "' ");
                            sb.AppendLine("     ,convert(varchar, getdate(), 120)  ");
                            sb.AppendLine("  )");
                        }
                    }
                }

                for (int i = 0; i < chk_dgv.Rows.Count; i++)
                {
                    sb.AppendLine("update F_PLAN set");
                    sb.AppendLine("      ORDER_YN = 'Y'");
                    sb.AppendLine("     ,UPSTAFF = '" + Common.p_strStaffNo + "' ");
                    sb.AppendLine("     ,UPTIME = convert(varchar, getdate(), 120) ");

                    sb.AppendLine(" where PLAN_DATE = '" + chk_dgv.Rows[i].Cells["PLAN_DATE"].Value + "'  and PLAN_CD= '" + chk_dgv.Rows[i].Cells["PLAN_CD"].Value + "' ");
                }

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                int qResult = wAdo.SqlCommandEtc(sCommand, "insert_SOYO_ORDER_TB");
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

        public int insertWorkResult(
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
            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                sb = new StringBuilder();
                sb.AppendLine("declare @w_cd int ");
                sb.AppendLine("select @w_cd =ISNULL(MAX(WORK_CD),0)+1 from F_WORK_RESULT "); 
                sb.AppendLine("where WORK_DATE = '" + work_date+ "' ");

            
                sb.AppendLine(" insert into F_WORK_RESULT( ");
                sb.AppendLine("     WORK_DATE");
                sb.AppendLine("     ,WORK_CD ");
                sb.AppendLine("     ,WORK_RS_NUM ");
                sb.AppendLine("     ,ITEM_CD ");
                sb.AppendLine("     ,TARGET_AMT ");
                sb.AppendLine("     ,RS_AMT ");

                sb.AppendLine("     ,ALL_INPUT_AMT ");
                sb.AppendLine("     ,ALL_LOSS ");
                sb.AppendLine("     ,ALL_RS_INPUT_AMT ");
                sb.AppendLine("     ,PLAN_DATE ");
                sb.AppendLine("     ,PLAN_CD ");
                sb.AppendLine("     ,INSTAFF ");
                sb.AppendLine("     ,INTIME ");
                sb.AppendLine("     ,COMPLETE_YN ");
                sb.AppendLine(" ) values ( ");
                sb.AppendLine("     '" + work_date + "' ");
                sb.AppendLine("    ,@w_cd ");
                sb.AppendLine("    ,' ' ");
                sb.AppendLine("    ,'" + item_cd + "' ");
                sb.AppendLine("    ,'" + target_amt + "' ");
                sb.AppendLine("    ,'" + rs_amt + "' ");
                sb.AppendLine("    ,'" + all_input_amt + "' ");
                sb.AppendLine("    ,'" + all_loss + "' ");
                sb.AppendLine("    ,'" + all_rs_input_amt + "' ");
                sb.AppendLine("    ,'" + plan_date + "' ");
                sb.AppendLine("    ,'" + plan_cd + "' ");
                sb.AppendLine("     ,'" + Common.p_strStaffNo + "' ");
                sb.AppendLine("     ,convert(varchar, getdate(), 120) ");
                sb.AppendLine("     , 'N' ");
                sb.AppendLine("  ) ");


                if (rawGrid.Rows.Count > 0)
                {
                    for (int i = 0; i < rawGrid.Rows.Count; i++)
                    {
                        sb.AppendLine("declare @w_raw_seq" + i + " int ");
                        sb.AppendLine("select @w_raw_seq" + i + " =ISNULL(MAX(SEQ),0)+1 from F_WORK_RESULT_RAW ");
                        sb.AppendLine("where WORK_DATE = '" + work_date + "' ");
                        sb.AppendLine("and WORK_CD =  @w_cd ");


                        sb.AppendLine(" insert into F_WORK_RESULT_RAW( ");
                        sb.AppendLine("     WORK_DATE");
                        sb.AppendLine("     ,WORK_CD ");
                        sb.AppendLine("     ,SEQ ");
                        sb.AppendLine("     ,RAW_MAT_CD ");
                        sb.AppendLine("     ,RS_AMT ");
                        sb.AppendLine("     ,PLAN_DATE ");
                        sb.AppendLine("     ,PLAN_CD ");
                        sb.AppendLine("     ,PLAN_SEQ ");
                        sb.AppendLine("     ,INSTAFF ");
                        sb.AppendLine("     ,INTIME ");
                        sb.AppendLine(" ) values ( ");
                        sb.AppendLine("     '" + work_date + "' ");
                        sb.AppendLine("    ,@w_cd ");
                        sb.AppendLine("    ,@w_raw_seq" + i + " ");
                        sb.AppendLine("    ,'" + (string)rawGrid.Rows[i].Cells["RAW_MAT_CD"].Value + "' ");
                        sb.AppendLine("     ,'" + ((string)rawGrid.Rows[i].Cells["RS_AMT"].Value.ToString()).Replace(",", "") + "' ");
                        sb.AppendLine("    ,'" + plan_date + "' ");
                        sb.AppendLine("    ,'" + plan_cd + "' ");
                        sb.AppendLine("    ,'" + (string)rawGrid.Rows[i].Cells["PLAN_SEQ"].Value + "' ");
                        sb.AppendLine("     ,'" + Common.p_strStaffNo + "' ");
                        sb.AppendLine("     ,convert(varchar, getdate(), 120) ");
                        sb.AppendLine("  ) ");
                    }
                }

                if (flowGrid.Rows.Count > 0)
                {
                    for (int i = 0; i < flowGrid.Rows.Count; i++)
                    {
                        sb.AppendLine("declare @w_flow_seq" + i + " int ");
                        sb.AppendLine("select @w_flow_seq" + i + " =ISNULL(MAX(SEQ),0)+1 from F_WORK_RESULT_FLOW ");
                        sb.AppendLine("where WORK_DATE = '" + work_date + "' ");
                        sb.AppendLine("and WORK_CD =  @w_cd ");

                        sb.AppendLine(" insert into F_WORK_RESULT_FLOW( ");
                        sb.AppendLine("     WORK_DATE");
                        sb.AppendLine("     ,WORK_CD ");
                        sb.AppendLine("     ,SEQ ");
                        sb.AppendLine("     ,FLOW_CD ");
                        sb.AppendLine("     ,INPUT_AMT ");
                        sb.AppendLine("     ,LOSS ");
                        sb.AppendLine("     ,RS_INPUT_AMT ");
                        sb.AppendLine("     ,INSTAFF ");
                        sb.AppendLine("     ,INTIME ");
                        sb.AppendLine(" ) values ( ");
                        sb.AppendLine("     '" + work_date + "' ");
                        sb.AppendLine("    ,@w_cd ");
                        sb.AppendLine("    ,@w_flow_seq" + i + " ");
                        sb.AppendLine("    ,'" + (string)flowGrid.Rows[i].Cells["FLOW_CD"].Value + "' ");
                        sb.AppendLine("     ,'" + ((string)flowGrid.Rows[i].Cells["INPUT_AMT"].Value.ToString()).Replace(",", "") + "' ");
                        sb.AppendLine("     ,'" + ((string)flowGrid.Rows[i].Cells["LOSS"].Value.ToString()).Replace(",", "") + "' ");
                        sb.AppendLine("     ,'" + ((string)flowGrid.Rows[i].Cells["RS_INPUT_AMT"].Value.ToString()).Replace(",", "") + "' ");
                        sb.AppendLine("     ,'" + Common.p_strStaffNo + "' ");
                        sb.AppendLine("     ,convert(varchar, getdate(), 120) ");
                        sb.AppendLine("  ) ");
                    }
                }

                sb.AppendLine("update F_PLAN set");
                sb.AppendLine("      COMPLETE_YN = 'Y' ");  // RAW_OUT_YN = 'Y', 
                sb.AppendLine("where PLAN_DATE = '" + plan_date + "' ");
                sb.AppendLine(" and PLAN_CD = '" + plan_cd + "' ");

                
                //COMPLETE_YN 디폴트값 설정
                //sb.AppendLine("     ALTER TABLE F_WORK_RESULT ");
                //sb.AppendLine("        ADD CONSTRAINT com_yn ");
                //sb.AppendLine("          DEFAULT 'N' FOR COMPLETE_YN; ");
                //        for (int i = 0; i < rawGrid.Rows.Count; i++)
                //        {
                //            sb.AppendLine("declare @work_seq" + i + " int ");
                //            sb.AppendLine("select @work_seq" + i + " =ISNULL(MAX(SEQ),0)+1 from F_WORK_INST_DETAIL ");
                //            sb.AppendLine("where W_INST_DATE = '" + txt_work_date + "' ");
                //            sb.AppendLine("and W_INST_CD =  @seq1 ");

                //            sb.AppendLine("insert into F_WORK_INST_DETAIL(");
                //            sb.AppendLine("     W_INST_DATE ");
                //            sb.AppendLine("     ,W_INST_CD ");
                //            sb.AppendLine("     ,SEQ ");
                //            sb.AppendLine("     ,LOT_NO ");
                //            sb.AppendLine("     ,RAW_MAT_CD ");
                //            sb.AppendLine("     ,SOYO_AMT ");
                //            sb.AppendLine("     ,TOTAL_AMT ");
                //            sb.AppendLine("     ,INSTAFF");
                //            sb.AppendLine("     ,INTIME");
                //            sb.AppendLine("  )values ( ");
                //            sb.AppendLine("     '" + work_date + "' ");
                //            sb.AppendLine("      ,@seq1 ");
                //            sb.AppendLine("     ,@work_seq" + i + " ");
                //            sb.AppendLine("     ,'" + txt_lot_no + "'+RIGHT('000'+ convert(varchar, @seq), 4) ");
                //            sb.AppendLine("     ,'" + ((string)rawGrid.Rows[i].Cells["RAW_MAT_CD"].Value).Replace(",", "") + "' ");
                //            sb.AppendLine("     ,'" + ((string)rawGrid.Rows[i].Cells["SOYO_AMT"].Value).Replace(",", "") + "' ");
                //            sb.AppendLine("     ,'" + ((string)rawGrid.Rows[i].Cells["TOTAL_SOYO_AMT"].Value).Replace(",", "") + "' ");
                //            sb.AppendLine("     ,'" + Common.p_strStaffNo + "' ");
                //            sb.AppendLine("     ,convert(varchar, getdate(), 120) ");

                //            sb.AppendLine("  )");
                //        }
                //    }

                //    if (flowGrid.Rows.Count > 0)
                //    {
                //        for (int i = 0; i < flowGrid.Rows.Count; i++)
                //        {
                //            sb.AppendLine("declare @half_seq" + i + " int ");
                //            sb.AppendLine("select @half_seq" + i + " =ISNULL(MAX(SEQ),0)+1 from F_WORK_INST_HALF_DETAIL ");
                //            sb.AppendLine("where W_INST_DATE = '" + work_date + "' ");
                //            sb.AppendLine("and W_INST_CD =  @seq1 ");

                //            sb.AppendLine("insert into F_WORK_INST_HALF_DETAIL(");
                //            sb.AppendLine("     W_INST_DATE ");
                //            sb.AppendLine("     ,W_INST_CD ");
                //            sb.AppendLine("     ,SEQ ");
                //            sb.AppendLine("     ,LOT_NO ");
                //            sb.AppendLine("     ,HALF_ITEM_CD ");
                //            sb.AppendLine("     ,SOYO_AMT ");
                //            sb.AppendLine("     ,TOTAL_AMT ");
                //            sb.AppendLine("     ,INSTAFF");
                //            sb.AppendLine("     ,INTIME");
                //            sb.AppendLine("  )values ( ");
                //            sb.AppendLine("     '" + work_date + "' ");
                //            sb.AppendLine("      ,@seq1 ");
                //            sb.AppendLine("     ,@half_seq" + i + " ");
                //            sb.AppendLine("     ,'" + txt_lot_no + "'+RIGHT('000'+ convert(varchar, @seq), 4) ");
                //            sb.AppendLine("     ,'" + ((string)flowGrid.Rows[i].Cells["HALF_ITEM_CD"].Value).Replace(",", "") + "' ");
                //            sb.AppendLine("     ,'" + ((string)flowGrid.Rows[i].Cells["H_SOYO_AMT"].Value).Replace(",", "") + "' ");
                //            sb.AppendLine("     ,'" + ((string)flowGrid.Rows[i].Cells["H_TOTAL_SOYO_AMT"].Value).Replace(",", "") + "' ");
                //            sb.AppendLine("     ,'" + Common.p_strStaffNo + "' ");
                //            sb.AppendLine("     ,convert(varchar, getdate(), 120) ");

                //            sb.AppendLine("  )");
                //        }
                //    }


                SqlCommand sCommand = new SqlCommand(sb.ToString());

                //    sCommand.Parameters.AddWithValue("@W_INST_DATE", work_date);
                //    sCommand.Parameters.AddWithValue("@ITEM_CD", txt_item_cd);
                //    sCommand.Parameters.AddWithValue("@CUST_CD", txt_cust_cd);
                //    sCommand.Parameters.AddWithValue("@INST_AMT", txt_inst_amt.Replace(",", ""));
                //    sCommand.Parameters.AddWithValue("@DELIVERY_DATE", deliver_req_date);
                //    sCommand.Parameters.AddWithValue("@LINE_CD", cmb_line);
                //    sCommand.Parameters.AddWithValue("@WORKER_CD", cmb_worker);
                //    sCommand.Parameters.AddWithValue("@PLAN_NUM", txt_plan_num);
                //    sCommand.Parameters.AddWithValue("@PLAN_ITEM", txt_plan_item);
                //    sCommand.Parameters.AddWithValue("@CHARGE_AMT", txt_char_amt.Replace(",", ""));
                //    sCommand.Parameters.AddWithValue("@PACK_AMT", txt_pack_amt.Replace(",", ""));
                //    sCommand.Parameters.AddWithValue("@INST_NOTICE", txt_inst_notice);
                int qResult = wAdo.SqlCommandEtc(sCommand, "insert_WORK_TB");
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

        public int insertWorkRsState(string work_date, string work_cd, string item_cd, string item_rs_amt)
        {
            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                sb = new StringBuilder();
                sb.AppendLine("declare @input_cd int ");
                sb.AppendLine("select @input_cd =ISNULL(MAX(WORK_CD),0)+1 from F_ITEM_INPUT ");
                sb.AppendLine("where INPUT_DATE = '" + work_date + "' ");


                sb.AppendLine(" insert into F_ITEM_INPUT( ");
                sb.AppendLine("     INPUT_DATE");
                sb.AppendLine("     ,INPUT_CD ");
                sb.AppendLine("     ,ITEM_CD ");
                sb.AppendLine("     ,INPUT_AMT ");
                sb.AppendLine("     ,WORK_DATE ");
                sb.AppendLine("     ,WORK_CD ");
                sb.AppendLine("     ,INSTAFF ");
                sb.AppendLine("     ,INTIME ");
                sb.AppendLine("     ,CURR_AMT ");
                sb.AppendLine("     ,COMPLETE_YN ");
                sb.AppendLine(" ) values ( ");
                sb.AppendLine("     CONVERT(date,GETDATE()) ");
                sb.AppendLine("    ,@input_cd ");
                sb.AppendLine("    ,'" + item_cd + "' ");
                sb.AppendLine("    ,'" + item_rs_amt + "' ");
                sb.AppendLine("    ,'" + work_date + "' ");
                sb.AppendLine("    ,'" + work_cd + "' ");
                sb.AppendLine("     ,'" + Common.p_strStaffNo + "' ");
                sb.AppendLine("     ,convert(varchar, getdate(), 120) ");
                sb.AppendLine("    ,'" + item_rs_amt + "' ");
                sb.AppendLine("    ,'N' ");
                sb.AppendLine("  ) ");


                sb.AppendLine("update F_WORK_RESULT set");
                sb.AppendLine("      COMPLETE_YN = 'Y' "); 
                sb.AppendLine("where WORK_DATE = '" + work_date + "' ");
                sb.AppendLine(" and WORK_CD = '" + work_cd + "' ");


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

    }
}
