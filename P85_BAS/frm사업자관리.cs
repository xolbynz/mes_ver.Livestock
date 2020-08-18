using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 스마트팩토리.CLS;


namespace 스마트팩토리.P85_BAS
{
    public partial class frm사업자관리 : Form
    {
        Image image;
        string path;

        string saup_no = "";
        public frm사업자관리()
        {
            InitializeComponent();
        }

        private void frm사업자관리_Load(object sender, EventArgs e)
        {
            saup_no = Common.p_saupNo;
            txt_saup_no.Text = saup_no.Substring(0, 3) + "-" + saup_no.Substring(3,2)+"-"+saup_no.Substring(5,5);
            saupSetting();
        }

        #region button logic

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txt_saup_nm.Text.ToString().Equals(""))
            {
                MessageBox.Show("사업자명을 입력하시기 바랍니다.");
            }

            byte[] saup_img;
            int saup_img_size = 0;
            if (path != null && path != "")
            {
                saup_img = ComInfo.GetImage(path);
                saup_img_size = saup_img.Length;
            }
            else
            {
                saup_img = null;
                saup_img_size = 0;
            }

            wnDm wDm = new wnDm();
            int rsNum = wDm.updateSaup(
                        saup_no,
                        txt_saup_nm.Text.ToString(),
                        txt_corporate.Text.ToString(),
                        txt_uptae.Text.ToString(),
                        txt_jongmok.Text.ToString(),
                        txt_open_date.Text.ToString(),
                        txt_post_no.Text.ToString(),
                        txt_addr.Text.ToString(),
                        txt_addr2.Text.ToString(),
                        txt_comp_phone.Text.ToString(),
                        txt_fax.Text.ToString(),
                        txt_mg_email.Text.ToString(),
                        txt_mg_phone.Text.ToString(),
                        txt_homepage.Text.ToString(),
                        saup_img,
                        saup_img_size,
                        Common.p_strCompNm
                        );

            if (rsNum == 0)
            {
                MessageBox.Show("성공적으로 등록하였습니다.");
            }
            else if (rsNum == 1)
            {
                MessageBox.Show("저장에 실패하였습니다.");
            }
            else 
            {
                MessageBox.Show("Exception 에러 ");
            }
        }

        private void btn_file_up_Click(object sender, EventArgs e)
        {
            pic_logic(pic_exam);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion button logic

        #region common logic
        private void saupSetting() 
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = new DataTable();
                dt = wDm.fn_Saup_List(saup_no);

                if (dt != null && dt.Rows.Count > 0)
                {
                    txt_saup_nm.Text = dt.Rows[0]["COMPANY_NM"].ToString();
                    txt_corporate.Text = dt.Rows[0]["CORPORATE_NO"].ToString();
                    txt_uptae.Text = dt.Rows[0]["UPTAE"].ToString();
                    txt_jongmok.Text = dt.Rows[0]["JONGMOK"].ToString();
                    txt_post_no.Text = dt.Rows[0]["POST_NO"].ToString();
                    txt_addr.Text = dt.Rows[0]["ADDR"].ToString();
                    txt_addr2.Text = dt.Rows[0]["ADDR2"].ToString();
                    txt_open_date.Text = dt.Rows[0]["OPEN_DATE"].ToString();
                    txt_comp_phone.Text = dt.Rows[0]["COMP_PHONE"].ToString();
                    txt_fax.Text = dt.Rows[0]["FAX"].ToString();
                    txt_mg_email.Text = dt.Rows[0]["MANAGER_EMAIL"].ToString();
                    txt_mg_phone.Text = dt.Rows[0]["MANAGER_PHONE"].ToString();
                    txt_homepage.Text = dt.Rows[0]["HOMEPAGE"].ToString();

                    if (int.Parse(dt.Rows[0]["LOGO_SIZE"].ToString()) > 0)
                    {
                        byte[] rs = (byte[])dt.Rows[0]["SAUP_LOGO"];
                        MemoryStream ms = new MemoryStream(rs);
                        Image img = Image.FromStream(ms);

                        Image cus_img = ComInfo.pic_resize_logic(pic_exam, img);

                        pic_exam.BackgroundImage = cus_img;
                    }
                }
                else 
                {
                    txt_saup_nm.Text = Common.p_strCompNm;
                }
            }
            catch (Exception e) 
            {

            }
        }

        private void pic_logic(PictureBox pic)
        {
            ofd.Filter = "*.png|*.jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //이미지 
                image = Image.FromFile(ofd.FileName);
                //이미지 경로 
                path = ofd.FileName;

                /* 이미지 리사이즈 */
                Image cus_img = ComInfo.pic_resize_logic(pic, image);
                //픽쳐박스에 이미지를 띄운다
                pic.BackgroundImage = cus_img;
            }
        }

        #endregion common logic
    }
}
