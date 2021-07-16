using refactor_this.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace refactor_this.Services
{
    public interface IAccountsService
    {
        // TODO: an AccountExists(Guid guid) would be handy for the controller to use

        Account GetAccount(Guid id);

        IEnumerable<Account> GetAccounts();


        void AddNewAccount(Account account);

        void UpdateExistingAccount(Account account);

        void DeleteExistingAccount(Account account);
    }
}
