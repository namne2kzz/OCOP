using OCOP.ViewModel.Catalog.PhanNhoms;
using OCOP.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OCOP.Business.Catalog.PhanNhoms
{
    public interface IPhanNhomService
    {
        Task<ResponseResult<List<PhanNhomViewModel>>> GetListPhanNhomByNhom(int nhomId);
    }
}
