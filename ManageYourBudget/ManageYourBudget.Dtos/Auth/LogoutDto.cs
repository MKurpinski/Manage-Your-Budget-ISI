using System;
using System.Collections.Generic;
using System.Text;

namespace ManageYourBudget.Dtos.Auth
{
    public class LogoutDto
    {
        public bool ExternallyLoggedOut { get; set; }
        public string Uri { get; set; }
    }
}
