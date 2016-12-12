using System;
using System.Collections.Generic;
using System.Linq;
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
            //var a1 = ctx.Notlar.FirstOrDefault(a => a.NotId == 5);
            //var a2 = a1.kayit;
            //var a3 = a2.Ogrenci;
            //var a4 = a3.Ad;
            if (Session["Role"] != null)
            {
                String role = Session["Role"].ToString();
                if (role == "Ogrenci")
                {
                    return RedirectToAction("Index", "Ogrenci");
                }
                else if (role == "OgretimElemani")
                {
                    return RedirectToAction("Index", "OgretimElemani");
                }

            }
            return View();
        }
    }
}