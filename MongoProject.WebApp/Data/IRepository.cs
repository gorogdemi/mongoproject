using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoProject.WebApp.Data
{
    public interface IRepository
    {
        Task UpdateComponentAsync(Component componentToUpdate);
        Task<Component> FindComponentAsync(string id);
        Task<List<Component>> GetAllComponentsAsync();
    }
}
