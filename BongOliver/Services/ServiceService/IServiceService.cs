using BongOliver.DTOs.Response;
using BongOliver.DTOs.Service;
using BongOliver.Models;

namespace BongOliver.Services.ServiceService
{
    public interface IServiceService
    {
        ResponseDTO GetServices(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID");
        ResponseDTO GetServiceById(int id);
        ResponseDTO UpdateService(int id, UpdateServiceDTO updateServiceDTO);
        ResponseDTO DeleteService(int id);
        ResponseDTO CreateService(CreateServiceDTO createServiceDTO);
    }
}
