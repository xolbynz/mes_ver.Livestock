using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using 스마트팩토리.CLS;

namespace 스마트팩토리
{
    public partial class frmMain : Form, IfrmInterface
    {
        private int nWidth = 1380;
        private int nHeight = 801;
        private Form savForm = null;
        private Form savBack = null;
        private Form savManager = null;
        private bool bManager = false;
        private wnGConstant wConst = new wnGConstant();
        private bool bDBcheck = true;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

            toolMenu.Visible = false;
            toolMenu.Items.Clear();

            this.Width = nWidth + 1;
            this.Height = nHeight + 1;
            this.Top = 1;
            this.Left = 1;

            init_FrameBorder();  // 반드시 최우선으로 실행.
            show_MainScreen();

            lblUserName.Text = "";
            lblCompName.Text = "";

            string sVersion = getAppVer();
            this.Text = this.Text + " ver " + sVersion;

            tmDelFile.Enabled = true;
            tmLogin.Enabled = true;
        }

        private string getAppVer()
        {
            string sRet = "";
            try
            {
                FileStream fs = new FileStream("check_update.ini", FileMode.Open, FileAccess.Read, FileShare.Read);
                StreamReader sr = new StreamReader(fs);

                string sTxt = "";
                sTxt = sr.ReadLine();
                sRet = sTxt.Replace("\r", "").Replace("\n", "");

                sr.Close();
                fs.Close();
            }
            catch
            {
            }
            return sRet;
        }

        private void init_FrameBorder()
        {
            //panBackLeft.SetBounds(0, panTopBorder.Height + panMenu.Height + 2, 4, this.Height);
            //panBackRight.SetBounds(this.Width - 20, panTopBorder.Height + panMenu.Height + 2, 20, this.Height);
            //panBackBottom.SetBounds(0, this.Height - 44, 2000, 20);
            panBackTop.SetBounds(0, panTopBorder.Height - 7, panTopBorder.Width + 100, 10);
            panBackLeft.SetBounds(0, panTopBorder.Height + 2, 4, this.Height);
            panBackRight.SetBounds(this.Width - 20, panTopBorder.Height + 2, 20, this.Height);
            panBackBottom.SetBounds(0, this.Height - 45, panTopBorder.Width + 100, 20);
        }

        private void show_MainScreen()
        {
            int nW = this.ClientSize.Width - 5;
            //int nH = this.ClientSize.Height - panTopBorder.Height - panMenu.Height - 7;
            int nH = this.ClientSize.Height - panTopBorder.Height - 7;

            frmBack frm = new frmBack(this as IfrmInterface);

            frm.MdiParent = this;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.SetBounds(0, 0, nW, nH);
            frm.Show();

            savBack = frm;
        }

        private void tmLogin_Tick(object sender, EventArgs e)
        {
            tmLogin.Enabled = false;

            ////bool bDBcheck = Check_DBConnection();
            show_FormCheck();
            if (bDBcheck != true)
            {
                MessageBox.Show("DB 연결에 문제가 있습니다." + "\r\n" + "또는, 사용자 컴퓨터의 인터넷 연결을 확인하세요.");
                this.Close();
            }

            //show_FormLogin();

            //// 로그인시...
            //// 상황에 따른 연결자 변경
            //Common.p_strConn = Common.p_strConnMain;

            get_Menu_Info();
            //get_Comp_Info();

            lblUserName.Text = Common.p_strUserName;
            lblCompName.Text = Common.p_strCompNm;
            //if (Common.p_strUserAdmin == "Y")
            //{
            //    tsSystem.Visible = true;
            //}

            //if (Common.p_strRoot == "Y")
            //{
            //    butSetting.Visible = true;
            //}
        }

        public void get_Menu_Info()
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_TopMenu_List();

                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int kk = 0; kk < dt.Rows.Count; kk++)
                    {
                        ToolStripDropDownButton top_01 = new ToolStripDropDownButton(dt.Rows[kk]["TopName"].ToString());
                        top_01.Font = new Font(top_01.Font, FontStyle.Bold);
                        top_01.DropDown.Font = new Font(top_01.DropDown.Font, FontStyle.Regular);
                        top_01.AutoSize = false;
                        Color colorTemp = Color.CadetBlue;
                        if (kk > 10)
                        {
                            colorTemp = Color.FromArgb(30, 30, 30);
                        }
                        else if (kk > 5)
                        {
                            colorTemp = Color.FromArgb(57, 119, 206);
                        }

                        top_01.BackColor = Color.FromArgb(250, 250, 250);
                        top_01.ForeColor = colorTemp;
                        top_01.BackgroundImageLayout = ImageLayout.None;
                        top_01.DisplayStyle = ToolStripItemDisplayStyle.Text;
                        top_01.ShowDropDownArrow = false;
                        top_01.Width = 75;
                        top_01.Height = 35;
                        top_01.MouseEnter += mainMenu_MouseEnter;
                        top_01.MouseLeave += mainMenu_MouseLeave;
                        toolMenu.Items.Add(top_01);

                        ToolStripSeparator mainSep = new ToolStripSeparator();
                        mainSep.AutoSize = false;
                        mainSep.Width = 6;
                        mainSep.Height = 10;
                        toolMenu.Items.Add(mainSep);

                        // Sub Menu 붙이기
                        get_SubMenu_Info(dt.Rows[kk]["TopID"].ToString(), top_01);
                    }
                }
            }
            catch (Exception ex)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
            }

            tsOpenWin = new ToolStripDropDownButton("열린창");
            tsOpenWin.Font = new Font(tsOpenWin.Font, FontStyle.Bold);
            tsOpenWin.DropDown.Font = new Font(tsOpenWin.DropDown.Font, FontStyle.Regular);
            tsOpenWin.AutoSize = false;
            tsOpenWin.BackColor = Color.FromArgb(250, 250, 250);
            tsOpenWin.ForeColor = Color.Black;
            tsOpenWin.BackgroundImageLayout = ImageLayout.None;
            tsOpenWin.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsOpenWin.ShowDropDownArrow = false;
            tsOpenWin.Width = 65;
            tsOpenWin.Height = 35;
            tsOpenWin.MouseEnter += mainMenu_MouseEnter;
            tsOpenWin.MouseLeave += mainMenu_MouseLeave;
            toolMenu.Items.Add(tsOpenWin);

            toolMenu.Visible = true;
        }

        public void get_SubMenu_Info(string sTopID, ToolStripDropDownButton top_Menu)
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_SubMenu_List(sTopID);

                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int kk = 0; kk < dt.Rows.Count; kk++)
                    {
                        ToolStripMenuItem subMenu = new ToolStripMenuItem(dt.Rows[kk]["SubName"].ToString());
                        subMenu.Tag = dt.Rows[kk]["AsmName"].ToString();
                        subMenu.Click += mainMemu_Click;
                        top_Menu.DropDownItems.Add(subMenu);
                    }
                }
            }
            catch (Exception ex)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
            }
        }

        public void get_Comp_Info()
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_업체정보_Detail(Common.p_strConn);

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.Text = this.Text + " - " + dt.Rows[0]["상호명"].ToString();
                    lbl업체명.Text = dt.Rows[0]["상호명"].ToString();
                }
            }
            catch (Exception ex)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
            }
        }

        //public bool Check_DBConnection()
        //{
        //    try
        //    {
        //        using (SqlConnection dbConn = new SqlConnection())
        //        {
        //            dbConn.ConnectionString = Common.p_strConnMain;
        //            dbConn.Open();
        //            dbConn.Close();
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        return false;
        //    }
        //    return true;
        //}

        private void show_FormCheck()
        {
            frmCheck frm = new frmCheck();

            frm.ShowDialog();
            bDBcheck = frm.bRet;

            frm.Dispose();
            frm = null;
        }

        private void show_FormLogin()
        {
            bool bLogin = false;

            frmLogin frm = new frmLogin();

            frm.ShowDialog();
            bLogin = frm.bRet;

            frm.Dispose();
            frm = null;

            if (bLogin == false)
            {
                this.Close();
            }
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (this.Width < nWidth)
            {
                this.Width = nWidth;
                return;
            }
            if (this.Height < nHeight)
            {
                this.Height = nHeight;
                return;
            }

            init_FrameBorder();

            int nW = this.ClientSize.Width - 5;
            //int nH = this.ClientSize.Height - panTopBorder.Height - panMenu.Height - 7;
            int nH = this.ClientSize.Height - panTopBorder.Height - 7;

            foreach (Form frm in this.MdiChildren)
            {
                frm.Width = nW;
                frm.Height = nH;
            }

            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //this.WindowState = FormWindowState.Maximized;
        }

        private void mainMemu_Click(object sender, EventArgs e)
        {
            string sFrmName = ((ToolStripMenuItem)sender).Tag.ToString();
            string sMnuName = ((ToolStripMenuItem)sender).Text.ToString();
            sub_Form(sFrmName, sMnuName);

        }
        public void sub_Form(string sFrmName, string sMemuName)
        {
            Cursor.Current = Cursors.WaitCursor;

            sFrmName = Regex.Replace(sFrmName, " ", "");
            Assembly ExAss = Assembly.GetExecutingAssembly();

            Form frmCall = (Form)ExAss.CreateInstance(string.Concat(ExAss.GetName().Name, ".", sFrmName));
            if (frmCall != null)
            {
                //foreach (ToolStripMenuItem sub in tsOpenWin.DropDownItems)
                //{
                //    //sub.BackColor = Color.White;
                //    //sub.ForeColor = Color.RoyalBlue;
                //}

                bool bExistFrm = false;
                foreach (Form frm in this.MdiChildren)
                {
                    if (frm.Name != "frmBack" && frm.Name != "frmBackRoot")
                    {
                        if (frm.Name == frmCall.Name)
                        {
                            bExistFrm = true;
                            frm.Activate();
                            foreach (ToolStripMenuItem sub in tsOpenWin.DropDownItems)
                            {
                                string sSubName = "";
                                if (sub.Tag.ToString().LastIndexOf('.') > 0)
                                {
                                    sSubName = sub.Tag.ToString().Substring(sub.Tag.ToString().LastIndexOf('.') + 1);
                                }
                                else
                                {
                                    sSubName = sub.Tag.ToString();
                                }
                                if (sSubName == frmCall.Name)
                                {
                                    //sub.BackColor = SystemColors.MenuHighlight;
                                    //sub.ForeColor = Color.Yellow;
                                    break;
                                }
                            }
                            break;
                        }

                    }
                }
                if (bExistFrm == false)
                {
                    int nW = this.ClientSize.Width - 5;
                    //int nH = this.ClientSize.Height - panTopBorder.Height - panMenu.Height - 7;
                    int nH = this.ClientSize.Height - panTopBorder.Height - 7;

                    frmCall.MdiParent = this;
                    frmCall.FormBorderStyle = FormBorderStyle.None;
                    frmCall.SetBounds(0, 0, nW, nH);
                    frmCall.FormClosed += new FormClosedEventHandler(ChildFormClosed);
                    frmCall.Tag = sFrmName;
                    frmCall.Show();

                    ToolStripMenuItem sub = new ToolStripMenuItem();
                    sub.Text = sMemuName;
                    sub.Tag = sFrmName;
                    //sub.BackColor = SystemColors.MenuHighlight;
                    //sub.ForeColor = Color.Yellow;
                    sub.Click += mainMemu_Click;
                    tsOpenWin.DropDownItems.Add(sub);

                    lstChild.Items.Add(sMemuName);
                    lstChild2.Items.Add(sFrmName);
                    lstChild_del.Items.Add("0");
                }
            }

            ExAss = null;

            Cursor.Current = Cursors.Arrow;
        }


        public void sub_Form(string sFrmName, string sMemuName, string lotno)
        {
            Cursor.Current = Cursors.WaitCursor;

            sFrmName = Regex.Replace(sFrmName, " ", "");
            Assembly ExAss = Assembly.GetExecutingAssembly();

            Form frmCall = (Form)ExAss.CreateInstance(string.Concat(ExAss.GetName().Name, ".", sFrmName));
            if (frmCall != null)
            {
                //foreach (ToolStripMenuItem sub in tsOpenWin.DropDownItems)
                //{
                //    //sub.BackColor = Color.White;
                //    //sub.ForeColor = Color.RoyalBlue;
                //}

                bool bExistFrm = false;
                foreach (Form frm in this.MdiChildren)
                {
                    if (frm.Name != "frmBack" && frm.Name != "frmBackRoot")
                    {
                        if (frm.Name == frmCall.Name)
                        {
                            bExistFrm = true;
                            frm.Activate();
                            foreach (ToolStripMenuItem sub in tsOpenWin.DropDownItems)
                            {
                                string sSubName = "";
                                if (sub.Tag.ToString().LastIndexOf('.') > 0)
                                {
                                    sSubName = sub.Tag.ToString().Substring(sub.Tag.ToString().LastIndexOf('.') + 1);
                                }
                                else
                                {
                                    sSubName = sub.Tag.ToString();
                                }
                                if (sSubName == frmCall.Name)
                                {
                                    //sub.BackColor = SystemColors.MenuHighlight;
                                    //sub.ForeColor = Color.Yellow;
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }

                if (bExistFrm == false)
                {
                    int nW = this.ClientSize.Width - 5;
                    //int nH = this.ClientSize.Height - panTopBorder.Height - panMenu.Height - 7;
                    int nH = this.ClientSize.Height - panTopBorder.Height - 7;

                    frmCall.MdiParent = this;
                    frmCall.FormBorderStyle = FormBorderStyle.None;
                    frmCall.SetBounds(0, 0, nW, nH);
                    frmCall.FormClosed += new FormClosedEventHandler(ChildFormClosed);
                    frmCall.Tag = sFrmName;
                    frmCall.Show();

                    if (frmCall.Name.Equals("frm씨지엠공정추적"))

                        try
                        {
                            foreach (Control x in frmCall.Controls)
                            {
                                if (x.Tag != null)
                                {
                                    if (((TextBox)x).Tag.ToString() == "1")
                                    {
                                        if (x.Name.Equals("txt_lotno"))
                                        {
                                            x.Text = lotno;
                                        }
                                    }
                                }
                            }
                        }
                        catch
                        {
                        }
                    else if (frmCall.Name.Equals("frm원자재투입현황"))
                    {
                        try
                        {
                            foreach (Control x in frmCall.Controls)
                            {
                                if (x.Tag != null)
                                {
                                    if (((TextBox)x).Tag.ToString() == "1")
                                    {
                                        if (x.Name.Equals("txt_lotno"))
                                        {
                                            x.Text = lotno;
                                        }
                                    }
                                }
                            }
                        }
                        catch
                        {
                        }
                    }

                    ToolStripMenuItem sub = new ToolStripMenuItem();
                    sub.Text = sMemuName;
                    sub.Tag = sFrmName;
                    //sub.BackColor = SystemColors.MenuHighlight;
                    //sub.ForeColor = Color.Yellow;
                    sub.Click += mainMemu_Click;
                    tsOpenWin.DropDownItems.Add(sub);

                    lstChild.Items.Add(sMemuName);
                    lstChild2.Items.Add(sFrmName);
                    lstChild_del.Items.Add("0");
                }
            }

            ExAss = null;

            Cursor.Current = Cursors.Arrow;
        }



        public void Open_SubForm_not_main(string sFrmName, string sMemuName)
        {
            sFrmName = Regex.Replace(sFrmName, " ", "");
            Assembly ExAss = Assembly.GetExecutingAssembly();

            Form frmCall = (Form)ExAss.CreateInstance(string.Concat(ExAss.GetName().Name, ".", sFrmName));
            if (frmCall != null)
            {

                bool bExistFrm = false;
                foreach (Form frm in this.MdiChildren)
                {
                    if (frm.Name != "frmBack" && frm.Name != "frmBackRoot")
                    {
                        if (frm.Name == frmCall.Name)
                        {
                            bExistFrm = true;
                            frm.Activate();
                            foreach (ToolStripMenuItem sub in tsOpenWin.DropDownItems)
                            {
                                string sSubName = "";
                                if (sub.Tag.ToString().LastIndexOf('.') > 0)
                                {
                                    sSubName = sub.Tag.ToString().Substring(sub.Tag.ToString().LastIndexOf('.') + 1);
                                }
                                else
                                {
                                    sSubName = sub.Tag.ToString();
                                }
                                if (sSubName == frmCall.Name)
                                {
                                    //sub.BackColor = SystemColors.MenuHighlight;
                                    //sub.ForeColor = Color.Yellow;
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }


            }

            ExAss = null;
        }

        public static ListBox lstChild = new ListBox();
        public static ListBox lstChild2 = new ListBox();
        public static ListBox lstChild_del = new ListBox();


        private void ChildFormClosed(object sender, FormClosedEventArgs e)
        {
            Form f = (Form)sender;

            tsOpenWin.DropDownItems.Clear();

            for (int kk = 0; kk < lstChild2.Items.Count; kk++)
            {
                if (lstChild2.Items[kk].ToString() == f.Tag.ToString())
                {
                    lstChild_del.Items[kk] = "1";
                }
                else
                {
                    ToolStripMenuItem sub = new ToolStripMenuItem();
                    sub.Text = lstChild.Items[kk].ToString();
                    sub.Tag = lstChild2.Items[kk].ToString();
                    //sub.BackColor = Color.White;
                    //sub.ForeColor = Color.RoyalBlue;
                    sub.Click += mainMemu_Click;
                    tsOpenWin.DropDownItems.Add(sub);
                }
            }

            int nCnt = lstChild_del.Items.Count;
            for (int kk = 0; kk < nCnt; kk++)
            {
                if (lstChild_del.Items[nCnt - 1 - kk].ToString() == "1")
                {
                    lstChild.Items.RemoveAt(nCnt - 1 - kk);
                    lstChild2.Items.RemoveAt(nCnt - 1 - kk);
                    lstChild_del.Items.RemoveAt(nCnt - 1 - kk);
                }
            }

            tmSelected.Enabled = true;
        }

        private void butSetting_Click(object sender, EventArgs e)
        {
            bool bChk = false;
            foreach (Form frm in this.MdiChildren)
            {
                if (frm.Name == "frmBackRoot")
                {
                    bChk = true;
                    frm.Activate();
                    break;
                }
            }
            if (bChk == false)
            {
                int nW = this.ClientSize.Width - 5;
                int nH = this.ClientSize.Height - panTopBorder.Height - 7;

                frmBackRoot frm = new frmBackRoot(this as IfrmInterface);

                frm.MdiParent = this;
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.SetBounds(0, 0, nW, nH);
                frm.Show();
            }
        }

        private void butExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picMain_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm.Name == "frmBack")
                {
                    frm.Activate();
                    break;
                }
            }
        }

        private void tmSelected_Tick(object sender, EventArgs e)
        {
            tmSelected.Enabled = false;

            //foreach (ToolStripMenuItem sub in tsOpenWin.DropDownItems)
            //{
            //    if (this.ActiveMdiChild != null)
            //    {
            //        if (this.ActiveMdiChild.Tag != null)
            //        {
            //            if (sub.Tag.ToString() == this.ActiveMdiChild.Tag.ToString())
            //            {
            //                sub.BackColor = SystemColors.MenuHighlight;
            //                sub.ForeColor = Color.Yellow;
            //                break;
            //            }
            //        }
            //    }
            //}
        }

        private void lbl업체명_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm.Name == "frmBack")
                {
                    frm.Activate();
                    break;
                }
            }
        }

        private void mainMenu_MouseEnter(object sender, EventArgs e)
        {
            //ToolStripDropDownButton p = (ToolStripDropDownButton)sender;
            //if (p != null)
            //{
            //    //p.ForeColor = Color.LightCoral;
            //    p.ShowDropDown();
            //}
        }

        private void mainMenu_MouseLeave(object sender, EventArgs e)
        {
            //ToolStripDropDownButton p = (ToolStripDropDownButton)sender;
            //if (p != null)
            //{
            //    p.ForeColor = Color.White;
            //}
        }

        private void tmDelFile_Tick(object sender, EventArgs e)
        {
            tmDelFile.Enabled = false;

            change_AutoUpdateFile();
        }

        private void change_AutoUpdateFile()
        {
            string p_strFolder = Path.GetDirectoryName(System.Environment.GetCommandLineArgs()[0]);

            if (File.Exists(p_strFolder + "\\" + "smartSales2r.exe") == true)
            {
                File.Delete(p_strFolder + "\\" + "smartSales2.exe");
                File.Move(p_strFolder + "\\" + "smartSales2r.exe", p_strFolder + "\\" + "smartSales2.exe");
            }
        }

    }
}
