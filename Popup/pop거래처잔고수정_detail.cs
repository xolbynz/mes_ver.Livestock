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
    public partial class pop거래처잔고수정_detail : Form
    {
        private wnGConstant wConst = new wnGConstant();

        wnAdo wAdo = new wnAdo();

        public string sCust_nm = "";
        public string sCust_cd = "";
        public string sCust_gubun = "";
        public string sBalance = "";


        public pop거래처잔고수정_detail()
        {
            InitializeComponent();
        }

        private void changeBalance()
        {

            txt_cust_nm.Text = sCust_nm;
            wnDm wDm = new wnDm();

            DataTable dt = null;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("WHERE CUST_CD = '"+sCust_cd+"' and INPUT_DATE <= '"+input_date.Text.ToString()+"'   ");

            dt = wDm.fn_sales_collect_list(sb.ToString());

            if (dt != null && dt.Rows.Count > 0)
            {
                txt_before_balnce.Text = decimal.Parse(dt.Rows[0]["BALANCE_DAY"].ToString()).ToString("#,0.######");
            }
            else
            {
                txt_before_balnce.Text = "0";
            }





        }

        private void pop거래처잔고수정_detail_Load(object sender, EventArgs e)
        {
            changeBalance();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void input_date_ValueChanged(object sender, EventArgs e)
        {
            changeBalance();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_after_balnce.Text == null || txt_after_balnce.Text.Equals(""))
                {
                    MessageBox.Show("수정 잔고를 반드시 입력해주십시오.");
                }

                string logic = "";
                decimal result = decimal.Parse(txt_after_balnce.Text) - decimal.Parse(txt_before_balnce.Text);
                if (result > 0)
                {
                    logic = "+";
                }
                else if (result < 0)
                {
                    logic = "-";
                }
                else
                {
                    MessageBox.Show("수정 잔고와 기존 잔고가 같습니다.");
                    return;
                }

                wnDm wDm = new wnDm();
                int rst = wDm.insert_Cust_Balance(
                    input_date.Text.ToString()
                    , sCust_cd
                    , txt_before_balnce.Text
                    , txt_after_balnce.Text
                    , logic
                    , result.ToString()
                    );

                if (rst == 0)
                {
                    MessageBox.Show("성공적으로 등록하였습니다");
                    this.Close();
                }
                else if (rst == 1)
                {
                    MessageBox.Show("등록에 실패하였습니다.");
                }
                else if (rst == 9)
                {
                    MessageBox.Show("SQL 커맨드 에러");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("오류발생");
            }

        }

        
    }
}

