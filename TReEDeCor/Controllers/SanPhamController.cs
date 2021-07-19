using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TReEDeCor.Models;
using System.IO;
using PagedList;
using PagedList.Mvc;
namespace TReEDeCor.Controllers
{

    public class SanPhamController : Controller
    {
        // GET: SanPham
        DatabaseDataContext db = new DatabaseDataContext();
        public ActionResult Index(int? page)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                int pageNumber = (page ?? 1);
                int pageSize = 4;
                return View(db.SANPHAMs.ToList().OrderBy(n => n.MaLoaiSP).ToPagedList(pageNumber, pageSize));
            }
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
                ViewBag.MaLoaiSP = new SelectList(db.LOAISANPHAMs.ToList().OrderBy(n => n.TenLoaiSP), "MaLoaiSP", "TenLoaiSP");
            ViewBag.MaNCC = new SelectList(db.NHACUNGCAPs.ToList().OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");
            return View();
        }
        [HttpPost]
        public ActionResult Create(SANPHAM sanpham, HttpPostedFileBase fileUp)
        {
            var fileName = Path.GetFileName(fileUp.FileName);
            var path = Path.Combine(Server.MapPath("/Image/IMG-PRODUCT/"), fileName);
            if (System.IO.File.Exists(path))
            {
                ViewBag.ThongBao = "Hình Đã Tồn Tại!";
            }
            else
            {
                fileUp.SaveAs(path);
                sanpham.AnhSP = fileName;
            }
            db.SANPHAMs.InsertOnSubmit(sanpham);
            db.SubmitChanges();
            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            SANPHAM sanpham = db.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                ViewBag.MaLoaiSP = new SelectList(db.LOAISANPHAMs.ToList().OrderBy(n => n.TenLoaiSP), "MaLoaiSP", "TenLoaiSP");
                ViewBag.MaNCC = new SelectList(db.NHACUNGCAPs.ToList().OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");
                ViewBag.Message = TempData["Error"];
                return View(sanpham);
            }
        }
        [HttpPost, ActionName("Edit")]
        [ValidateInput(false)]
        public ActionResult capnhat(SANPHAM sanpham, HttpPostedFileBase fileUp)
        {

            if (ModelState.IsValid)
            {
                //tim kiem xem có sp đó trong db hay không? nếu có mình mới update
                SANPHAM spUpdate = db.SANPHAMs.SingleOrDefault(p => p.MaSP == sanpham.MaSP);

                if(spUpdate != null) {
                    if (fileUp != null)
                    {
                        var fileName = Path.GetFileName(fileUp.FileName);
                        var path = Path.Combine(Server.MapPath("~/Image/IMG-PRODUCT/"), fileName);
                        if (System.IO.File.Exists(path))
                        {
                            TempData["Error"] = "Hình Đã Tồn Tại!";
                            return RedirectToAction("Edit", new { id = sanpham.MaSP });
                        }
                        else
                        {
                            fileUp.SaveAs(path);
                            spUpdate.AnhSP = fileName;
                        }
                    }

                    //mình demo 2 cái thôi còn lại tự thêm vào
                    spUpdate.TenSP = sanpham.TenSP;
                    spUpdate.TrangThai = sanpham.TrangThai;


                    UpdateModel(spUpdate);
                    db.SubmitChanges();
                }
               
            }

            return RedirectToAction("Index", "SanPham");

            ////SANPHAM sanpham = db.SANPHAMs.Where(n => n.MaSP == id).SingleOrDefault();
            //UpdateModel(sanpham);
            //db.SubmitChanges();

        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "SanPham");
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
            return RedirectToAction("Index", "SanPham");
        }
    }
}