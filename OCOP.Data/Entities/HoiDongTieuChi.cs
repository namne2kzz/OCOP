using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.Data.Entities
{
    public class HoiDongTieuChi
    {     
        public Guid HoiDongTieuChiId { get; set; }

        public Guid HoiDongId { get; set; }

        public int TieuChiId { get; set; }

        public decimal Diem { get; set; }

        public string GhiChu { get; set; }

        public virtual HoiDong HoiDong { get; set; }

        public virtual TieuChi TieuChi { get; set; }
    }
}
