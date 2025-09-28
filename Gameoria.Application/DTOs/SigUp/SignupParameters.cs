

using GameOria.Domains.Enums;

namespace GameOria.Application.DTOs.SigUp
{
    public class SignupParameters
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public Roles UserRole { get; set; }
    }
}
