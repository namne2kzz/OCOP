using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OCOP.ViewModel.Catalog.HoSos
{
    public class HoSoCreateRequest
    {
        public Guid AppUserId { get; set; }

        public int PhanNhomId { get; set; }

        public string TenHoSo { get; set; }

        public string TenSanPham { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile GiayDangKyYTuongSanPham { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile GiayPhuongAnKeHoachKinhDoanh { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile GiayGioiThieuBoMayToChuc { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile GiayDangKyKinhDoanh { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile GiayDieuKienSanXuat { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile GiayCongBoChatLuong { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile GiayTieuChuanSanPham { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile GiayAnToanVSTP { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile GiaySoHuuTriTue { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile GiayNguonGocNguyenLieu { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile GiayKeHoachBaoVeMT { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile GiayQLChatLuong { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile GiayHoatDongKeToan { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile GiayPhatTrienThiTruong { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile GiayCauChuyenSanPham { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile TaiLieuThanhTich { get; set; }
       
    }
}
