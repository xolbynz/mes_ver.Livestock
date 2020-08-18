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
    public partial class frm제품입고식별표 : Form
    {
        Popup.frmPrint readyPrt = new Popup.frmPrint();
        private wnGConstant wConst = new wnGConstant();

        DataTable adoPrt = null;
        wnAdo wAdo = new wnAdo();
        public Popup.frmPrint frmPrt;

        public string strCondition = "";

        public frm제품입고식별표()
        {
            InitializeComponent();
        }

        private void frm제품입고식별표_Load(object sender, EventArgs e)
        {
            start_date.Text = DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd");
            ComInfo.gridHeaderSet(InputTabGrid);
            //gridSetting();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            
            wnDm wDm = new wnDm();
            DataTable dt = new DataTable();

            StringBuilder sb = new StringBuilder();
            //sb.AppendLine(" and B.F_SUB_DATE >= '" + start_date.Text.ToString() + "' and  B.F_SUB_DATE <= '" + end_date.Text.ToString() + "' ");
            sb.AppendLine(" ");
            sb.AppendLine(" ");

            dt = wDm.fn_Item_Input_List_CZM2(sb.ToString());

            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    InputTabGrid.DataSource = dt;

            //inputRmGrid.Rows.Clear();
            this.InputTabGrid.DataSource = null;
            this.InputTabGrid.RowCount = 0;

            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)


                //for (int i = 0; i < InputTabGrid.Rows.Count; i++) 
                {
                    //string t_amt = string.Format("{0:#.##}", 100.2);
                    InputTabGrid.Rows.Add();
                    //InputTabGrid.Rows[i].Cells["INPUT_DATE"].Value = dt.Rows[i]["INPUT_DATE"].ToString();
                    InputTabGrid.Rows[i].Cells["LOT_NO"].Value = dt.Rows[i]["LOT_NO"].ToString();
                    //InputTabGrid.Rows[i].Cells["LOT_SUB"].Value = "0";
                    //inputRmGrid.Rows[i].Cells["SEQ"].Value = dt.Rows[i]["SEQ"].ToString();
                    //inputRmGrid.Rows[i].Cells["ORDER_DATE"].Value = dt.Rows[i]["ORDER_DATE"].ToString();
                    //inputRmGrid.Rows[i].Cells["ORDER_CD"].Value = dt.Rows[i]["ORDER_CD"].ToString();
                    //inputRmGrid.Rows[i].Cells["ORDER_SEQ"].Value = dt.Rows[i]["ORDER_SEQ"].ToString();
                    InputTabGrid.Rows[i].Cells["LOT_식별표"].Value = dt.Rows[i]["LOT_BAR"].ToString();
                    InputTabGrid.Rows[i].Cells["포장일자"].Value = dt.Rows[i]["PACK_DATE"].ToString();
                    InputTabGrid.Rows[i].Cells["제품코드"].Value = dt.Rows[i]["ITEM_CD"].ToString();
                    InputTabGrid.Rows[i].Cells["제품명"].Value = dt.Rows[i]["ITEM_NM"].ToString();
                    //InputTabGrid.Rows[i].Cells["규격"].Value = dt.Rows[i]["SPEC"].ToString();
                    InputTabGrid.Rows[i].Cells["단위코드"].Value = dt.Rows[i]["UNIT_CD"].ToString();
                    InputTabGrid.Rows[i].Cells["단위"].Value = dt.Rows[i]["UNIT_NM"].ToString();
                    InputTabGrid.Rows[i].Cells["부위"].Value = dt.Rows[i]["CLASS_NM"].ToString();
                    InputTabGrid.Rows[i].Cells["축종"].Value = dt.Rows[i]["CHUGJONG_NM"].ToString();
                    InputTabGrid.Rows[i].Cells["원산지"].Value = dt.Rows[i]["COUNTRY_NM"].ToString();
                    InputTabGrid.Rows[i].Cells["유형"].Value = dt.Rows[i]["TYPE_NM"].ToString();
                    InputTabGrid.Rows[i].Cells["수량"].Value = (decimal.Parse(dt.Rows[i]["INPUT_AMT"].ToString())).ToString("#,0.######");
                    InputTabGrid.Rows[i].Cells["함량"].Value = dt.Rows[i]["HAMYANG"].ToString();
                    InputTabGrid.Rows[i].Cells["유통기한"].Value = dt.Rows[i]["EXPRT_DATE"].ToString();
                    InputTabGrid.Rows[i].Cells["묶음번호"].Value = dt.Rows[i]["A_UNION_CD"].ToString();
                    InputTabGrid.Rows[i].Cells["냉동구분"].Value = dt.Rows[i]["FROZEN_NM"].ToString();
                    //InputTabGrid.Rows[i].Cells["OLD_TOTAL_AMT"].Value = dt.Rows[i]["TOTAL_AMT"].ToString();
                    //InputTabGrid.Rows[i].Cells["박스수량"].Value = "1";
                    //InputTabGrid.Rows[i].Cells["업체명"].Value = dt.Rows[i]["CUST_NM"].ToString();
                    //InputTabGrid.Rows[i].Cells["CHK"].Value = false;
                    InputTabGrid.Rows[i].Cells["CHK"].Value = false;
                }
            }
            else
            {
                MessageBox.Show("입고항목이 없습니다");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridSetting() 
        {
            for (int i = 0; i < InputTabGrid.ColumnCount - 1; i++)  //체크 박스만 제외
            {
                InputTabGrid.Columns[i].ReadOnly = true;
                InputTabGrid.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            //InputTabGrid.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //InputTabGrid.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //InputTabGrid.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //InputTabGrid.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //InputTabGrid.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //InputTabGrid.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //InputTabGrid.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //InputTabGrid.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //InputTabGrid.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //InputTabGrid.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        

        public void bindData()
        {
            Application.DoEvents();

            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = fn_제품입고식별표_List();

                adoPrt = new DataTable();
                adoPrt = dt.Copy();

                int j = 0;
                int k = 0;

                for (int i = 0; i < this.InputTabGrid.Rows.Count; i++)
                {
                    
                    if ((bool)InputTabGrid.Rows[i].Cells["CHK"].Value == true)  //--- 12= 확인 체크필드
                    //if ((bool)InputTabGrid.Rows[i].Cells["CHK"].Value == true)  //--- 12= 확인 체크필드
                    {
                        
                        k = 1;
                        string sLotno = "" + this.InputTabGrid.Rows[i].Cells["LOT_NO"].Value.ToString();    //lot no
                        string sLotsub = "";//lot sub
                        string sLot식별 = "" + this.InputTabGrid.Rows[i].Cells["LOT_식별표"].Value.ToString();    //lot_식별표
                        string sDate = "" + this.InputTabGrid.Rows[i].Cells["포장일자"].Value.ToString();    //포장일자
                        string sCode = "" + this.InputTabGrid.Rows[i].Cells["제품코드"].Value.ToString();    //제품코드
                        string sName = "" + this.InputTabGrid.Rows[i].Cells["제품명"].Value.ToString();    //제품명
                        string sSpec = "";    //규격
                        string sUnit = "" + this.InputTabGrid.Rows[i].Cells["단위"].Value.ToString();    //단위
                        string sType = "" + this.InputTabGrid.Rows[i].Cells["유형"].Value.ToString();    //유형
                        string sChugjong = "" + this.InputTabGrid.Rows[i].Cells["축종"].Value.ToString();    //축종
                        string sCountry = "" + this.InputTabGrid.Rows[i].Cells["원산지"].Value.ToString();    //원산지
                        string sClass = "" + this.InputTabGrid.Rows[i].Cells["부위"].Value.ToString();    //부위
                        string sHamyang = "" + this.InputTabGrid.Rows[i].Cells["함량"].Value.ToString();    //함량
                        string sExprtdate = "" + this.InputTabGrid.Rows[i].Cells["유통기한"].Value.ToString();    //유통기한
                        string sFrozen = "" + this.InputTabGrid.Rows[i].Cells["냉동구분"].Value.ToString();    //냉동구분
                        string sUnion = "" + this.InputTabGrid.Rows[i].Cells["묶음번호"].Value.ToString();    //묶음번호
                        string sUCode = "" + this.InputTabGrid.Rows[i].Cells["단위코드"].Value.ToString();    //단위코드
                        string sCustnm = "";    //업체명

                        string nQty = "" + this.InputTabGrid.Rows[i].Cells["수량"].Value.ToString(); // 수량
                        //string nBQty = "" + this.InputTabGrid.Rows[i].Cells["박스수량"].Value.ToString().Trim(); //박스수량

                        //string sLotno = "" + this.InputTabGrid.Rows[i].Cells[12].Value.ToString();    //lot no

                        //sNUm = sLotno.Substring(0, 12); //--- 입고번호 = 입고일자+번호(yyyyMMdd0000)
                        dt.Rows.Add();
                        dt.Rows[j]["no"] = j;

                        dt.Rows[j]["입고일자"] = sDate;
                        dt.Rows[j]["입고번호"] = "";
                        dt.Rows[j]["입고순번"] = "";
                        dt.Rows[j]["제품코드"] = sCode;
                        dt.Rows[j]["제품명"] = sName;
                        dt.Rows[j]["규격"] = sSpec;
                        dt.Rows[j]["수량"] = nQty;
                        dt.Rows[j]["단위코드"] = sUCode;
                        dt.Rows[j]["유형"] = sType;
                        dt.Rows[j]["원산지"] = sCountry;
                        dt.Rows[j]["축종"] = sChugjong;
                        dt.Rows[j]["단위명"] = sUnit;
                        dt.Rows[j]["부위"] = sClass;
                        dt.Rows[j]["함량"] = sHamyang.Trim();
                        dt.Rows[j]["냉동구분"] = sFrozen;
                        dt.Rows[j]["유통기한"] = sExprtdate;
                        dt.Rows[j]["묶음번호"] = sUnion;
                        dt.Rows[j]["HEAT_NO"] = "";
                        dt.Rows[j]["HEAT_TIME"] = "";
                        dt.Rows[j]["ORDER_DATE"] = "";
                        dt.Rows[j]["ORDER_CD"] = "";
                        dt.Rows[j]["ORDER_SEQ"] = "";
                        dt.Rows[j]["RAW_MAT_GUBUN"] = "";
                        dt.Rows[j]["S_CODE_NM"] = "";
                        dt.Rows[j]["수량"] = nQty;
                        //dt.Rows[j]["단가"] = nBQty;
                        dt.Rows[j]["단가"] = "";
                        dt.Rows[j]["금액"] = "";
                        dt.Rows[j]["업체명"] = sCustnm;
                        dt.Rows[j]["제조번호"] = sLotno;
                        //dt.Rows[j]["제조번호"] = sLotno + int.Parse(sLotsub).ToString("000");
                        dt.Rows[j]["바코드제조번호"] = "*" + sLot식별 + "*";
                        dt.Rows[j]["박스번호"] = "1";

                        j = j + 1;
                    }
                }
                adoPrt = dt.Copy();


                
                //데이타 끝나고 다시 copy를 써준 이유는 for중에 no에 값을 엏었기 때문에 출력물 데이타테이블(dt)를 다시 복사함
                //adoPrt = dt.Copy();

                //for (int i = 0; i < this.InputTabGrid.Rows.Count; i++)
                //{
                //    if (adoPrt.Rows[i]["입고일자"] == null || adoPrt.Rows[i]["입고일자"].ToString().Equals(""))
                //    {
                //        adoPrt.Rows[i].Delete();
                //    }
                //}
                //adoPrt.AcceptChanges(); //--삭제확정

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
                Console.WriteLine(ex.Message + " - " + ex.ToString());
            }
        }

        public DataTable fn_제품입고식별표_List()
        {
            StringBuilder sb = new StringBuilder();

            //sb.AppendLine("SELECT A.INPUT_DATE, '' AS no, '' AS 입고일자, '' AS 입고번호, ''  AS 입고순번, '' AS 원부재료코드, '' 원부재료명, '' AS 규격    ");
            //sb.AppendLine(", '' AS HEAT_NO, '' AS HEAT_TIME, '' AS ORDER_DATE, '' AS ORDER_CD, '' AS ORDER_SEQ, '' AS RAW_MAT_GUBUN ");
            //sb.AppendLine(", '' AS S_CODE_NM, '' AS 단위코드, '' AS 단위명  ");
            //sb.AppendLine(", '' AS 수량, '' AS 단가, '' AS 금액, '' AS 제조번호, '' AS 바코드제조번호 ");

            //sb.AppendLine("  FROM F_RAW_DETAIL			AS A  ");
            //sb.AppendLine(" WHERE A.INPUT_DATE >= '" + start_date.Text.ToString() + "' AND  A.INPUT_DATE <= '" + end_date.Text.ToString() + "'");

            sb.AppendLine(" SELECT '' AS INPUT_DATE, '' AS no ");
            sb.AppendLine(", '' AS 입고일자, '' AS 입고번호, ''  AS 입고순번, '' AS 제품코드, '' 제품명, '' AS 규격    ");
            sb.AppendLine(", '' AS HEAT_NO, '' AS HEAT_TIME, '' AS ORDER_DATE, '' AS ORDER_CD, '' AS ORDER_SEQ, '' AS RAW_MAT_GUBUN ");
            sb.AppendLine(", '' AS S_CODE_NM, '' AS 단위코드, '' AS 단위명  ");
            sb.AppendLine(", '' AS 부위, '' AS 축종, '' AS 함량, '' AS 유통기한, '' AS 묶음번호, '' AS 냉동구분, '' AS 원산지, '' AS 유형  ");
            sb.AppendLine(", '' AS 수량, '' AS 단가, '' AS 금액, '' AS 제조번호, '' AS 바코드제조번호, '' AS 업체명, '' AS 박스번호 ");
            //sb.AppendLine("  FROM F_ITEM_INPUT A  ");
            //sb.AppendLine("  LEFT OUTER JOIN F_ITEM_INPUT_DETAIL B ON A.INPUT_DATE = B.INPUT_DATE AND A.INPUT_CD = B.INPUT_CD  ");
            //sb.AppendLine("  LEFT OUTER JOIN N_ITEM_CODE C  ON A.ITEM_CD = C.ITEM_CD    ");
            ////sb.AppendLine("  LEFT OUTER JOIN N_FLOW_CODE D  ON B.FLOW_CD = D.FLOW_CD   ");
            ////sb.AppendLine("  INNER JOIN F_ITEM_INPUT Z  ON A.LOT_NO = Z.LOT_NO AND B.LOT_SUB = Z.LOT_SUB AND B.F_STEP = Z.F_STEP   ");
            ////sb.AppendLine("  left outer join F_WORK_INST AS K on A.LOT_NO = K.LOT_NO   ");
            //sb.AppendLine(" WHERE 1=1  ");
            ////sb.AppendLine("   AND D.ITEM_IDEN_YN = 'Y'  ");
            ////sb.AppendLine("   AND A.COMPLETE_YN = 'Y'  ");
            //sb.AppendLine(" ORDER BY A.INPUT_DATE, A.INPUT_CD");

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }

            return wAdo.SqlCommandSelect(sCommand);
        }

        private void btn출력2_Click(object sender, EventArgs e)
        {
            btn출력2.Enabled = false;

            strCondition = "";

            if (InputTabGrid.Rows.Count == 0)
            {
                MessageBox.Show("출력할 자료가 없습니다.");
                btn출력2.Enabled = true;
                return;
            }

            //bindData_Print();
            //----------------------------------------
            bindData();

            if (strCondition == "No")
            {
                MessageBox.Show("출력할 자료가 없습니다.");
                btn출력2.Enabled = true;
                return;
            }

            if (strCondition != "ERROR")
            {
                strCondition = "제품입고식별표2";

                frmPrt = readyPrt;
                frmPrt.Show();
                frmPrt.BringToFront();

                frmPrt.prt_식별표(adoPrt, strCondition);
            }

            btn출력2.Enabled = true;
        }

    }
}
