using BongOliver.Constants;
using BongOliver.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BongOliver.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public void CreateUser(User user)
        {
            _context.Users.Add(user);
        }

        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
        }

        public int GetTotal()
        {
            return _context.Users.Count();
        }

        public User GetUserById(int id)
        {
            return _context.Users.Include(r => r.Role).FirstOrDefault(u => u.Id == id);
        }
        public User GetUserByUsername(string userName)
        {
            return _context.Users.Include(r => r.Role).FirstOrDefault(u => u.Username == userName);
        }
        public User GetUserByEmail(string email)
        {
            return _context.Users.Include(r => r.Role).FirstOrDefault(u => u.Email == email);
        }

        public List<User> GetUsers(ref int total, int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID")
        {
            var query = _context.Users.Include(r => r.Role).Where(r => r.RoleId != AppConst.ROLE_ADMIN).AsQueryable();

            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(u => (u.FirstName.ToLower() + " " + u.LastName.ToLower()).Contains(key.ToLower()));
            }

            switch (sortBy)
            {
                case "NAME":
                    query = query.OrderBy(u => u.LastName);
                    break;
                default:
                    query = query.OrderBy(u => u.IsDelete).ThenByDescending(u => u.Id);
                    break;
            }

            total = query.Count();

            if (page == null || pageSize == null || sortBy == null) { return query.ToList(); }
            else
                return query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value).ToList();
        }

        public bool IsSaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}
