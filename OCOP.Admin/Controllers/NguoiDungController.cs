using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OCOP.Business.Catalog.Huyens;
using OCOP.Business.Catalog.MoHinhSXes;
using OCOP.Business.Catalog.Xas;
using OCOP.Business.System.AppUsers;
using OCOP.ViewModel.System.AppUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCOP.Admin.Controllers
{
    public class NguoiDungController : BaseController
    {
        private readonly IAppUserService _appUserService;
        private readonly IHuyenService _huyenService;
        private readonly IXaService _xaService;
        private readonly IMoHinhSXService _moHinhSXService;

        public NguoiDungController(IAppUserService appUserService, IHuyenService huyenService,IXaService xaService,IMoHinhSXService moHinhSXService)
        {
            _appUserService = appUserService;
            _huyenService = huyenService;
            _xaService = xaService;
            _moHinhSXService = moHinhSXService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _appUserService.GetListNguoiDung();
            return View(result.ResultObj);
        }

        public async Task<IActionResult> CreateNguoiDung()
        {
            var listHuyen = await _huyenService.GetListHuyen();
            ViewBag.ListHuyen = listHuyen.ResultObj.Select(x => new SelectListItem()
            {
                Text = x.TenHuyen,
                Value = x.HuyenId.ToString()
            });
            var listMoHinhSX = await _moHinhSXService.GetListMoHinhSX();
            ViewBag.ListMoHinhSX = listMoHinhSX.ResultObj.Select(x => new SelectListItem()
            {
                Text = x.TenMoHinhSX,
                Value = x.MoHinhSXId.ToString()
            });
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateNguoiDung(NguoiDungCreateRequest request)
        {
            if (!ModelState.IsValid) return View(request);
            var result = await _appUserService.CreateNguoiDung(request);
            if (result.IsSuccessed)
            {
                TempData["Success"] = result.Message;
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        public async Task<IActionResult> UpdateNguoiDung(Guid appUserId)
        {
            var nguoiDung = await _appUserService.GetNguoiDung(appUserId);
            if (nguoiDung.IsSuccessed)
            {
                var nguoiDungUpdateReques = new NguoiDungUpdateRequest()
                {
                    AppUserId = nguoiDung.ResultObj.AppUserId,
                    DiaChiChiTiet = nguoiDung.ResultObj.DiaChiChiTiet,
                    Email = nguoiDung.ResultObj.Email,
                    HuyenId = nguoiDung.ResultObj.HuyenId,
                    ImagePath = nguoiDung.ResultObj.ImagePath,
                    MoHinhSXId = nguoiDung.ResultObj.MoHinhSXId,
                    PhoneNumber = nguoiDung.ResultObj.PhoneNumber,
                    SoDangKyKinhDoanh = nguoiDung.ResultObj.SoDangKyKinhDoanh,
                    TenNguoiDaiDien = nguoiDung.ResultObj.TenNguoiDaiDien,
                    TenNhaSanXuat = nguoiDung.ResultObj.TenNhaSanXuat,
                    XaId = nguoiDung.ResultObj.XaId
                };
                var listHuyen = await _huyenService.GetListHuyen();
                ViewBag.ListHuyen = listHuyen.ResultObj.Select(x => new SelectListItem()
                {
                    Text = x.TenHuyen,
                    Value = x.HuyenId.ToString(),
                    Selected=x.HuyenId==nguoiDung.ResultObj.HuyenId
                });
                var listXa = await _xaService.GetListXaByHuyen(nguoiDung.ResultObj.HuyenId);
                ViewBag.ListXa = listXa.ResultObj.Select(x => new SelectListItem()
                {
                    Text = x.TenXa,
                    Value = x.XaId.ToString(),
                    Selected = x.XaId == nguoiDung.ResultObj.XaId
                });
                var listMoHinhSX = await _moHinhSXService.GetListMoHinhSX();
                ViewBag.ListMoHinhSX = listMoHinhSX.ResultObj.Select(x => new SelectListItem()
                {
                    Text = x.TenMoHinhSX,
                    Value = x.MoHinhSXId.ToString(),
                    Selected=x.MoHinhSXId==nguoiDung.ResultObj.MoHinhSXId
                });
                return View(nguoiDungUpdateReques);
            }
            TempData["Error"] = nguoiDung.Message;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateNguoiDung(NguoiDungUpdateRequest request)
        {
            if (!ModelState.IsValid) return View(request);
            var result = await _appUserService.UpdateNguoiDung(request);
            if (result.IsSuccessed)
            {
                TempData["Success"] = result.Message;
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteNguoiDung(Guid appUserId)
        {
            var result = await _appUserService.DeleteNguoiDung(appUserId);
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
        public async Task<JsonResult> LoadXaByHuyen(int huyenId)
        {
            var result = await _xaService.GetListXaByHuyen(huyenId);
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

        [HttpPost]
        public async Task<JsonResult> ChangeTrangThai(Guid appUserId)
        {
            var result = await _appUserService.ChangeTrangThai(appUserId);
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
    }
}
