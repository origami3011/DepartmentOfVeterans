using AutoMapper;
using DepartmentOfVeterans.Application.Contracts.Persistence;
using DepartmentOfVeterans.Application.Exceptions;
using DepartmentOfVeterans.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentOfVeterans.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IAsyncRepository<User> _UserRepository;
        private readonly IMapper _mapper;

        public DeleteUserCommandHandler(IMapper mapper, IAsyncRepository<User> UserRepository)
        {
            _mapper = mapper;
            _UserRepository = UserRepository;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var UserToDelete = await _UserRepository.GetByIdAsync(request.UserId);

            if (UserToDelete == null)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }

            await _UserRepository.DeleteAsync(UserToDelete);

            return Unit.Value;
        }
    }
}
