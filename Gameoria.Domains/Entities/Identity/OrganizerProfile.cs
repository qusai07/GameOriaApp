using Gameoria.Domains.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameoria.Domains.Entities.Users
{
    public class OrganizerProfile : BaseAuditableEntity
    {
        public string UserId { get; set; } // Changed to string because IdentityUser uses string for Id
        public virtual User.User User { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string BusinessEmail { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public bool IsVerified { get; set; }
        public DateTime? VerificationDate { get; set; }
    }
}
