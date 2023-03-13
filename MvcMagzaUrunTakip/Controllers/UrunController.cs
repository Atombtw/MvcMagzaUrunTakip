using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMagzaUrunTakip.Models.Entity;

namespace MvcMagzaUrunTakip.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        DbMvcStokEntities db = new DbMvcStokEntities();
        [Authorize]
        public ActionResult Index(string arama)
        {
            var t = from x in db.Tbl_Urunler select x;
            if (!string.IsNullOrEmpty(arama))
            {
                t = t.Where(x => x.UrunAd.Contains(arama) && x.UrunDurum == true);
            }
            return View(t.ToList());
        }

        [HttpGet]
        public ActionResult Yeni()
        {
            List<SelectListItem> t = (from i in db.Tbl_Kategori.ToList()
                                      select new SelectListItem
                                      {
                                          Text = i.KategoriAd,
                                          Value = i.KategoriID.ToString()
                                      }).ToList();
            ViewBag.dgr = t;
            return View();
        }

        [HttpPost]
        public ActionResult Yeni(Tbl_Urunler p)
        {
            var t = db.Tbl_Kategori.Where(i => i.KategoriID == p.Tbl_Kategori.KategoriID).FirstOrDefault();
            p.Tbl_Kategori = t;
            p.UrunDurum = true;
            db.Tbl_Urunler.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(Tbl_Urunler p, int id)
        {
            var t = db.Tbl_Urunler.Find(id);
            t.UrunDurum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Güncelle(int id)
        {
            List<SelectListItem> x = (from i in db.Tbl_Kategori.ToList()
                                      select new SelectListItem
                                      {
                                          Text = i.KategoriAd,
                                          Value = i.KategoriID.ToString()
                                      }).ToList();
            ViewBag.dgr = x;
            var t = db.Tbl_Urunler.Find(id);
            return View("Güncelle", t);
        }

        [HttpPost]
        public ActionResult Güncelle(Tbl_Urunler p)
        {
            var x = db.Tbl_Kategori.Where(i => i.KategoriID == p.Tbl_Kategori.KategoriID).FirstOrDefault();
            var t = db.Tbl_Urunler.Find(p.UrunID);
            t.Tbl_Kategori = x;
            t.UrunAd = p.UrunAd;
            t.UrunMarka = p.UrunMarka;
            t.UrunStok= p.UrunStok;
            t.UrunAlisFiyat= p.UrunAlisFiyat;
            t.UrunSatisFiyat = p.UrunSatisFiyat;
            t.UrunDurum = p.UrunDurum;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}