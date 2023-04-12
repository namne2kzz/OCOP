
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using OCOP.Business.Common;
using OCOP.Data.Context;
using OCOP.Data.Entities;
using OCOP.Utility;
using OCOP.ViewModel.Common;
using OCOP.ViewModel.System.AppUsers;
using OCOP.ViewModel.System.Roles;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OCOP.Business.System.AppUsers
{
    public class AppUserService : IAppUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IFileStorageService _fileStorageService;
        private readonly OCOPDbContext _context;
        private readonly IConfiguration _config;


        public AppUserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IConfiguration config, IFileStorageService fileStorageService, OCOPDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
            _fileStorageService = fileStorageService;
            _context = context;          
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _fileStorageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }

        public async Task<ResponseResult<string>> Authenticate(LoginRequest request)
        {
            var adminRole = await _roleManager.FindByNameAsync(SystemConstants.AdminRoleName);
            var user = await _userManager.FindByNameAsync(request.UserName);
            var thanhVien = await _context.ThanhViens.FindAsync(user.Id);
            if (user == null) return new ResponseErrorResult<string>("Đăng nhập không đúng");

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!result.Succeeded) return new ResponseErrorResult<string>("Đăng nhập không đúng");

            if (user.TrangThai == false) return new ResponseErrorResult<string>("Tài khoản đang bị khóa");
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var item in roles)
            {
                if (await _userManager.IsInRoleAsync(user, adminRole.Name))
                {
                    var userSession = new UserSession()
                    {
                        AppUserId = user.Id,
                        ImagePath = user.ImagePath,
                        Roles = roles,
                        Ten = thanhVien.Ten,
                        UserName = user.UserName
                    };                                                       

                    return new ResponseSuccessResult<string>(JsonConvert.SerializeObject(userSession));
                }
            }
            return new ResponseErrorResult<string>("Tài khoản không có quyền đăng nhập");
        }

        public async Task<ResponseResult<bool>> CreateThanhVien(ThanhVienCreateRequest request)
        {
            if (await _userManager.FindByNameAsync(request.UserName) != null) return new ResponseErrorResult<bool>("Tài khoản đã tồn tại");

            if (await _userManager.FindByEmailAsync(request.Email) != null) return new ResponseErrorResult<bool>("Email đã tồn tại");

            var user = new AppUser()
            {                             
                NormalizedUserName = request.UserName,              
                NormalizedEmail = request.Email,
                EmailConfirmed = true,             
                SecurityStamp = string.Empty,              
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                UserName = request.UserName,
                TrangThai = true
            };
            if (request.ThumbnailImage != null)
            {
                user.ImagePath = await this.SaveFile(request.ThumbnailImage);
            }         
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                var memberRole = await _roleManager.FindByNameAsync(SystemConstants.MemberRoleName);
                await _userManager.AddToRoleAsync(user, memberRole.Name);
                var thanhVien = new ThanhVien()
                {
                    AppUserId = user.Id,
                    Ten = request.Ten,
                    MaNhanVien = request.MaNhanVien.ToUpper().Trim(),
                    DonViCongTac = request.DonViCongTac,
                    MaCapBac=request.MaCapBac
                };
                _context.ThanhViens.Add(thanhVien);
                var affectRow = await _context.SaveChangesAsync();
                if (affectRow > 0) return new ResponseSuccessResult<bool>();
                return new ResponseErrorResult<bool>("Đăng ký không thành công");
            }
            return new ResponseErrorResult<bool>("Mật khẩu yêu cầu có chữ hoa, chữ thường, số và kí tự đặc biệt");
        }

        public async Task<ResponseResult<bool>> UpdateThanhVien(ThanhVienUpdateRequest request)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == request.Email && x.Id != request.AppUserid)) return new ResponseErrorResult<bool>("Email đã tồn tại");

            var user = await _userManager.FindByIdAsync(request.AppUserid.ToString());
            var thanhVien = await _context.ThanhViens.FindAsync(request.AppUserid);

            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;
            if (request.ThumbnailImage != null)
            {
                if (!string.IsNullOrEmpty(request.ImagePath))
                {
                    await _fileStorageService.DeleteFileAsync(request.ImagePath);
                }
                user.ImagePath = await this.SaveFile(request.ThumbnailImage);
            }
            thanhVien.Ten = request.Ten;
            thanhVien.MaNhanVien = request.MaNhanVien.ToUpper().Trim();
            thanhVien.DonViCongTac = request.DonViCongTac;
            thanhVien.AppUser = user;

            await _userManager.UpdateAsync(user);
            var result = await _context.SaveChangesAsync();
            if (result > 0) return new ResponseSuccessResult<bool>();
            return new ResponseErrorResult<bool>("Cập nhật không thành công");
        }

        public async Task<ResponseResult<bool>> DeleteThanhVien(Guid appUserId)
        {                                           
            var user = await _userManager.FindByIdAsync(appUserId.ToString()); 
            IList<string> listRole = await _userManager.GetRolesAsync(user);
            if (user == null)
            {
                return new ResponseErrorResult<bool>("Tài khoản không tồn tại");
            }  
            await _userManager.RemoveFromRolesAsync(user, listRole);
             _context.ThanhViens.Remove(await _context.ThanhViens.FindAsync(appUserId));
             var result = await _userManager.DeleteAsync(user);
                    
            if (result.Succeeded) return new ResponseSuccessResult<bool>();

            return new ResponseErrorResult<bool>("Xóa thành viên thất bại");
        }

        public async Task<ResponseResult<ThanhVienViewModel>> GetThanhVien(Guid appUserId)
        {
            var user = await _userManager.FindByIdAsync(appUserId.ToString());
            if (user == null)
            {
                return new ResponseErrorResult<ThanhVienViewModel>("Tài khoản không tồn tại");
            }
            var thanhVien = await _context.ThanhViens.FindAsync(appUserId);

            var result = new ThanhVienViewModel()
            {
                AppUserId = user.Id,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
                ImagePath = user.ImagePath,
                DonViCongTac = thanhVien.DonViCongTac,
                MaNhanVien = thanhVien.MaNhanVien,
                Ten = thanhVien.Ten,
                TrangThai = user.TrangThai,
                CapBac = thanhVien.MaCapBac == 1 ? "Hội đồng cấp huyện" : "Hội đồng cấp tỉnh"

            };
            return new ResponseSuccessResult<ThanhVienViewModel>(result);
        }

        public async Task<ResponseResult<List<ThanhVienViewModel>>> GetListThanhVien()
        {
            var listThanhVien = await _context.ThanhViens.ToListAsync();
            var result = new List<ThanhVienViewModel>();
            foreach (var item in listThanhVien)
            {
                var user = await _userManager.FindByIdAsync(item.AppUserId.ToString());
                var thanhVienViewModel = new ThanhVienViewModel()
                {
                    AppUserId = item.AppUserId,
                    DonViCongTac = item.DonViCongTac,
                    Email = user.Email,
                    ImagePath = user.ImagePath,
                    MaNhanVien = item.MaNhanVien,
                    PhoneNumber = user.PhoneNumber,
                    Ten = item.Ten,
                    TrangThai = user.TrangThai,
                    UserName = user.UserName,
                    CapBac=item.MaCapBac==1?"Hội đồng cấp huyện":"Hội đồng cấp tỉnh"
                };
                result.Add(thanhVienViewModel);
            }
            return new ResponseSuccessResult<List<ThanhVienViewModel>>(result);
        }

        public async Task<ResponseResult<bool>> ChangeTrangThai(Guid appUserId)
        {
            var user = await _userManager.FindByIdAsync(appUserId.ToString());
            if (user == null)
            {
                return new ResponseErrorResult<bool>("Tài khoản không tồn tại");
            }
            user.TrangThai = !user.TrangThai;
            await _userManager.UpdateAsync(user);
            return new ResponseSuccessResult<bool>(user.TrangThai);
        }

        public async Task<ResponseResult<bool>> CreateNguoiDung(NguoiDungCreateRequest request)
        {
           
            if (await _userManager.FindByNameAsync(request.UserName) != null) return new ResponseErrorResult<bool>("Tài khoản đã tồn tại");

            if (await _userManager.FindByEmailAsync(request.Email) != null) return new ResponseErrorResult<bool>("Email đã tồn tại");

            var user = new AppUser()
            {
                NormalizedUserName = request.UserName,
                NormalizedEmail = request.Email,
                EmailConfirmed = true,
                SecurityStamp = string.Empty,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                UserName = request.UserName,
                TrangThai = true
            };
            if (request.ThumbnailImage != null)
            {
                user.ImagePath = await this.SaveFile(request.ThumbnailImage);
            }         
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                var clientRole = await _roleManager.FindByNameAsync(SystemConstants.ClientRoleName);
                await _userManager.AddToRoleAsync(user, clientRole.Name);
                var nguoiDung = new NguoiDung()
                {
                    AppUserId = user.Id,
                    DiaChiChiTiet = request.DiaChiChiTiet,
                    MoHinhSXId = request.MoHinhSXId,
                    MoHinhSX = await _context.MoHinhSXes.FindAsync(request.MoHinhSXId),
                    SoDangKyKinhDoanh = request.SoDangKyKinhDoanh,
                    TenNguoiDaiDien = request.TenNguoiDaiDien,
                    TenNhaSanXuat = request.TenNhaSanXuat,
                    XaId = request.XaId,
                    Xa = await _context.Xas.FindAsync(request.XaId),
                    AppUser = user
                };
                _context.NguoiDungs.Add(nguoiDung);
                var affectRow = await _context.SaveChangesAsync();
                if (affectRow > 0) return new ResponseSuccessResult<bool>();
                return new ResponseErrorResult<bool>("Thêm mới người dùng không thành công");
            }
            return new ResponseErrorResult<bool>("Mật khẩu yêu cầu có chữ hoa, chữ thường, số và kí tự đặc biệt");
        }

        public async Task<ResponseResult<bool>> UpdateNguoiDung(NguoiDungUpdateRequest request)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == request.Email && x.Id != request.AppUserId)) return new ResponseErrorResult<bool>("Email đã tồn tại");

            var user = await _userManager.FindByIdAsync(request.AppUserId.ToString());
            var nguoiDung = await _context.NguoiDungs.FindAsync(request.AppUserId);

            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;
            if (request.ThumbnailImage != null)
            {
                await _fileStorageService.DeleteFileAsync(request.ImagePath);
                user.ImagePath = await this.SaveFile(request.ThumbnailImage);
            }
            nguoiDung.SoDangKyKinhDoanh = request.SoDangKyKinhDoanh;
            nguoiDung.TenNguoiDaiDien = request.TenNguoiDaiDien;
            nguoiDung.TenNhaSanXuat = request.TenNhaSanXuat;
            nguoiDung.DiaChiChiTiet = request.DiaChiChiTiet;
            if (nguoiDung.MoHinhSXId != request.MoHinhSXId)
            {
                nguoiDung.MoHinhSXId = request.MoHinhSXId;
                nguoiDung.MoHinhSX = await _context.MoHinhSXes.FindAsync(request.MoHinhSXId);
            }
            if (nguoiDung.XaId != request.XaId)
            {
                nguoiDung.XaId = request.XaId;
                nguoiDung.Xa = await _context.Xas.FindAsync(request.XaId);
            }
            nguoiDung.AppUser = user;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded) return new ResponseSuccessResult<bool>();
            return new ResponseErrorResult<bool>("Cập nhật không thành công");
        }

        public async Task<ResponseResult<bool>> DeleteNguoiDung(Guid appUserId)
        {
            var user = await _userManager.FindByIdAsync(appUserId.ToString());
            IList<string> listRole = await _userManager.GetRolesAsync(user);
            if (user == null)
            {
                return new ResponseErrorResult<bool>("Tài khoản không tồn tại");
            }
            await _userManager.RemoveFromRolesAsync(user, listRole);        
            _context.NguoiDungs.Remove(await _context.NguoiDungs.FindAsync(appUserId));
            var result = await _userManager.DeleteAsync(user);                    
            if (result.Succeeded) return new ResponseSuccessResult<bool>();
            return new ResponseErrorResult<bool>("Xóa người dùng thất bại");
        }

        public async Task<ResponseResult<NguoiDungViewModel>> GetNguoiDung(Guid appUserId)
        {
            var nguoiDung = await _context.NguoiDungs.FindAsync(appUserId);
            if (nguoiDung == null)
            {
                return new ResponseErrorResult<NguoiDungViewModel>("Tài khoản không tồn tại");
            }
            var appUser = await _userManager.FindByIdAsync(appUserId.ToString());
            var xa = await _context.Xas.FindAsync(nguoiDung.XaId);
            var huyen = await _context.Huyens.FindAsync(xa.HuyenId);
            var mhsx = await _context.MoHinhSXes.FindAsync(nguoiDung.MoHinhSXId);
            var result = new NguoiDungViewModel()
            {
                AppUserId = nguoiDung.AppUserId,
                Email = appUser.Email,
                PhoneNumber = appUser.PhoneNumber,
                UserName = appUser.UserName,
                ImagePath = appUser.ImagePath,
                DiaChiChiTiet = nguoiDung.DiaChiChiTiet,
                HuyenId = huyen.HuyenId,
                MoHinhSXId = nguoiDung.MoHinhSXId,
                SoDangKyKinhDoanh = nguoiDung.SoDangKyKinhDoanh,
                TenHuyen = huyen.TenHuyen,
                TenMoHinhSX = mhsx.TenMoHinhSX,
                TenXa = xa.TenXa,
                TenNguoiDaiDien = nguoiDung.TenNguoiDaiDien,
                TenNhaSanXuat = nguoiDung.TenNhaSanXuat,
                TrangThai = appUser.TrangThai,
                XaId = nguoiDung.XaId
            };
            return new ResponseSuccessResult<NguoiDungViewModel>(result);
        }

        public async Task<ResponseResult<List<NguoiDungViewModel>>> GetListNguoiDung()
        {
            var listNguoiDung = await _context.NguoiDungs.ToListAsync();
            var result = new List<NguoiDungViewModel>();
            foreach (var item in listNguoiDung)
            {
                var appUser = await _userManager.FindByIdAsync(item.AppUserId.ToString());
                var xa = await _context.Xas.FindAsync(item.XaId);
                var huyen = await _context.Huyens.FindAsync(xa.HuyenId);
                var mhsx = await _context.MoHinhSXes.FindAsync(item.MoHinhSXId);

                var nguoiDung = new NguoiDungViewModel()
                {
                    AppUserId = item.AppUserId,
                    Email = appUser.Email,
                    PhoneNumber = appUser.PhoneNumber,
                    UserName = appUser.UserName,
                    ImagePath = appUser.ImagePath,
                    DiaChiChiTiet = item.DiaChiChiTiet,
                    HuyenId = huyen.HuyenId,
                    MoHinhSXId = item.MoHinhSXId,
                    SoDangKyKinhDoanh = item.SoDangKyKinhDoanh,
                    TenHuyen = huyen.TenHuyen,
                    TenMoHinhSX = mhsx.TenMoHinhSX,
                    TenXa = xa.TenXa,
                    TenNguoiDaiDien = item.TenNguoiDaiDien,
                    TenNhaSanXuat = item.TenNhaSanXuat,
                    TrangThai = appUser.TrangThai,
                    XaId = item.XaId
                };
                result.Add(nguoiDung);
            }
            return new ResponseSuccessResult<List<NguoiDungViewModel>>(result);
        }

        public async Task<ResponseResult<AppUser>> GetUser(Guid appUserId)
        {
            var result = await _userManager.FindByIdAsync(appUserId.ToString());
            if (result != null) return new ResponseSuccessResult<AppUser>(result);
            return new ResponseErrorResult<AppUser>("Không tìm thấy người dùng");
        }

        public async Task<ResponseResult<List<ThanhVienViewModel>>> GetListThanhVienByCapBac(int maCapBac)
        {
            var listThanhVien = await _context.ThanhViens.Where(x=>x.MaCapBac==maCapBac).ToListAsync();
            var result = new List<ThanhVienViewModel>();
            foreach (var item in listThanhVien)
            {
                var user = await _userManager.FindByIdAsync(item.AppUserId.ToString());
                var thanhVienViewModel = new ThanhVienViewModel()
                {
                    AppUserId = item.AppUserId,
                    DonViCongTac = item.DonViCongTac,
                    Email = user.Email,
                    ImagePath = user.ImagePath,
                    MaNhanVien = item.MaNhanVien,
                    PhoneNumber = user.PhoneNumber,
                    Ten = item.Ten,
                    TrangThai = user.TrangThai,
                    UserName = user.UserName,
                    CapBac = item.MaCapBac == 1 ? "Hội đồng cấp huyện" : "Hội đồng cấp tỉnh"
                };
                result.Add(thanhVienViewModel);
            }
            return new ResponseSuccessResult<List<ThanhVienViewModel>>(result);
        }

        public async Task<ResponseResult<List<string>>> GetListThanhVienInHoiDong(Guid hoiDongId)
        {
            var listThanhVienInHoiDong = _context.HoiDongThanhViens.Where(x=>x.HoiDongId==hoiDongId).ToList();
            var result = new List<string>();
            foreach(var item in listThanhVienInHoiDong)
            {
                result.Add(item.AppUserId.ToString());
            }
            return new ResponseSuccessResult<List<string>>(result);
        }

        public async Task<ResponseResult<string>> AuthenticateForWebPortal(LoginRequest request)
        {
            var memberRole = await _roleManager.FindByNameAsync(SystemConstants.MemberRoleName);
            var clientRole = await _roleManager.FindByNameAsync(SystemConstants.ClientRoleName);

            var user = await _userManager.FindByNameAsync(request.UserName);          
            if (user == null) return new ResponseErrorResult<string>("Đăng nhập không đúng");

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!result.Succeeded) return new ResponseErrorResult<string>("Đăng nhập không đúng");

            if (user.TrangThai == false) return new ResponseErrorResult<string>("Tài khoản đang bị khóa");
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var item in roles)
            {
                var userSession = new UserSession();
                if (await _userManager.IsInRoleAsync(user, memberRole.Name))
                {
                    var thanhVien = await _context.ThanhViens.FindAsync(user.Id);
                    userSession.AppUserId = user.Id;
                    userSession.ImagePath = user.ImagePath;
                    userSession.Roles = roles;
                    userSession.Ten = thanhVien.Ten;
                    userSession.UserName = user.UserName;                                     
                }
                else if(await _userManager.IsInRoleAsync(user, clientRole.Name))
                {
                    var nguoiDung = await _context.NguoiDungs.FindAsync(user.Id);
                    userSession.AppUserId = user.Id;
                    userSession.ImagePath = user.ImagePath;
                    userSession.Roles = roles;
                    userSession.Ten = nguoiDung.TenNhaSanXuat;
                    userSession.UserName = user.UserName;
                } 
                return new ResponseSuccessResult<string>(JsonConvert.SerializeObject(userSession));
            }
            return new ResponseErrorResult<string>("Tài khoản không có quyền đăng nhập");
        }
    }
}
