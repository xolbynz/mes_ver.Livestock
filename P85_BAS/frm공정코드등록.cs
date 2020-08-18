using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 스마트팩토리.CLS;

namespace 스마트팩토리.P85_BAS
{
    public partial class frm공정코드등록 : Form
    {
        private wnGConstant wConst = new wnGConstant();
        
        public frm공정코드등록()
        {
            InitializeComponent();
        }

        private void frm공정코드등록_Load(object sender, EventArgs e)
        {
            ComInfo.gridHeaderSet(dataFlowGrid);
            btnDelete.Enabled = false;
            init_ComboBox();
            flow_list();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            frm공정분류 frm = new frm공정분류();
            frm.ShowDialog();
        }

        #region common logic

        private void init_ComboBox()
        {
            ComInfo comInfo = new ComInfo();
            string sqlQuery = "";
            //불량유형 시작
            cmb_poor.ValueMember = "코드";
            cmb_poor.DisplayMember = "명칭";
            sqlQuery = comInfo.queryPoor(); //" select TYPE_CD as 코드, TYPE_NM as 명칭 from N_TYPE_CODE where 1=1 and POOR_TYPE_YN = 'Y'";
            wConst.ComboBox_Read_Blank(cmb_poor, sqlQuery); 

            cmb_poor_type.ValueMember = "코드";
            cmb_poor_type.DisplayMember = "명칭";
            wConst.ComboBox_Read_Blank(cmb_poor_type, sqlQuery);
            //불량유형 끝

            //담당자 
            cmb_manager.ValueMember = "코드";
            cmb_manager.DisplayMember = "명칭";
            sqlQuery = comInfo.queryStaff();
            wConst.ComboBox_Read_Blank(cmb_manager, sqlQuery);

            //창고 
            cmb_stor.ValueMember = "코드";
            cmb_stor.DisplayMember = "명칭";
            sqlQuery = comInfo.queryStorage();
            wConst.ComboBox_Read_Blank(cmb_stor, sqlQuery);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            lbl_flow_gbn.Text = "";
            txt_flow_cd.Text = "";
            txt_flow_cd.Enabled = true;
            txt_flow_nm.Text = "";
            cmb_stor.SelectedIndex = 0;
            chk_flow_yn.Checked = false;
            chk_item_gbn.Checked = false;
            chk_flow_chk_yn.Checked = false;
            chk_temp_yn.Checked = false;
            chk_mold_yn.Checked = false;
            cmb_poor_type.SelectedIndex = 0;
            chk_manager_yn.Checked = false;
            cmb_manager.SelectedIndex = 0;
            txt_flow_comment.Text = "";
        }
            //if (tabControl.SelectedIndex == 0) //공정등록
            //{
            //    lbl_flow_gbn.Text = "";
            //    txt_flow_cd.Text = "";
            //    txt_flow_cd.Enabled = true;
            //    txt_flow_nm.Text = "";
            //    cmb_stor.SelectedIndex = 0;
            //    chk_flow_yn.Checked = false;
            //    chk_item_gbn.Checked = false;
            //    chk_flow_chk_yn.Checked = false;
            //    chk_temp_yn.Checked = false;
            //    chk_mold_yn.Checked = false;
            //    cmb_poor_type.SelectedIndex = 0;
            //    chk_manager_yn.Checked = false;
            //    cmb_manager.SelectedIndex = 0;
            //    txt_flow_comment.Text = "";
            //}
            //else if (tabControl.SelectedIndex == 1) //유형등록
            //{ //사용자 등록
                //txt_type_cd.Enabled = true;
                //txt_type_cd.Text = "";
                //txt_type_nm.Text = "";
                //txt_type_cmt.Text = "";
                //lbl_type_gbn.Text = "";
                //chk_type.Checked = false;

            //}
            //else if (tabControl.SelectedIndex == 2) //단위 등록
            //{
                //lbl_unit_gbn.Text = "";
                //txt_unit_cd.Text = "";
                //txt_unit_cd.Enabled = true;
                //txt_unit_nm.Text = "";
                //txt_unit_cmt.Text = "";
            //}
            //else if (tabControl.SelectedIndex == 3) //라인 등록
            //{
                //lbl_line_gbn.Text = "";
                //txt_line_cd.Text = "";
                //txt_line_cd.Enabled = true;
                //txt_line_nm.Text = "";
                //txt_line_cmt.Text = "";
            //}
            //else //불량 등록
            //{ 
                //lbl_poor_gbn.Text = "";
                //txt_poor_cd.Text = "";
                //txt_poor_cd.Enabled = true;
                //txt_poor_nm.Text = "";
                //txt_poor_cmt.Text = "";
                //cmb_poor.SelectedIndex = 0;
        //    }
        //}

        private void btnDelete_Click(object sender, EventArgs e)
        {
            flow_del();

            //if (tabControl.SelectedIndex == 0)
            //{ 
            //    flow_del();
                
            //}
            //else if (tabControl.SelectedIndex == 1)  //유형 삭제
            //{
            //    type_del();
                
            //}
            //else if (tabControl.SelectedIndex == 2) //단위 삭제
            //{
            //    unit_del();
                
            //}
            //else if (tabControl.SelectedIndex == 3) //라인 삭제
            //{
            //    line_del();
                
            //}
            //else 
            //{
            //    poor_del();
            //}
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            flow_logic();

            //if (tabControl.SelectedIndex == 0)
            //{ //공정 등록
            //    flow_logic();
            //}
            //else if (tabControl.SelectedIndex == 1)
            //{ //유형 등록
            //    type_logic();
            //}
            //else if (tabControl.SelectedIndex == 2) //단위 등록
            //{
            //    unit_logic();
            //}
            //else if (tabControl.SelectedIndex == 3) //라인 등록
            //{
            //    line_logic();
            //}
            //else //불량등록
            //{
            //    poor_logic();
            //}

        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            flow_list();

            //if (tabControl.SelectedIndex == 0)
            //{ //공정 등록
            //    flow_list();
            //}
            //else if (tabControl.SelectedIndex == 1)
            //{ //유형 등록
            //    type_list();
            //}
            //else if (tabControl.SelectedIndex == 2) //단위 등록
            //{
            //    unit_list();
            //}
            //else if (tabControl.SelectedIndex == 3) //라인 등록
            //{
            //    line_list();
            //}
            //else //불량등록
            //{
            //    poor_list();
            //}
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion common logic
        
        //#region type logic
        //private void type_logic() {
        //    if (txt_type_cd.Text.ToString().Equals("")) {
        //        MessageBox.Show("유형코드를 입력하시기 바랍니다.");
        //        return;
        //    }
        //    if (txt_type_nm.Text.ToString().Equals("")) {
        //        MessageBox.Show("유형명을 입력하시기 바랍니다.");
        //        return;
        //    }
        //    if (lbl_type_gbn.Text != "1")
        //    {
        //        wnDm wDm = new wnDm();
        //        string chk_type_yn = "";
        //        if (chk_type.Checked == true)
        //        {
        //            chk_type_yn = "Y";
        //        }
        //        else {
        //            chk_type_yn = "N";
        //        }
        //        int rsNum = wDm.insertType(txt_type_cd.Text.ToString(), txt_type_nm.Text.ToString(),chk_type_yn, txt_type_cmt.Text.ToString());

        //        if (rsNum == 0)
        //        {
        //            txt_type_cd.Text = "";
        //            txt_type_nm.Text = "";
        //            txt_type_cmt.Text = "";
        //            chk_type.Checked = false;

        //            type_list();
        //            MessageBox.Show("성공적으로 등록하였습니다.");
        //        }
        //        else if (rsNum == 1)
        //            MessageBox.Show("저장에 실패하였습니다");
        //        else if (rsNum == 2)
        //            MessageBox.Show("SQL COMMAND 에러");
        //        else if (rsNum == 3)
        //            MessageBox.Show("기존 코드가 있으니 \n 다른 코드로 입력 바랍니다.");
        //        else
        //            MessageBox.Show("Exception 에러1");
        //    }
        //    else {
        //        wnDm wDm = new wnDm();
        //        string chk_type_yn = "";
        //        if (chk_type.Checked == true)
        //        {
        //            chk_type_yn = "Y";
        //        }
        //        else
        //        {
        //            chk_type_yn = "N";
        //        }
        //        int rsNum = wDm.updateType(txt_type_cd.Text.ToString(), txt_type_nm.Text.ToString(), chk_type_yn, txt_type_cmt.Text.ToString());
        //        if (rsNum == 0)
        //        {
        //            type_list();
        //            MessageBox.Show("성공적으로 수정하였습니다.");
        //        }
        //        else if (rsNum == 1)
        //            MessageBox.Show("저장에 실패하였습니다");
        //        else
        //            MessageBox.Show("Exception 에러");   
        //    }
        //}

        //private void type_del() {
        //    wnDm wDm = new wnDm();
        //    int rsNum = wDm.deleteType(txt_type_cd.Text.ToString());
        //    if (rsNum == 0) {
        //        lbl_type_gbn.Text = "";
        //        txt_type_cd.Enabled = true;
        //        txt_type_cd.Text = "";
        //        txt_type_nm.Text = "";
        //        txt_type_cmt.Text = "";
        //        chk_type.Checked = false;

        //        btnDelete.Enabled = false;

        //        type_list();
        //        MessageBox.Show("성공적으로 삭제하였습니다.");
        //    }
        //    else if (rsNum == 1) {
        //        MessageBox.Show("삭제에 실패하였습니다.");
        //    }
        //}

        //private void type_list() {
        //    try
        //    {
        //        wnDm wDm = new wnDm();
        //        DataTable dt = null;
        //        dt = wDm.fn_Type_List();

        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            this.dataTypeGrid.RowCount = dt.Rows.Count;
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                dataTypeGrid.Rows[i].Cells[0].Value = dt.Rows[i]["TYPE_CD"].ToString();
        //                dataTypeGrid.Rows[i].Cells[1].Value = dt.Rows[i]["TYPE_NM"].ToString();
        //                dataTypeGrid.Rows[i].Cells[2].Value = dt.Rows[i]["POOR_TYPE_YN"].ToString();
        //                dataTypeGrid.Rows[i].Cells[3].Value = dt.Rows[i]["COMMENT"].ToString();
        //            }
        //        }
        //        else
        //        {
        //            dataTypeGrid.Rows.Clear();
        //        }
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //}

        //private void dataTypeGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    btnDelete.Enabled = true;
        //    lbl_type_gbn.Text = "1";
        //    txt_type_cd.Enabled = false;
        //    txt_type_cd.Text = dataTypeGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
        //    txt_type_nm.Text = dataTypeGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
        //    txt_type_cmt.Text = dataTypeGrid.Rows[e.RowIndex].Cells[3].Value.ToString();
        //    if (dataTypeGrid.Rows[e.RowIndex].Cells[2].Value.ToString().Equals("Y"))
        //        chk_type.Checked = true;
        //    else 
        //        chk_type.Checked = false;

        //}

        //private void txt_type_cd_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    ComInfo.onlyNum(sender, e);
        //}
        //#endregion type logic

        //#region unit logic
        //private void unit_logic()
        //{
        //    if (txt_unit_cd.Text.ToString().Equals(""))
        //    {
        //        MessageBox.Show("단위코드를 입력하시기 바랍니다.");
        //        return;
        //    }
        //    if (txt_unit_nm.Text.ToString().Equals(""))
        //    {
        //        MessageBox.Show("단위명을 입력하시기 바랍니다.");
        //        return;
        //    }
        //    if (lbl_unit_gbn.Text != "1")
        //    {
        //        wnDm wDm = new wnDm();
        //        int rsNum = wDm.insertUnit(txt_unit_cd.Text.ToString(), txt_unit_nm.Text.ToString(), txt_unit_cmt.Text.ToString());

        //        if (rsNum == 0)
        //        {
        //            txt_unit_cd.Text = "";
        //            txt_unit_nm.Text = "";
        //            txt_unit_cmt.Text = "";

        //            unit_list();
        //            MessageBox.Show("성공적으로 등록하였습니다.");
        //        }
        //        else if (rsNum == 1)
        //            MessageBox.Show("저장에 실패하였습니다");
        //        else if (rsNum == 2)
        //            MessageBox.Show("SQL COMMAND 에러");
                 
        //        else if(rsNum == 3)
        //            MessageBox.Show("기존 코드가 있으니 \n 다른 코드로 입력 바랍니다.");
        //        else
        //            MessageBox.Show("Exception 에러1");
        //    }
        //    else
        //    {
        //        wnDm wDm = new wnDm();
        //        int rsNum = wDm.updateUnit(txt_unit_cd.Text.ToString(), txt_unit_nm.Text.ToString(), txt_unit_cmt.Text.ToString());
        //        if (rsNum == 0)
        //        {
        //            unit_list();
        //            MessageBox.Show("성공적으로 수정하였습니다.");
        //        }
        //        else if (rsNum == 1)
        //            MessageBox.Show("저장에 실패하였습니다");
        //        else
        //            MessageBox.Show("Exception 에러2");
        //    }
        //}

        //private void unit_del()
        //{
        //    wnDm wDm = new wnDm();
        //    int rsNum = wDm.deleteUnit(txt_unit_cd.Text.ToString());
        //    if (rsNum == 0)
        //    {
        //        lbl_unit_gbn.Text = "";
        //        txt_unit_cd.Enabled = true;
        //        txt_unit_cd.Text = "";
        //        txt_unit_nm.Text = "";
        //        txt_unit_cmt.Text = "";
        //        btnDelete.Enabled = false;

        //        unit_list();
        //        MessageBox.Show("성공적으로 삭제하였습니다.");
        //    }
        //    else if (rsNum == 1)
        //    {
        //        MessageBox.Show("삭제에 실패하였습니다.");
        //    }
        //}

        //private void unit_list()
        //{
        //    try
        //    {
        //        wnDm wDm = new wnDm();
        //        DataTable dt = null;
        //        dt = wDm.fn_Unit_List();

        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            this.dataUnitGrid.RowCount = dt.Rows.Count;
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                dataUnitGrid.Rows[i].Cells[0].Value = dt.Rows[i]["UNIT_CD"].ToString();
        //                dataUnitGrid.Rows[i].Cells[1].Value = dt.Rows[i]["UNIT_NM"].ToString();
        //                dataUnitGrid.Rows[i].Cells[2].Value = dt.Rows[i]["COMMENT"].ToString();
        //            }
        //        }
        //        else
        //        {
        //            dataUnitGrid.Rows.Clear();
        //        }
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //}

        //private void dataUnitGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    btnDelete.Enabled = true;
        //    lbl_unit_gbn.Text = "1";
        //    txt_unit_cd.Enabled = false;
        //    txt_unit_cd.Text = dataUnitGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
        //    txt_unit_nm.Text = dataUnitGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
        //    txt_unit_cmt.Text = dataUnitGrid.Rows[e.RowIndex].Cells[2].Value.ToString();

        //}

        //private void txt_unit_cd_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    ComInfo.onlyNum(sender, e);
        //}

        //#endregion unit logic

        //#region line logic
        //private void line_logic()
        //{
        //    if (txt_line_cd.Text.ToString().Equals(""))
        //    {
        //        MessageBox.Show("라인코드를 입력하시기 바랍니다.");
        //        return;
        //    }
        //    if (txt_line_nm.Text.ToString().Equals(""))
        //    {
        //        MessageBox.Show("라인명을 입력하시기 바랍니다.");
        //        return;
        //    }
        //    if (lbl_line_gbn.Text != "1")
        //    {
        //        wnDm wDm = new wnDm();
        //        int rsNum = wDm.insertLine(txt_line_cd.Text.ToString(), txt_line_nm.Text.ToString(), txt_line_cmt.Text.ToString());

        //        if (rsNum == 0)
        //        {
        //            txt_line_cd.Text = "";
        //            txt_line_nm.Text = "";
        //            txt_line_cmt.Text = "";

        //            line_list();
        //            MessageBox.Show("성공적으로 등록하였습니다.");
        //        }
        //        else if (rsNum == 1)
        //            MessageBox.Show("저장에 실패하였습니다");
        //        else if (rsNum == 2)
        //            MessageBox.Show("SQL COMMAND 에러");

        //        else if (rsNum == 3)
        //            MessageBox.Show("기존 코드가 있으니 \n 다른 코드로 입력 바랍니다.");
        //        else
        //            MessageBox.Show("Exception 에러1");
        //    }
        //    else
        //    {
        //        wnDm wDm = new wnDm();
        //        int rsNum = wDm.updateLine(txt_line_cd.Text.ToString(), txt_line_nm.Text.ToString(), txt_line_cmt.Text.ToString());
        //        if (rsNum == 0)
        //        {
        //            line_list();
        //            MessageBox.Show("성공적으로 수정하였습니다.");
        //        }
        //        else if (rsNum == 1)
        //            MessageBox.Show("저장에 실패하였습니다");
        //        else
        //            MessageBox.Show("Exception 에러2");
        //    }
        //}

        //private void line_del()
        //{
        //    wnDm wDm = new wnDm();
        //    int rsNum = wDm.deleteLine(txt_line_cd.Text.ToString());
        //    if (rsNum == 0)
        //    {
        //        lbl_line_gbn.Text = "";
        //        txt_line_cd.Enabled = true;
        //        txt_line_cd.Text = "";
        //        txt_line_nm.Text = "";
        //        txt_line_cmt.Text = "";
        //        btnDelete.Enabled = false;

        //        line_list();
        //        MessageBox.Show("성공적으로 삭제하였습니다.");
        //    }
        //    else if (rsNum == 1)
        //    {
        //        MessageBox.Show("삭제에 실패하였습니다.");
        //    }
        //}

        //private void line_list()
        //{
        //    try
        //    {
        //        wnDm wDm = new wnDm();
        //        DataTable dt = null;
        //        dt = wDm.fn_Line_List();

        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            this.dataLineGrid.RowCount = dt.Rows.Count;
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                dataLineGrid.Rows[i].Cells[0].Value = dt.Rows[i]["LINE_CD"].ToString();
        //                dataLineGrid.Rows[i].Cells[1].Value = dt.Rows[i]["LINE_NM"].ToString();
        //                dataLineGrid.Rows[i].Cells[2].Value = dt.Rows[i]["COMMENT"].ToString();
        //            }
        //        }
        //        else
        //        {
        //            dataLineGrid.Rows.Clear();
        //        }
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //}

        //private void dataLineGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    btnDelete.Enabled = true;
        //    lbl_line_gbn.Text = "1";
        //    txt_line_cd.Enabled = false;
        //    txt_line_cd.Text = dataLineGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
        //    txt_line_nm.Text = dataLineGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
        //    txt_line_cmt.Text = dataLineGrid.Rows[e.RowIndex].Cells[2].Value.ToString();
        //}

        //private void txt_line_cd_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    ComInfo.onlyNum(sender, e);
        //}

        //#endregion line logic
        //#region poor logic
        //private void poor_logic()
        //{
        //    if (cmb_poor.SelectedValue == null) cmb_poor.SelectedValue = "";

        //    if (txt_poor_cd.Text.ToString().Equals(""))
        //    {
        //        MessageBox.Show("불량코드를 입력하시기 바랍니다.");
        //        return;
        //    }
        //    if (txt_poor_nm.Text.ToString().Equals(""))
        //    {
        //        MessageBox.Show("불량명을 입력하시기 바랍니다.");
        //        return;
        //    }
        //    if (cmb_poor.SelectedIndex == 0) {
        //        MessageBox.Show("불량유형을 선택하시기 바랍니다.");
        //        return;
        //    }
        //    if (lbl_poor_gbn.Text != "1")
        //    {
        //        wnDm wDm = new wnDm();
        //        int rsNum = wDm.insertPoor(txt_poor_cd.Text.ToString(),
        //            txt_poor_nm.Text.ToString(),
        //            cmb_poor.SelectedValue.ToString(),
        //            txt_poor_cmt.Text.ToString());

        //        if (rsNum == 0)
        //        {
        //            txt_poor_cd.Text = "";
        //            txt_poor_nm.Text = "";
        //            txt_poor_cmt.Text = "";
        //            cmb_poor.SelectedValue = 0;

        //            poor_list();
        //            MessageBox.Show("성공적으로 등록하였습니다.");
        //        }
        //        else if (rsNum == 1)
        //            MessageBox.Show("저장에 실패하였습니다");
        //        else if (rsNum == 2)
        //            MessageBox.Show("SQL COMMAND 에러");

        //        else if (rsNum == 3)
        //            MessageBox.Show("기존 코드가 있으니 \n 다른 코드로 입력 바랍니다.");
        //        else
        //            MessageBox.Show("Exception 에러1");
        //    }
        //    else
        //    {
        //        wnDm wDm = new wnDm();
        //        int rsNum = wDm.updatePoor(txt_poor_cd.Text.ToString(),
        //            txt_poor_nm.Text.ToString(),
        //            cmb_poor.SelectedValue.ToString(),
        //            txt_poor_cmt.Text.ToString());

        //        if (rsNum == 0)
        //        {
        //            poor_list();
        //            MessageBox.Show("성공적으로 수정하였습니다.");
        //        }
        //        else if (rsNum == 1)
        //            MessageBox.Show("저장에 실패하였습니다");
        //        else
        //            MessageBox.Show("Exception 에러2");
        //    }
        //}

        //private void poor_list()
        //{
        //    try
        //    {
        //        wnDm wDm = new wnDm();
        //        DataTable dt = null;
        //        dt = wDm.fn_Poor_List();

        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            this.dataPoorGrid.RowCount = dt.Rows.Count;
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                dataPoorGrid.Rows[i].Cells[0].Value = dt.Rows[i]["POOR_CD"].ToString();
        //                dataPoorGrid.Rows[i].Cells[1].Value = dt.Rows[i]["POOR_NM"].ToString();
        //                dataPoorGrid.Rows[i].Cells[2].Value = dt.Rows[i]["TYPE_CD"].ToString();
        //                dataPoorGrid.Rows[i].Cells[3].Value = dt.Rows[i]["TYPE_NM"].ToString();
        //                dataPoorGrid.Rows[i].Cells[4].Value = dt.Rows[i]["COMMENT"].ToString();
        //            }
        //        }
        //        else
        //        {
        //            dataPoorGrid.Rows.Clear();
        //        }
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //}

        //private void dataPoorGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    btnDelete.Enabled = true;
        //    lbl_poor_gbn.Text = "1";
        //    txt_poor_cd.Enabled = false;
        //    txt_poor_cd.Text = dataPoorGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
        //    txt_poor_nm.Text = dataPoorGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
        //    cmb_poor.SelectedValue = int.Parse(dataPoorGrid.Rows[e.RowIndex].Cells[2].Value.ToString());
        //    txt_poor_cmt.Text = dataPoorGrid.Rows[e.RowIndex].Cells[4].Value.ToString();
        //}

        //private void poor_del()
        //{
        //    wnDm wDm = new wnDm();
        //    int rsNum = wDm.deletePoor(txt_poor_cd.Text.ToString());
        //    if (rsNum == 0)
        //    {
        //        lbl_poor_gbn.Text = "";
        //        txt_poor_cd.Enabled = true;
        //        txt_poor_cd.Text = "";
        //        txt_poor_nm.Text = "";
        //        txt_poor_cmt.Text = "";
        //        cmb_poor.SelectedValue = "";
        //        btnDelete.Enabled = false;

        //        poor_list();
        //        MessageBox.Show("성공적으로 삭제하였습니다.");
        //    }
        //    else if (rsNum == 1)
        //    {
        //        MessageBox.Show("삭제에 실패하였습니다.");
        //    }
        //}
        //#endregion poor logic 

        #region flow logic

        private void flow_logic() {

            if (cmb_stor.SelectedValue == null) cmb_stor.SelectedValue = "";
            if (cmb_poor_type.SelectedValue == null) cmb_poor_type.SelectedValue = "";
            if (cmb_manager.SelectedValue == null) cmb_manager.SelectedValue = "";
            
            ComInfo comInfo = new ComInfo();
            if (txt_flow_cd.Text.ToString().Equals(""))
            {
                MessageBox.Show("공정코드를 입력하시기 바랍니다.");
                return;
            }
            if (txt_flow_nm.Text.ToString().Equals(""))
            {
                MessageBox.Show("공정명을 입력하시기 바랍니다.");
                return;
            }
            if (cmb_stor.SelectedIndex == 0)
            {
                MessageBox.Show("창고명을 선택하시기 바랍니다.");
                return;
            }
            if (cmb_poor_type.SelectedIndex == 0)
            {
                MessageBox.Show("불량유형을 선택하시기 바랍니다.");
                return;
            }
            if (chk_manager_yn.Checked == true) 
            {
                if (cmb_manager.SelectedIndex == 0)
                {
                    MessageBox.Show("담당자를 선택하시기 바랍니다.");
                    return;
                }
            }
            string flow_yn = comInfo.resultYn(chk_flow_yn); //공정등록 유무
            string item_iden_yn = comInfo.resultYn(chk_item_gbn); //제품식별표 유무
            string flow_chk_yn = comInfo.resultYn(chk_flow_chk_yn); //공정검사 유무
            string temp_time_yn = comInfo.resultYn(chk_temp_yn); //온도시간 유무
            string mold_yn = comInfo.resultYn(chk_mold_yn); //공정등록 유무
            string manager_yn = comInfo.resultYn(chk_manager_yn);

            if (lbl_flow_gbn.Text != "1")
            {
                wnDm wDm = new wnDm();
                int rsNum = wDm.insertFlow(
                     txt_flow_cd.Text.ToString()
                    , txt_flow_nm.Text.ToString()
                    , cmb_stor.SelectedValue.ToString()
                    , flow_yn
                    , item_iden_yn
                    , flow_chk_yn
                    , temp_time_yn
                    , mold_yn
                    , cmb_poor_type.SelectedValue.ToString()
                    , manager_yn
                    , cmb_manager.SelectedValue.ToString()
                    , txt_flow_comment.Text.ToString());

                if (rsNum == 0)
                {
                    txt_flow_cd.Text = "";
                    txt_flow_nm.Text = "";
                    cmb_stor.SelectedIndex = 0;
                    chk_flow_yn.Checked = false;
                    chk_item_gbn.Checked = false;
                    chk_flow_chk_yn.Checked = false;
                    chk_temp_yn.Checked = false;
                    chk_mold_yn.Checked = false;
                    cmb_poor_type.SelectedIndex = 0;
                    chk_manager_yn.Checked = false;
                    cmb_manager.SelectedIndex = 0;
                    txt_flow_comment.Text = "";

                    flow_list();
                    MessageBox.Show("성공적으로 등록하였습니다.");
                }
                else if (rsNum == 1)
                    MessageBox.Show("저장에 실패하였습니다");
                else if (rsNum == 2)
                    MessageBox.Show("SQL COMMAND 에러");

                else if (rsNum == 3)
                    MessageBox.Show("기존 코드가 있으니 \n 다른 코드로 입력 바랍니다.");
                else
                    MessageBox.Show("Exception 에러");
            }
            else
            {
                wnDm wDm = new wnDm();
                int rsNum = wDm.updateFlow(
                     txt_flow_cd.Text.ToString()
                    , txt_flow_nm.Text.ToString()
                    , cmb_stor.SelectedValue.ToString()
                    , flow_yn
                    , item_iden_yn
                    , flow_chk_yn
                    , temp_time_yn
                    , mold_yn
                    , cmb_poor_type.SelectedValue.ToString()
                    , manager_yn
                    , cmb_manager.SelectedValue.ToString()
                    , txt_flow_comment.Text.ToString());

                if (rsNum == 0)
                {
                    flow_list();
                    MessageBox.Show("성공적으로 수정하였습니다.");
                }
                else if (rsNum == 1)
                    MessageBox.Show("저장에 실패하였습니다");
                else
                    MessageBox.Show("Exception 에러2");
            }
        }
        private void chk_manager_yn_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_manager_yn.Checked == true)
            {
                cmb_manager.Visible = true;
            }
            else
            {
                cmb_manager.Visible = false;
            }
        }

        private void flow_list() 
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_Flow_List("");

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.dataFlowGrid.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dataFlowGrid.Rows[i].Cells[0].Value = dt.Rows[i]["FLOW_CD"].ToString();
                        dataFlowGrid.Rows[i].Cells[1].Value = dt.Rows[i]["FLOW_NM"].ToString();
                        dataFlowGrid.Rows[i].Cells[2].Value = dt.Rows[i]["STORAGE_NM"].ToString();
                    }
                }
                //else
                //{
                //    dataPoorGrid.Rows.Clear();
                //}
            }
            catch (Exception e)
            {

            }
        }

        private void dataFlowGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnDelete.Enabled = true;
            lbl_flow_gbn.Text = "1";
            txt_flow_cd.Enabled = false;

            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                string condition = "where flow_cd = '" + dataFlowGrid.Rows[e.RowIndex].Cells[0].Value.ToString() + "'";
                dt = wDm.fn_Flow_List(condition);

                if (dt != null && dt.Rows.Count > 0)
                {

                    txt_flow_cd.Text = dt.Rows[0]["FLOW_CD"].ToString();
                    txt_flow_nm.Text = dt.Rows[0]["FLOW_NM"].ToString();
                    cmb_stor.SelectedValue = dt.Rows[0]["STORAGE_CD"].ToString();
                    cmb_poor_type.SelectedValue = dt.Rows[0]["POOR_TYPE_CD"].ToString();
                    txt_flow_comment.Text = dt.Rows[0]["COMMENT"].ToString();

                    //공정등록 
                    if (dt.Rows[0]["FLOW_INSERT_YN"].ToString().Equals("Y"))
                        chk_flow_yn.Checked = true;
                    else 
                        chk_flow_chk_yn.Checked = false;
                    
                    //제품식별
                    if (dt.Rows[0]["ITEM_IDEN_YN"].ToString().Equals("Y"))
                        chk_item_gbn.Checked = true;
                    else 
                        chk_item_gbn.Checked = false;
                    
                    //공정체크 
                    if (dt.Rows[0]["FLOW_CHK_YN"].ToString().Equals("Y"))
                        chk_flow_chk_yn.Checked = true;
                    else chk_flow_chk_yn.Checked = false;
                    
                    //온도시간
                    if (dt.Rows[0]["TEMP_TIME_YN"].ToString().Equals("Y"))
                        chk_temp_yn.Checked = true;
                    else 
                        chk_temp_yn.Checked = false;
                    
                    //금형타수여부
                    if (dt.Rows[0]["MOLD_YN"].ToString().Equals("Y"))
                        chk_mold_yn.Checked = true;
                    else 
                        chk_mold_yn.Checked = false;
                    
                    //담당자지정
                    if (dt.Rows[0]["STAFF_YN"].ToString().Equals("Y"))
                    {
                        chk_manager_yn.Checked = true;
                        cmb_manager.SelectedValue = int.Parse(dt.Rows[0]["STAFF_CD"].ToString());
                    }
                    else 
                        chk_manager_yn.Checked = false;
                }
                else
                {
                    //MessageBox.Show("존재하지 않는 자료입니다.");
                }
            }
            catch (Exception ex)
            {

            }
        }


        private void flow_del()
        {
            wnDm wDm = new wnDm();
            int rsNum = wDm.deleteFlow(txt_flow_cd.Text.ToString());
            if (rsNum == 0)
            {
                lbl_flow_gbn.Text = "";
                txt_flow_cd.Text = "";
                txt_flow_nm.Text = "";
                cmb_stor.SelectedIndex = 0;
                chk_flow_yn.Checked = false;
                chk_item_gbn.Checked = false;
                chk_flow_chk_yn.Checked = false;
                chk_temp_yn.Checked = false;
                chk_mold_yn.Checked = false;
                cmb_poor_type.SelectedIndex = 0;
                chk_manager_yn.Checked = false;
                cmb_manager.SelectedIndex = 0;
                txt_flow_comment.Text = "";

                flow_list();
                MessageBox.Show("성공적으로 삭제하였습니다.");
            }
            else if (rsNum == 1)
            {
                MessageBox.Show("삭제에 실패하였습니다.");
            }
        }

        #endregion flow logic

        
        
    }
}
