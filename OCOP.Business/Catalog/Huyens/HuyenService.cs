using OCOP.Data.Context;
using OCOP.ViewModel.Catalog.Huyens;
using OCOP.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCOP.Business.Catalog.Huyens
{
    public class HuyenService : IHuyenService
    {
        private readonly OCOPDbContext _context;
        public HuyenService(OCOPDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseResult<List<HuyenViewModel>>> GetListHuyen()
        {
            var listHuyen = _context.Huyens.Select(x => new HuyenViewModel()
            {
                HuyenId = x.HuyenId,
                TenHuyen=x.TenHuyen
            }).ToList();
            return new ResponseSuccessResult<List<HuyenViewModel>>(listHuyen);
        }
    }
}
