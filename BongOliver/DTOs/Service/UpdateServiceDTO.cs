namespace BongOliver.DTOs.Service
{
    public class UpdateServiceDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public int Duration { get; set; }
        public int CategoryId { get; set; }
    }
}
