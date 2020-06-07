using System.Net;
using System.Threading.Tasks;
using ManageYourBudget.BussinessLogic.Providers.ChartData;
using ManageYourBudget.Common.Enums;
using ManageYourBudget.Dtos.Chart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManageYourBudget.Api.Controllers
{
    [Route("api/chart")]
    [Authorize]
    public class ChartController: BaseController
    {
        private readonly IChartDataProviderFactory _chartDataProviderFactory;

        public ChartController(IChartDataProviderFactory chartDataProviderFactory)
        {
            _chartDataProviderFactory = chartDataProviderFactory;
        }

        [Route("categoryPie")]
        [HttpGet]
        [ProducesResponseType(typeof(CategoryPieChartDataResponseDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCategoryPieChartData([FromQuery] DateTimeRangeChartDataRequestDto request)
        {
            return await GetChartData(request, ChartType.CategoryPie);
        }

        [Route("verticalBar")]
        [HttpGet]
        [ProducesResponseType(typeof(VerticalBarChartDataResponseDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetVerticalBarChartData([FromQuery] VerticalBarChartRequestDto request)
        {
            return await GetChartData(request, ChartType.VerticalBar);
        }

        private async Task<IActionResult> GetChartData(BaseChartDataRequestDto request, ChartType chartType)
        {
            var result = await _chartDataProviderFactory.GetData(request, chartType, UserId);
            return Ok(result);
        }
    }
}
