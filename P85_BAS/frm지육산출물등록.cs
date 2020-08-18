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
using 스마트팩토리.Controls;

namespace 스마트팩토리.P85_BAS
{
    public partial class frm지육산출물등록 : Form
    {

        private wnGConstant wConst = new wnGConstant();
        private ComInfo comInfo = new ComInfo();



        private DataGridView del_compGrid = new DataGridView();

        public frm지육산출물등록()
        {
            InitializeComponent();
            this.sourceRawGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellEndEdit);
            this.sourceRawGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grid_KeyDown);
            this.sourceRawGrid.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grid_ColumnHeaderMouseClick);
            this.sourceRawGrid.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.grid_RowsAdded);
            this.sourceRawGrid.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.grid_RowsRemoved);
            this.sourceRawGrid.CellValueChanged += new DataGridViewCellEventHandler(grid_CellValueChanged);
            
        }


        private void frm지육산출물등록_Load(object sender, EventArgs e)
        {
            ComInfo.gridHeaderSet(sourceRawGrid);
            ComInfo.gridHeaderSet(dataItemGrid);

            init_ComboBox();
            item_list();
        }



        private void init_ComboBox()
        {
            ComInfo comInfo = new ComInfo();
            string sqlQuery = "";

            cmb_used_srch.ValueMember = "코드";
            cmb_used_srch.DisplayMember = "명칭";
            sqlQuery = comInfo.queryCustUsedYnAll(); //사용여부검색
            wConst.ComboBox_Read_NoBlank(cmb_used_srch, sqlQuery);
        }


        private void item_logic()
        {
            try
            {
                
                if (txt_item_cd.Text.ToString().Equals(""))
                {
                    MessageBox.Show("지육을 선택하시기 바랍니다.");
                    return;
                }

                if (sourceRawGrid.Rows.Count > 0)
                {
                    int cnt = 0;
                    int grid_cnt = sourceRawGrid.Rows.Count;
                    for (int i = 0; i < grid_cnt; i++)
                    {
                        string txt_raw_mat_cd = (string)sourceRawGrid.Rows[i - cnt].Cells["RAW_MAT_CD"].Value;

                        if (txt_raw_mat_cd == "" || txt_raw_mat_cd == null)  //마지막 행에 원부재료코드가 없을 경우 제거
                        {
                            sourceRawGrid.Rows.RemoveAt(i - cnt);
                            cnt++;
                        }
                    }
                }
                else{
                    MessageBox.Show("산출물 없음");
                    return;

                }



                
                wnDm wDm = new wnDm();

                int rsNum = wDm.insertMeatOutCome(
                                  txt_item_cd.Text.ToString()
                                , sourceRawGrid
                                );

                    if (rsNum == 0)
                    {
                        //resetSetting();
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
                

            
            catch (Exception e)
            {
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
                sb.AppendLine("where 1=1 and RAW_MAT_GUBUN = 9 ");


                if (!txt_srch.Text.ToString().Equals(""))
                {
                    sb.AppendLine("and RAW_MAT_NM like '%" + txt_srch.Text.ToString() + "%' ");
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


                dt = wDm.fn_Raw_List(sb.ToString());

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
                        dataItemGrid.Rows[i].Cells[0].Value = dt.Rows[i]["RAW_MAT_CD"].ToString();
                        dataItemGrid.Rows[i].Cells[1].Value = //dt.Rows[i]["RAW_MAT_GUBUN"].ToString();
                                                                "지육";
                        dataItemGrid.Rows[i].Cells[2].Value = dt.Rows[i]["RAW_MAT_NM"].ToString();
                        dataItemGrid.Rows[i].Cells[3].Value = dt.Rows[i]["SPEC"].ToString();
                        
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


        private void btnSearch_Click(object sender, EventArgs e)
        {
            item_list();
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

             //del_compGrid.Rows.Clear(); //더블클릭 시 기존 삭제 데이터할 제품구성 리스트 초기화
             //del_flowGrid.Rows.Clear();

             try
             {
                 wnDm wDm = new wnDm();
                 DataTable dt = null;

                 string condition = "where RAW_MAT_CD = '" + dataItemGrid.Rows[e.RowIndex].Cells[0].Value.ToString() + "'";
                 string condition2 = "where RAW_SOURCE_CD = '" + dataItemGrid.Rows[e.RowIndex].Cells[0].Value.ToString() + "'";

                 try
                 {
                     dt = wDm.fn_Raw_List(condition);
                 }
                 catch (Exception e3)
                 {
                     Console.WriteLine(e3);
                 }

                 if (dt != null && dt.Rows.Count > 0)
                 {

                     txt_item_cd.Text = dt.Rows[0]["RAW_MAT_CD"].ToString();
                     txt_item_nm.Text = dt.Rows[0]["RAW_MAT_NM"].ToString();
                     
                     txt_comment.Text = dt.Rows[0]["COMMENT"].ToString();

                 }

                 gridDetail(condition2);

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

            dt = wDm.fn_Raw_Meat_List(condition);

            this.sourceRawGrid.RowCount = dt.Rows.Count;
            //sourceRawGrid.Rows.Clear();
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //sourceRawGrid.Rows.Add();
                    //sourceRawGrid.Rows[i].Cells["ITEM_CD"].Value = dt.Rows[i]["ITEM_CD"].ToString();
                    sourceRawGrid.Rows[i].Cells["SEQ"].Value = dt.Rows[i]["SEQ"].ToString();
                    sourceRawGrid.Rows[i].Cells["RAW_MAT_CD"].Value = dt.Rows[i]["RAW_MAT_CD"].ToString();
                    sourceRawGrid.Rows[i].Cells["RAW_MAT_NM"].Value = dt.Rows[i]["RAW_MAT_NM"].ToString();
                    sourceRawGrid.Rows[i].Cells["SPEC"].Value = dt.Rows[i]["SPEC"].ToString();
                    sourceRawGrid.Rows[i].Cells["IN_UNIT_NM"].Value = dt.Rows[i]["INPUT_UNIT_NM"].ToString();
                    sourceRawGrid.Rows[i].Cells["OUT_UNIT_NM"].Value = dt.Rows[i]["OUTPUT_UNIT_NM"].ToString();

                    //sourceRawGrid.Rows[i].Cells["OLD_RAW_MAT_NM"].Value = dt.Rows[i]["RAW_MAT_NM"].ToString();



                }
            }
            else
            {
                itemCompGridAdd(); //데이터가 없을 경우 빈 행 생성
            }

        }

        

        private void itemCompGridAdd()
        {
            sourceRawGrid.Rows.Add();
            sourceRawGrid.Rows[sourceRawGrid.Rows.Count - 1].Cells["TOTAL_AMT"].Value = "0";
        }

        private void btn_raw_plus_Click(object sender, EventArgs e)
        {
            plus_logic(sourceRawGrid);
        }

        private void btn_raw_minus_Click(object sender, EventArgs e)
        {
            minus_logic(sourceRawGrid, 1); //1 제품구성
        }


        private void plus_logic(DataGridView dgv)
        {
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

        private void minus_logic(DataGridView dgv, int num)
        {
            if (dgv.Rows.Count > 1)
            {
                dgv.Rows.RemoveAt(dgv.SelectedRows[0].Index);
                dgv.CurrentCell = dgv[2, dgv.Rows.Count - 1];
            }
            else
            {
                MessageBox.Show("마지막 행은 삭제할 수 없습니다.");
            }
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

        

        private void grid_CellEndEdit(object sender, DataGridViewCellEventArgs e)  //원재료 검색
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

                if (dt.Rows.Count > 1)
                { //row가 2줄이 넘을 경우 팝업으로 넘어간다.
                    Console.WriteLine("popup");
                    wConst.call_pop_raw_mat(grd, dt, e.RowIndex, rat_mat_nm, 1);
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
                else
                { //row가 없는 경우
                    MessageBox.Show("데이터가 없습니다.");
                }
            }
            #endregion 공통 그리드 체크

            //string sSearchTxt = "" + (string)grd.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

        }






        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //#endregion top menu
        private void btnSave_Click(object sender, EventArgs e)
        {
            item_logic();
        }



    }
}
