using BongOliver.DTOs.Response;
using BongOliver.DTOs.Role;

namespace BongOliver.Services.RoleService
{
    public interface IRoleService
    {
        ResponseDTO GetRoles(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID");
        ResponseDTO GetRoleById(int id);
        ResponseDTO UpdateRole(int id, string nameRole);
        ResponseDTO DeleteRole(int id);
        ResponseDTO CreateRole(string name);
    }
}
