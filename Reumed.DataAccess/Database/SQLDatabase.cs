using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Reumed.DataAccess.Database
{
    internal partial class SQLDatabase : IDatabase, IDatabaseAccess
    {
        private SqlConnectionStringBuilder _sqlinfo = null;

        internal SQLDatabase(SqlConnectionStringBuilder sqlinfo)
        {
            if (sqlinfo == null)
            {
                throw new ArgumentException($"{nameof(sqlinfo)}");
            }

            using (SqlConnection sqlconn = new SqlConnection(sqlinfo.ConnectionString.ToString()))
            {
                sqlconn.Open();

                sqlconn.Close();
            }

            _sqlinfo = sqlinfo;
        }

        public IDbConnection GetConfigurationConnection()
        {
            return new SqlConnection(_sqlinfo.ConnectionString.ToString());
        }
    }
}
