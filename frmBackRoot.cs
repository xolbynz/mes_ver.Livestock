using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 스마트팩토리.CLS;

namespace 스마트팩토리
{
    public partial class frmBackRoot : Form
    {
        private IfrmInterface parentFrm = null;

        public frmBackRoot(IfrmInterface pFrm)
        {
            InitializeComponent();

            parentFrm = pFrm;
        }

        private void frmBackRoot_Load(object sender, EventArgs e)
        {

        }

        private void frmBackRoot_Resize(object sender, EventArgs e)
        {
            panCenter.Left = this.ClientSize.Width / 2 - panCenter.Width / 2;
            panCenter.Top = this.ClientSize.Height / 2 - panCenter.Height / 2;
        }

        private void menuButton_Click(object sender, EventArgs e)
        {
            string sFrmName = ((Button)sender).Tag.ToString();
            string sMnuName = ((Button)sender).Text.ToString();
            parentFrm.sub_Form(sFrmName, sMnuName);
        }

    }
}
