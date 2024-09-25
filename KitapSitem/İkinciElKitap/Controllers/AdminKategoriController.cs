using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using İkinciElKitap.Models;
using İkinciElKitap.Models;

namespace İkinciElKitap.Controllers
{
    public class AdminKategoriController : Controller
    {
        denemeEntities kt = new denemeEntities();

        // GET: AdminKategori
        public ActionResult Index()
        {
            return View(kt.Kategoris.ToList());
        }

        [HttpGet]
        public ActionResult Ekle(int? id)
        {
            Kategori k = id == null ? new Kategori() : kt.Kategoris.FirstOrDefault(x => x.KategoriID == id);
            if (k == null)
            {
                return HttpNotFound();
            }
            return View(k);
        }

        [HttpPost]
        public ActionResult Ekle(Kategori k)
        {
            if (k.KategoriID == 0)
            {
                kt.Kategoris.Add(k);
            }
            else
            {
                Kategori existing = kt.Kategoris.FirstOrDefault(x => x.KategoriID == k.KategoriID);
                if (existing != null)
                {
                    existing.KategoriAd = k.KategoriAd;
                }
            }
            kt.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public void Sil(int id)
        {

            Kategori k = kt.Kategoris.FirstOrDefault(x => x.KategoriID == id);
            if (k != null)
            {
                kt.Kategoris.Remove(k);
                kt.SaveChanges();

            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Kategori k = kt.Kategoris.FirstOrDefault(x => x.KategoriID == id);
            if (k == null)
            {
                return HttpNotFound();
            }
            return View(k);
        }

        [HttpPost]
        public ActionResult Edit(Kategori k)
        {
            Kategori existing = kt.Kategoris.FirstOrDefault(x => x.KategoriID == k.KategoriID);
            if (existing != null)
            {
                existing.KategoriAd = k.KategoriAd;
                kt.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}