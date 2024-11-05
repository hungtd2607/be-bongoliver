using BongOliver.Models;

namespace BongOliver.DTOs.Service
{
    public class ServiceDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; } = 0;
        public bool IsDelete { get; set; } = false;
        public ServiceDTO(Models.Service service)
        {
            this.Id = service.Id;
            this.Name = service.Name;
            this.Description = service.Description;
            this.Price = service.Price;
            this.IsDelete = service.IsDelete;
        }
    }
}
