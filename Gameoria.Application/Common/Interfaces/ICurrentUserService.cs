//ICurrentUserService: للحصول على معلومات المستخدم الحالي

namespace GameOria.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string? UserId { get; }
        string? UserName { get; }
        string? Email { get; }
        bool IsAuthenticated { get; }
        IList<string> Roles { get; }
        bool IsInRole(string role);
    }
}
