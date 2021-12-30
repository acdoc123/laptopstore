using Ictshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ictshop.Dao
{
    public class DonHangDao
    {
        DuckShopEntities db = null;
        public DonHangDao()
        {
            db = new DuckShopEntities();
        }
        public Chitietdonhang ViewDetails(int id)
        {
            return db.Chitietdonhangs.Find(id);
        }

    }
}