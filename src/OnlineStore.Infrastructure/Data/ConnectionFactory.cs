using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace OnlineStore.Infrastructure.Data;
public class ConnectionStringFactory : IConnectionFactory
{
  public string? ConnectionString { get; set; }

  public ConnectionStringFactory(string? connectionString)
  {
    ConnectionString = connectionString;
  }

  public async Task<SqlConnection> CreateSqlConnection()
  {
    SqlConnection sqlconn = new SqlConnection(ConnectionString);
    await sqlconn.OpenAsync();
    return new SqlConnection(ConnectionString);
  }
}
