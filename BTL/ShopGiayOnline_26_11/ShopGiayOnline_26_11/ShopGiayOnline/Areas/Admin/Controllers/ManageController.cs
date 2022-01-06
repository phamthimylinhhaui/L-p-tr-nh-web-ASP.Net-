using ShopGiayOnline.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopGiayOnline.Areas.Admin.Controllers
{
    public class ManageController : ProtectAdminController
    {
        ShopGiayOnlineDB db = new ShopGiayOnlineDB();
        // GET: Admin/Manage
        public ActionResult Index()
        {
            ViewBag.UserName = user.full_name;
            var numberUser = db.Users.Count();
            ViewBag.NumberUser = numberUser;
            var numberProduct = db.Products.Count();
            ViewBag.NumberProduct = numberProduct;

            var order = db.Orders.Join(db.Users, o => o.user_id, u => u.user_id, (o, u) => new
            {
                order = o,
                user = u
            }).Select(o => new
            {
                order_id = o.order.order_id,
                user_id = o.user.full_name,
                status = o.order.status,
                created_at = o.order.created_at
            }).Join(db.Order_detail, x => x.order_id, od => od.order_id, (x, od) => new {
                ele = x,
                order_detail = od
            }).GroupBy(x => new {
                order_id = x.ele.order_id,
                user_id = x.ele.user_id,
                status = x.ele.status,
                created_at = x.ele.created_at
            })
            .Select(e => new {
                order_id = e.Key.order_id,
                user_id = e.Key.user_id,
                status = e.Key.status,
                created_at = e.Key.created_at,
                amount = e.Sum(v => v.order_detail.quantity * v.order_detail.price)
            }).ToList();

            var totalAmount = 0;
            foreach (var item in order)
            {
                totalAmount += int.Parse(item.amount.ToString());
            }
            ViewBag.TotalAmount = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", totalAmount) + " vnđ";
            return View();
        }

    }
}