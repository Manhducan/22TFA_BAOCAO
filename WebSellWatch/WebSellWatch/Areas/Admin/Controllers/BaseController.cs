using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebSellWatch.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        // GET: Admin/Base
        protected override void OnActionExecuting(ActionExecutingContext kt)
        {
            if (Session["User"] == null)
            {
                kt.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Login", action = "Login", Areas = "Admin" }));
            }
            base.OnActionExecuting(kt);
        }
    }
}