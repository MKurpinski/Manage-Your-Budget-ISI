﻿namespace ManageYourBudget.Shared.Events
{
    public class AssignToWalletMessage: BaseMessage
    {
        public override string Type => nameof(AssignToWalletMessage);
        public string By { get; set; }
        public string Link { get; set; }
    }
}
