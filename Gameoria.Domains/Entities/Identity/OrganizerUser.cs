using GameOria.Domains.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace GameOria.Domains.Entities.Users
{
    public class OrganizerUser : ApplicationUser
    {
        public Guid UserId { get; set; }
        public string StoreName { get; set; } 

        public string? BusinessEmail { get; set; } 

        public string? PhoneNumber { get; set; } 

        public string IdentityNumber { get; set; }

        public bool IsVerified { get; set; }

        public DateTime? VerificationDate { get; set; }
    }
}
