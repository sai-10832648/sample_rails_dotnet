using System;
using System.Security.Cryptography;
using System.Text;

namespace sample_rails_app_8th_edNT.Helpers
{
    public static class UsersHelper
    {
        // Returns the Gravatar URL for the given user
        public static string GravatarFor(string email, string name, int size = 80)
        {
            using (var md5 = MD5.Create())
            {
                var emailBytes = Encoding.UTF8.GetBytes(email.Trim().ToLower());
                var hashBytes = md5.ComputeHash(emailBytes);
                var gravatarId = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                var gravatarUrl = $"https://secure.gravatar.com/avatar/{gravatarId}?s={size}";
                // In Razor views, use: <img src="..." alt="..." class="gravatar" />
                return gravatarUrl;
            }
        }
    }
}
