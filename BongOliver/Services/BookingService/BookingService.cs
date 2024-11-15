using BongOliver.Constants;
using BongOliver.DTOs.Booking;
using BongOliver.DTOs.Response;
using BongOliver.DTOs.Role;
using BongOliver.Models;
using BongOliver.Repositories.BookingRepository;
using BongOliver.Repositories.ServiceRepository;
using BongOliver.Repositories.UserRepository;

namespace BongOliver.Services.BookingService
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IServiceRepository _serviceRepository;
        public BookingService(IBookingRepository bookingRepository, IUserRepository userRepository, IServiceRepository serviceRepository)
        {
            _bookingRepository = bookingRepository;
            _userRepository = userRepository;
            _serviceRepository = serviceRepository;
        }

        public ResponseDTO CreateBooking(CreateBookingDTO createBookingDTO)
        {
            var customer = _userRepository.GetUserById(createBookingDTO.CustomerId);
            if (customer == null) return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Customer không tồn tại!" };

            var stylist = _userRepository.GetUserById(createBookingDTO.StylistId);
            if (stylist == null) return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Stylist không tồn tại!" };

            var booking = new Booking()
            {
                Comment = createBookingDTO.Comment,
                Status = createBookingDTO.Status,
                Time = createBookingDTO.Time,
                Discount = createBookingDTO.Discount,
                CustomerId = createBookingDTO.CustomerId,
                StylistId = createBookingDTO.StylistId,
            };

            foreach (var serviceId in createBookingDTO.ServiceIds)
            {
                var service = _serviceRepository.GetServiceById(serviceId);

                if (service != null) booking.Services.Add(service);
                else return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Một trong số các Services không tồn tại!" };
            }

            _bookingRepository.CreateBooking(booking);

            if (_bookingRepository.IsSaveChanges()) return new ResponseDTO() { Message = "Tạo thành công" };
            else return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Tạo thất bại" };
        }

        public ResponseDTO DeleteBooking(int id)
        {
            var booking = _bookingRepository.GetBookingById(id);
            if (booking == null) return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Booking không tồn tại!" };

            booking.IsDelete = AppConst.DELETE;
            booking.Update = DateTime.Now;

            _bookingRepository.UpdateBooking(booking);
            if (_bookingRepository.IsSaveChanges())
                return new ResponseDTO() { Message = "Cập nhật thành công" };
            else
                return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Cập nhật thất bại" };
        }

        public ResponseDTO GetBookingById(int id)
        {
            var booking = _bookingRepository.GetBookingById(id);
            if (booking == null) return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Booking không tồn tại!" };
            else return new ResponseDTO() { Data = new BookingDTO(booking) };
        }

        public ResponseDTO GetBookings(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID")
        {
            var total = _bookingRepository.GetTotal();

            if (total == 0) return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Không có Booking nào" };

            var bookings = _bookingRepository.GetBookings(ref total, page, pageSize, key, sortBy);

            return new ResponseDTO()
            {
                Data = bookings.Select(b => new BookingDTO(b)).ToList(),
                Total = total
            };
        }

        public ResponseDTO UpdateBooking(int id, UpdateBookingDTO updateBookingDTO)
        {
            var booking = _bookingRepository.GetBookingById(id);
            if (booking == null) return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Booking không tồn tại!" };

            booking.Comment = updateBookingDTO.Comment;
            booking.Status = updateBookingDTO.Status;
            booking.Time = updateBookingDTO.Time;
            booking.Discount = updateBookingDTO.Discount;
            booking.Update = DateTime.Now;
            booking.Services = new List<Service>();

            foreach (var serviceId in updateBookingDTO.ServiceIds)
            {
                var service = _serviceRepository.GetServiceById(serviceId);

                if (service != null) booking.Services.Add(service);
                else return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Một trong số các Services không tồn tại!" };
            }

            _bookingRepository.UpdateBooking(booking);

            if (_bookingRepository.IsSaveChanges()) return new ResponseDTO() { Message = "Cập nhật thành công" };
            else return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Cập nhật thất bại" };
        }
    }
}
