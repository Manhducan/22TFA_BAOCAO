using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSellWatch.Models;
using Microsoft.Owin.Security.Facebook;

using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Drawing;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace WebSellWatch.Areas.Admin.Controllers
{
    public class QlySanPhamController : BaseController
    {
        WebsiteSellWatchesEntities db = new WebsiteSellWatchesEntities();
        public static Cloudinary cloudinary;

        public const string CLOUD_NAME = "dzhjhjcde";
        public const string API_KEY = "152152328779587";
        public const string API_SECRET = "XnL14FguDjDaiTfm2jRgQMR7T1Y";

        // GET: Admin/QlySanPham
        public ActionResult QuanLySanPham(string timkiem, int? page)
        {
            ViewBag.TuKhoa = timkiem;
            int pageNumber = (page ?? 1);
            int pageSize = 5;
            if (timkiem != null)
            {
                List<SANPHAM> listKQ = db.SANPHAMs.Where(n => n.TenSP.Contains(timkiem)).ToList();
                if (listKQ.Count == 0)
                {
                    TempData["tb"] = "Không tìm thấy sản phẩm nào phù hợp.";
                    return View(db.SANPHAMs.OrderBy(n => n.TenSP).ToPagedList(pageNumber, pageSize));
                }
                return View(listKQ.OrderBy(n => n.MaSP).ToPagedList(pageNumber, pageSize));
            }
            return View(db.SANPHAMs.OrderBy(n => n.MaSP).ToPagedList(pageNumber, pageSize));
        }



        [HttpGet]
        public ActionResult ThemMoi()
        {
            ViewBag.TenNSX = new SelectList(db.NSXes.ToList().OrderBy(n => n.TenNSX), "MaNSX", "TenNSX");
            ViewBag.TenSP = new SelectList(db.SANPHAMs.ToList().OrderBy(n => n.TenSP), "MaSP", "TenSP");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoi(SANPHAM sp, HttpPostedFileBase fileUpload, HttpPostedFileBase fileUpload2, HttpPostedFileBase fileUpload3)
        {
            int mansx = int.Parse(Request.Form["TenNSX"]);
            ViewBag.TenNSX = new SelectList(db.NSXes.ToList().OrderBy(n => n.TenNSX), "MaNSX", "TenNSX");
            if (fileUpload == null)
            {
                ViewBag.ThongBao = "Chọn hình ảnh";
                return View();
            }
            //Thêm vào cơ sở dữ liệu
            if (ModelState.IsValid)
            {
                //Lưu tên file
                var fileName = Path.GetFileName(fileUpload.FileName);

                var fileName2 = Path.GetFileName(fileUpload2.FileName);

                var fileName3 = Path.GetFileName(fileUpload3.FileName);
                //Lưu đường dẫn của file
                var path = Path.Combine(Server.MapPath("~/HinhAnh/HinhAnhSP"), fileName);

                var path2 = Path.Combine(Server.MapPath("~/HinhAnh/HinhAnhSP"), fileName2);

                var path3 = Path.Combine(Server.MapPath("~/HinhAnh/HinhAnhSP"), fileName3);
                //Kiểm tra hình ảnh đã tồn tại chưa
                if (System.IO.File.Exists(path))
                {
                    ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                }
                else
                {
                    fileUpload.SaveAs(path);
                }

                //Kiểm tra hình ảnh đã tồn tại chưa
                if (System.IO.File.Exists(path2))
                {
                    ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                }
                else
                {
                    fileUpload2.SaveAs(path2);
                }

                //Kiểm tra hình ảnh đã tồn tại chưa
                if (System.IO.File.Exists(path3))
                {
                    ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                }
                else
                {
                    fileUpload3.SaveAs(path3);
                }

                sp.Anh = fileUpload.FileName;
                sp.Anh2 = fileUpload2.FileName;
                sp.Anh3 = fileUpload3.FileName;
                //sp.NgayCapNhat = DateTime.Now;
                sp.MaNSX = mansx;
                String linkwweb = "https://i.9mobi.vn/cf/images/2015/03/nkk/nhung-hinh-anh-dep-19.jpg";
                //  string anh = MapURL(path);
           
                db.SANPHAMs.Add(sp);

                Account account = new Account(CLOUD_NAME, API_KEY, API_SECRET);
                cloudinary = new Cloudinary(account);

                //uploadImage(path);

                var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(path)
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);
                   
                        string link = uploadResult?.SecureUri?.AbsoluteUri;
                        Console.WriteLine("[Image was uploaded successfully]");
                        PublishMessage(sp.TenSP, link);
                    
                    
                   
               
                db.SaveChanges();
                TempData["thanhcong"] = "Thêm mới sản phẩm thành công!";
               
            }
            else
                TempData["kthanhcong"] = "Thêm sản phẩm thất bại";
            return RedirectToAction("QuanLySanPham");
        }
        // 105856808746192/photos? message = "dshakjdhsjkadhjksad" & link = https://giabaoluxury.com/dong-ho-hublot-chinh-hang&url=https://bizweb.dktcdn.net/thumb/2048x2048/100/175/988/files/dsc06221.jpg
       
        public async Task<string> PublishMessage(string tensp, string anh)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://graph.facebook.com/");
              



                var parametters = new Dictionary<string, string>
                {

                    { "access_token","EAAEBdeaJk6YBAFTv8bz25VpyYMgTkzz3fyZAZBZAEfiuIwZCc5U9YfKwZCxZBQbA9ZB8ZCNFzdgZClekzYNuyqHKz7UqMzx0BOEyQGETgFXfW2FAIsH1YIzjyVLvZCvZBZC22ZCJH9tPqcnAl4cdcVu11KgWdDXNZBIAZCz3IE57SOibfgLMwZBHxOFumOiiH6TJ4aJg83JoC2VBgdeCxgZDZD"},
                    {"message",tensp},
                    {"url",anh}
                 

            };
                var encodedContent = new FormUrlEncodedContent(parametters);
                
                var result = await httpClient.PostAsync($"105856808746192/photos", encodedContent);
                var msg = result.EnsureSuccessStatusCode();
                return await msg.Content.ReadAsStringAsync();
            }

        }

       /* public static void uploadImage(string imagePath)
        {
            try
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(imagePath)
                };

                var uploadResult = cloudinary.Upload(uploadParams);
                string link = uploadResult?.SecureUri?.AbsoluteUri;
                Console.WriteLine("[Image was uploaded successfully]");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }*/

        /*public string ImageToBase64(string path)
        {
            // string path = "D:\SampleImage.jpg";
            using (System.Drawing.Image image = System.Drawing.Image.FromFile(path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                    string base64String = Convert.ToBase64String(imageBytes);
                    String url = "data:image/png;base64," + base64String;
                    return url;
                }
            }
        }*/




        //Chỉnh sửa sản phẩm
        [HttpGet]
        public ActionResult ChinhSua(int masp)
        {

            //Lấy ra đối tượng sp theo mã
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MaSP == masp);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Đưa dữ liệu vào dropdownlist
            ViewBag.TenNSX = new SelectList(db.NSXes.ToList(), "MaNSX", "TenNSX", sp.MaNSX);
            return View(sp);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ChinhSua(SANPHAM sp)
        {
            int mansx = int.Parse(Request.Form["TenNSX"]);
            //Thêm vào CSDL
            if (ModelState.IsValid)
            {

                //Thực hiện cập nhật trong model
                sp.MaNSX = mansx;
                sp.NgayCapNhat = DateTime.Now;
                db.Entry(sp).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["thanhcong"] = "Chỉnh sửa sản phẩm thành công!";
            }
            else
            {
                TempData["kthanhcong"] = "Chỉnh sửa thất bại!";
            }
            //Đưa dữ liệu vào dropdownlist
            ViewBag.TenNSX = new SelectList(db.NSXes.ToList(), "MaNSX", "TenNSX", sp.MaNSX);
            return View();
        }

        public ActionResult HienThi(int masp)
        {
            //Lấy ra đối tượng sp theo mã
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MaSP == masp);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }

        //Xóa giày
        [HttpGet]
        public ActionResult Xoa(int masp)
        {
            //Lấy ra đối tượng sp theo mã
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MaSP == masp);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }

        [HttpPost, ActionName("Xoa")]
        public ActionResult XacNhanXoa(int masp)
        {
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MaSP == masp);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.SANPHAMs.Remove(sp);
            db.SaveChanges();
            return RedirectToAction("QuanLySanPham");
        }


        public ActionResult ChiTiet(int masp, int? page)
        {
            TempData["Masp"] = masp;
            int pageNumber = (page ?? 1);
            int pageSize = 5;
            //Lấy ra đối tượng sp theo mã
            var listsp = db.CHITIETSPs.Where(n => n.MaSP == masp).OrderBy(n => n.MaSP).ToPagedList(pageNumber, pageSize);
            if (listsp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(listsp);
        }
        public ActionResult ThemCT()
        {
            ViewBag.MaMau = new SelectList(db.MauSacs, "MaMau", "Color");
            ViewBag.MaSize = new SelectList(db.Sizes, "MaSize", "Size1");
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSP");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemCT([Bind(Include = "MaSP,MaSize,MaMau,SoLuong")] CHITIETSP cHITIETSP)
        {
            if (ModelState.IsValid)
            {
                db.CHITIETSPs.Add(cHITIETSP);
                db.SaveChanges();
                return RedirectToAction("QuanLySanPham");
            }

            ViewBag.MaMau = new SelectList(db.MauSacs, "MaMau", "Color", cHITIETSP.MaMau);
            ViewBag.MaSize = new SelectList(db.Sizes, "MaSize", "MaSize", cHITIETSP.MaSize);
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSP", cHITIETSP.MaSP);
            return View(cHITIETSP);
        }
    }

   
}