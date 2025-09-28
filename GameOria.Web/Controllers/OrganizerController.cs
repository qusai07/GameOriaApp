using Microsoft.AspNetCore.Mvc;

namespace GameOria.Web.Controllers
{
    public class OrganizerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
