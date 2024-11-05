using BongOliver.Models;

namespace BongOliver.Repositories.UserRepository
{
    public interface IUserRepository
    {
        List<User> GetUsers(ref int total, int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID");
        User GetUserById(int id);
        User GetUserByUsername(string userName);
        User GetUserByEmail(string email);
        void UpdateUser(User user);
        void DeleteUser(User user);
        void CreateUser(User user);
        int GetTotal();
        bool IsSaveChanges();
    }
}
