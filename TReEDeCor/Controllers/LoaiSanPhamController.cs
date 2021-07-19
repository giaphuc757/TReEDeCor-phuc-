using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TReEDeCor.Models;

namespace TReEDeCor.Controllers
{
    public class LoaiSanPhamController : Controller
    {
        DatabaseDataContext db = new DatabaseDataContext();
        // GET: NhaCungCap
        public ActionResult Index()
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            return View(db.LOAISANPHAMs.ToList());
        }

        public ActionResult Details(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                var sanpham = from LOAISANPHAM in db.LOAISANPHAMs where LOAISANPHAM.MaLoaiSP == id select LOAISANPHAM;
                return View(sanpham.SingleOrDefault());
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
                return View();
        }
        [HttpPost]
        public ActionResult Create(LOAISANPHAM loaisp)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                db.LOAISANPHAMs.InsertOnSubmit(loaisp);
                db.SubmitChanges();

                return View("Index", "Admin");
            }
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                var loaisp = from LOAISANPHAM in db.LOAISANPHAMs where LOAISANPHAM.MaLoaiSP == id select LOAISANPHAM;
                return View(loaisp.SingleOrDefault());
            }
        }
        [HttpPost, ActionName("Edit")]
        public ActionResult capnhat(int id)
        {
            LOAISANPHAM loaisp = db.LOAISANPHAMs.Where(n => n.MaLoaiSP == id).SingleOrDefault();
            UpdateModel(loaisp);
            db.SubmitChanges();
            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                var loaisp = from LOAISANPHAM in db.LOAISANPHAMs where LOAISANPHAM.MaLoaiSP == id select LOAISANPHAM;
                return View(loaisp.SingleOrDefault());
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult xoasp(int id)
        {
            LOAISANPHAM loaisp = db.LOAISANPHAMs.Where(n => n.MaLoaiSP == id).SingleOrDefault();
            db.LOAISANPHAMs.DeleteOnSubmit(loaisp);
            db.SubmitChanges();
            return RedirectToAction("Index", "Admin");
        }
    }
}