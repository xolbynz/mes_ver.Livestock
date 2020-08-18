using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 스마트팩토리.CLS;

namespace 스마트팩토리.P20_ORD
{
    public partial class frm원자재재고현황 : Form
    {
        Popup.frmPrint readyPrt = new Popup.frmPrint();
        private wnGConstant wConst = new wnGConstant();

        DataTable adoPrt = null;
        wnAdo wAdo = new wnAdo();
        public Popup.frmPrint frmPrt;

        public string strDay1 = "";
        public string strDay2 = "";
        public string strCondition = "";

        public frm원자재재고현황()
        {
            InitializeComponent();
        }

        private void frm원자재재고현황_Load(object sender, EventArgs e)
        {
            init_ComboBox();
            grdCellSetting();
            grdListPrint();
            ComInfo.gridHeaderSet(rawStockGrid);
            ComInfo.gridHeaderSet(rawDetailGrid);
        }

        private void init_ComboBox()
        {
            cmb_cd_srch.Items.Add("전체 검색");
            cmb_cd_srch.Items.Add("코드별 검색");
            cmb_cd_srch.Items.Add("원자재명 검색");
            cmb_cd_srch.Items.Add("규격 검색");
            cmb_cd_srch.SelectedIndex = 0;
        }
        #region button logic

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            grdListPrint();
        }
        #endregion button logic

        private void grdListPrint()
        {
            wnDm wDm = new wnDm();
            DataTable dt = null;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" where 1=1 ");
            if (chk_stock.Checked == true)
            {
                sb.AppendLine(" and ISNULL(B.INPUT_AMT,0) - ISNULL(C.OUTPUT_AMT,0) > 0 ");
            }

            if (cmb_cd_srch.SelectedIndex == 0)
            {
                sb.AppendLine("and ( A.RAW_MAT_NM like '%" + txt_srch.Text.ToString() + "%' OR A.LABEL_NM like '%" + txt_srch.Text.ToString() + "%' OR A.RAW_MAT_CD like '%" + txt_srch.Text.ToString() + "%' ) ");
                
            }
            else if (cmb_cd_srch.SelectedIndex == 1)
            {
                if (!txt_srch.Text.ToString().Equals(""))
                {
                    sb.AppendLine("and A.RAW_MAT_CD like '%" + txt_srch.Text.ToString() + "%' ");
                }

            }
            else if (cmb_cd_srch.SelectedIndex == 2)
            {
                if (!txt_srch.Text.ToString().Equals(""))
                {
                    sb.AppendLine("and ( A.RAW_MAT_NM like '%" + txt_srch.Text.ToString() + "%' OR A.LABEL_NM like '%" + txt_srch.Text.ToString() + "%' ) ");
                }
            }
            


            rawStockGrid.Rows.Clear();
            rawDetailGrid.Rows.Clear();
            lbl_raw_nm.Text = "";
            dt = wDm.fn_Raw_Stock_List(srch_date.Text.ToString(), sb.ToString());

            adoPrt = new DataTable();
            adoPrt = dt.Copy();

            if (dt != null && dt.Rows.Count > 0)
            {
                rawStockGrid.RowCount = dt.Rows.Count;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["no"] = (i + 1); //숫자의 경우  문자면 .string : 계산된 값을 READ 한 테이블로 다시 전달한다 - 출력물 사용

                    rawStockGrid.Rows[i].Cells["RAW_MAT_CD"].Value = dt.Rows[i]["원부재료코드"].ToString();
                    rawStockGrid.Rows[i].Cells["RAW_MAT_NM"].Value = dt.Rows[i]["원부재료명"].ToString();
                    rawStockGrid.Rows[i].Cells["SPEC"].Value = dt.Rows[i]["규격"].ToString();
                    rawStockGrid.Rows[i].Cells["INPUT_AMT"].Value = (decimal.Parse(dt.Rows[i]["입고량"].ToString())).ToString("#,0.######");
                    rawStockGrid.Rows[i].Cells["OUTPUT_AMT"].Value = (decimal.Parse(dt.Rows[i]["출고량"].ToString())).ToString("#,0.######");
                    rawStockGrid.Rows[i].Cells["STOCK_AMT"].Value = (decimal.Parse(dt.Rows[i]["재고량"].ToString())).ToString("#,0.######");
                    rawStockGrid.Rows[i].Cells["CHUGJONG_NM"].Value = dt.Rows[i]["CHUGJONG_NM"].ToString();
                    rawStockGrid.Rows[i].Cells["CLASS_NM"].Value = dt.Rows[i]["CLASS_NM"].ToString();
                    rawStockGrid.Rows[i].Cells["COUNTRY_NM"].Value = dt.Rows[i]["COUNTRY_NM"].ToString();
                    rawStockGrid.Rows[i].Cells["LABEL_NM"].Value = dt.Rows[i]["LABEL_NM"].ToString();
                    

                    if (decimal.Parse(dt.Rows[i]["재고량"].ToString()) < 0)
                    {
                        rawStockGrid.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    }
                }

                //데이타 끝나고 다시 copy를 써준 이유는 for중에 no에 값을 엏었기 때문에 출력물 데이타테이블(dt)를 다시 복사함
                adoPrt = dt.Copy();
            }
        }

        private void grdCellSetting()
        {
            ComInfo comInfo = new ComInfo();
            comInfo.grdCellSetting(rawStockGrid);
            comInfo.grdCellSetting(rawDetailGrid);
            grdListPrint();
        }

        private void rawStockGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            wnDm wDm = new wnDm();
            DataTable dt = null;

            lbl_raw_nm.Text = rawStockGrid.Rows[e.RowIndex].Cells["RAW_MAT_NM"].Value.ToString();
            dt = wDm.fn_Raw_St_Detail_List(srch_date.Text.ToString(),rawStockGrid.Rows[e.RowIndex].Cells["RAW_MAT_CD"].Value.ToString());

            rawDetailGrid.Rows.Clear();
            if (dt != null && dt.Rows.Count > 0)
            {
                rawDetailGrid.RowCount = dt.Rows.Count;

                decimal t_input_amt = 0;
                decimal t_output_amt = 0;
                decimal t_stock_amt = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rawDetailGrid.Rows[i].Cells["INPUT_CD"].Value = dt.Rows[i]["INPUT_CD"].ToString();
                    rawDetailGrid.Rows[i].Cells["INPUT_SEQ"].Value = dt.Rows[i]["SEQ"].ToString();
                    rawDetailGrid.Rows[i].Cells["INPUT_DATE"].Value = dt.Rows[i]["INPUT_DATE"].ToString();
                    rawDetailGrid.Rows[i].Cells["GRADE_NM"].Value = dt.Rows[i]["GRADE_NM"].ToString();
                    rawDetailGrid.Rows[i].Cells["MF_DATE"].Value = dt.Rows[i]["MF_DATE"].ToString();
                    rawDetailGrid.Rows[i].Cells["EXPRT_DATE"].Value = dt.Rows[i]["EXPRT_DATE"].ToString();
                    rawDetailGrid.Rows[i].Cells["FROZEN_NM"].Value = dt.Rows[i]["FROZEN_NM"].ToString();
                    rawDetailGrid.Rows[i].Cells["LOC_NM"].Value = dt.Rows[i]["LOC_NM"].ToString();
                    rawDetailGrid.Rows[i].Cells["R_INPUT_AMT"].Value = (decimal.Parse(dt.Rows[i]["INPUT_AMT"].ToString())).ToString("#,0.######");
                    rawDetailGrid.Rows[i].Cells["R_OUTPUT_AMT"].Value = (decimal.Parse(dt.Rows[i]["OUTPUT_AMT"].ToString())).ToString("#,0.######");
                    rawDetailGrid.Rows[i].Cells["R_STOCK_AMT"].Value = (decimal.Parse(dt.Rows[i]["STOCK_AMT"].ToString())).ToString("#,0.######");


                    rawDetailGrid.Rows[i].Cells["R_INPUT_AMT"].Style.ForeColor = Color.Blue;
                    rawDetailGrid.Rows[i].Cells["R_OUTPUT_AMT"].Style.ForeColor = Color.Blue;
                    rawDetailGrid.Rows[i].Cells["R_STOCK_AMT"].Style.ForeColor = Color.Blue;

                    t_input_amt += decimal.Parse(dt.Rows[i]["INPUT_AMT"].ToString());
                    t_output_amt += decimal.Parse(dt.Rows[i]["OUTPUT_AMT"].ToString());
                    t_stock_amt += decimal.Parse(dt.Rows[i]["STOCK_AMT"].ToString());
                }

                rawDetailGrid.Rows.Add();
                rawDetailGrid.Rows[rawDetailGrid.Rows.Count - 1].Cells[0].Value = "합계";
                rawDetailGrid.Rows[rawDetailGrid.Rows.Count - 1].Cells["R_INPUT_AMT"].Value = t_input_amt.ToString("#,0.######");
                rawDetailGrid.Rows[rawDetailGrid.Rows.Count - 1].Cells["R_OUTPUT_AMT"].Value = t_output_amt.ToString("#,0.######");
                rawDetailGrid.Rows[rawDetailGrid.Rows.Count - 1].Cells["R_STOCK_AMT"].Value = t_stock_amt.ToString("#,0.######");

                rawDetailGrid.Rows[rawDetailGrid.Rows.Count - 1].Cells[0].Style.ForeColor = Color.CadetBlue;
                rawDetailGrid.Rows[rawDetailGrid.Rows.Count - 1].Cells["R_INPUT_AMT"].Style.ForeColor = Color.Red;
                rawDetailGrid.Rows[rawDetailGrid.Rows.Count - 1].Cells["R_OUTPUT_AMT"].Style.ForeColor = Color.Red;
                rawDetailGrid.Rows[rawDetailGrid.Rows.Count - 1].Cells["R_STOCK_AMT"].Style.ForeColor = Color.Red;
            }
        }

        private void btn출력_Click(object sender, EventArgs e)
        {
            btn출력.Enabled = false;

            strCondition = "";

            if (rawStockGrid.Rows.Count == 0)
            {
                MessageBox.Show("출력할 자료가 없습니다.");
                btn출력.Enabled = true;
                return;
            }

            strDay1 = srch_date.Text;
            strCondition = "원자재재고현황";

            frmPrt = readyPrt;
            frmPrt.Show();
            frmPrt.BringToFront();
            //frmPrt.prt_원자재재고현황(adoPrt, strDay1, strDay2, strCust, strCondition);
            frmPrt.prt_원자재재고현황(adoPrt, strDay1, strCondition);

            btn출력.Enabled = true;
        }

        private void txt_srch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                grdListPrint();
            }
        }
    }
}
