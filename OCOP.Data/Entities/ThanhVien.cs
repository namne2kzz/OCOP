using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.Data.Entities
{
    public class ThanhVien
    {
        public Guid AppUserId { get; set; }

        public string Ten { get; set; }

        public string MaNhanVien { get; set; }

        public string DonViCongTac { get; set; }

        public int MaCapBac { get; set; }

        public virtual AppUser AppUser { get; set; }

        public virtual ICollection<HoiDongThanhVien> HoiDongThanhViens { get; set; }
    }
}
