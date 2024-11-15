using BongOliver.Models;

namespace BongOliver.Repositories.BookingRepository
{
    public interface IBookingRepository
    {
        List<Booking> GetBookings(ref int total, int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID");
        Booking GetBookingById(int id);
        void UpdateBooking(Booking booking);
        void DeleteBooking(Booking booking);
        void CreateBooking(Booking createBooking);
        int GetTotal();
        bool IsSaveChanges();
    }
}
