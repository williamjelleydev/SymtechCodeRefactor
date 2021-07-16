using Microsoft.AspNetCore.Mvc;
using refactor_this.Models;
using refactor_this.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace refactor_this.Controllers
{
    [ApiController]
    [Route("api/Accounts/{id}/Transactions")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public IActionResult GetTransactions(Guid id)
        {
            var transactions = _transactionService.GetTransactionsForAccount(id);
            return Ok(transactions);
        }

        [HttpPost]
        public IActionResult AddTransaction(Guid id, Transaction transaction)
        {
            // TODO: CreatedAtRoute() is a better option here
            if (!_transactionService.TryAddTransactionToAccount(id, transaction, out string failureMessage))
            {
                return BadRequest(failureMessage);
            }

            // TODO: Again, CreatedAtRoute() would be a better option here, so consumer can see where to locate newly created resouce
            return Ok();
        }
    }

}