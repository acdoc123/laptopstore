using Ictshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ictshop.Dao
{
    public class ProductViewModel
    {
        DuckShopEntities db = new DuckShopEntities();
        public int ID { set; get; }
        public string Images { set; get; }
        public string Name { set; get; }
        public decimal? Price { set; get; }
        public int Sale { set; get; }
        public string CateName { set; get; }
    }
    
}