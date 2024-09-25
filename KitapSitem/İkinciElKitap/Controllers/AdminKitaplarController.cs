using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using İkinciElKitap.Models;
using PagedList.Mvc;
using PagedList;

namespace İkinciElKitap.Controllers
{
    public class AdminKitaplarController : Controller
    {
        denemeEntities kt = new denemeEntities();

        // GET: AdminKitaplar
        public ActionResult Index(int sayfa=1)
        {
            List<Kitap> l = kt.Kitaps.ToList();
            return View(l.ToPagedList(sayfa,5));
        }
        [HttpGet]
        public ActionResult Ekle(int id=-1) {
            Kitap k = null;
          
            ViewBag.yayinevi = kt.Yayinevis.ToList();
            ViewBag.stok = kt.Stoks.ToList();
            ViewBag.kategori = kt.Kategoris.ToList();
            ViewBag.yazar = kt.Yazars.ToList();
            ViewBag.satici=kt.Saticis.ToList();
            if (id>0)
            {
                k = kt.Kitaps.FirstOrDefault(x => x.KitapID == id);
            }
            return View(k);


        }
        [HttpPost]
        public ActionResult Ekle(Kitap k, string YeniYazarAd, HttpPostedFileBase ResimURL)
        {
           

            if (!string.IsNullOrEmpty(YeniYazarAd))
            {
                Yazar yeniYazar = new Yazar { YazarAd = YeniYazarAd };
                kt.Yazars.Add(yeniYazar);
                kt.SaveChanges();

                k.YazarID = yeniYazar.YazarID;
            }

          
            if (k.KitapID > 0)
            {
                var mevcutKitap = kt.Kitaps.FirstOrDefault(x => x.KitapID == k.KitapID);
                if (mevcutKitap != null)
                {
                    mevcutKitap.KitapAd = k.KitapAd;
                    mevcutKitap.SayfaSayisi = k.SayfaSayisi;
                    mevcutKitap.Fiyat = k.Fiyat;
                    mevcutKitap.ResimURL = k.ResimURL;
                    mevcutKitap.YayineviID = k.YayineviID;
                    mevcutKitap.YazarID = k.YazarID;
                    mevcutKitap.StokID = k.StokID;
                    mevcutKitap.KategoriID = k.KategoriID;
                }
            }
            else
            {
                // Ekleme
                kt.Kitaps.Add(k);
            }

            kt.SaveChanges();
            return RedirectToAction("Index");

        }
      

        [HttpPost]
        public void Sil(int id)
        {

            Kitap k = kt.Kitaps.FirstOrDefault(x => x.KitapID == id);
            if (k != null)
            {
                kt.Kitaps.Remove(k);
                kt.SaveChanges();

            }
        }


       


    }
}