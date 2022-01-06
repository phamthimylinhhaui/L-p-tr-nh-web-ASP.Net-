using PagedList;
using ShopGiayOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopGiayOnline.Controllers
{
    public class HomeController : Controller
    {
        ShopGiayOnlineDB db = new ShopGiayOnlineDB();
        public ActionResult Index(int? page)
        {
            var products = db.Products.OrderByDescending(p => p.product_id);

            int pageNumber = (page ?? 1);


            return View(products.ToPagedList(pageNumber, 8));
        }

    }
}