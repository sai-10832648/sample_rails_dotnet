using Microsoft.AspNetCore.Mvc;
using sample_rails_app_8th_edNT.Models;
using Microsoft.AspNetCore.Authorization;

namespace sample_rails_app_8th_edNT.Controllers
{
    [Authorize]
    public class RelationshipsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RelationshipsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // POST: /Relationships/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int followedId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == followedId);
            // current user follows user (implement logic)
            // ...
            return RedirectToAction("Details", "Users", new { id = user?.Id });
        }

        // POST: /Relationships/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var relationship = _context.Relationships.FirstOrDefault(r => r.Id == id);
            var user = relationship?.Followed;
            if (relationship != null)
            {
                _context.Relationships.Remove(relationship);
                _context.SaveChanges();
            }
            return RedirectToAction("Details", "Users", new { id = user?.Id });
        }
    }
}
