using MediatR;

namespace DepartmentOfVeterans.Application.Features.Users.Queries.GetUsersExport
{
    public class GetUsersExportQuery : IRequest<UserExportFileVm>
    {
    }
}
