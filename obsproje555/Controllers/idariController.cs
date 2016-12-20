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
        public ActionResult TarihleriListele()//bu ekranda silip ekleme yapacak
        {
            List<DersTarihler> tarihler = null;
            ObsDbContext ctx = new ObsDbContext();
            tarihler = ctx.DersTarihler.ToList();
            ctx.Dispose();

            return View(tarihler);
        }

        [HttpPost]
        public ActionResult TarihEkle(String dersAcmaBaslangic, String dersAcmaBitis, String dersSecmeBaslangic, String dersSecmeBitis)
        {
            //
            ObsDbContext ctx = new ObsDbContext();
            DersTarihler dt = new DersTarihler();
            dt.dersAcmaBaslangic = DateTime.Parse(dersAcmaBaslangic);
            dt.dersAcmaBitis = DateTime.Parse(dersAcmaBitis);
            dt.dersSecmeBaslangic = DateTime.Parse(dersSecmeBaslangic);
            dt.dersSecmeBitis = DateTime.Parse(dersSecmeBitis);
            ctx.DersTarihler.Add(dt);
            ctx.SaveChanges();
            ctx.Dispose();

            return RedirectToAction("TarihleriListele");
        }

        [HttpPost]
        public ActionResult TarihiSil(int id)
        {
            ObsDbContext ctx = new ObsDbContext();
            ctx.DersTarihler.Remove(ctx.DersTarihler.First(dt => dt.Id == id.ToString()));
            ctx.SaveChanges();
            ctx.Dispose();

            return RedirectToAction("TarihleriListele");
        }

        [HttpGet]
        public ActionResult DersEklemeTalepleriListele()
        {
            List<AcilanDersler> eklemeTalepleri = null;
            ObsDbContext ctx = new ObsDbContext();
            eklemeTalepleri = ctx.AcilanDersler.Where(acilanDers => acilanDers.OnaylandiMi == false).ToList();
            ctx.Dispose();

            return View(eklemeTalepleri);
        }

        [HttpPost]
        public ActionResult DersEklemeTalebiniOnayla(int acilanDersId)
        {
            ObsDbContext ctx = new ObsDbContext();
            ctx.AcilanDersler.First(acilanDers => acilanDers.ADId == acilanDersId).OnaylandiMi = true;
            ctx.SaveChanges();
            ctx.Dispose();

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

        [HttpPost]
        public ActionResult OgrenciyeDanismanEkle(int ogrenciId, int danismanId)
        {
            ObsDbContext ctx = new ObsDbContext();
            ctx.Ogrenci.First(ogrenci => ogrenci.OgrenciNo == ogrenciId).DanismanId = danismanId;
            ctx.SaveChanges();
            ctx.Dispose();

            return RedirectToAction("OgrenciListele");
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
            ctx.SaveChanges();
            ctx.Dispose();
            return RedirectToAction("OgrenciListele");
        }

        
        [HttpGet]
        public ActionResult DersSorumlusuListele()
        {
            ObsDbContext ctx = new ObsDbContext();
            

            List<DersSorumlulari> dersSorumlulari = ctx.DersSorumlulari.ToList();
            ctx.Dispose();

            return View(dersSorumlulari);
        }

        [HttpGet]
        public ActionResult DersSorumlusuEkle()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DersSorumlusuSil(int hocaId)
        {
            ObsDbContext ctx = new ObsDbContext();
            ctx.DersSorumlulari.Remove(ctx.DersSorumlulari.First(hoca=>hoca.AkademisyenID == hocaId));
            ctx.SaveChanges();
            ctx.Dispose();

            return RedirectToAction("DersSorumlusuListele");
        }

        [HttpPost]
        public ActionResult DersSorumlusuEkle(int AkademisyenID, string Adi, string Soyadi, string Unvani, string Telefonu,
            string Maili, string OdaNo, string Sifre, string DanismanMi)
        {
            DersSorumlulari ds = new DersSorumlulari();
            ds.AkademisyenID = AkademisyenID;
            ds.Adi = Adi;
            ds.Soyadi = Soyadi;
            ds.Unvani = Unvani;
            ds.Telefonu = Telefonu;
            ds.Maili = Maili;
            ds.OdaNo = OdaNo;
            ds.Sifre = Sifre;
            ds.DanismanMi = Convert.ToBoolean(DanismanMi);
            ObsDbContext ctx = new ObsDbContext();
            ctx.DersSorumlulari.Add(ds);
            ctx.SaveChanges();
            ctx.Dispose();

            return RedirectToAction("DersSorumlusuListele");
        }

        [HttpPost]
        public ActionResult DanismanlikDurumuDuzenle(int hocaId, int durum)//1=true,0=false
        {
            ObsDbContext ctx = new ObsDbContext();
            ctx.DersSorumlulari.First(ds => ds.AkademisyenID == hocaId).DanismanMi = Convert.ToBoolean(durum);
            ctx.SaveChanges();
            ctx.Dispose();

            return RedirectToAction("DersSorumlusuListele");
        }
        

        [HttpGet]
        public ActionResult idariListele()
        {
            ObsDbContext ctx = new ObsDbContext();
            List<Idari> idariler = ctx.Idari.ToList();
            ctx.Dispose();

            return View(idariler);
        }

        [HttpGet]
        public ActionResult idariEkle()
        {


            return View();
        }

        [HttpPost]
        public ActionResult idariEkle(int Id, string Unvan, string Adi, string Soyadi,
            string Mail, string Tel, string Fax, string Adres, string Sifre)
        {
            Idari idari = new Idari();
            idari.Id = Id;
            idari.Unvan = Unvan;
            idari.Adi = Adi;
            idari.Soyadi = Soyadi;
            idari.Mail = Mail;
            idari.Tel = Tel;
            idari.Fax = Fax;
            idari.Adres = Adres;
            idari.Sifre = Sifre;
            ObsDbContext ctx = new ObsDbContext();
            ctx.Idari.Add(idari);
            ctx.SaveChanges();
            ctx.Dispose();

            return RedirectToAction("idariListele");
        }


    }
}