namespace GameOria.Api.DTO.SigUp
{
    public class SignupVerifyOtpParameters
    {
        public Guid Id { get; set; }
        public string OtpCode { get; set; }
    }
}
