using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OCOP.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCOP.Admin.Controllers
{
    public class BaseController : Controller
    {
        [Authorize]
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session.GetString(SystemConstants.AdminSession);
            if (session == null)
            {
                context.Result = new RedirectToActionResult("Index", "Authen", null);
            }
            base.OnActionExecuting(context);
        }

        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "info")
            {
                TempData["AlertType"] = "alert-info";
            }
        }
    }
}
