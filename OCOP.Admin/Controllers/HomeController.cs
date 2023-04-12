using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OCOP.Admin.Models;
using OCOP.Business.Catalog.HoiDongs;
using OCOP.Business.Catalog.HoSos;
using OCOP.ViewModel.Catalog.HoiDongs;
using OCOP.ViewModel.Catalog.HoSos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OCOP.Admin.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHoiDongService _hoiDongService;
        private readonly IHoSoService _hoSoService;

        public HomeController(ILogger<HomeController> logger,IHoiDongService hoiDongService, IHoSoService hoSoService)
        {
            _logger = logger;
            _hoiDongService = hoiDongService;
            _hoSoService = hoSoService;
        }

        public IActionResult Index()
        {         
            return View();
        }

        public async Task<IActionResult> ReportHoiDongByTrangThai()
        {
            var result = await _hoiDongService.GetReportHoiDongByTrangThai();
            return Json((List<HoiDongReportViewModel>)result.ResultObj);
        }

        public async Task<IActionResult> ReportHoSoByTrangThai()
        {
            var result = await _hoSoService.GetReportHoSoByTrangThai();
            return Json((List<HoSoReportViewModel>)result.ResultObj);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
