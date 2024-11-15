using BongOliver.DTOs.Service;
using BongOliver.Services.ServiceService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BongOliver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet]
        public IActionResult GetServices(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID")
        {
            var res = _serviceService.GetServices(page, pageSize, key, sortBy);
            return StatusCode(res.Code, res);
        }
        [HttpPost]
        public IActionResult CreateService(CreateServiceDTO createServiceDTO)
        {
            var res = _serviceService.CreateService(createServiceDTO);
            return StatusCode(res.Code, res);
        }
        [HttpGet("{id}")]
        public IActionResult GetServiceById(int id)
        {
            var res = _serviceService.GetServiceById(id);
            return StatusCode(res.Code, res);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateService(int id, UpdateServiceDTO updateServiceDTO)
        {
            var res = _serviceService.UpdateService(id, updateServiceDTO);
            return StatusCode(res.Code, res);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteService(int id)
        {
            var res = _serviceService.DeleteService(id);
            return StatusCode(res.Code, res);
        }
    }
}
