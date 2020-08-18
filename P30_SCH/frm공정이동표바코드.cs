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
    public partial class frm공정이동표바코드 : Form
    {
        private Popup.frmPrint readyPrt = new Popup.frmPrint();

        private Popup.frmPrint frmPrt;
        private string strCondition = "";
        private DataTable adoPrt = null;
        private wnAdo wAdo = new wnAdo();

        public frm공정이동표바코드()
        {
            InitializeComponent();
        }

        private void frm공정이동표바코드_Load(object sender, EventArgs e)
        {
            workLogic();
        }

        #region button logic 
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_bar_out_Click(object sender, EventArgs e)
        {
            btn_bar_out.Enabled = false;

            strCondition = "";

            //bindData_Print();
            //----------------------------------------
            bindData();

            if (strCondition == "No")
            {
                MessageBox.Show("출력할 자료가 없습니다.");
                btn_bar_out.Enabled = true;
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

            btn_bar_out.Enabled = true;
        }

        private void saveLogic() 
        {

            string in_bar_lot_no = txt_input_bar.Text.ToString().Substring(0, 10);
            string in_bar_sub_no = int.Parse(txt_input_bar.Text.ToString().Substring(10, 3)).ToString();

            if (in_bar_lot_no.ToString().Equals(txt_lot_no.Text.ToString()) && in_bar_sub_no.ToString().Equals(txt_sub_no.Text.ToString()))
            {
                
            }
            else
            {
                
                txt_lot_no.Text = txt_input_bar.Text.ToString().Substring(0, 10);
                txt_sub_no.Text = int.Parse(txt_input_bar.Text.ToString().Substring(10, 3)).ToString();
                //reGridLogic();

                wnDm wDm2 = new wnDm();
                DataTable dt2 = new DataTable();
                StringBuilder sb2 = new StringBuilder();
                sb2.AppendLine(" and LOT_NO = '" + txt_lot_no.Text.ToString() + "' ");

                dt2 = wDm2.fn_Work_Inst_Cnt(sb2.ToString()); //작업지시서 여부 
                if (dt2.Rows.Count > 0)
                {
                    txt_lot_bar.Text = txt_input_bar.Text.ToString();
                    lbl_bar.Text = txt_input_bar.Text.ToString();

                    dt2 = wDm2.fn_wf_item_srch(txt_lot_no.Text.ToString());
                    if (dt2 != null && dt2.Rows.Count > 0)
                    {
                        txt_item_cd.Text = dt2.Rows[0]["ITEM_CD"].ToString();
                        txt_item_nm.Text = dt2.Rows[0]["ITEM_NM"].ToString();
                        txt_spec.Text = dt2.Rows[0]["SPEC"].ToString();
                    }

                    //현재 공정 가져오기 

                    sb2 = new StringBuilder();
                    sb2.AppendLine(" and A.LOT_NO = '" + txt_lot_no.Text.ToString() + "' ");
                    sb2.AppendLine(" and A.LOT_SUB = '" + txt_sub_no.Text.ToString() + "' ");
                    //sb.AppendLine(" and A.LOT_SUB = '" + txt_sub_no.Text.ToString() + "' ");
                    dt2 = wDm2.fn_wf_LotNo_Sub_Status(sb2.ToString());
                    if (dt2 != null && dt2.Rows.Count > 0)
                    {
                        txt_f_step.Text = dt2.Rows[0]["F_STEP"].ToString();
                        txt_flow_cd.Text = dt2.Rows[0]["FLOW_CD"].ToString();
                        txt_flow_nm.Text = dt2.Rows[0]["FLOW_NM"].ToString();
                        txt_f_sub_amt.Text = decimal.Parse(dt2.Rows[0]["F_SUB_AMT"].ToString()).ToString("#,0.######");
                        txt_f_sub_date.Text = dt2.Rows[0]["F_SUB_DATE"].ToString();
                    }
                    else
                    {
                        txt_f_step.Text = "0";
                        txt_flow_cd.Text = "";
                        txt_flow_nm.Text = "없음";
                    }

                    //작업공정 미완료 현황 데이터에 LOT_NO 가져와서 제품공정목록 뿌리는 작업 
                    int chk = 0;
                    for (int i = 0; i < workGrid.Rows.Count; i++)
                    {
                        if (txt_lot_no.Text.ToString().Equals(workGrid.Rows[i].Cells["LOT_NO"].Value.ToString()))
                        {
                            chk = i;

                            break;
                        }
                    }

                    sb2 = new StringBuilder();
                    sb2.AppendLine(" and LOT_NO = '" + txt_lot_no.Text.ToString() + "' ");
                    dt2 = wDm2.fn_Work_Flow_Cnt(sb2.ToString());

                    lotSubGrid.Rows.Clear();
                    lotSubLogic(workGrid, chk); //제품공정목록 뿌리기 

                    lotSubLogicMain(dt2, new StringBuilder(), txt_lot_no.Text.ToString());
                }
                else
                {
                    MessageBox.Show("작업지시서에 등록된 LOT_NO가 없습니다. ");
                    return;
                }
            }

            wnDm wDm = new wnDm();
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" and LOT_NO = '" + txt_input_bar.Text.ToString().Substring(0, 10) + "' ");

            dt = wDm.fn_Work_Inst_Cnt(sb.ToString()); //작업지시서 등록여부
            if (dt.Rows.Count > 0)
            {
                DataTable dt2 = new DataTable();

                dt2 = wDm.fn_Work_Flow_Cnt(sb.ToString());
                if (int.Parse(dt2.Rows[0]["cnt"].ToString()) > 0) //작업공정이 진행되어 있는 경우
                {
                    DataTable dt3 = new DataTable();
                    dt3 = wDm.fn_Flow_Step_Curr(txt_input_bar.Text.ToString().Substring(0, 10), txt_input_bar.Text.ToString().Substring(10, 3));
                    if (dt3 != null && dt3.Rows.Count > 0)
                    {
                        int flow_seq = int.Parse(dt3.Rows[0]["FLOW_SEQ"].ToString());
                        int f_curr_step = int.Parse(dt3.Rows[0]["F_STEP"].ToString());

                        if (flow_seq - f_curr_step <= 0)
                        {
                            MessageBox.Show("공정 단계를 모두 등록되어 공정이동이 불가능합니다. ");
                            return;
                        }
                        else
                        {
                            int rsNum = wDm.insert_Work_Flow_Move(
                                                    dt3.Rows[0]["LOT_NO"].ToString()
                                                  , dt3.Rows[0]["LOT_SUB"].ToString()
                                                  , int.Parse(dt3.Rows[0]["F_STEP"].ToString())
                                                  , int.Parse(txt_f_sub_amt.Text.ToString()));

                            if (rsNum == 0)
                            {
                                reGridLogic();
                                MessageBox.Show("성공적으로 수정하였습니다.");

                            }
                            else if (rsNum == 1)
                                MessageBox.Show("저장에 실패하였습니다");
                            else
                                MessageBox.Show("Exception 에러");
                        }
                    }
                    else //LOT_NO의 작업지시서엔 등록되어있는데 해당 SUB_NO가 아직 공정등록하지 않았을 경우
                    {
                        int rsNum = wDm.insert_Work_Flow_Move(
                                                    txt_input_bar.Text.ToString().Substring(0, 10)
                                                  , txt_input_bar.Text.ToString().Substring(10, 3)
                                                  , 0
                                                  , int.Parse(txt_f_sub_amt.Text.ToString()));

                        if (rsNum == 0)
                        {
                            reGridLogic();
                            MessageBox.Show("성공적으로 등록하였습니다.");

                        }
                        else if (rsNum == 1)
                            MessageBox.Show("저장에 실패하였습니다");
                        else
                            MessageBox.Show("Exception 에러");
                    }
                }
                else //작업지시서엔 등록되있으나 공정이 진행 안된 경우  
                {
                    int rsNum = wDm.insert_Work_Flow_Move_First(
                                                        txt_input_bar.Text.ToString().Substring(0, 10)
                                                      , txt_input_bar.Text.ToString().Substring(10, 3)
                                                      , txt_item_cd.Text.ToString()
                                                      , int.Parse(txt_f_sub_amt.Text.ToString())
                                                      , 0);

                    if (rsNum == 0)
                    {
                        reGridLogic();
                        MessageBox.Show("성공적으로 등록하였습니다.");

                    }
                    else if (rsNum == 1)
                        MessageBox.Show("저장에 실패하였습니다");
                    else
                        MessageBox.Show("Exception 에러");
                }
            }
            else
            {
                MessageBox.Show("작업지시서에 LOT_NO가 없습니다. ");
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txt_input_bar.Text.ToString().Equals("")) 
            {
                MessageBox.Show("공정이동식별 번호를 입력하시기 바랍니다. ");
                return;
            }

            saveLogic();
            
        }
        #endregion button logic

        #region logic 
        private void workLogic()
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_Work_List("where ISNULL(C.COMPLETE_YN,'N') = 'N' ");

                if (dt.Rows.Count > 0)
                {
                    workGrid.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        workGrid.Rows[i].Cells["LOT_NO"].Value = dt.Rows[i]["LOT_NO"].ToString();
                        workGrid.Rows[i].Cells["ITEM_NM"].Value = dt.Rows[i]["ITEM_NM"].ToString();
                        workGrid.Rows[i].Cells["INST_AMT"].Value = dt.Rows[i]["INST_AMT"].ToString();
                        workGrid.Rows[i].Cells["ITEM_CD"].Value = dt.Rows[i]["ITEM_CD"].ToString();
                        workGrid.Rows[i].Cells["SPEC"].Value = dt.Rows[i]["SPEC"].ToString();
                        workGrid.Rows[i].Cells["W_INST_DATE"].Value = dt.Rows[i]["W_INST_DATE"].ToString();
                        workGrid.Rows[i].Cells["W_INST_CD"].Value = dt.Rows[i]["W_INST_CD"].ToString();
                        workGrid.Rows[i].Cells["CHAR_AMT"].Value = dt.Rows[i]["CHARGE_AMT"].ToString();
                        workGrid.Rows[i].Cells["PACK_AMT"].Value = dt.Rows[i]["PACK_AMT"].ToString();
                        workGrid.Rows[i].Cells["COMPLETE_YN"].Value = dt.Rows[i]["COMPLETE_YN"].ToString();
                    }
                }
                else
                {
                    workGrid.Rows.Clear();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("error" + e.ToString());
            }
        }

        public void bindData()
        {
            Application.DoEvents();

            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = fn_공정이동식별표_List();

                adoPrt = new DataTable();
                adoPrt = dt.Copy();

                int j = 0;
                //int k = 0;

                string sLotno = "" + txt_lot_no.Text.ToString();    //lot no
                string sLotsub = "" + txt_sub_no.Text.ToString();    //lot sub
                string sLot식별 = "" + txt_lot_bar.Text.ToString();    //lot_식별표
                string sDate = "";
                string sCode = "" + dt.Rows[0]["제품코드"].ToString();
                string sName = "" + dt.Rows[0]["제품명"].ToString(); //제품명
                string sSpec = "" + dt.Rows[0]["규격"].ToString();    //규격
                string sUnit = "" + dt.Rows[0]["단위명"].ToString();    //단위
                string sUCode = "" + dt.Rows[0]["단위코드"].ToString();    //단위
                string sCustnm = "" + dt.Rows[0]["업체명"].ToString();    //단위    //업체명

                string nQty = "" + decimal.Parse(txt_f_sub_amt.Text.ToString().Trim()).ToString("#,0.######"); //수량
                //string nBQty = "" + this.InputTabGrid.Rows[i].Cells["박스수량"].Value.ToString().Trim(); //박스수량

                //string sLotno = "" + this.InputTabGrid.Rows[i].Cells[12].Value.ToString();    //lot no

                //sNUm = sLotno.Substring(0, 12); //--- 입고번호 = 입고일자+번호(yyyyMMdd0000)

                dt.Rows[0]["no"] = j;

                dt.Rows[0]["입고일자"] = sDate;
                dt.Rows[j]["입고번호"] = "";
                dt.Rows[j]["입고순번"] = "";
                dt.Rows[j]["제품코드"] = sCode;
                dt.Rows[j]["제품명"] = sName;
                dt.Rows[j]["규격"] = sSpec;
                dt.Rows[j]["단위코드"] = sUCode;
                dt.Rows[j]["단위명"] = sUnit;
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
                dt.Rows[j]["제조번호"] = sLotno + int.Parse(sLotsub).ToString("000");
                dt.Rows[j]["바코드제조번호"] = "*" + sLot식별 + "*";
                dt.Rows[j]["박스번호"] = "1";

                j = j + 1;

                adoPrt = dt.Copy();

                //데이타 끝나고 다시 copy를 써준 이유는 for중에 no에 값을 엏었기 때문에 출력물 데이타테이블(dt)를 다시 복사함
                //adoPrt = dt.Copy();
                //int num = dt.Rows.Count;

                //for (int i = j; i < this.InputTabGrid.Rows.Count; i++)
                //{
                //    adoPrt.Rows[i].Delete();
                //}
                //adoPrt.AcceptChanges(); //--삭제확정

                //if (k == 0)
                //{
                //    strCondition = "No";
                //}

            }
            catch (Exception ex)
            {
                strCondition = "ERROR";
                MessageBox.Show("검색중에 오류가 발생했습니다.");
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
            }

        }

        public DataTable fn_공정이동식별표_List()
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendLine("  SELECT A.W_INST_DATE AS INPUT_DATE, '' AS no  ");
            sb.AppendLine(",  '' AS 입고일자, '' AS 입고번호, ''  AS 입고순번, A.ITEM_CD AS 제품코드, C.ITEM_NM AS 제품명, C.SPEC AS 규격    ");
            sb.AppendLine(", '' AS HEAT_NO, '' AS HEAT_TIME, '' AS ORDER_DATE, '' AS ORDER_CD, '' AS ORDER_SEQ, '' AS RAW_MAT_GUBUN  ");
            sb.AppendLine(", '' AS S_CODE_NM, C.UNIT_CD AS 단위코드, U.UNIT_NM AS 단위명   ");
            sb.AppendLine(", '' AS 수량, '' AS 단가, '' AS 금액, '' AS 제조번호, '' AS 바코드제조번호, Z.CUST_NM AS 업체명, '' AS 박스번호 ");
            sb.AppendLine("  FROM F_WORK_INST A     ");
            sb.AppendLine("  LEFT OUTER JOIN N_ITEM_CODE C  ON A.ITEM_CD = C.ITEM_CD     ");
            sb.AppendLine("  left outer join N_CUST_CODE Z on A.CUST_CD = Z.CUST_CD   ");
            sb.AppendLine("  left outer join N_UNIT_CODE U on C.UNIT_CD = U.UNIT_CD  ");
            sb.AppendLine(" WHERE 1=1  ");
            sb.AppendLine("   AND A.LOT_NO = '" + txt_lot_no.Text.ToString() + "'  ");
            sb.AppendLine(" ORDER BY A.LOT_NO");

            //sb.AppendLine("  SELECT A.FLOW_DATE AS INPUT_DATE, '' AS no  ");
            //sb.AppendLine(",  '' AS 입고일자, '' AS 입고번호, ''  AS 입고순번, A.ITEM_CD AS 제품코드, C.ITEM_NM AS 제품명, C.SPEC AS 규격    ");
            //sb.AppendLine(", '' AS HEAT_NO, '' AS HEAT_TIME, '' AS ORDER_DATE, '' AS ORDER_CD, '' AS ORDER_SEQ, '' AS RAW_MAT_GUBUN  ");
            //sb.AppendLine(", '' AS S_CODE_NM, C.UNIT_CD AS 단위코드, U.UNIT_NM AS 단위명   ");
            //sb.AppendLine(", '' AS 수량, '' AS 단가, '' AS 금액, '' AS 제조번호, '' AS 바코드제조번호, Z.CUST_NM AS 업체명, '' AS 박스번호 ");
            //sb.AppendLine("  FROM F_WORK_FLOW A   ");
            //sb.AppendLine("  LEFT OUTER JOIN N_ITEM_CODE C  ON A.ITEM_CD = C.ITEM_CD     ");
            //sb.AppendLine("  left outer join F_WORK_INST AS K on A.LOT_NO = K.LOT_NO     ");
            //sb.AppendLine("  left outer join N_CUST_CODE Z on K.CUST_CD = Z.CUST_CD   ");
            //sb.AppendLine("  left outer join N_UNIT_CODE U on C.UNIT_CD = U.UNIT_CD  ");
            //sb.AppendLine(" WHERE 1=1  ");
            //sb.AppendLine("   AND A.LOT_NO = '" + txt_lot_no.Text.ToString() + "'  ");
            //sb.AppendLine(" ORDER BY A.LOT_NO");

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }

            return wAdo.SqlCommandSelect(sCommand);
        }


        private void reGridLogic() 
        {

            //제품공정 목록 초기화 

            wnDm wDm = new wnDm();
            DataTable dt = null;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" and A.LOT_NO = '" + txt_lot_no.Text.ToString() + "' ");
            //sb.AppendLine(" and A.LOT_SUB = '" + txt_sub_no.Text.ToString() + "' ");
            dt = wDm.fn_wf_LotNo_Sub_Status(sb.ToString());

            if (dt != null && dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int lot_sub = int.Parse(dt.Rows[i]["LOT_SUB"].ToString());

                    lotSubGrid.Rows[lot_sub - 1].Cells[0].Value = dt.Rows[i]["LOT_NO"].ToString();
                    lotSubGrid.Rows[lot_sub - 1].Cells[1].Value = dt.Rows[i]["LOT_SUB"].ToString();
                    lotSubGrid.Rows[lot_sub - 1].Cells[2].Value = dt.Rows[i]["F_STEP"].ToString();
                    lotSubGrid.Rows[lot_sub - 1].Cells[3].Value = dt.Rows[i]["FLOW_NM"].ToString(); ;
                    lotSubGrid.Rows[lot_sub - 1].Cells[4].Value = decimal.Parse(dt.Rows[i]["F_SUB_AMT"].ToString()).ToString("#,0.######"); //장입 수량
                    lotSubGrid.Rows[lot_sub - 1].Cells[5].Value = dt.Rows[i]["F_SUB_DATE"].ToString(); ; //투입일자
                    lotSubGrid.Rows[lot_sub - 1].Cells[6].Value = dt.Rows[i]["FLOW_CD"].ToString(); ; //공정코드
                }

                //lotSubGrid.RowCount = dt.Rows.Count;
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    lotSubGrid.Rows[i].Cells[0].Value = dt.Rows[i]["LOT_NO"].ToString();
                //    lotSubGrid.Rows[i].Cells[1].Value = dt.Rows[i]["LOT_SUB"].ToString();
                //    lotSubGrid.Rows[i].Cells[2].Value = dt.Rows[i]["F_STEP"].ToString();
                //    lotSubGrid.Rows[i].Cells[3].Value = dt.Rows[i]["FLOW_NM"].ToString();
                //    lotSubGrid.Rows[i].Cells[4].Value = decimal.Parse(dt.Rows[i]["F_SUB_AMT"].ToString()).ToString("#,0.######"); //장입 수량
                //    lotSubGrid.Rows[i].Cells[5].Value = dt.Rows[i]["F_SUB_DATE"].ToString(); ; //투입일자
                //    lotSubGrid.Rows[i].Cells[6].Value = dt.Rows[i]["FLOW_CD"].ToString(); ; //공정코드
                //}
            }

            sb.AppendLine(" and A.LOT_SUB = '" + txt_sub_no.Text.ToString() + "' ");
            dt = wDm.fn_wf_LotNo_Sub_Status(sb.ToString());

            if (dt != null && dt.Rows.Count > 0) 
            {
                txt_lot_no.Text = dt.Rows[0]["LOT_NO"].ToString();
                txt_sub_no.Text = dt.Rows[0]["LOT_SUB"].ToString();
                txt_f_step.Text = dt.Rows[0]["F_STEP"].ToString();
                txt_flow_nm.Text = dt.Rows[0]["FLOW_NM"].ToString();
                txt_f_sub_amt.Text = decimal.Parse(dt.Rows[0]["F_SUB_AMT"].ToString()).ToString("#,0.######"); //장입 수량
                txt_f_sub_date.Text = dt.Rows[0]["F_SUB_DATE"].ToString(); ; //투입일자
                txt_flow_cd.Text = dt.Rows[0]["FLOW_CD"].ToString(); ; //공정코드
            }

            dt = wDm.fn_wf_item_srch(txt_lot_no.Text.ToString());

            if (dt != null && dt.Rows.Count > 0)
            {
                txt_item_nm.Text = dt.Rows[0]["ITEM_NM"].ToString();
                txt_spec.Text = dt.Rows[0]["SPEC"].ToString();
            }

            lot_sub_grd_logic();

        }
        #endregion logic

        #region grid logic 
        private void workGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            
            wnDm wDm = new wnDm();
            DataTable dt = null;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" and LOT_NO = '" + dgv.Rows[e.RowIndex].Cells["LOT_NO"].Value.ToString() + "' ");
            dt = wDm.fn_Work_Flow_Cnt(sb.ToString());
            
            lotSubGrid.Rows.Clear();

            lotSubLogic(dgv, e.RowIndex); //lotSubGrid 세팅 

            lotSubLogicMain(dt, sb, dgv.Rows[e.RowIndex].Cells["LOT_NO"].Value.ToString());
        }

        private void lotSubGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            wnDm wDm = new wnDm();
            DataTable dt = null;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" and LOT_NO = '" + dgv.Rows[e.RowIndex].Cells[0].Value.ToString() + "' ");

            dt = wDm.fn_Work_Inst_Cnt(sb.ToString());

            if (dt.Rows.Count > 0)
            {
                txt_lot_no.Text = dgv.Rows[e.RowIndex].Cells[0].Value.ToString();
                txt_sub_no.Text = dgv.Rows[e.RowIndex].Cells[1].Value.ToString();
                txt_f_step.Text = dgv.Rows[e.RowIndex].Cells[2].Value.ToString();
                txt_flow_nm.Text = dgv.Rows[e.RowIndex].Cells[3].Value.ToString();
                txt_f_sub_amt.Text = dgv.Rows[e.RowIndex].Cells[4].Value.ToString();
                txt_f_sub_date.Text = dgv.Rows[e.RowIndex].Cells[5].Value.ToString();
                txt_flow_cd.Text = dgv.Rows[e.RowIndex].Cells[6].Value.ToString();

                txt_input_bar.Text = txt_lot_no.Text.ToString() + int.Parse(txt_sub_no.Text.ToString()).ToString("000");
                txt_lot_bar.Text = txt_input_bar.Text.ToString();
                lbl_bar.Text = txt_input_bar.Text.ToString();
                dt = wDm.fn_wf_item_srch(txt_lot_no.Text.ToString());

                if (dt != null && dt.Rows.Count > 0)
                {
                    txt_item_cd.Text = dt.Rows[0]["ITEM_CD"].ToString();
                    txt_item_nm.Text = dt.Rows[0]["ITEM_NM"].ToString();
                    txt_spec.Text = dt.Rows[0]["SPEC"].ToString();
                }

                lot_sub_grd_logic();
            }
            else
            {
                MessageBox.Show("작업지시서에 등록된 LOT_NO가 없습니다. ");
                return;
            }
        }

        private void lot_sub_grd_logic() 
        {
            wnDm wDm = new wnDm();
            DataTable dt = new DataTable();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" and A.LOT_NO = '" + txt_lot_no.Text.ToString() + "' ");
            sb.AppendLine(" and A.LOT_SUB = '" + txt_sub_no.Text.ToString() + "' ");

            dt = wDm.fn_Work_Flow_Detail(sb.ToString());

            cplowLotSub.Rows.Clear();

            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cplowLotSub.Rows.Add();
                    cplowLotSub.Rows[i].Cells[0].Value = dt.Rows[i]["LOT_NO"].ToString();
                    cplowLotSub.Rows[i].Cells[1].Value = dt.Rows[i]["LOT_SUB"].ToString();
                    cplowLotSub.Rows[i].Cells[2].Value = dt.Rows[i]["F_STEP"].ToString();
                    cplowLotSub.Rows[i].Cells[3].Value = dt.Rows[i]["FLOW_NM"].ToString(); ;
                    cplowLotSub.Rows[i].Cells[4].Value = decimal.Parse(dt.Rows[i]["F_SUB_AMT"].ToString()).ToString("#,0.######"); //장입 수량
                    cplowLotSub.Rows[i].Cells[5].Value = dt.Rows[i]["F_SUB_DATE"].ToString(); ; //투입일자
                    cplowLotSub.Rows[i].Cells[6].Value = dt.Rows[i]["FLOW_CD"].ToString(); ; //공정코드
                }
            }
        }

        #endregion grid logic

        private void txt_input_bar_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComInfo.onlyNum(sender, e);
        }

        private void txt_input_bar_KeyUp(object sender, KeyEventArgs e)
        {
            if (txt_input_bar.Text.Length >= 13)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    return;
                }

                if (e.KeyCode == Keys.Tab) 
                {
                    return;
                }

                if (e.KeyCode == Keys.CapsLock) return;
                if (e.KeyCode == Keys.Space) return;
                if (e.KeyCode == Keys.Alt) return;
                if (e.KeyCode == Keys.ControlKey) return;

                saveLogic();
            }
        }

        private void lotSubLogic(DataGridView dgv, int rowIdx) 
        {
            wnDm wDm = new wnDm();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("where A.ITEM_CD = '" + dgv.Rows[rowIdx].Cells["ITEM_CD"].Value.ToString() + "' ");
            sb.AppendLine("     and A.SEQ = 1 ");

            DataTable dt = wDm.fn_Item_Flow_List(sb.ToString());
            string flow_nm = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                flow_nm = dt.Rows[0]["FLOW_NM"].ToString();
            }

            double d_inst_amt = double.Parse(dgv.Rows[rowIdx].Cells["INST_AMT"].Value.ToString());
            double d_char_amt = double.Parse(dgv.Rows[rowIdx].Cells["CHAR_AMT"].Value.ToString());

            double rs_div = 0.0; //나누기 결과 값 -> 13(수량)/2(포장수량) = 6(결과 값)
            double rs_remain = 0.0; // 나누기 나머지 값 -> 13(수량)% 6(포장수량) = 1 (나머지 값)

            if (d_char_amt == 0) //장입수량이 0일 경우 
            {
                rs_div = 0;
                rs_remain = d_inst_amt;
            }
            else
            {
                rs_div = d_inst_amt / d_char_amt;
                rs_remain = d_inst_amt % d_char_amt;
            }

            if (rs_remain > 0)
            {
                for (int i = 0; i < rs_div - 1; i++) //나누기 결과 값이 6이면 6번을 돌린다. 
                {
                    lotSubGrid.Rows.Add();

                    lotSubGrid.Rows[i].Cells[0].Value = dgv.Rows[rowIdx].Cells["LOT_NO"].Value.ToString();
                    lotSubGrid.Rows[i].Cells[1].Value = (i + 1).ToString();
                    lotSubGrid.Rows[i].Cells[2].Value = "0";
                    lotSubGrid.Rows[i].Cells[3].Value = "없음";
                    lotSubGrid.Rows[i].Cells[4].Value = d_char_amt.ToString("#,0.######"); //장입 수량
                    lotSubGrid.Rows[i].Cells[5].Value = ""; //투입일자
                    lotSubGrid.Rows[i].Cells[6].Value = ""; //공정코드

                }
                if (rs_remain > 0)
                {
                    lotSubGrid.Rows.Add();

                    lotSubGrid.Rows[lotSubGrid.Rows.Count - 1].Cells[0].Value = dgv.Rows[rowIdx].Cells["LOT_NO"].Value.ToString();
                    lotSubGrid.Rows[lotSubGrid.Rows.Count - 1].Cells[1].Value = lotSubGrid.Rows.Count.ToString();
                    lotSubGrid.Rows[lotSubGrid.Rows.Count - 1].Cells[2].Value = "0";
                    lotSubGrid.Rows[lotSubGrid.Rows.Count - 1].Cells[3].Value = "없음";
                    lotSubGrid.Rows[lotSubGrid.Rows.Count - 1].Cells[4].Value = rs_remain.ToString("#,0.######"); //나머지 수량
                    lotSubGrid.Rows[lotSubGrid.Rows.Count - 1].Cells[5].Value = ""; //투입일자
                    lotSubGrid.Rows[lotSubGrid.Rows.Count - 1].Cells[6].Value = ""; //공정코드
                }
                else
                {
                    lotSubGrid.Rows.Add();
                    lotSubGrid.Rows[lotSubGrid.Rows.Count - 1].Cells[0].Value = dgv.Rows[rowIdx].Cells["LOT_NO"].Value.ToString();
                    lotSubGrid.Rows[lotSubGrid.Rows.Count - 1].Cells[1].Value = lotSubGrid.Rows.Count.ToString();
                    lotSubGrid.Rows[lotSubGrid.Rows.Count - 1].Cells[2].Value = "0";
                    lotSubGrid.Rows[lotSubGrid.Rows.Count - 1].Cells[3].Value = "없음";
                    lotSubGrid.Rows[lotSubGrid.Rows.Count - 1].Cells[4].Value = d_char_amt.ToString("#,0.######"); //장입 수량
                    lotSubGrid.Rows[lotSubGrid.Rows.Count - 1].Cells[5].Value = ""; //투입일자
                    lotSubGrid.Rows[lotSubGrid.Rows.Count - 1].Cells[6].Value = ""; //공정코드
                }
            }
            else
            {
                if (rs_div > 1)
                {
                    for (int i = 0; i < rs_div; i++) //나누기 결과 값이 6이면 6번을 돌린다. 
                    {
                        lotSubGrid.Rows.Add();

                        lotSubGrid.Rows[i].Cells[0].Value = dgv.Rows[rowIdx].Cells["LOT_NO"].Value.ToString();
                        lotSubGrid.Rows[i].Cells[1].Value = (i + 1).ToString();
                        lotSubGrid.Rows[i].Cells[2].Value = "0";
                        lotSubGrid.Rows[i].Cells[3].Value = "없음";
                        lotSubGrid.Rows[i].Cells[4].Value = d_char_amt.ToString("#,0.######"); //장입 수량
                        lotSubGrid.Rows[i].Cells[5].Value = ""; //투입일자
                        lotSubGrid.Rows[i].Cells[6].Value = ""; //공정코드
                    }
                }
                else
                {
                    lotSubGrid.Rows.Add();

                    lotSubGrid.Rows[lotSubGrid.Rows.Count - 1].Cells[0].Value = dgv.Rows[rowIdx].Cells["LOT_NO"].Value.ToString();
                    lotSubGrid.Rows[lotSubGrid.Rows.Count - 1].Cells[1].Value = lotSubGrid.Rows.Count.ToString();
                    lotSubGrid.Rows[lotSubGrid.Rows.Count - 1].Cells[2].Value = "0";
                    lotSubGrid.Rows[lotSubGrid.Rows.Count - 1].Cells[3].Value = "없음";
                    lotSubGrid.Rows[lotSubGrid.Rows.Count - 1].Cells[4].Value = d_inst_amt.ToString("#,0.######"); //전체 수량
                    lotSubGrid.Rows[lotSubGrid.Rows.Count - 1].Cells[5].Value = ""; //투입일자
                    lotSubGrid.Rows[lotSubGrid.Rows.Count - 1].Cells[6].Value = ""; //공정코드
                }
            }
        }

        private void lotSubLogicMain(DataTable dt, StringBuilder sb,string lot_no)
        {
            if (int.Parse(dt.Rows[0]["cnt"].ToString()) > 0) //공정에 LOT_NO 투입되었는지 체크
            {
                wnDm wDm = new wnDm();
                sb = new StringBuilder();
                sb.AppendLine(" and A.LOT_NO = '" + lot_no +"' "); //dgv.Rows[e.RowIndex].Cells["LOT_NO"].Value.ToString()
                dt = wDm.fn_wf_LotNo_Sub_Status(sb.ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    //공정 LOT_NO의 SUB_NO가 모두 저장되어 있는지 아닌지 체크 

                    if (lotSubGrid.Rows.Count == dt.Rows.Count)
                    {
                        lotSubGrid.Rows.Clear();
                        lotSubGrid.RowCount = dt.Rows.Count;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            lotSubGrid.Rows[i].Cells[0].Value = dt.Rows[i]["LOT_NO"].ToString();
                            lotSubGrid.Rows[i].Cells[1].Value = dt.Rows[i]["LOT_SUB"].ToString();
                            lotSubGrid.Rows[i].Cells[2].Value = dt.Rows[i]["F_STEP"].ToString();
                            lotSubGrid.Rows[i].Cells[3].Value = dt.Rows[i]["FLOW_NM"].ToString(); ;
                            lotSubGrid.Rows[i].Cells[4].Value = decimal.Parse(dt.Rows[i]["F_SUB_AMT"].ToString()).ToString("#,0.######"); //장입 수량
                            lotSubGrid.Rows[i].Cells[5].Value = dt.Rows[i]["F_SUB_DATE"].ToString(); ; //투입일자
                            lotSubGrid.Rows[i].Cells[6].Value = dt.Rows[i]["FLOW_CD"].ToString(); ; //공정코드
                        }
                    }
                    else
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            int lot_sub = int.Parse(dt.Rows[i]["LOT_SUB"].ToString());

                            lotSubGrid.Rows[lot_sub - 1].Cells[0].Value = dt.Rows[i]["LOT_NO"].ToString();
                            lotSubGrid.Rows[lot_sub - 1].Cells[1].Value = dt.Rows[i]["LOT_SUB"].ToString();
                            lotSubGrid.Rows[lot_sub - 1].Cells[2].Value = dt.Rows[i]["F_STEP"].ToString();
                            lotSubGrid.Rows[lot_sub - 1].Cells[3].Value = dt.Rows[i]["FLOW_NM"].ToString(); ;
                            lotSubGrid.Rows[lot_sub - 1].Cells[4].Value = decimal.Parse(dt.Rows[i]["F_SUB_AMT"].ToString()).ToString("#,0.######"); //장입 수량
                            lotSubGrid.Rows[lot_sub - 1].Cells[5].Value = dt.Rows[i]["F_SUB_DATE"].ToString(); ; //투입일자
                            lotSubGrid.Rows[lot_sub - 1].Cells[6].Value = dt.Rows[i]["FLOW_CD"].ToString(); ; //공정코드
                        }
                    }
                }
            }
            else
            {
                //아예 없는 경우 (이미 세팅해놨긴 때문에 고칠 필요가 없음)
            }
        }
    }
}
