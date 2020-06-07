using System.ComponentModel.DataAnnotations;
using ManageYourBudget.Dtos.ValidationAttributes;

namespace ManageYourBudget.Dtos.Chart
{
    public class VerticalBarChartRequestDto: BaseChartDataRequestDto
    {
        [Required]
        [ValidYear]
        public int Year { get; set; }
    }
}
