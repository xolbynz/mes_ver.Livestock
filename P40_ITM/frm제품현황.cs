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

namespace 스마트팩토리.P40_ITM
{
    public partial class frm제품현황 : Form
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

        public frm제품현황()
        {
            InitializeComponent();
        }

        private void frm제품현황_Load(object sender, EventArgs e)
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
                dt = fn_제품현황_List(strDay1, strDay2);
                
                adoPrt = new DataTable();
                adoPrt = dt.Copy();

                this.itemOutGrid.RowCount = dt.Rows.Count;
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i]["no"] = (i + 1); //숫자의 경우  문자면 .string : 계산된 값을 READ 한 테이블로 다시 전달한다 - 출력물 사용

                        itemOutGrid.Rows[i].Cells["ITEM_CD"].Value = dt.Rows[i]["ITEM_CD"].ToString();
                        itemOutGrid.Rows[i].Cells["ITEM_NM"].Value = dt.Rows[i]["ITEM_NM"].ToString();
                        itemOutGrid.Rows[i].Cells["SPEC"].Value = dt.Rows[i]["SPEC"].ToString();
                        itemOutGrid.Rows[i].Cells["납기요청일"].Value = dt.Rows[i]["납기요청일"].ToString();
                        itemOutGrid.Rows[i].Cells["수주수량"].Value = (decimal.Parse(dt.Rows[i]["수주수량"].ToString())).ToString("#,0");
                        itemOutGrid.Rows[i].Cells["수주생산계획"].Value = (decimal.Parse(dt.Rows[i]["수주생산계획"].ToString())).ToString("#,0");
                        itemOutGrid.Rows[i].Cells["수주잔량"].Value = (decimal.Parse(dt.Rows[i]["수주잔량"].ToString())).ToString("#,0");
                        itemOutGrid.Rows[i].Cells["생산계획"].Value = (decimal.Parse(dt.Rows[i]["생산계획"].ToString())).ToString("#,0");
                        itemOutGrid.Rows[i].Cells["생산실적"].Value = (decimal.Parse(dt.Rows[i]["생산실적"].ToString())).ToString("#,0");
                        itemOutGrid.Rows[i].Cells["생산잔량"].Value = (decimal.Parse(dt.Rows[i]["생산잔량"].ToString())).ToString("#,0");
                        itemOutGrid.Rows[i].Cells["출하계획"].Value = (decimal.Parse(dt.Rows[i]["출하계획"].ToString())).ToString("#,0");
                        itemOutGrid.Rows[i].Cells["출하실적"].Value = (decimal.Parse(dt.Rows[i]["출하실적"].ToString())).ToString("#,0");
                        itemOutGrid.Rows[i].Cells["출하잔량"].Value = (decimal.Parse(dt.Rows[i]["출하잔량"].ToString())).ToString("#,0");
                        itemOutGrid.Rows[i].Cells["재고"].Value = (decimal.Parse(dt.Rows[i]["재고"].ToString())).ToString("#,0");

                        strDDate= dt.Rows[i]["납기요청일"].ToString();

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

        public DataTable fn_제품현황_List(string sDayFrom, string sDayTo)
        {
            StringBuilder sb = new StringBuilder();


            //=================== 제품별 집계 ======================
            sb.AppendLine(" SELECT '' AS NO, A.ITEM_CD, MAX(B.ITEM_NM) AS ITEM_NM, MAX(B.SPEC) AS SPEC, MIN(DELIVER_REQ_DATE) AS 납기요청일   ");
            sb.AppendLine(" , SUM(A.수주수량) AS 수주수량, SUM(A.수주생산계획) AS 수주생산계획, SUM(A.수주잔량) AS 수주잔량  ");
            sb.AppendLine(" , SUM(A.생산계획) AS 생산계획, SUM(A.생산실적) AS 생산실적, SUM(A.생산잔량) AS 생산잔량  ");
            sb.AppendLine(" , SUM(A.출하계획) AS 출하계획, SUM(A.출하실적) AS 출하실적, SUM(A.출하잔량) AS 출하잔량  ");
            sb.AppendLine(" , MAX(A.재고) AS 재고  ");
            sb.AppendLine(" FROM (  ");
            sb.AppendLine(" SELECT O.ITEM_CD, O.PLAN_DATE, L.DELIVER_REQ_DATE, TOTAL_AMT AS 수주수량, 0 AS 수주생산계획,0 AS 수주잔량  ");
            sb.AppendLine(" , 0 AS 생산계획,0 AS 생산실적,0 AS 생산잔량, 0 AS 출하계획,0 AS 출하실적,0 AS 출하잔량,  0 AS 재고    ");
            sb.AppendLine(" FROM F_PLAN_DETAIL AS O  LEFT OUTER JOIN F_PLAN AS L ON O.PLAN_DATE = L.PLAN_DATE AND O.PLAN_CD = L.PLAN_CD   ");
            sb.AppendLine(" WHERE O.PLAN_DATE BETWEEN @p_from AND @p_to  ");
            sb.AppendLine(" UNION ALL  ");
            sb.AppendLine(" SELECT ITEM_CD, W_INST_DATE, DELIVERY_DATE, 0,0,0, INST_AMT,0,0, 0,0,0,  0    ");
            sb.AppendLine(" FROM F_WORK_INST WHERE W_INST_DATE BETWEEN @p_from AND @p_to  ");
            sb.AppendLine(" UNION ALL  ");
            sb.AppendLine(" SELECT ITEM_CD, INPUT_DATE, INPUT_DATE, 0,0,0, 0,INPUT_AMT,0, 0,0,0,  0    ");
            sb.AppendLine(" FROM F_ITEM_INPUT WHERE INPUT_DATE BETWEEN @p_from AND @p_to  ");
            sb.AppendLine(" UNION ALL  ");
            sb.AppendLine(" SELECT ITEM_CD, OUTPUT_DATE, OUTPUT_DATE, 0,0,0, 0,0,0, OUTPUT_AMT,OUTPUT_AMT,0,  0    ");
            sb.AppendLine(" FROM F_ITEM_OUT_DETAIL WHERE OUTPUT_DATE BETWEEN @p_from AND @p_to  ");
            sb.AppendLine(" ) AS A LEFT OUTER JOIN N_ITEM_CODE AS B ON A.ITEM_CD = B.ITEM_CD   ");
            sb.AppendLine(" GROUP BY A.ITEM_CD  ");
            sb.AppendLine(" ORDER BY ITEM_NM ASC   ");


            ////=================== 제품별 건별 ===말이안됨(수주-생산-출하 링크가 없음)===================
            //sb.AppendLine(" SELECT '' AS NO, A.ITEM_CD, B.ITEM_NM, B.SPEC, DELIVER_REQ_DATE AS 납기요청일   ");
            //sb.AppendLine(" , 수주수량, 수주생산계획, 수주잔량  ");
            //sb.AppendLine(" , 생산계획, 생산실적,  생산잔량  ");
            //sb.AppendLine(" , 출하계획, 출하실적,  출하잔량  ");
            //sb.AppendLine(" , 재고  ");
            //sb.AppendLine(" FROM (  ");
            //sb.AppendLine(" SELECT O.ITEM_CD, O.PLAN_DATE, L.DELIVER_REQ_DATE, TOTAL_AMT AS 수주수량, 0 AS 수주생산계획,0 AS 수주잔량  ");
            //sb.AppendLine(" , 0 AS 생산계획,0 AS 생산실적,0 AS 생산잔량, 0 AS 출하계획,0 AS 출하실적,0 AS 출하잔량,  0 AS 재고    ");
            //sb.AppendLine(" FROM F_PLAN_DETAIL AS O  LEFT OUTER JOIN F_PLAN AS L ON O.PLAN_DATE = L.PLAN_DATE AND O.PLAN_CD = L.PLAN_CD   ");
            //sb.AppendLine(" WHERE O.PLAN_DATE BETWEEN @p_from AND @p_to  ");
            //sb.AppendLine(" UNION ALL  ");
            //sb.AppendLine(" SELECT ITEM_CD, W_INST_DATE, DELIVERY_DATE, 0,0,0, INST_AMT,0,0, 0,0,0,  0    ");
            //sb.AppendLine(" FROM F_WORK_INST WHERE W_INST_DATE BETWEEN @p_from AND @p_to  ");
            //sb.AppendLine(" UNION ALL  ");
            //sb.AppendLine(" SELECT ITEM_CD, INPUT_DATE, INPUT_DATE, 0,0,0, 0,INPUT_AMT,0, 0,0,0,  0    ");
            //sb.AppendLine(" FROM F_ITEM_INPUT WHERE INPUT_DATE BETWEEN @p_from AND @p_to  ");
            //sb.AppendLine(" UNION ALL  ");
            //sb.AppendLine(" SELECT ITEM_CD, OUTPUT_DATE, OUTPUT_DATE, 0,0,0, 0,0,0, OUTPUT_AMT,OUTPUT_AMT,0,  0    ");
            //sb.AppendLine(" FROM F_ITEM_OUT_DETAIL WHERE OUTPUT_DATE BETWEEN @p_from AND @p_to  ");
            //sb.AppendLine(" ) AS A LEFT OUTER JOIN N_ITEM_CODE AS B ON A.ITEM_CD = B.ITEM_CD   ");
            ////sb.AppendLine(" GROUP BY A.ITEM_CD  ");
            //sb.AppendLine(" ORDER BY ITEM_NM ASC   ");

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
    }
}
