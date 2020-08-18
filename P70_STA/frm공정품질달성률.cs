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
    public partial class frm공정품질달성률 : Form
    {
        private wnGConstant wConst = new wnGConstant();

        public frm공정품질달성률()
        {
            InitializeComponent();
        }

        private void frm공정품질달성률_Load(object sender, EventArgs e)
        {
            init_ComboBox();
        }
        private void btnSrch_Click(object sender, EventArgs e)
        {
        }

        private void init_ComboBox()
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
