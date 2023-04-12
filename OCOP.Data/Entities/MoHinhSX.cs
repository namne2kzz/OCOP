using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.Data.Entities
{
    public class MoHinhSX
    {
        public int MoHinhSXId { get; set; }

        public string TenMoHinhSX { get; set; }

        public string Mota { get; set; }

        public virtual ICollection<NguoiDung> NguoiDungs { get; set; }
    }
}
