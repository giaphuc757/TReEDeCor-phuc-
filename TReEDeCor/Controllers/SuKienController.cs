using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TReEDeCor.Controllers
{
    public class SuKienController : Controller
    {
        // GET: SuKien
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SuKienNew()
        {
            return View();
        }
    }
}