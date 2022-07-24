using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerService
{
    public static class SqlHelper
    {

        public static string connectionString = null;
        public static async Task<IEnumerable<CustomerTransactionDTO>> GetTransactionForAllCustomers(int numberOfMonths)
        {

            List<CustomerTransactionDTO> data = new List<CustomerTransactionDTO>();
            using (SqlConnection con = new SqlConnection(SqlHelper.connectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@numberofmonths", numberOfMonths);
                cmd.CommandText = "sp_get_alltransactions";
                con.Open();
                var reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    var d = new CustomerTransactionDTO();

                    d.CustomerName = reader.GetString("Name");
                    d.CustomerMobileNumber = reader.GetString("Mobile Number");
                    d.Amount = reader.GetDecimal("total");
                    d.CustomerId = reader.GetInt32("CustomerId");
                    data.Add(d);
                }
            }
            return data;
        }
        public static async Task<CustomerTransactionDTO> GetTransactionForSingleCustomers(string mobileNumber, int numberOfMonths)
        {
            var data = new CustomerTransactionDTO();

                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@mobilenumber", mobileNumber.ToString());
                    cmd.Parameters.AddWithValue("@numberofmonths", numberOfMonths);
                    cmd.CommandText = "sp_get_cutomer_transaction";
                    con.Open();
                    var reader = await cmd.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        data.CustomerName = reader.GetString("Name");
                        data.CustomerMobileNumber = reader.GetString("Mobile Number");
                        data.Amount = reader.GetDecimal("total");
                        data.CustomerId = reader.GetInt32("CustomerId");
                    }
                }
                return data;
            }
        }
}
