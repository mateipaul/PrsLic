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

        public List<Manufacturer> GetManufacturersFor(Retailer retailerName)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["PrsLicConnectionString"].ConnectionString))
            {
                string cmdText = $"Select Manufacturer.* from manufacturer inner join ManufacturerRetailer on manufacturerRetailer.ManufacturerId = Manufacturer.id inner join retailer on Retailer.id = manufacturerRetailer.retailerID where retailer.name = {retailerName.Name}";
                using (SqlCommand cmd = new SqlCommand(cmdText, connection))
                {

                }
            }
        }
    }
}
