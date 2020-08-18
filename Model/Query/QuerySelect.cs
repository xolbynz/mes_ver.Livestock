using 스마트팩토리.CLS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace 스마트팩토리.Model.Query
{
    class QuerySelect
    {

        wnAdo wAdo = new wnAdo();

        //탑 메뉴 
        public DataTable sTopMenuList()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select ");
            sb.AppendLine("     a.UtilCd ");
            sb.AppendLine("     , a.TopID ");
            sb.AppendLine("     , a.TopName ");
            sb.AppendLine("     , a.TopPath ");
            sb.AppendLine("     , b.UTIL_COLOR ");
            sb.AppendLine(" from T_Sales_TopMenu a ");
            sb.AppendLine(" inner join N_UTIL_CODE b ");
            sb.AppendLine(" on a.UtilCd = b.UTIL_CD ");
            sb.AppendLine(" order by a.SortNo asc ");

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);
        }

        public DataTable sSumMenuList(string uCD, string sID)
        {
            Console.WriteLine(uCD + "///" + sID);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select ");
            sb.AppendLine("     a.UtilCd ");
            sb.AppendLine("     , b.TopID ");
            sb.AppendLine("     , b.SubID ");
            sb.AppendLine("     , b.SubName ");
            sb.AppendLine("     , b.AsmName ");
            sb.AppendLine("     , a.TopPath ");
            sb.AppendLine("     , c.UTIL_TOP "); //솔빈 추가 
            sb.AppendLine(" from T_Sales_TopMenu a ");
            sb.AppendLine(" inner join T_Sales_SubMenu b ");
            sb.AppendLine(" on a.UtilCd = b.UtilCd ");
            sb.AppendLine(" and a.TopID = b.TopID ");
            sb.AppendLine(" inner join N_UTIL_CODE c "); //솔빈 추가 
            sb.AppendLine("  on a.UtilCd = c.UTIL_CD  "); //솔빈 추가 
            sb.AppendLine(" where a.UtilCd = @UtilCd ");
            sb.AppendLine("     and b.TopID = @TopID  ");
            sb.AppendLine("     and b.VIEW_YN = 'Y'  ");
            sb.AppendLine(" order by a.UtilCd, b.SortNo asc ");

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            sCommand.Parameters.AddWithValue("@UtilCd", uCD);
            sCommand.Parameters.AddWithValue("@TopID", sID);
            return wAdo.SqlCommandSelect(sCommand);
        }

        //기초 코드 조회 - 시찬
        public DataTable cCodeList(string dbName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select *");
            //sb.AppendLine("      UNIT_CD ");
            //sb.AppendLine("     , UNIT_NM ");
            //sb.AppendLine("     , COMMENT ");
            sb.AppendLine(" from  " + dbName);

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);

        }

        //생산
        public DataTable sPlanDetailList(string condition) 
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select A.PLAN_DATE");
            sb.AppendLine("     ,A.PLAN_CD ");
            sb.AppendLine("     ,A.ITEM_CD ");
            sb.AppendLine("     ,B.ITEM_NM ");
            sb.AppendLine("     ,A.TARGET_AMT ");
            sb.AppendLine("     ,A.WORKER_NUM ");
            sb.AppendLine("     ,A.CONDITION ");
            sb.AppendLine("     ,A.EXP_DATE ");
            sb.AppendLine("     ,A.PREV_COMMENT ");
            sb.AppendLine("     ,A.PACK_INFO ");
            sb.AppendLine("     ,A.WORKROOM_THC ");
            sb.AppendLine("     ,A.JUMUN_DATE ");
            sb.AppendLine("     ,A.JUMUN_CD ");
            sb.AppendLine("     ,A.JUMUN_SEQ ");
            sb.AppendLine("     ,A.LOT_NO ");
            sb.AppendLine("     ,A.ORDER_YN ");
            sb.AppendLine("     ,A.STATE_CD ");
            sb.AppendLine("     ,C.S_CODE_NM AS STATE_NM ");
            sb.AppendLine(" from  F_PLAN A ");
            sb.AppendLine(" left outer join N_ITEM_CODE B ");
            sb.AppendLine(" on A.ITEM_CD = B.ITEM_CD ");
            sb.AppendLine(" inner join T_S_CODE C ");
            sb.AppendLine(" on C.L_CODE = '450' ");
            sb.AppendLine("     and C.S_CODE = A.STATE_CD ");
            sb.AppendLine(" where 1=1 ");
            sb.AppendLine(condition);

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);
        }

        //생산
        public DataTable sPlanRawList(string condition)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("select A.PLAN_DATE");
            sb.AppendLine("     ,A.PLAN_CD ");
            sb.AppendLine("     ,A.SEQ ");
            sb.AppendLine("     ,A.RAW_MAT_CD ");
            sb.AppendLine("     ,B.RAW_MAT_NM ");
            sb.AppendLine("     ,B.SPEC  ");
            sb.AppendLine("     ,B.INPUT_UNIT ");
            sb.AppendLine("     ,(select UNIT_NM from N_UNIT_CODE where UNIT_CD = B.INPUT_UNIT) as INPUT_UNIT_NM  ");
            sb.AppendLine("     ,B.OUTPUT_UNIT ");
            sb.AppendLine("     ,(select UNIT_NM from N_UNIT_CODE where UNIT_CD = B.OUTPUT_UNIT) as OUTPUT_UNIT_NM   ");
            sb.AppendLine("     ,A.SOYO_AMT ");
            sb.AppendLine("     ,A.TOTAL_AMT AS TOTAL_SOYO_AMT ");
            sb.AppendLine("     ,B.BAL_STOCK  ");
            sb.AppendLine("     ,B.BASE_WEIGHT  ");
            sb.AppendLine("     ,B.BASE_UNIT  ");
            sb.AppendLine("     ,B.CVR_RATIO  ");
            sb.AppendLine("     ,ISNULL(C.TOTAL_AMT,0) AS UNIT_TOTAL_AMT  ");
            sb.AppendLine(" from  F_PLAN Z  ");
            sb.AppendLine(" inner join F_PLAN_RAW A");
            sb.AppendLine(" on Z.PLAN_DATE = A.PLAN_DATE ");
            sb.AppendLine("     and Z.PLAN_CD = A.PLAN_CD ");
            sb.AppendLine(" left outer join N_RAW_CODE B ");
            sb.AppendLine(" on A.RAW_MAT_CD = B.RAW_MAT_CD ");
            sb.AppendLine(" left outer join N_ITEM_COMP C ");
            sb.AppendLine(" on Z.ITEM_CD = C.ITEM_CD ");
            sb.AppendLine("     and A.RAW_MAT_CD = C.RAW_MAT_CD ");
            sb.AppendLine(" where 1=1 ");
            sb.AppendLine(condition);

            //sb.AppendLine("select A.PLAN_DATE");
            //sb.AppendLine("     ,A.PLAN_CD ");
            //sb.AppendLine("     ,A.SEQ ");
            //sb.AppendLine("     ,A.RAW_MAT_CD ");
            //sb.AppendLine("     ,B.RAW_MAT_NM ");
            //sb.AppendLine("     ,B.SPEC  ");
            //sb.AppendLine("     ,B.INPUT_UNIT ");
            //sb.AppendLine("     ,(select UNIT_NM from N_UNIT_CODE where UNIT_CD = B.INPUT_UNIT) as INPUT_UNIT_NM  ");
            //sb.AppendLine("     ,B.OUTPUT_UNIT ");
            //sb.AppendLine("     ,(select UNIT_NM from N_UNIT_CODE where UNIT_CD = B.OUTPUT_UNIT) as OUTPUT_UNIT_NM   ");
            //sb.AppendLine("     ,A.SOYO_AMT ");
            //sb.AppendLine("     ,A.TOTAL_AMT AS TOTAL_SOYO_AMT ");
            //sb.AppendLine("     ,B.BAL_STOCK  ");
            //sb.AppendLine("     ,B.BASE_WEIGHT  ");
            //sb.AppendLine("     ,B.BASE_UNIT  ");
            //sb.AppendLine("     ,B.CVR_RATIO  ");
            //sb.AppendLine(" from  F_PLAN_RAW A ");
            //sb.AppendLine(" left outer join N_RAW_CODE B ");
            //sb.AppendLine(" on A.RAW_MAT_CD = B.RAW_MAT_cD ");
            //sb.AppendLine(" where 1=1 ");
            //sb.AppendLine(condition);

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);
        }

        //생산 조치사항 
        public DataTable sPlanSubjectList(string condition)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select A.PLAN_DATE");
            sb.AppendLine("     ,A.PLAN_CD ");
            sb.AppendLine("     ,A.SUBJECT_CD ");
            sb.AppendLine("     ,A.SEQ ");
            sb.AppendLine("     ,A.SUBJECT_NM ");
            sb.AppendLine(" from  F_PLAN_SUBJECT A ");
            sb.AppendLine(" where 1=1 ");
            sb.AppendLine(condition);
            sb.AppendLine(" order by A.SEQ ");

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);
        }
        public DataTable sSoyoRsList(string condition) 
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" select U.RAW_MAT_CD ");
            sb.AppendLine("      , K.RAW_MAT_NM ");
            sb.AppendLine("      , K.SPEC ");
            sb.AppendLine("      , U.TOTAL_AMT * K.CVR_RATIO AS TOTAL_AMT  ");
            sb.AppendLine("      , ISNULL(K.BAL_STOCK,0) AS BAL_STOCK   ");
            sb.AppendLine("      , ((U.TOTAL_AMT * K.CVR_RATIO)-K.BAL_STOCK) as RS_AMT  ");
            sb.AppendLine("      , K.INPUT_PRICE ");
            sb.AppendLine("      , ((U.TOTAL_AMT * K.CVR_RATIO)-K.BAL_STOCK) * K.INPUT_PRICE AS TOTAL_MONEY    ");
           // sb.AppendLine("      , K.OUTPUT_PRICE ");
            sb.AppendLine("      , K.INPUT_UNIT ");
            sb.AppendLine("      , (select UNIT_NM from N_UNIT_CODE where UNIT_CD = K.INPUT_UNIT) AS INPUT_UNIT_NM  ");
            sb.AppendLine("      , K.OUTPUT_UNIT ");
            sb.AppendLine("      , (select UNIT_NM from N_UNIT_CODE where UNIT_CD = K.OUTPUT_UNIT) AS OUTPUT_UNIT_NM  ");
            sb.AppendLine("      , K.CUST_CD ");
            sb.AppendLine("      , P.CUST_NM ");
            sb.AppendLine("      , P.CUST_GUBUN  ");
            sb.AppendLine("      , DENSE_RANK() OVER(ORDER BY K.CUST_CD) AS CUST_NUM  ");
            sb.AppendLine("      , (select S_CODE_NM   ");
            sb.AppendLine("        from [SM_FACTORY_COM].[dbo].[T_S_CODE]  ");
            sb.AppendLine("        where L_CODE = '200' and S_CODE = P.CUST_GUBUN) AS CUST_GUBUN_NM   ");
            sb.AppendLine(" from ( ");
            sb.AppendLine("      select RAW_MAT_CD, SUM(TOTAL_AMT) AS TOTAL_AMT   ");
            sb.AppendLine("      from (  ");
            sb.AppendLine("          select A.PLAN_DATE    ");
            sb.AppendLine("               , A.PLAN_CD     ");
            sb.AppendLine("               , A.ITEM_CD     ");
            sb.AppendLine("               , C.RAW_MAT_CD     ");
            sb.AppendLine("               , ISNULL(B.TOTAL_AMT*A.TARGET_AMT,0) AS TOTAL_AMT     ");
            sb.AppendLine("          from F_PLAN A  ");
            sb.AppendLine("          left outer join N_ITEM_COMP B ");
            sb.AppendLine("          on A.ITEM_CD = B.ITEM_CD  ");
            sb.AppendLine("          left outer join N_RAW_CODE C ");
            sb.AppendLine("          on B.RAW_MAT_CD = C.RAW_MAT_CD  ");
            sb.AppendLine(condition);
            sb.AppendLine("         ) Z ");
            sb.AppendLine("         group by RAW_MAT_CD ");
            sb.AppendLine("     ) U ");
            sb.AppendLine(" left outer join N_RAW_CODE K ");
            sb.AppendLine(" on U.RAW_MAT_CD = K.RAW_MAT_CD ");
            sb.AppendLine(" left outer join N_CUST_CODE P ");
            sb.AppendLine(" on K.CUST_CD = P.CUST_CD ");
            sb.AppendLine(" where P.CUST_GUBUN = '2' ");

            //sb.AppendLine(" left outer join ( ");
            //sb.AppendLine("     select ZZ.RAW_MAT_CD,SUM(TOTAL_AMT)AS INST_RAW_AMT ");
            //sb.AppendLine("     from F_WORK_INST KK  ");
            //sb.AppendLine("     inner join F_WORK_INST_DETAIL ZZ ");
            //sb.AppendLine("     on KK.W_INST_DATE = ZZ.W_INST_DATE ");
            //sb.AppendLine("         and KK.W_INST_CD = ZZ.W_INST_CD  ");
            //sb.AppendLine("     where COMPLETE_YN = 'N'  ");
            //sb.AppendLine("     group by ZZ.RAW_MAT_CD ");
            //sb.AppendLine(" )W  ");
            //sb.AppendLine(" on U.RAW_MAT_CD = W.RAW_MAT_CD  ");
            sb.AppendLine(" order by K.CUST_CD,U.RAW_MAT_CD ");

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);
        }


        #region 작업일보등록 리스트
        public DataTable SelectWorkList(string condition)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT A.WORK_DATE");
            sb.AppendLine(",A.WORK_CD");
            sb.AppendLine(",A.ST_STATUS_YN");
            sb.AppendLine(",A.COMPLETE_YN");
            sb.AppendLine(",A.ITEM_CD");
            sb.AppendLine(",C.JUMUN_DATE");
            sb.AppendLine(",C.JUMUN_CD");
            sb.AppendLine(",(select ITEM_NM FROM N_ITEM_CODE B WHERE B.ITEM_CD = A.ITEM_CD) AS ITEM_NM");
            sb.AppendLine("FROM F_WORK_RESULT A");
            sb.AppendLine("INNER JOIN F_PLAN C");
            sb.AppendLine("ON A.PLAN_DATE = C.PLAN_DATE");
            sb.AppendLine("  and A.PLAN_CD = C.PLAN_CD");
            sb.AppendLine("  where 1=1 ");
            sb.AppendLine(condition);

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);
        }

        #endregion

        public DataTable SelectWorkMainList(string condition)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT A.WORK_DATE");
            sb.AppendLine(",A.WORK_CD");
            sb.AppendLine(",A.ITEM_CD");
            sb.AppendLine(",A.TARGET_AMT");
            sb.AppendLine(",A.RS_AMT");
            sb.AppendLine(",A.ALL_INPUT_AMT");
            sb.AppendLine(",A.ALL_LOSS");
            sb.AppendLine(",A.ALL_RS_INPUT_AMT");
            sb.AppendLine(",A.PLAN_DATE");
            sb.AppendLine(",A.PLAN_CD");
            sb.AppendLine(",A.COMPLETE_YN");
            sb.AppendLine(",(select ITEM_NM FROM N_ITEM_CODE B WHERE A.ITEM_CD = B.ITEM_CD) AS ITEM_NM");
            sb.AppendLine(",(select LOT_NO FROM F_PLAN C WHERE C.PLAN_DATE = A.PLAN_DATE AND C.PLAN_CD = A.PLAN_CD) AS LOT_NO");
            sb.AppendLine("FROM F_WORK_RESULT A");
            sb.AppendLine(condition);

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);
        }

        public DataTable SelectWorkRawList(string condition)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT A.RAW_MAT_CD");
            sb.AppendLine("  ,B.RAW_MAT_NM");
            sb.AppendLine("  ,B.CVR_RATIO");
            sb.AppendLine(",B.INPUT_UNIT");
            sb.AppendLine(",(select UNIT_NM from N_UNIT_CODE where UNIT_CD = B.INPUT_UNIT) as INPUT_UNIT_NM");
            sb.AppendLine(",B.OUTPUT_UNIT");
            sb.AppendLine(",(select UNIT_NM from N_UNIT_CODE where UNIT_CD = B.OUTPUT_UNIT) as OUTPUT_UNIT_NM");
            sb.AppendLine(",ISNULL(C.TOTAL_AMT,0) AS TOTAL_AMT");
            //sb.AppendLine(",ISNULL(C.SOYO_AMT,0) AS SOYO_AMT");
            sb.AppendLine(",ISNULL(C.SOYO_AMT,0) AS UNIT_TOTAL_AMT");
            sb.AppendLine(",A.RS_AMT");
            sb.AppendLine(",A.COMMENT");
            sb.AppendLine(",A.SEQ");
            sb.AppendLine(",C.SEQ AS PLAN_SEQ");
            sb.AppendLine("FROM F_WORK_RESULT_RAW A");
            sb.AppendLine("  inner join N_RAW_CODE B  ");
            sb.AppendLine(" on A.RAW_MAT_CD = B.RAW_MAT_CD");
            sb.AppendLine("inner join F_PLAN_RAW C");
            sb.AppendLine("on C.PLAN_DATE = A.PLAN_DATE");
            sb.AppendLine("and C.PLAN_CD = A.PLAN_CD ");
            sb.AppendLine("and C.SEQ = A.PLAN_SEQ ");
       
            sb.AppendLine(condition);

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);
        }

        public DataTable SelectWorkFlowList(string condition)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT A.FLOW_CD");
            sb.AppendLine(",(select FLOW_NM FROM N_FLOW_CODE B WHERE B.FLOW_CD = A.FLOW_CD ) AS FLOW_NM");
            sb.AppendLine(",A.LOSS");
            sb.AppendLine(",A.INPUT_AMT");
            sb.AppendLine(",A.RS_INPUT_AMT");
            sb.AppendLine(",A.COMMENT");
            sb.AppendLine(",A.SEQ");
            sb.AppendLine(" FROM F_WORK_RESULT_FLOW A");
         

            sb.AppendLine(condition);

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);
        }

        public DataTable SelectSalesList(string condition)
        {
            StringBuilder sb = new StringBuilder();
      
            sb.AppendLine("   SELECT           ");
            sb.AppendLine("  A.SALES_DATE      ");
            sb.AppendLine("  ,A.SALES_CD       ");
            sb.AppendLine("  ,A.TAX_CD         ");
            sb.AppendLine("  ,P.COMMENT        ");
            sb.AppendLine("  ,A.CUST_CD        ");
            sb.AppendLine("  ,C.CUST_NM        ");
            sb.AppendLine("  ,C.BALANCE        ");
            sb.AppendLine("  ,D.ITEM_AMT       ");
            sb.AppendLine("  ,O.JISI_DATE       ");
            sb.AppendLine("  ,O.JISI_CD       ");
            sb.AppendLine("  ,A.ALL_TOTAL_MONEY AS RS_TOTAL_MONEY   ");
            sb.AppendLine("  ,A.ALL_SUPPLY_MONEY                      ");
            sb.AppendLine("  ,A.ALL_TAX_MONEY                    ");
            sb.AppendLine("  FROM F_SALES A                         ");
            sb.AppendLine("  left outer join F_OUT_JISI O           ");
            sb.AppendLine("  on A.JISI_DATE = O.JISI_DATE             ");
            sb.AppendLine("  and A.JISI_CD = O.JISI_CD              ");
            sb.AppendLine("  left outer join N_CUST_CODE C          ");
            sb.AppendLine("  on A.CUST_CD = C.CUST_CD               ");
            sb.AppendLine("  left outer join F_PLAN P               ");
            sb.AppendLine("  on P.PLAN_DATE = O.PLAN_DATE           ");
            sb.AppendLine("  and P.PLAN_CD = O.PLAN_CD              ");
            sb.AppendLine("  left outer join (SELECT count(*) AS ITEM_AMT, SALES_DATE, SALES_CD FROM F_SALES_DETAIL GROUP BY SALES_DATE, SALES_CD ) D ");
            sb.AppendLine("  on A.SALES_DATE = D.SALES_DATE    ");
            sb.AppendLine("  and A.SALES_CD = D.SALES_CD      ");
            sb.AppendLine(condition);                                                                                          

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);
        }

        public DataTable SelectSales_Detail_List(string condition)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(" select ");
            sb.AppendLine(" A.VAT_CD ");
            sb.AppendLine(" ,A.SALES_DATE ");
            sb.AppendLine(" ,A.SALES_CD ");
            sb.AppendLine(" ,A.SEQ ");
            sb.AppendLine(" ,A.PRODUCT_GUBUN ");
            sb.AppendLine(" ,A.JISI_DATE ");
            sb.AppendLine(" ,A.JISI_CD ");
            sb.AppendLine(" ,A.JISI_SEQ ");
            sb.AppendLine(" ,A.INPUT_DATE ");
            sb.AppendLine(" ,A.INPUT_CD ");
            sb.AppendLine(" ,A.INPUT_SEQ ");
            sb.AppendLine(" ,A.TOTAL_AMT ");
            sb.AppendLine(" ,A.TOTAL_PRICE ");
            sb.AppendLine(" ,B.STORE_1F AS CURR_AMT ");
            sb.AppendLine(" ,B.EXPRT_DATE ");

            sb.AppendLine(" ,B.RAW_MAT_CD AS PRODUCT_CD ");
            sb.AppendLine(" ,(SELECT RAW_MAT_NM FROM N_RAW_CODE WHERE B.RAW_MAT_CD = RAW_MAT_CD) AS PRODUCT_NM ");
            sb.AppendLine(" ,B.CHUGJONG_CD ");
            sb.AppendLine(" ,B.CLASS_CD ");
            sb.AppendLine(" ,B.COUNTRY_CD ");
            sb.AppendLine(" ,B.TYPE_CD ");
            sb.AppendLine(" ,C.LABEL_NM ");
            sb.AppendLine(" ,(SELECT UNIT_NM FROM N_UNIT_CODE WHERE C.OUTPUT_UNIT = UNIT_CD) AS UNIT_NM ");

            sb.AppendLine(" ,(SELECT CHUGJONG_NM FROM N_CHUGJONG_CODE WHERE B.CHUGJONG_CD = CHUGJONG_CD) AS CHUGJONG_NM ");
            sb.AppendLine(" ,(SELECT CLASS_NM FROM N_MEAT_CLASS_CODE WHERE B.CLASS_CD = CLASS_CD) AS CLASS_NM ");
            sb.AppendLine(" ,(SELECT COUNTRY_NM FROM N_RAW_COUNTRY_CODE WHERE B.COUNTRY_CD = COUNTRY_CD) AS COUNTRY_NM ");
            sb.AppendLine(" ,(SELECT TYPE_NM FROM N_TYPE_CODE WHERE C.TYPE_CD = TYPE_CD) AS TYPE_NM ");
            sb.AppendLine(" ,B.UNION_CD  ");

            sb.AppendLine(" ,A.STORE_GUBUN ");
            sb.AppendLine(" ,'' AS SPEC ");

            sb.AppendLine(" FROM F_SALES_DETAIL A ");
            sb.AppendLine(" LEFT OUTER JOIN F_RAW_DETAIL B");
            sb.AppendLine(" ON B.INPUT_DATE = A.INPUT_DATE ");
            sb.AppendLine(" AND B.INPUT_CD = A.INPUT_CD ");
            sb.AppendLine(" AND B.SEQ = A.INPUT_SEQ ");
            sb.AppendLine(" LEFT OUTER JOIN N_RAW_CODE C");
            sb.AppendLine(" ON B.RAW_MAT_CD = C.RAW_MAT_CD ");
            sb.AppendLine(condition);
            sb.AppendLine("AND PRODUCT_GUBUN = '1' AND A.STORE_GUBUN = 'STORE_1F'  ");

            sb.AppendLine(" UNION ALL ");


            sb.AppendLine(" select ");
            sb.AppendLine(" A.VAT_CD ");
            sb.AppendLine(" ,A.SALES_DATE ");
            sb.AppendLine(" ,A.SALES_CD ");
            sb.AppendLine(" ,A.SEQ ");
            sb.AppendLine(" ,A.PRODUCT_GUBUN ");
            sb.AppendLine(" ,A.JISI_DATE ");
            sb.AppendLine(" ,A.JISI_CD ");
            sb.AppendLine(" ,A.JISI_SEQ ");
            sb.AppendLine(" ,A.INPUT_DATE ");
            sb.AppendLine(" ,A.INPUT_CD ");
            sb.AppendLine(" ,A.INPUT_SEQ ");
            sb.AppendLine(" ,A.TOTAL_AMT ");
            sb.AppendLine(" ,A.TOTAL_PRICE ");
            sb.AppendLine(" ,B.STORE_1NF AS CURR_AMT ");
            sb.AppendLine(" ,B.EXPRT_DATE ");

            sb.AppendLine(" ,B.RAW_MAT_CD AS PRODUCT_CD ");
            sb.AppendLine(" ,(SELECT RAW_MAT_NM FROM N_RAW_CODE WHERE B.RAW_MAT_CD = RAW_MAT_CD) AS PRODUCT_NM ");
            sb.AppendLine(" ,B.CHUGJONG_CD ");
            sb.AppendLine(" ,B.CLASS_CD ");
            sb.AppendLine(" ,B.COUNTRY_CD ");
            sb.AppendLine(" ,B.TYPE_CD ");
            sb.AppendLine(" ,C.LABEL_NM ");
            sb.AppendLine(" ,(SELECT UNIT_NM FROM N_UNIT_CODE WHERE C.OUTPUT_UNIT = UNIT_CD) AS UNIT_NM ");

            sb.AppendLine(" ,(SELECT CHUGJONG_NM FROM N_CHUGJONG_CODE WHERE B.CHUGJONG_CD = CHUGJONG_CD) AS CHUGJONG_NM ");
            sb.AppendLine(" ,(SELECT CLASS_NM FROM N_MEAT_CLASS_CODE WHERE B.CLASS_CD = CLASS_CD) AS CLASS_NM ");
            sb.AppendLine(" ,(SELECT COUNTRY_NM FROM N_RAW_COUNTRY_CODE WHERE B.COUNTRY_CD = COUNTRY_CD) AS COUNTRY_NM ");
            sb.AppendLine(" ,(SELECT TYPE_NM FROM N_TYPE_CODE WHERE C.TYPE_CD = TYPE_CD) AS TYPE_NM ");
            sb.AppendLine(" ,B.UNION_CD  ");

            sb.AppendLine(" ,A.STORE_GUBUN ");
            sb.AppendLine(" ,'' AS SPEC ");

            sb.AppendLine(" FROM F_SALES_DETAIL A ");
            sb.AppendLine(" LEFT OUTER JOIN F_RAW_DETAIL B");
            sb.AppendLine(" ON B.INPUT_DATE = A.INPUT_DATE ");
            sb.AppendLine(" AND B.INPUT_CD = A.INPUT_CD ");
            sb.AppendLine(" AND B.SEQ = A.INPUT_SEQ ");
            sb.AppendLine(" LEFT OUTER JOIN N_RAW_CODE C");
            sb.AppendLine(" ON B.RAW_MAT_CD = C.RAW_MAT_CD ");
            sb.AppendLine(condition);
            sb.AppendLine("AND PRODUCT_GUBUN = '1'  AND A.STORE_GUBUN = 'STORE_1NF' ");

            sb.AppendLine(" UNION ALL ");

            sb.AppendLine(" select ");
            sb.AppendLine(" A.VAT_CD ");
            sb.AppendLine(" ,A.SALES_DATE ");
            sb.AppendLine(" ,A.SALES_CD ");
            sb.AppendLine(" ,A.SEQ ");
            sb.AppendLine(" ,A.PRODUCT_GUBUN ");
            sb.AppendLine(" ,A.JISI_DATE ");
            sb.AppendLine(" ,A.JISI_CD ");
            sb.AppendLine(" ,A.JISI_SEQ ");
            sb.AppendLine(" ,A.INPUT_DATE ");
            sb.AppendLine(" ,A.INPUT_CD ");
            sb.AppendLine(" ,A.INPUT_SEQ ");
            sb.AppendLine(" ,A.TOTAL_AMT ");
            sb.AppendLine(" ,A.TOTAL_PRICE ");
            sb.AppendLine(" ,B.STORE_UF AS CURR_AMT ");
            sb.AppendLine(" ,B.EXPRT_DATE ");

            sb.AppendLine(" ,B.RAW_MAT_CD AS PRODUCT_CD ");
            sb.AppendLine(" ,(SELECT RAW_MAT_NM FROM N_RAW_CODE WHERE B.RAW_MAT_CD = RAW_MAT_CD) AS PRODUCT_NM ");
            sb.AppendLine(" ,B.CHUGJONG_CD ");
            sb.AppendLine(" ,B.CLASS_CD ");
            sb.AppendLine(" ,B.COUNTRY_CD ");
            sb.AppendLine(" ,B.TYPE_CD ");
            sb.AppendLine(" ,C.LABEL_NM ");
            sb.AppendLine(" ,(SELECT UNIT_NM FROM N_UNIT_CODE WHERE C.OUTPUT_UNIT = UNIT_CD) AS UNIT_NM ");

            sb.AppendLine(" ,(SELECT CHUGJONG_NM FROM N_CHUGJONG_CODE WHERE B.CHUGJONG_CD = CHUGJONG_CD) AS CHUGJONG_NM ");
            sb.AppendLine(" ,(SELECT CLASS_NM FROM N_MEAT_CLASS_CODE WHERE B.CLASS_CD = CLASS_CD) AS CLASS_NM ");
            sb.AppendLine(" ,(SELECT COUNTRY_NM FROM N_RAW_COUNTRY_CODE WHERE B.COUNTRY_CD = COUNTRY_CD) AS COUNTRY_NM ");
            sb.AppendLine(" ,(SELECT TYPE_NM FROM N_TYPE_CODE WHERE C.TYPE_CD = TYPE_CD) AS TYPE_NM ");
            sb.AppendLine(" ,B.UNION_CD  ");

            sb.AppendLine(" ,A.STORE_GUBUN ");
            sb.AppendLine(" ,'' AS SPEC ");

            sb.AppendLine(" FROM F_SALES_DETAIL A ");
            sb.AppendLine(" LEFT OUTER JOIN F_RAW_DETAIL B");
            sb.AppendLine(" ON B.INPUT_DATE = A.INPUT_DATE ");
            sb.AppendLine(" AND B.INPUT_CD = A.INPUT_CD ");
            sb.AppendLine(" AND B.SEQ = A.INPUT_SEQ ");
            sb.AppendLine(" LEFT OUTER JOIN N_RAW_CODE C");
            sb.AppendLine(" ON B.RAW_MAT_CD = C.RAW_MAT_CD ");
            sb.AppendLine(condition);
            sb.AppendLine("AND PRODUCT_GUBUN = '1'   AND A.STORE_GUBUN = 'STORE_UF'  ");

            sb.AppendLine(" UNION ALL ");

            sb.AppendLine(" select ");
            sb.AppendLine(" A.VAT_CD ");
            sb.AppendLine(" ,A.SALES_DATE ");
            sb.AppendLine(" ,A.SALES_CD ");
            sb.AppendLine(" ,A.SEQ ");
            sb.AppendLine(" ,A.PRODUCT_GUBUN ");
            sb.AppendLine(" ,A.JISI_DATE ");
            sb.AppendLine(" ,A.JISI_CD ");
            sb.AppendLine(" ,A.JISI_SEQ ");
            sb.AppendLine(" ,A.INPUT_DATE ");
            sb.AppendLine(" ,A.INPUT_CD ");
            sb.AppendLine(" ,A.INPUT_SEQ ");
            sb.AppendLine(" ,A.TOTAL_AMT ");
            sb.AppendLine(" ,A.TOTAL_PRICE ");
            sb.AppendLine(" ,B.REMAIN_AMT AS CURR_AMT ");
            sb.AppendLine(" ,B.EXPRT_DATE ");

            sb.AppendLine(" ,B.RAW_MAT_CD AS PRODUCT_CD ");
            sb.AppendLine(" ,(SELECT RAW_MAT_NM FROM N_RAW_CODE WHERE B.RAW_MAT_CD = RAW_MAT_CD) AS PRODUCT_NM ");
            sb.AppendLine(" ,B.CHUGJONG_CD ");
            sb.AppendLine(" ,B.CLASS_CD ");
            sb.AppendLine(" ,B.COUNTRY_CD ");
            sb.AppendLine(" ,B.TYPE_CD ");
            sb.AppendLine(" ,C.LABEL_NM ");
            sb.AppendLine(" ,(SELECT UNIT_NM FROM N_UNIT_CODE WHERE C.OUTPUT_UNIT = UNIT_CD) AS UNIT_NM ");

            sb.AppendLine(" ,(SELECT CHUGJONG_NM FROM N_CHUGJONG_CODE WHERE B.CHUGJONG_CD = CHUGJONG_CD) AS CHUGJONG_NM ");
            sb.AppendLine(" ,(SELECT CLASS_NM FROM N_MEAT_CLASS_CODE WHERE B.CLASS_CD = CLASS_CD) AS CLASS_NM ");
            sb.AppendLine(" ,(SELECT COUNTRY_NM FROM N_RAW_COUNTRY_CODE WHERE B.COUNTRY_CD = COUNTRY_CD) AS COUNTRY_NM ");
            sb.AppendLine(" ,(SELECT TYPE_NM FROM N_TYPE_CODE WHERE C.TYPE_CD = TYPE_CD) AS TYPE_NM ");
            sb.AppendLine(" ,B.UNION_CD  ");

            sb.AppendLine(" ,A.STORE_GUBUN ");
            sb.AppendLine(" ,'' AS SPEC ");

            sb.AppendLine(" FROM F_SALES_DETAIL A ");
            sb.AppendLine(" LEFT OUTER JOIN F_RAW_DETAIL B");
            sb.AppendLine(" ON B.INPUT_DATE = A.INPUT_DATE ");
            sb.AppendLine(" AND B.INPUT_CD = A.INPUT_CD ");
            sb.AppendLine(" AND B.SEQ = A.INPUT_SEQ ");
            sb.AppendLine(" LEFT OUTER JOIN N_RAW_CODE C");
            sb.AppendLine(" ON B.RAW_MAT_CD = C.RAW_MAT_CD ");
            sb.AppendLine(condition);
            sb.AppendLine("AND PRODUCT_GUBUN = '1'  AND A.STORE_GUBUN = 'REMAIN_AMT'  ");

            sb.AppendLine(" UNION ALL ");

            sb.AppendLine(" select ");
            sb.AppendLine(" A.VAT_CD ");
            sb.AppendLine(" ,A.SALES_DATE ");
            sb.AppendLine(" ,A.SALES_CD ");
            sb.AppendLine(" ,A.SEQ ");
            sb.AppendLine(" ,A.PRODUCT_GUBUN ");
            sb.AppendLine(" ,A.JISI_DATE ");
            sb.AppendLine(" ,A.JISI_CD ");
            sb.AppendLine(" ,A.JISI_SEQ ");
            sb.AppendLine(" ,A.INPUT_DATE ");
            sb.AppendLine(" ,A.INPUT_CD ");
            sb.AppendLine(" ,A.INPUT_SEQ ");
            sb.AppendLine(" ,A.TOTAL_AMT ");
            sb.AppendLine(" ,A.TOTAL_PRICE ");
            sb.AppendLine(" ,B.CURR_AMT ");
            sb.AppendLine(" ,B.EXPRT_DATE ");

            sb.AppendLine(" ,B.ITEM_CD AS PRODUCT_CD ");
            sb.AppendLine(" ,(SELECT ITEM_NM FROM N_ITEM_CODE WHERE B.ITEM_CD = ITEM_CD) AS PRODUCT_NM ");
            sb.AppendLine(" ,C.CHUGJONG_CD ");
            sb.AppendLine(" ,C.CLASS_CD ");
            sb.AppendLine(" ,C.COUNTRY_CD ");
            sb.AppendLine(" ,C.TYPE_CD ");
            sb.AppendLine(" ,C.LABEL_NM ");
            sb.AppendLine(" ,(SELECT UNIT_NM FROM N_UNIT_CODE WHERE C.UNIT_CD = UNIT_CD) AS UNIT_NM ");

            sb.AppendLine(" ,(SELECT CHUGJONG_NM FROM N_CHUGJONG_CODE WHERE C.CHUGJONG_CD = CHUGJONG_CD) AS CHUGJONG_NM ");
            sb.AppendLine(" ,(SELECT CLASS_NM FROM N_MEAT_CLASS_CODE WHERE C.CLASS_CD = CLASS_CD) AS CLASS_NM ");
            sb.AppendLine(" ,(SELECT COUNTRY_NM FROM N_RAW_COUNTRY_CODE WHERE C.COUNTRY_CD = COUNTRY_CD) AS COUNTRY_NM ");
            sb.AppendLine(" ,(SELECT TYPE_NM FROM N_TYPE_CODE WHERE C.TYPE_CD = TYPE_CD) AS TYPE_NM ");
            sb.AppendLine(" ,B.A_UNION_CD AS UNION_CD ");

            sb.AppendLine(" ,A.STORE_GUBUN ");
            sb.AppendLine(" ,C.SPEC AS SPEC ");

            sb.AppendLine(" FROM F_SALES_DETAIL A ");
            sb.AppendLine(" LEFT OUTER JOIN F_ITEM_INPUT_DETAIL B");
            sb.AppendLine(" ON B.INPUT_DATE = A.INPUT_DATE ");
            sb.AppendLine(" AND B.INPUT_CD = A.INPUT_CD ");
            sb.AppendLine(" AND B.SEQ = A.INPUT_SEQ ");
            sb.AppendLine(" LEFT OUTER JOIN N_ITEM_CODE C");
            sb.AppendLine(" ON C.ITEM_CD = B.ITEM_CD ");
            sb.AppendLine(condition);
            sb.AppendLine("AND PRODUCT_GUBUN = '2'  ");
            sb.AppendLine(" ORDER BY A.SALES_DATE, A.SALES_CD, A.SEQ ");

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);
        }

        public DataTable SelectReport_Cust_List(string condition)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(" SELECT ");
            sb.AppendLine(" A.SALES_DATE ");
            sb.AppendLine(" ,A.SALES_CD ");
            sb.AppendLine(" ,A.VAT_CD ");
            sb.AppendLine(" ,A.COMMENT ");
            sb.AppendLine(" ,A.CUST_CD ");
            sb.AppendLine(" ,B.CUST_NM ");
            sb.AppendLine(" ,B.BALANCE ");
            sb.AppendLine(" ,B.COUNTRY_CD");
            sb.AppendLine("  ,C.TOTAL_AMT ");
            sb.AppendLine(" ,C.PRICE ");
            sb.AppendLine(" ,C.TOTAL_MONEY ");
            sb.AppendLine(" ,(C.COMMENT) AS ITEM_COMMENT ");
            sb.AppendLine(" ,D.ITEM_NM");
            sb.AppendLine(" ,D.SPEC ");
            sb.AppendLine(" FROM F_SALES A ");
            sb.AppendLine(" inner join N_CUST_CODE B  ");
            sb.AppendLine(" ON A.CUST_CD = B.CUST_CD  ");
            sb.AppendLine(" inner join F_SALES_DETAIL C   ");
            sb.AppendLine(" ON C.SALES_DATE = A.SALES_DATE  ");
            sb.AppendLine(" AND C.SALES_CD = A.SALES_CD    ");
            sb.AppendLine(" inner join N_ITEM_CODE D     ");
            sb.AppendLine(" ON D.ITEM_CD = C.ITEM_CD     ");

            sb.AppendLine(" UNION ALL ");
            sb.AppendLine(" SELECT '-일계-' ");
            sb.AppendLine(" ,999999 ");
            sb.AppendLine(" ,'' ");
            sb.AppendLine(" ,'' ");
            sb.AppendLine(" ,'' ");
            sb.AppendLine(" ,'' ");
            sb.AppendLine(" ,SUM(B.BALANCE) AS BALANCE ");
            sb.AppendLine(" ,'' ");
            sb.AppendLine(" ,SUM(C.TOTAL_AMT) AS TOTAL_AMT");
            sb.AppendLine("  ,SUM(C.PRICE) AS PRICE ");
            sb.AppendLine(" ,SUM(C.TOTAL_MONEY) AS TOTAL_MONEY ");
            sb.AppendLine(" ,'' ");
            sb.AppendLine(" ,'' ");
            sb.AppendLine(" ,'' ");
            sb.AppendLine(" FROM F_SALES A ");
            sb.AppendLine(" inner join N_CUST_CODE B  ");
            sb.AppendLine(" ON A.CUST_CD = B.CUST_CD  ");
            sb.AppendLine(" inner join F_SALES_DETAIL C   ");
            sb.AppendLine(" ON C.SALES_DATE = A.SALES_DATE  ");
            sb.AppendLine(" AND C.SALES_CD = A.SALES_CD    ");
            sb.AppendLine("  GROUP BY A.SALES_DATE    ");

            sb.AppendLine(condition);

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);
        }

        public DataTable SelectReport_Item_List(string condition)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(" SELECT ");
            sb.AppendLine(" A.ITEM_CD ");
            sb.AppendLine(" ,B.ITEM_NM ");
            sb.AppendLine(" ,B.SPEC ");
            sb.AppendLine(" , B.BAL_STOCK ");
            sb.AppendLine(" ,A.INPUT_DATE ");
            sb.AppendLine(" ,ISNULL(INPUT_ITEM_AMT,0) AS INPUT_ITEM_AMT ");
            sb.AppendLine(" ,ISNULL(OUT_ITEM_AMT,0) AS OUT_ITEM_AMT ");
            sb.AppendLine(" FROM F_ITEM_INPUT A ");
            sb.AppendLine(" inner join N_ITEM_CODE B  ");
            sb.AppendLine(" ON A.ITEM_CD = B.ITEM_CD ");
            sb.AppendLine(" LEFT join (SELECT INPUT_DATE,ITEM_CD,SUM(INPUT_AMT)AS INPUT_ITEM_AMT FROM F_ITEM_INPUT   ");
            sb.AppendLine(" GROUP BY ITEM_CD,INPUT_DATE) D ");
            sb.AppendLine(" on A.ITEM_CD = D.ITEM_CD ");
            sb.AppendLine("  left join (SELECT ITEM_CD ,OUT_DATE,SUM(OUT_AMT) AS OUT_ITEM_AMT FROM F_ITEM_OUT_DETAIL");
            sb.AppendLine("  WHERE OUT_DATE = '"+condition+"'");
            sb.AppendLine("  GROUP BY ITEM_CD,OUT_DATE) C");
            sb.AppendLine("  on A.ITEM_CD = C.ITEM_CD");
            sb.AppendLine("WHERE A.INPUT_DATE = '" + condition + "'");

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);
        }

        public DataTable SelectItemSoyo(string condition)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(" select ");
            sb.AppendLine(" A.INPUT_DATE ");
            sb.AppendLine(" ,A.INPUT_CD ");
            sb.AppendLine(" , A.ITEM_CD ");
            sb.AppendLine(" , B.ITEM_NM ");
            sb.AppendLine(" , ISNULL(A.INPUT_AMT,0) AS INPUT_AMT ");
            sb.AppendLine(" , ISNULL(A.CURR_AMT,0) AS CURR_AMT ");
            sb.AppendLine(" from F_ITEM_INPUT A ");
            sb.AppendLine(" left outer join N_ITEM_CODE B ");
            sb.AppendLine(" on A.ITEM_CD = B.ITEM_CD ");
            sb.AppendLine(" where 1=1 ");
            sb.AppendLine(condition);
            sb.AppendLine(" order by INPUT_DATE, INPUT_CD ");

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);
        }

        public DataTable tvDeptList(string condition)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(" select ");
            sb.AppendLine("  DEPT_CD ");
            sb.AppendLine(" , LV ");
            sb.AppendLine(" , PARENT_DEPT_CD ");
            sb.AppendLine(" , DEPT_NM ");
            sb.AppendLine(" , ORD ");
            sb.AppendLine(" from N_DEPT_CODE_ND ");
            sb.AppendLine(" where 1=1 ");
            sb.AppendLine(condition);
            sb.AppendLine(" order by LV");
            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);
        }

        public DataTable tvDeptMaxLvCnt()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(" select ISNULL(MAX(LV),0) as MAX_LV from N_DEPT_CODE_ND ");
            sb.AppendLine(" group by LV ");

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);
        }
    }
}
