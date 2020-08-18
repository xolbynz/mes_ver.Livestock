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
    public partial class frm제품기초수량등록 : Form
    {
        public frm제품기초수량등록()
        {
            InitializeComponent();
        }

        private void frm제품기초수량등록_Load(object sender, EventArgs e)
        {
            item_list();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            save_logic();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void item_list()
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_Item_List("");

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.dataItemGrid.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dataItemGrid.Rows[i].Cells["ITEM_CD"].Value = dt.Rows[i]["ITEM_CD"].ToString();
                        dataItemGrid.Rows[i].Cells["ITEM_NM"].Value = dt.Rows[i]["ITEM_NM"].ToString();
                        dataItemGrid.Rows[i].Cells["SPEC"].Value = dt.Rows[i]["SPEC"].ToString();
                        dataItemGrid.Rows[i].Cells["BASIC_STOCK"].Value = dt.Rows[i]["BASIC_STOCK"].ToString();
                        dataItemGrid.Rows[i].Cells["PACK_DATE"].Value = "";
                        if (dt.Rows[i]["PRINT_YN"].ToString().Equals("Y"))
                        {
                            dataItemGrid.Rows[i].Cells["PRINT_YN"].Value = true;
                        }
                        else 
                        {
                            dataItemGrid.Rows[i].Cells["PRINT_YN"].Value = false;
                        }
                        //bool isChecked = Convert.ToBoolean(dataItemGrid.Rows[i].Cells["PRINT_YN"].Value);
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

        private void dataItemGrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            string name = dataItemGrid.CurrentCell.OwningColumn.Name;

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
             if (dataItemGrid.Rows.Count > 0) {
                 wnDm wDm = new wnDm();
                 int rsNum = wDm.updateBsItem(dataItemGrid);

                 if (rsNum == 0)
                 {
                     item_list();
                     MessageBox.Show("성공적으로 수정하였습니다.");
                 }
                 else if (rsNum == 1)
                     MessageBox.Show("저장에 실패하였습니다");
                 else
                     MessageBox.Show("Exception 에러");
             }
             else
             {
                 MessageBox.Show("데이터가 없습니다.");
             }
         }
    }
}
