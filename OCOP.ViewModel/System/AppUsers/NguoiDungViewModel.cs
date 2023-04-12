using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.ViewModel.System.AppUsers
{
    public class NguoiDungViewModel
    {
        public Guid AppUserId { get; set; }

        public int MoHinhSXId { get; set; }

        public string TenMoHinhSX { get; set; }

        public int HuyenId { get; set; }

        public string TenHuyen { get; set; }

        public int XaId { get; set; }

        public string TenXa { get; set; }

        public string TenNhaSanXuat { get; set; }

        public string SoDangKyKinhDoanh { get; set; }

        public string DiaChiChiTiet { get; set; }

        public string TenNguoiDaiDien { get; set; }      

        public string ImagePath { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string UserName { get; set; }

        public bool TrangThai { get; set; }
    }
}
