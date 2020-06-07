using System;
using System.ComponentModel.DataAnnotations;

namespace ManageYourBudget.Dtos.Chart
{
    public class DateTimeRangeChartDataRequestDto: BaseChartDataRequestDto
    {
        [Required]
        public DateTime? From { get; set; }
        [Required]
        public DateTime? To { get; set; }
    }
}
