using Cryptography.Obfuscation;

namespace ManageYourBudget.Common.Extensions
{
    public static class IntExtensions
    {
        public static string ToObfuscated(this int value)
        {
            return new Obfuscator().Obfuscate(value);
        }
    }
}
