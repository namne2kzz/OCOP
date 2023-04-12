using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.ViewModel.Catalog.HoiDongs
{
    public class HoiDongDanhGiaViewModel
    {
        public Guid HoiDongTieuChiId { get; set; }

        public Guid HoiDongId { get; set; }

        public int TieuChiId { get; set; }

        public string TenTieuChi { get; set; }

        public int DiemToiDa { get; set; }

        public decimal Diem { get; set; }

        public string GhiChu { get; set; }
    }
}
