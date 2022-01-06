using ShopGiayOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopGiayOnline.Areas.Admin.Controllers
{
    public class ProtectAdminController : Controller
    {
        public User user
        {
            get
            {
                User _user = Session["admin"] as User;
                if (_user == null)
                    _user = new User();
                return _user;
            }
        }

        public class NotAuthorizeAttribute : FilterAttribute { }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            object[] attributes = filterContext.ActionDescriptor.GetCustomAttributes(true);
            if (attributes.Any(a => a is NotAuthorizeAttribute)) return;
            if (Session["admin"] == null)
            {
                filterContext.Result = new RedirectResult("/Admin/Auth/Login");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}