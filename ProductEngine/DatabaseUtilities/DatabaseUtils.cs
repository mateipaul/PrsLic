using DatabaseModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseUtilities
{
    public class DatabaseUtils
    {

        private static DatabaseUtils _instance;

        private DatabaseUtils()
        {

        }

        public static DatabaseUtils Instance
        {
            get
            {
                return _instance ?? (_instance = new DatabaseUtils());
            }
        }

        public List<Manufacturer> GetManufacturersFor(string retailerName)
        {
            List<Manufacturer> tempManuList = new List<Manufacturer>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["PrsLicConnectionString"].ConnectionString))
            {
                string cmdText = $"Select Manufacturer.* from manufacturer inner join ManufacturerRetailer on manufacturerRetailer.ManufacturerId = Manufacturer.id inner join retailer on Retailer.id = manufacturerRetailer.retailerID where retailer.name = @retailerName";
                using (SqlCommand cmd = new SqlCommand(cmdText, connection))
                {
                    cmd.Parameters.AddWithValue("@retailerName", retailerName);
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Manufacturer manufacturer = new Manufacturer();
                            manufacturer.Id = Guid.Parse(reader["Id"].ToString());
                            manufacturer.Name = reader["Name"].ToString();
                            manufacturer.Inactive = Boolean.Parse(reader["Inactive"].ToString());
                            tempManuList.Add(manufacturer);
                        }
                    }
                }
            }
            return tempManuList;
        }
    }
}
