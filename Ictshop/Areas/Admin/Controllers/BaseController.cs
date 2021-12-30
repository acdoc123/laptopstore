using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Ictshop.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        //initilizing culture on controller initialization
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = Session["use"];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new
                {
                    controller = "User",
                    action = "Dangnhap", Area = "Ictshop"
                }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}