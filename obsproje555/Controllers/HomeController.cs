using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace proje_obs.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //Deneme:
            //ObsDbContext ctx = new ObsDbContext();
            //include örneği, eager loading.
            //var b1 = ctx.Ogrenci.Include("kayitlar").Include("kayitlar.not").FirstOrDefault(b => b.OgrenciNo == 130401002);
            //ctx.Dispose();
            //var b2 = b1.kayitlar;
            //var b3 = b2.Select(b => b.not);
            //referans testi
            //var a1 = ctx.Notlar.FirstOrDefault(a => a.NotId == 5);
            //var a2 = a1.kayit;
            //var a3 = a2.Ogrenci;
            //var a4 = a3.Ad;
            if (Request.IsAuthenticated)
            {
                if (User.IsInRole("Ogrenci"))
                {
                    return RedirectToAction("Index", "Ogrenci");
                }
                else if (User.IsInRole("OgretimElemani"))
                {
                    return RedirectToAction("Index", "OgretimElemani");
                }
                else if (User.IsInRole("idari"))
                {
                    return RedirectToAction("Index", "idari");
                }


            }
            return View();
        }

        //YÖNETİMSEL
        public ActionResult AddPasswordsToDatabase()
        {
            ObsDbContext ctx = new ObsDbContext();
            foreach (Idari i in ctx.Idari)
            {
                i.Sifre = Convert.ToString(i.Id);
            }
            foreach (Ogrenci o in ctx.Ogrenci)
            {
                o.Sifre = Convert.ToString(o.OgrenciNo);
            }
            foreach (DersSorumlulari o in ctx.DersSorumlulari)
            {
                o.Sifre = Convert.ToString(o.AkademisyenID);
            }
            ctx.SaveChanges();

            var userIdentity = (ClaimsIdentity)User.Identity;
            var claims = userIdentity.Claims;
            var roleClaimType = userIdentity.RoleClaimType;
            var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();

            Console.WriteLine(roles);

            return RedirectToAction("Index");
        }

        //YÖNETİMSEL
        public ActionResult OgrencisiOlmayanKayitlariSil()
        {
            ObsDbContext ctx = new ObsDbContext();
            ctx.Notlar.RemoveRange(ctx.Kayit.Where(kayit => !ctx.Ogrenci.Any(ogrenci => ogrenci.OgrenciNo == kayit.OgrenciNo)).Select(kayit => kayit.not));
            ctx.Kayit.RemoveRange(ctx.Kayit.Where(kayit => !ctx.Ogrenci.Any(ogrenci => ogrenci.OgrenciNo == kayit.OgrenciNo)));
            ctx.SaveChanges();
            return View();
        }

        //YÖNETİMSEL
        public ActionResult DikeyGecisleriSil()//xd
        {
            //ders yılı 1900 olan öğrenciler ve tüm kayıtlarını sil
            ObsDbContext ctx = new ObsDbContext();
            List<AcilanDersler> SorunluAcilanDersler = ctx.AcilanDersler.Where(ad => ad.YilDers == 1900).ToList();
            List<Ogrenci> SilinecekOgrenciler = new List<Ogrenci>();
            foreach(AcilanDersler ad in SorunluAcilanDersler)
            {
                foreach(Kayit k in ad.Kayitlar)
                {
                    SilinecekOgrenciler.AddRange(ctx.Ogrenci.Where(ogrenci => ogrenci.kayitlar.Contains(k)));
                }
            }
            foreach(Ogrenci ogrenci in SilinecekOgrenciler)
            {
                foreach(Kayit k in ogrenci.kayitlar)
                {
                    ctx.Notlar.Remove(k.not);
                }
                ctx.Kayit.RemoveRange(ogrenci.kayitlar);
            }
            ctx.Ogrenci.RemoveRange(SilinecekOgrenciler);
            ctx.SaveChanges();
            return View();
        }

        //YÖNETİMSEL
        [HttpGet]
        public ActionResult KopyaKayitlariSil()
        {
            ObsDbContext ctx = new ObsDbContext();
            List<Kayit> Kayitlar = ctx.Kayit.Include("not").ToList();
            List<Kayit> SilinecekKayitlar = new List<Kayit>();
            foreach(Kayit k in Kayitlar)
            {
                if(SilinecekKayitlar.Any(silinecekKayit => silinecekKayit.KayitId == k.KayitId))
                {
                    continue;
                }
                SilinecekKayitlar.AddRange(Kayitlar.Where(kayit => kayit.ADId == k.ADId && kayit.OgrenciNo == k.OgrenciNo && kayit.KayitId != k.KayitId));
            }
            foreach(Kayit k in SilinecekKayitlar)
            {
                ctx.Notlar.Remove(k.not);
                ctx.Kayit.Remove(k);
            }
            ctx.SaveChanges();
            ctx.Dispose();

            return RedirectToAction("Index");
        }

        public ActionResult Sidebar()
        {
            return PartialView();
        }
    }
}