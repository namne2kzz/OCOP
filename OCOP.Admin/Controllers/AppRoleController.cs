using Microsoft.AspNetCore.Mvc;
using OCOP.Business.System.AppRoles;
using OCOP.Business.System.AppUsers;
using OCOP.ViewModel.Common;
using OCOP.ViewModel.System.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCOP.Admin.Controllers
{
    public class AppRoleController : BaseController
    {
        private readonly IAppRoleService _appRoleService;
        private readonly IAppUserService _appUserService;

        public AppRoleController(IAppRoleService appRoleService,IAppUserService appUserService)
        {
            _appRoleService = appRoleService;
            _appUserService = appUserService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _appRoleService.GetAllRole();
            return View(result.ResultObj);
        }

        public async Task<IActionResult> AssignRole(Guid appUserId)
        {
            var roleAssignRequest = await GetRoleAssignRequest(appUserId);

            return View(roleAssignRequest);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(RoleAssignRequest request)
        {
            if (!ModelState.IsValid) return View();

            var result = await _appRoleService.RoleAssign(request);


            if (result.IsSuccessed)
            {
                SetAlert(result.Message, "success");
                return RedirectToAction("Index","AppUser");
            }
            SetAlert(result.Message, "warning");
            ModelState.AddModelError("", result.Message);           
            return View(request);
        }

        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleCreateRequest request)
        {
            if (!ModelState.IsValid) return View(request);
            var result = await _appRoleService.CreateRole(request);
            if(result.IsSuccessed) return RedirectToAction("Index");
            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteRole(Guid roleId)
        {
            var result = await _appRoleService.DeleteRole(roleId);
            if (result.IsSuccessed)
            {
                return Json(new
                {
                    status = true,
                    message = result.Message
                });
            }
            return Json(new
            {
                status = false,
                message = result.Message
            });
        }

        private async Task<RoleAssignRequest> GetRoleAssignRequest(Guid id)
        {
            var userRole = await _appRoleService.GetListRoleByUser(id);
            var roles = await _appRoleService.GetAllRole();
            var roleAssignRequest = new RoleAssignRequest();
            roleAssignRequest.Id = id;
            foreach (var role in roles.ResultObj)
            {
                roleAssignRequest.Roles.Add(new SelectedItem()
                {
                    Id = role.RoleId.ToString(),
                    Name = role.Name,
                    Selected = userRole.ResultObj.Contains(role.Name)
                });
            }
            return roleAssignRequest;
        }
    }
}
