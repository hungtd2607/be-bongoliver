using BongOliver.Models;

namespace BongOliver.Repositories.ServiceRepository
{
    public interface IServiceRepository
    {
        List<Service> GetServices(ref int total, int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID");
        Service GetServiceById(int id);
        void UpdateService(Service service);
        void DeleteService(Service service);
        void CreateService(Service service);
        int GetTotal();
        bool IsSaveChanges();
    }
}
