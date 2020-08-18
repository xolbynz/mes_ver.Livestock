using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//------------------------------
using System.Data;
using System.Data.SqlClient;
using 스마트팩토리.Controls;

namespace 스마트팩토리.CLS
{
    class wnDm3
    {
        wnAdo wAdo = new wnAdo();

        //--- SQL 자료 읽기
        public DataTable fn_SQLServer_Read(string sQuery)
        {
            StringBuilder sb = new StringBuilder();

            SqlCommand sCommand = new SqlCommand(sQuery);
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            
            return wAdo.SqlCommandSelect(sCommand);
        }
        //----------------------------------------------------------------------------------------------------------------    '// 19     
        public DataTable fn_F_WORK_INST_Print(string sDay, string sNum)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(" SELECT '' AS SEQ, A.W_INST_DATE, A.W_INST_CD, A.LOT_NO, A.ITEM_CD, A.CUST_CD, A.INST_NOTICE, A.INST_AMT, A.DELIVERY_DATE, A.PLAN_NUM  ");
            sb.AppendLine(" , C.FLOW_CD, D.ITEM_NM , D.SPEC , E.FLOW_NM   ");
            sb.AppendLine(" FROM F_WORK_INST AS A  ");
            //sb.AppendLine(" LEFT OUTER JOIN F_WORK_INST_DETAIL AS B ON A.W_INST_DATE = B.W_INST_DATE AND A.W_INST_CD = B.W_INST_CD  ");
            sb.AppendLine(" LEFT OUTER JOIN N_ITEM_FLOW AS C ON A.ITEM_CD = C.ITEM_CD ");
            sb.AppendLine(" LEFT OUTER JOIN N_ITEM_CODE AS D ON A.ITEM_CD = D.ITEM_CD ");
            sb.AppendLine(" LEFT OUTER JOIN N_FLOW_CODE AS E ON C.FLOW_CD = E.FLOW_CD ");
            sb.AppendLine(" WHERE A.W_INST_DATE = @p_1 AND A.W_INST_CD = @p_2 ");
            sb.AppendLine(" ORDER BY C.SEQ ASC  ");

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            sCommand.Parameters.AddWithValue("@p_1", sDay);
            sCommand.Parameters.AddWithValue("@p_2", sNum);

            return wAdo.SqlCommandSelect(sCommand);
        }

        //----------------------------------------------------------------------------------------------------------------    '// 19     
        public DataTable fn_F_WORK_INST_DETAIL_Print(string sDay, string sNum)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(" SELECT '' AS SEQ, A.W_INST_DATE, A.W_INST_CD, A.LOT_NO, A.ITEM_CD, A.CUST_CD, A.INST_NOTICE, A.INST_AMT, A.DELIVERY_DATE, A.PLAN_NUM  ");
            sb.AppendLine(" , B.SEQ, B.RAW_MAT_CD, B.SOYO_AMT, B.TOTAL_AMT, D.ITEM_NM , D.SPEC , E.RAW_MAT_NM, F.UNIT_NM  ");
            sb.AppendLine(" FROM F_WORK_INST AS A  ");
            sb.AppendLine(" LEFT OUTER JOIN F_WORK_INST_DETAIL AS B ON A.W_INST_DATE = B.W_INST_DATE AND A.W_INST_CD = B.W_INST_CD  ");
            sb.AppendLine(" LEFT OUTER JOIN N_ITEM_CODE AS D ON A.ITEM_CD = D.ITEM_CD ");
            sb.AppendLine(" LEFT OUTER JOIN N_RAW_CODE AS E ON B.RAW_MAT_CD = E.RAW_MAT_CD ");
            sb.AppendLine(" LEFT OUTER JOIN N_UNIT_CODE AS F ON E.OUTPUT_UNIT = F.UNIT_CD ");
            sb.AppendLine(" WHERE A.W_INST_DATE = @p_1 AND A.W_INST_CD = @p_2 ");
            sb.AppendLine(" ORDER BY B.SEQ  ");

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            sCommand.Parameters.AddWithValue("@p_1", sDay);
            sCommand.Parameters.AddWithValue("@p_2", sNum);

            return wAdo.SqlCommandSelect(sCommand);
        } 

        public DataTable fn_Permission_Check(string sProgram)
        {
            string sPG_Name = sProgram.Trim();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" SELECT ");  
            sb.AppendLine("  A.USER_CODE, A.MAIN_MENU, A.PROGRAM, A.PROGRAM_NAME, A.ALL_PERMISSION ");
            sb.AppendLine("  FROM TB_MENU_PERMISSION AS A ");
            sb.AppendLine(" WHERE  ");
            sb.AppendLine("       A.USER_CODE = '" + Common.p_strUserID + "' ");
            //sb.AppendLine("   AND A.MAIN_MENU = '" + sPG_Name + "' ");
            sb.AppendLine("   AND A.PROGRAM = '" + sPG_Name + "' ");

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
