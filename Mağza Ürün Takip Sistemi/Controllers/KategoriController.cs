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
    public class KategoriController : Controller
    {
        // GET: Kategori
        DbMağzaEntities db = new DbMağzaEntities();
        [Authorize]
        public ActionResult Index(int sayfa = 1)
        {
            var kategoriler = db.tblkategori.Where(x=>x.Durum==true).ToList().ToPagedList(sayfa, 15);
            return View(kategoriler);
        }
        [HttpGet]
        public ActionResult YeniKategori()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniKategori(tblkategori k)
        {
            k.Durum = true;
            db.tblkategori.Add(k);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriGetir(int id)
        {
            var ktgr = db.tblkategori.Find(id);
            return View("KategoriGetir", ktgr);
        }
        public ActionResult KategoriGuncelle(tblkategori k)
        {
            var ktg = db.tblkategori.Find(k.Id);
            ktg.Ad = k.Ad;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriSil(tblurunler k)
        {
            var ktgbul = db.tblkategori.Find(k.Id);
            ktgbul.Durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}