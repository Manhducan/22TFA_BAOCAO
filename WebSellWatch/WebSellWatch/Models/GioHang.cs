using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSellWatch.Models
{
    public class GioHang
    {
        public int masp { get; set; }

        public string tensp { get; set; }

        public string hinhanh { get; set; }

        public double dongia { get; set; }

        [Required(ErrorMessage = "{0} không được để trống")]
        public int masize { get; set; }

        [Required(ErrorMessage = "{0} không được để trống")]
        public int tensize { get; set; }

        public int mamau { get; set; }

        public string tenmau { get; set; }

        [Range(1, 10, ErrorMessage = ("Số lượng tối thiểu là 1 tối đa 10 sản phẩm"))]
        public int soluong { get; set; }

        public double thanhtien { get { return soluong * dongia; } }

        WebsiteSellWatchesEntities db = new WebsiteSellWatchesEntities();

        public GioHang(int Masp, int Mamau, int Masize)
        {
            masp = Masp;
            SANPHAM giay = db.SANPHAMs.SingleOrDefault(n => n.MaSP == masp);
            tensp = giay.TenSP;
            hinhanh = giay.Anh;
            dongia = double.Parse(giay.DonGia.ToString());
            soluong = 1;
            CHITIETSP ctgiay = db.CHITIETSPs.SingleOrDefault(s => s.MaSP == Masp && s.MaMau == Mamau && s.MaSize == Masize);
            mamau = ctgiay.MaMau;
            masize = ctgiay.MaSize;
            MauSac mausac = db.MauSacs.SingleOrDefault(m => m.MaMau == mamau);
            tenmau = mausac.Color;
            Size sizes = db.Sizes.SingleOrDefault(s => s.MaSize == masize);
            tensize = sizes.Size1;
        }

    }
}