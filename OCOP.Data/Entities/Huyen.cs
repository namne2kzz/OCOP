using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.Data.Entities
{
    public class Huyen
    {
        public int HuyenId { get; set; }

        public string TenHuyen { get; set; }

        public virtual ICollection<Xa> Xas { get; set; }
    }
}
