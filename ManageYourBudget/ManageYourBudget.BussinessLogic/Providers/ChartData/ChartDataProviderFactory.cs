using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac.Features.Indexed;
using ManageYourBudget.BussinessLogic.Interfaces;
using ManageYourBudget.Common.Enums;
using ManageYourBudget.Dtos.Chart;

namespace ManageYourBudget.BussinessLogic.Providers.ChartData
{
    public class ChartDataProviderFactory: IChartDataProviderFactory
    {
        private readonly IWalletPermissionService _permissionService;
        private readonly IIndex<ChartType, IChartDataProvider> _providers;

        public ChartDataProviderFactory(IIndex<ChartType, IChartDataProvider> providers, IWalletPermissionService permissionService)
        {
            _permissionService = permissionService;
            _providers = providers;
        }

        public async Task<List<BaseChartDataResponseDto>> GetData(BaseChartDataRequestDto request, ChartType type, string userId)
        {
            var hasPermission = await _permissionService.HasUserAccess(request.WalletId, userId);

            if (!hasPermission)
            {
                return new List<BaseChartDataResponseDto>();
            }

            var provider = _providers[type];

            return await provider.GetChartData(request);
        }
    }
}
