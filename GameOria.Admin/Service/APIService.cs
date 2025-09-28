using GameOria.Admin.ViewModels;
using System.Net.Http;
using System.Net.Http.Json;

namespace GameOria.Admin.Services
{
    public class APIService
    {
        private readonly HttpClient _httpClient;

        public APIService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("GameOriaApi");
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<UserDto>>("get-All-Customers");
        }

        public async Task ToggleUserActiveAsync(UserDto user)
        {
            // مثال request لتحديث حالة المستخدم
            var response = await _httpClient.PutAsJsonAsync($"ToggleActive/{user.EmailAddress}", user);
            response.EnsureSuccessStatusCode();
        }

        // Add, Edit, Delete methods يمكن إضافتها بنفس الأسلوب
    }
}
