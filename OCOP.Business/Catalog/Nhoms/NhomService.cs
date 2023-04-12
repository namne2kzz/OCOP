using OCOP.Data.Context;
using OCOP.ViewModel.Catalog.Nhoms;
using OCOP.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCOP.Business.Catalog.Nhoms
{
    public class NhomService :INhomService
    {
        private readonly OCOPDbContext _context;
        public NhomService(OCOPDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseResult<List<NhomViewModel>>> GetListNhomByNganh(int nganhId)
        {
            var listNhom = _context.Nhoms.Where(x => x.NganhId == nganhId).Select(x => new NhomViewModel()
            {
                NganhId = x.NganhId,
                NhomId = x.NhomId,
                TenNhom = x.TenNhom
            }).ToList();
            if (listNhom.Count == 0) return new ResponseErrorResult<List<NhomViewModel>>("Danh sách nhóm trống");
            return new ResponseSuccessResult<List<NhomViewModel>>(listNhom);
        }
    }
}
