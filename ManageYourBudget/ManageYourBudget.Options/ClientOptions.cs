using System;
using System.Collections.Generic;
using System.Text;

namespace ManageYourBudget.Options
{
    public class ClientOptions
    {
        public string BaseUrl { get; set; }
        public string WalletUrl => $"{BaseUrl}/wallet";
    }
}
