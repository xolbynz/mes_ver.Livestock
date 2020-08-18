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
    public partial class frm반품서 : Form
    {
        private wnGConstant wConst = new wnGConstant();
        private bool bData = false;
        private bool bEditText = false;
        private int currRow = -1;
        private int currCol = -1;

        private string sProgFolder = "";
        private string sBodyHH = "";
        private string sColumnsWW = "";
        private string sSettingFile = "return_saler.xml";
        private bool bSetFirst = true;

        public frm반품서()
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
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex < 0) return;

            conDataGridView grd = (conDataGridView)sender;

            // 수량, 금액 = 0 자료 구분
            grd.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Gray;
            grd.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Gray;

            // 수량, 금액 != 0 자료 구분
            if (grd.Rows[e.RowIndex].Cells[7].Value != null && grd.Rows[e.RowIndex].Cells[9].Value != null)
            {
                if (decimal.Parse("" + (string)grd.Rows[e.RowIndex].Cells[7].Value) > 0 && decimal.Parse("" + (string)grd.Rows[e.RowIndex].Cells[9].Value) > 0)
                {
                    grd.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                    grd.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;
                }
            }
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
                        wConst.get_Prod_Info(grd, e.RowIndex, sFindCode, txt거래처코드.Text, txt특매처.Text, txt거래형태.Text, txt주사제퍼센트.Text, txt도매퍼센트.Text);
                        wConst.Calc_Price(grd, e.RowIndex);

                        grd.Rows[e.RowIndex].Cells[e.ColumnIndex - 3].Value = grd.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value;
                        grd.Rows[e.RowIndex].Cells[e.ColumnIndex - 2].Value = grd.Rows[e.RowIndex].Cells[e.ColumnIndex - 0].Value;
                        //정상 제품인지 확인용으로 사용되므로, 신규 제품시에 제품명을 같이 입력해 둠.
                        grd.Rows[e.RowIndex].Cells[16].Value = grd.Rows[e.RowIndex].Cells[e.ColumnIndex - 0].Value;
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

            //if (e.ColumnIndex == 15)
            //{
            //    // 금액을 수정시, 낱개단가/박스단가 역계산하여 먼저, 설정함.
            //    if (decimal.Parse("" + (string)grd.Rows[e.RowIndex].Cells[12].Value) != 0)
            //    {
            //        grd.Rows[e.RowIndex].Cells[14].Value = (decimal.Parse("" + (string)grd.Rows[e.RowIndex].Cells[15].Value)
            //            / decimal.Parse("" + (string)grd.Rows[e.RowIndex].Cells[12].Value)
            //            ).ToString(Common.p_strFormatUnit);
            //    }
            //    //grd.Rows[e.RowIndex].Cells[13].Value = (decimal.Parse("" + (string)grd.Rows[e.RowIndex].Cells[14].Value)
            //    //    * decimal.Parse("" + (string)grd.Rows[e.RowIndex].Cells[21].Value)
            //    //    ).ToString("#,0");
            //}


            if (grd.Columns[e.ColumnIndex].ToolTipText.IndexOf("수량") >= 0
                || grd.Columns[e.ColumnIndex].ToolTipText.IndexOf("단가") >= 0
                || grd.Columns[e.ColumnIndex].ToolTipText.IndexOf("금액") >= 0)
            {
                wConst.Calc_Price(grd, e.RowIndex);
            }
        }

        #endregion 입력 그리드 제어 #####################################################################################################

        private void frm반품서_Load(object sender, EventArgs e)
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
            
            grdItem.Rows.Clear();
            grdItem.Rows.Add();

            btnSave.Enabled = true;
            txt거래처명.ReadOnly = false;
            btn거래처.Enabled = true;
            cmb반품사유.SelectedIndex = 0;

            if (bNew == true)
            {
                bData = false;
                btnDelete.Enabled = false;
                dtp일자.Enabled = true;
                rb급여.Checked = true;
                //txt배송.Text = "01";  // 01=택배

                cmb구분.Enabled = false;
                cmb구분.SelectedValue = "22";
            }
            else
            {
                bData = true;
                btnDelete.Enabled = true;
                dtp일자.Enabled = false;
                txt거래처명.ReadOnly = true;
                btn거래처.Enabled = false;

                cmb구분.Enabled = false;
            }
        }

        private void init_ComboBox()
        {
            string sqlQuery = "";

            wConst.set_Combo판매구분(false, cmb구분, true);

            cmb반품사유.ValueMember = "코드";
            cmb반품사유.DisplayMember = "반품사유내용";
            sqlQuery = " select convert(nvarchar(10), 반품사유) as 코드, 반품사유내용 from T_RETURN_REASON where 1=1 ";
            wConst.ComboBox_Read_Blank(cmb반품사유, sqlQuery);

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

        private string makeSearchCondition()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" and a.반품일자 >= '" + dtpDay1.Text + "' ");
            sb.Append(" and a.반품일자 <= '" + dtpDay2.Text + "' ");

            switch (this.txtS거래처코드.Text)
            {
                case "":
                    sb.Append("");
                    break;
                default:
                    sb.Append(" and a.거래처코드 = '" + txtS거래처코드.Text + "' ");
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
                dt = wDm.fn_PRODUCT_RETURN_List(condition);

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.GridRecord.RowCount = dt.Rows.Count;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.GridRecord.Rows[i].Cells[0].Value = "";
                        this.GridRecord.Rows[i].Cells[1].Value = (i + 1).ToString();
                        this.GridRecord.Rows[i].Cells[2].Value = dt.Rows[i]["반품일자"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[3].Value = dt.Rows[i]["반품번호"].ToString().Trim();
                        //this.GridRecord.Rows[i].Cells[4].Value = wConst.get_판매구분_명칭(dt.Rows[i]["구분"].ToString().Trim());
                        this.GridRecord.Rows[i].Cells[5].Value = dt.Rows[i]["거래처코드"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[6].Value = dt.Rows[i]["거래처명"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[7].Value = dt.Rows[i]["창고확인"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[8].Value = dt.Rows[i]["관리부확인"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[9].Value = dt.Rows[i]["주문일자"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[10].Value = dt.Rows[i]["주문번호"].ToString().Trim();
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

        private void btn선택삭제_Click(object sender, EventArgs e)
        {
            grdItem.Columns[0].HeaderText = "[ ]";
            int nCnt = 0;

            // 마지막 new row 제외
            for (int kk = grdItem.Rows.Count - 2; kk >= 0; kk--)
            {
                if ((bool)grdItem.Rows[kk].Cells[0].Value == true)
                {
                    nCnt += 1;
                    grdItem.Rows.RemoveAt(kk);
                }
            }
            // 마지막 new row 체크 해제.
            grdItem.Rows[grdItem.Rows.Count - 1].Cells[0].Value = false;

            if (nCnt > 0) MessageBox.Show(nCnt.ToString() + "개의 제품이 삭제되었습니다.");
            grdItem.Focus();
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
                grdItem._KeyInput = "f6";
            }
            //if (e.KeyCode == Keys.F7)
            //{
            //    e.Handled = true;
            //    grdItem._KeyInput = "f7";
            //}
            if (e.KeyCode == Keys.F8)
            {
                e.Handled = true;
                grdItem._KeyInput = "f8";
            }
            if (e.KeyCode == Keys.F11)
            {
                e.Handled = true;
                grdItem._KeyInput = "f11";
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
                dt = wDm.fn_PRODUCT_RETURN_Detail(sKey, sKey2);

                if (dt != null && dt.Rows.Count > 0)
                {
                    bEditText = false;

                    dtp일자.Text = dt.Rows[0]["반품일자"].ToString().Trim();
                    lbl번호.Text = dt.Rows[0]["반품번호"].ToString().Trim();
                    cmb구분.SelectedValue = dt.Rows[0]["구분"].ToString().Trim();
                    cmb반품사유.SelectedValue = dt.Rows[0]["반품사유"].ToString().Trim();

                    // PRODUCT_KIND = PRODUCT_KIND1 으로 처리.
                    if (dt.Rows[0]["급여"].ToString().Trim() == "10")
                    {
                        rb급여.Checked = true;
                    }
                    else
                    {
                        rb비급여.Checked = true;
                    }
                    txt거래처코드old.Text = dt.Rows[0]["거래처코드"].ToString().Trim();
                    txt거래처코드.Text = dt.Rows[0]["거래처코드"].ToString().Trim();
                    txt거래처명.Text = dt.Rows[0]["거래처명"].ToString().Trim();
                    lbl대표자.Text = dt.Rows[0]["대표자명"].ToString().Trim();
                    lbl사업자번호.Text = dt.Rows[0]["사업자번호"].ToString().Trim();
                    txt담당자코드old.Text = dt.Rows[0]["담당자코드"].ToString().Trim();
                    txt담당자코드.Text = dt.Rows[0]["담당자코드"].ToString().Trim();
                    txt담당자명.Text = dt.Rows[0]["담당자명"].ToString().Trim();
                    //txt비고.Text = dt.Rows[0]["MEMO"].ToString().Trim();
                    //txt비고1.Text = dt.Rows[0]["MEMO1"].ToString().Trim();
                    //txt비고2.Text = dt.Rows[0]["MEMO2"].ToString().Trim();
                    //txt비고3.Text = dt.Rows[0]["MEMO3"].ToString().Trim();
                    //txt비고4.Text = dt.Rows[0]["MEMO4"].ToString().Trim();

                    //txt배송.Text = dt.Rows[0]["TRANS_KIND"].ToString().Trim();
                    txt거래형태.Text = dt.Rows[0]["거래형태"].ToString().Trim();
                    txt이메일.Text = dt.Rows[0]["EMAIL"].ToString().Trim();
                    txt거래분류.Text = dt.Rows[0]["DEAL_KIND"].ToString().Trim();
                    txt특매처.Text = dt.Rows[0]["특매처"].ToString().Trim();
                    txt부서코드.Text = dt.Rows[0]["부서코드"].ToString().Trim();
                    txt부서명.Text = dt.Rows[0]["부서명"].ToString().Trim();

                    decimal nTot = wConst.get_거래처별_잔고(txt거래처코드.Text, dtp일자.Text);
                    lbl현잔고.Text = nTot.ToString("#,0");

                    // 창고확인 : 수정/삭제 불가
                    if (dt.Rows[0]["창고확인"].ToString().Trim() == "확인")
                    {
                        btnSave.Enabled = false;
                        btnDelete.Enabled = false;
                    }

                    //// 판매, 반품 외의 자료는 수정 불가
                    //if (dt.Rows[0]["STOCK_KIND"].ToString().Trim() != "11" && dt.Rows[0]["STOCK_KIND"].ToString().Trim() != "22")
                    //{
                    //    btnSave.Enabled = false;
                    //}

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
                        // 반품 건만 있는 테이블이어서, 음수 처리 불필요.
                        decimal nFlag = 1;
                        grdItem.Rows[kk].Cells[7].Value = (nFlag * decimal.Parse(dt.Rows[kk]["입력수량"].ToString().Trim())).ToString(Common.p_strFormatAmount);
                        grdItem.Rows[kk].Cells[8].Value = decimal.Parse(dt.Rows[kk]["입력단가"].ToString().Trim()).ToString(Common.p_strFormatUnit);
                        grdItem.Rows[kk].Cells[9].Value = (nFlag * decimal.Parse(dt.Rows[kk]["입력금액"].ToString().Trim())).ToString("#,0");
                        grdItem.Rows[kk].Cells[10].Value = (nFlag * decimal.Parse(dt.Rows[kk]["입력부가세"].ToString().Trim())).ToString("#,0");
                        //grdItem.Rows[kk].Cells[11].Value = decimal.Parse(dt.Rows[kk]["할인율"].ToString().Trim()).ToString("#,0");
                        //grdItem.Rows[kk].Cells[12].Value = decimal.Parse(dt.Rows[kk]["계약율"].ToString().Trim()).ToString("#,0");
                        //if (txt특매처.Text == "1")
                        //{
                        //    grdItem.Rows[kk].Cells[12].Value = "0";
                        //}
                        //grdItem.Rows[kk].Cells[13].Value = dt.Rows[kk]["표준단가"].ToString().Trim();

                        //grdItem.Rows[kk].Cells[14].Value = dt.Rows[kk]["이전주문일자"].ToString().Trim().Replace("/", "-");
                        grdItem.Rows[kk].Cells[14].Value = dt.Rows[kk]["이전주문일자"].ToString().Trim();
                        grdItem.Rows[kk].Cells[15].Value = dt.Rows[kk]["비고"].ToString().Trim();
                        grdItem.Rows[kk].Cells[16].Value = dt.Rows[kk]["제품명확인"].ToString().Trim();

                        ////wConst.Calc_Price(grdItem, kk, txt부가세코드.Text);
                    }
                }
                grdItem.Rows.Add();
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

        private void tmFocusNew_Tick(object sender, EventArgs e)
        {
            tmFocusNew.Enabled = false;
            txt거래처명.Focus();
        }

        private void tmCalc_Tick(object sender, EventArgs e)
        {
            tmCalc.Enabled = false;

            get_SumPrice();

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

        private void tmCell_Tick(object sender, EventArgs e)
        {
            tmCell.Enabled = false;
            grdItem.Rows[currRow].Cells[currCol].ReadOnly = false;
        }

        private void tmCellBack_Tick(object sender, EventArgs e)
        {
            tmCellBack.Enabled = false;
            SendKeys.Send("{LEFT}");
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

            lbl금액.Text = nSumPrice.ToString("#,0");
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

                    if (txt거래형태.Text == "2")
                    {
                        // 2=도매 : 입력불가
                        MessageBox.Show("[ 도매 ] 거래처는 주문을 입력할수 없습니다.");
                    }
                    if (txt이메일.Text == "")
                    {
                        // 이메일이 없으면 입력 불가 
                        MessageBox.Show("[ 이메일주소 ] 없는 거래처는 주문을 입력할수 없습니다.");
                    }
                    if (lbl사업자번호.Text == "")
                    {
                        // 이메일이 없으면 입력 불가 
                        MessageBox.Show("[ 사업자번호 ] 없는 거래처는 주문을 입력할수 없습니다.");
                    }

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
                //    MessageBox.Show("[ 도매 ] 거래처는 반품을 입력할수 없습니다.");
                //    return false;
                //}
                if (txt이메일.Text == "")
                {
                    // 이메일이 없으면 입력 불가 
                    MessageBox.Show("[ 이메일주소 ] 없는 거래처는 반품을 입력할수 없습니다.");
                    return false;
                }
                if (lbl사업자번호.Text == "")
                {
                    // 이메일이 없으면 입력 불가 
                    MessageBox.Show("[ 사업자번호 ] 없는 거래처는 반품을 입력할수 없습니다.");
                    return false;
                }

                if (txt담당자코드.Text == "")
                {
                    MessageBox.Show("[ 담당자 ] 를 입력하세요.");
                    return false;
                }

                if ("" + cmb반품사유.SelectedValue.ToString() == "")
                {
                    MessageBox.Show("[ 반품사유 ] 를 선택하세요.");
                    return false;
                }

                bool bZeroChk = false;
                bool bDayChk = false;
                bool bProdChk = false;
                string sErrProd = "";
                for (int kk = 0; kk < grdItem.Rows.Count; kk++)
                {
                    if ("" + (string)grdItem.Rows[kk].Cells[4].Value != "")
                    {
                        // 수량 = 0 체크
                        if (decimal.Parse("" + (string)grdItem.Rows[kk].Cells[7].Value) == 0)
                        {
                            bZeroChk = true;
                            break;
                        }
                        // 단가 = 0 체크
                        if (decimal.Parse("" + (string)grdItem.Rows[kk].Cells[8].Value) == 0)
                        {
                            bZeroChk = true;
                            break;
                        }
                        // 출하일자 = '' 체크
                        if ("" + (string)grdItem.Rows[kk].Cells[14].Value == "")
                        {
                            bDayChk = true;
                            break;
                        }
                        // 제품 정상 = '' 체크
                        if ("" + (string)grdItem.Rows[kk].Cells[16].Value == "")
                        {
                            sErrProd = (string)grdItem.Rows[kk].Cells[5].Value + " " + (string)grdItem.Rows[kk].Cells[6].Value;
                            bProdChk = true;
                            break;
                        }
                    }
                }
                if (bZeroChk == true)
                {
                    MessageBox.Show("제품의 [ 수량 또는, 단가 ] 가 '0'인 것이 있습니다.");
                    return false;
                }
                if (bDayChk == true)
                {
                    MessageBox.Show("제품의 [ 출하일자 ] 를 확인하세요.");
                    return false;
                }
                if (bProdChk == true)
                {
                    MessageBox.Show("정상제품이 아닌 것이 있습니다.\r\n제품 : " + sErrProd + " ");
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
            bool bRet = false;

            try
            {
                wnAdo wAdo = new wnAdo();

                StringBuilder sb = new StringBuilder();
                sb = new StringBuilder();

                string sRetVal = "";

                sb.AppendLine(" select right('000' + convert(nvarchar(4), isnull(max(convert(int, 반품번호)), 0) + 1), 4) ");
                sb.AppendLine(" from PRODUCT_RETURN ");
                sb.AppendLine(" where 반품일자 = '" + dtp일자.Text + "' ");
                //sb.AppendLine("     and substring(반품번호, 1, 1) = '0' ");

                sRetVal = wConst.maxValue_Check(sb.ToString());
                if (sRetVal == "")
                {
                    MessageBox.Show("데이터 검색 중 오류 발생!!!");
                    return false;
                }

                lbl번호.Text = sRetVal;

                sb = new StringBuilder();

                sb.AppendLine("declare @maxKey nvarchar(4) ");
                sb.AppendLine("set @maxKey = '" + sRetVal + "' ");

                int nRowCnt = 0;

                for (int kk = 0; kk < grdItem.Rows.Count; kk++)
                {
                    string s코드 = "";
                    string s명칭 = "";
                    string s규격 = "";
                    string s수량 = "";
                    string s단가 = "";
                    string s금액 = "";
                    string s부가세 = "";
                    //string s할인율 = "";
                    //string s표준단가 = "";
                    string s출하일자 = "";
                    string s비고 = "";

                    s코드 = "" + (string)grdItem.Rows[kk].Cells[4].Value;
                    s명칭 = "" + (string)grdItem.Rows[kk].Cells[5].Value;
                    s규격 = "" + (string)grdItem.Rows[kk].Cells[6].Value;
                    s수량 = ("" + (string)grdItem.Rows[kk].Cells[7].Value).Replace(",", "");
                    s단가 = ("" + (string)grdItem.Rows[kk].Cells[8].Value).Replace(",", "");
                    s금액 = ("" + (string)grdItem.Rows[kk].Cells[9].Value).Replace(",", "");
                    s부가세 = ("" + (string)grdItem.Rows[kk].Cells[10].Value).Replace(",", "");
                    //s할인율 = ("" + (string)grdItem.Rows[kk].Cells[11].Value).Replace(",", "");
                    //s표준단가 = ("" + (string)grdItem.Rows[kk].Cells[13].Value).Replace(",", "");
                    s출하일자 = "" + (string)grdItem.Rows[kk].Cells[14].Value;
                    s비고 = "" + (string)grdItem.Rows[kk].Cells[15].Value;
                    
                    if (s코드 != "")
                    {
                        nRowCnt += 1;

                        sb.AppendLine(" insert into PRODUCT_RETURN ( ");
                        sb.AppendLine("     반품일자 ");
                        sb.AppendLine("     , 반품번호 ");
                        sb.AppendLine("     , 구분 ");
                        sb.AppendLine("     , 거래처코드 ");
                        sb.AppendLine("     , 거래처명 ");
                        sb.AppendLine("     , 담당자코드 ");
                        sb.AppendLine("     , 담당자명 ");
                        sb.AppendLine("     , 부서코드 ");
                        sb.AppendLine("     , 부서명 ");
                        sb.AppendLine("     , 대표자명 ");
                        sb.AppendLine("     , 사업자번호 ");
                        sb.AppendLine("     , 거래형태 ");
                        sb.AppendLine("     , 급여 ");
                        sb.AppendLine("     , 반품사유 ");
                        sb.AppendLine("     , 반품사유내용 ");
                        sb.AppendLine("     , 순서 ");
                        sb.AppendLine("     , 제품코드 ");
                        sb.AppendLine("     , 제품명 ");
                        sb.AppendLine("     , 규격 ");
                        sb.AppendLine("     , 입력수량 ");
                        sb.AppendLine("     , 입력할증수량 ");
                        sb.AppendLine("     , 입력단가 ");
                        sb.AppendLine("     , 입력금액 ");
                        sb.AppendLine("     , 입력부가세 ");
                        sb.AppendLine("     , 이전주문일자 ");
                        sb.AppendLine("     , 이전주문번호 ");
                        sb.AppendLine("     , 반품구분 ");
                        sb.AppendLine("     , 비고 ");
                        sb.AppendLine(" ) values ( ");
                        sb.AppendLine("     @반품일자 ");
                        sb.AppendLine("     , @maxKey ");
                        sb.AppendLine("     , '" + cmb구분.SelectedValue.ToString() + "' "); //구분
                        sb.AppendLine("     , '" + txt거래처코드.Text + "' "); //거래처코드
                        sb.AppendLine("     , '" + txt거래처명.Text + "' "); //거래처명
                        sb.AppendLine("     , '" + txt담당자코드.Text + "' "); //담당자코드
                        sb.AppendLine("     , '" + txt담당자명.Text + "' "); //담당자명
                        sb.AppendLine("     , '" + txt부서코드.Text + "' "); //부서코드
                        sb.AppendLine("     , '" + txt부서명.Text + "' "); //부서명
                        sb.AppendLine("     , '" + lbl대표자.Text + "' "); //대표자명
                        sb.AppendLine("     , '" + lbl사업자번호.Text + "' "); //사업자번호
                        sb.AppendLine("     , '" + txt거래형태.Text + "' "); //거래형태
                        if (rb급여.Checked == true)
                        {
                            sb.AppendLine("     , '10' "); //급여
                        }
                        else
                        {
                            sb.AppendLine("     , '20' "); //급여
                        }
                        sb.AppendLine("     , '" + cmb반품사유.SelectedValue.ToString() + "' "); //반품사유
                        sb.AppendLine("     , '" + cmb반품사유.Text + "' "); //반품사유내용
                        sb.AppendLine("     , " + nRowCnt.ToString() + " "); //순서
                        sb.AppendLine("     , '" + s코드 + "' ");
                        sb.AppendLine("     , '" + s명칭 + "' "); //제품명
                        sb.AppendLine("     , '" + s규격 + "' "); //규격

                        sb.AppendLine("     , " + s수량 + " "); //입력수량
                        sb.AppendLine("     , 0 "); //입력할증수량
                        sb.AppendLine("     , " + s단가 + " "); //입력단가
                        sb.AppendLine("     , " + s금액 + " "); //입력금액
                        sb.AppendLine("     , " + s부가세 + " "); //입력부가세

                        sb.AppendLine("     , '" + s출하일자 + "' "); //이전주문일자
                        sb.AppendLine("     , '' "); //이전주문번호
                        sb.AppendLine("     , '' "); //반품구분
                        sb.AppendLine("     , '" + s비고 + "' "); //비고
                        sb.AppendLine(" ) ");
                    }
                }

                if (nRowCnt == 0)
                {
                    MessageBox.Show("[ 상품 정보 ] 를 입력하세요.");
                    return false;
                }

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@반품일자", dtp일자.Text);

                int qResult = wAdo.SqlCommandEtc(sCommand, "Insert-PRODUCT_RETURN");
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

                // 순서 = -1 : 아래 처리 후 삭제 대상임.
                sb.AppendLine(" update PRODUCT_RETURN set ");
                sb.AppendLine("     순서 = -1 ");
                sb.AppendLine(" where 반품일자 = @반품일자 ");
                sb.AppendLine("     and 반품번호 = @p1 ");

                int nRowCnt = 0;

                for (int kk = 0; kk < grdItem.Rows.Count; kk++)
                {
                    string s코드 = "";
                    string s명칭 = "";
                    string s규격 = "";
                    string s수량 = "";
                    string s단가 = "";
                    string s금액 = "";
                    string s부가세 = "";
                    //string s할인율 = "";
                    //string s표준단가 = "";
                    string s출하일자 = "";
                    string s비고 = "";

                    s코드 = "" + (string)grdItem.Rows[kk].Cells[4].Value;
                    s명칭 = "" + (string)grdItem.Rows[kk].Cells[5].Value;
                    s규격 = "" + (string)grdItem.Rows[kk].Cells[6].Value;
                    s수량 = ("" + (string)grdItem.Rows[kk].Cells[7].Value).Replace(",", "");
                    s단가 = ("" + (string)grdItem.Rows[kk].Cells[8].Value).Replace(",", "");
                    s금액 = ("" + (string)grdItem.Rows[kk].Cells[9].Value).Replace(",", "");
                    s부가세 = ("" + (string)grdItem.Rows[kk].Cells[10].Value).Replace(",", "");
                    //s할인율 = ("" + (string)grdItem.Rows[kk].Cells[11].Value).Replace(",", "");
                    //s표준단가 = ("" + (string)grdItem.Rows[kk].Cells[13].Value).Replace(",", "");
                    s출하일자 = "" + (string)grdItem.Rows[kk].Cells[14].Value;
                    s비고 = "" + (string)grdItem.Rows[kk].Cells[15].Value;

                    if (s코드 != "")
                    {
                        nRowCnt += 1;

                        sb.AppendLine(" select @cnt = count(*) from PRODUCT_RETURN ");
                        sb.AppendLine(" where 반품일자 = @반품일자 ");
                        sb.AppendLine("     and 반품번호 = @p1 ");
                        sb.AppendLine("     and 제품코드 = '" + s코드 + "' ");

                        sb.AppendLine(" if (@cnt = 0) ");
                        sb.AppendLine(" begin ");

                        sb.AppendLine(" insert into PRODUCT_RETURN ( ");
                        sb.AppendLine("     반품일자 ");
                        sb.AppendLine("     , 반품번호 ");
                        sb.AppendLine("     , 구분 ");
                        sb.AppendLine("     , 거래처코드 ");
                        sb.AppendLine("     , 거래처명 ");
                        sb.AppendLine("     , 담당자코드 ");
                        sb.AppendLine("     , 담당자명 ");
                        sb.AppendLine("     , 부서코드 ");
                        sb.AppendLine("     , 부서명 ");
                        sb.AppendLine("     , 대표자명 ");
                        sb.AppendLine("     , 사업자번호 ");
                        sb.AppendLine("     , 거래형태 ");
                        sb.AppendLine("     , 급여 ");
                        sb.AppendLine("     , 반품사유 ");
                        sb.AppendLine("     , 반품사유내용 ");
                        sb.AppendLine("     , 순서 ");
                        sb.AppendLine("     , 제품코드 ");
                        sb.AppendLine("     , 제품명 ");
                        sb.AppendLine("     , 규격 ");
                        sb.AppendLine("     , 입력수량 ");
                        sb.AppendLine("     , 입력할증수량 ");
                        sb.AppendLine("     , 입력단가 ");
                        sb.AppendLine("     , 입력금액 ");
                        sb.AppendLine("     , 입력부가세 ");
                        sb.AppendLine("     , 이전주문일자 ");
                        sb.AppendLine("     , 이전주문번호 ");
                        sb.AppendLine("     , 반품구분 ");
                        sb.AppendLine("     , 비고 ");
                        sb.AppendLine(" ) values ( ");
                        sb.AppendLine("     @반품일자 ");
                        sb.AppendLine("     , @p1 ");
                        sb.AppendLine("     , '" + cmb구분.SelectedValue.ToString() + "' "); //구분
                        sb.AppendLine("     , '" + txt거래처코드.Text + "' "); //거래처코드
                        sb.AppendLine("     , '" + txt거래처명.Text + "' "); //거래처명
                        sb.AppendLine("     , '" + txt담당자코드.Text + "' "); //담당자코드
                        sb.AppendLine("     , '" + txt담당자명.Text + "' "); //담당자명
                        sb.AppendLine("     , '" + txt부서코드.Text + "' "); //부서코드
                        sb.AppendLine("     , '" + txt부서명.Text + "' "); //부서명
                        sb.AppendLine("     , '" + lbl대표자.Text + "' "); //대표자명
                        sb.AppendLine("     , '" + lbl사업자번호.Text + "' "); //사업자번호
                        sb.AppendLine("     , '" + txt거래형태.Text + "' "); //거래형태
                        if (rb급여.Checked == true)
                        {
                            sb.AppendLine("     , '10' "); //급여
                        }
                        else
                        {
                            sb.AppendLine("     , '20' "); //급여
                        }
                        sb.AppendLine("     , '" + cmb반품사유.SelectedValue.ToString() + "' "); //반품사유
                        sb.AppendLine("     , '" + cmb반품사유.Text + "' "); //반품사유내용
                        sb.AppendLine("     , " + nRowCnt.ToString() + " "); //순서
                        sb.AppendLine("     , '" + s코드 + "' ");
                        sb.AppendLine("     , '" + s명칭 + "' "); //제품명
                        sb.AppendLine("     , '" + s규격 + "' "); //규격

                        sb.AppendLine("     , " + s수량 + " "); //입력수량
                        sb.AppendLine("     , 0 "); //입력할증수량
                        sb.AppendLine("     , " + s단가 + " "); //입력단가
                        sb.AppendLine("     , " + s금액 + " "); //입력금액
                        sb.AppendLine("     , " + s부가세 + " "); //입력부가세

                        sb.AppendLine("     , '" + s출하일자 + "' "); //이전주문일자
                        sb.AppendLine("     , '' "); //이전주문번호
                        sb.AppendLine("     , '' "); //반품구분
                        sb.AppendLine("     , '" + s비고 + "' "); //비고
                        sb.AppendLine(" ) ");

                        sb.AppendLine(" end ");
                        sb.AppendLine(" else ");
                        sb.AppendLine(" begin ");

                        sb.AppendLine(" update PRODUCT_RETURN set ");
                        sb.AppendLine("     구분 = '" + cmb구분.SelectedValue.ToString() + "' ");
                        sb.AppendLine("     , 거래처코드 = '" + txt거래처코드.Text + "' ");
                        sb.AppendLine("     , 거래처명 = '" + txt거래처명.Text + "' ");
                        sb.AppendLine("     , 담당자코드 = '" + txt담당자코드.Text + "' ");
                        sb.AppendLine("     , 담당자명 = '" + txt담당자명.Text + "' ");
                        sb.AppendLine("     , 부서코드 = '" + txt부서코드.Text + "' ");
                        sb.AppendLine("     , 부서명 = '" + txt부서명.Text + "' ");
                        sb.AppendLine("     , 대표자명 = '" + lbl대표자.Text + "' ");
                        sb.AppendLine("     , 사업자번호 = '" + lbl사업자번호.Text + "' ");
                        sb.AppendLine("     , 거래형태 = '" + txt거래형태.Text + "' ");
                        if (rb급여.Checked == true)
                        {
                            sb.AppendLine("     , 급여 = '10' ");
                        }
                        else
                        {
                            sb.AppendLine("     , 급여 = '20' ");
                        }
                        sb.AppendLine("     , 반품사유 = '" + cmb반품사유.SelectedValue.ToString() + "' ");
                        sb.AppendLine("     , 반품사유내용 = '" + cmb반품사유.Text + "' ");
                        sb.AppendLine("     , 순서 = " + nRowCnt.ToString() + " ");
                        sb.AppendLine("     , 제품명 = '" + s명칭 + "' ");
                        sb.AppendLine("     , 규격 = '" + s규격 + "' ");
                        sb.AppendLine("     , 입력수량 = " + s수량 + " ");
                        sb.AppendLine("     , 입력단가 = " + s단가 + " ");
                        sb.AppendLine("     , 입력금액 = " + s금액 + " ");
                        sb.AppendLine("     , 입력부가세 = " + s부가세 + " ");
                        sb.AppendLine("     , 이전주문일자 = '" + s출하일자 + "' ");
                        sb.AppendLine("     , 비고 = '" + s비고 + "' ");
                        sb.AppendLine(" where 반품일자 = @반품일자 ");
                        sb.AppendLine("     and 반품번호 = @p1 ");
                        sb.AppendLine("     and 제품코드 = '" + s코드 + "' ");

                        sb.AppendLine(" end ");

                    }
                }

                if (nRowCnt == 0)
                {
                    MessageBox.Show("[ 상품 정보 ] 를 입력하세요.");
                    return false;
                }

                sb.AppendLine(" delete from PRODUCT_RETURN ");
                sb.AppendLine(" where 반품일자 = @반품일자 ");
                sb.AppendLine("     and 반품번호 = @p1 ");
                sb.AppendLine("     and 순서 = -1 ");

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@반품일자", dtp일자.Text);
                sCommand.Parameters.AddWithValue("@p1", lbl번호.Text);

                int qResult = wAdo.SqlCommandEtc(sCommand, "Update-PRODUCT_RETURN");
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

                sb.AppendLine(" delete from PRODUCT_RETURN ");
                sb.AppendLine(" where 반품일자 = @반품일자 ");
                sb.AppendLine("     and 반품번호 = @p1 ");

                SqlCommand sCommand = new SqlCommand(sb.ToString());

                sCommand.Parameters.AddWithValue("@반품일자", this.dtp일자.Text);
                sCommand.Parameters.AddWithValue("@p1", this.lbl번호.Text);

                int qResult = wAdo.SqlCommandEtc(sCommand, "Delete-PRODUCT_RETURN");
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

        private void tmCmdKey_Tick(object sender, EventArgs e)
        {
            tmCmdKey.Enabled = false;

            //if (grdItem._KeyInput == "f2")
            //{
            //    grdItem._KeyInput = "";
            //    if (txt거래처코드.Text != "" && lbl번호.Text != "")
            //    {
            //        call_SameCust_Prev();
            //    }
            //}
            //if (grdItem._KeyInput == "f3")
            //{
            //    grdItem._KeyInput = "";
            //    if (txt거래처코드.Text != "" && lbl번호.Text != "")
            //    {
            //        call_SameCust_Next();
            //    }
            //}

            if (grdItem._KeyInput == "f6")
            {
                grdItem._KeyInput = "";
                btnNew_Click(this, null);
            }

            //if (grdItem._KeyInput == "f7")
            //{
            //    grdItem._KeyInput = "";
            //    if (btnDelete.Enabled == true) btnPrtSheet_Click(this, null);
            //}

            if (grdItem._KeyInput == "f8")
            {
                grdItem._KeyInput = "";
                if (btnSave.Enabled == true) btnSave_Click(this, null);
            }

            if (grdItem._KeyInput == "f11")
            {
                grdItem._KeyInput = "";
                if (btnDelete.Enabled == true) btnDelete_Click(this, null);
            }

            tmCmdKey.Enabled = true;
        }

    }
}
