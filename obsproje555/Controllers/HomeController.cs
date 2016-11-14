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