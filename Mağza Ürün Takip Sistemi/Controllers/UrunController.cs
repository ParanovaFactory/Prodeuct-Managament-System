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
    public class UrunController : Controller
    {
        // GET: Urun

        DbMağzaEntities db = new DbMağzaEntities();
        [Authorize]
        public ActionResult Index(int sayfa = 1) // string p
        {
            var urunler = db.tblurunler.ToList().ToPagedList(sayfa, 15);
            //var urunler = from x in db.tblurunler select x;
            //if (!string.IsNullOrEmpty(p))
            //{
            //    urunler = urunler.Where(x => x.Ad.Contains(p));
            //}
            return View(urunler); //.ToList()
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> urn = (from x in db.tblkategori.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.Ad,
                                            Value = x.Id.ToString()
                                        }
                                        ).ToList();
            ViewBag.drop = urn;
            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(tblurunler u)
        {
            u.Durum = true;
            var urn = db.tblkategori.Where(x => x.Id == u.tblkategori.Id).FirstOrDefault();
            u.tblkategori = urn;
            db.tblurunler.Add(u);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> urn = (from x in db.tblkategori.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.Ad,
                                            Value = x.Id.ToString()
                                        }
                                       ).ToList();
            var urun = db.tblurunler.Find(id);
            ViewBag.urnk = urn;
            return View("UrunGetir",urun);
        }
        public ActionResult UrunGuncelle(tblurunler u)
        {
            var urun = db.tblurunler.Find(u.Id);
            urun.Ad = u.Ad;
            urun.Marka = u.Marka;
            urun.Stok = u.Stok;
            urun.AlısFiyat = u.AlısFiyat;
            urun.SatisFiyat = u.SatisFiyat;
            var urn = db.tblkategori.Where(x => x.Id == u.tblkategori.Id).FirstOrDefault();
            urun.Kategori = urn.Id;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunKaldır(tblurunler u)
        {
            var urun = db.tblurunler.Find(u.Id);
            urun.Durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunEkle(tblurunler u)
        {
            var urun = db.tblurunler.Find(u.Id);
            urun.Durum = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunSil(int id)
        {
            var urun = db.tblurunler.Find(id);
            db.tblurunler.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}