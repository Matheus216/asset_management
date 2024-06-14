using Asset.Management.Domain.Interfaces;
using Asset.Management.Domain.Entities;
using Asset.Management.Domain.DTOs;

namespace Asset.Management.Domain.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<IEnumerable<Product>>> GetListAsync()
    {
        var response = await _productRepository.GetListAsync(); 
        return response; 
    }

    public async Task<Result<Product>> GetById(string id)
    {
        var response = await _productRepository.GetByIdAsync(id); 
        return response; 
    }

    public async Task<Result<List<Product>>> GetListCloseExpirationAsync(int daysToExpiration)
    {
        var response = await _productRepository.GetProductsToExpirationAsync(daysToExpiration); 

        if (!(response?.Success ?? false))
            return response ?? new Result<List<Product>>("Erro interno"); 

        return response;
    }

}