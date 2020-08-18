using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 스마트팩토리.CLS
{
    class wnDmProc
    {
        wnAdo wAdo = new wnAdo();

        public int sp_plan_work_yn(string plan_num, string plan_item)
        {
            SqlConnection wnConnection = new SqlConnection(Common.p_sConn);

            try
            {
                wnConnection.Open();
                StringBuilder sb = new StringBuilder();

                SqlCommand sCommand = new SqlCommand(sb.ToString());
                sCommand.Connection = wnConnection;
                sCommand.CommandType = CommandType.StoredProcedure;
                sCommand.CommandText = "dbo.SP_PLAN_WORK_YN";

                sCommand.Parameters.Add("@PLAN_NUM", SqlDbType.VarChar, 20);
                sCommand.Parameters.Add("@ITEM_CD", SqlDbType.VarChar, 20);
                //sCommand.Parameters.Add("@PLAN_SEQ", SqlDbType.Int);

                sCommand.Parameters["@PLAN_NUM"].Value = plan_num;
                sCommand.Parameters["@ITEM_CD"].Value = plan_item;

                int result_value = 0;
                SqlDataReader reader = sCommand.ExecuteReader();
                if (reader.Read())
                    result_value = reader.GetInt32(0);

                reader.Close();
                return result_value;
            }
            catch (Exception e) 
            {
                System.Console.WriteLine(e.Message);
                return 4;
            }
            finally
            {
                wnConnection.Close();
            }
        }

        public int sp_raw_out(string lot_no, string raw_mat_cd, double out_amt)
        {
            SqlConnection wnConnection = new SqlConnection(Common.p_sConn);

            try
            {
                wnConnection.Open();
                StringBuilder sb = new StringBuilder();

                SqlCommand sCommand = new SqlCommand(sb.ToString());
                sCommand.Connection = wnConnection;
                sCommand.CommandType = CommandType.StoredProcedure;
                sCommand.CommandText = "dbo.SP_RAW_OUT";

                sCommand.Parameters.Add("@LOT_NO", SqlDbType.VarChar, 20);
                sCommand.Parameters.Add("@RAW_MAT_CD", SqlDbType.VarChar, 20);
                sCommand.Parameters.Add("@OUT_AMT", SqlDbType.Decimal);

                sCommand.Parameters["@LOT_NO"].Value = lot_no;
                sCommand.Parameters["@RAW_MAT_CD"].Value = raw_mat_cd;
                sCommand.Parameters["@OUT_AMT"].Value = out_amt;

                int result_value = 0;
                SqlDataReader reader = sCommand.ExecuteReader();
                if (reader.Read())
                    result_value = reader.GetInt32(0);

                reader.Close();
                return result_value;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return 4;
            }
            finally
            {
                wnConnection.Close();
            }
        }

        public int sp_half_out(string lot_no, string half_item_cd, double out_amt)
        {
            SqlConnection wnConnection = new SqlConnection(Common.p_sConn);

            try
            {
                wnConnection.Open();
                StringBuilder sb = new StringBuilder();

                SqlCommand sCommand = new SqlCommand(sb.ToString());
                sCommand.Connection = wnConnection;
                sCommand.CommandType = CommandType.StoredProcedure;
                sCommand.CommandText = "dbo.SP_RAW_OUT";

                sCommand.Parameters.Add("@LOT_NO", SqlDbType.VarChar, 20);
                sCommand.Parameters.Add("@HALF_ITEM_CD", SqlDbType.VarChar, 20);
                sCommand.Parameters.Add("@OUT_AMT", SqlDbType.Decimal);

                sCommand.Parameters["@LOT_NO"].Value = lot_no;
                sCommand.Parameters["@HALF_ITEM_CD"].Value = half_item_cd;
                sCommand.Parameters["@OUT_AMT"].Value = out_amt;

                int result_value = 0;
                SqlDataReader reader = sCommand.ExecuteReader();
                if (reader.Read())
                    result_value = reader.GetInt32(0);

                reader.Close();
                return result_value;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return 4;
            }
            finally
            {
                wnConnection.Close();
            }
        }

        public int sp_item_stock_upd(string item_cd)
        {
            SqlConnection wnConnection = new SqlConnection(Common.p_sConn);

            try
            {
                wnConnection.Open();
                StringBuilder sb = new StringBuilder();

                SqlCommand sCommand = new SqlCommand(sb.ToString());
                sCommand.Connection = wnConnection;
                sCommand.CommandType = CommandType.StoredProcedure;
                sCommand.CommandText = "dbo.SP_ITEM_STOCK_UPDATE";

                sCommand.Parameters.Add("@ITEM_CD", SqlDbType.VarChar, 20);

                sCommand.Parameters["@ITEM_CD"].Value = item_cd;

                int result_value = 0;
                SqlDataReader reader = sCommand.ExecuteReader();
                if (reader.Read())
                    result_value = reader.GetInt32(0);

                reader.Close();
                return result_value;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return 4;
            }
            finally
            {
                wnConnection.Close();
            }
        }

        public int sp_plan_group(string plan_date, string plan_cd, string staffCd)
        {
            SqlConnection wnConnection = new SqlConnection(Common.p_sConn);

            try
            {
                wnConnection.Open();
                StringBuilder sb = new StringBuilder();

                SqlCommand sCommand = new SqlCommand(sb.ToString());
                sCommand.Connection = wnConnection;
                sCommand.CommandType = CommandType.StoredProcedure;
                sCommand.CommandText = "dbo.SP_PLAN_GROUP";

                sCommand.Parameters.Add("@PLAN_DATE", SqlDbType.VarChar, 20);
                sCommand.Parameters.Add("@PLAN_CD", SqlDbType.Int);
                sCommand.Parameters.Add("@STAFFCD", SqlDbType.VarChar, 20);

                sCommand.Parameters["@PLAN_DATE"].Value = plan_date;
                sCommand.Parameters["@PLAN_CD"].Value = plan_cd;
                sCommand.Parameters["@STAFFCD"].Value = staffCd;

                int result_value = 0;
                SqlDataReader reader = sCommand.ExecuteReader();
                if (reader.Read())
                    result_value = reader.GetInt32(0);

                reader.Close();
                return result_value;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return 4;
            }
            finally
            {
                wnConnection.Close();
            }
        }
    }
}
