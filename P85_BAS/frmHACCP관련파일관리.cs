using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 스마트팩토리.CLS;

namespace 스마트팩토리.P85_BAS
{
    public partial class frmHACCP관련파일관리 : Form
    {
        FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
        private wnGConstant wConst = new wnGConstant();
        DataGridView dgvTemp = null;


        private string fileName = "";
        private string fileFullName = "";
        private string filePath = "";
        private string pathTemp = "";
        private string oldEditTemp = "";

        public frmHACCP관련파일관리()
        {
            InitializeComponent();
        }

        private void frmHACCP관련파일관리_Load(object sender, EventArgs e)
        {
            ComInfo.gridHeaderSet(HaccpGrid1);
            ComInfo.gridHeaderSet(HaccpGrid2);
            ComInfo.gridHeaderSet(HaccpGrid3);

            txt_staff_cd.Text = Common.p_strStaffNo;
            txt_staff_nm.Text = Common.p_strUserName;
            txt_lower_date.Text = DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd");
            resetSetting();
            initComboBox();
            fn_insert_list("where STAFF_CD = '" + txt_staff_cd.Text + "'  and INPUT_DATE >= '" + txt_lower_date.Text + "' and INPUT_DATE <= '" + txt_upper_date.Text + "' "
                                + ((txt_docs_srch.Text == null || txt_docs_srch.Text.ToString().Equals("")) ? "" : "and FNAME like '%" + txt_docs_srch.Text.ToString() + "%'  "));
        }

        private void initComboBox()
        {
            ComInfo comInfo = new ComInfo();
            string sqlQuery = "";

            cmb_gubun.ValueMember = "코드";
            cmb_gubun.DisplayMember = "명칭";
            sqlQuery = comInfo.queryHACCP_GUBUN();
            wConst.ComboBox_Read_NoBlank(cmb_gubun, sqlQuery);
        }

        private void btn_Change_p_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = folderBrowser.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    DirectoryInfo di = new DirectoryInfo(folderBrowser.SelectedPath + "/Haccp/");
                    DirectoryInfo di_1 = new DirectoryInfo(folderBrowser.SelectedPath + "/Haccp/물리적위해요소/");
                    DirectoryInfo di_2 = new DirectoryInfo(folderBrowser.SelectedPath + "/Haccp/생물학적위해요소/");
                    DirectoryInfo di_3 = new DirectoryInfo(folderBrowser.SelectedPath + "/Haccp/화학적위해요소/");
                    //DirectoryInfo.Exists로 폴더 존재유무 확인
                    if (di.Exists)
                    {
                        if (!di_1.Exists)
                            di_1.Create();
                        if (!di_2.Exists)
                            di_2.Create();
                        if (!di_3.Exists)
                            di_3.Create();
                    }
                    else
                    {
                        di.Create();
                        di_1.Create();
                        di_2.Create();
                        di_3.Create();
                    }
                    SetMyRootPath(folderBrowser.SelectedPath + "/Haccp/", folderBrowser.SelectedPath);
                }
            }
            catch (IOException ioex)
            {
                MessageBox.Show("파일입출력 오류 !!\n 디렉토리 권한을 확인해주세요.");
            }
        }

        private void SetMyRootPath(string rootPath, string rootPathTemp)
        {
            try
            {
                wnDm wDm = new wnDm();
                int rst = wDm.insert_Haccp_Doc_Root(rootPath, txt_staff_cd.Text);

                if (rst == 0)
                {
                    MessageBox.Show("성공적으로 등록하였습니다 !");
                    txt_root_path.Text = rootPathTemp;
                    resetSetting();
                }
                else if (rst == 1)
                {
                    MessageBox.Show("등록에 실패하였습니다.");
                }
                else if (rst == 9)
                {
                    MessageBox.Show("sql오류");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("시스템오류발생");
            }
        }

        public void ShowFileOpenDialog()
        {
            //파일오픈창 생성 및 설정
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "업로드 파일 선택";
            ofd.FileName = "Upload_File";
            ofd.Filter = "모든 파일 (*.*) | *.*";
            //파일 오픈창 로드
            DialogResult dr = ofd.ShowDialog();
            //OK버튼 클릭시
            if (dr == DialogResult.OK)
            {
                if (txt_root_path.Text.Equals("미등록"))
                {
                    MessageBox.Show("작업 경로를 먼저 등록해주시기 바랍니다");
                    return;
                }
                fileName = ofd.SafeFileName;
                fileFullName = ofd.FileName;
                filePath = fileFullName.Replace(fileName, "");

                txt_file_path.Text = fileFullName;
            }
            else if (dr == DialogResult.Cancel)
            {
                return;
            }
            return;
        }

        private void resetSetting()
        {
            try
            {
                txt_comment.Text = "";
                cmb_gubun.Text = "물리적위해요소";
                input_date.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txt_file_path.Text = "";
                fileFullName = "";
                fileName = "";
                filePath = "";
                wnDm wDm = new wnDm();
                String root = wDm.select_Haccp_Doc_Root(txt_staff_cd.Text);
                if (root.Equals("failed"))
                {
                    MessageBox.Show("sql오류발생");
                }
                else
                {
                    txt_root_path.Text = root;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("시스템오류발생");
            }
        }

        private void btn_file_srch_Click(object sender, EventArgs e)
        {
            ShowFileOpenDialog();
        }


        private void insert_file()
        {
            if (fileFullName.Equals(""))
            {
                MessageBox.Show("등록할 파일을 먼저 선택해주시기 바랍니다");
                return;
            }
            if (txt_root_path.Equals("미등록"))
            {
                MessageBox.Show("작업 경로를 먼저 등록해주시기 바랍니다");
                return;
            }
            string targetPath = @txt_root_path.Text + "/Haccp/" + cmb_gubun.GetItemText(cmb_gubun.SelectedItem).ToString();
            string sourceFile = System.IO.Path.Combine(filePath, fileName);
            string destFile = System.IO.Path.Combine(targetPath, fileName);

            if (System.IO.Directory.Exists(targetPath))
            {
                if (System.IO.File.Exists(targetPath + "/" + fileName))
                {
                    MessageBox.Show("해당 이름의 파일이 이미 존재합니다. 업로드 파일명을 수정해주세요.");
                    return;
                }
                else
                {
                    try
                    {
                        wnDm wDm = new wnDm();
                        int rst = wDm.insert_Haccp_Doc_File(targetPath + "/" + fileName, fileName, txt_staff_cd.Text, input_date.Text, txt_comment.Text, cmb_gubun.SelectedValue.ToString());
                        if (rst == 0)
                        {
                            try
                            {
                                System.IO.File.Copy(sourceFile, destFile, true);
                                if (System.IO.File.Exists(targetPath + "/" + fileName))
                                {
                                    MessageBox.Show("성공적으로 등록하였습니다 !");
                                    resetSetting();
                                    fn_insert_list("where STAFF_CD = '" + txt_staff_cd.Text + "'  and INPUT_DATE >= '" + txt_lower_date.Text + "' and INPUT_DATE <= '" + txt_upper_date.Text + "' "
                                + ((txt_docs_srch.Text == null || txt_docs_srch.Text.ToString().Equals("")) ? "" : "and FNAME like '%" + txt_docs_srch.Text.ToString() + "%'  "));
                                }
                            }
                            catch (IOException)
                            {
                                MessageBox.Show("입출력 오류 디렉터리 권한을 확인해주세요.");
                            }
                        }
                        else if (rst == 1)
                            MessageBox.Show("등록에 실패하였습니다.");
                        else if (rst == 9)
                            MessageBox.Show("sql오류");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("시스템오류");
                    }
                }
            }
            else
            {
                DirectoryInfo di = new DirectoryInfo(targetPath);
                di.Create();
                insert_file();
            }
        }


        private void fn_insert_list(string condition)
        {
            try
            {
                HaccpGrid1.Rows.Clear();
                HaccpGrid2.Rows.Clear();
                HaccpGrid3.Rows.Clear();
                wnDm wDm = new wnDm();
                DataTable dt = wDm.select_Haccp_Docs(condition);
                DataGridView dgvTemp = null;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgvTemp = selectDgv(int.Parse(dt.Rows[i]["DOC_GUBUN"].ToString()));
                    if (dgvTemp != null)
                    {
                        dgvTemp.Rows.Add();
                        dgvTemp.Rows[dgvTemp.RowCount - 1].Cells[0].Value = dt.Rows[i]["INPUT_DATE"].ToString();
                        dgvTemp.Rows[dgvTemp.RowCount - 1].Cells[1].Value = dt.Rows[i]["STAFF_NM"].ToString();
                        dgvTemp.Rows[dgvTemp.RowCount - 1].Cells[2].Value = dt.Rows[i]["COMMENT"].ToString();
                        dgvTemp.Rows[dgvTemp.RowCount - 1].Cells[3].Value = dt.Rows[i]["FNAME"].ToString();
                        dgvTemp.Rows[dgvTemp.RowCount - 1].Cells[5].Value = dt.Rows[i]["INPUT_CD"].ToString();
                        if (!System.IO.File.Exists(dt.Rows[i]["DOCPATH"].ToString()))
                        {
                            dgvTemp.Rows[dgvTemp.RowCount - 1].DefaultCellStyle.BackColor = Color.Red;
                            dgvTemp.Rows[dgvTemp.RowCount - 1].DefaultCellStyle.ForeColor = Color.White;
                            dgvTemp.Rows[dgvTemp.RowCount - 1].Cells[3].Value += "(MISSING)";
                        }
                        dgvTemp.Rows[dgvTemp.RowCount - 1].Cells[4].Value = dt.Rows[i]["DOCPATH"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("DB정보 오류발생 : 유형구분 값이 비정상적입니다");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("시스템오류발생");
                MessageBox.Show(ex.ToString());
            }
        }

        private DataGridView selectDgv(int caseNum)
        {
            switch (caseNum)
            {
                case 1:
                    return HaccpGrid1;
                case 2:
                    return HaccpGrid2;
                case 3:
                    return HaccpGrid3;
            }
            return null;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            insert_file();
        }

        private void btn_srch1_Click(object sender, EventArgs e)
        {
            fn_insert_list("where STAFF_CD = '" + txt_staff_cd.Text + "'  and INPUT_DATE >= '" + txt_lower_date.Text + "' and INPUT_DATE <= '" + txt_upper_date.Text + "' "
                                + ((txt_docs_srch.Text == null || txt_docs_srch.Text.ToString().Equals("")) ? "" : "and FNAME like '%" + txt_docs_srch.Text.ToString() + "%'  "));
        }


        private void delete_logic(string path)
        {
            ComInfo comInfo = new ComInfo();
            DialogResult msgOk = comInfo.warningMessage(path + "\n 파일이 실제경로에 존재하기 않거나 손실되었습니다.\n 삭제하시겠습니까?");

            if (msgOk == DialogResult.No)
            {
                return;
            }
            else
            {
                delete_Haccp_Doc(path, txt_staff_cd.Text);
            }
        }

        private void delete_Haccp_Doc(string path, string staff_cd)
        {
            wnDm wDm = new wnDm();
            if (System.IO.File.Exists(@path))
            {
                try
                {
                    System.IO.File.Delete(@path);
                }
                catch (System.IO.IOException e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }
            }
            int rst = wDm.Delete_Haccp_Doc(path, staff_cd);
            if (rst == 0)
            {
                MessageBox.Show("성공적으로 삭제했습니다");
                fn_insert_list("where STAFF_CD = '" + txt_staff_cd.Text + "'  and INPUT_DATE >= '" + txt_lower_date.Text + "' and INPUT_DATE <= '" + txt_upper_date.Text + "' "
                                + ((txt_docs_srch.Text == null || txt_docs_srch.Text.ToString().Equals("")) ? "" : "and FNAME like '%" + txt_docs_srch.Text.ToString() + "%'  "));
            }
            else if (rst == 1)
                MessageBox.Show("삭제에 실패했습니다.");
            else if (rst == 9)
                MessageBox.Show("sql오류 발생");
        }



        private void HaccpGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            DataGridView dgvTemp = (DataGridView)sender;
            string path = dgvTemp.Rows[e.RowIndex].Cells[4].Value.ToString();
            if (System.IO.File.Exists(path))
            {
                System.Diagnostics.Process.Start(path);
            }
            else
            {
                delete_logic(path);
            }
        }


        private void HaccpGrid_MouseClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                dgvTemp = (DataGridView)sender;

                int currentMouseOverRow = dgvTemp.HitTest(e.X, e.Y).RowIndex;
                int currentMouseOverColumn = dgvTemp.HitTest(e.X, e.Y).ColumnIndex;
                if (currentMouseOverRow >= 0)
                {
                    EventHandler eh = new EventHandler(MenuClick);
                    ContextMenu m = new ContextMenu();
                    m.MenuItems.Add(new MenuItem("수정", eh));
                    m.MenuItems.Add(new MenuItem("삭제", eh));
                    dgvTemp.CurrentCell = dgvTemp.Rows[currentMouseOverRow].Cells[currentMouseOverColumn];
                    m.Show(dgvTemp, new Point(e.X, e.Y));
                }
            }
        }

        void MenuClick(object obj, EventArgs ea)
        {
            MenuItem mI = (MenuItem)obj;
            String str = mI.Text;

            if (str == "수정")
            {
                if (dgvTemp.CurrentCell.ColumnIndex == 1)
                {
                    MessageBox.Show("등록사원은 수정할 수 없습니다.");
                    return;
                }
                if (dgvTemp.CurrentCell.ColumnIndex == 3)
                {
                    dgvTemp.Rows[dgvTemp.CurrentCell.RowIndex].Cells[dgvTemp.CurrentCell.ColumnIndex].Value =
                        dgvTemp.Rows[dgvTemp.CurrentCell.RowIndex].Cells[dgvTemp.CurrentCell.ColumnIndex].Value.ToString().Replace("(MISSING)", "");
                }
                pathTemp = dgvTemp.Rows[dgvTemp.CurrentCell.RowIndex].Cells[4].Value.ToString();
                oldEditTemp = dgvTemp.Rows[dgvTemp.CurrentCell.RowIndex].Cells[dgvTemp.CurrentCell.ColumnIndex].Value.ToString();
                dgvTemp.ReadOnly = false;
                dgvTemp.Rows[dgvTemp.CurrentCell.RowIndex].Cells[dgvTemp.CurrentCell.ColumnIndex].ReadOnly = false;
                dgvTemp.BeginEdit(true);

            }
            if (str == "삭제")
            {
                ComInfo comInfo = new ComInfo();
                DialogResult msgOk = comInfo.warningMessage("정말로 삭제하시겠습니까?");

                if (msgOk == DialogResult.No)
                {
                    return;
                }
                else
                {
                    delete_Haccp_Doc(dgvTemp.Rows[dgvTemp.CurrentCell.RowIndex].Cells[4].Value.ToString(), txt_staff_cd.Text);
                }
            }
        }



        private void HaccpGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dgvTemp.ReadOnly = true;
            dgvTemp.BeginEdit(false);
            dgvTemp.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;

            if (dgvTemp.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
            {
                dgvTemp.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
            }
            if (dgvTemp.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Equals(oldEditTemp))
            {
                dgvTemp.BeginInvoke(new MethodInvoker(() =>
                {
                    fn_insert_list("where STAFF_CD = '" + txt_staff_cd.Text + "'  and INPUT_DATE >= '" + txt_lower_date.Text + "' and INPUT_DATE <= '" + txt_upper_date.Text + "' "
                                + ((txt_docs_srch.Text == null || txt_docs_srch.Text.ToString().Equals("")) ? "" : "and FNAME like '%" + txt_docs_srch.Text.ToString() + "%'  "));
                }));
                return;
            }
            if (e.ColumnIndex == 0)
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(dgvTemp.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), "[0-9]{4}-[0-9]{2}-[0-9]{2}") || dgvTemp.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Length != 10)
                {
                    MessageBox.Show("날짜입력양식이 형식과 맞지 않습니다.\n올바른 양식 : YYYY-MM-DD");
                    dgvTemp.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = pathTemp;
                    dgvTemp.BeginInvoke(new MethodInvoker(() =>
                    {
                        fn_insert_list("where STAFF_CD = '" + txt_staff_cd.Text + "'  and INPUT_DATE >= '" + txt_lower_date.Text + "' and INPUT_DATE <= '" + txt_upper_date.Text + "' "
                                + ((txt_docs_srch.Text == null || txt_docs_srch.Text.ToString().Equals("")) ? "" : "and FNAME like '%" + txt_docs_srch.Text.ToString() + "%'  "));
                    }));
                    return;
                }
            }



            try
            {
                wnDm wDm = new wnDm();

                int rst = -1;

                switch (e.ColumnIndex)
                {
                    case 0:
                        rst = wDm.Update_Haccp_Docs("INPUT_DATE", dgvTemp.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), pathTemp, txt_staff_cd.Text);
                        break;
                    case 2:
                        rst = wDm.Update_Haccp_Docs("COMMENT", dgvTemp.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), pathTemp, txt_staff_cd.Text);
                        break;
                    case 3:
                        rst = wDm.Update_Haccp_Docs("FNAME", dgvTemp.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), pathTemp, txt_staff_cd.Text);
                        break;
                    case 4:
                        rst = wDm.Update_Haccp_Docs("DOCPATH", dgvTemp.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), pathTemp, txt_staff_cd.Text);
                        break;
                }
                if (rst == 0)
                {

                    MessageBox.Show("성공적으로 수정하였습니다!");
                    dgvTemp.BeginInvoke(new MethodInvoker(() =>
                    {
                        fn_insert_list("where STAFF_CD = '" + txt_staff_cd.Text + "'  and INPUT_DATE >= '" + txt_lower_date.Text + "' and INPUT_DATE <= '" + txt_upper_date.Text + "' "
                                + ((txt_docs_srch.Text == null || txt_docs_srch.Text.ToString().Equals("")) ? "" : "and FNAME like '%" + txt_docs_srch.Text.ToString() + "%'  "));
                    }));

                    return;


                }
                else if (rst == 1)
                {
                    MessageBox.Show("수정에 실패하였습니다.");
                    dgvTemp.BeginInvoke(new MethodInvoker(() =>
                    {
                        fn_insert_list("where STAFF_CD = '" + txt_staff_cd.Text + "'  and INPUT_DATE >= '" + txt_lower_date.Text + "' and INPUT_DATE <= '" + txt_upper_date.Text + "' "
                                + ((txt_docs_srch.Text == null || txt_docs_srch.Text.ToString().Equals("")) ? "" : "and FNAME like '%" + txt_docs_srch.Text.ToString() + "%'  "));
                    }));
                }
                else if (rst == 9)
                {
                    MessageBox.Show("데이터베이스 오류발생");
                    dgvTemp.BeginInvoke(new MethodInvoker(() =>
                    {
                        fn_insert_list("where STAFF_CD = '" + txt_staff_cd.Text + "'  and INPUT_DATE >= '" + txt_lower_date.Text + "' and INPUT_DATE <= '" + txt_upper_date.Text + "' "
                                + ((txt_docs_srch.Text == null || txt_docs_srch.Text.ToString().Equals("")) ? "" : "and FNAME like '%" + txt_docs_srch.Text.ToString() + "%'  "));
                    }));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("시스템오류발생");
                dgvTemp.BeginInvoke(new MethodInvoker(() =>
                {
                    fn_insert_list("where STAFF_CD = '" + txt_staff_cd.Text + "'  and INPUT_DATE >= '" + txt_lower_date.Text + "' and INPUT_DATE <= '" + txt_upper_date.Text + "' "
                                + ((txt_docs_srch.Text == null || txt_docs_srch.Text.ToString().Equals("")) ? "" : "and FNAME like '%" + txt_docs_srch.Text.ToString() + "%'  "));
                }));
            }
        }

        private void btnAllSave_Click(object sender, EventArgs e)
        {
            if (txt_root_path.Text.Equals("미등록"))
            {
                MessageBox.Show("작업 경로를 먼저 등록해주시기 바랍니다");
                return;
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("작업경로 : " + txt_root_path.Text + "/Haccp/");
            sb.AppendLine("하위 3개 디렉터리");
            sb.AppendLine("내부의 모든 파일을 일괄 등록합니다.");
            sb.AppendLine("");
            sb.AppendLine("정말로 등록하시겠습니까?");
            sb.AppendLine("");
            sb.AppendLine("***작성일자는 현재 날짜로 등록됩니다***");
            sb.AppendLine("(이미 등록되었거나 이름이 중복되었다면 스킵합니다.)");
            ComInfo comInfo = new ComInfo();
            DialogResult msgOk = comInfo.warningMessage(sb.ToString());

            if (msgOk == DialogResult.No)
            {
                return;
            }
            else
            {
                pathAllScan();
            }
        }

        private void pathAllScan()
        {
            if (System.IO.Directory.Exists(txt_root_path.Text + "/Haccp/"))
            {
                if (System.IO.Directory.Exists(txt_root_path.Text + "/Haccp/물리적위해요소/") && System.IO.Directory.Exists(txt_root_path.Text + "/Haccp/생물학적위해요소/")
                    && System.IO.Directory.Exists(txt_root_path.Text + "/Haccp/화학적위해요소/"))
                {
                    ArrayList errorFailed = new ArrayList();
                    ArrayList SuccessList = new ArrayList();
                    ArrayList SkipList = new ArrayList();
                    ArrayList folderPool = new ArrayList();
                    folderPool.Add("/Haccp/물리적위해요소/");
                    folderPool.Add("/Haccp/생물학적위해요소/");
                    folderPool.Add("/Haccp/화학적위해요소/");

                    string[] files = null;
                    int rst = -1;
                    try
                    {
                        foreach (string f in folderPool)
                        {
                            files = System.IO.Directory.GetFiles(txt_root_path.Text + f);
                            foreach (string s in files)
                            {
                                if (System.IO.File.Exists(s))
                                {
                                    wnDm wDm = new wnDm();
                                    rst = wDm.insert_Haccp_Doc_File(s, s.Split('/')[s.Split('/').Length - 1], txt_staff_cd.Text, DateTime.Today.ToString("yyyy-MM-dd"), "",
                                        (folderPool.IndexOf(f) + 1) + "");
                                    if (rst == 0)
                                        SuccessList.Add(s);
                                    else if (rst == 7)
                                        SkipList.Add(s);
                                    else
                                        errorFailed.Add(s);
                                }
                            }
                        }

                        if (errorFailed.Count == 0)
                        {
                            StringBuilder sb = new StringBuilder();
                            if (errorFailed.Count == 0)
                                sb.AppendLine("모든 파일이 성공적으로 등록되었습니다");
                            else
                                sb.AppendLine("파일이 등록되었지만 몇개 파일은 누락되었습니다.");

                            if (SuccessList.Count > 0)
                            {
                                sb.AppendLine("\n");
                                sb.AppendLine("******등록 파일******");
                                for (int e = 0; e < SuccessList.Count; e++)
                                {
                                    sb.AppendLine(SuccessList[e].ToString());
                                }
                            }
                            if (SkipList.Count > 0)
                            {
                                sb.AppendLine("\n");
                                sb.AppendLine("******스킵 파일(already exist)******");
                                for (int e = 0; e < SkipList.Count; e++)
                                {
                                    sb.AppendLine(SkipList[e].ToString());
                                }
                            }
                            if (errorFailed.Count > 0)
                            {
                                sb.AppendLine("\n");
                                sb.AppendLine("******누락 파일******");
                                for (int e = 0; e < errorFailed.Count; e++)
                                {
                                    sb.AppendLine(errorFailed[e].ToString());
                                }
                            }
                            sb.AppendLine("\n");
                            MessageBox.Show(sb.ToString());
                            fn_insert_list("where STAFF_CD = '" + txt_staff_cd.Text + "'  and INPUT_DATE >= '" + txt_lower_date.Text + "' and INPUT_DATE <= '" + txt_upper_date.Text + "' " 
                                + ((txt_docs_srch.Text == null || txt_docs_srch.Text.ToString().Equals("")) ? "" : "and FNAME like '%" + txt_docs_srch.Text.ToString() + "%'  "));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("시스템오류");
                        MessageBox.Show(ex.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("작업경로 하위에 \"물리적위해요소\", \"생물학적위해요소\", \"화학적위해요소\" 폴더 중 하나이상이 누락되었습니다.(확인요망)\n작업경로를 재설정하는 것을 권장합니다.");
                    return;
                }
            }
            else
            {
                MessageBox.Show("작업경로에 해당하는 폴더가 없습니다.\n작업경로를 재설정하는 것을 권장합니다.");
                return;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
