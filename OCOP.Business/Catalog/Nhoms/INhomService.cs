using OCOP.ViewModel.Catalog.Nhoms;
using OCOP.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OCOP.Business.Catalog.Nhoms
{
    public interface INhomService
    {
        Task<ResponseResult<List<NhomViewModel>>> GetListNhomByNganh(int nganhId);
    }
}
