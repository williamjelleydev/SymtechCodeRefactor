using Microsoft.AspNetCore.Mvc;
using refactor_this.Models;
using refactor_this.Services;
using System;
using System.Collections.Generic;

namespace refactor_this.Controllers
{
    [ApiController]
    [Route("api/Accounts")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountsService _accountsService;

        public AccountController(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }

        //[HttpGet, Route("api/Accounts/{id}")]
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            // TODO: return NotFound() if account does not exist
            return Ok(_accountsService.GetAccount(id));
        }

        //[HttpGet, Route("api/Accounts")]
        [HttpGet]
        public IActionResult GetAccounts()
        {
            return Ok(_accountsService.GetAccounts());
        }


        [HttpPost]
        public IActionResult Add(Account account)
        {
            _accountsService.AddNewAccount(account);
            // TODO: need some way of validating that the account _actually_ got persisted to database successfully
            // It would also be nice to have a different input AccountDto from the persisted Account model
            // TODO: CreatedAtRoute() would be a better option here, so consumer can see where to locate newly created resouce
            return Ok(account);
        }

        // I'm not in charge of API decisions, but generally PATCH's are nicer to deal with than PUTs so should be considered
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, Account account)
        {
            var existingAccount = _accountsService.GetAccount(id);
            // TODO: null checking on if account actually exists
            // This would be way cleaner and easier to build on using models
            existingAccount.Name = account.Name;
            _accountsService.UpdateExistingAccount(existingAccount);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var existingAccount = _accountsService.GetAccount(id);
            // TODO: check if it exists and if not return NotFound()
            _accountsService.DeleteExistingAccount(existingAccount);
            return NoContent();
        }
    }
}