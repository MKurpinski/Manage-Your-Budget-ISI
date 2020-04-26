using System;

namespace ManageYourBudget.Common
{
    public static class RandomHashGenerator
    {
        public static string RandomHash => Guid.NewGuid().ToString("N");
    }
}
