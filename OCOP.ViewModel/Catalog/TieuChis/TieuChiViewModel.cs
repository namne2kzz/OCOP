using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.ViewModel.Catalog.TieuChis
{
    public class TieuChiViewModel
    {
        public int TieuChiId { get; set; }

        public int PhanNhomId { get; set; }

        public string LoaiTieuChiId { get; set; }

        public string TenTieuChi { get; set; }

        public int DiemCanDuoi { get; set; }

        public int DiemCanTren { get; set; }

        public string GhiChu { get; set; }

        public List<TieuChiChiTietViewModel> ListTieuChiChiTiet { get; set; }
    }
}
