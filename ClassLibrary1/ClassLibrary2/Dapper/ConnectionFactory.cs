using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALEF.Dapper
{
    public static class ConnectionFactory
    {
        public static IDbConnection Create(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}
