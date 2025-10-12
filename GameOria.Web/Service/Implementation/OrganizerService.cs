using GameOria.Application.Stores.DTOs;
using GameOria.Shared.DTOs.Organizer;
using GameOria.Shared.Response;
using GameOria.Web.Service.Interface;
using System.Net.Http;
using System.Text.Json;

namespace GameOria.Web.Service.Implementation
{
    public class OrganizerService : IOrganizerService
    {
        private readonly HttpClient _httpClient;

        public OrganizerService(HttpClient httpClient )
        {
            _httpClient = httpClient;
        }
        public async Task<HttpResponseMessage> RequestBecomeOrganizer(OrganizerRequestDto organizerRequestDto)
        {
            return await _httpClient.PostAsJsonAsync("Become-organizer-requests", organizerRequestDto);
        }
        public async Task<HttpResponseMessage> GetMyStatusRequest()
        {
            return await _httpClient.GetAsync("Get-My-Status-Request");
        }
        public async Task<StorOrganizerDto?> GetMyStoreAsync()
        {
            var response = await _httpClient.GetAsync("Get-My-Store");

            if (!response.IsSuccessStatusCode)
                return null;

            var apiResponse = await response.Content.ReadFromJsonAsync<APIResponse>();

            if (apiResponse == null || !apiResponse.Success || apiResponse.Data == null)
                return null;

            return JsonSerializer.Deserialize<StorOrganizerDto>(apiResponse.Data.ToString());
        }


    }
}
