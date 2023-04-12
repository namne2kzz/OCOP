using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.Data.Entities
{
    public class PhanNhom
    {
        public int PhanNhomId { get; set; }

        public int NhomId { get; set; }

        public string TenPhanNhom { get; set; }

        public string Mota { get; set; }

        public virtual Nhom Nhom { get; set; }

        public virtual ICollection<HoSo> HoSos { get; set; }

        public virtual ICollection<TieuChi> TieuChis { get; set; }     
    }
}
