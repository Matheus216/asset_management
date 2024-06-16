using Asset.Management.Domain.Entities;
using Asset.Management.Domain.DTOs;
using Asset.Management.Domain.Utils;

namespace Asset.Management.Domain.Interfaces;

public interface IProductService
{
    Task<Result<IEnumerable<Product>>> GetListAsync();
    Task<Result<List<Product>>> GetListCloseExpirationAsync(int daysToExpiration);
    Task<Result<Product>> GetByIdAsync(string id);
    ///<summary>
    /// Método para recalcular a quantidade disponíveis de ativos
    ///</summary>
    Task<Result<Product>> RecalculateAllowQuantity(TransactionTypeEnum transactionType, int quantity, Product product);
}