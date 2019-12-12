using System.Threading.Tasks;
using MongoProject.WebApp.Data.Models;
using MongoProject.WebApp.Utilities;

namespace MongoProject.WebApp.Data
{
    public interface IRepository
    {
        Task UpdateComponentAsync(Component componentToUpdate);
        Task<Component> FindComponentAsync(string id, ComponentType type);
        Task<PaginatedList<Component>> GetAllComponentsPaginatedAsync(string searchString, string sortOrder, int pageIndex, int pageSize);
        Task AddComponentAsync(Component component);
        Task DeleteComponentAsync(Component component);
        Task<int> GetComponentCount();
        Task AddKitAsync(Kit kit);
        Task<Kit> FindKitAsync(string id);
        Task UpdateKitAsync(Kit kitToUpdate);
        Task DeleteKitAsync(Kit kit);
        Task<PaginatedList<Kit>> GetAllKitsPaginatedAsync(string currentFilter, string currentSort, int v, int pageSize);
        Task<int> GetKitCount();
    }
}
