using İkinciElKitap.Models;
using İkinciElKitap.vievmodel;
using System;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace İkinciElKitap.Controllers
{
    public class AccountController : Controller
    {
        denemeEntities kt = new denemeEntities();
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Uyeler data)
        {
            var bilgiler = kt.Uyelers.FirstOrDefault(x => x.Email == data.Email && x.Sifre == data.Sifre);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.Email, false);
                Session["Mail"] = bilgiler.Email.ToString();
                Session["KullanıcıAdı"] = bilgiler.KullaniciAdi.ToString();
                Session["Ad"] = bilgiler.UyeAd.ToString();
                Session["Soyad"] = bilgiler.UyeSoyad.ToString();
                Session["ID"] = bilgiler.UyeID; // ID'yi int olarak saklıyoruz
                Session["Rol"] = bilgiler.Rol.ToString(); // Rol bilgisini Session'a ekleyin

                return RedirectToAction("Index", "Home");
            }
            ViewBag.hata = "Mail ya da şifre yanlış";
            return View(data);
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Uyeler
                {
                    UyeAd = model.UyeAd,
                    UyeSoyad = model.UyeSoyad,
                    KullaniciAdi = model.KullaniciAdi,
                    Email = model.Email,
                    Sifre = model.Sifre,
                    Rol = model.Rol
                };

                kt.Uyelers.Add(user);
                kt.SaveChanges();

                if (model.Rol == "Satıcı")
                {
                    var satici = new Satici
                    {
                        UyeID = user.UyeID
                    };
                    kt.Saticis.Add(satici);
                    kt.SaveChanges();
                }
                else if (model.Rol == "Alıcı")
                {
                    var alici = new Alici
                    {
                        UyeID = user.UyeID
                    };
                    kt.Alicis.Add(alici);
                    kt.SaveChanges();
                }

                return RedirectToAction("Login", "Account");
            }

            return View(model);
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear(); // Oturum verilerini temizleyin
            return RedirectToAction("Index", "Home");
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }

        // POST: ForgotPassword
        [AllowAnonymous]
        [HttpPost]
        public ActionResult ForgotPassword(Uyeler u)
        {
            using (var kt = new denemeEntities())
            {
                var uye = kt.Uyelers.FirstOrDefault(x => x.KullaniciAdi == u.KullaniciAdi && x.Email == u.Email);

                if (uye == null)
                {
                    ViewBag.Mesaj = "Kullanıcı Adı ya da Eposta yanlış";
                    return View();
                }

                try
                {
                    // Yeni şifreyi doğrudan kaydedin (şifreleme kullanılmıyor)
                    uye.Sifre = u.Sifre;
                    kt.SaveChanges();

                    FormsAuthentication.SetAuthCookie(uye.KullaniciAdi, false);
                    return RedirectToAction("Login", "Account");
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    foreach (var validationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            System.Diagnostics.Debug.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }

                    ViewBag.Mesaj = "Bir hata oluştu, lütfen tekrar deneyin.";
                    return View();
                }
            }
        }








    }
}
