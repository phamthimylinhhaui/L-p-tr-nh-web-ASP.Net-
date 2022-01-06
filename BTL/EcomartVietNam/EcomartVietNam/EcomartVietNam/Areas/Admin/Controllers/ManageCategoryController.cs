using EcomartVietNam.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EcomartVietNam.Areas.Admin.Controllers
{
    public class ManageCategoryController : ProtectAdminController
    {
        EcomartStoreDB db = new EcomartStoreDB();
        // GET: Admin/ManageCategory
        public ActionResult Index()
        {
            var categories = db.Categories.OrderByDescending(c=>c.category_id).ToList();
            return View(categories);
        }
        // GET: Admin/ManageCategory/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Categories.Add(category);
                    db.SaveChanges();

                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                ViewBag.Error = "Dữ liệu không hợp lệ!";
                return View(category);
            }
        }


        // GET: Admin/ManageCategory/Update
        public ActionResult Update(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Category catalogy)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(catalogy).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Dữ liệu không hợp lệ!";
                return View(catalogy);
            }


        }


        [HttpPost]
        public JsonResult Delete(int? id)
        {
            Category category = db.Categories.Find(id);
            try
            {
                db.Categories.Remove(category);
                db.SaveChanges();
                bool success = true;

                return Json(success, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                bool success = false;
                return Json(success, JsonRequestBehavior.AllowGet);
            }

        }
    }
}