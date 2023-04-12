using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.Data.Entities
{
    public class Nhom
    {
        public int NhomId { get; set; }

        public int NganhId { get; set; }

        public string TenNhom { get; set; }

        public string Mota { get; set; }

        public virtual Nganh Nganh { get; set; }

        public virtual ICollection<PhanNhom> PhanNhoms { get; set; }
    }
}
