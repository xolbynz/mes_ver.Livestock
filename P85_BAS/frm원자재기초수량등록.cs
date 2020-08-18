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

namespace 스마트팩토리.P85_BAS
{
    public partial class frm원자재기초수량등록 : Form
    {
        public frm원자재기초수량등록()
        {
            InitializeComponent();
        }

        private void frm원자재기초수량등록_Load(object sender, EventArgs e)
        {
            raw_list();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            save_logic();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void raw_list() 
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_Raw_List("");

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.dataRawGrid.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dataRawGrid.Rows[i].Cells["RAW_MAT_CD"].Value = dt.Rows[i]["RAW_MAT_CD"].ToString();
                        dataRawGrid.Rows[i].Cells["RAW_MAT_NM"].Value = dt.Rows[i]["RAW_MAT_NM"].ToString();
                        dataRawGrid.Rows[i].Cells["SPEC"].Value = dt.Rows[i]["SPEC"].ToString();
                        dataRawGrid.Rows[i].Cells["BASIC_STOCK"].Value = dt.Rows[i]["BASIC_STOCK"].ToString();
                        //datarawgrid.rows[i].cells["input_date"].value = "";
                        //if (dt.rows[i]["print_yn"].tostring().equals("y"))
                        //{
                        //    datarawgrid.rows[i].cells["print_yn"].value = true;
                        //}
                        //else
                        //{
                        //    datarawgrid.rows[i].cells["print_yn"].value = false;
                        //}
                        //bool isChecked = Convert.ToBoolean(dataItemGrid.Rows[i].Cells["PRINT_YN"].Value);
                    }
                }
                else
                {
                    dataRawGrid.Rows.Clear();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("시스템 에러: " + e.Message.ToString());
            }
        }

        private void dataRawGrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            string name = dataRawGrid.CurrentCell.OwningColumn.Name;

            if (name == "BASIC_STOCK")
            {
                e.Control.KeyPress += new KeyPressEventHandler(txtCheckNumeric_KeyPress);
            }

            else
            {
                e.Control.KeyPress -= new KeyPressEventHandler(txtCheckNumeric_KeyPress);
            }
        }

        private void txtCheckNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
                e.Handled = true;
        }

        private void save_logic()
        {
            if (dataRawGrid.Rows.Count > 0)
            {
                wnDm wDm = new wnDm();
                int rsNum = wDm.updateBsRaw(dataRawGrid);

                if (rsNum == 0)
                {
                    raw_list();
                    MessageBox.Show("성공적으로 수정하였습니다.");
                }
                else if (rsNum == 1)
                    MessageBox.Show("저장에 실패하였습니다");
                else
                    MessageBox.Show("Exception 에러");
            }
            else {
                MessageBox.Show("데이터가 없습니다.");
            }
        }
    }
}
