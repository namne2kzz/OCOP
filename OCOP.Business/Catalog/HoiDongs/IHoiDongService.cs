using OCOP.Data.Entities;
using OCOP.ViewModel.Catalog.HoiDongs;
using OCOP.ViewModel.Catalog.TieuChis;
using OCOP.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OCOP.Business.Catalog.HoiDongs
{
    public interface IHoiDongService
    {
        Task<ResponseResult<bool>> CreateHoiDong(HoiDongCreateRequest request);

        Task<ResponseResult<bool>> CheckHoSoAnyHoiDong(Guid hoSoId, int maCapBac);

        Task<ResponseResult<List<HoiDongViewModel>>> GetListHoiDong();

        Task<ResponseResult<bool>> AddThanhVien(HoiDongThanhVienAssignRequest request);

        Task<ResponseResult<HoiDongViewModel>> GetHoiDong(Guid hoiDongId);

        Task<ResponseResult<bool>> LockHoiDong(Guid hoiDongId);

        Task<ResponseResult<List<HoiDongViewModel>>> GetListHoiDongByAppUser(Guid appUserId);

        Task<ResponseResult<HoiDongViewModel>> GetHoiDongByHoSoAndCapBac(Guid hoSoId, int maCapBac);

        Task<ResponseResult<HoSo>> MarkHoSoByThanhVien(TieuChiMarkRequest request);

        Task<ResponseResult<List<HoiDongDanhGiaViewModel>>> GetDanhGiaByHoiDong(Guid hoiDongId);

        Task<ResponseResult<List<HoiDongReportViewModel>>> GetReportHoiDongByTrangThai();
    }
}
