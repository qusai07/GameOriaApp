using Gameoria.Application.Common.Interfaces;
using Gameoria.Domains.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace GameOria.Api.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _usermanger;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IDistributedCache _cache;
        private readonly IConfiguration _configuration;
    
        public async Task<IActionResult> Login()
        {

            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                var userLogin = await _usermanger.FindByNameAsync(User.Identity.Name);
                var UserRoles = await _usermanger.GetRolesAsync(userLogin);
                if ((UserRoles.Contains("Admin")))
                {
                    return RedirectToAction("Dash", "Admin");
                }
                else if ((UserRoles.Contains("")))
                {
                    return RedirectToAction("", "");
                }
                else if ((UserRoles.Contains("")))
                {
                    return RedirectToAction("", "");
                }
                else if ((UserRoles.Contains("")))
                {
                    return RedirectToAction("", "");
                }
                else if ((UserRoles.Contains("")))
                {
                    return RedirectToAction("", "");
                }
                else if ((UserRoles.Contains("")))
                {
                    return RedirectToAction("", "");
                }
                else
                {
                    return View("Login");
                }
            }

        }

    }
}
