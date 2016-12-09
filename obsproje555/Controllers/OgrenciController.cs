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
            bool DersSecmeHaftasi= false;
            //
            if(DersSecmeHaftasi)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        //bir ders seçildiğinde ders kodu post edilecek, bir tür session cookie'sinde falan geçici olarak seçilmiş dersler tutulacak
        [HttpPost]
        public ActionResult DersSecme(int dersId)
        {
            //
            return View();
        }

        //max kredi kontrolü yap, 
        [HttpPost]
        public ActionResult DersSecimKaydet()
        {
            bool MaxKrediAsilmadi = false;
            //
            if(MaxKrediAsilmadi)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public ActionResult TranskriptGor()
        {
            return View();
        }

    }
}