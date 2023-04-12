using OCOP.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.ViewModel.Catalog.HoiDongs
{
    public class HoiDongThanhVienAssignRequest
    {
        public Guid HoiDongId { get; set; }

        public List<SelectedItem> ThanhViens { get; set; } = new List<SelectedItem>();
    }
}
