﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;
using System.Diagnostics;

namespace DELFI_WHM.NET.FrHT.FrND
{
    public partial class frmDangNhap : DevExpress.XtraEditors.XtraForm
    {
        public frmDangNhap()
        {
            InitializeComponent();
            try
            {
                bscTaiKhoan.uText = Properties.Settings.Default.USER_NAME;
                chkNhoMK.Checked = Properties.Settings.Default.RememberPass;
                if (chkNhoMK.Checked)
                {
                    bscMatKhau.uText = Properties.Settings.Default.MatKhau;
                }
            }
            catch { }
            clRun.CheckProductVersion();
        }
        DELFI_Class.DELFIConnection cnn = new DELFI_Class.DELFIConnection(Program.sServerName, Program.sUserName, Program.SPassword, Program.sDatabaseName);
        clsRun clRun = new clsRun();
        private void btnTroLai_Click(object sender, EventArgs e)
        {
            Close();
        }
        int sl = 0;
        private void btnLuuQuyen_Click(object sender, EventArgs e)
        {
            sl++;
            Hashtable Val = new Hashtable();
            Val.Add("TAIKHOAN", bscTaiKhoan.uText);
            Val.Add("MATKHAU", DELFI_Class.EncryptAndDecrypt.Encrypt(bscMatKhau.uText, "Delfi VN"));
            DataTable dtSel = new DataTable();            
            dtSel = cnn.SelectRows(Val, "HT_NGUOIDUNG");
            try
            {
                if (dtSel.Rows.Count == 1)
                {
                    if (dtSel.Rows[0]["HOATDONG"].ToString() == "False")
                    {
                        XtraMessageBox.Show(this, "Tài khoản của bạn đã bị khóa, Vui lòng liên hệ Admin để mở khóa", "Đăng nhập chương trình", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        Properties.Settings.Default.USER_NAME = bscTaiKhoan.uText;
                        Properties.Settings.Default.USER_ID = dtSel.Rows[0]["NGUOIDUNG"].ToString();
                        Properties.Settings.Default.PER_MENU_NGANG = dtSel.Rows[0]["MENU_NGANG"].ToString();
                        Properties.Settings.Default.PER_MENU_DOC = dtSel.Rows[0]["MENU_DOC"].ToString();
                        Properties.Settings.Default.PER_PHAMVI = dtSel.Rows[0]["LINH_VUC"].ToString();
                        Properties.Settings.Default.FULL_NAME = dtSel.Rows[0]["HOTEN"].ToString();
                        Properties.Settings.Default.PER_SYSTEM = dtSel.Rows[0]["QUYEN"].ToString();
                        Properties.Settings.Default.PER_DANH_MUC = dtSel.Rows[0]["DANH_MUC"].ToString();
                        Properties.Settings.Default.SKIN_CAPTION = dtSel.Rows[0]["KIEUGIAODIEN"].ToString();
                        Properties.Settings.Default.RememberPass = chkNhoMK.Checked;
                        Properties.Settings.Default.Offline = chkOffline.Checked;
                        Program.sPathHinhAnh = dtSel.Rows[0]["PathHinhAnh"].ToString();
                        if (chkNhoMK.Checked)
                            Properties.Settings.Default.MatKhau = bscMatKhau.uText;
                        else
                            Properties.Settings.Default.MatKhau = "";
                        Properties.Settings.Default.Save();
                        DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(Properties.Settings.Default.SKIN_CAPTION);
                        this.Width = 0;
                        this.Height = 0;
                        this.Opacity = 0;
                        this.ShowInTaskbar = false;
                        //frmMain frm = new frmMain();
                        //frm.Show();

                        frmProcess frm = new frmProcess(new frmMain());
                        //this.Hide();
                        this.DialogResult = DialogResult.OK;
                        frm.Show();
                        return;
                    }
                }
            }
            catch { }
            if (sl >= 3)
            {
                XtraMessageBox.Show(this, "Bạn đã đăng nhập quá 3 lần không thành công", "Đăng nhập chương trình", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
            else
                XtraMessageBox.Show(this, "Tên người dùng hoặc mật khẩu đăng nhập không đúng", "Đăng nhập chương trình", MessageBoxButtons.OK, MessageBoxIcon.Error);
            bscTaiKhoan.Focus();
        }

        private void pictureEdit1_DoubleClick(object sender, EventArgs e)
        {
            DELFI_WHM.NET.HeThong.frmHeThong frm = new DELFI_WHM.NET.HeThong.frmHeThong(true);
            this.Hide();
            frm.Show();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            if (lbVersion.Text != cnn.Version())
            {
                Process.Start("Update.exe");
                System.Diagnostics.Process.GetCurrentProcess().Kill();
                Application.Exit();
            }
        }

        private void pictureEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}