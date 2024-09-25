using İkinciElKitap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace İkinciElKitap.Controllers
{
    public class UserController : Controller
    {
        denemeEntities kt = new denemeEntities();
        // GET: User
        public ActionResult Update()
        {
            var username = (string)Session["Mail"];
            var değerler = kt.Uyelers.FirstOrDefault(x=>x.Email == username); 
            
            return View(değerler);
        }
        [HttpPost]
        public ActionResult Update(Uyeler data)
        {
            var username = (string)Session["Mail"];
            var user = kt.Uyelers.Where(x => x.Email == username).FirstOrDefault();

            user.UyeAd=data.UyeAd;
            user.UyeSoyad=data.UyeSoyad;
            user.Email = data.Email;

            user.KullaniciAdi = data.KullaniciAdi;
            user.Sifre = data.Sifre;
            kt.SaveChanges();
            return RedirectToAction("Index", "Home");


        }
    }
}