using System;
using System.Collections;
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
    public partial class pop거래처잔고수정 : Form
    {
        private wnGConstant wConst = new wnGConstant();

        wnAdo wAdo = new wnAdo();

        public string sCust_nm = "";
        public string sCust_cd = "";
        public string sUptae = "";
        public string sJongmock = "";
        public string sDamdang = "";
        public string sSaupNo = "";
        public string sOwner = "";
        public string sPhone = "";
        public string sFax = "";
        public string sOpenDay = "";
        public string sAddr = "";
        public string sTax_nm = "";
        public string sBalance = "";


        public pop거래처잔고수정()
        {
            InitializeComponent();
        }

        private void pop거래처잔고수정_Load(object sender, EventArgs e)
        {
            txt_address.Text = sAddr;
            txt_cust_nm.Text = sCust_nm;
            txt_uptae.Text = sUptae;
            txt_jongmok.Text = sJongmock;
            txt_staff.Text = sDamdang;
            txt_saup.Text = sSaupNo;
            txt_ceo.Text = sOwner;
            txt_phone_num.Text = sPhone;
            txt_begin_date.Text = sOpenDay;
            txt_tax.Text = sTax_nm;


            custlist();


        }

        private void custlist()
        {
            wnDm wDm = new wnDm();

            StringBuilder sb = new StringBuilder();
            DataTable dt = new DataTable();


            dt = wDm.fn_Cust_List("where A.CUST_CD = '" + sCust_cd + "'   ");

            if (dt != null && dt.Rows.Count > 0)
            {
                txt_balance.Text = decimal.Parse(dt.Rows[0]["BALANCE"].ToString()).ToString("#,0.######");
            }



            sb.AppendLine("and A.CUST_CD = '" + sCust_cd + "'   ");

            dt = wDm.fn_cust_Change_Balance_list(sb.ToString());

            inputBalanceGrid.Rows.Clear();
            if (dt != null && dt.Rows.Count > 0)
            {
                inputBalanceGrid.RowCount = dt.Rows.Count;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    inputBalanceGrid.Rows[i].Cells["INPUT_DATE"].Value = dt.Rows[i]["INPUT_DATE"].ToString();
                    inputBalanceGrid.Rows[i].Cells["INPUT_CD"].Value = dt.Rows[i]["INPUT_CD"].ToString();
                    inputBalanceGrid.Rows[i].Cells["BEFORE_BALANCE"].Value = decimal.Parse(dt.Rows[i]["BEFORE_BALANCE"].ToString()).ToString("#,0.######");
                    inputBalanceGrid.Rows[i].Cells["AFTER_BALANCE"].Value = decimal.Parse(dt.Rows[i]["AFTER_BALANCE"].ToString()).ToString("#,0.######");
                    inputBalanceGrid.Rows[i].Cells["INTIME"].Value = dt.Rows[i]["INTIME"].ToString();
                    inputBalanceGrid.Rows[i].Cells["INSTAFF"].Value = dt.Rows[i]["STAFF_NM"].ToString();
                    inputBalanceGrid.Rows[i].Cells["VALUE"].Value = dt.Rows[i]["VALUE"].ToString();
                    
                }
            }

        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Popup.pop거래처잔고수정_detail msg2 = new pop거래처잔고수정_detail();
            msg2.sCust_cd = this.sCust_cd;
            msg2.sCust_nm = txt_cust_nm.Text;
            msg2.sBalance = txt_balance.Text;

            Console.WriteLine("TEST@"+this.sBalance);

            msg2.StartPosition = FormStartPosition.CenterParent;
            msg2.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            msg2.ShowDialog();


            custlist();

        }

        
    }
}

