
using Asset.Management.Domain.Utils;
using MongoDB.Bson.Serialization.Attributes;

namespace Asset.Management.Domain.Entities;

public class Transaction
{
    public Transaction()
    {
        this.Id = string.Empty;
    }

    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; }
    public string ProductId { get; set; }
    public decimal Value { get; set; }
    public TransactionTypeEnum TypeTransaction { get; set; }
}