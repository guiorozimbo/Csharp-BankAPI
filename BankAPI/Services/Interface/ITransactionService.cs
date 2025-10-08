
using BankAPI.Models;
using System.Transactions;

namespace BankAPI.Services.Interface
{
    public interface ITransactionService
    {
        Response CreateNewTransaction(System.Transactions.Transaction transaction);
        Response FindTransactionByDate(DateTime date);
        Response MakeDeposit(string AccountNumber, decimal Amount,string TransactionPin);
        Response MakeWithdraw(string AccountNumber, decimal Amount, string TransactionPin);
        Response MakeTransfer(string FromAccount, string ToAccount, decimal Amount, string TransactionPin);
    }
}
