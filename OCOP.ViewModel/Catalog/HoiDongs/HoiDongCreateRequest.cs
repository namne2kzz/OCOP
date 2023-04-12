using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.ViewModel.Catalog.HoiDongs
{
    public class HoiDongCreateRequest
    {
        public Guid HoSoId { get; set; }

        public string TenHoiDong { get; set; }

        public int MaCapBac { get; set; }       
    }
}
