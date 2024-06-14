using Asset.Management.Domain.Interfaces;
using Asset.Management.Domain.Services;
using Asset.Management.Infra.Repository;

namespace Asset.Management.API.Configuration;


public static class BuilderServiceConfigurationExtensions
{
    public static void ConfigurationDI(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IProductRepository, ProductRepository>();
        builder.Services.AddSingleton<IProductService, ProductService>();
        builder.Services.AddSingleton<ITransactionService, TransactionService>();
        builder.Services.AddSingleton<ITransactionRepository, TransactionRepository>();
    }
}