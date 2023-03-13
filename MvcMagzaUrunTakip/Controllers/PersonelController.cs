using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMagzaUrunTakip.Models.Entity;

namespace MvcMagzaUrunTakip.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        DbMvcStokEntities db = new DbMvcStokEntities();
        [Authorize]
        public ActionResult Index()
        {
            var t = db.Tbl_Personel.Where(x => x.PersonelDurum == true).ToList();
            return View(t);
        }

        [HttpGet]
        public ActionResult Yeni()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Yeni(Tbl_Personel p)
        {
            if (!ModelState.IsValid)
            {
                return View("Yeni");
            }
            p.PersonelDurum = true;
            db.Tbl_Personel.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var t = db.Tbl_Personel.Find(id);
            t.PersonelDurum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Güncelle(int id)
        {
            var t = db.Tbl_Personel.Find(id);
            return View("Güncelle", t);
        }

        [HttpPost]
        public ActionResult Güncelle(Tbl_Personel p)
        {
            var t = db.Tbl_Personel.Find(p.PersonelID);
            t.PersonelAd = p.PersonelAd;
            t.PersonelSoyad = p.PersonelSoyad;
            t.PersonelDepartman = p.PersonelDepartman;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}