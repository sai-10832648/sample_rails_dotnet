using Microsoft.AspNetCore.Mvc;
using sample_rails_app_8th_edNT.Models;
using Microsoft.AspNetCore.Authorization;

namespace sample_rails_app_8th_edNT.Controllers
{
    [AllowAnonymous]
    public class StaticPagesController : Controller
    {
        // GET: /StaticPages/Home
        public IActionResult Home(int? page)
        {
            if (User.Identity.IsAuthenticated)
            {
                // Build new micropost and get feed items for current user
                // ...
            }
            return View();
        }

        // GET: /StaticPages/Help
        public IActionResult Help()
        {
            return View();
        }

        // GET: /StaticPages/About
        public IActionResult About()
        {
            return View();
        }

        // GET: /StaticPages/Contact
        public IActionResult Contact()
        {
            return View();
        }
    }
}
