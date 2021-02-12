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
    public class PersonelController : Controller
    {
        // GET: Personel
        DbMağzaEntities db = new DbMağzaEntities();
        [Authorize]
        public ActionResult Index(int sayfa = 1)
        {
            var prsnl = db.tblpersonel.Where(x=>x.Durum==true).ToList().ToPagedList(sayfa, 15);
            return View(prsnl);
        }
        [HttpGet]
        public ActionResult YeniPersonel()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniPersonel(tblpersonel p)
        {
            p.Durum = true;
            db.tblpersonel.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PersonelSil(tblpersonel p)
        {
            var perbul = db.tblpersonel.Find(p.Id);
            perbul.Durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PersonelGetir(int id)
        {
            var per = db.tblpersonel.Find(id);
            return View("PersonelGetir", per);
        }
        public ActionResult PersonelGuncelle(tblpersonel p)
        {
            var per = db.tblpersonel.Find(p.Id);
            per.Ad = p.Ad;
            per.Soyad = p.Soyad;
            per.Departman = p.Departman;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}