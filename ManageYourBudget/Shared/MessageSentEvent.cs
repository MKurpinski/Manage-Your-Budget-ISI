using System;
using ManageYourBudget.Shared.Interfaces;

namespace ManageYourBudget.Shared
{
    public class MessageSentEvent : IEvent
    {
        public string Message { get; set; }
        public DateTime SendDate { get; set; }
    }
}