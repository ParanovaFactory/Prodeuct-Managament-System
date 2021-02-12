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
    public class SatisController : Controller
    {
        // GET: Satis
        DbMağzaEntities db = new DbMağzaEntities();
        [Authorize]
        public ActionResult Index(int sayfa = 1)
        {
            var sts = db.tblsatislar.ToList().ToPagedList(sayfa, 15);
            return View(sts);
        }
        [HttpGet]
        public ActionResult YeniSatis()
        {
            List<SelectListItem> urn = (from x in db.tblurunler.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.Ad,
                                            Value = x.Id.ToString()
                                        }
                                       ).ToList();
            ViewBag.drop = urn;


            List<SelectListItem> per = (from x in db.tblpersonel.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.Ad + " " + x.Soyad,
                                            Value = x.Id.ToString()
                                        }
                                       ).ToList();
            ViewBag.drop1 = per;

            List<SelectListItem> mus = (from x in db.tblmusteri.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.Ad + " " + x.Soyad,
                                            Value = x.Id.ToString()
                                        }
                                       ).ToList();
            ViewBag.drop2 = mus;
            return View();
        }
        [HttpPost]
        public ActionResult YeniSatis(tblsatislar s)
        {
            var urn = db.tblurunler.Where(x => x.Id == s.tblurunler.Id).FirstOrDefault();
            var mus = db.tblmusteri.Where(x => x.Id == s.tblmusteri.Id).FirstOrDefault();
            var per = db.tblpersonel.Where(x => x.Id == s.tblpersonel.Id).FirstOrDefault();
            s.tblurunler = urn;
            s.tblmusteri = mus;
            s.tblpersonel = per;
            db.tblsatislar.Add(s);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SatisGetir(int id)
        {
            var sts = db.tblsatislar.Find(id);
            return View("SatisGetir", sts);
        }
        public ActionResult SatisGuncelle(tblsatislar s)
        {
            var sts = db.tblsatislar.Find(s.Id);
            sts.Urun = s.Urun;
            sts.Personel = s.Personel;
            sts.Musteri = s.Musteri;
            sts.Tarih = s.Tarih;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SatisSil(int id)
        {
            var sts = db.tblsatislar.Find(id);
            db.tblsatislar.Remove(sts);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}