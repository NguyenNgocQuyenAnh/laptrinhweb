using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _19T1021006.DomainModels;
namespace _19T1021006.Web.Models
{
    public class ShipperSearchOutput : PaginationSearchOutput
    {
        public List<Shipper> Data { get; set; }
    }
}