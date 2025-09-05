using Microsoft.AspNetCore.Mvc;
using sample_rails_app_8th_edNT.Models;
using Microsoft.AspNetCore.Authorization;

namespace sample_rails_app_8th_edNT.Controllers
{
    [AllowAnonymous]
    public class PasswordResetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PasswordResetsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: /PasswordResets/New
        public IActionResult New()
        {
            return View();
        }

        // POST: /PasswordResets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                // Create reset digest and send email
                // ...
                TempData["Info"] = "Email sent with password reset instructions";
                return RedirectToAction("Index", "Home");
            }
            TempData["Danger"] = "Email address not found";
            return View();
        }

        // GET: /PasswordResets/Edit/{id}?email={email}
        public IActionResult Edit(string id, string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (!IsValidUser(user, id))
                return RedirectToAction("Index", "Home");
            if (IsResetExpired(user))
            {
                TempData["Danger"] = "Password reset has expired.";
                return RedirectToAction("New");
            }
            return View(user);
        }

        // POST: /PasswordResets/Update/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(string id, string email, string password, string passwordConfirmation)
        {
            User user = null;
            if (!IsValidUser(user, id))
                return RedirectToAction("Index", "Home");
            if (string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("Password", "can't be empty");
                return View("Edit", user);
            }
            // Update password logic
            // if (/* Update successful */)
            // {
            //     // Reset session and log in
            //     // ...
            //     TempData["Success"] = "Password has been reset.";
            //     return RedirectToAction("Details", "Users", new { id = user.Id });
            // }
            return View("Edit", user);
        }

        // Helper methods
        private bool IsValidUser(User user, string token)
        {
            // Check if user is activated and token is valid
            return true;
        }

        private bool IsResetExpired(User user)
        {
            // Check if password reset has expired
            return false;
        }
    }
}
