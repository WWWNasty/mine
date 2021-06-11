using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
 using  TG.Exam.WebMVC.Models;

namespace TG.Exam.WebMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var data = TG.Exam.WebMVC.Models.User.GetAll();
            return View(data);
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