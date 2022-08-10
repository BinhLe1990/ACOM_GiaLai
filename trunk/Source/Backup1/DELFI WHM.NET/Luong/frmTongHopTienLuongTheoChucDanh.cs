﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace DELFI_WHM.NET.Luong
{
    public partial class frmTongHopTienLuongTheoChucDanh : DevExpress.XtraEditors.XtraForm
    {
        public frmTongHopTienLuongTheoChucDanh()
        {
            InitializeComponent();
            clsRun.SetValueToControl(this);
            dtg.sKEY = sKey;
        }
        DELFI_Class.DELFIConnection cnn = new DELFI_WHM.NET.DELFI_Class.DELFIConnection(Program.sConnection);
        clsRun clsRun = new clsRun();
        string sKey = "TongHopTienLuongTheoChucVu";
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTongHopTienLuongTheoChucDanh_Load(object sender, EventArgs e)
        {
            txtNam.uText = DateTime.Now.Year.ToString();
        }

        private void btnLocDanhSach_Click(object sender, EventArgs e)
        {
            string sSQL = cnn.GetSQLString(sKey);
            sSQL = sSQL.Replace("2012", cnn.sNull2String(cnn.sNull2Int(txtNam.uText)));
            dtg.uDataSource = cnn.DT_DataTable(sSQL);
        }

        private void btnInDanhSach_Click(object sender, EventArgs e)
        {
            DataTable dt = dtg.GetDataSource();
            BaoCaoTK.frmViewReport frm = new DELFI_WHM.NET.BaoCaoTK.frmViewReport(Application.StartupPath+@"\Report\DS_TongHopTienLuongTheoChucVu.repx",dt);
            frm.Show();
        }
    }
}