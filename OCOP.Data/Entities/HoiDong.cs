using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.Data.Entities
{
    public class HoiDong
    {
        public Guid HoiDongId { get; set; }

        public Guid HoSoId { get; set; }      

        public string TenHoiDong { get; set; }

        public int MaCapBac { get; set; }

        public DateTime NgayTao { get; set; }     
        
        public DateTime NgayKetThuc { get; set; }

        public int TrangThai { get; set; }

        public int DiemChatLuongSanPhan { get; set; }

        public int DiemKhaNangTiepThi { get; set; }

        public int DiemSucManhCongDong { get; set; }

        public virtual HoSo HoSo { get; set; }

        public virtual ICollection<HoiDongThanhVien> HoiDongThanhViens { get; set; }

        public virtual ICollection<HoiDongTieuChi> HoiDongTieuChis { get; set; }

    }
}
