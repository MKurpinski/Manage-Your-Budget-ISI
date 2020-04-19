using System.ComponentModel.DataAnnotations;

namespace ManageYourBudget.Dtos.Auth
{
    public abstract class RegisterUserDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }
}