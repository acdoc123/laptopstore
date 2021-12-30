using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ictshop.Models;
using System.Data.Entity;
using System.Net;

namespace Ictshop.Controllers
{
    public class UserController : Controller
    {
        DuckShopEntities db = new DuckShopEntities();
        // ĐĂNG KÝ
        public ActionResult Dangky()
        {
            return View();
        }

        // ĐĂNG KÝ PHƯƠNG THỨC POST
        [HttpPost]
        public ActionResult Dangky(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
              
                if (db.Nguoidungs.Count(x => x.Email ==  model.Email) > 0 )
                {
                    ModelState.AddModelError("USER_NAME", "Email already exists");
                }
                else
                {
                    var user = new Nguoidung();
                    user.Email = model.Email;
                    user.Matkhau = model.Password;
                    user.Hoten = model.Name;
                    user.Diachi = model.Address;
                    user.Dienthoai = model.Phone;

                    db.Nguoidungs.Add(user);
                    db.SaveChanges();
                    if (user.MaNguoiDung > 0)
                    {
                        ViewBag.Success = "Register Success!";
                        model = new RegisterModel();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Register Fail!");
                    }
                }
            }
            return View(model);
        }

        //public ActionResult Dangky(Nguoidung nguoidung)
        //{
        //    try
        //    {

        //        // Thêm người dùng  mới
        //        db.Nguoidungs.Add(nguoidung);
        //        // Lưu lại vào cơ sở dữ liệu
        //        db.SaveChanges();
        //        // Nếu dữ liệu đúng thì trả về trang đăng nhập
        //        if (ModelState.IsValid)
        //        {
        //            return RedirectToAction("Dangnhap");
        //        }
        //        return View("Dangky");

        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        public ActionResult Dangnhap()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Dangnhap(FormCollection userlog)
        {
            string userMail = userlog["userMail"].ToString();
            string password = userlog["password"].ToString();
            var islogin = db.Nguoidungs.SingleOrDefault(x => x.Email.Equals(userMail) && x.Matkhau.Equals(password));

            if (islogin != null)
                {
     
                    if (userMail == "Admin@gmail.com")
                        {
                           Session["use"] = islogin;
                           return RedirectToAction("Index", "Admin/Home");
                        }
                     else
                         {
                           Session["use"] = islogin;
                           Session["username"] = userMail;
                           return RedirectToAction("Index","Home");
                         }
                 }
            else
                {
                    ViewBag.Fail = "Invalid Email or Password!!!";
                    return View("Dangnhap");
                }

        }
        public ActionResult DangXuat()
        {
            Session["use"] = null;
            Session["GioHang"] = null;
            return RedirectToAction("Index","Home");

        }
        public ActionResult Edit()
        {
            if (Session["use"] == null)
                return RedirectToAction("Index", "Home");
            string mail = Session["username"].ToString();
            Nguoidung id = db.Nguoidungs.Where(n => n.Email == mail).SingleOrDefault(); 
            return View(id);

        }

        [HttpPost]
        public ActionResult Edit(Nguoidung nguoidung)
        {
            if(nguoidung.Hoten == null || nguoidung.Hoten.Length < 5)
            {
                ViewBag.message1 = "Name too short!";
                return View();
            }
            if(nguoidung.Dienthoai == null)
            {
                ViewBag.message2 = "Phone can't be empty!";
                return View();
            }
            int sl = 0;
            int.TryParse(nguoidung.Dienthoai.ToString(), out sl);
            if (sl==0)
            {
                ViewBag.message2 = "Wrong Format! ex: 0123456789";
                return View();
            }
            if (nguoidung.Dienthoai.Length < 10 && nguoidung.Dienthoai.Length > 11)
            {
                ViewBag.message2 = "Phone number must be 10-11 numbers!"+nguoidung.Dienthoai.Length;
                return View();
            }
            if (nguoidung.Diachi == null || nguoidung.Diachi.Length < 5)
            {
                ViewBag.message3 = "Address too short!";
                return View();
            }
            try
            {
                // Sửa sản phẩm theo mã sản phẩm
                var oldItem = db.Nguoidungs.Find(nguoidung.MaNguoiDung);
                oldItem.Hoten = nguoidung.Hoten;
                oldItem.Dienthoai = nguoidung.Dienthoai;
                oldItem.Diachi = nguoidung.Diachi;
                // lưu lại
                db.SaveChanges();
                ViewBag.message123 = "Change your information successfull!!!";
                // xong chuyển qua index
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Changepwd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Changepwd(Nguoidung nguoidung)
        {
            try
            {
                // Sửa sản phẩm theo mã sản phẩm
                var oldItem = db.Nguoidungs.Find(nguoidung.MaNguoiDung);
                if(oldItem.Matkhau == nguoidung.Matkhau)
                {
                    oldItem.Matkhau = nguoidung.mk2;
                }
                // lưu lại
                db.SaveChanges();
                // xong chuyển qua index
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}