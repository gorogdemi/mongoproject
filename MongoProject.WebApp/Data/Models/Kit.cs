using System.Collections.Generic;
using System.ComponentModel;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoProject.WebApp.Data.Models
{
    public class Kit
    {
        [BsonId]
        [DisplayName("ID")]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Price { get; set; }

        public List<Component> Components { get; set; }
    }
}
