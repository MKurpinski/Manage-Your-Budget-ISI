using System.Collections.Generic;
using ManageYourBudget.Dtos.Profile;

namespace ManageYourBudget.Dtos.Wallet
{
    public class ExtendedWalletDto: BaseWalletDto
    {
        public List<WalletParticipantDto> Participants { get; set; }
    }
}
