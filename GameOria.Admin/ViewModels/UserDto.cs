using GameOria.Domains.Enums;

namespace GameOria.Admin.ViewModels
{
    public class UserDto
    {
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public Roles Role { get; set; }
        public bool IsActive { get; set; }
    }
}
