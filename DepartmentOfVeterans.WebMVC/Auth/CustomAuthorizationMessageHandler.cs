using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace DepartmentOfVeterans.WebMVC.Auth
{
    public class CustomAuthorizationMessageHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomAuthorizationMessageHandler(IHttpContextAccessor httpContextAccessor) //(ILocalStorageService localStorageService)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var savedToken = _httpContextAccessor.HttpContext.Request.Cookies["X-Access-Token"];
            if (!string.IsNullOrEmpty(savedToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
