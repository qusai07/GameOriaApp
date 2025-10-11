using GameOria.Shared.DTOs.Organizer;
using GameOria.Web.Service.Interface;
using System.Net.Http;

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
        public async Task<HttpResponseMessage> GetMyStore()
        {
            return await _httpClient.GetAsync("Get-Store-Owner-By-Id");
        }
    }
}
