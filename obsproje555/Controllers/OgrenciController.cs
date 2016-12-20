using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using Microsoft.WindowsAzure.Storage;
//using Microsoft.WindowsAzure.Storage.Auth;
//using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Security.Claims;

namespace proje_obs.Controllers
{
    public class OgrenciController : Controller
    {
        String authorize = "Ogrenci";
        // GET: Ogrenci

        public ActionResult Index()
        {
            if(!Request.IsAuthenticated)
            {
                return RedirectToAction("Login");
            }
            else if(!User.IsInRole("Ogrenci"))
            {
                return RedirectToAction("Index", "Home");
            }
            var ctx = new ObsDbContext();
            var Id = Convert.ToInt32(User.Identity.Name);
            var ogr = ctx.Ogrenci.FirstOrDefault(o => o.OgrenciNo == Id);
            if (ogr != null)
            {
                return View(ogr);
            }
            ctx.Dispose();
            return View(ogr);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(int Id, String Password)
        {
            ObsDbContext ctx = new ObsDbContext();
            var ogr = ctx.Ogrenci.FirstOrDefault(o => o.OgrenciNo == Id);
            if(ogr != null)
            {
                _Membership.Login(Convert.ToString(ogr.OgrenciNo), "Ogrenci", Response);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Logout()
        {
            _Membership.Logout();
            return RedirectToAction("Index", "Home");
        }

        //[Authorize(Roles = "Ogrenci")]
        //[HttpGet]
        //public ActionResult UploadProfilePicture()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult UploadProfilePicture(HttpPostedFileBase file) //files=resim olacak
        //{
        //    if (Request.IsAuthenticated && (User.IsInRole("Ogrenci")))
        //    {
        //        if(_Storage.UploadFile(file, User.Identity.Name + ".jpg"))
        //        {
        //            Console.WriteLine("başarılı");
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            Console.WriteLine("dosya yükleme başarısız");
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    else
        //    {
        //        TempData["Message"] = "Yetkiniz yok";
        //        return RedirectToAction("Index", "Home");
        //    }
        //}

        //YÖNETİMSEL
        public ActionResult AddPasswordsToDatabase()
        {
            ObsDbContext ctx = new ObsDbContext();
            foreach(Idari i in ctx.Idari)
            {
                i.Sifre = Convert.ToString(i.Id);
            }
            foreach(Ogrenci o in ctx.Ogrenci)
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

        //ilk açılış, ders seçme haftasıysa tüm dersleri göster, değilse hata ekranı ayarla
        [HttpGet]
        public ActionResult DersSecme()
        {
            ObsDbContext ctx = new ObsDbContext();
            List<AcilanDersler> secilebilecekDersler = null;
            List<Kayit> oncedenAlinmis_Kayit = null;
            List<int> gecilenDersler = null;
            List<AcilanDersler> cikarilacaklar = new List<AcilanDersler>();
            AcilanDersler eklenicek = null;
            if (Session["secilmis"] == null)
            {
                Session["secilmis"] = new List<AcilanDersler>();
            }
            int Id = Convert.ToInt32(User.Identity.Name);
            var o = ctx.Ogrenci.FirstOrDefault(a => a.OgrenciNo == Id);
            //secilebilecekDersler = ctx.AcilanDersler.Where(a => a.YariYil==int.Parse(o.AktifKayitDonemi.Split('.')[0]) && a.YilDers==2015 ).Select(a => a).ToList();
            int yil = int.Parse(o.AktifKayitDonemi.Substring(0, 1));
            secilebilecekDersler = ctx.AcilanDersler.Where(a => a.YariYil == yil  &&  a.YilDers==2014).Select(a => a).ToList();
            oncedenAlinmis_Kayit = ctx.Kayit.Where(a => a.OgrenciNo == o.OgrenciNo).Select(a => a).ToList();
            gecilenDersler = ctx.Notlar.Where(a => a.kayit.OgrenciNo == o.OgrenciNo && a.YilNot>49).Select(a=>a.KayitId).ToList();
            
            foreach(var i in oncedenAlinmis_Kayit)
            {
                if(!gecilenDersler.Contains(i.KayitId))
                {
                    eklenicek = (AcilanDersler) ctx.AcilanDersler.FirstOrDefault(a => a.ADId == i.ADId);
                    secilebilecekDersler.Add(eklenicek);
                }
            }
            
            bool DersSecmeHaftasi= true;
            if (Session["secilmis"] != null)
            {
                var yeni = (List < AcilanDersler > )Session["secilmis"];
                foreach(var i in yeni)
                {
                    foreach(var j in secilebilecekDersler)
                    {
                        if (i.DersKodu == j.DersKodu)
                        {
                            cikarilacaklar.Add(j);
                            
                        }
                    }
                }
            }
            foreach(var j in cikarilacaklar)
            {
                secilebilecekDersler.Remove(j);
            }
            if(DersSecmeHaftasi)
            {

                return View(secilebilecekDersler);//seçebileceği dersleri döndür
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        //bir ders seçildiğinde ders kodu post edilecek, bir tür session cookie'sinde falan geçici olarak seçilmiş dersler tutulacak
        //önceki senelerde aldığı dersleri de tekrar alabilir
        [HttpPost]
        public ActionResult DersSecme(int dersId)
        {

            ObsDbContext ctx = new ObsDbContext();
            AcilanDersler eklenicek = ctx.AcilanDersler.FirstOrDefault(a => a.ADId == dersId);
            var liste = (List<AcilanDersler>)Session["secilmis"];
            liste.Add(eklenicek);
            Session["secilmis"] = liste;
            return RedirectToAction("DersSecme");
        }

        //max kredi kontrolü yap,
        //minimum kredi kontrolü yap 
        [HttpPost]
        public ActionResult DersSecimKaydet()
        {
            
            ObsDbContext ctx = new ObsDbContext();
            int Id = Convert.ToInt32(User.Identity.Name);
            var o = ctx.Ogrenci.FirstOrDefault(a => a.OgrenciNo == Id);
            bool krediDurumuUygunMu = false;
            List<AcilanDersler> secilmis = (List < AcilanDersler > )Session["secilmis"];
            List<Kayit> onaylanmamisSilinecekKayitlar = null;
            Kayit k=null;
            int sum=0;
            Dersler d=null;
            foreach(var i in secilmis)
            {
                d = ctx.Dersler.FirstOrDefault(a => a.DersKodu == i.DersKodu);
                if(d.Kredi.HasValue)
                {
                    sum = sum + d.Kredi.Value;

                }
            }
            if (sum <= 36) krediDurumuUygunMu = true;
            if(krediDurumuUygunMu)
            {
                //session'dan dbye kaydet
                //
                onaylanmamisSilinecekKayitlar = ctx.Kayit.Where(a => a.OgrenciNo == o.OgrenciNo && a.OnaylandiMi == false).ToList();
                foreach(var i in onaylanmamisSilinecekKayitlar)
                {
                    ctx.Kayit.Remove(i);
                }
                foreach(var i in secilmis)
                {
                    k.ADId = i.ADId;
                    k.OgrenciNo = o.OgrenciNo;
                    k.OnaylandiMi = false;
                    k.NotId = 0;
                    ctx.Kayit.Add(k);
                }
                ctx.SaveChanges();
                ctx.Dispose();
                Session["secilmis"] = null;
                return RedirectToAction("Index");
            }
            ctx.Dispose();
            return RedirectToAction("Index");//hata raporu döndür, tempdata
        }

        [HttpGet]
        public ActionResult TranskriptGor()
        {
            //öğrenciyi, kayıtları ve notları db'den eager loading yöntemi ile çek. home index'te örnek var.
            ObsDbContext ctx = new ObsDbContext();
            int Id = Convert.ToInt32(User.Identity.Name);
            var o = ctx.Ogrenci.Include("kayitlar").Include("kayitlar.not").Include("kayitlar.AcilanDers").FirstOrDefault(a => a.OgrenciNo == Id);
            Ogrenci ogrenci = o;
            var k = o.kayitlar;
            var n = k.Select(a => a.not);
            return View(ogrenci);
        }

        [HttpGet]
        public ActionResult SinavSonuclarimiGor(int yil)
        {
            //db'den çek, öğrenciyi çek, yıl bilgisi ile tuple'a ata
            Ogrenci ogrenci = null;
            Tuple<int, Ogrenci> ogrenci_yil = null;
            //
            return View(ogrenci_yil);
        }

    }
}
