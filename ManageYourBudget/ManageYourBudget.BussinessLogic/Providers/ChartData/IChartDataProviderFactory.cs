using System.Collections.Generic;
using System.Threading.Tasks;
using ManageYourBudget.BussinessLogic.Factories;
using ManageYourBudget.Common.Enums;
using ManageYourBudget.Dtos.Chart;

namespace ManageYourBudget.BussinessLogic.Providers.ChartData
{
    public interface IChartDataProviderFactory: IFactory
    {
        Task<List<BaseChartDataResponseDto>> GetData(BaseChartDataRequestDto request, ChartType type, string userId);
    }
}
