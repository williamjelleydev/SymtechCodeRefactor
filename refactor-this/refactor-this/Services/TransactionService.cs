using refactor_this.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace refactor_this.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IDataConnectionProvider _connectionProvider;

        public TransactionService(IDataConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public bool TryAddTransactionToAccount(Guid accountId, Transaction transaction, out string failureMessage)
        {
            failureMessage = null;
            using (var connection = _connectionProvider.GetConnection())
            {

                // TODO: These sql commands need to be in the same "database transaction". It could put the database in an unexpected
                // state if one fails and the other doesn't.
                SqlCommand command = new SqlCommand($"update Accounts set Amount = Amount + {transaction.Amount} where Id = '{accountId}'", connection);
                connection.Open();
                // Hmm, this logic in here means there are several thing we need to return...
                if (command.ExecuteNonQuery() != 1)
                {
                    //return BadRequest("Could not update account amount");
                    failureMessage = "Could not update account amount";
                    return false;
                }

                command = new SqlCommand($"INSERT INTO Transactions (Id, Amount, Date, AccountId) VALUES ('{Guid.NewGuid()}', {transaction.Amount}, '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', '{accountId}')", connection);
                if (command.ExecuteNonQuery() != 1)
                {
                    //return BadRequest("Could not insert the transaction");
                    failureMessage = "Could not insert the transaction";
                    return false;
                }


                return true;
            }
        }

        public IEnumerable<Transaction> GetTransactionsForAccount(Guid accountId)
        {
            using (var connection = _connectionProvider.GetConnection())
            {
                SqlCommand command = new SqlCommand($"select Amount, Date from Transactions where AccountId = '{accountId}'", connection);
                connection.Open();
                var reader = command.ExecuteReader();
                var transactions = new List<Transaction>();
                while (reader.Read())
                {
                    var amount = (float)reader.GetDouble(0);
                    var date = reader.GetDateTime(1);
                    transactions.Add(new Transaction(amount, date));
                }
                return transactions;
            }
        }
    }
}