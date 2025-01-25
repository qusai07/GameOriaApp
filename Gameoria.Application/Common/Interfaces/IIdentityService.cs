﻿//IIdentityService: للتعامل مع الهوية والصلاحيات

using Gameoria.Application.Common.Models;

namespace Gameoria.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string?> GetUserNameAsync(string userId);
        Task<bool> IsInRoleAsync(string userId, string role);
        Task<bool> AuthorizeAsync(string userId, string policyName);
        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string email, string password);
        Task<Result> DeleteUserAsync(string userId);
        Task<string?> GetUserIdAsync(string userName);
    }
}
