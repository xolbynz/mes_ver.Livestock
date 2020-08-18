using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 스마트팩토리.CLS;

namespace 스마트팩토리.P30_SCH
{
    public partial class uc원장조회 : UserControl
    {
        public Popup.frmPrint frmPrt;

        public string strDay1 = "";
        public string strDay2 = "";
        public string strCust = "";
        public string strCondition = "";
        DataTable adoPrt = null;

        public uc원장조회()
        {
            InitializeComponent();

            this.Width = GridRecord.Left + GridRecord.Width + 3;
            this.Height = GridRecord.Top + GridRecord.Height + 6;
            GridRecord.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            lblCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        }

        private void uc원장조회_Load(object sender, EventArgs e)
        {
            lblCount.Text = "0 건";

        }

        public void bindData()
        {
            lblSearch.BringToFront();
            lblSearch.Left = this.ClientSize.Width / 2 - lblSearch.Width / 2;
            lblSearch.Top = this.ClientSize.Height / 2 - lblSearch.Height / 2;
            lblSearch.Visible = true;
            Application.DoEvents();

            this.GridRecord.DataSource = null;
            this.GridRecord.RowCount = 0;
            lblCount.Text = "0 건";

            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_PRODUCT_STOCK_List(strDay1, strDay2, strCust);

                adoPrt = new DataTable();
                adoPrt = dt.Copy();

                if (dt != null && dt.Rows.Count > 0)
                {
                    //// 합계 레코드만 있을시 skip.
                    //if (dt.Rows[0]["제조사명"].ToString() == "=== 합계 ===")
                    //{
                    //    return;
                    //}

                    this.GridRecord.RowCount = dt.Rows.Count;
                    this.lblCount.Text = dt.Rows.Count.ToString("#,0") + " 건";

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.GridRecord.Rows[i].Cells[0].Value = (i + 1).ToString();
                        this.GridRecord.Rows[i].Cells[1].Value = dt.Rows[i]["STOCK_KIND"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[2].Value = dt.Rows[i]["STOCK_DATE"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[3].Value = dt.Rows[i]["CUST_NAME1"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[4].Value = dt.Rows[i]["STOCK_CODE"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[5].Value = dt.Rows[i]["PRODUCT_NAME"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[6].Value = dt.Rows[i]["PRODUCT_SPEC1"].ToString().Trim();
                        this.GridRecord.Rows[i].Cells[7].Value = decimal.Parse(dt.Rows[i]["STOCK_QTY"].ToString().Trim()).ToString("#,0");
                        this.GridRecord.Rows[i].Cells[8].Value = decimal.Parse(dt.Rows[i]["STOCK_PRICE"].ToString().Trim()).ToString("#,0");
                        this.GridRecord.Rows[i].Cells[9].Value = decimal.Parse(dt.Rows[i]["STOCK_AMOUNT"].ToString().Trim()).ToString("#,0");
                        this.GridRecord.Rows[i].Cells[10].Value = decimal.Parse(dt.Rows[i]["STOCK_VAT"].ToString().Trim()).ToString("#,0");
                    }

                    ////wConst.mergeCells(GridRecord, 1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("검색중에 오류가 발생했습니다.");
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
            }

            lblSearch.Visible = false;
        }

        public void call_Print()
        {
            if (GridRecord.Rows.Count == 0)
            {
                MessageBox.Show("출력할 자료가 없습니다.");
                return;
            }

            frmPrt.Show();
            frmPrt.BringToFront();
            frmPrt.prt_원장조회(adoPrt, strDay1, strDay2, strCust, strCondition);
        }

    }
}
