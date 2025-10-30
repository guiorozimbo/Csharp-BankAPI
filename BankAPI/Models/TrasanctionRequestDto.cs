using System.ComponentModel.DataAnnotations;

namespace BankAPI.Models
{
    public class TrasanctionRequestDto
    {
        
      //  public int Id { get; set; }
       // public string TransactionUniqueReference { get; set; } // e.g., Deposit, Withdrawal, Transfer
        
        
        public string TransactionSourceAccount { get; set; } // Account number or identifier
        public string TransactionDestinationAccount { get; set; } // Account number or identifier
       // public string Transactionparticulars { get; set; } // Description or details of the transaction
        public TranType TransactionType { get; set; } // e.g., Credit, Debit
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
        public decimal TransactionAmount { get; set; }
    }
}
