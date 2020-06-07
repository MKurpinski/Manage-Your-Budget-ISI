using System.ComponentModel.DataAnnotations;

namespace ManageYourBudget.Dtos.Chart
{
    public class BaseChartDataRequestDto
    {
        [Required]
        public string WalletId { get; set; }
    }
}