using System.Collections.Generic;
using System.Threading.Tasks;
using ManageYourBudget.Dtos.Chart;

namespace ManageYourBudget.BussinessLogic.Providers.ChartData
{
    public interface IChartDataProvider
    {
        Task<List<BaseChartDataResponseDto>> GetChartData(BaseChartDataRequestDto request);
    }
}
