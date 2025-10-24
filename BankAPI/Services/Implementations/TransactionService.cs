using BankAPI.DAL;
using BankAPI.Models;
using BankAPI.Service;
using BankAPI.Services.Interface;
using BankAPI.Utils;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Services.Implementations
{
    public class TransactionService : ITransactionService
    {
        private readonly YouBakingDbContext _dbcontextFactory;
        ILogger<TransactionService> _logger;
        private AppSettings _settings;
        private static string _ourBankSettlementAccount;
        private readonly IAccountService _accountService;

        public TransactionService(YouBakingDbContext dbContext, ILogger<TransactionService> logger, IOptions<AppSettings> settings, IAccountService accountService)
        {
            _dbcontextFactory = dbContext;
            _logger = logger;
            _settings = settings.Value;
            _ourBankSettlementAccount = _settings.OurBankSettlementAccount;
            _accountService = accountService;
        }

        public Response CreateNewTransaction(Transaction transaction)
        {
            // Create new transaction
            Response response = new Response();
            _dbcontextFactory.Transactions.Add(transaction);
            _dbcontextFactory.SaveChanges();
            response.ResponseCode = "00";
            response.ResponseMessage = "Transaction created successfully!";
            response.Data = null;

            return response;
        }

        public Response FindTransactionByDate(DateTime date)
        {
            Response response = new Response();
            var transactions = _dbcontextFactory.Transactions
                .Where(t => t.TransactionDate.Date == date)
                .ToList(); // Ensure date comparison ignores time component beacause DateTime includes time
            response.ResponseCode = "00";
            response.ResponseMessage = "Transaction created successfully!";
            response.Data = transactions;
            return response;
        }

        public Response MakeDeposit(string AccountNumber, decimal Amount, string TransactionPin)
        {
            //make deposit
            Response response = new Response();
            Account sourceAccount;
            Account destinationAccount;
            Transaction transaction = new Transaction();
            // find our bank settlement account for user deposit autheticate
            var authUser = _accountService.Authenticate(AccountNumber,TransactionPin);
            if (authUser == null) throw new Exception("Authentication failed. Invalid account number or transaction pin.");
            // so validation passed
            try
            {
                //for deposit our source account is our bank settlement account giving money to user
                sourceAccount = _accountService.GetByAccountNumber(_ourBankSettlementAccount);
                destinationAccount = _accountService.GetByAccountNumber(AccountNumber);
                //now, lets update account balances
                sourceAccount.CurrentAccountBalance -= Amount;
                destinationAccount.CurrentAccountBalance += Amount;

                // check if source account has sufficient balance
                if ((_dbcontextFactory.Entry(sourceAccount).State== Microsoft.EntityFrameworkCore.EntityState.Modified) && (_dbcontextFactory.Entry(destinationAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified))
                {
                    // so transaction can proceed successfull
                    transaction.TransactionStatus= TranStatus.Success;
                    response.ResponseCode = "00";
                    response.ResponseMessage = "Insufficient funds in the source account.";
                    response.Data = null;
                    return response;
                }
                else
                {                     // proceed to create transaction record
                    transaction.TransactionStatus = TranStatus.Failed;
                    response.ResponseCode = "02";
                    response.ResponseMessage = "Transaction failed.";
                    response.Data = null;
                    transaction.TransactionUniqueReference = Guid.NewGuid().ToString();
                    transaction.TransactionSourceAccount = sourceAccount.AccountNumberGenerated;
                    transaction.TransactionDestinationAccount = destinationAccount.AccountNumberGenerated;
                    transaction.TransactionType = TranType.Deposit;
                    transaction.Transactionparticulars = $"Deposit of {Amount} to account {AccountNumber}";
                    transaction.TransactionDate = DateTime.Now;
                    // save changes to database}
                }
            catch (Exception ex)
            {
                _logger.LogError($"Error making deposit: {ex.Message}");
                response.ResponseCode = "96";
                response.ResponseMessage = "System malfunction. Please try again later.";
                response.Data = null;
                return response;
            }
            //set transaction details here
            transaction.TransactionType = TranType.Deposit;
            transaction.TransactionSourceAccount = _ourBankSettlementAccount;
            transaction.TransactionDestinationAccount = AccountNumber;
            transaction.Transactionparticulars = $"Deposit of to account {JsonConvert.SerializeObject(transaction.TransactionSourceAccount)}";
            transaction.TransactionAmount = Amount;
            transaction.TransactionDate = DateTime.Now;
            transaction.TransactionUniqueReference = Guid.NewGuid().ToString();
        }

        public Response MakeTransfer(string FromAccount, string ToAccount, decimal Amount, string TransactionPin)
        {
            throw new NotImplementedException();
        }

        public Response MakeWithdraw(string AccountNumber, decimal Amount, string TransactionPin)
        {
            throw new NotImplementedException();
        }
    }
}
