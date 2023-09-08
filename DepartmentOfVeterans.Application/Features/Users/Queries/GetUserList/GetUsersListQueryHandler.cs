using AutoMapper;
using DepartmentOfVeterans.Application.Contracts.Persistence;
using DepartmentOfVeterans.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentOfVeterans.Application.Features.Users.Queries.GetUserList
{
    public class GetUsersListQueryHandler : IRequestHandler<GetUsersListQuery, List<UsersListVM>>
    {
        private readonly IAsyncRepository<User> _userRepository;    // using dependency Injection
        private readonly IMapper _mapper;

        public GetUsersListQueryHandler(IMapper mapper, IAsyncRepository<User> userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<List<UsersListVM>> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
        {
            var allUsers = (await _userRepository.ListAllAsync()).OrderBy(x => x.CreatedDate);
            return _mapper.Map<List<UsersListVM>>(allUsers);
        }
    }
}
