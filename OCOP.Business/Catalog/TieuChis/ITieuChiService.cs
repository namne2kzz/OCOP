using OCOP.ViewModel.Catalog.TieuChis;
using OCOP.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OCOP.Business.Catalog.TieuChis
{
    public interface ITieuChiService
    {
        Task<ResponseResult<List<TieuChiViewModel>>> GetListTieuChiByHoSo(Guid hoSoId);
    }
}
