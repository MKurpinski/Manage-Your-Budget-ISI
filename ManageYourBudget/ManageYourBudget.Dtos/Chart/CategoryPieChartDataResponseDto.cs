using System.Collections.Generic;
using ManageYourBudget.Common.Enums;

namespace ManageYourBudget.Dtos.Chart
{
    public class CategoryPieChartDataResponseDto: BaseChartDataResponseDto
    {
        public string Type { get; set; }
        public List<decimal> Points { get; set; }
        public List<string> Labels { get; set; }
    }
}
