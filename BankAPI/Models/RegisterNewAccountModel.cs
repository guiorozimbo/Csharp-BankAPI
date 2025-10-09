using System.ComponentModel.DataAnnotations;

namespace BankAPI.Models
{
    public class RegisterNewAccountModel
    {
      
        public string Name { get; set; }
        public string LastName { get; set; }

       // public string AccountName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
      //  public decimal CurrentAccountBalance { get; set; }
        public AccountType AccountType { get; set; }
       // public string AccountNumberGenerated { get; set; }
        // Navigation property for related transactions store account transactions pin
       // public byte[] pinHash { get; set; }
      //  public byte[] pinSalt { get; set; }
        public DateTime DateCreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime DateLastUpdatedAt { get; set; } = DateTime.UtcNow;
        //lets add regullar properties for pin and confirm pin
        [Required]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Pin must be exactly 4 digits.")]
        [Compare("Pin", ErrorMessage = "Pin do not match.")]
        public string Pin { get; set; }
        public string ConfirmPin { get; set; }
    }
}
