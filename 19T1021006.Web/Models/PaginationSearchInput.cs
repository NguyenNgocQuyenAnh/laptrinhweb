using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021006.Web.Models
{   /// <summary>
    /// lưu trữ thông tin đầu vào dùng để tìm kiếm ,phân trang
    /// </summary>
    public class PaginationSearchInput
    {   /// <summary>
        ///  số dòng trên mỗi trang
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// giá trị tìm kiếm
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// giá trị tìm kiếm
        /// </summary>
        public string SearchValue { get; set; }
     
    }
}