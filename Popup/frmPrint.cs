using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 스마트팩토리.CLS;

namespace 스마트팩토리.Popup
{
    public partial class frmPrint : Form
    {
        public frmPrint()
        {
            InitializeComponent();
        }

        private void frmPrint_Load(object sender, EventArgs e)
        {
            
        }

        private void frmPrint_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            return;
        }

        private void frmPrint_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                reportViewer1.PrintDialog();
            }
            if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                this.Hide();
            }
        }

        public void prt_원장조회(DataTable dt, string sDay1, string sDay2, string sCust, string sCondition)
        {
            lblMsg.Visible = true;
            Application.DoEvents();

            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    ////for (int kk = 0; kk < dt.Rows.Count; kk++)
                    ////{
                    ////    //if (dt매출매출항목.Rows[kk]["규격"].ToString().Length > 10)
                    ////    //{
                    ////    //    dt매출매출항목.Rows[kk]["규격"] = dt매출매출항목.Rows[kk]["규격"].ToString().Substring(0, 9) + "...";
                    ////    //}
                    ////}

                    this.reportViewer1.Reset();
                    this.reportViewer1.LocalReport.DisplayName = "원장조회" + "_" + sDay1 + "_" + sDay2;
                    this.reportViewer1.LocalReport.ReportPath = "Reports\\rpt원장조회.rdlc";
                    
                    ReportDataSource ds = new ReportDataSource("DataSet1", dt);
                    this.reportViewer1.LocalReport.DataSources.Add(ds);

                    ReportParameterCollection rptParams = new ReportParameterCollection();
                    rptParams.Add(new ReportParameter("p제목", "원장 조회"));
                    rptParams.Add(new ReportParameter("p검색조건", sCondition));
                    
                    this.reportViewer1.LocalReport.SetParameters(rptParams);

                    ReportPageSettings rpg = reportViewer1.LocalReport.GetDefaultPageSettings();
                    System.Drawing.Printing.PageSettings pg = new System.Drawing.Printing.PageSettings();
                    pg.PaperSize = rpg.PaperSize;
                    pg.Margins = rpg.Margins;
                    pg.Landscape = false; // false = 세로, true = 가로

                    reportViewer1.SetPageSettings(pg);

                    this.reportViewer1.RefreshReport();
                    this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                    this.reportViewer1.ZoomMode = ZoomMode.PageWidth;
                }
            }
            catch (Exception ex)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
                MessageBox.Show("시스템에 문제가 있습니다.");
                this.Hide();
            }

            lblMsg.Visible = false;
        }
        public void prt_공정이동표(DataTable dt, string sDate, string sNum, string sCondition)
        {
            lblMsg.Visible = true;
            Application.DoEvents();

            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (sCondition == "공정이동표")
                    {
                        this.reportViewer1.Reset();
                        this.reportViewer1.LocalReport.DisplayName = "공정이동표" + "_" + sDate + "_" + sNum;
                        this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\rpt공정이동전표.rdlc";

                        ReportDataSource ds = new ReportDataSource("DataSet1", dt);
                        this.reportViewer1.LocalReport.DataSources.Add(ds);

                        ReportParameterCollection rptParams = new ReportParameterCollection();
                        rptParams.Add(new ReportParameter("p제목", "공정이동표"));
                        //rptParams.Add(new ReportParameter("p견적구분", "아래와 같이 견적합니다."));
                        //rptParams.Add(new ReportParameter("p검색조건", "일자 : " + sDate + "     번호 : " + sNum));

                        this.reportViewer1.LocalReport.SetParameters(rptParams);

                    }
                    else
                    {
                        this.reportViewer1.Reset();
                        this.reportViewer1.LocalReport.DisplayName = "자재요청서" + "_" + sDate + "_" + sNum;
                        this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\rpt원료청구.rdlc";

                        ReportDataSource ds = new ReportDataSource("DataSet1", dt);
                        this.reportViewer1.LocalReport.DataSources.Add(ds);

                        ReportParameterCollection rptParams = new ReportParameterCollection();
                        rptParams.Add(new ReportParameter("p제목", "자재요청서"));
                        //rptParams.Add(new ReportParameter("p견적구분", "아래와 같이 납품합니다."));
                        //rptParams.Add(new ReportParameter("p검색조건", "일자 : " + sDate + "     번호 : " + sNum));

                        this.reportViewer1.LocalReport.SetParameters(rptParams);
                    }

                    ReportPageSettings rpg = reportViewer1.LocalReport.GetDefaultPageSettings();
                    System.Drawing.Printing.PageSettings pg = new System.Drawing.Printing.PageSettings();
                    pg.PaperSize = rpg.PaperSize;
                    pg.Margins = rpg.Margins;
                    pg.Landscape = false; // false = 세로, true = 가로

                    reportViewer1.SetPageSettings(pg);

                    this.reportViewer1.RefreshReport();
                    this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                    this.reportViewer1.ZoomMode = ZoomMode.PageWidth;

                }
            }
            catch (Exception ex)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
                MessageBox.Show("시스템에 문제가 있습니다.");
                this.Hide();
            }

            lblMsg.Visible = false;
        }

        public void prt_발주현황표(DataTable dt,DataTable dt2, string sDate, string sNum, string sCondition)
        {
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    this.reportViewer1.Reset();
                    this.reportViewer1.LocalReport.DisplayName = "발주서현황" + "_" + sDate + "_" + sNum;
                    this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\rpt발주서현황.rdlc";

                    ReportDataSource ds = new ReportDataSource("DataSet1", dt);
                    this.reportViewer1.LocalReport.DataSources.Add(ds);
                    ds = new ReportDataSource("DataSet2", dt2);
                    this.reportViewer1.LocalReport.DataSources.Add(ds);


                    ReportParameterCollection rptParams = new ReportParameterCollection();
                    //rptParams.Add(new ReportParameter("p제목", "발주현황표"));

                    this.reportViewer1.LocalReport.SetParameters(rptParams);
                }

                ReportPageSettings rpg = reportViewer1.LocalReport.GetDefaultPageSettings();
                System.Drawing.Printing.PageSettings pg = new System.Drawing.Printing.PageSettings();
                pg.PaperSize = rpg.PaperSize;
                pg.Margins = rpg.Margins;
                pg.Landscape = false; // false = 세로, true = 가로

                //var setup = reportViewer1.GetPageSettings();
                //setup.PaperSize.Width = 900;
                //setup.PaperSize.Height = 600;
                //setup.Landscape = false;
                //setup.Margins.Left = 27;

                reportViewer1.SetPageSettings(pg);//pg

                this.reportViewer1.RefreshReport();
                this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                this.reportViewer1.ZoomMode = ZoomMode.PageWidth;

            }
            catch (Exception e) 
            {
                wnLog.writeLog(wnLog.LOG_ERROR, e.Message + " - " + e.ToString());
                MessageBox.Show("시스템에 문제가 있습니다.");
                this.Hide();
            }
        }

        public void prt_식별표(DataTable dt, string sCondition)
        {
            lblMsg.Visible = true;
            Application.DoEvents();

            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (sCondition == "원자재입고식별표")
                    {
                        this.reportViewer1.Reset();
                        this.reportViewer1.LocalReport.DisplayName = "원자재입고식별표";
                        this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\rpt자재식별표.rdlc";

                        ReportDataSource ds = new ReportDataSource("DataSet1", dt);
                        this.reportViewer1.LocalReport.DataSources.Add(ds);

                        ReportParameterCollection rptParams = new ReportParameterCollection();
                        rptParams.Add(new ReportParameter("p제목", "원자재입고식별표"));
                        //rptParams.Add(new ReportParameter("p견적구분", "아래와 같이 견적합니다."));
                        //rptParams.Add(new ReportParameter("p검색조건", "일자 : " + sDate + "     번호 : " + sNum));

                        this.reportViewer1.LocalReport.SetParameters(rptParams);

                        var setup = reportViewer1.GetPageSettings();
                        setup.PaperSize.Width = 480;
                        setup.PaperSize.Height = 250;
                        setup.Landscape = false;
                        setup.Margins.Top = 5;
                        setup.Margins.Left = 27;

                        reportViewer1.SetPageSettings(setup);

                    }
                    else if (sCondition == "제품입고식별표")
                    {
                        this.reportViewer1.Reset();
                        this.reportViewer1.LocalReport.DisplayName = "제품식별표";
                        this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\rpt제품식별표.rdlc";

                        ReportDataSource ds = new ReportDataSource("DataSet1", dt);
                        this.reportViewer1.LocalReport.DataSources.Add(ds);

                        ReportParameterCollection rptParams = new ReportParameterCollection();
                        rptParams.Add(new ReportParameter("p제목", "제품식별표"));
                        //rptParams.Add(new ReportParameter("p견적구분", "아래와 같이 납품합니다."));
                        //rptParams.Add(new ReportParameter("p검색조건", "일자 : " + sDate + "     번호 : " + sNum));

                        this.reportViewer1.LocalReport.SetParameters(rptParams);

                        var setup = reportViewer1.GetPageSettings();
                        setup.PaperSize.Width = 480;
                        setup.PaperSize.Height = 250;
                        setup.Landscape = false;
                        setup.Margins.Top = 5;
                        setup.Margins.Left = 27;

                        reportViewer1.SetPageSettings(setup);
                    }
                    else
                    {
                        this.reportViewer1.Reset();
                        this.reportViewer1.LocalReport.DisplayName = "제품식별표2";
                        this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\rpt제품식별표2.rdlc";

                        ReportDataSource ds = new ReportDataSource("DataSet1", dt);
                        this.reportViewer1.LocalReport.DataSources.Add(ds);


                        var setup = reportViewer1.GetPageSettings();
                        //setup.PaperSize.Width = 400;
                        ////setup.PaperSize.Height = 228;
                        //setup.PaperSize.Height = 200;
                        //setup.Landscape = false;
                        //setup.Margins.Top = 5;
                        //setup.Margins.Left = 27;


                        

                        reportViewer1.SetPageSettings(setup);
                    }
                    //--------------------------------------------
                    //ReportPageSettings rpg = reportViewer1.LocalReport.GetDefaultPageSettings();
                    //System.Drawing.Printing.PageSettings pg = new System.Drawing.Printing.PageSettings();
                    //pg.PaperSize = rpg.PaperSize;

                    //var setup = reportViewer1.GetPageSettings();
                    //setup.PaperSize.Width = 500;
                    //setup.PaperSize.Height = 228;
                    //setup.Landscape = false;
                    //setup.Margins.Left = 27;

                    ////pg.Margins = rpg.Margins;
                    ////pg.Landscape = false; // false = 세로, true = 가로

                    //reportViewer1.SetPageSettings(setup);

                    this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                    this.reportViewer1.ZoomMode = ZoomMode.PageWidth;
                    this.reportViewer1.RefreshReport();

                    //--------------------------------------------

                }
            }
            catch (Exception ex)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
                MessageBox.Show("시스템에 문제가 있습니다.");
                this.Hide();
            }

            lblMsg.Visible = false;
        }

        public void prt_바코드(DataTable dt, string sCondition,string displayNm)
        {
            lblMsg.Visible = true;
            Application.DoEvents();

            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (sCondition == "제품바코드")
                    {
                        this.reportViewer1.Reset();
                        this.reportViewer1.LocalReport.DisplayName = displayNm;
                        this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\rpt제품박스바코드.rdlc";

                        ReportDataSource ds = new ReportDataSource("DataSet1", dt);
                        this.reportViewer1.LocalReport.DataSources.Add(ds);

                        ReportParameterCollection rptParams = new ReportParameterCollection();
                        rptParams.Add(new ReportParameter("p제목", displayNm));
                        //rptParams.Add(new ReportParameter("p견적구분", "아래와 같이 견적합니다."));
                        //rptParams.Add(new ReportParameter("p검색조건", "일자 : " + sDate + "     번호 : " + sNum));

                        this.reportViewer1.LocalReport.SetParameters(rptParams);

                    }
                    
                    //--------------------------------------------
                    //ReportPageSettings rpg = reportViewer1.LocalReport.GetDefaultPageSettings();
                    //System.Drawing.Printing.PageSettings pg = new System.Drawing.Printing.PageSettings();
                    //pg.PaperSize = rpg.PaperSize;

                    var setup = reportViewer1.GetPageSettings();
                    setup.PaperSize.Width = 400;
                    setup.PaperSize.Height = 200;
                    setup.Landscape = false;
                    setup.Margins.Left = 27;

                    //pg.Margins = rpg.Margins;
                    //pg.Landscape = false; // false = 세로, true = 가로

                    reportViewer1.SetPageSettings(setup);

                    this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                    this.reportViewer1.ZoomMode = ZoomMode.PageWidth;
                    this.reportViewer1.RefreshReport();

                    //--------------------------------------------

                }
            }
            catch (Exception ex)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
                MessageBox.Show("시스템에 문제가 있습니다.");
                this.Hide();
            }

            lblMsg.Visible = false;
        }
        public void prt_원자재재고현황(DataTable dt, string sDay1, string sCondition)
        {
            lblMsg.Visible = true;
            Application.DoEvents();

            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (sCondition == "원자재재고현황")
                    {
                        this.reportViewer1.Reset();
                        this.reportViewer1.LocalReport.DisplayName = "원자재재고현황";
                        this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\rpt원자재재고현황.rdlc";

                        ReportDataSource ds = new ReportDataSource("DataSet1", dt);
                        this.reportViewer1.LocalReport.DataSources.Add(ds);

                        ReportParameterCollection rptParams = new ReportParameterCollection();
                        rptParams.Add(new ReportParameter("p제목", "원자재재고현황"));
                        //rptParams.Add(new ReportParameter("p견적구분", "아래와 같이 견적합니다."));
                        rptParams.Add(new ReportParameter("p검색조건", "검색일자 : " + sDay1 ));

                        this.reportViewer1.LocalReport.SetParameters(rptParams);

                    }
                    

                    ReportPageSettings rpg = reportViewer1.LocalReport.GetDefaultPageSettings();
                    System.Drawing.Printing.PageSettings pg = new System.Drawing.Printing.PageSettings();
                    pg.PaperSize = rpg.PaperSize;
                    pg.Margins = rpg.Margins;
                    pg.Landscape = false; // false = 세로, true = 가로

                    reportViewer1.SetPageSettings(pg);

                    this.reportViewer1.RefreshReport();
                    this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                    this.reportViewer1.ZoomMode = ZoomMode.PageWidth;

                }
            }
            catch (Exception ex)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
                MessageBox.Show("시스템에 문제가 있습니다.");
                this.Hide();
            }

            lblMsg.Visible = false;
        }
        public void prt_원자재투입현황(DataTable dt, string sDay1, string sDay2, string sCondition)
        {
            lblMsg.Visible = true;
            Application.DoEvents();

            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (sCondition == "원자재투입현황")
                    {
                        this.reportViewer1.Reset();
                        this.reportViewer1.LocalReport.DisplayName = "원자재투입현황";
                        this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\rpt원자재투입현황.rdlc";

                        ReportDataSource ds = new ReportDataSource("DataSet1", dt);
                        this.reportViewer1.LocalReport.DataSources.Add(ds);

                        ReportParameterCollection rptParams = new ReportParameterCollection();
                        rptParams.Add(new ReportParameter("p제목", "원자재투입현황"));
                        //rptParams.Add(new ReportParameter("p견적구분", "아래와 같이 견적합니다."));
                        rptParams.Add(new ReportParameter("p검색조건", "검색일자 : " + sDay1 + " ~ " + sDay2));

                        this.reportViewer1.LocalReport.SetParameters(rptParams);

                    }

                    ReportPageSettings rpg = reportViewer1.LocalReport.GetDefaultPageSettings();
                    System.Drawing.Printing.PageSettings pg = new System.Drawing.Printing.PageSettings();
                    pg.PaperSize = rpg.PaperSize;
                    pg.Margins = rpg.Margins;
                    pg.Landscape = false; // false = 세로, true = 가로

                    reportViewer1.SetPageSettings(pg);

                    this.reportViewer1.RefreshReport();
                    this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                    this.reportViewer1.ZoomMode = ZoomMode.PageWidth;

                }
            }
            catch (Exception ex)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
                MessageBox.Show("시스템에 문제가 있습니다.");
                this.Hide();
            }

            lblMsg.Visible = false;
        }
        public void prt_원자재원장현황(DataTable dt, string sDay1, string sDay2, string sCondition)
        {
            lblMsg.Visible = true;
            Application.DoEvents();

            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (sCondition == "원자재원장현황")
                    {
                        this.reportViewer1.Reset();
                        this.reportViewer1.LocalReport.DisplayName = "원자재원장현황";
                        this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\rpt원자재원장현황.rdlc";

                        ReportDataSource ds = new ReportDataSource("DataSet1", dt);
                        this.reportViewer1.LocalReport.DataSources.Add(ds);

                        ReportParameterCollection rptParams = new ReportParameterCollection();
                        rptParams.Add(new ReportParameter("p제목", "원자재원장현황"));
                        //rptParams.Add(new ReportParameter("p견적구분", "아래와 같이 견적합니다."));
                        rptParams.Add(new ReportParameter("p검색조건", "검색일자 : " + sDay1 + " ~ " + sDay2));

                        this.reportViewer1.LocalReport.SetParameters(rptParams);

                    }

                    ReportPageSettings rpg = reportViewer1.LocalReport.GetDefaultPageSettings();
                    System.Drawing.Printing.PageSettings pg = new System.Drawing.Printing.PageSettings();
                    pg.PaperSize = rpg.PaperSize;
                    pg.Margins = rpg.Margins;
                    pg.Landscape = false; // false = 세로, true = 가로

                    reportViewer1.SetPageSettings(pg);

                    this.reportViewer1.RefreshReport();
                    this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                    this.reportViewer1.ZoomMode = ZoomMode.PageWidth;

                }
            }
            catch (Exception ex)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
                MessageBox.Show("시스템에 문제가 있습니다.");
                this.Hide();
            }

            lblMsg.Visible = false;
        }

        public void prt_HACCP점검표(DataTable dt, DataTable dt2, string sDate, string sNum, string sCondition)
        {
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    this.reportViewer1.Reset();
                    this.reportViewer1.LocalReport.DisplayName = "중요관리점(CCP) 검증 점검표" + "_" + sDate + "_" + sNum;
                    this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\rptHACCP점검표.rdlc";

                    ReportDataSource ds = new ReportDataSource("DataSet1", dt);
                    //this.reportViewer1.LocalReport.DataSources.Add(ds);
                    ds = new ReportDataSource("DataSet1", dt2);
                    this.reportViewer1.LocalReport.DataSources.Add(ds);


                    ReportParameterCollection rptParams = new ReportParameterCollection();
                    //rptParams.Add(new ReportParameter("p제목", "발주현황표"));

                    this.reportViewer1.LocalReport.SetParameters(rptParams);
                }

                ReportPageSettings rpg = reportViewer1.LocalReport.GetDefaultPageSettings();
                System.Drawing.Printing.PageSettings pg = new System.Drawing.Printing.PageSettings();
                pg.PaperSize = rpg.PaperSize;
                pg.Margins = rpg.Margins;
                pg.Landscape = false; // false = 세로, true = 가로

                //var setup = reportViewer1.GetPageSettings();
                //setup.PaperSize.Width = 900;
                //setup.PaperSize.Height = 600;
                //setup.Landscape = false;
                //setup.Margins.Left = 27;

                reportViewer1.SetPageSettings(pg);//pg

                this.reportViewer1.RefreshReport();
                this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                this.reportViewer1.ZoomMode = ZoomMode.PageWidth;

            }
            catch (Exception e)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, e.Message + " - " + e.ToString());
                MessageBox.Show("시스템에 문제가 있습니다.");
                this.Hide();
            }
        }

        public void prt_매출처원장(DataTable adoPrt2, DataGridView dgv, string strDay1, string strDay2, string strCondition)
        {

            DataTable adoPrt = GetDataTableFromDGV(dgv);
            

            try
            {
                if (adoPrt != null && adoPrt.Rows.Count > 0)
                {
                    this.reportViewer1.Reset();
                    this.reportViewer1.LocalReport.DisplayName = "매출처 원장";
                    this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\rpt매출처원장.rdlc";


                    adoPrt.Columns["NO"].ColumnName = "전표분류";
                    adoPrt.Columns["SALES_DATE"].ColumnName = "일자명칭";
                    adoPrt.Columns["SALES_CD"].ColumnName = "전표번호";
                    adoPrt.Columns["INPUT_GUBUN"].ColumnName = "구분명칭";
                    adoPrt.Columns["PRODUCT_NM"].ColumnName = "상품명";
                    adoPrt.Columns["TOTAL_AMT"].ColumnName = "박스수량";
                    adoPrt.Columns["TOTAL_PRICE"].ColumnName = "박스단가";
                    adoPrt.Columns["TOTAL_MONEY"].ColumnName = "금액";
                    adoPrt.Columns["TOTAL_TAX_MONEY"].ColumnName = "부가세액";
                    adoPrt.Columns["SOO_MONEY"].ColumnName = "수금액";
                    adoPrt.Columns["TOTAL_SOO_MONEY"].ColumnName = "지급액";
                    adoPrt.Columns["DC_MONEY"].ColumnName = "할인액";
                    adoPrt.Columns["BALANCE"].ColumnName = "잔고";
                    
                    adoPrt.Columns.Add("서비스박스");
                    adoPrt.Columns.Add("서비스낱개");
                    adoPrt.Columns.Add("낱개단가");
                    adoPrt.Columns.Add("총수량");
                    adoPrt.Columns.Add("낱개수량");
                    adoPrt.Columns.Add("중간수량");
                    adoPrt.Columns.Add("규격");
                    adoPrt.Columns.Add("정렬구분");
                    adoPrt.Columns.Add("일자");


                    for (int i = 0; i < adoPrt.Rows.Count; i++)
                    {
                        adoPrt.Rows[i]["서비스박스"] = "0";
                        adoPrt.Rows[i]["서비스낱개"] = "0";
                        adoPrt.Rows[i]["낱개단가"] = "0";
                        adoPrt.Rows[i]["총수량"] = "0";
                        adoPrt.Rows[i]["낱개수량"] = "0";
                        adoPrt.Rows[i]["중간수량"] = "0";
                        adoPrt.Rows[i]["규격"] = "0";
                        adoPrt.Rows[i]["정렬구분"] = "0";
                        adoPrt.Rows[i]["일자"] = "0";
                    }



                    ReportDataSource ds = new ReportDataSource("DataSet1", adoPrt);
                    //this.reportViewer1.LocalReport.DataSources.Add(ds);
                    this.reportViewer1.LocalReport.DataSources.Add(ds);


                    ReportParameterCollection rptParams = new ReportParameterCollection();
                    rptParams.Add(new ReportParameter("p제목", adoPrt2.Rows[0]["CUST_NM"].ToString()+".거래원장"));
                    rptParams.Add(new ReportParameter("p검색기간", strDay1 + " ~ " + strDay2));
                    rptParams.Add(new ReportParameter("p담당사원", "담당사원 : "+adoPrt2.Rows[0]["CUST_MANAGER"].ToString()));
                    rptParams.Add(new ReportParameter("p거래처명", adoPrt2.Rows[0]["CUST_NM"].ToString()));
                    rptParams.Add(new ReportParameter("p거래처코드", adoPrt2.Rows[0]["CUST_CD"].ToString()));
                    rptParams.Add(new ReportParameter("p거래처사업자번호", adoPrt2.Rows[0]["SAUP_NO"].ToString()));
                    rptParams.Add(new ReportParameter("p거래처전화번호", adoPrt2.Rows[0]["CUST_COMP_PHONE"].ToString()));
                    rptParams.Add(new ReportParameter("p거래처주소", adoPrt2.Rows[0]["ADDR"].ToString()));
                    rptParams.Add(new ReportParameter("p거래처대표자명", adoPrt2.Rows[0]["OWNER"].ToString()));
                    //rptParams.Add(new ReportParameter("p제목", "발주현황표"));

                    this.reportViewer1.LocalReport.SetParameters(rptParams);
                }

                ReportPageSettings rpg = reportViewer1.LocalReport.GetDefaultPageSettings();
                System.Drawing.Printing.PageSettings pg = new System.Drawing.Printing.PageSettings();
                pg.PaperSize = rpg.PaperSize;
                pg.Margins = rpg.Margins;
                pg.Landscape = true; // false = 세로, true = 가로

                var setup = reportViewer1.GetPageSettings();
                setup.PaperSize.Width = 1600;
                setup.PaperSize.Height = 1200;
                setup.Landscape = false;
                setup.Margins.Left = 27;

                reportViewer1.SetPageSettings(pg);//pg

                this.reportViewer1.RefreshReport();
                this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                this.reportViewer1.ZoomMode = ZoomMode.PageWidth;

            }
            catch (Exception e)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, e.Message + " - " + e.ToString());
                MessageBox.Show("시스템에 문제가 있습니다.");
                MessageBox.Show(e.Message + " - " + e.ToString());
                this.Hide();
            }
        }

        public void prt_매입처원장(DataTable adoPrt2, DataGridView dgv, string strDay1, string strDay2, string strCondition)
        {

            DataTable adoPrt = GetDataTableFromDGV(dgv);


            try
            {
                if (adoPrt != null && adoPrt.Rows.Count > 0)
                {
                    this.reportViewer1.Reset();
                    this.reportViewer1.LocalReport.DisplayName = "매출처 원장";
                    this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\rpt매출처원장.rdlc";


                    adoPrt.Columns["NO"].ColumnName = "전표분류";
                    adoPrt.Columns["BUY_DATE"].ColumnName = "일자명칭";
                    adoPrt.Columns["BUY_CD"].ColumnName = "전표번호";
                    adoPrt.Columns["INPUT_GUBUN"].ColumnName = "구분명칭";
                    adoPrt.Columns["PRODUCT_NM"].ColumnName = "상품명";
                    adoPrt.Columns["TOTAL_AMT"].ColumnName = "박스수량";
                    adoPrt.Columns["TOTAL_PRICE"].ColumnName = "박스단가";
                    adoPrt.Columns["TOTAL_MONEY"].ColumnName = "금액";
                    adoPrt.Columns["TOTAL_TAX_MONEY"].ColumnName = "부가세액";
                    adoPrt.Columns["GIVE_MONEY"].ColumnName = "수금액";
                    adoPrt.Columns["TOTAL_GIVE_MONEY"].ColumnName = "지급액";
                    adoPrt.Columns["DC_MONEY"].ColumnName = "할인액";
                    adoPrt.Columns["BALANCE"].ColumnName = "잔고";

                    adoPrt.Columns.Add("서비스박스");
                    adoPrt.Columns.Add("서비스낱개");
                    adoPrt.Columns.Add("낱개단가");
                    adoPrt.Columns.Add("총수량");
                    adoPrt.Columns.Add("낱개수량");
                    adoPrt.Columns.Add("중간수량");
                    adoPrt.Columns.Add("규격");
                    adoPrt.Columns.Add("정렬구분");
                    adoPrt.Columns.Add("일자");


                    for (int i = 0; i < adoPrt.Rows.Count; i++)
                    {
                        adoPrt.Rows[i]["서비스박스"] = "0";
                        adoPrt.Rows[i]["서비스낱개"] = "0";
                        adoPrt.Rows[i]["낱개단가"] = "0";
                        adoPrt.Rows[i]["박스단가"] = "";
                        adoPrt.Rows[i]["총수량"] = "0";
                        adoPrt.Rows[i]["낱개수량"] = "0";
                        adoPrt.Rows[i]["중간수량"] = "0";
                        adoPrt.Rows[i]["규격"] = "0";
                        adoPrt.Rows[i]["정렬구분"] = "0";
                        adoPrt.Rows[i]["일자"] = "0";
                    }



                    ReportDataSource ds = new ReportDataSource("DataSet1", adoPrt);
                    //this.reportViewer1.LocalReport.DataSources.Add(ds);
                    this.reportViewer1.LocalReport.DataSources.Add(ds);


                    ReportParameterCollection rptParams = new ReportParameterCollection();
                    rptParams.Add(new ReportParameter("p제목", adoPrt2.Rows[0]["CUST_NM"].ToString() + ".거래원장"));
                    rptParams.Add(new ReportParameter("p검색기간", strDay1 + " ~ " + strDay2));
                    rptParams.Add(new ReportParameter("p담당사원", "담당사원 : " + adoPrt2.Rows[0]["CUST_MANAGER"].ToString()));
                    rptParams.Add(new ReportParameter("p거래처명", adoPrt2.Rows[0]["CUST_NM"].ToString()));
                    rptParams.Add(new ReportParameter("p거래처코드", adoPrt2.Rows[0]["CUST_CD"].ToString()));
                    rptParams.Add(new ReportParameter("p거래처사업자번호", adoPrt2.Rows[0]["SAUP_NO"].ToString()));
                    rptParams.Add(new ReportParameter("p거래처전화번호", adoPrt2.Rows[0]["CUST_COMP_PHONE"].ToString()));
                    rptParams.Add(new ReportParameter("p거래처주소", adoPrt2.Rows[0]["ADDR"].ToString()));
                    rptParams.Add(new ReportParameter("p거래처대표자명", adoPrt2.Rows[0]["OWNER"].ToString()));
                    //rptParams.Add(new ReportParameter("p제목", "발주현황표"));

                    this.reportViewer1.LocalReport.SetParameters(rptParams);
                }

                ReportPageSettings rpg = reportViewer1.LocalReport.GetDefaultPageSettings();
                System.Drawing.Printing.PageSettings pg = new System.Drawing.Printing.PageSettings();
                pg.PaperSize = rpg.PaperSize;
                pg.Margins = rpg.Margins;
                pg.Landscape = true; // false = 세로, true = 가로

                var setup = reportViewer1.GetPageSettings();
                setup.PaperSize.Width = 1600;
                setup.PaperSize.Height = 1200;
                setup.Landscape = false;
                setup.Margins.Left = 27;

                reportViewer1.SetPageSettings(pg);//pg

                this.reportViewer1.RefreshReport();
                this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                this.reportViewer1.ZoomMode = ZoomMode.PageWidth;

            }
            catch (Exception e)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, e.Message + " - " + e.ToString());
                MessageBox.Show("시스템에 문제가 있습니다.");
                MessageBox.Show(e.Message + " - " + e.ToString());
                this.Hide();
            }
        }

        private DataTable GetDataTableFromDGV(DataGridView dgv)
        {
            var dt = new DataTable();
            foreach (DataGridViewColumn column in dgv.Columns)
            {
               dt.Columns.Add(column.Name);
            }

            object[] cellValues = new object[dgv.Columns.Count];
            foreach (DataGridViewRow row in dgv.Rows)
            {
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    cellValues[i] = row.Cells[i].Value;
                }
                dt.Rows.Add(cellValues);
            }

            return dt;
        }

        public void prt_거래명세표(DataTable adoPrt, DataGridView dgv_itemOut, string strCondition)
        {
            try
            {
                if (adoPrt != null && adoPrt.Rows.Count > 0)
                {
                    this.reportViewer1.Reset();
                    this.reportViewer1.LocalReport.DisplayName = "거래명세표";
                    this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\rpt거래명세표.rdlc";


                    ReportDataSource ds = new ReportDataSource("DataSet1", adoPrt);
                    //this.reportViewer1.LocalReport.DataSources.Add(ds);
                    this.reportViewer1.LocalReport.DataSources.Add(ds);


                    //ReportParameterCollection rptParams = new ReportParameterCollection();
                    //rptParams.Add(new ReportParameter("p제목", adoPrt2.Rows[0]["CUST_NM"].ToString() + ".거래원장"));
                    //rptParams.Add(new ReportParameter("p검색기간", strDay1 + " ~ " + strDay2));
                    //rptParams.Add(new ReportParameter("p담당사원", "담당사원 : " + adoPrt2.Rows[0]["CUST_MANAGER"].ToString()));
                    //rptParams.Add(new ReportParameter("p거래처명", adoPrt2.Rows[0]["CUST_NM"].ToString()));
                    //rptParams.Add(new ReportParameter("p거래처코드", adoPrt2.Rows[0]["CUST_CD"].ToString()));
                    //rptParams.Add(new ReportParameter("p거래처사업자번호", adoPrt2.Rows[0]["SAUP_NO"].ToString()));
                    //rptParams.Add(new ReportParameter("p거래처전화번호", adoPrt2.Rows[0]["CUST_COMP_PHONE"].ToString()));
                    //rptParams.Add(new ReportParameter("p거래처주소", adoPrt2.Rows[0]["ADDR"].ToString()));
                    //rptParams.Add(new ReportParameter("p거래처대표자명", adoPrt2.Rows[0]["OWNER"].ToString()));
                    ////rptParams.Add(new ReportParameter("p제목", "발주현황표"));

                    //this.reportViewer1.LocalReport.SetParameters(rptParams);
                }

                //ReportPageSettings rpg = reportViewer1.LocalReport.GetDefaultPageSettings();
                //System.Drawing.Printing.PageSettings pg = new System.Drawing.Printing.PageSettings();
                //pg.PaperSize = rpg.PaperSize;
                //pg.Margins = rpg.Margins;
                //pg.Landscape = true; // false = 세로, true = 가로

                

                //reportViewer1.SetPageSettings(pg);//pg

                this.reportViewer1.RefreshReport();
                this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                this.reportViewer1.ZoomMode = ZoomMode.PageWidth;

            }
            catch (Exception e)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, e.Message + " - " + e.ToString());
                MessageBox.Show("시스템에 문제가 있습니다.");
                MessageBox.Show(e.Message + " - " + e.ToString());
                this.Hide();
            }
        }


        public void prt_일일작업결과(DataTable dt, DataTable dt2, string sCondition)
        {
            lblMsg.Visible = true;
            Application.DoEvents();

            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (sCondition == "일일작업결과")
                    {
                        this.reportViewer1.Reset();
                        this.reportViewer1.LocalReport.DisplayName = "일일작업결과";
                        this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\rpt제품생산일지.rdlc";

                        ReportDataSource ds = new ReportDataSource("TOIPDATA", dt);
                        this.reportViewer1.LocalReport.DataSources.Add(ds);
                        ds = new ReportDataSource("MADEDATA", dt2);
                        this.reportViewer1.LocalReport.DataSources.Add(ds);



                        //ReportParameterCollection rptParams = new ReportParameterCollection();
                        //rptParams.Add(new ReportParameter("p제목", "일일작업결과"));

                        //this.reportViewer1.LocalReport.SetParameters(rptParams);
                    }

                    //var setup = reportViewer1.GetPageSettings();
                    //setup.PaperSize.Width = 1000;
                    //setup.PaperSize.Height = 600;
                    //setup.Landscape = false;
                    //setup.Margins.Left = 27;

                    ////ReportPageSettings rpg = reportViewer1.LocalReport.GetDefaultPageSettings();
                    ////System.Drawing.Printing.PageSettings pg = new System.Drawing.Printing.PageSettings();
                    ////pg.PaperSize = rpg.PaperSize;
                    ////pg.Margins = rpg.Margins;
                    ////pg.Landscape = false; // false = 세로, true = 가로

                    //reportViewer1.SetPageSettings(setup);

                    this.reportViewer1.RefreshReport();
                    this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                    this.reportViewer1.ZoomMode = ZoomMode.PageWidth;

                }
            }
            catch (Exception ex)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
                MessageBox.Show("시스템에 문제가 있습니다.");
                this.Hide();
            }

            lblMsg.Visible = false;
        }


        public void prt_원장현황(DataTable dt, DataGridView dgv, string sDay1, string sDay2, string sCondition, string title)
        {
            lblMsg.Visible = true;
            Application.DoEvents();
            try
            {
                DataTable adoPrt = GetDataTableFromDGV(dgv);
                if (adoPrt != null && adoPrt.Rows.Count > 0)
                {
                    adoPrt.Columns.Add("제품코드");
                    adoPrt.Columns["PRODUCT_NM"].ColumnName = "제품명";
                    adoPrt.Columns.Add("원부재료코드");
                    adoPrt.Columns["LABEL_NM"].ColumnName = "원부재료명";
                    adoPrt.Columns.Add("규격");
                    adoPrt.Columns.Add("단위코드");
                    adoPrt.Columns["UNIT_NM"].ColumnName = "단위명";
                    adoPrt.Columns["OUTPUT_LOT"].ColumnName = "제조번호";
                    adoPrt.Columns.Add("제조번호S");
                    adoPrt.Columns["INPUT_AMT"].ColumnName = "입고량";
                    adoPrt.Columns["OUTPUT_AMT"].ColumnName = "출고량";
                    adoPrt.Columns["BALSTOCK"].ColumnName = "HEATNO";
                    adoPrt.Columns["no"].ColumnName = "no";
                    adoPrt.Columns.Add("출고일자");
                    adoPrt.Columns.Add("바코드");
                    adoPrt.Columns.Add("투입량");
                    adoPrt.Columns.Add("거래처코드");
                    adoPrt.Columns.Add("거래처명");
                    adoPrt.Columns["INPUT_DATE"].ColumnName = "입고일자";
                    adoPrt.Columns["INPUT_CD"].ColumnName = "입고번호";
                    adoPrt.Columns["SEQ"].ColumnName = "입고순번";
                    adoPrt.Columns.Add("구분코드");
                    adoPrt.Columns["INPUT_GUBUN"].ColumnName = "구분명";
                    adoPrt.Columns.Add("수량");
                    adoPrt.Columns.Add("금액");
                    adoPrt.Columns["CHUGJONG_NM"].ColumnName = "축종";
                    adoPrt.Columns["CLASS_NM"].ColumnName = "부위";
                    adoPrt.Columns.Add("등급");
                    adoPrt.Columns["COUNTRY_NM"].ColumnName = "원산지";
                    adoPrt.Columns.Add("유형");
                    adoPrt.Columns["FROZEN_GUBUN"].ColumnName = "보관구분";
                    adoPrt.Columns["UNION_CD"].ColumnName = "개체번호";

                    for (int i = 0; i < adoPrt.Rows.Count; i++)
                    {
                        adoPrt.Rows[i]["제품코드"] = "0";
                        adoPrt.Rows[i]["원부재료코드"] = "0";
                        adoPrt.Rows[i]["규격"] = "0";
                        adoPrt.Rows[i]["단위코드"] = "0";
                        adoPrt.Rows[i]["출고일자"] = "0";
                        adoPrt.Rows[i]["바코드"] = "0";
                        adoPrt.Rows[i]["투입량"] = "0";
                        adoPrt.Rows[i]["거래처코드"] = "0";
                        adoPrt.Rows[i]["거래처명"] = "0";
                        adoPrt.Rows[i]["구분코드"] = "0";
                        adoPrt.Rows[i]["수량"] = "0";
                        adoPrt.Rows[i]["금액"] = "0";
                        adoPrt.Rows[i]["등급"] = "0";
                        adoPrt.Rows[i]["유형"] = "0";
                    }
                }
                if (adoPrt != null && adoPrt.Rows.Count > 0)
                {
                    string TitleTemp = sCondition;
                    if (title != null && !title.Equals(""))
                    {
                        TitleTemp = title + "." + TitleTemp;
                    }
                    if (sCondition == "원자재원장현황")
                    {


                        this.reportViewer1.Reset();
                        this.reportViewer1.LocalReport.DisplayName = "원자재원장현황";
                        this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\rpt원장현황.rdlc";

                        ReportDataSource ds = new ReportDataSource("DataSet1", adoPrt);
                        this.reportViewer1.LocalReport.DataSources.Add(ds);
                        ReportParameterCollection rptParams = new ReportParameterCollection();
                        rptParams.Add(new ReportParameter("p제목", TitleTemp));
                        //rptParams.Add(new ReportParameter("p견적구분", "아래와 같이 견적합니다."));
                        rptParams.Add(new ReportParameter("p검색조건", "검색일자 : " + sDay1 + " ~ " + sDay2));

                        this.reportViewer1.LocalReport.SetParameters(rptParams);

                    }

                    else if (sCondition == "제품원장현황")
                    {
                        this.reportViewer1.Reset();
                        this.reportViewer1.LocalReport.DisplayName = "제품원장현황";
                        this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\rpt원장현황.rdlc";

                        ReportDataSource ds = new ReportDataSource("DataSet1", adoPrt);
                        this.reportViewer1.LocalReport.DataSources.Add(ds);
                        ReportParameterCollection rptParams = new ReportParameterCollection();
                        rptParams.Add(new ReportParameter("p제목", TitleTemp));
                        //rptParams.Add(new ReportParameter("p견적구분", "아래와 같이 견적합니다."));
                        rptParams.Add(new ReportParameter("p검색조건", "검색일자 : " + sDay1 + " ~ " + sDay2));

                        this.reportViewer1.LocalReport.SetParameters(rptParams);
                    }

                    ReportPageSettings rpg = reportViewer1.LocalReport.GetDefaultPageSettings();
                    System.Drawing.Printing.PageSettings pg = new System.Drawing.Printing.PageSettings();
                    pg.PaperSize = rpg.PaperSize;
                    pg.Margins = rpg.Margins;
                    pg.Landscape = true; // false = 세로, true = 가로

                    //var setup = reportViewer1.GetPageSettings();
                    //setup.PaperSize.Width = 1600;
                    //setup.PaperSize.Height = 1200;
                    //setup.Landscape = false;
                    //setup.Margins.Left = 200;

                    reportViewer1.SetPageSettings(pg);//pg

                    this.reportViewer1.RefreshReport();
                    this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                    this.reportViewer1.ZoomMode = ZoomMode.PageWidth;
                }
            }
            catch (Exception ex)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
                MessageBox.Show("시스템에 문제가 있습니다.");
                this.Hide();
            }

            lblMsg.Visible = false;
        }
    }
}
