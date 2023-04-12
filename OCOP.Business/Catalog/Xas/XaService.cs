using OCOP.Data.Context;
using OCOP.ViewModel.Catalog.Xas;
using OCOP.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCOP.Business.Catalog.Xas
{
    public class XaService : IXaService
    {
        private readonly OCOPDbContext _context;
        public XaService(OCOPDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseResult<List<XaViewModel>>> GetListXaByHuyen(int huyenId)
        {
            var listXa = _context.Xas.Where(x => x.HuyenId == huyenId).Select(x => new XaViewModel()
            {
                HuyenId=x.HuyenId,
                TenXa=x.TenXa,
                XaId=x.XaId
            }).ToList();
            return new ResponseSuccessResult<List<XaViewModel>>(listXa);
        }
    }
}
