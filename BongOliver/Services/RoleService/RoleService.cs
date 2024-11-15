using BongOliver.Constants;
using BongOliver.DTOs.Response;
using BongOliver.DTOs.Role;
using BongOliver.Models;
using BongOliver.Repositories.RoleRepository;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;

namespace BongOliver.Services.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public ResponseDTO CreateRole(string name)
        {
            var role = new Role { Name = name };
            _roleRepository.CreateRole(role);

            if (_roleRepository.IsSaveChanges()) return new ResponseDTO() { Message = "Tạo thành công" };
            else return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Tạo thất bại" };
        }

        public ResponseDTO DeleteRole(int id)
        {
            var role = _roleRepository.GetRoleById(id);
            if (role == null) return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Role không tồn tại" };

            //TODO
            //role.Is = nameRole;
            role.Update = DateTime.Now;

            _roleRepository.UpdateRole(role);
            if (_roleRepository.IsSaveChanges())
                return new ResponseDTO() { Message = "Cập nhật thành công" };
            else
                return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Cập nhật thất bại" };
        }

        public ResponseDTO GetRoleById(int id)
        {
            var role = _roleRepository.GetRoleById(id);

            if (role == null) return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Role không tồn tại" };
            else
                return new ResponseDTO() { Data = new RoleDTO(role) };
        }

        public ResponseDTO GetRoles(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID")
        {
            var total = _roleRepository.GetTotal();

            if (total == 0) return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Không có Role nào" };

            var roles = _roleRepository.GetRoles(ref total, page, pageSize, key, sortBy);

            return new ResponseDTO()
            {
                Data = roles.Select(r => new RoleDTO(r)).ToList(),
                Total = total
            };
        }

        public ResponseDTO UpdateRole(int id, string nameRole)
        {
            var role = _roleRepository.GetRoleById(id);
            if (role == null) return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Role không tồn tại" };
            
            role.Name = nameRole;
            role.Update = DateTime.Now;

            _roleRepository.UpdateRole(role);
            if (_roleRepository.IsSaveChanges())
                return new ResponseDTO() { Message = "Cập nhật thành công" };
            else
                return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Cập nhật thất bại" };
        }
    }
}
