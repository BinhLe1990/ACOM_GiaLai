using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BSC_HRM.NET.Luong
{
    public partial class frmThanhToanLuongQuaATM : DevExpress.XtraEditors.XtraForm
    {
        BSC_Class.BSCConnection cnn = new BSC_HRM.NET.BSC_Class.BSCConnection(Program.sConnection);
        clsRun clsrun = new clsRun();
        string sKEY = "TONGHOPTHUNHAPATM";
        public frmThanhToanLuongQuaATM()
        {
            InitializeComponent();
            clsrun.SetValueToControl(this);
            dtg.sKEY = sKEY;
        }

        private void frmThanhToanLuongQuaATM_Load(object sender, EventArgs e)
        {
            txtThang.uValue = DateTime.Now.Month;
            txtNam.uValue = DateTime.Now.Year;
        }

        private void btnLocDuLieu_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            string sReplace10 = "";
            string sReplace11 = "";
            string sSQL = "";
            if (raLuong.SelectedIndex == 0)
            {
                sKEY = "TONGHOPTHUNHAPATM";
                sReplace10 = " Thang=" + txtThang.uValue + " AND Nam=" + txtNam.uValue;
            }
            else
            {
                if(raThang.SelectedIndex==0)
                    sReplace10 = " Thang=7 AND Nam=" + txtNam.uValue;
                else
                    sReplace10 = " Thang=12 AND Nam=" + txtNam.uValue;
                sKEY = "TONGHOPTHUNHAPATM_ABC";
            }
            sSQL = cnn.GetSQLString(sKEY);
            if (!cnn.bComboIsNull(cbxCoSo))
                sReplace11 += "COSO=N'" + cnn.sNull2String(cbxCoSo.uEditValue) + "' AND ";
            if (!cnn.bComboIsNull(cbxPhanHe))
                sReplace11 += "PhanHe=N'" + cnn.sNull2String(cbxPhanHe.uEditValue) + "' AND ";
            if (!cnn.bComboIsNull(cbxPhongBan))
                sReplace11 += "PhongBan=N'" + cnn.sNull2String(cbxPhongBan.uEditValue) + "' AND ";
            if (sReplace11 != "")
            {
                sReplace11 = sReplace11.Substring(0, sReplace11.Length - 4);
                sSQL = sSQL.Replace("1 = 1", sReplace11);
            }
            sSQL = sSQL.Replace("1 = 0", sReplace10);
            DataTable DT = cnn.DT_DataTable(sSQL);
            if (DT.Rows.Count > 0)
            {
                foreach (DataRow r in DT.Rows)
                {
                    r["HODEM"] = clsrun.LoaiBoDauTiengViet(cnn.sNull2String(r["HODEM"])).ToUpper();
                    r["TEN"] = clsrun.LoaiBoDauTiengViet(cnn.sNull2String(r["TEN"])).ToUpper();
                }
            }
            dtg.uDataSource = DT;
            foreach(DevExpress.XtraGrid.Columns.GridColumn col in dtg.Columns)
            {
                if (col.FieldName != "GhiChu")
                    col.OptionsColumn.AllowEdit = false;
            }
            this.Cursor = Cursors.Default;
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            DataTable DT = dtg.GetDataSource();
            decimal TongThucLanh = 0;
            foreach (DataRow r in DT.Rows)
            {
                TongThucLanh += cnn.sNull2Number(r["TongTien"]);
            }
            BSC_Class.Doctien doctien = new BSC_HRM.NET.BSC_Class.Doctien();
            if (raLuong.SelectedIndex == 0)
            {
                BaoCaoTK.frmViewReport frm = new BSC_HRM.NET.BaoCaoTK.frmViewReport(Application.StartupPath + @"\Report\ThanhToanLuongQuaATM.repx", DT);
                frm.sSoTienBangChu = doctien.Convert_NumtoText(TongThucLanh.ToString());
                frm.iThang = cnn.sNull2Int(txtThang.uValue);
                frm.iNam = cnn.sNull2Int(txtNam.uValue);
                frm.Show();
            }
            else
            {
                BaoCaoTK.frmViewReport frm = new BSC_HRM.NET.BaoCaoTK.frmViewReport(Application.StartupPath + @"\Report\ThanhToanPhuCapABCQuaATM.repx", DT);
                frm.sSoTienBangChu = doctien.Convert_NumtoText(TongThucLanh.ToString());
                if (raThang.SelectedIndex == 0)
                    frm.sTuThangNam = "Từ tháng 1 đến tháng 6";
                else
                    frm.sDenThangNam = "Từ tháng 7 đến tháng 12";
                frm.iNam = cnn.sNull2Int(txtNam.uValue);
                frm.Show();
            }
        }

        private void raLuong_SelectedIndexChanged(object sender, EventArgs e)
        {
            raThang.Visible = raLuong.SelectedIndex == 1;
        }
    }
}