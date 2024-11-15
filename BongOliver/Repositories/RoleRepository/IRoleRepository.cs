using BongOliver.DTOs.Response;
using BongOliver.DTOs.Role;
using BongOliver.Models;

namespace BongOliver.Repositories.RoleRepository
{
    public interface IRoleRepository
    {
        List<Role> GetRoles(ref int total, int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID");
        Role GetRoleById(int id);
        void UpdateRole(Role role);
        void DeleteRole(Role role);
        void CreateRole(Role createRole);
        int GetTotal();
        bool IsSaveChanges();
    }
}
