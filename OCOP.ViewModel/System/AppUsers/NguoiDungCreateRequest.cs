using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OCOP.ViewModel.System.AppUsers
{
    public class NguoiDungCreateRequest
    {
        public int MoHinhSXId { get; set; }

        public int XaId { get; set; }

        public string TenNhaSanXuat { get; set; }

        public string SoDangKyKinhDoanh { get; set; }

        public string DiaChiChiTiet { get; set; }

        public string TenNguoiDaiDien { get; set; }         

        [DataType(DataType.Upload)]
        public IFormFile ThumbnailImage { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
