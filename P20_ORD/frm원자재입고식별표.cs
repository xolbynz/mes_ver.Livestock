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

namespace 스마트팩토리.P20_ORD
{
    public partial class frm원자재입고식별표 : Form
    {
        Popup.frmPrint readyPrt = new Popup.frmPrint();
        private wnGConstant wConst = new wnGConstant();
       
        DataTable adoPrt = null;
        wnAdo wAdo = new wnAdo();
        public Popup.frmPrint frmPrt;

        public string strCondition = "";

        public frm원자재입고식별표()
        {
            InitializeComponent();
        }

        private void frm원자재입고식별표_Load(object sender, EventArgs e)
        {
            start_date.Text = DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd");
            ComInfo.gridHeaderSet(inputRmGrid);
        }

        #region button logic

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSrch_Click(object sender, EventArgs e)
        {
            input_rm_logic();
        }

        #endregion button logic

        private void input_rm_logic()
        {
            string sDate, sNum, sSeq;
            string sLotno;
            wnDm wDm = new wnDm();
            DataTable dt = null;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("where 1=1 ");
            sb.AppendLine("and A.INPUT_DATE >= '" + start_date.Text.ToString() + "' and  A.INPUT_DATE <= '" + end_date.Text.ToString() + "'");
            sb.AppendLine("and ((A.CHECK_YN = 'Y' and C.PASS_YN = 'Y') or A.CHECK_YN = 'O')"); // 검사 승인 혹은 생략

            dt = wDm.fn_Input_Detail_List(sb.ToString());

            //inputRmGrid.Rows.Clear();
            this.inputRmGrid.DataSource = null;
            this.inputRmGrid.RowCount = 0;

            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //string t_amt = string.Format("{0:#.##}", 100.2);
                    inputRmGrid.Rows.Add();
                    inputRmGrid.Rows[i].Cells["INPUT_DATE"].Value = dt.Rows[i]["INPUT_DATE"].ToString();
                    inputRmGrid.Rows[i].Cells["INPUT_CD"].Value = dt.Rows[i]["INPUT_CD"].ToString();
                    //inputRmGrid.Rows[i].Cells["SEQ"].Value = dt.Rows[i]["SEQ"].ToString();
                    //inputRmGrid.Rows[i].Cells["ORDER_DATE"].Value = dt.Rows[i]["ORDER_DATE"].ToString();
                    //inputRmGrid.Rows[i].Cells["ORDER_CD"].Value = dt.Rows[i]["ORDER_CD"].ToString();
                    //inputRmGrid.Rows[i].Cells["ORDER_SEQ"].Value = dt.Rows[i]["ORDER_SEQ"].ToString();
                    inputRmGrid.Rows[i].Cells["RAW_MAT_NM"].Value = dt.Rows[i]["RAW_MAT_NM"].ToString();
                    inputRmGrid.Rows[i].Cells["RAW_MAT_CD"].Value = dt.Rows[i]["RAW_MAT_CD"].ToString();
                    inputRmGrid.Rows[i].Cells["SPEC"].Value = dt.Rows[i]["SPEC"].ToString();
                    inputRmGrid.Rows[i].Cells["UNIT_CD"].Value = dt.Rows[i]["UNIT_CD"].ToString();
                    inputRmGrid.Rows[i].Cells["UNIT_NM"].Value = dt.Rows[i]["UNIT_NM"].ToString();
                    inputRmGrid.Rows[i].Cells["TYPE_NM"].Value = dt.Rows[i]["TYPE_NM"].ToString();
                    inputRmGrid.Rows[i].Cells["CLASS_NM"].Value = dt.Rows[i]["CLASS_NM"].ToString();
                    inputRmGrid.Rows[i].Cells["GRADE_NM"].Value = dt.Rows[i]["GRADE_NM"].ToString();
                    inputRmGrid.Rows[i].Cells["COUNTRY_NM"].Value = dt.Rows[i]["COUNTRY_NM"].ToString();
                    inputRmGrid.Rows[i].Cells["FROZEN_NM"].Value = dt.Rows[i]["FROZEN_NM"].ToString();
                    inputRmGrid.Rows[i].Cells["CHUGJONG_NM"].Value = dt.Rows[i]["CHUGJONG_NM"].ToString();
                    inputRmGrid.Rows[i].Cells["SLAUHOUSE_NM"].Value = dt.Rows[i]["SLAUHOUSE_NM"].ToString();
                    inputRmGrid.Rows[i].Cells["RAW_MAT_GUBUN_NM"].Value = dt.Rows[i]["RAW_MAT_GUBUN_NM"].ToString();
                    inputRmGrid.Rows[i].Cells["RAW_MAT_GUBUN"].Value = dt.Rows[i]["RAW_MAT_GUBUN"].ToString();
                    inputRmGrid.Rows[i].Cells["HAMYANG"].Value = dt.Rows[i]["HAMYANG"].ToString();
                    inputRmGrid.Rows[i].Cells["EXPRT_DATE"].Value = dt.Rows[i]["EXPRT_DATE"].ToString();
                    inputRmGrid.Rows[i].Cells["LABEL_NM"].Value = dt.Rows[i]["LABEL_NM"].ToString();
                    //inputRmGrid.Rows[i].Cells["RAW_MAT_GUBUN"].Value = dt.Rows[i]["RAW_MAT_GUBUN"].ToString();
                    //inputRmGrid.Rows[i].Cells["RAW_MAT_GUBUN_NM"].Value = dt.Rows[i]["RAW_MAT_GUBUN_NM"].ToString();
                    inputRmGrid.Rows[i].Cells["TOTAL_AMT"].Value = (decimal.Parse(dt.Rows[i]["TOTAL_AMT"].ToString())).ToString("#,0.######");
                    //inputRmGrid.Rows[i].Cells["OLD_TOTAL_AMT"].Value = dt.Rows[i]["TOTAL_AMT"].ToString();
                    inputRmGrid.Rows[i].Cells["PRICE"].Value = (decimal.Parse(dt.Rows[i]["PRICE"].ToString())).ToString("#,0.######");
                    inputRmGrid.Rows[i].Cells["TOTAL_MONEY"].Value = (decimal.Parse(dt.Rows[i]["TOTAL_MONEY"].ToString())).ToString("#,0.######");

                    //inputRmGrid.Rows[i].Cells["HEAT_NO"].Value = dt.Rows[i]["HEAT_NO"].ToString();
                    //inputRmGrid.Rows[i].Cells["HEAT_TIME"].Value = dt.Rows[i]["HEAT_TIME"].ToString();
                    inputRmGrid.Rows[i].Cells["CHK"].Value = false;
                    sDate = DateTime.Parse("" + inputRmGrid.Rows[i].Cells["INPUT_DATE"].Value.ToString()).ToString("yyyyMMdd");
                    sNum = "" + dt.Rows[i]["번호"].ToString();
                    sSeq = "" + dt.Rows[i]["순번"].ToString();
                    sLotno = sDate + sNum + sSeq;

                    //inputRmGrid.Rows[i].Cells["INPUT_CD"].Value = sDate + sNum;
                    inputRmGrid.Rows[i].Cells["LOTNO"].Value = sLotno.ToString();
                    inputRmGrid.Rows[i].Cells["CUST_CD"].Value = dt.Rows[i]["CUST_CD"].ToString();
                    inputRmGrid.Rows[i].Cells["CUST_NM"].Value = dt.Rows[i]["CUST_NM"].ToString();
                }
            }
        }

        private void grdCellSetting()
        {
            ComInfo comInfo = new ComInfo();
            comInfo.grdCellSetting(inputRmGrid);
        }

        private void btn출력_Click(object sender, EventArgs e)
        {
            btn출력.Enabled = false;

            strCondition = "";

            if (inputRmGrid.Rows.Count == 0)
            {
                MessageBox.Show("출력할 자료가 없습니다.");
                btn출력.Enabled = true;
                return;
            }

            bindData();

            if (strCondition == "No")
            {
                MessageBox.Show("출력할 자료가 없습니다.");
                btn출력.Enabled = true;
                return;
            }

            if (strCondition != "ERROR")
            {
                strCondition = "원자재입고식별표";

                frmPrt = readyPrt;
                frmPrt.Show();
                frmPrt.BringToFront();
                frmPrt.prt_식별표(adoPrt, strCondition);
            }

            btn출력.Enabled = true;
        }

        public void bindData() //2019-12-30 문세진 출력을 위한 로직수정
        {
            Application.DoEvents();

            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = fn_원자재입고식별표_List();

                adoPrt = new DataTable();
                adoPrt = dt.Copy();

                int j = 0;
                int k = 0;

                for (int i = 0; i < this.inputRmGrid.Rows.Count; i++)
                {
                    if ((bool)inputRmGrid.Rows[i].Cells["CHK"].Value == true)  //--- 11= 확인 체크필드
                    {
                        k = 1;
                        string sDate = "" + this.inputRmGrid.Rows[i].Cells["INPUT_DATE"].Value.ToString();    //입고일자
                        string sNUm = "" + this.inputRmGrid.Rows[i].Cells["INPUT_CD"].Value.ToString();    //입고번호
                        //string sSeq = "" + this.inputRmGrid.Rows[i].Cells[2].Value.ToString();    //입고순번
                        string sName = "" + this.inputRmGrid.Rows[i].Cells["RAW_MAT_NM"].Value.ToString();    //원부재료명
                        string sSpec = "" + this.inputRmGrid.Rows[i].Cells["SPEC"].Value.ToString();    //규격
                        //string sODate = "" + this.inputRmGrid.Rows[i].Cells[1].Value.ToString();    //주문일자
                        //string sONUm = "" + this.inputRmGrid.Rows[i].Cells[2].Value.ToString();    //주문번호
                        //string sOSeq = "" + this.inputRmGrid.Rows[i].Cells[2].Value.ToString();    //주문순번
                        string sType = "" + this.inputRmGrid.Rows[i].Cells["TYPE_NM"].Value.ToString();    //유형
                        string sUnit = "" + this.inputRmGrid.Rows[i].Cells["UNIT_NM"].Value.ToString();    //단위
                        string sChugjong = "" + this.inputRmGrid.Rows[i].Cells["CHUGJONG_NM"].Value.ToString();    //축종
                        string sClass = "" + this.inputRmGrid.Rows[i].Cells["CLASS_NM"].Value.ToString();    //부위
                        string sGrade = "" + this.inputRmGrid.Rows[i].Cells["GRADE_NM"].Value.ToString();    //등급
                        string sCountry = "" + this.inputRmGrid.Rows[i].Cells["COUNTRY_NM"].Value.ToString();    //원산지
                        string sFrozen = "";
                        if (this.inputRmGrid.Rows[i].Cells["RAW_MAT_GUBUN"].Value.ToString().Equals("1"))
                        {
                            sFrozen = "-" + this.inputRmGrid.Rows[i].Cells["FROZEN_NM"].Value.ToString();    //냉동구분
                        }
                        string sExprtdate = "" + this.inputRmGrid.Rows[i].Cells["EXPRT_DATE"].Value.ToString();    //유통기한
                        string sSlauhouse = "" + this.inputRmGrid.Rows[i].Cells["SLAUHOUSE_NM"].Value.ToString();    //도축장
                        string nQty = "" + decimal.Parse(this.inputRmGrid.Rows[i].Cells["TOTAL_AMT"].Value.ToString().Trim()).ToString("#,0.######"); //수량
                        //string nCost = "" + this.inputRmGrid.Rows[i].Cells[6].Value.ToString().Trim(); //단가
                        //string nAmt = "" + this.inputRmGrid.Rows[i].Cells[7].Value.ToString().Trim(); //금액
                        string sHeat = "" + this.inputRmGrid.Rows[i].Cells[8].Value.ToString();    //heat no
                        //string sHeatTime = "" + this.inputRmGrid.Rows[i].Cells[6].Value.ToString();    //heat no
                        //string sCode = "" + this.inputRmGrid.Rows[i].Cells[9].Value.ToString();    //원부재료코드
                        //string sUcode = "" + this.inputRmGrid.Rows[i].Cells[10].Value.ToString();    //단위코드
                        //string sIsbn = "*" + this.inputRmGrid.Rows[i].Cells[5].Value.ToString() + "*";    //단위
                        string sLotno = "" + this.inputRmGrid.Rows[i].Cells["LOTNO"].Value.ToString();    //lot no
                        //string sCustcd = "" + this.inputRmGrid.Rows[i].Cells[13].Value.ToString();    //cust_cd
                        string sCustnm = "" + this.inputRmGrid.Rows[i].Cells["CUST_NM"].Value.ToString();    //cust_nm 
                        string sLabel = "" + this.inputRmGrid.Rows[i].Cells["LABEL_NM"].Value.ToString();


                        sNUm = sLotno.Substring(0, 12); //--- 입고번호 = 입고일자+번호(yyyyMMdd0000)

                        dt.Rows[j]["no"] = j;
                        dt.Rows[j]["입고일자"] = sDate;
                        dt.Rows[j]["입고번호"] = sNUm;
                        dt.Rows[j]["입고순번"] = "";
                        dt.Rows[j]["원부재료코드"] = 0;
                        dt.Rows[j]["원부재료명"] = sName;
                        //dt.Rows[j]["규격"] = sSpec;
                        dt.Rows[j]["유형"] = sType;
                        dt.Rows[j]["원산지"] = sCountry;
                        dt.Rows[j]["축종"] = sChugjong;
                        dt.Rows[j]["단위"] = sUnit;
                        dt.Rows[j]["등급"] = sGrade;
                        dt.Rows[j]["부위"] = sClass;
                        dt.Rows[j]["냉동구분"] = sFrozen;
                        dt.Rows[j]["도축장"] = sSlauhouse;
                        dt.Rows[j]["유통기한"] = sExprtdate;
                        dt.Rows[j]["HEAT_NO"] = "";
                        dt.Rows[j]["HEAT_TIME"] = "";
                        dt.Rows[j]["ORDER_DATE"] = "";
                        dt.Rows[j]["ORDER_CD"] = "";
                        dt.Rows[j]["ORDER_SEQ"] = "";
                        dt.Rows[j]["RAW_MAT_GUBUN"] = "";
                        dt.Rows[j]["S_CODE_NM"] = "";
                        dt.Rows[j]["단위코드"] = "";
                        dt.Rows[j]["단위명"] = sUnit;
                        dt.Rows[j]["수량"] = nQty;
                        dt.Rows[j]["단가"] = "";
                        dt.Rows[j]["금액"] = "";
                        dt.Rows[j]["제조번호"] = sLotno;
                        dt.Rows[j]["바코드제조번호"] = "*" + sLotno + "*";
                        dt.Rows[j]["제조번호"] = sLotno;
                        dt.Rows[j]["공급처"] = sCustnm;
                        dt.Rows[j]["라벨명"] = sLabel;

                        j = j + 1;

                        adoPrt = dt.Copy();
                    }
                }

                //데이타 끝나고 다시 copy를 써준 이유는 for중에 no에 값을 엏었기 때문에 출력물 데이타테이블(dt)를 다시 복사함
                adoPrt = dt.Copy();

                for (int i = j; i < this.inputRmGrid.Rows.Count; i++)
                {
                    adoPrt.Rows[i].Delete();
                }
                adoPrt.AcceptChanges(); //--삭제확정

                if (k == 0)
                {
                    strCondition = "No";
                }

            }
            catch (Exception ex)
            {
                strCondition = "ERROR";
                MessageBox.Show("검색중에 오류가 발생했습니다.");
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
            }

        }

        public DataTable fn_원자재입고식별표_List()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT A.INPUT_DATE, '' AS no, '' AS 입고일자, '' AS 입고번호, ''  AS 입고순번, '' AS 원부재료코드, '' 원부재료명, '' 축종, '' 단위, '' 등급, '' 부위, '' 유형, '' 냉동구분, '' 도축장, '' 유통기한, '' AS 원산지    ");
            sb.AppendLine(", '' AS HEAT_NO, '' AS HEAT_TIME, '' AS ORDER_DATE, '' AS ORDER_CD, '' AS ORDER_SEQ, '' AS RAW_MAT_GUBUN ");
            sb.AppendLine(", '' AS S_CODE_NM, '' AS 단위코드, '' AS 단위명  ");
            sb.AppendLine(", '' AS 수량, '' AS 단가, '' AS 금액, '' AS 제조번호, '' AS 바코드제조번호, '' AS 공급처, '' AS 라벨명 ");

            sb.AppendLine("  FROM F_RAW_DETAIL			AS A  ");
            sb.AppendLine(" WHERE A.INPUT_DATE >= '" + start_date.Text.ToString() + "' AND  A.INPUT_DATE <= '" + end_date.Text.ToString() + "'");
            sb.AppendLine(" AND (A.CHECK_YN = 'Y' OR A.CHECK_YN = 'O') ");

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
