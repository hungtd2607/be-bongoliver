using BongOliver.DTOs.Booking;
using BongOliver.DTOs.Response;
using BongOliver.DTOs.Service;

namespace BongOliver.Services.BookingService
{
    public interface IBookingService
    {
        ResponseDTO GetBookings(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID");
        ResponseDTO GetBookingById(int id);
        ResponseDTO UpdateBooking(int id, UpdateBookingDTO updateBookingDTO);
        ResponseDTO DeleteBooking(int id);
        ResponseDTO CreateBooking(CreateBookingDTO createBookingDTO);
    }
}
