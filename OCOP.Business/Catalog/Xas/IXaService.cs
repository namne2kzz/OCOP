using OCOP.ViewModel.Catalog.Xas;
using OCOP.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OCOP.Business.Catalog.Xas
{
    public interface IXaService
    {
        Task<ResponseResult<List<XaViewModel>>> GetListXaByHuyen(int huyenId);
    }
}
