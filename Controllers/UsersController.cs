using Microsoft.AspNetCore.Mvc;
using sample_rails_app_8th_edNT.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace sample_rails_app_8th_edNT.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: /Users
        public IActionResult Index(int? page)
        {
            var users = _context.Users.ToList(); // Add pagination as needed
            return View(users);
        }

        // GET: /Users/Details/5
        public IActionResult Details(int id, int? page)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            var microposts = _context.Microposts.Where(m => m.UserId == user.Id).ToList(); // Add pagination as needed
            ViewBag.Microposts = microposts;
            return View(user);
        }

        // GET: /Users/Create
        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Users/Create
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                // Save user and send activation email
                // ...
                TempData["Info"] = "Please check your email to activate your account.";
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }

        // GET: /Users/Edit/5
        public IActionResult Edit(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (!IsCorrectUser(user)) return RedirectToAction("Index", "Home");
            return View(user);
        }

        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, User user)
        {
            if (!IsCorrectUser(user)) return RedirectToAction("Index", "Home");
            if (ModelState.IsValid)
            {
                // Update user
                TempData["Success"] = "Profile updated";
                return RedirectToAction("Details", new { id = user.Id });
            }
            return View(user);
        }

        // POST: /Users/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
                TempData["Success"] = "User deleted";
            }
            return RedirectToAction("Index");
        }

        // GET: /Users/Following/5
        public IActionResult Following(int id, int? page)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            var following = user?.Following?.ToList() ?? new List<User>(); // Add pagination as needed
            ViewBag.Title = "Following";
            ViewBag.Users = following;
            return View("ShowFollow", user);
        }

        // GET: /Users/Followers/5
        public IActionResult Followers(int id, int? page)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            var followers = user?.Followers?.ToList() ?? new List<User>(); // Add pagination as needed
            ViewBag.Title = "Followers";
            ViewBag.Users = followers;
            return View("ShowFollow", user);
        }

        // Helper methods
        private bool IsCorrectUser(User user)
        {
            // Implement logic to check if current user matches
            return true;
        }
    }
}
