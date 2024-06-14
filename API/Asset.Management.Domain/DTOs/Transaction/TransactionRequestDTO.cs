using Asset.Management.Domain.Utils;

namespace Asset.Management.Domain.DTOs.Transaction;

public class TransactionRequestDTO
{
    public TransactionRequestDTO()
    {
        this.ProductId = "";
        this.Quantity = 0;
        this.ClientId = "";
    }

    public string ProductId { get; set; }
    public int Quantity { get; set; }
    public string ClientId { get; set; }
    public TransactionTypeEnum TypeTransaction { get; private set; }


    public List<string> ValidateStructureRequest()
    {
        var messages = new List<string>();
        if (string.IsNullOrEmpty(ProductId))
            messages.Add("Produto é obrigatório para transação");

        if (Quantity <= 0 )
            messages.Add($"Deve-se {(TypeTransaction == TransactionTypeEnum.Sale ? "vender" : "comprar")} um ou mais ativos");

        if (string.IsNullOrEmpty(ClientId))
            messages.Add("Cliente Obrigatório");

        return messages;
    }
}