using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace 스마트팩토리.CLS
{
    public class wnLog
    {
        public const int LOG_ERROR = 100;
        public const int LOG_ERROR_FILE_UPLOAD = 101;
        public const int LOG_ERROR_FILE_DOWN = 102;
        public const int LOG_ANOTHER = 200;
        public const int LOG_QUERY = 300;
        public const int LOG_QUERY_RESULT = 301;

        public const string LOG_ERROR_STRING = "ERROR";
        public const string LOG_ERROR_FILE_UPLOAD_STRING = "ERROR_FUL";
        public const string LOG_ERROR_FILE_DOWN_STRING = "ERROR_FDL";
        public const string LOG_ANOTHER_STRING = "ETC";
        public const string LOG_QUERY_STRING = "QUERY";
        public const string LOG_QUERY_RESULT_STRING = "QUERY_RESULT";

        private const string logFileName = "program.log";
        private const string logDirectory = "Log";
        private const string dToken = "\\";

        public const string LOGSTRING_NO_QUERY = " There is no queryString";

        private static string convertLogTypeToString(int logType)
        {
            switch (logType)
            {
                case LOG_ERROR:
                    return LOG_ERROR_STRING;
                case LOG_ANOTHER:
                    return LOG_ANOTHER_STRING;
                case LOG_QUERY:
                    return LOG_QUERY_STRING;
                case LOG_QUERY_RESULT:
                    return LOG_QUERY_RESULT_STRING;
                default:
                    return "UNIDENTIFIED LOG";
            }
        }

        private static string getCurrentDirectory()
        {
            bool bDebug = wnGConstant.debug;

            if (bDebug) return "c:";
            else return Directory.GetCurrentDirectory();
        }

        private static string getFilePathAndName()
        {

            //return dToken + logDirectory + dToken + logFileName;
            return dToken + logDirectory + dToken + DateTime.Now.ToString("yyyy-MM-dd") + logFileName;
        }

        private static bool checkIsLogFileExit()
        {
            FileInfo _fInfo = new FileInfo(getCurrentDirectory() + getFilePathAndName());
            return _fInfo.Exists;
        }

        private static void createFilePath()
        {
            if (Directory.Exists(getCurrentDirectory() + dToken + logDirectory))
                return;
            else
                Directory.CreateDirectory(getCurrentDirectory() + dToken + logDirectory);
        }

        private static bool createLogFile()
        {
            try
            {
                createFilePath();
                string fullPath = getCurrentDirectory() + getFilePathAndName();
                using (StreamWriter sWriter = File.CreateText(fullPath))
                {
                    sWriter.WriteLine("START LOG : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    sWriter.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        private static string getDefaultLogLineText(int logType)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\r\n");
            sb.Append(convertLogTypeToString(logType));
            sb.Append(" [");
            sb.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sb.Append("] ");
            return sb.ToString();
        }

        public static void writeLog(int logType, System.Data.SqlClient.SqlParameterCollection pCollection)
        {
            bool bDebug = wnGConstant.debug;
            if (!bDebug) return;

            if (checkIsLogFileExit()) { }
            else
            {
                if (createLogFile()) { }
                else { return; }
            }

            string fullPath = getCurrentDirectory() + getFilePathAndName();
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < pCollection.Count; i++)
            {
                sb.Append(getDefaultLogLineText(logType));
                sb.Append(pCollection[i].Value.ToString());
            }

            using (StreamWriter sWriter = File.AppendText(fullPath))
            {
                sWriter.WriteLine(sb.ToString());
                sWriter.Close();
            }
        }

        public static void writeLog(int logType, System.Data.DataTable dt)
        {
            bool bDebug = wnGConstant.debug;
            if (!bDebug) return;

            if (checkIsLogFileExit()) { }
            else
            {
                if (createLogFile()) { }
                else { return; }
            }

            string fullPath = getCurrentDirectory() + getFilePathAndName();
            StringBuilder sb = new StringBuilder();

            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        sb.Append(getDefaultLogLineText(logType));
                        sb.Append(dt.Rows[i][c].ToString().Replace("\r\n", ""));
                    }
                    sb.Append(getDefaultLogLineText(logType));
                    sb.Append("=========================================================");
                }
            }
            else
            {
                sb.Append(getDefaultLogLineText(logType));
                sb.Append("0 row has selected");
            }

            using (StreamWriter sWriter = File.AppendText(fullPath))
            {
                sWriter.WriteLine(sb.ToString());
                sWriter.Close();
            }

        }

        public static void writeLog(int logType, string[] logText)
        {
            bool bDebug = wnGConstant.debug;
            if (!bDebug) return;


            if (checkIsLogFileExit()) { }
            else
            {
                if (createLogFile()) { }
                else { return; }
            }

            string fullPath = getCurrentDirectory() + getFilePathAndName();
            StringBuilder sb = new StringBuilder();

            foreach (string s in logText)
            {
                sb.Append(getDefaultLogLineText(logType));
                sb.Append(s);
            }

            using (StreamWriter sWriter = File.AppendText(fullPath))
            {
                sWriter.WriteLine(sb.ToString());
                sWriter.Close();
            }
        }

        public static void writeLog(int logType, string logText)
        {
            bool bDebug = wnGConstant.debug;
            if (!bDebug) return;

            if (checkIsLogFileExit()) { }
            else
            {
                if (createLogFile()) { }
                else { return; }
            }

            string fullPath = getCurrentDirectory() + getFilePathAndName();
            StringBuilder sb = new StringBuilder();

            string[] sToken = { "\r\n" };
            string[] sString = logText.Split(sToken, StringSplitOptions.None);

            foreach (string s in sString)
            {
                sb.Append(getDefaultLogLineText(logType));
                sb.Append(s);
            }

            using (StreamWriter sWriter = File.AppendText(fullPath))
            {
                sWriter.WriteLine(sb.ToString());
                sWriter.Close();
            }
        }

        public static void writeExLog(Exception ex)
        {
            if (checkIsLogFileExit()) { }
            else
            {
                if (createLogFile()) { }
                else { return; }
            }

            string fullPath = getCurrentDirectory() + getFilePathAndName();
            StringBuilder sb = new StringBuilder();

            string exString = ex.Message + " - " + ex.ToString();
            int logType = LOG_ERROR;

            string[] sToken = { "\r\n" };
            string[] sString = exString.Split(sToken, StringSplitOptions.None);

            foreach (string s in sString)
            {
                sb.Append(getDefaultLogLineText(logType));
                sb.Append(s);
            }

            using (StreamWriter sWriter = File.AppendText(fullPath))
            {
                sWriter.WriteLine(sb.ToString());
                sWriter.Close();
            }
        }

    }
}
