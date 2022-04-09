using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebSellWatch.Models;

namespace WebSellWatch.Controllers
{
    public class KhachHangController : Controller
    {
        WebsiteSellWatchesEntities db = new WebsiteSellWatchesEntities();
        // GET: KhachHang
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ViewResult DangKy()
        {
            return View();
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public ViewResult DangKy(KHACHHANG kh)
        {
            if (ModelState.IsValid)
            {
                //chèn dữ liệu
                db.KHACHHANGs.Add(kh);
                //Lưu vào CSDL
                db.SaveChanges();
            }
            return View();
        }

        [HttpGet]
        public ViewResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            string sEmail = f["email"].ToString();
            string sMatKhau = f["password"].ToString();
            KHACHHANG kh = db.KHACHHANGs.SingleOrDefault(n => n.Email == sEmail && n.MatKhau == sMatKhau);
            if (kh != null)
            {
                ViewBag.ThongBao = "Đăng nhập thành công";
                Session["TaiKhoanKH"] = kh;
                Session["Userkh"] = kh.TenKH;
                Session["MaKH"] = kh.MAKH;
                ViewBag.TenKH = "Xin chào: " + kh.TenKH;
                return RedirectToAction("Index", "Home");
                ViewBag.ThongBao = "Đăng nhập thành công!!!";
            }
            ViewBag.ThongBao = "Tên tài khoản hoặc mật khẩu không đúng!!!";
            return View();

        }

        public ActionResult DangXuat()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("DangNhap");
        }
        public ActionResult Edit()
        {
            if (Session["MaKH"]== null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHACHHANG kHACHHANG = db.KHACHHANGs.Find(Session["MaKH"]);
            if (kHACHHANG == null)
            {
                return HttpNotFound();
            }
            return View(kHACHHANG);
        }

        // POST: KhackHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAKH,TenKH,GioiTinh,DiaChi,Email,Sdt,MatKhau")] KHACHHANG kHACHHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kHACHHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View("Index", "Home");
        }
    }
}