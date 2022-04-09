using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSellWatch.Models;

namespace WebSellWatch.Controllers
{
    public class CuaHangController : Controller
    {
        WebsiteSellWatchesEntities db = new WebsiteSellWatchesEntities();
        // GET: CuaHang
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FooterPartial()
        {
            return PartialView();
        }

        public ActionResult GioiThieu()
        {
            return View();
        }
    }
}