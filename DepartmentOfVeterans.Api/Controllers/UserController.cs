using DepartmentOfVeterans.Api.Utility;
using DepartmentOfVeterans.Application.Features.Users.Commands.CreateUser;
using DepartmentOfVeterans.Application.Features.Users.Commands.DeleteUser;
using DepartmentOfVeterans.Application.Features.Users.Commands.UpdateUser;
using DepartmentOfVeterans.Application.Features.Users.Queries.GetUserDetail;
using DepartmentOfVeterans.Application.Features.Users.Queries.GetUserList;
using DepartmentOfVeterans.Application.Features.Users.Queries.GetUsersExport;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentOfVeterans.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllUsers", Name = "GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<UsersListVM>>> GetAllUsers()
        {
            var dtos = await _mediator.Send(new GetUsersListQuery());
            return Ok(dtos);
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<ActionResult<UserDetailVM>> GetUserById(Guid id)
        {
            var getUserDetailQuery = new GetUserDetailQuery() { Id = id };
            return Ok(await _mediator.Send(getUserDetailQuery));
        }

        [Authorize]
        [HttpPost("CreateUser", Name = "CreateUser")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateUserCommand createUserCommand)
        {
            createUserCommand.UserId = Guid.NewGuid();  // generate UserID 
            var id = await _mediator.Send(createUserCommand);
            return Ok(id);
        }

        [Authorize]
        [HttpPut("UpdateUser", Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateUserCommand updateUserCommand)
        {
            await _mediator.Send(updateUserCommand);
            return NoContent();
        }

        [HttpDelete("DeleteUser/{id}", Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deleteUserCommand = new DeleteUserCommand() { UserId = id };
            await _mediator.Send(deleteUserCommand);
            return NoContent();
        }

        [Authorize]
        [HttpGet("export", Name = "ExportUsers")]
        [FileResultContentType("text/csv")]
        public async Task<FileResult> ExportUsers()
        {
            var fileDto = await _mediator.Send(new GetUsersExportQuery());

            return File(fileDto.Data, fileDto.ContentType, fileDto.UserExportFileName);
        }
    }
}
