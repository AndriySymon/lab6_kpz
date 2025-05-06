using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ClassLibrary2.Interfaces;

namespace ClassLibrary2
{
    public class AccountRepository : IDeletableAccount, IUpdatableAccount, ICreatableAccount, IReadableAccount
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
                            Convert.ToDouble(reader["Balance"])
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
                        var account = new Account(
                            reader["CardNumber"].ToString(),
                            reader["CardPIN"].ToString(),
                            reader["FirstName"].ToString(),
                            reader["LastName"].ToString(),
                            reader["Email"].ToString(),
                            reader["Phone"].ToString(),
                            Convert.ToDouble(reader["Balance"])
                        );

                        account.FailedLoginAttempts = reader["FailedLoginAttempts"] != DBNull.Value
                        ? Convert.ToInt32(reader["FailedLoginAttempts"])
                        : 0;

                        account.IsLocked = reader["IsLocked"] != DBNull.Value
                        ? Convert.ToBoolean(reader["IsLocked"])
                        : false;
                        account.LockTime = reader["LockTime"] != DBNull.Value
                            ? Convert.ToDateTime(reader["LockTime"])
                            : (DateTime?)null;

                        return account;
                    }
                }
            }
            return null;
        }
        public bool AddAccount(Account account)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand(
                    "INSERT INTO Accounts (CardNumber, CardPIN, FirstName, LastName, Email, Phone, Balance) " +
                    "VALUES (@CardNumber, @CardPIN, @FirstName, @LastName, @Email, @Phone, @Balance)", connection);

                command.Parameters.AddWithValue("@CardNumber", account.CardNumber);
                command.Parameters.AddWithValue("@CardPIN", account.CardPIN);
                command.Parameters.AddWithValue("@FirstName", account.FirstName);
                command.Parameters.AddWithValue("@LastName", account.LastName);
                command.Parameters.AddWithValue("@Email", account.Email);
                command.Parameters.AddWithValue("@Phone", account.Phone);
                command.Parameters.AddWithValue("@Balance", account.Balance);

                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateAccount(Account acc)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand(@"
            UPDATE Accounts SET
                FirstName = @fn,
                LastName = @ln,
                Phone = @ph,
                Balance = @bal,
                FailedLoginAttempts = @attempts,
                IsLocked = @locked,
                LockTime = @lockTime
            WHERE CardNumber = @cn", connection);

                command.Parameters.AddWithValue("@fn", acc.FirstName);
                command.Parameters.AddWithValue("@ln", acc.LastName);
                command.Parameters.AddWithValue("@ph", acc.Phone);
                command.Parameters.AddWithValue("@bal", acc.Balance);
                command.Parameters.AddWithValue("@attempts", acc.FailedLoginAttempts);
                command.Parameters.AddWithValue("@locked", acc.IsLocked);
                command.Parameters.AddWithValue("@lockTime", acc.LockTime.HasValue ? (object)acc.LockTime.Value : DBNull.Value);
                command.Parameters.AddWithValue("@cn", acc.CardNumber);

                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteAccount(string cardNumber)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM Accounts WHERE CardNumber = @cn", connection);
                command.Parameters.AddWithValue("@cn", cardNumber);
                return command.ExecuteNonQuery() > 0;
            }
        }

    }
}
