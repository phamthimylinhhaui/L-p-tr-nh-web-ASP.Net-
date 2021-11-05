using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaiTapNhom6.Areas.Admin.Controllers
{
    public class OrderAdminController : Controller
    {
        // GET: Admin/OrderAdmin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult OrderDetailAdmin()
        {
            return View();
        }
    }
}