using GameOria.Domains.Entities.Stores;
using GameOria.Domains.Enums;

namespace GameOria.Domains.Entities.Identity
{
    public class ApplicationUser
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string MobileNumber { get; set; }
        public string? IdentityNumber { get; set; }
        public string EmailAddress { get; set; }
        public string PasswordHash { get; set; }
        public string OtpCode { get; set; }
        public DateTime? OtpDate { get; set; }
        public bool IsActive { get; set; }
        public Roles Role { get; set; }
    }
    public class AdminUser : ApplicationUser{}
}
