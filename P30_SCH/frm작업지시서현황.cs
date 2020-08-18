using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 스마트팩토리.CLS;

namespace 스마트팩토리.P30_SCH
{
    public partial class frm작업지시서현황 : Form
    {
        Popup.frmPrint readyPrt = new Popup.frmPrint();
        private wnGConstant wConst = new wnGConstant();

        DataTable adoPrt = null;
        wnAdo wAdo = new wnAdo();
        public Popup.frmPrint frmPrt;

        public string strDay1 = "";
        public string strDay2 = "";
        public string strDate = "";
        public string strDDate = "";
        public string strDay = "";
        public string strCondition = "";

        public frm작업지시서현황()
        {
            InitializeComponent();
        }

        private void frm작업지시서현황_Load(object sender, EventArgs e)
        {
            start_date.Text = DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd");
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSrch_Click(object sender, EventArgs e)
        {
            try
            {
                strDay1 = start_date.Text.ToString();
                strDay2 = end_date.Text.ToString();

                strDate = DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd");
                strDate = DateTime.Today.ToString("yyyy-MM-dd");

                DateTime T1 = DateTime.Parse(strDate);

                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = fn_작업지시서현황_List(strDay1, strDay2);
                
                adoPrt = new DataTable();
                adoPrt = dt.Copy();

                this.itemOutGrid.RowCount = dt.Rows.Count;
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i]["no"] = (i + 1); //숫자의 경우  문자면 .string : 계산된 값을 READ 한 테이블로 다시 전달한다 - 출력물 사용

                        itemOutGrid.Rows[i].Cells["작업지시번호"].Value = dt.Rows[i]["LOT_NO"].ToString();
                        itemOutGrid.Rows[i].Cells["ITEM_CD"].Value = dt.Rows[i]["ITEM_CD"].ToString();
                        itemOutGrid.Rows[i].Cells["ITEM_NM"].Value = dt.Rows[i]["ITEM_NM"].ToString();
                        itemOutGrid.Rows[i].Cells["SPEC"].Value = dt.Rows[i]["SPEC"].ToString();
                        itemOutGrid.Rows[i].Cells["고객사"].Value = dt.Rows[i]["CUST_NM"].ToString();
                        itemOutGrid.Rows[i].Cells["납기요청일"].Value = dt.Rows[i]["DELIVERY_DATE"].ToString();

                        itemOutGrid.Rows[i].Cells["지시일자"].Value = dt.Rows[i]["W_INST_DATE"].ToString();
                        itemOutGrid.Rows[i].Cells["지시수량"].Value = (decimal.Parse(dt.Rows[i]["INST_AMT"].ToString())).ToString("#,0");

                        itemOutGrid.Rows[i].Cells["공정일자"].Value = dt.Rows[i]["공정일자"].ToString();
                        itemOutGrid.Rows[i].Cells["공정수량"].Value = (decimal.Parse(dt.Rows[i]["공정수량"].ToString())).ToString("#,#");

                        int intQty = (int.Parse(dt.Rows[i]["불량수량"].ToString()));
                        if (intQty == 0)
                        {
                            itemOutGrid.Rows[i].Cells["불량일자"].Value = "";
                            itemOutGrid.Rows[i].Cells["불량수량"].Value = "";
                        }
                        else
                        {
                            itemOutGrid.Rows[i].Cells["불량일자"].Value = dt.Rows[i]["공정일자"].ToString();    //==============
                            itemOutGrid.Rows[i].Cells["불량수량"].Value = (decimal.Parse(dt.Rows[i]["불량수량"].ToString())).ToString("#,0");
                        }
                        //itemOutGrid.Rows[i].Cells["불량일자"].Value = dt.Rows[i]["공정일자"].ToString();    //==============
                        //itemOutGrid.Rows[i].Cells["불량수량"].Value = (decimal.Parse(dt.Rows[i]["불량수량"].ToString())).ToString("#,#");

                        strDDate = dt.Rows[i]["DELIVERY_DATE"].ToString();

                        DateTime T2 = DateTime.Parse(strDDate);

                        TimeSpan TS = T2 - T1;
                        int diffDay = TS.Days;

                        if (diffDay > 2)
                        {
                            itemOutGrid.Rows[i].Cells["ITEM_CD"].Style.BackColor = Color.Green;
                            itemOutGrid.Rows[i].Cells["ITEM_NM"].Style.BackColor = Color.Green;
                        }
                        else if (diffDay > 0)
                        {
                            itemOutGrid.Rows[i].Cells["ITEM_CD"].Style.BackColor = Color.Yellow;
                            itemOutGrid.Rows[i].Cells["ITEM_NM"].Style.BackColor = Color.Yellow;
                        }
                        else
                        {
                            itemOutGrid.Rows[i].Cells["ITEM_CD"].Style.BackColor = Color.Red;
                            itemOutGrid.Rows[i].Cells["ITEM_NM"].Style.BackColor = Color.Red;
                        }



                    }

                    //데이타 끝나고 다시 copy를 써준 이유는 for중에 no에 값을 엏었기 때문에 출력물 데이타테이블(dt)를 다시 복사함
                    adoPrt = dt.Copy();
                }
                else
                {
                    itemOutGrid.Rows.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("시스템 오류" + e.ToString());
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
            }
        }

        public DataTable fn_작업지시서현황_List(string sDayFrom, string sDayTo)
        {
            StringBuilder sb = new StringBuilder();


            //=================== 작업지시서현황 ======================

            sb.AppendLine("select '' AS no, A.W_INST_DATE");
            sb.AppendLine("     ,A.W_INST_CD ");
            sb.AppendLine("     ,A.LOT_NO ");
            sb.AppendLine("     ,A.ITEM_CD ");
            sb.AppendLine("     ,B.ITEM_NM ");
            sb.AppendLine("     ,B.ITEM_GUBUN ");
            sb.AppendLine("     ,B.SPEC ");
            sb.AppendLine("     ,A.CUST_CD ");
            sb.AppendLine("     ,D.CUST_NM ");
            sb.AppendLine("     ,A.DELIVERY_DATE ");
            sb.AppendLine("     ,A.INST_AMT");
            sb.AppendLine("     ,A.CHARGE_AMT ");
            sb.AppendLine("     ,A.PACK_AMT ");
            sb.AppendLine("     ,A.PLAN_NUM");
            sb.AppendLine("     ,A.PLAN_ITEM");
            sb.AppendLine("     ,A.INSTAFF ");
            sb.AppendLine("     ,A.INST_NOTICE ");
            sb.AppendLine("     ,ISNULL(C.COMPLETE_YN,'N') AS COMPLETE_YN ");
            sb.AppendLine("     ,ISNULL((SELECT MAX(F_SUB_DATE) FROM F_WORK_FLOW_DETAIL AS K WHERE K.LOT_NO = A.LOT_NO), '') AS 공정일자 ");
            sb.AppendLine("     ,ISNULL((SELECT MAX(F_SUB_AMT) FROM F_WORK_FLOW_DETAIL AS K WHERE K.LOT_NO = A.LOT_NO), 0) AS 공정수량 ");
            sb.AppendLine("     ,ISNULL((SELECT SUM(LOSS) FROM F_WORK_FLOW_DETAIL AS K WHERE K.LOT_NO = A.LOT_NO), 0) AS 불량수량 ");
            sb.AppendLine(" FROM  F_WORK_INST A ");
            sb.AppendLine(" LEFT OUTER JOIN N_ITEM_CODE B ON A.ITEM_CD = B.ITEM_CD ");
            sb.AppendLine(" LEFT OUTER JOIN F_WORK_FLOW C ON A.LOT_NO = C.LOT_NO ");
            sb.AppendLine(" LEFT OUTER JOIN N_CUST_CODE D ON A.CUST_CD = D.CUST_CD ");

            sb.AppendLine(" WHERE A.W_INST_DATE BETWEEN @p_from AND @p_to  ");

            sb.AppendLine(" order by A.W_INST_DATE desc, A.W_INST_CD desc ");

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }

            sCommand.Parameters.AddWithValue("@p_from", sDayFrom);
            sCommand.Parameters.AddWithValue("@p_to", sDayTo);
            return wAdo.SqlCommandSelect(sCommand);
        }

        private void itemOutGrid_DoubleClick(object sender, EventArgs e)
        {
            if (itemOutGrid.CurrentCell == null) return;
            if (itemOutGrid.CurrentCell.RowIndex < 0) return;
            if (itemOutGrid.CurrentCell.ColumnIndex < 0) return;

            int nCnt = itemOutGrid.CurrentCell.RowIndex;

            // int nKeyCol = 2;
            // int nKeyCol2 = 3;
            // string sValue = "" + GridRecord.Rows[nCnt].Cells[nKeyCol].Value.ToString();
            // string sValue2 = "" + GridRecord.Rows[nCnt].Cells[nKeyCol2].Value.ToString();
            string sValue = "" + itemOutGrid.Rows[nCnt].Cells["ITEM_CD"].Value.ToString();
            string sValue2 = "" + itemOutGrid.Rows[nCnt].Cells["작업지시번호"].Value.ToString();


//========================ㅂㅂㅂㅂㅈㅈㅈㄷㄷㄷQQQQWWWEEE
            if (sValue2 == "") return;

            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = fn_작업지시소요량현황_List(sValue, sValue2);
                
                adoPrt = new DataTable();
                adoPrt = dt.Copy();

                this.rawGrid.RowCount = dt.Rows.Count;
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //dt.Rows[i]["no"] = (i + 1); //숫자의 경우  문자면 .string : 계산된 값을 READ 한 테이블로 다시 전달한다 - 출력물 사용

                        rawGrid.Rows[i].Cells["no"].Value = dt.Rows[i]["no"].ToString();
                        rawGrid.Rows[i].Cells["원자재코드"].Value = dt.Rows[i]["원부재료코드"].ToString();
                        rawGrid.Rows[i].Cells["원자재명"].Value = dt.Rows[i]["원부재료명"].ToString();
                        rawGrid.Rows[i].Cells["규격"].Value = dt.Rows[i]["규격"].ToString();
                        rawGrid.Rows[i].Cells["LOT_SUB"].Value = dt.Rows[i]["LOT_SUB"].ToString();

                        rawGrid.Rows[i].Cells["입고일자"].Value = dt.Rows[i]["INPUT_DATE"].ToString();

                        rawGrid.Rows[i].Cells["구매처"].Value = "";

                        decimal decQty = (decimal.Parse(dt.Rows[i]["투입량"].ToString()));
                        if (decQty == 0)
                        {
                            rawGrid.Rows[i].Cells["투입량"].Value = "";
                        }
                        else
                        {
                            rawGrid.Rows[i].Cells["투입량"].Value = (decimal.Parse(dt.Rows[i]["투입량"].ToString())).ToString("#,0");
                        }

                        //DateTime T2 = DateTime.Parse(strDDate);

                        //TimeSpan TS = T2 - T1;
                        //int diffDay = TS.Days;

                        //if (diffDay > 2)
                        //{
                        //    rawGrid.Rows[i].Cells["ITEM_CD"].Style.BackColor = Color.Green;
                        //    rawGrid.Rows[i].Cells["ITEM_NM"].Style.BackColor = Color.Green;
                        //}
                        //else if (diffDay > 0)
                        //{
                        //    rawGrid.Rows[i].Cells["ITEM_CD"].Style.BackColor = Color.Yellow;
                        //    rawGrid.Rows[i].Cells["ITEM_NM"].Style.BackColor = Color.Yellow;
                        //}
                        //else
                        //{
                        //    rawGrid.Rows[i].Cells["ITEM_CD"].Style.BackColor = Color.Red;
                        //    rawGrid.Rows[i].Cells["ITEM_NM"].Style.BackColor = Color.Red;
                        //}



                    }

                    //데이타 끝나고 다시 copy를 써준 이유는 for중에 no에 값을 엏었기 때문에 출력물 데이타테이블(dt)를 다시 복사함
                    adoPrt = dt.Copy();
                }
                else
                {
                    rawGrid.Rows.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("시스템 오류" + e.ToString());
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
            }

        }

        public DataTable fn_작업지시소요량현황_List(string sValue, string sValue2)
        {
            StringBuilder sb = new StringBuilder();


            //=================== 작업지시 : 원자재 소요량 현황 ======================

            sb.AppendLine("SELECT OUTPUT_CD AS no, A.OUTPUT_DATE AS 출고일자");
            sb.AppendLine("     ,A.RAW_MAT_CD AS 원부재료코드 ");
            sb.AppendLine("     ,A.LOT_NO AS 제조번호 ");
            sb.AppendLine("     ,B.RAW_MAT_NM AS 원부재료명 ");
            sb.AppendLine("     ,B.SPEC AS  규격 ");
            sb.AppendLine("     ,ISNULL(A.TOTAL_AMT,0) AS 투입량 ");
            sb.AppendLine("     ,A.LOT_NO ");
            sb.AppendLine("     ,A.LOT_SUB ");
            sb.AppendLine("     ,A.COMMENT ");
            sb.AppendLine("     ,A.INPUT_DATE ");
            sb.AppendLine("     ,A.INPUT_CD ");
            sb.AppendLine("     ,A.INPUT_SEQ ");
            sb.AppendLine("     ,A.OUTPUT_AMT ");
            sb.AppendLine(" FROM  F_RAW_OUTPUT A ");
            sb.AppendLine(" LEFT OUTER JOIN  N_RAW_CODE B ON A.RAW_MAT_CD = B.RAW_MAT_CD ");

            sb.AppendLine(" WHERE A.LOT_NO = @value2  ");

            sb.AppendLine(" ORDER BY A.OUTPUT_DATE ASC, A.OUTPUT_CD ASC ");

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }

            sCommand.Parameters.AddWithValue("@value2", sValue2);
            return wAdo.SqlCommandSelect(sCommand);
        }
    }
}
