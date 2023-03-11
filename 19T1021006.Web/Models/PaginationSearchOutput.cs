using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021006.Web.Models
{
    /// <summary>
    /// lớp cơ sở dùng để biểu diễn kết quả tìm kiếm dưới dạng phân trang
    /// </summary>
    public class PaginationSearchOutput
    {
        /// <summary>
        /// trang được hiển thị
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// số dòng trên mỗi trang
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// giá trị tìm kiếm
        /// </summary>
        public string SearchValue { get; set; }
        /// <summary>
        /// số dòng dữ liệu tìm được
        /// </summary>
        
        public int RowCount {get; set;}
        
        /// <summary>
        /// số trang
        /// </summary>
        public int PageCount 
        {
            get
            {
                if (PageSize == 0)
                    return 1;

                int p = RowCount / PageSize;
                if (RowCount % PageSize > 0)
                    p += 1;
                return p;
            } 
        }
    }
}