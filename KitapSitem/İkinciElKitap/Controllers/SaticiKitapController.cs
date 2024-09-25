using İkinciElKitap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace İkinciElKitap.Controllers
{
    public class SaticiKitapController : Controller
    {
        denemeEntities kt = new denemeEntities();
        
        // GET: Kitaplar
        public ActionResult Index(int? page)
        {
            if (Session["ID"] != null)
            {
                int saticiID = (int)Session["ID"]; // ID'yi doğrudan int olarak alıyoruz
                ViewBag.SaticiID = saticiID; // Satıcı ID'sini ViewBag ile View'a geçiriyoruz

                var books = kt.Kitaps
                              .Where(k => k.Satici.UyeID == saticiID)
                              .OrderBy(k => k.KitapAd) // Kitapları isimlerine göre sıralıyoruz
                              .ToList();

                // Kitapları konsolda kontrol etmek için loglayabiliriz
                foreach (var book in books)
                {
                    System.Diagnostics.Debug.WriteLine("Kitap: " + book.KitapAd + " SatıcıID: " + book.SaticiID);
                }

                return View(books);
            }
            else
            {
                // Kullanıcı giriş yapmamış, hata mesajı gösterebilir veya kullanıcıyı yönlendirebilirsiniz
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpGet]
        public ActionResult Ekle(int id = -1)
        {
            Kitap k = null;

            ViewBag.yayinevi = kt.Yayinevis.ToList();
            ViewBag.stok = kt.Stoks.ToList();
            ViewBag.kategori = kt.Kategoris.ToList();
            ViewBag.yazar = kt.Yazars.ToList();
            ViewBag.satici = kt.Saticis.ToList();
            if (id > 0)
            {
                k = kt.Kitaps.FirstOrDefault(x => x.KitapID == id);
            }
            return View(k);
        }

        [HttpPost]
        public ActionResult Ekle(Kitap k, string YeniYazarAd, HttpPostedFileBase ResimURL)
        {
            // Yeni yazar eklenmişse
            if (!string.IsNullOrEmpty(YeniYazarAd))
            {
                Yazar yeniYazar = new Yazar { YazarAd = YeniYazarAd };
                kt.Yazars.Add(yeniYazar);
                kt.SaveChanges();

                k.YazarID = yeniYazar.YazarID;
            }

            // Kullanıcı giriş yapmış mı kontrol et
            if (Session["ID"] != null)
            {
                int uyeID = (int)Session["ID"];
                // UyeID'ye sahip Satici'yi bul
                var satici = kt.Saticis.FirstOrDefault(s => s.UyeID == uyeID);

                if (satici != null)
                {
                    k.SaticiID = satici.SaticiID;
                }
                else
                {
                    // Satıcı bulunamadı, hata mesajı veya başka bir işlem yapabilirsiniz
                    ModelState.AddModelError("", "Satıcı bulunamadı.");
                    return View(k);
                }
            }
            else
            {
                // Kullanıcı giriş yapmamış, hata mesajı veya yönlendirme
                return RedirectToAction("Login", "Account");
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
                    mevcutKitap.SaticiID = k.SaticiID; // Güncelleme sırasında da SaticiID ayarlanmalı
                }
            }
            else
            {
                // Yeni kitap ekleme
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
