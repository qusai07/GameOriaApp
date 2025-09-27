using GameOria.Domains.Common;
using GameOria.Domains.Entities.Identity;


namespace GameOria.Domains.Entities.Users
{
    public class OrganizerUser : ApplicationUser
    {
        public string StoreName { get; set; } = string.Empty;

        public string BusinessEmail { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string IdentityNumber { get; set; } = string.Empty;

        public bool IsVerified { get; set; }

        public DateTime? VerificationDate { get; set; }
    }
}
