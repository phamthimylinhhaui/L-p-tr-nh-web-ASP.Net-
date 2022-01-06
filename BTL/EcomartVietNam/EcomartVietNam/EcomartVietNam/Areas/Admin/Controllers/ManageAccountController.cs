using EcomartVietNam.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EcomartVietNam.Areas.Admin.Controllers
{
    public class ManageAccountController : ProtectAdminController
    {
        EcomartStoreDB db = new EcomartStoreDB();
        // GET: Admin/Account
        public ActionResult Index()
        {
            var account = db.Users.OrderByDescending(a=>a.user_id).ToList();
            return View(account);
        }
        public ActionResult Create()
        {
            List<int> roles = new List<int>
            {
                0,1
            };
            ViewBag.Roles = roles;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection frm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string full_name = frm["full_name"];
                    string phone = frm["phone_number"];
                    string email = frm["email"];
                    string password = frm["password"];
                    string address = frm["address"];
                    string confirm_password = frm["confirm_password"];
                    string role = frm["role"];
                    string is_active = frm["is_active"];
                    if (!password.Equals(confirm_password))
                    {
                        ViewBag.Error = "Mật khẩu không khớp.";
                        return View();
                    }

                    var us = db.Users.Where(u => u.email == email).SingleOrDefault();

                    if (us != null)
                    {
                        ViewBag.Error = "Vui lòng nhập địa chỉ email khác.";
                        return View();
                    }
                    User user = new User();
                    user.full_name = full_name;
                    user.phone_number = phone;
                    user.email = email;
                    user.password = Helper.EncodePassword(password);
                    user.address = address;
                    user.role = role.Equals("Quản trị")? 1 : 0;
                    user.is_active = is_active == null ? false : true;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Users.Add(user);
                    db.SaveChanges();

                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                ViewBag.Error = "Dữ liệu không hợp lệ!";
                return View();
            }
        }

        public ActionResult Update(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(FormCollection frm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //string full_name = frm["full_name"];
                    //string phone = frm["phone_number"];
                    string email = frm["email"];
                    //string password = frm["password"];
                    //string address = frm["address"];
                    //string confirm_password = frm["confirm_password"];
                    string role = frm["role"];
                    string is_active = frm["is_active"];
                    //if (!password.Equals(confirm_password))
                    //{
                    //    ViewBag.Error = "Mật khẩu không khớp.";
                    //    return View();
                    //}
                    var user = db.Users.Where(u => u.email == email).SingleOrDefault();

                    if (user == null)
                    {
                        ViewBag.Error = "tài khoản không tồn tại";
                        return View();
                    }
                    //user.full_name = full_name;
                    //user.phone_number = phone;
                    //user.email = email;
                    //user.password = Helper.EncodePassword(password);
                    //user.address = address;
                    user.role = role.Equals("Quản trị") ? 1 : 0;
                    user.is_active = is_active == null ? false : true;
                    //db.Configuration.ValidateOnSaveEnabled = false;
                    //db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Dữ liệu không hợp lệ!";
                return View();
            }


        }

        [HttpPost]
        public JsonResult Delete(int? id)
        {
            bool success = false;
            User currentUser = Session["admin"] as User;
            if (currentUser.user_id==id)
            {
                 success = false;
                return Json(success, JsonRequestBehavior.AllowGet);
            }
            User user = db.Users.Find(id);
            try
            {
                db.Users.Remove(user);
                db.SaveChanges();
                 success = true;

                return Json(success, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                 success = false;
                return Json(success, JsonRequestBehavior.AllowGet);
            }

        }


    }
}