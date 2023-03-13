using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMagzaUrunTakip.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcMagzaUrunTakip.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri
        DbMvcStokEntities db = new DbMvcStokEntities();
        [Authorize]
        public ActionResult Index(int sayfa = 1)
        {
            var t = db.Tbl_Musteri.Where(x => x.MusteriDurum == true).ToList().ToPagedList(sayfa, 3);
            return View(t);
        }

        [HttpGet]
        public ActionResult Yeni()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Yeni(Tbl_Musteri p)
        {
            if (!ModelState.IsValid)
            {
                return View("Yeni");
            }
            p.MusteriDurum = true;
            db.Tbl_Musteri.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var t = db.Tbl_Musteri.Find(id);
            t.MusteriDurum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Güncelle(int id)
        {
            var t = db.Tbl_Musteri.Find(id);
            return View("Güncelle", t);
        }

        [HttpPost]
        public ActionResult Güncelle(Tbl_Musteri p)
        {
            var t = db.Tbl_Musteri.Find(p.MusteriID);
            t.MusteriAd = p.MusteriAd;
            t.MusteriSoyad = p.MusteriSoyad;
            t.MusteriSehir = p.MusteriSehir;
            t.MusteriBakiye = p.MusteriBakiye;
            t.MusteriDurum = p.MusteriDurum;
            t.MusteriDurum = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}