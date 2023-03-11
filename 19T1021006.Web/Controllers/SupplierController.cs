using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19T1021006.BussinessLayers;
using _19T1021006.DomainModels;
namespace _19T1021006.Web.Controllers
{
    [Authorize]
    public class SupplierController : Controller
    {
        private const int PAGE_SIZE = 5;
        private const string SUPPLIER_SEARCH = "SupplierSearchCondition";
        // GET: Supplier
        /// <summary>
        /// phan trang
        /// </summary>
        /// <returns></returns>
        //public ActionResult Index(int page = 1 , string searchValue="")
        //{
        //    int rowCount = 0;
        //    var data = CommonDataService.ListOfSuppliers(page, PAGE_SIZE,searchValue,out rowCount);
        //    int pageCount = rowCount / PAGE_SIZE;
        //    if(rowCount % PAGE_SIZE > 0)
        //    {
        //        pageCount += 1;
        //    }
        //    ViewBag.Page = page;
        //    ViewBag.RowCount = rowCount;
        //    ViewBag.PageCount = pageCount;
        //    ViewBag.SearchValue = searchValue;

        //    return View(data);// truyền dữ liệu bằng cách dùng model
        //}
        /// <summary>
        /// Models.PaginationSearchInput condition tương tự (int page , int pagesize, string searchvalue)
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ActionResult Index()
        {
            Models.PaginationSearchInput condition = Session[SUPPLIER_SEARCH] as Models.PaginationSearchInput;
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
            var data = CommonDataService.ListOfSuppliers(condition.Page,
                                                         condition.PageSize,
                                                         condition.SearchValue,
                                                         out rowCount);
            Models.SupplierSearchOutput result = new Models.SupplierSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data
            };
            Session[SUPPLIER_SEARCH] = condition;

            return View(result);
        }
      
        /// <summary>
        /// tạo 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var data = new Supplier()
            {
                SupplierID = 0
            };
            ViewBag.Title = "Bổ sung nhà cung cấp";
            return View("Edit", data);
        }
        /// <summary>
        /// sửa 
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id = 0) //int? cho phép nhận giá trị null
        {
            // return content để test dữ liệu
            if(id <= 0)
                return RedirectToAction("Index");

            var data = CommonDataService.GetSupplier(id);
            if (data == null)
                return RedirectToAction("Index");

            ViewBag.Title = "Cập nhật nhà cung cấp";
            return View(data);
        }
        [ValidateAntiForgeryToken] // xác định nguồn dữ liệu (fake bị chặn)
        [HttpPost]
        public ActionResult Save(Supplier data)
        {
            // kiểm soát dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(data.SupplierName))
                ModelState.AddModelError(nameof(data.SupplierName), "Tên không được để trống");
            if (string.IsNullOrWhiteSpace(data.ContactName))
                ModelState.AddModelError(nameof(data.ContactName), "Tên giao dịch không được để trống");
            if (string.IsNullOrWhiteSpace(data.Country))
                ModelState.AddModelError(nameof(data.Country), "vui lòng chọn quốc gia");

            data.Address = data.Address ?? "";
            data.Phone = data.Phone ?? "";
            data.City = data.City ?? "";
            data.PostalCode = data.PostalCode ?? "";


            if(!ModelState.IsValid)
            {
                ViewBag.Title = data.SupplierID == 0 ? "Bổ sung nhà cung cấp " : "Cập nhật nhà cung cấp";
                return View("Edit",data);
            }

            if (data.SupplierID == 0)
            {
                CommonDataService.AddSupplier(data);
            }
            else
            {
                CommonDataService.UpdateSupplier(data);
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// xóa
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int id = 0)
        {
            if (id <= 0)
                return RedirectToAction("Index");

            if(Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteSupplier(id);
                return RedirectToAction("Index");
            }
            var data = CommonDataService.GetSupplier(id);

            if (data == null)
                return RedirectToAction("Index");

            return View(data);
        }
    }
}