
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using System;
using sample_rails_app_8th_edNT.Models;

namespace sample_rails_app_8th_edNT.Helpers
{
    public static class SessionsHelper
    {
        // Logs in the given user
        public static void LogIn(HttpContext context, User user)
        {
            context.Session.SetInt32("UserId", user.Id);
            context.Session.SetString("SessionToken", user.SessionToken());
        }

        // Remembers a user in a persistent session
        public static void Remember(HttpContext context, User user)
        {
            user.Remember();
            context.Response.Cookies.Append("UserId", user.Id.ToString(), new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1), IsEssential = true });
            context.Response.Cookies.Append("RememberToken", user.RememberToken, new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1), IsEssential = true });
        }

        // Returns the user corresponding to the remember token cookie
        public static User CurrentUser(HttpContext context, Func<int, User> findUserById)
        {
            int? userId = context.Session.GetInt32("UserId");
            if (userId.HasValue)
            {
                var user = findUserById(userId.Value);
                if (user != null && context.Session.GetString("SessionToken") == user.SessionToken())
                    return user;
            }
            else if (context.Request.Cookies.TryGetValue("UserId", out var cookieUserId) && int.TryParse(cookieUserId, out var id))
            {
                var user = findUserById(id);
                if (user != null && user.Authenticated("remember", context.Request.Cookies["RememberToken"]))
                {
                    LogIn(context, user);
                    return user;
                }
            }
            return null;
        }

        // Returns true if the given user is the current user
        public static bool IsCurrentUser(User user, User currentUser)
        {
            return user != null && user == currentUser;
        }

        // Returns true if the user is logged in
        public static bool LoggedIn(User currentUser)
        {
            return currentUser != null;
        }

        // Forgets a persistent session
        public static void Forget(HttpContext context, User user)
        {
            user.Forget();
            context.Response.Cookies.Delete("UserId");
            context.Response.Cookies.Delete("RememberToken");
        }

        // Logs out the current user
        public static void LogOut(HttpContext context, User currentUser)
        {
            Forget(context, currentUser);
            context.Session.Clear();
        }

        // Stores the URL trying to be accessed
        public static void StoreLocation(HttpContext context)
        {
            if (context.Request.Method == "GET")
            {
                context.Session.SetString("ForwardingUrl", context.Request.Path + context.Request.QueryString);
            }
        }
    }
}
