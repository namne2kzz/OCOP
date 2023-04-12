using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.ViewModel.Catalog.HoiDongs
{
    public class HoiDongViewModel
    {
        public Guid HoiDongId { get; set; }

        public Guid HoSoId { get; set; }

        public string TenHoiDong { get; set; }

        public string TenHoSo { get; set; }

        public int MaCapBac { get; set; }

        public string CapBac { get; set; }

        public DateTime NgayTao { get; set; }

        public DateTime NgayKetThuc { get; set; }

        public int TrangThai { get; set; }
    }
}
