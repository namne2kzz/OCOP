using OCOP.Data.Context;
using OCOP.ViewModel.Catalog.TieuChis;
using OCOP.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCOP.Business.Catalog.TieuChis
{
    public class TieuChiService : ITieuChiService
    {
        private readonly OCOPDbContext _context;
        public TieuChiService(OCOPDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseResult<List<TieuChiViewModel>>> GetListTieuChiByHoSo(Guid hoSoId)
        {
            var hoSo = await _context.HoSos.FindAsync(hoSoId);
            var tieuChis = _context.TieuChis.Where(x => x.PhanNhomId == hoSo.PhanNhomId).ToList();
            var listTieuChiViewModel = new List<TieuChiViewModel>();
            foreach(var item in tieuChis)
            {
                var tieuChiViewModel = new TieuChiViewModel()
                {
                    PhanNhomId = item.PhanNhomId,
                    DiemCanDuoi = item.DiemCanDuoi,
                    DiemCanTren = item.DiemCanTren,
                    GhiChu = item.GhiChu,
                    LoaiTieuChiId = item.LoaiTieuChiId,
                    TenTieuChi = item.TenTieuChi,
                    TieuChiId = item.TieuChiId
                };
                tieuChiViewModel.ListTieuChiChiTiet = _context.TieuChiChiTiets.Where(x => x.TieuChiId == item.TieuChiId).Select(x => new TieuChiChiTietViewModel()
                {
                    TieuChiId=x.TieuChiId,
                    Diem=x.Diem,
                    Mota=x.Mota,
                    TieuChiChiTietId=x.TieuChiChiTietId
                }).ToList();
                listTieuChiViewModel.Add(tieuChiViewModel);
            }
            return new ResponseSuccessResult<List<TieuChiViewModel>>(listTieuChiViewModel);
        }
    }
}
