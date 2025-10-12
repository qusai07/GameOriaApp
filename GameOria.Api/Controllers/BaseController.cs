using GameOria.Api.Repo.Interface;
using GameOria.Domains.Entities.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected readonly IDataService _dataService;

    public BaseController(IDataService dataService)
    {
        _dataService = dataService;
    }

    protected async Task<ApplicationUser?> GetUserByIdAsync(Guid userId)
        => await _dataService.GetByIdAsync<ApplicationUser>(userId);

    protected async Task<ApplicationUser?> GetCurrentUserAsync()
    {
        var userId = GetUserId();
        if (userId == null) return null;
        return await GetUserByIdAsync(userId.Value);
    }

    protected Guid? GetUserId()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (Guid.TryParse(userId, out var guid))
            return guid;
        return null;
    }

    protected string? GetIdentityNumber() => User.FindFirst("IdentityNumber")?.Value;

    protected string? GetUserRole() => User.FindFirst(ClaimTypes.Role)?.Value;

    protected bool IsOrganizer() => GetUserRole()?.ToLower() == "organizer";
    protected bool IsAdmin() => GetUserRole()?.ToLower() == "admin";
}
