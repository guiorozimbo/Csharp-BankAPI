using System.ComponentModel.DataAnnotations;

namespace BankAPI.Models
{
    public class AuthenticateModel
    {
        [Required]
        public int AccountNumber { get; set; }
        [Required]
        public string Pin { get; set; }
    }
}
