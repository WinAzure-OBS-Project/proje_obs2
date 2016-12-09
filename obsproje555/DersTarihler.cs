using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proje_obs
{
    public class DersTarihler
    {
        public String Id { get; set; }
        public String Yıl { get; set; }
        public DateTime dersEklemeBaslangic { get; set; }
        public DateTime dersAcmaBaslangic { get; set; }
        public DateTime dersEklemeBitis { get; set; }
        public DateTime dersAcmaBitis { get; set; }
    }
}