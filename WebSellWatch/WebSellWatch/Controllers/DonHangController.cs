using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSellWatch.Models;

namespace WebSellWatch.Controllers
{
    public class DonHangController : Controller
    {
        WebsiteSellWatchesEntities db = new WebsiteSellWatchesEntities();
        // GET: DonHang
        public ActionResult Index()
        {
            return View();
        }
    }
}