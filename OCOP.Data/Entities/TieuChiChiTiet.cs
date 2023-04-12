using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.Data.Entities
{
    public class TieuChiChiTiet
    {
        public int TieuChiChiTietId { get; set; }

        public int TieuChiId { get; set; }

        public string Mota { get; set; }

        public int Diem { get; set; }

        public virtual TieuChi TieuChi { get; set; }
    }
}
