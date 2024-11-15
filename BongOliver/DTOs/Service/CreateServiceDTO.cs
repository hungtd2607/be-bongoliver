using BongOliver.DTOs.Category;

namespace BongOliver.DTOs.Service
{
    public class CreateServiceDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public int Duration { get; set; }
        public int CategoryId { get; set; }
    }
}
