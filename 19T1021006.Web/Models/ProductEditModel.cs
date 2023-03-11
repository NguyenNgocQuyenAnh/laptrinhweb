using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _19T1021006.DomainModels;

namespace _19T1021103.Web.Models
{
    public class ProductEditModel: Product
    {
        public List<ProductAttribute> Attributes { get; set; }
        public List<ProductPhoto> Photos { get; set; }

    }
}