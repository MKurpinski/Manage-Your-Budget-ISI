using System;

namespace ManageYourBudget.Common.Enums
{
    [Flags]
    public enum WalletRole
    {
        Admin = 0,
        Creator = 1,
        InActive = 2,
        Normal = 3
    }

    public static class WalletRoleExtensions
    {
        public static bool HasAllPrivileges(this WalletRole role)
        {
            return role == WalletRole.Admin || role == WalletRole.Creator;
        }
    }
}
