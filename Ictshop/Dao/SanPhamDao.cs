using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ictshop.Models;
namespace Ictshop.Dao
{
    public class SanPhamDao
    {
        DuckShopEntities db = null;
        public SanPhamDao()
        {
            db = new DuckShopEntities();
        }
        public Sanpham ViewDetail(int id)
        {
            return db.Sanphams.Find(id);
        }
        public List<Sanpham> SaleProduct(int top)
        {
            return db.Sanphams.Where(x=>x.Sale>0).OrderByDescending(x => x.Sale).Take(top).ToList();
        }
        public List<string> ListName(string keyword)
        {
            return db.Sanphams.Where(x => x.Tensp.Contains(keyword)).Select(x => x.Tensp).ToList();
        }
        public List<Ictshop.Models.Sanpham> Search(string keyword, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            totalRecord = db.Sanphams.Where(x => x.Tensp == keyword).Count();
            var model = (from a in db.Sanphams
                         join b in db.Hangsanxuats
                         on a.Mahang equals b.Mahang
                         where a.Tensp.Contains(keyword)
                         select new
                         {
                             CatName = b.Tenhang,
                             ID = a.Masp,
                             Images = a.Anhbia,
                             Name = a.Tensp,
                             Price = a.Giatien
                         }).AsEnumerable().Select(x => new Sanpham()
                         {
                             Masp = x.ID,
                             Anhbia = x.Images,
                             Tensp = x.Name,
                             Giatien = x.Price
                         });
            model.OrderByDescending(x => x.Giatien).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }
    }
}