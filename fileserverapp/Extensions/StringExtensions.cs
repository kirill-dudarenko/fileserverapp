using System;
using System.Text;

namespace Common.Extensions
{
    public static class StringExtensions
    {
        public static string ToBase64String(this string plainText)
        {
            if (plainText == null)
                return null;

            var plainTextBytes = Encoding.UTF8.GetBytes(plainText); 
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string FromBase64String(this string encodedText)
        {
            if (encodedText == null)
                return null;

            var base64EncodedBytes = Convert.FromBase64String(encodedText);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
