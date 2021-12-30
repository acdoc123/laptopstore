using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ictshop.Models;
using Ictshop.Dao;

namespace Ictshop.Controllers
{
    public class DanhmucController : Controller
    {
        DuckShopEntities db = new DuckShopEntities();
        // GET: Danhmuc
        public ActionResult DanhmucPartial()
        {
            var danhmuc = db.Hangsanxuats.ToList();
            return PartialView(danhmuc);
        }
        public ActionResult LaptopPartial()
        {
            var danhmuc = db.Sanphams.Take(4).ToList();
            return PartialView(danhmuc);
        }
        public ActionResult Category(int cateId, int page = 1, int pageSize = 1)
        {
            var category = new DanhmucDao().ViewDetail(cateId);
            ViewBag.Category = category;
            int totalRecord = 0;
            var model = new DanhmucDao().ListByCategoryId(cateId, ref totalRecord, page, pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(model);
        }
        public ActionResult LaptopCategory()
        {
            var category = new DanhmucDao().ViewList();
            ViewBag.Category = category;
            var model = db.Sanphams.ToList();
            return View(model);
        }
        public ActionResult SaleCategory()
        {
            var category = new DanhmucDao().ViewList();
            ViewBag.Category = category;
            var model = db.Sanphams.Where(x=>x.Sale>0).ToList();
            return View(model);
        }
    }
}