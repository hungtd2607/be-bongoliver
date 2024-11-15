using BongOliver.Models;
using Microsoft.EntityFrameworkCore;

namespace BongOliver.Repositories.ServiceRepository
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly DataContext _context;
        public ServiceRepository(DataContext context)
        {
            _context = context;
        }
        public void CreateService(Service service)
        {
            _context.Services.Add(service);
        }

        public void DeleteService(Service service)
        {
            _context.Services.Remove(service);
        }

        public Service GetServiceById(int id)
        {
            return _context.Services.Include(s => s.Category).FirstOrDefault(s => s.Id == id);
        }

        public List<Service> GetServices(ref int total, int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID")
        {
            var query = _context.Services.Include(s => s.Category).AsQueryable();

            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(s => s.Name.ToLower().Contains(key.ToLower()));
            }

            query = query.OrderBy(s => s.Id);

            total = query.Count();

            if (page == null || pageSize == null || sortBy == null) { return query.ToList(); }
            else
                return query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value).ToList();
        }

        public int GetTotal()
        {
            return _context.Services.Count();
        }

        public bool IsSaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void UpdateService(Service service)
        {
            _context.Entry(service).State = EntityState.Modified;
        }
    }
}
