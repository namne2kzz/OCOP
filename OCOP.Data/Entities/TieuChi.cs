using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.Data.Entities
{
    public class TieuChi
    {
        public int TieuChiId { get; set; }

        public int PhanNhomId { get; set; }

        public string LoaiTieuChiId { get; set; }

        public string TenTieuChi { get; set; }

        public int DiemCanDuoi { get; set; }

        public int DiemCanTren { get; set; }

        public string GhiChu { get; set; }

        public virtual PhanNhom PhanNhom { get; set; }

        public virtual LoaiTieuChi LoaiTieuChi { get; set; }

        public virtual ICollection<TieuChiChiTiet> TieuChiChiTiets { get; set; }

        public virtual ICollection<HoiDongTieuChi> HoSoTieuChis { get; set; }

    }
}
