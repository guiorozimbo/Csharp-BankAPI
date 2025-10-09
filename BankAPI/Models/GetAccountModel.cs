using System.ComponentModel.DataAnnotations;

namespace BankAPI.Models
{
    public class GetAccountModel
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
     
        public DateTime DateCreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime DateLastUpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
