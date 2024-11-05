using BongOliver.Constants;
using BongOliver.DTOs.Role;
using BongOliver.Models;
using BongOliver.Services.RoleService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Globalization;

namespace BongOliver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Constant.ROLE_NAME_ADMIN)]
    public class RoleController : ControllerBase
    {
        private IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public IActionResult GetRoles(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID")
        {
            var res = _roleService.GetRoles(page, pageSize, key, sortBy);
            return StatusCode(res.Code, res);
        }
        [HttpPost]
        public IActionResult CreateRole(string name)
        {
            var res = _roleService.CreateRole(name);
            return StatusCode(res.Code, res);
        }
        [HttpGet("{id}")]
        public IActionResult GetRoleById(int id)
        {
            var res = _roleService.GetRoleById(id);
            return StatusCode(res.Code, res);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateRole(int id, string name)
        {
            var res = _roleService.UpdateRole(id, name);
            return StatusCode(res.Code, res);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteRole(int id)
        {
            var res = _roleService.DeleteRole(id);
            return StatusCode(res.Code, res);
        }
    }
}
