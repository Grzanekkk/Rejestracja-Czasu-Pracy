using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RejestracjaCzasuPracy.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string path = "../TestPage";
            return View(path);
        }

        public ActionResult Details()
        {
            return RedirectToAction("Index");
        }
    }
}
