using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OCOP.Business.Catalog.HoiDongs;
using OCOP.Business.System.AppUsers;
using OCOP.Utility;
using OCOP.ViewModel.Catalog.HoiDongs;
using OCOP.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCOP.Admin.Controllers
{
    public class HoiDongController : BaseController
    {
        private readonly IHoiDongService _hoiDongService;
        private readonly IAppUserService _appUserService;
        public HoiDongController(IHoiDongService hoiDongService, IAppUserService appUserService)
        {
            _hoiDongService = hoiDongService;
            _appUserService = appUserService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _hoiDongService.GetListHoiDong();
            return View(result.ResultObj);
        }

        public IActionResult CreateHoiDong(Guid hoSoId, int maCapBac)
        {
            ViewBag.HoSoId = hoSoId;
            List<SelectListItem> listCapBac = new List<SelectListItem>();
            listCapBac.Add(new SelectListItem { Text = "Hội đồng cấp Huyện", Value = SystemConstants.MaCapHuyen.ToString(), Selected = maCapBac == SystemConstants.MaCapHuyen ? true : false });
            listCapBac.Add(new SelectListItem { Text = "Hội đồng cấp Tỉnh", Value = SystemConstants.MaCapTinh.ToString(), Selected = maCapBac == SystemConstants.MaCapTinh ? true : false });
            ViewBag.ListCapBac = listCapBac;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateHoiDong(HoiDongCreateRequest request)
        {
            if (!ModelState.IsValid) return View(request);
            var result = await _hoiDongService.CreateHoiDong(request);
            if (result.IsSuccessed) return RedirectToAction("Index");
            ModelState.AddModelError("", result.Message);
            return View();
        }

        public async Task<IActionResult> AddThanhVien(Guid hoiDongId)
        {
            var hoiDong = await _hoiDongService.GetHoiDong(hoiDongId);
            var listThanhVien = await _appUserService.GetListThanhVienByCapBac(hoiDong.ResultObj.MaCapBac);
            var listThanhVienInHoiDong = await _appUserService.GetListThanhVienInHoiDong(hoiDongId);
            var hoiDongThanhVienAssignRequest = new HoiDongThanhVienAssignRequest();
            hoiDongThanhVienAssignRequest.HoiDongId = hoiDongId;
            foreach (var thanhVien in listThanhVien.ResultObj)
            {
                hoiDongThanhVienAssignRequest.ThanhViens.Add(new SelectedItem()
                {
                    Id = thanhVien.AppUserId.ToString(),
                    Name = thanhVien.Ten + " -- " + thanhVien.MaNhanVien + " -- " + thanhVien.DonViCongTac + " -- " + thanhVien.CapBac,
                    Selected = listThanhVienInHoiDong.ResultObj.Contains(thanhVien.AppUserId.ToString())
                });
            }
            ViewBag.HoiDong = hoiDong.ResultObj;
            return View(hoiDongThanhVienAssignRequest);
        }

        [HttpPost]
        public async Task<IActionResult> AddThanhVien(HoiDongThanhVienAssignRequest request)
        {
            var result = await _hoiDongService.AddThanhVien(request);
            if (result.IsSuccessed) return RedirectToAction("Index");
            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [HttpPost]
        public async Task<JsonResult> LockHoiDong(Guid hoiDongId)
        {
            var result = await _hoiDongService.LockHoiDong(hoiDongId);
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

        public async Task<IActionResult> ViewHoiDong(Guid hoiDongId)
        {
            var hoiDong = await _hoiDongService.GetHoiDong(hoiDongId);
            var listThanhVien = await _appUserService.GetListThanhVienByCapBac(hoiDong.ResultObj.MaCapBac);
            var listThanhVienInHoiDong = await _appUserService.GetListThanhVienInHoiDong(hoiDongId);
            var hoiDongThanhVienAssignRequest = new HoiDongThanhVienAssignRequest();
            hoiDongThanhVienAssignRequest.HoiDongId = hoiDongId;
            foreach (var thanhVien in listThanhVien.ResultObj)
            {
                if (listThanhVienInHoiDong.ResultObj.Contains(thanhVien.AppUserId.ToString())){
                    hoiDongThanhVienAssignRequest.ThanhViens.Add(new SelectedItem()
                    {
                        Id = thanhVien.AppUserId.ToString(),
                        Name = thanhVien.Ten + " -- " + thanhVien.MaNhanVien + " -- " + thanhVien.DonViCongTac + " -- " + thanhVien.CapBac,
                        Selected = listThanhVienInHoiDong.ResultObj.Contains(thanhVien.AppUserId.ToString())
                    });
                }
            }
            ViewBag.HoiDong = hoiDong.ResultObj;
            return View(hoiDongThanhVienAssignRequest);
        }
    }
}
