using IronPdf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using OCOP.Business.Catalog.HoiDongs;
using OCOP.Business.Catalog.HoSos;
using OCOP.Business.Catalog.Nganhs;
using OCOP.Business.Catalog.Nhoms;
using OCOP.Business.Catalog.PhanNhoms;
using OCOP.Utility;
using OCOP.ViewModel.Catalog.HoSos;
using OCOP.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCOP.Controllers
{
    public class HomeClientController : BaseClientController
    {
        private readonly IHoSoService _hoSoService;
        private readonly INganhService _nganhService;
        private readonly INhomService _nhomService;
        private readonly IPhanNhomService _phanNhomService;
        private readonly IHoiDongService _hoiDongService;
        public HomeClientController(IHoSoService hoSoService, INganhService nganhService, INhomService nhomService, IPhanNhomService phanNhomService,IHoiDongService hoiDongService)
        {
            _hoSoService = hoSoService;
            _nganhService = nganhService;
            _nhomService = nhomService;
            _phanNhomService = phanNhomService;
            _hoiDongService = hoiDongService;
        }      

        public IActionResult Index()
        {          
            return View();
        }

        public async Task<IActionResult> ListHoSo()
        {
            UserSession _userSession = JsonConvert.DeserializeObject<UserSession>(HttpContext.Session.GetString(SystemConstants.ClientSession));

            var result = await _hoSoService.GetListHoSoByAppuser(_userSession.AppUserId);
            return View(result.ResultObj);
        }

        public async Task<IActionResult> ViewHoSo(Guid hoSoId)
        {
            var hoSo = await _hoSoService.GetHoSoDetail(hoSoId);
            if (hoSo.IsSuccessed) return View(hoSo.ResultObj);
            return RedirectToAction("ListHoSo");
        }

        public async Task<IActionResult> AddTapTinToHoSo(Guid hoSoId,IFormFile file)
        {
            var result = await _hoSoService.AddFileToHoSo(hoSoId, file);
            return RedirectToAction("ViewHoSo", new { hoSoId = hoSoId });
        }

        public async Task<IActionResult> CreateHoSo()
        {
            var listNganh = await _nganhService.GetListNganh();
            ViewBag.ListNganh = listNganh.ResultObj.Select(x => new SelectListItem()
            {
                Text = x.TenNganh,
                Value = x.NganhId.ToString()
            });
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateHoso(HoSoCreateRequest request)
        {
            UserSession _userSession = JsonConvert.DeserializeObject<UserSession>(HttpContext.Session.GetString(SystemConstants.ClientSession));

            if (!ModelState.IsValid) return View(request);
            request.AppUserId = _userSession.AppUserId;
            var result = await _hoSoService.CreateHoSo(request);
            if (result.IsSuccessed) return RedirectToAction("ListHoSo");
            ModelState.AddModelError("", result.Message);
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> LoadNhomByNganh(int nganhId)
        {
            var listNhom = await _nhomService.GetListNhomByNganh(nganhId);
            if (listNhom.IsSuccessed)
            {
                return Json(new
                {
                    status = true,
                    data = listNhom.ResultObj
                });
            }
            return Json(new
            {
                status = false,
                message = listNhom.Message
            });
        }

        [HttpPost]
        public async Task<JsonResult> LoadPhanNhomByNhom(int nhomId)
        {
            var listPhanNhom = await _phanNhomService.GetListPhanNhomByNhom(nhomId);
            if (listPhanNhom.IsSuccessed)
            {
                return Json(new
                {
                    status = true,
                    data = listPhanNhom.ResultObj
                });
            }
            return Json(new
            {
                status = false,
                message = listPhanNhom.Message
            });
        }

        public async Task<IActionResult> ViewDanhGiaHuyen(Guid hoSoId)
        {
            var hoiDong = await _hoiDongService.GetHoiDongByHoSoAndCapBac(hoSoId, SystemConstants.MaCapHuyen);
            if (!hoiDong.IsSuccessed || hoiDong.ResultObj.TrangThai !=SystemConstants.HoiDong_HoanThanh) return RedirectToAction("ViewHoSo", new { hoSoId = hoSoId });
            var result = await _hoiDongService.GetDanhGiaByHoiDong(hoiDong.ResultObj.HoiDongId);
            ViewBag.HoiDong = hoiDong.ResultObj;
            return View(result.ResultObj);
        }

        public async Task<IActionResult> ViewDanhGiaTinh(Guid hoSoId)
        {
            var hoiDong = await _hoiDongService.GetHoiDongByHoSoAndCapBac(hoSoId, SystemConstants.MaCapTinh);
            if (!hoiDong.IsSuccessed || hoiDong.ResultObj.TrangThai != SystemConstants.HoiDong_HoanThanh) return RedirectToAction("ViewHoSo", new { hoSoId = hoSoId });
            var result = await _hoiDongService.GetDanhGiaByHoiDong(hoiDong.ResultObj.HoiDongId);
            ViewBag.HoiDong = hoiDong.ResultObj;
            return View(result.ResultObj);
        }

        public IActionResult Certificate()
        {
            return View();
        }
    }
}
