using OCOP.ViewModel.Catalog.MoHinhSXes;
using OCOP.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OCOP.Business.Catalog.MoHinhSXes
{
    public interface IMoHinhSXService
    {
        Task<ResponseResult<List<MoHinhSXViewModel>>> GetListMoHinhSX();
    }
}
