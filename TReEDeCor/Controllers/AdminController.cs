using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TReEDeCor.Models;
using PagedList;
using PagedList.Mvc;

namespace TReEDeCor.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        DatabaseDataContext db = new DatabaseDataContext();
        public ActionResult Index()
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            
            return View();
        }
        [HttpGet]
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var tendn = collection["username"];
            var matkhau = collection["pass"];

            
                Admin ad = db.Admins.SingleOrDefault(n => n.UserAdmin == tendn && n.PassAdmin == matkhau);
                if (ad != null)
                {

                    Session["TKAdmin"] = ad;
                    return RedirectToAction("Index", "Admin");

                }
                else
                    ViewBag.ThongBao = "Tên Đăng Nhập Hoặc Tài Khoản Không Đúng";
            
            return View();
        }


        ///////////////////////////////////
        public ActionResult Product()
        {
            return View(db.SANPHAMs.ToList());
        }

        public ActionResult Details(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                var sanpham = from SANPHAM in db.SANPHAMs where SANPHAM.MaSP == id select SANPHAM;
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
        public ActionResult Create(SANPHAM sanpham)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                db.SANPHAMs.InsertOnSubmit(sanpham);
                db.SubmitChanges();

                return View("Index","Admin");
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
                var sanpham = from SANPHAM in db.SANPHAMs where SANPHAM.MaSP == id select SANPHAM;
                return View(sanpham.SingleOrDefault());
            }
        }
        [HttpPost, ActionName("Edit")]
        public ActionResult capnhat(int id)
        {
            SANPHAM sanpham = db.SANPHAMs.Where(n => n.MaSP == id).SingleOrDefault();
            UpdateModel(sanpham);
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
                var sanpham = from SANPHAM in db.SANPHAMs where SANPHAM.MaSP == id select SANPHAM;
                return View(sanpham.SingleOrDefault());
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult xoasp(int id)
        {
            SANPHAM sanpham = db.SANPHAMs.Where(n => n.MaSP == id).SingleOrDefault();
            db.SANPHAMs.DeleteOnSubmit(sanpham);
            db.SubmitChanges();
            return RedirectToAction("Index", "Admin");
        }
    }
}