using Asset.Management.Domain.Entities;
using Asset.Management.Domain.Interfaces;

namespace Asset.Management.Domain.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> CreateAsync(Product product)
    {
        var data = await _productRepository.Insert(product);

        return data.Data ?? new Product();
    }
}