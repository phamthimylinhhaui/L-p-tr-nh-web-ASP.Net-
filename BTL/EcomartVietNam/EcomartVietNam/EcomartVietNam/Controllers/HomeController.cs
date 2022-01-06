using EcomartVietNam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace EcomartVietNam.Controllers
{
    public class HomeController : Controller
    {
        EcomartStoreDB db = new EcomartStoreDB();
        public ActionResult Index(int? page)
        {
            var products = db.Products.OrderByDescending(p => p.product_id);

            int pageNumber = (page ?? 1);


            return View(products.ToPagedList(pageNumber, 8));
        }

    }
}