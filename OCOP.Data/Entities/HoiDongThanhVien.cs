using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.Data.Entities
{
    public class HoiDongThanhVien
    {
        public Guid HoiDongThanhVienId { get; set; }

        public Guid HoiDongId { get; set; }

        public Guid AppUserId { get; set; }

        public string DanhGia { get; set; }

        public Guid ThanhvienAppUserId
        {
            get;
            set;
        }

        public int DiemChatLuongSanPhan { get; set; }

        public int DiemKhaNangTiepThi { get; set; }

        public int DiemSucManhCongDong { get; set; }

        public virtual HoiDong HoiDong { get; set; }

        public virtual ThanhVien ThanhVien { get; set; }
    }
}
