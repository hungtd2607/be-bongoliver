using BongOliver.DTOs.Response;
using BongOliver.DTOs.User;
using BongOliver.Models;

namespace BongOliver.Services.UserService
{
    public interface IUserService
    {
        ResponseDTO GetUsers(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID");
        ResponseDTO GetUserById(int id);
        ResponseDTO UpdateUser(int id, UpdateUserDTO updateUserDTO);
        ResponseDTO DeleteUser(int id);
        ResponseDTO CreateUser(CreateUserDTO createUserDTO);
    }
}
