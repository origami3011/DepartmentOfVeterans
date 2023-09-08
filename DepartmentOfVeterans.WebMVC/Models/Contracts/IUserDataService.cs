using DepartmentOfVeterans.WebMVC.Services.Base;

namespace DepartmentOfVeterans.WebMVC.Models.Contracts
{
    public interface IUserDataService
    {
        Task<List<UserViewModel>> GetAllUsers();
        Task<UserViewModel> GetUserById(Guid id);
        Task<ApiResponse<Guid>> CreateUser(UserViewModel UserDetailViewModel);
        Task<ApiResponse<Guid>> UpdateUser(UserViewModel UserDetailViewModel);
        Task<ApiResponse<Guid>> DeleteUser(Guid id);
    }
}
