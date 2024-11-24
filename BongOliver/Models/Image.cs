using System.ComponentModel.DataAnnotations;

namespace BongOliver.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Path { get; set; }
        public DateTime Create { get; set; } = DateTime.Now;
        public DateTime Update { get; set; } = DateTime.Now;
    }
}
