using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 스마트팩토리.CLS;

namespace 스마트팩토리.P85_BAS
{
    public partial class frm처방처등록 : Form
    {
        private wnGConstant wConst = new wnGConstant();
        private bool bEditText = false;
        private bool bData = false;
        private int intTotalRecords = 0;
        private int intPageSize = 0;
        private int intPageCount = 0;
        private int intCurrentPage = 1;
        private int nPageSize = int.Parse(Common.p_PageSize);

        public frm처방처등록()
        {
            InitializeComponent();
        }

        private void frm처방처등록_Load(object sender, EventArgs e)
        {
            spCont.Dock = DockStyle.Fill;

            lblSearch.BringToFront();
            lblBody.BringToFront();

            this.init_ComboBox();
            this.init_InputBox(true);
            this.bindData();
        }

        private void init_ComboBox()
        {
            string sqlQuery = "";

            cmb거래형태.ValueMember = "코드";
            cmb거래형태.DisplayMember = "명칭";
            sqlQuery = " select '1' as 코드, '약국' as 명칭 ";
            sqlQuery += " union all ";
            sqlQuery += " select '2' as 코드, '도매' as 명칭 ";
            sqlQuery += " union all ";
            sqlQuery += " select '3' as 코드, '의원' as 명칭 ";
            sqlQuery += " union all ";
            sqlQuery += " select '4' as 코드, '보건소' as 명칭 ";
            sqlQuery += " union all ";
            sqlQuery += " select '5' as 코드, '병원' as 명칭 ";
            sqlQuery += " union all ";
            sqlQuery += " select '6' as 코드, '종합병원' as 명칭 ";
            sqlQuery += " union all ";
            sqlQuery += " select '9' as 코드, '기타' as 명칭 ";
            wConst.ComboBox_Read_Blank(cmb거래형태, sqlQuery);

            cmb진료과목.ValueMember = "코드";
            cmb진료과목.DisplayMember = "명칭";
            sqlQuery = " select MAJOR_CODE as 코드, CODE_DESC as 명칭 from MAJORCODE where 1=1 ";
            wConst.ComboBox_Read_NoBlank(cmb진료과목, sqlQuery);

            cmb거래분류.ValueMember = "코드";
            cmb거래분류.DisplayMember = "명칭";
            sqlQuery = " select DEAL_KIND as 코드, CODE_DESC as 명칭 from DEALKIND where 1=1 ";
            wConst.ComboBox_Read_Blank(cmb거래분류, sqlQuery);
        }

        private void init_InputBox(bool bNew)
        {
            wConst.Form_Clear(spCont.Panel1.Controls);
            cmb거래형태.SelectedIndex = 0;
            cmb진료과목.SelectedIndex = 0;
            cmb거래분류.SelectedIndex = 0;

            if (bNew == true)
            {
                bData = false;
                btnDelete.Enabled = false;
                txtCode.Enabled = false;
            }
            else
            {
                bData = true;
                btnDelete.Enabled = true;
                txtCode.Enabled = false;
            }
        }

        private string makeSearchCondition()
        {
            StringBuilder sb = new StringBuilder();

            switch (this.txtSrch.Text)
            {
                case "":
                    sb.Append("");
                    break;
                default:
                    sb.Append(" and a.CUST_NAME like '%" + txtSrch.Text + "%' ");
                    break;
            }

            return sb.ToString();
        }

        private void bindData()
        {
            lblSearch.Left = spCont.Panel2.ClientSize.Width / 2 - lblSearch.Width / 2;
            lblSearch.Top = spCont.Panel2.ClientSize.Height / 2 - lblSearch.Height / 2;
            lblSearch.Visible = true;
            Application.DoEvents();

            this.GridRecord.DataSource = null;
            this.GridRecord.RowCount = 0;

            // For Page view.
            intPageSize = nPageSize;
            intTotalRecords = getCount();
            intPageCount = intTotalRecords / intPageSize;

            // Adjust page count if the last page contains partial page.
            if (intTotalRecords % intPageSize > 0)
                intPageCount++;

            intCurrentPage = 0;

            cmbPage.Items.Clear();
            if (intTotalRecords == 0)
            {
                cmbPage.Items.Add("1");
            }
            else
            {
                for (int kk = 0; kk < intPageCount; kk++)
                {
                    cmbPage.Items.Add((kk + 1).ToString());
                }
            }
            cmbPage.SelectedIndex = 0;

            loadPage();

            lblSearch.Visible = false;
        }

        private void GridRecord_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (GridRecord.CurrentCell == null) return;
            if (GridRecord.CurrentCell.RowIndex < 0) return;
            if (GridRecord.CurrentCell.ColumnIndex < 0) return;

            int iRow = GridRecord.CurrentCell.RowIndex;
            //int iKeyCol = GridRecord.ColumnCount - 1;
            int iKeyCol = 0;
            string strValue = "" + GridRecord.Rows[iRow].Cells[iKeyCol].Value.ToString().Trim();

            getDetailPost(strValue);

            txtName.Focus();
        }

        private void getDetailPost(string sKey)
        {
            init_InputBox(false);

            lblBody.Left = spCont.Panel1.Width / 2 - lblBody.Width / 2;
            lblBody.Top = spCont.Panel1.Height / 2 - lblBody.Height / 2;
            lblBody.Visible = true;
            lblBody.Text = "Loading ...";
            Application.DoEvents();

            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_CUSTOMER_PRE_Detail(sKey);

                if (dt != null && dt.Rows.Count > 0)
                {
                    bEditText = false;

                    this.txtCode.Text = dt.Rows[0]["CUST_CODE"].ToString().Trim();
                    this.txtName.Text = dt.Rows[0]["CUST_NAME"].ToString().Trim();
                    this.txt대표자명.Text = dt.Rows[0]["REP_NAME"].ToString().Trim();

                    dt.Rows[0]["COMP_NUM"] = dt.Rows[0]["COMP_NUM"].ToString().Replace("-", "").Replace("_", "").Replace(" ", "").Trim();
                    this.txt사업자번호.Text = dt.Rows[0]["COMP_NUM"].ToString().Trim();
                    this.txt업태.Text = dt.Rows[0]["BUSINESS_STATUS"].ToString().Trim();
                    this.txt종목.Text = dt.Rows[0]["BUSINESS_KIND"].ToString().Trim();
                    dt.Rows[0]["RES_NUM"] = dt.Rows[0]["RES_NUM"].ToString().Replace("-", "").Replace("_", "").Replace(" ", "").Trim();
                    this.txt주민번호.Text = dt.Rows[0]["RES_NUM"].ToString().Trim();

                    this.txt회사전화.Text = dt.Rows[0]["TEL_NUM1"].ToString().Trim();
                    this.txt회사팩스.Text = dt.Rows[0]["TEL_NUM2"].ToString().Trim();

                    this.txt담당자코드old.Text = dt.Rows[0]["MAN_CODE"].ToString().Trim();
                    this.txt담당자코드.Text = dt.Rows[0]["MAN_CODE"].ToString().Trim();
                    this.txt담당자명.Text = dt.Rows[0]["담당자명"].ToString().Trim();
                    cmb거래형태.SelectedValue = dt.Rows[0]["DEAL_TYPE"].ToString().Trim();
                    cmb진료과목.SelectedValue = dt.Rows[0]["MAJOR_CODE"].ToString().Trim();
                    cmb거래분류.SelectedValue = dt.Rows[0]["DEAL_KIND"].ToString().Trim();

                    this.txt사업우편번호.Text = dt.Rows[0]["ZIP_CODE1"].ToString().Trim();
                    this.txt사업주소1.Text = dt.Rows[0]["ZIP_AREA1"].ToString().Trim();
                    this.txt사업주소2.Text = dt.Rows[0]["ZIP_ADDR1"].ToString().Trim();

                    if (dt.Rows[0]["REG_DATE"].ToString().Trim().Replace("/", "").Replace("-", "").Replace(" ", "").Replace("_", "") == "")
                    {
                        this.dtp거래개시일.Text = "9000-01-01";
                    }
                    else
                    {
                        this.dtp거래개시일.Text = DateTime.Parse(dt.Rows[0]["REG_DATE"].ToString().Trim().Replace(" ", "")).ToString("yyyy-MM-dd");
                    }

                    this.txt여신한도.Text = (dt.Rows[0]["LIMIT_AMOUNT"].ToString().Trim() == "" ? "0" : dt.Rows[0]["LIMIT_AMOUNT"].ToString().Trim());
                    this.txt요양기관번호.Text = dt.Rows[0]["CUST_NUM"].ToString().Trim();
                    this.txt기초잔고.Text = (dt.Rows[0]["CUST_SAIL"].ToString().Trim() == "" ? "0" : dt.Rows[0]["CUST_SAIL"].ToString().Trim());

                    this.txt비고.Text = dt.Rows[0]["MEMO"].ToString().Trim();

                    this.txt담보금액.Text = (dt.Rows[0]["담보금액"].ToString().Trim() == "" ? "0" : dt.Rows[0]["담보금액"].ToString().Trim());
                    this.txt담보종류.Text = dt.Rows[0]["담보종류"].ToString().Trim();
                }
                else
                {
                    MessageBox.Show("존재하지 않는 자료입니다.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("검색중에 오류가 발생했습니다.");
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
            }

            lblBody.Visible = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //this.init_InputBox(true);
            //this.init_InputBox_Sub(true);
            this.bindData();
            GridRecord.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            this.init_InputBox(true);
            txtCode.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            if (validate_InputBox() == false)
            {
                btnSave.Enabled = true;
                return;
            }

            if (bData == false)
            {
                if (get_Dup_Check(txtCode.Text, txtCode.Text) == true)
                {
                    MessageBox.Show("이미 존재하는 [ 코드 ]입니다." + " [ " + this.txtCode.Text + " ]");
                    btnSave.Enabled = true;
                    return;
                }
            }

            lblBody.Left = spCont.Panel1.Width / 2 - lblBody.Width / 2;
            lblBody.Top = spCont.Panel1.Height / 2 - lblBody.Height / 2;
            lblBody.Visible = true;
            lblBody.Text = "Saving ...";
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

            lblBody.Visible = false;

            if (bResult == true)
            {
                this.init_InputBox(true);
                this.bindData();
            }
            btnSave.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult msgOk = MessageBox.Show("자료를 삭제하시겠습니까?", "삭제여부", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (msgOk == DialogResult.Cancel)
            {
                return;
            }

            lblBody.Left = spCont.Panel1.Width / 2 - lblBody.Width / 2;
            lblBody.Top = spCont.Panel1.Height / 2 - lblBody.Height / 2;
            lblBody.Visible = true;
            lblBody.Text = "Deleting ...";
            Application.DoEvents();

            bool bResult = this.deletePost();

            lblBody.Visible = false;

            if (bResult == true)
            {
                this.init_InputBox(true);
                this.bindData();
            }
        }

        private bool validate_InputBox()
        {
            bool bRet = true;

            try
            {
                //if (this.txtCode.Text.Trim() == "")
                //{
                //    MessageBox.Show("[ 코드 ]를 입력하세요.");
                //    return false;
                //}

                if (this.txtName.Text.Trim() == "")
                {
                    MessageBox.Show("[ 거래처명 ]을 입력하세요.");
                    return false;
                }

                //if (this.txt대표자명.Text.Trim() == "")
                //{
                //    MessageBox.Show("[ 대표자명 ]을 입력하세요.");
                //    return false;
                //}

                //if (this.txt사업자번호.Text.Trim() == "")
                //{
                //    MessageBox.Show("[ 사업자번호 ]를 입력하세요.");
                //    return false;
                //}

                if (this.txt담당자코드.Text.Trim() == "")
                {
                    MessageBox.Show("[ 담당자 ]을 입력하세요.");
                    return false;
                }

                //if ("" + cmb거래형태.SelectedValue.ToString() == "")
                //{
                //    MessageBox.Show("[ 거래형태 ]을 입력하세요.");
                //    return false;
                //}

                //if ("" + cmb진료과목.SelectedValue.ToString() == "")
                //{
                //    MessageBox.Show("[ 진료과목 ]을 입력하세요.");
                //    return false;
                //}

                //if ("" + cmb거래분류.SelectedValue.ToString() == "")
                //{
                //    MessageBox.Show("[ 거래분류 ]을 입력하세요.");
                //    return false;
                //}

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
            bool bRet = false;

            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                string sRetVal = "";

                sb.AppendLine(" select right('000000' + convert(nvarchar(7), isnull(max(convert(int, CUST_CODE)), 0) + 1), 7) ");
                sb.AppendLine(" from CUSTOMER_PRE ");
                sb.AppendLine(" where substring(CUST_CODE, 1, 4) = '" + txt담당자코드.Text.Trim() + "' ");

                sRetVal = wConst.maxValue_Check(sb.ToString());
                if (sRetVal == "")
                {
                    MessageBox.Show("데이터 검색 중 오류 발생!!!");
                    return false;
                }
                if (sRetVal == "000001")
                {
                    sRetVal = txt담당자코드.Text.Trim() + "001";
                }

                txtCode.Text = sRetVal;

                sb = new StringBuilder();

                sb.AppendLine("declare @maxKey nvarchar(7) ");
                sb.AppendLine("set @maxKey = '" + sRetVal + "' ");

                sb.AppendLine(" insert into CUSTOMER_PRE ( ");
                sb.AppendLine("     CUST_CODE ");
                sb.AppendLine("     , CUST_NAME ");
                sb.AppendLine("     , REP_NAME ");
                sb.AppendLine("     , COMP_NUM ");
                sb.AppendLine("     , RES_NUM ");
                sb.AppendLine("     , ZIP_CODE1 ");
                sb.AppendLine("     , ZIP_AREA1 ");
                sb.AppendLine("     , ZIP_ADDR1 ");
                sb.AppendLine("     , TEL_NUM1 ");
                sb.AppendLine("     , ZIP_CODE2 ");
                sb.AppendLine("     , ZIP_AREA2 ");
                sb.AppendLine("     , ZIP_ADDR2 ");
                sb.AppendLine("     , TEL_NUM2 ");
                sb.AppendLine("     , BUSINESS_STATUS ");
                sb.AppendLine("     , BUSINESS_KIND ");
                sb.AppendLine("     , REG_DATE ");
                sb.AppendLine("     , DEAL_TYPE ");
                sb.AppendLine("     , SUPPLY_KIND ");
                sb.AppendLine("     , MAN_CODE ");
                sb.AppendLine("     , MAJOR_CODE ");
                sb.AppendLine("     , DEAL_KIND ");
                sb.AppendLine("     , TRANS_KIND ");
                sb.AppendLine("     , LIMIT_AMOUNT ");
                sb.AppendLine("     , BASE_AMOUNT ");
                sb.AppendLine("     , BILL_AMOUNT ");
                sb.AppendLine("     , MEMO ");
                sb.AppendLine("     , MEMO1 ");
                sb.AppendLine("     , MEMO2 ");
                sb.AppendLine("     , CUST_NUM ");
                sb.AppendLine("     , CUST_SAIL ");
                sb.AppendLine("     , 담보종류 ");
                sb.AppendLine("     , 담보금액 ");
                sb.AppendLine(" ) values ( ");
                sb.AppendLine("     @maxKey ");
                sb.AppendLine("     , @CUST_NAME ");
                sb.AppendLine("     , @REP_NAME ");
                sb.AppendLine("     , @COMP_NUM ");
                sb.AppendLine("     , @RES_NUM ");
                sb.AppendLine("     , @ZIP_CODE1 ");
                sb.AppendLine("     , @ZIP_AREA1 ");
                sb.AppendLine("     , @ZIP_ADDR1 ");
                sb.AppendLine("     , @TEL_NUM1 ");
                sb.AppendLine("     , @ZIP_CODE2 ");
                sb.AppendLine("     , @ZIP_AREA2 ");
                sb.AppendLine("     , @ZIP_ADDR2 ");
                sb.AppendLine("     , @TEL_NUM2 ");
                sb.AppendLine("     , @BUSINESS_STATUS ");
                sb.AppendLine("     , @BUSINESS_KIND ");
                sb.AppendLine("     , @REG_DATE ");
                sb.AppendLine("     , @DEAL_TYPE ");
                sb.AppendLine("     , @SUPPLY_KIND ");
                sb.AppendLine("     , @MAN_CODE ");
                sb.AppendLine("     , @MAJOR_CODE ");
                sb.AppendLine("     , @DEAL_KIND ");
                sb.AppendLine("     , @TRANS_KIND ");
                sb.AppendLine("     , @LIMIT_AMOUNT ");
                sb.AppendLine("     , @BASE_AMOUNT ");
                sb.AppendLine("     , @BILL_AMOUNT ");
                sb.AppendLine("     , @MEMO ");
                sb.AppendLine("     , @MEMO1 ");
                sb.AppendLine("     , @MEMO2 ");
                sb.AppendLine("     , @CUST_NUM ");
                sb.AppendLine("     , @CUST_SAIL ");
                sb.AppendLine("     , @담보종류 ");
                sb.AppendLine("     , @담보금액 ");
                sb.AppendLine(" ) ");

                //sb.AppendLine(" insert into MAN_BASEAMOUNT ( ");
                //sb.AppendLine("     CUST_CODE ");
                //sb.AppendLine("     , MAN_CODE ");
                //sb.AppendLine("     , MAN_NAME ");
                //sb.AppendLine("     , BASE_AMOUNT ");
                //sb.AppendLine("     , KIND1_AMOUNT ");
                //sb.AppendLine("     , KIND2_AMOUNT ");
                //sb.AppendLine("     , RPT_LINE ");
                //sb.AppendLine("     , RPT_AMOUNT ");
                //sb.AppendLine("     , RPT_DATE ");
                //sb.AppendLine("     , CARD_NUM ");
                //sb.AppendLine("     , START_DATE ");
                //sb.AppendLine("     , LAST_AMOUNT ");
                //sb.AppendLine(" ) values ( ");
                //sb.AppendLine("     @maxKey ");
                //sb.AppendLine("     , @MAN_CODE ");
                //sb.AppendLine("     , @p_MAN_NAME ");
                //sb.AppendLine("     , 0 ");
                //sb.AppendLine("     , 0 ");
                //sb.AppendLine("     , 0 ");
                //sb.AppendLine("     , 0 ");
                //sb.AppendLine("     , 0 ");
                //sb.AppendLine("     , '' ");
                //sb.AppendLine("     , '' ");
                //sb.AppendLine("     , '1990-01-01' ");
                //sb.AppendLine("     , 0 ");
                //sb.AppendLine(" ) ");

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                //sCommand.Parameters.AddWithValue("@CUST_CODE", txtCode.Text);
                sCommand.Parameters.AddWithValue("@CUST_NAME", txtName.Text);
                sCommand.Parameters.AddWithValue("@REP_NAME", txt대표자명.Text);
                sCommand.Parameters.AddWithValue("@COMP_NUM", txt사업자번호.Text);
                sCommand.Parameters.AddWithValue("@RES_NUM", txt주민번호.Text);
                sCommand.Parameters.AddWithValue("@ZIP_CODE1", txt사업우편번호.Text);
                sCommand.Parameters.AddWithValue("@ZIP_AREA1", txt사업주소1.Text);
                sCommand.Parameters.AddWithValue("@ZIP_ADDR1", txt사업주소2.Text);
                sCommand.Parameters.AddWithValue("@TEL_NUM1", txt회사전화.Text);
                sCommand.Parameters.AddWithValue("@ZIP_CODE2", "");
                sCommand.Parameters.AddWithValue("@ZIP_AREA2", "");
                sCommand.Parameters.AddWithValue("@ZIP_ADDR2", "");
                sCommand.Parameters.AddWithValue("@TEL_NUM2", txt회사팩스.Text);
                sCommand.Parameters.AddWithValue("@BUSINESS_STATUS", txt업태.Text);
                sCommand.Parameters.AddWithValue("@BUSINESS_KIND", txt종목.Text);
                sCommand.Parameters.AddWithValue("@REG_DATE", dtp거래개시일.Text);
                sCommand.Parameters.AddWithValue("@DEAL_TYPE", "" + cmb거래형태.SelectedValue.ToString());
                sCommand.Parameters.AddWithValue("@SUPPLY_KIND", "0"); //0=비해당, 1=해당
                sCommand.Parameters.AddWithValue("@MAN_CODE", txt담당자코드.Text);
                sCommand.Parameters.AddWithValue("@MAJOR_CODE", "" + cmb진료과목.SelectedValue.ToString());
                sCommand.Parameters.AddWithValue("@DEAL_KIND", "" + cmb거래형태.SelectedValue.ToString());
                sCommand.Parameters.AddWithValue("@TRANS_KIND", "01"); //01=택배
                sCommand.Parameters.AddWithValue("@LIMIT_AMOUNT", txt여신한도.Text.Replace(",", ""));
                sCommand.Parameters.AddWithValue("@BASE_AMOUNT", "0"); //기초금액
                sCommand.Parameters.AddWithValue("@BILL_AMOUNT", "0"); //어음금액
                sCommand.Parameters.AddWithValue("@MEMO", txt비고.Text);
                sCommand.Parameters.AddWithValue("@MEMO1", "");
                sCommand.Parameters.AddWithValue("@MEMO2", "");
                sCommand.Parameters.AddWithValue("@CUST_NUM", txt요양기관번호.Text);
                sCommand.Parameters.AddWithValue("@CUST_SAIL", txt기초잔고.Text.Replace(",", ""));
                sCommand.Parameters.AddWithValue("@담보종류", txt담보종류.Text);
                sCommand.Parameters.AddWithValue("@담보금액", txt담보금액.Text.Replace(",", ""));

                sCommand.Parameters.AddWithValue("@p_MAN_NAME", txt담당자명.Text);

                int qResult = wAdo.SqlCommandEtc(sCommand, "Insert CUSTOMER_PRE");
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
            bool bRet = false;

            try
            {
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                sb.AppendLine(" update CUSTOMER_PRE set ");
                sb.AppendLine("     CUST_NAME = @CUST_NAME ");
                sb.AppendLine("     , REP_NAME = @REP_NAME ");
                sb.AppendLine("     , COMP_NUM = @COMP_NUM ");
                sb.AppendLine("     , RES_NUM = @RES_NUM ");
                sb.AppendLine("     , ZIP_CODE1 = @ZIP_CODE1 ");
                sb.AppendLine("     , ZIP_AREA1 = @ZIP_AREA1 ");
                sb.AppendLine("     , ZIP_ADDR1 = @ZIP_ADDR1 ");
                sb.AppendLine("     , TEL_NUM1 = @TEL_NUM1 ");
                //sb.AppendLine("     , ZIP_CODE2 = @ZIP_CODE2 ");
                //sb.AppendLine("     , ZIP_AREA2 = @ZIP_AREA2 ");
                //sb.AppendLine("     , ZIP_ADDR2 = @ZIP_ADDR2 ");
                sb.AppendLine("     , TEL_NUM2 = @TEL_NUM2 ");
                sb.AppendLine("     , BUSINESS_STATUS = @BUSINESS_STATUS ");
                sb.AppendLine("     , BUSINESS_KIND = @BUSINESS_KIND ");
                sb.AppendLine("     , REG_DATE = @REG_DATE ");
                sb.AppendLine("     , DEAL_TYPE = @DEAL_TYPE ");
                //sb.AppendLine("     , SUPPLY_KIND = @SUPPLY_KIND ");
                sb.AppendLine("     , MAN_CODE = @MAN_CODE ");
                sb.AppendLine("     , MAJOR_CODE = @MAJOR_CODE ");
                sb.AppendLine("     , DEAL_KIND = @DEAL_KIND ");
                //sb.AppendLine("     , TRANS_KIND = @TRANS_KIND ");
                sb.AppendLine("     , LIMIT_AMOUNT = @LIMIT_AMOUNT ");
                //sb.AppendLine("     , BASE_AMOUNT = @BASE_AMOUNT ");
                //sb.AppendLine("     , BILL_AMOUNT = @BILL_AMOUNT ");
                sb.AppendLine("     , MEMO = @MEMO ");
                //sb.AppendLine("     , MEMO1 = @MEMO1 ");
                //sb.AppendLine("     , MEMO2 = @MEMO2 ");
                sb.AppendLine("     , CUST_NUM = @CUST_NUM ");
                sb.AppendLine("     , CUST_SAIL = @CUST_SAIL ");
                sb.AppendLine("     , 담보종류 = @담보종류 ");
                sb.AppendLine("     , 담보금액 = @담보금액 ");
                sb.AppendLine(" where 1=1 ");
                sb.AppendLine("     and CUST_CODE = @p1 ");

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@p1", txtCode.Text);
                sCommand.Parameters.AddWithValue("@CUST_NAME", txtName.Text);
                sCommand.Parameters.AddWithValue("@REP_NAME", txt대표자명.Text);
                sCommand.Parameters.AddWithValue("@COMP_NUM", txt사업자번호.Text);
                sCommand.Parameters.AddWithValue("@RES_NUM", txt주민번호.Text);
                sCommand.Parameters.AddWithValue("@ZIP_CODE1", txt사업우편번호.Text);
                sCommand.Parameters.AddWithValue("@ZIP_AREA1", txt사업주소1.Text);
                sCommand.Parameters.AddWithValue("@ZIP_ADDR1", txt사업주소2.Text);
                sCommand.Parameters.AddWithValue("@TEL_NUM1", txt회사전화.Text);
                //sCommand.Parameters.AddWithValue("@ZIP_CODE2", "");
                //sCommand.Parameters.AddWithValue("@ZIP_AREA2", "");
                //sCommand.Parameters.AddWithValue("@ZIP_ADDR2", "");
                sCommand.Parameters.AddWithValue("@TEL_NUM2", txt회사팩스.Text);
                sCommand.Parameters.AddWithValue("@BUSINESS_STATUS", txt업태.Text);
                sCommand.Parameters.AddWithValue("@BUSINESS_KIND", txt종목.Text);
                sCommand.Parameters.AddWithValue("@REG_DATE", dtp거래개시일.Text);
                sCommand.Parameters.AddWithValue("@DEAL_TYPE", "" + cmb거래형태.SelectedValue.ToString());
                //sCommand.Parameters.AddWithValue("@SUPPLY_KIND", "0"); //0=비해당, 1=해당
                sCommand.Parameters.AddWithValue("@MAN_CODE", txt담당자코드.Text);
                sCommand.Parameters.AddWithValue("@MAJOR_CODE", "" + cmb진료과목.SelectedValue.ToString());
                sCommand.Parameters.AddWithValue("@DEAL_KIND", "" + cmb거래형태.SelectedValue.ToString());
                //sCommand.Parameters.AddWithValue("@TRANS_KIND", "01"); //01=택배
                sCommand.Parameters.AddWithValue("@LIMIT_AMOUNT", txt여신한도.Text.Replace(",", ""));
                //sCommand.Parameters.AddWithValue("@BASE_AMOUNT", "0"); //기초금액
                //sCommand.Parameters.AddWithValue("@BILL_AMOUNT", "0"); //어음금액
                sCommand.Parameters.AddWithValue("@MEMO", txt비고.Text);
                //sCommand.Parameters.AddWithValue("@MEMO1", "");
                //sCommand.Parameters.AddWithValue("@MEMO2", "");
                sCommand.Parameters.AddWithValue("@CUST_NUM", txt요양기관번호.Text);
                sCommand.Parameters.AddWithValue("@CUST_SAIL", txt기초잔고.Text.Replace(",", ""));
                sCommand.Parameters.AddWithValue("@담보종류", txt담보종류.Text);
                sCommand.Parameters.AddWithValue("@담보금액", txt담보금액.Text.Replace(",", ""));

                int qResult = wAdo.SqlCommandEtc(sCommand, "Update CUSTOMER_PRE");
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
                wnAdo wAdo = new wnAdo();
                StringBuilder sb = new StringBuilder();

                //sb.AppendLine(" delete from MAN_BASEAMOUNT ");
                //sb.AppendLine(" where CUST_CODE = @p1  ");

                sb.AppendLine(" delete from CUSTOMER_PRE ");
                sb.AppendLine(" where CUST_CODE = @p1  ");

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@p1", this.txtCode.Text);

                int qResult = wAdo.SqlCommandEtc(sCommand, "Delete CUSTOMER_PRE");
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

        private bool get_Dup_Check(string sNew, string sOld)
        {
            try
            {
                if (sNew != sOld)
                {
                    wnDm wDm = new wnDm();
                    DataTable dt = null;
                    dt = wDm.fn_CUSTOMER_Detail(sNew);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
                return true;
            }

        }

        private void LastTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                btnSave.Focus();
            }
        }

        private void btnFirst_Click(object sender, System.EventArgs e)
        {
            this.goFirst();
        }

        private void btnPrevious_Click(object sender, System.EventArgs e)
        {
            this.goPrevious();
        }

        private void btnNext_Click(object sender, System.EventArgs e)
        {
            this.goNext();
        }

        private void btnLast_Click(object sender, System.EventArgs e)
        {
            this.goLast();
        }

        private void goFirst()
        {
            intCurrentPage = 0;

            //loadPage();

            cmbPage.SelectedIndex = intCurrentPage;
        }

        private void goPrevious()
        {
            if (intCurrentPage == intPageCount)
                intCurrentPage = intPageCount - 1;

            intCurrentPage--;

            if (intCurrentPage < 1)
                intCurrentPage = 0;

            //loadPage();

            cmbPage.SelectedIndex = intCurrentPage;
        }

        private void goNext()
        {
            intCurrentPage++;

            if (intCurrentPage > (intPageCount - 1))
                intCurrentPage = intPageCount - 1;

            //loadPage();

            cmbPage.SelectedIndex = intCurrentPage;
        }

        private void goLast()
        {
            intCurrentPage = intPageCount - 1;

            //loadPage();

            cmbPage.SelectedIndex = intCurrentPage;
        }

        private void cmbPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (intTotalRecords > 0)
            {
                intCurrentPage = cmbPage.SelectedIndex;

                loadPage();
            }
            GridRecord.Focus();
        }

        private int getCount()
        {
            int intCount = 0;

            wnDm wDm = new wnDm();
            DataTable dt = null;
            dt = wDm.fn_CUSTOMER_List_Count(this.makeSearchCondition());

            if (dt != null)
            {
                intCount = int.Parse(dt.Rows[0]["CNT"].ToString());
            }

            return intCount;
        }

        private void loadPage()
        {
            this.lblStatus.Text = "- / -";

            try
            {
                int intSkip = 0;
                intSkip = (intCurrentPage * intPageSize);

                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_CUSTOMER_PRE_List(this.makeSearchCondition(), intPageSize, intSkip);

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.GridRecord.RowCount = dt.Rows.Count;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.GridRecord.Rows[i].Cells[0].Value = dt.Rows[i]["CUST_CODE"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[1].Value = dt.Rows[i]["CUST_NAME"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[2].Value = dt.Rows[i]["REP_NAME"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[3].Value = dt.Rows[i]["COMP_NUM"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[4].Value = dt.Rows[i]["TEL_NUM1"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[5].Value = dt.Rows[i]["REG_DATE"].ToString().Trim().Replace("/", "-").Replace(" ", "");
                        this.GridRecord.Rows[i].Cells[6].Value = dt.Rows[i]["BUSINESS_STATUS"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[7].Value = dt.Rows[i]["BUSINESS_KIND"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[8].Value = dt.Rows[i]["RES_NUM"].ToString().Trim();
                    }
                }

                this.lblStatus.Text = (intCurrentPage + 1).ToString() + " / " + intPageCount.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show("검색중에 오류가 발생했습니다.");
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
            }
        }

        private void btn사업우편_Click(object sender, EventArgs e)
        {
            Popup.pop우편검색 frm = new Popup.pop우편검색();

            frm.ShowDialog();

            if (frm.sRetCode != "")
            {
                txt사업우편번호.Text = frm.sRetCode.Trim();
                txt사업주소1.Text = frm.sRetName.Trim();
            }

            frm.Dispose();
            frm = null;
        }

        private void get_Man_Info(string sCode)
        {
            try
            {
                //wnDm wDm = new wnDm();
                //DataTable dt = null;
                //dt = wDm.fn_User_Detail(sCode);

                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    txt부서코드.Text = dt.Rows[0]["USER_DEPT"].ToString().Trim();
                //    txt부서명.Text = dt.Rows[0]["CODE_DESC"].ToString().Trim();
                //}
            }
            catch (Exception ex)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
            }
        }

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

    }
}
