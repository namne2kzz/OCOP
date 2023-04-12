using OCOP.Data.Context;
using OCOP.ViewModel.Catalog.MoHinhSXes;
using OCOP.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCOP.Business.Catalog.MoHinhSXes
{
    public class MoHinhSXService : IMoHinhSXService
    {
        private readonly OCOPDbContext _context;
        public MoHinhSXService(OCOPDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseResult<List<MoHinhSXViewModel>>> GetListMoHinhSX()
        {
            var listMoHinhsX = _context.MoHinhSXes.Select(x => new MoHinhSXViewModel()
                {
                    MoHinhSXId=x.MoHinhSXId,
                    MoTa=x.Mota,
                    TenMoHinhSX=x.TenMoHinhSX
                }).ToList();
            return new ResponseSuccessResult<List<MoHinhSXViewModel>>(listMoHinhsX);
        }
    }
}
