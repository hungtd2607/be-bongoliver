using BongOliver.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BongOliver.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        public string Comment { get; set; } = string.Empty;
        public int Status { get; set; } = Constant.STATUS_NEW;
        public DateTime Time { get; set; } = DateTime.Now;
        public DateTime Create { get; set; } = DateTime.Now;
        public DateTime Update { get; set; } = DateTime.Now;
        public int CustomerId { get; set; }
        public User Customer { get; set; }
        public int StylistId { get; set; }
        public User Stylist { get; set; }
        public ICollection<Service> Services { get; set; }
    }
}
