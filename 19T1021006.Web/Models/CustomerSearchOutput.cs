using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _19T1021006.DomainModels;
namespace _19T1021006.Web.Models
{
    public class CustomerSearchOutput : PaginationSearchOutput
    {
        public List<Customer> Data { get; set; }
    }
}