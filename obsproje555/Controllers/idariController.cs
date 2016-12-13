using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proje_obs.Controllers
{
    public class idariController : Controller
    {

        String authorize = "idari";
        // GET: idari
        public ActionResult Index()
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login");
            }
            else if (!User.IsInRole(authorize))
            {
                return RedirectToAction("Index", "Home");
            }
            var ctx = new ObsDbContext();
            int Id = Convert.ToInt32(User.Identity.Name);
            var o = ctx.Idari.FirstOrDefault(a => a.Id == Id);
            if(o!=null)
            {
                return View(o);
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(int Id, String password)
        {
            ObsDbContext ctx = new ObsDbContext();
            var ogr = ctx.Idari.FirstOrDefault(o => o.Id == Id);
            if (ogr != null)
            {
                _Membership.Login(Convert.ToString(Id), "idari", Response);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Logout()
        {
            _Membership.Logout();
            return RedirectToAction("Index", "Home");
        }

        //storage iptal
        [HttpGet]
        public ActionResult UploadBanner()
        {
            return View();
        }

        //storage iptal
        [HttpPost]
        public ActionResult UploadBanner(HttpPostedFileBase file)
        {
            //_Storage.UploadFile(file, "__banner.jpg");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DersSecmeTarihiBelirle()
        {
            //
            return View();
        }

        [HttpPost]
        public ActionResult DersSecmeTarihiBelirle(String baslangic, String bitis)
        {
            //
            return View();
        }

        [HttpGet]
        public ActionResult DersEklemeTarihiBelirle()
        {
            //
            return View();
        }

        [HttpPost]
        public ActionResult DersEklemeTarihiBelirle(String baslangic, String bitis)
        {
            //
            return View();
        }

        [HttpGet]
        public ActionResult DersEklemeTalepleriListele()
        {
            List<AcilanDersler> eklemeTalepleri = null;
            //
            return View(eklemeTalepleri);
        }

        [HttpPost]
        public ActionResult DersEklemeTalebiniOnayla(int acilanDersId)
        {
            //
            return RedirectToAction("DersEklemeTalepleriListele");
        }

        [HttpGet]
        public ActionResult OgrenciListele()
        {
            ObsDbContext ctx = new ObsDbContext();
            List<Ogrenci> ogrenciler = ctx.Ogrenci.ToList();
            ctx.Dispose();
            return View(ogrenciler);
        }

        [HttpGet]
        public ActionResult OgrenciEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult OgrenciEkle(int OgrenciNo,string Ad,string Soyad,string Cinsiyet,string DogumTarihi,string Eposta,string Telefon,double? MezunOlunanOkulPuani,string AktifKayitDonemi,int DanismanId,
        string Sifre)
        {
            Ogrenci o = new Ogrenci();
            o.Ad = Ad;
            o.Soyad = Soyad;
            o.Cinsiyet = Cinsiyet;
            o.DogumTarihi = DogumTarihi;
            o.Eposta = Eposta;
            o.Telefon = Telefon;
            o.MezunOlunanOkulPuani = MezunOlunanOkulPuani;
            o.DanismanId = DanismanId;
            o.Sifre = Sifre;
            ObsDbContext ctx = new ObsDbContext();
            ctx.Ogrenci.Add(o);
            ctx.Dispose();
            return RedirectToAction("OgrenciListele");
        }

        [HttpGet]
        public ActionResult DersSorumlusuListele()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DersSorumlusuEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DersSorumlusuEkle(int id/*,ilgili parametreler*/)
        {
            return RedirectToAction("DersSorumlusuListele");
        }

        [HttpGet]
        public ActionResult idariListele()
        {
            return View();
        }

        [HttpGet]
        public ActionResult idariEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult idariEkle(int id/*parametreler bi sürü*/)
        {
            return RedirectToAction("idariListele");
        }


    }
}