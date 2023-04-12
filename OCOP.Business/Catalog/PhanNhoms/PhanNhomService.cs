using OCOP.Data.Context;
using OCOP.ViewModel.Catalog.PhanNhoms;
using OCOP.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCOP.Business.Catalog.PhanNhoms
{
    public class PhanNhomService : IPhanNhomService
    {
        private readonly OCOPDbContext _context;
        public PhanNhomService(OCOPDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseResult<List<PhanNhomViewModel>>> GetListPhanNhomByNhom(int nhomId)
        {
            var listPhanNhom = _context.PhanNhoms.Where(x => x.NhomId == nhomId).Select(x => new PhanNhomViewModel()
            {
                NhomId = x.NhomId,
                PhanNhomId = x.PhanNhomId,
                TenPhanNhom = x.TenPhanNhom
            }).ToList();
            if (listPhanNhom.Count == 0) return new ResponseErrorResult<List<PhanNhomViewModel>>("Danh sách phân nhóm trống");
            return new ResponseSuccessResult<List<PhanNhomViewModel>>(listPhanNhom);
        }
    }
}
