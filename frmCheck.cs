using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 스마트팩토리
{
    public partial class frmCheck : Form
    {
        public bool bRet = false;

        public frmCheck()
        {
            InitializeComponent();
        }

        private void frmCheck_Load(object sender, EventArgs e)
        {
            this.Width = panMain.Width;
            this.Height = panMain.Height;

            //tmSec.Enabled = true;
            tmStart.Enabled = true;
        }

        public bool Check_DBConnection()
        {
            try
            {
                using (SqlConnection dbConn = new SqlConnection())
                {
                    dbConn.ConnectionString = Common.p_sConn;
                    dbConn.Open();
                    dbConn.Close();
                }
            }
            catch (SqlException ex)
            {
                return false;
            }
            return true;
        }

        private void tmSec_Tick(object sender, EventArgs e)
        {
            lblToday.Text = DateTime.Now.ToString("ss");
            Application.DoEvents();
        }

        private void tmStart_Tick(object sender, EventArgs e)
        {
            tmStart.Enabled = false;

            bRet = Check_DBConnection();

            this.Close();
        }

    }
}
