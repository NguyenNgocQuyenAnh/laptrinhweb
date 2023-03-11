using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021006.DomainModels
{
    /// <summary>
    /// thông tin tài khoản của người dùng
    /// </summary>
   public class UserAccount
    {
         // authetication : kiểm tra người dùng có đăng nhập vào hệ thống hay không
         // authorrization : kiểm tra người dùng có được phép sử dụng chức năng nào đó trong hệ thống hay không
        public string UserID { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Password { get ; set; }

        public string RoleNames { get; set; }

        public string Photo{ get; set; }
    }
}
