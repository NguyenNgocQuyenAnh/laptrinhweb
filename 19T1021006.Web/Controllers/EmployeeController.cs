using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19T1021006.BussinessLayers;
using _19T1021006.DomainModels;
using System.Globalization;

namespace _19T1021006.Web.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private const int PAGE_SIZE = 5;
        private const string EMPLOYEE_SEARCH = "EmployeeSearchCondition";
        // GET: Employee
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //public ActionResult Index(int page = 1, string searchValue = "")
        //{
        //    int rowCount = 0;
        //    var data = CommonDataService.ListOfEmployees(page, PAGE_SIZE, searchValue, out rowCount);
        //    int pageCount = rowCount / PAGE_SIZE;
        //    if (rowCount % PAGE_SIZE > 0)
        //    {
        //        pageCount += 1;
        //    }
        //    ViewBag.Page = page;
        //    ViewBag.RowCount = rowCount;
        //    ViewBag.PageCount = pageCount;
        //    ViewBag.SearchValue = searchValue;

        //    return View(data);// truyền dữ liệu bằng cách dùng model
        //}
        public ActionResult Index()
        {
            Models.PaginationSearchInput condition = Session[EMPLOYEE_SEARCH] as Models.PaginationSearchInput;
            if (condition == null)
            {
                condition = new Models.PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = ""
                };
            }

            return View(condition);
        }
        public ActionResult Search(Models.PaginationSearchInput condition)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfEmployees(condition.Page,
                                                         condition.PageSize,
                                                         condition.SearchValue,
                                                         out rowCount);
            Models.EmployeeSearchOutput result = new Models.EmployeeSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data
            };
            Session[EMPLOYEE_SEARCH] = condition;

            return View(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var data = new Employee()
            {
               EmployeeID = 0
            };
            ViewBag.Title = "Bổ sung nhân viên";
            return View("Edit", data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id =0)
        {
            // return content để test dữ liệu
            if (id <= 0)
                return RedirectToAction("Index");

            var data = CommonDataService.GetEmployee(id);
            if (data == null)
                return RedirectToAction("Index");

            ViewBag.Title = "Cập nhật nhà cung cấp";
            return View(data);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Save(Employee data, string birthday, HttpPostedFileBase uploadPhoto)
        {
            DateTime? d = Converter.DMYStringToDateTime(birthday);
            if (d == null)
                ModelState.AddModelError("BirthDate", "ngày sinh không được để trống");
            else
                data.BirthDate = d.Value;
            //Kiểm soát dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(data.LastName))
                ModelState.AddModelError(nameof(data.LastName), "Họ nhân viên không được để trống");
            if (string.IsNullOrWhiteSpace(data.FirstName))
                ModelState.AddModelError(nameof(data.FirstName), "Tên nhân viên không được để trống");
            if (string.IsNullOrWhiteSpace(data.BirthDate.ToString()))
                ModelState.AddModelError(nameof(data.BirthDate), "ngày sinh không được để trống");
            
            data.Notes = data.Notes ?? "";
            data.Photo = data.Photo ?? "Images/Employees/avatardefault.png";
            data.Email = data.Email ?? "";

            if (ModelState.IsValid == false)
            {
                ViewBag.Title = data.EmployeeID == 0 ? "Bổ sung nhân viên" : "Cập nhật nhân viên";
                return View("Edit", data);
            }
            if (uploadPhoto != null)
            {
                string path = Server.MapPath("~/Images/Employees");
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                string filePath = System.IO.Path.Combine(path, fileName);
                uploadPhoto.SaveAs(filePath);
                data.Photo = $"Images/Employees/{fileName}";
            }
            if (data.EmployeeID == 0)
            {
                CommonDataService.AddEmployee(data);
            }
            else
            {
                CommonDataService.UpdateEmployee(data);
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int id =0)
        {
            if (id <= 0)
                return RedirectToAction("Index");

            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteEmployee(id);
                return RedirectToAction("Index");
            }
            var data = CommonDataService.GetEmployee(id);

            if (data == null)
                return RedirectToAction("Index");

            return View(data);
        }
    }
}