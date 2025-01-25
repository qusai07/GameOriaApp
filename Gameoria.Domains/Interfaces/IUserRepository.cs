using Gameoria.Domains.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Gameoria.Domains.Interfaces
{
    public interface IUserRepository 
    {
        Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken = default);

        // User Profile Management
        Task<bool> UpdateUserProfileAsync(
            int userId,
            string firstName,
            string lastName,
            CancellationToken cancellationToken = default);

        // User Status Management
        Task<bool> ActivateUserAsync(int userId, CancellationToken cancellationToken = default);
        Task<bool> DeactivateUserAsync(int userId, CancellationToken cancellationToken = default);

        // Role Management
        Task<bool> AddUserToRoleAsync(int userId, string role, CancellationToken cancellationToken = default);
        Task<bool> RemoveUserFromRoleAsync(int userId, string role, CancellationToken cancellationToken = default);
        Task<IList<string>> GetUserRolesAsync(int userId, CancellationToken cancellationToken = default);
        Task<bool> IsInRoleAsync(int userId, string role, CancellationToken cancellationToken = default);

        // Query Helpers
        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> IsEmailUniqueAsync(string email, int? excludeUserId = null, CancellationToken cancellationToken = default);
        Task<int> CountAsync(Expression<Func<User, bool>>? predicate = null, CancellationToken cancellationToken = default);

        // Audit Information
        Task<IEnumerable<User>> GetRecentlyCreatedUsersAsync(int count, CancellationToken cancellationToken = default);
        Task<IEnumerable<User>> GetRecentlyModifiedUsersAsync(int count, CancellationToken cancellationToken = default);

        // Basic Operations
        Task<User> AddAsync(User user, CancellationToken cancellationToken = default);
        Task UpdateAsync(User user, CancellationToken cancellationToken = default);
        Task DeleteAsync(int userId, CancellationToken cancellationToken = default);
    }
    }
