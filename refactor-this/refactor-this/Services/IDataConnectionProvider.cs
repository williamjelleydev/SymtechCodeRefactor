using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace refactor_this.Services
{
    public interface IDataConnectionProvider
    {
        //private const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\refactor-this.mdf;Integrated Security=True;Connect Timeout=30";

        //public static SqlConnection NewConnection()
        //{
        //    return new SqlConnection(ConnectionString);
        //}

        // TODO: CreateConnection() is probably a slightly better name
        SqlConnection GetConnection();
    }
}
