using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 스마트팩토리.CLS;

namespace 스마트팩토리.S30_PAY
{
    public partial class frm지출등록 : Form
    {
        private wnGConstant wConst = new wnGConstant();
        private DataTable del_inputGrid = new DataTable();
        private DateTimePicker dtp = new DateTimePicker();
        private ComInfo comInfo = new ComInfo();
        private string cmbstaff = "";
        private string cmbid = "";

        private DataGridView del_srchGrid = new DataGridView();

        public frm지출등록()
        {
            //
            InitializeComponent();

           // this.dgv_main.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.grid_RowsAdded);
        }

        private void frm지출등록_Load(object sender, EventArgs e)
        {
            //init_ComboBox();

            //del_srchGrid.AllowUserToAddRows = false;

            //del_inputGrid.Columns.Add("PAY_DATE");
            //del_inputGrid.Columns.Add("PAY_CD");
            //del_inputGrid.Columns.Add("SEQ");

            //txt_instaff.Text = Common.p_strUserName;


            //resetSetting();

            //ComInfo.gridHeaderSet(dgv_main);
            //ComInfo.gridHeaderSet(dgv_srch);
            //ComInfo.gridHeaderSet(dgv_staff);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //private void btnNew_Click(object sender, EventArgs e)
        //{
        //    resetSetting();
        //}

        //private void btnSave_Click(object sender, EventArgs e)
        //{
        //    out_logic();
        //}

        //private void btnDelete_Click(object sender, EventArgs e)
        //{
        //    if (txt_num.Text == "")
        //    {
        //        MessageBox.Show("번호를 입력해주세요");
        //    }
        //    else
        //    {
        //        if (MessageBox.Show("정말 삭제 하시겠습니까?", "삭제경고", MessageBoxButtons.YesNo) == DialogResult.Yes)
        //        {
        //            out_delete();
        //        }
        //        else
        //        {
        //        }
        //    }
        //}

        //private void btn_srch_Click(object sender, EventArgs e)
        //{
        //    dgvsrchlist();
        //}

        //private void resetSetting()
        //{
        //    lbl_gbn.Text = "";
        //    btnDelete.Enabled = false;
        //    dtp_outdate.Enabled = true;
        //    cmb_staff.Enabled = true;
        //    txt_num.ReadOnly = true;
        //    // txt_inputdate.ReadOnly = false;
        //    txt_staff.ReadOnly = false;

        //    cmb_staff.SelectedIndex = 0;
        //    cmb_staff2.SelectedIndex = 0;
        //    cmb_id.SelectedIndex = 0;

        //    txt_num.Text = "";
        //    txt_inputdate.Text = "";
        //    txt_comment.Text = "";
        //    txt_total.Text = "";
        //    txt_staff.Text = "";
        //    txt_instaff.Text = Common.p_strUserName;

        //    dtp_start.Text = DateTime.Today.AddDays(-7).ToString("yyyy-MM-dd");
        //    dtp_end.Text = DateTime.Today.ToString("yyyy-MM-dd");
        //    dtp_outdate.Value = DateTime.Today;

        //    dgv_main.Rows.Clear();
        //    dgv_staff.Rows.Clear();
        //    dgv_srch.Rows.Clear();

        //    del_inputGrid.Rows.Clear();

        //    add_logic();

        //    dgvsrchlist();

        //    //del_srchGrid.Rows.Clear();

        //}

        //private void out_delete()
        //{
        //    Model.Query.chaQuery CQ = new Model.Query.chaQuery();
        //    DataTable dt = new DataTable();

        //    if (dt.Rows.Count > 0)
        //    {
        //        MessageBox.Show("이미 출고된 원재료가 있기 때문에 삭제할 수 없습니다.");
        //        return;
        //    }
        //    int rsNum = CQ.OutDelete(
        //        dtp_outdate.Text.ToString()
        //        , txt_num.Text.ToString()
        //        );
        //    if (rsNum == 0)
        //    {
        //        resetSetting();
        //        dgvsrchlist();

        //        MessageBox.Show("성공적으로 삭제하였습니다.");
        //    }
        //    else if (rsNum == 1)
        //    {
        //        MessageBox.Show("삭제에 실패하였습니다.");
        //    }
        //}

        //private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        //{

        //}

        //private void out_logic()
        //{
        //    int cmbs = cmb_staff.SelectedIndex;
        //    string staffindex = cmb_staff.Items[cmbs].ToString();
        //    int cmbi = cmb_id.SelectedIndex;
        //    string idindex = cmb_id.Items[cmbi].ToString();

        //    for (int i = 0; i < dgv_main.Rows.Count; i++)
        //    {
        //        if (dgv_main.Rows[i].Cells["MONEY"].Value == null || dgv_main.Rows[i].Cells["MONEY"].Value.ToString().Equals("") ||
        //            dgv_main.Rows[i].Cells["MONEY"].Value.ToString().Equals("0"))
        //        {
        //            dgv_main.Rows.RemoveAt(i);
        //            i = 0;
        //        }
        //    }

        //    if (dgv_main.Rows.Count == 0)
        //    {
        //        MessageBox.Show("최소 하나이상의 지출을 입력해주세요");
        //        return;
        //    }
        //    if (cmb_staff.SelectedValue == null || cmb_staff.SelectedValue.ToString().Equals(""))
        //    {
        //        MessageBox.Show("사원을 선택해주세요");
        //        return;
        //    }


        //    try
        //    {
        //        if (lbl_gbn.Text != "1")
        //        {
        //            Model.Query.chaQuery CQ = new Model.Query.chaQuery();
        //            DataTable dt = new DataTable();


        //            int rsNum = CQ.OutInsert(
        //                dtp_outdate.Text,
        //                txt_comment.Text,
        //                cmb_staff.SelectedValue.ToString(),
        //                dgv_main
        //                );

        //            if (rsNum == 0)
        //            {
        //                resetSetting();
        //                dgvsrchlist();
        //                MessageBox.Show("성공적으로 등록하였습니다.");
        //            }
        //            else if (rsNum == 1)
        //                MessageBox.Show("저장에 실패하였습니다");
        //            else if (rsNum == 2)
        //                MessageBox.Show("SQL COMMAND 에러");
        //            else
        //                MessageBox.Show("Exception 에러1");
        //        }
        //        else
        //        {
        //            Model.Query.chaQuery CQ = new Model.Query.chaQuery();
        //            int rsNum = CQ.OutUpdate(
        //                dtp_outdate.Text,
        //                txt_num.Text,
        //                cmb_staff.SelectedValue.ToString(),
        //                txt_comment.Text,
        //                dgv_main,
        //                del_inputGrid
        //                 );

        //            if (rsNum == 0)
        //            {
        //                resetSetting();
        //                MessageBox.Show("성공적으로 수정하였습니다.");
        //            }
        //            else if (rsNum == 1)
        //                MessageBox.Show("수정 실패하였습니다");
        //            else if (rsNum == 2)
        //                MessageBox.Show("SQL COMMAND 에러");
        //            else
        //                MessageBox.Show("Exception 에러1");
        //        }
        //    }
        //    catch (Exception e)
        //    {

        //        MessageBox.Show(e.Message.ToString());
        //    }
        //}

        ////dgvsrch 셀렉트
        //public void dgvsrchlist()
        //{
        //    //this.cmbid = (string)this.cmb_id.SelectedValue ?? "";
        //    //this.cmbstaff = (string)this.cmb_staff.SelectedValue ?? "";
        //    dgv_srch.Rows.Clear();
        //    string condition = "WHERE PAY_DATE >= '" + dtp_start.Text.ToString() + "' and  PAY_DATE <= '" + dtp_end.Text.ToString() + "'";

        //    Model.Query.chaQuery CQ = new Model.Query.chaQuery();
        //    DataTable dt = new DataTable();

        //    //dt = qCtrl.fn_WorkList(" where A.WORK_DATE >= '" + strDay1 + "' and  A.WORK_DATE <= '" + strDay2 + "' ");

        //    dt = CQ.OutSrchSelect(condition);

        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            dgv_srch.Rows.Add();
        //            dgv_srch.Rows[i].Cells["OUT_DATE"].Value = dt.Rows[i]["PAY_DATE"].ToString();
        //            dgv_srch.Rows[i].Cells["NUM"].Value = dt.Rows[i]["PAY_CD"].ToString();
        //            dgv_srch.Rows[i].Cells["STAFF"].Value = dt.Rows[i]["STAFF_NM"].ToString();
        //            dgv_srch.Rows[i].Cells["ITEM_CNT"].Value = dt.Rows[i]["ITEM_CNT"].ToString();
        //            dgv_srch.Rows[i].Cells["SRCH_MONEY"].Value = decimal.Parse(dt.Rows[i]["TOTAL_MONEY"].ToString()).ToString("#,0.######");
        //            dgv_srch.Rows[i].Cells["COMMENT"].Value = dt.Rows[i]["COMMENT"].ToString();
        //            dgv_srch.Rows[i].Cells["INSTAFF"].Value = dt.Rows[i]["INSTAFF"].ToString();
        //            dgv_srch.Rows[i].Cells["INTIME"].Value = dt.Rows[i]["INTIME"].ToString();
        //            dgv_srch.Rows[i].Cells["STAFF_CD"].Value = dt.Rows[i]["STAFF_CD"].ToString();
        //        }
        //    }
        //}

        //private void grid_spend_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        //{
        //    //this.grdPayInsert.Rows[e.RowIndex].Cells[1].Value = (e.RowIndex + 1).ToString();
        //}



        //private void pnl_left_Panel1_Resize(object sender, EventArgs e)
        //{
        //    //pnl_left_mid.Height = pnl_left.SplitterDistance - 100;
        //    //grdPayInsert.Height = pnl_left_mid.Height;
        //    //Console.WriteLine(pnl_left.SplitterDistance);
        //    //Console.WriteLine(pnl_left_mid.Size.Height);
        //    //Console.WriteLine(grdPayInsert.Height);
        //}

        //private void add_logic()
        //{
        //    dgv_main.Rows.Add();
        //    dgv_main.Rows[dgv_main.Rows.Count - 1].Cells["JUKYO"].Value = "0";
        //    dgv_main.Rows[dgv_main.Rows.Count - 1].Cells["MONEY"].Value = "0";
        //}


        //private void txt_sumPrice_TextChanged(object sender, EventArgs e)
        //{
        //    //string lgsText;
        //    //TextBox txtselect = sender as TextBox;

        //    //lgsText = txtselect.Text.Replace(",", ""); //** 숫자변환시 콤마로 발생하는 에러방지...

        //    //txtselect.Text = String.Format("{0:#,##0}", Convert.ToDouble(lgsText));


        //    //txtselect.SelectionStart = txtselect.TextLength; //** 캐럿을 맨 뒤로 보낸다...

        //    //txtselect.SelectionLength = 0;
        //}

        ////오름차순 


        //private void frm지출등록_KeyPress(object sender, KeyPressEventArgs e)
        //{

        //}

        //private void btn_excel_Click(object sender, EventArgs e)
        //{
        //    if (this.dgv_srch.Rows.Count != 0)
        //    {
        //        SaveFileDialog saveFileDialog = new SaveFileDialog()
        //        {
        //            Filter = "Excel 통합 문서(*.xlsx)|*.xlsx|Excel 97 - 2003 통합문서(*.xls)|*.xls|CSV (쉼표로 분리)(*.csv)|*.csv",
        //            FileName = "지출전표목록(" + dtp_start.Value.ToString().Substring(0, 10) + "~" + dtp_end.Value.ToString().Substring(0, 10) + ").xls"
        //        };
        //        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        //        {
        //            //Label width = this.lblMsg;
        //            Size clientSize = base.ClientSize;
        //            //width.Left = clientSize.Width / 2 - this.lblMsg.Width / 2;
        //            // Label height = this.lblMsg;
        //            clientSize = base.ClientSize;
        //            //height.Top = clientSize.Height / 2 - this.lblMsg.Height / 2;
        //            //this.lblMsg.Text = "Exporting ...";
        //            //this.lblMsg.Visible = true;
        //            //this.lblMsg.BringToFront();
        //            Application.DoEvents();
        //            //this.wConst2.convert_to_CSV_XLS(this.grdSrch, saveFileDialog.FileName);
        //            //this.lblMsg.Visible = false;
        //            Application.DoEvents();
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("다운받을 자료가 없습니다.");
        //    }
        //}

        //private void dTime_insert_ValueChanged(object sender, EventArgs e)
        //{

        //}

        ////srch 그리드 더블클릭 했을경우 메인그리드 및 스텝 그리드에 데이터 출력
        //private void dgv_srch_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    btnDelete.Enabled = true;
        //    dtp_outdate.Enabled = false;
        //    cmb_staff.Enabled = false;
        //    txt_num.ReadOnly = true;
        //    txt_instaff.ReadOnly = true;
        //    // txt_inputdate.ReadOnly = true;
        //    txt_staff.ReadOnly = true;
        //    lbl_gbn.Text = "1";

        //    if (e.RowIndex > -1)
        //    {
        //        dtp_outdate.Text = dgv_srch.Rows[e.RowIndex].Cells["OUT_DATE"].Value.ToString();
        //        txt_num.Text = dgv_srch.Rows[e.RowIndex].Cells["NUM"].Value.ToString();
        //        txt_staff.Text = dgv_srch.Rows[e.RowIndex].Cells["STAFF"].Value.ToString();
        //        txt_inputdate.Text = dgv_srch.Rows[e.RowIndex].Cells["OUT_DATE"].Value.ToString();
        //        txt_comment.Text = dgv_srch.Rows[e.RowIndex].Cells["COMMENT"].Value.ToString();
        //        cmb_staff.SelectedValue = dgv_srch.Rows[e.RowIndex].Cells["STAFF_CD"].Value.ToString();
        //        txt_instaff.Text = dgv_srch.Rows[e.RowIndex].Cells["INSTAFF"].Value.ToString();

        //        Model.Query.chaQuery CQ = new Model.Query.chaQuery();
        //        DataTable dt = new DataTable();

        //        //dt = qCtrl.fn_WorkList(" where A.WORK_DATE >= '" + strDay1 + "' and  A.WORK_DATE <= '" + strDay2 + "' ");

        //        dt = CQ.OutMainSelect("WHERE A.PAY_DATE ='" + dtp_outdate.Text + "'  and A.PAY_CD = '" + txt_num.Text + "'  ");

        //        this.dgv_main.RowCount = dt.Rows.Count;

        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                dgv_main.Rows[i].Cells["ID"].Value = dt.Rows[i]["ACCU_CD"].ToString();
        //                //dgv_main.Rows[i].Cells["GUBUN"].Value = dt.Rows[i][""].ToString();
        //                dgv_main.Rows[i].Cells["JUKYO"].Value = dt.Rows[i]["JUKYO"].ToString();
        //                dgv_main.Rows[i].Cells["MONEY"].Value = decimal.Parse(dt.Rows[i]["MONEY"].ToString()).ToString("#,0.######");
        //                dgv_main.Rows[i].Cells["MAIN_SEQ"].Value = dt.Rows[i]["SEQ"].ToString();
        //                dgv_main.Rows[i].Cells["GUBUN"].Value = dt.Rows[i]["PAY_GUBUN"].ToString();
        //            }
        //        }



        //        //string condition = " WHERE '" + txt_staff.Text + "' = INSTAFF ";
        //        dt = CQ.OutMainSelect("WHERE B.STAFF_CD = '" + cmb_staff.SelectedValue.ToString() + "'   ");

        //        this.dgv_staff.RowCount = dt.Rows.Count;

        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                dgv_staff.Rows[i].Cells["DATE"].Value = dt.Rows[i]["PAY_DATE"].ToString();
        //                dgv_staff.Rows[i].Cells["STAFF_ID"].Value = dt.Rows[i]["ACCU_NM"].ToString();
        //                dgv_staff.Rows[i].Cells["STAFF_MONEY"].Value = decimal.Parse(dt.Rows[i]["MONEY"].ToString()).ToString("#,0.######");

        //            }
        //        }

        //    }
        //}

        //private void init_ComboBox()
        //{
        //    string sqlQuery = "";

        //    //계정 콤보
        //    cmb_id.ValueMember = "코드";
        //    cmb_id.DisplayMember = "명칭";
        //    sqlQuery = comInfo.queryAccu();
        //    wConst.ComboBox_Read_Blank(cmb_id, sqlQuery);

        //    //사원 콤보1
        //    cmb_staff2.ValueMember = "코드";
        //    cmb_staff2.DisplayMember = "명칭";
        //    sqlQuery = comInfo.queryStaff();
        //    wConst.ComboBox_Read_Blank(cmb_staff2, sqlQuery);

        //    //사원 콤보2
        //    cmb_staff.ValueMember = "코드";
        //    cmb_staff.DisplayMember = "명칭";
        //    sqlQuery = comInfo.queryStaff();
        //    wConst.ComboBox_Read_Blank(cmb_staff, sqlQuery);

        //    ID.ValueMember = "코드";
        //    ID.DisplayMember = "명칭";
        //    sqlQuery = comInfo.queryAccu();
        //    wConst.ComboBox_Read_NoBlank(ID, sqlQuery);

        //    GUBUN.ValueMember = "코드";
        //    GUBUN.DisplayMember = "명칭";
        //    sqlQuery = new SF_NEW.Model.Query.scQuery().queryTSCode("910");
        //    wConst.ComboBox_Read_NoBlank(GUBUN, sqlQuery);

        //}

        //private void grid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        //{
        //    DataGridView grd = (DataGridView)sender;

        //    grd.Rows[e.RowIndex].Cells[0].Value = false;

        //    //wConst.init_RowText(grd, e.RowIndex);

        //    for (int kk = 0; kk < grd.Rows.Count; kk++)
        //    {
        //        grd.Rows[kk].Cells[1].Value = (kk + 1).ToString();
        //    }
        //}

        //private void pnl_left_Panel2_SizeChanged(object sender, EventArgs e)
        //{

        //}

        //private void btn_changedate_Click(object sender, EventArgs e)
        //{

        //}

        //private void button2_Click(object sender, EventArgs e)
        //{


        //}

        //private void panel3_Paint(object sender, PaintEventArgs e)
        //{

        //}

        //private void btnRowAdd_Click(object sender, EventArgs e)
        //{
        //    dgv_main.Rows.Add();
        //}

        //private void btnSelcetdelete_Click(object sender, EventArgs e)
        //{
        //    if (dgv_main.CurrentCell.RowIndex > -1)
        //    {
        //        if (dgv_main.Rows[dgv_main.CurrentCell.RowIndex].Cells["MAIN_SEQ"].Value != null &&
        //            !dgv_main.Rows[dgv_main.CurrentCell.RowIndex].Cells["MAIN_SEQ"].Value.ToString().Equals(""))
        //        {
        //            del_inputGrid.Rows.Add();
        //            del_inputGrid.Rows[del_inputGrid.Rows.Count - 1]["PAY_DATE"] = dtp_outdate.Text.ToString();
        //            del_inputGrid.Rows[del_inputGrid.Rows.Count - 1]["PAY_CD"] = txt_num.Text.ToString();
        //            del_inputGrid.Rows[del_inputGrid.Rows.Count - 1]["SEQ"] = dgv_main.Rows[dgv_main.CurrentCell.RowIndex].Cells["MAIN_SEQ"].Value.ToString();
        //        }
        //        dgv_main.Rows.RemoveAt(dgv_main.CurrentCell.RowIndex);
        //    }
        //}

        
    }
}
