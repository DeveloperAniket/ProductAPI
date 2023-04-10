using System.Xml.Linq;

namespace ProductService.ProductAPI.DbContexts
{


    public static class ConnectionsString
    {
        private static string  username = "aniketmaster";
        private static string password = "Aniket123";
        private static string hostname = "productmaster.c4jksrdqtjxw.us-west-2.rds.amazonaws.com";
        private static string port = "5433";
        private static string dbname = "ProductDB";
            
        public static string GetConnection()
        {
            return "Host=" + hostname + ";Port=" + port +  ";Database=" + dbname + ";User ID=" + username + ";Password=" + password + ";";

        }
    }
}
    