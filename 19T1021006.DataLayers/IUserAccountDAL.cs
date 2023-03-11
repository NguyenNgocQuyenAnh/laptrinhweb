using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021006.DomainModels;
namespace _19T1021006.DataLayers
{
    /// <summary>
    /// định nghĩa các phép sử dụng dữ liệu liên quan dến tài khoản của người dùng
    /// </summary>
   public interface IUserAccountDAL
    {
        /// <summary>
        /// kiểm tra xem username và pass có hợp lệ hay không ?
        /// nếu hợp lệ thì trả về thông tin của người dùng và ngược lại  thì trả về nulll
        /// </summary>
        UserAccount Authorize(string userName, string password);
        /// <summary>
        /// đổi mật khẩu người dùng
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        bool ChangePassword(string userName, string oldPassword, string newPassword);
    }
}
