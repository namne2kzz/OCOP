using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.Data.Entities
{
    public class LoaiTieuChi
    {
        public string LoaiTieuChiId { get; set; }

        public string TenLoaiTieuChi { get; set; }

        public int DiemToiDa { get; set; }

        public virtual ICollection<TieuChi> TieuChis { get; set; }
    }
}
