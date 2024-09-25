using İkinciElKitap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace İkinciElKitap.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        denemeEntities kt = new denemeEntities();
        public ActionResult Index(int sayfa=1)
        {
            List<Kitap> l = kt.Kitaps.ToList();
            return View(l.ToPagedList(sayfa, 5));
        }
        public ActionResult KategoriyeGoreListele(int id, int sayfa = 1)
        {
            List<Kitap> kitaplar = kt.Kitaps.Where(k => k.KategoriID == id).ToList();
            return View("Index", kitaplar.ToPagedList(sayfa, 5));
        }

    }
}