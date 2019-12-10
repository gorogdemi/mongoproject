using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace MongoProject.WebApp.Data
{
    public class MongoRepository : IRepository
    {
        private readonly IMongoDatabase _database;

        public MongoRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Component> FindComponentAsync(string id)
        {
            var component = await _database.GetCollection<Component>("Components").FindAsync(x => x.Id == id);
            return await component.FirstOrDefaultAsync();
        }

        public async Task<List<Component>> GetAllComponentsAsync()
        {
            var components = await _database.GetCollection<Component>("Components").FindAsync(x => true);
            return await components.ToListAsync();
        }

        public async Task UpdateComponentAsync(Component componentToUpdate)
            => await _database.GetCollection<Component>("Components").FindOneAndReplaceAsync(x => x.Id == componentToUpdate.Id, componentToUpdate);
    }
}
