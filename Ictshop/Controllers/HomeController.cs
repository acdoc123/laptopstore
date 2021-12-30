using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ictshop.Dao;
namespace Ictshop.Controllers
{
    public class HomeController : Controller
    {
      
        public ActionResult Index()
        {
            var spdao = new SanPhamDao();
            ViewBag.SaleProducts = spdao.SaleProduct(4);
            return View();

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult SlidePartial()
        {
            return PartialView();

        }
    }
}