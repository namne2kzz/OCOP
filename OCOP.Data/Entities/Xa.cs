using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.Data.Entities
{
    public class Xa
    {
        public int XaId { get; set; }

        public int HuyenId { get; set; }

        public string TenXa { get; set; }

        public virtual Huyen Huyen { get; set; }

        public virtual ICollection<NguoiDung> NguoiDungs { get; set; }
      
    }
}
