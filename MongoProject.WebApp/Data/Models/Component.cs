using System.ComponentModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoProject.WebApp.Data.Models
{
    [BsonIgnoreExtraElements()]
    public class Component
    {
        [BsonId]
        [DisplayName("ID")]
        public string Id { get; set; }

        public string Name { get; set; }

        [BsonRepresentation(BsonType.String)]
        public ComponentType Type { get; set; }

        public int Quantity { get; set; }
    }
}
