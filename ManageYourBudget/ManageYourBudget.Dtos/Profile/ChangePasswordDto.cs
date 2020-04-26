using System.ComponentModel.DataAnnotations;

namespace ManageYourBudget.Dtos.Profile
{
    public class ChangePasswordDto
    {
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string Password { get; set; }
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}