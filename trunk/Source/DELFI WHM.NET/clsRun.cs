using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Reflection;
using DevExpress.XtraEditors;

namespace DELFI_WHM.NET
{
    class clsRun
    {
        public clsRun()
        {
        }

        DELFI_Class.DELFIConnection cnn = new DELFI_WHM.NET.DELFI_Class.DELFIConnection(Program.sServerName, Program.sUserName, Program.SPassword, Program.sDatabaseName);
        
        public void SetValueToControl(System.Windows.Forms.Control CtrContainer)
        {
            SetConnectionToControl(CtrContainer);
            SetPermit(CtrContainer);
        }
        private void SetConnectionToControl(System.Windows.Forms.Control CtrContainer)
        {
            foreach (Control ctr in CtrContainer.Controls)
            {
                if (ctr is DELFI_User_Control.uComboBox)
                {
                    DELFI_User_Control.uComboBox cbx = (DELFI_User_Control.uComboBox)ctr;
                    cbx.BSCConn = cnn;
                }
                else if (ctr is DELFI_User_Control.uDataGrid)
                {
                    DELFI_User_Control.uDataGrid dtg = (DELFI_User_Control.uDataGrid)ctr;
                    dtg.BSCConn = cnn;
                }
                else if (ctr is DELFI_User_Control.uLop)
                {
                    DELFI_User_Control.uLop uL = (DELFI_User_Control.uLop)ctr;
                    uL.BSCConn = cnn;
                }
                else if (ctr is DELFI_User_Control.uFind)
                {
                    DELFI_User_Control.uFind uTK = (DELFI_User_Control.uFind)ctr;
                    uTK.BSCConn = cnn;
                }
                if (ctr.Controls.Count > 0) //Nếu ctr chứa đựng các control khác
                {
                    if (cnn.bUserControl(ctr))
                    {
                        continue;
                    }
                    SetConnectionToControl(ctr);
                }
            }
        }

        public void SetPermit(System.Windows.Forms.Control CtrContainer)
        {
            if (Properties.Settings.Default.USER_NAME == "Supervisor" || Properties.Settings.Default.USER_NAME == "Administrator")
                return;
            string sPermit = Properties.Settings.Default.PER_SYSTEM;
            string sTag = cnn.sNull2String(CtrContainer.Tag);
            if (sTag == "")
                return;
            if (sPermit.Contains(sTag))
            {
                string Quyen = sPermit.Substring(sPermit.IndexOf(sTag) + sTag.Length + 1, 9);
                string Q_Show = Quyen.Substring(0, 1);
                string Q_Them = Quyen.Substring(2, 1);
                string Q_Sua = Quyen.Substring(4, 1);
                string Q_Xoa = Quyen.Substring(6, 1);
                string Q_CanDoi = Quyen.Substring(8, 1);
                if (Q_Show == "1")
                {
                    SetQuyen(CtrContainer, Q_Them, Q_Sua, Q_Xoa, Q_CanDoi);
                }
            }
            else
            {
                SetQuyen(CtrContainer, "0", "0", "0", "0");
            }
        }

        private void SetQuyen(System.Windows.Forms.Control CtrContainer, string Them, string Sua, string Xoa, string CanDoi)
        {
            foreach (Control ctr in CtrContainer.Controls)
            {
                if (ctr is DevExpress.XtraEditors.SimpleButton)
                {
                    DevExpress.XtraEditors.SimpleButton btn = (DevExpress.XtraEditors.SimpleButton)ctr;
                    string sTag = cnn.sNull2String(btn.Tag);
                    if (sTag == "ADD")
                        btn.Enabled = (Them == "1" ? true : false);
                    else if(sTag == "EDIT")
                        btn.Enabled = (Sua == "1" ? true : false);
                    else if (sTag == "DEL")
                        btn.Enabled = (Xoa == "1" ? true : false);
                    else if (sTag == "CANDOI")
                        btn.Enabled = (CanDoi == "1" ? true : false);
                }
                else if (ctr is Button)
                {
                    Button btn = (Button)ctr;
                    string sTag = cnn.sNull2String(btn.Tag);
                    if (sTag == "ADD")
                        btn.Enabled = (Them == "1" ? true : false);
                    else if (sTag == "EDIT")
                        btn.Enabled = (Sua == "1" ? true : false);
                    else if (sTag == "DEL")
                        btn.Enabled = (Xoa == "1" ? true : false);
                    else if (sTag == "CANDOI")
                        btn.Enabled = (CanDoi == "1" ? true : false);
                }
                if (ctr.Controls.Count > 0) //Nếu ctr chứa đựng các control khác
                {
                    if (cnn.bUserControl(ctr))
                    {
                        continue;
                    }
                    SetQuyen(ctr, Them, Sua, Xoa, CanDoi);
                }
            }
        }

        public DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit AddReference(string Table, string DisplayMember, string ValueMemeber)
        {
            DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rilRef = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            rilRef.DataSource = cnn.SelectRows("select * from " + Table);
            rilRef.NullText = "";
            rilRef.PopupWidth = 150;
            rilRef.DisplayMember = DisplayMember;
            rilRef.ValueMember = ValueMemeber;
            return rilRef;
        }

        public DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit AddReference(DataTable dtRef, string DisplayMember, string ValueMemeber)
        {
            DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit tmp = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            tmp.DataSource = dtRef;
            tmp.NullText = "";
            tmp.PopupWidth = 150;
            tmp.DisplayMember = DisplayMember;
            tmp.ValueMember = ValueMemeber;
            return tmp;
        }
        #region Dau Viet
        private readonly string[] DauTiengViet = new string[] {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ" };
        
        public string LoaiBoDauTiengViet(string str)
        {
            //Tiến hành thay thế , lọc bỏ dấu cho chuỗi
            for (int i = 1; i < DauTiengViet.Length; i++)
            {
                for (int j = 0; j < DauTiengViet[i].Length; j++)
                    str = str.Replace(DauTiengViet[i][j], DauTiengViet[0][i - 1]);
            }
            return str;
        }
        #endregion
        public string sTime(object TuNgay, object DenNgay)
        {
            if (TuNgay == DBNull.Value && DenNgay == DBNull.Value)
                return "";
            else if (TuNgay != DBNull.Value && DenNgay == DBNull.Value)
                return "Tính từ ngày " + Convert.ToDateTime(TuNgay).ToString("dd/MM/yyyy");
            else if (TuNgay == DBNull.Value && DenNgay != DBNull.Value)
                return "Tính đến ngày " + Convert.ToDateTime(DenNgay).ToString("dd/MM/yyyy");
            else if (TuNgay != DBNull.Value && DenNgay != DBNull.Value && TuNgay != DenNgay)
                return "Từ ngày " + Convert.ToDateTime(TuNgay).ToString("dd/MM/yyyy") + " đến ngày " + Convert.ToDateTime(DenNgay).ToString("dd/MM/yyyy");
            else if (TuNgay != DBNull.Value && DenNgay != DBNull.Value && TuNgay == DenNgay)
                return "Ngày " + Convert.ToDateTime(TuNgay).ToString("dd/MM/yyyy");
            else
                return "";
        }

        /// <summary>
        /// Xếp loại học lực, Trả về kiểu string [2] với string[0] - Xếp loại HL và string[1] là iXeploai
        /// </summary>
        /// <param name="MaQuyChe"></param>
        /// <returns></returns>

        public string getServerPath()
        {
            string sPath = "";
            try
            {
                object obj = cnn.SQL_ExecuteScalar("select pathserver from ht_hethong");
                if (obj != null)
                {
                    sPath = obj.ToString();
                    if (!Directory.Exists(sPath))
                    {
                        throw new DirectoryNotFoundException();
                    }
                }
                else
                {
                    sPath = Application.StartupPath;
                }
            }
            catch (DirectoryNotFoundException di)
            {
                //if (sPath.Trim() == "")
                //{
                //    DevExpress.XtraEditors.XtraMessageBox.Show("Bạn chưa chọn đường dẫn kết nối đến thư mục Update trên máy chủ. \nBạn cần làm thao tác này để chương trình tự động cập nhật phiên bản mới.", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                //else
                //{
                //    string msg = string.Format("Chưa kết nối với máy chủ hoặc đường dẫn ({0}) không tồn tại.", sPath);
                //    DevExpress.XtraEditors.XtraMessageBox.Show(msg, di.Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                sPath = Application.StartupPath;
            }
            catch
            {
                return "";
            }
            if (sPath.PadRight(1) != @"\")
            {
                sPath += @"\";
            }
            Program.sPathServer = sPath;
            return (sPath);
        }

        public void Check_AutoUpdate()
        {
            try
            {
                string sPathServer = getServerPath();
                string path = sPathServer + "Update.exe";
                string path1 = Application.StartupPath + @"\Update.exe";


                DateTime dtServer = System.IO.File.GetLastWriteTime(path);
                DateTime dtClient = System.IO.File.GetLastWriteTime(path1);

                if (dtServer > dtClient)
                    File.Copy(path, path1, true);
            }
            catch
            { }
        }

        public void CheckProductVersion()
        {
            Check_AutoUpdate();
            bool bNew = false;
            try
            {
                string sPathServer = getServerPath();
                string sExecFile = Path.GetFileName(Application.ExecutablePath);
                string path = sPathServer + sExecFile;
                string path1 = Application.StartupPath + @"\" + sExecFile;

                string[] sFile = new string[] { "READER.NET.exe" };
                for (int i = 0; i < sFile.Length; i++)
                {
                    path = sPathServer + sFile[i];
                    path1 = Application.StartupPath + @"\" + sFile[i];

                    DateTime dtServer = System.IO.File.GetLastWriteTime(path);
                    DateTime dtClient = System.IO.File.GetLastWriteTime(path1);
                    path1 = Application.StartupPath + @"\FileCopy\";

                    if (!Directory.Exists(path1))
                        Directory.CreateDirectory(path1);
                    path1 = Application.StartupPath + @"\FileCopy\" + sFile[i];
                    if (dtServer > dtClient)
                    {
                        File.Copy(path, path1, true);
                        bNew = true;
                    }
                }
                
                //    string msg = "\nĐã có phiên bản mới. Để cập nhật phiên bản mới, bạn phải thoát khỏi chương trình và làm theo các bước sau.\n" +
                //    "Bước 1:\n" +
                //    "Bạn vào thư mục: " + Application.StartupPath + @"\FileCopy"+"\n" +
                //    "Bước 2:\n" +
                //    "Copy hết các file có trong thư mục 'FileCopy' rồi ghi đè lên thư mục: " + Application.StartupPath +
                //    "\nXin cảm ơn vì đã làm phiền bạn.\n" +
                //    "\nĐể thoát ra và cập nhật chương trình, vui lòng nhấn 'Yes'.\nĐể tiếp tục sử dụng chương trình với phiên bản này, bạn hãy nhấn 'No'.";
                //    if (bNew)
                //    {
                //        if(DevExpress.XtraEditors.XtraMessageBox.Show(msg, "Cập nhật phiên bản mới", MessageBoxButtons.YesNo) == DialogResult.Yes)
                //        {
                //            System.Diagnostics.Process.GetCurrentProcess().Kill();
                //        }
                //    }

                string msg = "Đã có phiên bản mới:\n\t\t Time build: " + System.IO.File.GetLastWriteTime(path1).ToString("dd/MM/yyyy HH:mm:ss") + ".\n";
                //msg += "Phiên bản hiện tại:\n\t\t Version: " + Application.ProductVersion + "\n\t\t Time build: " + DateOld.ToString("dd/MM/yyyy HH:mm:ss") + ".\n";
                msg += "Chọn Yes để chương trình tự động cập nhật phiên bản mới!";
                if (bNew)
                {
                    if (DevExpress.XtraEditors.XtraMessageBox.Show(msg, "Cập nhật phiên bản mới", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        System.Diagnostics.ProcessStartInfo prst = new System.Diagnostics.ProcessStartInfo(Application.StartupPath + @"\Update.exe");
                        System.Diagnostics.Process pr = new System.Diagnostics.Process();
                        pr.StartInfo = prst;
                        pr.Start();

                        System.Diagnostics.Process.GetCurrentProcess().Kill();
                    }
                }
            }
            catch (FileNotFoundException fo)
            {
                MessageBox.Show(fo.ToString(), fo.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool CheckVersionReports(string sServerFileName, string sFileName)
        {
            try
            {
                if (!System.IO.File.Exists(sFileName))
                {
                    if (System.IO.File.Exists(sServerFileName))
                    {
                        System.IO.File.Copy(sServerFileName, sFileName);
                    }
                    else
                    {
                        throw new FileNotFoundException();
                    }
                }
                else
                {
                    if (System.IO.File.Exists(sServerFileName))
                    {
                        DateTime dtServer = System.IO.File.GetLastWriteTime(sServerFileName);
                        DateTime dtClient = System.IO.File.GetLastWriteTime(sFileName);
                        if (dtServer > dtClient)
                        {
                            if (DevExpress.XtraEditors.XtraMessageBox.Show("Có mẫu báo báo cáo mới trên máy chủ.\nBạn có muốn cập nhật không? Nếu đồng ý thì chọn Yes, ngược lại chọn No.", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            {
                                System.IO.File.Copy(sServerFileName, sFileName, true);
                            }
                        }
                    }
                    else
                    {
                        throw new FileNotFoundException();
                    }
                }
            }
            catch (FileNotFoundException fo)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Không tìm thấy file: " + sServerFileName, fo.Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return (System.IO.File.Exists(sFileName));
        }

        public void ShowHelp(Form frm)
        {
            //Help.ShowHelp(frm, @"D:\help.chm", HelpNavigator.Topic, "Bổ xung lý lịch trích ngang");
            
        }
        
        public PictureEdit GetHinhAnh(string sFileName)
        {
            PictureEdit pic = new PictureEdit();
            try
            {
                pic.Image = System.Drawing.Image.FromFile(sFileName);
                return pic;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        

        public PictureEdit GetAnhSV(string MaSinhVien)
        {
            string sPathServer = getServerPath();
            string path = sPathServer + @"\HinhAnh\";
            string path1 = Application.StartupPath + @"\HinhAnh\";

            if (!Directory.Exists(path1))
                Directory.CreateDirectory(path1);

            string[] Files = Directory.GetFiles(path1);
            PictureEdit pic = new PictureEdit();
            foreach (string fn in Files)
            {
                string tmp = fn.Replace(path1, "");
                if (tmp.Contains(MaSinhVien))
                {
                    if (fn.ToLower().EndsWith(".jpg") || fn.ToLower().EndsWith(".gif") ||
                        fn.ToLower().EndsWith(".png") || fn.ToLower().EndsWith(".bmp") ||
                        fn.ToLower().EndsWith(".jpeg"))
                    {
                        pic.Image = System.Drawing.Image.FromFile(fn);
                        return pic;
                    }
                }
            }
            string[] FilesServer;
            try
            {
                FilesServer = Directory.GetFiles(path);
            }
            catch (System.Exception ex)
            {
                return null;
            }
            foreach (string fn in FilesServer)
            {
                string tmp = fn.Replace(path, "");
                if (tmp.Contains(MaSinhVien))
                {
                    if (fn.ToLower().EndsWith(".jpg") || fn.ToLower().EndsWith(".gif") ||
                        fn.ToLower().EndsWith(".png") || fn.ToLower().EndsWith(".bmp") ||
                        fn.ToLower().EndsWith(".jpeg"))
                    {
                        path1 += tmp;
                        File.Copy(fn, path1, true);
                        pic.Image = System.Drawing.Image.FromFile(fn);
                        return pic;
                    }
                }
            }
            return pic;
        }
        public byte[] GetAnhSVConvertToByte(string MaSinhVien)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            byte[] arrPic;
            PictureEdit picHinhAnh = new PictureEdit();
            picHinhAnh = GetAnhSV(MaSinhVien);
            if (picHinhAnh == null)
            {
                return new byte[0];
            }
            if (picHinhAnh.Image != null)
            {
                System.Drawing.Imaging.ImageFormat imgft = picHinhAnh.Image.RawFormat;
                picHinhAnh.Image.Save(ms, imgft);
                arrPic = ms.GetBuffer();
            }
            else
            {
                arrPic = new byte[0];
            }
            return arrPic;
        }
    }
}