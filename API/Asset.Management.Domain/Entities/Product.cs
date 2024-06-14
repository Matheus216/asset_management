using MongoDB.Bson.Serialization.Attributes;

namespace Asset.Management.Domain.Entities;


public class Product
{
    public Product()
    {
        this.Id = "";
    }

    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set;}
    public string Description { get; set; }
    public string Code { get; set; }
    public DateTime ExpirationDate { get; set; }
    public Decimal Price { get; set; }
    public int AvailableQuantity { get; set; }
}