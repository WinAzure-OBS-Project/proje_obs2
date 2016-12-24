using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace proje_obs
{
    public class DersTarihlers
    {
        public String Id { get; set; }
        public DateTime dersSecmeBaslangic { get; set; }
        public DateTime dersAcmaBaslangic { get; set; }
        public DateTime dersSecmeBitis { get; set; }
        public DateTime dersAcmaBitis { get; set; }
        public String Yil { get; set; }
    }
}