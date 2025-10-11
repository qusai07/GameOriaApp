using GameOria.Domains.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace GameOria.Domains.Entities.Users
{
    public class OrganizerUser 
    {
        public int ID { get; set; }

        public string StoreName { get; set; } 

        public string IdentityNumber { get; set; } 

        public string Email { get; set; }

        public bool IsVerified { get; set; }

        public DateTime? VerificationDate { get; set; }
    }
}
