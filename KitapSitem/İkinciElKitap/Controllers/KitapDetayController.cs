using İkinciElKitap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace İkinciElKitap.Controllers
{
    public class KitapDetayController : Controller
    {
        private denemeEntities kt = new denemeEntities();
        // GET: KitapDetay
        public ActionResult Detay(int id)
        {
            var kitap = kt.Kitaps
                  .Where(kt => kt.KitapID == id)
                  .Select(kt => new KitapDetayViewModel
                  {
                      KitapAd = kt.KitapAd,
                      SayfaSayisi = (int)kt.SayfaSayisi,
                      YazarAd = kt.Yazar.YazarAd,
                      YayineviAd = kt.Yayinevi.YayineviAd,
                      Fiyat = (int)kt.Fiyat,
                      KategoriAd=kt.Kategori.KategoriAd,
                      ResimURL=kt.ResimURL
                  })
                  .FirstOrDefault();

            if (kitap == null)
            {
                return HttpNotFound();
            }

            return View(kitap);
        }
    }
}