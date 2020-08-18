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

namespace 스마트팩토리.P60_FAC
{
    public partial class frm설비카드등록 : Form
    {
        private wnGConstant wConst = new wnGConstant();
        private ComInfo comInfo = new ComInfo();

        public frm설비카드등록()
        {
            InitializeComponent();
            init_ComboBox();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void init_ComboBox()
        {
            ComInfo comInfo = new ComInfo();
            string sqlQuery = "";

            cmb_mainten.ValueMember = "코드";
            cmb_mainten.DisplayMember = "명칭";
            sqlQuery = comInfo.queryCode("800");
            wConst.ComboBox_Read_Blank(cmb_mainten, sqlQuery);

            cmb_dept.ValueMember = "코드";
            cmb_dept.DisplayMember = "명칭";
            sqlQuery = comInfo.queryDept();
            wConst.ComboBox_Read_Blank(cmb_dept, sqlQuery);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Popup.pop설비검색 frm = new Popup.pop설비검색();

            //frm.sUsedYN = sUsedYN;
            frm.ShowDialog();

            if (frm.dt.Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = frm.dt;
                btnDelete.Enabled = true;
                lbl_fac_gbn.Text = "1";
                txt_fac_cd.Enabled = false;
                txt_fac_cd.Text = dt.Rows[0]["FAC_CD"].ToString();
                txt_fac_nm.Text = dt.Rows[0]["FAC_NM"].ToString();
                txt_model_nm.Text = dt.Rows[0]["MODEL_NM"].ToString();
                txt_spec.Text = dt.Rows[0]["SPEC"].ToString();
                txt_manu_comp.Text = dt.Rows[0]["MANU_COMPANY"].ToString();
                acq_date.Text = dt.Rows[0]["ACQ_DATE"].ToString();
                txt_acq_price.Text = dt.Rows[0]["ACQ_PRICE"].ToString();
                cmb_dept.SelectedValue = dt.Rows[0]["DEPT_CD"].ToString();
                txt_used.Text = dt.Rows[0]["USED"].ToString();
                txt_pro_capa.Text = dt.Rows[0]["PRO_CAPA"].ToString();
                txt_power_num.Text = dt.Rows[0]["POWER_NUMBER"].ToString();
                cmb_mainten.SelectedValue = dt.Rows[0]["MAINTEN_CLASS"].ToString();
            }
            else 
            {

            }

            frm.Dispose();
            frm = null;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            resetSetting();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            fac_logic();
        }

        #region fac logic

        private void fac_logic() {
            try
            {
                if (txt_fac_cd.Text.ToString().Equals(""))
                {
                    MessageBox.Show("설비코드를 입력하시기 바랍니다.");
                    return;
                }

                if (lbl_fac_gbn.Text != "1")
                {
                    wnDm wDm = new wnDm();
                    int rsNum = wDm.insertFac(
                                      txt_fac_cd.Text.ToString()
                                    , txt_fac_nm.Text.ToString()
                                    , txt_model_nm.Text.ToString()
                                    , txt_spec.Text.ToString()
                                    , txt_manu_comp.Text.ToString()
                                    , acq_date.Text.ToString()
                                    , txt_acq_price.Text.ToString()
                                    , cmb_dept.SelectedValue.ToString()
                                    , txt_used.Text.ToString()
                                    , txt_pro_capa.Text.ToString()
                                    , txt_power_num.Text.ToString()
                                    , cmb_mainten.SelectedValue.ToString());
                    if (rsNum == 0)
                    {
                        resetSetting();
                        MessageBox.Show("성공적으로 등록하였습니다.");
                    }
                    else if (rsNum == 1)
                        MessageBox.Show("저장에 실패하였습니다");
                    else if (rsNum == 2)
                        MessageBox.Show("SQL COMMAND 에러");
                    else if (rsNum == 3)
                        MessageBox.Show("기존 코드가 있으니 \n 다른 코드로 입력 바랍니다.");
                    else
                        MessageBox.Show("Exception 에러1");
                }
                else 
                {
                    wnDm wDm = new wnDm();
                    int rsNum = wDm.updateFac(
                                      txt_fac_cd.Text.ToString()
                                    , txt_fac_nm.Text.ToString()
                                    , txt_model_nm.Text.ToString()
                                    , txt_spec.Text.ToString()
                                    , txt_manu_comp.Text.ToString()
                                    , acq_date.Text.ToString()
                                    , txt_acq_price.Text.ToString()
                                    , cmb_dept.SelectedValue.ToString()
                                    , txt_used.Text.ToString()
                                    , txt_pro_capa.Text.ToString()
                                    , txt_power_num.Text.ToString()
                                    , cmb_mainten.SelectedValue.ToString());

                    if (rsNum == 0)
                    {
                        MessageBox.Show("성공적으로 수정하였습니다.");
                    }
                    else if (rsNum == 1)
                        MessageBox.Show("저장에 실패하였습니다");
                    else
                        MessageBox.Show("Exception 에러");
                }
            }
            catch (Exception e) {
                 MessageBox.Show(e.ToString());
            }
        }

        private void resetSetting()
        {
            lbl_fac_gbn.Text = "";
            btnDelete.Enabled = false;
            txt_fac_cd.Text = "";
            txt_fac_cd.Enabled = true;
            txt_fac_nm.Text = "";
            txt_spec.Text = "";
            txt_model_nm.Text = "";
            txt_manu_comp.Text = "";
            acq_date.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txt_acq_price.Text = "0";
            cmb_dept.SelectedIndex = 0;
            txt_used.Text = "";
            txt_pro_capa.Text = "0";
            txt_power_num.Text = "0";
            cmb_mainten.SelectedIndex = 0;
        }

        #endregion fac logic

        private void txt_power_num_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComInfo.onlyNum(sender, e); 
        }

        private void txt_pro_capa_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComInfo.onlyNum(sender, e); 
        }

        private void txt_acq_price_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComInfo.onlyNum(sender, e); 
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            wnDm wDm = new wnDm();
            int rsNum = wDm.deleteFac(txt_fac_cd.Text.ToString());
            if (rsNum == 0)
            {
                resetSetting();

                MessageBox.Show("성공적으로 삭제하였습니다.");
            }
            else if (rsNum == 1)
            {
                MessageBox.Show("삭제에 실패하였습니다.");
            }
        }
    }
}
