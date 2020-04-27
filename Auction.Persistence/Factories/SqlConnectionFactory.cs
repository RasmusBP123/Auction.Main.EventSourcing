using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Auction.Persistence.Factories
{
    public class SqlConnectionFactory : ISQLConnectionFactory
    {
        private readonly string connectionString;

        public SqlConnectionFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public SqlConnection SqlConnection()
        {
            return new SqlConnection(this.connectionString);
        }
    }
}
