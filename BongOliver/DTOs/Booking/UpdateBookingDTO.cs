namespace BongOliver.DTOs.Booking
{
    public class UpdateBookingDTO
    {
        public string Comment { get; set; }
        public int Status { get; set; }
        public DateTime Time { get; set; }
        public int Discount { get; set; }
        public List<int> ServiceIds { get; set; }
    }
}
