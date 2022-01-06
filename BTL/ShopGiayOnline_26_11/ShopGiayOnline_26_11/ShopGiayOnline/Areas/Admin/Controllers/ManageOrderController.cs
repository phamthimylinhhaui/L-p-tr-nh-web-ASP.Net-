using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopGiayOnline.Models;

namespace ShopGiayOnline.Areas.Admin.Controllers
{
    public class ManageOrderController : ProtectAdminController
    {
        ShopGiayOnlineDB db = new ShopGiayOnlineDB();
        static List<Custom_order> list;

        // GET: Admin/ManageOrder
        public ActionResult Index()
        {
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
            }).Join(db.Order_detail, x => x.order_id, od => od.order_id, (x, od) => new
            {
                ele = x,
                order_detail = od
            }).GroupBy(x => new
            {
                order_id = x.ele.order_id,
                user_id = x.ele.user_id,
                status = x.ele.status,
                created_at = x.ele.created_at
            })
            .Select(e => new
            {
                order_id = e.Key.order_id,
                user_id = e.Key.user_id,
                status = e.Key.status,
                created_at = e.Key.created_at,
                amount = e.Sum(v => v.order_detail.quantity * v.order_detail.price)
            }).OrderByDescending(o => o.order_id).ToList();

            List<string> status = new List<string>
            {
                "Đã giao",  "Đang giao",  "Đã huỷ"
            };
            ViewBag.Status = status;
            list = new List<Custom_order>();
            foreach (var item in order)
            {
                Custom_order c = new Custom_order();
                c.order_id = item.order_id;
                c.user_id = item.user_id;
                if (item.status == 1)
                {
                    c.status = "Đang giao";
                }
                else if (item.status == 2)
                {
                    c.status = "Đã giao";
                }
                else if (item.status == 3)
                {
                    c.status = "Đã huỷ";
                }
                c.amount = decimal.Parse(item.amount.ToString());
                c.created_at = DateTime.Parse(item.created_at.ToString());

                list.Add(c);
            }
            return View(list);
        }
        // GET: Admin/ManageOrder/id
        //public ActionResult Details()
        //{
        //    return View();
        //}

        public ActionResult Details(int id)
        {
            Order order = db.Orders.Find(id);
            User user = db.Users.Find(order.user_id);
            List<Order_detail> productsInOrder = db.Order_detail.Where(p => p.order_id == id).ToList();
            List<Product> listProduct = new List<Product>();
            foreach (var item in productsInOrder)
            {
                Product pr = db.Products.Where(p => p.product_id == item.product_id).SingleOrDefault();
                listProduct.Add(pr);
            }
            ViewBag.User = user;
            ViewBag.Order = order;
            if (order.status == 1)
            {
                ViewBag.Status = "Đang giao";
            }
            else if (order.status == 2)
            {
                ViewBag.Status = "Đã giao";
            }
            else if (order.status == 3)
            {
                ViewBag.Status = "Đã huỷ";
            }
            ViewBag.Products = productsInOrder;
            ViewBag.ProductInfo = listProduct;
            foreach (var item in list)
            {
                if (item.order_id == id)
                {
                    ViewBag.Total = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", item.amount) + " vnđ";
                }
            }

            return View();
        }

        [HttpPost]
        public JsonResult Edit(int? id, FormCollection frm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int status = int.Parse(frm["status"]);

                    Order order = db.Orders.Find(id);
                    if (order == null)
                    {
                        return Json("NOT_FOUND", JsonRequestBehavior.AllowGet);
                    }

                    order.status = status;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Entry(order).State = EntityState.Modified;
                    db.SaveChanges();

                    return Json("SUCCESS", JsonRequestBehavior.AllowGet);
                }
                return Json("NOT_FOUND", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json("NOT_FOUND", JsonRequestBehavior.AllowGet);
            }
        }
    }
}