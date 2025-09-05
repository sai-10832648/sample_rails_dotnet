using Microsoft.AspNetCore.Mvc;
using sample_rails_app_8th_edNT.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace sample_rails_app_8th_edNT.Controllers
{
    [AllowAnonymous]
    public class SessionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SessionsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: /Sessions/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Sessions/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password, bool rememberMe)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user != null /* && Authenticate user */)
            {
                if (user.Activated)
                {
                    // Handle session, remember me, and login
                    // ...
                    var forwardingUrl = TempData["ForwardingUrl"] as string;
                    // ...session reset logic...
                    // ...remember/forget logic...
                    // ...log in logic...
                    return Redirect(forwardingUrl ?? Url.Action("Details", "Users", new { id = user.Id }));
                }
                else
                {
                    TempData["Warning"] = "Account not activated. Check your email for the activation link.";
                    return RedirectToAction("Index", "Home");
                }
            }
            TempData["Danger"] = "Invalid email/password combination";
            return View();
        }

        // POST: /Sessions/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            // Log out logic
            // ...
            return RedirectToAction("Index", "Home");
        }
    }
}
