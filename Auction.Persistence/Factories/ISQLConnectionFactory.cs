using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Auction.Persistence.Factories
{
    public interface ISQLConnectionFactory
    {
        SqlConnection SqlConnection();
    }
}
