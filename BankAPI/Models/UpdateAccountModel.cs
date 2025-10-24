using System.ComponentModel.DataAnnotations;

namespace BankAPI.Models
{
    public class UpdateAccountModel
    {
        [Key]
        public int Id { get; set; }
      
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
      
        public DateTime DateLastUpdatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "Pin must be exactly 4 digits.")]
        [Compare("Pin", ErrorMessage = "Pin do not match.")]
        public string Pin { get; set; }
        public string ConfirmPin { get; set; }
    }
}
