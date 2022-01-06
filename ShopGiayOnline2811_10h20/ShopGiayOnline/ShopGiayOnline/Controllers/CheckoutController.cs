using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopGiayOnline.Models;

namespace ShopGiayOnline.Controllers
{
    public class CheckoutController : Controller
    {
         ShopGiayOnlineDB db = new ShopGiayOnlineDB();

        // GET: Checkout
        public ActionResult Index()
        {
            var categories = db.Categories.ToList();
            ViewBag.Categories = categories;
            return View();
        }

    }
}
