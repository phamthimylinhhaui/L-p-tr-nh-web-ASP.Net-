using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopGiayOnline.Models
{
    public class ListHomeModel
    {
        public List<Category> listCategory { get; set; }
        public List<Product> listProduct { get; set; }
    }
}