using Microsoft.AspNetCore.Http;
using OCOP.ViewModel.Catalog.HoSos;
using OCOP.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OCOP.Business.Catalog.HoSos
{
    public interface IHoSoService
    {
        Task<ResponseResult<List<HoSoViewModel>>> GetListHoSo();

        Task<ResponseResult<List<HoSoViewModel>>> GetListHoSoByAppuser(Guid appUserId);

        Task<ResponseResult<List<HoSoViewModel>>> GetListHoSoPending();

        Task<ResponseResult<List<HoSoViewModel>>> GetListHoSoPended();

        Task<ResponseResult<bool>> CreateHoSo(HoSoCreateRequest request);

        Task<ResponseResult<HoSoDetailViewModel>> GetHoSoDetail(Guid hoSoId);

        Task<ResponseResult<HoSoViewModel>> GetHoSo(Guid hoSoId);

        Task<ResponseResult<List<HoSoReportViewModel>>> GetReportHoSoByTrangThai();

        Task<ResponseResult<bool>> AddFileToHoSo(Guid hoSoId, IFormFile file);
    }
}
