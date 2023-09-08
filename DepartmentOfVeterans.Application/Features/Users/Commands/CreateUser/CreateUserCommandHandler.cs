using AutoMapper;
using DepartmentOfVeterans.Application.Contracts.Common;
using DepartmentOfVeterans.Application.Contracts.Persistence;
using DepartmentOfVeterans.Application.Models.Mail;
using DepartmentOfVeterans.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentOfVeterans.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateUserCommandHandler> _logger;
        private readonly IEmailService _emailService;

        public CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository, ILogger<CreateUserCommandHandler> logger, IEmailService emailService)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _logger = logger;
            _emailService = emailService;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateUserCommandValidator(_userRepository);
            var result = await validator.ValidateAsync(request);

            if (result.Errors.Count > 0)
                throw new Exceptions.ValidationException(result);

            var @user = _mapper.Map<User>(request);

            @user = await _userRepository.AddAsync(@user);

            //  sending email notification to admin address
            var email = new Email()
            {
                To = "admin@example.com",
                Body = $"A new user was created: {request}",
                Subject = "A new user was created"
            };

            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                //this shouldn't stop the API from doing else so this can be logged
                _logger.LogError($"Mailing about user {@user.UserName} failed due to an error with the mail service: {ex.Message}");
            }

            return @user.UserId;
        }
    }
}
