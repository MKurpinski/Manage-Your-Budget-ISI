using System;
using System.Collections.Generic;
using System.Text;

namespace ManageYourBudget.EmailMessages
{
    public class WalletArchivedMessage: BaseMessage
    {
        public override string Type => nameof(WalletArchivedMessage);
        public string By { get; set; }
        public string WalletName { get; set; }
    }
}
