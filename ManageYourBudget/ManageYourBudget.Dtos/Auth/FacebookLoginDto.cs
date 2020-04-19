using System.ComponentModel.DataAnnotations;

namespace ManageYourBudget.Dtos.Auth
{
    public class FacebookLoginDto
    {
        [Required]
        public string AccessToken { get; set; }
    }
}
