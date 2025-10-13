using System.ComponentModel.DataAnnotations;

namespace BankAPI.Models
{
    public class AuthenticateModel
    {
        [Required]
        [RegularExpression(@"^[0][1-9]\d{9}$^[1-9]\d{9}$", ErrorMessage = "Account number must be a 10-digit number.")]
        public int AccountNumber { get; set; }
        [Required]
        public string Pin { get; set; }
    }
}
