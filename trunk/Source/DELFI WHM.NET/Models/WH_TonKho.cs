//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DELFI_WHM.NET.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class WH_TonKho
    {
        public int ID { get; set; }
        public string QRCode { get; set; }
        public string ItemNo { get; set; }
        public string Lot { get; set; }
        public string PackageType { get; set; }
        public string PackageCode { get; set; }
        public Nullable<decimal> QRCodeQty { get; set; }
        public string BinCode { get; set; }
        public Nullable<bool> Sample { get; set; }
        public Nullable<decimal> LotQty { get; set; }
        public string Certificate { get; set; }
        public string CropYear { get; set; }
        public Nullable<bool> Exported { get; set; }
        public Nullable<int> TruckQty { get; set; }
    }
}
