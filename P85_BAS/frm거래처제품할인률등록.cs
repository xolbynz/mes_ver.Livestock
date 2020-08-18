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

namespace 스마트팩토리.P85_BAS
{
    public partial class frm거래처제품할인률등록 : Form
    {
        private wnGConstant wConst = new wnGConstant();
        private bool bData = false;
        private bool bEditText = false;

        private string sProgFolder = "";
        private string sBodyHH = "";
        private string sColumnsWW = "";
        private string sSettingFile = "cust_discount.xml";
        private bool bSetFirst = true;

        public frm거래처제품할인률등록()
        {
            InitializeComponent();

            this.grdItem.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellEndEdit);
            this.grdItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grid_KeyDown);
            this.grdItem.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grid_ColumnHeaderMouseClick);
            this.grdItem.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.grid_RowsAdded);
            this.grdItem.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.grid_RowsRemoved);
            this.spCont.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.spCont_SplitterMoved);
            this.grdItem.CellValueChanged += new DataGridViewCellEventHandler(grid_CellValueChanged);
            this.grdItem.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.grid_ColumnWidthChanged);
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

        private void grid_KeyDown(object sender, KeyEventArgs e)
        {
            // Edit 모드가 아닐때, 작동함.

            conDataGridView grd = (conDataGridView)sender;
            DataGridViewCell cell = grd[grd.CurrentCell.ColumnIndex, grd.CurrentCell.RowIndex];

            if (grd.CurrentCell == null) return;
            if (grd.CurrentCell.RowIndex < 0) return;
            if (grd.CurrentCell.ColumnIndex < 0) return;

            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
                e.Handled = true;
            }
        }

        private void grid_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            conDataGridView grd = (conDataGridView)sender;

            for (int kk = 0; kk < grd.Rows.Count; kk++)
            {
                grd.Rows[kk].Cells[1].Value = (kk + 1).ToString();
            }
        }

        private void grid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            conDataGridView grd = (conDataGridView)sender;

            grd.Rows[e.RowIndex].Cells[0].Value = false;

            // No.
            grd.Rows[e.RowIndex].Cells[1].Style.BackColor = Color.WhiteSmoke;
            grd.Rows[e.RowIndex].Cells[1].Style.SelectionBackColor = Color.Khaki;

            // 코드
            grd.Rows[e.RowIndex].Cells[4].Style.BackColor = Color.WhiteSmoke;
            grd.Rows[e.RowIndex].Cells[4].Style.SelectionBackColor = Color.Khaki;

            // 규격
            grd.Rows[e.RowIndex].Cells[6].Style.BackColor = Color.WhiteSmoke;
            grd.Rows[e.RowIndex].Cells[6].Style.SelectionBackColor = Color.Khaki;

            // 금액
            grd.Rows[e.RowIndex].Cells[9].Style.BackColor = Color.WhiteSmoke;
            grd.Rows[e.RowIndex].Cells[9].Style.SelectionBackColor = Color.Khaki;

            // 계약율
            grd.Rows[e.RowIndex].Cells[12].Style.BackColor = Color.WhiteSmoke;
            grd.Rows[e.RowIndex].Cells[12].Style.SelectionBackColor = Color.Khaki;

            wConst.init_RowText(grd, e.RowIndex);

            for (int kk = 0; kk < grd.Rows.Count; kk++)
            {
                grd.Rows[kk].Cells[1].Value = (kk + 1).ToString();
            }
        }

        private void grid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            conDataGridView grd = (conDataGridView)sender;

            // 헤더 첫 컬럼 클릭시
            if (e.ColumnIndex != 0) return;

            if (bHeadCheck == false)
            {
                grd.Columns[0].HeaderText = "[v]";
                bHeadCheck = true;
                select_Check(grd, bHeadCheck);
            }
            else
            {
                grd.Columns[0].HeaderText = "[ ]";
                bHeadCheck = false;
                select_Check(grd, bHeadCheck);
            }
            grd.RefreshEdit();
            grd.Refresh();
        }

        private bool bHeadCheck = false;
        private void select_Check(conDataGridView grd, bool bCheck)
        {
            for (int kk = 0; kk < grd.Rows.Count; kk++)
            {
                if (bCheck == true)
                {
                    grd.Rows[kk].Cells[0].Value = true;
                }
                else
                {
                    grd.Rows[kk].Cells[0].Value = false;
                }
            }
        }

        private void grid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex < 0) return;
            //if (e.ColumnIndex < 0) return;

            //conDataGridView grd = (conDataGridView)sender;

            //// 수량, 금액 = 0 자료 구분
            //grd.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Gray;
            //grd.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Gray;

            //// 수량, 금액 != 0 자료 구분
            //if (grd.Rows[e.RowIndex].Cells[7].Value != null && grd.Rows[e.RowIndex].Cells[9].Value != null)
            //{
            //    if (decimal.Parse("" + (string)grd.Rows[e.RowIndex].Cells[7].Value) > 0 && decimal.Parse("" + (string)grd.Rows[e.RowIndex].Cells[9].Value) > 0)
            //    {
            //        grd.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            //        grd.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;
            //    }
            //}
        }

        private void grid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            conDataGridView grd = (conDataGridView)sender;
            DataGridViewCell cell = grd[e.ColumnIndex, e.RowIndex];

            cell.Style.BackColor = Color.White;

            #region 공통 그리드 체크 ###################################################################

            if (grd.Columns[e.ColumnIndex].ToolTipText.IndexOf("수량") >= 0)
            {
                try
                {
                    cell.Value = (Math.Floor(decimal.Parse("" + (string)cell.Value) * 100) / 100).ToString("#,0.##");
                }
                catch
                {
                    cell.Value = "0";
                }
            }
            if (grd.Columns[e.ColumnIndex].ToolTipText.IndexOf("단가") >= 0)
            {
                try
                {
                    cell.Value = (Math.Floor(decimal.Parse("" + (string)cell.Value) * 10000000) / 10000000).ToString("#,0.#######");
                }
                catch
                {
                    cell.Value = "0";
                }
            }
            if (grd.Columns[e.ColumnIndex].ToolTipText.IndexOf("금액") >= 0)
            {
                try
                {
                    cell.Value = (Math.Floor(decimal.Parse("" + (string)cell.Value) * 1) / 1).ToString("#,0");
                }
                catch
                {
                    cell.Value = "0";
                }
            }

            #endregion 공통 그리드 체크 ###################################################################

            if (grd.Columns[e.ColumnIndex].ToolTipText.IndexOf("명칭") >= 0 && grd._KeyInput == "enter")
            {
                if (txt거래처코드.Text == "")
                {
                    MessageBox.Show("[ 거래처 ]를 입력하세요.");
                    tmCellBack.Enabled = true;
                    return;
                }
                string sSearchTxt = "" + (string)grd.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                if (sSearchTxt == "")
                {
                    grd.Rows[e.RowIndex].Cells[e.ColumnIndex - 3].Value = "";
                    grd.Rows[e.RowIndex].Cells[e.ColumnIndex - 2].Value = "";
                    grd.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value = "";
                }

                string sOldCode = "" + (string)grd.Rows[e.RowIndex].Cells[e.ColumnIndex - 3].Value;
                string sOldName = "" + (string)grd.Rows[e.RowIndex].Cells[e.ColumnIndex - 2].Value;

                if (sOldCode == "" || sOldName != sSearchTxt)
                {
                    wConst.call_pop_Prod(grd, e.RowIndex, sSearchTxt, txt거래처코드.Text, e.ColumnIndex - 1, e.ColumnIndex, "0");

                    string sFindCode = "" + (string)grd.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value;

                    // 상품 중복입력 체크 ================================================
                    if (sFindCode != "")
                    {
                        for (int kk = 0; kk < grd.Rows.Count; kk++)
                        {
                            if (kk != e.RowIndex)
                            {
                                if ("" + (string)grd.Rows[kk].Cells[e.ColumnIndex - 1].Value == sFindCode)
                                {
                                    MessageBox.Show("이미 입력한 제품입니다.");
                                    sFindCode = "";
                                    break;
                                }
                            }
                        }
                    }
                    // 상품 중복입력 체크 ================================================ end

                    if (sOldCode != sFindCode)
                    {
                        //wConst.get_Prod_Info(grd, e.RowIndex, sFindCode, txt거래처코드.Text, txt특매처.Text, txt거래형태.Text, txt주사제퍼센트.Text, txt도매퍼센트.Text);
                        //wConst.Calc_Price(grd, e.RowIndex);

                        grd.Rows[e.RowIndex].Cells[e.ColumnIndex - 3].Value = grd.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value;
                        grd.Rows[e.RowIndex].Cells[e.ColumnIndex - 2].Value = grd.Rows[e.RowIndex].Cells[e.ColumnIndex - 0].Value;
                    }
                    else
                    {
                        grd.Rows[e.RowIndex].Cells[e.ColumnIndex - 0].Value = sOldName;
                    }

                    if (sFindCode == "")
                    {
                        wConst.init_RowText(grd, e.RowIndex);
                    }

                }
            }

            if (grd.Columns[e.ColumnIndex].ToolTipText.IndexOf("명칭") >= 0 && grd._KeyInput != "enter")
            {
                string sCurrCode = "" + (string)grd.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value;
                string sSearchTxt = "" + (string)grd.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                if (sSearchTxt == "")
                {
                    wConst.init_RowText(grd, e.RowIndex);
                }
                else
                {
                    //wConst.get_Prod_Info_Code(grd, e.RowIndex, sCurrCode);

                    if ("" + (string)grd.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == "")
                    {
                        wConst.init_RowText(grd, e.RowIndex);
                    }
                }
            }

        }

        #endregion 입력 그리드 제어 #####################################################################################################

        private void frm거래처제품할인률등록_Load(object sender, EventArgs e)
        {
            spCont.Dock = DockStyle.Fill;

            lblSearch.BringToFront();
            lblBody.BringToFront();

            sProgFolder = Path.GetDirectoryName(System.Environment.GetCommandLineArgs()[0]);
            Read_Layout();
            setting_Layout();

            this.init_ComboBox();
            this.init_InputBox(true);
            this.bindData(this.makeSearchCondition());
        }

        private void init_ComboBox()
        {
            
        }

        private void init_InputBox(bool bNew)
        {
            wConst.Form_Clear(spCont.Panel1.Controls);

            grdItem.Rows.Clear();
            grdItem.Rows.Add();

            if (bNew == true)
            {
                bData = false;
                btnDelete.Enabled = false;
                txt거래처명.Enabled = true;
                btn거래처.Enabled = true;
            }
            else
            {
                bData = true;
                btnDelete.Enabled = true;
                txt거래처명.Enabled = false;
                btn거래처.Enabled = false;
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
                dt = wDm.fn_CUST_DISCOUNT_List(condition);

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.GridRecord.RowCount = dt.Rows.Count;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.GridRecord.Rows[i].Cells[0].Value = dt.Rows[i]["CUST_CODE"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[1].Value = dt.Rows[i]["CUST_NAME"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[2].Value = dt.Rows[i]["REP_NAME"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[3].Value = dt.Rows[i]["MAN_CODE"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[4].Value = dt.Rows[i]["담당자명"].ToString().Trim();
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

        private void GridRecord_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (GridRecord.CurrentCell == null) return;
            if (GridRecord.CurrentCell.RowIndex < 0) return;
            if (GridRecord.CurrentCell.ColumnIndex < 0) return;

            int iRow = GridRecord.CurrentCell.RowIndex;
            //int iKeyCol = GridRecord.ColumnCount - 1;
            int iKeyCol = 0;
            string strValue = "" + (string)GridRecord.Rows[iRow].Cells[iKeyCol].Value;

            getDetailPost(strValue);
            txt거래처코드.Focus();
            grdItem.Focus();
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
                dt = wDm.fn_CUST_DISCOUNT_Detail(sKey);

                if (dt != null && dt.Rows.Count > 0)
                {
                    bEditText = false;

                    this.txt거래처코드old.Text = dt.Rows[0]["거래처코드"].ToString().Trim();
                    this.txt거래처코드.Text = dt.Rows[0]["거래처코드"].ToString().Trim();
                    this.txt거래처명.Text = dt.Rows[0]["거래처명"].ToString().Trim();

                    lbl대표자.Text = dt.Rows[0]["REP_NAME"].ToString().Trim();
                    lbl사업자번호.Text = dt.Rows[0]["COMP_NUM"].ToString().Trim();
                    lbl담당자명.Text = dt.Rows[0]["담당자명"].ToString().Trim();

                    grdItem.Rows.Clear();

                    for (int kk = 0; kk < dt.Rows.Count; kk++)
                    {
                        grdItem.Rows.Add();

                        grdItem.Rows[kk].Cells[0].Value = false;
                        grdItem.Rows[kk].Cells[1].Value = (kk + 1).ToString();
                        grdItem.Rows[kk].Cells[2].Value = dt.Rows[kk]["제품코드"].ToString().Trim();
                        grdItem.Rows[kk].Cells[3].Value = dt.Rows[kk]["제품명"].ToString().Trim();
                        grdItem.Rows[kk].Cells[4].Value = dt.Rows[kk]["제품코드"].ToString().Trim();
                        grdItem.Rows[kk].Cells[5].Value = dt.Rows[kk]["제품명"].ToString().Trim();
                        grdItem.Rows[kk].Cells[6].Value = dt.Rows[kk]["규격"].ToString().Trim();
                        grdItem.Rows[kk].Cells[11].Value = decimal.Parse(dt.Rows[kk]["할인율"].ToString().Trim()).ToString("#0.#");

                    }
                }
                else
                {
                    MessageBox.Show("존재하지 않는 자료입니다.");
                }
                grdItem.Rows.Add();
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
            this.bindData(this.makeSearchCondition());
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            this.init_InputBox(true);
            txt거래처명.Focus();
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
                if (get_Dup_Check(txt거래처코드.Text, "") == true)
                {
                    MessageBox.Show("이미 존재하는 [ 거래처 코드 ]입니다." + " [ " + this.txt거래처코드.Text + " ]");
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
                this.bindData(this.makeSearchCondition());
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
                this.bindData(this.makeSearchCondition());
            }
        }

        private bool validate_InputBox()
        {
            bool bRet = true;

            try
            {
                if (this.txt거래처코드.Text.Trim() == "")
                {
                    MessageBox.Show("[ 거래처 코드 ]를 입력하세요.");
                    return false;
                }

                //if (this.txtName.Text.Trim() == "")
                //{
                //    MessageBox.Show("[ 영업소명 ]을 입력하세요.");
                //    return false;
                //}

                //if (this.txt조회순서.Text.Trim() == "")
                //{
                //    this.txt조회순서.Text = "999";
                //}
                //else
                //{
                //    //if (int.Parse(this.txt조회순서.Text.Trim()) == 0)
                //    //{
                //    //    this.txt조회순서.Text = "999";
                //    //}
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

                int nRowCnt = 0;

                for (int kk = 0; kk < grdItem.Rows.Count; kk++)
                {
                    string s코드 = "";
                    string s명칭 = "";
                    string s규격 = "";
                    string s할인율 = "";

                    s코드 = "" + (string)grdItem.Rows[kk].Cells[4].Value;
                    s명칭 = "" + (string)grdItem.Rows[kk].Cells[5].Value;
                    s규격 = "" + (string)grdItem.Rows[kk].Cells[6].Value;
                    s할인율 = ("" + (string)grdItem.Rows[kk].Cells[11].Value).Replace(",", "");

                    if (s코드 != "")
                    {
                        nRowCnt += 1;

                        sb.AppendLine(" insert into CUST_DISCOUNT ( ");
                        sb.AppendLine("     거래처코드 ");
                        sb.AppendLine("     , 거래처명 ");
                        sb.AppendLine("     , 제품코드 ");
                        sb.AppendLine("     , 제품명 ");
                        sb.AppendLine("     , 규격 ");
                        sb.AppendLine("     , 할인율 ");
                        sb.AppendLine("     , 입력자 ");
                        sb.AppendLine("     , 입력자명 ");
                        sb.AppendLine("     , 최종처리자 ");
                        sb.AppendLine("     , 최종처리일 ");
                        sb.AppendLine("     , 비고 ");
                        sb.AppendLine(" ) values ( ");
                        sb.AppendLine("     @거래처코드 ");
                        sb.AppendLine("     , @거래처명 ");
                        sb.AppendLine("     , '" + s코드 + "' ");
                        sb.AppendLine("     , '" + s명칭 + "' ");
                        sb.AppendLine("     , '" + s규격 + "' ");
                        sb.AppendLine("     , " + s할인율 + " ");
                        sb.AppendLine("     , @입력자 ");
                        sb.AppendLine("     , @입력자명 ");
                        sb.AppendLine("     , @최종처리자 ");
                        sb.AppendLine("     , getdate() ");
                        sb.AppendLine("     , '' ");
                        sb.AppendLine(" ) ");
                    }
                }

                if (nRowCnt == 0)
                {
                    MessageBox.Show("[ 상품 정보 ] 를 입력하세요.");
                    return false;
                }

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@거래처코드", txt거래처코드.Text);
                sCommand.Parameters.AddWithValue("@거래처명", txt거래처명.Text);
                sCommand.Parameters.AddWithValue("@입력자", Common.p_strUserID);
                sCommand.Parameters.AddWithValue("@입력자명", "" + Common.p_strUserName);
                sCommand.Parameters.AddWithValue("@최종처리자", "" + Common.p_strUserID);

                int qResult = wAdo.SqlCommandEtc(sCommand, "Insert CUST_DISCOUNT");
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

                sb.AppendLine(" declare @cnt int ");

                // 최종처리자 = -1 : 아래 처리 후 삭제 대상임.
                sb.AppendLine(" update CUST_DISCOUNT set ");
                sb.AppendLine("     최종처리자 = '(삭제)' ");
                sb.AppendLine(" where 거래처코드 = @p1  ");

                int nRowCnt = 0;

                for (int kk = 0; kk < grdItem.Rows.Count; kk++)
                {
                    string s코드 = "";
                    string s명칭 = "";
                    string s규격 = "";
                    string s할인율 = "";

                    s코드 = "" + (string)grdItem.Rows[kk].Cells[4].Value;
                    s명칭 = "" + (string)grdItem.Rows[kk].Cells[5].Value;
                    s규격 = "" + (string)grdItem.Rows[kk].Cells[6].Value;
                    s할인율 = ("" + (string)grdItem.Rows[kk].Cells[11].Value).Replace(",", "");

                    if (s코드 != "")
                    {
                        nRowCnt += 1;

                        sb.AppendLine(" select @cnt = count(*) from CUST_DISCOUNT ");
                        sb.AppendLine(" where 거래처코드 = @p1 ");
                        sb.AppendLine("     and 제품코드 = '" + s코드 + "' ");

                        sb.AppendLine(" if (@cnt = 0) ");
                        sb.AppendLine(" begin ");

                        // ####################################################################################
                        sb.AppendLine(" insert into CUST_DISCOUNT ( ");
                        sb.AppendLine("     거래처코드 ");
                        sb.AppendLine("     , 거래처명 ");
                        sb.AppendLine("     , 제품코드 ");
                        sb.AppendLine("     , 제품명 ");
                        sb.AppendLine("     , 규격 ");
                        sb.AppendLine("     , 할인율 ");
                        sb.AppendLine("     , 입력자 ");
                        sb.AppendLine("     , 입력자명 ");
                        sb.AppendLine("     , 최종처리자 ");
                        sb.AppendLine("     , 최종처리일 ");
                        sb.AppendLine("     , 비고 ");
                        sb.AppendLine(" ) values ( ");
                        sb.AppendLine("     @p1 ");
                        sb.AppendLine("     , @거래처명 ");
                        sb.AppendLine("     , '" + s코드 + "' ");
                        sb.AppendLine("     , '" + s명칭 + "' ");
                        sb.AppendLine("     , '" + s규격 + "' ");
                        sb.AppendLine("     , " + s할인율 + " ");
                        sb.AppendLine("     , @입력자 ");
                        sb.AppendLine("     , @입력자명 ");
                        sb.AppendLine("     , @최종처리자 ");
                        sb.AppendLine("     , getdate() ");
                        sb.AppendLine("     , '' ");
                        sb.AppendLine(" ) ");

                        sb.AppendLine(" end ");
                        sb.AppendLine(" else ");
                        sb.AppendLine(" begin ");

                        // ####################################################################################
                        sb.AppendLine(" update CUST_DISCOUNT set ");
                        sb.AppendLine("     할인율 = " + s할인율 + " ");
                        sb.AppendLine("     , 최종처리자 = @최종처리자 ");
                        sb.AppendLine("     , 최종처리일 = getdate() ");
                        sb.AppendLine(" where 거래처코드 = @p1 ");
                        sb.AppendLine("     and 제품코드 = '" + s코드 + "' ");

                        sb.AppendLine(" end ");
                    }
                }

                if (nRowCnt == 0)
                {
                    MessageBox.Show("[ 상품 정보 ] 를 입력하세요.");
                    return false;
                }

                sb.AppendLine(" delete from CUST_DISCOUNT ");
                sb.AppendLine(" where 거래처코드 = @p1 ");
                sb.AppendLine("     and 최종처리자 = '(삭제)' ");

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@p1", txt거래처코드old.Text);
                sCommand.Parameters.AddWithValue("@거래처명", txt거래처명.Text);
                sCommand.Parameters.AddWithValue("@입력자", Common.p_strUserID);
                sCommand.Parameters.AddWithValue("@입력자명", "" + Common.p_strUserName);
                sCommand.Parameters.AddWithValue("@최종처리자", "" + Common.p_strUserID);

                int qResult = wAdo.SqlCommandEtc(sCommand, "Update CUST_DISCOUNT");
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

                sb.AppendLine(" delete from CUST_DISCOUNT ");
                sb.AppendLine(" where 거래처코드 = @p1  ");

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@p1", this.txt거래처코드old.Text);

                int qResult = wAdo.SqlCommandEtc(sCommand, "Delete CUST_DISCOUNT");
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
                    dt = wDm.fn_CUST_DISCOUNT_Detail(sNew);

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
                    lbl담당자명.Text = dt.Rows[0]["CODE_DESC"].ToString().Trim();
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
            lbl담당자명.Text = "";
        }

        #endregion 입력 거래처 ##############################################################################################################

        private void tmCellBack_Tick(object sender, EventArgs e)
        {
            tmCellBack.Enabled = false;
            SendKeys.Send("{LEFT}");
        }

        private void tmCmdKey_Tick(object sender, EventArgs e)
        {
            tmCmdKey.Enabled = false;

            ////if (grdItem._KeyInput == "f2")
            ////{
            ////    grdItem._KeyInput = "";
            ////    if (txt거래처코드.Text != "" && lbl번호.Text != "")
            ////    {
            ////        call_SameCust_Prev();
            ////    }
            ////}
            ////if (grdItem._KeyInput == "f3")
            ////{
            ////    grdItem._KeyInput = "";
            ////    if (txt거래처코드.Text != "" && lbl번호.Text != "")
            ////    {
            ////        call_SameCust_Next();
            ////    }
            ////}

            //if (grdItem._KeyInput == "f6")
            //{
            //    grdItem._KeyInput = "";
            //    btnNew_Click(this, null);
            //}

            ////if (grdItem._KeyInput == "f7")
            ////{
            ////    grdItem._KeyInput = "";
            ////    if (btnDelete.Enabled == true) btnPrtSheet_Click(this, null);
            ////}

            //if (grdItem._KeyInput == "f8")
            //{
            //    grdItem._KeyInput = "";
            //    if (btnSave.Enabled == true) btnSave_Click(this, null);
            //}

            //if (grdItem._KeyInput == "f11")
            //{
            //    grdItem._KeyInput = "";
            //    if (btnDelete.Enabled == true) btnDelete_Click(this, null);
            //}

            tmCmdKey.Enabled = true;
        }

        private void tmCalc_Tick(object sender, EventArgs e)
        {
            tmCalc.Enabled = false;

            //get_SumPrice();

            // 마지막 빈 row 추가기능
            if (grdItem.CurrentRow != null)
            {
                if (grdItem.CurrentRow.Index == grdItem.Rows.Count - 1 && ("" + (string)grdItem.CurrentRow.Cells[4].Value).Length > 0)
                {
                    grdItem.RowCount = grdItem.Rows.Count + 1;
                }
            }

            tmCalc.Enabled = true;
        }

    }
}
