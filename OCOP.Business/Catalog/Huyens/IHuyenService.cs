using OCOP.ViewModel.Catalog.Huyens;
using OCOP.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OCOP.Business.Catalog.Huyens
{
    public interface IHuyenService
    {
        Task<ResponseResult<List<HuyenViewModel>>> GetListHuyen();
    }
}
