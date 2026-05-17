using Application.Services.UserService.DTOs;

namespace Application.Services.UserService
{
    public interface IUserService
    {
        Task CreateUser(CreateUserDto input);
        Task<List<GetAllUsersDto>> GetAllUsers(string? name, string? email);
        Task<GetUserDto> GetUserById(Guid id);
        Task UpdateUser(Guid id, UpdateUserDto input);
        Task DeleteUser(Guid id);
    }
}
