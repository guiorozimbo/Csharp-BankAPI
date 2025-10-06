using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAPI.Models
{
    [Table("Transactions")]
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public string TransactionUniqueReference { get; set; } // e.g., Deposit, Withdrawal, Transfer
        public TranStatus TransactionStatus { get; set; } // e.g., Pending, Completed, Failed
        public bool IsSuccessful => TransactionStatus.Equals(TranStatus.Success);
        public string TransactionSourceAccount { get; set; } // Account number or identifier
        public string TransactionDestinationAccount { get; set; } // Account number or identifier
        public string Transactionparticulars { get; set; } // Description or details of the transaction
        public TranType TransactionType { get; set; } // e.g., Credit, Debit
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
        public Transaction()
        {
            // Generate a unique reference for the transaction
            TransactionUniqueReference = $"{Guid.NewGuid().ToString().Replace("-", "").Substring(1, 17)}";
        }
    }
    public enum TranStatus
    {
        Failed,
        Success,
        Error
    }
    public enum TranType
    {
       // Credit,
        Deposit,
        withdrawal,
        Transfer
    }
}
