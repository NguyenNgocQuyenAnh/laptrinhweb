using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021006.Web.Models
{
    public class OrderSearchInput : PaginationSearchInput
    {
        /// <summary>
        /// trạng thái đơn hàng
        /// </summary>
        public int Status { get; set; }
    }
}