using DepartmentOfVeterans.Application.Models.Mail;

namespace DepartmentOfVeterans.Application.Contracts.Common
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}
