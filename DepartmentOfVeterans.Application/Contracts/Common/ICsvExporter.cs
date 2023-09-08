using DepartmentOfVeterans.Application.Features.Users.Queries.GetUsersExport;

namespace DepartmentOfVeterans.Application.Contracts.Common
{
    public interface ICsvExporter
    {
        byte[] ExportUsersToCsv(List<UserExportDto> eventExportDtos);
    }
}
