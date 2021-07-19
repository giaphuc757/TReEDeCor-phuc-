using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TReEDeCor.Models;

namespace TReEDeCor.Controllers
{
    public class NhaCungCapController : Controller
    {
        DatabaseDataContext db = new DatabaseDataContext();
        // GET: NhaCungCap
        public ActionResult Index()
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            return View(db.NHACUNGCAPs.ToList());
        }

        public ActionResult Details(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                var sanpham = from NHACUNGCAP in db.NHACUNGCAPs where NHACUNGCAP.MaNCC == id select NHACUNGCAP;
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
        public ActionResult Create(NHACUNGCAP nhacungcap)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                db.NHACUNGCAPs.InsertOnSubmit(nhacungcap);
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
                var sanpham = from NHACUNGCAP in db.NHACUNGCAPs where NHACUNGCAP.MaNCC == id select NHACUNGCAP;
                return View(sanpham.SingleOrDefault());
            }
        }
        [HttpPost, ActionName("Edit")]
        public ActionResult capnhat(int id)
        {
            NHACUNGCAP nhacungcap = db.NHACUNGCAPs.Where(n => n.MaNCC == id).SingleOrDefault();
            UpdateModel(nhacungcap);
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
                var nhacungcap = from NHACUNGCAP in db.NHACUNGCAPs where NHACUNGCAP.MaNCC == id select NHACUNGCAP;
                return View(nhacungcap.SingleOrDefault());
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult xoasp(int id)
        {
            NHACUNGCAP nhacungcap = db.NHACUNGCAPs.Where(n => n.MaNCC == id).SingleOrDefault();
            db.NHACUNGCAPs.DeleteOnSubmit(nhacungcap);
            db.SubmitChanges();
            return RedirectToAction("Index", "Admin");
        }
    }
}