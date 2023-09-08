using DepartmentOfVeterans.App.Services;

namespace DepartmentOfVeterans.WebMVC.Models.Contracts
{
    public interface IAuthenticationService
    {
        Task<string> Authenticate(string email, string password);
        Task<RegistrationResponse> Register(string firstName, string lastName, string userName, string email, string password);
        Task Logout();
    }
}
