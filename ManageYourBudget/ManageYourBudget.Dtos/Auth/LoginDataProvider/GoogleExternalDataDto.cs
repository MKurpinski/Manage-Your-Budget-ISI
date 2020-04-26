using System.Net;

namespace ManageYourBudget.Dtos.Auth.LoginDataProvider
{
    public class GoogleExternalDataDto: IExternalDataDto
    {
        public GoogleLoginDto LoginDto { get; set; }
        public IPAddress Ip { get; set; }
    }
}
