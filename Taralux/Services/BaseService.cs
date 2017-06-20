using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Taralux.Services
{
    public class BaseService
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["TaraluxDB"].ConnectionString;

        protected async Task ExecuteNonQuery(string storedProcedure, Action<SqlCommand> fillCommand)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand()
                    {
                        CommandType = System.Data.CommandType.StoredProcedure,
                        CommandText = storedProcedure,
                        Connection = connection
                    };
                    fillCommand(command);
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        protected async Task<string> ExecuteScalar(string storedProcedure, Action<SqlCommand> fillCommand)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand()
                    {
                        CommandType = System.Data.CommandType.StoredProcedure,
                        CommandText = storedProcedure,
                        Connection = connection
                    };
                    fillCommand(command);
                    await connection.OpenAsync();
                    return (await command.ExecuteScalarAsync()).ToString();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        protected async Task ExecuteReader(string storedProcedure, Action<SqlCommand> fillCommand, Action<SqlDataReader> fetchData)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand()
                    {
                        CommandType = System.Data.CommandType.StoredProcedure,
                        CommandText = storedProcedure,
                        Connection = connection
                    };
                    fillCommand(command);
                    await connection.OpenAsync();
                    fetchData(await command.ExecuteReaderAsync());
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}