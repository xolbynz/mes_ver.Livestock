using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 스마트팩토리.CLS;

namespace 스마트팩토리.P10_ORD
{
    public partial class frm반품조회 : Form
    {
        private wnGConstant wConst = new wnGConstant();

        public frm반품조회()
        {
            InitializeComponent();
        }

        private void frm반품조회_Load(object sender, EventArgs e)
        {
            lblSearch.BringToFront();

            this.init_ComboBox();
            btnSearch_Click(this, null);
        }

        private void init_ComboBox()
        {
            string sqlQuery = "";

            cmbS구분.ValueMember = "코드";
            cmbS구분.DisplayMember = "명칭";
            sqlQuery = " select '1' as 코드, '영업소접수만' as 명칭 "; //요청일자
            sqlQuery += " union all ";
            sqlQuery += " select '2' as 코드, '창고접수' as 명칭 "; //창고접수일자
            sqlQuery += " union all ";
            sqlQuery += " select '3' as 코드, '관리부접수' as 명칭 "; //관리부처리일자
            wConst.ComboBox_Read_ALL(cmbS구분, sqlQuery);
        }

        private string makeSearchCondition()
        {
            StringBuilder sb = new StringBuilder();

            switch ("" + cmbS구분.SelectedValue.ToString())
            {
                case "1":
                    sb.Append(" and a.반품일자 + 반품번호 >= '" + dtpDay1.Text + txtNum1.Text + "' ");
                    sb.Append(" and a.반품일자 + 반품번호 <= '" + dtpDay2.Text + txtNum2.Text + "' ");
                    sb.Append(" and a.창고확인 = '미확인' and a.관리부확인 = '미확인' ");
                    break;
                case "2":
                    sb.Append(" and a.창고확인일자 >= '" + dtpDay1.Text + "' ");
                    sb.Append(" and a.창고확인일자 <= '" + dtpDay2.Text + "' ");
                    sb.Append(" and a.창고확인 = '확인' ");
                    break;
                case "3":
                    sb.Append(" and a.관리부확인일자 >= '" + dtpDay1.Text + "' ");
                    sb.Append(" and a.관리부확인일자 <= '" + dtpDay2.Text + "' ");
                    sb.Append(" and a.창고확인 = '확인' and a.관리부확인 = '확인' ");
                    break;
                default:
                    txtNum1.Text = "0000";
                    txtNum2.Text = "9999";
                    sb.Append(" and a.반품일자 >= '" + dtpDay1.Text + "' ");
                    sb.Append(" and a.반품일자 <= '" + dtpDay2.Text + "' ");
                    break;
            }

            switch (this.txtS거래처명.Text)
            {
                case "":
                    sb.Append("");
                    break;
                default:
                    sb.Append(" and a.거래처명 like '" + txtS거래처명.Text + "%' ");
                    break;
            }

            switch (this.txtS제품명.Text)
            {
                case "":
                    sb.Append("");
                    break;
                default:
                    sb.Append(" and a.제품명 like '" + txtS제품명.Text + "%' ");
                    break;
            }

            switch (this.txtS담당자명.Text)
            {
                case "":
                    sb.Append("");
                    break;
                default:
                    sb.Append(" and a.담당자명 like '" + txtS담당자명.Text + "%' ");
                    break;
            }

            return sb.ToString();
        }

        public void bindData(string condition)
        {
            lblSearch.BringToFront();
            lblSearch.Left = spCont.Panel1.Width / 2 - lblSearch.Width / 2;
            lblSearch.Top = spCont.Panel1.Height / 2 - lblSearch.Height / 2;
            lblSearch.Visible = true;
            Application.DoEvents();

            this.GridRecord.DataSource = null;
            this.GridRecord.RowCount = 0;
            //lblCount.Text = "0 건";

            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_반품조회_List(condition);

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.GridRecord.RowCount = dt.Rows.Count;
                    //this.lblCount.Text = dt.Rows.Count.ToString("#,0") + " 건";

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.GridRecord.Rows[i].Cells[0].Value = (i + 1).ToString();
                        this.GridRecord.Rows[i].Cells[1].Value = dt.Rows[i]["반품일자"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[2].Value = dt.Rows[i]["반품번호"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[3].Value = dt.Rows[i]["거래처코드"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[4].Value = dt.Rows[i]["거래처명"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[5].Value = dt.Rows[i]["대표자명"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[6].Value = dt.Rows[i]["담당자코드"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[7].Value = dt.Rows[i]["담당자명"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[8].Value = dt.Rows[i]["반품사유"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[9].Value = dt.Rows[i]["반품사유내용"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[10].Value = dt.Rows[i]["제품코드"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[11].Value = dt.Rows[i]["제품명"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[12].Value = dt.Rows[i]["규격"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[13].Value = dt.Rows[i]["창고비고"].ToString().Trim();

                        this.GridRecord.Rows[i].Cells[14].Value = decimal.Parse(dt.Rows[i]["입력수량"].ToString().Trim()).ToString("#,0");
                        this.GridRecord.Rows[i].Cells[15].Value = decimal.Parse(dt.Rows[i]["입력할증수량"].ToString().Trim()).ToString("#,0");
                        this.GridRecord.Rows[i].Cells[16].Value = decimal.Parse(dt.Rows[i]["입력단가"].ToString().Trim()).ToString("#,0");
                        this.GridRecord.Rows[i].Cells[17].Value = decimal.Parse(dt.Rows[i]["입력금액"].ToString().Trim()).ToString("#,0");

                        this.GridRecord.Rows[i].Cells[18].Value = dt.Rows[i]["주문일자"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[19].Value = dt.Rows[i]["주문번호"].ToString().Trim();
                    }

                    ////wConst.mergeCells(GridRecord, 1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("검색중에 오류가 발생했습니다.");
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
            }

            lblSearch.Visible = false;
        }

        public void bindData_Sub(string sKey, string sKey2)
        {
            this.grdItem.DataSource = null;
            this.grdItem.RowCount = 0;
            //lblCount.Text = "0 건";

            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_PRODUCT_RETURN_Detail(sKey, sKey2);

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.grdItem.RowCount = dt.Rows.Count;
                    //this.lblCount.Text = dt.Rows.Count.ToString("#,0") + " 건";

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.grdItem.Rows[i].Cells[0].Value = (i + 1).ToString();
                        ////this.grdItem.Rows[i].Cells[1].Value = dt.Rows[i]["반품일자"].ToString().Trim();
                        ////this.grdItem.Rows[i].Cells[2].Value = dt.Rows[i]["반품번호"].ToString().Trim();
                        ////this.grdItem.Rows[i].Cells[3].Value = dt.Rows[i]["거래처코드"].ToString().Trim();
                        ////this.grdItem.Rows[i].Cells[4].Value = dt.Rows[i]["거래처명"].ToString().Trim();
                        ////this.grdItem.Rows[i].Cells[5].Value = dt.Rows[i]["대표자명"].ToString().Trim();
                        ////this.grdItem.Rows[i].Cells[6].Value = dt.Rows[i]["담당자코드"].ToString().Trim();
                        ////this.grdItem.Rows[i].Cells[7].Value = dt.Rows[i]["담당자명"].ToString().Trim();
                        ////this.grdItem.Rows[i].Cells[8].Value = dt.Rows[i]["반품사유"].ToString().Trim();
                        ////this.grdItem.Rows[i].Cells[9].Value = dt.Rows[i]["반품사유내용"].ToString().Trim();
                        //this.grdItem.Rows[i].Cells[10].Value = dt.Rows[i]["제품코드"].ToString().Trim();
                        //this.grdItem.Rows[i].Cells[11].Value = dt.Rows[i]["제품명"].ToString().Trim();
                        //this.grdItem.Rows[i].Cells[12].Value = dt.Rows[i]["규격"].ToString().Trim();
                        ////this.grdItem.Rows[i].Cells[13].Value = dt.Rows[i]["창고비고"].ToString().Trim();

                        //this.grdItem.Rows[i].Cells[14].Value = decimal.Parse(dt.Rows[i]["입력수량"].ToString().Trim()).ToString("#,0");
                        //this.grdItem.Rows[i].Cells[15].Value = decimal.Parse(dt.Rows[i]["입력할증수량"].ToString().Trim()).ToString("#,0");
                        //this.grdItem.Rows[i].Cells[16].Value = decimal.Parse(dt.Rows[i]["입력단가"].ToString().Trim()).ToString("#,0");
                        //this.grdItem.Rows[i].Cells[17].Value = decimal.Parse(dt.Rows[i]["입력금액"].ToString().Trim()).ToString("#,0");

                        //this.grdItem.Rows[i].Cells[18].Value = dt.Rows[i]["창고확인일자"].ToString().Trim();
                        //this.grdItem.Rows[i].Cells[19].Value = dt.Rows[i]["창고입고수량"].ToString().Trim();
                        //this.grdItem.Rows[i].Cells[19].Value = dt.Rows[i]["창고비고"].ToString().Trim();
                        //this.grdItem.Rows[i].Cells[19].Value = dt.Rows[i]["관리부확인일자"].ToString().Trim();
                        //this.grdItem.Rows[i].Cells[19].Value = dt.Rows[i]["관리부확인일자"].ToString().Trim();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("검색중에 오류가 발생했습니다.");
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
            }

            lblSearch.Visible = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.bindData(this.makeSearchCondition());

        }

        private void btn엑셀_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GridRecord_DoubleClick(object sender, EventArgs e)
        {
            if (GridRecord.CurrentCell == null) return;
            if (GridRecord.CurrentCell.RowIndex < 0) return;
            if (GridRecord.CurrentCell.ColumnIndex < 0) return;

            int nCnt = GridRecord.CurrentCell.RowIndex;
            int nKeyCol = 1;
            int nKeyCol2 = 2;
            string sValue = "" + GridRecord.Rows[nCnt].Cells[nKeyCol].Value.ToString();
            string sValue2 = "" + GridRecord.Rows[nCnt].Cells[nKeyCol2].Value.ToString();

            if (sValue == "") return;

            call_Item(sValue, sValue2);
        }

        private void GridRecord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                e.Handled = true;

                if (GridRecord.CurrentCell == null) return;
                if (GridRecord.CurrentCell.RowIndex < 0) return;
                if (GridRecord.CurrentCell.ColumnIndex < 0) return;

                int nCnt = GridRecord.CurrentCell.RowIndex;
                int nKeyCol = 1;
                int nKeyCol2 = 2;
                string sValue = "" + GridRecord.Rows[nCnt].Cells[nKeyCol].Value.ToString();
                string sValue2 = "" + GridRecord.Rows[nCnt].Cells[nKeyCol2].Value.ToString();

                if (sValue == "") return;

                call_Item(sValue, sValue2);
            }

            if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;

                dtpDay1.Focus();
            }
        }

        private void call_Item(string sKey, string sKey2)
        {
            //init_InputBox(false);

            //lblSaving.Left = spCont.Panel1.ClientSize.Width / 2 - lblSaving.Width / 2;
            //lblSaving.Top = spCont.Panel1.ClientSize.Height / 2 - lblSaving.Height / 2;
            //lblSaving.Text = "Loading...";
            //lblSaving.Visible = true;
            //lblSaving.BringToFront();
            //Application.DoEvents();

            bindData_Sub(sKey, sKey2);

            //lblSaving.Visible = false;

            //tmFocus.Enabled = true;
        }

    }
}
