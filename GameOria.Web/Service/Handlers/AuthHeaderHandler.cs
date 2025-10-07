using GameOria.Shared.Response;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace GameOria.Web.Service.Handlers
{
    public class AuthHeaderHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthHeaderHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("AuthToken");
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
        public async Task<APIResponse> HandleApiResponse(HttpResponseMessage response)
        {
            if (response == null) return null;

            var content = await response.Content.ReadAsStringAsync();
            try
            {
                return JsonConvert.DeserializeObject<APIResponse>(content);
            }
            catch
            {
                return new APIResponse { Success = false, Message = "Unexpected server response." };
            }
        }

    }

}
