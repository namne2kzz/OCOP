using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OCOP.Models;
using OCOP.Utility;
using OCOP.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OCOP.Business.Catalog.HoiDongs;
using OCOP.Business.Catalog.HoSos;
using OCOP.Business.Catalog.TieuChis;
using OCOP.ViewModel.Catalog.TieuChis;
using OCOP.Business.System.AppUsers;
using Microsoft.Extensions.Configuration;

namespace OCOP.Controllers
{
    public class HomeMemberController : BaseMemberController
    {
        private readonly ILogger<HomeMemberController> _logger;
        private readonly IHoiDongService _hoiDongService;
        private readonly IHoSoService _hoSoService;
        private readonly ITieuChiService _tieuChiService;
        private readonly IAppUserService _appUserService;
        private readonly IConfiguration _configuration;


        public HomeMemberController(ILogger<HomeMemberController> logger, IHoiDongService hoiDongService, IHoSoService hoSoService, ITieuChiService tieuChiService, IAppUserService appUserService, IConfiguration configuration)
        {
            _logger = logger;
            _hoiDongService = hoiDongService;
            _hoSoService = hoSoService;
            _tieuChiService = tieuChiService;
            _appUserService = appUserService;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ListHoiDongByAppUser()
        {
            UserSession _userSession = JsonConvert.DeserializeObject<UserSession>(HttpContext.Session.GetString(SystemConstants.MemberSession));
            var result = await _hoiDongService.GetListHoiDongByAppUser(_userSession.AppUserId);
            return View(result.ResultObj);
        }

        public async Task<IActionResult> ViewHoSo(Guid hoSoId)
        {
            var hoSo = await _hoSoService.GetHoSoDetail(hoSoId);
            if (hoSo.IsSuccessed) return View(hoSo.ResultObj);
            return RedirectToAction("ListHoiDongByAppUser");
        }

        public async Task<IActionResult> MarkHoSo(Guid hoSoId)
        {
            UserSession _userSession = JsonConvert.DeserializeObject<UserSession>(HttpContext.Session.GetString(SystemConstants.MemberSession));
            var thanhVien = await _appUserService.GetThanhVien(_userSession.AppUserId);
            var maCapBac = thanhVien.ResultObj.CapBac.Contains("huyện") ? 1 : 2;
            var hoiDong = await _hoiDongService.GetHoiDongByHoSoAndCapBac(hoSoId, maCapBac);
            var result = await _tieuChiService.GetListTieuChiByHoSo(hoSoId);
            if (result.IsSuccessed)
            {
                var hoSo = await _hoSoService.GetHoSo(hoSoId);
                ViewBag.HoSo = hoSo.ResultObj;
                ViewBag.ListTieuChi = result.ResultObj;
                var tieuChiMarkRequest = new TieuChiMarkRequest()
                {
                    AppUserId = _userSession.AppUserId,
                    HoiDongId = hoiDong.ResultObj.HoiDongId
                };
                foreach (var item in result.ResultObj)
                {
                    tieuChiMarkRequest.DanhGias.Add(new DanhGiaCreateRequest() { TieuChiId = item.TieuChiId });
                }
                return View(tieuChiMarkRequest);
            }
            return RedirectToAction("ViewHoSo", new { hoSoId = hoSoId });
        }

        [HttpPost]
        public async Task<IActionResult> MarkHoSo(TieuChiMarkRequest request)
        {
            var result = await _hoiDongService.MarkHoSoByThanhVien(request);
            if (result.IsSuccessed)
            {
                if (result.ResultObj.TrangThai == SystemConstants.HoSo_TrangThai_Pass_Tinh || result.ResultObj.TrangThai == SystemConstants.HoSo_TrangThai_Fail_Huyen || result.ResultObj.TrangThai == SystemConstants.HoSo_TrangThai_Fail_Tinh || result.ResultObj.TrangThai == SystemConstants.HoSo_TrangThai_Fail)
                {
                    var nguoiDung = await _appUserService.GetNguoiDung(result.ResultObj.AppUserId);

                    string content = System.IO.File.ReadAllText("Views/resultmail.html");

                    content = content.Replace("{{TenNhaSX}}", nguoiDung.ResultObj.TenNhaSanXuat);
                    content = content.Replace("{{TenNguoiDaiDien}}", nguoiDung.ResultObj.TenNguoiDaiDien);
                    content = content.Replace("{{TenMHSX}}", nguoiDung.ResultObj.TenMoHinhSX);
                    content = content.Replace("{{DiaChi}}", nguoiDung.ResultObj.DiaChiChiTiet);
                    content = content.Replace("{{Email}}", nguoiDung.ResultObj.Email);
                    content = content.Replace("{{SoDienThoai}}", nguoiDung.ResultObj.PhoneNumber);
                    content = content.Replace("{{TenHoSo}}", result.ResultObj.TenHoSo);
                    content = content.Replace("{{TenSanPham}}", result.ResultObj.TenSanPham);
                    content = content.Replace("{{NgayDangKy}}", result.ResultObj.NgayTao.ToShortDateString());
                    if (result.ResultObj.TrangThai == SystemConstants.HoSo_TrangThai_Pass_Tinh)
                    {
                        content = content.Replace("{{KetQua}}", result.ResultObj.DanhGiaTinh + " sao");
                    }
                    else
                    {
                        content = content.Replace("{{KetQua}}", "Trượt. Hồ sơ không đạt yêu cầu");
                    }

                    var emailConfig = new EmailConfig()
                    {
                        SMTPPort = _configuration["EmailConfig:SMTPPort"].ToString(),
                        SMTPHost = _configuration["EmailConfig:SMTPHost"].ToString(),
                        EnableSSL = bool.Parse(_configuration["EmailConfig:EnableSSL"].ToString()),
                        DisplayEnaimName = _configuration["EmailConfig:DisplayEmailName"].ToString(),
                        FromEmailAddress = _configuration["EmailConfig:FromEmailAddress"].ToString(),
                        FromEmailPassword = _configuration["EmailConfig:FromEmailPassword"].ToString()
                    };
                    //vào appsettings.json điền lại email và mật khẩu để gửi mail
                    // vào cài đặt mail bỏ chặn truy cập từ ứng dụng không xác định
                    // new MailHelper().SendMail(nguoiDung.ResultObj.Email, "OCOP - KẾT QUẢ ĐÁNH GIÁ", content, emailConfig);
                }
                return RedirectToAction("ListHoiDongByAppUser");
            }
            return View(result);
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
