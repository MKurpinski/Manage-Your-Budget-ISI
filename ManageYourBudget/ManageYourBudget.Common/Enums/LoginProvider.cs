using global::System;
using System.Collections.Generic;
using System.Text;

namespace ManageYourBudget.Common.Enums
{
    [Flags]
    public enum LoginProvider
    {
        Local = 0,
        Facebook = 1,
        Google = 2
    }
}
