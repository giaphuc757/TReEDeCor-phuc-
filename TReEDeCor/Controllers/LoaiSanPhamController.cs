using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TReEDeCor.Models;
using System.IO;

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
        public ActionResult Create(LOAISANPHAM loaisp, HttpPostedFileBase fileUp)
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
                loaisp.AnhLoaiSP = fileName;
            }
            db.LOAISANPHAMs.InsertOnSubmit(loaisp);
            db.SubmitChanges();
            return RedirectToAction("Index", "LoaiSanPham");
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
        [ValidateInput(false)]
        public ActionResult capnhat(LOAISANPHAM loaisp, HttpPostedFileBase fileUp)
        {
            if (ModelState.IsValid)
            {
                //tim kiem xem có sp đó trong db hay không? nếu có mình mới update
                LOAISANPHAM spUpdate = db.LOAISANPHAMs.SingleOrDefault(p => p.MaLoaiSP == loaisp.MaLoaiSP);

                if (spUpdate != null)
                {
                    if (fileUp != null)
                    {
                        var fileName = Path.GetFileName(fileUp.FileName);
                        var path = Path.Combine(Server.MapPath("~/Image/IMG-PRODUCT/"), fileName);
                        if (System.IO.File.Exists(path))
                        {
                            TempData["Error"] = "Hình Đã Tồn Tại!";
                            return RedirectToAction("Edit", new { id = loaisp.MaLoaiSP });
                        }
                        else
                        {
                            fileUp.SaveAs(path);
                            spUpdate.AnhLoaiSP = fileName;
                        }
                    }
                    spUpdate.TenLoaiSP = loaisp.TenLoaiSP;
                    spUpdate.AnhLoaiSP = loaisp.AnhLoaiSP;
                    spUpdate.Mota = loaisp.Mota;
                    UpdateModel(spUpdate);
                    db.SubmitChanges();
                }
            }
            return RedirectToAction("Index", "LoaiSanPham");
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
            return RedirectToAction("Index", "LoaiSanPham");
        }
    }
}