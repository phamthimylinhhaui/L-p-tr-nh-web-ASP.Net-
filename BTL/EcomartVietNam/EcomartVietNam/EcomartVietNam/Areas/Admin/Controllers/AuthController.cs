using EcomartVietNam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcomartVietNam.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        EcomartStoreDB db = new EcomartStoreDB();

        // GET: Admin/Auth
        public ActionResult Index()
        {
            return View();
        }
        // GET: Admin/Auth/Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(FormCollection frm)
        {
            if (ModelState.IsValid)
            {
                string email = frm["email"];
                string password = frm["password"];
                string currentPass = Helper.EncodePassword(password);
                User user = db.Users.Where(s => s.email.Equals(email) && s.password.Equals(currentPass)).SingleOrDefault();

                if(user != null && user.role != 1)
                {
                    ViewBag.error = "Bạn không có quyền truy cập";
                    return View();
                } 
                else if( user != null && !user.is_active)
                {
                    ViewBag.error = "Tài khoản của bạn đã bị khoá";
                    return View();
                }
                else if (user != null && user.role == 1 && user.is_active)
                {
                    //add session
                    Session["admin"] = user;
                    return Redirect("/Admin/Manage");
                }
                else
                {
                    {
                        ViewBag.error = "Đăng nhập thất bại";
                        return View();
                    }
                }
            }
            ViewBag.error = "Đăng nhập thất bại";
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}