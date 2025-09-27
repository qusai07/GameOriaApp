using GameOria.Domains.Entities.Identity;

using System.Linq.Expressions;


namespace GameOria.Domains.Interfaces
{
    public interface IUserRepository 
    {
        Task<ApplicationUser?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<ApplicationUser>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ApplicationUser?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<IEnumerable<ApplicationUser>> FindAsync(Expression<Func<ApplicationUser, bool>> predicate, CancellationToken cancellationToken = default);

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
        Task<int> CountAsync(Expression<Func<ApplicationUser, bool>>? predicate = null, CancellationToken cancellationToken = default);

        // Audit Information
        Task<IEnumerable<ApplicationUser>> GetRecentlyCreatedUsersAsync(int count, CancellationToken cancellationToken = default);
        Task<IEnumerable<ApplicationUser>> GetRecentlyModifiedUsersAsync(int count, CancellationToken cancellationToken = default);

        // Basic Operations
        Task<ApplicationUser> AddAsync(ApplicationUser user, CancellationToken cancellationToken = default);
        Task UpdateAsync(ApplicationUser user, CancellationToken cancellationToken = default);
        Task DeleteAsync(int userId, CancellationToken cancellationToken = default);
    }
    }
