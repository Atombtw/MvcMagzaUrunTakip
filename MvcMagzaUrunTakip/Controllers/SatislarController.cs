using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMagzaUrunTakip.Models.Entity;

namespace MvcMagzaUrunTakip.Controllers
{
    public class SatislarController : Controller
    {
        // GET: Satislar
        DbMvcStokEntities db = new DbMvcStokEntities();
        [Authorize]
        public ActionResult Index()
        {
            var t = db.Tbl_Satis.ToList();
            return View(t);
        }

        [HttpGet]
        public ActionResult Yeni()
        {
            List<SelectListItem> t = (from i in db.Tbl_Urunler.Where(x => x.UrunDurum == true).ToList()
                                      select new SelectListItem
                                      {
                                          Text = i.UrunAd,
                                          Value = i.UrunID.ToString()
                                      }).ToList();
            ViewBag.dgr = t;

            List<SelectListItem> tt = (from i in db.Tbl_Personel.Where(x => x.PersonelDurum == true).ToList()
                                       select new SelectListItem
                                       {
                                           Text = i.PersonelAd,
                                           Value = i.PersonelID.ToString()
                                       }).ToList();
            ViewBag.dgrr = tt;

            List<SelectListItem> ttt = (from i in db.Tbl_Musteri.Where(x => x.MusteriDurum == true).ToList()
                                        select new SelectListItem
                                        {
                                            Text = i.MusteriAd,
                                            Value = i.MusteriID.ToString()
                                        }).ToList();
            ViewBag.dgrrr = ttt;
            return View();
        }

        [HttpPost]
        public ActionResult Yeni(Tbl_Satis p)
        {
            var t = db.Tbl_Urunler.Where(x => x.UrunID == p.Tbl_Urunler.UrunID).FirstOrDefault();
            var tt = db.Tbl_Personel.Where(x => x.PersonelID == p.Tbl_Personel.PersonelID).FirstOrDefault();
            var ttt = db.Tbl_Musteri.Where(x => x.MusteriID == p.Tbl_Musteri.MusteriID).FirstOrDefault();

            var prodStock = db.Tbl_Urunler.Find(t.UrunID);
            var cbalance = db.Tbl_Musteri.Find(ttt.MusteriID);

            decimal balance = Convert.ToDecimal(ttt.MusteriBakiye);
            decimal price = Convert.ToDecimal(t.UrunSatisFiyat);
            int stock = Convert.ToInt32(t.UrunStok);


            p.Tbl_Urunler = t;
            p.Tbl_Personel = tt;
            p.Tbl_Musteri = ttt;

            p.SatısFiyat = t.UrunSatisFiyat;

            p.SatısTarih = DateTime.Parse(DateTime.Now.ToShortDateString());

            int resStock = stock = stock - 1;
            decimal result = balance = balance - price;



            if (result == 0 || result < 0 || balance == 0 || balance < 0)
            {
                return RedirectPermanent("/Musteri/Index/" + ttt.MusteriID);
            }

            if (stock == 0 || stock < 0)
            {
                return RedirectPermanent("/Satislar/Index/" + t.UrunID);
            }

            cbalance.MusteriBakiye = result;
            prodStock.UrunStok = Convert.ToInt16(resStock);

            db.Tbl_Satis.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}