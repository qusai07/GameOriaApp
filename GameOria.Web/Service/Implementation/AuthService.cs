using GameOria.Application.DTOs.Login;
using GameOria.Shared.DTOs.SigUp;
using GameOria.Shared.ViewModels;
using GameOria.Web.Service.Interface;


namespace GameOria.Web.Service.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient )
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<HttpResponseMessage> SignUpAsync(SignupParameters parameters)
        {
            return await _httpClient.PostAsJsonAsync("SignUp", parameters);
        }

        public async Task<HttpResponseMessage> SignupVerifyOtpAsync(SignupVerifyOtpParameters parameters)
        {
            return await _httpClient.PostAsJsonAsync("SignupVerifyOtp", parameters);
        }

        public async Task<HttpResponseMessage> SignupResendOtpAsync(SignupUserParameters parameters)
        {
            return await _httpClient.PostAsJsonAsync("SignupResendOtp", parameters);
        }

        public async Task<HttpResponseMessage> LoginAsync(LoginParameters parameters)
        {
            return await _httpClient.PostAsJsonAsync("Login", parameters);
        }

        public async Task<ProfileViewModel?> GetProfileAsync()
        {
            return await _httpClient.GetFromJsonAsync<ProfileViewModel>("GetProfile");
        }

        public async Task<HttpResponseMessage> UpdateProfileAsync(ProfileViewModel request)
        {
            return await _httpClient.PostAsJsonAsync("UpdateProfile", request);
        }

        public async Task<HttpResponseMessage> VerifyOtpAsync(Guid id, string otp)
        {
            var request = new { Id = id, OtpCode = otp };
            return await _httpClient.PostAsJsonAsync("SignupVerifyOtp", request);
        }
        public async Task<HttpResponseMessage> ResendOtpAsync(Guid id)
        {
            var request = new { Id = id};
            return await _httpClient.PostAsJsonAsync("SignupResendOtp", request);
        }

        public async Task<HttpResponseMessage> LogoutAsync()
        {
            return await _httpClient.PostAsync("Logout", null);
        }
    }

}
