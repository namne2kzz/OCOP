using OCOP.Data.Entities;
using OCOP.ViewModel.Common;
using OCOP.ViewModel.System.AppUsers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OCOP.Business.System.AppUsers
{
    public interface IAppUserService
    {
        Task<ResponseResult<string>> Authenticate(LoginRequest request);

        Task<ResponseResult<string>> AuthenticateForWebPortal(LoginRequest request);

        Task<ResponseResult<AppUser>> GetUser(Guid appUserId);

        Task<ResponseResult<bool>> CreateThanhVien(ThanhVienCreateRequest request);

        Task<ResponseResult<bool>> UpdateThanhVien(ThanhVienUpdateRequest request);

        Task<ResponseResult<bool>> DeleteThanhVien(Guid appUserId);

        Task<ResponseResult<ThanhVienViewModel>> GetThanhVien(Guid appUserId);

        Task<ResponseResult<List<ThanhVienViewModel>>> GetListThanhVien();

        Task<ResponseResult<List<ThanhVienViewModel>>> GetListThanhVienByCapBac(int maCapBac);

        Task<ResponseResult<List<string>>> GetListThanhVienInHoiDong(Guid hoiDongId);

        Task<ResponseResult<bool>> ChangeTrangThai(Guid appUserId);

        Task<ResponseResult<bool>> CreateNguoiDung(NguoiDungCreateRequest request);

        Task<ResponseResult<bool>> UpdateNguoiDung(NguoiDungUpdateRequest request);

        Task<ResponseResult<bool>> DeleteNguoiDung(Guid appUserId);

        Task<ResponseResult<NguoiDungViewModel>> GetNguoiDung(Guid appUserId);

        Task<ResponseResult<List<NguoiDungViewModel>>> GetListNguoiDung();
    }
}
