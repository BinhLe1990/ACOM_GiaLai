using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace DELFI_WHM.NET.Luong
{
    public partial class frmPhuCapGioPhuTroi : DevExpress.XtraEditors.XtraForm
    {
        DELFI_Class.DELFIConnection cnn = new DELFI_WHM.NET.DELFI_Class.DELFIConnection(Program.sConnection);
        clsRun clsrun = new clsRun();
        string sKEY = "TONGHOPPHUCAPGIOPHUTROI";
        public frmPhuCapGioPhuTroi()
        {
            InitializeComponent();
            clsrun.SetValueToControl(this);
            dtg.sKEY = sKEY;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmPhuCapGioPhuTroi_Load(object sender, EventArgs e)
        {
            txtThang.uValue = DateTime.Now.Month;
            txtNam.uValue = DateTime.Now.Year;
        }

        private void btnLocDuLieu_Click(object sender, EventArgs e)
        {
            string Replace1 = "", Replace2 = "";
            if (!cnn.bComboIsNull(cbxCoSo))
                Replace1 += " AND COSO='" + cnn.sNull2String(cbxCoSo.uEditValue) + "'";
            if (!cnn.bComboIsNull(cbxPhongBan))
                Replace1 += " AND PHONGBAN='" + cnn.sNull2String(cbxPhongBan.uEditValue) + "'";
            if (!cnn.bComboIsNull(cbxPhanHe))
                Replace1 += " AND PHANHE='" + cnn.sNull2String(cbxPhanHe.uEditValue) + "'";
            Replace2 = " Thang=" + txtThang.uValue + " AND Nam=" + txtNam.uValue;
            string sSQL = cnn.GetSQLString(sKEY);
            sSQL = sSQL.Replace("1 = 0", Replace2);
            sSQL = sSQL.Replace("AND 1 = 1", Replace1);
            DataTable dt = cnn.DT_DataTable(sSQL);
            dtg.uDataSource = dt;
            foreach (DevExpress.XtraGrid.Columns.GridColumn col in dtg.Columns)
            {
                if (col.FieldName != "SoNgayLamViecTieuChuan" && col.FieldName != "SoNgayLamViecThucTe" && col.FieldName != "SoNgayTrucCa3ThucTe")
                    col.OptionsColumn.AllowEdit = false;
            }
        }

        private void txtDonGiaNgayCong_uValidated(object sender, EventArgs e)
        {
            if (txtDonGiaNgayCong.uText.Trim().Length > 0)
            {
                DataTable dt = dtg.GetDataSource();
                foreach (DataRow r in dt.Rows)
                {
                    r["DonGiaNgayCong"] = cnn.sNull2Number(txtDonGiaNgayCong.uText);
                }
            }
        }

        private void txtDonGiaNgayCongCa3_uValidated(object sender, EventArgs e)
        {
            if (txtDonGiaNgayCongCa3.uText.Trim().Length > 0)
            {
                DataTable dt = dtg.GetDataSource();
                foreach (DataRow r in dt.Rows)
                {
                    r["DonGiaNgayCongCa3"] = cnn.sNull2Number(txtDonGiaNgayCongCa3.uText);
                }
            }
        }

        private void txtSoNgayTieuChuan_uValidated(object sender, EventArgs e)
        {
            if (txtSoNgayTieuChuan.uText.Trim().Length > 0)
            {
                DataTable dt = dtg.GetDataSource();
                foreach (DataRow r in dt.Rows)
                {
                    r["SoNgayLamViecTieuChuan"] = cnn.sNull2Int(txtSoNgayTieuChuan.uText);
                }
            }
        }

        private void dtg_uCellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                dtg.CurRow["ThucLanh"] = (cnn.sNull2Int(dtg.CurRow["SoNgayLamViecThucTe"]) + cnn.sNull2Int(dtg.CurRow["SoNgayTrucCa3ThucTe"]) - cnn.sNull2Int(dtg.CurRow["SoNgayLamViecTieuChuan"])) * cnn.sNull2Number(dtg.CurRow["DonGiaNgayCong"]) + (cnn.sNull2Number(dtg.CurRow["DonGiaNgayCongCa3"]) - cnn.sNull2Number(dtg.CurRow["DonGiaNgayCong"])) * cnn.sNull2Int(dtg.CurRow["SoNgayTrucCa3ThucTe"]);
            }
            catch { }
        }

        private void btnLuuPhuCap_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = dtg.GetDataSource();
                int Thang = (int)txtThang.uValue, Nam = (int)txtNam.uValue;
                foreach (DataRow r in dt.Rows)
                {
                    int NhanSu = cnn.sNull2Int(r["NhanSu"]);
                    int Exists = cnn.sNull2Int(cnn.SQL_ExecuteScalar("SELECT COUNT(*) FROM NS_BangPhuCapGioPhuTroi WHERE Thang=" + Thang + " AND Nam=" + Nam + " AND NhanSu=" + NhanSu));
                    Hashtable Val = new Hashtable();
                    if (Exists > 0)
                    {
                        Val.Add("SoNgayLamViecTieuChuan", cnn.sNull2Int(r["SoNgayLamViecTieuChuan"]));
                        Val.Add("SoNgayLamViecThucTe", cnn.sNull2Int(r["SoNgayLamViecThucTe"]));
                        Val.Add("SoNgayTrucCa3ThucTe", cnn.sNull2Int(r["SoNgayTrucCa3ThucTe"]));
                        Val.Add("DonGiaNgayCong", cnn.sNull2Number(r["DonGiaNgayCong"]));
                        Val.Add("DonGiaNgayCongCa3", cnn.sNull2Number(r["DonGiaNgayCongCa3"]));
                        Val.Add("ThucLanh", cnn.sNull2Number(r["ThucLanh"]));
                        Hashtable Cond = new Hashtable();
                        Cond.Add("Thang", Thang);
                        Cond.Add("Nam", Nam);
                        Cond.Add("NhanSu", NhanSu);
                        cnn.UpdateRows(Val, Cond, "NS_BangPhuCapGioPhuTroi");
                    }
                    else
                    {
                        Val.Add("Thang", Thang);
                        Val.Add("Nam", Nam);
                        Val.Add("NhanSu", NhanSu);
                        Val.Add("SoNgayLamViecTieuChuan", cnn.sNull2Int(r["SoNgayLamViecTieuChuan"]));
                        Val.Add("SoNgayLamViecThucTe", cnn.sNull2Int(r["SoNgayLamViecThucTe"]));
                        Val.Add("SoNgayTrucCa3ThucTe", cnn.sNull2Int(r["SoNgayTrucCa3ThucTe"]));
                        Val.Add("DonGiaNgayCong", cnn.sNull2Number(r["DonGiaNgayCong"]));
                        Val.Add("DonGiaNgayCongCa3", cnn.sNull2Number(r["DonGiaNgayCongCa3"]));
                        Val.Add("ThucLanh", cnn.sNull2Number(r["ThucLanh"]));
                        cnn.InsertNewRow(Val, "NS_BangPhuCapGioPhuTroi");
                    }
                }
                XtraMessageBox.Show("Cập nhật phụ cấp giờ phụ trội thành công", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            DataTable DT = dtg.GetDataSource();
            decimal TongThucLanh = 0;
            foreach (DataRow r in DT.Rows)
            {
                TongThucLanh += cnn.sNull2Number(r["ThucLanh"]);
            }
            BaoCaoTK.frmViewReport frm = new DELFI_WHM.NET.BaoCaoTK.frmViewReport(Application.StartupPath + @"\Report\BangPhuCapGioPhuTroi.repx", DT);
            DELFI_Class.Doctien doctien = new DELFI_WHM.NET.DELFI_Class.Doctien();
            frm.sSoTienBangChu = doctien.Convert_NumtoText(TongThucLanh.ToString());
            frm.iThang = cnn.sNull2Int(txtThang.uValue);
            frm.iNam = cnn.sNull2Int(txtNam.uValue);
            frm.Show();
        }
    }
}