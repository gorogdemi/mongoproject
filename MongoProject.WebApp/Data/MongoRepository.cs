using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoProject.WebApp.Data.Models;
using MongoProject.WebApp.Utilities;

namespace MongoProject.WebApp.Data
{
    public class MongoRepository : IRepository
    {
        private readonly IMongoDatabase _database;

        public MongoRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task AddComponentAsync(Component component) => await _database.GetCollection<Component>("Components").InsertOneAsync(component);
        public async Task DeleteComponentAsync(Component component) => await _database.GetCollection<Component>("Components").FindOneAndDeleteAsync(x => x.Id == component.Id);

        public async Task<Component> FindComponentAsync(string id, ComponentType type)
        {
            var component = type switch
            {
                ComponentType.BLDCMotor => await (await _database.GetCollection<BLDCMotor>("Components").FindAsync(x => x.Id == id)).FirstOrDefaultAsync(),
                ComponentType.Bolt => await (await _database.GetCollection<Bolt>("Components").FindAsync(x => x.Id == id)).FirstOrDefaultAsync(),
                ComponentType.Microcontroller => await (await _database.GetCollection<Microcontroller>("Components").FindAsync(x => x.Id == id)).FirstOrDefaultAsync(),
                ComponentType.Propeller => await (await _database.GetCollection<Propeller>("Components").FindAsync(x => x.Id == id)).FirstOrDefaultAsync(),
                ComponentType.StepperMotor => await (await _database.GetCollection<StepperMotor>("Components").FindAsync(x => x.Id == id)).FirstOrDefaultAsync(),
                _ => await (await _database.GetCollection<Component>("Components").FindAsync(x => x.Id == id)).FirstOrDefaultAsync()
            };
            return component;
        }

        public async Task<PaginatedList<Component>> GetAllComponentsPaginatedAsync(string searchString, string sortOrder, int pageIndex, int pageSize)
        {
            searchString ??= string.Empty;
            var components = _database.GetCollection<Component>("Components").AsQueryable().Where(x => x.Name.ToLower().Contains(searchString.ToLower()));

            components = sortOrder switch
            {
                "Name" => components.OrderBy(x => x.Name),
                "NameDesc" => components.OrderByDescending(x => x.Name),
                "Type" => components.OrderBy(x => x.Type),
                "TypeDesc" => components.OrderByDescending(x => x.Type),
                "Quantity" => components.OrderBy(x => x.Quantity),
                "QuantityDesc" => components.OrderByDescending(x => x.Quantity),
                _ => components.OrderBy(x => x.Name),
            };

            components = components.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            var componentList = await components.ToListAsync();
            return new PaginatedList<Component>(componentList, await GetComponentCount(), pageIndex, pageSize);
        }

        public async Task UpdateComponentAsync(Component componentToUpdate)
            => await _database.GetCollection<Component>("Components").FindOneAndReplaceAsync(x => x.Id == componentToUpdate.Id, componentToUpdate);

        public async Task<int> GetComponentCount() => (int)await _database.GetCollection<Component>("Components").CountDocumentsAsync(x => true);
    }
}
