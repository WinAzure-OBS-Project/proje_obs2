using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proje_obs.Controllers
{
    public class OgretimElemaniController : Controller
    {
        // GET: OgretimElemani
        public ActionResult Index()
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login");
            }
            else if (!User.IsInRole("Hoca"))
            {
                return RedirectToAction("Index", "Home");
            }
            var ctx = new ObsDbContext();
            var Id = Convert.ToInt32(User.Identity.Name);
            var ogr = ctx.DersSorumlulari.FirstOrDefault(o => o.AkademisyenID == Id);
            if (ogr != null)
            {
                return View(ogr);
            }
            ctx.Dispose();
            return View(ogr);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(int Id, String Password)
        {
            ObsDbContext ctx = new ObsDbContext();
            var ogr = ctx.DersSorumlulari.FirstOrDefault(o => o.AkademisyenID == Id);
            if (ogr != null)
            {
                _Membership.Login(Convert.ToString(ogr.AkademisyenID), "Hoca", Response);
            }
            ctx.Dispose();
            return RedirectToAction("Index");
        }

        //eski yöntem
        //[HttpPost]
        //public ActionResult Login(DersSorumlulari ogretimElemani)
        //{
        //    ObsDbContext ctx = new ObsDbContext();
        //    var ogr = ctx.DersSorumlulari.FirstOrDefault(o => o.AkademisyenID == ogretimElemani.AkademisyenID &&
        //    o.Sifre == ogretimElemani.Sifre);
        //    if (ogr != null)
        //    {
        //        Session.Add("Id", ogretimElemani.AkademisyenID);
        //        Session.Add("Ad", ogr.Adi);
        //        Session.Add("Role", "OgretimElemani");
        //    }
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        public ActionResult Logout()
        {
            _Membership.Logout();
            return RedirectToAction("Index", "Home");
        }
        

        [HttpGet]
        public ActionResult DersSecmeTalepleriniGor()
        {
            Dictionary<Ogrenci, List<Kayit>> dersSecmeTalepleri = null;
            ObsDbContext ctx = new ObsDbContext();
            ctx.Ogrenci.Include("kayitlar").Include("kayitlar.AcilanDers")
                .Where(ogrenci => ogrenci.kayitlar.Any(kayit => kayit.OnaylandiMi == false && ogrenci.DanismanId == Convert.ToInt32(User.Identity.Name)))
                .ToDictionary(ogrenci=>ogrenci, ogrenci=>ogrenci.kayitlar);
            ctx.Dispose(); //çok güzel yazmışım da kayıt listesine burada gerek yokmuş :'( öğretici/hatırlatıcı amaçla dursun burda

            return View(dersSecmeTalepleri);
        }

        [HttpGet]
        public ActionResult DersSecmeTalebiniIncele(int ogrenciId)
        {
            Tuple<Ogrenci, List<Kayit>> talep; //doldur
            //
            ObsDbContext ctx = new ObsDbContext();
            Ogrenci ogrenci = ctx.Ogrenci.Include("kayitlar").First(o => o.OgrenciNo == ogrenciId);
            talep = new Tuple<Ogrenci, List<Kayit>>(ogrenci, ogrenci.kayitlar.ToList());
            ctx.Dispose();

            return View(talep);
        }

        [HttpPost]
        public ActionResult DersSecmeTalebiniOnayla(int ogrenciNo)
        {
            ObsDbContext ctx = new ObsDbContext();
            var kayitlar = ctx.Ogrenci.Include("kayitlar").First(o => o.OgrenciNo == ogrenciNo).kayitlar;
            foreach(Kayit k in kayitlar)
            {
                k.OnaylandiMi = true;
            }
            ctx.SaveChanges();
            ctx.Dispose();

            return RedirectToAction("DersSecmeTalepleriniGor");
        }

        [HttpGet]
        public ActionResult DersEklemeTalebi()
        {
            bool DersEklemeHaftasi = false;
            ObsDbContext ctx = new ObsDbContext();

            DersTarihler dt = ctx.DersTarihler.Last();
            DersEklemeHaftasi = (DateTime.Now > dt.dersAcmaBaslangic && DateTime.Now < dt.dersAcmaBitis);

            List<Dersler> dersler = null;
            //
            if(DersEklemeHaftasi)
            {
                dersler = ctx.Dersler.ToList();
                return View(dersler);
            }
            else
            {
                //
                return RedirectToAction("DersEklemeTaleplerimiListele");
            }
        }

        [HttpGet]
        public ActionResult DersEklemeTaleplerimiListele()
        {
            //hocanın taleplerinden, henüz onaylanmamışları getir
            List<AcilanDersler> taleplerim = null;
            int uid = Convert.ToInt32(User.Identity.Name);
            ObsDbContext ctx = new ObsDbContext();
            taleplerim = ctx.AcilanDersler.Where(acilanDers => acilanDers.AkademisyenId == uid && acilanDers.OnaylandiMi == false).ToList();
            ctx.Dispose();

            return View(taleplerim);
        }


        [HttpGet]
        public ActionResult TalebimiSil(int acilanDersId)
        {
            ObsDbContext ctx = new ObsDbContext();
            ctx.AcilanDersler.Remove(ctx.AcilanDersler.First(acilanDers => acilanDers.ADId == acilanDersId));
            ctx.SaveChanges();
            ctx.Dispose();

            return RedirectToAction("DersEklemeTaleplerimiListele");
        }

        [HttpPost]
        public ActionResult DersEklemeTalebi(String DersKodu, String DersAdi, int YilDers, int Yariyil)
        {
            ObsDbContext ctx = new ObsDbContext();
            AcilanDersler acilanDers = new AcilanDersler();
            acilanDers.AkademisyenId = Convert.ToInt32(User.Identity.Name);
            acilanDers.DersAdi = DersAdi;
            acilanDers.DersKodu = DersKodu;
            acilanDers.YilDers = YilDers;
            acilanDers.YariYil = Yariyil;
            acilanDers.OnaylandiMi = false;
            ctx.AcilanDersler.Add(acilanDers);
            ctx.SaveChanges();
            ctx.Dispose();

            return RedirectToAction("DersEklemeTaleplerimiListele");
        }

        [HttpGet]
        public ActionResult DersiAlanOgrenciler(int acilanDersId)
        {
            List<Ogrenci> ogrenciler = new List<Ogrenci>();
            //bu yılkiler
            ObsDbContext ctx = new ObsDbContext();
            var acilanDers = ctx.AcilanDersler.First(ad => ad.ADId == acilanDersId);
            var kayitlar = acilanDers.Kayitlar;
            foreach(Kayit k in kayitlar)
            {
                if(ogrenciler.Contains(k.Ogrenci))
                {
                    ogrenciler.Add(k.Ogrenci);
                }
            }
            ctx.Dispose();

            return View(ogrenciler);
        }

        [HttpGet]
        public ActionResult BuSenekiDerslerimiGor()
        {
            ObsDbContext ctx = new ObsDbContext();
            int Id = Convert.ToInt32(User.Identity.Name);
            var o = ctx.DersSorumlulari.FirstOrDefault(a => a.AkademisyenID == Id);
            List<AcilanDersler> dersler = null;

            dersler = o.AcilanDersler.Where(ad => ad.YilDers == DateTime.Now.Year).ToList();

            ctx.Dispose();

            return View(dersler);
        }

        //başka yıl
        [HttpGet]
        public ActionResult BaskaSenekiDerslerimiGor(int yil)
        {
            List<AcilanDersler> dersler = null;
            ObsDbContext ctx = new ObsDbContext();
            dersler = ctx.AcilanDersler.Where(acilanDers => acilanDers.YilDers == yil).ToList();
            ctx.Dispose();

            return View(dersler);
        }

        //kayıt tablosundan çekilecek
        [HttpGet]
        public ActionResult SinavSonuclariGir(int dersId)
        {
            //eager loading, öğrenciler, kayıtları ve notlarını çek include ile. ya da çekme bilemedim

            Dictionary<Ogrenci, Notlar> ogrenci_not = new Dictionary<Ogrenci, Notlar>();
            
            ObsDbContext ctx = new ObsDbContext();

            var acilanDersler = ctx.AcilanDersler.First(ad => ad.ADId == dersId);
            var kayitlar = acilanDersler.Kayitlar;
            foreach(Kayit k in kayitlar)
            {
                ogrenci_not.Add(k.Ogrenci, k.not);
            }
            
            return View(ogrenci_not);
            //view düzeltilmeli, hala viewbag var ya
        }
        
        [HttpPost]
        public ActionResult SinavSonucunuKaydet(int notId, int vize, int final, int butunleme)
        {
            ObsDbContext ctx = new ObsDbContext();
            Notlar not = ctx.Notlar.First(n => n.NotId == notId);
            not.Vize = vize;
            not.Final = final;
            not.But = butunleme;
            ctx.SaveChanges();
            ctx.Dispose();

            return RedirectToAction("SinavSonuclariGir");
        }
    }
}