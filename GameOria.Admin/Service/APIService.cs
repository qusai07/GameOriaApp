using GameOria.Admin.ViewModels;
using GameOria.Shared.DTOs.Organizer;
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
            var response = await _httpClient.PutAsJsonAsync($"ToggleActive/{user.EmailAddress}", user);
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<OrganizerRequestDto>> GetAllOrganizerRequestsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<OrganizerRequestDto>>("get-All-Organizers");
        }
        public async Task ApproveOrganizerAsync(string userId)
        {
            var response = await _httpClient.PutAsync($"approve/{userId}", null);
            response.EnsureSuccessStatusCode();
        }

    }
}
