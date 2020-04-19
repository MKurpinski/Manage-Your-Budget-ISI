using System;

namespace ManageYourBudget.Options
{
    public class GoogleOptions
    {
        public string AuthorizeEndpoint => $"{BaseUrl}";
        public string ResponseType { get; set; }
        public string RedirectUrl { get; set; }
        public string ClientId { get; set; }
        public string Scope { get; set; }
        public string BaseUrl { get; set; }
        public string ClientSecret { get; set; }
        public string TokenEndpoint { get; set; }
        public string UserInfoEndpoint { get; set; }
        public string JwksUrl { get; set; }
        public string ValidIssuer { get; set; }
        public string ClientLoginEndpoint { get; set; }
        public string LogOutEndpoint => $"https://www.google.com/accounts/Logout?continue=https://appengine.google.com/_ah/logout?continue={ClientLoginEndpoint}";
    }
}
