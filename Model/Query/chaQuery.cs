using 스마트팩토리.CLS;
using 스마트팩토리.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 스마트팩토리.Model.Query
{ 
    class chaQuery
    {
        wnAdo wAdo = new wnAdo();
        #region staff logic

        #region insertstaff logic

        public int insertStaff(
            string txt_user_cd
            , string txt_user_nm
            , string txt_id
            , string txt_birth
            , string txt_zipcode
            , string txt_add
            , string txt_detail_add
            , string dtp_join_date
            , string chk_leave
            , string dtp_leave_date
            , string cmb_div
            , string cmb_pos
            , string txt_comment
            , string txt_pw
            , string txt_homephone
            , string txt_cellphone)
        {

            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                sb.AppendLine(" select count(*) as cnt");
                sb.AppendLine(" from N_STAFF_CODE");
                sb.AppendLine(" where STAFF_CD = '" + txt_user_cd + "'");
                // sb.AppendLine(" and STAFF_CD = '" + txt_user_cd + "'");

                SqlCommand sCommand = new SqlCommand(sb.ToString());
                if (sCommand.CommandText.Equals(null))
                {
                    wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                    return 2;
                }
                DataTable dt = wAdo.SqlCommandSelect(sCommand);

                if (!dt.Rows[0]["cnt"].ToString().Equals("0"))
                {
                    return 3;
                }

                sb = new StringBuilder();
                sb.AppendLine("insert into N_STAFF_CODE(");
                sb.AppendLine("     STAFF_CD ");
                sb.AppendLine("     ,STAFF_NM ");
                sb.AppendLine("     ,BIRTH_DATE ");
                sb.AppendLine("     ,JOIN_DATE ");
                sb.AppendLine("     ,POST_NO ");
                sb.AppendLine("     ,ADDR1 ");
                sb.AppendLine("     ,ADDR2 ");
                sb.AppendLine("     ,OUT_YN ");
                sb.AppendLine("     ,OUT_DATE ");
                sb.AppendLine("     ,CALL_NUM ");
                sb.AppendLine("     ,PHONE_NUM ");
                sb.AppendLine("     ,DEPT_CD ");
                sb.AppendLine("     ,POS_CD ");
                sb.AppendLine("     ,LOGIN_ID ");
                sb.AppendLine("     ,PW ");
                sb.AppendLine("     ,COMMENT ");
                sb.AppendLine(" ) values ( ");
                sb.AppendLine("     @STAFF_CD ");
                sb.AppendLine("     ,@STAFF_NM ");
                sb.AppendLine("     ,@BIRTH_DATE ");
                sb.AppendLine("     ,@JOIN_DATE ");
                sb.AppendLine("     ,@POST_NO ");
                sb.AppendLine("     ,@ADDR1 ");
                sb.AppendLine("     ,@ADDR2 ");
                sb.AppendLine("     ,@OUT_YN ");
                sb.AppendLine("     ,@OUT_DATE ");
                sb.AppendLine("     ,@CALL_NUM ");
                sb.AppendLine("     ,@PHONE_NUM ");
                sb.AppendLine("     ,@DEPT_CD ");
                sb.AppendLine("     ,@POS_CD ");
                sb.AppendLine("     ,@LOGIN_ID ");
                sb.AppendLine("     ,@PW ");
                sb.AppendLine("     ,@COMMENT ");
                sb.AppendLine(" ) ");

                sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@STAFF_CD", txt_user_cd);
                sCommand.Parameters.AddWithValue("@STAFF_NM", txt_user_nm);
                sCommand.Parameters.AddWithValue("@BIRTH_DATE", txt_birth);
                sCommand.Parameters.AddWithValue("@JOIN_DATE", dtp_join_date);
                sCommand.Parameters.AddWithValue("@POST_NO", txt_zipcode);
                sCommand.Parameters.AddWithValue("@ADDR1", txt_add);
                sCommand.Parameters.AddWithValue("@ADDR2", txt_detail_add);
                sCommand.Parameters.AddWithValue("@OUT_YN", chk_leave);
                sCommand.Parameters.AddWithValue("@OUT_DATE", dtp_leave_date);
                sCommand.Parameters.AddWithValue("@CALL_NUM", txt_homephone);
                sCommand.Parameters.AddWithValue("@PHONE_NUM", txt_cellphone);
                sCommand.Parameters.AddWithValue("@DEPT_CD ", cmb_div);
                sCommand.Parameters.AddWithValue("@POS_CD", cmb_pos);
                sCommand.Parameters.AddWithValue("@LOGIN_ID", txt_id);
                sCommand.Parameters.AddWithValue("@PW", txt_pw);
                sCommand.Parameters.AddWithValue("@COMMENT", txt_comment);

                int qResult = wAdo.SqlCommandEtc(sCommand, "insert_USER_TB");
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

        #endregion insertstaff logic

        #region deletestaff logic

        public int deleteStaff(string txt_user_cd)
        {
            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                sb = new StringBuilder();
                sb.AppendLine("delete from N_STAFF_CODE ");
                sb.AppendLine("    where STAFF_CD = @STAFF_CD  ");

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@STAFF_CD", txt_user_cd);

                int qResult = wAdo.SqlCommandEtc(sCommand, "delete_USER_TB");
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

        #endregion deletestaff logic

        #region selectstaff logic

        public DataTable fn_Staff_List(string condition)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select A.STAFF_CD");
            sb.AppendLine(" ,A.STAFF_NM");
            sb.AppendLine(" ,A.BIRTH_DATE");
            sb.AppendLine(" ,A.JOIN_DATE");
            sb.AppendLine(" ,A.POST_NO");
            sb.AppendLine(" ,A.ADDR1");
            sb.AppendLine(" ,A.ADDR2");
            sb.AppendLine(" ,A.OUT_YN");
            sb.AppendLine(" ,A.OUT_DATE");
            sb.AppendLine(" ,A.CALL_NUM");
            sb.AppendLine(" ,A.PHONE_NUM");
            sb.AppendLine(" ,A.DEPT_CD");
            //sb.AppendLine(" ,(SELECT DEPT_NM FROM N_DEPT_CODE WHERE DEPT_CD = A.DEPT_CD) AS DEPT_NM");
            sb.AppendLine(" ,A.POS_CD");
            //sb.AppendLine(" ,(SELECT STORAGE_NM FROM N_STORAGE_CODE WHERE STORAGE_CD = A.STORAGE_CD) AS STORAGE_NM");
            sb.AppendLine(" ,A.LOGIN_ID");
            sb.AppendLine(" ,A.PW");
            sb.AppendLine(" ,A.COMMENT");
            sb.AppendLine(" from N_STAFF_CODE A ");
            sb.AppendLine(condition);
            //sb.AppendLine(" order by CAST(A.STAFF_CD as int) ");


            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }

            return wAdo.SqlCommandSelect(sCommand);
        }

        #endregion selectstaff logic

        #region updatestaff logic

        public int updateStaff(
           string txt_user_cd
            , string txt_user_nm
            , string txt_id
            , string txt_birth
            , string txt_zipcode
            , string txt_add
            , string txt_detail_add
            , string dtp_join_date
            , string chk_leave
            , string dtp_leave_date
            , string cmb_div
            , string cmb_pos
            , string txt_comment
            , string txt_pw
            , string txt_homephone
            , string txt_cellphone
           )
        {
            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                sb = new StringBuilder();
                sb.AppendLine("update N_STAFF_CODE set");
                sb.AppendLine("     STAFF_CD = @STAFF_CD ");
                sb.AppendLine("     ,STAFF_NM = @STAFF_NM ");
                sb.AppendLine("     ,BIRTH_DATE = @BIRTH_DATE ");
                sb.AppendLine("     ,JOIN_DATE  = @JOIN_DATE");
                sb.AppendLine("     ,POST_NO  = @POST_NO");
                sb.AppendLine("     ,ADDR1  = @ADDR1");
                sb.AppendLine("     ,ADDR2  = @ADDR2");
                sb.AppendLine("     ,OUT_YN  = @OUT_YN");
                sb.AppendLine("     ,OUT_DATE  = @OUT_DATE");
                sb.AppendLine("     ,CALL_NUM  = @CALL_NUM");
                sb.AppendLine("     ,PHONE_NUM = @PHONE_NUM ");
                sb.AppendLine("     ,DEPT_CD = @DEPT_CD ");
                sb.AppendLine("     ,POS_CD = @POS_CD");
                sb.AppendLine("     ,LOGIN_ID = @LOGIN_ID ");
                sb.AppendLine("     ,PW = @PW");
                sb.AppendLine("     ,COMMENT = @COMMENT ");
                sb.AppendLine(" where STAFF_CD = @STAFF_CD");

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@STAFF_CD", txt_user_cd);
                sCommand.Parameters.AddWithValue("@STAFF_NM", txt_user_nm);
                sCommand.Parameters.AddWithValue("@BIRTH_DATE", txt_birth);
                sCommand.Parameters.AddWithValue("@JOIN_DATE", dtp_join_date);
                sCommand.Parameters.AddWithValue("@POST_NO", txt_zipcode);
                sCommand.Parameters.AddWithValue("@ADDR1", txt_add);
                sCommand.Parameters.AddWithValue("@ADDR2", txt_detail_add);
                sCommand.Parameters.AddWithValue("@OUT_YN", chk_leave);
                sCommand.Parameters.AddWithValue("@OUT_DATE", dtp_leave_date);
                sCommand.Parameters.AddWithValue("@CALL_NUM", txt_homephone);
                sCommand.Parameters.AddWithValue("@PHONE_NUM", txt_cellphone);
                sCommand.Parameters.AddWithValue("@DEPT_CD", cmb_div);
                sCommand.Parameters.AddWithValue("@POS_CD", cmb_pos);
                sCommand.Parameters.AddWithValue("@LOGIN_ID", txt_id);
                sCommand.Parameters.AddWithValue("@PW", txt_pw);
                sCommand.Parameters.AddWithValue("@COMMENT", txt_comment);
                int qResult = wAdo.SqlCommandEtc(sCommand, "update_USER_TB");
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

        #endregion updatestaff logic

        #endregion staff logic

        #region itemcode logic

        #region insertitem logic

       public int insertItem(
            string txt_itemcd
            , string txt_itemnm
            , string txt_spec
            , double txt_weight
            , string txt_shelf
            , double txt_stock
            , string txt_comment
            , string dtp_reg_date
            , string cmb_fac
            , string cmb_stor    
            , string cmb_unit  
            , string cmb_tax                  
            , string cmb_chk_use
          //  , string cmb_cust
            , conDataGridView dgv_num
            , conDataGridView dgv_raw
            , conDataGridView dgv_pro
            , conDataGridView dgv_lang
            , byte[] img          
            
            )                        
        {

            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                sb.AppendLine(" select count(*) as cnt");
                sb.AppendLine(" from N_ITEM_CODE ");
                sb.AppendLine(" where ITEM_CD = '" + txt_itemcd + "'");
                // sb.AppendLine(" and STAFF_CD = '" + txt_user_cd + "'");

                SqlCommand sCommand = new SqlCommand(sb.ToString());
                if (sCommand.CommandText.Equals(null))
                {
                    wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                    return 2;
                }
                DataTable dt = wAdo.SqlCommandSelect(sCommand);

                if (!dt.Rows[0]["cnt"].ToString().Equals("0"))
                {
                    return 3;
                }

                sb = new StringBuilder();
                sb.AppendLine("insert into N_ITEM_CODE(");
                sb.AppendLine("     ITEM_CD ");
                sb.AppendLine("     ,ITEM_NM ");
                sb.AppendLine("     ,ITEM_GUBUN ");
                sb.AppendLine("     ,SPEC ");
                sb.AppendLine("     ,UNIT_CD ");
                sb.AppendLine("     ,PROP_STOCK ");
                sb.AppendLine("     ,EXP_DATE ");
                sb.AppendLine("     ,BAL_STOCK ");
                sb.AppendLine("     ,COMMENT ");
                sb.AppendLine("     ,F_PLAN_NUM ");
                sb.AppendLine("     ,LOC_CD ");
                sb.AppendLine("     ,CUST_CD ");
                sb.AppendLine("     ,TAX_CD ");
                sb.AppendLine("     ,ITEM_WEIGHT ");
                sb.AppendLine("     ,USED_CD ");
                sb.AppendLine("     ,INPUT_DATE ");
                sb.AppendLine("     ,ITEM_IMG ");
                sb.AppendLine("     ,B_BOX_AMT");
                sb.AppendLine("     ,B_BOX_CON_PRICE");
                sb.AppendLine("     ,B_BOX_BAR");
                sb.AppendLine("     ,S_BOX_AMT");
                sb.AppendLine("     ,S_BOX_CON_PRICE");
                sb.AppendLine("     ,S_BOX_BAR");
                sb.AppendLine("     ,UNIT_AMT");
                sb.AppendLine("     ,UNIT_CON_PRICE");
                sb.AppendLine("     ,UNIT_BOX_BAR");  
                sb.AppendLine(" ) values ( ");
                sb.AppendLine("     @ITEM_CD ");
                sb.AppendLine("     ,@ITEM_NM ");
                sb.AppendLine("     ,@ITEM_GUBUN ");
                sb.AppendLine("     ,@SPEC ");
                sb.AppendLine("     ,@UNIT_CD ");
                sb.AppendLine("     ,@PROP_STOCK ");
                sb.AppendLine("     ,@EXP_DATE ");
                sb.AppendLine("     ,@BAL_STOCK  ");
                sb.AppendLine("     ,@COMMENT ");
                sb.AppendLine("     ,@F_PLAN_NUM  ");
                sb.AppendLine("     ,@LOC_CD ");
                sb.AppendLine("     ,1 ");
                sb.AppendLine("     ,@TAX_CD ");
                sb.AppendLine("     ,@ITEM_WEIGHT ");
                sb.AppendLine("     ,@USED_CD ");
                sb.AppendLine("     ,@INPUT_DATE ");
                sb.AppendLine("     ,@ITEM_IMG ");
                sb.AppendLine("     ,@B_BOX_AMT");
                sb.AppendLine("     ,@B_BOX_CON_PRICE");
                sb.AppendLine("     ,@B_BOX_BAR");
                sb.AppendLine("     ,@S_BOX_AMT");
                sb.AppendLine("     ,@S_BOX_CON_PRICE");
                sb.AppendLine("     ,@S_BOX_BAR");
                sb.AppendLine("     ,@UNIT_AMT");
                sb.AppendLine("     ,@UNIT_CON_PRICE");
                sb.AppendLine("     ,@UNIT_BOX_BAR");
                sb.AppendLine(" ) ");

                if (dgv_raw.Rows.Count > 0)
                {
                    for (int i = 0; i < dgv_raw.Rows.Count; i++)
                    {
                        sb.AppendLine("declare @seq" + i + " int ");
                        sb.AppendLine("select @seq" + i + " =ISNULL(MAX(SEQ),0)+1 from N_ITEM_COMP ");
                        sb.AppendLine("where ITEM_CD = '" + txt_itemcd + "' ");

                        sb.AppendLine("insert into N_ITEM_COMP(");
                        sb.AppendLine("     ITEM_CD ");
                        sb.AppendLine("     ,SEQ ");
                        sb.AppendLine("     ,RAW_MAT_CD ");
                        sb.AppendLine("     ,ORD_NUM ");
                        sb.AppendLine("     ,COMBI_PERCENT ");
                        sb.AppendLine("     ,TOTAL_AMT ");
                        sb.AppendLine("  )values ( ");
                        sb.AppendLine("     '" + txt_itemcd + "' ");
                        sb.AppendLine("     ,@seq" + i + " ");
                        sb.AppendLine("     ,'" + dgv_raw.Rows[i].Cells["dgv_raw_cd"].Value + "'  ");
                        sb.AppendLine("     ,@seq" + i + " ");
                        sb.AppendLine("     ,'" + double.Parse(dgv_raw.Rows[i].Cells["dgv_raw_ratio"].Value.ToString()) + "'  ");
                        sb.AppendLine("     ,'" + double.Parse(dgv_raw.Rows[i].Cells["dgv_raw_out"].Value.ToString()) + "'  ");
                        //sb.AppendLine("  ,'" + dgv_raw.Rows[i].Cells["dgv_raw_cd"].Value + "'    ");//순서
                        //sb.AppendLine("  ,'" + dgv_raw.Rows[i].Cells["dgv_raw_ratio"].Value + "'    ");//배합비율
                        //sb.AppendLine("  ,'" + dgv_raw.Rows[i].Cells["dgv_raw_out"].Value + "'    ");
                       // sb.AppendLine("     ,'" + ((string)dgv_raw.Rows[i].Cells["dgv_raw_out"].Value).Replace(",", "") + "' ");
                        sb.AppendLine("  )");
                    }
                }

                if (dgv_pro.Rows.Count > 0)
                {
                    for (int i = 0; i < dgv_pro.Rows.Count; i++)
                    {
                        sb.AppendLine("declare @f_seq" + i + " int ");
                        sb.AppendLine("select @f_seq" + i + " =ISNULL(MAX(SEQ),0)+1 from N_ITEM_FLOW ");
                        sb.AppendLine("where ITEM_CD = '" + txt_itemcd + "' ");

                        sb.AppendLine("insert into N_ITEM_FLOW(");
                        sb.AppendLine("     ITEM_CD ");
                        sb.AppendLine("     ,SEQ ");
                        sb.AppendLine("     ,FLOW_CD ");
                        sb.AppendLine("     ,COMMENT ");                        
                        sb.AppendLine("  )values ( ");
                        sb.AppendLine("     '" + txt_itemcd + "' ");
                        sb.AppendLine("     ,@f_seq" + i + " ");
                        sb.AppendLine("     ,'" + dgv_pro.Rows[i].Cells["FLOW_CD"].Value + "' ");
                        sb.AppendLine("     ,'" + dgv_pro.Rows[i].Cells["FLOW_ETC"].Value + "' ");
                        
                        //sb.AppendLine("     ,'" + dgv_pro.Rows[i].Cells["dgv_pro_cd"].Value + "' ");
                        //sb.AppendLine("     ,'" + dgv_pro.Rows[i].Cells["dgv_pro_etc"].Value + "' ");                        

                        sb.AppendLine("  )");
                    }
                }


                

                //다국어 테이블
                if (dgv_lang.Rows.Count > 0)
                {
                    for (int i = 0; i < dgv_lang.Rows.Count; i++)
                    {
                        sb.AppendLine("declare @h_seq" + i + " int ");
                        sb.AppendLine("select @h_seq" + i + " =ISNULL(MAX(SEQ),0)+1 from N_ITEM_MULTI_LANG ");
                        sb.AppendLine("where ITEM_CD = '" + txt_itemcd + "' ");

                        sb.AppendLine("insert into N_ITEM_MULTI_LANG (");
                        sb.AppendLine("     ITEM_CD ");
                        sb.AppendLine("     ,SEQ ");
                        sb.AppendLine("     ,COUNTRY_CD ");
                        sb.AppendLine("     ,COUNTRY_ITEM_NM ");
                        sb.AppendLine("  )values ( ");
                        sb.AppendLine("     '" + txt_itemcd + "' ");
                        sb.AppendLine("     ,@h_seq" + i + " ");
                        sb.AppendLine("     ,'" + dgv_lang.Rows[i].Cells["COUNTRY_CD"].Value + "' ");
                        sb.AppendLine("     ,'" + dgv_lang.Rows[i].Cells["COUNTRY_ITEM_NM"].Value + "' ");
                        sb.AppendLine("  )");
                    }
                }

                sCommand = new SqlCommand(sb.ToString());

                //ITEM_GUBUN은 DEFAULT '1' (완제품) (화면표시 X)
                //TAX_CD '1' 과세 ‘2’ 면세 (410)

                //설비 어디로 넣어야 하는지?? 2019-11-28

                sCommand.Parameters.AddWithValue("@ITEM_CD", txt_itemcd);
                sCommand.Parameters.AddWithValue("@ITEM_NM", txt_itemnm);
                sCommand.Parameters.AddWithValue("@ITEM_GUBUN ", 1);
                sCommand.Parameters.AddWithValue("@SPEC", txt_spec);
                sCommand.Parameters.AddWithValue("@UNIT_CD", cmb_unit);
                sCommand.Parameters.AddWithValue("@PROP_STOCK", double.Parse(txt_stock.ToString()));
                sCommand.Parameters.AddWithValue("@EXP_DATE", txt_shelf);
                sCommand.Parameters.AddWithValue("@BAL_STOCK ", 0);
                sCommand.Parameters.AddWithValue("@COMMENT", txt_comment);
                sCommand.Parameters.AddWithValue("@F_PLAN_NUM ", cmb_fac);
                sCommand.Parameters.AddWithValue("@LOC_CD", cmb_stor);
                // sCommand.Parameters.AddWithValue("@CUST_CD ", cmb_cust );
                sCommand.Parameters.AddWithValue("@TAX_CD", cmb_tax);
                sCommand.Parameters.AddWithValue("@ITEM_WEIGHT", double.Parse(txt_weight.ToString()));
                sCommand.Parameters.AddWithValue("@USED_CD", cmb_chk_use );
                sCommand.Parameters.AddWithValue("@INPUT_DATE", dtp_reg_date);
                sCommand.Parameters.AddWithValue("@ITEM_IMG", img);
                sCommand.Parameters.AddWithValue("@B_BOX_AMT", double.Parse(dgv_num.Rows[0].Cells[1].Value.ToString()));
                sCommand.Parameters.AddWithValue("@B_BOX_CON_PRICE", double.Parse(dgv_num.Rows[0].Cells[2].Value.ToString()));
                sCommand.Parameters.AddWithValue("@B_BOX_BAR", dgv_num.Rows[0].Cells[3].Value);
                sCommand.Parameters.AddWithValue("@S_BOX_AMT", double.Parse(dgv_num.Rows[1].Cells[1].Value.ToString()));
                sCommand.Parameters.AddWithValue("@S_BOX_CON_PRICE", double.Parse(dgv_num.Rows[1].Cells[2].Value.ToString()));
                sCommand.Parameters.AddWithValue("@S_BOX_BAR", dgv_num.Rows[1].Cells[3].Value);
                sCommand.Parameters.AddWithValue("@UNIT_AMT", double.Parse(dgv_num.Rows[2].Cells[1].Value.ToString()));
                sCommand.Parameters.AddWithValue("@UNIT_CON_PRICE", double.Parse(dgv_num.Rows[2].Cells[2].Value.ToString()));
                sCommand.Parameters.AddWithValue("@UNIT_BOX_BAR", dgv_num.Rows[2].Cells[3].Value);

                int qResult = wAdo.SqlCommandEtc(sCommand, "insert_USER_TB");
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
        #endregion insertitem logic

        #region updateitem logic

        public int updateItem(
              string txt_itemcd
            , string txt_itemnm
            , string txt_spec
            , double txt_weight
            , string txt_shelf
            , double txt_stock
            , string txt_comment
            , string dtp_reg_date
            , string cmb_fac
            , string cmb_stor
            , string cmb_unit
            , string cmb_tax
            , string cmb_chk_use
            //  , string cmb_cust
            , conDataGridView dgv_num
            , conDataGridView dgv_raw
            , conDataGridView dgv_pro
            , conDataGridView dgv_lang
            , DataGridView del_dgvRaw
            , DataGridView del_dgvPro
            , DataGridView del_dgvLang
            , byte[] img
           )
        {
            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                sb = new StringBuilder();

                sb.AppendLine("update N_ITEM_CODE set");
                sb.AppendLine("     ITEM_CD  = @ITEM_CD  ");
                sb.AppendLine("     ,ITEM_NM = @ITEM_NM ");
                sb.AppendLine("     ,ITEM_GUBUN = @ITEM_GUBUN ");
                sb.AppendLine("     ,SPEC  = @SPEC");
                sb.AppendLine("     ,UNIT_CD  = @UNIT_CD");
                sb.AppendLine("     ,PROP_STOCK  = @PROP_STOCK");
                sb.AppendLine("     ,EXP_DATE  = @EXP_DATE");
                sb.AppendLine("     ,BAL_STOCK  = @BAL_STOCK");
                sb.AppendLine("     ,COMMENT  = @COMMENT");
                sb.AppendLine("     ,F_PLAN_NUM  = @F_PLAN_NUM");
                sb.AppendLine("     ,LOC_CD = @LOC_CD ");
                sb.AppendLine("     ,CUST_CD = 1 ");
                sb.AppendLine("     ,TAX_CD = @TAX_CD");
                sb.AppendLine("     ,ITEM_WEIGHT = @ITEM_WEIGHT ");
                sb.AppendLine("     ,USED_CD = @USED_CD");
                sb.AppendLine("     ,INPUT_DATE = @INPUT_DATE ");
                sb.AppendLine("     ,ITEM_IMG = @ITEM_IMG ");
                sb.AppendLine("     ,B_BOX_AMT = @B_BOX_AMT ");
                sb.AppendLine("     ,B_BOX_CON_PRICE = @B_BOX_CON_PRICE ");
                sb.AppendLine("     ,B_BOX_BAR = @B_BOX_BAR ");
                sb.AppendLine("     ,S_BOX_AMT = @S_BOX_AMT ");
                sb.AppendLine("     ,S_BOX_CON_PRICE = @S_BOX_CON_PRICE ");
                sb.AppendLine("     ,S_BOX_BAR = @S_BOX_BAR ");
                sb.AppendLine("     ,UNIT_AMT = @UNIT_AMT ");
                sb.AppendLine("     ,UNIT_CON_PRICE = @UNIT_CON_PRICE ");
                sb.AppendLine("     ,UNIT_BOX_BAR = @UNIT_BOX_BAR ");
                sb.AppendLine(" where ITEM_CD = @ITEM_CD");

                if (dgv_raw.Rows.Count > 0)
                {
                    for (int i = 0; i < dgv_raw.Rows.Count; i++)
                    {
                        string txt_seq = (string)dgv_raw.Rows[i].Cells["SEQ"].Value;
                        if (txt_seq == "" || txt_seq == null)
                        {
                            sb.AppendLine("declare @seq" + i + " int ");
                            sb.AppendLine("select @seq" + i + " =ISNULL(MAX(SEQ),0)+1 from N_ITEM_COMP ");
                            sb.AppendLine("where ITEM_CD = '" + txt_itemcd + "' ");

                            sb.AppendLine("insert into N_ITEM_COMP(");
                            sb.AppendLine("     ITEM_CD ");
                            sb.AppendLine("     ,SEQ ");
                            sb.AppendLine("     ,RAW_MAT_CD ");
                            sb.AppendLine("     ,ORD_NUM ");
                            sb.AppendLine("     ,COMBI_PERCENT ");
                            sb.AppendLine("     ,TOTAL_AMT ");
                            sb.AppendLine("  )values ( ");
                            sb.AppendLine("     '" + txt_itemcd + "' ");
                            sb.AppendLine("     ,@seq" + i + " ");
                            sb.AppendLine("     ,'" + dgv_raw.Rows[i].Cells["dgv_raw_cd"].Value + "'  "); //원부재료코드
                            sb.AppendLine("     ,@seq" + i + " "); //시퀀스랑 같은 값으로 일단 추가
                            sb.AppendLine("     ," + double.Parse(dgv_raw.Rows[i].Cells["dgv_raw_ratio"].Value.ToString()) + "  "); //배합량
                            sb.AppendLine("     ," + double.Parse(dgv_raw.Rows[i].Cells["dgv_raw_out"].Value.ToString()) + "  "); //사용량
                            sb.AppendLine("  )");
                        }
                        else
                        {
                            sb.AppendLine("update N_ITEM_COMP set");
                            sb.AppendLine("      RAW_MAT_CD =  '" + dgv_raw.Rows[i].Cells["dgv_raw_cd"].Value + "' ");
                            sb.AppendLine("     , COMBI_PERCENT = " + double.Parse(dgv_raw.Rows[i].Cells["dgv_raw_ratio"].Value.ToString()) + "  ");
                            sb.AppendLine("     , TOTAL_AMT = " + double.Parse(dgv_raw.Rows[i].Cells["dgv_raw_out"].Value.ToString()) + "  ");
                            sb.AppendLine(" where ITEM_CD = '" + txt_itemcd + "' ");
                            sb.AppendLine(" and SEQ = '" + dgv_raw.Rows[i].Cells["SEQ"].Value + "'");
                        }
                    }
                }

                if (dgv_pro.Rows.Count > 0)
                {
                    for (int i = 0; i < dgv_pro.Rows.Count; i++)
                    {
                        string txt_seq = (string)dgv_pro.Rows[i].Cells["SEQ1"].Value;
                        if (txt_seq == "" || txt_seq == null)
                        {
                            sb.AppendLine("declare @f_seq" + i + " int ");
                            sb.AppendLine("select @f_seq" + i + " =ISNULL(MAX(SEQ),0)+1 from N_ITEM_FLOW ");
                            sb.AppendLine("where ITEM_CD = '" + txt_itemcd + "' ");

                            sb.AppendLine("insert into N_ITEM_FLOW(");
                            sb.AppendLine("     ITEM_CD ");
                            sb.AppendLine("     ,SEQ ");
                            sb.AppendLine("     ,FLOW_CD ");
                            sb.AppendLine("     ,COMMENT ");
                            sb.AppendLine("  )values ( ");
                            sb.AppendLine("     '" + txt_itemcd + "' ");
                            sb.AppendLine("     ,@f_seq" + i + " ");
                            sb.AppendLine("     ,'"+dgv_pro.Rows[i].Cells["FLOW_CD"].Value+"' ");
                            sb.AppendLine("     ,'"+dgv_pro.Rows[i].Cells["FLOW_ETC"].Value+"' ");

                            sb.AppendLine("  )");
                        }
                        else
                        {
                            sb.AppendLine("update N_ITEM_FLOW set");
                            sb.AppendLine("      FLOW_CD =  '"+dgv_pro.Rows[i].Cells["FLOW_CD"].Value+"' ");
                            sb.AppendLine("     ,COMMENT = '" + dgv_pro.Rows[i].Cells["FLOW_ETC"].Value + "' ");
                            sb.AppendLine(" where ITEM_CD = '" + txt_itemcd + "' ");
                            sb.AppendLine(" and SEQ = '" + dgv_pro.Rows[i].Cells["SEQ1"].Value + "'");
                        }
                    }
                }

                if (dgv_lang.Rows.Count > 0)
                {
                    for (int i = 0; i < dgv_lang.Rows.Count; i++)
                    {
                        string txt_seq = (string)dgv_lang.Rows[i].Cells["SEQ2"].Value;
                        if (txt_seq == "" || txt_seq == null)
                        {
                            sb.AppendLine("declare @h_seq" + i + " int ");
                            sb.AppendLine("select @h_seq" + i + " =ISNULL(MAX(SEQ),0)+1 from N_ITEM_MULTI_LANG ");
                            sb.AppendLine("where ITEM_CD = '" + txt_itemcd + "' ");

                            sb.AppendLine("insert into N_ITEM_MULTI_LANG (");
                            sb.AppendLine("     ITEM_CD ");
                            sb.AppendLine("     ,SEQ ");
                            sb.AppendLine("     ,COUNTRY_CD ");
                            sb.AppendLine("     ,COUNTRY_ITEM_NM ");
                            sb.AppendLine("  )values ( ");
                            sb.AppendLine("     '" + txt_itemcd + "' ");
                            sb.AppendLine("     ,@h_seq" + i + " ");
                            sb.AppendLine("     ,'" + dgv_lang.Rows[i].Cells["COUNTRY_CD"].Value + "' ");
                            sb.AppendLine("     ,'" + dgv_lang.Rows[i].Cells["COUNTRY_ITEM_NM"].Value + "' ");
                            sb.AppendLine("  )");
                        }
                        else
                        {
                            sb.AppendLine("update N_ITEM_MULTI_LANG set");
                            sb.AppendLine("     COUNTRY_CD =  '" + dgv_lang.Rows[i].Cells["COUNTRY_CD"].Value + "' ");
                            sb.AppendLine("     ,COUNTRY_ITEM_NM =  '" + dgv_lang.Rows[i].Cells["COUNTRY_ITEM_NM"].Value + "' ");
                            sb.AppendLine(" where ITEM_CD = '" + txt_itemcd + "' ");
                            sb.AppendLine(" and SEQ = '" + dgv_lang.Rows[i].Cells["SEQ2"].Value + "'");

                        }
                    }
                }
                if (del_dgvRaw.Rows.Count > 0)
                {
                    for (int i = 0; i < del_dgvRaw.Rows.Count; i++)
                    {
                        sb.AppendLine("delete from N_ITEM_COMP ");
                        sb.AppendLine("    where ITEM_CD = '" + txt_itemcd + "' ");
                        sb.AppendLine("     and SEQ = '" + del_dgvRaw.Rows[i].Cells["SEQ"].Value + "' ");
                    }
                }
                if (del_dgvPro.Rows.Count > 0)
                {
                    for (int i = 0; i < del_dgvPro.Rows.Count; i++)
                    {
                        sb.AppendLine("delete from N_ITEM_FLOW ");
                        sb.AppendLine("    where ITEM_CD = '" + txt_itemcd + "' ");
                        sb.AppendLine("     and SEQ = '" + del_dgvPro.Rows[i].Cells["SEQ"].Value + "' ");
                    }
                }

                if (del_dgvLang.Rows.Count > 0)
                {
                    for (int i = 0; i < del_dgvLang.Rows.Count; i++)
                    {
                        sb.AppendLine("delete from N_ITEM_MULTI_LANG ");
                        sb.AppendLine("    where ITEM_CD = '" + txt_itemcd + "' ");
                        sb.AppendLine("     and SEQ = '" + del_dgvLang.Rows[i].Cells["SEQ"].Value + "' ");
                    }
                }
                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@ITEM_CD", txt_itemcd);
                sCommand.Parameters.AddWithValue("@ITEM_NM", txt_itemnm);
                sCommand.Parameters.AddWithValue("@ITEM_GUBUN ", 1);
                sCommand.Parameters.AddWithValue("@SPEC", txt_spec);
                sCommand.Parameters.AddWithValue("@UNIT_CD", cmb_unit);
                sCommand.Parameters.AddWithValue("@PROP_STOCK", double.Parse(txt_stock.ToString()));
                sCommand.Parameters.AddWithValue("@EXP_DATE", txt_shelf);
                sCommand.Parameters.AddWithValue("@BAL_STOCK ", 0);
                sCommand.Parameters.AddWithValue("@COMMENT", txt_comment);
                sCommand.Parameters.AddWithValue("@F_PLAN_NUM ", cmb_fac);
                sCommand.Parameters.AddWithValue("@LOC_CD", cmb_stor);
                // sCommand.Parameters.AddWithValue("@CUST_CD ", cmb_cust);
                sCommand.Parameters.AddWithValue("@TAX_CD", cmb_tax);
                sCommand.Parameters.AddWithValue("@ITEM_WEIGHT", double.Parse(txt_weight.ToString()));
                sCommand.Parameters.AddWithValue("@USED_CD", cmb_chk_use);
                sCommand.Parameters.AddWithValue("@INPUT_DATE", dtp_reg_date);
                sCommand.Parameters.AddWithValue("@ITEM_IMG", img);
                sCommand.Parameters.AddWithValue("@B_BOX_AMT", double.Parse(dgv_num.Rows[0].Cells[1].Value.ToString()));
                sCommand.Parameters.AddWithValue("@B_BOX_CON_PRICE", double.Parse(dgv_num.Rows[0].Cells[2].Value.ToString()));
                sCommand.Parameters.AddWithValue("@B_BOX_BAR", dgv_num.Rows[0].Cells[3].Value);
                sCommand.Parameters.AddWithValue("@S_BOX_AMT", double.Parse(dgv_num.Rows[1].Cells[1].Value.ToString()));
                sCommand.Parameters.AddWithValue("@S_BOX_CON_PRICE", double.Parse(dgv_num.Rows[1].Cells[2].Value.ToString()));
                sCommand.Parameters.AddWithValue("@S_BOX_BAR", dgv_num.Rows[1].Cells[3].Value);
                sCommand.Parameters.AddWithValue("@UNIT_AMT", double.Parse(dgv_num.Rows[2].Cells[1].Value.ToString()));
                sCommand.Parameters.AddWithValue("@UNIT_CON_PRICE", double.Parse(dgv_num.Rows[2].Cells[2].Value.ToString()));
                sCommand.Parameters.AddWithValue("@UNIT_BOX_BAR", dgv_num.Rows[2].Cells[3].Value);

                int qResult = wAdo.SqlCommandEtc(sCommand, "update_USER_TB");
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

        #endregion updateitem logic

        #region deleteItem logic

        public int deleteItem(string txt_item_cd)
        {
            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                sb = new StringBuilder();
                sb.AppendLine("delete from N_ITEM_CODE ");
                sb.AppendLine("    where ITEM_CD = @ITEM_CD ");

                sb.AppendLine("delete from N_ITEM_COMP "); //제품구성
                sb.AppendLine("    where ITEM_CD = @ITEM_CD ");

                sb.AppendLine("delete from N_ITEM_FLOW "); //공정구성
                sb.AppendLine("    where ITEM_CD = @ITEM_CD ");

                sb.AppendLine("delete from N_ITEM_MULTI_LANG "); //국가
                sb.AppendLine("    where ITEM_CD = @ITEM_CD ");
       

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@ITEM_CD", txt_item_cd);

                int qResult = wAdo.SqlCommandEtc(sCommand, "delete_ITEM_TB");
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

        #endregion deleteItem logic

        #region selectitem logic

        public DataTable fn_item_List(string condition)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select ");
            sb.AppendLine(" A.ITEM_CD ");
            sb.AppendLine(" ,A.ITEM_NM");
            sb.AppendLine(" ,A.ITEM_GUBUN");
            sb.AppendLine(" ,A.SPEC");
            sb.AppendLine(" ,A.UNIT_CD");
            sb.AppendLine("  , (select UNIT_NM from N_UNIT_CODE where UNIT_CD = A.UNIT_CD) AS UNIT_NM  "); //정훈 추가
            sb.AppendLine(" ,A.PROP_STOCK");
            sb.AppendLine(" ,A.EXP_DATE");
            sb.AppendLine(" ,A.BAL_STOCK");
            sb.AppendLine(" ,A.COMMENT");
            sb.AppendLine(" ,A.F_PLAN_NUM");
            sb.AppendLine(" ,A.LOC_CD");
            sb.AppendLine(" ,A.CUST_CD");
            sb.AppendLine(" ,A.TAX_CD");
            sb.AppendLine(" ,A.ITEM_WEIGHT");
            sb.AppendLine(" ,A.USED_CD");
            sb.AppendLine(" ,A.INPUT_DATE");
            sb.AppendLine(" ,A.ITEM_IMG");
            sb.AppendLine(" ,A.B_BOX_AMT");
            sb.AppendLine(" ,A.B_BOX_CON_PRICE");
            sb.AppendLine(" ,A.B_BOX_BAR");
            sb.AppendLine(" ,A.S_BOX_AMT");
            sb.AppendLine(" ,A.S_BOX_CON_PRICE");
            sb.AppendLine(" ,A.S_BOX_BAR");
            sb.AppendLine(" ,A.UNIT_AMT");
            sb.AppendLine(" ,A.UNIT_CON_PRICE");
            sb.AppendLine(" ,A.UNIT_BOX_BAR");
            //sb.AppendLine(" ,(SELECT DEPT_NM FROM N_DEPT_CODE WHERE DEPT_CD = A.DEPT_CD) AS DEPT_NM");            
            //sb.AppendLine(" ,(SELECT STORAGE_NM FROM N_STORAGE_CODE WHERE STORAGE_CD = A.STORAGE_CD) AS STORAGE_NM");            
            sb.AppendLine(" from N_ITEM_CODE A ");
            sb.AppendLine(condition);
            //sb.AppendLine(" order by CAST(A.STAFF_CD as int) ");


            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }

            return wAdo.SqlCommandSelect(sCommand);
        }

        public DataTable fn_Item_Comp_List(string condition)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select ITEM_CD");
            sb.AppendLine("     ,A.SEQ ");
            sb.AppendLine("     ,A.RAW_MAT_CD");
            sb.AppendLine("     ,B.RAW_MAT_NM");
            sb.AppendLine("     ,A.ORD_NUM");
            sb.AppendLine("     ,A.COMBI_PERCENT");
            sb.AppendLine("     ,B.SPEC");
            sb.AppendLine("     ,B.INPUT_UNIT ");
            sb.AppendLine("     ,B.OUTPUT_UNIT ");
            sb.AppendLine("     ,(SELECT UNIT_NM FROM N_UNIT_CODE WHERE UNIT_CD = B.INPUT_UNIT) AS INPUT_UNIT_NM  ");
            sb.AppendLine("     ,(SELECT UNIT_NM FROM N_UNIT_CODE WHERE UNIT_CD = B.OUTPUT_UNIT) AS OUTPUT_UNIT_NM ");
            sb.AppendLine("     ,A.TOTAL_AMT ");
            sb.AppendLine(" from N_ITEM_COMP A ");
            sb.AppendLine(" LEFT OUTER JOIN N_RAW_CODE B ");
            sb.AppendLine(" ON A.RAW_MAT_CD = B.RAW_MAT_CD ");
            sb.AppendLine(condition);
            sb.AppendLine(" order by A.ITEM_CD ,A.SEQ");


            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }

            return wAdo.SqlCommandSelect(sCommand);
        }

        public DataTable fn_Item_Flow_List(string condition)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select ITEM_CD");
            sb.AppendLine("     ,A.SEQ ");
            sb.AppendLine("     ,A.FLOW_CD");
            sb.AppendLine("     ,B.FLOW_INSERT_YN");
            sb.AppendLine("     ,B.ITEM_IDEN_YN");
            //sb.AppendLine("     ,(SELECT FLOW_INSERT_YN FROM N_FLOW_CODE WHERE FLOW_CD = A.FLOW_CD)AS FLOW_INSERT_YN");
            sb.AppendLine("     ,A.COMMENT");
            sb.AppendLine("     ,B.FLOW_NM");
            sb.AppendLine("     ,C.TYPE_CD");
            sb.AppendLine(" from N_ITEM_FLOW A ");
            sb.AppendLine(" LEFT OUTER JOIN N_FLOW_CODE B ");
            sb.AppendLine(" ON A.FLOW_CD = B.FLOW_CD ");
            sb.AppendLine(" LEFT OUTER JOIN N_TYPE_CODE C  ");
            sb.AppendLine(" ON B.POOR_TYPE_CD = C.TYPE_CD ");
            //sb.AppendLine("     and C.POOR_TYPE_YN = 'Y' ");
            sb.AppendLine(condition);
            sb.AppendLine(" order by A.ITEM_CD,A.SEQ ");

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }

            return wAdo.SqlCommandSelect(sCommand);
        }


        public DataTable fn_Item_Lang_List(string condition)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select ITEM_CD");
            sb.AppendLine("     ,A.SEQ ");
            sb.AppendLine("     ,A.COUNTRY_CD");
            sb.AppendLine("     ,A.COUNTRY_ITEM_NM");
            //sb.AppendLine("     ,B.ITEM_IDEN_YN");
            //sb.AppendLine("     ,(SELECT FLOW_INSERT_YN FROM N_FLOW_CODE WHERE FLOW_CD = A.FLOW_CD)AS FLOW_INSERT_YN");
            sb.AppendLine("     ,B.COUNTRY_NM");

            sb.AppendLine(" from N_ITEM_MULTI_LANG A ");
            sb.AppendLine(" LEFT OUTER JOIN N_COUNTRY_CODE B  ");
            sb.AppendLine("  ON A.COUNTRY_CD = B.COUNTRY_CD  ");

            //sb.AppendLine("     and C.POOR_TYPE_YN = 'Y' ");
            sb.AppendLine(condition);
            sb.AppendLine(" order by A.ITEM_CD,A.SEQ  ");

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }

            return wAdo.SqlCommandSelect(sCommand);
        }

        #endregion selectitem logic

        #endregion itemcode logic

        #region selectfac logic
        public DataTable fn_Fac_Code_List(string condition)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select ");
            sb.AppendLine(" FAC_CD ");
            sb.AppendLine(" ,FAC_NM  ");
            sb.AppendLine(" ,MODEL_NM  ");
            sb.AppendLine(" ,SPEC  ");
            sb.AppendLine(" ,MAKER_NM  ");
            sb.AppendLine(" ,INPUT_DATE  ");
            sb.AppendLine(" ,INPUT_PRICE  ");
            sb.AppendLine(" ,DEPT_CD  ");
            sb.AppendLine(" ,USAGE  ");
            sb.AppendLine(" ,PRODUCT_AMT  ");
            sb.AppendLine(" ,POWER_NUM  ");
            sb.AppendLine(" ,MAIN_CLASS  ");
            sb.AppendLine(" ,CHECK_FLOW  ");
            sb.AppendLine(" ,CHECK_METHOD  ");
            sb.AppendLine(" ,CHECK_STAN  ");
            sb.AppendLine(" ,FAC_IMG  ");
            sb.AppendLine(" ,COMMENT  ");
            //sb.AppendLine(" ,(SELECT DEPT_NM FROM N_DEPT_CODE WHERE DEPT_CD = A.DEPT_CD) AS DEPT_NM");            
            //sb.AppendLine(" ,(SELECT STORAGE_NM FROM N_STORAGE_CODE WHERE STORAGE_CD = A.STORAGE_CD) AS STORAGE_NM");            
            sb.AppendLine(" from N_FAC_CODE  ");
            sb.AppendLine(condition);
            //sb.AppendLine(" order by CAST(A.STAFF_CD as int) ");

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }

            return wAdo.SqlCommandSelect(sCommand);
        }

        public DataTable fn_Fac_Set_List(string condition)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select ");
            sb.AppendLine(" FAC_CD ");
            sb.AppendLine(" ,FAC_SET_CD  ");
            sb.AppendLine(" ,FAC_SET_NM  ");
            sb.AppendLine(" ,VALUE1  ");
            sb.AppendLine(" ,VALUE2  ");
            sb.AppendLine(" ,VALUE3  ");
            sb.AppendLine(" ,VALUE4  ");
            sb.AppendLine(" ,VALUE5  ");
            sb.AppendLine(" ,VALUE6  ");
            sb.AppendLine(" ,VALUE7  ");
            sb.AppendLine(" ,VALUE8  ");
            sb.AppendLine(" ,VALUE9  ");
            sb.AppendLine(" ,VALUE10  ");
            sb.AppendLine(" from N_FAC_SETTING  ");

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }

            return wAdo.SqlCommandSelect(sCommand);
        }

        public DataTable fn_Fac_Class_List(string condition)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select ");
            sb.AppendLine(" FAC_CD ");
            sb.AppendLine(" ,SEQ  ");
            sb.AppendLine(" ,FAC_DATE  ");
            sb.AppendLine(" ,FAC_ITEM  ");
            sb.AppendLine(" ,FAC_COMMENT  ");
            sb.AppendLine(" ,FAC_ACTION  ");
            sb.AppendLine(" ,IMG_PREV  ");
            sb.AppendLine(" ,IMG_AFTER  ");
            sb.AppendLine(" ,INSTAFF  ");
            sb.AppendLine(" ,INTIME  ");
            sb.AppendLine(" ,UPSTAFF  ");
            sb.AppendLine(" ,UPTIME  ");
            sb.AppendLine(" from N_FAC_MAIN_CLASS  ");
            sb.AppendLine(condition);



            //sb.AppendLine(" order by CAST(A.STAFF_CD as int) ");

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }

            return wAdo.SqlCommandSelect(sCommand);
        }

        #endregion selectfac logic

        #region insertfac logic

        public int insertfac(
            string txt_faccd
            , string txt_facnm
            , string txt_modelnm
            , string txt_spec
            , string txt_makernm
            , string dtp_input_date
            , double txt_inputprice
            , string cmb_deptcd
            , string txt_usage
            , double txt_product_amt
            , double txt_power_num
            , string cmb_main_class
            , string cmb_chk_flow
            , string txt_chk_method
            , string txt_chk_stan
            , byte[] img
            , string txt_comment
            , conDataGridView dgv_fac_value
            , conDataGridView dgv_main_class
            , string dtp_chk_date
            , string txt_fac_item
            , string txt_fac_comment
            , string txt_fac_action
            , byte[] before_img
            , byte[] after_img)
        {

            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                sb.AppendLine(" select count(*) as cnt");
                sb.AppendLine(" from N_FAC_CODE");
                sb.AppendLine(" where FAC_CD = '" + txt_faccd + "'");
                // sb.AppendLine(" and STAFF_CD = '" + txt_user_cd + "'");

                SqlCommand sCommand = new SqlCommand(sb.ToString());
                if (sCommand.CommandText.Equals(null))
                {
                    wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                    return 2;
                }
                DataTable dt = wAdo.SqlCommandSelect(sCommand);

                if (!dt.Rows[0]["cnt"].ToString().Equals("0"))
                {
                    return 3;
                }

                sb = new StringBuilder();

                sb.AppendLine("insert into N_FAC_CODE( ");
                sb.AppendLine("     FAC_CD ");
                sb.AppendLine("     ,FAC_NM ");
                sb.AppendLine("     ,MODEL_NM ");
                sb.AppendLine("     ,SPEC ");
                sb.AppendLine("     ,MAKER_NM ");
                sb.AppendLine("     ,INPUT_DATE ");
                sb.AppendLine("     ,INPUT_PRICE ");
                sb.AppendLine("     ,DEPT_CD ");
                sb.AppendLine("     ,USAGE ");
                sb.AppendLine("     ,PRODUCT_AMT ");
                sb.AppendLine("     ,POWER_NUM ");
                sb.AppendLine("     ,MAIN_CLASS ");
                sb.AppendLine("     ,CHECK_FLOW ");
                sb.AppendLine("     ,CHECK_METHOD ");
                sb.AppendLine("     ,CHECK_STAN ");
                sb.AppendLine("     ,FAC_IMG ");
                sb.AppendLine("     ,COMMENT ");
                sb.AppendLine(" ) values ( ");
                sb.AppendLine("     '" + txt_faccd + "' ");
                sb.AppendLine("     ,@FAC_NM ");
                sb.AppendLine("     ,@MODEL_NM ");
                sb.AppendLine("     ,@SPEC ");
                sb.AppendLine("     ,@MAKER_NM ");
                sb.AppendLine("     ,@INPUT_DATE ");
                sb.AppendLine("     ,@INPUT_PRICE ");
                sb.AppendLine("     ,@DEPT_CD ");
                sb.AppendLine("     ,@USAGE ");
                sb.AppendLine("     ,@PRODUCT_AMT ");
                sb.AppendLine("     ,@POWER_NUM ");
                sb.AppendLine("     ,@MAIN_CLASS ");
                sb.AppendLine("     ,@CHECK_FLOW ");
                sb.AppendLine("     ,@CHECK_METHOD ");
                sb.AppendLine("     ,@CHECK_STAN ");
                sb.AppendLine("     ,@FAC_IMG ");
                sb.AppendLine("     ,@COMMENT ");
                sb.AppendLine(" ) ");

                if (dgv_fac_value.Rows.Count > 0)
                {
                    for (int i = 0; i < dgv_fac_value.Rows.Count-1; i++)
                    {
                        sb.AppendLine("declare @seq" + i + " int ");
                        sb.AppendLine("select @seq" + i + " =ISNULL(MAX(FAC_SET_CD),0)+1 from N_FAC_SETTING ");
                        sb.AppendLine("where FAC_CD = '" + txt_faccd + "' ");
                        //sb.AppendLine("and FAC_SET_CD = @seq");

                        sb.AppendLine("insert into N_FAC_SETTING( ");
                        sb.AppendLine("     FAC_CD ");
                        sb.AppendLine("     ,FAC_SET_CD ");
                        sb.AppendLine("     ,FAC_SET_NM ");
                        sb.AppendLine("     ,VALUE1 ");
                        sb.AppendLine("     ,VALUE2 ");
                        sb.AppendLine("     ,VALUE3 ");
                        sb.AppendLine("     ,VALUE4 ");
                        sb.AppendLine("     ,VALUE5 ");
                        sb.AppendLine("     ,VALUE6 ");
                        sb.AppendLine("     ,VALUE7 ");
                        sb.AppendLine("     ,VALUE8 ");
                        sb.AppendLine("     ,VALUE9 ");
                        sb.AppendLine("     ,VALUE10 ");
                        sb.AppendLine(" ) values ( ");
                        sb.AppendLine("     '" + txt_faccd + "' ");
                        sb.AppendLine("     ,@seq" + i + " ");
                        sb.AppendLine("     ,'" + dgv_fac_value.Rows[0].Cells["FAC_SET_NM"].Value + "' ");
                        sb.AppendLine("     ,'" + dgv_fac_value.Rows[0].Cells["S1"].Value + "' ");
                        sb.AppendLine("     ,'" + dgv_fac_value.Rows[0].Cells["S2"].Value + "' ");
                        sb.AppendLine("     ,'" + dgv_fac_value.Rows[0].Cells["S3"].Value + "' ");
                        sb.AppendLine("     ,'" + dgv_fac_value.Rows[0].Cells["S4"].Value + "' ");
                        sb.AppendLine("     ,'" + dgv_fac_value.Rows[0].Cells["S5"].Value + "' ");
                        sb.AppendLine("     ,'" + dgv_fac_value.Rows[0].Cells["S6"].Value + "' ");
                        sb.AppendLine("     ,'" + dgv_fac_value.Rows[0].Cells["S7"].Value + "' ");
                        sb.AppendLine("     ,'" + dgv_fac_value.Rows[0].Cells["S8"].Value + "' ");
                        sb.AppendLine("     ,'" + dgv_fac_value.Rows[0].Cells["S9"].Value + "' ");
                        sb.AppendLine("     ,'" + dgv_fac_value.Rows[0].Cells["S10"].Value + "' ");
                        sb.AppendLine(" ) ");
                    }
                }

                if (dgv_main_class.Rows.Count > 0)
                {
                    for (int i = 0; i < dgv_main_class.Rows.Count; i++)
                    {
                        sb.AppendLine("declare @seq1" + i + " int ");
                        sb.AppendLine("select @seq1" + i + " =ISNULL(MAX(SEQ),0)+1 from N_FAC_MAIN_CLASS ");
                        sb.AppendLine("where FAC_CD = " + txt_faccd + " ");
                        //sb.AppendLine("and SEQ = @seq0");

                        sb.AppendLine("insert into N_FAC_MAIN_CLASS(");
                        sb.AppendLine("     FAC_CD ");
                        sb.AppendLine("     ,SEQ ");
                        sb.AppendLine("     ,FAC_DATE ");
                        sb.AppendLine("     ,FAC_ITEM ");
                        sb.AppendLine("     ,FAC_COMMENT ");
                        sb.AppendLine("     ,FAC_ACTION ");
                        sb.AppendLine("     ,IMG_PREV ");
                        sb.AppendLine("     ,IMG_AFTER ");
                        sb.AppendLine(" ) values ( ");
                        sb.AppendLine("     '" + txt_faccd + "' ");
                        sb.AppendLine("     ,@seq1" + i + " ");
                        sb.AppendLine("     ,@FAC_DATE ");
                        sb.AppendLine("     ,@FAC_ITEM ");
                        sb.AppendLine("     ,@FAC_COMMENT ");
                        sb.AppendLine("     ,@FAC_ACTION ");
                        sb.AppendLine("     ,@IMG_PREV ");
                        sb.AppendLine("     ,@IMG_AFTER ");
                        sb.AppendLine(" ) ");
                    }
                }

                sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@FAC_CD", txt_faccd);
                sCommand.Parameters.AddWithValue("@FAC_NM", txt_facnm);
                sCommand.Parameters.AddWithValue("@MODEL_NM", txt_modelnm);
                sCommand.Parameters.AddWithValue("@SPEC", txt_spec);
                sCommand.Parameters.AddWithValue("@MAKER_NM", txt_makernm);
                sCommand.Parameters.AddWithValue("@INPUT_DATE", 0);
                sCommand.Parameters.AddWithValue("@INPUT_PRICE", double.Parse(txt_inputprice.ToString()));
                sCommand.Parameters.AddWithValue("@DEPT_CD", cmb_deptcd);
                sCommand.Parameters.AddWithValue("@USAGE", txt_usage);
                sCommand.Parameters.AddWithValue("@PRODUCT_AMT", double.Parse(txt_product_amt.ToString()));
                sCommand.Parameters.AddWithValue("@POWER_NUM", double.Parse(txt_power_num.ToString()));
                sCommand.Parameters.AddWithValue("@MAIN_CLASS ", cmb_main_class);
                sCommand.Parameters.AddWithValue("@CHECK_FLOW", cmb_chk_flow);
                sCommand.Parameters.AddWithValue("@CHECK_METHOD", txt_chk_method);
                sCommand.Parameters.AddWithValue("@CHECK_STAN", txt_chk_stan);
                sCommand.Parameters.AddWithValue("@FAC_IMG", img);
                sCommand.Parameters.AddWithValue("@COMMENT", txt_comment);
                sCommand.Parameters.AddWithValue("@FAC_SET_CD", txt_facnm);
                //sCommand.Parameters.AddWithValue("@FAC_SET_NM", dgv_fac_value.Rows[0].Cells["dgv_setnm"].ToString());
                //sCommand.Parameters.AddWithValue("@VALUE1", dgv_fac_value.Rows[0].Cells["SET1"].ToString());
                //sCommand.Parameters.AddWithValue("@VALUE2", dgv_fac_value.Rows[0].Cells["SET2"].ToString());
                //sCommand.Parameters.AddWithValue("@VALUE3", dgv_fac_value.Rows[0].Cells["SET3"].ToString());
                //sCommand.Parameters.AddWithValue("@VALUE4", dgv_fac_value.Rows[0].Cells["SET4"].ToString());
                //sCommand.Parameters.AddWithValue("@VALUE5", dgv_fac_value.Rows[0].Cells["SET5"].ToString());
                //sCommand.Parameters.AddWithValue("@VALUE6", dgv_fac_value.Rows[0].Cells["SET6"].ToString());
                //sCommand.Parameters.AddWithValue("@VALUE7", dgv_fac_value.Rows[0].Cells["SET7"].ToString());
                //sCommand.Parameters.AddWithValue("@VALUE8", dgv_fac_value.Rows[0].Cells["SET8"].ToString());
                //sCommand.Parameters.AddWithValue("@VALUE9", dgv_fac_value.Rows[0].Cells["SET9"].ToString());
                //sCommand.Parameters.AddWithValue("@VALUE10", dgv_fac_value.Rows[0].Cells["SET10"].ToString());
                sCommand.Parameters.AddWithValue("@FAC_DATE", dtp_chk_date);
                sCommand.Parameters.AddWithValue("@FAC_ITEM", txt_fac_item);
                sCommand.Parameters.AddWithValue("@FAC_COMMENT", txt_fac_comment);
                sCommand.Parameters.AddWithValue("@FAC_ACTION", txt_fac_action);
                if (before_img.Length < 2)
                {
                    before_img = new byte[0];
                }
                sCommand.Parameters.AddWithValue("@IMG_PREV", before_img);
                if (after_img.Length < 2)
                {
                    after_img = new byte[0];
                }
                sCommand.Parameters.AddWithValue("@IMG_AFTER", after_img);
                //sCommand.Parameters.AddWithValue("@INSTAFF", 0);
                //sCommand.Parameters.AddWithValue("@INTIME", 0);
                //sCommand.Parameters.AddWithValue("@UPSTAFF", 0);
                //sCommand.Parameters.AddWithValue("@UPTIME", 0);           

                int qResult = wAdo.SqlCommandEtc(sCommand, "insert_FAC_TB");
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

        #endregion insertfac logic

        #region deletefac logic

        public int deletefac(string txt_faccd)
        {
            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                sb = new StringBuilder();
                sb.AppendLine("delete from N_FAC_CODE ");
                sb.AppendLine("    where FAC_CD = @FAC_CD  ");
                sb.AppendLine("delete from N_FAC_SETTING ");
                sb.AppendLine("    where FAC_CD = @FAC_CD  ");
                sb.AppendLine("delete from N_FAC_MAIN_CLASS ");
                sb.AppendLine("    where FAC_CD = @FAC_CD  ");



                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@FAC_CD", txt_faccd);

                int qResult = wAdo.SqlCommandEtc(sCommand, "delete_FAC_TB");
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
        #endregion deletefac logic

        #region updatefac logic

        public int updatefac(
              string txt_faccd
            , string txt_facnm
            , string txt_modelnm
            , string txt_spec
            , string txt_makernm
            , string dtp_input_date
            , double txt_inputprice
            , string cmb_deptcd
            , string txt_usage
            , double txt_product_amt
            , double txt_power_num
            , string cmb_main_class
            , string cmb_chk_flow
            , string txt_chk_method
            , string txt_chk_stan
            , byte[] img
            , string txt_comment
            , conDataGridView dgv_fac_value
            , conDataGridView dgv_main_class
            , string dtp_chk_date
            , string txt_fac_item
            , string txt_fac_comment
            , string txt_fac_action
            , byte[] before_img
            , byte[] after_img)
        {
            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                sb = new StringBuilder();

                sb.AppendLine("update N_FAC_CODE set");
                sb.AppendLine("     FAC_CD  = @FAC_CD  ");
                sb.AppendLine("     ,FAC_NM = @FAC_NM ");
                sb.AppendLine("     ,MODEL_NM = @MODEL_NM ");
                sb.AppendLine("     ,SPEC  = @SPEC");
                sb.AppendLine("     ,MAKER_NM  = @MAKER_NM");
                sb.AppendLine("     ,INPUT_DATE  = @INPUT_DATE");
                sb.AppendLine("     ,INPUT_PRICE  = @INPUT_PRICE");
                sb.AppendLine("     ,DEPT_CD  = @DEPT_CD");
                sb.AppendLine("     ,USAGE  = @USAGE");
                sb.AppendLine("     ,PRODUCT_AMT  = @PRODUCT_AMT");
                sb.AppendLine("     ,POWER_NUM = @POWER_NUM ");
                sb.AppendLine("     ,MAIN_CLASS = MAIN_CLASS ");
                sb.AppendLine("     ,CHECK_FLOW = @CHECK_FLOW");
                sb.AppendLine("     ,CHECK_METHOD = @CHECK_METHOD ");
                sb.AppendLine("     ,CHECK_STAN = @CHECK_STAN");
                sb.AppendLine("     ,FAC_IMG = @FAC_IMG ");
                sb.AppendLine("     ,COMMENT = @COMMENT ");
                sb.AppendLine(" where FAC_CD = @FAC_CD");

                if (dgv_fac_value.Rows.Count > 0)
                {
                    for (int i = 0; i < dgv_fac_value.Rows.Count-1; i++)
                    {
                        string txt_seq = (string)dgv_fac_value.Rows[i].Cells["FAC_SET_CD"].Value;
                        if (txt_seq == "" || txt_seq == null)
                        {
                            sb.AppendLine("declare @seq" + i + " int ");
                            sb.AppendLine("select @seq" + i + " =ISNULL(MAX(FAC_SET_CD),0)+1 from N_FAC_SETTING ");
                            sb.AppendLine("where FAC_CD = " + txt_faccd + " ");
                            //sb.AppendLine("and FAC_SET_CD = @seq1");

                            sb.AppendLine("insert into N_FAC_SETTING(");
                            sb.AppendLine("     FAC_CD ");
                            sb.AppendLine("     ,FAC_SET_CD ");
                            sb.AppendLine("     ,FAC_SET_NM ");
                            sb.AppendLine("     ,VALUE1 ");
                            sb.AppendLine("     ,VALUE2 ");
                            sb.AppendLine("     ,VALUE3 ");
                            sb.AppendLine("     ,VALUE4 ");
                            sb.AppendLine("     ,VALUE5 ");
                            sb.AppendLine("     ,VALUE6 ");
                            sb.AppendLine("     ,VALUE7 ");
                            sb.AppendLine("     ,VALUE8 ");
                            sb.AppendLine("     ,VALUE9 ");
                            sb.AppendLine("     ,VALUE10 ");
                            sb.AppendLine(" ) values ( ");
                            sb.AppendLine("     @FAC_CD ");
                            sb.AppendLine("     ,@seq" + i + " ");
                            sb.AppendLine("     ,'" + dgv_fac_value.Rows[0].Cells["FAC_SET_NM"].Value + "' ");
                            sb.AppendLine("     ,'" + dgv_fac_value.Rows[0].Cells["S1"].Value + "' ");
                            sb.AppendLine("     ,'" + dgv_fac_value.Rows[0].Cells["S2"].Value + "' ");
                            sb.AppendLine("     ,'" + dgv_fac_value.Rows[0].Cells["S3"].Value + "' ");
                            sb.AppendLine("     ,'" + dgv_fac_value.Rows[0].Cells["S4"].Value + "' ");
                            sb.AppendLine("     ,'" + dgv_fac_value.Rows[0].Cells["S5"].Value + "' ");
                            sb.AppendLine("     ,'" + dgv_fac_value.Rows[0].Cells["S6"].Value + "' ");
                            sb.AppendLine("     ,'" + dgv_fac_value.Rows[0].Cells["S7"].Value + "' ");
                            sb.AppendLine("     ,'" + dgv_fac_value.Rows[0].Cells["S8"].Value + "' ");
                            sb.AppendLine("     ,'" + dgv_fac_value.Rows[0].Cells["S9"].Value + "' ");
                            sb.AppendLine("     ,'" + dgv_fac_value.Rows[0].Cells["S10"].Value + "' ");
                            sb.AppendLine(" ) ");
                        }
                        else
                        {
                            sb.AppendLine("declare @seq" + i + " int ");
                            sb.AppendLine("select @seq" + i + " =ISNULL(MAX(FAC_SET_CD),0)+1 from N_FAC_SETTING ");
                            sb.AppendLine("where FAC_CD = " + txt_faccd + " ");

                            sb.AppendLine("update N_FAC_SETTING set");
                            sb.AppendLine("      FAC_CD =  @FAC_CD ");
                            sb.AppendLine("     ,FAC_SET_CD = @seq" + i + " ");
                            sb.AppendLine("     ,FAC_SET_NM = '" + dgv_fac_value.Rows[i].Cells["FAC_SET_NM"].Value + "'   ");
                            sb.AppendLine("     ,VALUE1 = '" + dgv_fac_value.Rows[i].Cells["S1"].Value + "'  ");
                            sb.AppendLine("     ,VALUE2 = '" + dgv_fac_value.Rows[i].Cells["S2"].Value + "' ");
                            sb.AppendLine("     ,VALUE3 = '" + dgv_fac_value.Rows[i].Cells["S3"].Value + "'  ");
                            sb.AppendLine("     ,VALUE4 = '" + dgv_fac_value.Rows[i].Cells["S4"].Value + "'  ");
                            sb.AppendLine("     ,VALUE5 = '" + dgv_fac_value.Rows[i].Cells["S5"].Value + "'  ");
                            sb.AppendLine("     ,VALUE6 = '" + dgv_fac_value.Rows[i].Cells["S6"].Value + "'  ");
                            sb.AppendLine("     ,VALUE7 = '" + dgv_fac_value.Rows[i].Cells["S7"].Value + "'  ");
                            sb.AppendLine("     ,VALUE8 = '" + dgv_fac_value.Rows[i].Cells["S8"].Value + "'  ");
                            sb.AppendLine("     ,VALUE9 = '" + dgv_fac_value.Rows[i].Cells["S9"].Value + "'  ");
                            sb.AppendLine("     ,VALUE10 = '" + dgv_fac_value.Rows[i].Cells["S10"].Value + "'  ");
                            sb.AppendLine(" where FAC_CD = '" + txt_faccd + "' ");
                            sb.AppendLine(" and SEQ = '" + dgv_fac_value.Rows[i].Cells["FAC_SET_CD"].Value + "'");
                        }
                    }


                }
                if (dgv_main_class.Rows.Count > 0)
                {
                    for (int i = 0; i < dgv_main_class.Rows.Count; i++)
                    {
                        string txt_seq = (string)dgv_main_class.Rows[i].Cells["NO"].Value;
                        if (txt_seq == "" || txt_seq == null)
                        {
                            sb.AppendLine("declare @seq1" + i + " int ");
                            sb.AppendLine("select @seq1" + i + " =ISNULL(MAX(SEQ),0)+1 from N_FAC_MAIN_CLASS ");
                            sb.AppendLine("where FAC_CD = '" + txt_faccd + "' ");
                            //sb.AppendLine("and SEQ = @seq1");

                            sb.AppendLine("insert into N_FAC_MAIN_CLASS(");
                            sb.AppendLine("     FAC_CD ");
                            sb.AppendLine("     ,SEQ ");
                            sb.AppendLine("     ,FAC_DATE ");
                            sb.AppendLine("     ,FAC_ITEM ");
                            sb.AppendLine("     ,FAC_COMMENT ");
                            sb.AppendLine("     ,FAC_ACTION ");
                            sb.AppendLine("     ,IMG_PREV ");
                            sb.AppendLine("     ,IMG_AFTER ");
                            sb.AppendLine(" ) values ( ");
                            sb.AppendLine("     @FAC_CD ");
                            sb.AppendLine("     ,@seq1" + i + " ");
                            sb.AppendLine("     ,@FAC_DATE ");
                            sb.AppendLine("     ,@FAC_ITEM ");
                            sb.AppendLine("     ,@FAC_COMMENT ");
                            sb.AppendLine("     ,@FAC_ACTION ");
                            sb.AppendLine("     ,@IMG_PREV ");
                            sb.AppendLine("     ,@IMG_AFTER ");
                            sb.AppendLine(" ) ");
                        }
                        else
                        {
                            sb.AppendLine("declare @seq1" + i + " int ");
                            sb.AppendLine("select @seq1" + i + " =ISNULL(MAX(SEQ),0)+1 from N_FAC_MAIN_CLASS ");
                            sb.AppendLine("where FAC_CD = '" + txt_faccd + "' ");

                            sb.AppendLine("update N_FAC_MAIN_CLASS set");
                            sb.AppendLine("      FAC_CD =  @FAC_CD ");
                            sb.AppendLine("     ,SEQ = @seq1" + i + " ");
                            sb.AppendLine("     ,FAC_DATE = @FAC_DATE  ");
                            sb.AppendLine("     ,FAC_ITEM = @FAC_ITEM  ");
                            sb.AppendLine("     ,FAC_COMMENT = @FAC_COMMENT  ");
                            sb.AppendLine("     ,FAC_ACTION = @FAC_ACTION  ");
                            sb.AppendLine("     ,IMG_PREV = @IMG_PREV  ");
                            sb.AppendLine("     ,IMG_AFTER = @IMG_AFTER  ");
                            //sb.AppendLine("     ,UPSTAFF = '" + Common.p_strStaffNo + "'  ");
                            //sb.AppendLine("     ,UPTIME = convert(varchar, getdate(), 120)  ");                            

                            sb.AppendLine(" where FAC_CD = '" + txt_faccd + "' ");
                            sb.AppendLine(" and SEQ = '" + dgv_main_class.Rows[i].Cells["NO"].Value + "'");
                        }
                    }
                }


                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand = new SqlCommand(sb.ToString());
                sCommand.Parameters.AddWithValue("@FAC_CD", txt_faccd);
                sCommand.Parameters.AddWithValue("@FAC_NM", txt_facnm);
                sCommand.Parameters.AddWithValue("@MODEL_NM", txt_modelnm);
                sCommand.Parameters.AddWithValue("@SPEC", txt_spec);
                sCommand.Parameters.AddWithValue("@MAKER_NM", txt_makernm);
                sCommand.Parameters.AddWithValue("@INPUT_DATE", dtp_input_date);
                sCommand.Parameters.AddWithValue("@INPUT_PRICE", double.Parse(txt_inputprice.ToString()));
                sCommand.Parameters.AddWithValue("@DEPT_CD", cmb_deptcd);
                sCommand.Parameters.AddWithValue("@USAGE", txt_usage);
                sCommand.Parameters.AddWithValue("@PRODUCT_AMT", double.Parse(txt_product_amt.ToString()));
                sCommand.Parameters.AddWithValue("@POWER_NUM", double.Parse(txt_power_num.ToString()));
                sCommand.Parameters.AddWithValue("@MAIN_CLASS ", cmb_main_class);
                sCommand.Parameters.AddWithValue("@CHECK_FLOW", cmb_chk_flow);
                sCommand.Parameters.AddWithValue("@CHECK_METHOD", txt_chk_method);
                sCommand.Parameters.AddWithValue("@CHECK_STAN", txt_chk_stan);
                sCommand.Parameters.AddWithValue("@FAC_IMG", img);
                sCommand.Parameters.AddWithValue("@COMMENT", txt_comment);
                //sCommand.Parameters.AddWithValue("@FAC_SET_CD", txt_facnm);
                //sCommand.Parameters.AddWithValue("@FAC_SET_NM", dgv_fac_value.Rows[0].Cells["dgv_setnm"].ToString());
                //sCommand.Parameters.AddWithValue("@VALUE1", dgv_fac_value.Rows[0].Cells["SET1"].ToString());
                //sCommand.Parameters.AddWithValue("@VALUE2", dgv_fac_value.Rows[0].Cells["SET2"].ToString());
                //sCommand.Parameters.AddWithValue("@VALUE3", dgv_fac_value.Rows[0].Cells["SET3"].ToString());
                //sCommand.Parameters.AddWithValue("@VALUE4", dgv_fac_value.Rows[0].Cells["SET4"].ToString());
                //sCommand.Parameters.AddWithValue("@VALUE5", dgv_fac_value.Rows[0].Cells["SET5"].ToString());
                //sCommand.Parameters.AddWithValue("@VALUE6", dgv_fac_value.Rows[0].Cells["SET6"].ToString());
                //sCommand.Parameters.AddWithValue("@VALUE7", dgv_fac_value.Rows[0].Cells["SET7"].ToString());
                //sCommand.Parameters.AddWithValue("@VALUE8", dgv_fac_value.Rows[0].Cells["SET8"].ToString());
                //sCommand.Parameters.AddWithValue("@VALUE9", dgv_fac_value.Rows[0].Cells["SET9"].ToString());
                //sCommand.Parameters.AddWithValue("@VALUE10", dgv_fac_value.Rows[0].Cells["SET10"].ToString());
                sCommand.Parameters.AddWithValue("@FAC_DATE", dtp_chk_date);
                sCommand.Parameters.AddWithValue("@FAC_ITEM", txt_fac_item);
                sCommand.Parameters.AddWithValue("@FAC_COMMENT", txt_fac_comment);
                sCommand.Parameters.AddWithValue("@FAC_ACTION", txt_fac_action);
                sCommand.Parameters.AddWithValue("@IMG_PREV", before_img);
                sCommand.Parameters.AddWithValue("@IMG_AFTER", after_img);
                //sCommand.Parameters.AddWithValue("@INSTAFF", 0);
                //sCommand.Parameters.AddWithValue("@INTIME", 0);
                //sCommand.Parameters.AddWithValue("@UPSTAFF", 0);
                //sCommand.Parameters.AddWithValue("@UPTIME", 0);

                int qResult = wAdo.SqlCommandEtc(sCommand, "update_USER_TB");
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

        #endregion updatefac logic



        #region 원자재입고등록


        #endregion 원자재입고등록
        public int insert_input_raw(
            string dtp_input_date
            ,string txt_custcd
            ,string chk_order
            ,string txt_comment
            , conDataGridView dgv_item_list
            , string eye_chk
            ,string txt_input_num


            //  string dtp_input_date            
            //, string txt_custnm
            //, string txt_comment
            //, string txt_custcd   
            //, string dtp_out_date
            //, string dtp_srch_start
            //, string dtp_srch_end
            //, string chk_order
            //, conDataGridView dgv_item_list
            //, conDataGridView dgv_today_list
            //, conDataGridView dgv_date_list
            //, DataGridView del_dgv

            )
        {
            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();
                SqlCommand sCommand;

                for (int i = 0; i < dgv_item_list.Rows.Count; i++)
                {
                    if ((string)dgv_item_list.Rows[i].Cells["ORDER_DATE"].Value != null && (string)dgv_item_list.Rows[i].Cells["ORDER_DATE"].Value != "")
                    {
                        sb = new StringBuilder();
                        sb.AppendLine(" select A.ORDER_DATE,A.ORDER_CD,B.SEQ,C.ORDER_AMT, C.INPUT_AMT");
                        sb.AppendLine(" FROM F_ORDER A ");
                        sb.AppendLine(" LEFT OUTER JOIN F_ORDER_DETAIL B  ");
                        sb.AppendLine(" ON A.ORDER_DATE = B.ORDER_DATE ");
                        sb.AppendLine("     AND A.ORDER_CD = B.ORDER_CD ");
                        sb.AppendLine(" LEFT OUTER JOIN(	 ");
                        sb.AppendLine("                     SELECT AA.ORDER_DATE	 ");
                        sb.AppendLine("                           ,AA.ORDER_CD       ");
                        sb.AppendLine("                           ,AA.SEQ ");
                        sb.AppendLine("                           ,FLOOR(ISNULL(AA.TOTAL_AMT,0)) AS ORDER_AMT ");
                        sb.AppendLine("                           ,ISNULL(SUM(BB.TOTAL_AMT),0) AS INPUT_AMT ");
                        sb.AppendLine("                           , ISNULL(AA.TOTAL_AMT,0)-ISNULL(SUM(BB.TOTAL_AMT),0) AS NO_INPUT_AMT ");
                        sb.AppendLine("                     FROM F_ORDER_DETAIL AA ");
                        sb.AppendLine("                     LEFT OUTER JOIN F_RAW_DETAIL BB ");
                        sb.AppendLine("                     ON AA.ORDER_DATE = BB.ORDER_DATE ");
                        sb.AppendLine("                         AND AA.ORDER_CD = BB.ORDER_CD ");
                        sb.AppendLine("                         AND AA.SEQ = BB.ORDER_SEQ ");
                        sb.AppendLine("                     GROUP BY AA.ORDER_DATE,AA.ORDER_CD,AA.SEQ,AA.TOTAL_AMT)C ");
                        sb.AppendLine(" ON A.ORDER_DATE = C.ORDER_DATE  ");
                        sb.AppendLine("     AND A.ORDER_CD = C.ORDER_CD ");
                        sb.AppendLine("     AND B.SEQ = C.SEQ  ");
                        sb.AppendLine(" WHERE A.ORDER_DATE = '" + dgv_item_list.Rows[i].Cells["ORDER_DATE"].Value + "' ");
                        sb.AppendLine("      AND A.ORDER_CD = '" + dgv_item_list.Rows[i].Cells["ORDER_CD"].Value + "' ");
                        sb.AppendLine("      AND B.SEQ = '" + dgv_item_list.Rows[i].Cells["ORDER_SEQ"].Value + "' ");

                        sCommand = new SqlCommand(sb.ToString());
                        if (sCommand.CommandText.Equals(null))
                        {
                            wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                            return 2;
                        }

                        DataTable dt = wAdo.SqlCommandSelect(sCommand);
                        double rs_num = 0;
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            Console.WriteLine(dt.Rows[0]["ORDER_AMT"].ToString());
                            MessageBox.Show(dt.Rows[0]["ORDER_AMT"].ToString());
                            double order_amt = double.Parse(dt.Rows[0]["ORDER_AMT"].ToString());
                            double input_amt = double.Parse(dt.Rows[0]["INPUT_AMT"].ToString());
                            double grd_total_amt = double.Parse(((string)dgv_item_list.Rows[i].Cells["TOTAL_AMT"].Value).Replace(",", ""));
                            // double grd_ord_total_amt = double.Parse(((string)dgv_item_list.Rows[i].Cells["OLD_TOTAL_AMT"].Value)); //백업은 콤마 정의 안함
                       

                        // 발주수량 - 입고수량 - 기존입고량 = 결과값

                        rs_num = order_amt - input_amt - grd_total_amt;
                        }
                        if (rs_num < 0)
                        {
                            StringBuilder alert_sb = new StringBuilder();
                            alert_sb.AppendLine(i + 1 + "번째 줄 원부재료에 포함된 발주번호 \n ");
                            alert_sb.AppendLine(dgv_item_list.Rows[i].Cells["ORDER_DATE"].Value + " [" + dgv_item_list.Rows[i].Cells["ORDER_CD"].Value + "] 의 발주수량보다 더 많게 입력하셨습니다. \n");
                            alert_sb.AppendLine("그대로 저장하시겠습니까? (저장:예 / 취소:아니오)");

                            DialogResult msgOk = MessageBox.Show(alert_sb.ToString(), "삭제여부", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (msgOk == DialogResult.No)
                            {
                                return 3;
                            }
                        }
                    }
                }

                sb = new StringBuilder();
                sb.AppendLine("declare @seq int ");
                sb.AppendLine("select @seq =ISNULL(MAX(INPUT_CD),0)+1 from F_RAW_INPUT ");
                sb.AppendLine("where INPUT_DATE = '" + dtp_input_date + "' ");

                sb.AppendLine("insert into F_RAW_INPUT(");
                sb.AppendLine("     INPUT_DATE");
                sb.AppendLine("     ,INPUT_CD ");
                sb.AppendLine("     ,CUST_CD ");
                sb.AppendLine("     ,COMPLETE_YN ");
                sb.AppendLine("     ,COMMENT ");
                sb.AppendLine("     ,INSTAFF ");
                sb.AppendLine("     ,INTIME ");                
                sb.AppendLine(" ) values ( ");
                sb.AppendLine("      @INPUT_DATE ");
                sb.AppendLine("     ,@seq");
                sb.AppendLine("     ,@CUST_CD ");
                sb.AppendLine("     ,'" + chk_order + "' ");
                sb.AppendLine("     ,@COMMENT ");
                sb.AppendLine("     ,'" + Common.p_strStaffNo + "' ");
                sb.AppendLine("     ,convert(varchar, getdate(), 120) ");                
                sb.AppendLine(" ) ");

                if (dgv_item_list.Rows.Count > 0)
                {
                    for (int i = 0; i < dgv_item_list.Rows.Count; i++)
                    {
                       string txt_seq = (string)dgv_item_list.Rows[i].Cells["SEQ"].Value;
                        if (txt_seq == "" || txt_seq == null)
                        {
                            sb.AppendLine("declare @input_seq" + i + " int, @chk_gbn" + i + "  nvarchar(1), @chk_yn" + i + " nvarchar(1), @final_amt" + i + " nvarchar(20) ");
                            sb.AppendLine("select @input_seq" + i + " =ISNULL(MAX(SEQ),0)+1 from F_RAW_DETAIL ");
                            sb.AppendLine("where INPUT_DATE = '" + dtp_input_date + "' ");
                            sb.AppendLine("and INPUT_CD =  @seq ");

                            sb.AppendLine("select @chk_gbn" + i + " = check_gubun from N_RAW_CODE ");
                            sb.AppendLine("where RAW_MAT_CD = '" + dgv_item_list.Rows[i].Cells["RAW_MAT_CD"].Value + "' ");

                            sb.AppendLine("IF @chk_gbn" + i + " = '1' BEGIN set @chk_yn" + i + " = 'S' set @final_amt" + i + " = '0' END "); //원자재 검사여부가 검사일 경우 'S' 대기 
                            sb.AppendLine("ELSE BEGIN set @chk_yn" + i + " = 'O' set @final_amt" + i + " = '" + ((string)dgv_item_list.Rows[i].Cells["TOTAL_AMT"].Value).Replace(",", "") + "' END "); //원자재 검사여부가 생략일 경우 'O'

                            sb.AppendLine("insert into F_RAW_DETAIL(");
                            sb.AppendLine("     INPUT_DATE ");
                            sb.AppendLine("     ,INPUT_CD ");
                            sb.AppendLine("     ,SEQ ");
                            sb.AppendLine("     ,RAW_MAT_CD ");
                            sb.AppendLine("     ,UNIT_CD ");
                            sb.AppendLine("     ,TEMP_AMT ");
                            sb.AppendLine("     ,TOTAL_AMT ");
                            sb.AppendLine("     ,CURR_AMT ");
                            sb.AppendLine("     ,PRICE ");
                            sb.AppendLine("     ,TOTAL_MONEY ");
                            //sb.AppendLine("     ,HEAT_NO ");
                            //sb.AppendLine("     ,HEAT_TIME ");
                            sb.AppendLine("     ,ORDER_DATE ");
                            sb.AppendLine("     ,ORDER_CD ");
                            sb.AppendLine("     ,ORDER_SEQ ");
                            sb.AppendLine("     ,COMPLETE_YN ");
                            sb.AppendLine("     ,CHECK_YN ");
                            sb.AppendLine("     ,INSTAFF ");
                            sb.AppendLine("     ,INTIME ");
                            sb.AppendLine("  )values ( ");
                            sb.AppendLine("     '" + dtp_input_date + "' ");
                            sb.AppendLine("      ,@seq ");
                            sb.AppendLine("     ,@input_seq" + i + " ");
                            sb.AppendLine("     ,'" + dgv_item_list.Rows[i].Cells["RAW_MAT_CD"].Value + "' ");
                            sb.AppendLine("     ,'" + dgv_item_list.Rows[i].Cells["UNIT_CD"].Value + "' ");
                            sb.AppendLine("     ," + dgv_item_list.Rows[i].Cells["TOTAL_AMT"].Value.ToString().Replace(",","") + " ");
                            //sb.AppendLine("     ,'" + ((string)dgv_item_list.Rows[i].Cells["TOTAL_AMT"].Value).Replace(",", "") + "' "); //가입고
                            sb.AppendLine("     ,@final_amt" + i + " ");
                            sb.AppendLine("     ,@final_amt" + i + " ");
                            sb.AppendLine("     ," + ((string)dgv_item_list.Rows[i].Cells["PRICE"].Value).Replace(",", "") + " ");
                            sb.AppendLine("     ," + ((string)dgv_item_list.Rows[i].Cells["TOTAL_MONEY"].Value).Replace(",", "") + " ");
                            // sb.AppendLine("     ,'" + double.Parse(dgv_item_list.Rows[i].Cells["PRICE"].Value.ToString().Replace(",", "") + "' "));
                            // sb.AppendLine("     ,'" + double.Parse(dgv_item_list.Rows[i].Cells["TOTAL_MONEY"].Value.ToString().Replace(",", "") + "' "));
                            //sb.AppendLine("     ,'" + dgv_item_list.Rows[i].Cells["HEAT_NO"].Value + "' ");
                            //sb.AppendLine("     ,'" + dgv_item_list.Rows[i].Cells["HEAT_TIME"].Value + "' ");
                            sb.AppendLine("     ,'" + dgv_item_list.Rows[i].Cells["ORDER_DATE"].Value + "' ");
                            sb.AppendLine("     ,'" + dgv_item_list.Rows[i].Cells["ORDER_CD"].Value + "' ");
                            sb.AppendLine("     ,'" + dgv_item_list.Rows[i].Cells["ORDER_SEQ"].Value + "' ");
                            sb.AppendLine("     ,'" + chk_order + "' ");
                            sb.AppendLine("     ,'" + eye_chk + "'  "); //BE
                            sb.AppendLine("     ,'" + Common.p_strStaffNo + "' ");
                            sb.AppendLine("     ,convert(varchar, getdate(), 120)  ");
                            sb.AppendLine("  )");
                        }

                        else
                        {
                            sb.AppendLine("update F_RAW_DETAIL set");
                            //sb.AppendLine("       TEMP_AMT =  '" + ((string)dgv_item_list.Rows[i].Cells["TOTAL_AMT"].Value).Replace(",", "") + "' ");
                            sb.AppendLine("       TEMP_AMT =  '" + dgv_item_list.Rows[i].Cells["TOTAL_AMT"].Value + "' ");
                            sb.AppendLine("      ,PRICE =  '" + ((string)dgv_item_list.Rows[i].Cells["PRICE"].Value).Replace(",", "") + "' ");
                            sb.AppendLine("      ,TOTAL_MONEY =  '" + ((string)dgv_item_list.Rows[i].Cells["TOTAL_MONEY"].Value).Replace(",", "") + "' ");
                            sb.AppendLine("      ,CHECK_YN = '" + eye_chk + "' ");
                            //sb.AppendLine("       TEMP_AMT =  '" + double.Parse(dgv_item_list.Rows[i].Cells["TOTAL_AMT"].Value.ToString().Replace(",", "") + "' "));
                            //sb.AppendLine("      ,PRICE =  '" + double.Parse(dgv_item_list.Rows[i].Cells["PRICE"].Value.ToString().Replace(",", "") + "' "));
                            //sb.AppendLine("      ,TOTAL_MONEY =  '" + double.Parse(dgv_item_list.Rows[i].Cells["TOTAL_MONEY"].Value.ToString().Replace(",", "") + "' "));
                            //sb.AppendLine("      ,HEAT_NO =  '" + dgv_item_list.Rows[i].Cells["HEAT_NO"].Value + "' ");
                            //sb.AppendLine("      ,HEAT_TIME =  '" + dgv_item_list.Rows[i].Cells["HEAT_TIME"].Value + "' ");
                            sb.AppendLine("      ,UPSTAFF =  '" + Common.p_strStaffNo + "' ");
                            sb.AppendLine("      ,UPTIME =  convert(varchar, getdate(), 120) ");
                            sb.AppendLine(" where INPUT_DATE = '" + dtp_input_date + "' ");
                            sb.AppendLine(" and INPUT_CD = '" + txt_input_num + "' ");
                            sb.AppendLine(" and SEQ = '" + dgv_item_list.Rows[i].Cells["SEQ"].Value + "'");
                        }


                        //sb.AppendLine(" update N_RAW_CODE set ");
                        //sb.AppendLine("     BAL_STOCK = ISNULL(BAL_STOCK,0) +" + double.Parse(((string)in_rm_dgv.Rows[i].Cells["TOTAL_AMT"].Value).Replace(",", "") + " ") );
                        //sb.AppendLine(" where RAW_MAT_CD = '" +in_rm_dgv.Rows[i].Cells["RAW_MAT_CD"].Value + "' ");
                    }
                }

                sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@INPUT_DATE", dtp_input_date);
                sCommand.Parameters.AddWithValue("@CUST_CD", txt_custcd);
                sCommand.Parameters.AddWithValue("@COMMENT", txt_comment);

                int qResult = wAdo.SqlCommandEtc(sCommand, "insert_RAW_INPUT_TB");
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

        public DataTable fn_Rm_Input_List(string condition)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select A.INPUT_DATE");
            sb.AppendLine("     ,A.INPUT_CD ");
            sb.AppendLine("     ,ISNULL(B.RAW_MAT_CNT,0) AS RAW_MAT_CNT ");
            sb.AppendLine("     ,A.CUST_CD ");
            sb.AppendLine("     ,(select CUST_NM from N_CUST_CODE where CUST_GUBUN = '2' and CUST_CD = A.CUST_CD) as CUST_NM  ");
            sb.AppendLine("     ,A.COMPLETE_YN ");
            sb.AppendLine("     ,A.INSTAFF ");
            sb.AppendLine("     ,(select STAFF_NM from N_STAFF_CODE where STAFF_CD = A.INSTAFF) as STAFF_NM ");
            sb.AppendLine("     ,COMMENT ");
            sb.AppendLine(" from F_RAW_INPUT A ");
            sb.AppendLine(" LEFT OUTER JOIN ( ");
            sb.AppendLine(" SELECT INPUT_DATE,INPUT_CD,COUNT(RAW_MAT_CD) AS RAW_MAT_CNT FROM F_RAW_DETAIL ");
            sb.AppendLine(" GROUP BY INPUT_DATE,INPUT_CD) B ");
            sb.AppendLine(" ON A.INPUT_DATE = B.INPUT_DATE ");
            sb.AppendLine(" AND A.INPUT_CD = B.INPUT_CD  ");
            sb.AppendLine(condition);
            sb.AppendLine(" order by A.INPUT_DATE desc, A.INPUT_CD desc ");

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);
        }

        //public DataTable fn_Input_Raw_List(string condition)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendLine("select A.INPUT_DATE");
        //    sb.AppendLine("     ,A.INPUT_CD ");
        //    sb.AppendLine("     ,ISNULL(B.RAW_MAT_CNT,0) AS RAW_MAT_CNT ");
        //    sb.AppendLine("     ,A.CUST_CD ");
        //    sb.AppendLine("     ,(select CUST_NM from N_CUST_CODE where CUST_GUBUN = '2' and CUST_CD = A.CUST_CD) as CUST_NM  ");
        //    sb.AppendLine("     ,A.COMPLETE_YN ");
        //    sb.AppendLine("     ,A.INSTAFF ");
        //    sb.AppendLine("     ,(select STAFF_NM from N_STAFF_CODE where STAFF_CD = A.INSTAFF) as STAFF_NM ");
        //    sb.AppendLine("     ,COMMENT ");
        //    sb.AppendLine(" from F_RAW_INPUT A ");
        //    sb.AppendLine(" LEFT OUTER JOIN ( ");
        //    sb.AppendLine(" SELECT INPUT_DATE,INPUT_CD,COUNT(RAW_MAT_CD) AS RAW_MAT_CNT FROM F_RAW_DETAIL ");
        //    sb.AppendLine(" GROUP BY INPUT_DATE,INPUT_CD) B ");
        //    sb.AppendLine(" ON A.INPUT_DATE = B.INPUT_DATE ");
        //    sb.AppendLine(" AND A.INPUT_CD = B.INPUT_CD  ");
        //    sb.AppendLine(condition);
        //    sb.AppendLine(" order by A.INPUT_DATE desc, A.INPUT_CD desc ");

        //    SqlCommand sCommand = new SqlCommand(sb.ToString());
        //    if (sCommand.CommandText.Equals(null))
        //    {
        //        wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
        //        return null;
        //    }
        //    return wAdo.SqlCommandSelect(sCommand);

        //}
        public int update_input_raw(
            string dtp_input_date
            , string txt_input_num
            , string txt_custcd
            , string txt_comment
            , string chk_order
            , conDataGridView dgv_item_list
            , DataGridView del_dgv
            , string eye_chk

            )
        {
            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                SqlCommand sCommand = new SqlCommand(sb.ToString());
                for (int i = 0; i < dgv_item_list.Rows.Count; i++)
                {
                    if ((string)dgv_item_list.Rows[i].Cells["ORDER_DATE"].Value != null && (string)dgv_item_list.Rows[i].Cells["ORDER_DATE"].Value != "")
                    {
                        sb.AppendLine(" select A.ORDER_DATE,A.ORDER_CD,B.SEQ,C.ORDER_AMT, C.INPUT_AMT");
                        sb.AppendLine(" FROM F_ORDER A ");
                        sb.AppendLine(" LEFT OUTER JOIN F_ORDER_DETAIL B  ");
                        sb.AppendLine(" ON A.ORDER_DATE = B.ORDER_DATE ");
                        sb.AppendLine("     AND A.ORDER_CD = B.ORDER_CD ");
                        sb.AppendLine(" LEFT OUTER JOIN(	 ");
                        sb.AppendLine("                     SELECT AA.ORDER_DATE	 ");
                        sb.AppendLine("                           ,AA.ORDER_CD       ");
                        sb.AppendLine("                           ,AA.SEQ ");
                        sb.AppendLine("                           ,FLOOR(ISNULL(AA.TOTAL_AMT,0)) AS ORDER_AMT ");
                        sb.AppendLine("                           ,ISNULL(SUM(BB.TOTAL_AMT),0) AS INPUT_AMT ");
                        sb.AppendLine("                           , ISNULL(AA.TOTAL_AMT,0)-ISNULL(SUM(BB.TOTAL_AMT),0) AS NO_INPUT_AMT ");
                        sb.AppendLine("                     FROM F_ORDER_DETAIL AA ");
                        sb.AppendLine("                     LEFT OUTER JOIN F_RAW_DETAIL BB ");
                        sb.AppendLine("                     ON AA.ORDER_DATE = BB.ORDER_DATE ");
                        sb.AppendLine("                         AND AA.ORDER_CD = BB.ORDER_CD ");
                        sb.AppendLine("                         AND AA.SEQ = BB.ORDER_SEQ ");
                        sb.AppendLine("                     GROUP BY AA.ORDER_DATE,AA.ORDER_CD,AA.SEQ,AA.TOTAL_AMT)C ");
                        sb.AppendLine(" ON A.ORDER_DATE = C.ORDER_DATE  ");
                        sb.AppendLine("     AND A.ORDER_CD = C.ORDER_CD ");
                        sb.AppendLine("     AND B.SEQ = C.SEQ  ");
                        sb.AppendLine(" WHERE A.ORDER_DATE = '" + dgv_item_list.Rows[i].Cells["ORDER_DATE"].Value + "' ");
                        sb.AppendLine("      AND A.ORDER_CD = '" + dgv_item_list.Rows[i].Cells["ORDER_CD"].Value + "' ");
                        sb.AppendLine("      AND B.SEQ = '" + dgv_item_list.Rows[i].Cells["SEQ"].Value + "' ");

                        sCommand = new SqlCommand(sb.ToString());
                        if (sCommand.CommandText.Equals(null))
                        {
                            wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                            return 2;
                        }
                        DataTable dt = wAdo.SqlCommandSelect(sCommand);
                        double rs_num = 0;
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            double order_amt = double.Parse(dt.Rows[0]["ORDER_AMT"].ToString());
                            double input_amt = double.Parse(dt.Rows[0]["INPUT_AMT"].ToString());
                            double grd_total_amt = double.Parse(((string)dgv_item_list.Rows[i].Cells["TOTAL_AMT"].Value).Replace(",", ""));
                            double grd_ord_total_amt = double.Parse(((string)dgv_item_list.Rows[i].Cells["OLD_TOTAL_AMT"].Value).Replace(",", "")); //백업은 콤마 정의 안함

                            //발주수량 + 입력하기 전 수량백업 값 - 입고수량 - 입력한 수량 값 = 결과값

                            rs_num = order_amt + grd_ord_total_amt - input_amt - grd_total_amt;
                        }
                        

                        if (rs_num < 0)
                        {
                            StringBuilder alert_sb = new StringBuilder();
                            alert_sb.AppendLine(i + 1 + "번째 줄 원부재료에 포함된 발주번호 \n ");
                            alert_sb.AppendLine(dgv_item_list.Rows[i].Cells["ORDER_DATE"].Value + " [" + dgv_item_list.Rows[i].Cells["ORDER_CD"].Value + "] 의 발주수량보다 더 많게 입력하셨습니다. \n");
                            alert_sb.AppendLine("그대로 저장하시겠습니까? (저장:예 / 취소:아니오)");

                            DialogResult msgOk = MessageBox.Show(alert_sb.ToString(), "삭제여부", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (msgOk == DialogResult.No)
                            {
                                return 3;
                            }
                        }
                    }
                }

                sb = new StringBuilder();
                sb.AppendLine("update F_RAW_INPUT set");
                sb.AppendLine("      CUST_CD = @CUST_CD ");
                sb.AppendLine("     ,COMPLETE_YN = '" + chk_order + "' ");
                sb.AppendLine("     ,UPSTAFF = '" + Common.p_strStaffNo + "' ");
                sb.AppendLine("     ,UPTIME = convert(varchar, getdate(), 120) ");
                sb.AppendLine("     ,COMMENT = @COMMENT ");

                sb.AppendLine(" where INPUT_DATE = @INPUT_DATE and INPUT_CD= @INPUT_CD ");

                

                if (dgv_item_list.Rows.Count > 0)
                {
                    for (int i = 0; i < dgv_item_list.Rows.Count; i++)
                    {
                        string txt_seq = (string)dgv_item_list.Rows[i].Cells["SEQ"].Value;
                        if (txt_seq == "" || txt_seq == null)
                        {
                            sb.AppendLine("declare @input_seq" + i + " int, @chk_gbn" + i + "  nvarchar(1), @chk_yn" + i + " nvarchar(1), @final_amt" + i + " nvarchar(20) ");
                            sb.AppendLine("select @input_seq" + i + " =ISNULL(MAX(SEQ),0)+1 from F_RAW_DETAIL ");
                            sb.AppendLine("where INPUT_DATE = '" + dtp_input_date + "' ");
                            sb.AppendLine("and INPUT_CD = '" + txt_input_num + "' ");

                            sb.AppendLine("select @chk_gbn" + i + " = check_gubun from N_RAW_CODE ");
                            sb.AppendLine("where RAW_MAT_CD = '" + dgv_item_list.Rows[i].Cells["RAW_MAT_CD"].Value + "' ");

                            sb.AppendLine("IF @chk_gbn" + i + " = '1' BEGIN set @chk_yn" + i + " = 'S' set @final_amt" + i + " = '0' END "); //원자재 검사여부가 검사일 경우 'S' 대기 
                            sb.AppendLine("ELSE BEGIN set @chk_yn" + i + " = 'O' set @final_amt" + i + " = '" + ((string)dgv_item_list.Rows[i].Cells["TOTAL_AMT"].Value).Replace(",", "") + "' END "); //원자재 검사여부가 생략일 경우 'O'

                            sb.AppendLine("insert into F_RAW_DETAIL(");
                            sb.AppendLine("     INPUT_DATE ");
                            sb.AppendLine("     ,INPUT_CD ");
                            sb.AppendLine("     ,SEQ ");
                            sb.AppendLine("     ,RAW_MAT_CD ");
                            sb.AppendLine("     ,SPEC ");
                            sb.AppendLine("     ,UNIT_CD ");
                            sb.AppendLine("     ,TEMP_AMT ");
                            sb.AppendLine("     ,TOTAL_AMT ");
                            sb.AppendLine("     ,CURR_AMT ");
                            sb.AppendLine("     ,PRICE ");
                            sb.AppendLine("     ,TOTAL_MONEY ");
                            sb.AppendLine("     ,CHECK_YN ");
                            sb.AppendLine("     ,ORDER_DATE ");
                            sb.AppendLine("     ,ORDER_CD ");
                            sb.AppendLine("     ,ORDER_SEQ ");
                            sb.AppendLine("     ,COMPLETE_YN ");                            
                            sb.AppendLine("     ,INSTAFF ");
                            sb.AppendLine("     ,INTIME ");
                            sb.AppendLine("  )values ( ");
                            sb.AppendLine("     '" + dtp_input_date + "' ");
                            sb.AppendLine("      ,'" + txt_input_num + "'  ");
                            sb.AppendLine("     ,@input_seq" + i + " ");
                            sb.AppendLine("     ,'" + dgv_item_list.Rows[i].Cells["RAW_MAT_CD"].Value + "' ");
                            sb.AppendLine("     ,'" + dgv_item_list.Rows[i].Cells["SPEC"].Value + "' ");
                            sb.AppendLine("     ,'" + dgv_item_list.Rows[i].Cells["UNIT_CD"].Value + "' ");
                            sb.AppendLine("     ," + dgv_item_list.Rows[i].Cells["TOTAL_AMT"].Value + " ");
                            sb.AppendLine("     ,@final_amt" + i + " ");
                            sb.AppendLine("     ,@final_amt" + i + " ");
                            sb.AppendLine("     ," + ((string)dgv_item_list.Rows[i].Cells["PRICE"].Value).Replace(",", "") + " ");
                            sb.AppendLine("     ," + ((string)dgv_item_list.Rows[i].Cells["TOTAL_MONEY"].Value).Replace(",", "") + " ");
                            //sb.AppendLine("     ,'" + double.Parse(dgv_item_list.Rows[i].Cells["PRICE"].Value.ToString().Replace(",", "") + "' "));
                            //sb.AppendLine("     ,'" + double.Parse(dgv_item_list.Rows[i].Cells["TOTAL_MONEY"].Value.ToString().Replace(",", "") + "' "));
                            //sb.AppendLine("     ,'" + dgv_item_list.Rows[i].Cells["HEAT_NO"].Value + "' ");
                            //sb.AppendLine("     ,'" + dgv_item_list.Rows[i].Cells["HEAT_TIME"].Value + "' ");
                            sb.AppendLine("     ,'" + eye_chk + "' ");
                            sb.AppendLine("     ,'" + dgv_item_list.Rows[i].Cells["ORDER_DATE"].Value + "' ");
                            sb.AppendLine("     ,'" + dgv_item_list.Rows[i].Cells["ORDER_CD"].Value + "' ");
                            sb.AppendLine("     ,'" + dgv_item_list.Rows[i].Cells["ORDER_SEQ"].Value + "' ");
                            sb.AppendLine("     ,'N' ");                            
                            sb.AppendLine("     ,'" + Common.p_strStaffNo + "' ");
                            sb.AppendLine("     ,convert(varchar, getdate(), 120)  ");
                            sb.AppendLine("  )");
                        }
                        else
                        {
                            sb.AppendLine("update F_RAW_DETAIL set");
                            sb.AppendLine("       TEMP_AMT =  " + dgv_item_list.Rows[i].Cells["TOTAL_AMT"].Value + " ");
                            sb.AppendLine("      ,PRICE =  " + ((string)dgv_item_list.Rows[i].Cells["PRICE"].Value).Replace(",", "") + " ");
                            sb.AppendLine("      ,TOTAL_MONEY =  " + ((string)dgv_item_list.Rows[i].Cells["TOTAL_MONEY"].Value).Replace(",", "") + " ");
                            sb.AppendLine("      ,CHECK_YN = '" + eye_chk + "' ");
                            //sb.AppendLine("       TEMP_AMT =  '" + double.Parse(dgv_item_list.Rows[i].Cells["TOTAL_AMT"].Value.ToString().Replace(",", "") + "' "));
                            //sb.AppendLine("      ,PRICE =  '" + double.Parse(dgv_item_list.Rows[i].Cells["PRICE"].Value.ToString().Replace(",", "") + "' "));
                            //sb.AppendLine("      ,TOTAL_MONEY =  '" + double.Parse(dgv_item_list.Rows[i].Cells["TOTAL_MONEY"].Value.ToString().Replace(",", "") + "' "));
                            //sb.AppendLine("      ,HEAT_NO =  '" + dgv_item_list.Rows[i].Cells["HEAT_NO"].Value + "' ");
                            //sb.AppendLine("      ,HEAT_TIME =  '" + dgv_item_list.Rows[i].Cells["HEAT_TIME"].Value + "' ");
                            sb.AppendLine("      ,UPSTAFF =  '" + Common.p_strStaffNo + "' ");
                            sb.AppendLine("      ,UPTIME =  convert(varchar, getdate(), 120) ");
                            sb.AppendLine(" where INPUT_DATE = '" + dtp_input_date + "' ");
                            sb.AppendLine(" and INPUT_CD = '" + txt_input_num + "' ");
                            sb.AppendLine(" and SEQ = '" + dgv_item_list.Rows[i].Cells["SEQ"].Value + "'");
                        }
                    }
                }

                if (del_dgv.Rows.Count > 0)
                {
                    for (int i = 0; i < del_dgv.Rows.Count; i++)
                    {
                        sb.AppendLine("delete from F_RAW_DETAIL ");
                        sb.AppendLine("    where INPUT_DATE = '" + dtp_input_date + "' ");
                        sb.AppendLine("     and INPUT_CD = '" + txt_input_num + "' ");
                        sb.AppendLine("     and SEQ = '" + del_dgv.Rows[i].Cells["SEQ"].Value + "' ");
                    }
                }

                sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@CUST_CD", txt_custcd);
                sCommand.Parameters.AddWithValue("@COMMENT", txt_comment);

                sCommand.Parameters.AddWithValue("@INPUT_DATE", dtp_input_date);
                sCommand.Parameters.AddWithValue("@INPUT_CD", txt_input_num);

                int qResult = wAdo.SqlCommandEtc(sCommand, "update_INPUT_TB");
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

        public DataTable fn_Input_Order_Detail_List(string condition)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select A.ORDER_DATE");
            sb.AppendLine("     ,A.ORDER_CD ");
            sb.AppendLine("     ,B.SEQ ");
            sb.AppendLine("     ,A.INPUT_REQ_DATE ");
            sb.AppendLine("     ,A.COMPLETE_YN  ");
            sb.AppendLine("     ,B.RAW_MAT_CD    ");
            sb.AppendLine("     ,D.RAW_MAT_NM ");
            sb.AppendLine("     ,D.SPEC ");
            sb.AppendLine("     ,B.UNIT_CD ");
            sb.AppendLine("     ,(select UNIT_NM from N_UNIT_CODE where UNIT_CD = B.UNIT_CD) AS UNIT_NM  ");
            sb.AppendLine("     ,D.RAW_MAT_GUBUN ");
            //sb.AppendLine("     ,FLOOR(ISNULL(TOTAL_AMT,0)) AS ORDER_AMT  ");
            //sb.AppendLine("     ,FLOOR(B.PRICE) AS PRICE  ");
            //sb.AppendLine("     ,FLOOR(B.TOTAL_MONEY) TOTAL_MONEY  ");
            sb.AppendLine("     ,ISNULL(TOTAL_AMT,0) AS ORDER_AMT  ");
            sb.AppendLine("     ,B.PRICE  ");
            sb.AppendLine("     ,B.TOTAL_MONEY ");
            sb.AppendLine("     ,(select S_CODE_NM from [SM_FACTORY_COM].[dbo].[T_S_CODE] where L_CODE = '300' and S_CODE = D.RAW_MAT_GUBUN) AS RAW_MAT_GUBUN_NM ");
            sb.AppendLine("     , C.INPUT_AMT ");
            sb.AppendLine("     , C.NO_INPUT_AMT");
            sb.AppendLine(" FROM F_ORDER A ");
            sb.AppendLine(" LEFT OUTER JOIN F_ORDER_DETAIL B ");
            sb.AppendLine(" ON A.ORDER_DATE = B.ORDER_DATE ");
            sb.AppendLine(" AND A.ORDER_CD = B.ORDER_CD  ");
            sb.AppendLine(" LEFT OUTER JOIN(	 ");
            sb.AppendLine("                     SELECT AA.ORDER_DATE	 ");
            sb.AppendLine("                           ,AA.ORDER_CD       ");
            sb.AppendLine("                           ,AA.SEQ ");
            sb.AppendLine("                           ,ISNULL(SUM(BB.TEMP_AMT),0) AS INPUT_AMT ");
            sb.AppendLine("                           , ISNULL(AA.TOTAL_AMT,0)-ISNULL(SUM(BB.TEMP_AMT),0) AS NO_INPUT_AMT ");
            sb.AppendLine("                     FROM F_ORDER_DETAIL AA ");
            sb.AppendLine("                     LEFT OUTER JOIN F_RAW_DETAIL BB ");
            sb.AppendLine("                     ON AA.ORDER_DATE = BB.ORDER_DATE ");
            sb.AppendLine("                         AND AA.ORDER_CD = BB.ORDER_CD ");
            sb.AppendLine("                         AND AA.SEQ = BB.ORDER_SEQ ");
            sb.AppendLine("                     GROUP BY AA.ORDER_DATE,AA.ORDER_CD,AA.SEQ,AA.TOTAL_AMT)C ");
            sb.AppendLine(" ON A.ORDER_DATE = C.ORDER_DATE  ");
            sb.AppendLine(" AND A.ORDER_CD = C.ORDER_CD ");
            sb.AppendLine(" AND B.SEQ = C.SEQ  ");

            sb.AppendLine(" LEFT OUTER JOIN N_RAW_CODE D	 ");
            sb.AppendLine(" ON B.RAW_MAT_CD = D.RAW_MAT_CD  ");

            sb.AppendLine(condition);
            sb.AppendLine(" order by A.ORDER_DATE desc, A.ORDER_CD desc, B.SEQ desc ");


            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);
        }
        public DataTable fn_Input_Detail_List(string condition)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select A.INPUT_DATE");
            sb.AppendLine("     ,A.INPUT_CD ");
            sb.AppendLine("     ,A.SEQ ");
            sb.AppendLine("     ,A.RAW_MAT_CD ");
            sb.AppendLine("     ,B.RAW_MAT_NM  ");
            sb.AppendLine("     ,B.SPEC    ");
            //sb.AppendLine("     ,A.HEAT_NO ");
            //sb.AppendLine("     ,A.HEAT_TIME ");
            sb.AppendLine("     ,A.ORDER_DATE ");
            sb.AppendLine("     ,A.ORDER_CD ");
            sb.AppendLine("     ,A.ORDER_SEQ ");
            sb.AppendLine("     ,B.RAW_MAT_GUBUN ");
            sb.AppendLine("     , (select S_CODE_NM from [SM_FACTORY_COM].[dbo].[T_S_CODE] where L_CODE = '300' and S_CODE = B.RAW_MAT_GUBUN) AS RAW_MAT_GUBUN_NM ");
            sb.AppendLine("     ,A.UNIT_CD ");
            sb.AppendLine("     , (select UNIT_NM from N_UNIT_CODE where UNIT_CD = A.UNIT_CD) AS UNIT_NM  ");
            //sb.AppendLine("     ,FLOOR(A.TOTAL_AMT) AS TOTAL_AMT ");
            //sb.AppendLine("     ,FLOOR(A.PRICE) AS PRICE ");
            //sb.AppendLine("     ,FLOOR(A.TOTAL_MONEY) AS TOTAL_MONEY ");
            sb.AppendLine("     , A.TEMP_AMT ");
            sb.AppendLine("     , A.TOTAL_AMT ");
            sb.AppendLine("     , ISNULL(A.PRICE,0) AS PRICE");
            sb.AppendLine("     , A.TOTAL_MONEY ");
            sb.AppendLine("     , A.CHECK_YN ");
            sb.AppendLine("     , (select S_CODE_NM from [SM_FACTORY_COM].[dbo].[T_S_CODE] A where L_CODE= '601'  and S_CODE = B.CHECK_GUBUN) AS CHECK_GUBUN_NM ");
            sb.AppendLine("     , case when ( SELECT count(D.OUTPUT_CD) FROM F_RAW_OUTPUT D LEFT OUTER JOIN F_RAW_DETAIL A ON A.INPUT_DATE = D.INPUT_DATE and A.INPUT_CD = D.INPUT_CD and A.SEQ = D.INPUT_SEQ " + condition + " and D.TOTAL_AMT != 0  ) = 0 THEN 0 ELSE 1 END AS OUTPUT_CD ");

            //---hsp 출력을위해 추가
            sb.AppendLine("     , right('000' + convert(varchar(4), isnull(convert(int, A.INPUT_CD), 0)), 4) AS 번호");
            sb.AppendLine("     , right('0' + convert(varchar(2), isnull(convert(int, A.SEQ), 0)), 2) AS 순번");

            //2019-10-25 유정훈 수정 (바코드 출력물에 거래처명 표시
            sb.AppendLine("     , K.CUST_CD ");
            sb.AppendLine("     , D.CUST_NM ");

            sb.AppendLine(" from F_RAW_INPUT K ");
            sb.AppendLine(" inner join F_RAW_DETAIL A ");
            sb.AppendLine(" on K.INPUT_DATE = A.INPUT_DATE ");
            sb.AppendLine("     and K.INPUT_CD = A.INPUT_CD ");
            // 수정 끝

            sb.AppendLine(" LEFT OUTER JOIN N_RAW_CODE B ");
            sb.AppendLine(" ON A.RAW_MAT_CD = B.RAW_MAT_CD ");
            sb.AppendLine(" LEFT OUTER JOIN F_RAW_CHK C");
            sb.AppendLine(" on A.INPUT_DATE = C.INPUT_DATE");
            sb.AppendLine("     and A.INPUT_CD = C.INPUT_CD");
            sb.AppendLine("     and A.SEQ = C.SEQ ");

            //2019-10-25 유정훈 바코드 출력물에 거래처명 표시 
            sb.AppendLine(" LEFT OUTER JOIN N_CUST_CODE D ");
            sb.AppendLine(" on K.CUST_CD = D.CUST_CD ");
            //수정 끝 
            sb.AppendLine(condition);

            sb.AppendLine(" order by A.INPUT_DATE desc, A.INPUT_CD desc, A.SEQ ");

            Console.WriteLine(sb.ToString());


            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);
        }


        public int delete_Input_Raw(string txt_input_num)
        {
            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                sb = new StringBuilder();
                sb.AppendLine("delete from N_RAW_CODE ");
                sb.AppendLine("    where RAW_MAT_CD = @RAW_MAT_CD  ");

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@RAW_MAT_CD", txt_input_num);

                int qResult = wAdo.SqlCommandEtc(sCommand, "delete_RAW_TB");
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
        public DataTable isRawOut(string inputDate, string inputNum)
        {
            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                sb = new StringBuilder();
                sb.AppendLine("SELECT * FROM F_RAW_OUTPUT ");
                sb.AppendLine("    where INPUT_DATE = @INPUT_DATE ");
                sb.AppendLine("    and INPUT_CD = @INPUT_CD ");

                SqlCommand sCommand = new SqlCommand(sb.ToString());
                if (sCommand.CommandText.Equals(null))
                {
                    wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                    return null;
                }


                sCommand.Parameters.AddWithValue("@INPUT_DATE", inputDate);
                sCommand.Parameters.AddWithValue("@INPUT_CD", inputNum);


                return wAdo.SqlCommandSelect(sCommand);


            }
            catch (Exception ex)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
                return null;
            }
        }

        public int deleteInput(string inputDate, string inputNum)
        {
            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                sb = new StringBuilder();
                sb.AppendLine("delete from F_RAW_INPUT ");
                sb.AppendLine("    where INPUT_DATE = @INPUT_DATE ");
                sb.AppendLine("    and INPUT_CD = @INPUT_CD ");


                sb.AppendLine("delete from F_RAW_DETAIL ");
                sb.AppendLine("    where INPUT_DATE = @INPUT_DATE ");
                sb.AppendLine("    and INPUT_CD = @INPUT_CD ");


                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@INPUT_DATE", inputDate);
                sCommand.Parameters.AddWithValue("@INPUT_CD", inputNum);

                int qResult = wAdo.SqlCommandEtc(sCommand, "delete_RAW_INPUT_TB");
                if (qResult > 0)
                {
                    return 0;  // 0 true, 1 false
                }
                else return 1;
            }
            catch (Exception ex)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
                return 9;
            }
        }


        public int updateStRaw(conDataGridView dgv)
        {
            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    sb.AppendLine("update N_RAW_CODE set ");
                    sb.AppendLine("      BAL_STOCK = (select     ");
                    sb.AppendLine("                         ISNULL((");
                    sb.AppendLine("                             select SUM(ISNULL(TOTAL_AMT,0)) from F_RAW_DETAIL ");
                    sb.AppendLine("                             where RAW_MAT_CD = '" + dgv.Rows[i].Cells["RAW_MAT_CD"].Value + "'  ");
                    sb.AppendLine("                                 and (CHECK_YN = 'Y' or CHECK_YN = 'O') ");
                    sb.AppendLine("                     group by RAW_MAT_CD),0)  ");
                    sb.AppendLine("                    -  ");
                    sb.AppendLine("                     ISNULL((  ");
                    sb.AppendLine("                     select SUM(ISNULL(TOTAL_AMT,0)) from F_RAW_OUTPUT ");
                    sb.AppendLine("                     where RAW_MAT_CD = '" + dgv.Rows[i].Cells["RAW_MAT_CD"].Value + "'   ");
                    sb.AppendLine("                     group by RAW_MAT_CD),0))");
                    sb.AppendLine("where RAW_MAT_CD = '" + dgv.Rows[i].Cells["RAW_MAT_CD"].Value + "' ");
                }

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                int qResult = wAdo.SqlCommandEtc(sCommand, "update_RAW_STOCK_TB");
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

        public int updateStRaw(string raw_mat_cd)
        {
            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("update N_RAW_CODE set ");
                sb.AppendLine("      BAL_STOCK = (select     ");
                sb.AppendLine("                         ISNULL((");
                sb.AppendLine("                             select SUM(ISNULL(TOTAL_AMT,0)) from F_RAW_DETAIL ");
                sb.AppendLine("                             where RAW_MAT_CD = '" + raw_mat_cd + "'  ");
                sb.AppendLine("                                 and (CHECK_YN = 'Y' or CHECK_YN = 'O') ");
                sb.AppendLine("                     group by RAW_MAT_CD),0)  ");
                sb.AppendLine("                    -  ");
                sb.AppendLine("                     ISNULL((  ");
                sb.AppendLine("                     select SUM(ISNULL(TOTAL_AMT,0)) from F_RAW_OUTPUT ");
                sb.AppendLine("                     where RAW_MAT_CD = '" + raw_mat_cd + "'   ");
                sb.AppendLine("                     group by RAW_MAT_CD),0))");
                sb.AppendLine("where RAW_MAT_CD = '" + raw_mat_cd + "' ");
                SqlCommand sCommand = new SqlCommand(sb.ToString());

                int qResult = wAdo.SqlCommandEtc(sCommand, "update_RAW_STOCK_TB");
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



        public DataTable fn_Flow_List(string condition)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select A.FLOW_CD");
            sb.AppendLine(" , A.FLOW_NM");
            sb.AppendLine(" , A.STORAGE_CD");
            sb.AppendLine(" , (SELECT STORAGE_NM FROM N_STORAGE_CODE WHERE STORAGE_CD = A.STORAGE_CD) AS STORAGE_NM");
            sb.AppendLine(" , FLOW_INSERT_YN ");
            sb.AppendLine(" , ITEM_IDEN_YN ");
            sb.AppendLine(" , FLOW_CHK_YN ");
            sb.AppendLine(" , TEMP_TIME_YN ");
            sb.AppendLine(" , MOLD_YN ");
            sb.AppendLine(" , POOR_TYPE_CD ");
            sb.AppendLine(" , (SELECT TYPE_NM FROM N_TYPE_CODE WHERE TYPE_CD = A.POOR_TYPE_CD) AS POOR_TYPE_NM ");
            sb.AppendLine(" , STAFF_YN ");
            sb.AppendLine(" , STAFF_CD ");
            sb.AppendLine(" , (SELECT STAFF_NM FROM N_STAFF_CODE WHERE STAFF_CD = A.STAFF_CD) AS STAFF_NM ");
            sb.AppendLine(" , A.COMMENT");
            sb.AppendLine(" from N_FLOW_CODE A ");
            sb.AppendLine(condition);
            sb.AppendLine(" order by A.FLOW_CD ");

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }

            return wAdo.SqlCommandSelect(sCommand);
        }

        public DataTable fn_Rm_Raw_List(string condition, string txt_raw_mat_nm)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select A.INPUT_DATE");           
            sb.AppendLine(" ,A.INPUT_CD ");
            sb.AppendLine(" ,Z.SEQ ");
            sb.AppendLine(" ,Z.RAW_MAT_CD ");
            sb.AppendLine(" ,C.RAW_MAT_NM ");
            sb.AppendLine(" , SUM(ISNULL(Z.TOTAL_AMT,0)) as INPUT_AMT ");
            sb.AppendLine(" , SUM(ISNULL(K.TOTAL_AMT,0)) as OUTPUT_AMT ");
            sb.AppendLine(" , SUM(ISNULL(Z.TOTAL_AMT,0)) - SUM(ISNULL(K.TOTAL_AMT,0)) as STOCK_AMT ");            
            sb.AppendLine(" from F_RAW_INPUT A ");
            sb.AppendLine(" left outer join F_RAW_DETAIL Z on A.INPUT_DATE = Z.INPUT_DATE ");
            sb.AppendLine(" and A.INPUT_CD = Z.INPUT_CD ");
            sb.AppendLine(" LEFT OUTER JOIN ( ");
            sb.AppendLine(" select INPUT_DATE,INPUT_CD,INPUT_SEQ,SUM(TOTAL_AMT)AS TOTAL_AMT from F_RAW_OUTPUT ");
            sb.AppendLine(" where 1=1  ");               
            //sb.AppendLine(" and RAW_MAT_CD = '" + raw_mat_cd + "' ");
            //sb.AppendLine(" and INPUT_DATE <= '" + srch_date + "'  ");
            sb.AppendLine(" group by INPUT_DATE,INPUT_CD,INPUT_SEQ ");
            sb.AppendLine(" )K ");
            sb.AppendLine(" on A.INPUT_DATE = K.INPUT_DATE ");
            sb.AppendLine(" and A.INPUT_CD = K.INPUT_CD ");
            sb.AppendLine(" and Z.SEQ = K.INPUT_SEQ ");
            sb.AppendLine(" left outer join N_RAW_CODE C ON Z.RAW_MAT_CD = C.RAW_MAT_CD ");
            sb.AppendLine(" WHERE 1=1 AND RAW_MAT_NM LIKE '%" + txt_raw_mat_nm + "%' ");  
            sb.AppendLine(condition);
            sb.AppendLine(" group by A.INPUT_DATE,A.INPUT_CD,Z.SEQ,Z.RAW_MAT_CD,C.RAW_MAT_NM ");                     

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);
        }

        public int insert_Haccp_Doc_Root(string rootPath, string staff_cd)
        {
            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("Select * from N_HACCP_DOCPATH ");
                sb.AppendLine("     WHERE STAFF_CD = '" + staff_cd + "' ");



                SqlCommand sCommand = new SqlCommand(sb.ToString());

                if (sCommand.CommandText.Equals(null))
                {
                    wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                    return 1;
                }
                DataTable dtTemp = wAdo.SqlCommandSelect(sCommand);

                if (dtTemp.Rows != null && dtTemp.Rows.Count > 0)
                {
                    sb = new StringBuilder();
                    sb.AppendLine("update N_HACCP_DOCPATH ");
                    sb.AppendLine("SET DOCPATH = '" + rootPath + "'  ");
                    sb.AppendLine("   where  STAFF_CD ='" + staff_cd + "' ");

                }
                else
                {
                    sb = new StringBuilder();
                    sb.AppendLine("insert into N_HACCP_DOCPATH(STAFF_CD,DOCPATH) ");
                    sb.AppendLine("   values ('" + staff_cd + "' , '" + rootPath + "') ");

                }

                sCommand = new SqlCommand(sb.ToString());

                int qResult = wAdo.SqlCommandEtc(sCommand, "insert or update HACCP root path");
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

        public int insert_Haccp_Doc_File(string destFile, string fileName, string staff_cd, string input_date, string txt_comment, string gubun)
        {
            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("Select * from F_HACCP_DOCS ");
                sb.AppendLine("     WHERE DOCPATH = '" + destFile + "' and STAFF_CD = '" + staff_cd + "'  ");



                SqlCommand sCommand = new SqlCommand(sb.ToString());

                if (sCommand.CommandText.Equals(null))
                {
                    wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                    return 1;
                }

                DataTable dtTemp = wAdo.SqlCommandSelect(sCommand);

                if (dtTemp.Rows != null && dtTemp.Rows.Count > 0)
                {
                    return 7;
                }

                sb = new StringBuilder();



                sb.AppendLine("declare @seq int ");
                sb.AppendLine("select @seq =ISNULL(MAX(INPUT_CD),0)+1 from F_HACCP_DOCS ");
                sb.AppendLine("where INPUT_DATE = '" + input_date + "' and STAFF_CD = '" + staff_cd + "'  ");

                sb.AppendLine("INSERT INTO F_HACCP_DOCS ( ");
                sb.AppendLine("  INPUT_DATE  ");
                sb.AppendLine("  ,INPUT_CD  ");
                sb.AppendLine("  ,DOC_GUBUN  ");
                sb.AppendLine("  ,STAFF_CD  ");
                sb.AppendLine("  ,DOCPATH  ");
                sb.AppendLine("  ,FNAME  ");
                sb.AppendLine("  ,COMMENT  ");
                sb.AppendLine("  ,INTIME  ");
                sb.AppendLine("  ,INSTAFF  ");
                sb.AppendLine("  ) VALUES (  ");
                sb.AppendLine("  @INPUT_DATE  ");
                sb.AppendLine("  ,@seq  ");
                sb.AppendLine("  ,@DOC_GUBUN  ");
                sb.AppendLine("  ,@STAFF_CD  ");
                sb.AppendLine("  ,@DOCPATH  ");
                sb.AppendLine("  ,@FNAME  ");
                sb.AppendLine("  ,@COMMENT  ");
                sb.AppendLine("  ,convert(varchar, getdate(), 120)");
                sb.AppendLine("  ,'" + Common.p_strStaffNo + "'  ");
                sb.AppendLine("  )  ");




                sCommand = new SqlCommand(sb.ToString());
                sCommand.Parameters.AddWithValue("@INPUT_DATE", input_date);
                sCommand.Parameters.AddWithValue("@DOC_GUBUN", gubun);
                sCommand.Parameters.AddWithValue("@STAFF_CD", staff_cd);
                sCommand.Parameters.AddWithValue("@DOCPATH", destFile);
                sCommand.Parameters.AddWithValue("@FNAME", fileName);
                sCommand.Parameters.AddWithValue("@COMMENT", txt_comment);


                int qResult = wAdo.SqlCommandEtc(sCommand, "insert HACCP_DOCS");
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

        public DataTable select_Haccp_Docs(string condition)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select INPUT_DATE ");
            sb.AppendLine("  ,INPUT_CD ");
            sb.AppendLine("  ,DOC_GUBUN ");
            sb.AppendLine("  ,STAFF_CD ");
            sb.AppendLine("  ,(SELECT STAFF_NM FROM N_STAFF_CODE WHERE A.STAFF_CD = STAFF_CD) AS STAFF_NM ");
            sb.AppendLine("  ,DOCPATH ");
            sb.AppendLine("  ,FNAME ");
            sb.AppendLine("  ,COMMENT ");
            sb.AppendLine(" from F_HACCP_DOCS A");
            sb.AppendLine(condition);


            Console.WriteLine(sb.ToString());


            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }

            return wAdo.SqlCommandSelect(sCommand);
        }

        public int Update_Haccp_Docs(string columnName, string changeValue, string docPath, string staff_cd)
        {
            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                sb = new StringBuilder();
                if (columnName.Equals("INPUT_DATE"))
                {
                    sb.AppendLine("declare @seq int ");
                    sb.AppendLine("select @seq =ISNULL(MAX(INPUT_CD),0)+1 from F_HACCP_DOCS ");
                    sb.AppendLine("where INPUT_DATE = '" + changeValue + "' and STAFF_CD = '" + staff_cd + "'   ");

                    sb.AppendLine("UPDATE F_HACCP_DOCS  ");
                    sb.AppendLine("SET " + columnName + " = '" + changeValue + "'  ");
                    sb.AppendLine(", INPUT_CD = @seq ");
                    sb.AppendLine(", UPTIME = convert(varchar, getdate(), 120)  ");
                    sb.AppendLine(", UPSTAFF = '" + Common.p_strStaffNo + "' ");
                    sb.AppendLine("  WHERE DOCPATH = @DOCPATH ");
                    sb.AppendLine("  and STAFF_CD = '" + staff_cd + "'   ");

                }
                else
                {
                    sb.AppendLine("UPDATE F_HACCP_DOCS  ");
                    sb.AppendLine("SET " + columnName + " = '" + changeValue + "'  ");
                    sb.AppendLine(", UPTIME = convert(varchar, getdate(), 120)  ");
                    sb.AppendLine(", UPSTAFF = '" + Common.p_strStaffNo + "' ");
                    sb.AppendLine("WHERE DOCPATH = @DOCPATH ");
                    sb.AppendLine("  and STAFF_CD = '" + staff_cd + "'   ");
                }

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@DOCPATH", docPath);


                int qResult = wAdo.SqlCommandEtc(sCommand, "update_HACCP_DOCS");
                if (qResult > 0)
                {
                    return 0;
                }
                else return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + " - " + e.ToString());
                wnLog.writeLog(wnLog.LOG_ERROR, e.Message + " - " + e.ToString());
                return 9;
            }
        }

        public int Delete_Haccp_Doc(string path, string staff_cd)
        {
            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                sb = new StringBuilder();

                sb.AppendLine("DELETE FROM F_HACCP_DOCS  ");
                sb.AppendLine("WHERE DOCPATH = @DOCPATH AND STAFF_CD = '" + staff_cd + "'  ");


                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@DOCPATH", path);


                int qResult = wAdo.SqlCommandEtc(sCommand, "delete_HACCP_DOCS");
                if (qResult > 0)
                {
                    return 0;
                }
                else return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + " - " + e.ToString());
                wnLog.writeLog(wnLog.LOG_ERROR, e.Message + " - " + e.ToString());
                return 9;
            }
        }        

        public string select_Haccp_Doc_Root(string staff_cd)
        {
            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("Select * from N_HACCP_DOCPATH ");
                sb.AppendLine("     WHERE STAFF_CD = '" + staff_cd + "' ");



                SqlCommand sCommand = new SqlCommand(sb.ToString());

                if (sCommand.CommandText.Equals(null))
                {
                    wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                    return "failed";
                }

                DataTable dtTemp = wAdo.SqlCommandSelect(sCommand);

                if (dtTemp.Rows != null && dtTemp.Rows.Count > 0)
                {
                    String[] strArrTemp = dtTemp.Rows[0]["DOCPATH"].ToString().Split('/');
                    string ReturnTemp = "";
                    for (int i = 0; i < strArrTemp.Length - 2; i++)
                    {
                        ReturnTemp += strArrTemp[i];
                    }
                    return ReturnTemp;

                }
                else
                {
                    return "미등록";

                }
            }
            catch (Exception e)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, e.Message + " - " + e.ToString());
                return "failed";
            }
        }
               

        public DataTable select_eye(            
            string input_date,
            string input_cd,
            string raw_mat_cd
            )
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT A.* ");
            sb.AppendLine(",B.RAW_MAT_NM ");
            sb.AppendLine(",B.SPEC ");
            sb.AppendLine("FROM F_RAW_CHK A ");
            sb.AppendLine("INNER JOIN N_RAW_CODE B ON '" + raw_mat_cd + "' = B.RAW_MAT_CD ");
            sb.AppendLine("WHERE 1=1 ");
            sb.AppendLine("AND '" + input_date + "' = A.INPUT_DATE ");
            sb.AppendLine("AND '" + input_cd + "' = A.INPUT_CD ");
            
                       
            //sb.AppendLine(" order by CAST(A.STAFF_CD as int) ");

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }

            return wAdo.SqlCommandSelect(sCommand);
        }

        public int insert_eye(
            string dtp_input_date, 
            string txt_input_cd,             
            string txt_itemnm, 
            string txt_spec, 
            string cmb_yn, 
            string cmb_ok, 
            string dtp_magin_date, 
            string txt_car_temperature, 
            string cmb_car_clean, 
            string txt_case, 
            string txt_foreign, 
            string txt_in_foreign, 
            string txt_content,
            string chk_yn,
            string txt_raw_mat_cd,
            string chk_gubun,
            string txt_seq
            )              
        {
            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                
                sb = new StringBuilder();

                if ( chk_gubun == "등록")
                {
                sb.AppendLine("declare @SEQ int ");
                sb.AppendLine("select @SEQ = ISNULL(MAX(SEQ),0)+1 from F_RAW_CHK ");
                sb.AppendLine("where INPUT_DATE = '" + dtp_input_date + "' and INPUT_CD = '" + txt_input_cd + "'  ");
                sb.AppendLine("INSERT INTO F_RAW_CHK ( ");
                sb.AppendLine("  INPUT_DATE  ");
                sb.AppendLine("  ,INPUT_CD  ");
                sb.AppendLine("  ,SEQ  ");
                sb.AppendLine("  ,GRADE_OWN_YN  ");
                sb.AppendLine("  ,GRADE_FIT_YN  ");
                sb.AppendLine("  ,EXP_DATE  ");
                sb.AppendLine("  ,CAR_TEMP  ");
                sb.AppendLine("  ,CAR_CLEAN_ST  ");
                sb.AppendLine("  ,IKON  ");
                sb.AppendLine("  ,BOW_MIXING  ");
                sb.AppendLine("  ,IN_OUT_PACK  ");
                sb.AppendLine("  ,RESULT_YN  ");
                sb.AppendLine("  ,NON_RS_COMMENT  ");
                sb.AppendLine("  ,INSTAFF  ");
                sb.AppendLine("  ,INTIME  ");
                sb.AppendLine("  ,RAW_MAT_CD  ");
                sb.AppendLine("  ) VALUES (  ");
                sb.AppendLine("  '" + dtp_input_date + "' ");
                sb.AppendLine("  ,'" + txt_input_cd + "'  ");
                sb.AppendLine("  ,@SEQ ");
                sb.AppendLine("  ,'" + cmb_yn + "'  ");
                sb.AppendLine("  ,'" + cmb_ok + "'  ");
                sb.AppendLine("  ,'" + dtp_magin_date + "'  ");
                sb.AppendLine("  ,'" + txt_car_temperature + "'  ");
                sb.AppendLine("  ,'" + cmb_car_clean + "'  ");
                sb.AppendLine("  ,'" + txt_foreign + "'  ");
                sb.AppendLine("  ,'" + txt_in_foreign + "'  ");
                sb.AppendLine("  ,'" + txt_case + "'  ");
                sb.AppendLine("  ,'" + chk_yn + "'  ");
                sb.AppendLine("  ,'" + txt_content + "'  ");
                sb.AppendLine("  ,'" + Common.p_strStaffNo + "'  ");
                sb.AppendLine("  ,convert(varchar, getdate(), 120) ");
                sb.AppendLine("  ,'" + txt_raw_mat_cd + "'  ");                
                sb.AppendLine("  )  ");
                                   
                sb.AppendLine("update F_RAW_DETAIL set");        
                sb.AppendLine("      CHECK_YN = 'Y' ");               
                sb.AppendLine("      ,UPSTAFF =  '" + Common.p_strStaffNo + "' ");
                sb.AppendLine("      ,UPTIME =  convert(varchar, getdate(), 120) ");
                sb.AppendLine(" where INPUT_DATE = '" + dtp_input_date + "' ");
                sb.AppendLine(" and INPUT_CD = '" + txt_input_cd + "' ");
                sb.AppendLine(" and SEQ = '" + txt_seq + "'");


                }

                else if (chk_gubun == "완료")
                {
                    sb.AppendLine("update F_RAW_CHK set ");
                    sb.AppendLine("      GRADE_OWN_YN = '" + cmb_yn + "'  ");
                    sb.AppendLine(",      GRADE_FIT_YN = '" + cmb_ok + "'  ");
                    sb.AppendLine(",      EXP_DATE = '" + dtp_magin_date + "'   ");
                    sb.AppendLine(",      CAR_TEMP = '" + txt_car_temperature + "' ");
                    sb.AppendLine(",      CAR_CLEAN_ST = '" + cmb_car_clean + "' ");
                    sb.AppendLine(",      IKON = '" + txt_foreign + "'  ");
                    sb.AppendLine(",      BOW_MIXING = '" + txt_in_foreign + "'     ");
                    sb.AppendLine(",      IN_OUT_PACK = '" + txt_case + "'  ");
                    sb.AppendLine(",      RESULT_YN = '" + chk_yn + "'     ");
                    sb.AppendLine(",      NON_RS_COMMENT = '" + txt_content + "'  ");
                    sb.AppendLine(",      UPSTAFF = '" + Common.p_strStaffNo + "'  ");
                    sb.AppendLine(",      UPTIME = convert(varchar, getdate(), 120)     ");

                    sb.AppendLine("where INPUT_DATE = '" + dtp_input_date + "' ");
                    sb.AppendLine("AND INPUT_CD = '" + txt_input_cd + "' ");
                    sb.AppendLine("AND SEQ = '" + txt_seq + "' ");
                }

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                if (sCommand.CommandText.Equals(null))
                {
                    wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                    return 1;
                }

                DataTable dtTemp = wAdo.SqlCommandSelect(sCommand);

                sCommand = new SqlCommand(sb.ToString());           
                              

                int qResult = wAdo.SqlCommandEtc(sCommand, "insert raw_chk");
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

        public int update_eye(
            string dtp_input_date,
            string txt_input_cd,
            string txt_seq,            
            string txt_itemnm,
            string txt_spec,
            string cmb_yn,
            string cmb_ok,
            string dtp_magin_date,
            string txt_car_temperature,
            string cmb_car_clean,
            string txt_case,
            string txt_foreign,
            string txt_in_foreign,
            string txt_content,
            string chk_yn,
            string txt_raw_mat_cd            
            )
        {
            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                             
                SqlCommand sCommand = new SqlCommand(sb.ToString());
           
                int qResult = wAdo.SqlCommandEtc(sCommand, "update_RAW_STOCK_TB");
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

        public DataTable fn_Raw_Chk_List()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select ");
            sb.AppendLine(" ,RAW_MAT_CD   ");
            sb.AppendLine(" ,INPUT_DATE   ");
            sb.AppendLine(" ,INPUT_CD   ");
            sb.AppendLine(" ,SEQ   ");
            sb.AppendLine(" ,GRADE_OWN_YN   ");
            sb.AppendLine(" ,GRADE_FIT_YN   ");
            sb.AppendLine(" ,EXP_DATE   ");
            sb.AppendLine(" ,CAR_TEMP   ");
            sb.AppendLine(" ,CAR_CLEAN_ST   ");
            sb.AppendLine(" ,IKON   ");
            sb.AppendLine(" ,BOW_MIXING   ");
            sb.AppendLine(" ,IN_OUT_PACK   ");
            sb.AppendLine(" ,RESULT_YN   ");
            sb.AppendLine(" ,NON_RS_COMMENT   ");
            sb.AppendLine(" ,INSTAFF   ");
            sb.AppendLine(" ,INTIME   ");
            sb.AppendLine(" ,UPSTAFF   ");
            sb.AppendLine(" ,UPTIME   ");                       
            sb.AppendLine(" from F_RAW_CHK ");
            //sb.AppendLine(condition);

            //sb.AppendLine(" order by CAST(A.STAFF_CD as int) ");


            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }

            return wAdo.SqlCommandSelect(sCommand);
        }

        public int OutInsert(               
            string outdate,
            string txt_comment,      
            string staff_cd,
            DataGridView dgv_main
            )
        {
            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                //sb.AppendLine("Select * from F_HACCP_DOCS ");
                //sb.AppendLine("     WHERE DOCPATH = '" + destFile + "' and STAFF_CD = '" + staff_cd + "'  ");


                SqlCommand sCommand = new SqlCommand(sb.ToString());

                if (sCommand.CommandText.Equals(null))
                {
                    wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                    return 1;
                }

                DataTable dtTemp = wAdo.SqlCommandSelect(sCommand);

                sb = new StringBuilder();

                for (int i = 0; i < dgv_main.Rows.Count; i++)
                {
                    sb.AppendLine("DECLARE @PAY_CD INT ");
                    sb.AppendLine("SELECT @PAY_CD = ISNULL(MAX(PAY_CD),0)+1 FROM F_PAY ");
                    sb.AppendLine(" WHERE PAY_DATE = '" + outdate + "'   ");




                    sb.AppendLine("INSERT INTO F_PAY( ");
                    sb.AppendLine("PAY_DATE  ");
                    sb.AppendLine(",PAY_CD  ");
                    sb.AppendLine(",STAFF_CD  ");
                    sb.AppendLine(",COMMENT  ");
                    sb.AppendLine(",INSTAFF  ");
                    sb.AppendLine(",INTIME  ");
                    sb.AppendLine(") VALUES (  ");
                    sb.AppendLine("'" + outdate + "'  ");
                    sb.AppendLine(",@PAY_CD  ");
                    sb.AppendLine(", '" + staff_cd + "' ");
                    sb.AppendLine(", '" + txt_comment + "' ");
                    sb.AppendLine(", '" + Common.p_strStaffNo + "'  ");
                    sb.AppendLine(" ,convert(varchar, getdate(), 120) ");
                    sb.AppendLine(" ) ");

                    for (int j = 0; j < dgv_main.Rows.Count; j++)
                    {                        
                        string mjukyo = dgv_main.Rows[j].Cells["JUKYO"].Value.ToString();
                        string mmoney = dgv_main.Rows[j].Cells["MONEY"].Value.ToString();
                        string mid = dgv_main.Rows[j].Cells["ID"].Value.ToString();
                        string mgubun = dgv_main.Rows[j].Cells["GUBUN"].Value.ToString();

                        if (mid != "")
                        {
                            sb.AppendLine("DECLARE @SEQ"+j+" INT ");
                            sb.AppendLine("SELECT @SEQ" + j + " = ISNULL(MAX(SEQ),0)+1 FROM F_PAY_DETAIL ");
                            sb.AppendLine(" WHERE PAY_DATE = '" + outdate + "'   ");
                            sb.AppendLine(" AND PAY_CD = @PAY_CD   ");



                            sb.AppendLine("INSERT INTO F_PAY_DETAIL( ");
                            sb.AppendLine("PAY_DATE ");
                            sb.AppendLine(",PAY_CD ");
                            sb.AppendLine(",SEQ ");
                            sb.AppendLine(",ACCU_CD ");
                            sb.AppendLine(",JUKYO ");
                            sb.AppendLine(",MONEY ");
                            sb.AppendLine(",PAY_GUBUN ");
                            sb.AppendLine(",INSTAFF ");
                            sb.AppendLine(",INTIME ");
                            sb.AppendLine(") VALUES ( ");
                            sb.AppendLine("'" + outdate + "' ");
                            sb.AppendLine(",@PAY_CD ");
                            sb.AppendLine(",@SEQ" + j + "");
                            sb.AppendLine(",'" + mid + "' ");
                            sb.AppendLine(",'" + mjukyo + "' ");
                            sb.AppendLine(",'" + mmoney.Replace(",", "") + "' ");
                            sb.AppendLine(",'" + mgubun.Replace(",", "") + "' ");
                            sb.AppendLine(",'" + Common.p_strStaffNo + "' ");
                            sb.AppendLine(" ,convert(varchar, getdate(), 120) ");
                            sb.AppendLine(" ) ");
                        }
                    }

                    //sb.AppendLine(" UPDATE F_PAY_DETAIL ");
                    //sb.AppendLine(" SET TOTAL_AMT = TEMP_AMT, CURR_AMT = TEMP_AMT, TEMP_AMT = 0, CHECK_YN = 'Y', COMPLETE_YN = 'Y' ");
                    //sb.AppendLine(" where INPUT_DATE = '" + dtp_input_date + "' ");
                    //sb.AppendLine("     and INPUT_CD = '" + txt_input_cd + "' ");
                    //sb.AppendLine("     and SEQ = '" + txt_seq + "' ");
                }

                sCommand = new SqlCommand(sb.ToString());
                                
                int qResult = wAdo.SqlCommandEtc(sCommand, "insert raw_chk");
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

        public int OutUpdate(
            string outdate,
            string outcd,
            string txt_instaff,
            string txt_comment,     
            DataGridView dgv_main,
            DataTable delGrid
            )
        {
            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("UPDATE  F_PAY SET  ");
                sb.AppendLine("COMMENT  = '" + txt_comment + "'  ");
                sb.AppendLine(",UPSTAFF  = '" + Common.p_strStaffNo + "'  ");
                sb.AppendLine(",UPTIME  = convert(varchar, getdate(), 120)   ");
               
                sb.AppendLine(" WHERE  PAY_DATE ='"+outdate+"'  ");
                sb.AppendLine(" and  PAY_CD ='"+outcd+"'  ");


                for (int i = 0; i < dgv_main.Rows.Count; i++)
                {
                    if (dgv_main.Rows[i].Cells["MAIN_SEQ"].Value != null && !dgv_main.Rows[i].Cells["MAIN_SEQ"].Value.ToString().Equals(""))
                    {
                        sb.AppendLine("UPDATE F_PAY_DETAIL SET ");
                        //sb.AppendLine("     PAY_DATE   = @STAFF_CD ");
                        //sb.AppendLine("     ,SEQ     = @STAFF_NM ");
                        sb.AppendLine("      ACCU_CD = '" + dgv_main.Rows[i].Cells["ID"].Value + "' ");
                        sb.AppendLine("     ,JUKYO  = '" + dgv_main.Rows[i].Cells["JUKYO"].Value + "'");
                        sb.AppendLine("     ,MONEY  = '" + dgv_main.Rows[i].Cells["MONEY"].Value + "'");
                        sb.AppendLine("     ,UPSTAFF  = '" + Common.p_strStaffNo + "'");
                        sb.AppendLine("     ,UPTIME  = convert(varchar, getdate(), 120) ");
                        sb.AppendLine(" WHERE PAY_DATE = '" + outdate + "' ");
                        sb.AppendLine(" AND PAY_CD = '" + outcd + "'");
                        sb.AppendLine(" AND SEQ = '" + dgv_main.Rows[i].Cells["MAIN_SEQ"].Value + "'");

                    }

                    else
                    {
                        string mjukyo = dgv_main.Rows[i].Cells["JUKYO"].Value.ToString();
                        string mmoney = dgv_main.Rows[i].Cells["MONEY"].Value.ToString();
                        string mid = dgv_main.Rows[i].Cells["ID"].Value.ToString();
                        string mgubun = dgv_main.Rows[i].Cells["GUBUN"].Value.ToString();


                        sb.AppendLine("DECLARE @SEQ" + i + " INT ");
                        sb.AppendLine("SELECT @SEQ" + i + " = ISNULL(MAX(SEQ),0)+1 FROM F_PAY_DETAIL ");
                        sb.AppendLine(" WHERE PAY_DATE = '" + outdate + "'   ");
                        sb.AppendLine(" and PAY_CD = '"+outcd+"'   ");

                        sb.AppendLine("INSERT INTO F_PAY_DETAIL( ");
                        sb.AppendLine("PAY_DATE ");
                        sb.AppendLine(",PAY_CD ");
                        sb.AppendLine(",SEQ ");
                        sb.AppendLine(",ACCU_CD ");
                        sb.AppendLine(",JUKYO ");
                        sb.AppendLine(",MONEY ");
                        sb.AppendLine(",PAY_GUBUN ");
                        sb.AppendLine(",INSTAFF ");
                        sb.AppendLine(",INTIME ");
                        sb.AppendLine(") VALUES ( ");
                        sb.AppendLine("'" + outdate + "' ");
                        sb.AppendLine(", '"+outcd+"' ");
                        sb.AppendLine(",@SEQ" + i + "");
                        sb.AppendLine(",'" + mid + "' ");
                        //sb.AppendLine(",'" + mgubun + "' ");
                        sb.AppendLine(",'" + mjukyo.Replace(",","") + "' ");
                        sb.AppendLine(",'" + mmoney.Replace(",", "") + "' ");
                        sb.AppendLine(",'" + mgubun + "' ");
                        sb.AppendLine(",'" + Common.p_strStaffNo + "' ");
                        sb.AppendLine(" ,convert(varchar, getdate(), 120) ");
                        sb.AppendLine(" ) ");

                    }
                    //sb.AppendLine(" AND SEQ = '" + dgv_srch.Rows[i].Cells["SEQ"].Value + "'");                                        
                }


                for (int i = 0; i < delGrid.Rows.Count; i++)
                {
                    
                        sb.AppendLine("DELETE FROM F_PAY_DETAIL  ");
                        sb.AppendLine(" WHERE ");
                        sb.AppendLine(" PAY_DATE = '"+delGrid.Rows[i]["PAY_DATE"].ToString()+"'  ");
                        sb.AppendLine(" AND PAY_CD = '" + delGrid.Rows[i]["PAY_CD"].ToString() + "'  ");
                        sb.AppendLine(" AND SEQ = '" + delGrid.Rows[i]["SEQ"].ToString() + "'  ");
 
                    
                }


                SqlCommand sCommand = new SqlCommand(sb.ToString());
                int qResult = wAdo.SqlCommandEtc(sCommand, "update_RAW_STOCK_TB");
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

        public int OutDelete(
            string dtp_outdate,
            string txt_num    
            )
        {           

            try
            {                              

                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                sb = new StringBuilder();
                sb.AppendLine("DELETE FROM F_PAY ");
                sb.AppendLine("    WHERE PAY_CD = '" + txt_num + "'  ");
                sb.AppendLine("    AND PAY_DATE = '" + dtp_outdate + "'  ");


                sb.AppendLine("DELETE FROM F_PAY_DETAIL ");
                sb.AppendLine("    WHERE PAY_CD = '" + txt_num + "'  ");
                sb.AppendLine("    AND PAY_DATE = '" + dtp_outdate + "'  ");
    
                SqlCommand sCommand = new SqlCommand(sb.ToString());

                int qResult = wAdo.SqlCommandEtc(sCommand, "delete_USER_TB");
                if (qResult > 0)
                {                    
                    return 0;  // 0 true, 1 false
                }
                else
                    return 1;
               
            }
            catch (Exception e)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, e.Message + " - " + e.ToString());
                return 1;
            }
        }

        public DataTable OutMainSelect(string condition)
        {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("SELECT A.* ");
                sb.AppendLine("     ,G.ACCU_NM     ");
                sb.AppendLine("     ,A.PAY_GUBUN     ");
                sb.AppendLine(" FROM F_PAY_DETAIL A ");
                sb.AppendLine(" LEFT OUTER JOIN N_ACCOUNT_CODE G ON G.ACCU_CD = A.ACCU_CD ");
                sb.AppendLine(" LEFT OUTER JOIN F_PAY B ON B.PAY_DATE = A.PAY_DATE AND B.PAY_CD = A.PAY_CD ");
                sb.AppendLine(condition);
            
            
            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }

            return wAdo.SqlCommandSelect(sCommand);
        }

        public DataTable OutSrchSelect(string condition)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT A.*");
            sb.AppendLine(" , F.STAFF_NM  ");
            sb.AppendLine(" , B.ITEM_CNT   ");
            sb.AppendLine(" , B.TOTAL_MONEY  ");
            sb.AppendLine("  FROM F_PAY A  ");
            sb.AppendLine("   INNER JOIN N_STAFF_CODE F ON F.STAFF_CD = A.STAFF_CD    ");
            sb.AppendLine("   LEFT OUTER JOIN (SELECT PAY_DATE, PAY_CD, COUNT(*) AS ITEM_CNT, ISNULL(SUM(MONEY),0) AS TOTAL_MONEY    ");
            sb.AppendLine("   FROM F_PAY_DETAIL     ");
            sb.AppendLine(condition);
            sb.AppendLine("  GROUP BY PAY_DATE, PAY_CD) B ON B.PAY_CD = A.PAY_CD    ");            
            
            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);
        } 
       
        public DataTable queryAccu()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT ");
            sb.AppendLine(" ACCU_CD, ACCU_NM ");
            sb.AppendLine(" FROM N_ACCOUNT_CODE");
            sb.AppendLine(" WHERE 1=1 ");

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }

            return wAdo.SqlCommandSelect(sCommand);
        }

        public DataTable queryStaff()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT ");
            sb.AppendLine(" STAFF_CD, STAFF_NM");
            sb.AppendLine(" FROM N_STAFF_CODE");
            sb.AppendLine(" WHERE 1=1 ");

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }

            return wAdo.SqlCommandSelect(sCommand);
        }

        #region 계정등록 로직
        public int AccuInsert(
            string txt_cd,
            string txt_nm,
            string chk            
            )
        {
            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                if (sCommand.CommandText.Equals(null))
                {
                    wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                    return 1;
                }

                DataTable dtTemp = wAdo.SqlCommandSelect(sCommand);

                sb = new StringBuilder();

                sb.AppendLine(" INSERT INTO N_ACCOUNT_CODE( ");
                sb.AppendLine("  ACCU_CD  ");
                sb.AppendLine("  ,ACCU_NM  ");
                sb.AppendLine("  ,POOR_TYPE_YN  ");     
                sb.AppendLine("  ) VALUES (  ");
                sb.AppendLine("  '" + txt_cd + "' ");
                sb.AppendLine("  ,'" + txt_nm + "'  ");
                sb.AppendLine("  ,'" + chk + "'  ");                           
                sb.AppendLine("  )  ");

                sCommand = new SqlCommand(sb.ToString());



                int qResult = wAdo.SqlCommandEtc(sCommand, "insert raw_chk");
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
        public int AccuUpdate(
            string txt_cd,
            string txt_nm,
            string chk 
            )
        {
            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("UPDATE N_ACCOUNT_CODE SET ");
                sb.AppendLine("      ACCU_NM = '" + txt_nm + "' ");
                sb.AppendLine("      POOR_TYPE_YN = '" + chk + "' ");

                sb.AppendLine(" WHERE ACCU_CD = '" + txt_cd + "' ");


                SqlCommand sCommand = new SqlCommand(sb.ToString());

                int qResult = wAdo.SqlCommandEtc(sCommand, "update_RAW_STOCK_TB");
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

        public DataTable AccuSelect(string condition)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT ACCU_CD ");
            sb.AppendLine("     ,ACCU_NM     ");
            sb.AppendLine("     ,POOR_TYPE_YN     ");            
            sb.AppendLine(" FROM N_ACCOUNT_CODE ");
            sb.AppendLine(condition);

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }

            return wAdo.SqlCommandSelect(sCommand);
        }

        public int AccuDelete(string txt_cd)
        {
            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                sb = new StringBuilder();
                sb.AppendLine("DELETE FROM N_ACCOUNT_CODE ");
                sb.AppendLine("    WHERE ACCU_CD = '" + txt_cd + "' ");

                SqlCommand sCommand = new SqlCommand(sb.ToString());                

                int qResult = wAdo.SqlCommandEtc(sCommand, "delete_USER_TB");
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

        #endregion 계정등록 로직
    }
}
