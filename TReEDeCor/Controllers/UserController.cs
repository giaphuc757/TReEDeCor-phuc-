using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TReEDeCor.Models;

namespace TReEDeCor.Controllers
{
    public class UserController : Controller
    {
        // GET: PhroDuct

        DatabaseDataContext data = new DatabaseDataContext();
        private List<SANPHAM> laysanpham(int count)
        {
            return data.SANPHAMs.OrderByDescending(a => a.Ngaycapnhat).Take(count).ToList();
        }
        public ActionResult Index()
        {
            var noithat = laysanpham(12);
            return View(noithat);
        }

        public ActionResult Doanhmucsanpham()
        {
            var doanhmuc = from tt in data.SLIDERs select tt;
            return PartialView(doanhmuc);
        }
        public ActionResult Quangcao()
        {
            var doanhmuc = from tt in data.SLIDERs select tt;
            return PartialView(doanhmuc);
        }
        public ActionResult Menu()
        {
            var All_Loại = from loai in data.LOAISANPHAMs select loai;
            return PartialView(All_Loại);
        }

        //SANPHAM them loai
        public ActionResult SPtheoloai(int id)
        {
            var sp = from s in data.SANPHAMs
                     where s.MaLoaiSP == id
                     select s;
            return PartialView(sp);
        }
    }
}