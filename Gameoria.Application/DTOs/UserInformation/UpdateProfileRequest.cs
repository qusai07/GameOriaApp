namespace GameOria.Application.DTOs.UserInformation
{
    public class UpdateProfileRequest
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
    }
}
