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
    public partial class frm기초코드조회 : Form
    {
        public frm기초코드조회()
        {
            InitializeComponent();
        }

        private void frm기초코드조회_Load(object sender, EventArgs e)
        {
            ComInfo.gridHeaderSet(tsGrid);

            wnDm wDm = new wnDm();
            DataTable dt = wDm.fn_tscode_list();
          
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tsGrid.Rows.Add();
                tsGrid.Rows[i].Cells["L_CODE"].Value = dt.Rows[i]["L_CODE"].ToString();
                tsGrid.Rows[i].Cells["S_CODE"].Value = dt.Rows[i]["S_CODE"].ToString();
                tsGrid.Rows[i].Cells["ORD"].Value = dt.Rows[i]["ORD"].ToString();
                tsGrid.Rows[i].Cells["S_CODE_NM"].Value = dt.Rows[i]["S_CODE_NM"].ToString();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
