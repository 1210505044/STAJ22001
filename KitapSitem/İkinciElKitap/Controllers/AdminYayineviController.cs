using İkinciElKitap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace İkinciElKitap.Controllers
{
    public class AdminYayineviController : Controller
    {
        denemeEntities kt = new denemeEntities();
        // GET: AdminYayinevi
        public ActionResult Index()
        {
            List<Yayinevi> a = kt.Yayinevis.ToList();
            return View(a);
        }


        [HttpGet]
        public ActionResult Ekle(int? id)
        {
            Yayinevi k = id == null ? new Yayinevi() : kt.Yayinevis.FirstOrDefault(x => x.YayineviID == id);
            if (k == null)
            {
                return HttpNotFound();
            }
            return View(k);
        }

        [HttpPost]
        public ActionResult Ekle(Yayinevi k)
        {
            if (k.YayineviID == 0)
            {
                kt.Yayinevis.Add(k);
            }
            else
            {
                Yayinevi existing = kt.Yayinevis.FirstOrDefault(x => x.YayineviID == k.YayineviID);
                if (existing != null)
                {
                    existing.YayineviAd = k.YayineviAd;
                }
            }
            kt.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public void Sil(int id)
        {

            Yayinevi k = kt.Yayinevis.FirstOrDefault(x => x.YayineviID == id);
            if (k != null)
            {
                kt.Yayinevis.Remove(k);
                kt.SaveChanges();

            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Yayinevi k = kt.Yayinevis.FirstOrDefault(x => x.YayineviID == id);
            if (k == null)
            {
                return HttpNotFound();
            }
            return View(k);
        }

        [HttpPost]
        public ActionResult Edit(Yayinevi k)
        {
            Yayinevi existing = kt.Yayinevis.FirstOrDefault(x => x.YayineviID == k.YayineviID);
            if (existing != null)
            {
                existing.YayineviAd = k.YayineviAd;
                kt.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}