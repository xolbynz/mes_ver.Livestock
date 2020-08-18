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
using 스마트팩토리.Controls;

namespace 스마트팩토리.P30_SCH
{
    public partial class frm일일작업결과 : Form
    {
        private wnGConstant wConst = new wnGConstant();
        private DataGridView del_inputGrid = new DataGridView();
        private DateTimePicker dtp = new DateTimePicker();
        private Rectangle _Retangle;

        private string old_cust_nm = "";
        private bool bHeadCheck = false;
        private ComInfo comInfo = new ComInfo();
        private DataGridView copied_dgv = new DataGridView();

        Popup.frmPrint readyPrt = new Popup.frmPrint();

        DataTable adoPrt1 = null;
        DataTable adoPrt2 = null;
        wnAdo wAdo = new wnAdo();
        public Popup.frmPrint frmPrt;



        private bool isUserInput = true;

        private bool first_touch = false;

        private string currunt_column_temp = "";

        string strCondition;

        

        public frm일일작업결과()
        {
            InitializeComponent();
        }

        private void frm일일작업결과_Load(object sender, EventArgs e)
        {
            ComInfo.gridHeaderSet(MadeGrid);
            ComInfo.gridHeaderSet(ToipGrid);
        }


        public void Srch_by_LotNo(string LotNo)
        {
            try
            {
                // 생산 & 지시정보 + 지시 원료육
                wnDm wDm = new wnDm();
                DataTable dt = null;
                DataTable dt2 = null;

                dt = wDm.fn_WorkDay_toip_list(LotNo);
                adoPrt1 = new DataTable();
                adoPrt1 = dt.Copy();
                dt2 = wDm.fn_WorkDay_Made_list(LotNo);
                adoPrt2 = new DataTable();
                adoPrt2 = dt2.Copy();
                if (dt != null && dt2 != null && dt.Rows.Count > 0 && dt2.Rows.Count > 0)
                {

                    ToipGrid.Rows.Clear();
                    MadeGrid.Rows.Clear();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ToipGrid.Rows.Add();
                        ToipGrid.Rows[i].Cells["TO_LABEL_NM"].Value = dt.Rows[i]["LABEL_NM"].ToString();
                        ToipGrid.Rows[i].Cells["TO_UNIT_NM"].Value = dt.Rows[i]["UNIT_NM"].ToString();
                        ToipGrid.Rows[i].Cells["TO_COUNTRY_NM"].Value = dt.Rows[i]["COUNTRY_NM"].ToString();
                        ToipGrid.Rows[i].Cells["TO_CUST_NM"].Value = dt.Rows[i]["CUST_NM"].ToString();
                        ToipGrid.Rows[i].Cells["TO_TOTAL_AMT"].Value = decimal.Parse(dt.Rows[i]["TOTAL_AMT"].ToString()).ToString("#,0.######");
                        ToipGrid.Rows[i].Cells["TO_LOT_NO"].Value = dt.Rows[i]["LOT_NO"].ToString();
                    }

                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        MadeGrid.Rows.Add();
                        MadeGrid.Rows[i].Cells["LABEL_NM"].Value = dt2.Rows[i]["LABEL_NM"].ToString();
                        MadeGrid.Rows[i].Cells["UNIT_NM"].Value = dt2.Rows[i]["UNIT_NM"].ToString();
                        MadeGrid.Rows[i].Cells["SPEC"].Value = dt2.Rows[i]["SPEC"].ToString();
                        MadeGrid.Rows[i].Cells["TOTAL_AMT"].Value = decimal.Parse(dt2.Rows[i]["TOTAL_AMT"].ToString()).ToString("#,0.######");
                        MadeGrid.Rows[i].Cells["LOT_NO"].Value = dt2.Rows[i]["LOT_NO"].ToString();
                        MadeGrid.Rows[i].Cells["FROZEN_GUBUN"].Value = dt2.Rows[i]["FROZEN_GUBUN"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("해당 일자에 생산 내역이 없습니다.");
                    return;
                }


                for (int i = 0; i < ToipGrid.RowCount; i++)
                {
                    adoPrt1.Rows[i]["TOTAL_AMT"] = ToipGrid.Rows[i].Cells["TO_TOTAL_AMT"].Value.ToString();
                }
                for (int i = 0; i < MadeGrid.RowCount; i++)
                {
                    adoPrt2.Rows[i]["TOTAL_AMT"] = MadeGrid.Rows[i].Cells["TOTAL_AMT"].Value.ToString();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("검색중 오류가 발생했습니다");
                Console.WriteLine(ex);
                return;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSrch_Click(object sender, EventArgs e)
        {
            Srch_by_LotNo("L" + txt_input_date.Text.ToString().Replace("-","").Substring(2));
            btn출력.Enabled = true;
        }

        private void btn출력_Click(object sender, EventArgs e)
        {
            btn출력.Enabled = false;

            strCondition = "";
            if (ToipGrid.Rows.Count == 0 && MadeGrid.Rows.Count ==0)
            {
                MessageBox.Show("출력할 자료가 없습니다.");
                btn출력.Enabled = true;
                return;
            }
            strCondition = "일일작업결과";
            frmPrt = readyPrt;
            frmPrt.Show();
            frmPrt.BringToFront();
            frmPrt.prt_일일작업결과(adoPrt1,adoPrt2, strCondition);
            btn출력.Enabled = true;
        }

        
       





    }
}
