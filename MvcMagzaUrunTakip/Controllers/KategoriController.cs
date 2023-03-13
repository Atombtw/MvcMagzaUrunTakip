using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMagzaUrunTakip.Models.Entity;

namespace MvcMagzaUrunTakip.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        DbMvcStokEntities db = new DbMvcStokEntities();
        [Authorize]
        public ActionResult Index()
        {
            var t = db.Tbl_Kategori.ToList();
            return View(t);
        }

        [HttpGet]
        public ActionResult Yeni()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Yeni(Tbl_Kategori p)
        {
            db.Tbl_Kategori.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index", "Kategori");
        }

        public ActionResult Sil(int id)
        {
            var t = db.Tbl_Kategori.Find(id);
            db.Tbl_Kategori.Remove(t);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Güncelle(int id)
        {
            var t = db.Tbl_Kategori.Find(id);
            return View("Güncelle", t);
        }

        [HttpPost]
        public ActionResult Güncelle(Tbl_Kategori p)
        {
            var t = db.Tbl_Kategori.Find(p.KategoriID);
            t.KategoriAd = p.KategoriAd;
            db.SaveChanges();
            return RedirectToAction("Index", "Kategori");
        }
    }
}