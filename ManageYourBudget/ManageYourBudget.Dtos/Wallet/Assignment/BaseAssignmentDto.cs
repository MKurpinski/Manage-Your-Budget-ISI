using System.ComponentModel.DataAnnotations;

namespace ManageYourBudget.Dtos.Wallet.Assignment
{
    public abstract class BaseAssignmentDto
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string WalletId { get; set; }
    }
}
