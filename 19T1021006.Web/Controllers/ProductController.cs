using _19T1021006.BussinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19T1021006.DomainModels;

namespace _19T1021006.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [RoutePrefix("product")]
    public class ProductController : Controller
    {
        private const int PAGE_SIZE = 5;
        private const string PRODUCT_SEARCH = "ProductSearchCondition";
       
        /// <summary>
        /// Tìm kiếm, hiển thị mặt hàng dưới dạng phân trang
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            Models.ProductSearchInput condition = Session[PRODUCT_SEARCH] as Models.ProductSearchInput;
            if (condition == null)
            {
                condition = new Models.ProductSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = ""
                };
            }

            return View(condition);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Save(Product data, HttpPostedFileBase uploadPhoto)
        {
            if (uploadPhoto != null)
            {
                string path = Server.MapPath("~/Images/Products");
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                string filePath = System.IO.Path.Combine(path, fileName);
                uploadPhoto.SaveAs(filePath);

                data.Photo = $"Images/Products/{fileName}";

            }

            if (string.IsNullOrWhiteSpace(data.ProductName))
                ModelState.AddModelError(nameof(data.ProductName), "Tên mặt hàng không được để trống");

            if (string.IsNullOrWhiteSpace(data.Unit))
                ModelState.AddModelError(nameof(data.Unit), "Đơn vị không được để trống");

            if (string.IsNullOrEmpty(data.SupplierID.ToString()))
                ModelState.AddModelError(nameof(data.SupplierID), "Vui lòng chọn nhà cung cấp");

            if (string.IsNullOrEmpty(data.CategoryID.ToString()))
                ModelState.AddModelError(nameof(data.CategoryID), "Vui lòng chọn loại hàng");

            if (string.IsNullOrWhiteSpace(data.Price.ToString()))
                ModelState.AddModelError(nameof(data.Price), "Giá bán không được để trống");

            if (data.Price <= 0)
                ModelState.AddModelError(nameof(data.Price), "Giá bán phải lớn hơn 0");

            if (string.IsNullOrWhiteSpace(data.Photo))
                ModelState.AddModelError(nameof(data.Photo), "Ảnh không được để trống");

            if (!ModelState.IsValid)
            {
                if (data.ProductID > 0)
                {
                    ViewBag.Title = "Cập nhật thông tin mặt hàng";
                    return View("Edit", data);
                }
                else
                {
                    ViewBag.Title = "Bổ sung mặt hàng";
                    return View("Create", data);
                }
            }

            if (data.ProductID == 0)
            {
                ProductDataService.AddProduct(data);
            }
            else
            {
                ProductDataService.UpdateProduct(data);
            }

            return RedirectToAction("Index");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ActionResult Search(Models.ProductSearchInput condition)
        {
            int rowCount = 0;
            var data = ProductDataService.ListProducts(condition.Page, condition.PageSize,condition.SearchValue, condition.CategoryID, condition.SupplierID, out rowCount);
            Models.ProductSearchOutput result = new Models.ProductSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data
            };
            Session[PRODUCT_SEARCH] = condition;

            return View(result);
        }
        /// <summary>
        /// Tạo mặt hàng mới
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var data = new Product()
            {
                ProductID = 0
            };
            ViewBag.Title = "Bổ sung mặt hàng";
            return View(data);
        }
        /// <summary>
        /// Cập nhật thông tin mặt hàng, 
        /// Hiển thị danh sách ảnh và thuộc tính của mặt hàng, điều hướng đến các chức năng
        /// quản lý ảnh và thuộc tính của mặt hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        public ActionResult Edit(int id = 0)
        {
            // return content để test dữ liệu
            if (id <= 0)
                return RedirectToAction("Index");

            var data = ProductDataService.GetProduct(id);
            if (data == null)
                return RedirectToAction("Index");

            ViewBag.Title = "Cập nhật mặt hàng";
            return View(data);
        }
        /// <summary>
        /// Xóa mặt hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        public ActionResult Delete(int id = 0)
        {
            if (Request.HttpMethod == "POST")
            {
                ProductDataService.DeleteProduct(id);
                return RedirectToAction("Index");
            }
            var data = ProductDataService.GetProduct(id);
            if (data == null)
                return RedirectToAction("Index");

            return View(data);
        }

        /// <summary>
        /// Các chức năng quản lý ảnh của mặt hàng
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="photoID"></param>
        /// <returns></returns>
        [Route("photo/{method?}/{productID?}/{photoID?}")]
        public ActionResult Photo(string method = "add", int productID = 0, long photoID = 0)
        {
            switch (method)
            {
                case "add":
                    ViewBag.Title = "Bổ sung ảnh";
                    return View();
                case "edit":
                    ViewBag.Title = "Thay đổi ảnh";
                    return View();
                case "delete":
                    //ProductDataService.DeletePhoto(photoID);
                    return RedirectToAction($"Edit/{productID}"); //return RedirectToAction("Edit", new { productID = productID });
                default:
                    return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Các chức năng quản lý thuộc tính của mặt hàng
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        [Route("attribute/{method?}/{productID}/{attributeID?}")]
        public ActionResult Attribute(string method = "add", int productID = 0, long attributeID = 0)
        {
            switch (method)
            {
                case "add":
                    ViewBag.Title = "Bổ sung thuộc tính";
                    return View();
                case "edit":
                    ViewBag.Title = "Thay đổi thuộc tính";
                    return View();
                case "delete":
                    //ProductDataService.DeleteAttribute(attributeID);
                    return RedirectToAction($"Edit/{productID}"); //return RedirectToAction("Edit", new { productID = productID });
                default:
                    return RedirectToAction("Index");
            }
        }
    }
}