using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.Data.Entities
{
    public class NguoiDung
    {
        public Guid AppUserId { get; set; }

        public int MoHinhSXId { get; set; }

        public int XaId { get; set; }

        public string TenNhaSanXuat { get; set; }

        public string SoDangKyKinhDoanh { get; set; }

        public string DiaChiChiTiet { get; set; }

        public string TenNguoiDaiDien { get; set; }

        public virtual AppUser AppUser { get; set; }

        public virtual MoHinhSX MoHinhSX { get; set; }

        public virtual Xa Xa { get; set; }

        public virtual ICollection<HoSo> HoSos { get; set; }

    }
}
