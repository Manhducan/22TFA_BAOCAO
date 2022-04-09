﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSellWatch.Models;

namespace WebSellWatch.Controllers
{
    public class SanPhamController : Controller
    {
        WebsiteSellWatchesEntities db = new WebsiteSellWatchesEntities();
        // GET: SanPhamt
        public PartialViewResult SanPhamNoiBatPartial()
        {
            var listSPNoiBat = db.SANPHAMs.Take(5).ToList();
            return PartialView(listSPNoiBat);
        }

        public PartialViewResult DanhSachSanPhamPartial()
        {
            var listSP = db.SANPHAMs.ToList();
            return PartialView(listSP);
        }

        //Xem chi tiết đồng hồ
        public ViewResult XemChiTiet(int masp = 0, int mansx = 0)
        {
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MaSP == masp);
            if (sp == null)
            {
                //Trả về trang báo lỗi
                Response.StatusCode = 404;
                return null;
            }
            //----
            var mau = from a in db.CHITIETSPs
                      join b in db.MauSacs
                      on a.MaMau equals b.MaMau
                      where (a.MaSP == masp)
                      select new
                      {
                          a.MaMau,
                          b.Color
                      };

            var size = from a in db.CHITIETSPs
                       join b in db.Sizes
                       on a.MaSize equals b.MaSize
                       where (a.MaSP == masp)
                       select new
                       {
                           a.MaSize,
                           b.Size1
                       };
            //---
            ViewBag.TenNSX = db.NSXes.Single(n => n.MaNSX == sp.MaNSX).TenNSX;
            ViewBag.MauSac = new SelectList(mau.GroupBy(g => g.MaMau).Select(g => g.FirstOrDefault()), "MaMau", "Color");
            ViewBag.Size = new SelectList(size.GroupBy(g => g.MaSize).Select(g => g.FirstOrDefault()), "MaSize", "Size1");
            ViewBag.SPLienQuan = db.SANPHAMs.Where(n => n.MaSP != masp && n.MaNSX == mansx).Take(4).ToList();
            return View(sp);
        }
    }
}