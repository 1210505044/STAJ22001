using İkinciElKitap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace İkinciElKitap.Controllers
{
    public class AdminMüsteriController : Controller
    {
        denemeEntities kt = new denemeEntities();

        // GET: AdminMüsteri
        public ActionResult Index()
        {
            List<Alici> a = kt.Alicis.ToList();
            return View(a);
        }

        [HttpGet]
        public ActionResult Ekle(int? id)
        {
            Alici a = id == null ? new Alici() : kt.Alicis.FirstOrDefault(x => x.AliciID == id);
            if (a== null)
            {
                return HttpNotFound();
            }
            return View(a);
        }

        [HttpPost]
        public ActionResult Ekle(Alici a)
        {
            if (a.AliciID == 0)
            {
                kt.Alicis.Add(a);
            }
            else
            {
                Alici existing = kt.Alicis.FirstOrDefault(x => x.AliciID == a.AliciID);
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

        [HttpPost]
        public void Sil(int id)
        {

            Alici k = kt.Alicis.FirstOrDefault(x => x.AliciID == id);
            if (k != null)
            {
                kt.Alicis.Remove(k);
                kt.SaveChanges();

            }
        }
    }
}