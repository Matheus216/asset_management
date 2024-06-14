using Asset.Management.Domain.DTOs;
using Asset.Management.Domain.Entities;

namespace Asset.Management.Domain.Interfaces;

public interface IProductRepository : IBaseRepository<Product>
{
   Task<Result<List<Product>>> GetProductsToExpirationAsync(int daysToExpiration);
}