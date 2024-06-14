
using MongoDB.Bson.Serialization.Attributes;
using Asset.Management.Domain.Utils;

namespace Asset.Management.Domain.Entities;

public class Transaction
{
    public Transaction(string productId,
        int quantity, 
        decimal unitValue, 
        decimal totalValue, 
        TransactionTypeEnum transactionType, 
        DateTime createdDate,
        string clientId
    )
    {
        this.Id = string.Empty;
        this.ProductId = productId;
        this.Quantity = quantity;
        this.UnitValue = unitValue;
        this.TotalValue = totalValue;
        this.TransactionType = transactionType;
        this.CreatedDate = createdDate;
        this.ClientId = clientId;
    }

    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; private set; }
    public string ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitValue { get; private set; }
    public decimal TotalValue { get; private set; }
    public TransactionTypeEnum TransactionType { get; private set; }
    public DateTime CreatedDate { get; set; }
    public string ClientId { get; private set; }
}