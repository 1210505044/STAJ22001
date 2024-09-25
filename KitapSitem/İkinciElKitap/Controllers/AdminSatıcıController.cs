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
    public class AdminSatıcıController : Controller
    {
        denemeEntities kt = new denemeEntities();

        // GET: AdminSatıcı
        public ActionResult Index()
        {

            List<Satici> a = kt.Saticis.ToList();
            return View(a);
        }
        [HttpPost]
        public void Sil(int id)
        {

            Satici k = kt.Saticis.FirstOrDefault(x => x.SaticiID == id);
            if (k != null)
            {
                kt.Saticis.Remove(k);
                kt.SaveChanges();

            }
        }
        [HttpGet]
        public ActionResult Ekle(int? id)
        {
            Satici a = id == null ? new Satici() : kt.Saticis.FirstOrDefault(x => x.SaticiID == id);
            if (a == null)
            {
                return HttpNotFound();
            }
            return View(a);
        }

        [HttpPost]
        public ActionResult Ekle(Satici a)
        {
            if (a.SaticiID == 0)
            {
                kt.Saticis.Add(a);
            }
            else
            {
                Satici existing = kt.Saticis.FirstOrDefault(x => x.SaticiID == a.SaticiID);
                if (existing != null)
                {
                    existing.Uyeler.UyeAd = a.Uyeler.UyeAd;
                    existing.Uyeler.UyeSoyad = a.Uyeler.UyeSoyad;
                    existing.Uyeler.KullaniciAdi = a.Uyeler.KullaniciAdi;
                    existing.Uyeler.Email = a.Uyeler.Email;
                    existing.Uyeler.Sifre = a.Uyeler.Sifre;

                }
            }
            kt.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}