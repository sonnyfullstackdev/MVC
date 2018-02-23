
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// ********** FYI: System Generated File

namespace ProjectName.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return RedirectToAction("SplashPage", "Member");
        }

        public ActionResult JeremyRamos()
        {
            return View();
        }
        public ActionResult JeremyRamosNg()
        {
            return View();
        }
        public ActionResult Sonny()
        {
            return View();
        }

        public ActionResult SonnyController()
        {
            return View();
        }
        public ActionResult Britto()
        {
            return View();
        }
        public ActionResult Mylinh()
        {
            return View();
        }
        public ActionResult Adan()
        {
            return View();
        }
        public ActionResult Jae()
        {
            return View();
        }
        public ActionResult Tiana()
        {

            return View();
        }

        public ActionResult TianasTestView()
        {
            return View();
        }

        public ActionResult TianasUploadTest()
        {
            return View();
        }

        public ActionResult Alexa()
        {
            return View();
        }

        public ActionResult AngularJae()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}