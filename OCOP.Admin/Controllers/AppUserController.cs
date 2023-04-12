using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OCOP.Business.System.AppUsers;
using OCOP.ViewModel.System.AppUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCOP.Admin.Controllers
{
    public class AppUserController : BaseController
    {
        private readonly IAppUserService _appUserService;

        public AppUserController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _appUserService.GetListThanhVien();
            return View(result.ResultObj);
        }

        public IActionResult CreateThanhVien()
        {
            List<SelectListItem> listCapBac = new List<SelectListItem>();
            listCapBac.Add(new SelectListItem { Text = "Thành viên cấp Huyện", Value = "1", Selected = true });
            listCapBac.Add(new SelectListItem { Text = "Thành viên cấp Tỉnh", Value = "2" });
            ViewBag.ListCapBac = listCapBac;
            return View();
        } 

        [HttpPost]
        public async Task<IActionResult> CreateThanhVien(ThanhVienCreateRequest request)
        {
            if (!ModelState.IsValid) return View(request);
            var result = await _appUserService.CreateThanhVien(request);
            if (result.IsSuccessed)
            {
                SetAlert(result.Message, "success");
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", result.Message);
            return View();
        }

        public async Task<IActionResult> UpdateThanhVien(Guid appUserId)
        {
            var thanhVien = await _appUserService.GetThanhVien(appUserId);
            if (thanhVien.IsSuccessed)
            {
                var thanhVienUpdateRequest = new ThanhVienUpdateRequest()
                {
                    AppUserid = thanhVien.ResultObj.AppUserId,
                    DonViCongTac = thanhVien.ResultObj.DonViCongTac,
                    Email = thanhVien.ResultObj.Email,
                    ImagePath = thanhVien.ResultObj.ImagePath,
                    MaNhanVien = thanhVien.ResultObj.MaNhanVien,
                    PhoneNumber = thanhVien.ResultObj.PhoneNumber,
                    Ten = thanhVien.ResultObj.Ten
                };
                return View(thanhVienUpdateRequest);
            }
            TempData["Error"] = thanhVien.Message;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateThanhVien(ThanhVienUpdateRequest request)
        {
            if (!ModelState.IsValid) return View(request);
            var result = await _appUserService.UpdateThanhVien(request);
            if (result.IsSuccessed) return RedirectToAction("Index");
            TempData["Error"] = result.Message;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<JsonResult> DeleteThanhVien(Guid appUserId)
        {
            var result = await _appUserService.DeleteThanhVien(appUserId);
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

        [HttpPost]
        public async Task<JsonResult> ChangeTrangThai(Guid appUserId)
        {
            var result = await _appUserService.ChangeTrangThai(appUserId);
          
            if (result.IsSuccessed)
            {
                return Json(new
                {
                    status = true,
                    message = result.Message,
                    data=result.ResultObj
                });
            }
            return Json(new
            {
                status = false,
                message = result.Message
            });
        }        
    }
}
