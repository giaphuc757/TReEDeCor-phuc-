using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TReEDeCor.Models;

namespace TReEDeCor.Controllers
{
    public class PhroDuctController : Controller
    {
        DatabaseDataContext data = new DatabaseDataContext();
        public List<SANPHAM> getList()
        {
            var list = data.SANPHAMs.ToList();
            return list;
        }
        public ActionResult Index()
        {
            var All_Loại = data.SANPHAMs.ToList();
            return View(All_Loại);
        }
        public ActionResult Chitietsanpham(int id)
        {
            var sp = from s in data.SANPHAMs 
                     where s.MaSP == id select s;
            return View(sp.Single());
        }
    }
}