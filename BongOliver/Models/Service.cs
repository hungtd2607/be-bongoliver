using BongOliver.Constants;
using System.ComponentModel.DataAnnotations;

namespace BongOliver.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(256)]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; } = 0;
        public bool IsDelete { get; set; } = Constant.NOT_DELETE;
        public DateTime Create { get; set; } = DateTime.Now;
        public DateTime Update { get; set; } = DateTime.Now;
    }
}
