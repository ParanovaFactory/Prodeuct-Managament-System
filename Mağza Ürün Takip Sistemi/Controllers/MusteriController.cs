using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mağza_Ürün_Takip_Sistemi.Models.Entity;
using PagedList;
using PagedList.Mvc;


namespace Mağza_Ürün_Takip_Sistemi.Controllers
{
    public class MusteriController : Controller
        
    {
        // GET: Musteri
        DbMağzaEntities db = new DbMağzaEntities();
        [Authorize]
        public ActionResult Index(int sayfa = 1)
        {
            var musteriler = db.tblmusteri.Where(x => x.Durum == true).ToList().ToPagedList(sayfa, 15);
            return View(musteriler);
        }
        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniMusteri(tblmusteri m)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            m.Durum = true;
            db.tblmusteri.Add(m);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriiSil(tblmusteri m)
        {
            var mus = db.tblmusteri.Find(m.Id);
            mus.Durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriGetir(int id)
        {
            var mus = db.tblmusteri.Find(id);
            return View("MusteriGetir", mus);
        }
        public ActionResult MusteriGuncelle(tblmusteri m)
        {
            var mus = db.tblmusteri.Find(m.Id);
            mus.Ad = m.Ad;
            mus.Soyad = m.Soyad;
            mus.Sehir = m.Sehir;
            mus.Bakiye = m.Bakiye;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}