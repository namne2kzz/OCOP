using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using OCOP.Business.Catalog.Huyens;
using OCOP.Business.Catalog.MoHinhSXes;
using OCOP.Business.Catalog.Xas;
using OCOP.Business.System.AppUsers;
using OCOP.Utility;
using OCOP.ViewModel.Common;
using OCOP.ViewModel.System.AppUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCOP.Controllers
{
    public class AuthenController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IHuyenService _huyenService;
        private readonly IXaService _xaService;
        private readonly IMoHinhSXService _moHinhSXService;

        public AuthenController(IAppUserService appUserService, IHuyenService huyenService, IXaService xaService, IMoHinhSXService moHinhSXService)
        {
            _appUserService = appUserService;
            _huyenService = huyenService;
            _xaService = xaService;
            _moHinhSXService = moHinhSXService;
        }

        public async Task<IActionResult> Index()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove(SystemConstants.ClientSession);
            HttpContext.Session.Remove(SystemConstants.MemberSession);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginRequest request)
        {
            if (!ModelState.IsValid) return View(request);
            var result = await _appUserService.AuthenticateForWebPortal(request);
            if (!result.IsSuccessed)
            {
                ModelState.AddModelError("", result.Message);
                return View();
            }
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                IsPersistent = true
            };
            var sessionObj = JsonConvert.DeserializeObject<UserSession>(result.ResultObj);
            if (sessionObj.Roles.Contains(SystemConstants.MemberRoleName))
            {
                HttpContext.Session.SetString(SystemConstants.MemberSession, result.ResultObj);
                return RedirectToAction("Index", "HomeMember");
            }
            else
            {
                HttpContext.Session.SetString(SystemConstants.ClientSession, result.ResultObj);
                return RedirectToAction("Index", "HomeClient");
            }
        }

        public async Task<IActionResult> RegisterNguoiDung()
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
        public async Task<IActionResult> RegisterNguoiDung(NguoiDungCreateRequest request)
        {
            if (!ModelState.IsValid) return View(request);
            var result = await _appUserService.CreateNguoiDung(request);
            if (result.IsSuccessed)
            {               
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", result.Message);
            return View(request);
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
                    data = result.ResultObj
                });
            }
            return Json(new
            {
                status = false,
                message = result.Message
            });
        }


        public IActionResult Forbidden()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove(SystemConstants.AdminSession);
            HttpContext.Session.Remove(SystemConstants.ClientSession);
            return Redirect("Index");
        }
    }
}
