using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 스마트팩토리.CLS;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
namespace 스마트팩토리.Model.Query
{
    class scQuery
    {
        StringBuilder sb;
        SqlCommand sCommand;
        wnAdo wAdo;
        private DataRow adoRow;

        public DataTable grdSelectFlow(string code, string dbName)
        {

            wAdo = new wnAdo();



            sb = new StringBuilder();
            sb.AppendLine("select  ");
            sb.AppendLine("     FLOW_CD ");
            sb.AppendLine("     , FLOW_NM ");
            sb.AppendLine("     , STORAGE_CD ");
            sb.AppendLine("     , FLOW_CHK_YN ");
            sb.AppendLine("     , FLOW_INSERT_YN ");
            sb.AppendLine("     , COMMENT");
            sb.AppendLine("     , POOR_TYPE_CD");
            sb.AppendLine("     , STAFF_CD  ");
            sb.AppendLine("     from  N_FLOW_CODE");
            sb.AppendLine("      where  ");
            sb.AppendLine("      FLOW_CD = @CODE  ");



            sCommand = new SqlCommand(sb.ToString());
            sCommand.Parameters.AddWithValue("@CODE", code);

            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);
        }

        public DataTable selectSalesDetailList(string salesDate, string salesCd)
        {

            wAdo = new wnAdo();
            sb = new StringBuilder();

            sb.AppendLine("select  ");
            sb.AppendLine("     A.SEQ  ");
            sb.AppendLine("     , A.ITEM_CD  ");
            sb.AppendLine("     , A.TOTAL_AMT");
            sb.AppendLine("     , A.PRICE");
            sb.AppendLine("     , A.TOTAL_MONEY  ");
            sb.AppendLine("     , A.SALES_GUBUN  ");
            sb.AppendLine("     , A.TAX_CD    ");
            sb.AppendLine("     , A.COMMENT    ");
            sb.AppendLine("     , isnull(B.SOO_MONEY,0)as SOO_MONEY    ");
            sb.AppendLine("     , isnull(B.DC_MONEY,0) as DC_MONEY   ");
            sb.AppendLine("     from F_SALES_DETAIL A  ");
            sb.AppendLine("     left outer join (select SALES_DATE, SALES_CD, SEQ, sum(SOO_MONEY)as SOO_MONEY,sum(DC_MONEY) as DC_MONEY   from F_COLLECT_DETAIL group by SALES_DATE, SALES_CD, SEQ) B  ");
            sb.AppendLine("     on A.SALES_DATE = B.SALES_DATE  ");
            sb.AppendLine("     and A.SALES_CD = B.SALES_CD  ");
            sb.AppendLine("     and A.SEQ = B.SEQ ");
            sb.AppendLine("      where A.SALES_DATE = '" + salesDate + "'");
            sb.AppendLine("      and A.SALES_CD = '" + salesCd + "'");

            sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);
        }


        public DataTable selectCustBalance(string custCd)
        {

            wAdo = new wnAdo();



            sb = new StringBuilder();
            sb.AppendLine("select  ");
            sb.AppendLine("     BALANCE ");
            sb.AppendLine("     , TAX_CD  ");
            sb.AppendLine("     , COMMENT  ");
            sb.AppendLine("     from N_CUST_CODE  ");
            sb.AppendLine("      where CUST_CD = '" + custCd + "'");

            sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);
        }

        public DataTable selectSooDeatailList(string sooDate, string sooCd)
        {

            wAdo = new wnAdo();

            sb = new StringBuilder();
            sb.AppendLine("select  ");
            sb.AppendLine("       A.SEQ  ");
            sb.AppendLine("     , A.SOO_MONEY  ");
            sb.AppendLine("     ,(select CUST_NM from N_CUST_CODE where CUST_CD = B.CUST_CD) as CUST_NM ");
            sb.AppendLine("     ,(select BALANCE from N_CUST_CODE where CUST_CD = B.CUST_CD) as BALANCE  ");
            sb.AppendLine("     , A.DC_MONEY  ");
            sb.AppendLine("     , A.SALES_DATE  ");
            sb.AppendLine("     , A.SALES_CD  ");
            sb.AppendLine("     , A.COMMENT  ");
            sb.AppendLine("     , C.TOTAL_MONEY ");
            sb.AppendLine("     , B.COMMENT AS SOO_COMM ");
            sb.AppendLine("     from F_COLLECT_DETAIL A  ");
            sb.AppendLine("      inner join F_SALES_DETAIL C  ");
            sb.AppendLine("     on A.SALES_CD = C.SALES_CD  ");
            sb.AppendLine("     and A.SALES_DATE = C.SALES_DATE  ");
            sb.AppendLine("     and A.SEQ = C.SEQ  ");
            sb.AppendLine("     inner join F_COLLECT B ");
            sb.AppendLine("     on A.SOO_CD = B.SOO_CD  ");
            sb.AppendLine("     and A.SOO_DATE = B.SOO_DATE  ");
            sb.AppendLine("     where  A.SOO_DATE ='" + sooDate + "'  ");
            sb.AppendLine("     and  A.SOO_CD ='" + sooCd + "'  ");

            sCommand = new SqlCommand(sb.ToString());







            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);
        }

        public DataTable selectSooList(string custCd)
        {
            wAdo = new wnAdo();
            sb = new StringBuilder();
            sb.AppendLine("select  ");
            sb.AppendLine("     A.SOO_DATE ");
            sb.AppendLine("     , A.SOO_CD ");
            sb.AppendLine("     , A.CUST_CD ");
            sb.AppendLine("     , (select CUST_NM from N_CUST_CODE where CUST_CD = A.CUST_CD) AS CUST_NM ");
            sb.AppendLine("     , A.TOTAL_MONEY ");
            sb.AppendLine("     , A.SOO_GUBUN  ");
            sb.AppendLine("     , B.BALANCE  ");
            sb.AppendLine("     from F_COLLECT A ");
            sb.AppendLine("     left outer join N_CUST_CODE B ");
            sb.AppendLine("     ON A.CUST_CD = B.CUST_CD ");
            sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);
        }

        public DataTable selectGiveList(string custCd)
        {
            wAdo = new wnAdo();
            sb = new StringBuilder();
            sb.AppendLine("select  ");
            sb.AppendLine("     A.GIVE_DATE ");
            sb.AppendLine("     , A.GIVE_CD ");
            sb.AppendLine("     , A.CUST_CD ");
            sb.AppendLine("     , (select CUST_NM from N_CUST_CODE where CUST_CD = A.CUST_CD) AS CUST_NM ");
            sb.AppendLine("     , A.TOTAL_MONEY ");
            sb.AppendLine("     , A.GIVE_GUBUN  ");
            sb.AppendLine("     , B.BALANCE  ");
            sb.AppendLine("     from F_GIVE A ");
            sb.AppendLine("     left outer join N_CUST_CODE B ");
            sb.AppendLine("     ON A.CUST_CD = B.CUST_CD ");
            sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);
        }



        public DataTable selectSalesList(string custCd)
        {

            wAdo = new wnAdo();



            sb = new StringBuilder();
            sb.AppendLine("select  ");
            sb.AppendLine("     A.SALES_DATE ");
            sb.AppendLine("     , A.SALES_CD ");
            sb.AppendLine("     , A.CUST_CD ");
            sb.AppendLine("     , A.VAT_CD ");
            sb.AppendLine("     , A.PRINT_YN ");
            sb.AppendLine("     , A.OUT_DATE");
            sb.AppendLine("     , A.OUT_CD");
            sb.AppendLine("     , A.COMPLETE_YN  ");
            sb.AppendLine("     , A.COMMENT ");
            sb.AppendLine("     , B.TOTAL_MONEY  ");
            sb.AppendLine("     , C.BALANCE  ");
            sb.AppendLine("     from F_SALES A ");
            sb.AppendLine("     inner join (select SALES_DATE, SALES_CD, SUM(TOTAL_MONEY) as TOTAL_MONEY from F_SALES_DETAIL ");
            sb.AppendLine("      group by SALES_DATE,SALES_CD) B  ");
            sb.AppendLine("      on A.SALES_DATE =  B.SALES_DATE   ");
            sb.AppendLine("      AND A.SALES_CD = B.SALES_CD  ");
            sb.AppendLine("      inner join N_CUST_CODE C  ");
            sb.AppendLine("      on A.CUST_CD = C.CUST_CD  ");
            sb.AppendLine("      where A.CUST_CD = '" + custCd + "'");
            sb.AppendLine("      AND A.COMPLETE_YN = 'N'");


            sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);
        }


        public DataTable grdSelectBank(string code, string dbName)
        {
            wAdo = new wnAdo();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select ");
            sb.AppendLine("     BANK_CD ");
            sb.AppendLine("     , BANK_NM ");
            sb.AppendLine("     , COMMENT ");
            sb.AppendLine("     , COUNTRY_CD ");
            sb.AppendLine("     , DOMESTIC_FEE ");
            sb.AppendLine("     , INTERNATIONAL_FEE ");
            sb.AppendLine("     , USED_CD ");
            sb.AppendLine("     , ACCOUNT_NUM ");
            sb.AppendLine("     , ACCOUNT_HOLDER ");



            //sb.AppendLine("     , UNIT_NM ");
            //sb.AppendLine("     , COMMENT ");
            sb.AppendLine(" from " + dbName);
            sb.AppendLine("where BANK_CD = @CODE");

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            sCommand.Parameters.AddWithValue("@CODE", code);
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);

        }


        public DataTable selectLScode(string gbCd)
        {
            wAdo = new wnAdo();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select *");

            //sb.AppendLine("     , UNIT_NM ");
            //sb.AppendLine("     , COMMENT ");
            sb.AppendLine(" from T_S_CODE");
            sb.AppendLine("where L_CODE = @GUBUN");

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            sCommand.Parameters.AddWithValue("@GUBUN", gbCd);
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);

        }



        //기초 코드 조회 - 시찬
        public DataTable selectGubun(
            string gbCd
            , string dbName)
        {
            wAdo = new wnAdo();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select *");
            //sb.AppendLine("      UNIT_CD ");
            //sb.AppendLine("     , UNIT_NM ");
            //sb.AppendLine("     , COMMENT ");
            sb.AppendLine(" from  " + dbName);
            sb.AppendLine("where L_CODE = @GUBUN");

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            sCommand.Parameters.AddWithValue("@GUBUN", gbCd);
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);

        }

        public DataTable selectExchange()
        {
            wAdo = new wnAdo();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select *");
            sb.AppendLine(" from  N_USD_INFO ");

            SqlCommand sCommand = new SqlCommand(sb.ToString());

            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);

        }


        public DataTable grdSelectCountry(
            string code
            , string dbName)
        {
            wAdo = new wnAdo();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select *");
            sb.AppendLine(" from " + dbName);
            sb.AppendLine(" where  COUNTRY_CD = @CODE ");


            SqlCommand sCommand = new SqlCommand(sb.ToString());
            sCommand.Parameters.AddWithValue("@CODE", code);

            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);
        }

        public DataTable selectItemOut(string today)
        {

            wAdo = new wnAdo();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select ");
            sb.AppendLine("       OUT_DATE");
            sb.AppendLine("      , OUT_CD");
            sb.AppendLine("      , CUST_CD");
            sb.AppendLine("      , (select CUST_NM FROM N_CUST_CODE WHERE CUST_CD = A.CUST_CD ) AS CUST_NM");

            //sb.AppendLine("    , RECIPIENT");
            //sb.AppendLine("    , RECIPIENT_PHONE");
            //sb.AppendLine("    , REQ_COMMENT");
            //sb.AppendLine("    , COMP_PHONE_CD");
            //sb.AppendLine("    , CUST_ADDR_CD ");
            //sb.AppendLine("    , OUT_REQ_CD");
            //sb.AppendLine("    , OUT_REQ_DATE");
            //sb.AppendLine("    , OUT_SIGN_YN");
            //sb.AppendLine("    , OUT_SIGN_CMT");
            //sb.AppendLine("    , QUAL_CD");
            //sb.AppendLine("    , QUAL_DATE");
            //sb.AppendLine("    , QUAL_SIGN_YN");
            //sb.AppendLine("    , QUAL_SIGN_CMT");
            //sb.AppendLine("    , ST_MG_CD");
            //sb.AppendLine("    , ST_MG_DATE");
            //sb.AppendLine("    , ST_MG_SIGN_YN");
            //sb.AppendLine("    , ST_MG_SIGN_CMT");
            //sb.AppendLine("    , DELIVERY_CD");
            //sb.AppendLine("    , DELIVERY_INVOICE");
            //sb.AppendLine("    , DELIVERY_CMT");
            //sb.AppendLine("    , INSTAFF");
            //sb.AppendLine("    , INTIME");
            //sb.AppendLine("    , UPSTAFF");
            //sb.AppendLine("    , UPTIME");
            sb.AppendLine(" from F_ITEM_OUT A");
            //sb.AppendLine(" where OUT_DATE =  '" + today + "'");

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);

        }
        public DataTable selectGrdItemOut(string today, string out_cd)
        {

            wAdo = new wnAdo();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select ");
            sb.AppendLine("     RECIPIENT");
            sb.AppendLine("    , RECIPIENT_PHONE");
            sb.AppendLine("    , REQ_COMMENT");
            sb.AppendLine("    , COMP_PHONE_CD");
            sb.AppendLine("    , CUST_ADDR_CD ");
            sb.AppendLine("    , OUT_REQ_CD");
            sb.AppendLine("    , OUT_REQ_DATE");
            sb.AppendLine("    , OUT_SIGN_YN");
            sb.AppendLine("    , OUT_SIGN_CMT");
            sb.AppendLine("    , QUAL_CD");
            sb.AppendLine("    , QUAL_DATE");
            sb.AppendLine("    , QUAL_SIGN_YN");
            sb.AppendLine("    , QUAL_SIGN_CMT");
            sb.AppendLine("    , ST_MG_CD");
            sb.AppendLine("    , ST_MG_DATE");
            sb.AppendLine("    , ST_MG_SIGN_YN");
            sb.AppendLine("    , ST_MG_SIGN_CMT");
            sb.AppendLine("    , DELIVERY_CD");
            sb.AppendLine("    , DELIVERY_INVOICE");
            sb.AppendLine("    , DELIVERY_CMT");
            sb.AppendLine("    , PROG_CD ");
            sb.AppendLine("    , INSTAFF");
            sb.AppendLine("    , INTIME");
            sb.AppendLine("    , UPSTAFF");
            sb.AppendLine("    , UPTIME");
            sb.AppendLine(" from F_ITEM_OUT");
            sb.AppendLine(" where OUT_DATE =  '" + today + "'");
            sb.AppendLine(" AND OUT_CD =  '" + out_cd + "'");


            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);

        }

        public DataTable selectPriceInfoSearch(string custCd)
        {

            wAdo = new wnAdo();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select ");
            sb.AppendLine("       B.CUST_CD");
            sb.AppendLine("      , A.ITEM_CD");
            sb.AppendLine("      , A.ITEM_NM");
            sb.AppendLine("      , isnull(B.B_BOX_PRICE,0) as B_BOX_PRICE ");
            sb.AppendLine("      , isnull(B.S_BOX_PRICE,0) as S_BOX_PRICE ");
            sb.AppendLine("      , isnull(B.UNIT_PRICE,0) as UNIT_PRICE");
            sb.AppendLine("      , B.INSTAFF");
            sb.AppendLine("      , B.INTIME");
            sb.AppendLine("      , B.UPSTAFF");
            sb.AppendLine("      , B.UPTIME");
            sb.AppendLine(" from N_ITEM_CODE A ");
            sb.AppendLine(" left outer join (");
            sb.AppendLine("         select CUST_CD ");
            sb.AppendLine("               ,ITEM_CD ");
            sb.AppendLine("               ,B_BOX_PRICE ");
            sb.AppendLine("               ,S_BOX_PRICE ");
            sb.AppendLine("               ,UNIT_PRICE ");
            sb.AppendLine("               ,INSTAFF ");
            sb.AppendLine("               ,INTIME ");
            sb.AppendLine("               ,UPSTAFF ");
            sb.AppendLine("               ,UPTIME ");
            sb.AppendLine("          from N_PRICE_INFO  ");
            sb.AppendLine("         where CUST_CD = @CUST_CD");
            sb.AppendLine("               )B ");
            sb.AppendLine(" on A.ITEM_CD = B.ITEM_CD  ");


            SqlCommand sCommand = new SqlCommand(sb.ToString());
            sCommand.Parameters.AddWithValue("@CUST_CD", custCd);

            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }

            return wAdo.SqlCommandSelect(sCommand);

        }

        public DataTable fnOrderList(string condition)
        {
            wAdo = new wnAdo();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select A.ORDER_DATE");
            sb.AppendLine("     ,A.ORDER_CD ");
            sb.AppendLine("     ,ISNULL(B.RAW_MAT_CNT,0) AS RAW_MAT_CNT ");
            sb.AppendLine("     ,A.CUST_CD ");
            sb.AppendLine("     ,(select CUST_NM from N_CUST_CODE where CUST_GUBUN = '2' and CUST_CD = A.CUST_CD) as CUST_NM  ");
            sb.AppendLine("     ,A.INPUT_REQ_DATE ");
            sb.AppendLine("     ,A.COMPLETE_YN ");
            sb.AppendLine("     ,A.STAFF_CD ");
            sb.AppendLine("     ,(select STAFF_NM from N_STAFF_CODE where STAFF_CD = A.STAFF_CD) as STAFF_NM ");
            sb.AppendLine("     ,COMMENT ");
            sb.AppendLine(" from F_ORDER A ");
            sb.AppendLine(" LEFT OUTER JOIN ( ");
            sb.AppendLine(" SELECT ORDER_DATE,ORDER_CD,COUNT(RAW_MAT_CD) AS RAW_MAT_CNT FROM F_ORDER_DETAIL ");
            sb.AppendLine(" GROUP BY ORDER_DATE,ORDER_CD) B ");
            sb.AppendLine(" ON A.ORDER_DATE = B.ORDER_DATE ");
            sb.AppendLine(" AND A.ORDER_CD = B.ORDER_CD  ");
            sb.AppendLine(condition);
            sb.AppendLine(" order by A.ORDER_DATE desc, A.ORDER_CD desc ");

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }

            return wAdo.SqlCommandSelect(sCommand);

        }

        public DataTable fn_Order_Detail_List(string condition)
        {
            wAdo = new wnAdo();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select A.ORDER_DATE");
            sb.AppendLine("     ,A.ORDER_CD ");
            sb.AppendLine("     ,A.SEQ ");
            sb.AppendLine("     ,A.RAW_MAT_CD ");
            sb.AppendLine("     ,B.RAW_MAT_NM  ");
            sb.AppendLine("     ,B.SPEC    ");
            sb.AppendLine("     ,A.UNIT_CD ");
            sb.AppendLine("     , (select UNIT_NM from N_UNIT_CODE where UNIT_CD = A.UNIT_CD) AS UNIT_NM  ");
            sb.AppendLine("     , A.TOTAL_AMT ");
            sb.AppendLine("     , A.PRICE ");
            sb.AppendLine("     , A.TOTAL_MONEY ");
            sb.AppendLine(" from F_ORDER_DETAIL A ");
            sb.AppendLine(" LEFT OUTER JOIN N_RAW_CODE B ");
            sb.AppendLine(" ON A.RAW_MAT_CD = B.RAW_MAT_CD ");
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



        public DataTable fnItmeOutDetailList(string condition)
        {
            wAdo = new wnAdo();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select ");
            sb.AppendLine(" A.OUT_DATE");

            sb.AppendLine("     ,A.OUT_CD");
            sb.AppendLine("      ,A.SEQ");
            sb.AppendLine("     , A.ITEM_CD ");
            sb.AppendLine("     , B.ITEM_NM ");
            sb.AppendLine("     , B.BAL_STOCK ");
            sb.AppendLine("     , A.OUT_AMT");
            sb.AppendLine("     , A.VALID_DATE");
            sb.AppendLine("     , A.PRICE ");

            //sb.AppendLine("     , ITEM_OUT_BAR ");
            sb.AppendLine(" from F_ITEM_OUT_DETAIL A ");
            sb.AppendLine(" left outer join N_ITEM_CODE B");
            sb.AppendLine(" ON A.ITEM_CD = B.ITEM_CD ");
            sb.AppendLine(condition);



            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);
        }
















        public DataTable fn_Raw_List(string condition)
        {
            wAdo = new wnAdo();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select RAW_MAT_CD");
            sb.AppendLine(" , RAW_MAT_NM");
            sb.AppendLine(" , SPEC");
            sb.AppendLine(" , RAW_MAT_GUBUN");
            sb.AppendLine(" , (select S_CODE_NM ");
            sb.AppendLine("    from [SM_FACTORY_COM].[dbo].[T_S_CODE] ");
            sb.AppendLine("    where L_CODE = '300' and S_CODE = A.RAW_MAT_GUBUN) AS RAW_MAT_GUBUN_NM ");
            sb.AppendLine(" , INPUT_UNIT ");
            sb.AppendLine(" , (select UNIT_NM from N_UNIT_CODE where UNIT_CD = A.INPUT_UNIT) AS INPUT_UNIT_NM  ");
            sb.AppendLine(" , OUTPUT_UNIT ");
            sb.AppendLine(" , (select UNIT_NM from N_UNIT_CODE where UNIT_CD = A.OUTPUT_UNIT) AS OUTPUT_UNIT_NM  ");
            sb.AppendLine(" , CVR_RATIO ");
            sb.AppendLine(" , INPUT_PRICE ");
            sb.AppendLine(" , ST_STATUS_YN ");
            sb.AppendLine(" , (select S_CODE_NM ");
            sb.AppendLine("    from T_S_CODE ");
            sb.AppendLine("    where L_CODE = '500' and S_CODE = A.ST_STATUS_YN) AS ST_STATUS_NM ");
            sb.AppendLine(" , STOCK_LOC ");
            sb.AppendLine(" , (select LOC_NM ");
            sb.AppendLine("    from N_LOC_CODE ");
            sb.AppendLine("    where LOC_CD = A.STOCK_LOC ) AS LOC_NM ");
            sb.AppendLine(" , BASE_WEIGHT ");
            sb.AppendLine(" , BASE_UNIT ");
            sb.AppendLine(" , USED_CD ");
            sb.AppendLine(" , CUST_CD ");
            sb.AppendLine(" , (select CUST_NM from N_CUST_CODE where CUST_CD = A.CUST_CD and CUST_GUBUN ='2') AS CUST_NM ");
            sb.AppendLine(" , CHECK_GUBUN ");
            sb.AppendLine(" , PROP_STOCK ");
            sb.AppendLine(" , BAL_STOCK ");
            sb.AppendLine(" , COMMENT");
            sb.AppendLine(" from N_RAW_CODE A ");
            sb.AppendLine(condition);
            sb.AppendLine(" order by RAW_MAT_CD ");

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }

            return wAdo.SqlCommandSelect(sCommand);
        }


        public DataTable grdItemcount()
        {

            wAdo = new wnAdo();
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("select ");
            sb.AppendLine("      ITEM_CD");
            sb.AppendLine("      , ITEM_NM");
            sb.AppendLine("      , BAL_STOCK");

            sb.AppendLine(" from    N_ITEM_CODE ");

            SqlCommand sCommand = new SqlCommand(sb.ToString());

            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);
        }

        public DataTable grdRawCount()
        {


            wAdo = new wnAdo();
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("select ");
            sb.AppendLine("      RAW_MAT_CD");
            sb.AppendLine("      , RAW_MAT_NM");
            sb.AppendLine("      , BASE_WEIGHT");
            sb.AppendLine("      , BAL_STOCK");

            sb.AppendLine(" from    N_RAW_CODE ");

            SqlCommand sCommand = new SqlCommand(sb.ToString());

            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);
        }




        public DataTable grdRawInputList()
        {



            wAdo = new wnAdo();
            StringBuilder sb = new StringBuilder();



            sb.AppendLine("select ");
            sb.AppendLine("      B.INPUT_DATE");
            sb.AppendLine("      , A.RAW_MAT_CD");
            sb.AppendLine("      , A.RAW_MAT_NM");
            sb.AppendLine("      , B.TOTAL_AMT");
            sb.AppendLine("      , C.CUST_CD");
            sb.AppendLine("      , B.INPUT_AMT");
            sb.AppendLine("      , (select CUST_NM from N_CUST_CODE where CUST_CD = C.CUST_CD) AS CUST_NM");
            sb.AppendLine(" FROM N_RAW_CODE A ");
            sb.AppendLine(" inner join F_RAW_DETAIL B ");
            sb.AppendLine(" on A.RAW_MAT_CD = B.RAW_MAT_CD");
            sb.AppendLine(" inner join  F_RAW_INPUT C ");
            sb.AppendLine(" on B.INPUT_DATE =  C.INPUT_DATE AND B.INPUT_CD =  C.INPUT_CD ");



            SqlCommand sCommand = new SqlCommand(sb.ToString());

            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);
        }







        public DataTable grdItemInputList()
        {

            wAdo = new wnAdo();
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("select ");
            sb.AppendLine("      B.INPUT_DATE");
            sb.AppendLine("       ,A.ITEM_NM");
            sb.AppendLine("      ,A.ITEM_CD");
            sb.AppendLine("      ,B.INPUT_AMT");
            sb.AppendLine(" FROM N_ITEM_CODE A ");
            sb.AppendLine(" inner join F_ITEM_INPUT B ");
            sb.AppendLine(" on A.ITEM_CD = B.ITEM_CD");



            SqlCommand sCommand = new SqlCommand(sb.ToString());

            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);
        }






        public int insertOrder(
         string order_date
       , string txt_cust_cd
       , string in_req_date
       //, string pur_yn
       , string comment
       , DataGridView o_rm_dgv)
        {
            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                sb = new StringBuilder();
                sb.AppendLine("declare @seq int ");
                sb.AppendLine("select @seq =ISNULL(MAX(ORDER_CD),0)+1 from F_ORDER ");
                sb.AppendLine("where ORDER_DATE = '" + order_date + "' ");

                sb.AppendLine("insert into F_ORDER(");
                sb.AppendLine("     ORDER_DATE");
                sb.AppendLine("     ,ORDER_CD ");
                sb.AppendLine("     ,CUST_CD ");
                sb.AppendLine("     ,INPUT_REQ_DATE ");
                sb.AppendLine("     ,COMPLETE_YN ");
                //sb.AppendLine("     ,STAFF_CD ");
                sb.AppendLine("     ,COMMENT ");
                sb.AppendLine("     ,INSTAFF ");
                sb.AppendLine("     ,INTIME ");
                sb.AppendLine(" ) values ( ");
                sb.AppendLine("      @ORDER_DATE ");
                sb.AppendLine("     ,@seq");
                sb.AppendLine("     ,@CUST_CD ");
                sb.AppendLine("     ,@INPUT_REQ_DATE ");
                sb.AppendLine("     ,'N' ");
                sb.AppendLine("     ,'"+Common.p_strStaffNo+"' ");
                sb.AppendLine("     ,@COMMENT ");
                sb.AppendLine("     ,convert(varchar, getdate(), 120) ");
                sb.AppendLine(" ) ");

                if (o_rm_dgv.Rows.Count > 0)
                {
                    for (int i = 0; i < o_rm_dgv.Rows.Count; i++)
                    {
                        sb.AppendLine("declare @order_seq" + i + " int ");
                        sb.AppendLine("select @order_seq" + i + " =ISNULL(MAX(SEQ),0)+1 from F_ORDER_DETAIL ");
                        sb.AppendLine("where ORDER_DATE = '" + order_date + "' ");
                        sb.AppendLine("and ORDER_CD =  @seq ");

                        sb.AppendLine("insert into F_ORDER_DETAIL(");
                        sb.AppendLine("     ORDER_DATE ");
                        sb.AppendLine("     ,ORDER_CD ");
                        sb.AppendLine("     ,SEQ ");
                        sb.AppendLine("     ,RAW_MAT_CD ");
                        sb.AppendLine("     ,SPEC ");
                        sb.AppendLine("     ,UNIT_CD ");
                        sb.AppendLine("     ,TOTAL_AMT ");
                        sb.AppendLine("     ,PRICE ");
                        sb.AppendLine("     ,TOTAL_MONEY ");
                        //sb.AppendLine("     ,INSTAFF ");
                        sb.AppendLine("     ,INTIME ");
                        sb.AppendLine("  )values ( ");
                        sb.AppendLine("     '" + order_date + "' ");
                        sb.AppendLine("      ,@seq ");
                        sb.AppendLine("     ,@order_seq" + i + " ");
                        sb.AppendLine("     ,'" + o_rm_dgv.Rows[i].Cells["RAW_MAT_CD"].Value + "' ");
                        sb.AppendLine("     ,'" + o_rm_dgv.Rows[i].Cells["SPEC"].Value + "' ");
                        sb.AppendLine("     ,'" + o_rm_dgv.Rows[i].Cells["UNIT_CD"].Value + "' ");
                        sb.AppendLine("     ," + (o_rm_dgv.Rows[i].Cells["TOTAL_AMT"].Value.ToString()).Replace(",", "") + " ");
                        sb.AppendLine("     ," + (o_rm_dgv.Rows[i].Cells["PRICE"].Value.ToString()).Replace(",", "") + " ");
                        sb.AppendLine("     ," + (o_rm_dgv.Rows[i].Cells["TOTAL_MONEY"].Value.ToString()).Replace(",", "") + " ");
                        //sb.AppendLine("     ,'" + Common.p_strStaffNo + "' ");
                        sb.AppendLine("     ,convert(varchar, getdate(), 120)  ");
                        sb.AppendLine("  )");
                    }
                }
                //Common.p_strStaffNo 

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@ORDER_DATE", order_date);
                sCommand.Parameters.AddWithValue("@CUST_CD", txt_cust_cd);
                sCommand.Parameters.AddWithValue("@INPUT_REQ_DATE", in_req_date);
                sCommand.Parameters.AddWithValue("@COMMENT", comment);

                int qResult = wAdo.SqlCommandEtc(sCommand, "insert_ORDER_TB");
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

        //2019-12-23 씨지엠 회계를 위한 수정
        public int insertSoo(
                 string txt_soo_date
                         , string custCd
                         , string sooMoney
                         , string dcMoney
                         , string comm
                         , string totalMoney
                         , string gubun
            )
        {
            try
            {
                wAdo = new wnAdo();
                wnDm wDm = new wnDm();

                sb = new StringBuilder();
                sb.AppendLine("declare @seq int ");
                sb.AppendLine("select @seq =ISNULL(MAX(SOO_CD),0)+1 from F_COLLECT ");
                sb.AppendLine("where SOO_DATE = '" + txt_soo_date + "' ");

                sb.AppendLine("insert into F_COLLECT(");
                sb.AppendLine("     SOO_DATE ");
                sb.AppendLine("     ,SOO_CD ");
                sb.AppendLine("     ,SOO_GUBUN ");
                sb.AppendLine("     ,CUST_CD ");
                sb.AppendLine("     ,SOO_MONEY ");
                sb.AppendLine("     ,TOTAL_MONEY ");
                sb.AppendLine("     ,DC_MONEY ");
                sb.AppendLine("     ,COMMENT ");
                sb.AppendLine("     ,INSTAFF ");
                sb.AppendLine("     ,INTIME )");
                sb.AppendLine("  values ( ");
                sb.AppendLine("     '" + txt_soo_date + "' ");
                sb.AppendLine("     ,@seq ");
                sb.AppendLine("     ,'" + gubun + "' ");
                sb.AppendLine("     ,'" + custCd + "' ");
                sb.AppendLine("     ," + sooMoney.Replace(",", "") + " ");
                sb.AppendLine("     ," + totalMoney.Replace(",","") + " ");
                sb.AppendLine("     ," + dcMoney.Replace(",", "") + " ");
                sb.AppendLine("     ,'" + comm + "' ");
                sb.AppendLine("     ,'"+Common.p_strStaffNo+"' ");
                sb.AppendLine("     ,convert(varchar, getdate(), 120)  ");
                sb.AppendLine(" ) ");

                sb.AppendLine("update N_CUST_CODE set");
                sb.AppendLine("     BALANCE = BALANCE - " + totalMoney.Replace(",","") + " ");
                sb.AppendLine("     where CUST_CD = '" + custCd + "' ");


                bool isCustDay = wDm.isCustDayTotal(txt_soo_date, custCd);

                if (!isCustDay)
                {
                    sb.AppendLine(wDm.Create_New_CustDayTotal(txt_soo_date, custCd));
                }

                sb.AppendLine(" UPDATE T_CUST_DAY_TOTAL SET ");
                sb.AppendLine(" COL_MONEY = COL_MONEY + "+sooMoney.Replace(",","") + " ");
                sb.AppendLine(" ,COL_DC_MONEY = COL_DC_MONEY + " + dcMoney.Replace(",", "") + " ");
                sb.AppendLine(" ,COL_TOTAL_MONEY = COL_TOTAL_MONEY + "+ totalMoney.Replace(",", "") + " ");
                sb.AppendLine(" WHERE INPUT_DATE ='" + txt_soo_date + "'  AND CUST_CD = '" + custCd + "'  ");

                sb.AppendLine(wDm.CustDayTotal_Change_Balance_Today(txt_soo_date,custCd));

                sb.AppendLine(wDm.CustDayTotal_Change_Balance(txt_soo_date,custCd,totalMoney.Replace(",",""),"-"));


                //sb.AppendLine("update F_SALES set");
                //sb.AppendLine("     COMPLETE_YN = 'Y' ");
                //sb.AppendLine("     where SALES_DATE = '" + grdSalesDetail.Rows[0].Cells["SALES_DATE"].Value + "' ");
                //sb.AppendLine("     and SALES_CD = '" + grdSalesDetail.Rows[0].Cells["SALES_CD"].Value + "' ");
                

                sCommand = new SqlCommand(sb.ToString());
                int qResult = wAdo.SqlCommandEtc(sCommand, "insert_COLLECT_TB");
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


        public int insertGive(
                 string txt_give_date
                         , string custCd
                         , string giveMoney
                         , string dcMoney
                         , string comm
                         , string totalMoney
                         , string gubun
            )
        {
            try
            {
                wAdo = new wnAdo();
                wnDm wDm = new wnDm();

                sb = new StringBuilder();
                sb.AppendLine("declare @seq int ");
                sb.AppendLine("select @seq =ISNULL(MAX(GIVE_CD),0)+1 from F_GIVE ");
                sb.AppendLine("where GIVE_DATE = '" + txt_give_date + "' ");

                sb.AppendLine("insert into F_GIVE(");
                sb.AppendLine("     GIVE_DATE ");
                sb.AppendLine("     ,GIVE_CD ");
                sb.AppendLine("     ,GIVE_GUBUN ");
                sb.AppendLine("     ,CUST_CD ");
                sb.AppendLine("     ,GIVE_MONEY ");
                sb.AppendLine("     ,TOTAL_MONEY ");
                sb.AppendLine("     ,DC_MONEY ");
                sb.AppendLine("     ,COMMENT ");
                sb.AppendLine("     ,INSTAFF ");
                sb.AppendLine("     ,INTIME )");
                sb.AppendLine("  values ( ");
                sb.AppendLine("     '" + txt_give_date + "' ");
                sb.AppendLine("     ,@seq ");
                sb.AppendLine("     ,'" + gubun + "' ");
                sb.AppendLine("     ,'" + custCd + "' ");
                sb.AppendLine("     ," + giveMoney.Replace(",", "") + " ");
                sb.AppendLine("     ," + totalMoney.Replace(",", "") + " ");
                sb.AppendLine("     ," + dcMoney.Replace(",", "") + " ");
                sb.AppendLine("     ,'" + comm + "' ");
                sb.AppendLine("     ,'" + Common.p_strStaffNo + "' ");
                sb.AppendLine("     ,convert(varchar, getdate(), 120)  ");
                sb.AppendLine(" ) ");

                sb.AppendLine("update N_CUST_CODE set");
                sb.AppendLine("     BALANCE = BALANCE + " + totalMoney.Replace(",", "") + " ");
                sb.AppendLine("     where CUST_CD = '" + custCd + "' ");


                bool isCustDay = wDm.isCustDayTotal(txt_give_date, custCd);

                if (!isCustDay)
                {
                    sb.AppendLine(wDm.Create_New_CustDayTotal(txt_give_date, custCd));
                }

                sb.AppendLine(" UPDATE T_CUST_DAY_TOTAL SET ");
                sb.AppendLine(" PAY_MONEY = PAY_MONEY + " + giveMoney.Replace(",", "") + " ");
                sb.AppendLine(" ,PAY_DC_MONEY = PAY_DC_MONEY + " + dcMoney.Replace(",", "") + " ");
                sb.AppendLine(" ,PAY_TOTAL_MONEY = PAY_TOTAL_MONEY + " + totalMoney.Replace(",", "") + " ");
                sb.AppendLine(" WHERE INPUT_DATE ='" + txt_give_date + "'  AND CUST_CD = '" + custCd + "'  ");

                sb.AppendLine(wDm.CustDayTotal_Change_Balance_Today(txt_give_date, custCd));

                sb.AppendLine(wDm.CustDayTotal_Change_Balance(txt_give_date, custCd, totalMoney.Replace(",", ""), "+"));


                //sb.AppendLine("update F_SALES set");
                //sb.AppendLine("     COMPLETE_YN = 'Y' ");
                //sb.AppendLine("     where SALES_DATE = '" + grdSalesDetail.Rows[0].Cells["SALES_DATE"].Value + "' ");
                //sb.AppendLine("     and SALES_CD = '" + grdSalesDetail.Rows[0].Cells["SALES_CD"].Value + "' ");


                sCommand = new SqlCommand(sb.ToString());
                int qResult = wAdo.SqlCommandEtc(sCommand, "insert_COLLECT_TB");
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




        public int insertExchange(
             string seq
            , string realTime
            , string baseUsd)
        {
            try
            {
                wAdo = new wnAdo();


                sb = new StringBuilder();
                sb.AppendLine("insert into N_USD_INFO");
                sb.AppendLine("  values ( ");
                sb.AppendLine("     @SEQ ");
                sb.AppendLine("     ,@REALTIME ");
                sb.AppendLine("     ,@BASEUSD ");
                sb.AppendLine(" ) ");
                sCommand = new SqlCommand(sb.ToString());
                sCommand.Parameters.AddWithValue("@SEQ", seq);
                sCommand.Parameters.AddWithValue("@REALTIME", realTime);
                sCommand.Parameters.AddWithValue("@BASEUSD", baseUsd);


                int qResult = wAdo.SqlCommandEtc(sCommand, "insert_CUST_TB");
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


        public int insertCountry(
             string code
           , string name
           , string token
           , string comment
           , string dbName)
        {
            try
            {
                wAdo = new wnAdo();


                sb = new StringBuilder();
                sb.AppendLine("insert into " + dbName);
                sb.AppendLine("  values ( ");
                sb.AppendLine("     @CODE ");
                sb.AppendLine("     ,@NAME ");
                sb.AppendLine("     ,@TOKEN ");
                sb.AppendLine("     ,@COMMENT ");
                sb.AppendLine(" ) ");
                sCommand = new SqlCommand(sb.ToString());
                sCommand.Parameters.AddWithValue("@CODE", code);
                sCommand.Parameters.AddWithValue("@NAME", name);
                sCommand.Parameters.AddWithValue("@TOKEN", token);
                sCommand.Parameters.AddWithValue("@COMMENT", comment);

                int qResult = wAdo.SqlCommandEtc(sCommand, "insert_CUST_TB");
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







        public int insertLoc(
             string code
           , string name
           , string stroage
           , string comment
           , string dbName)
        {
            try
            {
                wAdo = new wnAdo();


                sb = new StringBuilder();
                sb.AppendLine("insert into " + dbName);
                sb.AppendLine("  values ( ");
                sb.AppendLine("     @CODE ");
                sb.AppendLine("     ,@STORAGE ");
                sb.AppendLine("     ,@NAME ");
                sb.AppendLine("     ,@COMMENT ");
                sb.AppendLine(" ) ");

                sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@CODE", code);
                sCommand.Parameters.AddWithValue("@NAME", name);
                sCommand.Parameters.AddWithValue("@STORAGE", stroage);
                sCommand.Parameters.AddWithValue("@COMMENT", comment);



                int qResult = wAdo.SqlCommandEtc(sCommand, "insert_CUST_TB");
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

        public int insertCategory(
                string gubun
               , string code
               , string name
               , string comment
               , string dbName)
        {
            try
            {
                wAdo = new wnAdo();





                sb = new StringBuilder();
                sb.AppendLine("insert into " + dbName);
                sb.AppendLine("  values ( ");
                sb.AppendLine("     @GUBUN ");
                sb.AppendLine("     ,@CODE ");
                sb.AppendLine("     ,@NAME ");
                sb.AppendLine("     ,@COMMENT ");
                sb.AppendLine(" ) ");

                sCommand = new SqlCommand(sb.ToString());
                sCommand.Parameters.AddWithValue("@GUBUN", gubun);
                sCommand.Parameters.AddWithValue("@CODE", code);
                sCommand.Parameters.AddWithValue("@NAME", name);
                sCommand.Parameters.AddWithValue("@COMMENT", comment);
                //if(comment != null)
                //    sCommand.Parameters.AddWithValue("@COMMENT", comment);
                //else
                //    sCommand.Parameters.AddWithValue("@COMMENT", "");


                int qResult = wAdo.SqlCommandEtc(sCommand, "insert_CUST_TB");
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




        public int insertFlow(
            string code
            , string name
            , string fac
            , string insertYn
            , string ckYn
            , string storage
            , string staff
            , string comm
            , string dbName)
        {

            try
            {
                wAdo = new wnAdo();



                sb = new StringBuilder();
                sb.AppendLine("insert into " + dbName);
                sb.AppendLine("     (FLOW_CD ");
                sb.AppendLine("     , FLOW_NM ");
                sb.AppendLine("     , STORAGE_CD ");
                sb.AppendLine("     , FLOW_INSERT_YN ");
                sb.AppendLine("     , FLOW_CHK_YN ");
                sb.AppendLine("     , POOR_TYPE_CD ");
                sb.AppendLine("     , STAFF_CD ");
                sb.AppendLine("     , COMMENT) ");
                sb.AppendLine("  values ( ");
                sb.AppendLine("     @CODE ");
                sb.AppendLine("     , @NAME ");
                sb.AppendLine("     , @STORAGE ");
                sb.AppendLine("     , @INSERTYN ");
                sb.AppendLine("     , @CKYN ");
                sb.AppendLine("     , @FAC ");
                sb.AppendLine("     , @STAFF");
                sb.AppendLine("     , @COMMENT ");
                sb.AppendLine(" ) ");

                sCommand = new SqlCommand(sb.ToString());
                sCommand.Parameters.AddWithValue("@CODE", code);
                sCommand.Parameters.AddWithValue("@NAME", name);
                sCommand.Parameters.AddWithValue("@STORAGE", storage);
                sCommand.Parameters.AddWithValue("@INSERTYN", insertYn);
                sCommand.Parameters.AddWithValue("@CKYN", ckYn);
                sCommand.Parameters.AddWithValue("@FAC", fac);
                sCommand.Parameters.AddWithValue("@STAFF", staff);
                sCommand.Parameters.AddWithValue("@COMMENT", comm);






                int qResult = wAdo.SqlCommandEtc(sCommand, "insert_CUST_TB");
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






        // 기초코드 삽입 쿼리
        public int insertCode(
                  string code
                , string name
                , string comment
                , string dbName)
        {
            try
            {
                wAdo = new wnAdo();





                sb = new StringBuilder();
                sb.AppendLine("insert into " + dbName);
                sb.AppendLine("  values ( ");
                sb.AppendLine("     @CODE ");
                sb.AppendLine("     ,@NAME ");
                sb.AppendLine("     ,@COMMENT ");
                sb.AppendLine(" ) ");

                sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@CODE", code);
                sCommand.Parameters.AddWithValue("@NAME", name);
                sCommand.Parameters.AddWithValue("@COMMENT", comment);
                //if(comment != null)
                //    sCommand.Parameters.AddWithValue("@COMMENT", comment);
                //else
                //    sCommand.Parameters.AddWithValue("@COMMENT", "");


                int qResult = wAdo.SqlCommandEtc(sCommand, "insert_CUST_TB");
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

        //유형코드 등록
        public int insertType(
              string code
            , string name
            , string yn
            , string comment
            , string dbName)
        {
            try
            {
                wAdo = new wnAdo();



                sb = new StringBuilder();
                sb.AppendLine("insert into " + dbName);
                sb.AppendLine("  values ( ");
                sb.AppendLine("     @CODE ");
                sb.AppendLine("     ,@NAME ");
                sb.AppendLine("     ,@yn ");
                sb.AppendLine("     ,@COMMENT ");
                sb.AppendLine(" ) ");

                sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@CODE", code);
                sCommand.Parameters.AddWithValue("@NAME", name);
                sCommand.Parameters.AddWithValue("@yn", yn);
                sCommand.Parameters.AddWithValue("@COMMENT", comment);



                int qResult = wAdo.SqlCommandEtc(sCommand, "insert_CUST_TB");
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
        public int insertPoor(
             string code
           , string name
           , string tyCd
           , string comment
           , string dbName)
        {
            try
            {
                wAdo = new wnAdo();






                sb = new StringBuilder();
                sb.AppendLine("insert into " + dbName);
                sb.AppendLine("  values ( ");
                sb.AppendLine("     @CODE ");
                sb.AppendLine("     ,@NAME ");
                sb.AppendLine("     ,@tyCd ");
                sb.AppendLine("     ,@COMMENT ");
                sb.AppendLine(" ) ");
                MessageBox.Show(code);
                sCommand = new SqlCommand(sb.ToString());
                sCommand.Parameters.AddWithValue("@CODE", code);
                sCommand.Parameters.AddWithValue("@NAME", name);
                sCommand.Parameters.AddWithValue("@tyCd", tyCd);
                sCommand.Parameters.AddWithValue("@COMMENT", comment);



                int qResult = wAdo.SqlCommandEtc(sCommand, "insert_CUST_TB");
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

        public int insertBank(
        string code
      , string name
      , string gubun
      , string dFee
      , string iFee
      , string used
      , string comment
      , string account_num
      , string account_holder
      , string dbName)
        {
            try
            {
                wAdo = new wnAdo();



                sb = new StringBuilder();
                sb.AppendLine("insert into " + dbName);
                sb.AppendLine("  values ( ");
                sb.AppendLine("     @CODE ");
                sb.AppendLine("     ,@NAME ");
                sb.AppendLine("     ,@COMMENT ");
                sb.AppendLine("     ,@GUBUN ");
                sb.AppendLine("     ,@DFEE ");
                sb.AppendLine("     ,@IFEE ");
                sb.AppendLine("     ,@USED)");

                sb.AppendLine(" ) ");

                sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@CODE", code);
                sCommand.Parameters.AddWithValue("@NAME", name);
                sCommand.Parameters.AddWithValue("@COMMENT", comment);
                sCommand.Parameters.AddWithValue("@GUBUN", gubun);
                sCommand.Parameters.AddWithValue("@DFEE", dFee);
                sCommand.Parameters.AddWithValue("@IFEE", iFee);
                sCommand.Parameters.AddWithValue("@USED", used);




                int qResult = wAdo.SqlCommandEtc(sCommand, "insert_CUST_TB");
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
        public int updateCountry(
             string code
           , string name
           , string token
           , string comment
           , string dbName)
        {
            try
            {
                wAdo = new wnAdo();
                sb = new StringBuilder();
                sb.AppendLine("update " + dbName + " set ");
                sb.AppendLine("     COUNTRY_NM = @NAME ");
                sb.AppendLine("     ,COMMENT = @COMMENT ");
                sb.AppendLine("     ,TOKEN_NM = @TOKEN ");

                sb.AppendLine(" where ");
                sb.AppendLine("     COUNTRY_CD = @CODE ");


                sCommand = new SqlCommand(sb.ToString());
                sCommand.Parameters.AddWithValue("@NAME", name);
                sCommand.Parameters.AddWithValue("@COMMENT", comment);
                sCommand.Parameters.AddWithValue("@TOKEN", token);
                sCommand.Parameters.AddWithValue("@CODE", code);


                int qResult = wAdo.SqlCommandEtc(sCommand, "insert_CUST_TB");
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



        public int insertItemOut(
                   string outdate
                        , string seq
                        , string recipient
                        , string recipientPhone
                        , string comment
                        , string custcd
                        , string addr
                        , string corPhone
                        , string outReq
                        , string outReqDate
                        , string outSignCmt
                        , string qualCd
                        , string qualDate
                        , string qualSignCmt
                        , string stMg
                        , string stMgDate
                        , string stMgSignCmt
                        , string deliveryCd
                        , string deliveryInvoice
                        , string deliveryCmt
                        , 스마트팩토리.Controls.conDataGridView dgv
            )
        {
            try
            {
                wAdo = new wnAdo();

                sb = new StringBuilder();
                sb.AppendLine("declare @SEQ int ");
                sb.AppendLine("select @SEQ =ISNULL(MAX(OUT_CD),0)+1 from F_ITEM_OUT ");
                sb.AppendLine("where OUT_DATE = '" + outdate + "' ");

                sb.AppendLine("insert into F_ITEM_OUT( ");
                sb.AppendLine("     OUT_DATE ");
                sb.AppendLine("    , OUT_CD ");
                sb.AppendLine("    , CUST_CD ");

                // sb.AppendLine("    , ITEM_CD ");
                sb.AppendLine("    , RECIPIENT ");
                sb.AppendLine("    , RECIPIENT_PHONE ");
                sb.AppendLine("    , REQ_COMMENT ");
                sb.AppendLine("    , COMP_PHONE_CD ");
                sb.AppendLine("    , CUST_ADDR_CD ");
                sb.AppendLine("    , OUT_REQ_CD ");
                sb.AppendLine("    , OUT_REQ_DATE ");
                sb.AppendLine("    , OUT_SIGN_CMT ");
                sb.AppendLine("    , QUAL_CD ");
                sb.AppendLine("    , QUAL_DATE ");
                // sb.AppendLine("    , QUAL_SIGN_YN ");
                sb.AppendLine("    , QUAL_SIGN_CMT ");
                sb.AppendLine("    , ST_MG_CD ");
                sb.AppendLine("    , ST_MG_DATE ");
                // sb.AppendLine("    , ST_MG_SIGN_YN ");
                sb.AppendLine("    , ST_MG_SIGN_CMT ");
                sb.AppendLine("    , DELIVERY_CD ");
                sb.AppendLine("    , DELIVERY_INVOICE ");
                sb.AppendLine("    , DELIVERY_CMT ");
                sb.AppendLine("    , INSTAFF ");
                sb.AppendLine("    , INTIME) ");


                sb.AppendLine("  values ( ");
                sb.AppendLine("     @OUT_DATE ");
                sb.AppendLine("     ,@SEQ ");
                sb.AppendLine("     ,@CUST_CD ");
                // sb.AppendLine("     ,@ITEM_CD ");
                sb.AppendLine("     ,@RECIPIENT ");
                sb.AppendLine("     ,@RECIPIENT_PHONE ");
                sb.AppendLine("     ,@REQ_COMMENT ");
                sb.AppendLine("     ,@COMP_PHONE_CD ");
                sb.AppendLine("     ,@CUST_ADDR_CD ");
                sb.AppendLine("     ,@OUT_REQ_CD ");
                sb.AppendLine("     ,@OUT_REQ_DATE ");
                sb.AppendLine("     ,@OUT_SIGN_CMT ");
                sb.AppendLine("     ,@QUAL_CD ");
                sb.AppendLine("     ,@QUAL_DATE ");
                //  sb.AppendLine("     ,@QUAL_SIGN_YN ");
                sb.AppendLine("     ,@QUAL_SIGN_CMT ");
                sb.AppendLine("     ,@ST_MG_CD ");
                sb.AppendLine("     ,@ST_MG_DATE ");
                // sb.AppendLine("     ,@ST_MG_SIGN_YN ");
                sb.AppendLine("     ,@ST_MG_SIGN_CMT ");
                sb.AppendLine("     ,@DELIVERY_CD ");
                sb.AppendLine("     ,@DELIVERY_INVOICE ");
                sb.AppendLine("     ,@DELIVERY_CMT ");
                sb.AppendLine("     ,@INSTAFF ");
                sb.AppendLine("     ,convert(varchar, getdate(), 120)  ");
                sb.AppendLine(" ) ");



                //if(comment != null)
                //    sCommand.Parameters.AddWithValue("@COMMENT", comment);
                //else
                //    sCommand.Parameters.AddWithValue("@COMMENT", "");

                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    sb.AppendLine("declare @SEQ" + i + " int ");
                    sb.AppendLine("select @SEQ" + i + " =ISNULL(MAX(SEQ),0)+1 from F_ITEM_OUT_DETAIL ");
                    sb.AppendLine("where OUT_DATE = '" + outdate + "' ");
                    sb.AppendLine("AND OUT_CD = @SEQ");

                    sb.AppendLine("insert into F_ITEM_OUT_DETAIL( ");
                    sb.AppendLine("     OUT_DATE ");
                    sb.AppendLine("    , OUT_CD ");
                    sb.AppendLine("    , SEQ ");
                    sb.AppendLine("    , ITEM_CD ");
                    sb.AppendLine("    , OUT_AMT ");
                    sb.AppendLine("    , VALID_DATE ");
                    sb.AppendLine("    , PRICE ");
                    // sb.AppendLine("    , ITEM_OUT_BAR ");
                    sb.AppendLine("    , INSTAFF ");
                    sb.AppendLine("    , INTIME) ");
                    sb.AppendLine("  values ( ");
                    sb.AppendLine("     '" + outdate + "' ");
                    sb.AppendLine("     ,@SEQ ");
                    sb.AppendLine("     ,@SEQ" + i);
                    sb.AppendLine("     ,'" + dgv.Rows[i].Cells["ITEM_CD"].Value.ToString() + "' ");
                    sb.AppendLine("     ,'" + double.Parse(dgv.Rows[i].Cells["TOTAL_AMT"].Value.ToString()) + "' ");
                    //유효기간 수정필요
                    // sb.AppendLine("     ,'" + dgv.Rows[i].Cells["VALID_DATE"].Value.ToString() + "' ");
                    sb.AppendLine("     ,'' ");
                    sb.AppendLine("     ,'" + double.Parse(dgv.Rows[i].Cells["PRICE"].Value.ToString()) + "' ");
                    // sb.AppendLine("     ,'" + dgv.Rows[i].Cells["ITEM_OUT_BAR"].Value.ToString() + "' ");
                    sb.AppendLine("     ,@INSTAFF ");
                    sb.AppendLine("     ,convert(varchar, getdate(), 120) ");
                    sb.AppendLine(" ) ");


                }


                sCommand = new SqlCommand(sb.ToString());
                sCommand.Parameters.AddWithValue("@OUT_DATE", outdate);
                //sCommand.Parameters.AddWithValue("@OUT_CD", seq);
                //sCommand.Parameters.AddWithValue("@ITEM_CD", recipient);
                sCommand.Parameters.AddWithValue("@RECIPIENT", recipient);
                sCommand.Parameters.AddWithValue("@RECIPIENT_PHONE", recipientPhone);
                sCommand.Parameters.AddWithValue("@CUST_CD", custcd);
                sCommand.Parameters.AddWithValue("@REQ_COMMENT", comment);
                sCommand.Parameters.AddWithValue("@COMP_PHONE_CD", corPhone);
                sCommand.Parameters.AddWithValue("@CUST_ADDR_CD", addr);
                sCommand.Parameters.AddWithValue("@OUT_REQ_CD", outReq);
                sCommand.Parameters.AddWithValue("@OUT_REQ_DATE", outReqDate);
                //sCommand.Parameters.AddWithValue("@OUT_SIGN_YN", ck);
                sCommand.Parameters.AddWithValue("@OUT_SIGN_CMT", outSignCmt);
                sCommand.Parameters.AddWithValue("@QUAL_CD", qualCd);
                sCommand.Parameters.AddWithValue("@QUAL_DATE", qualDate);
                sCommand.Parameters.AddWithValue("@QUAL_SIGN_CMT", qualSignCmt);
                sCommand.Parameters.AddWithValue("@ST_MG_CD", stMg);
                sCommand.Parameters.AddWithValue("@ST_MG_DATE", stMgDate);
                sCommand.Parameters.AddWithValue("@ST_MG_SIGN_CMT", stMgSignCmt);
                sCommand.Parameters.AddWithValue("@DELIVERY_CD", deliveryCd);
                sCommand.Parameters.AddWithValue("@DELIVERY_INVOICE", deliveryInvoice);
                sCommand.Parameters.AddWithValue("@DELIVERY_CMT", deliveryCmt);
                sCommand.Parameters.AddWithValue("@INSTAFF", "");

                int qResult = wAdo.SqlCommandEtc(sCommand, "insert_CUST_TB");
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




        public int updateItemOut(
           string outdate
                , string seq
                , string recipient
                , string recipientPhone
                , string comment
                , string custcd
                , string addr
                , string corPhone
                , string outReq
                , string outReqDate
                , string outSignCmt
                , string qualCd
                , string qualDate
                , string qualSignCmt
                , string stMg
                , string stMgDate
                , string stMgSignCmt
                , string deliveryCd
                , string deliveryInvoice
                , string deliveryCmt
                , 스마트팩토리.Controls.conDataGridView dgv
                , string ckQual
                , string ckSt
                , DataGridView del_dgv

    )
        {
            try
            {
                wAdo = new wnAdo();

                sb = new StringBuilder();


                sb.AppendLine("update F_ITEM_OUT set ");
                // sb.AppendLine("    , ITEM_CD ");
                sb.AppendLine("    , RECIPIENT = @RECIPIENT");
                sb.AppendLine("    , RECIPIENT_PHONE = @RECIPIENT_PHONE");
                sb.AppendLine("    , REQ_COMMENT = @REQ_COMMENT");
                sb.AppendLine("    , COMP_PHONE_CD = @COMP_PHONE_CD");
                sb.AppendLine("    , CUST_ADDR_CD = @CUST_ADDR_CD ");
                sb.AppendLine("    , OUT_REQ_CD = @OUT_REQ_CD  ");
                sb.AppendLine("    , OUT_REQ_DATE = @OUT_REQ_DATE ");
                //sb.AppendLine("    , OUT_SIGN_YN ");

                if (ckSt != "")
                {
                    sb.AppendLine("    , ST_MG_SIGN_YN ");
                }
                else
                {
                    sb.AppendLine("    , QUAL_SIGN_YN ");
                }
                sb.AppendLine("    , OUT_SIGN_CMT = @OUT_SIGN_CMT ");
                sb.AppendLine("    , QUAL_CD = @QUAL_CD ");
                sb.AppendLine("    , QUAL_DATE = @QUAL_DATE ");

                sb.AppendLine("    , QUAL_SIGN_CMT = @QUAL_SIGN_CMT ");
                sb.AppendLine("    , ST_MG_CD = @ST_MG_CD ");
                sb.AppendLine("    , ST_MG_DATE = @ST_MG_DATE ");

                sb.AppendLine("    , ST_MG_SIGN_CMT = @ST_MG_SIGN_CMT ");
                sb.AppendLine("    , DELIVERY_CD = @DELIVERY_CD ");
                sb.AppendLine("    , DELIVERY_INVOICE = @DELIVERY_INVOIC ");
                sb.AppendLine("    , DELIVERY_CMT = @DELIVERY_CMT ");
                sb.AppendLine("    , INSTAFF = @INSTAFF ");
                sb.AppendLine("    , INTIME = convert(varchar, getdate(), 120)) ");



                //if(comment != null)
                //    sCommand.Parameters.AddWithValue("@COMMENT", comment);
                //else
                //    sCommand.Parameters.AddWithValue("@COMMENT", "");

                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    if (dgv.Rows[i].Cells["SEQ"].Value != null)
                    {
                        sb.AppendLine("declare @SEQ" + i + " int ");
                        sb.AppendLine("select @SEQ" + i + " =ISNULL(MAX(SEQ),0)+1 from F_ITEM_OUT_DETAIL ");
                        sb.AppendLine("where OUT_DATE = '" + outdate + "'");
                        sb.AppendLine("AND OUT_CD = @SEQ");

                        sb.AppendLine("insert into F_ITEM_OUT_DETAIL( ");
                        sb.AppendLine("     OUT_DATE ");
                        sb.AppendLine("    , OUT_CD ");
                        sb.AppendLine("    , SEQ ");
                        sb.AppendLine("    , ITEM_CD ");
                        sb.AppendLine("    , OUT_AMT ");
                        sb.AppendLine("    , VALID_DATE ");
                        sb.AppendLine("    , PRICE ");
                        // sb.AppendLine("    , ITEM_OUT_BAR ");
                        sb.AppendLine("    , INSTAFF ");
                        sb.AppendLine("    , INTIME) ");
                        sb.AppendLine("  values ( ");
                        sb.AppendLine("     '" + outdate + "' ");
                        sb.AppendLine("     ,@SEQ ");
                        sb.AppendLine("     ,@SEQ" + i + " ");
                        sb.AppendLine("     ,'" + dgv.Rows[i].Cells["ITEM_CD"].Value.ToString() + "' ");
                        sb.AppendLine("     ,'" + double.Parse(dgv.Rows[i].Cells["TOTAL_AMT"].Value.ToString()) + "' ");
                        //유효기간 수정필요
                        // sb.AppendLine("     ,'" + dgv.Rows[i].Cells["VALID_DATE"].Value.ToString() + "' ");
                        sb.AppendLine("     ,'' ");
                        sb.AppendLine("     ,'" + double.Parse(dgv.Rows[i].Cells["PRICE"].Value.ToString()) + "' ");
                        // sb.AppendLine("     ,'" + dgv.Rows[i].Cells["ITEM_OUT_BAR"].Value.ToString() + "' ");
                        sb.AppendLine("     ,@INSTAFF ");
                        sb.AppendLine("     ,convert(varchar, getdate(), 120) ");
                        sb.AppendLine(" ) ");

                    }
                    else
                    {
                        sb.AppendLine("update F_ITEM_OUT_DETAIL set");

                        sb.AppendLine("    , ITEM_CD =  '" + dgv.Rows[i].Cells["ITEM_CD"].Value.ToString() + "' ");
                        sb.AppendLine("    , OUT_AMT = '" + double.Parse(dgv.Rows[i].Cells["TOTAL_AMT"].Value.ToString()) + "' ");

                        //유효기간 수정필요
                        // sb.AppendLine("     ,VALID_DATE = '" + dgv.Rows[i].Cells["VALID_DATE"].Value.ToString() + "' ");
                        sb.AppendLine("    , VALID_DATE =''");
                        sb.AppendLine("    , PRICE = '" + double.Parse(dgv.Rows[i].Cells["PRICE"].Value.ToString()) + "' ");
                        // sb.AppendLine("    , ITEM_OUT_BAR ");
                        sb.AppendLine("    , INSTAFF  = ''");
                        sb.AppendLine("    , INTIME = convert(varchar, getdate(), 120)) ");


                    }

                    if (del_dgv != null && del_dgv.Rows.Count > 0)
                    {
                        sb.AppendLine("delete from F_ITEM_OUT_DETAIL ");
                        sb.AppendLine("where  OUT_DATE = '" + del_dgv.Rows[i].Cells["OUT_DATE"].Value.ToString() + "'");
                        sb.AppendLine("AND  OUT_DATE = '" + del_dgv.Rows[i].Cells["OUT_CD"].Value.ToString() + "'");
                        sb.AppendLine("AND  OUT_DATE = '" + del_dgv.Rows[i].Cells["SEQ"].Value.ToString() + "'");


                    }


                }





                sCommand = new SqlCommand(sb.ToString());
                sCommand.Parameters.AddWithValue("@OUT_DATE", outdate);
                //sCommand.Parameters.AddWithValue("@OUT_CD", seq);
                //sCommand.Parameters.AddWithValue("@ITEM_CD", recipient);
                sCommand.Parameters.AddWithValue("@RECIPIENT", recipient);
                sCommand.Parameters.AddWithValue("@RECIPIENT_PHONE", recipientPhone);
                sCommand.Parameters.AddWithValue("@CUST_CD", custcd);
                sCommand.Parameters.AddWithValue("@REQ_COMMENT", comment);
                sCommand.Parameters.AddWithValue("@COMP_PHONE_CD", corPhone);
                sCommand.Parameters.AddWithValue("@CUST_ADDR_CD", addr);
                sCommand.Parameters.AddWithValue("@OUT_REQ_CD", outReq);
                sCommand.Parameters.AddWithValue("@OUT_REQ_DATE", outReqDate);
                sCommand.Parameters.AddWithValue("@OUT_SIGN_CMT", outSignCmt);
                sCommand.Parameters.AddWithValue("@QUAL_CD", qualCd);
                sCommand.Parameters.AddWithValue("@QUAL_DATE", qualDate);
                sCommand.Parameters.AddWithValue("@QUAL_SIGN_CMT", qualSignCmt);
                sCommand.Parameters.AddWithValue("@ST_MG_CD", stMg);
                sCommand.Parameters.AddWithValue("@ST_MG_DATE", stMgDate);
                sCommand.Parameters.AddWithValue("@ST_MG_SIGN_CMT", stMgSignCmt);
                if (ckSt != "")
                {
                    sCommand.Parameters.AddWithValue("@ST_MG_SIGN_YN", ckSt);
                }
                else
                {
                    sCommand.Parameters.AddWithValue("@QUAL_SIGN_YN", ckQual);
                }
                sCommand.Parameters.AddWithValue("@DELIVERY_CD", deliveryCd);
                sCommand.Parameters.AddWithValue("@DELIVERY_INVOICE", deliveryInvoice);
                sCommand.Parameters.AddWithValue("@DELIVERY_CMT", deliveryCmt);
                sCommand.Parameters.AddWithValue("@INSTAFF", "");

                int qResult = wAdo.SqlCommandEtc(sCommand, "UPDATE_OUT_TB");
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






















        public int updateLoc(
            string code
            , string name
            , string storage
            , string comment
            , string dbName)
        {
            try
            {
                wAdo = new wnAdo();
                sb = new StringBuilder();
                sb.AppendLine("update " + dbName + " set ");
                sb.AppendLine("     LOC_NM = @NAME ");
                sb.AppendLine("     , STORAGE_CD = @STORAGE ");
                sb.AppendLine("     , COMMENT = @COMMENT ");

                sb.AppendLine(" where ");
                sb.AppendLine("     LOC_CD = @CODE ");
                sCommand = new SqlCommand(sb.ToString());
                sCommand.Parameters.AddWithValue("@NAME", name);
                sCommand.Parameters.AddWithValue("@COMMENT", comment);
                sCommand.Parameters.AddWithValue("@STORAGE", storage);
                sCommand.Parameters.AddWithValue("@CODE", code);


                int qResult = wAdo.SqlCommandEtc(sCommand, "insert_CUST_TB");
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

        public int updateFlow(string code
            , string name
            , string fac
            , string insertYn
            , string ckYn
            , string storage
            , string staff
            , string comm
            , string dbName)
        {

            try
            {
                wAdo = new wnAdo();



                sb = new StringBuilder();
                sb.AppendLine("update " + dbName + " set");
                sb.AppendLine("      FLOW_NM = @NAME  ");
                sb.AppendLine("     , STORAGE_CD  = @STORAGE");
                sb.AppendLine("     , FLOW_INSERT_YN = @INSERTYN ");
                sb.AppendLine("     , FLOW_CHK_YN  = @CKYN");
                sb.AppendLine("     , POOR_TYPE_CD = @FAC ");
                sb.AppendLine("     , STAFF_CD = @STAFF");
                sb.AppendLine("     , COMMENT = @COMMENT ");
                sb.AppendLine(" where ");
                sb.AppendLine("     FLOW_CD = @CODE ");

                sCommand = new SqlCommand(sb.ToString());
                sCommand.Parameters.AddWithValue("@CODE", code);
                sCommand.Parameters.AddWithValue("@NAME", name);
                sCommand.Parameters.AddWithValue("@STORAGE", storage);
                sCommand.Parameters.AddWithValue("@INSERTYN", insertYn);
                sCommand.Parameters.AddWithValue("@CKYN", ckYn);
                sCommand.Parameters.AddWithValue("@FAC", fac);
                sCommand.Parameters.AddWithValue("@STAFF", staff);
                sCommand.Parameters.AddWithValue("@COMMENT", comm);






                int qResult = wAdo.SqlCommandEtc(sCommand, "insert_CUST_TB");
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








        public int updateBank(
       string code
      , string name
      , string gubun
      , string dFee
      , string iFee
      , string used
      , string comment
      , string account_num
      , string account_holder
      , string dbName)
        {
            try
            {
                wAdo = new wnAdo();
                sb = new StringBuilder();
                sb.AppendLine("update " + dbName + " set ");
                sb.AppendLine("     BANK_NM = @NAME ");
                sb.AppendLine("     ,COMMENT = @COMMENT ");
                sb.AppendLine("     ,COUNTRY_CD = @GUBUN ");
                sb.AppendLine("     ,DOMESTIC_FEE = @DFEE ");
                sb.AppendLine("     ,INTERNATIONAL_FEE = @IFEE ");
                sb.AppendLine("     ,USED_CD = @USED  ");
                sb.AppendLine(" where ");
                sb.AppendLine("     BANK_CD = @CODE ");


                sCommand = new SqlCommand(sb.ToString());
                sCommand.Parameters.AddWithValue("@NAME", name);
                sCommand.Parameters.AddWithValue("@COMMENT", comment);
                sCommand.Parameters.AddWithValue("@GUBUN", gubun);
                sCommand.Parameters.AddWithValue("@DFEE", dFee);
                sCommand.Parameters.AddWithValue("@IFEE", used);
                sCommand.Parameters.AddWithValue("@USED", used);
                sCommand.Parameters.AddWithValue("@CODE", code);




                int qResult = wAdo.SqlCommandEtc(sCommand, "insert_CUST_TB");
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


        //기초코드 수정 쿼리
        public int updateDept(
            string code
            , string name
            , string comment
            , string dbName)
        {
            try
            {
                wAdo = new wnAdo();
                sb = new StringBuilder();
                sb.AppendLine("update " + dbName + " set ");
                sb.AppendLine("     DEPT_NM = @NAME ");
                sb.AppendLine("     ,COMMENT = @COMMENT ");
                sb.AppendLine(" where ");
                sb.AppendLine("     DEPT_CD = @CODE ");


                sCommand = new SqlCommand(sb.ToString());
                sCommand.Parameters.AddWithValue("@NAME", name);
                sCommand.Parameters.AddWithValue("@COMMENT", comment);
                sCommand.Parameters.AddWithValue("@CODE", code);


                int qResult = wAdo.SqlCommandEtc(sCommand, "insert_CUST_TB");
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

        //기초코드 수정 쿼리
        public int updatePos(
            string code
            , string name
            , string comment
            , string dbName)
        {
            try
            {
                wAdo = new wnAdo();
                sb = new StringBuilder();
                sb.AppendLine("update " + dbName + " set ");
                sb.AppendLine("     POS_NM = @NAME ");
                sb.AppendLine("     ,COMMENT = @COMMENT ");
                sb.AppendLine(" where ");
                sb.AppendLine("     POS_CD = @CODE ");


                sCommand = new SqlCommand(sb.ToString());
                sCommand.Parameters.AddWithValue("@NAME", name);
                sCommand.Parameters.AddWithValue("@COMMENT", comment);
                sCommand.Parameters.AddWithValue("@CODE", code);


                int qResult = wAdo.SqlCommandEtc(sCommand, "insert_CUST_TB");
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
        }       //기초코드 수정 쿼리
        public int updateStorage(
            string code
            , string name
            , string comment
            , string dbName)
        {
            try
            {
                wAdo = new wnAdo();
                sb = new StringBuilder();
                sb.AppendLine("update " + dbName + " set ");
                sb.AppendLine("     STORAGE_NM = @NAME ");
                sb.AppendLine("     ,COMMENT = @COMMENT ");
                sb.AppendLine(" where ");
                sb.AppendLine("     STORAGE_CD = @CODE ");


                sCommand = new SqlCommand(sb.ToString());
                sCommand.Parameters.AddWithValue("@NAME", name);
                sCommand.Parameters.AddWithValue("@COMMENT", comment);
                sCommand.Parameters.AddWithValue("@CODE", code);


                int qResult = wAdo.SqlCommandEtc(sCommand, "insert_CUST_TB");
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
        }       //기초코드 수정 쿼리
        public int updateUnit(
            string code
            , string name
            , string comment
            , string dbName)
        {
            try
            {
                wAdo = new wnAdo();
                sb = new StringBuilder();
                sb.AppendLine("update " + dbName + " set ");
                sb.AppendLine("     UNIT_NM = @NAME ");
                sb.AppendLine("     ,COMMENT = @COMMENT ");
                sb.AppendLine(" where ");
                sb.AppendLine("     UNIT_CD = @CODE ");


                sCommand = new SqlCommand(sb.ToString());
                sCommand.Parameters.AddWithValue("@NAME", name);
                sCommand.Parameters.AddWithValue("@COMMENT", comment);
                sCommand.Parameters.AddWithValue("@CODE", code);


                int qResult = wAdo.SqlCommandEtc(sCommand, "insert_CUST_TB");
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
        }       //기초코드 수정 쿼리
        public int updateBank(
            string code
            , string name
            , string comment
            , string dbName)
        {
            try
            {
                wAdo = new wnAdo();
                sb = new StringBuilder();
                sb.AppendLine("update " + dbName + " set ");
                sb.AppendLine("     BANK_NM = @NAME ");
                sb.AppendLine("     ,COMMENT = @COMMENT ");
                sb.AppendLine(" where ");
                sb.AppendLine("     BANK_CD = @CODE ");


                sCommand = new SqlCommand(sb.ToString());
                sCommand.Parameters.AddWithValue("@NAME", name);
                sCommand.Parameters.AddWithValue("@COMMENT", comment);
                sCommand.Parameters.AddWithValue("@CODE", code);


                int qResult = wAdo.SqlCommandEtc(sCommand, "insert_CUST_TB");
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


        //기초코드 수정 쿼리
        public int updatePoor(
            string code
            , string name
            , string tyCd
            , string comment
            , string dbName)
        {
            try
            {
                wAdo = new wnAdo();
                sb = new StringBuilder();
                sb.AppendLine("update " + dbName + " set ");
                sb.AppendLine("     POOR_NM = @NAME ");
                sb.AppendLine("     ,COMMENT = @COMMENT ");
                sb.AppendLine("     ,TYPE_CD = @tyCd ");

                sb.AppendLine(" where ");
                sb.AppendLine("     POOR_CD = @CODE ");


                sCommand = new SqlCommand(sb.ToString());
                sCommand.Parameters.AddWithValue("@NAME", name);
                sCommand.Parameters.AddWithValue("@COMMENT", comment);
                sCommand.Parameters.AddWithValue("@tyCd", tyCd);

                sCommand.Parameters.AddWithValue("@CODE", code);


                int qResult = wAdo.SqlCommandEtc(sCommand, "insert_CUST_TB");
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



        }       //기초코드 수정 쿼리
        public int updateUcost(
            string code
            , string name
            , string comment
            , string dbName)
        {
            try
            {
                wAdo = new wnAdo();
                sb = new StringBuilder();
                sb.AppendLine("update " + dbName + " set ");
                sb.AppendLine("     N_UCOST_NM = @NAME ");
                sb.AppendLine("     ,COMMENT = @COMMENT ");
                sb.AppendLine(" where ");
                sb.AppendLine("     N_UCOST_CODE = @CODE ");


                sCommand = new SqlCommand(sb.ToString());
                sCommand.Parameters.AddWithValue("@NAME", name);
                sCommand.Parameters.AddWithValue("@COMMENT", comment);
                sCommand.Parameters.AddWithValue("@CODE", code);


                int qResult = wAdo.SqlCommandEtc(sCommand, "insert_CUST_TB");
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
        }       //기초코드 수정 쿼리
        public int updateDelivery(
            string code
            , string name
            , string comment
            , string dbName)
        {
            try
            {
                wAdo = new wnAdo();
                sb = new StringBuilder();
                sb.AppendLine("update " + dbName + " set ");
                sb.AppendLine("     DELIVERY_NM = @NAME ");
                sb.AppendLine("     ,COMMENT = @COMMENT ");
                sb.AppendLine(" where ");
                sb.AppendLine("     DELIVERY_CD = @CODE ");


                sCommand = new SqlCommand(sb.ToString());
                sCommand.Parameters.AddWithValue("@NAME", name);
                sCommand.Parameters.AddWithValue("@COMMENT", comment);
                sCommand.Parameters.AddWithValue("@CODE", code);


                int qResult = wAdo.SqlCommandEtc(sCommand, "insert_CUST_TB");
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
        }       //기초코드 수정 쿼리
        public int updateCategory(
            string gubun
            , string code
            , string name
            , string comment
            , string dbName)
        {
            try
            {
                wAdo = new wnAdo();
                sb = new StringBuilder();
                sb.AppendLine("update " + dbName + " set ");
                sb.AppendLine("     CATEGORY_NM = @NAME ");
                sb.AppendLine("     ,COMMENT = @COMMENT ");
                sb.AppendLine(" where ");
                sb.AppendLine("     CATEGORY_CD = @CODE ");
                sb.AppendLine("     AND  CATEGORY_GUBUN= @GUBUN ");


                sCommand = new SqlCommand(sb.ToString());
                sCommand.Parameters.AddWithValue("@NAME", name);
                sCommand.Parameters.AddWithValue("@COMMENT", comment);
                sCommand.Parameters.AddWithValue("@GUBUN", gubun);
                sCommand.Parameters.AddWithValue("@CODE", code);


                int qResult = wAdo.SqlCommandEtc(sCommand, "insert_CUST_TB");
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

        //유형 수정 쿼리
        public int updateType(
            string code
            , string name
            , string yn
            , string comment
            , string dbName)
        {
            try
            {
                wAdo = new wnAdo();
                sb = new StringBuilder();
                sb.AppendLine("update " + dbName + " set ");
                sb.AppendLine("     TYPE_NM = @NAME ");
                sb.AppendLine("     ,COMMENT = @COMMENT ");
                sb.AppendLine("     ,POOR_TYPE_YN = @yn ");
                sb.AppendLine(" where ");
                sb.AppendLine("     TYPE_CD = @CODE ");


                sCommand = new SqlCommand(sb.ToString());
                sCommand.Parameters.AddWithValue("@NAME", name);
                sCommand.Parameters.AddWithValue("@COMMENT", comment);
                sCommand.Parameters.AddWithValue("@yn", yn);
                sCommand.Parameters.AddWithValue("@CODE", code);


                int qResult = wAdo.SqlCommandEtc(sCommand, "insert_CUST_TB");
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

        //유형 수정 쿼리
        public int updateExchange(
            string seq
            , string realTime
            , string baseUsd)
        {
            try
            {
                wAdo = new wnAdo();
                sb = new StringBuilder();
                sb.AppendLine("update N_USD_INFO set ");
                sb.AppendLine("     USD_REALTIME = @REALTIME ");
                sb.AppendLine("     ,USD_BASE = @BASE ");
                sb.AppendLine(" where ");
                sb.AppendLine("     SEQ = @SEQ ");


                sCommand = new SqlCommand(sb.ToString());
                sCommand.Parameters.AddWithValue("@SEQ", seq);
                sCommand.Parameters.AddWithValue("@REALTIME", realTime);
                sCommand.Parameters.AddWithValue("@BASE", baseUsd);



                int qResult = wAdo.SqlCommandEtc(sCommand, "insert_CUST_TB");
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




        public int updateSoo(
             string dateSoo
                        , string sooCd
                        , string custCd
                        , string sooMoney
                        , string dcMoney
                        , string comm
                        , string balance
                        , string totalMoney
                        , string gubun
                        , string totalMoneyTemp
                        , string sooMoneyTemp
                        , string dcMoneyTemp
            )
        {
            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                wnDm wDm = new wnDm();


                sb.AppendLine("update  F_COLLECT set");
                sb.AppendLine("     SOO_GUBUN =  '"+gubun+"'");
                sb.AppendLine("     ,SOO_MONEY =" + sooMoney.Replace(",", "") + "");
                sb.AppendLine("     ,TOTAL_MONEY =" + totalMoney.Replace(",","") + " ");
                sb.AppendLine("     ,DC_MONEY =" + dcMoney.Replace(",", "") + " ");
                sb.AppendLine("     ,COMMENT ='" + comm + "' ");
                sb.AppendLine("     ,INSTAFF = '' ");
                sb.AppendLine("     ,UPTIME  = convert(varchar, getdate(), 120)");
                sb.AppendLine("     where SOO_DATE = '" + dateSoo + "'");
                sb.AppendLine("     and SOO_CD = '" + sooCd + "'");

                

                sb.AppendLine("update N_CUST_CODE set");
                sb.AppendLine("     BALANCE  =  (select  BALANCE " );
                sb.AppendLine("     from N_CUST_CODE " );
                sb.AppendLine("     where CUST_CD = '" + custCd + "' ) + " + double.Parse(totalMoneyTemp).ToString() + " ");
                sb.AppendLine("     where CUST_CD = '" + custCd + "' ");

                sb.AppendLine("update N_CUST_CODE set");
                sb.AppendLine("     BALANCE  =  (select  BALANCE ");
                sb.AppendLine("     from N_CUST_CODE ");
                sb.AppendLine("     where CUST_CD = '" + custCd + "' ) - " + double.Parse(totalMoney).ToString() + " ");
                sb.AppendLine("     where CUST_CD = '" + custCd + "' ");


                bool isCustDay = wDm.isCustDayTotal(dateSoo,custCd);

                if (!isCustDay)
                {
                    sb.AppendLine(wDm.Create_New_CustDayTotal(dateSoo, custCd));
                }

                sb.AppendLine(" UPDATE T_CUST_DAY_TOTAL SET ");
                sb.AppendLine(" COL_MONEY = COL_MONEY - " + sooMoneyTemp.Replace(",", "") + " ");
                sb.AppendLine(" ,COL_DC_MONEY = COL_DC_MONEY - " + dcMoneyTemp.Replace(",", "") + " ");
                sb.AppendLine(" ,COL_TOTAL_MONEY = COL_TOTAL_MONEY - " + totalMoneyTemp.Replace(",", "") + " ");
                sb.AppendLine(" WHERE INPUT_DATE ='" + dateSoo + "'  AND CUST_CD = '" + custCd + "'  ");


                sb.AppendLine(" UPDATE T_CUST_DAY_TOTAL SET ");
                sb.AppendLine(" COL_MONEY = COL_MONEY + " + sooMoney.Replace(",", "") + " ");
                sb.AppendLine(" ,COL_DC_MONEY = COL_DC_MONEY + " + dcMoney.Replace(",", "") + " ");
                sb.AppendLine(" ,COL_TOTAL_MONEY = COL_TOTAL_MONEY + " + totalMoney.Replace(",", "") + " ");
                sb.AppendLine(" WHERE INPUT_DATE ='" + dateSoo + "'  AND CUST_CD = '" + custCd + "'  ");

                sb.AppendLine(wDm.CustDayTotal_Change_Balance_Today(dateSoo, custCd));

                sb.AppendLine(wDm.CustDayTotal_Change_Balance(dateSoo, custCd, totalMoneyTemp.Replace(",", ""), "+"));
                sb.AppendLine(wDm.CustDayTotal_Change_Balance(dateSoo, custCd, totalMoney.Replace(",", ""), "-"));


                sCommand = new SqlCommand(sb.ToString());

                int qResult = wAdo.SqlCommandEtc(sCommand, "update_PLAN_TB");
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

        public int updateGive(
             string dateGive
                        , string giveCd
                        , string custCd
                        , string giveMoney
                        , string dcMoney
                        , string comm
                        , string balance
                        , string totalMoney
                        , string gubun
                        , string totalMoneyTemp
                        , string sooMoneyTemp
                        , string dcMoneyTemp
            )
        {
            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                wnDm wDm = new wnDm();


                sb.AppendLine("update  F_GIVE set");
                sb.AppendLine("     GIVE_GUBUN =  '" + gubun + "'");
                sb.AppendLine("     ,GIVE_MONEY =" + giveMoney.Replace(",", "") + "");
                sb.AppendLine("     ,TOTAL_MONEY =" + totalMoney.Replace(",", "") + " ");
                sb.AppendLine("     ,DC_MONEY =" + dcMoney.Replace(",", "") + " ");
                sb.AppendLine("     ,COMMENT ='" + comm + "' ");
                sb.AppendLine("     ,INSTAFF = '' ");
                sb.AppendLine("     ,UPTIME  = convert(varchar, getdate(), 120)");
                sb.AppendLine("     where GIVE_DATE = '" + dateGive + "'");
                sb.AppendLine("     and GIVE_CD = '" + giveCd + "'");



                sb.AppendLine("update N_CUST_CODE set");
                sb.AppendLine("     BALANCE  =  (select  BALANCE ");
                sb.AppendLine("     from N_CUST_CODE ");
                sb.AppendLine("     where CUST_CD = '" + custCd + "' ) - " + double.Parse(totalMoneyTemp).ToString() + " ");
                sb.AppendLine("     where CUST_CD = '" + custCd + "' ");

                sb.AppendLine("update N_CUST_CODE set");
                sb.AppendLine("     BALANCE  =  (select  BALANCE ");
                sb.AppendLine("     from N_CUST_CODE ");
                sb.AppendLine("     where CUST_CD = '" + custCd + "' ) + " + double.Parse(totalMoney).ToString() + " ");
                sb.AppendLine("     where CUST_CD = '" + custCd + "' ");


                bool isCustDay = wDm.isCustDayTotal(dateGive, custCd);

                if (!isCustDay)
                {
                    sb.AppendLine(wDm.Create_New_CustDayTotal(dateGive, custCd));
                }

                sb.AppendLine(" UPDATE T_CUST_DAY_TOTAL SET ");
                sb.AppendLine(" PAY_MONEY = PAY_MONEY - " + sooMoneyTemp.Replace(",", "") + " ");
                sb.AppendLine(" ,PAY_DC_MONEY = PAY_DC_MONEY - " + dcMoneyTemp.Replace(",", "") + " ");
                sb.AppendLine(" ,PAY_TOTAL_MONEY = PAY_TOTAL_MONEY - " + totalMoneyTemp.Replace(",", "") + " ");
                sb.AppendLine(" WHERE INPUT_DATE ='" + dateGive + "'  AND CUST_CD = '" + custCd + "'  ");


                sb.AppendLine(" UPDATE T_CUST_DAY_TOTAL SET ");
                sb.AppendLine(" PAY_MONEY = PAY_MONEY + " + giveMoney.Replace(",", "") + " ");
                sb.AppendLine(" ,PAY_DC_MONEY = PAY_DC_MONEY + " + dcMoney.Replace(",", "") + " ");
                sb.AppendLine(" ,PAY_TOTAL_MONEY = PAY_TOTAL_MONEY + " + totalMoney.Replace(",", "") + " ");
                sb.AppendLine(" WHERE INPUT_DATE ='" + dateGive + "'  AND CUST_CD = '" + custCd + "'  ");

                sb.AppendLine(wDm.CustDayTotal_Change_Balance_Today(dateGive, custCd));

                sb.AppendLine(wDm.CustDayTotal_Change_Balance(dateGive, custCd, totalMoneyTemp.Replace(",", ""), "-"));
                sb.AppendLine(wDm.CustDayTotal_Change_Balance(dateGive, custCd, totalMoney.Replace(",", ""), "+"));


                sCommand = new SqlCommand(sb.ToString());

                int qResult = wAdo.SqlCommandEtc(sCommand, "update_PLAN_TB");
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

        public int updateOrder(
              string order_date
            , string lbl_order_cd
            , string txt_cust_cd
            , string in_req_date
            , string comment
            //, string pur_yn
            , DataGridView o_rm_dgv
            , DataGridView del_dgv)
        {
            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                sb = new StringBuilder();
                sb.AppendLine("update F_ORDER set");
                sb.AppendLine("      CUST_CD = @CUST_CD ");
                sb.AppendLine("     ,INPUT_REQ_DATE = @INPUT_REQ_DATE ");
              //  sb.AppendLine("     ,COMPLETE_YN = '" + pur_yn + "' ");
                sb.AppendLine("     ,UPSTAFF = '" + Common.p_strStaffNo + "' ");
                sb.AppendLine("     ,UPTIME = convert(varchar, getdate(), 120) ");
                sb.AppendLine("     ,COMMENT = @COMMENT ");

                sb.AppendLine(" where ORDER_DATE = @ORDER_DATE and ORDER_CD= @ORDER_CD ");

                if (o_rm_dgv.Rows.Count > 0)
                {
                    for (int i = 0; i < o_rm_dgv.Rows.Count; i++)
                    {
                        string txt_seq = (string)o_rm_dgv.Rows[i].Cells["SEQ"].Value;
                        if (txt_seq == "" || txt_seq == null)
                        {
                            sb.AppendLine("declare @order_seq" + i + " int ");
                            sb.AppendLine("select @order_seq" + i + " =ISNULL(MAX(SEQ),0)+1 from F_ORDER_DETAIL ");
                            sb.AppendLine("where ORDER_DATE = '" + order_date + "' ");
                            sb.AppendLine("and ORDER_CD = '" + lbl_order_cd + "' ");

                            sb.AppendLine("insert into F_ORDER_DETAIL(");
                            sb.AppendLine("     ORDER_DATE ");
                            sb.AppendLine("     ,ORDER_CD ");
                            sb.AppendLine("     ,SEQ ");
                            sb.AppendLine("     ,RAW_MAT_CD ");
                            sb.AppendLine("     ,TOTAL_AMT ");
                            sb.AppendLine("     ,PRICE ");
                            sb.AppendLine("     ,TOTAL_MONEY ");
                            //sb.AppendLine("     ,INSTAFF ");
                            sb.AppendLine("     ,INTIME ");
                            sb.AppendLine("  )values ( ");
                            sb.AppendLine("     '" + order_date + "' ");
                            sb.AppendLine("     ,'" + lbl_order_cd + "' ");
                            sb.AppendLine("     ,@order_seq" + i + " ");
                            sb.AppendLine("     ,'" + o_rm_dgv.Rows[i].Cells["RAW_MAT_CD"].Value + "' ");
                            sb.AppendLine("     ,'" + ((string)o_rm_dgv.Rows[i].Cells["TOTAL_AMT"].Value).Replace(",", "") + "' ");
                            sb.AppendLine("     ,'" + (o_rm_dgv.Rows[i].Cells["PRICE"].Value.ToString()).Replace(",", "") + "' ");
                            sb.AppendLine("     ,'" + (o_rm_dgv.Rows[i].Cells["TOTAL_MONEY"].Value.ToString()).Replace(",", "") + "' ");
                            //sb.AppendLine("     ,'" + Common.p_strStaffNo + "' ");
                            sb.AppendLine("     ,convert(varchar, getdate(), 120)  ");
                            sb.AppendLine("  )");
                        }
                        else
                        {
                            sb.AppendLine("update F_ORDER_DETAIL set");
                            sb.AppendLine("       RAW_MAT_CD =  '" + o_rm_dgv.Rows[i].Cells["RAW_MAT_CD"].Value + "' ");
                            sb.AppendLine("      ,TOTAL_AMT =  '" + ((string)o_rm_dgv.Rows[i].Cells["TOTAL_AMT"].Value).Replace(",", "") + "' ");
                            sb.AppendLine("      ,PRICE =  '" + (o_rm_dgv.Rows[i].Cells["PRICE"].Value.ToString()).Replace(",", "") + "' ");
                            sb.AppendLine("      ,TOTAL_MONEY =  '" + (o_rm_dgv.Rows[i].Cells["TOTAL_MONEY"].Value.ToString()).Replace(",", "") + "' ");
                            sb.AppendLine("      ,UPSTAFF =  '" + Common.p_strStaffNo + "' ");
                            sb.AppendLine("      ,UPTIME =  convert(varchar, getdate(), 120) ");
                            sb.AppendLine(" where ORDER_DATE = '" + order_date + "' ");
                            sb.AppendLine(" and ORDER_CD = '" + lbl_order_cd + "' ");
                            sb.AppendLine(" and SEQ = '" + o_rm_dgv.Rows[i].Cells["SEQ"].Value + "'");
                        }
                    }
                }

                if (del_dgv.Rows.Count > 0)
                {
                    for (int i = 0; i < del_dgv.Rows.Count; i++)
                    {
                        sb.AppendLine("delete from F_ORDER_DETAIL ");
                        sb.AppendLine("    where ORDER_DATE = '" + del_dgv.Rows[i].Cells["ORDER_DATE"].Value + "' ");
                        sb.AppendLine("     and ORDER_CD = '" + del_dgv.Rows[i].Cells["ORDER_CD"].Value + "' ");
                        sb.AppendLine("     and SEQ = '" + del_dgv.Rows[i].Cells["SEQ"].Value + "' ");
                    }
                }
                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@CUST_CD", txt_cust_cd);
                sCommand.Parameters.AddWithValue("@INPUT_REQ_DATE", in_req_date);
                sCommand.Parameters.AddWithValue("@COMMENT", comment);

                sCommand.Parameters.AddWithValue("@ORDER_DATE", order_date);
                sCommand.Parameters.AddWithValue("@ORDER_CD", lbl_order_cd);

                int qResult = wAdo.SqlCommandEtc(sCommand, "update_PLAN_TB");
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

















        //기초코드 삭제 쿼리
        public int deleteCountry(
            string code
            , string dbName)
        {
            wAdo = new wnAdo();
            sb = new StringBuilder();
            sb.AppendLine("delete from " + dbName);
            sb.AppendLine(" where ");
            sb.AppendLine("     COUNTRY_CD = @CODE ");
            sCommand = new SqlCommand(sb.ToString());
            sCommand.Parameters.AddWithValue("@CODE", code);


            int qResult = wAdo.SqlCommandEtc(sCommand, "insert_CUST_TB");
            if (qResult > 0)
            {
                return 0;  // 0 true, 1 false
            }
            else return 1;
        }

        //기초코드 삭제 쿼리
        public int deleteDept(
            string code
            , string dbName)
        {
            wAdo = new wnAdo();
            sb = new StringBuilder();
            sb.AppendLine("delete from " + dbName);
            sb.AppendLine(" where ");
            sb.AppendLine("     DEPT_CD = @CODE ");
            sCommand = new SqlCommand(sb.ToString());
            sCommand.Parameters.AddWithValue("@CODE", code);


            int qResult = wAdo.SqlCommandEtc(sCommand, "insert_CUST_TB");
            if (qResult > 0)
            {
                return 0;  // 0 true, 1 false
            }
            else return 1;
        }

        public int deletePos(
        string code
        , string dbName)
        {
            wAdo = new wnAdo();
            sb = new StringBuilder();
            sb.AppendLine("delete from " + dbName);
            sb.AppendLine(" where ");
            sb.AppendLine("     POS_CD = @CODE ");
            sCommand = new SqlCommand(sb.ToString());
            sCommand.Parameters.AddWithValue("@CODE", code);


            int qResult = wAdo.SqlCommandEtc(sCommand, "insert_CUST_TB");
            if (qResult > 0)
            {
                return 0;  // 0 true, 1 false
            }
            else return 1;
        }

        public int deleteStorage(
        string code
        , string dbName)
        {
            wAdo = new wnAdo();
            sb = new StringBuilder();
            sb.AppendLine("delete from " + dbName);
            sb.AppendLine(" where ");
            sb.AppendLine("     STORAGE_CD = @CODE ");
            sCommand = new SqlCommand(sb.ToString());
            sCommand.Parameters.AddWithValue("@CODE", code);


            int qResult = wAdo.SqlCommandEtc(sCommand, "insert_CUST_TB");
            if (qResult > 0)
            {
                return 0;  // 0 true, 1 false
            }
            else return 1;
        }
        public int deleteUnit(
        string code
        , string dbName)
        {
            wAdo = new wnAdo();
            sb = new StringBuilder();
            sb.AppendLine("delete from " + dbName);
            sb.AppendLine(" where ");
            sb.AppendLine("     UNIT_CD = @CODE ");
            sCommand = new SqlCommand(sb.ToString());
            sCommand.Parameters.AddWithValue("@CODE", code);


            int qResult = wAdo.SqlCommandEtc(sCommand, "insert_CUST_TB");
            if (qResult > 0)
            {
                return 0;  // 0 true, 1 false
            }
            else return 1;
        }


        public int deleteType(
        string code
        , string dbName)
        {
            wAdo = new wnAdo();
            sb = new StringBuilder();
            sb.AppendLine("delete from " + dbName);
            sb.AppendLine(" where ");
            sb.AppendLine("     TYPE_CD = @CODE ");
            sCommand = new SqlCommand(sb.ToString());
            sCommand.Parameters.AddWithValue("@CODE", code);


            int qResult = wAdo.SqlCommandEtc(sCommand, "insert_CUST_TB");
            if (qResult > 0)
            {
                return 0;  // 0 true, 1 false
            }
            else return 1;
        }


        public int deletePoor(
        string code
        , string dbName)
        {
            wAdo = new wnAdo();
            sb = new StringBuilder();
            sb.AppendLine("delete from " + dbName);
            sb.AppendLine(" where ");
            sb.AppendLine("     POOR_CD = @CODE ");
            sCommand = new SqlCommand(sb.ToString());
            sCommand.Parameters.AddWithValue("@CODE", code);


            int qResult = wAdo.SqlCommandEtc(sCommand, "insert_CUST_TB");
            if (qResult > 0)
            {
                return 0;  // 0 true, 1 false
            }
            else return 1;
        }

        public int deleteUcost(
        string code
        , string dbName)
        {
            wAdo = new wnAdo();
            sb = new StringBuilder();
            sb.AppendLine("delete from " + dbName);
            sb.AppendLine(" where ");
            sb.AppendLine("     N_UCOST_CODE = @CODE ");
            sCommand = new SqlCommand(sb.ToString());
            sCommand.Parameters.AddWithValue("@CODE", code);


            int qResult = wAdo.SqlCommandEtc(sCommand, "insert_CUST_TB");
            if (qResult > 0)
            {
                return 0;  // 0 true, 1 false
            }
            else return 1;
        }

        public int deleteDelivery(
        string code
        , string dbName)
        {
            wAdo = new wnAdo();
            sb = new StringBuilder();
            sb.AppendLine("delete from " + dbName);
            sb.AppendLine(" where ");
            sb.AppendLine("     DELIVERY_CD = @CODE ");
            sCommand = new SqlCommand(sb.ToString());
            sCommand.Parameters.AddWithValue("@CODE", code);


            int qResult = wAdo.SqlCommandEtc(sCommand, "insert_CUST_TB");
            if (qResult > 0)
            {
                return 0;  // 0 true, 1 false
            }
            else return 1;
        }


        public int deleteCust(
        string code
        , string dbName)
        {
            wAdo = new wnAdo();
            sb = new StringBuilder();
            sb.AppendLine("delete from " + dbName);
            sb.AppendLine(" where ");
            sb.AppendLine("     CUST_CD = @CODE ");
            sCommand = new SqlCommand(sb.ToString());
            sCommand.Parameters.AddWithValue("@CODE", code);


            int qResult = wAdo.SqlCommandEtc(sCommand, "insert_CUST_TB");
            if (qResult > 0)
            {
                return 0;  // 0 true, 1 false
            }
            else return 1;
        }

        public int deleteBank(
        string code
        , string dbName)
        {
            wAdo = new wnAdo();
            sb = new StringBuilder();
            sb.AppendLine("delete from " + dbName);
            sb.AppendLine(" where ");
            sb.AppendLine("     BANK_CD = @CODE ");
            sCommand = new SqlCommand(sb.ToString());
            sCommand.Parameters.AddWithValue("@CODE", code);


            int qResult = wAdo.SqlCommandEtc(sCommand, "insert_CUST_TB");
            if (qResult > 0)
            {
                return 0;  // 0 true, 1 false
            }
            else return 1;
        }
        public int deleteCategory(
            string gubun
            , string code
            , string dbName)
        {
            wAdo = new wnAdo();
            sb = new StringBuilder();
            sb.AppendLine("delete from " + dbName);
            sb.AppendLine(" where ");
            sb.AppendLine("     CATEGORY_CD = @CODE ");
            sb.AppendLine("     AND CATEGORY_GUBUN = @GUBUN ");
            sCommand = new SqlCommand(sb.ToString());
            sCommand.Parameters.AddWithValue("@CODE", code);
            sCommand.Parameters.AddWithValue("@GUBUN", gubun);


            int qResult = wAdo.SqlCommandEtc(sCommand, "insert_CUST_TB");
            if (qResult > 0)
            {
                return 0;  // 0 true, 1 false
            }
            else return 1;
        }

        public int deleteFlow(string code, string dbName)
        {

            wAdo = new wnAdo();
            sb = new StringBuilder();
            sb.AppendLine("delete from " + dbName);
            sb.AppendLine(" where ");
            sb.AppendLine("     FLOW_CD = @CODE ");

            sCommand = new SqlCommand(sb.ToString());
            sCommand.Parameters.AddWithValue("@CODE", code);



            int qResult = wAdo.SqlCommandEtc(sCommand, "insert_CUST_TB");
            if (qResult > 0)
            {
                return 0;  // 0 true, 1 false
            }
            else return 1;

        }

        internal int deleteLoc(
            string code
            , string dbName)
        {
            wAdo = new wnAdo();
            sb = new StringBuilder();
            sb.AppendLine("delete from " + dbName);
            sb.AppendLine(" where ");
            sb.AppendLine("     LOC_CD = @CODE ");

            sCommand = new SqlCommand(sb.ToString());
            sCommand.Parameters.AddWithValue("@CODE", code);



            int qResult = wAdo.SqlCommandEtc(sCommand, "delect_CUST_TB");
            if (qResult > 0)
            {
                return 0;  // 0 true, 1 false
            }
            else return 1;
        }

        public int deletePriceInfo(string cust, string item)
        {

            wAdo = new wnAdo();
            sb = new StringBuilder();
            sb.AppendLine("delete from N_PRICE_INFO");
            sb.AppendLine(" where ");
            sb.AppendLine("     CUST_CD = @CUST ");
            sb.AppendLine("     AND ITEM_CD = @ITEM ");


            sCommand = new SqlCommand(sb.ToString());
            sCommand.Parameters.AddWithValue("@CUST", cust);
            sCommand.Parameters.AddWithValue("@ITEM", item);



            int qResult = wAdo.SqlCommandEtc(sCommand, "insert_CUST_TB");
            if (qResult > 0)
            {
                return 0;  // 0 true, 1 false
            }
            else return 1;

        }

        public int deleteOrder(string txt_plan_date, string plan_cd)
        {
            try
            {
                wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                sb = new StringBuilder();
                sb.AppendLine("delete from F_ORDER ");
                sb.AppendLine("    where ORDER_DATE = @ORDER_DATE ");
                sb.AppendLine("    and ORDER_CD = @ORDER_CD ");

                sb.AppendLine("delete from F_ORDER_DETAIL ");
                sb.AppendLine("    where ORDER_DATE = @ORDER_DATE ");
                sb.AppendLine("    and ORDER_CD = @ORDER_CD ");

                //sb.AppendLine("delete from F_PLAN_DETAIL ");
                //sb.AppendLine("    where PLAN_DATE = @PLAN_DATE ");
                //sb.AppendLine("    and PLAN_CD = @PLAN_CD ");

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@ORDER_DATE", txt_plan_date);
                sCommand.Parameters.AddWithValue("@ORDER_CD", plan_cd);

                int qResult = wAdo.SqlCommandEtc(sCommand, "delete_ORDER_TB");
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
























        // 콤보박스

        public string queryDelivery()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select ");
            sb.AppendLine(" DELIVERY_CD as 코드, DELIVERY_NM as 명칭");
            sb.AppendLine(" from N_DELIVERY_CODE");

            return sb.ToString();

        }

        public string queryFac()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select ");
            sb.AppendLine(" FAC_CD as 코드, FAC_NM as 명칭");
            sb.AppendLine(" from N_FAC_CODE");

            return sb.ToString();

        }
        public string queryStaff()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select ");
            sb.AppendLine(" STAFF_CD as 코드, STAFF_NM as 명칭");
            sb.AppendLine(" from N_STAFF_CODE");

            return sb.ToString();

        }
        public string queryStorage()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select ");
            sb.AppendLine(" STORAGE_CD as 코드, STORAGE_NM as 명칭");
            sb.AppendLine(" from  N_STORAGE_CODE");

            return sb.ToString();

        }
        public string queryLoc()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select ");
            sb.AppendLine(" LOC_CD as 코드, LOC_NM as 명칭");
            sb.AppendLine(" from  N_LOC_CODE");

            return sb.ToString();

        }

        internal string queryPoor()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select ");
            sb.AppendLine(" TYPE_CD as 코드, TYPE_NM as 명칭");
            sb.AppendLine(" from  N_TYPE_CODE");
            sb.AppendLine(" where  POOR_TYPE_YN = 'Y'");

            return sb.ToString();
        }

        internal string queryCustAddr(
            string tsCode)
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendLine("select ");
            sb.AppendLine(" B.SEQ as 코드 ");
            sb.AppendLine("     ,B.ADDR2 as 명칭");

            sb.AppendLine("  from N_CUST_CODE A");
            sb.AppendLine("   inner join N_CUST_ADDR B");
            sb.AppendLine("   on A.CUST_CD = B.CUST_CD");

            sb.AppendLine(" where A.CUST_CD = '" + tsCode + "'");



            return sb.ToString();
        }


        internal string queryCustPhone(
            string tsCode)
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendLine("select ");
            sb.AppendLine(" B.SEQ as 코드 ");
            sb.AppendLine("      , B.NUMBER as 명칭");

            sb.AppendLine("  from N_CUST_CODE A");
            sb.AppendLine("   inner join N_CUST_PHONE B");
            sb.AppendLine("   on A.CUST_CD = B.CUST_CD");

            sb.AppendLine("where A.CUST_CD = '" + tsCode + "'");



            return sb.ToString();
        }


        internal string queryTSCode(
            string tsCode)
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendLine("select ");
            sb.AppendLine(" S_CODE as 코드, S_CODE_NM as 명칭");
            sb.AppendLine(" from  T_S_CODE");
            sb.AppendLine("where L_CODE = " + tsCode);

            return sb.ToString();
        }







        //public void ComboBox_Read_Blank(ComboBox sCombo, string sQuery)
        //{
        //    if (sQuery.Trim().Length > 10)
        //    {
        //        strQuery = sQuery.Trim();
        //    }
        //    adoTable = RunTable(strQuery);
        //    adoRow = adoTable.NewRow();
        //    adoRow[0] = "";
        //    adoRow[1] = "";
        //    adoTable.Rows.InsertAt(adoRow, 0);

        //    sCombo.DataSource = adoTable;
        //}


        public void ComboBox_Read_NoBlank(ComboBox sCombo, string sQuery)
        {

            wAdo = new wnAdo();
            string strQuery;
            if (sQuery.Trim().Length > 10)
                strQuery = sQuery.Trim();
            else
                strQuery = sQuery;

            sCommand = new SqlCommand(strQuery);
            DataTable adoTable = wAdo.SqlCommandSelect(sCommand);
            //adoRow = adoTable.NewRow();
            //adoTable.Rows.InsertAt(adoRow, 0);

            sCombo.DataSource = adoTable;
        }

        //public void ComboBox_Read_ALL(ComboBox sCombo, string sQuery)
        //{
        //    if (sQuery.Trim().Length > 10)
        //    {
        //        strQuery = sQuery.Trim();
        //    }
        //    adoTable = RunTable(strQuery);
        //    adoRow = adoTable.NewRow();
        //    adoRow[0] = "";
        //    adoRow[1] = "(전체)";
        //    adoTable.Rows.InsertAt(adoRow, 0);

        //    sCombo.DataSource = adoTable;
        //}

        public int inUpPriceInfo(
             DataGridView dgv
            , string code)
        {
            //
            try
            {

                wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    sb.AppendLine("declare @chk" + i + " int ");
                    sb.AppendLine("select @chk" + i + " = count(*) from N_PRICE_INFO ");
                    sb.AppendLine("where CUST_CD = '" + dgv.Rows[i].Cells[0].Value.ToString() + "' ");
                    sb.AppendLine(" and ITEM_CD = '" + dgv.Rows[i].Cells[1].Value.ToString() + "' ");

                    sb.AppendLine("IF(@chk" + i + ">0)  ");
                    sb.AppendLine("update N_PRICE_INFO set");
                    sb.AppendLine("      B_BOX_PRICE  = '" + dgv.Rows[i].Cells[3].Value.ToString() + "'");
                    sb.AppendLine("     , S_BOX_PRICE =  '" + dgv.Rows[i].Cells[4].Value.ToString() + "'");
                    sb.AppendLine("     , UNIT_PRICE  = '" + dgv.Rows[i].Cells[5].Value.ToString() + "'");
                    sb.AppendLine("     , UPSTAFF = '" + dgv.Rows[i].Cells[8].Value.ToString() + "'");
                    sb.AppendLine("     , UPTIME =  convert(varchar, getdate(), 120) ");
                    sb.AppendLine("where");
                    sb.AppendLine("      CUST_CD = '" + dgv.Rows[i].Cells[0].Value.ToString() + "'");
                    sb.AppendLine("     AND ITEM_CD =  '" + dgv.Rows[i].Cells[1].Value.ToString() + "'");

                    sb.AppendLine("ELSE ");
                    sb.AppendLine("insert into N_PRICE_INFO(");
                    sb.AppendLine("     CUST_CD");
                    sb.AppendLine("     , ITEM_CD");
                    sb.AppendLine("     , B_BOX_PRICE");
                    sb.AppendLine("     , S_BOX_PRICE");
                    sb.AppendLine("     , UNIT_PRICE");
                    sb.AppendLine("     , INSTAFF");
                    sb.AppendLine("     , INTIME");
                    sb.AppendLine(")");
                    sb.AppendLine("  values ( ");
                    sb.AppendLine("      '" + dgv.Rows[i].Cells[0].Value.ToString() + "'");
                    sb.AppendLine("     , '" + dgv.Rows[i].Cells[1].Value.ToString() + "'");
                    sb.AppendLine("     , '" + dgv.Rows[i].Cells[3].Value.ToString() + "'");
                    sb.AppendLine("     , '" + dgv.Rows[i].Cells[4].Value.ToString() + "'");
                    sb.AppendLine("     , '" + dgv.Rows[i].Cells[5].Value.ToString() + "'");
                    sb.AppendLine("     , '" + dgv.Rows[i].Cells[6].Value.ToString() + "'");
                    sb.AppendLine("     , convert(varchar, getdate(), 120)  ");
                    sb.AppendLine(" ) ");


                }

                sCommand = new SqlCommand(sb.ToString());
                int qResult = wAdo.SqlCommandEtc(sCommand, "insert_CUST_TB");
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

        public DataTable fn_Collect_list(string condition)
        {
            wAdo = new wnAdo();

            sb = new StringBuilder();
            sb.AppendLine("select  ");
            sb.AppendLine("     A.SOO_DATE ");
            sb.AppendLine("     , A.SOO_CD ");
            sb.AppendLine("     , A.SOO_GUBUN ");
            sb.AppendLine("     , (SELECT S_CODE_NM FROM T_S_CODE WHERE L_CODE = '910' and S_CODE = A.SOO_GUBUN) AS SOO_GUBUN_NM ");
            sb.AppendLine("     , A.CUST_CD ");
            sb.AppendLine("     , (SELECT CUST_NM FROM N_CUST_CODE WHERE CUST_CD = A.CUST_CD) AS CUST_NM ");
            sb.AppendLine("     , A.SOO_MONEY ");
            sb.AppendLine("     , A.DC_MONEY  ");
            sb.AppendLine("     , A.TOTAL_MONEY ");
            sb.AppendLine("     , A.COMMENT  ");
            sb.AppendLine("     from  F_COLLECT A ");
            sb.AppendLine(condition);
            sb.AppendLine(" order by A.SOO_DATE, A.SOO_CD desc ");



            sCommand = new SqlCommand(sb.ToString());

            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);
        }

        public DataTable fn_Give_list(string condition)
        {
            wAdo = new wnAdo();

            sb = new StringBuilder();
            sb.AppendLine("select  ");
            sb.AppendLine("     A.GIVE_DATE ");
            sb.AppendLine("     , A.GIVE_CD ");
            sb.AppendLine("     , A.GIVE_GUBUN ");
            sb.AppendLine("     , (SELECT S_CODE_NM FROM T_S_CODE WHERE L_CODE = '910' and S_CODE = A.GIVE_GUBUN) AS GIVE_GUBUN_NM ");
            sb.AppendLine("     , A.CUST_CD ");
            sb.AppendLine("     , (SELECT CUST_NM FROM N_CUST_CODE WHERE CUST_CD = A.CUST_CD) AS CUST_NM ");
            sb.AppendLine("     , A.GIVE_MONEY ");
            sb.AppendLine("     , A.DC_MONEY  ");
            sb.AppendLine("     , A.TOTAL_MONEY ");
            sb.AppendLine("     , A.COMMENT  ");
            sb.AppendLine("     from  F_GIVE A ");
            sb.AppendLine(condition);
            sb.AppendLine(" order by A.GIVE_DATE, A.GIVE_CD desc ");



            sCommand = new SqlCommand(sb.ToString());

            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);
        }

        public int deleteSoo(string txt_sooDate, string txt_sooCd, string cust_cd, string totalMoneyTemp, string sooTemp, string dcTemp)
        {
            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                wnDm wDm = new wnDm();

                sb.AppendLine("delete from F_COLLECT ");
                sb.AppendLine("     where SOO_DATE = '" + txt_sooDate + "'");
                sb.AppendLine("     and SOO_CD = '" + txt_sooCd + "'");

                sb.AppendLine("update N_CUST_CODE set");
                sb.AppendLine("     BALANCE  =  (select  BALANCE ");
                sb.AppendLine("     from N_CUST_CODE ");
                sb.AppendLine("     where CUST_CD = '" + cust_cd + "' ) + " + double.Parse(totalMoneyTemp).ToString() + " ");
                sb.AppendLine("     where CUST_CD = '" + cust_cd + "' ");

                bool isCustDay = wDm.isCustDayTotal(txt_sooDate, cust_cd);

                if (!isCustDay)
                {
                    sb.AppendLine(wDm.Create_New_CustDayTotal(txt_sooDate, cust_cd));
                }

                sb.AppendLine(" UPDATE T_CUST_DAY_TOTAL SET ");
                sb.AppendLine(" COL_MONEY = COL_MONEY - " + sooTemp.Replace(",", "") + " ");
                sb.AppendLine(" ,COL_DC_MONEY = COL_DC_MONEY - " + dcTemp.Replace(",", "") + " ");
                sb.AppendLine(" ,COL_TOTAL_MONEY = COL_TOTAL_MONEY - " + totalMoneyTemp.Replace(",", "") + " ");
                sb.AppendLine(" WHERE INPUT_DATE ='" + txt_sooDate + "'  AND CUST_CD = '" + cust_cd + "'  ");


                sb.AppendLine(wDm.CustDayTotal_Change_Balance_Today(txt_sooDate, cust_cd));

                sb.AppendLine(wDm.CustDayTotal_Change_Balance(txt_sooDate, cust_cd, totalMoneyTemp.Replace(",", ""), "+"));

               

                sCommand = new SqlCommand(sb.ToString());

                int qResult = wAdo.SqlCommandEtc(sCommand, "delete_collect_TB");
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

        public int deleteGive(string txt_giveDate, string txt_giveCd, string cust_cd, string totalMoneyTemp, string giveTemp, string dcTemp)
        {
            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                wnDm wDm = new wnDm();

                sb.AppendLine("delete from F_GIVE ");
                sb.AppendLine("     where GIVE_DATE = '" + txt_giveDate + "'");
                sb.AppendLine("     and GIVE_CD = '" + txt_giveCd + "'");

                sb.AppendLine("update N_CUST_CODE set");
                sb.AppendLine("     BALANCE  =  (select  BALANCE ");
                sb.AppendLine("     from N_CUST_CODE ");
                sb.AppendLine("     where CUST_CD = '" + cust_cd + "' ) - " + double.Parse(totalMoneyTemp).ToString() + " ");
                sb.AppendLine("     where CUST_CD = '" + cust_cd + "' ");

                bool isCustDay = wDm.isCustDayTotal(txt_giveDate, cust_cd);

                if (!isCustDay)
                {
                    sb.AppendLine(wDm.Create_New_CustDayTotal(txt_giveDate, cust_cd));
                }

                sb.AppendLine(" UPDATE T_CUST_DAY_TOTAL SET ");
                sb.AppendLine(" PAY_MONEY = PAY_MONEY - " + giveTemp.Replace(",", "") + " ");
                sb.AppendLine(" ,PAY_DC_MONEY = PAY_DC_MONEY - " + dcTemp.Replace(",", "") + " ");
                sb.AppendLine(" ,PAY_TOTAL_MONEY = PAY_TOTAL_MONEY - " + totalMoneyTemp.Replace(",", "") + " ");
                sb.AppendLine(" WHERE INPUT_DATE ='" + txt_giveDate + "'  AND CUST_CD = '" + cust_cd + "'  ");


                sb.AppendLine(wDm.CustDayTotal_Change_Balance_Today(txt_giveDate, cust_cd));

                sb.AppendLine(wDm.CustDayTotal_Change_Balance(txt_giveDate, cust_cd, totalMoneyTemp.Replace(",", ""), "-"));



                sCommand = new SqlCommand(sb.ToString());

                int qResult = wAdo.SqlCommandEtc(sCommand, "delete_collect_TB");
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