using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ictshop.Models;

namespace Ictshop.Controllers
{
    public class DonhangsController : Controller
    {
        private DuckShopEntities db = new DuckShopEntities();

        // GET: Donhangs
        public ActionResult Index()
        {
            if (Session["use"]==null)
                return RedirectToAction("Index", "Home");
            string mail = Session["username"].ToString();
            Nguoidung id = db.Nguoidungs.Where(n => n.Email == mail).SingleOrDefault();
            var donhangs = db.Donhangs.Where(x => x.MaNguoidung == id.MaNguoiDung);
            ViewBag.Name = id.Hoten;
            return View(donhangs.ToList().OrderByDescending(d => d.Madon));
        }

        // GET: Donhangs/Details/5
        

        // GET: Donhangs/Create
        public ActionResult Create()
        {
            ViewBag.MaNguoidung = new SelectList(db.Nguoidungs, "MaNguoiDung", "Hoten");
            return View();
        }

        // POST: Donhangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Madon,Ngaydat,Tinhtrang,MaNguoidung,YeuCau")] Donhang donhang)
        {
            if (ModelState.IsValid)
            {
                db.Donhangs.Add(donhang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaNguoidung = new SelectList(db.Nguoidungs, "MaNguoiDung", "Hoten", donhang.MaNguoidung);
            return View(donhang);
        }

        // GET: Donhangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donhang donhang = db.Donhangs.Find(id);
            if (donhang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNguoidung = new SelectList(db.Nguoidungs, "MaNguoiDung", "Hoten", donhang.MaNguoidung);
            return View(donhang);
        }

        // POST: Donhangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Madon,Ngaydat,Tinhtrang,MaNguoidung,YeuCau")] Donhang donhang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donhang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNguoidung = new SelectList(db.Nguoidungs, "MaNguoiDung", "Hoten", donhang.MaNguoidung);
            return View(donhang);
        }

        // GET: Donhangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donhang donhang = db.Donhangs.Find(id);
            if (donhang == null)
            {
                return HttpNotFound();
            }
            return View(donhang);
        }

        // POST: Donhangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Donhang donhang = db.Donhangs.Find(id);
      
            db.Donhangs.Remove(donhang);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Details(int id)
        {
            var model = db.Chitietdonhangs.Where(x => x.Madon == id).ToList();
            return View(model);
        }
        public ActionResult Cancle(int id)
        {
            Donhang donhang = db.Donhangs.Find(id);
            donhang.YeuCau = "Cancle this order!";
            db.SaveChanges();
            ViewBag.cancel = "Your order will be cancel soon!!!";
            return RedirectToAction("Index");
        }
    }
}
