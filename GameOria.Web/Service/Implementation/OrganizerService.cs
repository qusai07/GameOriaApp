using GameOria.Shared.DTOs.Organizer;
using GameOria.Web.Service.Interface;
using System.Net.Http;

namespace GameOria.Web.Service.Implementation
{
    public class OrganizerService : IOrganizerService
    {
        private readonly HttpClient _httpClient;

        public OrganizerService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }
        public async Task<HttpResponseMessage> RequestBecomeOrganizer(OrganizerRequestDto organizerRequestDto)
        {
            return await _httpClient.PostAsJsonAsync("api/OrganizerRequests", organizerRequestDto);
        }
    }
}
