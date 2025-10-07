using GameOria.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace GameOria.Api.Controllers
{
    public class OrganizerController : Controller
    {
        private readonly GameOriaDbContext _context;

        public OrganizerController()
        {
        }

     


        public IActionResult Index()
        {
            return View();
        }
    }
}
