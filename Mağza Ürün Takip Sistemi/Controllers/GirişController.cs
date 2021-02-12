using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mağza_Ürün_Takip_Sistemi.Models.Entity;
using System.Web.Security;

namespace Mağza_Ürün_Takip_Sistemi.Controllers
{
    public class GirişController : Controller
    {
        // GET: Giriş
        DbMağzaEntities db = new DbMağzaEntities();
        public ActionResult Giriş()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Giriş(tbladmin t)
        {
            var blg = db.tbladmin.FirstOrDefault(x => x.Kullanici == t.Kullanici && x.Sifre == t.Sifre);
            if (blg != null)
            {
                FormsAuthentication.SetAuthCookie(blg.Kullanici, false);
                return RedirectToAction("Index", "Musteri");
            }
            else
            {
                return View();
            }
        }
    }
}