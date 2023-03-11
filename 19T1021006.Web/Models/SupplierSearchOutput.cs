using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _19T1021006.DomainModels;
namespace _19T1021006.Web.Models
{ 
    /// <summary>
    /// kết quả tìm kiếm , phân trang đối với nhà cung cấp
    /// </summary>
    public class SupplierSearchOutput : PaginationSearchOutput
    {
        /// <summary>
        /// 
        /// </summary>
        public List<Supplier> Data { get; set; }
    }
}