using BongOliver.Constants;
using BongOliver.DTOs.Response;
using BongOliver.DTOs.Role;
using BongOliver.DTOs.Service;
using BongOliver.Models;
using BongOliver.Repositories.CategoryRepository;
using BongOliver.Repositories.RoleRepository;
using BongOliver.Repositories.ServiceRepository;
using System.Data;

namespace BongOliver.Services.ServiceService
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly ICategoryRepository _categoryRepository;
        public ServiceService(IServiceRepository serviceRepository, ICategoryRepository categoryRepository)
        {
            _serviceRepository = serviceRepository;
            _categoryRepository = categoryRepository;
        }
        public ResponseDTO CreateService(CreateServiceDTO createServiceDTO)
        {
            var category = _categoryRepository.GetCategoryById(createServiceDTO.CategoryId);
            if (category == null) return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Category không tồn tại" };

            var service = new Service
            {
                Name = createServiceDTO.Name,
                Description = createServiceDTO.Description,
                Image = createServiceDTO.Image,
                Price = createServiceDTO.Price,
                Duration = createServiceDTO.Duration,
                CategoryId = createServiceDTO.CategoryId
            };

            _serviceRepository.CreateService(service);

            if (_serviceRepository.IsSaveChanges()) return new ResponseDTO() { Message = "Tạo thành công" };
            else return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Tạo thất bại" };
        }

        public ResponseDTO DeleteService(int id)
        {
            var service = _serviceRepository.GetServiceById(id);
            if (service == null) return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Service không tồn tại" };

            service.IsDelete = AppConst.DELETE;
            service.Update = DateTime.Now;

            _serviceRepository.UpdateService(service);
            if (_serviceRepository.IsSaveChanges())
                return new ResponseDTO() { Message = "Cập nhật thành công" };
            else
                return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Cập nhật thất bại" };
        }

        public ResponseDTO GetServiceById(int id)
        {
            var service = _serviceRepository.GetServiceById(id);
            if (service == null) return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Service không tồn tại" };
            else
                return new ResponseDTO() { Data = new ServiceDTO(service) };
        }

        public ResponseDTO GetServices(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID")
        {
            var total = _serviceRepository.GetTotal();

            if (total == 0) return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Không có Service nào" };

            var services = _serviceRepository.GetServices(ref total, page, pageSize, key, sortBy);

            return new ResponseDTO()
            {
                Data = services.Select(s => new ServiceDTO(s)).ToList(),
                Total = total
            };
        }

        public ResponseDTO UpdateService(int id, UpdateServiceDTO updateServiceDTO)
        {
            var service = _serviceRepository.GetServiceById(id);
            if (service == null) return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Service không tồn tại" };

            var category = _categoryRepository.GetCategoryById(updateServiceDTO.CategoryId);
            if (category == null) return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Category không tồn tại" };

            service.Name = updateServiceDTO.Name;
            service.Description = updateServiceDTO.Description;
            service.Image = updateServiceDTO.Image;
            service.Price = updateServiceDTO.Price;
            service.Duration = updateServiceDTO.Duration;
            service.CategoryId = updateServiceDTO.CategoryId;
            service.Update = DateTime.Now;

            _serviceRepository.UpdateService(service);
            if (_serviceRepository.IsSaveChanges())
                return new ResponseDTO() { Message = "Cập nhật thành công" };
            else
                return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Cập nhật thất bại" };
        }
    }
}
