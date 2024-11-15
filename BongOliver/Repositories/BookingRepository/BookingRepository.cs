using BongOliver.Constants;
using BongOliver.Models;
using Microsoft.EntityFrameworkCore;

namespace BongOliver.Repositories.BookingRepository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly DataContext _context;

        public BookingRepository(DataContext context)
        {
            _context = context;
        }
        public void CreateBooking(Booking createBooking)
        {
            _context.Bookings.Add(createBooking);
        }

        public void DeleteBooking(Booking booking)
        {
            _context.Bookings.Remove(booking);
        }

        public Booking GetBookingById(int id)
        {
            return _context.Bookings
                .Include(b => b.Customer).ThenInclude(c => c.Role)
                .Include(b => b.Stylist).ThenInclude(s => s.Role)
                .Include(b => b.Services).ThenInclude(s => s.Category)
                .FirstOrDefault((b => b.Id == id));
        }

        public List<Booking> GetBookings(ref int total, int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID")
        {
            var query = _context.Bookings
                .Include(b => b.Customer).ThenInclude(c => c.Role)
                .Include(b => b.Stylist).ThenInclude(s => s.Role)
                .Include(b => b.Services).ThenInclude(s => s.Category)
                .AsQueryable();

            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(s => s.Comment.ToLower().Contains(key.ToLower()));
            }

            switch (sortBy)
            {
                case "TIME":
                    query = query.OrderBy(u => u.Time);
                    break;
                case "CREATE":
                    query = query.OrderByDescending(u => u.Create);
                    break;
                default:
                    query = query.OrderBy(u => u.Id);
                    break;
            }

            total = query.Count();

            if (page == null || pageSize == null || sortBy == null) { return query.ToList(); }
            else
                return query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value).ToList();
        }

        public int GetTotal()
        {
            return _context.Bookings.Count();
        }

        public bool IsSaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void UpdateBooking(Booking booking)
        {
            _context.Entry(booking).State = EntityState.Modified;
        }
    }
}
