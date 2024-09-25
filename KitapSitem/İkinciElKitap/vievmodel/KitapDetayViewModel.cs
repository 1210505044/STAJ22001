using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace İkinciElKitap.Models
{
    public class KitapDetayViewModel
    {

        public string KitapAd { get; set; }
        public int SayfaSayisi { get; set; }
        public string YazarAd { get; set; }
        public string YayineviAd { get; set; }
        public decimal Fiyat { get; set; }
        public string ResimURL { get; set; }
        public string KategoriAd { get; set; }
        public int KitapID { get; set; }

    }
}