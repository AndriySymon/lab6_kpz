using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ClassLibrary2
{
    public class AccountRepository
    {
        private readonly string connectionString = "Server=DESKTOP-PVSCHEE;Database=ATMDatabase;Trusted_Connection=True;";

        public Account GetAccount(string cardNumber, string cardPIN)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Accounts WHERE CardNumber = @cardNumber AND CardPIN = @cardPIN", connection);
                command.Parameters.AddWithValue("@cardNumber", cardNumber);
                command.Parameters.AddWithValue("@cardPIN", cardPIN);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Account(
                            reader["CardNumber"].ToString(),
                            reader["CardPIN"].ToString(),
                            reader["FirstName"].ToString(),
                            reader["LastName"].ToString(),
                            reader["Email"].ToString(),
                            reader["Phone"].ToString(),
                            Convert.ToDouble(reader["Balance"]),
                            Convert.ToDouble(reader["MaxDepositLimit"])
                        );
                    }
                }
            }
            return null;
        }

        public Account GetAccountByCardNumber(string cardNumber)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Accounts WHERE CardNumber = @cardNumber", connection);
                command.Parameters.AddWithValue("@cardNumber", cardNumber);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Account(
                            reader["CardNumber"].ToString(),
                            reader["CardPIN"].ToString(),
                            reader["FirstName"].ToString(),
                            reader["LastName"].ToString(),
                            reader["Email"].ToString(),
                            reader["Phone"].ToString(),
                            Convert.ToDouble(reader["Balance"]),
                            Convert.ToDouble(reader["MaxDepositLimit"])
                        );
                    }
                }
            }
            return null;
        }
    }
}
