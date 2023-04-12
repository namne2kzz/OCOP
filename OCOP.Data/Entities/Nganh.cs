using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.Data.Entities
{
    public class Nganh
    {
        public int NganhId { get; set; }

        public string TenNganh { get; set; }

        public string Mota { get; set; }

        public virtual ICollection<Nhom> Nhoms { get; set; }
    }
}
