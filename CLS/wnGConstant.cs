using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 스마트팩토리.Controls;

namespace 스마트팩토리.CLS
{
    public class wnGConstant
    {
        public const bool debug = true;

        private string strQuery;
        private SqlConnection adoConnection = new SqlConnection();
        private SqlCommand adoCommand = new SqlCommand();
        private SqlDataAdapter adoAdapter = new SqlDataAdapter();
        private DataTable adoTable = new DataTable();
        private DataRow adoRow;

        public string NumbDisplay(decimal nVal, string sFormat)
        {
            string sRet = "";

            if (nVal != 0)
            {
                sRet = nVal.ToString(sFormat);
            }

            return sRet;
        }

        public bool isNum(string sValue)
        {
            bool bRet = false;

            try
            {
                decimal num = decimal.Parse(sValue);
                bRet = true;
            }
            catch
            {
                bRet = false;
            }
            return bRet;
        }

        public bool check_NumText(Form frm, string sName, string sMsg)
        {
            bool bRet = true;

            try
            {
                var txtBox = frm.Controls.Find("txt" + sName, true);

                if (txtBox[0].Text == "")
                {
                    MessageBox.Show("[ " + sMsg + " ] 을 입력하세요.");
                    bRet = false;
                }
                else
                {
                    if (isNum(txtBox[0].Text) == false)
                    {
                        MessageBox.Show("[ " + sMsg + " ] 에 숫자만 입력하세요.");
                        bRet = false;
                    }
                    //else
                    //{
                    //    txtBox[0].Text = txtBox[0].Text.Replace(",", "");
                    //}
                }
            }
            catch
            {
                MessageBox.Show("[ " + sMsg + " ] 에 알수 없는 문제가 있습니다.");
                bRet = false;
            }

            return bRet;
        }

        public bool check_NumText(UserControl uc, string sName, string sMsg)
        {
            bool bRet = true;

            try
            {
                var txtBox = uc.Controls.Find("txt" + sName, true);

                if (txtBox[0].Text == "")
                {
                    MessageBox.Show("[ " + sMsg + " ] 을 입력하세요.");
                    bRet = false;
                }
                else
                {
                    if (isNum(txtBox[0].Text) == false)
                    {
                        MessageBox.Show("[ " + sMsg + " ] 에 숫자만 입력하세요.");
                        bRet = false;
                    }
                    //else
                    //{
                    //    txtBox[0].Text = txtBox[0].Text.Replace(",", "");
                    //}
                }
            }
            catch
            {
                MessageBox.Show("[ " + sMsg + " ] 에 알수 없는 문제가 있습니다.");
                bRet = false;
            }

            return bRet;
        }

        public void set_공용일자(conDateTimePicker dtp)
        {
            try
            {
                string sqlQuery = "select getdate() ";

                adoTable = RunTable(sqlQuery);

                if (adoTable != null)
                {
                    dtp.Text = DateTime.Parse(adoTable.Rows[0][0].ToString()).ToString("yyyy-MM-dd");
                }
            }
            catch
            {
                dtp.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        public void set_공용시각(conDateTimePicker dtp)
        {
            try
            {
                string sqlQuery = "select getdate() ";

                adoTable = RunTable(sqlQuery);

                if (adoTable != null)
                {
                    dtp.Text = DateTime.Parse(adoTable.Rows[0][0].ToString()).ToString("HH:mm:ss");
                }
            }
            catch
            {
                dtp.Text = DateTime.Now.ToString("HH:mm:ss");
            }
        }

        public void set_공용일자(Label dtp)
        {
            try
            {
                string sqlQuery = "select getdate() ";

                adoTable = RunTable(sqlQuery);

                if (adoTable != null)
                {
                    dtp.Text = DateTime.Parse(adoTable.Rows[0][0].ToString()).ToString("yyyy-MM-dd");
                }
            }
            catch
            {
                dtp.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        public void set_공용시각(Label dtp)
        {
            try
            {
                string sqlQuery = "select getdate() ";

                adoTable = RunTable(sqlQuery);

                if (adoTable != null)
                {
                    dtp.Text = DateTime.Parse(adoTable.Rows[0][0].ToString()).ToString("HH:mm:ss");
                }
            }
            catch
            {
                dtp.Text = DateTime.Now.ToString("HH:mm:ss");
            }
        }

        public bool DB_Open(string sConn)
        {
            if (adoConnection.State == ConnectionState.Open)
            {
                return true;
            }
            if (string.IsNullOrEmpty(sConn))
            {
                return false;
            }

            adoConnection.ConnectionString = sConn;
            try
            {
                adoConnection.Open();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "에러발생", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        public DataTable RunTable(string query)
        {
            string sConnDB = Common.p_sConn; //Common.p_strConn;

            if (!DB_Open(sConnDB))
            {
                return null;
            };
            DataTable adoDT = new DataTable();
            adoCommand = adoConnection.CreateCommand();
            try
            {
                adoCommand.CommandType = CommandType.Text;
                adoCommand.CommandText = query;
                adoAdapter.SelectCommand = adoCommand;
                adoAdapter.Fill(adoDT);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "에러발생", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                adoConnection.Close();
            }

            return adoDT;
        }

        public void ComboBox_Read_Blank(conComboBox sCombo, string sQuery)
        {
            if (sQuery.Trim().Length > 10)
            {
                strQuery = sQuery.Trim();
            }
            adoTable = RunTable(strQuery);
            adoRow = adoTable.NewRow();
            adoRow[0] = "";
            adoRow[1] = "";
            adoTable.Rows.InsertAt(adoRow, 0);

            sCombo.DataSource = adoTable;
        }

        public void ComboBox_Read_NoBlank(conComboBox sCombo, string sQuery)
        {
            if (sQuery.Trim().Length > 10)
            {
                strQuery = sQuery.Trim();
            }
            adoTable = RunTable(strQuery);

            sCombo.DataSource = adoTable;
        }

        public void ComboBox_Read_NoBlank(ComboBox sCombo, string sQuery)
        {
            if (sQuery.Trim().Length > 10)
            {
                strQuery = sQuery.Trim();
            }
            adoTable = RunTable(strQuery);

            sCombo.DataSource = adoTable;
        }

        public void ComboBox_Read_ALL(conComboBox sCombo, string sQuery)
        {
            if (sQuery.Trim().Length > 10)
            {
                strQuery = sQuery.Trim();
            }
            adoTable = RunTable(strQuery);
            adoRow = adoTable.NewRow();
            adoRow[0] = "";
            adoRow[1] = "(전체)";
            adoTable.Rows.InsertAt(adoRow, 0);

            sCombo.DataSource = adoTable;
        }

        public void GridComboBox_Read_NoBlank(DataGridViewComboBoxColumn sCombo, string sQuery)
        {
            if (sQuery.Trim().Length > 10)
            {
                strQuery = sQuery.Trim();
            }
            adoTable = RunTable(strQuery);

            sCombo.DataSource = adoTable;
        }

        public void GridComboBox_Read_Blank(DataGridViewComboBoxColumn sCombo, string sQuery)
        {
            if (sQuery.Trim().Length > 10)
            {
                strQuery = sQuery.Trim();
            }
            adoTable = RunTable(strQuery);
            adoRow = adoTable.NewRow();
            adoRow[0] = "";
            adoRow[1] = "";
            adoTable.Rows.InsertAt(adoRow, 0);

            sCombo.DataSource = adoTable;
        }

        public void GridComboBoxCell_Read_Blank(DataGridViewComboBoxCell sCombo, string sQuery)
        {
            if (sQuery.Trim().Length > 10)
            {
                strQuery = sQuery.Trim();
            }
            adoTable = RunTable(strQuery);
            adoRow = adoTable.NewRow();
            adoRow[0] = "";
            adoRow[1] = "";
            adoTable.Rows.InsertAt(adoRow, 0);

            sCombo.DataSource = adoTable;
        }


        public void Form_Clear(Control.ControlCollection sCtrols)
        {
            foreach (Control cCtrl in sCtrols)
            {
                if (cCtrl is TextBox)
                {
                    TextBox sControl = (TextBox)cCtrl;
                    if (sControl.Text.Trim().Length > 0)
                    {
                        sControl.Text = "";
                    }
                    if (sControl.GetType().Name == "conTextNumber")
                    {
                        conTextNumber cont = (conTextNumber)cCtrl;
                        cont._FormatString = "#0";
                        if (cont._ValueType == "수량")
                        {
                            cont._FormatString = Common.p_strFormatAmount;
                        }
                        if (cont._ValueType == "단가")
                        {
                            cont._FormatString = Common.p_strFormatUnit;
                        }
                        sControl.Text = (0).ToString(cont._FormatString);
                    }
                }
                else if (cCtrl is CheckBox)
                {
                    CheckBox myCheck = (CheckBox)cCtrl;
                    myCheck.Checked = false;
                }
                else if (cCtrl is RadioButton)
                {
                    RadioButton myCheck = (RadioButton)cCtrl;
                    myCheck.Checked = false;
                }
                else if (cCtrl is MaskedTextBox)
                {
                    cCtrl.Text = "";
                }
                else if (cCtrl is conLabel)
                {
                    cCtrl.Text = "";
                }
                else if (cCtrl is RichTextBox)
                {
                    cCtrl.Text = "";
                }
                else if (cCtrl is NumericUpDown)
                {
                    cCtrl.Text = "";
                }
                else if (cCtrl is DateTimePicker)
                {
                    cCtrl.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
        }

        /// BYTE[]를 이미지로 변환
        public Image ConvertByteToImage(byte[] pByte)
        {
            MemoryStream ms = new MemoryStream();
            Image theImage = null;
            try
            {
                ms.Position = 0;
                ms.Write(pByte, 0, (int)pByte.Length);
                theImage = Image.FromStream(ms);
            }
            catch (Exception ex)
            {
                string aa = ex.Message;
            }
            return theImage;
        }

        /// 이미지를 BYTE[]로 변환..
        public byte[] ConvertImageToByte(Bitmap img, string sFileExt)
        {
            // 받은 이미지를 다시 설정해서, 저장해야 함.
            Bitmap theImage = new Bitmap(img);

            byte[] pByte = new byte[0];
            MemoryStream ms = new MemoryStream();
            try
            {
                switch (sFileExt.ToLower())
                {
                    case "jpeg":
                        theImage.Save(ms, ImageFormat.Jpeg);
                        break;
                    case "bmp":
                        theImage.Save(ms, ImageFormat.Bmp);
                        break;
                    case "emf":
                        theImage.Save(ms, ImageFormat.Emf);
                        break;
                    case "exif":
                        theImage.Save(ms, ImageFormat.Exif);
                        break;
                    case "gif":
                        theImage.Save(ms, ImageFormat.Gif);
                        break;
                    case "icon":
                        theImage.Save(ms, ImageFormat.Icon);
                        break;
                    case "memorybmp":
                        theImage.Save(ms, ImageFormat.MemoryBmp);
                        break;
                    case "png":
                        theImage.Save(ms, ImageFormat.Png);
                        break;
                    case "tiff":
                        theImage.Save(ms, ImageFormat.Tiff);
                        break;
                    case "wmf":
                        theImage.Save(ms, ImageFormat.Wmf);
                        break;
                }
                pByte = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(pByte, 0, (int)ms.Length);
                ms.Close();
                ms = null;
            }
            catch (Exception ex)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
            }
            return pByte;
        }

        public string maxValue_Check(string sqlQuery)
        {
            string sRet = "";

            try
            {
                adoTable = RunTable(sqlQuery);

                if (adoTable != null)
                {
                    if (adoTable.Rows.Count > 0)
                    {
                        sRet = adoTable.Rows[0][0].ToString();
                    }
                }
            }
            catch (Exception e)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, e.Message + " - " + e.ToString());
                sRet = "";
            }

            return sRet;
        }

        public void select_Check(DataGridView grd, int nCol, bool bCheck)
        {
            for (int kk = 0; kk < grd.Rows.Count; kk++)
            {
                if (bCheck == true)
                {
                    if (grd.Rows[kk].Cells[nCol].ReadOnly == false)
                    {
                        grd.Rows[kk].Cells[nCol].Value = true;
                    }
                }
                else
                {
                    grd.Rows[kk].Cells[nCol].Value = false;
                }
            }
        }

        public void init_RowText(conDataGridView dgv, int nRow)
        {
            dgv.Rows[nRow].Cells[0].Value = false;
            //dgv.Rows[nRow].Cells[1].Value = "";
            dgv.Rows[nRow].Cells[2].Value = "";
            dgv.Rows[nRow].Cells[3].Value = "";
            dgv.Rows[nRow].Cells[4].Value = "";
            dgv.Rows[nRow].Cells[5].Value = "";
            dgv.Rows[nRow].Cells[6].Value = "";
            dgv.Rows[nRow].Cells[7].Value = "0";
            dgv.Rows[nRow].Cells[8].Value = "0";
            dgv.Rows[nRow].Cells[9].Value = "0";
            dgv.Rows[nRow].Cells[10].Value = "0";
            dgv.Rows[nRow].Cells[11].Value = "0";
            dgv.Rows[nRow].Cells[12].Value = "0";
            dgv.Rows[nRow].Cells[13].Value = "0";
            dgv.Rows[nRow].Cells[14].Value = "";    //endCol
        }

        public void call_popRef_Man(string sTxt, TextBox txt_Code, TextBox txt_Name, string sUsedYN)
        {
            Popup.pop담당자검색 frm = new Popup.pop담당자검색();

            //frm.sUsedYN = sUsedYN;
            frm.txtSrch.Text = sTxt;
            frm.ShowDialog();

            if (frm.sRetCode != "")
            {
                txt_Code.Text = frm.sRetCode.Trim();
                txt_Name.Text = frm.sRetName.Trim();
            }

            frm.Dispose();
            frm = null;
        }

        public void call_popRef_Cust(string sTxt, TextBox txt_Code, TextBox txt_Name, string sUsedYN)
        {
            Popup.pop거래처검색 frm = new Popup.pop거래처검색();

            //frm.sUsedYN = sUsedYN;
            frm.txtSrch.Text = sTxt;
            frm.ShowDialog();

            if (frm.sCode != "")
            {
                txt_Code.Text = frm.sCode.Trim();
                txt_Name.Text = frm.sName.Trim();
            }
            frm.Dispose();
            frm = null;
        }

        public void call_pop_Prod(conDataGridView dgv, int nRow, string sTxt, string sCust, int nColCode, int nColName, string sUsedYN)
        {
            Popup.pop제품검색 frm = new Popup.pop제품검색();

            //frm.sUsedYN = sUsedYN;
            frm.txtSrch.Text = sTxt;
            frm.ShowDialog();

            if (frm.sRetCode != "")
            {
                dgv.Rows[nRow].Cells[nColCode].Value = frm.sRetCode.Trim();
                dgv.Rows[nRow].Cells[nColName].Value = frm.sRetName.Trim();
            }

            frm.Dispose();
            frm = null;
        }

        public void Calc_Price(conDataGridView dgv, int nRow)
        {
            dgv.Rows[nRow].Cells[9].Value = "0";   // 금액

            string s수량 = ("" + (string)dgv.Rows[nRow].Cells[7].Value).Replace(",", "");
            string s단가 = ("" + (string)dgv.Rows[nRow].Cells[8].Value).Replace(",", "");
            if (s수량 != "" && s단가 != "")
            {
                string s금액 = (decimal.Parse(s수량) * decimal.Parse(s단가)).ToString();

                dgv.Rows[nRow].Cells[9].Value = (decimal.Parse(s금액)).ToString("#,0");

                double nINprice = 0;
                double nNet = 0;
                double nGlos = 0;
                double nVat = 0;

                if (s금액 != "")
                {
                    nINprice = double.Parse(s금액);

                    nVat = Math.Round(nINprice * 0.1, 0, MidpointRounding.AwayFromZero);
                    nNet = nINprice;
                    nGlos = nVat + nNet;
                    nNet = 0;
                }

                dgv.Rows[nRow].Cells[10].Value = nVat.ToString();
            }
        }


        public void f_Calc_Price(conDataGridView dgv, int nRow ,string total_amt, string price)
        {
            dgv.Rows[nRow].Cells["TOTAL_MONEY"].Value = "0";   // 금액
            string s수량 = ("" + (string)dgv.Rows[nRow].Cells[total_amt].Value).Replace(",", "");
            string s단가 = ("" + (string)dgv.Rows[nRow].Cells[price].Value).Replace(",", "");

            if (s수량 != "" && s단가 != "")
            {
                string s금액 = (decimal.Parse(s수량) * decimal.Parse(s단가)).ToString();

                dgv.Rows[nRow].Cells[total_amt].Value = (decimal.Parse(s수량)).ToString("#,0.######");
                dgv.Rows[nRow].Cells[price].Value = (decimal.Parse(s단가)).ToString("#,0.######");
                dgv.Rows[nRow].Cells["TOTAL_MONEY"].Value = (decimal.Parse(s금액)).ToString("#,0.######");
            }
        }

        public void get_Prod_Info(conDataGridView dgv, int nRow, string sCode, string sCust, string s특매처, string s거래형태, string s주사제퍼센트, string s도매퍼센트)
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_PRODUCT_Detail_Cust(sCode, sCust);

                if (dt != null && dt.Rows.Count > 0)
                {
                    dgv.Rows[nRow].Cells[6].Value = dt.Rows[0]["PRODUCT_SPEC"].ToString().Trim();

                    // null 처리
                    if (dt.Rows[0]["SELLING_PRICE1"].ToString().Trim() == "") dt.Rows[0]["SELLING_PRICE1"] = 0;
                    if (dt.Rows[0]["SELLING_PRICE2"].ToString().Trim() == "") dt.Rows[0]["SELLING_PRICE2"] = 0;
                    if (dt.Rows[0]["SELLING_PRICE3"].ToString().Trim() == "") dt.Rows[0]["SELLING_PRICE3"] = 0;
                    if (dt.Rows[0]["SELLING_PRICE4"].ToString().Trim() == "") dt.Rows[0]["SELLING_PRICE4"] = 0;
                    if (dt.Rows[0]["SELLING_PRICE5"].ToString().Trim() == "") dt.Rows[0]["SELLING_PRICE5"] = 0;
                    if (dt.Rows[0]["PRODUCT_PRICE2"].ToString().Trim() == "") dt.Rows[0]["PRODUCT_PRICE2"] = 0;
                    if (dt.Rows[0]["MIN_QTY"].ToString().Trim() == "") dt.Rows[0]["MIN_QTY"] = 0;

                    dgv.Rows[nRow].Cells[8].Value = decimal.Parse(dt.Rows[0]["SELLING_PRICE2"].ToString().Trim()).ToString(Common.p_strFormatUnit);    // 단가
                    dgv.Rows[nRow].Cells[11].Value = decimal.Parse(dt.Rows[0]["할인율"].ToString().Trim()).ToString("#,0");    // 할인율
                    dgv.Rows[nRow].Cells[12].Value = decimal.Parse(dt.Rows[0]["계약율"].ToString().Trim()).ToString("#,0");    // 계약율
                    dgv.Rows[nRow].Cells[13].Value = decimal.Parse(dt.Rows[0]["SELLING_PRICE6"].ToString().Trim()).ToString("#,0");    // 표준단가
                    if (s특매처 == "1")
                    {
                        dgv.Rows[nRow].Cells[11].Value = "0";    // 할인율
                        dgv.Rows[nRow].Cells[12].Value = "0";    // 계약율
                    }

                    //가. 거래처 거래형태(dealtype)가 “2.도매” 이면 도매단가(selling_price1) 적용
                    //나. 거래처 거래형태(dealtype)가 “1.약국” 이면 약국단가(selling_price2) 적용
                    //다. 거래처 거래형태(dealtype)가 “3.의원” 이면 의원단가(selling_price3) 적용
                    //라. 거래처 거래형태(dealtype)가 “5.병원”, “6.종합병원” 이면 병원단가(selling_price4) 적용
                    //마. 거래처 거래형태(dealtype)가 “9.기타” ... 

                    switch (s거래형태)
                    {
                        case "1":
                            dgv.Rows[nRow].Cells[8].Value = decimal.Parse(dt.Rows[0]["SELLING_PRICE2"].ToString().Trim()).ToString(Common.p_strFormatUnit);
                            break;
                        case "2":
                            dgv.Rows[nRow].Cells[8].Value = decimal.Parse(dt.Rows[0]["SELLING_PRICE1"].ToString().Trim()).ToString(Common.p_strFormatUnit);
                            break;
                        case "3":
                            dgv.Rows[nRow].Cells[8].Value = decimal.Parse(dt.Rows[0]["SELLING_PRICE3"].ToString().Trim()).ToString(Common.p_strFormatUnit);
                            break;
                        case "5":
                            dgv.Rows[nRow].Cells[8].Value = decimal.Parse(dt.Rows[0]["SELLING_PRICE4"].ToString().Trim()).ToString(Common.p_strFormatUnit);
                            break;
                        case "6":
                            dgv.Rows[nRow].Cells[8].Value = decimal.Parse(dt.Rows[0]["SELLING_PRICE4"].ToString().Trim()).ToString(Common.p_strFormatUnit);
                            break;
                        case "9":
                            string s기타단가 = decimal.Parse(dt.Rows[0]["SELLING_PRICE5"].ToString().Trim()).ToString(Common.p_strFormatUnit);

                            if (dt.Rows[0]["PRODUCT_TYPE"].ToString() == "12")
                            {
                                // 비급여 경우
                                dgv.Rows[nRow].Cells[8].Value = s기타단가;
                            }
                            else
                            {
                                // 급여 경우
                                if (int.Parse(s주사제퍼센트) != 0 && "" + dt.Rows[0]["PRODUCT_KIND"].ToString().Trim() == "08")
                                {
                                    //약가 * 환산량 (여기서 수숫점 버리지 말고 그냥 가져간다 ss_단가 - double 필드)
                                    double nDanGa = 0;
                                    nDanGa = double.Parse(dt.Rows[0]["PRODUCT_PRICE2"].ToString().Trim()) * double.Parse(dt.Rows[0]["MIN_QTY"].ToString().Trim());
                                    nDanGa = nDanGa / 1.1;

                                    //세바의약푸(519-001) 마이알주(399056) 단가가 33370이 나와야하는데 33380이 나와 소숫점 버림함)2013/12/30)
                                    //제품 스포라틴정(219166) 86%의 경우 단단위가 26520이 나와야 하는데 26510이나옴 그래서 fIX 제거함(2014-01-27)
                                    // FIX 제거하면 다른 제품이안 맞음...ㅠㅠ..VB6.0의 오류 FIX 앞에 VAL 을 해주면 된다
                                    nDanGa = nDanGa * ((100 - int.Parse(s주사제퍼센트)) / 100f);
                                    nDanGa = Math.Truncate(nDanGa);
                                    nDanGa = Math.Truncate(nDanGa / 10f);
                                    nDanGa = nDanGa * 10;

                                    dgv.Rows[nRow].Cells[8].Value = nDanGa.ToString(Common.p_strFormatUnit);
                                }
                                else
                                {
                                    if (int.Parse(s도매퍼센트) == 0 || "" + dt.Rows[0]["PRODUCT_TYPE"].ToString().Trim() == "12")
                                    {
                                        dgv.Rows[nRow].Cells[8].Value = s기타단가;
                                    }
                                    else
                                    {
                                        //약가 * 환산량 (여기서 수숫점 버리지 말고 그냥 가져간다 ss_단가 - double 필드)
                                        double nDanGa = 0;
                                        nDanGa = double.Parse(dt.Rows[0]["PRODUCT_PRICE2"].ToString().Trim()) * double.Parse(dt.Rows[0]["MIN_QTY"].ToString().Trim());
                                        nDanGa = nDanGa / 1.1;

                                        //세바의약푸(519-001) 마이알주(399056) 단가가 33370이 나와야하는데 33380이 나와 소숫점 버림함)2013/12/30)
                                        //제품 스포라틴정(219166) 85%의 경우 단단위가 26520이 나와야 하는데 26510이나옴 그래서 fIX 제거함(2014-01-27)
                                        //FIX 제거하면 다른 제품이안 맞음...ㅠㅠ..VB6.0의 오류 FIX 앞에 VAL 을 해주면 된다
                                        nDanGa = nDanGa * ((100 - int.Parse(s주사제퍼센트)) / 100f);
                                        nDanGa = Math.Truncate(nDanGa);
                                        nDanGa = Math.Truncate(nDanGa / 10f);
                                        nDanGa = nDanGa * 10;

                                        dgv.Rows[nRow].Cells[8].Value = nDanGa.ToString(Common.p_strFormatUnit);
                                    }
                                }
                            }
                            break;
                    }

                    //    //Calc_Price(dgv, nRow, s거래처부가세코드);
                }

            }
            catch (Exception ex)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
            }
        }

        public decimal get_거래처별_잔고(string sCust, string sDay)
        {
            decimal nRet = 0;

            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_거래처잔고_Summary(sCust, sDay);

                if (dt != null && dt.Rows.Count > 0)
                {
                    nRet = decimal.Parse(dt.Rows[0]["현잔고"].ToString());
                }
            }
            catch (Exception ex)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
            }
            return nRet;
        }

        public void set_Combo판매구분(bool bAll, conComboBox cmb, bool bNew)
        {
            string sqlQuery = "";

            cmb.ValueMember = "코드";
            cmb.DisplayMember = "명칭";

            if (bNew == true)
            {
                // 판매만 입력

                sqlQuery = " select '11' as 코드, '판매' as 명칭 ";
                sqlQuery += " union all ";
                sqlQuery += " select '22' as 코드, '반품' as 명칭 ";
            }
            else
            {
                // 옛날 데이터 표시용으로 활용

                sqlQuery = " select '11' as 코드, '판매' as 명칭 ";
                sqlQuery += " union all ";
                sqlQuery += " select '12' as 코드, '가조' as 명칭 ";
                sqlQuery += " union all ";
                sqlQuery += " select '13' as 코드, '기증' as 명칭 ";
                sqlQuery += " union all ";
                sqlQuery += " select '14' as 코드, '샘플' as 명칭 ";
                sqlQuery += " union all ";
                sqlQuery += " select '15' as 코드, '교환출고' as 명칭 ";
                sqlQuery += " union all ";
                sqlQuery += " select '16' as 코드, '수금찬조' as 명칭 ";
                sqlQuery += " union all ";
                sqlQuery += " select '17' as 코드, '할인찬조' as 명칭 ";
                sqlQuery += " union all ";
                sqlQuery += " select '18' as 코드, '신규판조' as 명칭 ";
                sqlQuery += " union all ";
                sqlQuery += " select '21' as 코드, '취소' as 명칭 ";
                sqlQuery += " union all ";
                sqlQuery += " select '22' as 코드, '반품' as 명칭 ";
                sqlQuery += " union all ";
                sqlQuery += " select '23' as 코드, '할증취소' as 명칭 ";
                sqlQuery += " union all ";
                sqlQuery += " select '25' as 코드, '교환입고' as 명칭 ";
                sqlQuery += " union all ";
                sqlQuery += " select '31' as 코드, '판매' as 명칭 ";
                sqlQuery += " union all ";
                sqlQuery += " select '32' as 코드, '반품' as 명칭 ";
                sqlQuery += " union all ";
                sqlQuery += " select '41' as 코드, '특판' as 명칭 ";
                sqlQuery += " union all ";
                sqlQuery += " select '42' as 코드, '특반' as 명칭 ";
                sqlQuery += " union all ";
                sqlQuery += " select '43' as 코드, '수출' as 명칭 ";
            }

            if (bAll == true)
            {
                ComboBox_Read_ALL(cmb, sqlQuery);
            }
            else
            {
                ComboBox_Read_NoBlank(cmb, sqlQuery);
            }
        }

        public void set_Combo수금구분(bool bAll, conComboBox cmb)
        {
            string sqlQuery = "";

            cmb.ValueMember = "코드";
            cmb.DisplayMember = "명칭";

            sqlQuery = " select '51' as 코드, '현금' as 명칭 ";
            sqlQuery += " union all ";
            sqlQuery += " select '52' as 코드, '카드' as 명칭 ";
            sqlQuery += " union all ";
            sqlQuery += " select '53' as 코드, '취소' as 명칭 ";
            sqlQuery += " union all ";
            sqlQuery += " select '61' as 코드, '은행도' as 명칭 ";
            sqlQuery += " union all ";
            sqlQuery += " select '62' as 코드, '신협' as 명칭 ";
            sqlQuery += " union all ";
            sqlQuery += " select '63' as 코드, '자가' as 명칭 ";
            sqlQuery += " union all ";
            sqlQuery += " select '64' as 코드, '당좌' as 명칭 ";
            sqlQuery += " union all ";
            sqlQuery += " select '65' as 코드, '가계' as 명칭 ";
            sqlQuery += " union all ";
            sqlQuery += " select '71' as 코드, '매출할인' as 명칭 ";
            sqlQuery += " union all ";
            sqlQuery += " select '72' as 코드, '대손' as 명칭 ";
            sqlQuery += " union all ";
            sqlQuery += " select '73' as 코드, '신고할인' as 명칭 ";
            sqlQuery += " union all ";
            sqlQuery += " select '90' as 코드, '잔고이월' as 명칭 ";

            if (bAll == true)
            {
                ComboBox_Read_ALL(cmb, sqlQuery);
            }
            else
            {
                ComboBox_Read_NoBlank(cmb, sqlQuery);
            }
        }

        public void set_Combo수금구분_영업소(bool bAll, conComboBox cmb)
        {
            string sqlQuery = "";

            cmb.ValueMember = "코드";
            cmb.DisplayMember = "명칭";

            sqlQuery = " select '51' as 코드, '현금' as 명칭 ";
            sqlQuery += " union all ";
            sqlQuery += " select '52' as 코드, '카드' as 명칭 ";
            sqlQuery += " union all ";
            sqlQuery += " select '53' as 코드, '취소' as 명칭 ";
            sqlQuery += " union all ";
            sqlQuery += " select '61' as 코드, '은행도' as 명칭 ";
            sqlQuery += " union all ";
            sqlQuery += " select '62' as 코드, '신협' as 명칭 ";
            sqlQuery += " union all ";
            sqlQuery += " select '63' as 코드, '자가' as 명칭 ";
            sqlQuery += " union all ";
            sqlQuery += " select '64' as 코드, '당좌' as 명칭 ";
            sqlQuery += " union all ";
            sqlQuery += " select '65' as 코드, '가계' as 명칭 ";
            //sqlQuery += " union all ";
            //sqlQuery += " select '71' as 코드, '매출할인' as 명칭 ";
            //sqlQuery += " union all ";
            //sqlQuery += " select '72' as 코드, '대손' as 명칭 ";
            //sqlQuery += " union all ";
            //sqlQuery += " select '73' as 코드, '신고할인' as 명칭 ";
            //sqlQuery += " union all ";
            //sqlQuery += " select '90' as 코드, '잔고이월' as 명칭 ";

            if (bAll == true)
            {
                ComboBox_Read_ALL(cmb, sqlQuery);
            }
            else
            {
                ComboBox_Read_NoBlank(cmb, sqlQuery);
            }
        }

        public void set_Combo어음처리형태(bool bAll, conComboBox cmb)
        {
            string sqlQuery = "";

            cmb.ValueMember = "코드";
            cmb.DisplayMember = "명칭";

            sqlQuery = " select '0' as 코드, '미결재' as 명칭 ";
            sqlQuery += " union all ";
            sqlQuery += " select '1' as 코드, '만기결제' as 명칭 ";
            sqlQuery += " union all ";
            sqlQuery += " select '2' as 코드, '예금입금' as 명칭 ";
            sqlQuery += " union all ";
            sqlQuery += " select '3' as 코드, '대체결제' as 명칭 ";
            sqlQuery += " union all ";
            sqlQuery += " select '4' as 코드, '할인' as 명칭 ";

            if (bAll == true)
            {
                ComboBox_Read_ALL(cmb, sqlQuery);
            }
            else
            {
                ComboBox_Read_NoBlank(cmb, sqlQuery);
            }
        }

        public string get_판매구분_명칭(string sCode)
        {
            string sRet = "";

            switch (sCode.Trim())
            {
                case "11":
                    sRet = "판매";
                    break;
                case "12":
                    sRet = "가조";
                    break;
                case "13":
                    sRet = "기증";
                    break;
                case "14":
                    sRet = "샘플";
                    break;
                case "15":
                    sRet = "교환출고";
                    break;
                case "16":
                    sRet = "수금찬조";
                    break;
                case "17":
                    sRet = "할인찬조";
                    break;
                case "18":
                    sRet = "신규판조";
                    break;
                case "21":
                    sRet = "취소";
                    break;
                case "22":
                    sRet = "반품";
                    break;
                case "23":
                    sRet = "할증취소";
                    break;
                case "25":
                    sRet = "교환입고";
                    break;
                case "31":
                    sRet = "판매";
                    break;
                case "32":
                    sRet = "반품";
                    break;
                case "41":
                    sRet = "특판";
                    break;
                case "42":
                    sRet = "특반";
                    break;
                case "43":
                    sRet = "수출";
                    break;
                default:
                    sRet = "?";
                    break;
            }
            return sRet;
        }

        public string get_수금구분_명칭(string sCode)
        {
            string sRet = "";

            switch (sCode.Trim())
            {
                case "51":
                    sRet = "현금";
                    break;
                case "52":
                    sRet = "카드";
                    break;
                case "53":
                    sRet = "취소";
                    break;
                case "61":
                    sRet = "은행도";
                    break;
                case "62":
                    sRet = "신협";
                    break;
                case "63":
                    sRet = "자가";
                    break;
                case "64":
                    sRet = "당좌";
                    break;
                case "65":
                    sRet = "가계";
                    break;
                case "71":
                    sRet = "매출할인";
                    break;
                case "72":
                    sRet = "대손";
                    break;
                case "73":
                    sRet = "신고할인";
                    break;
                case "90":
                    sRet = "잔고이월";
                    break;
                default:
                    sRet = "?";
                    break;
            }
            return sRet;
        }

        public void call_pop_raw_mat(conDataGridView dgv, DataTable dt, int nRow, string sTxt, int gbn)
        {
            Popup.pop원부재료검색 frm = new Popup.pop원부재료검색();
            Console.WriteLine("in the call_pop_raw_mat");
            //frm.sUsedYN = sUsedYN;
            frm.dt = dt;
            frm.txtSrch.Text = sTxt;
            frm.ShowDialog();

            if (frm.sRetCode != "")
            {
                if (gbn == 1)
                {
                    dgv.Rows[nRow].Cells["RAW_MAT_CD"].Value = frm.dgv.Rows[0].Cells["RAW_MAT_CD"].Value;
                    dgv.Rows[nRow].Cells["RAW_MAT_NM"].Value = frm.dgv.Rows[0].Cells["RAW_MAT_NM"].Value;
                    dgv.Rows[nRow].Cells["OLD_RAW_MAT_NM"].Value = frm.dgv.Rows[0].Cells["RAW_MAT_NM"].Value;
                    dgv.Rows[nRow].Cells["SPEC"].Value = frm.dgv.Rows[0].Cells["SPEC"].Value;


                    dgv.Rows[nRow].Cells["CHUGJONG_NM"].Value = frm.dgv.Rows[0].Cells["CHUGJONG_NM"].Value;
                    dgv.Rows[nRow].Cells["CLASS_NM"].Value = frm.dgv.Rows[0].Cells["CLASS_NM"].Value;
                    dgv.Rows[nRow].Cells["COUNTRY_NM"].Value = frm.dgv.Rows[0].Cells["COUNTRY_NM"].Value;
                    dgv.Rows[nRow].Cells["TYPE_NM"].Value = frm.dgv.Rows[0].Cells["TYPE_NM"].Value;

                    dgv.Rows[nRow].Cells["IN_UNIT"].Value = frm.dgv.Rows[0].Cells["INPUT_UNIT"].Value;
                    dgv.Rows[nRow].Cells["OUT_UNIT"].Value = frm.dgv.Rows[0].Cells["OUTPUT_UNIT"].Value;
                    dgv.Rows[nRow].Cells["IN_UNIT_NM"].Value = frm.dgv.Rows[0].Cells["INPUT_UNIT_NM"].Value;
                    dgv.Rows[nRow].Cells["OUT_UNIT_NM"].Value = frm.dgv.Rows[0].Cells["OUTPUT_UNIT_NM"].Value;
                    dgv.Rows[nRow].Cells["LABEL_NM"].Value = frm.dgv.Rows[0].Cells["LABEL_NM"].Value;
                    dgv.Rows[nRow].Cells["TOTAL_AMT"].Value = "0";
                    dgv.Rows.Add();
                    dgv.Rows[dgv.Rows.Count - 1].Cells["TOTAL_AMT"].Value = "0";
                }
                else if (gbn == 2)
                {
                    
                    dgv.Rows[nRow].Cells["RAW_MAT_CD"].Value = frm.dgv.Rows[0].Cells["RAW_MAT_CD"].Value;

                    dgv.Rows[nRow].Cells["RAW_MAT_CD"].Value = frm.dgv.Rows[0].Cells["RAW_MAT_CD"].Value;
                    dgv.Rows[nRow].Cells["RAW_MAT_NM"].Value = frm.dgv.Rows[0].Cells["RAW_MAT_NM"].Value;
                    dgv.Rows[nRow].Cells["OLD_RAW_MAT_NM"].Value = frm.dgv.Rows[0].Cells["RAW_MAT_NM"].Value;
                    dgv.Rows[nRow].Cells["SPEC"].Value = frm.dgv.Rows[0].Cells["SPEC"].Value;
                    dgv.Rows[nRow].Cells["UNIT_CD"].Value = frm.dgv.Rows[0].Cells["INPUT_UNIT"].Value;
                    dgv.Rows[nRow].Cells["UNIT_NM"].Value = frm.dgv.Rows[0].Cells["INPUT_UNIT_NM"].Value;
                    dgv.Rows[nRow].Cells["PRICE"].Value = frm.dgv.Rows[0].Cells["INPUT_PRICE"].Value;
                    dgv.Rows[nRow].Cells["CHUGJONG_CD"].Value = frm.dgv.Rows[0].Cells["CHUGJONG_CD"].Value;
                    dgv.Rows[nRow].Cells["CHUGJONG_NM"].Value = frm.dgv.Rows[0].Cells["CHUGJONG_NM"].Value;
                    dgv.Rows[nRow].Cells["CLASS_NM"].Value = frm.dgv.Rows[0].Cells["CLASS_NM"].Value;
                    dgv.Rows[nRow].Cells["CLASS_CD"].Value = frm.dgv.Rows[0].Cells["CLASS_CD"].Value;
                    dgv.Rows[nRow].Cells["COUNTRY"].Value = frm.dgv.Rows[0].Cells["COUNTRY_NM"].Value;
                    dgv.Rows[nRow].Cells["COUNTRY_CD"].Value = frm.dgv.Rows[0].Cells["COUNTRY_CD"].Value;
                    ////DataGridViewComboBoxCell cmbTemp = (DataGridViewComboBoxCell)dgv.Rows[nRow].Cells["GRADE_NM"];
                    //cmbTemp.Value = frm.dgv.Rows[0].Cells["GRADE_CD"].Value.ToString();
                    dgv.Rows[nRow].Cells["GRADE_CD"].Value = frm.dgv.Rows[0].Cells["GRADE_CD"].Value;
                    dgv.Rows[nRow].Cells["BOX_AMT"].Value = frm.dgv.Rows[0].Cells["BOX_AMT"].Value;
                    dgv.Rows[nRow].Cells["LABEL_NM"].Value = frm.dgv.Rows[0].Cells["LABEL_NM"].Value;
                    dgv.Rows[nRow].Cells["TYPE_CD"].Value = frm.dgv.Rows[0].Cells["TYPE_CD"].Value;
                    dgv.Rows[nRow].Cells["TYPE_NM"].Value = frm.dgv.Rows[0].Cells["TYPE_NM"].Value;
                    dgv.Rows[nRow].Cells["RAW_MAT_GUBUN"].Value = frm.dgv.Rows[0].Cells["RAW_MAT_GUBUN_CD"].Value;
                    dgv.Rows[nRow].Cells["RAW_MAT_GUBUN_NM"].Value = frm.dgv.Rows[0].Cells["RAW_MAT_GUBUN_NM"].Value;
                    dgv.Rows[nRow].Cells["VAT_CD"].Value = frm.dgv.Rows[0].Cells["VAT_CD"].Value;

                    if (frm.dgv.Rows[0].Cells["RAW_MAT_GUBUN_CD"].Value.Equals("1"))
                    {
                        DataGridViewCellStyle style = new DataGridViewCellStyle();
                        style.BackColor = Color.White;

                        dgv.Rows[nRow].Cells["UNION_CD"].Style = style;
                        dgv.Rows[nRow].Cells["GRADE_NM"].Style = style;
                        dgv.Rows[nRow].Cells["MF_DATE"].Style = style;
                        dgv.Rows[nRow].Cells["EXPRT_DATE"].Style = style;
                        dgv.Rows[nRow].Cells["GRADE_NM"].Style = style;
                        dgv.Rows[nRow].Cells["TOTAL_AMT"].Style = style;
                        dgv.Rows[nRow].Cells["TOTAL_MONEY"].Style = style;
                        dgv.Rows[nRow].Cells["UNIT_NM"].Style = style;
                        dgv.Rows[nRow].Cells["CHUGJONG_NM"].Style = style;
                        dgv.Rows[nRow].Cells["CLASS_NM"].Style = style;
                        dgv.Rows[nRow].Cells["COUNTRY"].Style = style;
                        dgv.Rows[nRow].Cells["TYPE_NM"].Style = style;
                        dgv.Rows[nRow].Cells["LABEL_NM"].Style = style;
                        dgv.Rows[nRow].Cells["RAW_MAT_GUBUN_NM"].Style = style;

                        
                        



                        dgv.Rows[nRow].Cells["UNION_CD"].ReadOnly = false;
                        dgv.Rows[nRow].Cells["GRADE_NM"].ReadOnly = false;
                        dgv.Rows[nRow].Cells["FROZEN_GUBUN"].ReadOnly = false;
                        dgv.Rows[nRow].Cells["STORE_GUBUN"].ReadOnly = false;
                        dgv.Rows[nRow].Cells["LOC_GUBUN"].ReadOnly = false;
                        dgv.Rows[nRow].Cells["TOTAL_AMT"].ReadOnly = false;
                        dgv.Rows[nRow].Cells["TOTAL_MONEY"].ReadOnly = false;

                    }
                    else
                    {
                        //dgv.Rows[nRow].Cells["UNION_CD"].ReadOnly = true;
                        //dgv.Rows[nRow].Cells["GRADE_NM"].ReadOnly = true;
                        //dgv.Rows[nRow].Cells["FROZEN_GUBUN"].ReadOnly = true;
                        //dgv.Rows[nRow].Cells["STORE_GUBUN"].ReadOnly = true;
                        //dgv.Rows[nRow].Cells["LOC_GUBUN"].ReadOnly = true;
                        //dgv.Rows[nRow].Cells["TOTAL_AMT"].ReadOnly = false;
                        //dgv.Rows[nRow].Cells["TOTAL_MONEY"].ReadOnly = false;
                        dgv.Rows[nRow].Cells["UNION_CD"].ReadOnly = true;
                        dgv.Rows[nRow].Cells["GRADE_NM"].ReadOnly = true;
                        dgv.Rows[nRow].Cells["FROZEN_GUBUN"].ReadOnly = true;
                        dgv.Rows[nRow].Cells["STORE_GUBUN"].ReadOnly = false;
                        dgv.Rows[nRow].Cells["LOC_GUBUN"].ReadOnly = false;
                        dgv.Rows[nRow].Cells["TOTAL_AMT"].ReadOnly = false;
                        dgv.Rows[nRow].Cells["TOTAL_MONEY"].ReadOnly = false;
                        DataGridViewCellStyle style = new DataGridViewCellStyle();
                        style.BackColor = Color.White;

                        dgv.Rows[nRow].Cells["TOTAL_AMT"].Style = style;
                        dgv.Rows[nRow].Cells["TOTAL_MONEY"].Style = style;
                        dgv.Rows[nRow].Cells["UNIT_NM"].Style = style;
                        dgv.Rows[nRow].Cells["RAW_MAT_GUBUN_NM"].Style = style;
                        dgv.Rows[nRow].Cells["MF_DATE"].Style = style;
                        dgv.Rows[nRow].Cells["EXPRT_DATE"].Style = style;
                    }
                    


                    dgv.Rows.Add();
                    dgv.Rows[dgv.Rows.Count - 1].Cells["TOTAL_AMT"].Value = "0";
                    dgv.Rows[dgv.Rows.Count - 1].Cells["PRICE"].Value = "0";
                    dgv.Rows[dgv.Rows.Count - 1].Cells["TOTAL_MONEY"].Value = "0";
                    dgv.Rows[dgv.Rows.Count - 1].Cells["TOTAL_BOX_AMT"].Value = "0";

                }
                else //gbn == 3
                {
                    dgv.Rows[nRow].Cells["RAW_MAT_CD"].Value = frm.dgv.Rows[0].Cells["RAW_MAT_CD"].Value;
                    dgv.Rows[nRow].Cells["RAW_MAT_NM"].Value = frm.dgv.Rows[0].Cells["RAW_MAT_NM"].Value;
                    //dgv.Rows[nRow].Cells["OLD_RAW_MAT_NM"].Value = frm.dgv.Rows[0].Cells["RAW_MAT_NM"].Value;
                    dgv.Rows[nRow].Cells["SPEC"].Value = frm.dgv.Rows[0].Cells["SPEC"].Value;
                    dgv.Rows[nRow].Cells["INPUT_UNIT"].Value = frm.dgv.Rows[0].Cells["INPUT_UNIT"].Value;
                    dgv.Rows[nRow].Cells["OUTPUT_UNIT"].Value = frm.dgv.Rows[0].Cells["OUTPUT_UNIT"].Value;
                    dgv.Rows[nRow].Cells["INPUT_UNIT_NM"].Value = frm.dgv.Rows[0].Cells["INPUT_UNIT_NM"].Value;
                    dgv.Rows[nRow].Cells["OUTPUT_UNIT_NM"].Value = frm.dgv.Rows[0].Cells["OUTPUT_UNIT_NM"].Value;
                    dgv.Rows[nRow].Cells["BAL_STOCK"].Value = frm.dgv.Rows[0].Cells["BAL_STOCK"].Value;
                    dgv.Rows[nRow].Cells["CUST_CD"].Value = frm.dgv.Rows[0].Cells["CUST_CD"].Value;
                    dgv.Rows[nRow].Cells["CUST_NM"].Value = frm.dgv.Rows[0].Cells["CUST_NM"].Value;

                    dgv.Rows[nRow].Cells["CVR_RATIO"].Value = frm.dgv.Rows[0].Cells["CVR_RATIO"].Value;
                }
            }
            else
            {
                dgv.Rows[nRow].Cells["RAW_MAT_NM"].Value = (string)dgv.Rows[nRow].Cells["OLD_RAW_MAT_NM"].Value;
            }

            frm.Dispose();
            frm = null;
        }

        public void call_pop_flow(conDataGridView dgv, DataTable dt, int nRow, string sTxt)
        {
            Popup.pop공정검색 frm = new Popup.pop공정검색();

            //frm.sUsedYN = sUsedYN;
            frm.dt = dt;
            frm.txtSrch.Text = sTxt;
            frm.ShowDialog();

            if (frm.sRetCode != "")
            {
                dgv.Rows[nRow].Cells["FLOW_CD"].Value = frm.sRetCode.Trim();
                dgv.Rows[nRow].Cells["FLOW_NM"].Value = frm.sRetName.Trim();
                dgv.Rows[nRow].Cells["OLD_FLOW_NM"].Value = frm.sRetName.Trim();

                if (frm.sRetFlowYN.Trim().ToString().Equals("Y"))
                {
                    dgv.Rows[nRow].Cells["FLOW_YN"].Value = true;
                }
                else
                {
                    dgv.Rows[nRow].Cells["FLOW_YN"].Value = false;
                }
                dgv.Rows.Add();
            }
            else
            {
                dgv.Rows[nRow].Cells["FLOW_NM"].Value = (string)dgv.Rows[nRow].Cells["OLD_FLOW_NM"].Value;
            }

            frm.Dispose();
            frm = null;
        }

        public string call_pop_item(conDataGridView dgv, DataTable dt, int nRow, string sTxt)
        {
            Popup.pop_sf_제품검색 frm = new Popup.pop_sf_제품검색();

            //frm.sUsedYN = sUsedYN;
            frm.dt = dt;
            frm.txtSrch.Text = sTxt;
            frm.ShowDialog();
            string sCode = frm.sCode;

            if (frm.sCode != "")
            {

                if ((string)dgv.Rows[nRow].Cells["OLD_ITEM_CD"].Value != frm.sCode.Trim())
                {
                    dgv.Rows[nRow].Cells["ITEM_CD"].Value = frm.sCode.Trim();
                    dgv.Rows[nRow].Cells["SPEC"].Value = frm.sSpec.Trim();
                    dgv.Rows[nRow].Cells["ITEM_NM"].Value = frm.sName.Trim();

                    dgv.Rows[nRow].Cells["OLD_ITEM_CD"].Value = frm.sCode.Trim();
                    dgv.Rows[nRow].Cells["OLD_ITEM_NM"].Value = frm.sName.Trim();
                    dgv.Rows[nRow].Cells["UNIT_CD"].Value = frm.sUnitCd.Trim();
                    dgv.Rows[nRow].Cells["UNIT_NM"].Value = frm.sUnitNm.Trim();
                    dgv.Rows[nRow].Cells["PRICE"].Value = frm.sOutputPrice.Trim();

                    //dgv.Rows[dgv.Rows.Count - 1].Cells["TOTAL_MONEY"].Value = "0";
                    dgv.Rows[dgv.Rows.Count - 1].Cells["TOTAL_AMT"].Value = "1";
                    dgv.Rows[dgv.Rows.Count - 1].Cells["UNIT_AMT"].Value = "1";

                    dgv.Rows.Add();
                    dgv.Rows[dgv.Rows.Count - 1].Cells["TOTAL_AMT"].Value = "0";
                    dgv.Rows[dgv.Rows.Count - 1].Cells["PRICE"].Value = "0";
                    dgv.Rows[dgv.Rows.Count - 1].Cells["TOTAL_MONEY"].Value = "0";
                }
                else
                {
                    dgv.Rows[nRow].Cells["ITEM_NM"].Value = (string)dgv.Rows[nRow].Cells["OLD_ITEM_NM"].Value;
                }
            }
            else
            {
                dgv.Rows[nRow].Cells["ITEM_NM"].Value = (string)dgv.Rows[nRow].Cells["OLD_ITEM_NM"].Value;
            }

            frm.Dispose();
            frm = null;

            return sCode;
        }
        //2019-12-02 이재원 씨지엠 제품 검색 새로 작성
        public string CZM_call_pop_item(conDataGridView dgv, DataTable dt, int nRow, string sTxt)
        {
            Popup.pop_sf_제품검색 frm = new Popup.pop_sf_제품검색();

            //frm.sUsedYN = sUsedYN;
            frm.dt = dt;
            frm.txtSrch.Text = sTxt;
            frm.ShowDialog();
            string sCode = frm.sCode;

            if (frm.sCode != "")
            {
                string sName = frm.sName;
                string sSpec = frm.sSpec;
                string sUnitCd = frm.sUnitCd;
                string sUnitNm = frm.sUnitNm;
                string sCharge =frm.sCharge;
                string sPack =frm.sPack;
                string sInputPrice = frm.sInputPrice;
                string sOutputPrice =frm.sOutputPrice;
                string sRetFlowYN = frm.sRetFlowYN;
                string sLabelNM = frm.sLabelNM;
                string sTypeNM = frm.sTypeNM;
                

                dgv.Rows[nRow].Cells["IT_ITEM_CD"].Value = frm.sCode;
                dgv.Rows[nRow].Cells["IT_ITEM_NM"].Value = frm.sName;
                dgv.Rows[nRow].Cells["IT_LABEL_NM"].Value = frm.sLabelNM;
                dgv.Rows[nRow].Cells["IT_UNIT_CD"].Value = frm.sUnitCd;
                dgv.Rows[nRow].Cells["IT_UNIT_NM"].Value = frm.sUnitNm;
                dgv.Rows[nRow].Cells["IT_TYPE_NM"].Value = frm.sTypeNM;

                
               
                
            }
            

            frm.Dispose();
            frm = null;

            return sCode;
        }

        public string CZM_call_pop_raw_and_item(conDataGridView dgv, DataTable dt, int nRow, string sTxt)
        {
            Popup.pop_sf_상품제품검색 frm = new Popup.pop_sf_상품제품검색();

            //frm.sUsedYN = sUsedYN;
            frm.dt = dt;
            frm.txtSrch.Text = sTxt;
            frm.ShowDialog();
            string sCode = frm.sCode;

            if (frm.sCode != "")
            {
                string sName = frm.sName;
                string sSpec = frm.sSpec;
                string sUnitCd = frm.sUnitCd;
                string sUnitNm = frm.sUnitNm;
                string sLabelNM = frm.sLabelNM;


                dgv.Rows[nRow].Cells["ITEM_CD"].Value = frm.sCode;
                dgv.Rows[nRow].Cells["ITEM_NM"].Value = frm.sName;
                dgv.Rows[nRow].Cells["LABEL_NM"].Value = frm.sLabelNM;
                dgv.Rows[nRow].Cells["UNIT_CD"].Value = frm.sUnitCd;
                dgv.Rows[nRow].Cells["UNIT_NM"].Value = frm.sUnitNm;

                dgv.Rows[nRow].Cells["CHUGJONG_CD"].Value = frm.sChugjong_cd;
                dgv.Rows[nRow].Cells["CHUGJONG_NM"].Value = frm.sChugjong_NM;
                dgv.Rows[nRow].Cells["CLASS_CD"].Value = frm.sClass_cd;
                dgv.Rows[nRow].Cells["CLASS_NM"].Value = frm.sClass_nm;
                dgv.Rows[nRow].Cells["COUNTRY_CD"].Value = frm.sCountry_cd;
                dgv.Rows[nRow].Cells["COUNTRY_NM"].Value = frm.sCountry_nm;
                dgv.Rows[nRow].Cells["RAW_ITEM_GUBUN"].Value = frm.sGubun;
                dgv.Rows[nRow].Cells["TYPE_CD"].Value = frm.sType_cd;
                dgv.Rows[nRow].Cells["TYPE_NM"].Value = frm.sType_nm;


            }


            frm.Dispose();
            frm = null;

            return sCode;
        }

        public void call_pop_item_half(conDataGridView dgv, DataTable dt, int nRow, string sTxt)
        {
            Popup.pop_sf_제품검색 frm = new Popup.pop_sf_제품검색();


            //frm.sUsedYN = sUsedYN;
            frm.dt = dt;
            frm.txtSrch.Text = sTxt;
            frm.ShowDialog();

            if (frm.sCode != "")
            {
                dgv.Rows[nRow].Cells["HALF_ITEM_CD"].Value = frm.sCode.Trim();
                dgv.Rows[nRow].Cells["HALF_ITEM_NM"].Value = frm.sName.Trim();
                dgv.Rows[nRow].Cells["OLD_HALF_ITEM_NM"].Value = frm.sName.Trim();
                dgv.Rows[nRow].Cells["H_UNIT_CD"].Value = frm.sUnitCd.Trim();
                dgv.Rows[nRow].Cells["H_UNIT_NM"].Value = frm.sUnitNm.Trim();
                dgv.Rows[nRow].Cells["H_TOTAL_AMT"].Value = "0";

                dgv.Rows.Add();
                dgv.Rows[dgv.Rows.Count - 1].Cells["H_TOTAL_AMT"].Value = "0";
            }
            else
            {
                dgv.Rows[nRow].Cells["HALF_ITEM_NM"].Value = (string)dgv.Rows[nRow].Cells["OLD_HALF_ITEM_NM"].Value;
            }

            frm.Dispose();
            frm = null;
        }

        public string call_pop_chk(conDataGridView dgv, DataTable dt, int nRow, string sTxt, int chk_gbn)
        {
            Popup.pop검사기준검색 frm = new Popup.pop검사기준검색();

            //frm.sUsedYN = sUsedYN;
            frm.dt = dt;
            frm.txtSrch.Text = sTxt;
            frm.chk_gbn = chk_gbn;
            frm.ShowDialog();
            string sCode = frm.sCode;

            if (frm.sCode != "")
            {

                bool chk = true;
                string msg = "";
                if ((string)dgv.Rows[nRow].Cells["OLD_CHK_CD"].Value != frm.sCode.Trim())
                {
                    for (int i = 0; i < dgv.Rows.Count; i++) 
                    {
                        if ((string)dgv.Rows[i].Cells["CHK_CD"].Value == frm.sCode.Trim()) 
                        {
                             msg = "검사기준 코드: " + frm.sName.Trim() + "(이)가 존재합니다. ";
                            chk = false;

                            break;
                        }
                    }

                    if (chk)
                    {
                        dgv.Rows[nRow].Cells["NO"].Value = (nRow + 1).ToString();
                        dgv.Rows[nRow].Cells["CHK_CD"].Value = frm.sCode.Trim();
                        dgv.Rows[nRow].Cells["CHK_NM"].Value = frm.sName.Trim();
                        dgv.Rows[nRow].Cells["CHK_ORD"].Value = frm.sOrd.Trim();

                        dgv.Rows[nRow].Cells["OLD_CHK_CD"].Value = frm.sCode.Trim();
                        dgv.Rows[nRow].Cells["OLD_CHK_NM"].Value = frm.sName.Trim();

                    }
                    else 
                    {
                        MessageBox.Show(msg);
                    }
                    
                }
                else
                {
                    dgv.Rows[nRow].Cells["CHK_NM"].Value = (string)dgv.Rows[nRow].Cells["OLD_CHK_NM"].Value;
                }
            }
            else
            {
                dgv.Rows[nRow].Cells["CHK_NM"].Value = (string)dgv.Rows[nRow].Cells["OLD_CHK_NM"].Value;
            }

            frm.Dispose();
            frm = null;

            return sCode;
        }

        public void f_Calc_PriceAndBox(conDataGridView dgv, int nRow, string total_amt, string price ,string box_amt)
        {
            dgv.Rows[nRow].Cells["TOTAL_MONEY"].Value = (decimal.Parse(dgv.Rows[nRow].Cells["TOTAL_MONEY"].Value.ToString())).ToString("#,0.######");   // 금액
            string s수량 = ("" + (string)dgv.Rows[nRow].Cells[total_amt].Value).Replace(",", "");
            string s단가 = ("" + (string)dgv.Rows[nRow].Cells[price].Value).Replace(",", "");
            string s장입중량 = ("" + (string)dgv.Rows[nRow].Cells[box_amt].Value).Replace(",", "");
            if (s장입중량.Equals("0"))
            {
                string s박스 = "0";
                dgv.Rows[nRow].Cells[total_amt].Value = (decimal.Parse(s수량)).ToString("#,0.######");
                dgv.Rows[nRow].Cells[box_amt].Value = (decimal.Parse(s장입중량)).ToString("#,0.######");
                dgv.Rows[nRow].Cells["TOTAL_BOX_AMT"].Value = (decimal.Parse(s박스)).ToString("#,0.######");

                //if (s수량 != "" && s단가 != "")
                //{
                  //  string s금액 = (decimal.Parse(s수량) * decimal.Parse(s단가)).ToString();

                   // dgv.Rows[nRow].Cells[total_amt].Value = (decimal.Parse(s수량)).ToString("#,0.######");
                   // dgv.Rows[nRow].Cells[price].Value = (decimal.Parse(s단가)).ToString("#,0.######");
                   // dgv.Rows[nRow].Cells["TOTAL_MONEY"].Value = (decimal.Parse(s금액)).ToString("#,0.######");

                //}
                return;
            }

            if (s수량 != "" && s장입중량 != "")
            {
                decimal 나머지 = (decimal.Parse(s수량) % int.Parse(s장입중량));
                string s박스 = ((int)(decimal.Parse(s수량) / int.Parse(s장입중량)) + (나머지 > 0 ? 1 : 0)).ToString();

                dgv.Rows[nRow].Cells[total_amt].Value = (decimal.Parse(s수량)).ToString("#,0.######");
                dgv.Rows[nRow].Cells[box_amt].Value = (decimal.Parse(s장입중량)).ToString("#,0.######");
                dgv.Rows[nRow].Cells["TOTAL_BOX_AMT"].Value = (decimal.Parse(s박스)).ToString("#,0.######");

                //if (s수량 != "" && s단가 != "")
               // {
                    //string s금액 = (decimal.Parse(s수량) * decimal.Parse(s단가)).ToString();

                   // dgv.Rows[nRow].Cells[total_amt].Value = (decimal.Parse(s수량)).ToString("#,0.######");
                   // dgv.Rows[nRow].Cells[price].Value = (decimal.Parse(s단가)).ToString("#,0.######");
                   // dgv.Rows[nRow].Cells["TOTAL_MONEY"].Value = (decimal.Parse(s금액)).ToString("#,0.######");
               // }
            }
        }

        private void mergeCell_2Row(DataGridView grd, int nColPos)
        {
            for (int i = 0; i < grd.Rows.Count; i++)
            {
                if (i % 2 == 0)
                {
                    ((DataGridViewTextBoxCellEx)grd[nColPos, i]).RowSpan = 2;
                }
            }
        }

        public void mergeCells(DataGridView grd, int nColCnt)
        {
            int i;
            DataGridViewTextBoxCellEx item;
            string[] value = new string[nColCnt];
            int[] numArray = new int[nColCnt];
            int[] numArray1 = new int[nColCnt];
            for (i = 0; i < nColCnt; i++)
            {
                value[i] = "---";
                numArray[i] = 0;
                numArray1[i] = 0;
            }
            for (int j = 0; j < grd.Rows.Count; j++)
            {
                for (i = 0; i < nColCnt; i++)
                {
                    if (!(value[i] == ((string)grd.Rows[j].Cells[i].Value ?? "")))
                    {
                        if (numArray1[i] > 0)
                        {
                            item = (DataGridViewTextBoxCellEx)grd[i, numArray[i]];
                            item.RowSpan = numArray1[i] + 1;
                        }
                        numArray[i] = j;
                        numArray1[i] = 0;
                        value[i] = (string)grd.Rows[j].Cells[i].Value ?? "";
                    }
                    else if (i == 0)
                    {
                        numArray1[i]++;
                    }
                    else if (numArray1[i - 1] <= 0)
                    {
                        if (numArray1[i] > 0)
                        {
                            item = (DataGridViewTextBoxCellEx)grd[i, numArray[i]];
                            item.RowSpan = numArray1[i] + 1;
                        }
                        numArray[i] = j;
                        numArray1[i] = 0;
                        value[i] = (string)grd.Rows[j].Cells[i].Value ?? "";
                    }
                    else
                    {
                        numArray1[i]++;
                    }
                }
            }
            for (i = 0; i < nColCnt; i++)
            {
                if (numArray1[i] > 0)
                {
                    item = (DataGridViewTextBoxCellEx)grd[i, numArray[i]];
                    item.RowSpan = numArray1[i] + 1;
                }
            }
        }


    }
}
