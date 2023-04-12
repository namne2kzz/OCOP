using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.ViewModel.Catalog.HoSos
{
    public class HoSoDetailViewModel
    {
        public Guid HoSoId { get; set; }

        public Guid AppUserId { get; set; }

        public string PhanNhom { get; set; }

        public string TenHoSo { get; set; }

        public string TenSanPham { get; set; }

        public DateTime NgayTao { get; set; }

        public string GiayDangKyYTuongSanPham { get; set; }

        public string GiayPhuongAnKeHoachKinhDoanh { get; set; }

        public string GiayGioiThieuBoMayToChuc { get; set; }

        public string GiayDangKyKinhDoanh { get; set; }

        public string GiayDieuKienSanXuat { get; set; }

        public string GiayCongBoChatLuong { get; set; }

        public string GiayTieuChuanSanPham { get; set; }

        public string GiayAnToanVSTP { get; set; }

        public string GiaySoHuuTriTue { get; set; }

        public string GiayNguonGocNguyenLieu { get; set; }

        public string GiayKeHoachBaoVeMT { get; set; }

        public string GiayQLChatLuong { get; set; }

        public string GiayHoatDongKeToan { get; set; }

        public string GiayPhatTrienThiTruong { get; set; }

        public string GiayCauChuyenSanPham { get; set; }

        public string TaiLieuThanhTich { get; set; }

        public List<string> TapTinDinhKems { get; set; }
    }
}
