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

        [HttpPost]
        public ActionResult Login(DersSorumlulari ogretimElemani)
        {
            ObsDbContext ctx = new ObsDbContext();
            var ogr = ctx.DersSorumlulari.FirstOrDefault(o => o.AkademisyenID == ogretimElemani.AkademisyenID &&
            o.Sifre == ogretimElemani.Sifre);
            if(ogr != null)
            {
                Session.Add("Id", ogretimElemani.AkademisyenID);
                Session.Add("Ad", ogr.Adi);
                Session.Add("Role", "OgretimElemani");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}