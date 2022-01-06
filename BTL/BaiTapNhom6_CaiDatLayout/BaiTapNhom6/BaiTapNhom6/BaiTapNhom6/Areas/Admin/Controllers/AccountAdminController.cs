using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaiTapNhom6.Areas.Admin.Controllers
{
    public class AccountAdminController : Controller
    {
        // GET: Admin/AccountAdmin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddAccount()
        {
            return View();
        }
        public ActionResult EditAccount()
        {
            return View();
        }
        public ActionResult DeleteAccount()
        {
            return View();
        }
    }

}