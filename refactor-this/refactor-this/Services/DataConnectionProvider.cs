using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace refactor_this.Services
{
    public class DataConnectionProvider : IDataConnectionProvider
    {
        private readonly string _connectionString;

        public DataConnectionProvider(string connectionString)
        {
            // TODO: probably some validation of the connection string should be done at some level
            _connectionString = connectionString;
        }

        public SqlConnection GetConnection()
        {
            // Right, this is failing because it expects to find some randimg refactor-this.mdf file....
            //private const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\refactor-this.mdf;Integrated Security=True;Connect Timeout=30";
            return new SqlConnection(_connectionString);
        }
    }
}