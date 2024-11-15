using BongOliver.DTOs.Category;
using BongOliver.Models;

namespace BongOliver.DTOs.Service
{
    public class ServiceDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public int Duration { get; set; }
        public bool IsDelete { get; set; }
        public CategoryDTO Category { get; set; }
        public ServiceDTO(Models.Service service)
        {
            this.Id = service.Id;
            this.Name = service.Name;
            this.Description = service.Description;
            this.Image = service.Image;
            this.Price = service.Price;
            this.Duration = service.Duration;
            this.IsDelete = service.IsDelete;
            this.Category = new CategoryDTO(service.Category);
        }
    }
}
