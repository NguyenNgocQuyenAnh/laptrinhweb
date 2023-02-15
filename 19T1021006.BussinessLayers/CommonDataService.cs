using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021006.DomainModels;
using _19T1021006.DataLayers;
using System.Configuration;
namespace _19T1021006.BussinessLayers
{ /// <summary>
/// cung cấp các chức năng nghiệp vụ xử lý dữ liêu chung liên quan đến:
/// quốc gia , nhà cung cấp , khách hàng , người giao hàng, nhân viên , loại hàng
/// </summary>
  public  class CommonDataService
    {
        private static ICountryDAL CountryDB;
        private static ICommonDAL<Supplier> SupplierDB;
        private static ICommonDAL<Employee> EmployeeDB;
        private static ICommonDAL<Categorie> CategorieDB;
        private static ICommonDAL<Shipper> ShipperDB;
        private static ICommonDAL<Customer> CustomerDB;
      
        /// <summary>
        /// Contructer
        /// </summary>
        static CommonDataService()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            CountryDB = new DataLayers.SQLServer.CountryDAL(connectionString);
        }

        #region xuwe lý liên quan đến quốc gia
            public static List<Country> ListOfCountries()
        {
            return CountryDB.List().ToList();
        }
     
        #endregion
    }
}
