using System;
using System.Security.Cryptography;
using System.Text;
using Cryptography.Obfuscation;

namespace ManageYourBudget.Common.Extensions
{
    public static class StringExtensions
    {
        public static byte[] ToByteArray(this string value)
        {
            return Encoding.ASCII.GetBytes(value);
        }

        public static string ToUncodedString(this byte[] value)
        {
            return Encoding.ASCII.GetString(value);
        }

        public static string ToMdHashed(this string value)
        {
            var md5Hash = MD5.Create();

            var inputBytes = Encoding.UTF8.GetBytes(value);

            var hash = md5Hash.ComputeHash(inputBytes);

            var sb = new StringBuilder();

            foreach (var t in hash)
            {
                sb.Append(t.ToString("x2"));
            }

            return sb.ToString();
        }

        public static int ToDeobfuscated(this string value)
        {
            return new Obfuscator().Deobfuscate(value);
        }

        public static TEnum ToEnumValue<TEnum>(this string value) where TEnum : struct, IConvertible
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }
            return !Enum.TryParse(value, true, out TEnum enumValue) ? default : enumValue;
        }
    }
}
