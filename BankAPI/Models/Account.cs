using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Models
{
    [Table("Accounts")]
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public string AccountName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public decimal CurrentAccountBalance { get; set; }
        public AccountType AccountType { get; set; }
        public string AccountNumberGenerated { get; set; }
        // Navigation property for related transactions store account transactions pin
        public byte[] pinHash { get; set; }
        public byte[] pinSalt { get; set; }
        public DateTime DateCreatedAt { get; set; } =  DateTime.UtcNow;
        public DateTime DateLastUpdatedAt { get; set; } = DateTime.UtcNow;
        // now generate an AccountNumber, lets see do that in the constructor
        // First create random number generator obj
        Random random = new Random();
        public Account()
        {
            AccountNumberGenerated = Convert.ToString((long)Math.Floor(random.NextDouble() * 9_000_000_000L + 1_000_000_000L));// wee did generate a 10 digit account number
            // also AccountName property is a combination of Name and LastName properties
            AccountName = $"{Name} {LastName}";
        }
        public List<Transaction> Transactions { get; set; }
    }
    public enum AccountType
    {
        Savings,
        Current,
        Corporate,
        Governament
    }
}
