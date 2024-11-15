using BongOliver.Constants;
using System.ComponentModel.DataAnnotations;

namespace BongOliver.DTOs.Booking
{
    public class CreateBookingDTO
    {
        public string Comment { get; set; }
        public int Status { get; set; }
        public DateTime Time { get; set; }
        public int Discount { get; set; }
        public int CustomerId { get; set; }
        public int StylistId { get; set; }
        public List<int> ServiceIds { get; set; }
    }
}
