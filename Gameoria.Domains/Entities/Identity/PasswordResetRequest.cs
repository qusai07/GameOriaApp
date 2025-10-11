namespace GameOria.Domains.Entities.Identity
{
    public class PasswordResetRequest
    {
        public int Id { get; set; } 
        public Guid UserId { get; set; }
        public string ResetCode { get; set; } = null!;
        public DateTime ExpiryDate { get; set; }
        public bool IsUsed { get; set; } = false;

        public ApplicationUser User { get; set; } = null!;
    }
}
