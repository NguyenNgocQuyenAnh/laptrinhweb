using _19T1021006.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace _19T1021006.DataLayers.SQLServer
{
    public class CountryDAL : _BaseDAL,ICountryDAL
    {/// <summary>
    /// 
    /// </summary>
    /// <param name="connectionString"></param>
        public CountryDAL(string connectionString) : base(connectionString)
        {
        }

        IList<Country> ICountryDAL.List()
        {
            List<Country> data = new List<Country>();
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "select * from Countries";
                cmd.CommandType = CommandType.Text;
                SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dbReader.Read())
                {
                    data.Add(new Country()
                    {
                        CountryName = Convert.ToString(dbReader["CountryName"])
                    }) ;
                }
                dbReader.Close();
                cn.Close();
            }
            return data;
        }
    }
}
