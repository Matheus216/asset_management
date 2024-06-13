using MongoDB.Bson.Serialization.Attributes;

namespace Asset.Management.Domain.Entities;


public class Product
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set;}
    public string Description { get; set; }
    public DateTime Vencimento { get; set; }
}