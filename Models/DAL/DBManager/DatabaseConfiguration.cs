using System.Configuration;

namespace DAL.DBManager
{
    public class DatabaseConfiguration
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["OracleDbContext"].ConnectionString;

       public static string WarehouseApi = "http://localhost:62672/Api/";

         //public static string WarehouseApi = "http://192.168.2.231/Api/"; //Live Api

    }
}
