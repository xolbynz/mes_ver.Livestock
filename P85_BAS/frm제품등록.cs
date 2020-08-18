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
using 스마트팩토리.Controls;

namespace 스마트팩토리.P85_BAS
{
    public partial class frm제품등록 : Form
    {
        Popup.frmPrint readyPrt = new Popup.frmPrint();
        //private wnGConstant wConst = new wnGConstant();

        DataTable adoPrt = null;
        wnAdo wAdo = new wnAdo();
        public Popup.frmPrint frmPrt;

        public string strCondition = "";

        private wnGConstant wConst = new wnGConstant();
        private ComInfo comInfo = new ComInfo();
        private DataGridView del_compGrid = new DataGridView();
        private DataGridView del_flowGrid = new DataGridView();
        private DataGridView del_halfGrid = new DataGridView();

        public frm제품등록()
        {
            InitializeComponent();

            this.itemRawGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellEndEdit);
            this.itemRawGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grid_KeyDown);
            this.itemRawGrid.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grid_ColumnHeaderMouseClick);
            this.itemRawGrid.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.grid_RowsAdded);
            this.itemRawGrid.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.grid_RowsRemoved);
            this.itemRawGrid.CellValueChanged += new DataGridViewCellEventHandler(grid_CellValueChanged);
            this.itemRawGrid.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.grid_ColumnWidthChanged);

            this.itemFlowGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellEndEdit2);
            this.itemFlowGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grid_KeyDown);
            this.itemFlowGrid.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grid_ColumnHeaderMouseClick);
            this.itemFlowGrid.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.grid_RowsAdded);
            this.itemFlowGrid.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.grid_RowsRemoved);
            this.itemFlowGrid.CellValueChanged += new DataGridViewCellEventHandler(grid_CellValueChanged);
            this.itemFlowGrid.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.grid_ColumnWidthChanged);

            this.itemHalfGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellEndEdit3);
            this.itemHalfGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grid_KeyDown);
            this.itemHalfGrid.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grid_ColumnHeaderMouseClick);
            this.itemHalfGrid.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.grid_RowsAdded);
            this.itemHalfGrid.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.grid_RowsRemoved);
            this.itemHalfGrid.CellValueChanged += new DataGridViewCellEventHandler(grid_CellValueChanged);
            this.itemHalfGrid.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.grid_ColumnWidthChanged);
        }

        private void frm제품등록_Load(object sender, EventArgs e)
        {
            ComInfo.gridHeaderSet(dataItemGrid);
            ComInfo.gridHeaderSet(itemRawGrid);

            init_ComboBox();
            item_list();

            itemCompGridAdd();

            //this.itemRawGrid = new ItemGrid();

            itemFlowGridAdd();
            itemHalfGridAdd();

            del_compGrid.AllowUserToAddRows = false;
            del_flowGrid.AllowUserToAddRows = false;
            del_halfGrid.AllowUserToAddRows = false;

            del_compGrid.Columns.Add("ITEM_CD", "ITEM_CD");
            del_compGrid.Columns.Add("SEQ", "SEQ");
            del_flowGrid.Columns.Add("F_ITEM_CD", "F_ITEM_CD");
            del_flowGrid.Columns.Add("F_SEQ", "F_SEQ");

            del_halfGrid.Columns.Add("MAIN_ITEM_CD", "MAIN_ITEM_CD");
            del_halfGrid.Columns.Add("H_SEQ", "H_SEQ");

        }
        
        #region top menu 
        private void btnNew_Click(object sender, EventArgs e)
        {
            resetSetting();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            item_logic();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            item_del();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion top menu

        #region item logic
        private void init_ComboBox()
        {
            ComInfo comInfo = new ComInfo();
            string sqlQuery = "";

            cmb_type.ValueMember = "코드";
            cmb_type.DisplayMember = "명칭";
            sqlQuery = comInfo.queryType();
            wConst.ComboBox_Read_Blank(cmb_type, sqlQuery);

            cmb_line.ValueMember = "코드";
            cmb_line.DisplayMember = "명칭";
            sqlQuery = comInfo.queryLine();
            wConst.ComboBox_Read_Blank(cmb_line, sqlQuery);

            cmb_unit.ValueMember = "코드";
            cmb_unit.DisplayMember = "명칭";
            sqlQuery = comInfo.queryUnit();
            wConst.ComboBox_Read_Blank(cmb_unit, sqlQuery);

            cmb_cust.ValueMember = "코드";
            cmb_cust.DisplayMember = "명칭";
            sqlQuery = comInfo.queryCustUsed("1");
            wConst.ComboBox_Read_Blank(cmb_cust, sqlQuery);

            cmb_used.ValueMember = "코드";
            cmb_used.DisplayMember = "명칭";
            sqlQuery = comInfo.queryUsedYn();
            wConst.ComboBox_Read_NoBlank(cmb_used, sqlQuery);

            cmb_item_gbn.ValueMember = "코드";
            cmb_item_gbn.DisplayMember = "명칭";
            sqlQuery = comInfo.queryCode("400");
            wConst.ComboBox_Read_NoBlank(cmb_item_gbn, sqlQuery);

            cmb_srch_gbn.ValueMember = "코드";
            cmb_srch_gbn.DisplayMember = "명칭";
            sqlQuery = comInfo.queryItemGbnAll();
            wConst.ComboBox_Read_NoBlank(cmb_srch_gbn, sqlQuery);

            cmb_used_srch.ValueMember = "코드";
            cmb_used_srch.DisplayMember = "명칭";
            sqlQuery = comInfo.queryCustUsedYnAll(); //사용여부검색
            wConst.ComboBox_Read_NoBlank(cmb_used_srch, sqlQuery);

            
            cmb_vat_cd.ValueMember = "코드";
            cmb_vat_cd.DisplayMember = "명칭";
            sqlQuery = comInfo.queryVat(); // 과세구분
            wConst.ComboBox_Read_NoBlank(cmb_vat_cd, sqlQuery);

            cmb_chugjong.ValueMember = "코드";
            cmb_chugjong.DisplayMember = "명칭";
            sqlQuery = comInfo.queryChugjongAll(); // 과세구분
            wConst.ComboBox_Read_NoBlank(cmb_chugjong, sqlQuery);

            cmb_class.ValueMember = "코드";
            cmb_class.DisplayMember = "명칭";
            sqlQuery = comInfo.queryClassAll(); // 과세구분
            wConst.ComboBox_Read_NoBlank(cmb_class, sqlQuery);

            cmb_country.ValueMember = "코드";
            cmb_country.DisplayMember = "명칭";
            sqlQuery = comInfo.queryCountryAll(); // 과세구분
            wConst.ComboBox_Read_NoBlank(cmb_country, sqlQuery);


            
        }

        private void resetSetting()
        {
            lbl_item_gbn.Text = "";
            btnDelete.Enabled = false;
            txt_item_cd.Text = "";
            txt_item_cd.Enabled = true;
            txt_item_nm.Text = "";
            txt_spec.Text = "";
            cmb_item_gbn.SelectedIndex = 0;
            cmb_type.SelectedIndex = 0;
            cmb_line.SelectedIndex = 0;
            cmb_unit.SelectedIndex = 0;
            txt_prop_stock.Text = "0";
            txt_item_weight.Text = "0";
            txt_input_price.Text = "0";
            txt_output_price.Text = "0";
            txt_char_amt.Text = "0";
            txt_pack_amt.Text = "0";
            txt_comment.Text = "";
            txt_label_nm.Text = "";
            txt_hamyang.Text = "";
            cmb_cust.SelectedIndex = 0;
            chk_print_yn.Checked = false;
            cmb_used.SelectedIndex = 0;

            txt_box_bar_cd.Text = "";
            txt_box_amt.Text = "";
            txt_unit_bar_cd.Text = "";
            cmb_vat_cd.SelectedIndex = 0;

            itemRawGrid.Rows.Clear();
            itemFlowGrid.Rows.Clear();
            itemHalfGrid.Rows.Clear();

            itemCompGridAdd();
            itemFlowGridAdd();
            itemHalfGridAdd();

            del_compGrid.Rows.Clear();
            del_flowGrid.Rows.Clear();
            del_halfGrid.Rows.Clear();

        }

        private void item_logic() 
        {
            try
            {
                if (cmb_type.SelectedValue == null) cmb_type.SelectedValue = "";
                if (cmb_line.SelectedValue == null) cmb_line.SelectedValue = "";
                if (cmb_unit.SelectedValue == null) cmb_unit.SelectedValue = "";
                if (cmb_line.SelectedValue == null) cmb_line.SelectedValue = "";
                if (cmb_cust.SelectedValue == null) cmb_cust.SelectedValue = "";
                if (cmb_used.SelectedValue == null) cmb_used.SelectedValue = "";
                if (cmb_item_gbn.SelectedValue == null) cmb_item_gbn.SelectedValue = "";
                if (cmb_vat_cd.SelectedValue == null) cmb_vat_cd.SelectedValue = "";

                if (txt_item_cd.Text.ToString().Equals("")) 
                {
                    MessageBox.Show("제품코드를 입력하시기 바랍니다.");
                    return;
                }
                if (txt_item_nm.Text.ToString().Equals("")) 
                {
                    MessageBox.Show("제품명을 입력하시기 바랍니다.");
                    return;
                }
                if (cmb_unit.SelectedIndex == 0 || cmb_unit == null)
                {
                    MessageBox.Show("단위코드를 선택하시기 바랍니다.");
                    return;
                }
                if (cmb_cust.SelectedIndex == 0 || cmb_cust == null) 
                {
                    MessageBox.Show("거래처명을 선택하시기 바랍니다.");
                    return;
                }
                
                if (cmb_vat_cd == null)
                {
                    MessageBox.Show("과세구분을 선택하시기 바랍니다");
                    return;
                }

                if (itemRawGrid.Rows.Count > 0) 
                {
                    int cnt = 0;
                    int grid_cnt = itemRawGrid.Rows.Count;
                    for (int i = 0; i < grid_cnt; i++)
                    {
                        string txt_raw_mat_cd = (string)itemRawGrid.Rows[i-cnt].Cells["RAW_MAT_CD"].Value;

                        if (txt_raw_mat_cd == "" || txt_raw_mat_cd == null)  //마지막 행에 원부재료코드가 없을 경우 제거
                        {
                            itemRawGrid.Rows.RemoveAt(i-cnt);
                            cnt++;
                        }
                    }
                }

                if (itemFlowGrid.Rows.Count > 0)
                {
                    int cnt = 0;
                    int grid_cnt = itemFlowGrid.Rows.Count;
                    for (int i = 0; i < grid_cnt; i++)
                    {
                        string txt_flow_cd = (string)itemFlowGrid.Rows[i-cnt].Cells["FLOW_CD"].Value;

                        if (txt_flow_cd == "" || txt_flow_cd == null)  //마지막 행에 원부재료코드가 없을 경우 제거
                        {
                            itemFlowGrid.Rows.RemoveAt(i-cnt);
                            cnt++;
                        }
                    }
                }

                if (itemHalfGrid.Rows.Count > 0)
                {
                    int cnt = 0;
                    int grid_cnt = itemHalfGrid.Rows.Count;
                    for (int i = 0; i < grid_cnt; i++)
                    {
                        string txt_half_item_cd = (string)itemHalfGrid.Rows[i - cnt].Cells["HALF_ITEM_CD"].Value;

                        if (txt_half_item_cd == "" || txt_half_item_cd == null)  //마지막 행에 원부재료코드가 없을 경우 제거
                        {
                            itemHalfGrid.Rows.RemoveAt(i - cnt);
                            cnt++;
                        }
                    }

                }
                string print_yn = comInfo.resultYn(chk_print_yn);
                if (lbl_item_gbn.Text != "1")
                {

                    wnDm wDm = new wnDm();
                    
                    int rsNum = wDm.insertItem(
                                    txt_item_cd.Text.ToString()
                                    , txt_item_nm.Text.ToString()
                                    , cmb_item_gbn.SelectedValue.ToString()
                                    , txt_spec.Text.ToString()
                                    , cmb_type.SelectedValue.ToString()
                                    , cmb_line.SelectedValue.ToString()
                                    , cmb_unit.SelectedValue.ToString()
                                    , ""
                                    , double.Parse(txt_prop_stock.Text.ToString())
                                    , double.Parse(txt_item_weight.Text.ToString())
                                    , double.Parse(txt_input_price.Text.ToString())
                                    , double.Parse(txt_output_price.Text.ToString())
                                    , double.Parse(txt_char_amt.Text.ToString())
                                    , double.Parse(txt_pack_amt.Text.ToString())
                                    , cmb_cust.SelectedValue.ToString()
                                    , print_yn
                                    , cmb_used.SelectedValue.ToString()
                                    , input_date.Text.ToString()
                                    , txt_box_bar_cd.Text.ToString()
                                    , txt_box_amt.Text.ToString()
                                    , txt_unit_bar_cd.Text.ToString()
                                    , txt_unit_amt.Text.ToString()
                                    , txt_comment.Text.ToString()
                                    , cmb_vat_cd.SelectedValue.ToString()
                                    , cmb_chugjong.SelectedValue.ToString()
                                    , cmb_class.SelectedValue.ToString()
                                    , cmb_country.SelectedValue.ToString()
                                    , txt_label_nm.Text.ToString()
                                    , txt_hamyang.Text.ToString()
                                    , itemRawGrid
                                    , itemFlowGrid
                                    , itemHalfGrid);

                    if (rsNum == 0)
                    {
                        resetSetting();
                        item_list();
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
                    int rsNum = wDm.updateItem(
                                    txt_item_cd.Text.ToString()
                                    , txt_item_nm.Text.ToString()
                                    , cmb_item_gbn.SelectedValue.ToString()
                                    , txt_spec.Text.ToString()
                                    , cmb_type.SelectedValue.ToString()
                                    , cmb_line.SelectedValue.ToString()
                                    , cmb_unit.SelectedValue.ToString()
                                    , ""
                                    , double.Parse(txt_prop_stock.Text.ToString())
                                    , double.Parse(txt_item_weight.Text.ToString())
                                    , double.Parse(txt_input_price.Text.ToString())
                                    , double.Parse(txt_output_price.Text.ToString())
                                    , double.Parse(txt_char_amt.Text.ToString())
                                    , double.Parse(txt_pack_amt.Text.ToString())
                                    , cmb_cust.SelectedValue.ToString()
                                    , print_yn
                                    , cmb_used.SelectedValue.ToString()
                                    , input_date.Text.ToString()
                                    , txt_box_bar_cd.Text.ToString()
                                    , txt_box_amt.Text.ToString()
                                    , txt_unit_bar_cd.Text.ToString()
                                    , txt_unit_amt.Text.ToString()
                                    , txt_comment.Text.ToString()
                                    , cmb_vat_cd.SelectedValue.ToString()
                                    , cmb_chugjong.SelectedValue.ToString()
                                    , cmb_class.SelectedValue.ToString()
                                    , cmb_country.SelectedValue.ToString()
                                    , txt_label_nm.Text.ToString()
                                    , txt_hamyang.Text.ToString()
                                    , itemRawGrid
                                    , itemFlowGrid
                                    , itemHalfGrid
                                    , del_compGrid
                                    , del_flowGrid
                                    , del_halfGrid);

                    if (rsNum == 0)
                    {
                        del_compGrid.Rows.Clear(); //기존 삭제 데이터할 제품구성 리스트 초기화
                        del_flowGrid.Rows.Clear();
                        del_halfGrid.Rows.Clear();
                        item_list();
                        gridDetail("where A.item_cd = '" + txt_item_cd.Text.ToString() + "'");
                        MessageBox.Show("성공적으로 수정하였습니다.");
                    }
                    else if (rsNum == 1)
                        MessageBox.Show("저장에 실패하였습니다");
                    else
                        MessageBox.Show("Exception 에러");
                }

            }
            catch (Exception e) {
                MessageBox.Show("시스템 에러: " + e.Message.ToString());
            }
        }

        private void item_list() 
        {
            try
            {

                wnDm wDm = new wnDm();
                DataTable dt = null;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("where 1=1 ");

                if (cmb_srch_gbn.SelectedIndex > 0)
                {
                    sb.AppendLine("and ITEM_GUBUN = '" + cmb_srch_gbn.SelectedValue.ToString() + "' ");
                }

                if (!txt_srch.Text.ToString().Equals("")) 
                {
                    sb.AppendLine(" and ( ITEM_NM like '%" + txt_srch.Text.ToString() + "%' OR LABEL_NM like '%" + txt_srch.Text.ToString() + "%' ) ");
                }

                if (cmb_used_srch.SelectedIndex == 1)
                {
                    sb.AppendLine(" and USED_CD = 1 ");
                }
                else if (cmb_used_srch.SelectedIndex == 2)
                {
                    sb.AppendLine(" and USED_CD = 2 ");
                }
                else if (cmb_used_srch.SelectedIndex == 3)
                {
                    sb.AppendLine(" and USED_CD = 3 ");
                }


                dt = wDm.fn_Item_List(sb.ToString());

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.dataItemGrid.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        if (dt.Rows[i]["USED_CD"].ToString().Equals("2"))
                        {
                            dataItemGrid.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                        }
                        else if (dt.Rows[i]["USED_CD"].ToString().Equals("3"))
                        {
                            dataItemGrid.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                        else if (dt.Rows[i]["USED_CD"].ToString().Equals("1"))
                        {
                            dataItemGrid.Rows[i].DefaultCellStyle.BackColor = Color.Empty;
                        }
                        dataItemGrid.Rows[i].Cells["I_ITEM_CD"].Value = dt.Rows[i]["ITEM_CD"].ToString();
                        dataItemGrid.Rows[i].Cells["I_ITEM_NM"].Value = dt.Rows[i]["ITEM_NM"].ToString();
                        dataItemGrid.Rows[i].Cells["I_VAT_CD"].Value = dt.Rows[i]["VAT_CD"].ToString();
                        dataItemGrid.Rows[i].Cells["I_VAT_NM"].Value = dt.Rows[i]["VAT_NM"].ToString();
                        dataItemGrid.Rows[i].Cells["I_CHUGJONG_NM"].Value = dt.Rows[i]["CHUGJONG_NM"].ToString();
                        dataItemGrid.Rows[i].Cells["I_CLASS_NM"].Value = dt.Rows[i]["CLASS_NM"].ToString();
                        dataItemGrid.Rows[i].Cells["I_COUNTRY_NM"].Value = dt.Rows[i]["COUNTRY_NM"].ToString();
                        dataItemGrid.Rows[i].Cells["I_TYPE_NM"].Value = dt.Rows[i]["TYPE_NM"].ToString();
                        dataItemGrid.Rows[i].Cells["I_LABEL_NM"].Value = dt.Rows[i]["LABEL_NM"].ToString();
                        
                    }
                }
                else
                {
                    dataItemGrid.Rows.Clear();
                }
            }
            catch (Exception e) 
            {
                MessageBox.Show("시스템 에러: " + e.Message.ToString());
            }
        }

        private void dataItemGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            btnDelete.Enabled = true;
            lbl_item_gbn.Text = "1";
            txt_item_cd.Enabled = false;

            del_compGrid.Rows.Clear(); //더블클릭 시 기존 삭제 데이터할 제품구성 리스트 초기화
            del_flowGrid.Rows.Clear();
           
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;

                string condition = "where A.item_cd = '" + dataItemGrid.Rows[e.RowIndex].Cells[0].Value.ToString() + "'";
                Console.WriteLine(dataItemGrid.Rows[e.RowIndex].Cells[0].Value.ToString());
                try
                {
                    dt = wDm.fn_Item_List(condition);
                }catch(Exception e3){
                    Console.WriteLine(e3);
                }

                if (dt != null && dt.Rows.Count > 0)
                {

                    txt_item_cd.Text = dt.Rows[0]["ITEM_CD"].ToString();
                    txt_item_nm.Text = dt.Rows[0]["ITEM_NM"].ToString();
                    cmb_item_gbn.SelectedValue = dt.Rows[0]["ITEM_GUBUN"].ToString();
                    txt_spec.Text = dt.Rows[0]["SPEC"].ToString();
                    cmb_type.SelectedValue = dt.Rows[0]["TYPE_CD"].ToString();
                    cmb_line.SelectedValue = dt.Rows[0]["LINE_CD"].ToString();
                    cmb_unit.SelectedValue = dt.Rows[0]["UNIT_CD"].ToString();
                    txt_prop_stock.Text = dt.Rows[0]["PROP_STOCK"].ToString();
                    txt_item_weight.Text = decimal.Parse(dt.Rows[0]["ITEM_WEIGHT"].ToString()).ToString("#,0.######");
                    txt_input_price.Text = decimal.Parse(dt.Rows[0]["INPUT_PRICE"].ToString()).ToString("#,0.######");
                    txt_output_price.Text = decimal.Parse(dt.Rows[0]["OUTPUT_PRICE"].ToString()).ToString("#,0.######");
                    txt_char_amt.Text = dt.Rows[0]["CHARGE_AMT"].ToString();
                    txt_pack_amt.Text = dt.Rows[0]["PACK_AMT"].ToString();

                    txt_box_bar_cd.Text = dt.Rows[0]["BOX_BAR_CD"].ToString();
                    txt_box_amt.Text = dt.Rows[0]["BOX_AMT"].ToString();
                    txt_unit_bar_cd.Text = dt.Rows[0]["UNIT_BAR_CD"].ToString();
                    txt_unit_amt.Text = dt.Rows[0]["UNIT_AMT"].ToString();

                    txt_label_nm.Text = dt.Rows[0]["LABEL_NM"].ToString();
                    txt_hamyang.Text = dt.Rows[0]["HAMYANG"].ToString();



                    cmb_cust.SelectedValue = dt.Rows[0]["CUST_CD"].ToString();
                    if (dt.Rows[0]["PRINT_YN"].ToString().Equals("Y"))
                    {
                        chk_print_yn.Checked = true;
                    }
                    else
                    {
                        chk_print_yn.Checked = false;
                    }
                    cmb_used.SelectedValue = int.Parse(dt.Rows[0]["USED_CD"].ToString());
                    input_date.Text = dt.Rows[0]["INPUT_DATE"].ToString();
                    txt_comment.Text = dt.Rows[0]["COMMENT"].ToString();
                    if (dt.Rows[0]["VAT_CD"].ToString() == null)
                    {
                        cmb_vat_cd.SelectedIndex = 1;
                    }
                    if (dt.Rows[0]["VAT_CD"].ToString() != null)
                        cmb_vat_cd.SelectedValue = dt.Rows[0]["VAT_CD"].ToString();
                    else
                        cmb_vat_cd.SelectedValue = 0;

                    if(dt.Rows[0]["CHUGJONG_CD"].ToString() != null)
                        cmb_chugjong.SelectedValue = dt.Rows[0]["CHUGJONG_CD"].ToString();
                    else
                        cmb_chugjong.SelectedIndex = 0;

                    if (dt.Rows[0]["CLASS_CD"].ToString() != null)
                        cmb_class.SelectedValue = dt.Rows[0]["CLASS_CD"].ToString();
                    else
                        cmb_class.SelectedIndex = 0;

                    if (dt.Rows[0]["COUNTRY_CD"].ToString() != null)
                        cmb_country.SelectedValue = dt.Rows[0]["COUNTRY_CD"].ToString();
                    else
                        cmb_country.SelectedIndex = 0;



                    //txt_link_input.Text = (dt.Rows[0]["LINK_CD"]!=null?dt.Rows[0]["LINK_CD"].ToString():"");

                }

               gridDetail(condition);

            }
            catch (Exception ex)
            {
                MessageBox.Show("시스템 에러: " + ex.Message.ToString());
            }
        }

        private void gridDetail(string condition) 
        {
            wnDm wDm = new wnDm();
            DataTable dt = null;

            dt = wDm.fn_Item_Comp_List(condition);

            this.itemRawGrid.RowCount = dt.Rows.Count;
            //itemRawGrid.Rows.Clear();
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //itemRawGrid.Rows.Add();
                    itemRawGrid.Rows[i].Cells["ITEM_CD"].Value = dt.Rows[i]["ITEM_CD"].ToString();
                    itemRawGrid.Rows[i].Cells["SEQ"].Value = dt.Rows[i]["SEQ"].ToString();
                    itemRawGrid.Rows[i].Cells["RAW_MAT_CD"].Value = dt.Rows[i]["RAW_MAT_CD"].ToString();
                    itemRawGrid.Rows[i].Cells["RAW_MAT_NM"].Value = dt.Rows[i]["RAW_MAT_NM"].ToString();
                    itemRawGrid.Rows[i].Cells["SPEC"].Value = dt.Rows[i]["SPEC"].ToString();
                    itemRawGrid.Rows[i].Cells["OLD_RAW_MAT_NM"].Value = dt.Rows[i]["RAW_MAT_NM"].ToString();
                    itemRawGrid.Rows[i].Cells["LABEL_NM"].Value = dt.Rows[i]["LABEL_NM"].ToString();
                    itemRawGrid.Rows[i].Cells["CHUGJONG_NM"].Value = dt.Rows[i]["CHUGJONG_NM"].ToString();
                    itemRawGrid.Rows[i].Cells["CLASS_NM"].Value = dt.Rows[i]["CLASS_NM"].ToString();
                    itemRawGrid.Rows[i].Cells["TYPE_NM"].Value = dt.Rows[i]["TYPE_NM"].ToString();
                    itemRawGrid.Rows[i].Cells["COUNTRY_NM"].Value = dt.Rows[i]["COUNTRY_NM"].ToString();

                    itemRawGrid.Rows[i].Cells["IN_UNIT"].Value = dt.Rows[i]["INPUT_UNIT"].ToString();
                    itemRawGrid.Rows[i].Cells["OUT_UNIT"].Value = dt.Rows[i]["OUTPUT_UNIT"].ToString();
                    itemRawGrid.Rows[i].Cells["IN_UNIT_NM"].Value = dt.Rows[i]["INPUT_UNIT_NM"].ToString();
                    itemRawGrid.Rows[i].Cells["OUT_UNIT_NM"].Value = dt.Rows[i]["OUTPUT_UNIT_NM"].ToString();
                    itemRawGrid.Rows[i].Cells["TOTAL_AMT"].Value = (decimal.Parse(dt.Rows[i]["TOTAL_AMT"].ToString())).ToString("#,0.######");
                }
            }
            else
            {
                //itemCompGridAdd(); //데이터가 없을 경우 빈 행 생성
            }

            dt = wDm.fn_Item_Flow_List(condition);

            this.itemFlowGrid.RowCount = dt.Rows.Count;
            //itemFlowGrid.Rows.Clear();
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //itemFlowGrid.Rows.Add();
                    itemFlowGrid.Rows[i].Cells["F_ITEM_CD"].Value = dt.Rows[i]["ITEM_CD"].ToString();
                    itemFlowGrid.Rows[i].Cells["F_SEQ"].Value = dt.Rows[i]["SEQ"].ToString();
                    itemFlowGrid.Rows[i].Cells["FLOW_CD"].Value = dt.Rows[i]["FLOW_CD"].ToString();
                    itemFlowGrid.Rows[i].Cells["FLOW_NM"].Value = dt.Rows[i]["FLOW_NM"].ToString();
                    itemFlowGrid.Rows[i].Cells["FLOW_ETC"].Value = dt.Rows[i]["COMMENT"].ToString();
                    if (dt.Rows[i]["FLOW_INSERT_YN"].ToString().Equals("Y"))
                    {
                        itemFlowGrid.Rows[i].Cells["FLOW_YN"].Value = true;
                    }
                    else
                    {
                        itemFlowGrid.Rows[i].Cells["FLOW_YN"].Value = false;
                    }
                }
            }
            else
            {
                //itemFlowGridAdd();
            }

            dt = wDm.fn_Item_Half_List(condition,1);

            this.itemHalfGrid.RowCount = dt.Rows.Count;
            //itemFlowGrid.Rows.Clear();
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //itemFlowGrid.Rows.Add();
                    itemHalfGrid.Rows[i].Cells["MAIN_ITEM_CD"].Value = dt.Rows[i]["ITEM_CD"].ToString();
                    itemHalfGrid.Rows[i].Cells["H_SEQ"].Value = dt.Rows[i]["SEQ"].ToString();
                    itemHalfGrid.Rows[i].Cells["HALF_ITEM_CD"].Value = dt.Rows[i]["HALF_ITEM_CD"].ToString();
                    itemHalfGrid.Rows[i].Cells["HALF_ITEM_NM"].Value = dt.Rows[i]["HALF_ITEM_NM"].ToString();
                    itemHalfGrid.Rows[i].Cells["H_UNIT_CD"].Value = dt.Rows[i]["UNIT_CD"].ToString();
                    itemHalfGrid.Rows[i].Cells["H_UNIT_NM"].Value = dt.Rows[i]["UNIT_NM"].ToString();
                    itemHalfGrid.Rows[i].Cells["OLD_HALF_ITEM_NM"].Value = dt.Rows[i]["HALF_ITEM_NM"].ToString();
                    itemHalfGrid.Rows[i].Cells["H_TOTAL_AMT"].Value = (decimal.Parse(dt.Rows[i]["TOTAL_AMT"].ToString())).ToString("#,0.######");
                }
            }
            else
            {
               // itemHalfGridAdd();
            }
        }
        private void item_del()
        {
            ComInfo comInfo = new ComInfo();
            DialogResult msgOk = comInfo.deleteConfrim("원자재입고", txt_item_nm.Text.ToString());

            if (msgOk == DialogResult.No)
            {
                return;
            }

            wnDm wDm = new wnDm();
            int rsNum = wDm.deleteItem(txt_item_cd.Text.ToString());
            if (rsNum == 0)
            {
                resetSetting();

                item_list();
                MessageBox.Show("성공적으로 삭제하였습니다.");
            }
            else if (rsNum == 1)
            {
                MessageBox.Show("삭제에 실패하였습니다.");
            }

        }

        #endregion item logic

        private void txt_bs_stock_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComInfo.onlyNum2(sender, e);
        }

        private void txt_item_weight_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComInfo.onlyNum2(sender, e);
        }

        private void txt_input_price_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComInfo.onlyNum2(sender, e);
        }

        private void txt_output_price_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComInfo.onlyNum2(sender, e);
        }

        private void txt_char_amt_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComInfo.onlyNum2(sender, e);
        }

        private void txt_pack_amt_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComInfo.onlyNum2(sender, e);
        }

        private void txt_box_bar_cd_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComInfo.onlyNum2(sender, e);
        }

        private void txt_unit_bar_cd_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComInfo.onlyNum2(sender, e);
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            item_list();
        }

        #region input grid logic

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

            //wConst.init_RowText(grd, e.RowIndex);

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

        private void grid_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
        //     폼 로딩시에는, 저장 안함.
        //    if (bSetFirst == true) return;

        //    conDataGridView grd = (conDataGridView)sender;

        //    sColumnsWW = "";
        //    for (int kk = 0; kk < grd.Columns.Count; kk++)
        //    {
        //        if (kk > 0)
        //        {
        //            sColumnsWW += ",";
        //        }
        //        sColumnsWW += grd.Columns[kk].Width.ToString();
        //    }
        //    Save_Layout();
        }

        private void grid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            conDataGridView grd = (conDataGridView)sender;
            DataGridViewCell cell = grd[e.ColumnIndex, e.RowIndex];

            cell.Style.BackColor = Color.White;

            #region 공통 그리드 체크
            if (grd.Columns[e.ColumnIndex].ToolTipText.IndexOf("명칭") >= 0 && grd._KeyInput == "enter")
            {
                string rat_mat_nm = (string)grd.Rows[e.RowIndex].Cells["RAW_MAT_NM"].Value;
                wnDm wDm = new wnDm();
                DataTable dt = new DataTable();
                dt = wDm.fn_Raw_List("where RAW_MAT_NM like '%" + rat_mat_nm + "%' ");

                if (dt.Rows.Count > 1) { //row가 2줄이 넘을 경우 팝업으로 넘어간다.

                    wConst.call_pop_raw_mat(grd, dt, e.RowIndex, rat_mat_nm,1);
                    //itemCompGridAdd();
                }
                else if (dt.Rows.Count == 1) //row가 1일 경우 해당 row에 값을 자동 입력한다.
                {
                    grd.Rows[e.RowIndex].Cells["RAW_MAT_CD"].Value = dt.Rows[0]["RAW_MAT_CD"].ToString();
                    grd.Rows[e.RowIndex].Cells["RAW_MAT_NM"].Value = dt.Rows[0]["RAW_MAT_NM"].ToString();
                    grd.Rows[e.RowIndex].Cells["OLD_RAW_MAT_NM"].Value = dt.Rows[0]["RAW_MAT_NM"].ToString(); //백업 키워드 
                    grd.Rows[e.RowIndex].Cells["SPEC"].Value = dt.Rows[0]["SPEC"].ToString();
                    grd.Rows[e.RowIndex].Cells["IN_UNIT"].Value = dt.Rows[0]["INPUT_UNIT"].ToString(); 
                    grd.Rows[e.RowIndex].Cells["OUT_UNIT"].Value = dt.Rows[0]["OUTPUT_UNIT"].ToString(); 
                    grd.Rows[e.RowIndex].Cells["IN_UNIT_NM"].Value = dt.Rows[0]["INPUT_UNIT_NM"].ToString();
                    grd.Rows[e.RowIndex].Cells["OUT_UNIT_NM"].Value = dt.Rows[0]["OUTPUT_UNIT_NM"].ToString();
                    grd.Rows[e.RowIndex].Cells["TOTAL_AMT"].Value = "0";

                    itemCompGridAdd();
                }
                else { //row가 없는 경우
                    MessageBox.Show("데이터가 없습니다.");
                }
            }
            #endregion 공통 그리드 체크 

            //string sSearchTxt = "" + (string)grd.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

        }

        private void grid_CellEndEdit2(object sender, DataGridViewCellEventArgs e)
        {
            conDataGridView grd = (conDataGridView)sender;
            DataGridViewCell cell = grd[e.ColumnIndex, e.RowIndex];

            cell.Style.BackColor = Color.White;

            #region 공통 그리드 체크
            if (grd.Columns[e.ColumnIndex].ToolTipText.IndexOf("명칭") >= 0 && grd._KeyInput == "enter")
            {
                string flow_nm = (string)grd.Rows[e.RowIndex].Cells["FLOW_NM"].Value;
                wnDm wDm = new wnDm();
                DataTable dt = new DataTable();
                dt = wDm.fn_Flow_List("where FLOW_NM like '%" + flow_nm + "%' ");

                if (dt.Rows.Count > 1)
                { //row가 2줄이 넘을 경우 팝업으로 넘어간다.

                    wConst.call_pop_flow(grd, dt, e.RowIndex, flow_nm);
                    //itemFlowGridAdd();
                }
                else if (dt.Rows.Count == 1) //row가 1일 경우 해당 row에 값을 자동 입력한다.
                {
                    grd.Rows[e.RowIndex].Cells["FLOW_CD"].Value = dt.Rows[0]["FLOW_CD"].ToString();
                    grd.Rows[e.RowIndex].Cells["FLOW_NM"].Value = dt.Rows[0]["FLOW_NM"].ToString();
                    grd.Rows[e.RowIndex].Cells["OLD_FLOW_NM"].Value = dt.Rows[0]["FLOW_NM"].ToString(); //백업 키워드 
                    if (dt.Rows[0]["FLOW_INSERT_YN"].ToString().Equals("Y"))
                    {
                        grd.Rows[e.RowIndex].Cells["FLOW_YN"].Value = true;
                    }
                    else 
                    {
                        grd.Rows[e.RowIndex].Cells["FLOW_YN"].Value = false;
                    }

                    itemFlowGridAdd();
                }
                else
                { //row가 없는 경우
                    MessageBox.Show("데이터가 없습니다.");
                }
            }
            #endregion 공통 그리드 체크

            //string sSearchTxt = "" + (string)grd.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
        }

        private void grid_CellEndEdit3(object sender, DataGridViewCellEventArgs e)
        {
            conDataGridView grd = (conDataGridView)sender;
            DataGridViewCell cell = grd[e.ColumnIndex, e.RowIndex];

            cell.Style.BackColor = Color.White;

            #region 공통 그리드 체크
            if (grd.Columns[e.ColumnIndex].ToolTipText.IndexOf("명칭") >= 0 && grd._KeyInput == "enter")
            {
                string half_item_nm = (string)grd.Rows[e.RowIndex].Cells["HALF_ITEM_NM"].Value;
                wnDm wDm = new wnDm();
                DataTable dt = new DataTable();

                StringBuilder sb = new StringBuilder();
                if (txt_item_cd.Text != null && !txt_item_cd.Text.ToString().Equals("")) 
                {
                    sb.AppendLine("and A.ITEM_CD != '" + txt_item_cd.Text.ToString() + "' ");
                }
                sb.AppendLine("and A.ITEM_NM like '%" + half_item_nm + "%' ");
                dt = wDm.fn_Half_List(sb.ToString());

                if (dt.Rows.Count > 1)
                { //row가 2줄이 넘을 경우 팝업으로 넘어간다.

                    wConst.call_pop_item_half(grd, dt, e.RowIndex, half_item_nm);
                    //itemHalfGridAdd();
                }
                else if (dt.Rows.Count == 1) //row가 1일 경우 해당 row에 값을 자동 입력한다.
                {
                    grd.Rows[e.RowIndex].Cells["HALF_ITEM_CD"].Value = dt.Rows[0]["ITEM_CD"].ToString();
                    grd.Rows[e.RowIndex].Cells["HALF_ITEM_NM"].Value = dt.Rows[0]["ITEM_NM"].ToString();
                    grd.Rows[e.RowIndex].Cells["OLD_HALF_ITEM_NM"].Value = dt.Rows[0]["ITEM_NM"].ToString(); //백업 키워드 
                    grd.Rows[e.RowIndex].Cells["H_UNIT_CD"].Value = dt.Rows[0]["UNIT_CD"].ToString();
                    grd.Rows[e.RowIndex].Cells["H_UNIT_NM"].Value = dt.Rows[0]["UNIT_NM"].ToString();
                    grd.Rows[e.RowIndex].Cells["H_TOTAL_AMT"].Value = "0";

                    itemHalfGridAdd();

                }
                else
                { //row가 없는 경우
                    MessageBox.Show("데이터가 없습니다.");
                }
            }
            #endregion 공통 그리드 체크

            //string sSearchTxt = "" + (string)grd.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
        }
        #endregion input grid logic

        private void btn_raw_plus_Click(object sender, EventArgs e)
        {
            plus_logic(itemRawGrid);
        }

        private void btn_raw_minus_Click(object sender, EventArgs e)
        {
            minus_logic(itemRawGrid,1); //1 제품구성
        }

        private void btn_flow_plus_Click(object sender, EventArgs e)
        {
            plus_logic(itemFlowGrid);
        }

        private void btn_flow_minus_Click(object sender, EventArgs e)
        {
            minus_logic(itemFlowGrid,2); //2 공정구성
        }

        private void btn_half_plus_Click(object sender, EventArgs e)
        {
            plus_logic(itemHalfGrid);
        }

        private void btn_half_minus_Click(object sender, EventArgs e)
        {
            minus_logic(itemHalfGrid, 3); //2 반제품구성
        }

        private void plus_logic(DataGridView dgv) {
            if (dgv.Rows.Count < 50)
            {
                dgv.Rows.Add();
                dgv.CurrentCell = dgv[2, dgv.Rows.Count - 1];
            }
            else 
            {
                MessageBox.Show("최대 50행까지 가능합니다.");
            }
        }

        private void minus_logic(DataGridView dgv, int num) {
            if (dgv.Rows.Count > 1)
            {
                if (num == 1)
                {
                    if ((string)dgv.SelectedRows[0].Cells["SEQ"].Value != "" && dgv.SelectedRows[0].Cells["SEQ"].Value != null)
                    {
                        del_compGrid.Rows.Add();

                        del_compGrid.Rows[del_compGrid.Rows.Count - 1].Cells["ITEM_CD"].Value = dgv.SelectedRows[0].Cells["ITEM_CD"].Value;
                        del_compGrid.Rows[del_compGrid.Rows.Count - 1].Cells["SEQ"].Value = dgv.SelectedRows[0].Cells["SEQ"].Value;
                    }
                }
                else if (num == 2)
                {
                    if ((string)dgv.SelectedRows[0].Cells["F_SEQ"].Value != "" && dgv.SelectedRows[0].Cells["F_SEQ"].Value != null)
                    {
                        del_flowGrid.Rows.Add();

                        del_flowGrid.Rows[del_flowGrid.Rows.Count - 1].Cells["F_ITEM_CD"].Value = dgv.SelectedRows[0].Cells["F_ITEM_CD"].Value;
                        del_flowGrid.Rows[del_flowGrid.Rows.Count - 1].Cells["F_SEQ"].Value = dgv.SelectedRows[0].Cells["F_SEQ"].Value;
                    }
                }
                else
                {
                    if ((string)dgv.SelectedRows[0].Cells["H_SEQ"].Value != "" && dgv.SelectedRows[0].Cells["H_SEQ"].Value != null)
                    {
                        del_halfGrid.Rows.Add();

                        del_halfGrid.Rows[del_halfGrid.Rows.Count - 1].Cells["MAIN_ITEM_CD"].Value = dgv.SelectedRows[0].Cells["MAIN_ITEM_CD"].Value;
                        del_halfGrid.Rows[del_halfGrid.Rows.Count - 1].Cells["H_SEQ"].Value = dgv.SelectedRows[0].Cells["H_SEQ"].Value;
                    }
                }

                dgv.Rows.RemoveAt(dgv.SelectedRows[0].Index);
                dgv.CurrentCell = dgv[2, dgv.Rows.Count - 1];
            }
            else
            {
                MessageBox.Show("마지막 행은 삭제할 수 없습니다.");
            }
        }

        private void itemCompGridAdd() 
        {
            itemRawGrid.Rows.Add();
            itemRawGrid.Rows[itemRawGrid.Rows.Count - 1].Cells["TOTAL_AMT"].Value = "0";
        }

        private void itemFlowGridAdd()
        {
      
            itemFlowGrid.Rows.Add();
            
        }

        private void itemHalfGridAdd()
        {
            itemHalfGrid.Rows.Add();
            itemHalfGrid.Rows[itemHalfGrid.Rows.Count - 1].Cells["H_TOTAL_AMT"].Value = "0";
        }

        private void btn_Cust_Click(object sender, EventArgs e)
        {
            Popup.pop거래처검색 frm = new Popup.pop거래처검색();

            frm.sCustGbn = "1";
            frm.ShowDialog();

            if (frm.sCode != "")
            {
                cmb_cust.SelectedValue = frm.sCode.Trim();
            }
            else
            {
                // txt_cust_cd.Text = old_cust_nm;
            }

            frm.Dispose();
            frm = null;
        }

        private void btnBoxBar_Click(object sender, EventArgs e)
        {
            btnBoxBar.Enabled = false;
            strCondition = "";

            if (txt_item_cd.Text.Trim() == "")
            {
                MessageBox.Show("제품을 선택하세요.");
                btnBoxBar.Enabled = true;
                return;
            }

            if(txt_box_bar_cd.Text.ToString().Equals(""))
            {
                MessageBox.Show("박스바코드를 입력하시기 바랍니다.");
                btnBoxBar.Enabled = true;
                return;
            }

            //bindData_Print();
            //----------------------------------------
            bindData(txt_box_bar_cd, txt_box_amt);

            if (strCondition == "No")
            {
                MessageBox.Show("출력할 자료가 없습니다.");
                btnBoxBar.Enabled = true;
                return;
            }

            if (strCondition != "ERROR")
            {
                strCondition = "제품바코드";

                frmPrt = readyPrt;
                frmPrt.Show();
                frmPrt.BringToFront();
                frmPrt.prt_바코드(adoPrt, strCondition, "제품박스바코드");
            }

            btnBoxBar.Enabled = true;
        }

        private void btnUnitBar_Click(object sender, EventArgs e)
        {
            btnUnitBar.Enabled = false;
            strCondition = "";

            if (txt_item_cd.Text.Trim() == "")
            {
                MessageBox.Show("제품을 선택하세요.");
                btnUnitBar.Enabled = true;
                return;
            }

            if (txt_unit_bar_cd.Text.ToString().Equals(""))
            {
                MessageBox.Show("낱개바코드를 입력하시기 바랍니다.");
                btnUnitBar.Enabled = true;
                return;
            }

            bindData(txt_unit_bar_cd, txt_unit_amt);

            if (strCondition == "No")
            {
                MessageBox.Show("출력할 자료가 없습니다.");
                btnUnitBar.Enabled = true;
                return;
            }

            if (strCondition != "ERROR")
            {
                strCondition = "제품바코드";

                frmPrt = readyPrt;
                frmPrt.Show();
                frmPrt.BringToFront();
                frmPrt.prt_바코드(adoPrt, strCondition,"제품낱개바코드");
            }

            btnUnitBar.Enabled = true;
        }

        public void bindData(TextBox txt_bar_cd, TextBox txt_amt)
        {
            Application.DoEvents();

            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = fn_제품바코드_List();

                adoPrt = new DataTable();
                adoPrt = dt.Copy();

                int j = 0;
                int k = 0;

                string sCode = "" + this.txt_item_cd.Text.Trim();    //제품코드
                string sName = "" + this.txt_item_nm.Text.Trim();    //제품명
                string sSpec = "" + this.txt_spec.Text.Trim();    //규격
                string sBarcode = "" + txt_bar_cd.Text.Trim();    //박스바코드
                //string sBarcode = "" + this.txt_item_cd.Text.Trim();    //박스바코드

                string nQty = "" + txt_amt.Text.Trim(); //수량
                string nQtyS = "" + txt_amt.Text.Trim(); //수량

                //--- 수량은 6자리(100,000) 까지 하고 제품코드+수량으로 한다
                int nLength = 6 - nQtyS.Length;
                for (k = 0; k < nLength; k++)
                {
                    nQtyS = "0" + nQtyS;
                }
                //sBarcode = txt_box_bar_cd.Text.ToString(); //sBarcode + nQtyS;
                //----- end

                int nCnt = 1; //출력수량...나중에 출력수량 입력받아 출력한다

                for (int i = 0; i < nCnt; i++)
                {

                    dt.Rows[j]["no"] = j;

                    dt.Rows[j]["제조처"] = Common.p_strCompNm;

                    dt.Rows[j]["제품코드"] = sCode;
                    dt.Rows[j]["제품명"] = sName;
                    dt.Rows[j]["규격"] = sSpec;
                    dt.Rows[j]["수량"] = nQty;

                    dt.Rows[j]["바코드제조번호"] = "*" + sBarcode + "*";

                    j = j + 1;

                    adoPrt = dt.Copy();
                    
                }

                //데이타 끝나고 다시 copy를 써준 이유는 for중에 no에 값을 엏었기 때문에 출력물 데이타테이블(dt)를 다시 복사함
                adoPrt = dt.Copy();

                //for (int i = j + 1; i < this.InputTabGrid.Rows.Count; i++)
                //{
                //    adoPrt.Rows[i].Delete();
                //}
                //adoPrt.AcceptChanges(); //--삭제확정

                if (k == 0)
                {
                    strCondition = "No";
                }

            }
            catch (Exception ex)
            {
                strCondition = "ERROR";
                MessageBox.Show("검색중에 오류가 발생했습니다.");
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
            }
        }

        public DataTable fn_제품바코드_List()
        {
            StringBuilder sb = new StringBuilder();

            //sb.AppendLine("SELECT A.INPUT_DATE, '' AS no, '' AS 입고일자, '' AS 입고번호, ''  AS 입고순번, '' AS 원부재료코드, '' 원부재료명, '' AS 규격    ");
            //sb.AppendLine(", '' AS HEAT_NO, '' AS HEAT_TIME, '' AS ORDER_DATE, '' AS ORDER_CD, '' AS ORDER_SEQ, '' AS RAW_MAT_GUBUN ");
            //sb.AppendLine(", '' AS S_CODE_NM, '' AS 단위코드, '' AS 단위명  ");
            //sb.AppendLine(", '' AS 수량, '' AS 단가, '' AS 금액, '' AS 제조번호, '' AS 바코드제조번호 ");

            //sb.AppendLine("  FROM F_RAW_DETAIL			AS A  ");
            //sb.AppendLine(" WHERE A.INPUT_DATE >= '" + start_date.Text.ToString() + "' AND  A.INPUT_DATE <= '" + end_date.Text.ToString() + "'");

            sb.AppendLine(" SELECT '' AS no ");
            sb.AppendLine(", '' AS 제품코드, '' 제품명, '' AS 규격    ");
            sb.AppendLine(", '' AS 바코드제조번호, '' AS 수량, '' AS 제조처 ");
            sb.AppendLine("  FROM N_ITEM_CODE A  ");
            sb.AppendLine(" WHERE 1=1  ");
            sb.AppendLine("   AND A.ITEM_CD = '" + txt_item_cd.Text.ToString() + "'  ");

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }

            return wAdo.SqlCommandSelect(sCommand);
        }

        private void btn_link_srch_Click(object sender, EventArgs e)
        {
            Popup.pop_sf_장터지기제품 frm = new Popup.pop_sf_장터지기제품();

            //frm.sCustGbn = "1";
            frm.ShowDialog();

            if (frm.sCode != "")
            {
                //txt_link_input.Text = frm.sCode.Trim();
            }

            frm.Dispose();
            frm = null;
        }

        private void txt_srch_Leave(object sender, EventArgs e)
        {
            item_list();
        }
    }
}
