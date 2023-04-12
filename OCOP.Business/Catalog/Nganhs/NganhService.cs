using OCOP.Data.Context;
using OCOP.ViewModel.Catalog.Nganhs;
using OCOP.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCOP.Business.Catalog.Nganhs
{
    public class NganhService : INganhService 
    {
        private readonly OCOPDbContext _context;
        public NganhService(OCOPDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseResult<List<NganhViewModel>>> GetListNganh()
        {
            var listNganh = _context.Nganhs.Select(x => new NganhViewModel()
            {
                NganhId = x.NganhId,
                TenNganh = x.TenNganh
            }).ToList();           
            if (listNganh.Count == 0)  return new ResponseErrorResult<List<NganhViewModel>>("Danh sách ngành rỗng");           
            return new ResponseSuccessResult<List<NganhViewModel>>(listNganh);
        }
    }
}
