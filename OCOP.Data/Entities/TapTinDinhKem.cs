using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.Data.Entities
{
    public class TapTinDinhKem
    {
        public int TapTinId { get; set; }

        public Guid HoSoId { get; set; }

        public string TenTapTin { get; set; }

        public virtual HoSo HoSo { get; set; }
    }
}
