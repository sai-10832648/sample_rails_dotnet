using Microsoft.AspNetCore.Mvc;
using sample_rails_app_8th_edNT.Models;
using Microsoft.AspNetCore.Authorization;

namespace sample_rails_app_8th_edNT.Controllers
{
    [AllowAnonymous]
    public class AccountActivationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountActivationsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: /AccountActivations/Edit/{id}?email={email}
        public IActionResult Edit(string id, string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user != null && !user.Activated)
            {
                // Activate user and log in
                // ...
                TempData["Success"] = "Account activated!";
                return RedirectToAction("Details", "Users", new { id = user.Id });
            }
            TempData["Danger"] = "Invalid activation link";
            return RedirectToAction("Index", "Home");
        }
    }
}
