using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.ViewModel.Catalog.TieuChis
{
    public class TieuChiMarkRequest
    {
        public Guid HoiDongId { get; set; }

        public Guid AppUserId { get; set; }

        public List<DanhGiaCreateRequest> DanhGias { get; set; } = new List<DanhGiaCreateRequest>();
    }
}
