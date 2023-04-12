using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OCOP.Data.Context;
using OCOP.Data.Entities;
using OCOP.ViewModel.Common;
using OCOP.ViewModel.System.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCOP.Business.System.AppRoles
{
    public class AppRoleService : IAppRoleService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IdentityUserRole<Guid> _identityUserRole; 
        private readonly OCOPDbContext _context;
        private readonly IConfiguration _config;

        public AppRoleService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IConfiguration config, OCOPDbContext context)
        {
            _userManager = userManager;       
            _roleManager = roleManager;
            _config = config;
            _context = context;
        }
        public async Task<ResponseResult<bool>> CreateRole(RoleCreateRequest request)
        {
            var roles = await _roleManager.Roles.Select(x => x.Name).ToListAsync();

            var roleExist = await _roleManager.RoleExistsAsync(request.Name);
            if (!roleExist)
            {
                var role = new AppRole()
                {
                    Id = new Guid(),
                    Name = request.Name,
                    ConcurrencyStamp = new Guid().ToString(),
                    Description = request.Description,
                    NormalizedName = request.Name,
                };
                await _roleManager.CreateAsync(role);
                return new ResponseSuccessResult<bool>();
            }
            return new ResponseErrorResult<bool>("Thêm mới quyền không thành công");
        }

        public async Task<ResponseResult<bool>> DeleteRole(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null) return new ResponseErrorResult<bool>("Không tìm thấy quyền");

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded) return new ResponseSuccessResult<bool>();
            return new ResponseErrorResult<bool>("Xóa quyền thất bại");
        }

        public async Task<ResponseResult<List<RoleViewModel>>> GetAllRole()
        {
            var roles = await _roleManager.Roles.Select(x => new RoleViewModel()
            {
                RoleId = x.Id,
                Name = x.Name,
                NormalizedName = x.NormalizedName,
                Description = x.Description
            }).ToListAsync();
            return new ResponseSuccessResult<List<RoleViewModel>>(roles);
        }

        public async Task<ResponseResult<IList<string>>> GetListRoleByUser(Guid appUserId)
        {
            var user = await _userManager.FindByIdAsync(appUserId.ToString());
            if (user == null) return new ResponseErrorResult<IList<string>>("Không tìm thấy người dùng");
            var result = await _userManager.GetRolesAsync(user);
            return new ResponseSuccessResult<IList<string>>(result);
        }

        public async Task<ResponseResult<bool>> RoleAssign(RoleAssignRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null) return new ResponseErrorResult<bool>("Tài khoản không tồn tại");

            var removedRoles = request.Roles.Where(x => x.Selected == false).Select(x => x.Name).ToList();
            foreach (var roleName in removedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == true)
                {
                    await _userManager.RemoveFromRoleAsync(user, roleName);
                }
            }
            await _userManager.RemoveFromRolesAsync(user, removedRoles);

            var addedRoles = request.Roles.Where(x => x.Selected == true).Select(x => x.Name).ToList();
            foreach (var roleName in addedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == false)
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                }
            }

            return new ResponseSuccessResult<bool>();
        }
    }
}
