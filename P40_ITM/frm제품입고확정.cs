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

namespace 스마트팩토리.P40_ITM
{
    public partial class frm제품입고확정 : Form
    {
        public frm제품입고확정()
        {
            InitializeComponent();
        }

        private void frm제품입고확정_Load(object sender, EventArgs e)
        {
            start_date.Text = DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

         private void btnSearch_Click(object sender, EventArgs e)
        {
            wnDm wDm = new wnDm();
            DataTable dt = new DataTable();

            StringBuilder sb = new StringBuilder();
            //sb.AppendLine(" and B.F_SUB_DATE >= '" + start_date.Text.ToString() + "' and  B.F_SUB_DATE <= '" + end_date.Text.ToString() + "' ");
            sb.AppendLine(" and D.ITEM_IDEN_YN = 'Y' ");
            sb.AppendLine(" and A.COMPLETE_YN = 'Y' ");
            sb.AppendLine(" and (C.LINK_CD is not null or C.LINK_CD != '')");

            dt = wDm.fn_Item_Input_List(sb.ToString());

            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    InputTabGrid.DataSource = dt;

            //inputRmGrid.Rows.Clear();
            this.InputTabGrid.DataSource = null;
            this.InputTabGrid.RowCount = 0;

            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)


                //for (int i = 0; i < InputTabGrid.Rows.Count; i++) 
                {

                    InputTabGrid.Rows.Add();
                    //InputTabGrid.Rows[i].Cells["CHK"].Value = false;
                    InputTabGrid.Rows[i].Cells["LOT_NO"].Value = dt.Rows[i]["LOT_NO"].ToString();
                    InputTabGrid.Rows[i].Cells["LOT_SUB"].Value = dt.Rows[i]["LOT_SUB"].ToString();
                    InputTabGrid.Rows[i].Cells["LOT_식별표"].Value = dt.Rows[i]["LOT_BAR"].ToString();
                    InputTabGrid.Rows[i].Cells["포장일자"].Value = dt.Rows[i]["PACK_DATE"].ToString();
                    InputTabGrid.Rows[i].Cells["제품코드"].Value = dt.Rows[i]["ITEM_CD"].ToString();
                    InputTabGrid.Rows[i].Cells["제품명"].Value = dt.Rows[i]["ITEM_NM"].ToString();
                    InputTabGrid.Rows[i].Cells["규격"].Value = dt.Rows[i]["SPEC"].ToString();
                    InputTabGrid.Rows[i].Cells["단위코드"].Value = dt.Rows[i]["UNIT_CD"].ToString();
                    InputTabGrid.Rows[i].Cells["단위"].Value = dt.Rows[i]["UNIT_NM"].ToString();
                    InputTabGrid.Rows[i].Cells["입고확정여부"].Value = dt.Rows[i]["COMPLETE_YN"].ToString();
                    InputTabGrid.Rows[i].Cells["LINK_CD"].Value = dt.Rows[i]["LINK_CD"].ToString();
                    InputTabGrid.Rows[i].Cells["CHK"].Value = false;
                    if (InputTabGrid.Rows[i].Cells["입고확정여부"].Value.ToString().Equals("Y"))
                    {
                        InputTabGrid.Rows[i].Cells["CHK"].ReadOnly = true;
                    }
                    InputTabGrid.Rows[i].Cells["입고가격"].Value = dt.Rows[i]["INPUT_PRICE"].ToString();


                    InputTabGrid.Rows[i].Cells["수량"].Value = (decimal.Parse(dt.Rows[i]["F_SUB_AMT"].ToString())).ToString("#,0.00");

                    InputTabGrid.Rows[i].Cells["박스수량"].Value = "1";
                    InputTabGrid.Rows[i].Cells["업체명"].Value = dt.Rows[i]["CUST_NM"].ToString();
                }
            }
        }
         private void resetsetting()
         {
             wnDm wDm = new wnDm();
             DataTable dt = new DataTable();

             StringBuilder sb = new StringBuilder();
             //sb.AppendLine(" and B.F_SUB_DATE >= '" + start_date.Text.ToString() + "' and  B.F_SUB_DATE <= '" + end_date.Text.ToString() + "' ");
             sb.AppendLine(" and D.ITEM_IDEN_YN = 'Y' ");
             sb.AppendLine(" and A.COMPLETE_YN = 'Y' ");
             sb.AppendLine(" and (C.LINK_CD is not null or C.LINK_CD != '')");

             dt = wDm.fn_Item_Input_List(sb.ToString());

             //if (dt != null && dt.Rows.Count > 0)
             //{
             //    InputTabGrid.DataSource = dt;

             //inputRmGrid.Rows.Clear();
             this.InputTabGrid.DataSource = null;
             this.InputTabGrid.RowCount = 0;

             if (dt != null && dt.Rows.Count > 0)
             {
                 for (int i = 0; i < dt.Rows.Count; i++)


                 //for (int i = 0; i < InputTabGrid.Rows.Count; i++) 
                 {
                     InputTabGrid.Rows.Add();
                     InputTabGrid.Rows[i].Cells["LOT_NO"].Value = dt.Rows[i]["LOT_NO"].ToString();
                     InputTabGrid.Rows[i].Cells["LOT_SUB"].Value = dt.Rows[i]["LOT_SUB"].ToString();
                     InputTabGrid.Rows[i].Cells["LOT_식별표"].Value = dt.Rows[i]["LOT_BAR"].ToString();
                     InputTabGrid.Rows[i].Cells["포장일자"].Value = dt.Rows[i]["PACK_DATE"].ToString();
                     InputTabGrid.Rows[i].Cells["제품코드"].Value = dt.Rows[i]["ITEM_CD"].ToString();
                     InputTabGrid.Rows[i].Cells["제품명"].Value = dt.Rows[i]["ITEM_NM"].ToString();
                     InputTabGrid.Rows[i].Cells["규격"].Value = dt.Rows[i]["SPEC"].ToString();
                     InputTabGrid.Rows[i].Cells["단위코드"].Value = dt.Rows[i]["UNIT_CD"].ToString();
                     InputTabGrid.Rows[i].Cells["단위"].Value = dt.Rows[i]["UNIT_NM"].ToString();
                     InputTabGrid.Rows[i].Cells["입고확정여부"].Value = dt.Rows[i]["COMPLETE_YN"].ToString();
                     InputTabGrid.Rows[i].Cells["LINK_CD"].Value = dt.Rows[i]["LINK_CD"].ToString();
                     InputTabGrid.Rows[i].Cells["CHK"].Value = false;
                     if (InputTabGrid.Rows[i].Cells["입고확정여부"].Value.ToString().Equals("Y"))
                     {
                         
                         InputTabGrid.Rows[i].Cells["CHK"].ReadOnly = true;
                     }
                     InputTabGrid.Rows[i].Cells["입고가격"].Value = dt.Rows[i]["INPUT_PRICE"].ToString();


                     InputTabGrid.Rows[i].Cells["수량"].Value = (decimal.Parse(dt.Rows[i]["F_SUB_AMT"].ToString())).ToString("#,0.00");

                     InputTabGrid.Rows[i].Cells["박스수량"].Value = "1";
                     InputTabGrid.Rows[i].Cells["업체명"].Value = dt.Rows[i]["CUST_NM"].ToString();
                 }
             }
         }

         private void btnSave_Click(object sender, EventArgs e)
         {

             StringBuilder sb = new StringBuilder();
             sb.AppendLine("※ 입고 확정 ※");
             sb.AppendLine("입고일자는 현재 날짜로 저장됩니다.");
             sb.AppendLine("정말로 저장하시겠습니까? ");
             ComInfo comInfo = new ComInfo();
             DialogResult msgOk = comInfo.warningMessage(sb.ToString());

             if (msgOk == DialogResult.No)
             {
                 return;
             }


             if (InputTabGrid.Rows.Count == 0)
             {
                 MessageBox.Show("입고확정할 자료가 없습니다.");
                 return;
             }

             Boolean chkflag = false;
             for (int i = 0; i < InputTabGrid.Rows.Count; i++)
             {
                 if (InputTabGrid.Rows[i].Cells["CHK"].Value == null || (bool)InputTabGrid.Rows[i].Cells["CHK"].Value == true)
                 {
                     chkflag = true;
                     break;
                 }
             }

             if (chkflag == false)
             {
                 MessageBox.Show("입고확정할 자료가 없습니다.");
                 return;
             }
             bindData();

             
         }


         public void bindData()
         {

             try
             {
                 wnDm wDm = new wnDm();

                 

                         int result = wDm.fn_Insert_Item_To_Jang(InputTabGrid);

                         if (result == 0)
                         {
                             MessageBox.Show("입고 확정 성공");
                             resetsetting();
                             
                         }
                         else
                         {
                             MessageBox.Show("입고 확정 실패");

                         }
                     
                 

                 //데이타 끝나고 다시 copy를 써준 이유는 for중에 no에 값을 엏었기 때문에 출력물 데이타테이블(dt)를 다시 복사함
                 

             }
             catch (Exception ex)
             {
                 MessageBox.Show("검색중에 오류가 발생했습니다.");
                 wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
             }
         }

        
    }
}
