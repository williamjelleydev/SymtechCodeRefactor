# Refactoring Assessment

This repository contains a terribly written Web API project. It's terrible on purpose, so you can show us how we can improve it.

## Getting Started

Fork this repository, and make the changes you would want to see if you had to maintain this api. To set up the project:

 - Open in Visual Studio (2015 or later is preferred)
 - Restore the NuGet packages and rebuild
 - Run the project
 
 Once you are satisied, replace the contents of the readme with a summary of what you have changed, and why. If there are more things that could be improved, list them as well.

The api is composed of the following endpoints:

| Verb     | Path                                   | Description
|----------|----------------------------------------|--------------------------------------------------------
| `GET`    | `/api/Accounts`                        | Gets the list of all accounts
| `GET`    | `/api/Accounts/{id:guid}`              | Gets an account by the specified id
| `POST`   | `/api/Accounts`                        | Creates a new account
| `PUT`    | `/api/Accounts/{id:guid}`              | Updates an account
| `DELETE` | `/api/Accounts/{id:guid}`              | Deletes an account
| `GET`    | `/api/Accounts/{id:guid}/Transactions` | Gets the list of transactions for an account
| `POST`   | `/api/Accounts/{id:guid}/Transactions` | Adds a transaction to an account, and updates the amount of money in the account

Models should conform to the following formats:

**Account**
```
{
    "Id": "01234567-89ab-cdef-0123-456789abcdef",
	"Name": "Savings",
	"Number": "012345678901234",
	"Amount": 123.4
}
```	

**Transaction**
```
{
    "Date": "2018-09-01",
    "Amount": -12.3
}
```

# Changes I Have made
In the time I had, I focused mainly on separating out code into separate modular components/classes. This makes the code more readable, testable, and easier to change. As a next step I attempted to upgrade to .NetCore 2.1 and use AspNetCore MVC for the controllers, in hindsight this was a bit much to chew off in an hour. As the code is, there are still issues trying to connect to the database for some reason. But in saying that, I had issues figuring out how to inject my new classes/interfaces into the Controllers, and when you are having issues with old libraries/frameworks - moving forward and upgrading is often the best option/easiest anyway.


## Here are some specifics of what I have changed:
* Move business logic out of AccountController and into IAccountsService/AccountsService. I made it inherit Microsoft.AspNetCore.Mvc.ControllerBase.
* AccountService is now responsible for all database interactions specific to Accounts table. This helped clean up the AccountController and Account classes.
* IDataConnectionProvider/DataConnectionProvider is responsible for providing the SqlConnection. This will make unit testing easier in the future, not having to rely on static calls to Helpers class (which was removed)
* Moved business logic out of TransactionController and into ITransactionService/TransactionService. I made it inherit Microsoft.AspNetCore.Mvc.ControllerBase.
* TransactionService is now responsible for all database interactions specific to the Transactions table. This helped clean up the TransactionController.
* I moved all database logic out of the Account class and into the AccountsService. That kind of logic doesn't really belong in a Model.
* Created a standard Program and Startup class for setting up the AspNetCore web application, and configured some of the services.


## What I would do if I had more time:
* Return more appropriate status codes in AccountController and TransactionController. 
* Implement TryGet() patterns in AccountsService and TransactionService. This will make it clearer for the Controllers to know whether to return a NotFound()
* Stop calling direct sql queries in AccountsService and TransactionService. Creating models (maybe Entity Framework?) that integrate with the database is a much better option.
* Get the DataConnectionProvider.GetConnection() method working, my limited experience in mssql probably hindered me here. Validation of the connection string passed into DataConnectionProvider as well.
* Create an appsettings.json file, and put the database connection details in there. (If we started putting passwords in the connection string then would need another option though)
* Obviously tidying up my sporadic commented out code everywhere.
* It would probably make sense to have a separate AccountDto that the user/client interacts with and a separate AccountModel that the database can interact with. Same applies for transaction.
* Minor but it triggers me that Accounts.cs is plural when the class name is Account - they should match.
* Remove the now redundant isNew field from Account. It's no longer needed after moving logic into AccountsService
* Ensure that Account.Id is set appropriately during create and updates. This may fall out of creating a sepearate AccountDto though..?
* Rename AccountsService to AccountService (that ones my bad but the plural naming should really be consistent)
* Ensure that format of api responses are the same as before. There is some setup in WebApiConfig that has not yet been replaced.
* I don't even know what to make of the entry point for this program. Cutting it out completely seemed like a good option, so that which is something the Program.cs and Startup.cs classes would fix anyway
* Write integration tests for the API. In an actual work environment, I would not attempt a big refactor like this without at least gettting some integration tests set up.
* Write unit tests for classes. This should be a lot easier now that the classes are more modular.
* Upgrade further to latest LTS version of dotnetcore.

