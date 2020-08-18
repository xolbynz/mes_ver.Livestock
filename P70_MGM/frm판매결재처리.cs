using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 스마트팩토리.CLS;
using 스마트팩토리.Controls;

namespace 스마트팩토리.P70_MGM
{
    public partial class frm판매결재처리 : Form
    {
        private wnGConstant wConst = new wnGConstant();
        private bool bData = false;
        private bool bEditText = false;
        //private int currRow = -1;
        //private int currCol = -1;
        private string srchDay = "";
        private string srchCust = "";

        private string sProgFolder = "";
        private string sBodyHH = "";
        private string sColumnsWW = "";
        private string sSettingFile = "order_sign.xml";
        private bool bSetFirst = true;

        public frm판매결재처리()
        {
            InitializeComponent();

            this.spCont.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.spCont_SplitterMoved);
            this.grdItem.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.grid_ColumnWidthChanged);
            this.grdItem.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.grid_RowsAdded);
        }

        #region 입력 그리드 제어 #####################################################################################################

        private void Read_Layout()
        {
            try
            {
                Hashtable htDB = new Hashtable();
                SettingXML sXml = new SettingXML(sProgFolder + "\\" + sSettingFile);
                htDB = sXml.ReadXml();

                foreach (DictionaryEntry entry in htDB)
                {
                    if (entry.Key.ToString().Equals("BodyHeight"))
                    {
                        sBodyHH = entry.Value.ToString();
                    }
                    if (entry.Key.ToString().Equals("GridWidth"))
                    {
                        sColumnsWW = entry.Value.ToString();
                    }
                }
            }
            catch
            {
            }
        }

        private void Save_Layout()
        {
            Hashtable htDB = new Hashtable();
            htDB["BodyHeight"] = sBodyHH;
            htDB["GridWidth"] = sColumnsWW;
            SettingXML sXml = new SettingXML(sProgFolder + "\\" + sSettingFile);
            sXml.WriteXML(htDB);
        }

        private void setting_Layout()
        {
            if (sBodyHH != "")
            {
                if (float.Parse(sBodyHH) > 0)
                {
                    float nHH = float.Parse(sBodyHH) / 100 * spCont.Height;
                    spCont.SplitterDistance = (int)nHH;
                }
            }
            if (sColumnsWW != "")
            {
                string[] sWW = sColumnsWW.Split(',');

                for (int kk = 0; kk < sWW.Length; kk++)
                {
                    try
                    {
                        grdItem.Columns[kk].Width = int.Parse(sWW[kk]);
                    }
                    catch
                    {
                    }
                }
            }
            bSetFirst = false;
        }

        private void spCont_SplitterMoved(object sender, SplitterEventArgs e)
        {
            float nHH = (float)spCont.SplitterDistance / spCont.Height * 100;
            sBodyHH = nHH.ToString("#0.00");

            Save_Layout();
        }

        private void grid_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            // 폼 로딩시에는, 저장 안함.
            if (bSetFirst == true) return;

            conDataGridView grd = (conDataGridView)sender;

            sColumnsWW = "";
            for (int kk = 0; kk < grd.Columns.Count; kk++)
            {
                if (kk > 0)
                {
                    sColumnsWW += ",";
                }
                sColumnsWW += grd.Columns[kk].Width.ToString();
            }
            Save_Layout();
        }

        private void grid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DataGridView grd = (DataGridView)sender;

            // 할증체크
            grd.Rows[e.RowIndex].Cells[14].Style.BackColor = Color.WhiteSmoke;
            grd.Rows[e.RowIndex].Cells[14].Style.SelectionBackColor = Color.Khaki;

            // 단가체크
            grd.Rows[e.RowIndex].Cells[15].Style.BackColor = Color.WhiteSmoke;
            grd.Rows[e.RowIndex].Cells[15].Style.SelectionBackColor = Color.Khaki;

            // 회전율체크
            grd.Rows[e.RowIndex].Cells[16].Style.BackColor = Color.WhiteSmoke;
            grd.Rows[e.RowIndex].Cells[16].Style.SelectionBackColor = Color.Khaki;

        }

        #endregion 입력 그리드 제어 #####################################################################################################

        private void frm판매결재처리_Load(object sender, EventArgs e)
        {
            lblSaving.BringToFront();
            lblSearch.BringToFront();

            sProgFolder = Path.GetDirectoryName(System.Environment.GetCommandLineArgs()[0]);
            Read_Layout();
            setting_Layout();

            dtp일자.Text = DateTime.Now.ToString("yyyy-MM-dd");
            dtpDay1.Text = DateTime.Now.ToString("yyyy-MM-dd");

            string sqlQuery = "";
            cmbS결재구분.ValueMember = "코드";
            cmbS결재구분.DisplayMember = "명칭";
            sqlQuery = " select '0' as 코드, '미결' as 명칭 ";
            sqlQuery += " union all ";
            sqlQuery += " select '1' as 코드, '완결' as 명칭 ";
            wConst.ComboBox_Read_ALL(cmbS결재구분, sqlQuery);

            this.init_InputBox(true);
            btnSearch_Click(this, null);
        }

        public void init_InputBox(bool bNew)
        {
            wConst.Form_Clear(spCont.Panel1.Controls);
            txt비고.Text = "";
            txt비고1.Text = "";
            txt비고2.Text = "";
            txt비고3.Text = "";
            txt비고4.Text = "";

            grdItem.Rows.Clear();

            btnCancel.Enabled = false;
            btnSave.Enabled = false;
            btn출력.Enabled = false;

            get_ComboBox(bNew);

            if (bNew == true)
            {
                bData = false;
                btnDelete.Enabled = false;
                dtp일자.Enabled = true;
                rb급여.Checked = true;
                txt배송.Text = "01";  // 01=택배

                cmb구분.Enabled = false;
                cmb구분.SelectedValue = "11";
            }
            else
            {
                bData = true;
                btnCancel.Enabled = true;
                btnSave.Enabled = true;
                btn출력.Enabled = true;
                btnDelete.Enabled = true;
                dtp일자.Enabled = false;
                //txt거래처명.ReadOnly = true;
                //btn거래처.Enabled = false;

                cmb구분.Enabled = false;
            }
        }

        private void get_ComboBox(bool bNew)
        {
            string sqlQuery = "";

            wConst.set_Combo판매구분(false, cmb구분, bNew);

            //cmb지역.ValueMember = "코드";
            //cmb지역.DisplayMember = "명칭";
            //sqlQuery = " select AreaCode as 코드, AreaName as 명칭 from T_Area where 1=1 ";
            //wConst.ComboBox_Read_NoBlank(cmb지역, sqlQuery);

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.bindData(this.makeSearchCondition());
            tmFocusList.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            init_InputBox(true);
            tmFocusList.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // 보류사항) 재고가 없으면 미송테이블(product_store)에 저장하고, 영업주문소(product_stock_dept)에서 삭제한다

            btnSave.Enabled = false;
            if (validate_InputBox() == false)
            {
                // 필수 입력 체크.
                btnSave.Enabled = true;
                return;
            }

            lblSaving.Left = spCont.Panel1.ClientSize.Width / 2 - lblSaving.Width / 2;
            lblSaving.Top = spCont.Panel1.ClientSize.Height / 2 - lblSaving.Height / 2;
            lblSaving.Text = "Saving...";
            lblSaving.Visible = true;
            lblSaving.BringToFront();
            Application.DoEvents();

            bool bResult = false;

            if (bData == false)
            {
            }
            else
            {
                bResult = this.updatePost();
            }

            if (bResult == true)
            {
                btnSearch_Click(this, null);
                btnCancel_Click(this, null);
            }
            else
            {
                call_Item(dtp일자.Text, lbl번호.Text);
            }

            lblSaving.Visible = false;
            btnSave.Enabled = true;
        }

        private void btnSaveAll_Click(object sender, EventArgs e)
        {
            // 보류사항) 재고가 없으면 미송테이블(product_store)에 저장하고, 영업주문소(product_stock_dept)에서 삭제한다

            btnSaveAll.Enabled = false;
            
            lblSaving.Text = "Saving...";
            lblSaving.Visible = true;
            lblSaving.BringToFront();
            Application.DoEvents();

            bool bResult = false;

            if (bData == false)
            {
            }
            else
            {
                bResult = this.updatePost_all();
            }

            if (bResult == true)
            {
                btnSearch_Click(this, null);
                btnCancel_Click(this, null);
            }

            lblSaving.Visible = false;
            btnSaveAll.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult msgOk = MessageBox.Show("자료를 삭제하시겠습니까?", "삭제여부", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (msgOk == DialogResult.No)
            {
                return;
            }

            bool bResult = this.deletePost();
            if (bResult == true)
            {
                init_InputBox(true);
                btnSearch_Click(this, null);
            }
        }

        private void btn출력_Click(object sender, EventArgs e)
        {
            MessageBox.Show("출력");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string makeSearchCondition()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" and a.STOCK_DATE = '" + dtpDay1.Text + "' ");
            sb.Append(" and a.STOCK_KIND = '11' "); // 판매 건만

            switch (this.txtS거래처코드.Text)
            {
                case "":
                    sb.Append("");
                    break;
                default:
                    sb.Append(" and a.CUST_CODE1 = '" + txtS거래처코드.Text + "' ");
                    break;
            }

            switch ("" + this.cmbS결재구분.SelectedValue.ToString())
            {
                case "0":
                    sb.Append(" and isnull(a.STOCK_LAST1, '0') <> '1' ");
                    break;
                case "1":
                    sb.Append(" and isnull(a.STOCK_LAST1, '0') = '1' ");
                    break;
                default:
                    break;
            }

            return sb.ToString();
        }

        private void bindData(string condition)
        {
            // 판매 영업소주문서만 가져옴.
            // 반품은 영업소반품등록으로 별도 처리.
            lblSearch.Left = spCont.Panel2.ClientSize.Width / 2 - lblSearch.Width / 2;
            lblSearch.Top = spCont.Panel2.ClientSize.Height / 2 - lblSearch.Height / 2;
            lblSearch.Visible = true;
            Application.DoEvents();

            this.GridRecord.DataSource = null;
            this.GridRecord.RowCount = 0;

            srchDay = dtpDay1.Text;
            srchCust = txtS거래처코드.Text;

            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_PRODUCT_STOCK_DEPT_List(condition);

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.GridRecord.RowCount = dt.Rows.Count;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.GridRecord.Rows[i].Cells[0].Value = "";
                        this.GridRecord.Rows[i].Cells[1].Value = (i + 1).ToString();
                        this.GridRecord.Rows[i].Cells[2].Value = dt.Rows[i]["STOCK_DATE"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[3].Value = dt.Rows[i]["STOCK_NUM"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[4].Value = wConst.get_판매구분_명칭(dt.Rows[i]["STOCK_KIND"].ToString().Trim());
                        this.GridRecord.Rows[i].Cells[5].Value = dt.Rows[i]["CUST_CODE1"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[6].Value = dt.Rows[i]["CUST_NAME1"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[7].Value = dt.Rows[i]["STOCK_LAST1"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[8].Value = dt.Rows[i]["TAX_DATE"].ToString().Trim();
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

        private void frm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                e.Handled = true;
                btnCancel_Click(this, null);
            }
            if (e.KeyCode == Keys.F8)
            {
                e.Handled = true;
                if (btnSave.Enabled == true) btnSave_Click(this, null);
            }
            if (e.KeyCode == Keys.F11)
            {
                e.Handled = true;
                if (btnDelete.Enabled == true) btnDelete_Click(this, null);
            }
        }

        private void GridRecord_DoubleClick(object sender, EventArgs e)
        {
            if (GridRecord.CurrentCell == null) return;
            if (GridRecord.CurrentCell.RowIndex < 0) return;
            if (GridRecord.CurrentCell.ColumnIndex < 0) return;

            int nCnt = GridRecord.CurrentCell.RowIndex;
            int nKeyCol = 2;
            int nKeyCol2 = 3;
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
                int nKeyCol = 2;
                int nKeyCol2 = 3;
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
            init_InputBox(false);

            lblSaving.Left = spCont.Panel1.ClientSize.Width / 2 - lblSaving.Width / 2;
            lblSaving.Top = spCont.Panel1.ClientSize.Height / 2 - lblSaving.Height / 2;
            lblSaving.Text = "Loading...";
            lblSaving.Visible = true;
            lblSaving.BringToFront();
            Application.DoEvents();

            getDetailPost(sKey, sKey2);

            lblSaving.Visible = false;

            tmFocus.Enabled = true;
        }

        private void getDetailPost(string sKey, string sKey2)
        {
            grdItem.Rows.Clear();

            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_PRODUCT_STOCK_DEPT_Detail_결재조건(sKey, sKey2);

                if (dt != null && dt.Rows.Count > 0)
                {
                    bEditText = false;

                    dtp일자.Text = dt.Rows[0]["STOCK_DATE"].ToString().Trim();
                    lbl일자.Text = dt.Rows[0]["STOCK_DATE"].ToString().Trim();
                    lbl번호.Text = dt.Rows[0]["STOCK_NUM"].ToString().Trim();
                    cmb구분.SelectedValue = dt.Rows[0]["STOCK_KIND"].ToString().Trim();
                    lbl구분.Text = cmb구분.Text;

                    // PRODUCT_KIND = PRODUCT_KIND1 으로 처리.
                    if (dt.Rows[0]["PRODUCT_KIND"].ToString().Trim() == "10")
                    {
                        rb급여.Checked = true;
                    }
                    else
                    {
                        rb비급여.Checked = true;
                    }
                    //txt거래처코드old.Text = dt.Rows[0]["CUST_CODE1"].ToString().Trim();
                    txt거래처코드.Text = dt.Rows[0]["CUST_CODE1"].ToString().Trim();
                    //txt거래처명.Text = dt.Rows[0]["CUST_NAME1"].ToString().Trim();
                    lbl거래처명.Text = dt.Rows[0]["CUST_NAME1"].ToString().Trim();
                    lbl대표자.Text = dt.Rows[0]["REP_NAME1"].ToString().Trim();
                    lbl사업자번호.Text = dt.Rows[0]["COMP_NUM1"].ToString().Trim();
                    //txt담당자코드old.Text = dt.Rows[0]["MAN_CODE1"].ToString().Trim();
                    txt담당자코드.Text = dt.Rows[0]["MAN_CODE1"].ToString().Trim();
                    //txt담당자명.Text = dt.Rows[0]["MAN_NAME1"].ToString().Trim();
                    lbl담당자명.Text = dt.Rows[0]["MAN_NAME1"].ToString().Trim();
                    txt비고.Text = dt.Rows[0]["MEMO"].ToString().Trim();
                    txt비고1.Text = dt.Rows[0]["MEMO1"].ToString().Trim();
                    txt비고2.Text = dt.Rows[0]["MEMO2"].ToString().Trim();
                    txt비고3.Text = dt.Rows[0]["MEMO3"].ToString().Trim();
                    txt비고4.Text = dt.Rows[0]["MEMO4"].ToString().Trim();

                    txt배송.Text = dt.Rows[0]["TRANS_KIND"].ToString().Trim();
                    txt거래형태.Text = dt.Rows[0]["DEAL_TYPE"].ToString().Trim();
                    txt이메일.Text = dt.Rows[0]["EMAIL"].ToString().Trim();
                    txt거래분류.Text = dt.Rows[0]["DEAL_KIND"].ToString().Trim();
                    txt특매처.Text = dt.Rows[0]["특매처"].ToString().Trim();
                    txt부서코드.Text = dt.Rows[0]["DEPT_CODE1"].ToString().Trim();
                    txt부서명.Text = dt.Rows[0]["DEPT_NAME1"].ToString().Trim();

                    decimal nTot = wConst.get_거래처별_잔고(txt거래처코드.Text, dtp일자.Text);
                    lbl현잔고.Text = nTot.ToString("#,0");

                    // 본사 결재완료 : 수정/삭제 불가
                    if (dt.Rows[0]["STOCK_LAST1"].ToString().Trim() == "1")
                    {
                        btnSave.Enabled = false;
                        btnDelete.Enabled = false;
                    }

                    grdItem.RowCount = dt.Rows.Count;

                    for (int kk = 0; kk < dt.Rows.Count; kk++)
                    {
                        grdItem.Rows[kk].Cells[0].Value = false;
                        grdItem.Rows[kk].Cells[1].Value = (kk + 1).ToString();
                        grdItem.Rows[kk].Cells[2].Value = dt.Rows[kk]["STOCK_CODE"].ToString().Trim();
                        grdItem.Rows[kk].Cells[3].Value = dt.Rows[kk]["PRODUCT_NAME"].ToString().Trim();
                        grdItem.Rows[kk].Cells[4].Value = dt.Rows[kk]["STOCK_CODE"].ToString().Trim();
                        grdItem.Rows[kk].Cells[5].Value = dt.Rows[kk]["PRODUCT_NAME"].ToString().Trim();
                        grdItem.Rows[kk].Cells[6].Value = dt.Rows[kk]["PRODUCT_SPEC1"].ToString().Trim();
                        // 판매 건만 조회하므로, 반품에 대한 음수 처리 안함.
                        grdItem.Rows[kk].Cells[7].Value = decimal.Parse(dt.Rows[kk]["STOCK_QTY"].ToString().Trim()).ToString(Common.p_strFormatAmount);
                        grdItem.Rows[kk].Cells[8].Value = decimal.Parse(dt.Rows[kk]["STOCK_PRICE"].ToString().Trim()).ToString(Common.p_strFormatUnit);
                        grdItem.Rows[kk].Cells[9].Value = decimal.Parse(dt.Rows[kk]["STOCK_AMOUNT"].ToString().Trim()).ToString("#,0");
                        grdItem.Rows[kk].Cells[10].Value = decimal.Parse(dt.Rows[kk]["STOCK_VAT"].ToString().Trim()).ToString("#,0");
                        grdItem.Rows[kk].Cells[11].Value = decimal.Parse(dt.Rows[kk]["할인율"].ToString().Trim()).ToString("#,0");
                        grdItem.Rows[kk].Cells[12].Value = decimal.Parse(dt.Rows[kk]["계약율"].ToString().Trim()).ToString("#,0");
                        if (txt특매처.Text == "1")
                        {
                            grdItem.Rows[kk].Cells[12].Value = "0";
                        }
                        grdItem.Rows[kk].Cells[13].Value = dt.Rows[kk]["표준단가"].ToString().Trim();
                        grdItem.Rows[kk].Cells[14].Value = dt.Rows[kk]["할증체크"].ToString().Trim();
                        grdItem.Rows[kk].Cells[15].Value = dt.Rows[kk]["단가체크"].ToString().Trim();
                        grdItem.Rows[kk].Cells[16].Value = dt.Rows[kk]["회전일체크"].ToString().Trim();

                        //wConst.Calc_Price(grdItem, kk, txt부가세코드.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
            }
        }

        private void tmFocus_Tick(object sender, EventArgs e)
        {
            tmFocus.Enabled = false;
            grdItem.Focus();
        }

        private void tmCalc_Tick(object sender, EventArgs e)
        {
            tmCalc.Enabled = false;

            get_SumPrice();

            //// 마지막 빈 row 추가기능
            //if (grdItem.CurrentRow != null)
            //{
            //    if (grdItem.CurrentRow.Index == grdItem.Rows.Count - 1 && ("" + (string)grdItem.CurrentRow.Cells[4].Value).Length > 0)
            //    {
            //        grdItem.RowCount = grdItem.Rows.Count + 1;
            //    }
            //}

            tmCalc.Enabled = true;
        }

        private void tmFocusList_Tick(object sender, EventArgs e)
        {
            tmFocusList.Enabled = false;
            txt거래처코드.Focus();
            txt부서명.Focus();
            if (GridRecord.Rows.Count == 0)
            {
                dtpDay1.Focus();
            }
            else
            {
                GridRecord.Focus();
            }
        }

        private void get_SumPrice()
        {
            decimal nSumPrice = 0;

            for (int kk = 0; kk < grdItem.Rows.Count; kk++)
            {
                string s금액 = ("" + (string)grdItem.Rows[kk].Cells[9].Value).Trim().Replace(",", "");

                if (s금액 != "")
                {
                    decimal nMoney = decimal.Parse(s금액);
                    nSumPrice += nMoney;
                }
            }

            lbl합계.Text = nSumPrice.ToString("#,0");
        }

        #region 검색 거래처 ##############################################################################################################

        private void btnS거래처_Click(object sender, EventArgs e)
        {
            bEditText = false;

            wConst.call_popRef_Cust("", txtS거래처코드, txtS거래처명, "");
            //if (txtS거래처코드.Text != "")
            //{
            //    get_Srch_Cust_Info(txtS거래처코드.Text, txtS거래처명);
            //}

            bEditText = true;
            SendKeys.Send("{TAB}");
        }

        private void txtS거래처_Enter(object sender, EventArgs e)
        {
            bEditText = true;
        }

        private void txtS거래처_TextChanged(object sender, EventArgs e)
        {
            if (bEditText == false) return;

            txtS거래처코드.Text = "";
        }

        private void txtS거래처_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;

                bEditText = false;

                bool bPopSrch = true;

                if (txtS거래처코드.Text != "")
                {
                    bPopSrch = false;
                }

                if (bPopSrch == true)
                {
                    wConst.call_popRef_Cust(txtS거래처명.Text, txtS거래처코드, txtS거래처명, "");
                    //get_Srch_Cust_Info(txtS거래처코드.Text, txtS거래처명);
                }

                if (txtS거래처코드.Text == "")
                {
                    init_SearchText_Cust();
                }
                SendKeys.Send("{TAB}");
                bEditText = true;
            }
        }

        private void init_SearchText_Cust()
        {
            txtS거래처코드.Text = "";
            txtS거래처명.Text = "";
        }

        //private void get_Srch_Cust_Info(string sID, TextBox txt_Name)
        //{
        //    try
        //    {
        //        wnDm wDm = new wnDm();
        //        DataTable dt = null;
        //        dt = wDm.fn_CUSTOMER_Detail(sID);

        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            txt_Name.Text = dt.Rows[0]["CUST_NAME"].ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
        //    }
        //}

        #endregion 검색 거래처 ##############################################################################################################

        private bool validate_InputBox()
        {
            bool bRet = true;

            try
            {
                //if (txt거래형태.Text == "2")
                //{
                //    // 2=도매 : 입력불가
                //    MessageBox.Show("[ 도매 ] 거래처는 주문을 입력할수 없습니다.");
                //    return false;
                //}

                bool bChk = false;
                for (int kk = 0; kk < grdItem.Rows.Count; kk++)
                {
                    //// 할인율 = V 체크
                    //if ("" + (string)grdItem.Rows[kk].Cells[14].Value == "V")
                    //{
                    //    bChk = true;
                    //    break;
                    //}
                    // 단가 = V 체크
                    if ("" + (string)grdItem.Rows[kk].Cells[15].Value == "V")
                    {
                        bChk = true;
                        break;
                    }
                    // 회전일 = V 체크
                    if ("" + (string)grdItem.Rows[kk].Cells[16].Value == "V")
                    {
                        bChk = true;
                        break;
                    }
                }
                if (bChk == true)
                {
                    MessageBox.Show("결재조건에 맞지않는 제품이 있습니다.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                bRet = false;
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
                MessageBox.Show("입력 데이터 확인 중에 오류가 있습니다.");
            }
            return bRet;
        }

        private bool updatePost()
        {
            bool bRet = false;

            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                sb.AppendLine(" exec dbo.proc_영업소주문서결재처리 @STOCK_DATE, @STOCK_NUM ");

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@STOCK_DATE", dtp일자.Text);
                sCommand.Parameters.AddWithValue("@STOCK_NUM", lbl번호.Text);

                int qResult = wAdo.SqlCommandEtc(sCommand, "Move-PRODUCT_STOCK_DEPT");
                if (qResult > 0) bRet = true;
                else bRet = false;

                if (bRet == true)
                {
                }
                else
                {
                    MessageBox.Show("저장 중에 오류가 발생했습니다.");
                }
            }
            catch (Exception ex)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
                MessageBox.Show("데이터베이스에 문제가 발생했습니다.");
            }
            return bRet;
        }

        private bool updatePost_all()
        {
            bool bRet = false;

            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();
                int nRowCnt = 0;

                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_PRODUCT_STOCK_DEPT_Detail_결재대상들(srchDay, srchCust);

                if (dt != null && dt.Rows.Count > 0)
                {
                    string sDate_old = "";
                    string sNum_old = "";
                    bool bChk = false;

                    for (int kk = 0; kk < dt.Rows.Count; kk++)
                    {
                        string sDate = dt.Rows[kk]["STOCK_DATE"].ToString();
                        string sNum = dt.Rows[kk]["STOCK_NUM"].ToString();

                        string s할인율 = dt.Rows[kk]["할증체크"].ToString();
                        string s단가 = dt.Rows[kk]["단가체크"].ToString();
                        string s회전일 = dt.Rows[kk]["회전일체크"].ToString();

                        if (kk == 0)
                        {
                            // 첫 row 기본값 처리
                            sDate_old = sDate;
                            sNum_old = sNum;
                            bChk = false;
                        }

                        if (sDate_old + "_" + sNum_old != sDate + "_" + sNum)
                        {
                            if (bChk == false)
                            {
                                nRowCnt += 1;
                                sb.AppendLine(" exec dbo.proc_영업소주문서결재처리 '" + sDate_old + "', '" + sNum_old + "' ");
                            }

                            sDate_old = sDate;
                            sNum_old = sNum;
                            bChk = false;
                        }

                        //// 할인율 = V 체크
                        //if (s할인율 == "V")
                        //{
                        //    bChk = true;
                        //}

                        // 단가 = V 체크
                        if (s단가 == "V")
                        {
                            bChk = true;
                        }
                        // 회전일 = V 체크
                        if (s회전일 == "V")
                        {
                            bChk = true;
                        }

                    }

                    // 마지막 처리
                    if (bChk == false)
                    {
                        nRowCnt += 1;
                        sb.AppendLine(" exec dbo.proc_영업소주문서결재처리 '" + sDate_old + "', '" + sNum_old + "' ");
                    }

                }

                if (nRowCnt == 0)
                {
                    MessageBox.Show("결재조건에 적합한 주문서가 없습니다.");
                    return false;
                }

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                //sCommand.Parameters.AddWithValue("@STOCK_DATE", srchDay);
                //sCommand.Parameters.AddWithValue("@STOCK_NUM", "");

                int qResult = wAdo.SqlCommandEtc(sCommand, "MoveAll-PRODUCT_STOCK_DEPT");
                if (qResult > 0) bRet = true;
                else bRet = false;

                if (bRet == true)
                {
                }
                else
                {
                    MessageBox.Show("저장 중에 오류가 발생했습니다.");
                }
            }
            catch (Exception ex)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
                MessageBox.Show("데이터베이스에 문제가 발생했습니다.");
            }
            return bRet;
        }

        private bool deletePost()
        {
            bool bRet = false;

            try
            {
                wnDm wDm = new wnDm();
                wnAdo wAdo = new wnAdo();

                StringBuilder sb = new StringBuilder();

                sb.AppendLine(" delete from PRODUCT_STOCK_DEPT ");
                sb.AppendLine(" where STOCK_DATE = @STOCK_DATE ");
                sb.AppendLine("     and STOCK_NUM = @p1 ");

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@STOCK_DATE", this.dtp일자.Text);
                sCommand.Parameters.AddWithValue("@p1", this.lbl번호.Text);
                ////sCommand.Parameters.AddWithValue("@수정사원번호", Common.p_strUserNo);

                int qResult = wAdo.SqlCommandEtc(sCommand, "Delete-PRODUCT_STOCK_DEPT");
                if (qResult > 0) bRet = true;
                else bRet = false;

                if (bRet == true)
                {
                }
                else
                {
                    MessageBox.Show("삭제 중에 오류가 발생했습니다.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("데이터베이스에 문제가 발생했습니다.");
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
            }
            return bRet;
        }

    }
}
