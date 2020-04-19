namespace ManageYourBudget.Options
{
    public class JwtTokenOptions
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int TokenExpirationTimeInMinutes { get; set; }
    }
}