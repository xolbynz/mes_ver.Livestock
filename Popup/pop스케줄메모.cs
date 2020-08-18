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
    public partial class pop스케줄메모 : Form
    {
        string date;
        string title;
        string contents;
        public bool check;
        public pop스케줄메모(string date)
        {
            this.date = date;
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }

        private void pop스케쥴메모_Load(object sender, EventArgs e)
        {
            lbl_Date.Text = date;
            getschedule();
        }

        private void getschedule()
        {
            try
            {
                txt_Title.Clear();
                txt_Contents.Clear();

                // 생산 & 지시정보 + 지시 원료육
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.getSchedule(date);

                if (dt != null && dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        txt_Title.Text = dt.Rows[i]["TITLE"].ToString();
                        txt_Contents.Text = dt.Rows[i]["CONTENTS"].ToString();
                    }
                }
                else
                {
                    lbl_Nothing.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("검색중 오류가 있습니다");
            }
        }

        private void btm_Exit_Click(object sender, EventArgs e)
        {

            wnDm wdm = new wnDm();
            if (!txt_Title.Text.ToString().Equals("") && !txt_Contents.Text.ToString().Equals(""))
            {
                title = txt_Title.Text.ToString();
                contents = txt_Contents.Text.ToString();
                int rsNum = wdm.insert_Schedule(date.ToString(), title, contents);
                if (rsNum == 0)
                {
                    this.Close();
                }
            }
            else if(!txt_Title.Text.ToString().Equals("") && txt_Contents.Text.ToString().Equals(""))
            {
                 title = txt_Title.Text.ToString();
                contents = txt_Contents.Text.ToString();
                int rsNum = wdm.insert_Schedule(date.ToString(), title, contents);
                if (rsNum == 0)
                {
                    this.Close();
                }
            }
            else if (txt_Title.Text.ToString().Equals("") && !txt_Contents.Text.ToString().Equals(""))
            {
                MessageBox.Show("TITLE를 적어주세요");
                return;
            }
            else
            {
                wdm.deleteSchdule(date);
                this.Close();
            }
        }

        private void txt_Title_Click(object sender, EventArgs e)
        {
            lbl_Nothing.Visible = false;
        }

        private void txt_Contents_Click(object sender, EventArgs e)
        {
            lbl_Nothing.Visible = false;
        }
    }
}
