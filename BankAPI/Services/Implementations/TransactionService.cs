using BankAPI.DAL;
using BankAPI.Models;
using BankAPI.Services.Interface;
using BankAPI.Utils;
using System.Linq;


namespace BankAPI.Services.Implementations
{
    public class TransactionService : ITransactionService
    {
        private readonly YouBakingDbContext _dbcontextFactory;
        ILogger<TransactionService> _logger;
        private AppSettings _settings;
        private static string _ourBankSettlementAccount;
        public TransactionService(YouBakingDbContext dbContext, ILogger<TransactionService> logger,IOptions<AppSettings>settings)
        {
            _dbcontextFactory = dbContext;
            _logger = logger;
            _settings = settings.Value;
            _ourBankSettlementAccount = _settings.OurBankSettlementAccount;
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
            throw new NotImplementedException();
        }

        public Response MakeDeposit(string AccountNumber, decimal Amount, string TransactionPin)
        {
            throw new NotImplementedException();
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
