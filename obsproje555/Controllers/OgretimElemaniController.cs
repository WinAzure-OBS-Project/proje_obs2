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
            if (Session["Id"] == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

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
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult DersSecmeTalepleriniGor()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DersSecmeTalebiniIncele(int dersEklemeTalebiId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult DersSecmeTalebiniOnayla(int dersEklemeTalebiId)
        {
            return View();
        }

        [HttpGet]
        public ActionResult DersEklemeTalebi()
        {
            bool DersEklemeHaftasi = false;
            //
            if(DersEklemeHaftasi)
            {
                //
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult DersEklemeTalebi(int asdf/*buraya ders parametreleri gelecek*/)
        {
            //
            return View();
        }

        [HttpGet]
        public ActionResult BuSenekiDerslerimiGor()
        {
            //
            return View();
        }

        //kayıt tablosundan çekilecek
        [HttpGet]
        public ActionResult SinavSonuclariGir(int dersId)
        {
            //
            return View();
        }

        [HttpPost]
        public ActionResult SinavSonucunuKaydet(int kayitId /*değişikliğin yapıldığı kayıt parametreleri*/)
        {
            //
            return View();
        }
    }
}