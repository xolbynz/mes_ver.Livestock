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

namespace 스마트팩토리.Popup
{
    public partial class pop설비검색 : Form
    {
        public DataTable dt = new DataTable();
        public pop설비검색()
        {
            InitializeComponent();
        }

        private void pop설비검색_Load(object sender, EventArgs e)
        {
            fac_list();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        #region fac logic 

        private void fac_list() 
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_Fac_List("");

                if (dt != null && dt.Rows.Count > 0)
                {
                    facGrid.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        facGrid.Rows[i].Cells[0].Value = dt.Rows[i]["FAC_CD"].ToString();
                        facGrid.Rows[i].Cells[1].Value = dt.Rows[i]["FAC_NM"].ToString();
                        facGrid.Rows[i].Cells[2].Value = dt.Rows[i]["SPEC"].ToString();
                        facGrid.Rows[i].Cells[3].Value = dt.Rows[i]["MANU_COMPANY"].ToString();
                        facGrid.Rows[i].Cells[4].Value = dt.Rows[i]["DEPT_NM"].ToString();
                    }
                }
                else
                {
                    facGrid.Rows.Clear();
                }
            }
            catch (Exception e) 
            {
                MessageBox.Show(e.ToString());
            }
            

        }
        #endregion fac logic

        private void facGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;

                string condition = "where FAC_CD = '" + facGrid.Rows[e.RowIndex].Cells[0].Value.ToString() + "'";

                dt = wDm.fn_Fac_List(condition);
                this.dt = dt;

                this.Close();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
