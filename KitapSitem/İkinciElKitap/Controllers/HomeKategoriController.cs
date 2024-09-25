using İkinciElKitap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace İkinciElKitap.Controllers
{
    public class HomeKategoriController : Controller
    {

        denemeEntities kt = new denemeEntities();
        // GET: HomeKategori
        public PartialViewResult KategoriListe()
        {
            List<Kategori> l = kt.Kategoris.ToList();
            return PartialView(l.ToList());
        }
       




    }



    }