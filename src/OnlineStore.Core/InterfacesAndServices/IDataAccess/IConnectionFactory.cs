﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace OnlineStore.Infrastructure.Data;
public interface IConnectionFactory
{
  public string? ConnectionString { get; set; }
  public Task<SqlConnection> CreateSqlConnection();
}
