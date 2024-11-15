using BongOliver.Constants;
using System.ComponentModel.DataAnnotations;

namespace BongOliver.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsDelete { get; set; } = AppConst.NOT_DELETE;
        public DateTime Create { get; set; } = DateTime.Now;
        public DateTime Update { get; set; } = DateTime.Now;
        public ICollection<Service> Services { get; set; }
    }
}
