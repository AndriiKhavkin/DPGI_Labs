using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPGI_Labs
{
    internal class DataAccess
    {
        private string connectionString;

        public DataAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public DataTable GetDataFromDatabase()
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Products";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка під час виконання запиту до бази даних: " + ex.Message);
            }

            Console.WriteLine("123", dataTable);
            return dataTable;
        }

        public int ExecuteNonQuery(string query)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка під час виконання NonQuery запиту до бази даних: " + ex.Message);
            }

            return rowsAffected;
        }

        public int InsertRecord(string query)
        {
            return ExecuteNonQuery(query);
        }

        public int UpdateRecord(string query)
        {
            return ExecuteNonQuery(query);
        }

        public int DeleteRecord(string query)
        {
            return ExecuteNonQuery(query);
        }
    }

}
