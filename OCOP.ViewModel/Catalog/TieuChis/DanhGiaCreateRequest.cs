using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.ViewModel.Catalog.TieuChis
{
    public class DanhGiaCreateRequest
    {
        public int TieuChiId { get; set; }

        public int Diem { get; set; }

        public string GhiChu { get; set; }
    }
}
