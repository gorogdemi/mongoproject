using System.Threading.Tasks;
using MongoProject.WebApp.Data.Models;
using MongoProject.WebApp.Utilities;

namespace MongoProject.WebApp.Data
{
    public interface IRepository
    {
        Task UpdateComponentAsync(Component componentToUpdate);
        Task<Component> FindComponentAsync(string id);
        Task<PaginatedList<Component>> GetAllComponentsPaginatedAsync(string searchString, string sortOrder, int pageIndex, int pageSize);
        Task AddComponentAsync(Component component);
        Task DeleteComponentAsync(Component component);
        Task<int> GetComponentCount();
    }
}
