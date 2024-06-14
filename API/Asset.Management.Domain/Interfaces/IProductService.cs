using Asset.Management.Domain.Entities;
using Asset.Management.Domain.DTOs;

namespace Asset.Management.Domain.Interfaces;

public interface IProductService
{
    Task<Result<IEnumerable<Product>>> GetListAsync();
    Task<Result<List<Product>>> GetListCloseExpirationAsync(int daysToExpiration);
    Task<Result<Product>> GetById(string id);
}