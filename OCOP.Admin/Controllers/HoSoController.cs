using Microsoft.AspNetCore.Mvc;
using OCOP.Business.Catalog.HoiDongs;
using OCOP.Business.Catalog.HoSos;
using OCOP.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCOP.Admin.Controllers
{
    public class HoSoController : BaseController
    {
        private readonly IHoSoService _hoSoService;
        private readonly IHoiDongService _hoiDongService;
        public HoSoController(IHoSoService hoSoService,IHoiDongService hoiDongService)
        {
            _hoSoService = hoSoService;
            _hoiDongService = hoiDongService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _hoSoService.GetListHoSoPended();
            return View(result.ResultObj);
        }

        public async Task<IActionResult> Index_Pending()
        {
            var result = await _hoSoService.GetListHoSoPending();
            return View(result.ResultObj);
        }      

        public async Task<IActionResult> ViewHoSo(Guid hoSoId)
        {
            var hoSo = await _hoSoService.GetHoSoDetail(hoSoId);
            if (hoSo.IsSuccessed) return View(hoSo.ResultObj);
            return RedirectToAction("Index","HoiDong");
        }

        public async Task<IActionResult> ViewDanhGiaHuyen(Guid hoSoId)
        {
            var hoiDong = await _hoiDongService.GetHoiDongByHoSoAndCapBac(hoSoId, SystemConstants.MaCapHuyen);
            if (!hoiDong.IsSuccessed || hoiDong.ResultObj.TrangThai != SystemConstants.HoiDong_HoanThanh) return RedirectToAction("ViewHoSo", new { hoSoId = hoSoId });
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
    }
}
