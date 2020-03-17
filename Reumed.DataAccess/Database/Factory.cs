using Reumed.DataAccess.Service;
using System.Data.SqlClient;

namespace Reumed.DataAccess.Database
{
    public static class Factory
    {
        public static IService GetService()
        {
            return new ServiceManager();
        }

        public static IDatabase GetDatabase()
        {
            return GetSQLDatabase();
        }

        internal static SQLDatabase GetSQLDatabase()
        {
            SqlConnectionStringBuilder sConnection = new SqlConnectionStringBuilder()
            {
                DataSource = "192.185.6.239",
                InitialCatalog = "jrozon05_reumapp",
                UserID = "jrozon05_reumapp",
                Password = "Santiago05@"
            };

            return new SQLDatabase(sConnection);
        }
    }
}
