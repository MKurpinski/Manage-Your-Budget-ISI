using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManageYourBudget.Common.Extensions;
using ManageYourBudget.DataAccess.Interfaces;
using ManageYourBudget.Dtos.Chart;

namespace ManageYourBudget.BussinessLogic.Providers.ChartData
{
    public class CategoryPieChartDataProvider: IChartDataProvider
    {
        private readonly IExpenseRepository _expenseRepository;

        public CategoryPieChartDataProvider(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<List<BaseChartDataResponseDto>> GetChartData(BaseChartDataRequestDto baseRequest)
        {
            var dateTimeRange = baseRequest as DateTimeRangeChartDataRequestDto;
            var allExpensesFromRange = await _expenseRepository.GetByDates(dateTimeRange.From.Value, dateTimeRange.To.Value, dateTimeRange.WalletId.ToDeobfuscated());

            var groupedByTypes = allExpensesFromRange.GroupBy(x => x.Type);

            var list = new List<BaseChartDataResponseDto>();

            foreach (var typeGroup in groupedByTypes)
            {
                var pieChartData = new CategoryPieChartDataResponseDto
                {
                    Labels = new List<string>(),
                    Points = new List<decimal>(),
                    Type = typeGroup.Key.GetStringValue()
                };

                var groupedByCategory = typeGroup.GroupBy(x => x.Category);
                foreach (var groupedCategory in groupedByCategory)
                {
                    pieChartData.Labels.Add(groupedCategory.Key.GetStringValue());
                    pieChartData.Points.Add(Math.Round(groupedCategory.Sum(x => x.Price), 2));
                }

                list.Add(pieChartData);
            }

            return list;
        }
    }
}
