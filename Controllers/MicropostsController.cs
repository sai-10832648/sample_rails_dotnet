using Microsoft.AspNetCore.Mvc;
using sample_rails_app_8th_edNT.Models;
using Microsoft.AspNetCore.Authorization;

namespace sample_rails_app_8th_edNT.Controllers
{
    [Authorize]
    public class MicropostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MicropostsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // POST: /Microposts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Micropost micropost)
        {
            // Attach image if present
            // ...
            if (ModelState.IsValid)
            {
                // Save micropost for current user
                // ...
                TempData["Success"] = "Micropost created!";
                return RedirectToAction("Index", "Home");
            }
            // If save fails, reload feed items
            // ...
            return View("Home", micropost); // Or pass feed items as needed
        }

        // POST: /Microposts/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var micropost = _context.Microposts.FirstOrDefault(m => m.Id == id);
            if (micropost == null)
            {
                return RedirectToAction("Index", "Home");
            }
            _context.Microposts.Remove(micropost);
            _context.SaveChanges();
            TempData["Success"] = "Micropost deleted";
            var referrer = Request.Headers["Referer"].ToString();
            if (string.IsNullOrEmpty(referrer))
                return RedirectToAction("Index", "Home");
            else
                return Redirect(referrer);
        }
    }
}
