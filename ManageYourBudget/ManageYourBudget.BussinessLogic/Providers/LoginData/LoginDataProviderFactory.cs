using System.Collections.Generic;
using System.Linq;
using ManageYourBudget.Common.Enums;

namespace ManageYourBudget.BussinessLogic.Providers.LoginData
{
    public class LoginDataProviderFactory: ILoginDataProviderFactory
    {
        private readonly IEnumerable<ILoginDataProvider> _loginDataProviders;

        public LoginDataProviderFactory(IEnumerable<ILoginDataProvider> loginDataProviders)
        {
            _loginDataProviders = loginDataProviders;
        }

        public ILoginDataProvider Create(LoginProvider loginProvider)
        {
            return _loginDataProviders.FirstOrDefault(x => x.Type == loginProvider);
        }
    }
}
