using OCOP.ViewModel.Catalog.Nganhs;
using OCOP.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OCOP.Business.Catalog.Nganhs
{
    public interface INganhService
    {
        Task<ResponseResult<List<NganhViewModel>>> GetListNganh();
    }
}
