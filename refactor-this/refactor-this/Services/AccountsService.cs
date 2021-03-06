using refactor_this.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace refactor_this.Services
{
    // TODO: naming is weird with plural. AccountService and IAccountService would be nicer
    public class AccountsService : IAccountsService
    {
        private readonly IDataConnectionProvider _connectionProvider;

        public AccountsService(IDataConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        // TODO: implement a TryGet() pattern instead
        public Account GetAccount(Guid id)
        {
            using (var connection = _connectionProvider.GetConnection())
            {
                // TODO: implement proper models instead of calling directly to the database..
                SqlCommand command = new SqlCommand($"select * from Accounts where Id = '{id}'", connection);
                connection.Open();
                var reader = command.ExecuteReader();
                if (!reader.Read())
                    throw new ArgumentException();

                var account = new Account(id);
                account.Name = reader["Name"].ToString();
                account.Number = reader["Number"].ToString();
                account.Amount = float.Parse(reader["Amount"].ToString());
                return account;
            }
        }

        // TODO: This whole things is just awful. There should only be one database call to get all the account rows
        // Currently it has a call for EACH account which is ridiculous
        public IEnumerable<Account> GetAccounts()
        {
            using (var connection = _connectionProvider.GetConnection())
            {
                SqlCommand command = new SqlCommand($"select Id from Accounts", connection);
                connection.Open();
                var reader = command.ExecuteReader();
                var accounts = new List<Account>();
                while (reader.Read())
                {
                    var id = Guid.Parse(reader["Id"].ToString());
                    var account = GetAccount(id);
                    accounts.Add(account);
                }

                return accounts;
            }
        }

        public void AddNewAccount(Account account)
        {
            using (var connection = _connectionProvider.GetConnection())
            {
                SqlCommand command = new SqlCommand($"insert into Accounts (Id, Name, Number, Amount) values ('{Guid.NewGuid()}', '{account.Name}', {account.Number}, 0)", connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateExistingAccount(Account account)
        {
            using (var connection = _connectionProvider.GetConnection())
            {
                SqlCommand command = new SqlCommand($"update Accounts set Name = '{account.Name}' where Id = '{account.Id}'", connection);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteExistingAccount(Account account)
        {
            using (var connection = _connectionProvider.GetConnection())
            {
                SqlCommand command = new SqlCommand($"delete from Accounts where Id = '{account.Id}'", connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}