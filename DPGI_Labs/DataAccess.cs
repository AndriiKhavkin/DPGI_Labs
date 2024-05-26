using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPGI_Labs
{
    public class DataAccess
    {
        private readonly string _connectionString;

        public DataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<CurrencyInfo> GetDataFromDatabase()
        {
            var currencies = new List<CurrencyInfo>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT CurrencyCode, CurrencyName, Rate FROM ExchangeRates";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            currencies.Add(new CurrencyInfo
                            {
                                CurrencyCode = reader["CurrencyCode"].ToString(),
                                CurrencyName = reader["CurrencyName"].ToString(),
                                Rate = Convert.ToDouble(reader["Rate"])
                            });
                        }
                    }
                }
            }

            return currencies;
        }
    }
}
