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
    public partial class frm작업지시서_모니터링 : Form
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
        public int TimerCount;
        public int TimerFirstValue = 61;

        public frm작업지시서_모니터링()
        {
            InitializeComponent();
        }




        private void frm작업지시서_모니터링_Load(object sender, EventArgs e)
        {

            Lbl.Text = Common.p_strCompNm + "   생산 모니터링     " + DateTime.Now.ToString("yyyy.MM.dd    HH:mm:ss");
            start_date.Text = DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd");
            //start_date.Text = DateTime.Today.ToString("yyyy-MM-dd");
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            // 타이머당 이벤트 호출 시간 부여 1초 = 1000 
            TimerCount = TimerFirstValue;
            Timer2.Interval = 1000; //카운트다운을 출력하기 위한 타이머 = 1초
            Timer2.Enabled = true;

            refresh_list();
        }  


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void refresh_list()
        {

            TimerCount = TimerFirstValue;
            try
            {
                strDay1 = start_date.Text.ToString();
                strDay2 = end_date.Text.ToString();

                strDate = DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd");
                strDate = DateTime.Today.ToString("yyyy-MM-dd");

                DateTime T1 = DateTime.Parse(strDate);

                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = fn_작업지시서_모니터링_List(strDay1, strDay2);

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
                        //2019-11-12 이재원 생산일자와 생산수량 추가
                        //------------------------------------------
                        itemOutGrid.Rows[i].Cells["생산일자"].Value = dt.Rows[i]["INPUT_DATE"].ToString();
                        itemOutGrid.Rows[i].Cells["생산수량"].Value = (decimal.Parse(dt.Rows[i]["INPUT_AMT"].ToString())).ToString("#,#");
                        //------------------------------------------

                        itemOutGrid.Rows[i].Cells["불량수량"].Value = dt.Rows[i]["불량수량"].ToString();
                        itemOutGrid.Rows[i].Cells["불량일자"].Value = dt.Rows[i]["불량일자"].ToString();



                        
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
                MessageBox.Show("시스템 오류" + ex.ToString());
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
            }

            
        }

        private void btnSrch_Click(object sender, EventArgs e)
        {
            refresh_list();
            
        }

        public DataTable fn_작업지시서_모니터링_List(string sDayFrom, string sDayTo)
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
            //2019-11-12 이재원 작업지시서_모니터링에서 생산일자와 생산수량이 출력되지 않는 것 수정
            //------------------------------------------
            sb.AppendLine("     ,ISNULL(E.INPUT_AMT, 0) AS INPUT_AMT ");
            sb.AppendLine("     ,ISNULL(E.INPUT_DATE,'') AS INPUT_DATE ");
            //------------------------------------------
            sb.AppendLine("     ,ISNULL(C.COMPLETE_YN,'N') AS COMPLETE_YN ");
            sb.AppendLine("     ,ISNULL((SELECT MAX(F_SUB_DATE) FROM F_WORK_FLOW_DETAIL AS K WHERE K.LOT_NO = A.LOT_NO), '') AS 공정일자 ");
            sb.AppendLine("     ,ISNULL((SELECT MAX(F_SUB_AMT) FROM F_WORK_FLOW_DETAIL AS K WHERE K.LOT_NO = A.LOT_NO), 0) AS 공정수량 ");
            //2019-11-12 이재원 불량수량을 프로그램상에서 0인지 구분해서 공백으로 넣지 않고 받아올때부터 0이면 공백 숫자가 있으면 숫자를 찍도록 쿼리문 수정
            sb.AppendLine("     ,CASE WHEN (ISNULL((SELECT SUM(LOSS) FROM F_WORK_FLOW_DETAIL AS K WHERE K.LOT_NO = A.LOT_NO), 0) = 0) THEN '' ELSE (SELECT CONVERT(nvarchar,SUM(LOSS)) FROM F_WORK_FLOW_DETAIL AS K WHERE K.LOT_NO = A.LOT_NO) END AS 불량수량 ");
            //2019-11-12 이재원 불량일자를 어떤 것으로 표시해야하는지 조건이 없음 LOT_SUB별로 뿌리는 테이블이 아니기 때문
            //따라서 공정과정중 가장 최근에 불량이 발생한 일자를 뿌리도록 일단 구현하였음. 추후 수정해야할 듯
            sb.AppendLine("     ,ISNULL((SELECT TOP 1 (FLOW_DATE) FROM F_WORK_FLOW_DETAIL AS K WHERE K.LOT_NO = A.LOT_NO AND LOSS != 0 order by FLOW_DATE desc), '') AS 불량일자  ");
            sb.AppendLine(" FROM  F_WORK_INST A ");
            sb.AppendLine(" LEFT OUTER JOIN N_ITEM_CODE B ON A.ITEM_CD = B.ITEM_CD ");
            sb.AppendLine(" LEFT OUTER JOIN F_WORK_FLOW C ON A.LOT_NO = C.LOT_NO ");
            sb.AppendLine(" LEFT OUTER JOIN N_CUST_CODE D ON A.CUST_CD = D.CUST_CD ");
            sb.AppendLine(" LEFT OUTER JOIN F_ITEM_INPUT E ON A.LOT_NO = E.LOT_NO ");

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

            string sValue = "" + itemOutGrid.Rows[nCnt].Cells["ITEM_CD"].Value.ToString();
            string sValue2 = "" + itemOutGrid.Rows[nCnt].Cells["작업지시번호"].Value.ToString();

            return;

            //if (sValue2 == "") return;


        }

        private void frm작업지시서_모니터링_Resize(object sender, EventArgs e)
        {
            int iFormW = frm작업지시서_모니터링.ActiveForm.Size.Width;
            int iFormH = frm작업지시서_모니터링.ActiveForm.Size.Height ;

            itemOutGrid.Width = iFormW - 10;
            itemOutGrid.Height = iFormH - itemOutGrid.Location.Y - 100;

        }


        private void Timer2_Tick(object sender, EventArgs e)
        {
            Lbl.Text = Common.p_strCompNm + "   생산 모니터링     " + DateTime.Now.ToString("yyyy.MM.dd    HH:mm:ss");
            if (lbl_timer_count.Text.Equals("1"))
            {
                lbl_timer_count.ForeColor = Color.Black;
                refresh_list();
            }
            else if (TimerCount <= 11)
            {
                lbl_timer_count.ForeColor = Color.Red;
            }
            lbl_timer_count.Text = (--TimerCount).ToString();
            
        }

        
    }
}
