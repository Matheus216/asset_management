using Asset.Management.Domain.Entities;

namespace Asset.Management.Domain.Interfaces;

public interface IProductService
{
    Task<Product> CreateAsync(Product product);
}