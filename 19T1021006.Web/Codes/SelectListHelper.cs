using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _19T1021006.DomainModels;
using _19T1021006.BussinessLayers;
using System.Web.Mvc;
namespace _19T1021006.Web
{
    public static class SelectListHelper
    {
        /// <summary>
        /// danh sách quốc gia
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> Countries()
        {
            // mỗi option gọi là selectListItem
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "",
                Text ="---Chọn Quốc Gia ---"
            });
            foreach(var item in CommonDataService.ListOfCountries())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.CountryName,
                    Text = item.CountryName
                });
            }
            return list;
        }
        public static List<SelectListItem> CategoryNames()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "",
                Text = "--Chọn Loại Hàng--"
            });
            foreach (var item in CommonDataService.ListOfCategories())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.CategoryID.ToString(),
                    Text = item.CategoryName
                });
            }
            return list;
        }
        public static List<SelectListItem> SupplierNames()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "",
                Text = "--Chọn Nhà Cung Cấp--"
            });
            foreach (var item in CommonDataService.ListOfSuppliers())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.SupplierID.ToString(),
                    Text = item.SupplierName
                });
            }
            return list;
        }
      
    }
}