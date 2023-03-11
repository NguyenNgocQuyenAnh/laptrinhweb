using _19T1021006.BussinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace _19T1021006.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        // GET: Account
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Trang đăng nhập vào hệ thống
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous] 
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// xử lý về đăng nhập
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [AllowAnonymous] // không đăng nhập vẫn xài đc
        [HttpPost]
        public ActionResult Login(string userName ="", string password = "")
        {
            ViewBag.UserName = userName;
            if(string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError("", "Thông tin không đầy đủ");
                return View();
            }

            var userAccount = UserAccountService.Authorize(AccountTypes.Employee, userName, password);
            if(userAccount == null)
            {
                ModelState.AddModelError("", "Đăng nhập thất bại");
                return View();
            }

            // ghi cookie cho phiên đăng nhập
            string cookieString = Newtonsoft.Json.JsonConvert.SerializeObject(userAccount);
            FormsAuthentication.SetAuthCookie(cookieString, false);
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        /// <summary>
        /// nhay qua trang moi
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            return View();
        }
        /// <summary>
        /// thay doi password
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <param name="newPasswordAgain"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        
        public ActionResult ChangePassword(string UserName, string oldPassword, string newPassword, string newPasswordAgain)
        {
            if (newPassword != newPasswordAgain)
            {
                ModelState.AddModelError("", "Không trùng với mật khẩu mới");
                return View("Edit");
            }
            bool changePass = UserAccountService.ChangePassword(AccountTypes.Employee, UserName, oldPassword, newPassword);

            if (changePass == false)
            {
                ModelState.AddModelError("", "Sai mật khẩu cũ");
                return View("Edit");
            }

            return RedirectToAction("Edit");
        }
    }
}