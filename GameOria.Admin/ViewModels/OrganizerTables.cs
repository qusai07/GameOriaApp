namespace GameOria.Admin.ViewModels
{
    public class OrganizerTables
    {
        public Guid UserId { get; set; }
        public string StoreName { get; set; }
        public string? BusinessEmail { get; set; }
        public string IdentityNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsVerified { get; set; } = false;
        public DateTime RequestDate { get; set; } = DateTime.UtcNow;
    }
}
