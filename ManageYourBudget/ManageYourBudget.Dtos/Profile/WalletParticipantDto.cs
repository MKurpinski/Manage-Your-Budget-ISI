using System;
using System.Collections.Generic;
using System.Text;

namespace ManageYourBudget.Dtos.Profile
{
    public class WalletParticipantDto
    {
        public UserDto User { get; set; }
        public string Role { get; set; }
    }
}
