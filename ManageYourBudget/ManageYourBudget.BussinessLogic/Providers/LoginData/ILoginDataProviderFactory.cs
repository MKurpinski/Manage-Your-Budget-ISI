using ManageYourBudget.Common.Enums;

namespace ManageYourBudget.BussinessLogic.Providers.LoginData
{
    public interface ILoginDataProviderFactory
    {
        ILoginDataProvider Create(LoginProvider loginProvider);
    }
}
