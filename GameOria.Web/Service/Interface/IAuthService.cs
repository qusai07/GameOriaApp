using GameOria.Application.DTOs.Login;
using GameOria.Shared.DTOs.SigUp;
using GameOria.Shared.ViewModels;

namespace GameOria.Web.Service.Interface
{
    public interface IAuthService
    {
        Task<HttpResponseMessage> SignUpAsync(SignupParameters parameters);
        Task<HttpResponseMessage> SignupVerifyOtpAsync(SignupVerifyOtpParameters parameters);
        Task<HttpResponseMessage> SignupResendOtpAsync(SignupUserParameters parameters);
        Task<HttpResponseMessage> LoginAsync(LoginParameters parameters);
        Task<ProfileViewModel?> GetProfileAsync();
        Task<HttpResponseMessage> UpdateProfileAsync(ProfileViewModel request);
        Task<HttpResponseMessage> VerifyOtpAsync(Guid id, string otp);
        Task<HttpResponseMessage> ResendOtpAsync(Guid id);


    }
}
