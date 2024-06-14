using System.Collections;
using Asset.Management.Domain.DTOs;
using Asset.Management.Domain.Entities;
using Asset.Management.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Asset.Management.Infra.Repository;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(IConfiguration configuration) 
        : base(configuration, nameof(Product))
    {
    }

    public async Task<Result<List<Product>>> GetProductsToExpirationAsync(int daysToExpiration)
    {
        try
        {
            var filterDaysAgo = DateTime.Now.AddDays(daysToExpiration);

            var filter = Builders<Product>
                .Filter
                .And (
                    Builders<Product>.Filter.Gte(x => x.ExpirationDate, DateTime.Now),
                    Builders<Product>.Filter.Lte(x => x.ExpirationDate, filterDaysAgo)
                );

            var response = await _collection.Find(filter).ToListAsync();
            return new Result<List<Product>>(response);
        }
        catch (Exception ex)
        {
            return new Result<List<Product>>
            (
                ex.Message ?? 
                "Problemas para encontrar produtos proximos de expiração"
            );
        }
    }
}