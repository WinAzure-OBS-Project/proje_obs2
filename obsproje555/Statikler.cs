using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proje_obs
{
    public static class Statikler
    {
        public static List<AcilanDersler> AçılanDersTalepleri = new List<AcilanDersler>();
        public static List<Kayit> KayıtlarTalepleri = new List<Kayit>();
        public static DateTime dersAcmaBaslangic = new DateTime();
        public static DateTime dersEklemeBaslangic = new DateTime();
        public static DateTime dersAcmaBitis = new DateTime();
        public static DateTime dersEklemeBitis = new DateTime();
    }
}