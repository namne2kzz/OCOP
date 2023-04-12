using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.ViewModel.Catalog.HoSos
{
    public class HoSoViewModel
    {
        public Guid HoSoId { get; set; }

        public Guid AppUserId { get; set; }

        public string PhanNhom { get; set; }

        public string TenHoSo { get; set; }

        public string TenSanPham { get; set; }

        public DateTime NgayTao { get; set; }

        public int TrangThai { get; set; }

        public int DanhGiaHuyen { get; set; }

        public int DanhGiaTinh { get; set; }

        public int KetQua { get; set; }

        public bool IsExistHoiDongHuyen { get; set; }

        public bool IsExistHoiDongTinh { get; set; }
    }
}
