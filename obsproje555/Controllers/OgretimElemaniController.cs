using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proje_obs.Models;

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
        public ActionResult Login(OgretimElemani ogretimElemani)
        {
            ObsContext ctx = new ObsContext();
            var ogr = ctx.OgretimElemanlari.FirstOrDefault(o => o.OgretimElemaniId == ogretimElemani.OgretimElemaniId &&
            o.Sifre == ogretimElemani.Sifre);
            if(ogr != null)
            {
                Session.Add("Id", ogretimElemani.OgretimElemaniId);
                Session.Add("Ad", ogr.Ad);
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