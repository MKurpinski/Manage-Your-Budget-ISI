using System.ComponentModel.DataAnnotations;

namespace ManageYourBudget.Dtos.Auth
{
    public class ResetPasswordDto
    {
        [Required]
        public string Hash { get; set; }
        [Required]
        public string Password { get; set; }
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
