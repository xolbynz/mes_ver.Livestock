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

namespace 스마트팩토리.P50_QUA
{
    public partial class frm원자재수입검사기준 : Form
    {
        private wnGConstant wConst = new wnGConstant();
        private DataGridView del_rawGrid = new DataGridView();

        public frm원자재수입검사기준()
        {
            InitializeComponent();

            this.rawChkGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellEndEdit);
            this.rawChkGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grid_KeyDown);
            this.rawChkGrid.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.grid_RowsAdded);
        }

        private void frm원자재수입검사기준_Load(object sender, EventArgs e)
        {
            del_rawGrid.AllowUserToAddRows = false;

            del_rawGrid.Columns.Add("CHK_CD", "CHK_CD");
            del_rawGrid.Columns.Add("RAW_MAT_CHK", "RAW_MAT_CHK");

            chk_logic();
        }

        #region button logic

        private void btnNew_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveLogic();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            chk_logic();
        }

        private void btn_plus_Click(object sender, EventArgs e)
        {
            rawChkGridAdd();
        }

        private void btn_minus_Click(object sender, EventArgs e)
        {
            minus_logic(rawChkGrid);
        }

        #endregion button logic

        #region raw chk logic 

        private void chk_logic() 
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                StringBuilder sb = new StringBuilder();
                if (!txt_srch.Text.ToString().Equals("")) 
                {
                    sb.AppendLine(" and A.RAW_MAT_NM like '%" + txt_srch.Text.ToString() + "%' ");
                }
                dt = wDm.fn_Raw_Chk_List(sb.ToString());

                raw_chk_list(rawGrid, dt);
            }
            catch (Exception e)
            {

            }
        }

        private void raw_chk_list(DataGridView dg, DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                dg.RowCount = dt.Rows.Count;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dg.Rows[i].Cells[0].Value = (i + 1).ToString();
                    dg.Rows[i].Cells[1].Value = dt.Rows[i]["RAW_MAT_CD"].ToString();
                    dg.Rows[i].Cells[2].Value = dt.Rows[i]["RAW_MAT_NM"].ToString();
                    dg.Rows[i].Cells[3].Value = dt.Rows[i]["SPEC"].ToString();
                    if (dt.Rows[i]["RAW_MAT_CHK"] != null && !dt.Rows[i]["RAW_MAT_CHK"].ToString().Equals(""))
                    {
                        dg.Rows[i].Cells[4].Value = "Y";
                    }
                    else 
                    {
                        dg.Rows[i].Cells[4].Value = "N";
                    }
                }
            }
            else
            {
                dg.Rows.Clear();
            }
        }

        private void resetSetting()
        {
            lbl_raw_chk_gbn.Text = "";
            txt_raw_mat_cd.Text = "";
            txt_raw_mat_nm.Text = "";
            txt_spec.Text = "";
            txt_control_no.Text = "";
            txt_part_no.Text = "";
            btnDelete.Enabled = false;

            rawChkGrid.Rows.Clear();
        }

        private void rawChkGridAdd()
        {
            rawChkGrid.Rows.Add();
        }

        private void minus_logic(conDataGridView dgv)
        {
            if (dgv.Rows.Count > 1)
            {
                if ((string)dgv.SelectedRows[0].Cells["RAW_MAT_CHK"].Value != "" && dgv.SelectedRows[0].Cells["RAW_MAT_CHK"].Value != null)
                {
                    del_rawGrid.Rows.Add();

                    del_rawGrid.Rows[del_rawGrid.Rows.Count - 1].Cells["CHK_CD"].Value = dgv.SelectedRows[0].Cells["CHK_CD"].Value;
                    del_rawGrid.Rows[del_rawGrid.Rows.Count - 1].Cells["RAW_MAT_CHK"].Value = dgv.SelectedRows[0].Cells["RAW_MAT_CHK"].Value;

                }

                dgv.Rows.RemoveAt(dgv.SelectedRows[0].Index);
                dgv.CurrentCell = dgv[2, dgv.Rows.Count - 1];

                for (int i = 0; i < dgv.Rows.Count; i++) 
                {
                    dgv.Rows[i].Cells["NO"].Value = (i + 1).ToString();
                }
            }
        }

        #endregion raw chk logic

        #region raw chk grid logic
        private void rawGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //lbl_raw_chk_gbn.Text = "1";
            DataGridView dgv = (DataGridView)sender;

            wnDm wDm = new wnDm();
            DataTable dt = null;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" and A.RAW_MAT_CD = '" + dgv.Rows[e.RowIndex].Cells["RAW_MAT_CD"].Value.ToString() + "' ");

            dt = wDm.fn_Raw_Chk_List(sb.ToString());

            if (dt != null && dt.Rows.Count > 0)
            {
                txt_raw_mat_cd.Text = dt.Rows[0]["RAW_MAT_CD"].ToString();
                txt_raw_mat_nm.Text = dt.Rows[0]["RAW_MAT_NM"].ToString();
                txt_spec.Text = dt.Rows[0]["SPEC"].ToString();
                txt_control_no.Text = dt.Rows[0]["CONTROL_NO"].ToString();
                txt_part_no.Text = dt.Rows[0]["PART_NO"].ToString();

                if (dgv.Rows[e.RowIndex].Cells[4].Value.ToString().Equals("N"))
                {
                    lbl_raw_chk_gbn.Text = "";
                    btnDelete.Enabled = false;
                }
                else 
                {
                    lbl_raw_chk_gbn.Text = "1";
                    btnDelete.Enabled = true;
                    del_rawGrid.Rows.Clear();
                }

                dt = wDm.fn_Raw_Chk_Detail_List(sb.ToString());

                if (dt != null && dt.Rows.Count > 0)
                {
                    rawChkGrid.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++) 
                    {
                        rawChkGrid.Rows[i].Cells["NO"].Value = (i + 1).ToString();
                        rawChkGrid.Rows[i].Cells["CHK_CD"].Value = dt.Rows[i]["CHK_CD"].ToString();
                        rawChkGrid.Rows[i].Cells["CHK_ORD"].Value = dt.Rows[i]["CHK_ORD"].ToString();
                        rawChkGrid.Rows[i].Cells["CHK_NM"].Value = dt.Rows[i]["CHK_NM"].ToString();
                        rawChkGrid.Rows[i].Cells["CHK_STAN_VALUE"].Value = dt.Rows[i]["CHK_STAN_VALUE"].ToString();
                        rawChkGrid.Rows[i].Cells["RAW_MAT_CHK"].Value = dt.Rows[i]["RAW_MAT_CD"].ToString();
                        rawChkGrid.Rows[i].Cells["OLD_CHK_NM"].Value = dt.Rows[i]["CHK_NM"].ToString();
                        rawChkGrid.Rows[i].Cells["OLD_CHK_CD"].Value = dt.Rows[i]["CHK_CD"].ToString();
                    }
                }
                else 
                {
                    rawChkGrid.Rows.Clear();
                }
                
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

        private void grid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            conDataGridView grd = (conDataGridView)sender;

            grd.Rows[e.RowIndex].Cells[0].Value = false;

            // No.
            grd.Rows[e.RowIndex].Cells[1].Style.BackColor = Color.WhiteSmoke;
            grd.Rows[e.RowIndex].Cells[1].Style.SelectionBackColor = Color.Khaki;

        }

        private void grid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            conDataGridView grd = (conDataGridView)sender;
            DataGridViewCell cell = grd[e.ColumnIndex, e.RowIndex];

            cell.Style.BackColor = Color.White;


            #region 공통 그리드 체크
            if (grd.Columns[e.ColumnIndex].ToolTipText.IndexOf("명칭") >= 0 && grd._KeyInput == "enter")
            {
                string chk_nm = (string)grd.Rows[e.RowIndex].Cells["CHK_NM"].Value;
                wnDm wDm = new wnDm();
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("where 1=1 ");
                if (chk_nm != null) 
                {
                    sb.AppendLine(" and CHK_NM like '%" + chk_nm + "%' ");
                }
                sb.AppendLine(" and CHK_GUBUN = '3' ");
                dt = wDm.fn_Chk_List(sb.ToString());

                if (dt.Rows.Count > 1)
                {
                    wConst.call_pop_chk(grd, dt, e.RowIndex, chk_nm, 3);
                }
                else if (dt.Rows.Count == 1) //row가 1일 경우 해당 row에 값을 자동 입력한다.
                {
                    grd.Rows[e.RowIndex].Cells["NO"].Value = (e.RowIndex + 1).ToString();
                    grd.Rows[e.RowIndex].Cells["CHK_CD"].Value = dt.Rows[0]["CHK_CD"].ToString();
                    grd.Rows[e.RowIndex].Cells["CHK_ORD"].Value = dt.Rows[0]["CHK_ORD"].ToString();
                    grd.Rows[e.RowIndex].Cells["CHK_NM"].Value = dt.Rows[0]["CHK_NM"].ToString();
                    rawChkGrid.Rows[e.RowIndex].Cells["OLD_CHK_NM"].Value = dt.Rows[0]["CHK_NM"].ToString();
                    rawChkGrid.Rows[e.RowIndex].Cells["OLD_CHK_CD"].Value = dt.Rows[0]["CHK_CD"].ToString();
                }
                else 
                {
                    //row가 없는 경우
                    MessageBox.Show("데이터가 없습니다.");
                }
            }

            #endregion 공통 그리드 체크

            //string sSearchTxt = "" + (string)grd.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
        }
        #endregion raw chk grid logic
        

        private void btn_all_chk_Click(object sender, EventArgs e)
        {
            conDataGridView dgv = rawChkGrid;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("and A.CHK_GUBUN = '3' ");
            all_chk_logic(dgv, sb.ToString(), txt_raw_mat_cd);
        }

        private void all_chk_logic(conDataGridView dgv, string condition, TextBox txt_raw_mat_cd)
        {
            if (txt_raw_mat_cd.Text.ToString().Equals("")) 
            {
                MessageBox.Show("원자재 정보가 없습니다. ");
                return;
            }
            if (dgv.Rows.Count > 0) 
            {
                MessageBox.Show("항목 데이터가 존재합니다. ");
                return;
            }

            wnDm wDm = new wnDm();
            DataTable dt = new DataTable();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("where 1=1 ");
            sb.AppendLine(condition);//"and A.CHK_GUBUN = '3'"

            dt = wDm.fn_Chk_List(sb.ToString());

            if (dt != null && dt.Rows.Count > 0)
            {
                dgv.RowCount = dt.Rows.Count;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgv.Rows[i].Cells["NO"].Value = (i + 1).ToString();
                    dgv.Rows[i].Cells["CHK_CD"].Value = dt.Rows[i]["CHK_CD"].ToString();
                    dgv.Rows[i].Cells["CHK_ORD"].Value = dt.Rows[i]["CHK_ORD"].ToString();
                    dgv.Rows[i].Cells["CHK_NM"].Value = dt.Rows[i]["CHK_NM"].ToString();
                }
            }
            else
            {
                MessageBox.Show("데이터 일시 오류");
            }
        }

        private void saveLogic() 
        {
            if (txt_raw_mat_cd.Text.ToString().Equals(""))
            {
                MessageBox.Show("원자재 정보가 없습니다. ");
                return;
            }

            conDataGridView dgv = rawChkGrid;

            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("항목 데이터를 입력하셔야 합니다. ");
                return;
            }

            if (lbl_raw_chk_gbn.Text != "1") //신규
            {
                wnDm wDm = new wnDm();
                int rsNum = wDm.insertRawChk(txt_raw_mat_cd.Text.ToString()
                                              , txt_control_no.Text.ToString()
                                              , dgv);

                if (rsNum == 0)
                {
                    chk_logic();
                    lbl_raw_chk_gbn.Text = "1";
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
                int rsNum = wDm.updateRawChk(txt_raw_mat_cd.Text.ToString()
                                                  , txt_control_no.Text.ToString()
                                                  , dgv
                                                  , del_rawGrid);

                if (rsNum == 0)
                {
                    chk_logic();
                    MessageBox.Show("성공적으로 수정하였습니다.");
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
        }
    }
}
