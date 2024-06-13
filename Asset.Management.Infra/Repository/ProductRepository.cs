using Asset.Management.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Asset.Management.Infra.Repository;


public class ProductRepository : BaseRepository<Product>
{
    public ProductRepository(IConfiguration configuration) 
        : base(configuration, "Product")
    {
    }
}