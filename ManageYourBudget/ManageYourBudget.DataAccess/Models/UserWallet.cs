using System;
using ManageYourBudget.Common.Enums;

namespace ManageYourBudget.DataAccess.Models
{
    public class UserWallet: Entity
    {
        public int WalletId { get; set; }
        public virtual string UserId { get; set; }
        public virtual Wallet Wallet { get; set; }
        public virtual User User { get; set; }
        public DateTime? LastOpened { get; set; }
        public bool Archived { get; set; }
        public WalletRole Role { get; set; }
    }
}
