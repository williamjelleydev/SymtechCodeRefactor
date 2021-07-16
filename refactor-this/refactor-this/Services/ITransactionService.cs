using refactor_this.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace refactor_this.Services
{
    public interface ITransactionService
    {
        IEnumerable<Transaction> GetTransactionsForAccount(Guid accountId);

        bool TryAddTransactionToAccount(Guid accountId, Transaction transaction, out string failureMessage);
    }
}
