using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Asset.Management.Domain.Interfaces;
using Asset.Management.Domain.Entities;
using Asset.Management.Domain.Utils;
using Asset.Management.Domain.DTOs;
using System.Text.Json;

namespace Asset.Management.Domain.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IDistributedCache _cache;
    private DistributedCacheEntryOptions _optionsCache;

    public ProductService(IProductRepository productRepository, IDistributedCache cache, IConfiguration configuration)
    {
        var section = configuration.GetSection("redis");
        _productRepository = productRepository;
        _cache = cache;
        _optionsCache = new DistributedCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(int.Parse(section["SlidingExpiratio"] ?? "0")))
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(int.Parse(section["AbsoluteExpiration"] ?? "0")));
    }

    public async Task<Result<IEnumerable<Product>>> GetListAsync()
    {
        var cacheKey = "Products";
        var json = await _cache.GetAsync(cacheKey);

        if (json != null)
        {
            var response = JsonSerializer.Deserialize<List<Product>>(json);
            return new Result<IEnumerable<Product>>(response ?? new List<Product>()); 
        }
        else 
        {
            var responseWithoutCache = await _productRepository.GetListAsync(); 

            if (responseWithoutCache?.Data != null && responseWithoutCache.Data.Any())
            {
                var serializerToCache = JsonSerializer.Serialize(responseWithoutCache.Data);
                await _cache.SetStringAsync(cacheKey,serializerToCache,_optionsCache);
            }

            return responseWithoutCache ?? new Result<IEnumerable<Product>>("Erro na busca"); 
        }
    }

    public async Task<Result<Product>> GetByIdAsync(string id)
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

    public async Task<Result<Product>> RecalculateAllowQuantity(TransactionTypeEnum transactionType, int quantity, Product product)
    {
        if (transactionType == TransactionTypeEnum.Purchase)
            product.AvailableQuantity -= quantity;
        else
            product.AvailableQuantity += quantity;

        var updated = await _productRepository.UpdateAsync(product);

        _cache.Remove("Products");

        return updated;
    }
}