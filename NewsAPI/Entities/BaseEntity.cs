using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using NewsAPI.Entities.Enums;

namespace NewsAPI.Entities
{
    public abstract class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        public bool Deleted { get; set; }
        
        public string Slug { get; set; }
        
        [BsonElement("publishDate")]
        public DateTime PublishDate { get; protected set; }
        
        [BsonElement("status")]
        public Status Status { get; protected set; }
    }
}