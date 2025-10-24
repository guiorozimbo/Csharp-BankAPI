using System.ComponentModel.DataAnnotations;

namespace BankAPI.Models
{
    public class AuthenticateModel
    {
        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Account number must be a 10-digit number.")]
        public string AccountNumber { get; set; }

        [Required]
        public string Pin { get; set; }
    }

}
