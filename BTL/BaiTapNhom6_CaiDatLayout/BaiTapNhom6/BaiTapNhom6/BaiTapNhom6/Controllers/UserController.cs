using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaiTapNhom6.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        //login user
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult register()
        {
            return View();
        }
        //acount
        public ActionResult account()
        {
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }
    }
}