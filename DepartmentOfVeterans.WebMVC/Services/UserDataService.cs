using AutoMapper;
using DepartmentOfVeterans.App.Services;
using DepartmentOfVeterans.WebMVC.Models;
using DepartmentOfVeterans.WebMVC.Models.Contracts;
using DepartmentOfVeterans.WebMVC.Services.Base;

namespace DepartmentOfVeterans.WebMVC.Services
{
    public class UserDataService : BaseDataService, IUserDataService
    {
        private readonly IMapper _mapper;

        public UserDataService(IClient client, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(client, httpContextAccessor)
        {
            _mapper = mapper;
        }

        public async Task<List<UserViewModel>> GetAllUsers()
        {
            AddBearerToken();

            var UserUsers = await _client.GetAllUsersAsync();
            var mappedUsers = _mapper.Map<ICollection<UserViewModel>>(UserUsers);
            return mappedUsers.ToList();
        }

        public async Task<UserViewModel> GetUserById(Guid id)
        {
            var selectedUser = await _client.GetUserByIdAsync(id);
            var mappedUser = _mapper.Map<UserViewModel>(selectedUser);
            return mappedUser;
        }

        public async Task<ApiResponse<Guid>> CreateUser(UserViewModel UserViewModel)
        {
            try
            {
                CreateUserCommand createUserCommand = _mapper.Map<CreateUserCommand>(UserViewModel);
                var newId = await _client.CreateUserAsync(createUserCommand);
                return new ApiResponse<Guid>() { Data = newId, Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<Guid>(ex);
            }
        }

        public async Task<ApiResponse<Guid>> UpdateUser(UserViewModel UserViewModel)
        {
            try
            {
                UpdateUserCommand updateUserCommand = _mapper.Map<UpdateUserCommand>(UserViewModel);
                await _client.UpdateUserAsync(updateUserCommand);
                return new ApiResponse<Guid>() { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<Guid>(ex);
            }
        }

        public async Task<ApiResponse<Guid>> DeleteUser(Guid id)
        {
            try
            {
                await _client.DeleteUserAsync(id);
                return new ApiResponse<Guid>() { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<Guid>(ex);
            }
        }
    }
}
