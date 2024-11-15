using BongOliver.DTOs.Booking;
using BongOliver.Services.BookingService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BongOliver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public IActionResult GetRoles(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID")
        {
            var res = _bookingService.GetBookings(page, pageSize, key, sortBy);
            return StatusCode(res.Code, res);
        }
        [HttpPost]
        public IActionResult CreateBooking(CreateBookingDTO createBookingDTO)
        {
            var res = _bookingService.CreateBooking(createBookingDTO);
            return StatusCode(res.Code, res);
        }
        [HttpGet("{id}")]
        public IActionResult GetBookingById(int id)
        {
            var res = _bookingService.GetBookingById(id);
            return StatusCode(res.Code, res);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBooking(int id, UpdateBookingDTO updateBookingDTO)
        {
            var res = _bookingService.UpdateBooking(id, updateBookingDTO);
            return StatusCode(res.Code, res);
        }
    }
}
