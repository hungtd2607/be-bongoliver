using BongOliver.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BongOliver.Repositories.RoleRepository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DataContext _context;

        public RoleRepository(DataContext context)
        {
            _context = context;
        }

        public void CreateRole(Role createRole)
        {
            _context.Roles.Add(createRole);
        }

        public void DeleteRole(Role role)
        {
            _context.Roles.Remove(role);
        }

        public Role GetRoleById(int id)
        {
            return _context.Roles.FirstOrDefault(r => r.Id == id);
        }

        public List<Role> GetRoles(ref int total, int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID")
        {
            var query = _context.Roles.AsQueryable();

            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(au => au.Name.ToLower().Contains(key.ToLower()));
            }

            query = query.OrderBy(u => u.Id);

            total = query.Count();

            if (page == null || pageSize == null || sortBy == null) { return query.ToList(); }
            else
                return query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value).ToList();
        }

        public void UpdateRole(Role role)
        {
            _context.Entry(role).State = EntityState.Modified;
        }
        public bool IsSaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
        public int GetTotal()
        {
            return _context.Roles.Count();
        }
    }
}
