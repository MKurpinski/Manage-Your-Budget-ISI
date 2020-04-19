using System.ComponentModel.DataAnnotations;

namespace ManageYourBudget.Dtos.Auth
{
    public class LoginUserDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
