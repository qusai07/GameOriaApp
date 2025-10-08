using GameOria.Admin.ViewModels;
using GameOria.Shared.DTOs.Organizer;
using GameOria.Shared.Response;
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
            return await _httpClient.GetFromJsonAsync<List<UserDto>>("Get-All-Users");
        }

        public async Task ToggleUserActiveAsync(UserDto user)
        {
            var response = await _httpClient.PutAsJsonAsync($"ToggleActive/{user.EmailAddress}", user);
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<OrganizerTables>> GetAllOrganizerRequestsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<OrganizerTables>>("get-All-Organizers");
        }

        public async Task<APIResponse> ApproveOrganizerAsync(Guid userId)
        {
            var response = await _httpClient.PostAsync($"approve/{userId}", null);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<APIResponse>();
        }


    }
}
