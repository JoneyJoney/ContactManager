using ContactManager.Application.RepoInterface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.InfraStructure.RepoImplementation
{
    public class ErrorHandleRepo : IErrorHandleRepo
    {
        private readonly IConfiguration _configuration;
        public ErrorHandleRepo(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void InsertLogIntoDatabaseTable(Exception ex)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string ProcName = "SP_ErrorLogEntries_Insert";
                    using (SqlCommand command = new SqlCommand(ProcName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ErrorMessage", ex.Message);
                        command.Parameters.AddWithValue("@StackTrace", ex.StackTrace.ToString());
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception exsql)
            {
                // Log or handle the error related to the database operation
                
                Console.WriteLine($"Error while logging to the database: {exsql.Message}");
            }
        }
    }
}
