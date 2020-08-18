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

namespace 스마트팩토리.P10_ORD
{
    public partial class frm수금 : Form
    {
        private wnGConstant wConst = new wnGConstant();
        private bool bData = false;
        private bool bEditText = false;
        //private int currRow = -1;
        //private int currCol = -1;

        private string sProgFolder = "";
        private string sBodyHH = "";
        //private string sColumnsWW = "";
        private string sSettingFile = "collect.xml";
        private bool bSetFirst = true;

        public frm수금()
        {
            InitializeComponent();

            this.spCont.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.spCont_SplitterMoved);
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
            //bSetFirst = false;
        }

        private void spCont_SplitterMoved(object sender, SplitterEventArgs e)
        {
            float nHH = (float)spCont.SplitterDistance / spCont.Height * 100;
            sBodyHH = nHH.ToString("#0.00");

            Save_Layout();
        }

        #endregion 입력 그리드 제어 #####################################################################################################

        private void frm수금_Load(object sender, EventArgs e)
        {
            lblSaving.BringToFront();
            lblSearch.BringToFront();

            sProgFolder = Path.GetDirectoryName(System.Environment.GetCommandLineArgs()[0]);
            Read_Layout();
            setting_Layout();

            dtp일자.Text = DateTime.Now.ToString("yyyy-MM-dd");
            dtpDay1.Text = DateTime.Now.ToString("yyyy-MM-dd");

            init_ComboBox();

            this.init_InputBox(true);
            btnSearch_Click(this, null);
        }

        public void init_InputBox(bool bNew)
        {
            wConst.Form_Clear(spCont.Panel1.Controls);
            wConst.Form_Clear(gb어음.Controls);

            btnSave.Enabled = true;
            txt거래처명.ReadOnly = false;
            btn거래처.Enabled = true;

            cmb구분.SelectedIndex = 0;
            cmb처리형태.SelectedIndex = 0;
            dtp처리일자.Text = "1900-01-01";
            dtp발행일자.Text = "1900-01-01";
            dtp만기일자.Text = "1900-01-01";

            if (bNew == true)
            {
                bData = false;
                btnDelete.Enabled = false;
                dtp일자.Enabled = true;
                rb급여.Checked = true;
                //txt배송.Text = "01";  // 01=택배
            }
            else
            {
                bData = true;
                btnDelete.Enabled = true;
                dtp일자.Enabled = false;
                //txt거래처명.ReadOnly = true;
                //btn거래처.Enabled = false;
            }
        }

        private void init_ComboBox()
        {
            string sqlQuery = "";

            wConst.set_Combo수금구분(false, cmb구분);

            wConst.set_Combo어음처리형태(false, cmb처리형태);

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.bindData(this.makeSearchCondition());
            tmFocusList.Enabled = true;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            init_InputBox(true);
            tmFocusNew.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
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
                bResult = this.insertPost();
            }
            else
            {
                bResult = this.updatePost();
            }

            if (bResult == true)
            {
                btnSearch_Click(this, null);

                btnNew_Click(this, null);
            }

            lblSaving.Visible = false;
            btnSave.Enabled = true;
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn출력_Click(object sender, EventArgs e)
        {

        }

        private string makeSearchCondition()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" and a.COLLECT_DATE = '" + dtpDay1.Text + "' ");

            if (chk대손포함.Checked == false)
            {
                sb.Append(" and (a.COLLECT_KIND <> '72' ");
                sb.Append("     or (a.COLLECT_KIND = '72' and a.COLLECT_NUM < '7000') ");
                sb.Append("     ) ");
            }

            switch (this.txtS거래처코드.Text)
            {
                case "":
                    sb.Append("");
                    break;
                default:
                    sb.Append(" and a.CUST_CODE1 = '" + txtS거래처코드.Text + "' ");
                    break;
            }

            return sb.ToString();
        }

        private void bindData(string condition)
        {
            lblSearch.Left = spCont.Panel2.ClientSize.Width / 2 - lblSearch.Width / 2;
            lblSearch.Top = spCont.Panel2.ClientSize.Height / 2 - lblSearch.Height / 2;
            lblSearch.Visible = true;
            Application.DoEvents();

            this.GridRecord.DataSource = null;
            this.GridRecord.RowCount = 0;

            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_PRODUCT_COLLECT_List(condition);

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.GridRecord.RowCount = dt.Rows.Count;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.GridRecord.Rows[i].Cells[0].Value = "";
                        this.GridRecord.Rows[i].Cells[1].Value = (i + 1).ToString();
                        this.GridRecord.Rows[i].Cells[2].Value = dt.Rows[i]["COLLECT_DATE"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[3].Value = dt.Rows[i]["COLLECT_NUM"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[4].Value = wConst.get_수금구분_명칭(dt.Rows[i]["COLLECT_KIND"].ToString().Trim());
                        this.GridRecord.Rows[i].Cells[5].Value = dt.Rows[i]["CUST_CODE1"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[6].Value = dt.Rows[i]["CUST_NAME1"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[7].Value = decimal.Parse(dt.Rows[i]["COLLECT_AMOUNT"].ToString().Trim()).ToString("#,0");
                        this.GridRecord.Rows[i].Cells[8].Value = dt.Rows[i]["BILL_NUMBER"].ToString().Trim();

                        this.GridRecord.Rows[i].Cells[9].Value = dt.Rows[i]["SETTLE_DATE"].ToString().Trim().Replace("/", "-");
                        if (dt.Rows[i]["SETTLE_DATE"].ToString().Trim().Replace("/", "-") == "0000-00-00")
                        {
                            this.GridRecord.Rows[i].Cells[9].Value = "";
                        }

                        this.GridRecord.Rows[i].Cells[9].Value = dt.Rows[i]["MEMO"].ToString().Trim();
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
            //if (e.KeyCode == Keys.F2)
            //{
            //    e.Handled = true;
            //    grdItem._KeyInput = "f2";
            //}
            //if (e.KeyCode == Keys.F3)
            //{
            //    e.Handled = true;
            //    grdItem._KeyInput = "f3";
            //}
            if (e.KeyCode == Keys.F6)
            {
                e.Handled = true;
                btnNew_Click(this, null);
            }
            //if (e.KeyCode == Keys.F7)
            //{
            //    e.Handled = true;
            //    grdItem._KeyInput = "f7";
            //}
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
            // chk수금구분 : 체크함= 1 = B수금, 체크안함= 0 = A수금

            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_PRODUCT_COLLECT_Detail(sKey, sKey2);

                if (dt != null && dt.Rows.Count > 0)
                {
                    bEditText = false;

                    dtp일자.Text = dt.Rows[0]["COLLECT_DATE"].ToString().Trim();
                    lbl번호.Text = dt.Rows[0]["COLLECT_NUM"].ToString().Trim();
                    cmb구분.SelectedValue = dt.Rows[0]["COLLECT_KIND"].ToString().Trim();

                    // PRODUCT_KIND = PRODUCT_KIND1 으로 처리.
                    if (dt.Rows[0]["PRODUCT_KIND"].ToString().Trim() == "10")
                    {
                        rb급여.Checked = true;
                    }
                    else
                    {
                        rb비급여.Checked = true;
                    }

                    if (dt.Rows[0]["미송구분"].ToString().Trim() == "1")
                    {
                        // 0= A수금, 1= B수금
                        chk미송구분.Checked = true;
                    }
                    else
                    {
                        chk미송구분.Checked = false;
                    }

                    if (dt.Rows[0]["KIND_AB"].ToString().Trim() == "1")
                    {
                        // 0= A수금, 1= B수금
                        chk수금구분.Checked = true;
                    }
                    else
                    {
                        chk수금구분.Checked = false;
                    }

                    decimal nFlg = 1;
                    // 수금구분(53:취소) 의 경우 - 값이 붙습니다
                    if (dt.Rows[0]["COLLECT_KIND"].ToString().Trim() == "53")
                    {
                        nFlg = -1;
                    }
                    txt금액.Text = (nFlg * decimal.Parse(dt.Rows[0]["COLLECT_AMOUNT"].ToString().Trim())).ToString("#,0");

                    txt어음번호.Text = dt.Rows[0]["BILL_NUMBER"].ToString().Trim();
                    cmb처리형태.SelectedValue = dt.Rows[0]["BILL_TYPE"].ToString().Trim();
                    try
                    {
                        dtp처리일자.Text = dt.Rows[0]["HANDLE_DATE"].ToString().Trim().Replace("/", "-");
                    }
                    catch
                    {
                        dtp처리일자.Text = "1900-01-01";
                    }
                    try
                    {
                        dtp발행일자.Text = dt.Rows[0]["ISSUE_DATE"].ToString().Trim().Replace("/", "-");
                    }
                    catch
                    {
                        dtp발행일자.Text = "1900-01-01";
                    }
                    try
                    {
                        dtp만기일자.Text = dt.Rows[0]["SETTLE_DATE"].ToString().Trim().Replace("/", "-");
                    }
                    catch
                    {
                        dtp만기일자.Text = "1900-01-01";
                    }
                    txt발행은행.Text = dt.Rows[0]["SETTLE_CUST"].ToString().Trim();
                    txt발행인.Text = dt.Rows[0]["COLLECT_NAME1"].ToString().Trim();
                    txt배서인.Text = dt.Rows[0]["COLLECT_NAME2"].ToString().Trim();
                    txt지급처.Text = dt.Rows[0]["SETTLE_CUST1"].ToString().Trim();
                    txt비고.Text = dt.Rows[0]["MEMO"].ToString().Trim();

                    txt거래처코드old.Text = dt.Rows[0]["CUST_CODE1"].ToString().Trim();
                    txt거래처코드.Text = dt.Rows[0]["CUST_CODE1"].ToString().Trim();
                    txt거래처명.Text = dt.Rows[0]["CUST_NAME1"].ToString().Trim();
                    lbl대표자.Text = dt.Rows[0]["REP_NAME1"].ToString().Trim();
                    lbl사업자번호.Text = dt.Rows[0]["COMP_NUM1"].ToString().Trim();
                    txt담당자코드old.Text = dt.Rows[0]["MAN_CODE1"].ToString().Trim();
                    txt담당자코드.Text = dt.Rows[0]["MAN_CODE1"].ToString().Trim();
                    txt담당자명.Text = dt.Rows[0]["MAN_NAME1"].ToString().Trim();

                    //txt배송.Text = dt.Rows[0]["TRANS_KIND"].ToString().Trim();
                    txt거래형태.Text = dt.Rows[0]["DEAL_TYPE"].ToString().Trim();
                    txt이메일.Text = dt.Rows[0]["EMAIL"].ToString().Trim();
                    txt거래분류.Text = dt.Rows[0]["DEAL_KIND"].ToString().Trim();
                    txt특매처.Text = dt.Rows[0]["특매처"].ToString().Trim();
                    txt부서코드.Text = dt.Rows[0]["DEPT_CODE1"].ToString().Trim();
                    txt부서명.Text = dt.Rows[0]["DEPT_NAME1"].ToString().Trim();

                    decimal nTot = wConst.get_거래처별_잔고(txt거래처코드.Text, dtp일자.Text);
                    lbl현잔고.Text = nTot.ToString("#,0");

                    //// 본사 결재완료 : 수정/삭제 불가
                    //if (dt.Rows[0]["STOCK_LAST1"].ToString().Trim() == "1")
                    //{
                    //    btnSave.Enabled = false;
                    //    btnDelete.Enabled = false;
                    //}

                    //// 판매, 반품 외의 자료는 수정 불가
                    //if (dt.Rows[0]["STOCK_KIND"].ToString().Trim() != "11" && dt.Rows[0]["STOCK_KIND"].ToString().Trim() != "22")
                    //{
                    //    btnSave.Enabled = false;
                    //}

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
            txt거래처명.Focus();
        }

        private void tmFocusNew_Tick(object sender, EventArgs e)
        {
            tmFocusNew.Enabled = false;
            txt거래처명.Focus();
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

        private void get_Cust_Info(string sCode)
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_CUSTOMER_Detail(sCode);

                if (dt != null && dt.Rows.Count > 0)
                {
                    txt거래처명.Text = dt.Rows[0]["CUST_NAME"].ToString().Trim();
                    lbl대표자.Text = dt.Rows[0]["REP_NAME"].ToString().Trim();
                    lbl사업자번호.Text = dt.Rows[0]["COMP_NUM"].ToString().Trim();
                    txt담당자코드.Text = dt.Rows[0]["MAN_CODE"].ToString().Trim();
                    txt담당자명.Text = dt.Rows[0]["CODE_DESC"].ToString().Trim();
                    txt거래형태.Text = dt.Rows[0]["DEAL_TYPE"].ToString().Trim();
                    txt이메일.Text = dt.Rows[0]["EMAIL"].ToString().Trim();
                    txt거래분류.Text = dt.Rows[0]["DEAL_KIND"].ToString().Trim();
                    txt특매처.Text = dt.Rows[0]["특매처"].ToString().Trim();

                    if (dt.Rows[0]["주사제퍼센트"].ToString().Trim() == "") dt.Rows[0]["주사제퍼센트"] = 0;
                    if (dt.Rows[0]["도매퍼센트"].ToString().Trim() == "") dt.Rows[0]["도매퍼센트"] = 0;
                    txt주사제퍼센트.Text = dt.Rows[0]["주사제퍼센트"].ToString().Trim();
                    txt도매퍼센트.Text = dt.Rows[0]["도매퍼센트"].ToString().Trim();

                    txt부서코드.Text = dt.Rows[0]["DEPT_CODE"].ToString().Trim();
                    txt부서명.Text = dt.Rows[0]["부서명"].ToString().Trim();

                    //if (txt거래형태.Text == "2")
                    //{
                    //    // 2=도매 : 입력불가
                    //    MessageBox.Show("[ 도매 ] 거래처는 주문을 입력할수 없습니다.");
                    //}
                    //if (txt이메일.Text == "")
                    //{
                    //    // 이메일이 없으면 입력 불가 
                    //    MessageBox.Show("[ 이메일주소 ] 없는 거래처는 주문을 입력할수 없습니다.");
                    //}
                    //if (lbl사업자번호.Text == "")
                    //{
                    //    // 이메일이 없으면 입력 불가 
                    //    MessageBox.Show("[ 사업자번호 ] 없는 거래처는 주문을 입력할수 없습니다.");
                    //}

                    decimal nTot = wConst.get_거래처별_잔고(txt거래처코드.Text, dtp일자.Text);
                    lbl현잔고.Text = nTot.ToString("#,0");
                }
            }
            catch (Exception ex)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
            }
        }

        private void get_Man_Info(string sCode)
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_User_Detail(sCode);

                if (dt != null && dt.Rows.Count > 0)
                {
                    txt부서코드.Text = dt.Rows[0]["USER_DEPT"].ToString().Trim();
                    txt부서명.Text = dt.Rows[0]["CODE_DESC"].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
            }
        }

        #region 입력 거래처 ##############################################################################################################

        private void btn거래처_Click(object sender, EventArgs e)
        {
            bEditText = false;

            if (txt거래처명.Text == "")
            {
                txt거래처코드.Text = "";
                txt거래처코드old.Text = "";
            }
            wConst.call_popRef_Cust("", txt거래처코드, txt거래처명, "0");
            if (txt거래처코드.Text != "")
            {
                if (txt거래처코드old.Text != txt거래처코드.Text)
                {
                    get_Cust_Info(txt거래처코드.Text);
                    txt거래처코드old.Text = txt거래처코드.Text;
                }

            }

            bEditText = true;
            SendKeys.Send("{TAB}");
        }

        private void txt명칭_Enter(object sender, EventArgs e)
        {
            bEditText = true;
        }

        private void txt명칭_TextChanged(object sender, EventArgs e)
        {
            if (bEditText == false) return;

            txt거래처코드.Text = "";
        }

        private void txt명칭_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;

                bEditText = false;

                bool bPopSrch = true;

                if (txt거래처코드.Text != "")
                {
                    bPopSrch = false;
                }

                if (bPopSrch == true)
                {
                    if (txt거래처명.Text == "")
                    {
                        txt거래처코드.Text = "";
                        txt거래처코드old.Text = "";
                    }
                    wConst.call_popRef_Cust(txt거래처명.Text, txt거래처코드, txt거래처명, "0");
                    if (txt거래처코드old.Text != txt거래처코드.Text)
                    {
                        get_Cust_Info(txt거래처코드.Text);
                        txt거래처코드old.Text = txt거래처코드.Text;
                    }

                }

                if (txt거래처코드.Text == "")
                {
                    init_DataText_Cust();
                }
                SendKeys.Send("{TAB}");
                bEditText = true;
            }
        }

        private void init_DataText_Cust()
        {
            txt거래처코드old.Text = "";
            txt거래처코드.Text = "";
            txt거래처명.Text = "";
            lbl대표자.Text = "";
            lbl사업자번호.Text = "";
            lbl현잔고.Text = "";
            txt담당자코드.Text = "";
            txt담당자명.Text = "";
        }

        #endregion 입력 거래처 ##############################################################################################################

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

        #region 입력 담당자 ##############################################################################################################

        private void btn담당자_Click(object sender, EventArgs e)
        {
            bEditText = false;

            if (txt담당자명.Text == "")
            {
                txt담당자코드.Text = "";
                txt담당자코드old.Text = "";
            }
            wConst.call_popRef_Man("", txt담당자코드, txt담당자명, "0");
            if (txt담당자코드.Text != "")
            {
                if (txt담당자코드old.Text != txt담당자코드.Text)
                {
                    get_Man_Info(txt담당자코드.Text);
                    txt담당자코드old.Text = txt담당자코드.Text;
                }

            }

            bEditText = true;
            SendKeys.Send("{TAB}");
        }

        private void txt담당자명_Enter(object sender, EventArgs e)
        {
            bEditText = true;
        }

        private void txt담당자명_TextChanged(object sender, EventArgs e)
        {
            if (bEditText == false) return;

            txt담당자코드.Text = "";
        }

        private void txt담당자명_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;

                bEditText = false;

                bool bPopSrch = true;

                if (txt담당자코드.Text != "")
                {
                    bPopSrch = false;
                }

                if (bPopSrch == true)
                {
                    if (txt담당자명.Text == "")
                    {
                        txt담당자코드.Text = "";
                        txt담당자코드old.Text = "";
                    }
                    wConst.call_popRef_Man(txt담당자명.Text, txt담당자코드, txt담당자명, "0");
                    if (txt담당자코드old.Text != txt담당자코드.Text)
                    {
                        get_Man_Info(txt담당자코드.Text);
                        txt담당자코드old.Text = txt담당자코드.Text;
                    }

                }

                if (txt담당자코드.Text == "")
                {
                    init_DataText_Man();
                }
                SendKeys.Send("{TAB}");
                bEditText = true;
            }
        }

        private void init_DataText_Man()
        {
            txt담당자코드.Text = "";
            txt담당자명.Text = "";
        }

        #endregion 입력 담당자 ##############################################################################################################

        private bool validate_InputBox()
        {
            bool bRet = true;

            try
            {
                if (txt거래처코드.Text == "")
                {
                    MessageBox.Show("[ 거래처 ] 를 입력하세요.");
                    return false;
                }

                // 정상거래처(00) 아닐 경우 저장여부
                if (txt거래분류.Text != "00")
                {
                    DialogResult msgOk = MessageBox.Show("[ 정상거래처 ]가 아닙니다.\r\n그래도, 저장하시겠습니까?", "저장여부", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (msgOk == DialogResult.No)
                    {
                        return false;
                    }
                }

                //if (txt거래형태.Text == "2")
                //{
                //    // 2=도매 : 입력불가
                //    MessageBox.Show("[ 도매 ] 거래처는 주문을 입력할수 없습니다.");
                //    return false;
                //}
                //if (txt이메일.Text == "")
                //{
                //    // 이메일이 없으면 입력 불가 
                //    MessageBox.Show("[ 이메일주소 ] 없는 거래처는 주문을 입력할수 없습니다.");
                //    return false;
                //}
                //if (lbl사업자번호.Text == "")
                //{
                //    // 이메일이 없으면 입력 불가 
                //    MessageBox.Show("[ 사업자번호 ] 없는 거래처는 주문을 입력할수 없습니다.");
                //    return false;
                //}

                if (txt담당자코드.Text == "")
                {
                    MessageBox.Show("[ 담당자 ] 를 입력하세요.");
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

        private bool insertPost()
        {
            // 서버1에 저장시에는, 수금구분 = 71, 72는 저장하지 않음

            bool bRet = false;

            try
            {
                wnAdo wAdo = new wnAdo();

                StringBuilder sb = new StringBuilder();
                sb = new StringBuilder();

                string sRetVal = "";

                string s구분코드 = "" + cmb구분.SelectedValue.ToString();

                if (s구분코드 != "71" && s구분코드 != "72" && s구분코드 != "73" && s구분코드 != "90")
                {
                    //일반용 전표번호
                    sb.AppendLine(" select right('000' + convert(nvarchar(4), isnull(max(convert(int, COLLECT_NUM)), 0) + 1), 4) ");
                    sb.AppendLine(" from PRODUCT_COLLECT ");
                    sb.AppendLine(" where COLLECT_DATE = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ");
                    sb.AppendLine("     and COLLECT_NUM < '7000' ");
                }
                else
                {
                    if (s구분코드 == "90")
                    {
                        sb.AppendLine(" select right('000' + convert(nvarchar(4), isnull(max(convert(int, COLLECT_NUM)), 9000) + 1), 4) ");
                        sb.AppendLine(" from PRODUCT_COLLECT ");
                        sb.AppendLine(" where COLLECT_DATE = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ");
                        sb.AppendLine("     and COLLECT_NUM > '9000' ");
                    }
                    else
                    {
                        sb.AppendLine(" select @maxKey = right('000' + convert(nvarchar(4), isnull(max(convert(int, COLLECT_NUM)), 7000) + 1), 4) ");
                        sb.AppendLine(" from PRODUCT_COLLECT ");
                        sb.AppendLine(" where COLLECT_DATE = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ");
                        sb.AppendLine("     and COLLECT_NUM > '7000' ");
                        sb.AppendLine("     and COLLECT_NUM < '9000' ");
                    }
                }

                sRetVal = wConst.maxValue_Check(sb.ToString());
                if (sRetVal == "")
                {
                    MessageBox.Show("데이터 검색 중 오류 발생!!!");
                    return false;
                }

                lbl번호.Text = sRetVal;

                sb = new StringBuilder();

                sb.AppendLine(" declare @maxKey nvarchar(4) ");
                sb.AppendLine(" set @maxKey = '" + sRetVal + "' ");

                sb.AppendLine(" insert into PRODUCT_COLLECT ( ");
                sb.AppendLine("     COLLECT_DATE ");
                sb.AppendLine("     , COLLECT_NUM ");
                sb.AppendLine("     , COLLECT_KIND ");
                sb.AppendLine("     , CUST_CODE1 ");
                sb.AppendLine("     , MAN_CODE1 ");
                sb.AppendLine("     , COLLECT_AMOUNT ");
                sb.AppendLine("     , BILL_NUMBER ");
                sb.AppendLine("     , BILL_TYPE ");
                sb.AppendLine("     , HANDLE_DATE ");
                sb.AppendLine("     , ISSUE_DATE ");
                sb.AppendLine("     , SETTLE_DATE ");
                sb.AppendLine("     , SETTLE_CUST ");
                sb.AppendLine("     , SETTLE_CUST1 ");
                sb.AppendLine("     , COLLECT_NAME1 ");
                sb.AppendLine("     , COLLECT_NAME2 ");
                sb.AppendLine("     , MEMO ");
                sb.AppendLine("     , USER_CODE ");
                sb.AppendLine("     , PRODUCT_KIND ");
                sb.AppendLine("     , CUST_NAME1 ");
                sb.AppendLine("     , MAN_NAME1 ");
                sb.AppendLine("     , DEPT_NAME1 ");
                sb.AppendLine("     , REP_NAME1 ");
                sb.AppendLine("     , COMP_NUM1 ");
                sb.AppendLine("     , KIND_AB ");
                sb.AppendLine("     , DEAL_TYPE ");
                sb.AppendLine("     , DEPT_CODE1 ");
                sb.AppendLine("     , INSERT_DATE ");
                sb.AppendLine("     , 미송구분 ");
                sb.AppendLine("     , CHK_PRT ");
                sb.AppendLine(" ) values ( ");
                sb.AppendLine("     @COLLECT_DATE ");
                sb.AppendLine("     , @maxKey ");
                sb.AppendLine("     , @COLLECT_KIND ");
                sb.AppendLine("     , @CUST_CODE1 ");
                sb.AppendLine("     , @MAN_CODE1 ");
                sb.AppendLine("     , @COLLECT_AMOUNT ");
                sb.AppendLine("     , @BILL_NUMBER ");
                sb.AppendLine("     , @BILL_TYPE ");
                sb.AppendLine("     , @HANDLE_DATE ");
                sb.AppendLine("     , @ISSUE_DATE ");
                sb.AppendLine("     , @SETTLE_DATE ");
                sb.AppendLine("     , @SETTLE_CUST ");
                sb.AppendLine("     , @SETTLE_CUST1 ");
                sb.AppendLine("     , @COLLECT_NAME1 ");
                sb.AppendLine("     , @COLLECT_NAME2 ");
                sb.AppendLine("     , @MEMO ");
                sb.AppendLine("     , @USER_CODE ");
                sb.AppendLine("     , @PRODUCT_KIND ");
                sb.AppendLine("     , @CUST_NAME1 ");
                sb.AppendLine("     , @MAN_NAME1 ");
                sb.AppendLine("     , @DEPT_NAME1 ");
                sb.AppendLine("     , @REP_NAME1 ");
                sb.AppendLine("     , @COMP_NUM1 ");
                sb.AppendLine("     , @KIND_AB ");
                sb.AppendLine("     , @DEAL_TYPE ");
                sb.AppendLine("     , @DEPT_CODE1 ");
                sb.AppendLine("     , getdate() "); //INSERT_DATE
                sb.AppendLine("     , @미송구분 ");
                sb.AppendLine("     , '0' ");
                sb.AppendLine(" ) ");

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@COLLECT_DATE", dtp일자.Text);
                sCommand.Parameters.AddWithValue("@COLLECT_KIND", "" + cmb구분.SelectedValue.ToString());
                sCommand.Parameters.AddWithValue("@CUST_CODE1", txt거래처코드.Text);
                sCommand.Parameters.AddWithValue("@MAN_CODE1", txt담당자코드.Text);
                if ("" + cmb구분.SelectedValue.ToString() == "53")
                {
                    sCommand.Parameters.AddWithValue("@COLLECT_AMOUNT", (-1 * decimal.Parse(txt금액.Text.Replace(",", ""))).ToString());
                }
                else
                {
                    sCommand.Parameters.AddWithValue("@COLLECT_AMOUNT", txt금액.Text.Replace(",", ""));
                }
                sCommand.Parameters.AddWithValue("@BILL_NUMBER", txt어음번호.Text);
                sCommand.Parameters.AddWithValue("@BILL_TYPE", "" + cmb처리형태.SelectedValue.ToString());

                sCommand.Parameters.AddWithValue("@HANDLE_DATE", (dtp처리일자.Text == "1900-01-01" ? "0000-00-00" : dtp처리일자.Text));
                sCommand.Parameters.AddWithValue("@ISSUE_DATE", (dtp발행일자.Text == "1900-01-01" ? "0000-00-00" : dtp발행일자.Text));
                sCommand.Parameters.AddWithValue("@SETTLE_DATE", (dtp만기일자.Text == "1900-01-01" ? "0000-00-00" : dtp만기일자.Text));

                sCommand.Parameters.AddWithValue("@SETTLE_CUST", txt발행은행.Text);
                sCommand.Parameters.AddWithValue("@SETTLE_CUST1", txt지급처.Text);
                sCommand.Parameters.AddWithValue("@COLLECT_NAME1", txt발행인.Text);
                sCommand.Parameters.AddWithValue("@COLLECT_NAME2", txt배서인.Text);
                sCommand.Parameters.AddWithValue("@MEMO", txt비고.Text);
                sCommand.Parameters.AddWithValue("@USER_CODE", Common.p_strUserCode);
                if (rb급여.Checked == true)
                {
                    sCommand.Parameters.AddWithValue("@PRODUCT_KIND", "10");
                }
                else
                {
                    sCommand.Parameters.AddWithValue("@PRODUCT_KIND", "20");
                }
                sCommand.Parameters.AddWithValue("@CUST_NAME1", txt거래처명.Text);
                sCommand.Parameters.AddWithValue("@MAN_NAME1", txt담당자명.Text);
                sCommand.Parameters.AddWithValue("@DEPT_NAME1", txt부서명.Text);
                sCommand.Parameters.AddWithValue("@REP_NAME1", lbl대표자.Text);
                sCommand.Parameters.AddWithValue("@COMP_NUM1", lbl사업자번호.Text);

                sCommand.Parameters.AddWithValue("@KIND_AB", (chk수금구분.Checked == true ? "1" : "0"));
                sCommand.Parameters.AddWithValue("@DEAL_TYPE", txt거래형태.Text);
                sCommand.Parameters.AddWithValue("@DEPT_CODE1", txt부서코드.Text);
                sCommand.Parameters.AddWithValue("@미송구분", "");

                int qResult = wAdo.SqlCommandEtc(sCommand, "Insert-PRODUCT_COLLECT");
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

        private bool updatePost()
        {
            // 서버1에 저장시에는, 수금구분 = 71, 72는 저장하지 않음

            bool bRet = false;

            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                sb.AppendLine(" update PRODUCT_COLLECT set ");
                sb.AppendLine("     COLLECT_KIND = @COLLECT_KIND ");
                sb.AppendLine("     , CUST_CODE1 = @CUST_CODE1 ");
                sb.AppendLine("     , MAN_CODE1 = @MAN_CODE1 ");
                sb.AppendLine("     , COLLECT_AMOUNT = @COLLECT_AMOUNT ");
                sb.AppendLine("     , BILL_NUMBER = @BILL_NUMBER ");
                sb.AppendLine("     , BILL_TYPE = @BILL_TYPE ");
                sb.AppendLine("     , HANDLE_DATE = @HANDLE_DATE ");
                sb.AppendLine("     , ISSUE_DATE = @ISSUE_DATE ");
                sb.AppendLine("     , SETTLE_DATE = @SETTLE_DATE ");
                sb.AppendLine("     , SETTLE_CUST = @SETTLE_CUST ");
                sb.AppendLine("     , SETTLE_CUST1 = @SETTLE_CUST1 ");
                sb.AppendLine("     , COLLECT_NAME1 = @COLLECT_NAME1 ");
                sb.AppendLine("     , COLLECT_NAME2 = @COLLECT_NAME2 ");
                sb.AppendLine("     , MEMO = @MEMO ");
                //sb.AppendLine("     , USER_CODE = @USER_CODE ");
                sb.AppendLine("     , PRODUCT_KIND = @PRODUCT_KIND ");
                sb.AppendLine("     , CUST_NAME1 = @CUST_NAME1 ");
                sb.AppendLine("     , MAN_NAME1 = @MAN_NAME1 ");
                sb.AppendLine("     , DEPT_NAME1 = @DEPT_NAME1 ");
                sb.AppendLine("     , REP_NAME1 = @REP_NAME1 ");
                sb.AppendLine("     , COMP_NUM1 = @COMP_NUM1 ");
                sb.AppendLine("     , KIND_AB = @KIND_AB ");
                sb.AppendLine("     , DEAL_TYPE = @DEAL_TYPE ");
                sb.AppendLine("     , DEPT_CODE1 = @DEPT_CODE1 ");
                //sb.AppendLine("     , 미송구분 = @미송구분 ");
                sb.AppendLine(" where COLLECT_DATE = @COLLECT_DATE ");
                sb.AppendLine("     and COLLECT_NUM = @p1 ");

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@COLLECT_DATE", dtp일자.Text);
                sCommand.Parameters.AddWithValue("@p1", lbl번호.Text);
                sCommand.Parameters.AddWithValue("@COLLECT_KIND", "" + cmb구분.SelectedValue.ToString());
                sCommand.Parameters.AddWithValue("@CUST_CODE1", txt거래처코드.Text);
                sCommand.Parameters.AddWithValue("@MAN_CODE1", txt담당자코드.Text);
                if ("" + cmb구분.SelectedValue.ToString() == "53")
                {
                    sCommand.Parameters.AddWithValue("@COLLECT_AMOUNT", (-1 * decimal.Parse(txt금액.Text.Replace(",", ""))).ToString());
                }
                else
                {
                    sCommand.Parameters.AddWithValue("@COLLECT_AMOUNT", txt금액.Text.Replace(",", ""));
                }
                sCommand.Parameters.AddWithValue("@BILL_NUMBER", txt어음번호.Text);
                sCommand.Parameters.AddWithValue("@BILL_TYPE", "" + cmb처리형태.SelectedValue.ToString());

                sCommand.Parameters.AddWithValue("@HANDLE_DATE", (dtp처리일자.Text == "1900-01-01" ? "0000-00-00" : dtp처리일자.Text));
                sCommand.Parameters.AddWithValue("@ISSUE_DATE", (dtp발행일자.Text == "1900-01-01" ? "0000-00-00" : dtp발행일자.Text));
                sCommand.Parameters.AddWithValue("@SETTLE_DATE", (dtp만기일자.Text == "1900-01-01" ? "0000-00-00" : dtp만기일자.Text));

                sCommand.Parameters.AddWithValue("@SETTLE_CUST", txt발행은행.Text);
                sCommand.Parameters.AddWithValue("@SETTLE_CUST1", txt지급처.Text);
                sCommand.Parameters.AddWithValue("@COLLECT_NAME1", txt발행인.Text);
                sCommand.Parameters.AddWithValue("@COLLECT_NAME2", txt배서인.Text);
                sCommand.Parameters.AddWithValue("@MEMO", txt비고.Text);
                //sCommand.Parameters.AddWithValue("@USER_CODE", Common.p_strUserCode);
                if (rb급여.Checked == true)
                {
                    sCommand.Parameters.AddWithValue("@PRODUCT_KIND", "10");
                }
                else
                {
                    sCommand.Parameters.AddWithValue("@PRODUCT_KIND", "20");
                }
                sCommand.Parameters.AddWithValue("@CUST_NAME1", txt거래처명.Text);
                sCommand.Parameters.AddWithValue("@MAN_NAME1", txt담당자명.Text);
                sCommand.Parameters.AddWithValue("@DEPT_NAME1", txt부서명.Text);
                sCommand.Parameters.AddWithValue("@REP_NAME1", lbl대표자.Text);
                sCommand.Parameters.AddWithValue("@COMP_NUM1", lbl사업자번호.Text);

                sCommand.Parameters.AddWithValue("@KIND_AB", (chk수금구분.Checked == true ? "1" : "0"));
                sCommand.Parameters.AddWithValue("@DEAL_TYPE", txt거래형태.Text);
                sCommand.Parameters.AddWithValue("@DEPT_CODE1", txt부서코드.Text);
                //sCommand.Parameters.AddWithValue("@미송구분", "");

                int qResult = wAdo.SqlCommandEtc(sCommand, "Update-PRODUCT_COLLECT");
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

                sb.AppendLine(" delete from PRODUCT_COLLECT ");
                sb.AppendLine(" where COLLECT_DATE = @COLLECT_DATE ");
                sb.AppendLine("     and COLLECT_NUM = @p1 ");

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@COLLECT_DATE", dtp일자.Text);
                sCommand.Parameters.AddWithValue("@p1", lbl번호.Text);

                int qResult = wAdo.SqlCommandEtc(sCommand, "Delete-PRODUCT_COLLECT");
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
