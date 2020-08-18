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

namespace 스마트팩토리.P70_STA
{
    public partial class frm매출목표달성률 : Form
    {
        private wnGConstant wConst = new wnGConstant();

        public frm매출목표달성률()
        {
            InitializeComponent();
        }

        private void frm매출목표달성률_Load(object sender, EventArgs e)
        {
            init_ComboBox();
        }

        private void btnSrch_Click(object sender, EventArgs e)
        {
            pic01.Visible = true;
        }
        private void init_ComboBox()
        {
            string sqlQuery = "";
            cmb_year.ValueMember = "코드";
            cmb_year.DisplayMember = "명칭";
            sqlQuery = "select '1' as 코드, DATEPART(YYYY,dateadd(year,0,getdate())) as 명칭";
            //sqlQuery += " union all ";
            //sqlQuery += "select DATEPART(YYYY,dateadd(year,-1,getdate())) as s_year";

            wConst.ComboBox_Read_NoBlank(cmb_year, sqlQuery);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
