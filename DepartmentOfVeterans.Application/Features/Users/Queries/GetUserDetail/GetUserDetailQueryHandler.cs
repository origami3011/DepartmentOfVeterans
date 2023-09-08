using AutoMapper;
using DepartmentOfVeterans.Application.Contracts.Persistence;
using DepartmentOfVeterans.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentOfVeterans.Application.Features.Users.Queries.GetUserDetail
{
    public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, UserDetailVM>
    {
        private readonly IAsyncRepository<User> _userRepository;    // using dependency Injection
        private readonly IMapper _mapper;

        public GetUserDetailQueryHandler(IMapper mapper, IAsyncRepository<User> userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UserDetailVM> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
        {
            var @user = await _userRepository.GetByIdAsync(request.Id);
            var userDetailDto = _mapper.Map<UserDetailVM>(@user);

            return userDetailDto;
        }
    }
}
