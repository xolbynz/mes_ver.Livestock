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
using 스마트팩토리.Controls;

namespace 스마트팩토리.P90_SYS
{
    public partial class frm공지사항 : Form
    {
        private wnGConstant wConst = new wnGConstant();    

        private ComInfo comInfo = new ComInfo();
        private DataTable adoPrt = new DataTable();
        private DataTable adoPrt2 = new DataTable();        

        public frm공지사항()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            resetSetting();            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            notice_logic();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            DialogResult result1 = MessageBox.Show("정말로 삭제하시겠습니까?", "",
                                                    MessageBoxButtons.YesNo);

            if (result1 == DialogResult.Yes)
            {
                try
                {
                    wnDm wDm = new wnDm();

                    int rsNum = wDm.deleteNotice(
                                         textBox1.Text.ToString()
                                        );
                    DataTable dt = null;
                    StringBuilder sb = new StringBuilder();

                    MessageBox.Show("삭제되었습니다");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("시스템 오류" + e.ToString());
                }
            }
            else
            {
                MessageBox.Show("취소하셨습니다");
            }

            resetSetting();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCustSrch2_Click(object sender, EventArgs e)
        {            
            try
            {
                wnDm wDm = new wnDm();

                DataTable dt = null;

                StringBuilder sb = new StringBuilder();                       

                //string condition = " WHERE '" + noticeGrid.Rows[e.RowIndex].Cells["TITLE"].Value.ToString() + "' like '%" + textBox6 + "%' ";

                if (!txtSrch.Text.ToString().Equals(""))
                {
                    sb.AppendLine(" where TITLE LIKE '%" + txtSrch.Text.ToString() + "%' ");
                }
                else 
                {
                    sb.AppendLine("");
                }
                dt = wDm.noticeList(sb.ToString());

                
                //sb.AppendLine("where A.ORDER_DATE >= '" + start_date.Text.ToString() + "' and  A.ORDER_DATE <= '" + end_date.Text.ToString() + "' ");

                this.noticeGrid.RowCount = dt.Rows.Count;
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        noticeGrid.Rows[i].Cells["SEQ"].Value = dt.Rows[i]["SEQ"].ToString();
                        noticeGrid.Rows[i].Cells["TITLE"].Value = dt.Rows[i]["TITLE"].ToString();
                        noticeGrid.Rows[i].Cells["CONTENT"].Value = dt.Rows[i]["CONTENT"].ToString();
                        noticeGrid.Rows[i].Cells["INTIME"].Value = dt.Rows[i]["INTIME"].ToString();
                        noticeGrid.Rows[i].Cells["INSTAFF"].Value = dt.Rows[i]["INSTAFF"].ToString();

                    }
                }
                else
                {
                    noticeGrid.Rows.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("시스템 오류" + e.ToString());
            }
        }

        private void resetSetting()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            dTP1.Text = DateTime.Today.ToString("yyyy-MM-dd");
            textBox5.Text = "";
            txtSrch.Text = "";
            lbl.Text = null;

            noticeGrid.Refresh();

            btnDelete.Enabled = false;

        }

        private void frm공지사항_Load(object sender, EventArgs e)
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;

                StringBuilder sb = new StringBuilder();
                dt = wDm.noticeList(sb.ToString());
                //sb.AppendLine("where A.ORDER_DATE >= '" + start_date.Text.ToString() + "' and  A.ORDER_DATE <= '" + end_date.Text.ToString() + "' ");

                this.noticeGrid.RowCount = dt.Rows.Count;
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //noticeGrid.Rows.Add();
                        noticeGrid.Rows[i].Cells["SEQ"].Value = dt.Rows[i]["SEQ"].ToString();
                        noticeGrid.Rows[i].Cells["TITLE"].Value = dt.Rows[i]["TITLE"].ToString();
                        noticeGrid.Rows[i].Cells["CONTENT"].Value = dt.Rows[i]["CONTENT"].ToString();
                        noticeGrid.Rows[i].Cells["INTIME"].Value = dt.Rows[i]["INTIME"].ToString();
                        noticeGrid.Rows[i].Cells["INSTAFF"].Value = dt.Rows[i]["INSTAFF"].ToString();
                    }
                }
                else
                {
                    noticeGrid.Rows.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("시스템 오류" + e.ToString());
            }
        }
        private void notice_List(string condition)
        {
            try
            {
                wnDm wDm = new wnDm();

                DataTable dt = null;

                StringBuilder sb = new StringBuilder();
                dt = wDm.noticeList(condition);

                sb.AppendLine(" WHERE @TITLE like '%" + txtSrch + "%' ");
                //sb.AppendLine("where A.ORDER_DATE >= '" + start_date.Text.ToString() + "' and  A.ORDER_DATE <= '" + end_date.Text.ToString() + "' ");

                this.noticeGrid.RowCount = dt.Rows.Count;
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        noticeGrid.Rows[i].Cells["SEQ"].Value = dt.Rows[i]["SEQ"].ToString();
                        noticeGrid.Rows[i].Cells["TITLE"].Value = dt.Rows[i]["TITLE"].ToString();
                        noticeGrid.Rows[i].Cells["CONTENT"].Value = dt.Rows[i]["CONTENT"].ToString();
                        noticeGrid.Rows[i].Cells["INTIME"].Value = dt.Rows[i]["INTIME"].ToString();
                        noticeGrid.Rows[i].Cells["INSTAFF"].Value = dt.Rows[i]["INSTAFF"].ToString();

                    }
                }
                else
                {
                    noticeGrid.Rows.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("시스템 오류" + ex.ToString());
            }
        }


        private void notice_logic()
        {
            try
            {                
                if (textBox2.Text.ToString().Equals(""))
                {
                    MessageBox.Show("제목을 입력하시기 바랍니다.");
                    return;
                }
                if (textBox3.Text.ToString().Equals(""))
                {
                    MessageBox.Show("내용을 입력하시기 바랍니다.");
                    return;            
                }            
                                
               // string print_yn = comInfo.resultYn(chk_print_yn);
                if (lbl.Text != "")
                {
                    wnDm wDm = new wnDm();
                    int rsNum = wDm.updateNotice(

                                    textBox1.Text.ToString()
                                    ,textBox2.Text.ToString()
                                    ,textBox3.Text.ToString()
                                   
                                    );

                    if (rsNum == 0)
                    {
                        noticeGrid.Rows.Clear(); //기존 삭제 데이터할 제품구성 리스트 초기화

                        //item_list();
                        //gridDetail("where A.item_cd = '" + txt_item_cd.Text.ToString() + "'");
                        MessageBox.Show("성공적으로 수정하였습니다.");
                    }
                    else if (rsNum == 1)
                        MessageBox.Show("저장에 실패하였습니다");
                    else
                        MessageBox.Show("Exception 에러");                   
                }
                else
                {                  

                    wnDm wDm = new wnDm();

                    int rsNum = wDm.insertNotice(                                    
                                     textBox2.Text.ToString()
                                    ,textBox3.Text.ToString()
                                    

                                    //, itemRawGrid
                        //, itemFlowGrid
                        //, itemHalfGrid
                                    );

                    if (rsNum == 0)
                    {
                        resetSetting();
                        //item_list();
                        MessageBox.Show("성공적으로 등록하였습니다.");
                    }
                    else if (rsNum == 1)
                        MessageBox.Show("저장에 실패하였습니다");
                    else if (rsNum == 2)
                        MessageBox.Show("SQL COMMAND 에러");
                    else if (rsNum == 3)
                        MessageBox.Show("기존 코드가 있으니 \n 다른 코드로 입력 바랍니다.");
                    else
                        MessageBox.Show("Exception 에러");
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("시스템 에러: " + e.Message.ToString());
            }
        }

        private void noticeGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = noticeGrid.Rows[e.RowIndex].Cells["SEQ"].Value.ToString();
            textBox2.Text = noticeGrid.Rows[e.RowIndex].Cells["TITLE"].Value.ToString();
            textBox3.Text = noticeGrid.Rows[e.RowIndex].Cells["CONTENT"].Value.ToString();
            textBox5.Text = noticeGrid.Rows[e.RowIndex].Cells["INSTAFF"].Value.ToString();
            //textBox1.Text = noticeGrid.Rows[e.RowIndex].Cells["SEQ"].Value.ToString();
            lbl.Text = noticeGrid.Rows[e.RowIndex].Cells["SEQ"].Value.ToString();

            btnDelete.Enabled = true;
        }

        
    }
}
