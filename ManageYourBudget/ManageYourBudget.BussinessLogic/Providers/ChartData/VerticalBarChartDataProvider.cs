using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ManageYourBudget.Common.Extensions;
using ManageYourBudget.DataAccess.Interfaces;
using ManageYourBudget.Dtos.Chart;

namespace ManageYourBudget.BussinessLogic.Providers.ChartData
{
    public class VerticalBarChartDataProvider: IChartDataProvider
    {
        private readonly IExpenseRepository _expenseRepository;

        public VerticalBarChartDataProvider(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<List<BaseChartDataResponseDto>> GetChartData(BaseChartDataRequestDto request)
        {
            var dateRequest = request as VerticalBarChartRequestDto;
            var date = new DateTime(dateRequest.Year, 1, 1);
            var allExpenses = await _expenseRepository.GetByDates(date.StartOfYear(), date.EndOfYear(), request.WalletId.ToDeobfuscated());

            var groupedByTypes = allExpenses.GroupBy(x => x.Type);
            var monthList = GetMonthList();

            var list = new List<BaseChartDataResponseDto>();

            foreach (var typeGroup in groupedByTypes)
            {
                var verticalBarData = new VerticalBarChartDataResponseDto
                {
                    Labels = new List<string>(),
                    Points = new List<decimal>(),
                    Type = typeGroup.Key.GetStringValue()
                };

                var groupedByMonth = typeGroup.GroupBy(x => x.Date.Month).OrderBy(x => x.Key).ToList();
                foreach (var month in monthList)
                {
                    var groupedMonth = groupedByMonth.FirstOrDefault(x => x.Key == month);
                    verticalBarData.Labels.Add(DateTimeFormatInfo.CurrentInfo.GetMonthName(month));
                    verticalBarData.Points.Add(Math.Round(groupedMonth?.Sum(x => x.Price) ?? 0, 2));
                }

                list.Add(verticalBarData);
            }

            return list;
        }

        private List<int> GetMonthList()
        {
            return Enumerable.Range(1, 12).ToList();
        }
    }
}
