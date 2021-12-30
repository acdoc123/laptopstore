using Ictshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ictshop.Dao
{
    public class DanhmucDao
    {
        DuckShopEntities db = null;
        public DanhmucDao()
        {
            db = new DuckShopEntities();
        }
        public Hangsanxuat ViewDetail(int id)
        {
            return db.Hangsanxuats.Find(id);
        }
        public List<Hangsanxuat> ViewList()
        {
            return db.Hangsanxuats.ToList();
        }
        public List<Ictshop.Models.Sanpham> ListByCategoryId(int categoryID, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            totalRecord = db.Sanphams.Where(x => x.Mahang == categoryID).Count();
            var model = (from a in db.Sanphams
                         join b in db.Hangsanxuats
                         on a.Mahang equals b.Mahang
                         where a.Mahang == categoryID
                         select new
                         {
                             CateName = b.Tenhang,
                             ID = a.Masp,
                             Images = a.Anhbia,
                             Name = a.Tensp,
                             Sale = a.Sale,
                             Price = a.Giatien
                         }).AsEnumerable().Select(x => new Sanpham()
                         {
                             Masp = x.ID,
                             Anhbia = x.Images,
                             Tensp = x.Name,
                             Sale = (int)x.Sale,
                             Giatien = x.Price
                         });
            model.OrderByDescending(x => x.Sale).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }
    }
}