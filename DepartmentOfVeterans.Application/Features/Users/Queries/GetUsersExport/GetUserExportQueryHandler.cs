using AutoMapper;
using DepartmentOfVeterans.Application.Contracts.Common;
using DepartmentOfVeterans.Application.Contracts.Persistence;
using DepartmentOfVeterans.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentOfVeterans.Application.Features.Users.Queries.GetUsersExport
{
    public class GetUsersExportQueryHandler : IRequestHandler<GetUsersExportQuery, UserExportFileVm>
    {
        private readonly IAsyncRepository<User> _UserRepository;
        private readonly IMapper _mapper;
        private readonly ICsvExporter _csvExporter;

        public GetUsersExportQueryHandler(IMapper mapper, IAsyncRepository<User> UserRepository, ICsvExporter csvExporter)
        {
            _mapper = mapper;
            _UserRepository = UserRepository;
            _csvExporter = csvExporter;
        }

        public async Task<UserExportFileVm> Handle(GetUsersExportQuery request, CancellationToken cancellationToken)
        {
            var allUsers = _mapper.Map<List<UserExportDto>>((await _UserRepository.ListAllAsync()).OrderBy(x => x.CreatedDate));

            var fileData = _csvExporter.ExportUsersToCsv(allUsers);

            var UserExportFileDto = new UserExportFileVm() { ContentType = "text/csv", Data = fileData, UserExportFileName = $"{Guid.NewGuid()}.csv" };

            return UserExportFileDto;
        }
    }
}
