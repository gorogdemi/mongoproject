using MongoDB.Bson.Serialization.Attributes;

namespace MongoProject.WebApp.Data
{
    public class Component
    {
        [BsonId]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public double Size { get; set; }

        public int Quantity { get; set; }
    }
}
