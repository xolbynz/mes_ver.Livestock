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
    public partial class frm원자재투입현황 : Form
    {
        Popup.frmPrint readyPrt = new Popup.frmPrint();
        private wnGConstant wConst = new wnGConstant();

        DataTable adoPrt = null;
        wnAdo wAdo = new wnAdo();
        public Popup.frmPrint frmPrt;

        public string strDay1 = "";
        public string strDay2 = "";
        public string strCondition = "";

        public frm원자재투입현황()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm원자재투입현황_Load(object sender, EventArgs e)
        {
            start_date.Text = DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd");
            ComInfo.gridHeaderSet(rawOutGrid);

            init_ComboBox();
        }

        private void init_ComboBox()
        {
            cmb_cd_srch.Items.Add("전체 검색");
            cmb_cd_srch.Items.Add("코드별 검색");
            cmb_cd_srch.Items.Add("원자재명 검색");
            cmb_cd_srch.Items.Add("부위 검색");
            cmb_cd_srch.Items.Add("원산지 검색");
            cmb_cd_srch.Items.Add("유형 검색");
            cmb_cd_srch.Items.Add("축종 검색");
            cmb_cd_srch.Items.Add("등급 검색");
            cmb_cd_srch.Items.Add("단위 검색");
            cmb_cd_srch.SelectedIndex = 0;
        }

        private void btnSrch_Click(object sender, EventArgs e) // 2019-12-27 문세진 제품생산에 쓰이는 원자재만 검색되도록 , 원자재 특징별로 검색수정
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("where 1=1 ");
                sb.AppendLine("and A.OUTPUT_GUBUN = 1");
                sb.AppendLine("and A.OUTPUT_DATE >= '" + start_date.Text.ToString() + "' and  A.OUTPUT_DATE <= '" + end_date.Text.ToString() + "'");

                if (cmb_cd_srch.SelectedIndex == 0)
                {
                    sb.AppendLine("and ( B.RAW_MAT_NM like '%" + txt_srch.Text.ToString() + "%' OR B.LABEL_NM like '%" + txt_srch.Text.ToString() + "%'  )  ");
                }
                else if (cmb_cd_srch.SelectedIndex == 1)
                {
                    if (txt_srch.Text.ToString().Equals(""))
                    {
                        MessageBox.Show("코드명을 입력하시기 바랍니다.");
                        return;
                    }
                    sb.AppendLine("and A.RAW_MAT_CD like '%" + txt_srch.Text.ToString() + "%' ");

                }
                else if (cmb_cd_srch.SelectedIndex == 2)
                {
                    if (txt_srch.Text.ToString().Equals(""))
                    {
                        MessageBox.Show("원자재명을 입력하시기 바랍니다.");
                        return;
                    }
                    sb.AppendLine("and B.RAW_MAT_NM like '%" + txt_srch.Text.ToString() + "%' ");
                }
                else if (cmb_cd_srch.SelectedIndex == 3)
                {
                    if (txt_srch.Text.ToString().Equals(""))
                    {
                        MessageBox.Show("부위을 입력하시기 바랍니다.");
                        return;
                    }
                    sb.AppendLine(" and G.CLASS_NM like '%" + txt_srch.Text.ToString() + "%' ");
                }
                else if (cmb_cd_srch.SelectedIndex == 4)
                {
                    if (txt_srch.Text.ToString().Equals(""))
                    {
                        MessageBox.Show("원산지을 입력하시기 바랍니다.");
                        return;
                    }
                    sb.AppendLine(" and I.COUNTRY_NM like '%" + txt_srch.Text.ToString() + "%' ");
                }
                else if (cmb_cd_srch.SelectedIndex == 5)
                {
                    if (txt_srch.Text.ToString().Equals(""))
                    {
                        MessageBox.Show("유형을 입력하시기 바랍니다.");
                        return;
                    }
                    sb.AppendLine(" and J.TYPE_NM like '%" + txt_srch.Text.ToString() + "%' ");
                }
                else if (cmb_cd_srch.SelectedIndex == 6)
                {
                    if (txt_srch.Text.ToString().Equals(""))
                    {
                        MessageBox.Show("축종을 입력하시기 바랍니다.");
                        return;
                    }
                    sb.AppendLine(" and E.CHUGJONG_NM like '%" + txt_srch.Text.ToString() + "%' ");
                }
                else if (cmb_cd_srch.SelectedIndex == 7)
                {
                    if (txt_srch.Text.ToString().Equals(""))
                    {
                        MessageBox.Show("등급을 입력하시기 바랍니다.");
                        return;
                    }
                    sb.AppendLine(" and H.GRADE_NM like '%" + txt_srch.Text.ToString() + "%' ");
                }
                else if (cmb_cd_srch.SelectedIndex == 8)
                {
                    if (txt_srch.Text.ToString().Equals(""))
                    {
                        MessageBox.Show("단위을 입력하시기 바랍니다.");
                        return;
                    }
                    sb.AppendLine(" and D.UNIT_NM like '%" + txt_srch.Text.ToString() + "%' ");
                }

                dt = wDm.fn_Raw_Output_List(sb.ToString()); // 2019-12-27 문세진 메서드 수정

                adoPrt = new DataTable();
                adoPrt = dt.Copy();

                this.rawOutGrid.RowCount = dt.Rows.Count;
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i]["no"] = (i + 1); //숫자의 경우  문자면 .string : 계산된 값을 READ 한 테이블로 다시 전달한다 - 출력물 사용

                        rawOutGrid.Rows[i].Cells[0].Value = (i + 1).ToString();
                        rawOutGrid.Rows[i].Cells["OUTPUT_DATE"].Value = dt.Rows[i]["출고일자"].ToString();
                        rawOutGrid.Rows[i].Cells["RAW_MAT_CD"].Value = dt.Rows[i]["원부재료코드"].ToString();
                        rawOutGrid.Rows[i].Cells["RAW_MAT_NM"].Value = dt.Rows[i]["원부재료명"].ToString();
                        rawOutGrid.Rows[i].Cells["SPEC"].Value = dt.Rows[i]["규격"].ToString();
                        rawOutGrid.Rows[i].Cells["LOT_NO"].Value = dt.Rows[i]["제조번호"].ToString();
                        rawOutGrid.Rows[i].Cells["TOTAL_AMT"].Value = (decimal.Parse(dt.Rows[i]["투입량"].ToString())).ToString("#,0.######");
                        rawOutGrid.Rows[i].Cells["TYPE"].Value = dt.Rows[i]["유형"].ToString();
                        rawOutGrid.Rows[i].Cells["GRADE"].Value = dt.Rows[i]["등급"].ToString();
                        rawOutGrid.Rows[i].Cells["CLASS"].Value = dt.Rows[i]["부위"].ToString();
                        rawOutGrid.Rows[i].Cells["CHUGJONG"].Value = dt.Rows[i]["축종"].ToString();
                        rawOutGrid.Rows[i].Cells["UNIT"].Value = dt.Rows[i]["단위명"].ToString();
                        rawOutGrid.Rows[i].Cells["COUNTRY"].Value = dt.Rows[i]["원산지"].ToString();
                        rawOutGrid.Rows[i].Cells["INPUT_DATE"].Value = dt.Rows[i]["입고일자"].ToString();
                        rawOutGrid.Rows[i].Cells["INPUT_CD"].Value = dt.Rows[i]["입고번호"].ToString();
                        rawOutGrid.Rows[i].Cells["BAR_NUM"].Value = dt.Rows[i]["입고일자"].ToString().Replace("-", "")
                            + int.Parse(dt.Rows[i]["입고번호"].ToString()).ToString("D4") + int.Parse(dt.Rows[i]["입고순번"].ToString()).ToString("D2");
                    }
                }
                else
                {
                    rawOutGrid.Rows.Clear();
                }

                //데이타 끝나고 다시 copy를 써준 이유는 for중에 no에 값을 엏었기 때문에 출력물 데이타테이블(dt)를 다시 복사함
                adoPrt = dt.Copy();
            }
            catch (Exception ex)
            {
                MessageBox.Show("시스템 오류" + ex.ToString());
            }
        }

        private void btn출력_Click(object sender, EventArgs e)
        {
            btn출력.Enabled = false;

            strCondition = "";

            if (rawOutGrid.Rows.Count == 0)
            {
                MessageBox.Show("출력할 자료가 없습니다.");
                btn출력.Enabled = true;
                return;
            }

            strDay1 = start_date.Text;
            strDay2 = end_date.Text;
            strCondition = "원자재투입현황";

            frmPrt = readyPrt;
            frmPrt.Show();
            frmPrt.BringToFront();
            //frmPrt.prt_원자재재고현황(adoPrt, strDay1, strDay2, strCust, strCondition);
            frmPrt.prt_원자재투입현황(adoPrt, strDay1, strDay2, strCondition);

            btn출력.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void txt_srch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    wnDm wDm = new wnDm();
                    DataTable dt = null;

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("where 1=1 ");
                    sb.AppendLine("and A.OUTPUT_GUBUN = 1");
                    sb.AppendLine("and A.OUTPUT_DATE >= '" + start_date.Text.ToString() + "' and  A.OUTPUT_DATE <= '" + end_date.Text.ToString() + "'");

                    if (cmb_cd_srch.SelectedIndex == 0)
                    {
                        sb.AppendLine("");
                    }
                    else if (cmb_cd_srch.SelectedIndex == 1)
                    {
                        if (txt_srch.Text.ToString().Equals(""))
                        {
                            MessageBox.Show("코드명을 입력하시기 바랍니다.");
                            return;
                        }
                        sb.AppendLine("and A.RAW_MAT_CD like '%" + txt_srch.Text.ToString() + "%' ");

                    }
                    else if (cmb_cd_srch.SelectedIndex == 2)
                    {
                        if (txt_srch.Text.ToString().Equals(""))
                        {
                            MessageBox.Show("원자재명을 입력하시기 바랍니다.");
                            return;
                        }
                        sb.AppendLine("and ( B.RAW_MAT_NM like '%" + txt_srch.Text.ToString() + "%' OR B.LABEL_NM like '%" + txt_srch.Text.ToString() + "%'  ) ");
                    }
                    else if (cmb_cd_srch.SelectedIndex == 3)
                    {
                        if (txt_srch.Text.ToString().Equals(""))
                        {
                            MessageBox.Show("부위을 입력하시기 바랍니다.");
                            return;
                        }
                        sb.AppendLine(" and G.CLASS_NM like '%" + txt_srch.Text.ToString() + "%' ");
                    }
                    else if (cmb_cd_srch.SelectedIndex == 4)
                    {
                        if (txt_srch.Text.ToString().Equals(""))
                        {
                            MessageBox.Show("원산지을 입력하시기 바랍니다.");
                            return;
                        }
                        sb.AppendLine(" and I.COUNTRY_NM like '%" + txt_srch.Text.ToString() + "%' ");
                    }
                    else if (cmb_cd_srch.SelectedIndex == 5)
                    {
                        if (txt_srch.Text.ToString().Equals(""))
                        {
                            MessageBox.Show("유형을 입력하시기 바랍니다.");
                            return;
                        }
                        sb.AppendLine(" and J.TYPE_NM like '%" + txt_srch.Text.ToString() + "%' ");
                    }
                    else if (cmb_cd_srch.SelectedIndex == 6)
                    {
                        if (txt_srch.Text.ToString().Equals(""))
                        {
                            MessageBox.Show("축종을 입력하시기 바랍니다.");
                            return;
                        }
                        sb.AppendLine(" and E.CHUGJONG_NM like '%" + txt_srch.Text.ToString() + "%' ");
                    }
                    else if (cmb_cd_srch.SelectedIndex == 7)
                    {
                        if (txt_srch.Text.ToString().Equals(""))
                        {
                            MessageBox.Show("등급을 입력하시기 바랍니다.");
                            return;
                        }
                        sb.AppendLine(" and H.GRADE_NM like '%" + txt_srch.Text.ToString() + "%' ");
                    }
                    else if (cmb_cd_srch.SelectedIndex == 8)
                    {
                        if (txt_srch.Text.ToString().Equals(""))
                        {
                            MessageBox.Show("단위을 입력하시기 바랍니다.");
                            return;
                        }
                        sb.AppendLine(" and D.UNIT_NM like '%" + txt_srch.Text.ToString() + "%' ");
                    }

                    dt = wDm.fn_Raw_Output_List(sb.ToString()); // 2019-12-27 문세진 메서드 수정

                    adoPrt = new DataTable();
                    adoPrt = dt.Copy();

                    this.rawOutGrid.RowCount = dt.Rows.Count;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dt.Rows[i]["no"] = (i + 1); //숫자의 경우  문자면 .string : 계산된 값을 READ 한 테이블로 다시 전달한다 - 출력물 사용

                            rawOutGrid.Rows[i].Cells[0].Value = (i + 1).ToString();
                            rawOutGrid.Rows[i].Cells["OUTPUT_DATE"].Value = dt.Rows[i]["출고일자"].ToString();
                            rawOutGrid.Rows[i].Cells["RAW_MAT_CD"].Value = dt.Rows[i]["원부재료코드"].ToString();
                            rawOutGrid.Rows[i].Cells["RAW_MAT_NM"].Value = dt.Rows[i]["원부재료명"].ToString();
                            rawOutGrid.Rows[i].Cells["SPEC"].Value = dt.Rows[i]["규격"].ToString();
                            rawOutGrid.Rows[i].Cells["LOT_NO"].Value = dt.Rows[i]["제조번호"].ToString();
                            rawOutGrid.Rows[i].Cells["TOTAL_AMT"].Value = (decimal.Parse(dt.Rows[i]["투입량"].ToString())).ToString("#,0.######");
                            rawOutGrid.Rows[i].Cells["TYPE"].Value = dt.Rows[i]["유형"].ToString();
                            rawOutGrid.Rows[i].Cells["GRADE"].Value = dt.Rows[i]["등급"].ToString();
                            rawOutGrid.Rows[i].Cells["CLASS"].Value = dt.Rows[i]["부위"].ToString();
                            rawOutGrid.Rows[i].Cells["CHUGJONG"].Value = dt.Rows[i]["축종"].ToString();
                            rawOutGrid.Rows[i].Cells["UNIT"].Value = dt.Rows[i]["단위명"].ToString();
                            rawOutGrid.Rows[i].Cells["COUNTRY"].Value = dt.Rows[i]["원산지"].ToString();
                            rawOutGrid.Rows[i].Cells["INPUT_DATE"].Value = dt.Rows[i]["입고일자"].ToString();
                            rawOutGrid.Rows[i].Cells["INPUT_CD"].Value = dt.Rows[i]["입고번호"].ToString();
                            rawOutGrid.Rows[i].Cells["BAR_NUM"].Value = dt.Rows[i]["입고일자"].ToString().Replace("-", "")
                                + int.Parse(dt.Rows[i]["입고번호"].ToString()).ToString("D4") + int.Parse(dt.Rows[i]["입고순번"].ToString()).ToString("D2");
                        }
                    }
                    else
                    {
                        rawOutGrid.Rows.Clear();
                    }

                    //데이타 끝나고 다시 copy를 써준 이유는 for중에 no에 값을 엏었기 때문에 출력물 데이타테이블(dt)를 다시 복사함
                    adoPrt = dt.Copy();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("시스템 오류" + ex.ToString());
                }
            }
        }
    }
}
