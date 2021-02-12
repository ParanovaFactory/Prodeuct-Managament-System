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
    public class VitrinController : Controller
    {
        // GET: Vitrin
        DbMağzaEntities db = new DbMağzaEntities();
        public ActionResult Index(int sayfa = 1)
        {
            var dgr = db.tblurunler.ToList().ToPagedList(sayfa,9);
            return View(dgr);
        }
    }
}