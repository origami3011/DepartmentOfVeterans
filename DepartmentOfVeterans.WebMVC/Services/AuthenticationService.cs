using DepartmentOfVeterans.App.Services;
using DepartmentOfVeterans.WebMVC.Auth;
using DepartmentOfVeterans.WebMVC.Models.Contracts;
using DepartmentOfVeterans.WebMVC.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;

namespace DepartmentOfVeterans.WebMVC.Services
{
    public class AuthenticationService : BaseDataService, IAuthenticationService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthenticationService(IClient client, AuthenticationStateProvider authenticationStateProvider, IHttpContextAccessor httpContextAccessor) : base(client, httpContextAccessor)
        {
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<string> Authenticate(string email, string password)
        {
            try
            {
                AuthenticationRequest authenticationRequest = new AuthenticationRequest() { Email = email, Password = password };
                var authenticationResponse = await _client.AuthenticateAsync(authenticationRequest);

                if (authenticationResponse.Token != string.Empty)
                {
                    ((CustomAuthenticationStateProvider)_authenticationStateProvider).SetUserAuthenticated(email);
                    _client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authenticationResponse.Token);                    
                }
                return authenticationResponse.Token;
            }
            catch
            {
                return string.Empty;
            }
        }

        public async Task<RegistrationResponse> Register(string firstName, string lastName, string userName, string email, string password)
        {
            RegistrationRequest registrationRequest = new RegistrationRequest() { FirstName = firstName, LastName = lastName, Email = email, UserName = userName, Password = password };
            var response = await _client.RegisterAsync(registrationRequest);

            return response;
        }

        public async Task Logout()
        {
            ((CustomAuthenticationStateProvider)_authenticationStateProvider).SetUserLoggedOut();
            _client.HttpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
