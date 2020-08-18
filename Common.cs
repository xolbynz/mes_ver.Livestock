using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace 스마트팩토리
{
    public class Common
    {
        public static string p_strConn = "DATA SOURCE = hnic.iptime.org,21433;"
            + "INITIAL CATALOG = HW_DATA_SQL1;"
            + "PERSIST SECURITY INFO = false;"
            + "USER ID = sa;"
            + "PASSWORD = ##leezen123;";

        public static string p_sConnCom = "DATA SOURCE = 218.38.14.36;" //183.98.215.16
        + "INITIAL CATALOG = SM_FACTORY_COM;"
        + "PERSIST SECURITY INFO = false;"
        + "USER ID = smartUser;"
        + "PASSWORD =  smart/?25;";

        public static string p_sConnCom_jang = "DATA SOURCE = 218.38.14.33,14332;" //183.98.215.16
        + "INITIAL CATALOG = smartSales_Public;"
        + "PERSIST SECURITY INFO = false;"
        + "USER ID = smartUser;"
        + "PASSWORD =  wkdxj2017!@;";


        public static string p_sConn = "";
        public static string p_sConn_jang = "";
        

        public static string p_strLocation = "";

        public static string p_strRoot = "";        // Root 여부
        public static string p_strUserNo = "";     // 사원번호
        public static string p_strStaffNo = "";     // 사원번호
        public static string p_strStaffNo_jang = "";
        public static string p_strUserID = "";      // 사용자아이디
        public static string p_strUserCode = "전주소";      // 사용자코드
        public static string p_strUserMan = "전주소";      // 사용자 USER_MAN
        public static string p_strUserReg = "61";      // 사용자 주민번호
        public static string p_strUserDept = "600";      // 사용자 부서코드
        public static string p_strUserPW = "";      // 사용자암호
        public static string p_strUserName = "홍길동";    // 사용자명
        public static string p_strUserAdmin = "";   // 관리자여부
        public static string p_saupNo = ""; //사업자번호
        public static string p_strCompNm = "";

        public static string sp_code = ""; //공급업체 코드 
        public static string sp_site = ""; //공급업체 사이트 

        //public static string p_strCompID = "";      // 회사코드
        //public static string p_PopSheetPrint = "";  // 매출:거래명세서출력여부
        //public static string p_PopMoneyInput = "";  // 매출:수금입력여부
        //public static string p_PopPresPrint = "";   // 매출:견적서출력여부
        //public static string p_PopBuyPrint = "";   // 매입:발주서출력여부
        public static string p_PageSize = "100";   // 페이지사이즈

        //public static string p_AutoCust = "";   // 자동검색거래처
        //public static string p_AutoProd = "";   // 자동검색상품
        //public static string p_AutoCustCode = "";   // 자동코드거래처
        //public static string p_AutoProdCode = "";   // 자동코드상품
        //public static string p_AutoCustLength = "";   // 자동코드거래처길이
        //public static string p_AutoProdLength = "";   // 자동코드상품길이
        //public static string p_SheetBarcode = "";   // 거래명세바코드여부
        //public static string p_SheetSpecial = "";   // 거래명세특별양식
        //public static string p_InternetXLS = "";   // 인터넷몰 엑셀자료 사용여부

        ////public static string p_strSpotCode = "";    // 사업소코드
        ////public static string p_strSpotName = "";    // 사업소명
        ////public static string p_strEmplCode = "";    // 사원코드
        ////public static string p_strDeptCode = "";    // 부서코드
        ////public static string p_strPointFormat = "#,0.##"; // 소수점 경우 양식.
        public static string p_strFormatAmount = "#,0"; // 수량 양식.
        //public static string p_strFlag_Amount = "N"; // 수량 소수점여부
        public static string p_strFormatUnit = "#,0"; // 단가 양식.
        //public static string p_strFlag_Unit = "N";   // 단가 소수점여부
        //public static string p_strEgg = "N";         // 계란업여부
        //public static string p_strNoVAT = "N";       // 면세여부
        //public static string p_strBox = "3";         // 수량등록방식코드

        //public static string p_intRollLevel = "";   // 권한등급
        //public static string p_intViewFlg = "";     // 보기권한
        //public static string p_intPrintFlg = "";    // 출력권한
        //public static string p_intWriteFlg = "";    // 등록/수정권한

        //public static string p_strConnMain = "DATA SOURCE = localhost;"
        //    + "INITIAL CATALOG = HaWon;"
        //    + "PERSIST SECURITY INFO = false;"
        //    + "USER ID = sa;"
        //    + "PASSWORD = ##leezen123;";

        //public static string p_strConnMain2 = "DATA SOURCE = localhost;"
        //    + "INITIAL CATALOG = HaWon;"
        //    + "PERSIST SECURITY INFO = false;"
        //    + "USER ID = sa;"
        //    + "PASSWORD = ##leezen123;";

        public static bool isNum(string txt)
        {
            string str = txt.Trim();
            if (!Regex.IsMatch(txt.Trim(), @"^[0-9]+$"))
            {
                return false;
            }
            return true;
        }

        public static bool isNumFloat(string txt)
        {
            string str = txt.Trim();
            try
            {
                float flo = float.Parse(txt.Trim());
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static string GetKeyStringbyKeys(Keys keys)
        {
            switch (keys)
            {
                case Keys.A: return "A";
                case Keys.B: return "B";
                case Keys.C: return "C";
                case Keys.D: return "D";
                case Keys.E: return "E";
                case Keys.F: return "F";
                case Keys.G: return "G";
                case Keys.H: return "H";
                case Keys.I: return "I";
                case Keys.J: return "J";
                case Keys.K: return "K";
                case Keys.L: return "L";
                case Keys.M: return "M";
                case Keys.N: return "N";
                case Keys.O: return "O";
                case Keys.P: return "P";
                case Keys.Q: return "Q";
                case Keys.R: return "R";
                case Keys.S: return "S";
                case Keys.T: return "T";
                case Keys.U: return "U";
                case Keys.V: return "V";
                case Keys.W: return "W";
                case Keys.X: return "X";
                case Keys.Y: return "Y";
                case Keys.Z: return "Z";
                case Keys.Enter: return "~";
                case Keys.Escape: return "{ESC}";
                case Keys.Back: return "{BKSP}";
                case Keys.Pause: return "{BREAK}";
                case Keys.CapsLock: return "{CAPSLOCK}";
                case Keys.Delete: return "{DEL}";
                case Keys.End: return "{END}";
                case Keys.Help: return "{HELP}";
                case Keys.Home: return "{HOME}";
                case Keys.Insert: return "{INSERT}";
                case Keys.PageDown: return "{PGDN}";
                case Keys.PageUp: return "{PGUP}";
                case Keys.PrintScreen: return "{PRTSC}";
                case Keys.Scroll: return "{SCROLLLOCK}";
                case Keys.Tab: return "{TAB}";
                case Keys.F1: return "{F1}";
                case Keys.F2: return "{F2}";
                case Keys.F3: return "{F3}";
                case Keys.F4: return "{F4}";
                case Keys.F5: return "{F5}";
                case Keys.F6: return "{F6}";
                case Keys.F7: return "{F7}";
                case Keys.F8: return "{F8}";
                case Keys.F9: return "{F9}";
                case Keys.F10: return "{F10}";
                case Keys.F11: return "{F11}";
                case Keys.F12: return "{F12}";
                case Keys.Left: return "{LEFT}";
                case Keys.Right: return "{RIGHT}";
                case Keys.Up: return "{UP}";
                case Keys.Down: return "{DOWN}";
                case Keys.Add: return "{ADD}";
                case Keys.Subtract: return "{SUBTRACT}";
                case Keys.Multiply: return "{MULTIPLY}";
                case Keys.Divide: return "{DIVIDE}";
                case Keys.Shift: return "*";
                case Keys.Control: return "^";
                case Keys.Alt: return "%";
                default: return "";
            }
        }

    }
}
