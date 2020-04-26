using System.ComponentModel.DataAnnotations;

namespace ManageYourBudget.Dtos.Wallet
{
    public class AddWalletDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Currency { get; set; }
    }
}
