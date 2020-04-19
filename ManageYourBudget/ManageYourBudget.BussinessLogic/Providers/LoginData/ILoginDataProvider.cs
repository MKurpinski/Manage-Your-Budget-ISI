using System.Threading.Tasks;
using ManageYourBudget.Common.Enums;
using ManageYourBudget.Dtos.Auth;
using ManageYourBudget.Dtos.Auth.LoginDataProvider;

namespace ManageYourBudget.BussinessLogic.Providers.LoginData
{
    public interface ILoginDataProvider
    {
        LoginProvider Type { get; }
        Task<ExternalRegisterUserDto> GetExternalData(IExternalDataDto externalData);
        string GetRedirectUrl();
        string GetLogoutUri();
    }
}
