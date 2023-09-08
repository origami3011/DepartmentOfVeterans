using AutoMapper;
using DepartmentOfVeterans.Application.Contracts.Persistence;
using DepartmentOfVeterans.Application.Exceptions;
using DepartmentOfVeterans.Domain.Entities;
using MediatR;

namespace DepartmentOfVeterans.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IAsyncRepository<User> _UserRepository;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IMapper mapper, IAsyncRepository<User> UserRepository)
        {
            _mapper = mapper;
            _UserRepository = UserRepository;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {

            var UserToUpdate = await _UserRepository.GetByIdAsync(request.UserId);
            if (UserToUpdate == null)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }

            var validator = new UpdateUserCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            _mapper.Map(request, UserToUpdate, typeof(UpdateUserCommand), typeof(User));

            await _UserRepository.UpdateAsync(UserToUpdate);

            return Unit.Value;
        }
    }
}
