using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace 스마트팩토리.CLS
{
    public class wnAdo
    {
        public DataTable SqlCommandSelect(SqlCommand sCommand)
        {
            SqlConnection Conn = new SqlConnection(Common.p_sConn);

            try
            {
                sCommand.Connection = Conn;
                Conn.Open();

                SqlDataAdapter dAdapter = new SqlDataAdapter();
                dAdapter.SelectCommand = sCommand;
                DataTable dTable = new DataTable();
                dAdapter.Fill(dTable);
                if (wnGConstant.debug) wnLog.writeLog(wnLog.LOG_QUERY, sCommand.CommandText);
                wnLog.writeLog(wnLog.LOG_QUERY_RESULT, dTable);
                return dTable;
            }
            catch (Exception ex)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
                return null;
            }
            finally
            {
                Conn.Close();
            }
        }

        public DataTable SqlCommandSelect_Jang(SqlCommand sCommand)
        {
            SqlConnection Conn = new SqlConnection(Common.p_sConn_jang);

            try
            {
                sCommand.Connection = Conn;
                Conn.Open();

                SqlDataAdapter dAdapter = new SqlDataAdapter();
                dAdapter.SelectCommand = sCommand;
                DataTable dTable = new DataTable();
                dAdapter.Fill(dTable);
                if (wnGConstant.debug) wnLog.writeLog(wnLog.LOG_QUERY, sCommand.CommandText);
                wnLog.writeLog(wnLog.LOG_QUERY_RESULT, dTable);
                return dTable;
            }
            catch (Exception ex)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
                return null;
            }
            finally
            {
                Conn.Close();
            }
        }


        public int SqlCommandEtc(SqlCommand sCommand, string sDesc)
        {
            int qResult = 0;

            qResult = SqlCommandRun(sCommand, sDesc);
            return qResult;
        }

        public int SqlCommandEtc_Jang(SqlCommand sCommand, string sDesc)
        {
            int qResult = 0;

            qResult = SqlCommandRun_Jang(sCommand, sDesc);
            return qResult;
        }

        public int SqlCommandRun(SqlCommand sCommand, string sDesc)
        {
            SqlConnection wnConnection = new SqlConnection(Common.p_sConn);
            SqlTransaction tran = null;

            try
            {
                sCommand.Connection = wnConnection;
                wnConnection.Open();

                // 트랜잭션 ###############################################################################################
                tran = wnConnection.BeginTransaction();
                sCommand.Transaction = tran;
                //sCommand.CommandType = CommandType.Text;
                if (wnGConstant.debug) wnLog.writeLog(wnLog.LOG_QUERY, sCommand.CommandText);
                int qResult = sCommand.ExecuteNonQuery();
                //sCommand.CommandText = null;
                
                if (qResult > 0)
                {
                    tran.Commit();
                }
                else
                {
                    tran.Rollback();
                }
                wnLog.writeLog(wnLog.LOG_QUERY_RESULT, sDesc + "-" + qResult);
                return qResult;
            }
            catch (Exception ex)
            {
                if (wnConnection != null)
                {
                    if (wnConnection.State == ConnectionState.Open)
                    {
                        if (tran != null)
                        {
                            tran.Rollback();
                        }
                        wnConnection.Close();
                    }
                }
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
                return -1;
            }
            finally
            {
                wnConnection.Close();
            }
        }

        public int SqlCommandRun_Jang(SqlCommand sCommand, string sDesc)
        {
            SqlConnection wnConnection = new SqlConnection(Common.p_sConn_jang);
            SqlTransaction tran = null;

            try
            {
                sCommand.Connection = wnConnection;
                wnConnection.Open();

                // 트랜잭션 ###############################################################################################
                tran = wnConnection.BeginTransaction();
                sCommand.Transaction = tran;
               // sCommand.CommandType = CommandType.Text;

                if (wnGConstant.debug) wnLog.writeLog(wnLog.LOG_QUERY, sCommand.CommandText);
                int qResult = sCommand.ExecuteNonQuery();

               // sCommand.CommandText = null;
                if (qResult > 0)
                {
                    tran.Commit();
                }
                else
                {
                    tran.Rollback();
                }
                wnLog.writeLog(wnLog.LOG_QUERY_RESULT, sDesc + "-" + qResult);
                return qResult;
            }
            catch (Exception ex)
            {
                if (wnConnection != null)
                {
                    if (wnConnection.State == ConnectionState.Open)
                    {
                        if (tran != null)
                        {
                            tran.Rollback();
                        }
                        wnConnection.Close();
                    }
                }
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
                return -1;
            }
            finally
            {
                wnConnection.Close();
            }
        }

        public int SqlCommandProd(SqlCommand sCommand, string sDesc)
        {
            SqlConnection wnConnection = new SqlConnection(Common.p_sConn);
            sCommand.Connection = wnConnection;
            sCommand.CommandType = CommandType.StoredProcedure;
            sCommand.CommandText = "dbo.SP_PLAN_WORK_YN";

            sCommand.Parameters.Add("@PLAN_NUM", SqlDbType.VarChar, 20);
            sCommand.Parameters.Add("@PLAN_SEQ", SqlDbType.Int);
            sCommand.Parameters.Add("@ITEM_CD", SqlDbType.VarChar, 20);


            return 0;
        }
    }
}
