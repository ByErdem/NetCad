using System;
using System.Linq;

namespace NetCad.StudentManagementDesktop.Extensions
{
    public static class StringExtensions
    {
        public static string GenerateUniqueId()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var uniqueId = new string(Enumerable.Repeat(chars, 11)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            return uniqueId;
        }
    }
}
