using Microsoft.AspNetCore.Mvc;

namespace GameOria.Web.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
